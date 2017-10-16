using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using System.Text;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Params;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Shared.Utility;

public partial class Pages_TaskMaster_GeneralTaskImport : PageBaseRecForm
{
    bool isError = false;
    bool isExportPDF;
    bool isExportExcel;
    List<UserHdrInfo> oListUserHdrInfo;
    #region "Public Methods"
    public override string GetMenuKey()
    {
        return "TaskViewer";
    }

    public string ReturnUrl
    {
        get
        {
            return ViewState["returnUrl"].ToString();
        }
        set
        {
            ViewState["returnUrl"] = value;
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            RefreshDataImportStatusGrid(true);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        oListUserHdrInfo = CacheHelper.SelectAllUsersForCurrentCompany();
        PopulateItemsOnPage();
        if (Request.UrlReferrer == null)
        {
            //Handle the case where the page is requested directly
            //throw new Exception("This page has been called without a referring page");
        }
        else
        {
            if (!IsPostBack)
            {
                string isRedirectedFromStatusPage = Convert.ToString(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE]);

                if (string.IsNullOrEmpty(isRedirectedFromStatusPage))
                {
                    ReturnUrl = Request.UrlReferrer.PathAndQuery;
                    Session["RedirectUrlForImportPage"] = ReturnUrl;
                }
                else
                {
                    string confirmStatusMessageID = Convert.ToString(Request.QueryString[QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE]);
                    ReturnUrl = Convert.ToString(Session["RedirectUrlForImportPage"]);

                    if (!string.IsNullOrEmpty(confirmStatusMessageID) && Convert.ToInt32(confirmStatusMessageID) == 3)
                    {
                        ViewState["ValidDataTable"] = Session["DataTableForImport"];
                        ImportAllUploadData();
                    }
                    else if (!string.IsNullOrEmpty(confirmStatusMessageID) && Convert.ToInt32(confirmStatusMessageID) == 2)
                    {
                        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                        oMasterPageBase.ShowConfirmationMessage(2399);
                    }
                }

                //Setting allowed file extensions.
                this.radFileUpload.AllowedFileExtensions = DataImportHelper.GetAllowedFileExtensions();

                //Set allowed file size in bytes
                this.radFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);
                //setting error messages to be shown at runtime
                this.cvFileUpload.Attributes.Add("fileNameErrorMessage", LanguageUtil.GetValue(5000035));
                this.cvFileUpload.Attributes.Add("fileExtensionErrorMessage", LanguageUtil.GetValue(5000036));

                //Hide all Preview Grids
                pnlImportGrid.Visible = false;
                rgImportList.Visible = false;
                RefreshDataImportStatusGrid(true);

            }



        }
    }
    /// <summary>
    /// Insert records for failure message to database
    /// Table affected: DataIMportHDR, DataImportFailureMessage
    /// </summary>
    protected void cvFileUpload_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            UploadedFile validFile = radFileUpload.UploadedFiles[0];
            if (validFile.FileName.Length > WebConstants.MAX_FILE_NAME_LENGTH_FOR_ATTACHMENT)
            {
                args.IsValid = false;
                this.cvFileUpload.ErrorMessage = string.Format(LanguageUtil.GetValue(5000347), WebConstants.MAX_FILE_NAME_LENGTH_FOR_ATTACHMENT); //File name is too long. Maximum allowed length is {0} characters.
            }
        }

        if (this.radFileUpload.InvalidFiles.Count > 0)
        {
            //Set default error message
            this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000037);

            //Check if there is enough space for this company to save this file
            UploadedFile invalidFile = radFileUpload.InvalidFiles[0];
            if (invalidFile.ContentLength > DataImportHelper.GetAvailableDataStorageByCompanyId(SessionHelper.CurrentCompanyID.Value))
            {
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000034);
                //throw new ARTException(5000034);//File size exceeds available space
            }
            //File size exceeds maximum file upload size.
            if (invalidFile.ContentLength > DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value))
            {
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000038);//File size is more than maximum allowed file size
            }

           
            //file extension validation 
            string[] arr = DataImportHelper.GetAllowedFileExtensions();
            string invalidFileExtension = invalidFile.GetExtension();
            bool valid = false;
            foreach (string str in arr)
            {
                if (str == invalidFileExtension)
                {
                    valid = true;
                    break;
                }
            }
            if (!valid)
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000036);//Invalid file extension 
            args.IsValid = false;

            //disable import and cancel buttons and remove grid
            pnlImportGrid.Visible = false;
        }

    }

    #region "Control Event Handlers"
    protected void btnImportAll_Click(object sender, EventArgs e)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            StringBuilder oSBErrors = null;
            short importType = -1;
            DataTable dt = null;
            //DataTable dtPeriodEndDates;
            try
            {
                UploadedFile validFile = radFileUpload.UploadedFiles[0];
                ImportFileAttributes ImportFile;
                importType = (short)ARTEnums.DataImportType.GeneralTaskImport;
                //Get folder path and name as per companyid and import type
                targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                filePath = Path.Combine(targetFolder, fileName);

                //save file
                validFile.SaveAs(filePath, true);

                //Save file attributes, they will be used at the time of importing file records.
                ImportFile.FileOriginalName = validFile.GetName();
                ImportFile.FileModifiedName = fileName;
                ImportFile.FilePhysicalPath = filePath;
                ImportFile.FileSize = validFile.ContentLength;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                {
                    string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                    //Once file is saved, get records in a DataTable.
                    //Show records in grid.
                    oSBErrors = new StringBuilder();
                    dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);

                    if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                        Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                    {
                        DataTable dtUploadData = new DataTable();
                        dtUploadData = dt.Clone();
                        DataRow[] drValidRows = dt.Select("IsValidRow = true");
                        if (drValidRows != null && drValidRows.Count() > 0)
                        {
                            dtUploadData = drValidRows.CopyToDataTable();
                        }
                        if (dt.Select("IsValidRow = false").Count() > 0)
                        {
                            Session["DataTableForImport"] = dtUploadData;
                            Session["ErrorMessage"] = oSBErrors;
                            //Response.Redirect(GetUrlForRecItemImportStatusPage());
                            SessionHelper.RedirectToUrl(GetUrlForRecItemImportStatusPage());
                            return;
                        }
                        else //(dtUploadData.Rows.Count > 0)
                        {
                            ViewState["ValidDataTable"] = dtUploadData;
                            ImportAllUploadData();
                        }

                    }
                    //if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                    //    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblError") as ExLabel, Helper.GetLabelIDValue(5000043));
                    //{
                    //    rgImportList.PageSize = Convert.ToInt32(hdnNewPageSize.Value);
                    //    rgImportList.VirtualItemCount = dt.Rows.Count;
                    //    ViewState["DataTable"] = dt;
                    //    this.ShowGridAndImportButtons(dt);
                    //}
                }
            }
            catch (ARTException ex)
            {
                switch (ex.ExceptionPhraseID)
                {
                    //Invalid File. All Mandatory fields not present
                    //Save import and failure message to database
                    case 5000037:
                        InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure, 0);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                    //Invalid Data. 
                    //Save import and failure message to database
                    case 5000047:
                        InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure, 0);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                }
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            finally
            {
                this.radFileUpload.UploadedFiles.Clear();
            }
        }
        else
        {
            pnlImportGrid.Visible = false;
        }
    }

    private void ImportAllUploadData()
    {
        try
        {
            object obj = ViewState["ValidDataTable"];
            DataTable objectCollection = null;
            if (obj != null)
            {
                objectCollection = (DataTable)obj;
            }
            int DataImportID = 0;
            DataImportID = InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(1744), (short)ARTEnums.DataImportType.GeneralTaskImport, (short)WebEnums.DataImportStatus.Success, objectCollection.Rows.Count);
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            int? TempTaskSequenceNumber = 0;


            if (objectCollection != null)
            {
                foreach (DataRow rowUpload in objectCollection.Rows)
                {
                    TempTaskSequenceNumber += 1;
                    TaskHdrInfo oTaskHdrInfo = this.GetTaskItemDetailForImportALL(rowUpload, TempTaskSequenceNumber);
                    oTaskHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                    oTaskHdrInfo.DateAdded = System.DateTime.Now;
                    oTaskHdrInfo.DataImportID = DataImportID;
                    oTaskHdrInfoList.Add(oTaskHdrInfo);
                }
            }


            List<AttachmentInfo> oAttachmentInfoList = new List<AttachmentInfo>();
            DataTable dtSequence = TaskMasterHelper.AddTask(oTaskHdrInfoList, SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault(), SessionHelper.CurrentUserLoginID, System.DateTime.Now, oAttachmentInfoList);
            //Update TaskNumber
            if (dtSequence != null && dtSequence.Rows.Count > 0 && oTaskHdrInfoList.Count > 0)
            {
                for (int i = 0; i < oTaskHdrInfoList.Count; i++)
                {
                    oTaskHdrInfoList[i].TaskNumber = dtSequence.Rows[i][1].ToString();
                }

            }
            TaskMasterHelper.RaiseTaskAlert(oTaskHdrInfoList);
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.ShowConfirmationMessage(1608);
            pnlImportGrid.Visible = false;
            Reset();
        }
        catch (Exception ex)
        {

            Helper.LogException(ex);
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            StringBuilder oSBErrors = null;
            short importType = -1;
            DataTable dt = null;
            //DataTable dtPeriodEndDates;
            try
            {
                UploadedFile validFile = radFileUpload.UploadedFiles[0];
                ImportFileAttributes ImportFile;
                importType = (short)ARTEnums.DataImportType.GeneralTaskImport;
                //Get folder path and name as per companyid and import type
                targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                filePath = Path.Combine(targetFolder, fileName);


                //save file
                validFile.SaveAs(filePath, true);

                //Save file attributes, they will be used at the time of importing file records.
                ImportFile.FileOriginalName = validFile.GetName();
                ImportFile.FileModifiedName = fileName;
                ImportFile.FilePhysicalPath = filePath;
                ImportFile.FileSize = validFile.ContentLength;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                {
                    string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                    //Once file is saved, get records in a DataTable.
                    //Show records in grid.
                    oSBErrors = new StringBuilder();
                    dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                    if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                        Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblError") as ExLabel, Helper.GetLabelIDValue(5000043));
                    {
                        rgImportList.PageSize = Convert.ToInt32(hdnNewPageSize.Value);
                        rgImportList.VirtualItemCount = dt.Rows.Count;
                        ViewState["DataTable"] = dt;
                        this.ShowGridAndImportButtons(dt);
                    }
                }
            }
            catch (ARTException ex)
            {
                switch (ex.ExceptionPhraseID)
                {
                    //Invalid File. All Mandatory fields not present
                    //Save import and failure message to database
                    case 5000037:
                        InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure, 0);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                    //Invalid Data. 
                    //Save import and failure message to database
                    case 5000047:
                        InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure, 0);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                }
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            finally
            {
                this.radFileUpload.UploadedFiles.Clear();
            }
        }
        else
        {
            pnlImportGrid.Visible = false;
        }
    }

    protected void btnPageCancel_Click(object sender, EventArgs e)
    {
        // Response.Redirect(ReturnUrl, true);
        //Response.Redirect("TaskViewer.aspx", true);
        SessionHelper.RedirectToUrl("TaskViewer.aspx");
        return;
    }

    protected void rgImportList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        Sel.Value = string.Empty;
    }

    protected void rgImportList_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
            if (rgImportList.AllowCustomPaging)
            {
                BindPageSizeGrid(oRadComboBox);
                oRadComboBox.SelectedValue = hdnNewPageSize.Value.ToString();
                oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "');");
                oRadComboBox.Visible = true;
            }
            else
            {
                Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                pnlPageSizeDDL.Visible = false;
            }
            Control numericPagerControl = gridPager.GetNumericPager();
            Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
            placeHolder.Controls.Add(numericPagerControl);

        }

    }

    protected void rgImportList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            ExLabel lblTaskListName = (ExLabel)e.Item.FindControl("lblTaskListName");
            ExLabel lblTaskName = (ExLabel)e.Item.FindControl("lblTaskName");
            ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
            //ExLabel lblStartDate = (ExLabel)e.Item.FindControl("lblStartDate");
            ExLabel lblTaskDueDate = (ExLabel)e.Item.FindControl("lblTaskDueDate");
            ExLabel lblTaskAsignedto = (ExLabel)e.Item.FindControl("lblTaskAsignedto");
            ExLabel lblApprover = (ExLabel)e.Item.FindControl("lblApprover");
            ExLabel lblAssigneeDueDate = (ExLabel)e.Item.FindControl("lblAssigneeDueDate");
            //ExLabel lblApproverDueDate = (ExLabel)e.Item.FindControl("lblApproverDueDate");
            ExLabel lblError = (ExLabel)e.Item.FindControl("lblError");

            CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

            StringBuilder oSBError = new StringBuilder();
            string errorFormat = Helper.GetLabelIDValue(2495);
            List<DateTime> TaskDateList = new List<DateTime>();

            string TaskListName = (string)DataBinder.Eval(e.Item.DataItem, "Task List Name");
            if (!string.IsNullOrEmpty(TaskListName))
            {
                lblTaskListName.Text = (string)DataBinder.Eval(e.Item.DataItem, "Task List Name");
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2584)));
                oSBError.Append("<br/>");
            }

            string TaskName = (string)DataBinder.Eval(e.Item.DataItem, "Task Name");
            if (!string.IsNullOrEmpty(TaskName))
            {
                lblTaskName.Text = (string)DataBinder.Eval(e.Item.DataItem, "Task Name");
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2545)));
                oSBError.Append("<br/>");
            }

            string description = (string)DataBinder.Eval(e.Item.DataItem, "Description");
            if (!string.IsNullOrEmpty(description))
            {
                lblDescription.Text = (string)DataBinder.Eval(e.Item.DataItem, "Description");
            }

            //try
            //{
            //    DateTime StartDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Start Date"));
            //    lblStartDate.Text = Helper.GetDisplayDate(StartDate);
            //    if (StartDate < Convert.ToDateTime(DateTime.Today))
            //    {
            //        isError = true;
            //        oSBError.Append(string.Format(Helper.GetLabelIDValue(5000214), Helper.GetLabelIDValue(1449), Helper.GetLabelIDValue(2062)));
            //        oSBError.Append("<br/>");
            //    }
            //    else
            //        TaskDateList.Add(StartDate);
            //}
            //catch (Exception ex)
            //{
            //    isError = true;
            //    oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1449)));
            //    oSBError.Append("<br/>");
            //}

            string TaskAssignee = (string)DataBinder.Eval(e.Item.DataItem, "TaskAssignee");
            if (!string.IsNullOrEmpty(TaskAssignee))
            {
                lblTaskAsignedto.Text = TaskAssignee;
                UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == TaskAssignee.ToLower()).FirstOrDefault();
                if (oUser == null)
                {
                    isError = true;
                    oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2564)));
                    oSBError.Append("<br/>");

                }
            }

            string TaskApprover = (string)DataBinder.Eval(e.Item.DataItem, "TaskApprover");
            if (!string.IsNullOrEmpty(TaskApprover))
            {
                lblApprover.Text = TaskApprover;
                UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == TaskApprover.ToLower()).FirstOrDefault();
                if (oUser == null)
                {
                    isError = true;
                    oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1132)));
                    oSBError.Append("<br/>");

                }
            }

            if (!string.IsNullOrEmpty(TaskAssignee) && !string.IsNullOrEmpty(TaskApprover))
            {
                if (TaskAssignee.ToLower().Equals(TaskApprover.ToLower()))
                {
                    isError = true;
                    oSBError.Append(string.Format(Helper.GetLabelIDValue(5000354), Helper.GetLabelIDValue(2564), Helper.GetLabelIDValue(1132), LanguageUtil.GetValue(2525)));
                    oSBError.Append("<br/>");
                }
            }
            if (!string.IsNullOrEmpty(TaskApprover))
            {
                try
                {
                    DateTime AssigneeDueDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Assignee Due Date"));
                    lblAssigneeDueDate.Text = Helper.GetDisplayDate(AssigneeDueDate);
                    TaskDateList.Add(AssigneeDueDate);
                }
                catch (Exception)
                {
                    isError = true;
                    oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2567)));
                    oSBError.Append("<br/>");
                }
            }
            //try
            //{
            //    DateTime ApprovalDueDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Approval Due Date"));
            //    lblApproverDueDate.Text = Helper.GetDisplayDate(ApprovalDueDate);
            //    TaskDateList.Add(ApprovalDueDate);
            //}
            //catch (Exception ex)
            //{
            //    isError = true;
            //    oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2568)));
            //    oSBError.Append("<br/>");
            //}
            try
            {
                DateTime TaskDueDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Task Due Date"));
                lblTaskDueDate.Text = Helper.GetDisplayDate(TaskDueDate);
                TaskDateList.Add(TaskDueDate);
            }
            catch (Exception)
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2566)));
                oSBError.Append("<br/>");
            }
            if (Helper.DatesInOrder(TaskDateList))
            {
            }
            else
            {
                string errorMessage = LanguageUtil.GetValue(5000342);
                isError = true;
                oSBError.Append(string.Format(errorMessage, Helper.GetLabelIDValue(2582)));
                oSBError.Append("<br/>");
            }

            lblError.Text = oSBError.ToString();

            if (!string.IsNullOrEmpty(oSBError.ToString()))
            {
                checkBox.Enabled = false;
                Sel.Value += e.Item.ItemIndex.ToString() + ":";
            }
        }
    }

    protected void rgImportList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            int pageIndex = rgImportList.CurrentPageIndex;
            int pageSize = Convert.ToInt32(hdnNewPageSize.Value);
            int count;
            int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
            count = ((((pageIndex + 1) * pageSize) / defaultItemCount) + 1) * defaultItemCount;

            object obj = ViewState["DataTable"];
            if (obj != null)
            {
                DataTable objectCollection = (DataTable)obj;
                if (objectCollection.Rows.Count % defaultItemCount == 0)
                    rgImportList.VirtualItemCount = objectCollection.Rows.Count + 1;
                else
                    rgImportList.VirtualItemCount = objectCollection.Rows.Count;
                rgImportList.MasterTableView.DataSource = objectCollection;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }

    protected void rgImportList_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
        hdnNewPageSize.Value = e.NewPageSize.ToString();

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            int DataImportID = 0;
            DataImportID = InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(1744), (short)ARTEnums.DataImportType.GeneralTaskImport, (short)WebEnums.DataImportStatus.Success, rgImportList.SelectedItems.Count);
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            int? TempTaskSequenceNumber = 0;
            foreach (GridDataItem item in rgImportList.SelectedItems)
            {
                TempTaskSequenceNumber += 1;
                TaskHdrInfo oTaskHdrInfo = this.GetTaskHdrInfo(item, TempTaskSequenceNumber);
                oTaskHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oTaskHdrInfo.DateAdded = System.DateTime.Now;
                oTaskHdrInfo.DataImportID = DataImportID;
                oTaskHdrInfoList.Add(oTaskHdrInfo);
            }
            List<AttachmentInfo> oAttachmentInfoList = new List<AttachmentInfo>();
            DataTable dtSequence = TaskMasterHelper.AddTask(oTaskHdrInfoList, SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault(), SessionHelper.CurrentUserLoginID, System.DateTime.Now, oAttachmentInfoList);
            //Update TaskNumber
            if (dtSequence != null && dtSequence.Rows.Count > 0 && oTaskHdrInfoList.Count > 0)
            {
                for (int i = 0; i < oTaskHdrInfoList.Count; i++)
                {
                    oTaskHdrInfoList[i].TaskNumber = dtSequence.Rows[i][1].ToString();
                }

            }

            // raise alert
            TaskMasterHelper.RaiseTaskAlert(oTaskHdrInfoList);
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.ShowConfirmationMessage(1608);

            pnlImportGrid.Visible = false;
            Reset();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlImportGrid.Visible = false;
    }

    private void PopulateItemsOnPage()
    {
        hlOpenExcelFile.NavigateUrl = "~/Templates/GeneralTaskImport.xls";
        this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.TaskUploadLabelID;
        Helper.SetPageTitle(this, 2593);
        //Helper.ShowInputRequirementSection(this, 1641, 1647);
    }

    private void ShowGridAndImportButtons(DataTable dt)
    {
        rgImportList.Visible = true;
        Sel.Value = string.Empty;
        pnlImportGrid.Visible = true;
        //DataView dv = dt.DefaultView;
        //dv.RowFilter = "IsDuplicate = false";
        //this.rgImportList.DataSource = dv;
        this.rgImportList.DataSource = dt;
        this.rgImportList.DataBind();

        GridColumn oGridColumn = this.rgImportList.Columns.FindByUniqueNameSafe("Error");

        if (isError == false)
        {
            if (oGridColumn != null)
                oGridColumn.Visible = false;
            //this.rgImportList.DataBind();
        }
        else
        {
            if (oGridColumn != null)
                oGridColumn.Visible = true;
        }
    }

    private void DeleteFile(string filePath)
    {
        //Delete the saved file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        //Send exception to user interface
    }
    private int InsertDataImportHdrWithFailureMsg(string failureMsg, short dataImportTypeID, short dataImportStatusID, int RecordsImported)
    {
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.DataImportName = this.txtImportName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = dataImportTypeID;
        oDataImportHrdInfo.DataImportStatusID = dataImportStatusID;
        oDataImportHrdInfo.RecordsImported = RecordsImported;
        if (dataImportTypeID == (short)ARTEnums.DataImportType.GeneralTaskImport)
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;

        oDataImport.InsertDataImportWithFailureMsg(oDataImportHrdInfo, failureMsg, Helper.GetAppUserInfo());

        return oDataImportHrdInfo.DataImportID.GetValueOrDefault();

    }

    private void BindPageSizeGrid(DropDownList DDL)
    {
        String defaultPageSize = AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_PAGE_SIZE);
        String[] PageSize = defaultPageSize.Split(',');
        for (int i = 0; i < PageSize.Length; i++)
        {

            DDL.Items.Add(new ListItem(PageSize[i], PageSize[i]));
        }

    }


    private TaskHdrInfo GetTaskHdrInfo(GridDataItem item, int? TempTaskSequenceNumber)
    {

        ExLabel lblTaskListName = (ExLabel)item.FindControl("lblTaskListName");
        ExLabel lblTaskName = (ExLabel)item.FindControl("lblTaskName");
        ExLabel lblDescription = (ExLabel)item.FindControl("lblDescription");
        // ExLabel lblStartDate = (ExLabel)item.FindControl("lblStartDate");
        ExLabel lblTaskDueDate = (ExLabel)item.FindControl("lblTaskDueDate");
        ExLabel lblTaskAsignedto = (ExLabel)item.FindControl("lblTaskAsignedto");
        ExLabel lblApprover = (ExLabel)item.FindControl("lblApprover");
        ExLabel lblAssigneeDueDate = (ExLabel)item.FindControl("lblAssigneeDueDate");
        ExLabel lblApproverDueDate = (ExLabel)item.FindControl("lblApproverDueDate");


        TaskHdrInfo oTaskHdrInfo = new TaskHdrInfo();
        oTaskHdrInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oTaskHdrInfo.TaskTypeID = 1;
        oTaskHdrInfo.TempTaskSequenceNumber = TempTaskSequenceNumber;
        oTaskHdrInfo.IsActive = true;

        TaskListHdrInfo oTaskListHdrInfo = new TaskListHdrInfo();
        oTaskListHdrInfo.TaskListName = lblTaskListName.Text;
        oTaskHdrInfo.TaskList = oTaskListHdrInfo;

        //Task Name       
        oTaskHdrInfo.TaskName = lblTaskName.Text;
        //Description
        oTaskHdrInfo.TaskDescription = lblDescription.Text;




        //Approver
        int? Approver;
        if (string.IsNullOrEmpty(lblApprover.Text))
            Approver = null;
        else
        {
            UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == lblApprover.Text.ToLower()).FirstOrDefault();
            Approver = oUser.UserID;
        }

        UserHdrInfo oApproverUserHdrInfo = new UserHdrInfo();
        oApproverUserHdrInfo.UserID = Approver;
       // oTaskHdrInfo.Reviewer = oApproverUserHdrInfo;


        //Assigned To
        int? AssignedTo;
        if (string.IsNullOrEmpty(lblTaskAsignedto.Text))
            AssignedTo = SessionHelper.CurrentUserID;
        else
        {
            UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == lblTaskAsignedto.Text.ToLower()).FirstOrDefault();
            AssignedTo = oUser.UserID;
        }
        UserHdrInfo oAssignedUserHdrInfo = new UserHdrInfo();
        oAssignedUserHdrInfo.UserID = AssignedTo;
        //oTaskHdrInfo.AssignedTo = oAssignedUserHdrInfo;


        //Recurrence Type
        TaskRecurrenceTypeMstInfo oTaskRecurr = new TaskRecurrenceTypeMstInfo();
        oTaskRecurr.TaskRecurrenceTypeID = 1;
        oTaskHdrInfo.RecurrenceType = oTaskRecurr;


        //Task Start Date       
        // oTaskHdrInfo.TaskStartDate = Convert.ToDateTime(lblStartDate.Text);
        //Task Due Date
        oTaskHdrInfo.TaskDueDate = Convert.ToDateTime(lblTaskDueDate.Text);
        //Assignee Due Date
        if (!string.IsNullOrEmpty(lblApprover.Text))
            oTaskHdrInfo.AssigneeDueDate = Convert.ToDateTime(lblAssigneeDueDate.Text);
        //Approval Due Date
        //oTaskHdrInfo.ApprovalDueDate = Convert.ToDateTime(lblApproverDueDate.Text);
        return oTaskHdrInfo;
    }

    private void Reset()
    {
        txtImportName.Text = string.Empty;
    }

    private TaskHdrInfo GetTaskItemDetailForImportALL(DataRow drUpload, int? TempTaskSequenceNumber)
    {

        TaskHdrInfo oTaskHdrInfo = new TaskHdrInfo();
        oTaskHdrInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oTaskHdrInfo.TaskTypeID = 1;
        oTaskHdrInfo.TempTaskSequenceNumber = TempTaskSequenceNumber;
        oTaskHdrInfo.IsActive = true;

        TaskListHdrInfo oTaskListHdrInfo = new TaskListHdrInfo();
        oTaskListHdrInfo.TaskListName = drUpload["Task List Name"].ToString();
        oTaskHdrInfo.TaskList = oTaskListHdrInfo;

        //Task Name       
        oTaskHdrInfo.TaskName = drUpload["Task Name"].ToString();
        //Description
        oTaskHdrInfo.TaskDescription = drUpload["Description"].ToString();




        //Approver
        int? Approver;
        if (string.IsNullOrEmpty(drUpload["TaskApprover"].ToString()))
            Approver = null;
        else
        {
            UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == drUpload["TaskApprover"].ToString().ToLower()).FirstOrDefault();
            Approver = oUser.UserID;
        }

        UserHdrInfo oApproverUserHdrInfo = new UserHdrInfo();
        oApproverUserHdrInfo.UserID = Approver;
      //  oTaskHdrInfo.Reviewer = oApproverUserHdrInfo;


        //Assigned To
        int? AssignedTo;
        if (string.IsNullOrEmpty(drUpload["TaskAssignee"].ToString()))
            AssignedTo = SessionHelper.CurrentUserID;
        else
        {
            UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == drUpload["TaskAssignee"].ToString().ToLower()).FirstOrDefault();
            AssignedTo = oUser.UserID;
        }
        UserHdrInfo oAssignedUserHdrInfo = new UserHdrInfo();
        oAssignedUserHdrInfo.UserID = AssignedTo;
     //   oTaskHdrInfo.AssignedTo = oAssignedUserHdrInfo;


        //Recurrence Type
        TaskRecurrenceTypeMstInfo oTaskRecurr = new TaskRecurrenceTypeMstInfo();
        oTaskRecurr.TaskRecurrenceTypeID = 1;
        oTaskHdrInfo.RecurrenceType = oTaskRecurr;


        //Task Start Date       
        //oTaskHdrInfo.TaskStartDate = Convert.ToDateTime(drUpload["Start Date"]);
        //Task Due Date
        oTaskHdrInfo.TaskDueDate = Convert.ToDateTime(drUpload["Task Due Date"]);
        //Assignee Due Date
        if (Approver != null)
            oTaskHdrInfo.AssigneeDueDate = Convert.ToDateTime(drUpload["Assignee Due Date"]);
        ////Approval Due Date
        //oTaskHdrInfo.ApprovalDueDate = Convert.ToDateTime(drUpload["Approval Due Date"]);
        return oTaskHdrInfo;
    }


    private string GetUrlForRecItemImportStatusPage()
    {
        return "TaskImportStatusMessage.aspx";
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            if (radFileUpload.UploadedFiles.Count > 0)
            {
                string filePath = string.Empty;
                string fileName = string.Empty;
                string targetFolder = string.Empty;
                StringBuilder oSBErrors = null;
                short importType = -1;
                try
                {
                    UploadedFile validFile = radFileUpload.UploadedFiles[0];
                    ImportFileAttributes ImportFile;
                    importType = (short)ARTEnums.DataImportType.GeneralTaskImport;
                    //Get folder path and name as per companyid and import type
                    targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                    MultilingualAttributeInfo oMultilingualAttributeInfo;
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                    fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                    filePath = Path.Combine(targetFolder, fileName);

                    //save file
                    validFile.SaveAs(filePath, true);

                    //Save file attributes, they will be used at the time of importing file records.
                    ImportFile.FileOriginalName = validFile.GetName();
                    ImportFile.FileModifiedName = fileName;
                    ImportFile.FilePhysicalPath = filePath;
                    ImportFile.FileSize = validFile.ContentLength;

                    decimal? dataStorageCapacity;
                    decimal? currentUsage;

                    DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                    if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                    {
                        string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                        throw new Exception(exceptionMessage);
                    }
                    else
                    {
                        ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                        string SheetName;
                        SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.GeneralTaskImport, null);
                        if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                            throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));

                        string errorMessage;
                        if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.GeneralTaskImport, filePath, validFile.GetExtension(),null,SheetName, out errorMessage))
                        {
                            Session["GeneralTaskImportInfo"] = GetDataImportHdrDetails();
                            SubmitFileForProcessing();
                            RefreshDataImportStatusGrid(true);
                        }
                        else
                        {
                            throw new Exception(errorMessage);
                        }
                    }
                }
                catch (ARTException ex)
                {
                    switch (ex.ExceptionPhraseID)
                    {
                        //Invalid File. All Mandatory fields not present
                        //Save import and failure message to database
                        case 5000037:
                            InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure);
                            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                            break;
                        //Invalid Data. 
                        //Save import and failure message to database
                        case 5000047:
                            InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure);
                            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                            break;
                    }
                    Helper.ShowErrorMessage(this, ex);
                }
                catch (Exception ex)
                {
                    this.DeleteFile(filePath);
                    ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                    Helper.ShowErrorMessage(this, ex);
                }
                finally
                {
                    this.radFileUpload.UploadedFiles.Clear();
                }
            }
            else
            {
                pnlImportGrid.Visible = false;
            }
        }
    }

    private DataImportHdrInfo GetDataImportHdrDetails()
    {
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.DataImportName = this.txtImportName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.GeneralTaskImport;
        oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Success;
        oDataImportHrdInfo.RecordsImported = rgImportList.SelectedItems.Count;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
        oDataImportHrdInfo.LanguageID = SessionHelper.GetUserLanguage();
        return oDataImportHrdInfo;
    }

    private void SubmitFileForProcessing()
    {
        DataImportHdrInfo oDataImportHrdInfo = null;
        if (Session["GeneralTaskImportInfo"] != null)
        {
            oDataImportHrdInfo = (DataImportHdrInfo)Session["GeneralTaskImportInfo"];
        }
        else
        {
            oDataImportHrdInfo = GetDataImportHdrDetails();
        }
        oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Submitted;
        oDataImportHrdInfo.RecordsImported = 0;
        IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
        int rowsAffected;

        oDataImportClient.InsertDataImportGLDataRecItemSchedule(oDataImportHrdInfo, null, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted), out rowsAffected, Helper.GetAppUserInfo());

        Helper.ShowConfirmationMessage(this, Helper.GetLabelIDValue(2742));
    }

    private void RefreshDataImportStatusGrid(bool IsRebind)
    {
        try
        {
            List<DataImportHdrInfo> oDataImportHdrInfoCollection = null;
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();

            // Get All Data Imports done in the Current Rec Period
            if (SessionHelper.CurrentReconciliationPeriodID != null)
            {
                int? recPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                DataImportParamInfo oDataImportParamInfo = new DataImportParamInfo();
                Helper.FillCommonServiceParams(oDataImportParamInfo);
                oDataImportParamInfo.DataImportTypeID = (short)ARTEnums.DataImportType.GeneralTaskImport;

                oDataImportHdrInfoCollection = oDataImportClient.GetGeneralTaskImportStatus(oDataImportParamInfo, Helper.GetAppUserInfo());

                // Get the Values for Status and Type
                for (int i = 0; i < oDataImportHdrInfoCollection.Count; i++)
                {
                    oDataImportHdrInfoCollection[i].DataImportStatus = LanguageUtil.GetValue(oDataImportHdrInfoCollection[i].DataImportStatusLabelID.Value);
                    oDataImportHdrInfoCollection[i].DataImportType = LanguageUtil.GetValue(oDataImportHdrInfoCollection[i].DataImportTypeLabelID.Value);
                }
            }
            else
            {
                oDataImportHdrInfoCollection = new List<DataImportHdrInfo>();
            }

            var query = from d in oDataImportHdrInfoCollection
                        where d.AddedBy == SessionHelper.CurrentUserLoginID
                        select d;
            foreach (var d in query)
            {
                d.IsRecordOwner = true;
            }
            rgDataImport.MasterTableView.DataSource = oDataImportHdrInfoCollection;
            if (IsRebind)
                rgDataImport.Rebind();

            // Sort the Data
            GridHelper.SortDataSource(rgDataImport.MasterTableView);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    #endregion
    #region "Data Import Grid By Rec Period ID"
    protected void rgDataImport_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RefreshDataImportStatusGrid(false);
    }
    protected void rgDataImport_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            BindCommonFields(e);
            GridDataItem item = (GridDataItem)e.Item;
            DataImportHdrInfo oDataImportHdrInfo = (DataImportHdrInfo)e.Item.DataItem;

            // Show Data for Force Commit
            ExHyperLink hlForceCommitDetails = (ExHyperLink)e.Item.FindControl("hlForceCommitDetails");
            hlForceCommitDetails.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.ForceCommitDate);

            // Set URLs
            string url = GetUrlForDataImportStatusPage(oDataImportHdrInfo);
            hlForceCommitDetails.NavigateUrl = url;

            DataImportHdrInfo obj = (DataImportHdrInfo)e.Item.DataItem;

            short status = obj.DataImportStatusID.Value;
            short importType = obj.DataImportTypeID.Value;
        }
    }

    protected void rgDataImport_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgDataImport.Rebind();
    }

    protected void rgDataImport_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            if (ibExportToExcel != null)
            {
                ibExportToExcel.CausesValidation = false;
                Helper.RegisterPostBackToControls(this, ibExportToExcel);
            }
        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            if (ibExportToExcel != null)
            {
                ibExportToExcel.CausesValidation = false;
                Helper.RegisterPostBackToControls(this, ibExportToExcel);
            }
        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibRefresh = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_REFRESH_ICON) as ImageButton;
            if (ibRefresh != null)
            {
                ibRefresh.CausesValidation = false;
                Helper.RegisterPostBackToControls(this, ibRefresh);
            }
        }
    }

    protected void rgDataImport_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                //rgDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgDataImport, 1219);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                //rgDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgDataImport, 1219);
            }
            if (e.CommandName == TelerikConstants.GridRefreshCommandName)
            {
                Session[SessionConstants.SEARCH_RESULTS_DATA_IMPORT_SCHEDULE_REC_ITEM] = null;
                rgDataImport.Rebind();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion
    private void BindCommonFields(Telerik.Web.UI.GridItemEventArgs e)
    {
        DataImportHdrInfo oDataImportHdrInfo = (DataImportHdrInfo)e.Item.DataItem;

        ExHyperLink hlProfileName = (ExHyperLink)e.Item.FindControl("hlProfileName");
        ExHyperLink hlDate = (ExHyperLink)e.Item.FindControl("hlDate");
        ExHyperLink hlImportType = (ExHyperLink)e.Item.FindControl("hlImportType");
        ExHyperLink hlStatus = (ExHyperLink)e.Item.FindControl("hlStatus");
        ExHyperLink hlRecordsAffected = (ExHyperLink)e.Item.FindControl("hlRecordsAffected");
        ExHyperLink hlFileName = (ExHyperLink)e.Item.FindControl("hlFileName");
        ExImageButton imgFileType = (ExImageButton)e.Item.FindControl("imgFileType");
        ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
        lblAddedBy.Text = oDataImportHdrInfo.AddedBy;

        if (!string.IsNullOrEmpty(oDataImportHdrInfo.DataImportName))
        {
            hlProfileName.Text = oDataImportHdrInfo.DataImportName;
        }
        hlDate.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.DateAdded);
        hlImportType.Text = oDataImportHdrInfo.DataImportType;
        hlStatus.Text = oDataImportHdrInfo.DataImportStatus;
        hlRecordsAffected.Text = Helper.GetDisplayIntegerValue(oDataImportHdrInfo.RecordsImported);
        hlFileName.Text = oDataImportHdrInfo.FileName;

        //string url = "../DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(oDataImportHdrInfo.PhysicalPath));
        //imgFileType.OnClientClick = "document.location.href = '" + url + "';return false;";
        string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadDataImportFile);
        url += "&" + QueryStringConstants.DATA_IMPORT_ID + "=" + oDataImportHdrInfo.DataImportID.GetValueOrDefault()
        + "&" + QueryStringConstants.DATA_IMPORT_TYPE_ID + "=" + oDataImportHdrInfo.DataImportTypeID.GetValueOrDefault();
        imgFileType.Attributes.Add("onclick", "javascript:{$get('" + ifDownloader.ClientID + "').src='" + url + "'; return false;}");

        //// Icons
        WebEnums.DataImportStatus eDataImportStatus = (WebEnums.DataImportStatus)System.Enum.Parse(typeof(WebEnums.DataImportStatus), oDataImportHdrInfo.DataImportStatusID.Value.ToString());

        switch (eDataImportStatus)
        {
            case WebEnums.DataImportStatus.Success:
                ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
                imgSuccess.Visible = true;
                break;

            case WebEnums.DataImportStatus.Failure:
                ExImage imgFailure = (ExImage)e.Item.FindControl("imgFailure");
                imgFailure.Visible = true;
                break;

            case WebEnums.DataImportStatus.Warning:
                ExImage imgWarning = (ExImage)e.Item.FindControl("imgWarning");
                imgWarning.Visible = true;
                break;

            case WebEnums.DataImportStatus.Processing:
                ExImage imgProcessing = (ExImage)e.Item.FindControl("imgProcessing");
                imgProcessing.Visible = true;
                break;

            case WebEnums.DataImportStatus.Submitted:
                ExImage imgToBeProcessed = (ExImage)e.Item.FindControl("imgToBeProcessed");
                imgToBeProcessed.Visible = true;
                break;
            case WebEnums.DataImportStatus.Reject:
                ExImage imgReject = (ExImage)e.Item.FindControl("imgReject");
                imgReject.Visible = true;
                break;

        }
        // Set the Hyperlink for Next Page
        SetHyperlinkForStatusPage(oDataImportHdrInfo, hlProfileName, hlDate, hlImportType, hlRecordsAffected, hlStatus, hlFileName);
    }

    private void SetHyperlinkForStatusPage(DataImportHdrInfo oDataImportHdrInfo, ExHyperLink hlProfileName,
        ExHyperLink hlDate, ExHyperLink hlImportType, ExHyperLink hlRecordsAffected, ExHyperLink hlStatus, ExHyperLink hlFileName)
    {
        string url = GetUrlForDataImportStatusPage(oDataImportHdrInfo);
        hlStatus.NavigateUrl = url;
        hlProfileName.NavigateUrl = url;
        hlDate.NavigateUrl = url;
        hlImportType.NavigateUrl = url;
        hlRecordsAffected.NavigateUrl = url;
        hlFileName.NavigateUrl = url;
    }

    private static string GetUrlForDataImportStatusPage(DataImportHdrInfo oDataImportHdrInfo)
    {
        string url = "~/pages/RecItemImportStatusMessage.aspx?" + QueryStringConstants.DATA_IMPORT_ID + "=" + oDataImportHdrInfo.DataImportID.ToString() + "&" + QueryStringConstants.IS_REDIRECTED_FROM_TASK_IMOPRT + "=" + 1;
        return url;
    }

    private void InsertDataImportHdrWithFailureMsg(string failureMsg, short dataImportTypeID, short dataImportStatusID)
    {
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.DataImportName = this.txtImportName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = dataImportTypeID;
        oDataImportHrdInfo.DataImportStatusID = dataImportStatusID;
        oDataImportHrdInfo.RecordsImported = 0;
        if (dataImportTypeID == (short)ARTEnums.DataImportType.GLData)
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
        oDataImportHrdInfo.LanguageID = SessionHelper.GetUserLanguage();

        oDataImport.InsertDataImportWithFailureMsg(oDataImportHrdInfo, failureMsg, Helper.GetAppUserInfo());
    }
}
