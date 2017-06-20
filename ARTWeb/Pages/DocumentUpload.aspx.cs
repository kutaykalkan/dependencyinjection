using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using System.IO;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using System.Text;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Shared.Utility;

public partial class Pages_DocumentUpload : PopupPageBase
{
    #region Variables & Constants
    long? _recordID = 0;
    int? _recordTypeID = 0;
    int maxFileSize;
    static int attachmentCount = 0;
    long? _glDataID = 0;
    private bool _IsForwardedItem = false;
    private bool _IsMultiDcoumentUploadEnabled = false;
    #endregion

    #region Properties
    private GLDataHdrInfo _GLDataHdrInfo;
    private GLDataHdrInfo GLDataHdrInfo
    {
        get
        {
            if (this._GLDataHdrInfo == null)
            {
                if (ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] == null)
                {
                    if (Request.QueryString[QueryStringConstants.RECORD_TYPE_ID] != null)
                    {
                        _recordTypeID = Convert.ToInt32(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]);
                        if (_recordTypeID == (int?)WebEnums.RecordType.GLDataHdr)
                        {
                            this._glDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.RECORD_ID]);
                        }
                        else
                        {
                            if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
                                this._glDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                        }
                    }
                    this._GLDataHdrInfo = Helper.GetGLDataHdrInfo(this._glDataID);

                    ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = this._GLDataHdrInfo;
                }
                else
                {
                    this._GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
                }
            }
            return this._GLDataHdrInfo;
        }
    }

    private string inputItemId
    {
        get
        {
            return ViewState["ItemId"] != null ? ViewState["ItemId"].ToString() : "";
        }
        set
        {
            ViewState["ItemId"] = value;
        }
    }
    private string inputItemType
    {
        get
        {
            return ViewState["ItemType"] != null ? ViewState["ItemType"].ToString() : "";
        }
        set
        {
            ViewState["ItemType"] = value;
        }
    }

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


    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        //set error messages
        this.SetErrorMessages();
        //this.RadFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);
        maxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetcountOfDocumentAttached", ScriptHelper.GetJSForPopupSessionTimeout());

        //ViewState["ItemId"] = this.hdnControl.Text ;
        this.Title = LanguageUtil.GetValue(1392) + WebConstants.HYPHEN + LanguageUtil.GetValue(1393);

        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.RECORD_ID]) && Request.QueryString[QueryStringConstants.RECORD_ID] != null)
            _recordID = Convert.ToInt64(Request.QueryString[QueryStringConstants.RECORD_ID]);

        if (Request.QueryString[QueryStringConstants.RECORD_TYPE_ID] != null)
            _recordTypeID = Convert.ToInt32(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]);

        if (Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM] != null)
            _IsForwardedItem = Convert.ToBoolean(Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM]);



        ucAccountHierarchyDetail.AccountID = this.GLDataHdrInfo.AccountID.Value;
        ucAccountHierarchyDetail.NetAccountID = this.GLDataHdrInfo.NetAccountID.Value;

        lblMaxFileSize.Text = Helper.GetLabelIDValue(1801) + " " + DataImportHelper.GetAllowedMaximumFileSizeInt(SessionHelper.CurrentCompanyID.Value) + Helper.GetLabelIDValue(1802);
        _IsMultiDcoumentUploadEnabled = Helper.IsFeatureActivated(WebEnums.Feature.MultiDocumentUpload, SessionHelper.CurrentReconciliationPeriodID);


        if (!this.IsPostBack)
        {

            string s1 = this.hdnItemId.Value;
            IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
            IList<AttachmentInfo> oAttachmentCollection = new List<AttachmentInfo>();

            if (this._recordID != null && this._recordID > 0)
            {
                oAttachmentCollection = oAttachmentClient.GetAttachment(_recordID.Value, _recordTypeID.Value, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            }
            else if (Session[SessionConstants.ATTACHMENTS] != null)
            {
                oAttachmentCollection = (List<AttachmentInfo>)Session[SessionConstants.ATTACHMENTS];
            }
            //this.gvDocuments.DataSource = oAttachmentCollection;
            //this.gvDocuments.DataBind();
            rgGLAdjustments.EntityNameText = LanguageUtil.GetValue(1392) + WebConstants.HYPHEN + LanguageUtil.GetValue(1393);
            rgGLAdjustments.DataSource = oAttachmentCollection;
            rgGLAdjustments.DataBind();
            attachmentCount = oAttachmentCollection.Count;
            lblInputFormRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);
            lblEnteredByValue.Text = SessionHelper.GetCurrentUser().FirstName + " " + SessionHelper.GetCurrentUser().LastName;//TODO: change it
            lblEnteredDateValue.Text = Helper.GetDisplayDateTime(DateTime.Now);

            AddNewAttachmentRow();
        }

        WebEnums.RecordType eRecordType = (WebEnums.RecordType)Enum.Parse(typeof(WebEnums.RecordType), this._recordTypeID.Value.ToString());

        if (eRecordType == WebEnums.RecordType.GLDataHdr)
            _glDataID = _recordID;

        short? _ReconciliationStatusID = Helper.GetReconciliationStatusByGLDataID(_glDataID);
        //string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + _accountID + "&" + QueryStringConstants.GLDATA_ID + "=" + _gLDataID;


        if (_ReconciliationStatusID.HasValue)
        {
            WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.DocumentUpload, this.GLDataHdrInfo);
            if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MODE]))
            {
                string _readOnlyMode = Request.QueryString[QueryStringConstants.MODE];
                if (_readOnlyMode == WebEnums.FormMode.ReadOnly.ToString())
                    eFormMode = WebEnums.FormMode.ReadOnly;
                else
                    eFormMode = WebEnums.FormMode.Edit;
            }
            GridColumn oDeleteColumn = rgGLAdjustments.Columns.FindByUniqueNameSafe("DeleteColumn");
            if (eFormMode == WebEnums.FormMode.Edit)
            {
                if (_IsForwardedItem == true)
                {
                    pnlHideOnEdit.Visible = false;
                    //rgGLAdjustments.Columns[5].Visible = false;
                    oDeleteColumn.Visible = false;
                }
                else
                {
                    pnlHideOnEdit.Visible = true;
                }
                if (_IsForwardedItem == true && _recordTypeID == 3)
                {
                    pnlHideOnEdit.Visible = true;
                    //rgGLAdjustments.Columns[5].Visible = true;
                    oDeleteColumn.Visible = true;
                }

            }
            else
            {
                pnlHideOnEdit.Visible = false;
                //rgGLAdjustments.Columns[5].Visible = false;
                oDeleteColumn.Visible = false;
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        short? _ReconciliationStatusID = Helper.GetReconciliationStatusByGLDataID(_recordID);

        if (_ReconciliationStatusID.HasValue)
        {
            WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.DocumentUpload, this.GLDataHdrInfo);
            if (eFormMode == WebEnums.FormMode.Edit)
            {
                string scriptKey = "SetcountOfDocumentAttached";
                // Render JS to Open the grid Customization Window, 
                StringBuilder script = new StringBuilder();
                ScriptHelper.AddJSStartTag(script);

                script.Append("function SetLabelCount()");
                script.Append(System.Environment.NewLine);
                script.Append("{");
                script.Append(System.Environment.NewLine);
                script.Append(" if (GetRadWindow().BrowserWindow.CalledFn != undefined) ");
                script.Append(" { GetRadWindow().BrowserWindow.CalledFn('" + attachmentCount + "') ; }");
                //script.Append(System.Environment.NewLine);
                //script.Append("alert(oLabel);");
                //script.Append(System.Environment.NewLine);
                //script.Append("oLabel.innerHTML='" + attachmentCount + "';");
                script.Append(System.Environment.NewLine);
                script.Append("}");
                script.Append(System.Environment.NewLine);
                script.Append("SetLabelCount();");
                ScriptHelper.AddJSEndTag(script);
                if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), scriptKey))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, script.ToString());
                }
            }
        }
    }
    #endregion

    #region Grid Events

    #region GLAdjustments
    protected void rgGLAdjustments_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Header)
        {
            if (_recordTypeID.GetValueOrDefault() != (int)WebEnums.RecordType.GLDataReviewNote)
                rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("IsPermanentOrTemporary").Visible = true;
            else
                rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("IsPermanentOrTemporary").Visible = false;
        }
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            AttachmentInfo oAttachmentInfo = (AttachmentInfo)e.Item.DataItem;

            ExLabel lblIsPermanentOrTemporary = (ExLabel)e.Item.FindControl("lblIsPermanentOrTemporary");
            ExHyperLink hlDocumentName = (ExHyperLink)e.Item.FindControl("hlDocumentName");

            lblIsPermanentOrTemporary.Text = SetPermanentOrTemporary(oAttachmentInfo);

            //string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(oAttachmentInfo.PhysicalPath));
            string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadGLAttachmentFile);
            url += "&" + QueryStringConstants.RECORD_ID + "=" + oAttachmentInfo.RecordID.GetValueOrDefault()
            + "&" + QueryStringConstants.RECORD_TYPE_ID + "=" + oAttachmentInfo.RecordTypeID.GetValueOrDefault()
            + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataHdrInfo.GLDataID.GetValueOrDefault()
            + "&" + QueryStringConstants.GENERIC_ID + "=" + oAttachmentInfo.AttachmentID.GetValueOrDefault();
            hlDocumentName.NavigateUrl = url;


            GridColumn gcDelete = rgGLAdjustments.Columns.FindByUniqueNameSafe("DeleteColumn");
            ImageButton imgBtnDelete = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];
            //imgBtnDelete.Visible = HideDeleteParmanentFile(oAttachmentInfo);

            if (_recordTypeID.GetValueOrDefault() == (int)WebEnums.RecordType.GLDataReviewNote)
            {
                if (oAttachmentInfo.UserID.GetValueOrDefault() != SessionHelper.GetCurrentUser().UserID.GetValueOrDefault())
                    imgBtnDelete.Visible = false;
                else
                    imgBtnDelete.Visible = true;

                imgBtnDelete.CommandArgument = "1^" + //oAttachmentInfo.PhysicalPath +
                     "^" + oAttachmentInfo.FileSize + "^" + oAttachmentInfo.StartRecPeriodID;
                imgBtnDelete.ToolTip = Helper.GetLabelIDValue(1564);
            }
            else
            {

                if (HideDeleteParmanentFile(oAttachmentInfo))
                    imgBtnDelete.CommandArgument = "1^" + //oAttachmentInfo.PhysicalPath + 
                        "^" + oAttachmentInfo.FileSize + "^" + oAttachmentInfo.StartRecPeriodID;
                else
                    imgBtnDelete.CommandArgument = "0^" + //oAttachmentInfo.PhysicalPath + 
                        "^" + oAttachmentInfo.FileSize + "^" + oAttachmentInfo.StartRecPeriodID;
                imgBtnDelete.ToolTip = Helper.GetLabelIDValue(1564);

            }
        }
    }


    protected void rgGLAdjustments_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        List<AttachmentInfo> oAttachmentCollection = new List<AttachmentInfo>();
        int attachmentID = Convert.ToInt32((rgGLAdjustments.MasterTableView.DataKeyValues[e.Item.ItemIndex][rgGLAdjustments.MasterTableView.DataKeyNames[0]]).ToString());
        string[] arrComandArgs = e.CommandArgument.ToString().Split('^');
        bool IsFileDeleteParmanent = false;
        //string filePath = "";
        if (arrComandArgs.Length > 0)
        {
            //filePath = arrComandArgs[1];
            if (arrComandArgs[0] == "1")
                IsFileDeleteParmanent = true;
            else
                IsFileDeleteParmanent = false;
        }

        if (this._recordID.HasValue && this._recordID.Value > 0)
        {
            IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
            oAttachmentCollection = oAttachmentClient.GetAttachment((int)_recordID, (int)_recordTypeID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            AttachmentInfo oAttachmentToDelete = oAttachmentCollection.Find(r => r.AttachmentID == attachmentID);
            oAttachmentClient.DeleteAttachment(attachmentID, Helper.GetAppUserInfo());
            if (IsFileDeleteParmanent)
            {
                int StartRecPeriodID = oAttachmentToDelete.StartRecPeriodID.GetValueOrDefault();
                //int.TryParse(arrComandArgs[3], out StartRecPeriodID);
                if (StartRecPeriodID == SessionHelper.CurrentReconciliationPeriodID)
                {
                    decimal FileSize = oAttachmentToDelete.FileSize.GetValueOrDefault(); 
                    //decimal.TryParse(arrComandArgs[2], out FileSize);
                    DataImportHelper.UpdateCompanyDataStorageCapacityAndCurrentUsage(SessionHelper.CurrentCompanyID.Value, FileSize, SessionHelper.CurrentUserLoginID, DateTime.Now);
                    DeleteFile(oAttachmentToDelete.PhysicalPath);
                }
            }
            oAttachmentCollection = oAttachmentClient.GetAttachment((int)_recordID, (int)_recordTypeID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        }
        else
        {
            oAttachmentCollection = (List<AttachmentInfo>)Session[SessionConstants.ATTACHMENTS];
            AttachmentInfo oAttachmentToDelete = oAttachmentCollection.Find(r => r.AttachmentID == attachmentID);
            if (oAttachmentToDelete != null)
            {
                oAttachmentCollection.Remove(oAttachmentToDelete);
                DeleteFile(oAttachmentToDelete.PhysicalPath);
            }
        }
        rgGLAdjustments.DataSource = oAttachmentCollection;
        rgGLAdjustments.DataBind();
        attachmentCount = oAttachmentCollection.Count;
        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("RefreshParentPageOnWindowClose"))
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RefreshParentPageOnWindowClose", ScriptHelper.GetJSForRefreshParentPageOnWindowClose(true));
    }

    #endregion

    #region rptFileUpload(Repeater)
    protected void rptFileUpload_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Panel pnlFileTypeHdr = (Panel)e.Item.FindControl("pnlFileTypeHdr");
            if (_recordTypeID.GetValueOrDefault() != (int)WebEnums.RecordType.GLDataReviewNote)
                pnlFileTypeHdr.Visible = true;
            else
                pnlFileTypeHdr.Visible = false;

        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            AttachmentInfo oAttachmentInfo = (AttachmentInfo)e.Item.DataItem;
            ExTextBox txtDocumentName = (ExTextBox)e.Item.FindControl("txtDocumentName");
            ExRadUpload ruRadFileUpload = (ExRadUpload)e.Item.FindControl("ruRadFileUpload");
            DropDownList ddlFileType = (DropDownList)e.Item.FindControl("ddlFileType");
            ExImageButton ImgBtnRemove = (ExImageButton)e.Item.FindControl("ImgBtnRemove");
            ExRequiredFieldValidator rfvFileType = (ExRequiredFieldValidator)e.Item.FindControl("rfvFileType");
            ExCustomValidator cvRadUpload = (ExCustomValidator)e.Item.FindControl("cvRadUpload");
            Panel pnlFileType = (Panel)e.Item.FindControl("pnlFileType");


            cvRadUpload.Attributes.Add("RadUploadControlID", cvRadUpload.ClientID);
            ruRadFileUpload.MaxFileSize = maxFileSize;

            txtDocumentName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1680);
            if (_recordTypeID.GetValueOrDefault() != (int)WebEnums.RecordType.GLDataReviewNote)
            {
                pnlFileType.Visible = true;
                rfvFileType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2028);
                rfvFileType.InitialValue = WebConstants.SELECT_ONE;
                ListControlHelper.BindFileTypeDropdown(ddlFileType, true);
                if (oAttachmentInfo.FileType.HasValue)
                    ddlFileType.SelectedValue = oAttachmentInfo.FileType.Value.ToString();
            }
            else
            {
                pnlFileType.Visible = false;
            }
            if (oAttachmentInfo.RowNumber == 1)
                ImgBtnRemove.Visible = false;
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            LinkButton lnkBtnAttachMore = (LinkButton)e.Item.FindControl("lnkBtnAttachMore");
            lnkBtnAttachMore.Visible = _IsMultiDcoumentUploadEnabled;
        }
    }
    protected void rptFileUpload_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            RemoveAttachmentRow(Convert.ToInt32(e.CommandArgument));
        }
    }
    #endregion

    #endregion

    #region Other Events
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(1000 * 10);
        rgGLAdjustments.EntityNameText = LanguageUtil.GetValue(1392) + WebConstants.HYPHEN + LanguageUtil.GetValue(1393);
        //TODO: get (recordID and recordTypeID) from page
        //int recordID=1;//int gLDataID=1;
        //int recordTypeID=1;//int gLDataID=1;
        if (this.IsValid)
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
                    else
                    {

                        IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
                        if (this._recordID.HasValue && this._recordID.Value > 0)
                        {
                            oAttachmentClient.InsertAttachmentBulk(AttachmentListToUpload, DateTime.Now, Helper.GetAppUserInfo());
                            //Show records in grid.

                            //TODO: show confirmation message message

                            //Bind the grid again
                            //IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
                            IList<AttachmentInfo> oAttachmentCollection = oAttachmentClient.GetAttachment((int)_recordID, (int)_recordTypeID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                            //this.gvDocuments.DataSource = oAttachmentCollection;
                            //this.gvDocuments.DataBind();

                            rgGLAdjustments.DataSource = oAttachmentCollection;
                            rgGLAdjustments.DataBind();
                            attachmentCount = oAttachmentCollection.Count;
                        }
                        else
                        {
                            List<AttachmentInfo> oAttachmentInfoCollection = null;

                            if (Session[SessionConstants.ATTACHMENTS] != null)
                            {
                                oAttachmentInfoCollection = (List<AttachmentInfo>)Session[SessionConstants.ATTACHMENTS];
                            }
                            else
                            {
                                oAttachmentInfoCollection = new List<AttachmentInfo>();
                                Session[SessionConstants.ATTACHMENTS] = oAttachmentInfoCollection;
                            }
                            foreach (AttachmentInfo item in AttachmentListToUpload)
                            {
                                AttachmentInfo oAttachmentInfo = (AttachmentInfo)Helper.DeepClone(item);
                                oAttachmentInfo.AttachmentID = this.GetTempAttachmentIDFromSessionKey();
                                oAttachmentInfoCollection.Add(oAttachmentInfo);
                                Session[SessionConstants.ATTACHMENTS] = oAttachmentInfoCollection;
                            }

                            rgGLAdjustments.DataSource = oAttachmentInfoCollection;
                            rgGLAdjustments.DataBind();
                            attachmentCount = oAttachmentInfoCollection.Count;
                        }
                    }

                    //Code To Change Rec Status and RecPerios Status : By Prafull 
                    //***********************************************************************************
                    WebEnums.RecordType eRecordType = (WebEnums.RecordType)Enum.Parse(typeof(WebEnums.RecordType), this._recordTypeID.Value.ToString());

                    if (eRecordType == WebEnums.RecordType.GLDataHdr)
                    {
                        _glDataID = _recordID;

                        short? _ReconciliationStatusID = Helper.GetReconciliationStatusByGLDataID(_glDataID);


                        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                        List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(_glDataID, Helper.GetAppUserInfo());
                        if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
                        {
                            if (_ReconciliationStatusID == (Int16)WebEnums.ReconciliationStatus.NotStarted)
                            {

                                GLDataHdrInfo oGLDataHdrInfo = oGLDataHdrInfoCollection[0];
                                oGLDataHdrInfo.ReconciliationStatusID = (Int16)WebEnums.ReconciliationStatus.InProgress;
                                oGLDataHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;

                                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                                oUtilityClient.UpdateGLDataReconciliationStatus(oGLDataHdrInfo, Helper.GetAppUserInfo());
                            }

                        }

                    }
                    //***********************************************************************************
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("RefreshParentPageOnWindowClose"))
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RefreshParentPageOnWindowClose", ScriptHelper.GetJSForRefreshParentPageOnWindowClose(true));

                }
                catch (ARTException ex)
                {
                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                }
                catch (Exception ex)
                {
                    AttachmentListToUpload.ForEach(T => this.DeleteFile(T.PhysicalPath));
                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                }
                finally
                {
                    this.ClearControls();

                }
            }

        }
    }
    protected void lnkBtnAttachMore_Click(object sender, EventArgs e)
    {
        AddNewAttachmentRow();
    }
    #endregion

    #region Validation Control Events
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
            if (invalidFile.ContentLength == 0 || invalidFile.ContentLength > maxFileSize) //DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value))
                cvRadUpload.ErrorMessage = LanguageUtil.GetValue(5000038);//File size is zero or more than maximum allowed file size
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

    #endregion

    #region Private Methods

    /// <summary>
    /// Deletes file from specified path.
    /// </summary>
    /// <param name="filePath">file physical path</param>
    /// <param name="exceptionMessage"></param>
    private void DeleteFile(string filePath)
    {
        try
        {
            //Delete the saved file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch { }
        //Send exception to user interface
    }
    private void SetErrorMessages()
    {
        //this.txtDocumentName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblDocumentName.LabelID);

    }

    private void ClearControls()
    {
        AttachmentListToUpload = new List<AttachmentInfo>();
        AddNewAttachmentRow();
        //this.RadFileUpload.UploadedFiles.Clear();
        //this.txtComments.Text = "";
        //this.txtDocumentName.Text = "";
    }

    private bool HideDeleteParmanentFile(AttachmentInfo oAttachmentInfo)
    {
        if (oAttachmentInfo.PreviousAttachmentID.Value > 0 && oAttachmentInfo.OriginalAttachmentID.Value > 0 && oAttachmentInfo.IsPermanentOrTemporary.Value == true)
            return false;
        else
            return true;

    }
    private long? GetTempAttachmentIDFromSessionKey()
    {
        long? tmpAttachmentKey = 0;
        if (Session[SessionConstants.ATTACHMENTS] != null)
        {
            List<AttachmentInfo> oAttachmentCollection = (List<AttachmentInfo>)Session[SessionConstants.ATTACHMENTS];
            if (oAttachmentCollection.Count > 0)
            {
                tmpAttachmentKey = oAttachmentCollection[0].AttachmentID.Value;
                for (int i = 1; i < oAttachmentCollection.Count; i++)
                {
                    if (oAttachmentCollection[i].AttachmentID.Value < tmpAttachmentKey)
                        tmpAttachmentKey = oAttachmentCollection[i].AttachmentID.Value;

                }
            }
        }
        return tmpAttachmentKey - 1;
    }
    #endregion

    #region Other Methods
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
                DropDownList ddlFileType = (DropDownList)item.FindControl("ddlFileType");

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
                    //oAttachmentInfo.IsPermanentOrTemporary = Helper.SetSetFlagBasedOnYesNoRadioButtons(optPermanent, optTemporary);
                    oAttachmentInfo.Date = oUploadDateTime;
                    oAttachmentInfo.UserID = SessionHelper.GetCurrentUser().UserID;
                    oAttachmentInfo.IsActive = true;
                    if (_recordTypeID.GetValueOrDefault() != (int)WebEnums.RecordType.GLDataReviewNote)
                    {
                        if (ddlFileType.SelectedValue == WebConstants.SELECT_ONE)
                            oAttachmentInfo.FileType = null;
                        else
                            oAttachmentInfo.FileType = (short)Convert.ToInt32(ddlFileType.SelectedValue);
                    }
                    else
                        oAttachmentInfo.FileType = null;
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
    public string SetPermanentOrTemporary(AttachmentInfo oAttachmentInfo)
    {
        string output = "";
        if (oAttachmentInfo != null && oAttachmentInfo.IsPermanentOrTemporary.HasValue)
        {
            if (oAttachmentInfo.IsPermanentOrTemporary.Value)
            {
                output = LanguageUtil.GetValue(1681);//TODO: can change it from WebConstant.cs
            }
            else
            {
                output = LanguageUtil.GetValue(1682);
            }
        }
        return output;
    }
    #endregion

}
