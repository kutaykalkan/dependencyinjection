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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.Language.LanguageUtility;


public partial class Pages_Matching_ViewMatchSet : PageBaseMatching
{
    bool isExportPDF;
    bool isExportExcel;
    Int16? matchingTypeID = null;
    long? matchSetID = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        this.GLDataID = Helper.GetGLDataID(this, this.AccountID, this.NetAccountID);
        isExportPDF = false;
        isExportExcel = false;
        radMatching.MasterTableView.CurrentPageIndex = 0;
        GridSortExpression oGridSortExpression = new GridSortExpression();
        oGridSortExpression.FieldName = "DateAdded";
        oGridSortExpression.SortOrder = GridSortOrder.Descending;
        radMatching.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
        radMatching.Rebind();
        DisableButton();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SessionHelper.ClearCurrentMatchSet();
        Helper.SetPageTitle(this, 2185);
        radMatching.EntityNameLabelID = 2277;
        GetQueryStringValues();
        if (matchingTypeID == (short)ARTEnums.MatchingType.AccountMatching)
        {
            btnBack.Visible = true;
        }
        else
        {
            Session[SessionConstants.PARENT_PAGE_URL] = null;
            btnBack.Visible = false;
        }

        if (!Page.IsPostBack)
        {
            //has to be conditional
            MatchingHelper.ShowHideBackToRecFormBtn(this.btnBackToRecForm);
            DisableButton();
            isExportPDF = false;
            isExportExcel = false;
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = "DateAdded";
            oGridSortExpression.SortOrder = GridSortOrder.Descending;
            radMatching.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
            BindGrid();
            this.ReturnUrl = Helper.ReturnURL(this.Page);
        }
        GridHelper.SetRecordCount(radMatching);
    }
    public override string GetMenuKey()
    {
        return "Matching";
    }
    public void DisableButton()
    {
        btnCreateNew.Enabled = true;
        btnUploadNewFiles.Enabled = true;

        ARTEnums.MatchingType? eMatchingType = (ARTEnums.MatchingType?)matchingTypeID;

        WebEnums.ReconciliationStatus? eRecStatus = (WebEnums.ReconciliationStatus?)Helper.GetReconciliationStatusByGLDataID(this.GLDataID);
        GLDataAndAccountHdrInfo oGLDataHdrInfo = GetGlDataHdrInfoByGLDataID(this.GLDataID);
        MatchSetHdrInfo oMatchSetHdrInfo = new MatchSetHdrInfo();
        oMatchSetHdrInfo.PreparerUserID = oGLDataHdrInfo.PreparerUserID;
        oMatchSetHdrInfo.BackupPreparerUserID = oGLDataHdrInfo.BackupPreparerUserID;
        this.FormMode = MatchingHelper.GetFormModeForMatching(WebEnums.ARTPages.ViewMatchSets, eMatchingType, eRecStatus,
            this.GLDataID, oMatchSetHdrInfo);
        bool isEditMode = (this.FormMode == WebEnums.FormMode.Edit);
        btnCreateNew.Enabled = isEditMode;
        btnUploadNewFiles.Enabled = isEditMode;
    }

    private GLDataAndAccountHdrInfo GetGlDataHdrInfoByGLDataID(long? GLDataID)
    {
        bool isDualReviewEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview);
        bool isCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation);
        bool isMaterialityEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MaterialitySelection);

        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();//TODO: make a different Info object for this control and BLL methods
        GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = new GLDataAndAccountHdrInfo();
        if (GLDataID.HasValue && GLDataID.Value > 0)
        {
            oGLDataAndAccountHdrInfo = oGLDataClient.GetGLDataAndAccountInfoByGLDataID((long)GLDataID, (int)SessionHelper.CurrentReconciliationPeriodID,
                (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentUserID, (int)SessionHelper.CurrentRoleID
                , isDualReviewEnabled, isCertificationEnabled, isMaterialityEnabled, (short)WebEnums.CertificationType.Certification
                , (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                 (short)ARTEnums.AccountAttribute.BackupPreparer, (short)ARTEnums.AccountAttribute.BackupReviewer, (short)ARTEnums.AccountAttribute.BackupApprover, Helper.GetAppUserInfo());
        }

        return oGLDataAndAccountHdrInfo;
    }

    void BindGrid()
    {
        List<MatchSetHdrInfo> oMatchSetHdrInfoCollection = null;
        MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
        oMatchingParamInfo.UserID = SessionHelper.CurrentUserID;
        oMatchingParamInfo.RoleID = SessionHelper.CurrentRoleID;
        oMatchingParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oMatchingParamInfo.GLDataID = this.GLDataID;
        oMatchSetHdrInfoCollection = MatchingHelper.SelectAllMatchSetHdrInfo(oMatchingParamInfo);
        radMatching.DataSource = oMatchSetHdrInfoCollection;
        //radMatching.DataBind();

        // Sort the Data
        GridHelper.SortDataSource(radMatching.MasterTableView);
    }

    protected void radMatching_ItemCommand(object sender, GridCommandEventArgs e)
    {
        MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
        //GridDataItem item = (GridDataItem)e.Item;
        ExLabel lblMatchSetID = (ExLabel)e.Item.FindControl("lblMatchSetID");

        long? matchSetID = null;
        if (lblMatchSetID != null)
        {
            matchSetID = Convert.ToInt64(lblMatchSetID.Text);
            oMatchingParamInfo.MatchSetID = matchSetID;
            oMatchingParamInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.Matching;
            oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oMatchingParamInfo.DateRevised = DateTime.Now;

        }
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            // Commented by manoj : bug fix for nonproper align ment in PDF
            radMatching.MasterTableView.Columns.FindByUniqueName("MatchSetRef").HeaderStyle.Width = Unit.Pixel(30);
            //radMatching.MasterTableView.Columns.FindByUniqueName("MatchSetName").HeaderStyle.Width = Unit.Pixel(175);
            //radMatching.MasterTableView.Columns.FindByUniqueName("MatchingType").HeaderStyle.Width = Unit.Pixel(75);
            //radMatching.MasterTableView.Columns.FindByUniqueName("AccountName").HeaderStyle.Width = Unit.Pixel(75);
            //radMatching.MasterTableView.Columns.FindByUniqueName("DateAdded").HeaderStyle.Width = Unit.Pixel(60);
            //radMatching.MasterTableView.Columns.FindByUniqueName("AddedBy").HeaderStyle.Width = Unit.Pixel(75);
            //radMatching.MasterTableView.Columns.FindByUniqueName("ProcessedOn").HeaderStyle.Width = Unit.Pixel(100);

            radMatching.MasterTableView.Columns.FindByUniqueName("imgStatus").Display = false;
            radMatching.MasterTableView.Columns.FindByUniqueName("MatchingResult").Display = false;
            radMatching.MasterTableView.Columns.FindByUniqueName("Delete").Display = false;
            //GridHelper.SetStylesForExportGrid(
            GridHelper.ExportGridToPDF(radMatching, this.PageTitleLabelID);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            radMatching.MasterTableView.Columns.FindByUniqueName("imgStatus").Display = false;
            radMatching.MasterTableView.Columns.FindByUniqueName("MatchingResult").Display = false;
            radMatching.MasterTableView.Columns.FindByUniqueName("Delete").Display = false;
            GridHelper.ExportGridToExcel(radMatching, this.PageTitleLabelID);
        }
        if (e.CommandName == TelerikConstants.GridRefreshCommandName)
        {
            radMatching.Rebind();
        }

        if (e.CommandName == "DeleteMatchSet")
        {
            MatchingHelper.DeleteMatchSet(oMatchingParamInfo);
            //BindGrid();
            radMatching.Rebind();
        }
        if (e.CommandName == "EditMatchSet")
        {
            string url = e.CommandArgument.ToString();
            oMatchingParamInfo.MatchingStatusID = (short)WebEnums.MatchingStatus.Draft;
            MatchingHelper.EditMatchSet(oMatchingParamInfo);
            Response.Redirect(url);
            //radMatching.Rebind();
        }
    }

    protected void radMatching_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);
        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }

        //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
        //GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }


    /// <summary>
    /// Handles rad grids item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void radMatching_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            MatchSetHdrInfo oMatchSetHdrInfo = (MatchSetHdrInfo)e.Item.DataItem;

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                oMatchingParamInfo.MatchSetID = oMatchSetHdrInfo.MatchSetID;
                oMatchingParamInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.Matching;

                string url = URLConstants.URL_MATCHING_WIZARD + "?" +
                             QueryStringConstants.MATCH_SET_ID + "=" + oMatchSetHdrInfo.MatchSetID.ToString() + "&" +
                             QueryStringConstants.MATCHING_STATUS_ID + "=" + oMatchSetHdrInfo.MatchingStatusID.ToString() + "&" +
                             QueryStringConstants.MATCHING_TYPE_ID + "=" + oMatchSetHdrInfo.MatchingTypeID.ToString() + "&" +
                             QueryStringConstants.ADDEDBY_USER_ID + "=" + oMatchSetHdrInfo.AddedByUserID.ToString() + "&" +
                             QueryStringConstants.GLDATA_ID + "=" + oMatchSetHdrInfo.GLDataID.ToString() + "&" +
                             QueryStringConstants.ACCOUNT_ID + "=" + oMatchSetHdrInfo.AccountID.ToString();

                ExLabel lblMatchSetID = (ExLabel)e.Item.FindControl("lblMatchSetID");
                lblMatchSetID.Text = Helper.GetDisplayIntegerValue(oMatchSetHdrInfo.MatchSetID);

                if (oMatchSetHdrInfo.MatchSetID == matchSetID)
                {
                    e.Item.CssClass = "SelectedMatchSetRow";
                }


                ExHyperLink hlMatchSetName = (ExHyperLink)e.Item.FindControl("hlMatchSetName");
                hlMatchSetName.Text = Helper.GetDisplayStringValue(oMatchSetHdrInfo.MatchSetName);
                Helper.SetUrlForHyperlink(hlMatchSetName, url);

                ExHyperLink hlMatchSetRef = (ExHyperLink)e.Item.FindControl("hlMatchSetRef");
                hlMatchSetRef.Text = Helper.GetDisplayStringValue(oMatchSetHdrInfo.MatchSetRef);
                if (hlMatchSetRef.Text != "-")
                    Helper.SetUrlForHyperlink(hlMatchSetRef, url);

                ExHyperLink hlMatchingType = (ExHyperLink)e.Item.FindControl("hlMatchingType");
                hlMatchingType.Text = Helper.GetDisplayStringValue(oMatchSetHdrInfo.MatchingType);
                Helper.SetUrlForHyperlink(hlMatchingType, url);


                ExHyperLink hlAccount = (ExHyperLink)e.Item.FindControl("hlAccount");
                string AccountName = "";
                if (oMatchSetHdrInfo.AcountNameLabelID != null)
                {
                    AccountName = LanguageUtil.GetValue((Int32)oMatchSetHdrInfo.AcountNameLabelID);
                }
                hlAccount.Text = Helper.GetDisplayStringValue(AccountName);
                if (AccountName != "")
                    Helper.SetUrlForHyperlink(hlAccount, url);

                DateTime? DateRevised = null;
                if (oMatchSetHdrInfo.MatchingStatusID == (short)WebEnums.MatchingStatus.Success
                    || oMatchSetHdrInfo.MatchingStatusID == (short)WebEnums.MatchingStatus.Warning
                    || oMatchSetHdrInfo.MatchingStatusID == (short)WebEnums.MatchingStatus.Error)
                    DateRevised = oMatchSetHdrInfo.DateRevised;

                ExHyperLink hlProcessedOn = (ExHyperLink)e.Item.FindControl("hlProcessedOn");
                hlProcessedOn.Text = Helper.GetDisplayDateTime(DateRevised);
                if (DateRevised != null)
                    Helper.SetUrlForHyperlink(hlProcessedOn, url);

                ExHyperLink hlDateAdded = (ExHyperLink)e.Item.FindControl("hlDateAdded");
                hlDateAdded.Text = Helper.GetDisplayDate(oMatchSetHdrInfo.DateAdded);
                Helper.SetUrlForHyperlink(hlDateAdded, url);

                ExHyperLink hlAddedBy = (ExHyperLink)e.Item.FindControl("hlAddedBy");
                Helper.GetDisplayAddedBy(hlAddedBy, oMatchSetHdrInfo.FirstName, oMatchSetHdrInfo.LastName, oMatchSetHdrInfo.AddedBy);
                Helper.SetUrlForHyperlink(hlAddedBy, url);


                //ExHyperLink hlAddedByUserID = (ExHyperLink)e.Item.FindControl("hlAddedByUserID");
                //hlAddedByUserID.Text = Helper.GetDisplayIntegerValue(oMatchSetHdrInfo.AddedByUserID);
                //Helper.SetUrlForHyperlink(hlAddedByUserID, url);


                //ExHyperLink hlStatus = (ExHyperLink)e.Item.FindControl("hlStatus");
                //hlStatus.Text = Helper.GetDisplayStringValue(oMatchSetHdrInfo.MatchingStatus);
                //Helper.SetUrlForHyperlink(hlStatus, url);


                //WebEnums.MatchingStatus eMatchingStatus = (WebEnums.MatchingStatus)System.Enum.Parse(typeof(WebEnums.MatchingStatus), oMatchSetHdrInfo.MatchingStatusID.Value.ToString());
                ARTEnums.MatchingStatus eMatchingStatus = (ARTEnums.MatchingStatus)System.Enum.Parse(typeof(ARTEnums.MatchingStatus), oMatchSetHdrInfo.MatchingStatusID.Value.ToString());

                switch (eMatchingStatus)
                {
                    case ARTEnums.MatchingStatus.Success:
                        ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
                        imgSuccess.Visible = true;
                        break;
                    case ARTEnums.MatchingStatus.Error:
                        ExHyperLink hlFailure = (ExHyperLink)e.Item.FindControl("hlFailure");
                        string MatchsetStatusMessagesUrl = URLConstants.URL_MATCHSET_STATUS_MESSAGES + "?" +
                            QueryStringConstants.MATCH_SET_ID + "=" + oMatchSetHdrInfo.MatchSetID.ToString();
                        if (oMatchSetHdrInfo.GLDataID != null)
                            MatchsetStatusMessagesUrl += "&" + QueryStringConstants.GLDATA_ID + "=" + oMatchSetHdrInfo.GLDataID.ToString();
                        hlFailure.Visible = true;
                        hlFailure.NavigateUrl = MatchsetStatusMessagesUrl;
                        break;
                    case ARTEnums.MatchingStatus.Warning:
                        ExImage imgWarning = (ExImage)e.Item.FindControl("imgWarning");
                        imgWarning.Visible = true;
                        break;
                    case ARTEnums.MatchingStatus.InProgress:
                        ExImage imgProcessing = (ExImage)e.Item.FindControl("imgProcessing");
                        imgProcessing.Visible = true;
                        break;
                    case ARTEnums.MatchingStatus.ToBeProcessed:
                        ExImage imgToBeProcessed = (ExImage)e.Item.FindControl("imgToBeProcessed");
                        imgToBeProcessed.Visible = true;
                        break;
                    case ARTEnums.MatchingStatus.Draft:
                        ExImage imgDraft = (ExImage)e.Item.FindControl("imgDraft");
                        imgDraft.Visible = true;
                        break;
                }

                ExImageButton btnDelete = (ExImageButton)e.Item.FindControl("btnDelete");
                //Delete button visibility on the basis of RecProcessStatus,MatchingType,Matchingstatus etc
                ARTEnums.MatchingType? eMatchingType = (ARTEnums.MatchingType?)oMatchSetHdrInfo.MatchingTypeID;
                WebEnums.ReconciliationStatus? eRecStatus = (WebEnums.ReconciliationStatus?)Helper.GetReconciliationStatusByGLDataID(oMatchSetHdrInfo.GLDataID);
                WebEnums.FormMode? eFormMode = null;
                eFormMode = MatchingHelper.GetFormModeForMatching(WebEnums.ARTPages.ViewMatchSets, eMatchingType, eRecStatus, oMatchSetHdrInfo.GLDataID, oMatchSetHdrInfo);
                bool IsRecItemCreated = MatchingHelper.IsRecItemCreated(oMatchingParamInfo);
                if (eFormMode == WebEnums.FormMode.ReadOnly)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                    if (IsRecItemCreated)
                    {
                        //Rec Item Cascade delete message
                        btnDelete.Attributes.Add("onclick", "return ConfirmDeletion('" + LanguageUtil.GetValue(2241) + "');");
                    }
                    else
                    {
                        //Simple delete Confirmation message
                        btnDelete.Attributes.Add("onclick", "return ConfirmDeletion('" + LanguageUtil.GetValue(2236) + "');");
                    }
                }

                ExHyperLink hlReconciliationStatus = (ExHyperLink)e.Item.FindControl("hlReconciliationStatus");
                string recStatus = Helper.GetRecStatus(eRecStatus);
                hlReconciliationStatus.Text = Helper.GetDisplayStringValue(recStatus);
                if (!string.IsNullOrEmpty(recStatus))
                {
                    Helper.SetUrlForHyperlink(hlReconciliationStatus, url);
                }

                string matchResultUrl = URLConstants.URL_MATCHING_RESULT + "?" +
                             QueryStringConstants.MATCH_SET_ID + "=" + oMatchSetHdrInfo.MatchSetID.ToString();
                if (oMatchSetHdrInfo.MatchingTypeID != null)
                    matchResultUrl += "&" + QueryStringConstants.MATCHING_TYPE_ID + "=" + oMatchSetHdrInfo.MatchingTypeID.ToString();
                if (oMatchSetHdrInfo.GLDataID != null)
                    matchResultUrl += "&" + QueryStringConstants.GLDATA_ID + "=" + oMatchSetHdrInfo.GLDataID.ToString();
                if (oMatchSetHdrInfo.AccountID != null)
                    matchResultUrl += "&" + QueryStringConstants.ACCOUNT_ID + "=" + oMatchSetHdrInfo.AccountID.ToString();

                ExHyperLink hlMatchingResult = (ExHyperLink)e.Item.FindControl("hlMatchingResult");
                Helper.SetUrlForHyperlink(hlMatchingResult, matchResultUrl);

                if (oMatchSetHdrInfo.MatchingStatusID != (short)WebEnums.MatchingStatus.Success)
                    hlMatchingResult.Visible = false;
                showHideEdit(e, eMatchingStatus, IsRecItemCreated, eFormMode, url);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void showHideEdit(Telerik.Web.UI.GridItemEventArgs e, ARTEnums.MatchingStatus eMatchingStatus, bool IsRecItemCreated, WebEnums.FormMode? eFormMode, string url)
    {
        ExImageButton btnEditMatchSet = (ExImageButton)e.Item.FindControl("btnEditMatchSet");
        btnEditMatchSet.CommandArgument = url;
        if (eFormMode == WebEnums.FormMode.ReadOnly)
        {
            btnEditMatchSet.Visible = false;
        }
        else
        {

            switch (eMatchingStatus)
            {
                case ARTEnums.MatchingStatus.Success:
                    if (IsRecItemCreated)
                    {
                        btnEditMatchSet.Visible = false;
                    }
                    else
                    {
                        btnEditMatchSet.Visible = true;
                    }
                    btnEditMatchSet.Attributes.Add("onclick", "return ConfirmDeletion('" + LanguageUtil.GetValue(5000355) + "');");
                    break;
                case ARTEnums.MatchingStatus.Draft:
                    if (IsRecItemCreated)
                    {
                        btnEditMatchSet.Visible = false;
                    }
                    else
                    {
                        btnEditMatchSet.Visible = true;
                    }
                    break;

                case ARTEnums.MatchingStatus.Error:
                case ARTEnums.MatchingStatus.Warning:
                case ARTEnums.MatchingStatus.InProgress:
                case ARTEnums.MatchingStatus.ToBeProcessed:
                    btnEditMatchSet.Visible = false;
                    break;

            }



        }
    }

    protected void radMatching_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        BindGrid();
    }

    protected void radMatching_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        radMatching.Rebind();
    }

    protected void btnCreateNew_Click(Object Sender, EventArgs e)
    {
        string navUrl = URLConstants.URL_MATCHING_WIZARD + "?" + QueryStringConstants.MATCHING_TYPE_ID + "=" + matchingTypeID;
        if (this.GLDataID != null)
            navUrl += "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID.ToString();
        if (this.AccountID != null)
            navUrl += "&" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID.ToString();
        Response.Redirect(navUrl, false);
    }


    protected void btnUploadNewFiles_Click(Object Sender, EventArgs e)
    {

        string navUrl = URLConstants.URL_MATCHING_SOURCE_DATAIMPORT + "?" + QueryStringConstants.MATCHING_TYPE_ID + "=" + matchingTypeID;
        if (this.GLDataID != null)
            navUrl += "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID.ToString();
        if (this.AccountID != null)
            navUrl += "&" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID.ToString();
        Response.Redirect(navUrl);
    }

    protected void btnUploadStatus_Click(Object Sender, EventArgs e)
    {
        Response.Redirect(URLConstants.URL_MATCHING_SOURCE_DATAIMPORT_STATUS);
    }

    private void GetQueryStringValues()
    {
        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
        {
            this.GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        }

        if (Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID] != null)
        {
            this.matchingTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]);
        }
        else
        {
            this.matchingTypeID = (short)ARTEnums.MatchingType.DataMatching;
        }

        if (Request.QueryString[QueryStringConstants.MATCH_SET_ID] != null)
        {
            this.matchSetID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCH_SET_ID]);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (matchingTypeID == (short)ARTEnums.MatchingType.AccountMatching)
        {
            Response.Redirect(ReturnUrl, true);
        }
    }

    protected void btnBackToRecForm_Click(object sender, EventArgs e)
    {

        if (Session[SessionConstants.PARENT_PAGE_URL] != null)
        {
            String Url = Session[SessionConstants.PARENT_PAGE_URL].ToString();
            Response.Redirect(Url, true);
        }
    }


}
