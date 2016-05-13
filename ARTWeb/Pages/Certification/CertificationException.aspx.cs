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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Exception;

public partial class Pages_CertificationException : PageBaseRecPeriod
{


    private const WebEnums.ARTPages eARTPages = WebEnums.ARTPages.CertificationException;
    private const WebEnums.CertificationType eCertificationType = WebEnums.CertificationType.ExceptionCertification;
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
        Helper.SetPageTitle(this, 1211);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);
        Helper.ShowExportToolbarOnCertificationPages(this, true, "CertificationPrint/CertificationExceptionPrint.aspx", 1211, WebEnums.CertificationType.ExceptionCertification);
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1211;
        bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
        if (isShowContent)
        {
            CallEveryTime();
            if (!IsPostBack)
            {
                HttpContext.Current.Session.Remove(SessionConstants.CERTIFICATION_EXCEPTION_DATA);
                CallFirstTime();
            }
        }
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
            if (isShowContent)
            {
                HttpContext.Current.Session.Remove(SessionConstants.CERTIFICATION_EXCEPTION_DATA);
                CallEveryTime();
                CallFirstTime();
            }
        }
    }

    private void CallEveryTime()
    {
        ucSkyStemARTGrid.BasePageTitle = 1211;
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

        //decimal total = GetGridData().Sum<GLDataHdrInfo>();
    }

    //private void HideApproverColumn()
    //{
    //    if (this._IsDualReviewEnabled == false)
    //    {
    //        GridColumn oGridColumn = ucSkyStemARTGrid.Grid.Columns.FindByUniqueName("Approver");
    //        if (oGridColumn != null)
    //        {
    //            oGridColumn.Visible = false;
    //        }
    //    }
    //}

    private void BindAccountGrid()
    {
        ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
        ucSkyStemARTGrid.Grid.AllowPaging = true;
        ucSkyStemARTGrid.Grid.PagerStyle.AlwaysVisible = true;

        //IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        //List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
        //string sortExpression = "NetAccountID";
        //string sortDirection = "ASC";
        //int count = 100;
        //oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserID(
        //       this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
        //       (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
        //       (short)WebEnums.UserRole.PREPARER, (short)WebEnums.UserRole.REVIEWER, (short)WebEnums.UserRole.APPROVER,
        //       (short)WebEnums.UserRole.SYSTEM_ADMIN, (short)WebEnums.UserRole.CEO_CFO, (short)WebEnums.UserRole.SKYSTEM_ADMIN
        //       , true, count, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
        //       SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
        //       sortExpression, sortDirection);


        ////TODO: filter properly
        //decimal companyUnexplainedVarianceThreshold = GetCompanyUnexplainedVarianceThreshold();
        //List<GLDataHdrInfo> oGLDataHdrInfoFilteredCollection = (from oGLDataHdrInfo in oGLDataHdrInfoCollection
        //                                                        where (oGLDataHdrInfo.UnexplainedVarianceReportingCurrency.HasValue && Math.Abs(oGLDataHdrInfo.UnexplainedVarianceReportingCurrency.Value) > Math.Abs(companyUnexplainedVarianceThreshold))
        //                                                        ||(oGLDataHdrInfo.WriteOnOffAmountReportingCurrency.HasValue && Math.Abs(oGLDataHdrInfo.WriteOnOffAmountReportingCurrency.Value) > 0) //TODO: may be check if even a single entry for wrightOnOff then true
        //                                                        || (oGLDataHdrInfo.ReconciliationStatusID.HasValue && oGLDataHdrInfo.ReconciliationStatusID != (short)WebEnums.ReconciliationStatus.Reconciled)
        //                                                        //&& duedateForUser < DateTime.Now()) //or just status in not reconciled as rec process must have already stopped eo start certification
        //                                                        select oGLDataHdrInfo).ToList();
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        ucSkyStemARTGrid.CompanyID = _CompanyID;
        ucSkyStemARTGrid.ShowFSCaptionAndAccountType();
        //ucSkyStemARTGrid.GridGroupByExpression = Helper.GetGridGroupByExpressionForFSCaption();
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
        List<GLDataHdrInfo> oGLDataHdrInfoFilteredCollection = null;
        try
        {
            oGLDataHdrInfoFilteredCollection = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.CERTIFICATION_EXCEPTION_DATA];
            if (oGLDataHdrInfoFilteredCollection == null || oGLDataHdrInfoFilteredCollection.Count == 0)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                string sortExpression = "NetAccountID";
                string sortDirection = "ASC";
                int count = 100;
                oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserID(null,
                       this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
                       (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                       (short)WebEnums.UserRole.PREPARER, (short)WebEnums.UserRole.REVIEWER, (short)WebEnums.UserRole.APPROVER,
                       (short)WebEnums.UserRole.SYSTEM_ADMIN, (short)WebEnums.UserRole.CEO_CFO, (short)WebEnums.UserRole.SKYSTEM_ADMIN
                       , true, count, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                       SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
                       sortExpression, sortDirection, Helper.GetAppUserInfo());
                //TODO: filter properly
                decimal companyUnexplainedVarianceThreshold = GetCompanyUnexplainedVarianceThreshold();
                oGLDataHdrInfoFilteredCollection = (from oGLDataHdrInfo in oGLDataHdrInfoCollection
                                                    where (oGLDataHdrInfo.UnexplainedVarianceReportingCurrency.HasValue && Math.Abs(oGLDataHdrInfo.UnexplainedVarianceReportingCurrency.Value) > Math.Abs(companyUnexplainedVarianceThreshold))
                                                    || (oGLDataHdrInfo.WriteOnOffAmountReportingCurrency.HasValue && Math.Abs(oGLDataHdrInfo.WriteOnOffAmountReportingCurrency.Value) > 0) //TODO: may be check if even a single entry for wrightOnOff then true
                                                    || ((oGLDataHdrInfo.ReconciliationStatusID.HasValue && (oGLDataHdrInfo.ReconciliationStatusID != (short)WebEnums.ReconciliationStatus.Reconciled && oGLDataHdrInfo.ReconciliationStatusID != (short)WebEnums.ReconciliationStatus.SysReconciled))
                                                        && SessionHelper.CurrentCertificationStartDate.HasValue && SessionHelper.CurrentCertificationStartDate.Value < DateTime.Now)
                                                    //&& duedateForUser < DateTime.Now()) //or just status in not reconciled as rec process must have already stopped eo start certification
                                                    select oGLDataHdrInfo).ToList();
                LanguageHelper.TranslateLabelsGLData(oGLDataHdrInfoFilteredCollection);
                Session[SessionConstants.CERTIFICATION_EXCEPTION_DATA] = oGLDataHdrInfoFilteredCollection;
                ucSkyStemARTGrid.DataSource = oGLDataHdrInfoFilteredCollection;
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

        return oGLDataHdrInfoFilteredCollection;
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
        if (signOffDate.HasValue)
        {
            MakeSignatureVisible(signOffDate, userName);
            //txtAdditionalComments.Visible = false;
            //lblAdditionalCommentsValue.Visible = true;
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
            Helper.SetBreadcrumbs(this, 1072, 1464, 1211);
        }
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        CertificationHelper.ItemDataBoundCertificationException(e);
        if (e.Item.ItemType == GridItemType.Footer)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoList = GetGridData();

            decimal totalUnexplainedVariance = (from item in oGLDataHdrInfoList
                                                where item.UnexplainedVarianceReportingCurrency.HasValue
                                                select Math.Abs(item.UnexplainedVarianceReportingCurrency.Value)).Sum();
            decimal totalWriteOffOn = (from item in oGLDataHdrInfoList
                                       where item.WriteOnOffAmountReportingCurrency.HasValue
                                       select Math.Abs(item.WriteOnOffAmountReportingCurrency.Value)).Sum();

            //decimal totalUnexplainedVariance = (from item in oGLDataHdrInfoList
            //                                    select item.UnexplainedVarianceReportingCurrency.Value).Sum();
            //decimal totalWriteOffOn = (from item in oGLDataHdrInfoList
            //                           select item.WriteOnOffAmountReportingCurrency.Value).Sum();


            ExLabel lblTotalWriteOffOn = (ExLabel)e.Item.FindControl("lblTotalWriteOffOn");
            ExLabel lblTotalUnexplainedVariance = (ExLabel)e.Item.FindControl("lblTotalUnexplainedVariance");

            GridFooterItem oGridFooterItem = e.Item as GridFooterItem;
            oGridFooterItem["Preparer"].ColumnSpan = 2;
            oGridFooterItem["Reviewer"].Visible = false;

            lblTotalUnexplainedVariance.Text = Helper.GetDisplayDecimalValue(Math.Abs(totalUnexplainedVariance));
            lblTotalWriteOffOn.Text = Helper.GetDisplayDecimalValue(Math.Abs(totalWriteOffOn));
        }
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataHdrInfo oGLDataHdrInfo = (GLDataHdrInfo)e.Item.DataItem;
            ExHyperLink hlUnexplainedVariance = (ExHyperLink)e.Item.FindControl("hlUnexplainedVariance");
            ExHyperLink hlWriteOffOn = (ExHyperLink)e.Item.FindControl("hlWriteOffOn");

            //hlUnexplainedVariance.Text = Helper.GetDisplayDecimalValue(Math.Abs (oGLDataHdrInfo.UnexplainedVarianceReportingCurrency.Value));
            //hlWriteOffOn.Text = Helper.GetDisplayDecimalValue(Math.Abs (oGLDataHdrInfo.WriteOnOffAmountReportingCurrency.Value ));
            if (oGLDataHdrInfo.UnexplainedVarianceReportingCurrency.HasValue)
                hlUnexplainedVariance.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfo.UnexplainedVarianceReportingCurrency.Value);
            if (oGLDataHdrInfo.WriteOnOffAmountReportingCurrency.HasValue)
                hlWriteOffOn.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfo.WriteOnOffAmountReportingCurrency.Value);


        }

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
        btnAgree.Visible = false;
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
        //oCertificationSignOffInfo.CertificationVerbiageID = 100;  // _CertificationVerbiageID;//TODO:
        //oCertificationSignOffInfo.UserName = Helper.GetUserFullName();
        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        oCertificationClient.SaveCertificationSignoffDetail(oCertificationSignOffInfo,Helper.GetAppUserInfo());

        return oCertificationSignOffInfo.SignOffDate;
    }

    public override string GetMenuKey()
    {
        return "ExceptionAgreement";
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

    private decimal GetCompanyUnexplainedVarianceThreshold()
    {
        //decimal? companyUnexplainedVarianceThreshold = null;
        decimal companyUnexplainedVarianceThreshold = 0;
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<CompanyUnexplainedVarianceThresholdInfo> oCompanyUnexplainedVarianceThresholdInfoCollection = oCompanyClient.GetUnexplainedVarianceThresholdByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (oCompanyUnexplainedVarianceThresholdInfoCollection != null && oCompanyUnexplainedVarianceThresholdInfoCollection.Count > 0)
        {
            if (oCompanyUnexplainedVarianceThresholdInfoCollection[0].CompanyUnexplainedVarianceThreshold.HasValue)
            {
                companyUnexplainedVarianceThreshold = oCompanyUnexplainedVarianceThresholdInfoCollection[0].CompanyUnexplainedVarianceThreshold.Value;
            }
        }
        return companyUnexplainedVarianceThreshold;
    }

}//end of class
