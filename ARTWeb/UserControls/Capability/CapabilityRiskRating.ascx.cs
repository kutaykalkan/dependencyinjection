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
using SkyStem.ART.Web.Utility;

using System.Text;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;

public partial class UserControls_CapabilityRiskRating : UserControlBase
{
    #region Variables & Constants
    IUtility oUtilityClient;
    ICompany oCompanyClient;
    int RISKRATINGFREQUENCYID_CUSTOM_LOCAL = 4;
    int _riskRatingID;
    int _companyID;
    bool? _isRiskRatingFrequencyForwarded = null;
    int _riskRatingFrequencyID = 0;
    bool? _isRiskRatingYesChecked = false;
    //bool _isRiskRatingCheckedToYes = false;
    bool isCustomSelected = false;
    RiskRatingMstInfo oRiskRatingMstInfo;
    string viewStateKey = "RiskRatingRecPeriod";
    #endregion
    #region Properties
    public bool? IsRiskRatingFrequencyForwarded
    {
        get { return _isRiskRatingFrequencyForwarded; }
        set { _isRiskRatingFrequencyForwarded = value; }
    }
    public int RiskRatingFrequencyID
    {
        get { return _riskRatingFrequencyID; }
        set { _riskRatingFrequencyID = value; }
    }
    public bool? IsRiskRatingYesChecked
    {
        get { return _isRiskRatingYesChecked; }
        set
        {
            _isRiskRatingYesChecked = value;

        }
    }
    public bool IsCustomSelected
    {
        get { return isCustomSelected; }
        set { isCustomSelected = value; }
    }
    public RiskRatingMstInfo RiskRatingMstInfo
    {
        get { return oRiskRatingMstInfo; }
        set
        {
            oRiskRatingMstInfo = value;
            _riskRatingID = Convert.ToInt32(oRiskRatingMstInfo.RiskRatingID);
        }
    }
    #endregion
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cvRiskRating.Attributes.Add("ControlToValidateCBL", cblRiskRatingPeriodsCustom.ClientID);
            if (RiskRatingMstInfo.RiskRatingID.HasValue)
            {
                SetErrorMessages(RiskRatingMstInfo.RiskRatingID.Value);
            }
            _companyID = SessionHelper.CurrentCompanyID.Value;
            oUtilityClient = RemotingHelper.GetUtilityObject();
            oCompanyClient = RemotingHelper.GetCompanyObject();

            if (_isRiskRatingYesChecked == true)
            {
                if (!IsPostBack)
                {
                    Helper.SetCarryforwardedStatus(imgStatusRiskRatingFrequencyForwardYes, imgStatusRiskRatingFrequencyForwardNo, _isRiskRatingFrequencyForwarded);

                    LoadAndBind();

                }
                else
                {
                    lblRiskRatingHeading.LabelID = Convert.ToInt32(RiskRatingMstInfo.RiskRatingLabelID);
                }
                if (ddlRiskRatingFrequency.SelectedValue == RISKRATINGFREQUENCYID_CUSTOM_LOCAL.ToString())//for custom
                {
                    isCustomSelected = true;
                }
                else
                {
                    isCustomSelected = false;
                }

            }
            //TODO: do we need to handle for 'false' and 'null'
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    #endregion
    #region Other Events
    protected void ddlRiskRatingFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRiskRatingFrequency.SelectedValue == RISKRATINGFREQUENCYID_CUSTOM_LOCAL.ToString())//for custom
            {
                pnlRiskRatingCustom.Visible = true;
                isCustomSelected = true;
                BindAndSelectFinancialYearDropdown();
            }
            else
            {
                pnlRiskRatingCustom.Visible = false;
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    #endregion
    #region Private Methods
    private void BindAndSelectFinancialYearDropdown()
    {
        ListControlHelper.BindFinancialYearDropdown(ddlFinancialYear, true);
        if (SessionHelper.CurrentFinancialYearID != null)
        {
            ddlFinancialYear.SelectedValue = SessionHelper.CurrentFinancialYearID.Value.ToString();
        }

        LoadRecPeriodsFromDBForRiskRating();

        HandleFinancialYearChange();
    }
    private void HandleFinancialYearChange()
    {
        FillReconciliationPeriodCheckBoxList();
        SetSelectedRecPeriods();
    }
    private void SetSelectedRecPeriods()
    {
        RiskRatingReconciliationPeriodInfo oRiskRatingReconciliationPeriodInfo = null;
        List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = GetRecPeriodsFromViewState();
        for (int i = 0; i < cblRiskRatingPeriodsCustom.Items.Count; i++)
        {
            oRiskRatingReconciliationPeriodInfo = oRiskRatingReconciliationPeriodInfoCollection.Find(c => c.ReconciliationPeriodID.ToString() == cblRiskRatingPeriodsCustom.Items[i].Value);
            // TODO: Apoorv - shld try to use LINQ
            if (oRiskRatingReconciliationPeriodInfo != null)
            {
                cblRiskRatingPeriodsCustom.Items[i].Selected = true;
            }
        }
    }
    private void SetErrorMessages(int riskRatingID)
    {
        switch (riskRatingID)
        {
            case 1:
                this.cvRiskRating.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 5000066);
                break;

            case 2:
                this.cvRiskRating.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 5000067);
                break;

            case 3:
                this.cvRiskRating.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 5000068);
                break;
        }
    }
    private void SaveRecPeriodsInViewState(List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection)
    {
        // store the updated collection into ViewState
        ViewState[viewStateKey] = oRiskRatingReconciliationPeriodInfoCollection;
    }
    private void UpdateRecPeriodsInViewState()
    {
        List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollectionFromViewState = (List<RiskRatingReconciliationPeriodInfo>)ViewState[viewStateKey];

        if (oRiskRatingReconciliationPeriodInfoCollectionFromViewState == null)
        {
            oRiskRatingReconciliationPeriodInfoCollectionFromViewState = new List<RiskRatingReconciliationPeriodInfo>();
        }

        RiskRatingReconciliationPeriodInfo oRiskRatingReconciliationPeriodInfo = null;
        for (int i = 0; i < cblRiskRatingPeriodsCustom.Items.Count; i++)
        {
            // if selected, check for already exists in ViewState Collection
            oRiskRatingReconciliationPeriodInfo = oRiskRatingReconciliationPeriodInfoCollectionFromViewState.Find(c => c.ReconciliationPeriodID == Convert.ToInt32(cblRiskRatingPeriodsCustom.Items[i].Value));

            if (cblRiskRatingPeriodsCustom.Items[i].Selected == true)
            {
                if (oRiskRatingReconciliationPeriodInfo == null)
                {
                    // add
                    oRiskRatingReconciliationPeriodInfo = new RiskRatingReconciliationPeriodInfo();
                    oRiskRatingReconciliationPeriodInfo.ReconciliationPeriodID = Convert.ToInt32(cblRiskRatingPeriodsCustom.Items[i].Value);
                    oRiskRatingReconciliationPeriodInfo.RiskRatingID = Convert.ToInt16(_riskRatingID);
                    oRiskRatingReconciliationPeriodInfo.CompanyID = SessionHelper.CurrentCompanyID;
                    oRiskRatingReconciliationPeriodInfoCollectionFromViewState.Add(oRiskRatingReconciliationPeriodInfo);
                }
            }
            else
            {
                if (oRiskRatingReconciliationPeriodInfo != null)
                {
                    // Remove
                    oRiskRatingReconciliationPeriodInfoCollectionFromViewState.Remove(oRiskRatingReconciliationPeriodInfo);
                }
            }
        }
        SaveRecPeriodsInViewState(oRiskRatingReconciliationPeriodInfoCollectionFromViewState);
    }
    #endregion
    #region Other Methods
    protected void LoadRecPeriodsFromDBForRiskRating()
    {
        // Get from DB and Store Into ViewState
        IRiskRating oRiskRatingClient = RemotingHelper.GetRiskRatingObject();
        List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = oRiskRatingClient.SelectAllRiskRatingReconciliationPeriodByRiskRatingIDAndReconciliationPeriodID(SessionHelper.CurrentReconciliationPeriodID, Convert.ToInt16(_riskRatingID), Helper.GetAppUserInfo());
        SaveRecPeriodsInViewState(oRiskRatingReconciliationPeriodInfoCollection);
        ViewState[ViewStateConstants.RISK_RATING_RECPERIODS_CURRENT_DB_VAL] = oRiskRatingReconciliationPeriodInfoCollection;
    } 
    protected void FillReconciliationPeriodCheckBoxList()
    {
        int? financialYearID = null;
        if (ddlFinancialYear.SelectedItem != null
            && ddlFinancialYear.SelectedItem.Value != WebConstants.SELECT_ONE)
        {
            financialYearID = Convert.ToInt32(ddlFinancialYear.SelectedItem.Value);
        }

        ListControlHelper.BindReconciliationPeriodForRiskRating(cblRiskRatingPeriodsCustom, financialYearID);
    }
    public void LoadAndBind()
    {
        ListControlHelper.BindReconciliationFrequency(ddlRiskRatingFrequency);


        if (_riskRatingFrequencyID == 0)
        {
            switch (_riskRatingID)
            {
                case 1://High
                    _riskRatingFrequencyID = 1;//every recperiod
                    break;
                case 2://Medium
                case 3://Low
                    _riskRatingFrequencyID = 4;//custom
                    break;
            }
        }
        ViewState[ViewStateConstants.RISK_RATING_CURRENT_DB_VAL] = _riskRatingFrequencyID;
        ddlRiskRatingFrequency.SelectedIndex = ddlRiskRatingFrequency.Items.IndexOf(ddlRiskRatingFrequency.Items.FindByValue(_riskRatingFrequencyID.ToString()));

        lblRiskRatingHeading.LabelID = Convert.ToInt32(RiskRatingMstInfo.RiskRatingLabelID);
        if (ddlRiskRatingFrequency.SelectedValue == RISKRATINGFREQUENCYID_CUSTOM_LOCAL.ToString())//TODO: may be use enum -for custom
        {
            pnlRiskRatingCustom.Visible = true;
            BindAndSelectFinancialYearDropdown();
        }
        else
        {
            pnlRiskRatingCustom.Visible = false;
        }
    }

    protected void cblRiskRatingPeriodsCustom_DataBinding(object sender, EventArgs e)
    {
        for (int i = 0; i < cblRiskRatingPeriodsCustom.Items.Count; i++)
        {
            cblRiskRatingPeriodsCustom.Items[i].Text = Helper.GetDisplayDate(Convert.ToDateTime(cblRiskRatingPeriodsCustom.Items[i].Text));
        }
    }

    //Handle masterpage DDLs change
    public void ChangedEventHandler()
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
        if (_isRiskRatingYesChecked == true)
        {
            LoadAndBind();
            Helper.SetCarryforwardedStatus(imgStatusRiskRatingFrequencyForwardYes, imgStatusRiskRatingFrequencyForwardNo, _isRiskRatingFrequencyForwarded);

            if (ddlRiskRatingFrequency.SelectedValue == RISKRATINGFREQUENCYID_CUSTOM_LOCAL.ToString())//for custom
            {
                isCustomSelected = true;
            }
            else
            {
                isCustomSelected = false;
            }
        }
    }

    public void SetCarryforwardedStatus()
    {
        Helper.SetCarryforwardedStatus(imgStatusRiskRatingFrequencyForwardYes, imgStatusRiskRatingFrequencyForwardNo, _isRiskRatingFrequencyForwarded);
    }
    public void RegisterClientFunctionRiskRating(string functionName, string scriptName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script > function ");
        sb.Append(functionName);
        sb.Append("(source, args){ ");
        sb.Append(" var cbl = document.getElementById(" + cblRiskRatingPeriodsCustom.ClientID + "); ");
        sb.Append(" if(cbl != null){ ");
        sb.Append(" if (!IsCBLAtLeastOneSelectedRiskRating(cbl)){ ");
        sb.Append(" args.IsValid = false;} else args.IsValid = true;}} </script> ");

        Page.ClientScript.RegisterStartupScript(this.GetType(), scriptName, sb.ToString());
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Update the View State with Rec Periods 
            UpdateRecPeriodsInViewState();

            // Rebind Rec Periods
            HandleFinancialYearChange();
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    public List<RiskRatingReconciliationPeriodInfo> GetRecPeriodsFromViewState()
    {
        List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = (List<RiskRatingReconciliationPeriodInfo>)ViewState[viewStateKey];
        return oRiskRatingReconciliationPeriodInfoCollection;
    }   
    public List<RiskRatingReconciliationPeriodInfo> GetSelectedRecPeriods()
    {
        // Update the View State and return the Collection as in View State
        UpdateRecPeriodsInViewState();

        List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = (List<RiskRatingReconciliationPeriodInfo>)ViewState[viewStateKey];
        return oRiskRatingReconciliationPeriodInfoCollection;
    }
    public bool IsValueChanged()
    {
        bool? IsValueChangeFlag = false;
        string DBRiskRatingVal = string.Empty;
        if (ViewState[ViewStateConstants.RISK_RATING_CURRENT_DB_VAL] != null)
            DBRiskRatingVal = ViewState[ViewStateConstants.RISK_RATING_CURRENT_DB_VAL].ToString();

        if (string.IsNullOrEmpty(DBRiskRatingVal) && ddlRiskRatingFrequency.Visible && ddlRiskRatingFrequency.SelectedValue != WebConstants.SELECT_ONE)
            IsValueChangeFlag = true;
        else if (ddlRiskRatingFrequency.SelectedValue != DBRiskRatingVal)
            IsValueChangeFlag = true;
        else
        {
            List<RiskRatingReconciliationPeriodInfo> oDBRiskRatingReconciliationPeriodInfoList = (List<RiskRatingReconciliationPeriodInfo>)ViewState[ViewStateConstants.RISK_RATING_RECPERIODS_CURRENT_DB_VAL];
            List<RiskRatingReconciliationPeriodInfo> oCurrentRiskRatingReconciliationPeriodInfoList = GetSelectedRecPeriods();
            if (oDBRiskRatingReconciliationPeriodInfoList != null && oCurrentRiskRatingReconciliationPeriodInfoList != null)
            {
                if (oDBRiskRatingReconciliationPeriodInfoList.Count != oCurrentRiskRatingReconciliationPeriodInfoList.Count)
                    IsValueChangeFlag = true;
                else
                {
                    foreach (var CurrentRiskRatingReconciliationPeriodInfo in oCurrentRiskRatingReconciliationPeriodInfoList)
                    {
                        var oRiskRatingReconciliationPeriodInfo = oDBRiskRatingReconciliationPeriodInfoList.Find(c => c.ReconciliationPeriodID == CurrentRiskRatingReconciliationPeriodInfo.ReconciliationPeriodID);
                        if (oRiskRatingReconciliationPeriodInfo == null)
                        {
                            IsValueChangeFlag = true;
                            break;
                        }
                    }
                }
            }
        }
        return IsValueChangeFlag.Value;
    }
    #endregion


}//end of class
