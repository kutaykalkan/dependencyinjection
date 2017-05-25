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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Exception;

//TODO: if no value in grid then "-"
//count = 20;(hard coded yet), also should IsIncludeSRA be true when fetching the accounts
//use GetAccountAttributeIDCollection() properly with page enum passed

public partial class Pages_CertificationBalances : PageBaseRecPeriod
{

    private const WebEnums.ARTPages eARTPages = WebEnums.ARTPages.CertificationBalances;
    private const WebEnums.CertificationType eCertificationType = WebEnums.CertificationType.CertificationBalances;
    private int _CompanyID;
    private short _RoleID;
    private int _ReconciliationPeriodID;
    private int _UserID;
    private bool _IsDualReviewEnabled;
    private bool _IsMaterialityEnabled;
    public bool _IsMultiCurrencyEnabled;
    private bool _IsUserFromQueryString = false;
    string userName = "";
    string roleName = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

        Helper.SetPageTitle(this, 1210);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1210;

        Helper.ShowExportToolbarOnCertificationPages(this, true, "CertificationPrint/CertificationBalancesPrint.aspx", 1462, WebEnums.CertificationType.CertificationBalances);
        ucSkyStemARTGrid.Grid_MinimizeEventHandler += new UserControls_SkyStemARTGrid.Grid_Minimize(ucSkyStemARTGrid_Grid_MinimizeEventHandler);
        ucSkyStemARTGrid.Grid_MaximizeEventHandler += new UserControls_SkyStemARTGrid.Grid_Maximize(ucSkyStemARTGrid_Grid_MaximizeEventHandler);
        ucSkyStemARTGrid.GridCommand += new GridCommandEventHandler(ucSkyStemARTGrid_GridCommand);
        bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
        if (isShowContent)
        {
            CallEveryTime();
            if (!IsPostBack)
            {
                HttpContext.Current.Session.Remove(SessionConstants.CERTIFICATION_BALANCES_DATA);
                CallFirstTime();
                //CollapseGrouping();
            }

            ShowReadOnlyModeonBasisofRecordCount();




        }




    }

    void ucSkyStemARTGrid_GridCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
       {
           if (!e.Item.Expanded)
           {
               ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
               ucSkyStemARTGrid.Grid.AllowPaging = true;
               ucSkyStemARTGrid.Grid.PagerStyle.AlwaysVisible = true;
               ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
               BindGrid();
               ExpandedGrouping();
           }
           

       }    
    }

    void ucSkyStemARTGrid_Grid_MaximizeEventHandler()
    {

        ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
        ucSkyStemARTGrid.Grid.AllowPaging = true ;
        ucSkyStemARTGrid.Grid.PagerStyle.AlwaysVisible = true;
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        BindGrid();
        ExpandedGrouping();
    }

    void ucSkyStemARTGrid_Grid_MinimizeEventHandler()
    {
        ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
        ucSkyStemARTGrid.Grid.AllowPaging = false;
        ucSkyStemARTGrid.Grid.PagerStyle.AlwaysVisible = false;
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        BindGrid();

        CollapseGrouping();
        //throw new NotImplementedException();
    }
    private void CollapseGrouping()
    {
        GridItem[] oGridItemCollection = ucSkyStemARTGrid.Grid.MasterTableView.GetItems(GridItemType.GroupHeader);
        foreach (GridItem oGridItem in oGridItemCollection)
        {
            oGridItem.Expanded = false;
        }
    }
    private void ExpandedGrouping()
    {
        GridItem[] oGridItemCollection = ucSkyStemARTGrid.Grid.MasterTableView.GetItems(GridItemType.GroupHeader);
        foreach (GridItem oGridItem in oGridItemCollection)
        {
            oGridItem.Expanded = true;
        }
       
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
            if (isShowContent)
            {
                HttpContext.Current.Session.Remove(SessionConstants.CERTIFICATION_BALANCES_DATA);
                CallEveryTime();
                CallFirstTime();
                //CollapseGrouping();
            }
        }
    }

    private void CallEveryTime()
    {
        ucSkyStemARTGrid.BasePageTitle = 1210;
        //TODO: handle if coming from certificationStatus page , also breadCrumbs
        SetPrivateVariableValue();
        //PopulateBaseAndReportingCurrency();
    }
    private void CallFirstTime()
    {
        //HideApproverColumn();
        BindAccountGrid();
        lblCertificationVerbiage.Text = CertificationHelper.GetCertificationVerbiage(eCertificationType, userName, roleName);
        HandleFormModeForCertification();
        HandleCertificationSignOffDate();
    }

    private void BindAccountGrid()
    {
        gridSettings();
        BindGrid();
       
    }

    private void gridSettings()
    {
        ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
        ucSkyStemARTGrid.Grid.AllowPaging = true;
        ucSkyStemARTGrid.Grid.PagerStyle.AlwaysVisible = true;
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;

    }
    private void BindGrid()
    {
        ucSkyStemARTGrid.CompanyID = _CompanyID;
        ucSkyStemARTGrid.ShowFSCaptionAndAccountType();
        ucSkyStemARTGrid.GridGroupByExpression = Helper.GetGridGroupByExpressionForFSCaption();

        //ucSkyStemARTGrid.DataSource = oGLDataHdrInfoCollection;

        ucSkyStemARTGrid.DataSource = GetGridData();
        ucSkyStemARTGrid.BindGrid();

    }



    protected object ucSkyStemARTGrid_NeedDataSourceEventHandler(int count)
    {
        return GetGridData();
    }
    private List<GLDataHdrInfo> GetGridData()
    {
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
        try
        {
            oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.CERTIFICATION_BALANCES_DATA];

            if (oGLDataHdrInfoCollection == null || oGLDataHdrInfoCollection.Count == 0)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                //string sortExpression = "NetAccountID";
                //string sortDirection = "ASC";
                int count = 100;
                //oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserID(
                oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserIDForCertificationBalances(
                       this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
                       (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                       (short)ARTEnums.UserRole.PREPARER, (short)ARTEnums.UserRole.REVIEWER, (short)ARTEnums.UserRole.APPROVER,
                       (short)ARTEnums.UserRole.SYSTEM_ADMIN, (short)ARTEnums.UserRole.CEO_CFO, (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                       , true, count, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                       SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID()
                       , Helper.GetAppUserInfo());

                LanguageHelper.TranslateLabelsGLData(oGLDataHdrInfoCollection);
                Session[SessionConstants.CERTIFICATION_BALANCES_DATA] = oGLDataHdrInfoCollection;

                ucSkyStemARTGrid.DataSource = oGLDataHdrInfoCollection;
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

        return oGLDataHdrInfoCollection;
    }



    private void HandleFormModeForCertification()
    {
        WebEnums.FormMode eFormMode = Helper.GetFormModeForCertification(eARTPages);
        if (eFormMode == WebEnums.FormMode.ReadOnly || _IsUserFromQueryString == true)
        {
            btnAgree.Enabled = false;
            txtAdditionalComments.Visible = false;
            lblAdditionalCommentsValue.Visible = true;
        }
        else
            if (eFormMode == WebEnums.FormMode.Edit)
            {
                btnAgree.Enabled = true;
                txtAdditionalComments.Visible = true;
                lblAdditionalCommentsValue.Visible = false;
            }
    }

    private void HandleCertificationSignOffDate()
    {
        DateTime? signOffDate = null;
        string signOffComment = "";
        signOffDate = CertificationHelper.GetCertificationSignOffDateAndComment(eCertificationType, out signOffComment, _UserID, _RoleID);
        CertificationHelper.ShowHideSignature(ucSignature, signOffDate, userName);
        if (signOffDate.HasValue)
        {
            MakeSignatureVisible(signOffDate, userName);
            txtAdditionalComments.Visible = false;
            lblAdditionalCommentsValue.Visible = true;
        }
        else
        {
            ucSignature.Visible = false;
        }
        lblAdditionalCommentsValue.Text = signOffComment;
    }
    private void SetPrivateVariableValue()
    {
        this._CompanyID = SessionHelper.CurrentCompanyID.Value;
        this._ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.User_ID]))
        {
            this._UserID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
            _IsUserFromQueryString = true;
        }
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ROLE_ID]))
            this._RoleID = Convert.ToInt16(Request.QueryString[QueryStringConstants.ROLE_ID]);

        if (_UserID > 0 && _RoleID > 0)
        {
            _IsUserFromQueryString = true;
        }
        else
        {
            this._UserID = SessionHelper.CurrentUserID.Value;
            this._RoleID = SessionHelper.CurrentRoleID.Value;
            _IsUserFromQueryString = false;
        }
        userName = Helper.GetUserFullName(_UserID);
        roleName = Helper.GetRoleName(_RoleID);

        SetCapabilityInfo();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.User_ID] != null)
        {
            Helper.SetBreadcrumbs(this, 1072, 1464, 1210); ;
        }
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        CertificationHelper.ItemDataBoundCertificationBalances(e);
    }

    protected void btnAgree_Click(object sender, EventArgs e)
    {
        DateTime? signOffDate = SaveInDB();
        MakeSignatureVisible(signOffDate, userName);
    }

    private void MakeSignatureVisible(DateTime? signatureDate, string userName)
    {
        ucSignature.Visible = true;
        CertificationHelper.ShowHideSignature(ucSignature, signatureDate, userName);
        btnAgree.Visible = false ;
        txtAdditionalComments.Visible = false;
        lblAdditionalCommentsValue.Text = txtAdditionalComments.Text;
        lblAdditionalCommentsValue.Visible = true;
    }

    private DateTime? SaveInDB()
    {
        CertificationSignOffInfo oCertificationSignOffInfo = new CertificationSignOffInfo();
        oCertificationSignOffInfo.CompanyID = _CompanyID;
        oCertificationSignOffInfo.CertificationTypeID = (short)eCertificationType;
        oCertificationSignOffInfo.SignOffComments = txtAdditionalComments.Text;
        oCertificationSignOffInfo.UserID = _UserID;
        oCertificationSignOffInfo.RoleID = _RoleID;
        oCertificationSignOffInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oCertificationSignOffInfo.SignOffDate = DateTime.Now;

        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        oCertificationClient.SaveCertificationSignoffDetail(oCertificationSignOffInfo, Helper.GetAppUserInfo());

        return oCertificationSignOffInfo.SignOffDate;
    }

    public override string GetMenuKey()
    {
        return "CertificationBalanceAgreement";
    }

    private void SetCapabilityInfo()
    {
        List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();

        foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oCompanyCapabilityInfoCollection)
        {
            if (oCompanyCapabilityInfo.CapabilityID.HasValue)
            {
                ARTEnums.Capability oCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oCompanyCapabilityInfo.CapabilityID.Value.ToString());

                switch (oCapability)
                {
                    case ARTEnums.Capability.DualLevelReview:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsDualReviewEnabled = true;
                        }
                        break;
                    case ARTEnums.Capability.MaterialitySelection:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsMaterialityEnabled = true;
                        }
                        break;
                    case ARTEnums.Capability.MultiCurrency:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            _IsMultiCurrencyEnabled = true;
                        }
                        break;
                }
            }
        }
    }

    private void ShowReadOnlyModeonBasisofRecordCount()
    {
        //**** Show the form in Read Only Mode if No Account is Assigned to the User(i.e. Grid has No records)
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.CERTIFICATION_BALANCES_DATA];
        if (oGLDataHdrInfoCollection == null || oGLDataHdrInfoCollection.Count == 0)
        {
            btnAgree.Enabled = false;
            txtAdditionalComments.Visible = false;
            lblAdditionalCommentsValue.Visible = true;
        }


    }

    //private void PopulateBaseAndReportingCurrency()
    //{
    //    if (this._IsMultiCurrencyEnabled)
    //    {
    //        lblBaseCurrency.Visible = true;
    //        lblBaseCurrencyValue.Visible = true;
    //        lblBaseCurrencyValue.Text = SessionHelper.BaseCurrencyCode;
    //    }
    //    else
    //    {
    //        lblBaseCurrency.Visible = false;
    //        lblBaseCurrencyValue.Visible = false;
    //    }

    //    lblReportingCurrencyValue.Text = SessionHelper.ReportingCurrencyCode;
    //}



}//end of class
