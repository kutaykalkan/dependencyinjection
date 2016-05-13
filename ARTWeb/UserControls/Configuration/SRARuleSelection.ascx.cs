using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Data;

public partial class UserControls_SRARuleSelection : UserControlBase
{

    List<CompanySystemReconciliationRuleInfo> _CompanySRARuleInfoCollection = new List<CompanySystemReconciliationRuleInfo>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ucInputRequirements.ShowInputRequirements(2030, 2029);
    }

    public void PopulateData()
    {
        //Sel.Value = string.Empty;
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        this._CompanySRARuleInfoCollection = oUtilityClient.SelectCompanySRARuleInfoByRecPeriodID(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

        List<SystemReconciliationRuleMstInfo> oSystemReconciliationRuleMstInfoCollection = oUtilityClient.SelectAllSRARules(Helper.GetAppUserInfo());
        Array.ForEach(oSystemReconciliationRuleMstInfoCollection.ToArray(), sra => sra.SystemReconciliationRule = Helper.GetLabelIDValue(sra.SystemReconciliationRuleLabelID.Value));

        rgSRARuleSelection.DataSource = oSystemReconciliationRuleMstInfoCollection;
        rgSRARuleSelection.DataBind();

        if (!CertificationHelper.IsCertificationStarted() &&
            (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress))
        {
            btnSave.Visible = true;
            pnlSRARuleSelection.Enabled = true;
        }
        else
        {
            btnSave.Visible = false;
            pnlSRARuleSelection.Enabled = false;
        }
    }

    protected void rgSRARuleSelection_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                SystemReconciliationRuleMstInfo oSystemReconciliationRuleMstInfo = (SystemReconciliationRuleMstInfo)e.Item.DataItem;

                CheckBox chkSRARule = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                ExLabel lblSRARule = (ExLabel)e.Item.FindControl("lblSRARule");
                ExLabel lblSRARuleNo = (ExLabel)e.Item.FindControl("lblSRARuleNo");
                short? sraRuleID = (from csra in this._CompanySRARuleInfoCollection
                                    where csra.SystemReconciliationRuleID == oSystemReconciliationRuleMstInfo.SystemReconciliationRuleID
                                    && csra.IsActive == true
                                    select csra.SystemReconciliationRuleID).FirstOrDefault();


                if (oSystemReconciliationRuleMstInfo.CapabilityID != null && oSystemReconciliationRuleMstInfo.CapabilityID.Value > 0)
                {
                    ARTEnums.Capability eCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oSystemReconciliationRuleMstInfo.CapabilityID.ToString());
                    if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(eCapability))
                    {
                        chkSRARule.Enabled = false;
                        lblSRARule.Enabled = false;
                        lblSRARuleNo.Enabled = false;
                    }
                }

                if (sraRuleID != null)
                {
                    chkSRARule.Checked = true;
                    e.Item.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
            List<SystemReconciliationRuleMstInfo> oSystemReconciliationRuleMstInfoCollection = oUtilityClient.SelectAllSRARules(Helper.GetAppUserInfo());

            List<CompanySystemReconciliationRuleInfo> oCompanySRARuleInfoCollection = new List<CompanySystemReconciliationRuleInfo>();
            DateTime updateTime = DateTime.Now;
            foreach (GridDataItem item in rgSRARuleSelection.Items)
            {
                short sraRuleID = (short)item.GetDataKeyValue("SystemReconciliationRuleID");
                if (sraRuleID > 0)
                {
                    CompanySystemReconciliationRuleInfo oCompanySRARuleInfo = new CompanySystemReconciliationRuleInfo();
                    oCompanySRARuleInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                    oCompanySRARuleInfo.CompanyID = SessionHelper.CurrentCompanyID;
                    oCompanySRARuleInfo.SystemReconciliationRuleID = sraRuleID;
                    oCompanySRARuleInfo.DateAdded = updateTime;
                    oCompanySRARuleInfo.IsActive = item.Selected;

                    oCompanySRARuleInfoCollection.Add(oCompanySRARuleInfo);
                }
            }
            CompanyConfigurationParamInfo oCompanyConfigurationParamInfo = new CompanyConfigurationParamInfo();
            Helper.FillCommonServiceParams(oCompanyConfigurationParamInfo);
            oCompanyConfigurationParamInfo.CompanySystemReconciliationRuleInfoList = oCompanySRARuleInfoCollection;
            oCompanyConfigurationParamInfo.SystemLockdownInfo = Helper.GetSystemLockdownInfo(ARTEnums.SystemLockdownReason.SRAProcessingRequired);
            oUtilityClient.InsertCompanySRARule(oCompanyConfigurationParamInfo, Helper.GetAppUserInfo());

            Helper.RedirectToHomePage(1928);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        }
    }
}
