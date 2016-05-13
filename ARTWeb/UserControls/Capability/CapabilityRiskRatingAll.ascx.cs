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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
public partial class UserControls_CapabilityRiskRatingAll : UserControlBase
{
    #region Variables & Constants
    IUtility oUtilityClient;
    RiskRatingMstInfo _oRiskRatingMstInfo1;
    RiskRatingMstInfo _oRiskRatingMstInfo2;
    RiskRatingMstInfo _oRiskRatingMstInfo3;
    int _companyID;
    bool? _isRiskRatingYesChecked = null;
    bool? _isRiskRatingForwarded = false;
    #endregion
    #region Properties
    public bool? IsRiskRatingForwarded
    {
        get { return _isRiskRatingForwarded; }
        set { _isRiskRatingForwarded = value; }
    }
    public bool? IsRiskRatingYesChecked
    {
        get { return _isRiskRatingYesChecked; }
        set { _isRiskRatingYesChecked = value; }
    }
    #endregion
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _companyID = SessionHelper.CurrentCompanyID.Value;
            oUtilityClient = RemotingHelper.GetUtilityObject();
            if (!IsPostBack)
            {

                Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, _isRiskRatingForwarded);
                Helper.SetCarryforwardedStatus(imgStatusRiskRatingForwardYes, imgStatusRiskRatingForwardNo, _isRiskRatingForwarded);
                Helper.SetYesNoRadioButtons(optRiskRatingYes, optRiskRatingNo, _isRiskRatingYesChecked);
            }
            ShowHideForRadioButtonYesNoChecked();
            BindPropertiesToChildControls();
            SetIsRiskRatingCheckedPropertyInChildControl();
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
    protected void optRiskRatingYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            LoadAndBindChildControls();
            ucCapabilityRiskRating1.SetCarryforwardedStatus();
            ucCapabilityRiskRating2.SetCarryforwardedStatus();
            ucCapabilityRiskRating3.SetCarryforwardedStatus();
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
    protected void optRiskRatingNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
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
    #region Other Methods
    protected void ShowHideForRadioButtonYesNoChecked()
    {
        if (optRiskRatingYes.Checked == true && optRiskRatingNo.Checked == false)
        {
            pnlRiskRatingFrequencyExtended.Visible = true;
            pnlContent.Visible = true;
            _isRiskRatingYesChecked = true;
            //imgCollapse.Visible = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";
            //pnlMain.CssClass = "PanelCapability";
            //pnlContent.CssClass = "PanelContent";
            //pnlYesNo.CssClass = "PanelCapabilityYesNo";
        }
        else if (optRiskRatingYes.Checked == false && optRiskRatingNo.Checked == true)
        {
            pnlRiskRatingFrequencyExtended.Visible = false;
            pnlContent.Visible = false;
            _isRiskRatingYesChecked = false;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            //pnlContent.CssClass = "";
            //pnlYesNo.CssClass = "";
        }
        else if (optRiskRatingYes.Checked == false && optRiskRatingNo.Checked == false)
        {
            pnlRiskRatingFrequencyExtended.Visible = false;
            pnlContent.Visible = false;
            _isRiskRatingYesChecked = null;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            //pnlContent.CssClass = "";
            //pnlYesNo.CssClass = "";
        }
    }
    protected void SetIsRiskRatingCheckedPropertyInChildControl()
    {
        ucCapabilityRiskRating1.IsRiskRatingYesChecked = _isRiskRatingYesChecked;
        ucCapabilityRiskRating2.IsRiskRatingYesChecked = _isRiskRatingYesChecked;
        ucCapabilityRiskRating3.IsRiskRatingYesChecked = _isRiskRatingYesChecked;
    }
    protected void BindPropertiesToChildControls()
    {
        IList<RiskRatingMstInfo> oRiskRatingMstInfoCollection = oUtilityClient.SelectAllRiskRating(Helper.GetAppUserInfo());
        IRiskRating oRiskRatingClient = RemotingHelper.GetRiskRatingObject();
        IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection = oRiskRatingClient.SelectAllRiskRatingReconciliationFrequencyByReconciliationPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        ucCapabilityRiskRating1.RiskRatingMstInfo = oRiskRatingMstInfoCollection[0];
        ucCapabilityRiskRating2.RiskRatingMstInfo = oRiskRatingMstInfoCollection[1];
        ucCapabilityRiskRating3.RiskRatingMstInfo = oRiskRatingMstInfoCollection[2];
        _oRiskRatingMstInfo1 = oRiskRatingMstInfoCollection[0];
        _oRiskRatingMstInfo2 = oRiskRatingMstInfoCollection[1];
        _oRiskRatingMstInfo3 = oRiskRatingMstInfoCollection[2];

        RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfoHigh = null;
        RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfoMedium = null;
        RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfoLow = null;

        if (oRiskRatingReconciliationFrequencyInfoCollection != null && oRiskRatingReconciliationFrequencyInfoCollection.Count > 0)
        {
            foreach (RiskRatingReconciliationFrequencyInfo item in oRiskRatingReconciliationFrequencyInfoCollection)
            {
                if (item.RiskRatingID == 1)
                {
                    oRiskRatingReconciliationFrequencyInfoHigh = item;
                }

                if (item.RiskRatingID == 2)
                {
                    oRiskRatingReconciliationFrequencyInfoMedium = item;
                }

                if (item.RiskRatingID == 3)
                {
                    oRiskRatingReconciliationFrequencyInfoLow = item;
                }
            }
            ucCapabilityRiskRating1.RiskRatingFrequencyID = oRiskRatingReconciliationFrequencyInfoHigh.ReconciliationFrequencyID.Value;
            ucCapabilityRiskRating2.RiskRatingFrequencyID = oRiskRatingReconciliationFrequencyInfoMedium.ReconciliationFrequencyID.Value;
            ucCapabilityRiskRating3.RiskRatingFrequencyID = oRiskRatingReconciliationFrequencyInfoLow.ReconciliationFrequencyID.Value;
            ucCapabilityRiskRating1.IsRiskRatingFrequencyForwarded = oRiskRatingReconciliationFrequencyInfoHigh.IsCarryForwardedFromPreviousRecPeriod.Value;
            ucCapabilityRiskRating2.IsRiskRatingFrequencyForwarded = oRiskRatingReconciliationFrequencyInfoMedium.IsCarryForwardedFromPreviousRecPeriod.Value;
            ucCapabilityRiskRating3.IsRiskRatingFrequencyForwarded = oRiskRatingReconciliationFrequencyInfoLow.IsCarryForwardedFromPreviousRecPeriod.Value;
        }
    }
    protected void LoadAndBindChildControls()
    {
        ucCapabilityRiskRating1.LoadAndBind();
        ucCapabilityRiskRating2.LoadAndBind();
        ucCapabilityRiskRating3.LoadAndBind();
    }
    //Handle masterpage DDLs change
    public void ChangedEventHandler()
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
        //if (!IsPostBack)
        //{

        Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, _isRiskRatingForwarded);
        Helper.SetCarryforwardedStatus(imgStatusRiskRatingForwardYes, imgStatusRiskRatingForwardNo, _isRiskRatingForwarded);
        Helper.SetYesNoRadioButtons(optRiskRatingYes, optRiskRatingNo, _isRiskRatingYesChecked);

        //}
        ShowHideForRadioButtonYesNoChecked();
        BindPropertiesToChildControls();
        SetIsRiskRatingCheckedPropertyInChildControl();

        ucCapabilityRiskRating1.ChangedEventHandler();
        ucCapabilityRiskRating2.ChangedEventHandler();
        ucCapabilityRiskRating3.ChangedEventHandler();
    }
    public IList<RiskRatingReconciliationFrequencyInfo> GetRiskRatingReconciliationFrequencyObjectToBeSavedFromUC()
    {
        IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection = null;
        if (optRiskRatingYes.Checked == true)
        {
            DropDownList ddlRiskRatingFrequency1 = (DropDownList)ucCapabilityRiskRating1.FindControl("ddlRiskRatingFrequency");
            DropDownList ddlRiskRatingFrequency2 = (DropDownList)ucCapabilityRiskRating2.FindControl("ddlRiskRatingFrequency");
            DropDownList ddlRiskRatingFrequency3 = (DropDownList)ucCapabilityRiskRating3.FindControl("ddlRiskRatingFrequency");

            //TODO: if we needed List<int> instead of IList<RiskRatingReconciliationPeriodInfo> then do it accordingly in tablevalue sp, but presently 
            oRiskRatingReconciliationFrequencyInfoCollection = new List<RiskRatingReconciliationFrequencyInfo>();
            RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfo1 = new RiskRatingReconciliationFrequencyInfo();
            oRiskRatingReconciliationFrequencyInfo1.CompanyID = _companyID;
            oRiskRatingReconciliationFrequencyInfo1.RiskRatingID = _oRiskRatingMstInfo1.RiskRatingID;
            oRiskRatingReconciliationFrequencyInfo1.ReconciliationFrequencyID = Convert.ToInt16(ddlRiskRatingFrequency1.SelectedValue);
            oRiskRatingReconciliationFrequencyInfoCollection.Add(oRiskRatingReconciliationFrequencyInfo1);

            RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfo2 = new RiskRatingReconciliationFrequencyInfo();
            oRiskRatingReconciliationFrequencyInfo2.CompanyID = _companyID;
            oRiskRatingReconciliationFrequencyInfo2.RiskRatingID = _oRiskRatingMstInfo2.RiskRatingID;
            oRiskRatingReconciliationFrequencyInfo2.ReconciliationFrequencyID = Convert.ToInt16(ddlRiskRatingFrequency2.SelectedValue);
            oRiskRatingReconciliationFrequencyInfoCollection.Add(oRiskRatingReconciliationFrequencyInfo2);

            RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfo3 = new RiskRatingReconciliationFrequencyInfo();
            oRiskRatingReconciliationFrequencyInfo3.CompanyID = _companyID;
            oRiskRatingReconciliationFrequencyInfo3.RiskRatingID = _oRiskRatingMstInfo3.RiskRatingID;
            oRiskRatingReconciliationFrequencyInfo3.ReconciliationFrequencyID = Convert.ToInt16(ddlRiskRatingFrequency3.SelectedValue);
            oRiskRatingReconciliationFrequencyInfoCollection.Add(oRiskRatingReconciliationFrequencyInfo3);
            IRiskRating oRiskRatingClient = RemotingHelper.GetRiskRatingObject();
            //int returnValue = oRiskRatingClient.SaveRiskRatingReconciliationFrequencyTableValue(oRiskRatingReconciliationFrequencyInfoCollection, companyID);
        }
        return oRiskRatingReconciliationFrequencyInfoCollection;
    }
    public IList<RiskRatingReconciliationPeriodInfo> GetRiskRatingReconciliationPeriodObjectToBeSavedFromUC()
    {
        List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = new List<RiskRatingReconciliationPeriodInfo>();

        if (ucCapabilityRiskRating1.IsCustomSelected == true)
        {
            oRiskRatingReconciliationPeriodInfoCollection.AddRange(ucCapabilityRiskRating1.GetSelectedRecPeriods());
        }

        if (ucCapabilityRiskRating2.IsCustomSelected == true)
        {
            oRiskRatingReconciliationPeriodInfoCollection.AddRange(ucCapabilityRiskRating2.GetSelectedRecPeriods());
        }

        if (ucCapabilityRiskRating3.IsCustomSelected == true)
        {
            oRiskRatingReconciliationPeriodInfoCollection.AddRange(ucCapabilityRiskRating3.GetSelectedRecPeriods());
        }

        return oRiskRatingReconciliationPeriodInfoCollection;
    }
    public bool IsValueChanged()
    {
        bool IsValueChangeFlag = false;
        if (ucCapabilityRiskRating1.IsValueChanged() || ucCapabilityRiskRating2.IsValueChanged() || ucCapabilityRiskRating3.IsValueChanged())
            IsValueChangeFlag = true;
        return IsValueChangeFlag;
    }
    #endregion
}//end of class
