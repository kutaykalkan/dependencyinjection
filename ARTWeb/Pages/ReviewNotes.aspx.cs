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
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Grid;
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Data;

public partial class Pages_ReviewNotes : PageBaseRecPeriod
{

    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    const string POPUP_PAGE = "EditItemReviewNote.aspx?";
    const int POPUP_WIDTH = 820;
    const int POPUP_HEIGHT = 500;
    WebEnums.FormMode _FormMode = WebEnums.FormMode.None;
    string _PopupUrl = string.Empty;
    bool isPagevalid = true;
    WebEnums.ARTPages _ARTPages = WebEnums.ARTPages.AccountViewer;
    #endregion

    #region Properties
    Int16? _RecCategoryTypeID = null;

    private GLDataHdrInfo _GLDataHdrInfo;
    private GLDataHdrInfo GLDataHdrInfo
    {
        get
        {
            if (this._GLDataHdrInfo == null)
            {
                if (ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] == null)
                {
                    if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
                    {
                        long? gldataid = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                        this._GLDataHdrInfo = Helper.GetGLDataHdrInfo(gldataid);
                    }
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

    public bool IsRefreshData
    {
        get
        {
            if (hdIsRefreshData.Value == "1")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                hdIsRefreshData.Value = "1";
            else
                hdIsRefreshData.Value = "0";
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        oMasterPageBase.ReconciliationPeriodChangedEventHandler += new EventHandler(oMasterPageBase_ReconciliationPeriodChangedEventHandler);

        if (!this.IsPostBack)
        {
            this.btnCancel.PostBackUrl = Request.UrlReferrer.PathAndQuery;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        /* 
         * 1. Get the GL Data ID
         * 2. Get the Review Notes assiciated with this GL Data ID
         * 3. Display in Grid with option to Edit
         * 4. Show a Button to Add Note
         * 4. handle the case of Rec Period Changed
         */

        Helper.RegisterPostBackToControls(this, rgReviewNotes);

        Helper.SetPageTitle(this, 1394);

        GetQueryStringValues();

        // Set default Sorting
        SetDefaultSortExpression();

        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            SessionHelper.ClearSearchResultsFromSession();
        }
        SetControlState();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        int lastPagePhreaseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
        if (lastPagePhreaseID != -1)
        {
            switch (_ARTPages)
            {
                case WebEnums.ARTPages.AccountViewer:
                    Helper.SetBreadcrumbs(this, 1071, 1187, lastPagePhreaseID, 1394);

                    break;

                case WebEnums.ARTPages.SystemReconciledAccounts:
                    Helper.SetBreadcrumbs(this, 1071, 1075, lastPagePhreaseID, 1394);
                    break;
            }

        }
        else
        {
            switch (_ARTPages)
            {
                case WebEnums.ARTPages.AccountViewer:
                    Helper.SetBreadcrumbs(this, 1071, 1187, 1394);
                    break;

                case WebEnums.ARTPages.SystemReconciledAccounts:
                    Helper.SetBreadcrumbs(this, 1071, 1075, 1394);
                    break;
            }
        }

        // Setup Add Button
        SetURLForAdd();

    }
    void oMasterPageBase_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {

            //_GLDataID = Helper.GetGLDataIDAndRecStatusForPeriodChange(this, _AccountID, _NetAccountID, Helper.GetRecTemplateForCurrentPage(this._RecCategoryTypeID), null);
            Helper.ValidateRecTemplateForGLDataID(this, this.GLDataHdrInfo, Helper.GetRecTemplateForCurrentPage(this._RecCategoryTypeID), null);
            Helper.HideMessage(this);

            // Set the Master Page Properties for GL Data ID
            RecHelper.SetRecStatusBarPropertiesForOtherPages(this, this.GLDataHdrInfo.GLDataID);
            IsRefreshData = true;
            SetControlState();
        }
        catch (ARTException ex)
        {
            isPagevalid = false;
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            isPagevalid = false;
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Grid Events
    protected void rgReviewNotes_ItemCreated(object sender, GridItemEventArgs e)
    {



        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgReviewNotes_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        string url = _PopupUrl;

        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo = (GLDataReviewNoteHdrInfo)e.Item.DataItem;

            // get the Labels
            ExLabel lblSubject = (ExLabel)e.Item.FindControl("lblSubject");
            ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");
            ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
            ExLabel lblDateRevised = (ExLabel)e.Item.FindControl("lblDateRevised");
            ExLabel lblRevisedBy = (ExLabel)e.Item.FindControl("lblRevisedBy");
            ExHyperLink hlItemPopup = (ExHyperLink)e.Item.FindControl("hlItemPopup");

            lblSubject.Text = Helper.GetDisplayStringValue(oGLDataReviewNoteHdrInfo.ReviewNoteSubject);
            lblDateAdded.Text = Helper.GetDisplayDateTime(oGLDataReviewNoteHdrInfo.DateAdded);
            lblAddedBy.Text = Helper.GetDisplayStringValue(oGLDataReviewNoteHdrInfo.AddedByFullName);
            lblDateRevised.Text = Helper.GetDisplayDateTime(oGLDataReviewNoteHdrInfo.DateRevised);
            lblRevisedBy.Text = Helper.GetDisplayStringValue(oGLDataReviewNoteHdrInfo.RevisedByFullName);

            url += "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataHdrInfo.GLDataID +
                "&" + QueryStringConstants.REVIEW_NOTE_ID + "=" + oGLDataReviewNoteHdrInfo.GLDataReviewNoteID;
            url += "&" + QueryStringConstants.PARENT_HIDDEN_FIELD + "=" + hdIsRefreshData.ClientID;

            hlItemPopup.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + url + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
            Helper.SetImageURLForViewVersusEdit(_FormMode, hlItemPopup);

            // Show / Hide Delete, based on the User who added it
            ImageButton imgBtnDelete = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];
            if (oGLDataReviewNoteHdrInfo.AddedByUserID == SessionHelper.CurrentUserID
                && _FormMode == WebEnums.FormMode.Edit)
            {
                // Show Delete
                imgBtnDelete.Visible = true;
                imgBtnDelete.CommandArgument = oGLDataReviewNoteHdrInfo.GLDataReviewNoteID.ToString();
            }
            else
            {
                imgBtnDelete.Visible = false;
            }

            ExHyperLink hlAttachmentCount = (ExHyperLink)e.Item.FindControl("hlAttachmentCount");
            hlAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLDataReviewNoteHdrInfo.AttachmentCount);
            string windowName;
            hlAttachmentCount.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + Helper.SetDocumentUploadURLForRecItemInput(this.GLDataHdrInfo.GLDataID, oGLDataReviewNoteHdrInfo.GLDataReviewNoteID, this.GLDataHdrInfo.AccountID, this.GLDataHdrInfo.NetAccountID, true, Request.Url.ToString(), out windowName, false, WebEnums.RecordType.GLDataReviewNote) + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
        }
    }
    protected void rgReviewNotes_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        List<GLDataReviewNoteHdrInfo> oGLDataReviewNoteHdrInfoCollection = null;
        if (isPagevalid)
        {
            try
            {
                // Check in Session Object
                oGLDataReviewNoteHdrInfoCollection = (List<GLDataReviewNoteHdrInfo>)Session[SessionConstants.SEARCH_RESULTS_REVIEW_NOTES];
                if (oGLDataReviewNoteHdrInfoCollection == null)
                {
                    // get from DB
                    IGLData oGLData = RemotingHelper.GetGLDataObject();
                    oGLDataReviewNoteHdrInfoCollection = oGLData.GetReviewNotes(this.GLDataHdrInfo.GLDataID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                    Session[SessionConstants.SEARCH_RESULTS_REVIEW_NOTES] = oGLDataReviewNoteHdrInfoCollection;
                    SessionHelper.ClearSearchResultsFromSession(SessionConstants.SEARCH_RESULTS_REVIEW_NOTES);
                }
                rgReviewNotes.MasterTableView.DataSource = oGLDataReviewNoteHdrInfoCollection;
                // Sort the Data
                GridHelper.SortDataSource(rgReviewNotes.MasterTableView);
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
        else
        {
            oGLDataReviewNoteHdrInfoCollection = new List<GLDataReviewNoteHdrInfo>();
            SessionHelper.ClearSession(SessionConstants.SEARCH_RESULTS_REVIEW_NOTES);
            rgReviewNotes.MasterTableView.DataSource = oGLDataReviewNoteHdrInfoCollection;
        }
    }
    protected void rgReviewNotes_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgReviewNotes, 1394);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgReviewNotes, 1394);

            }


        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void rgReviewNotes_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgReviewNotes.Rebind();
    }


    protected void rgReviewNotes_DeleteCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo = new GLDataReviewNoteHdrInfo();
            oGLDataReviewNoteHdrInfo.GLDataReviewNoteID = Convert.ToInt64(e.CommandArgument);
            oGLDataReviewNoteHdrInfo.DateRevised = DateTime.Now;
            oGLDataReviewNoteHdrInfo.RevisedByUserID = SessionHelper.CurrentUserID;
            oGLDataReviewNoteHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;

            // Called for Delete
            IGLData oGLData = RemotingHelper.GetGLDataObject();
            oGLData.DeleteReviewNote(oGLDataReviewNoteHdrInfo, Helper.GetAppUserInfo());

            // Remove the Object from Session and ReBind the GRid
            List<GLDataReviewNoteHdrInfo> oGLDataReviewNoteHdrInfoCollection = (List<GLDataReviewNoteHdrInfo>)Session[SessionConstants.SEARCH_RESULTS_REVIEW_NOTES];
            int index = oGLDataReviewNoteHdrInfoCollection.FindIndex(c => c.GLDataReviewNoteID == oGLDataReviewNoteHdrInfo.GLDataReviewNoteID);
            oGLDataReviewNoteHdrInfoCollection.RemoveAt(index);
            Session[SessionConstants.SEARCH_RESULTS_REVIEW_NOTES] = oGLDataReviewNoteHdrInfoCollection;

            // Rebind the Grid
            rgReviewNotes.Rebind();
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

    #region Other Events
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void SetControlState()
    {

        _PopupUrl = POPUP_PAGE + QueryStringConstants.ACCOUNT_ID + "=" + this.GLDataHdrInfo.AccountID
        + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.GLDataHdrInfo.NetAccountID
        + "&" + QueryStringConstants.REC_STATUS_ID + "=" + this.GLDataHdrInfo.ReconciliationStatusID;

        WebEnums.ReconciliationStatus eRecStatus = WebEnums.ReconciliationStatus.NotStarted;
        eRecStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), this.GLDataHdrInfo.ReconciliationStatusID.ToString());

        _FormMode = Helper.GetFormMode(WebEnums.ARTPages.ReviewNotes, this.GLDataHdrInfo);

        LoadData();
    }

    private void GetQueryStringValues()
    {

        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            this._RecCategoryTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);


        if (Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID] != null)
        {
            string strARTPages = Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID];
            _ARTPages = (WebEnums.ARTPages)Enum.Parse(typeof(WebEnums.ARTPages), Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID]);
        }
        if (!Page.IsPostBack)
        {
            Helper.ValidateRecTemplateForGLDataID(this, this.GLDataHdrInfo, Helper.GetRecTemplateForCurrentPage(this._RecCategoryTypeID), null);
        }
    }

    public void LoadData()
    {
        List<GLDataReviewNoteHdrInfo> oGLDataReviewNoteHdrInfoCollection = null;
        if (!Page.IsPostBack || IsRefreshData)
        {
            SessionHelper.ClearSession(SessionConstants.SEARCH_RESULTS_REVIEW_NOTES);
            // get from DB
            IGLData oGLData = RemotingHelper.GetGLDataObject();
            oGLDataReviewNoteHdrInfoCollection = oGLData.GetReviewNotes(this.GLDataHdrInfo.GLDataID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            Session[SessionConstants.SEARCH_RESULTS_REVIEW_NOTES] = oGLDataReviewNoteHdrInfoCollection;
            SessionHelper.ClearSearchResultsFromSession(SessionConstants.SEARCH_RESULTS_REVIEW_NOTES);
            rgReviewNotes.DataSource = oGLDataReviewNoteHdrInfoCollection;
            rgReviewNotes.DataBind();
            IsRefreshData = false;
        }
    }
    private void SetDefaultSortExpression()
    {
        // Get the Review Notes
        if (!Page.IsPostBack)
        {
            // Add Default Sort as Date Revised, Desc
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = "DateRevised";
            oGridSortExpression.SortOrder = GridSortOrder.Descending;
            rgReviewNotes.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
        }
    }

    private void SetURLForAdd()
    {
        btnAdd.Visible = false;
        if (isPagevalid)
        {
            string url = _PopupUrl;
            url += "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataHdrInfo.GLDataID;
            url += "&" + QueryStringConstants.PARENT_HIDDEN_FIELD + "=" + hdIsRefreshData.ClientID;

            ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());
            //if (eUserRole == ARTEnums.UserRole.REVIEWER
            //    || eUserRole == ARTEnums.UserRole.APPROVER)
            //{
            //    if (_FormMode == WebEnums.FormMode.Edit)
            //    {
            //        // Set the Open Window
            //        btnAdd.OnClientClick = "javascript:OpenRadWindow('" + url + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
            //        btnAdd.Visible = true;
            //    }
            //}
            if (eUserRole == ARTEnums.UserRole.REVIEWER
            || eUserRole == ARTEnums.UserRole.APPROVER
            || eUserRole == ARTEnums.UserRole.BACKUP_REVIEWER
            || eUserRole == ARTEnums.UserRole.BACKUP_APPROVER)
            {
                if (_FormMode == WebEnums.FormMode.Edit)
                {
                    // Set the Open Window
                    btnAdd.OnClientClick = "javascript:OpenRadWindow('" + url + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
                    btnAdd.Visible = true;
                }
            }
        }
    }
    private string GetPreviousPageName()
    {
        string PName = "";
        if (Request.UrlReferrer != null)
        {
            PName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        return PName;
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "AccountViewer";
    }

    #endregion

}
