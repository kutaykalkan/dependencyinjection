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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model.Report;


public partial class Pages_ReportRun : PageBase
{
    short? _reportID = 0;

    private bool _IsDualReviewEnabled;
    public bool _IsMaterialityEnabled = false;
    private bool _IsZeroBalanceEnabled;
    private bool _IsKeyAccountEnabled;
    private bool _IsRiskRatingEnabled;
    public bool _IsNetAccountEnabled = false;
    public bool _IsMultiCurrencyEnabled = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 1631);
        //if (isComingFromParameterPage)//or from activity or saved reports
        //{
        Helper.SetBreadcrumbs(this, 1073, 1563, 1631);
        //}
        //else
        //{
        //    Helper.SetBreadcrumbs(this, 1073, 1375);
        //}
        if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
            _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);
        if (!IsPostBack)
        {
            hlReselectParameters.NavigateUrl = "~/Pages/ReportParameter.aspx?" + QueryStringConstants.REPORT_ID + "=" + _reportID;
            hlReselectParameters.Text = "Change Parameters";
            lblReportPeriodValue.Text = "10/12/2009";//TODO: change with the passed parameters
            IReport oReportClient = RemotingHelper.GetReportObject();
            //lblReportDetails.LabelID = oReportClient.GetReportByID(_reportID).ReportLabelID.Value;
            divComment.Visible = false;
        }



        List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
        SetCapabilityInfo(oCompanyCapabilityInfoCollection);
        RunReport();




    }

    protected void btnReportSignOff_Click(object sender, EventArgs e)
    {
        divComment.Visible = true;
    }
    protected void btnArchive_Click(object sender, EventArgs e)
    {
    }
    protected void btnSignOff_Click(object sender, EventArgs e)
    {
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divComment.Visible = false;
    }

    public override string GetMenuKey()
    {
        return "";
    }















    private ReportSearchCriteria GetSearchCriteria()
    {
        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();
        try
        {
            //Get Organizational Keys and Values
            GetOrganizationalKeysAndValues(oReportSearchCriteria);

            //oReportSearchCriteria.ReportID  = 1;


            oReportSearchCriteria.CompanyID = 1;
            //oReportSearchCriteria.FromAccountNumber = txtAcNumber.Text.Trim();
            //oReportSearchCriteria.ToAccountNumber = txtToAcNumber.Text.Trim();
            //oReportSearchCriteria.IsKeyccount = true;
            oReportSearchCriteria.IsZeroBalanceAccount = true;
            //oReportSearchCriteria.RiskRatingID = Convert.ToInt32(ddlRiskRating.SelectedValue);
            oReportSearchCriteria.ReconciliationPeriodID = 3;

            //oReportSearchCriteria.IsDualReviewEnabled = this._IsDualReviewEnabled;
            //oReportSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;

            oReportSearchCriteria.IsRiskRatingEnabled = true;
            oReportSearchCriteria.IsZeroBalanceAccountEnabled = true;
            oReportSearchCriteria.LCID = 1034;
            oReportSearchCriteria.BusinessEntityID = 1;
            oReportSearchCriteria.DefaultLanguageID = 1033;
            oReportSearchCriteria.RequesterUserID = 2;
            oReportSearchCriteria.RequesterRoleID = 2;


            oReportSearchCriteria.Count = 100;

            //oReportSearchCriteria.IsMaterialAccount  = true ;
            oReportSearchCriteria.ExcludeNetAccount = true;
            oReportSearchCriteria.RiskRatingIDs = "1,2,3";
            oReportSearchCriteria.ReasonCodeIDs = "1,2,3";
            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;


        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }

        return oReportSearchCriteria;
    }

    private void GetOrganizationalKeysAndValues(ReportSearchCriteria oReportSearchCriteria)
    {

        oReportSearchCriteria.Key2 = 2;
        oReportSearchCriteria.Key2Value = "Ger";

    }


    private void RunReport()
    {
        List<UnusualBalancesReportInfo> oUnusualBalancesReportInfoCollection = null;
        //ReportSearchCriteria oReportSearchCriteria = this.GetSearchCriteria();
        ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
        
        IReport oReportClient = RemotingHelper.GetReportObject();
        DataTable dt = GetEntitySearchCriteria();
        oUnusualBalancesReportInfoCollection = oReportClient.GetReportUnusualBalancesReport(oReportSearchCriteria, dt, Helper.GetAppUserInfo());
        oUnusualBalancesReportInfoCollection = LanguageHelper.TranslateLabelsUnusualBalancesReport(oUnusualBalancesReportInfoCollection);
        if (oUnusualBalancesReportInfoCollection != null)
        {
            hlReselectParameters.Text = oUnusualBalancesReportInfoCollection.Count.ToString();
        }
        ucSkyStemARTGrid.ShowStatusImageColumn = true;
        ucSkyStemARTGrid.CompanyID = 1;
        ucSkyStemARTGrid.DataSource = oUnusualBalancesReportInfoCollection;
        ucSkyStemARTGrid.BindGrid();
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            UnusualBalancesReportInfo oGLDataAndAccountHdrInfo = (UnusualBalancesReportInfo)e.Item.DataItem;

            ExHyperLink hlAccountNumber = (ExHyperLink)e.Item.FindControl("hlAccountNumber");
            ExHyperLink hlAccountName = (ExHyperLink)e.Item.FindControl("hlAccountName");
            ExHyperLink hlGLBalance = (ExHyperLink)e.Item.FindControl("hlGLBalance");
            ExHyperLink hlRiskRating = (ExHyperLink)e.Item.FindControl("hlRiskRating");
            ExHyperLink hlIsMaterial = (ExHyperLink)e.Item.FindControl("hlIsMaterial");
            ExHyperLink hlIsKeyAccount = (ExHyperLink)e.Item.FindControl("hlIsKeyAccount");
            ExHyperLink hlPreparer = (ExHyperLink)e.Item.FindControl("hlPreparer");
            ExHyperLink hlReason = (ExHyperLink)e.Item.FindControl("hlReason");
            


            hlAccountNumber.Text = oGLDataAndAccountHdrInfo.AccountNumber;
            hlAccountName.Text = oGLDataAndAccountHdrInfo.AccountName;
            hlGLBalance.Text = Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.GLBalanceReportingCurrency);
            hlRiskRating.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.RiskRating);//TODO: get from labelID,oGLDataAndAccountHdrInfo.RiskRatingLabelID
            hlIsMaterial.Text = ReportHelper.SetYesNoCodeBasedOnBool(oGLDataAndAccountHdrInfo.IsMaterial);
            hlIsKeyAccount.Text = ReportHelper.SetYesNoCodeBasedOnBool(oGLDataAndAccountHdrInfo.IsKeyAccount);
            hlPreparer.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.PreparerFirstName + oGLDataAndAccountHdrInfo.PreparerLastName);
            hlReason.Text = oGLDataAndAccountHdrInfo.Reason ;

            //string url = Helper.GetHyperlinkForAccountViewer(oGLDataAndAccountHdrInfo.ReconciliationTemplateID, oGLDataAndAccountHdrInfo.AccountID.ToString(), oGLDataAndAccountHdrInfo.GLDataID.ToString(), oGLDataAndAccountHdrInfo.NetAccountID.ToString());
            //hlAccountNumber.NavigateUrl = url;
            //hlAccountName.NavigateUrl = url;
            //hlGLBalance.NavigateUrl = url;
            //hlRiskRating.NavigateUrl = url;
            //hlIsMaterial.NavigateUrl = url;
            //hlIsKeyAccount.NavigateUrl = url;
            //hlPreparer.NavigateUrl = url;
            //hlReviewer.NavigateUrl = url;
            //hlApprover.NavigateUrl = url;
            //Helper.SetHyperLinkForOrganizationalHierarchyColumns(url, e);
        }
    }



    





    
    private void SetTextForHyperlink(ExHyperLink oExHyperLink, string value)
    {
        if (oExHyperLink != null)
        {
            oExHyperLink.Text = value;
        }
    }


    private ReportSearchCriteria GetNormalSearchCriteria()
    {


        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();
        {
            //oReportSearchCriteria.ReportID  = 1;

            oReportSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID ;
            //oReportSearchCriteria.FromAccountNumber = txtAcNumber.Text.Trim();
            //oReportSearchCriteria.ToAccountNumber = txtToAcNumber.Text.Trim();
            //oReportSearchCriteria.IsKeyccount = true;
            oReportSearchCriteria.IsZeroBalanceAccount = true;
            //oReportSearchCriteria.RiskRatingID = Convert.ToInt32(ddlRiskRating.SelectedValue);
            oReportSearchCriteria.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value  ;
            oReportSearchCriteria.IsDualReviewEnabled = this._IsDualReviewEnabled;
            oReportSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;
            oReportSearchCriteria.IsRiskRatingEnabled = this._IsRiskRatingEnabled;
            oReportSearchCriteria.IsZeroBalanceAccountEnabled = this._IsZeroBalanceEnabled;
            oReportSearchCriteria.LCID = SessionHelper.GetUserLanguage();
            oReportSearchCriteria.BusinessEntityID = SessionHelper.GetBusinessEntityID();
            oReportSearchCriteria.DefaultLanguageID = AppSettingHelper.GetDefaultLanguageID();
            oReportSearchCriteria.RequesterUserID = SessionHelper.CurrentUserID.Value  ;
            oReportSearchCriteria.RequesterRoleID = SessionHelper.CurrentRoleID.Value  ;
            oReportSearchCriteria.Count = 100;

            //oReportSearchCriteria.IsMaterialAccount  = true ;
            oReportSearchCriteria.ExcludeNetAccount = true;
            //oReportSearchCriteria.RiskRatingIDs = "1,2,3";
            //oReportSearchCriteria.ReasonCodeIDs = "1,2,3";
            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;
        }

        Dictionary<string, string> oCriteriaCollection = null;
        if (Session[SessionConstants.REPORT_CRITERIA] != null)
        {
            oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
            foreach (KeyValuePair<string, string> keyValuePair in oCriteriaCollection)
            {
                switch (keyValuePair.Key)
                {
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMACCOUNT :
                        oReportSearchCriteria.FromAccountNumber = keyValuePair.Value;
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOACCOUNT :
                        oReportSearchCriteria.ToAccountNumber = keyValuePair.Value;
                        break;

                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_REASON:
                        oReportSearchCriteria.ReasonCodeIDs  = keyValuePair.Value;
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISMATERIALACCOUNT:
                        oReportSearchCriteria.IsMaterialAccount = GetBoolValueFromKeyValue(keyValuePair.Value);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RISKRATING:
                        oReportSearchCriteria.RiskRatingIDs  = keyValuePair.Value;
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISKEYACCOUNT:
                        oReportSearchCriteria.IsKeyccount = GetBoolValueFromKeyValue(keyValuePair.Value);
                        break;

                }
            }
        }
        return oReportSearchCriteria;
    }

    private bool? GetBoolValueFromKeyValue(string value)
    {
        bool? returnValue= null;
        if (!string.IsNullOrEmpty(value))// && (value is bool ))
        {
            return Convert.ToBoolean(value);
        }
        return returnValue ;
    }


    private DataTable GetEntitySearchCriteria()
    {
        DataTable dt = new DataTable("IDTable");
        DataColumn dcKeyID = dt.Columns.Add("KeyID");
        DataColumn dcValue = dt.Columns.Add("Value");
        DataRow dr= null ;
        Dictionary<string, string> oCriteriaCollection = null;
        if (Session[SessionConstants.REPORT_CRITERIA] != null)
        {
            oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
            foreach (KeyValuePair<string, string> keyValuePair in oCriteriaCollection)
            {
                switch (keyValuePair.Key)
                {
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key2);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY3:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key3);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY4:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key4);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY5:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key5);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY6:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key6);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY7:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key7);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY8:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key8);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY9:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key9);
                        break;
                }
            }
        }
        return dt;
    }


    private void AddSameLevelEntityInTable(KeyValuePair<string, string> keyValuePair,DataTable dt,DataRow dr,WebEnums.GeographyClass eGeographyClass)
    {
        if (!string.IsNullOrEmpty(keyValuePair.Value))
        {
            string[] arrayKeyValue = keyValuePair.Value.Split(',');
            if (arrayKeyValue != null && arrayKeyValue.Length > 0)
            {
                for (int i = 0; i < arrayKeyValue.Length; i++)
                {
                    dr = dt.NewRow();
                    //dr["KeyID"] = keyValuePair.Key.Substring(keyValuePair.Key.Length - 1, 1);
                    dr["KeyID"] = (short) eGeographyClass;
                    dr["Value"] = arrayKeyValue[i];
                    dt.Rows.Add(dr);
                }
            }
        }
    }

    private void SetCapabilityInfo(List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection)
    {
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

                    case ARTEnums.Capability.KeyAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsKeyAccountEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.NetAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsNetAccountEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.RiskRating:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsRiskRatingEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.ZeroBalanceAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsZeroBalanceEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.MultiCurrency:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsMultiCurrencyEnabled = true;
                        }
                        break;
                }
            }
        }
    }


}//end of class










//private ReportSearchCriteria GetSearchCriteria()
//    {
//        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();
//        try
//        {
//            //Get Organizational Keys and Values
//            GetOrganizationalKeysAndValues(oReportSearchCriteria);

//            //oReportSearchCriteria.ReportID  = 1;


//            oReportSearchCriteria.CompanyID = 1;
//            //oReportSearchCriteria.FromAccountNumber = txtAcNumber.Text.Trim();
//            //oReportSearchCriteria.ToAccountNumber = txtToAcNumber.Text.Trim();
//            //oReportSearchCriteria.IsKeyccount = true;
//            oReportSearchCriteria.IsZeroBalanceAccount = true;
//            //oReportSearchCriteria.RiskRatingID = Convert.ToInt32(ddlRiskRating.SelectedValue);
//            oReportSearchCriteria.ReconciliationPeriodID = 3;
//            //oReportSearchCriteria.IsDualReviewEnabled = this._IsDualReviewEnabled;
//            //oReportSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;
//            oReportSearchCriteria.IsRiskRatingEnabled = true;
//            oReportSearchCriteria.IsZeroBalanceAccountEnabled = true;
//            oReportSearchCriteria.LCID = 1034;
//            oReportSearchCriteria.BusinessEntityID = 1;
//            oReportSearchCriteria.DefaultLanguageID = 1033;
//            oReportSearchCriteria.RequesterUserID = 2;
//            oReportSearchCriteria.RequesterRoleID = 2;


//            oReportSearchCriteria.Count = 100;

//            //oReportSearchCriteria.IsMaterialAccount  = true ;
//            oReportSearchCriteria.ExcludeNetAccount = true;
//            oReportSearchCriteria.RiskRatingIDs = "1,2,3";
//            oReportSearchCriteria.ReasonCodeIDs = "1,2,3";
//            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;


//        }
//        catch (Exception ex)
//        {
//            Helper.ShowErrorMessage((PageBase)this.Page, ex);
//        }

//        return oReportSearchCriteria;
//    }
