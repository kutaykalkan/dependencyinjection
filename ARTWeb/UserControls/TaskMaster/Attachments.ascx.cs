using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.Model;
using Telerik.Web.UI;
using SkyStem.ART.Web.Utility;
using System.IO;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using System.Text;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class UserControls_TaskMaster_Attachments : UserControlTaskMasterBase
{
    private const string _sessionKey = SessionConstants.TASK_MASTER_ATTACHMENT;
    long? _recordID = 0;
    int? _recordTypeID = 0;
    int maxFileSize;
    private bool _IsMultiDcoumentUploadEnabled = false;
    string _mode = null;
    protected List<AttachmentInfo> AttachmentListToUpload
    {
        get
        {
            List<AttachmentInfo> oAttachmentListToUpload = (List<AttachmentInfo>)ViewState["AttachmentListToUpload"];
            if (oAttachmentListToUpload == null)
                oAttachmentListToUpload = new List<AttachmentInfo>();
            return oAttachmentListToUpload;
        }
        set
        {
            ViewState["AttachmentListToUpload"] = value;
        }
    }

    public List<AttachmentInfo> DataSource
    {
        get { return Session[_sessionKey] as List<AttachmentInfo>; }
        set { Session[_sessionKey] = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //set error messages
        this.SetErrorMessages();
        //UserRoleSelection.setDropDownFromPage = @"Setdropdownlist('" + ddlDefaultRole.ClientID + "', '"+UserRoleSelection.ClientID+ "')";
        //this.RadFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);
        maxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMaxFileSize.Text = Helper.GetLabelIDValue(1801) + " " + DataImportHelper.GetAllowedMaximumFileSizeInt(SessionHelper.CurrentCompanyID.Value) + " " + Helper.GetLabelIDValue(1802);

        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.RECORD_ID]))
        {
            _recordID = Convert.ToInt64(Request.QueryString[QueryStringConstants.RECORD_ID]);
            Session[_sessionKey] = null;
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]))
            _recordTypeID = Convert.ToInt32(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]);

        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MODE]))
            _mode = Request.QueryString[QueryStringConstants.MODE];

        _IsMultiDcoumentUploadEnabled = Helper.IsFeatureActivated(WebEnums.Feature.MultiDocumentUpload, SessionHelper.CurrentReconciliationPeriodID);

        if (!Page.IsPostBack)
        {
            ShowHideControls();
            AddNewAttachmentRow();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        LoadData();
        BindGrid(DataSource);
    }

    void ShowHideControls()
    {
        bool flag = false;
        if (!string.IsNullOrEmpty(_mode) && _mode == QueryStringConstants.INSERT)
        {
            flag = true;
        }
        pnlFileUpload.Visible = flag;
    }

    void LoadData()
    {
        if (this._recordID != null && this._recordID > 0)
        {
            DataSource = TaskMasterHelper.GetTaskAttachments(_recordID, (ARTEnums.RecordType)_recordTypeID);
        }
        if (DataSource == null)
            DataSource = new List<AttachmentInfo>();
    }

    void BindGrid(List<AttachmentInfo> data)
    {
        rgAttachments.DataSource = data;
        rgAttachments.DataBind();
    }

    void AddAttachmentToDataSourceAndSesssion()
    {
        if (DataSource == null)
            DataSource = new List<AttachmentInfo>();
        DataSource.AddRange(AttachmentListToUpload);
        //Session[_sessionKey] = DataSource;
    }

    protected void rgAttachments_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        rgAttachments.DataSource = DataSource;
    }

    protected void rgAttachments_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            AttachmentInfo oAttachmentInfo = (AttachmentInfo)e.Item.DataItem;

            ExHyperLink hlDocumentName = (ExHyperLink)e.Item.FindControl("hlDocumentName");


            string url = this.ResolveUrl(URLConstants.URL_DOWNLOAD_ATTACHMENT) + "?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(oAttachmentInfo.PhysicalPath);
            hlDocumentName.NavigateUrl = url;

            //GridColumn gcDelete = rgAttachments.Columns.FindByUniqueNameSafe("DeleteColumn");

            ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
            if (lblDescription != null)
            {
                Helper.SetTextAndTooltipValue(lblDescription, oAttachmentInfo.Comments);
            }

            ImageButton imgBtnDelete = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];

            if ((_mode == QueryStringConstants.INSERT || _mode == QueryStringConstants.EDIT) && SessionHelper.CurrentUserID == oAttachmentInfo.UserID)
            {
                imgBtnDelete.ToolTip = Helper.GetLabelIDValue(1564);
                imgBtnDelete.CommandArgument = "1^" + oAttachmentInfo.PhysicalPath + "^" + oAttachmentInfo.FileSize + "^" + oAttachmentInfo.StartRecPeriodID;
                imgBtnDelete.Enabled = true;
                imgBtnDelete.Visible = true;
            }
            else
                imgBtnDelete.Visible = false;
        }
    }

    protected void rgGLAdjustments_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        int attachmentID = Convert.ToInt32((rgAttachments.MasterTableView.DataKeyValues[e.Item.ItemIndex][rgAttachments.MasterTableView.DataKeyNames[0]]).ToString());
        string[] arrComandArgs = e.CommandArgument.ToString().Split('^');
        //bool IsFileDeleteParmanent = true;
        string filePath = "";
        IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
        if (this._recordID.HasValue && this._recordID.Value > 0)
        {
            filePath = arrComandArgs[1];
            var fileReferenceCount = oAttachmentClient.DeleteAttachmentAndGetFileRefrenceCount(attachmentID, Helper.GetAppUserInfo());
            if (!fileReferenceCount.HasValue || fileReferenceCount <= 0)
            {
                int StartRecPeriodID;
                int.TryParse(arrComandArgs[3], out StartRecPeriodID);
                if (StartRecPeriodID == SessionHelper.CurrentReconciliationPeriodID)
                {
                    decimal FileSize;
                    decimal.TryParse(arrComandArgs[2], out FileSize);
                    DataImportHelper.UpdateCompanyDataStorageCapacityAndCurrentUsage(SessionHelper.CurrentCompanyID.Value, FileSize, SessionHelper.CurrentUserLoginID, DateTime.Now);
                    DeleteFile(filePath);
                }
            }
            DataSource = oAttachmentClient.GetAttachment((int)_recordID, (int)_recordTypeID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        }
        else
        {
            filePath = arrComandArgs[1];
            DataSource.RemoveAt(e.Item.DataSetIndex);
            DeleteFile(filePath);
        }
        BindGrid(DataSource);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ReadAttachmentInfoFromRepeater(true);
            if (AttachmentListToUpload.Count > 0)
            {
                try
                {
                    decimal? dataStorageCapacity;
                    decimal? currentUsage;
                    decimal? uploadSize = 0;
                    AttachmentListToUpload.ForEach(T => uploadSize += T.FileSize);

                    DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                    if (((decimal)(uploadSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                    {
                        string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                        throw new Exception(exceptionMessage);
                    }

                    SaveAttachment();
                    ClearControls();
                    //Bind the grid
                    BindGrid(DataSource);

                    //***********************************************************************************
                    //if (!this.Page.ClientScript.IsClientScriptBlockRegistered("RefreshParentPageOnWindowClose"))
                    //    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RefreshParentPageOnWindowClose", ScriptHelper.GetJSForRefreshParentPageOnWindowClose(true));
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                }
            }
        }
    }

    protected void cvRadUpload_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        ExCustomValidator cvRadUpload = (ExCustomValidator)source;
        ExRadUpload RadFileUpload = (ExRadUpload)cvRadUpload.Parent.FindControl("ruRadFileUpload");
        if (RadFileUpload.UploadedFiles.Count == 0 && RadFileUpload.InvalidFiles.Count == 0)
        {
            cvRadUpload.ErrorMessage = LanguageUtil.GetValue(5000035);
            args.IsValid = false;
        }
        else if (RadFileUpload.InvalidFiles.Count > 0)
        {
            args.IsValid = false;
            UploadedFile invalidFile = RadFileUpload.InvalidFiles[0];
            //File size exceeds maximum file upload size.
            if (invalidFile.ContentLength == 0 || invalidFile.ContentLength > maxFileSize)
                cvRadUpload.ErrorMessage = LanguageUtil.GetValue(5000038);//File size is more than maximum allowed file size
            else
                cvRadUpload.ErrorMessage = LanguageUtil.GetValue(5000036);//Invalid file extension 
        }
        else if (RadFileUpload.UploadedFiles.Count > 0)
        {
            UploadedFile validFile = RadFileUpload.UploadedFiles[0];
            if (validFile.ContentLength == 0)
            {
                cvRadUpload.ErrorMessage = LanguageUtil.GetValue(5000038);//File size is zero or more than maximum allowed file size
                args.IsValid = false;
            }
            else if (validFile.FileName.Length > WebConstants.MAX_FILE_NAME_LENGTH_FOR_ATTACHMENT)
            {
                args.IsValid = false;
                cvRadUpload.ErrorMessage = string.Format(LanguageUtil.GetValue(5000347), WebConstants.MAX_FILE_NAME_LENGTH_FOR_ATTACHMENT); //File name is too long. Maximum allowed length is {0} characters.
            }
        }
    }

    private void DeleteFile(string filePath)
    {
        //Delete the saved file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        //Send exception to user interface
    }

    private bool HideDeleteParmanentFile(AttachmentInfo oAttachmentInfo)
    {
        if (oAttachmentInfo.PreviousAttachmentID.Value > 0 && oAttachmentInfo.OriginalAttachmentID.Value > 0 && oAttachmentInfo.IsPermanentOrTemporary.Value == true)
            return false;
        else
            return true;

    }

    private void SetErrorMessages()
    {
        //this.txtDocumentName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblDocumentName.LabelID);

    }

    private void SaveAttachment()
    {
        try
        {
            if (this._recordID.HasValue && this._recordID.Value > 0)
            {
                IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
                oAttachmentClient.InsertAttachmentBulk(AttachmentListToUpload, DateTime.Now, Helper.GetAppUserInfo());

                DataSource = oAttachmentClient.GetAttachment((int)_recordID, (int)_recordTypeID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                //Session[_sessionKey] = DataSource;
            }
            else
            {
                AddAttachmentToDataSourceAndSesssion();
            }
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
        }

    }

    void ClearControls()
    {
        AttachmentListToUpload = new List<AttachmentInfo>();
        AddNewAttachmentRow();
        //txtDocumentName.Text = string.Empty;
        //txtComments.Text = string.Empty;
    }

    protected void rptFileUpload_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            AttachmentInfo oAttachmentInfo = (AttachmentInfo)e.Item.DataItem;
            ExTextBox txtDocumentName = (ExTextBox)e.Item.FindControl("txtDocumentName");
            ExRadUpload ruRadFileUpload = (ExRadUpload)e.Item.FindControl("ruRadFileUpload");
            ExTextBox txtComments = (ExTextBox)e.Item.FindControl("txtComments");
            ExImageButton ImgBtnRemove = (ExImageButton)e.Item.FindControl("ImgBtnRemove");
            ExCustomValidator cvRadUpload = (ExCustomValidator)e.Item.FindControl("cvRadUpload");

            cvRadUpload.Attributes.Add("RadUploadControlID", cvRadUpload.ClientID);
            ruRadFileUpload.MaxFileSize = maxFileSize;

            txtDocumentName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1680);

            if (oAttachmentInfo.RowNumber == 1)
                ImgBtnRemove.Visible = false;
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            LinkButton lnkBtnAttachMore = (LinkButton)e.Item.FindControl("lnkBtnAttachMore");
            lnkBtnAttachMore.Visible = _IsMultiDcoumentUploadEnabled;
        }
    }
    protected void lnkBtnAttachMore_Click(object sender, EventArgs e)
    {
        AddNewAttachmentRow();
    }
    protected void AddNewAttachmentRow()
    {
        ReadAttachmentInfoFromRepeater(false);
        AttachmentInfo oAttachmentInfo = new AttachmentInfo();
        oAttachmentInfo.RowNumber = 1;
        AttachmentListToUpload.ForEach((T) =>
        {
            if (T.RowNumber >= oAttachmentInfo.RowNumber)
                oAttachmentInfo.RowNumber = T.RowNumber + 1;
        });
        List<AttachmentInfo> oAttachmentListToUpload = AttachmentListToUpload;
        oAttachmentListToUpload.Add(oAttachmentInfo);
        // This is required to save into view state
        AttachmentListToUpload = oAttachmentListToUpload;
        BindAttachmentRepeater();
    }
    protected void RemoveAttachmentRow(int rowNumber)
    {
        ReadAttachmentInfoFromRepeater(false);
        List<AttachmentInfo> oAttachmentListToUpload = AttachmentListToUpload;
        AttachmentInfo oAttachmentInfo = oAttachmentListToUpload.Find(T => T.RowNumber == rowNumber);
        if (oAttachmentInfo != null)
            oAttachmentListToUpload.Remove(oAttachmentInfo);
        // This is required to save into view state
        AttachmentListToUpload = oAttachmentListToUpload;
        BindAttachmentRepeater();
    }

    protected void ReadAttachmentInfoFromRepeater(bool bSaveFile)
    {
        string filePath = string.Empty;
        string fileName = string.Empty;
        DateTime oUploadDateTime = DateTime.Now;
        //Get folder path and name as per companyid 
        string targetFolder = DataImportHelper.GetFolderForAttachment(SessionHelper.CurrentCompanyID.Value, (int)SessionHelper.CurrentReconciliationPeriodID);
        List<AttachmentInfo> oAttachmentListToUpload = AttachmentListToUpload;
        foreach (RepeaterItem item in rptFileUpload.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnRowNumber = (HiddenField)item.FindControl("hdnRowNumber");
                ExTextBox txtDocumentName = (ExTextBox)item.FindControl("txtDocumentName");
                ExRadUpload ruRadFileUpload = (ExRadUpload)item.FindControl("ruRadFileUpload");
                ExTextBox txtComments = (ExTextBox)item.FindControl("txtComments");

                AttachmentInfo oAttachmentInfo = oAttachmentListToUpload.Find(T => T.RowNumber == Convert.ToInt32(hdnRowNumber.Value));
                if (oAttachmentInfo != null)
                {
                    if (ruRadFileUpload.UploadedFiles.Count > 0)
                    {
                        UploadedFile validFile = ruRadFileUpload.UploadedFiles[0];
                        fileName = Helper.GetFileName(validFile);
                        filePath = Path.Combine(targetFolder, fileName);
                        if (bSaveFile)
                        {
                            int i = 1;
                            while (File.Exists(filePath))
                            {
                                fileName = Helper.GetFileName(validFile, i++);
                                filePath = Path.Combine(targetFolder, fileName);
                            }
                            //save file
                            validFile.SaveAs(filePath, true);
                        }
                        oAttachmentInfo.FileName = fileName;
                        oAttachmentInfo.PhysicalPath = filePath;
                        oAttachmentInfo.FileSize = validFile.ContentLength;
                        //oAttachmentInfo.Comments = txtComments.Text;
                    }
                    else
                    {
                        oAttachmentInfo.FileName = null;
                        oAttachmentInfo.PhysicalPath = null;
                        oAttachmentInfo.FileSize = null;
                    }
                    oAttachmentInfo.DocumentName = txtDocumentName.Text;//TODO validate for invalid character
                    oAttachmentInfo.RecordID = _recordID;
                    oAttachmentInfo.RecordTypeID = _recordTypeID;
                    oAttachmentInfo.IsPermanentOrTemporary = true;
                    oAttachmentInfo.Date = oUploadDateTime;
                    oAttachmentInfo.UserID = SessionHelper.GetCurrentUser().UserID;
                    oAttachmentInfo.IsActive = true;
                    //oAttachmentInfo.Comments = txtComments.Text.Length > 250 ? txtComments.Text.Substring(0, 250) : txtComments.Text;
                    oAttachmentInfo.UserFullName = Helper.GetUserFullName();
                    oAttachmentInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                }
            }
        }
        // This is required to save into view state
        AttachmentListToUpload = oAttachmentListToUpload;
    }

    protected void BindAttachmentRepeater()
    {
        rptFileUpload.DataSource = AttachmentListToUpload;
        rptFileUpload.DataBind();
    }
    protected void rptFileUpload_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            RemoveAttachmentRow(Convert.ToInt32(e.CommandArgument));
        }
    }

}
