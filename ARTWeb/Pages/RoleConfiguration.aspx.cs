using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using System.Web.UI.HtmlControls;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;

public partial class Pages_RoleConfiguration : PageBaseRecPeriod
{
    private List<CompanyAttributeConfigInfo> _CompanyRoleConfigInfoList = null;
    public List<CompanyAttributeConfigInfo> CompanyRoleConfigInfoList
    {
        get
        {
            if (_CompanyRoleConfigInfoList == null)
                _CompanyRoleConfigInfoList = (List<CompanyAttributeConfigInfo>)Session[SessionConstants.ROLE_CONFIG_COMPANY_ROLE_CONFIG_LIST];
            return _CompanyRoleConfigInfoList;
        }
        set
        {
            _CompanyRoleConfigInfoList = value;
            Session[SessionConstants.ROLE_CONFIG_COMPANY_ROLE_CONFIG_LIST] = value;
        }
    }

    private List<CompanyAttributeConfigInfo> _CompanyRoleConfigInfoFilterList = null;
    public List<CompanyAttributeConfigInfo> CompanyRoleConfigInfoFilterList
    {
        get
        {
            if (_CompanyRoleConfigInfoFilterList == null)
                _CompanyRoleConfigInfoFilterList = (List<CompanyAttributeConfigInfo>)Session[SessionConstants.ROLE_CONFIG_COMPANY_ROLE_CONFIG_FILTER_LIST];
            return _CompanyRoleConfigInfoFilterList;
        }
        set
        {
            _CompanyRoleConfigInfoFilterList = value;
            Session[SessionConstants.ROLE_CONFIG_COMPANY_ROLE_CONFIG_FILTER_LIST] = value;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        btnSave.Visible = true;

        //Sel.Value = string.Empty;
        PopulateData();
        if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open ||
            CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress ||
            CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted)
        {
            this.pnlRoleConfiguration.Enabled = true;
            btnSave.Visible = true;
        }
        else
        {
            this.pnlRoleConfiguration.Enabled = false;
            btnSave.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2514);
        ucInputRequirements.ShowInputRequirements(2520);
        Helper.SetBreadcrumbs(this, 1207, 2514);
    }

    public void PopulateData()
    {
        //SetControlState();
        CompanyRoleConfigInfoFilterList = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.AuditRoleFilter);
        CompanyRoleConfigInfoList = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);
        rgRoleConfiguration.DataSource = CompanyRoleConfigInfoList;
        rgRoleConfiguration.DataBind();
    }

    protected void rgRoleConfiguration_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                CompanyAttributeConfigInfo oRoleConfigMstInfo = (CompanyAttributeConfigInfo)e.Item.DataItem;

                CheckBox chkRoleConfig = (CheckBox)((e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0]);
                ExLabel lblRoleConfiguration = (ExLabel)e.Item.FindControl("lblRoleConfiguration");
                lblRoleConfiguration.LabelID = (int)oRoleConfigMstInfo.DescriptionLabelID;

                PopulateFilterColumns(oRoleConfigMstInfo, e.Item);

                chkRoleConfig.Checked = oRoleConfigMstInfo.IsEnabled.GetValueOrDefault();
                e.Item.Selected = oRoleConfigMstInfo.IsEnabled.GetValueOrDefault();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    private void PopulateFilterColumns(CompanyAttributeConfigInfo oRoleConfigMstInfo, GridItem gridItem)
    {
        List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList = CompanyRoleConfigInfoFilterList.FindAll(T => T.ParentAttributeID == oRoleConfigMstInfo.AttributeID);
        if (oCompanyAttributeConfigInfoList != null && oCompanyAttributeConfigInfoList.Count > 0)
        {
            foreach (CompanyAttributeConfigInfo item in oCompanyAttributeConfigInfoList)
            {
                switch ((ARTEnums.AttributeList)item.AttributeID)
                {
                    case ARTEnums.AttributeList.ViewPeriodsInRangeFrom:
                        Panel oPanelFrom = (Panel)gridItem.FindControl("pnlFromRecPeriod");
                        ExLabel lblFromPeriod = (ExLabel)gridItem.FindControl("lblFromPeriod");
                        DropDownList ddlFromPeriod = (DropDownList)gridItem.FindControl("ddlFromPeriod");
                        ExRequiredFieldValidator rfvFromPeriod = (ExRequiredFieldValidator)gridItem.FindControl("rfvFromPeriod");
                        ExCustomValidator cvToPeriod = (ExCustomValidator)gridItem.FindControl("cvToPeriod");

                        ListControlHelper.BindReconciliationPeriod(ddlFromPeriod, SessionHelper.CurrentFinancialYearID);
                        ListControlHelper.AddListItemForSelectOne(ddlFromPeriod);

                        rfvFromPeriod.InitialValue = WebConstants.SELECT_ONE; 
                        rfvFromPeriod.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, item.DescriptionLabelID.Value);
                        cvToPeriod.Attributes.Add("ddlFromPeriodID", ddlFromPeriod.ClientID);

                        lblFromPeriod.LabelID = item.DescriptionLabelID.Value;
                        if (item.ReferenceID.HasValue)
                            ddlFromPeriod.SelectedValue = item.ReferenceID.ToString();
                        oPanelFrom.Visible = true;
                        break;
                    case ARTEnums.AttributeList.ViewPeriodsInRangeTo:
                        Panel oPanelTo = (Panel)gridItem.FindControl("pnlToRecPeriod");
                        ExLabel lblToPeriod = (ExLabel)gridItem.FindControl("lblToPeriod");
                        DropDownList ddlToPeriod = (DropDownList)gridItem.FindControl("ddlToPeriod");
                        ExRequiredFieldValidator rfvToPeriod = (ExRequiredFieldValidator)gridItem.FindControl("rfvToPeriod");
                        ExCustomValidator cvToPeriod1 = (ExCustomValidator)gridItem.FindControl("cvToPeriod");

                        ListControlHelper.BindReconciliationPeriod(ddlToPeriod, SessionHelper.CurrentFinancialYearID);
                        ListControlHelper.AddListItemForSelectOne(ddlToPeriod);
                        cvToPeriod1.Attributes.Add("ddlToPeriodID", ddlToPeriod.ClientID);

                        rfvToPeriod.InitialValue = WebConstants.SELECT_ONE;
                        rfvToPeriod.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, item.DescriptionLabelID.Value);

                        lblToPeriod.LabelID = item.DescriptionLabelID.Value;
                        if (item.ReferenceID.HasValue)
                            ddlToPeriod.SelectedValue = item.ReferenceID.ToString();
                        oPanelTo.Visible = true;
                        break;
                }
            }
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        SaveRoleConfiguration();
    }

    private void SaveRoleConfiguration()
    {
        try
        {
            foreach (GridDataItem oItem in rgRoleConfiguration.Items)
            {
                CheckBox chkRoleConfig = (CheckBox)((oItem as GridDataItem)["CheckboxSelectColumn"].Controls[0]);
                int AttributeID = (int)oItem.GetDataKeyValue("AttributeID");
                CompanyAttributeConfigInfo oCompanyRoleConfigInfo = CompanyRoleConfigInfoList.Find(T => T.AttributeID == AttributeID);

                if (chkRoleConfig.Checked)
                {
                    oCompanyRoleConfigInfo.IsEnabled = true;
                }
                else
                {
                    oCompanyRoleConfigInfo.IsEnabled = false;
                }
                GetDataFilterColumns(oCompanyRoleConfigInfo, oItem);
            }
            List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList = new List<CompanyAttributeConfigInfo>();
            oCompanyAttributeConfigInfoList.AddRange(CompanyRoleConfigInfoList);
            oCompanyAttributeConfigInfoList.AddRange(CompanyRoleConfigInfoFilterList);
            AttributeConfigHelper.SaveCompanyAttributeConfigInfoList(oCompanyAttributeConfigInfoList, SessionHelper.CurrentUserLoginID);
            Helper.RedirectToHomePage(2521);
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

    private void GetDataFilterColumns(CompanyAttributeConfigInfo oRoleConfigMstInfo, GridItem gridItem)
    {
        List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList = CompanyRoleConfigInfoFilterList.FindAll(T => T.ParentAttributeID == oRoleConfigMstInfo.AttributeID);
        if (oCompanyAttributeConfigInfoList != null && oCompanyAttributeConfigInfoList.Count > 0)
        {
            foreach (CompanyAttributeConfigInfo item in oCompanyAttributeConfigInfoList)
            {
                item.IsEnabled = oRoleConfigMstInfo.IsEnabled;
                switch ((ARTEnums.AttributeList)item.AttributeID)
                {
                    case ARTEnums.AttributeList.ViewPeriodsInRangeFrom:
                        DropDownList ddlFromPeriod = (DropDownList)gridItem.FindControl("ddlFromPeriod");
                        if (oRoleConfigMstInfo.IsEnabled.GetValueOrDefault())
                        {
                            item.ReferenceID = Convert.ToInt32(ddlFromPeriod.SelectedValue);
                        }
                        else
                        {
                            item.ReferenceID = null;
                        }
                        break;
                    case ARTEnums.AttributeList.ViewPeriodsInRangeTo:
                        DropDownList ddlToPeriod = (DropDownList)gridItem.FindControl("ddlToPeriod");
                        if (oRoleConfigMstInfo.IsEnabled.GetValueOrDefault())
                        {
                            item.ReferenceID = Convert.ToInt32(ddlToPeriod.SelectedValue);
                        }
                        else
                        {
                            item.ReferenceID = null;
                        }
                        break;
                }
            }
        }
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        //Response.Redirect("Home.aspx");
        SessionHelper.RedirectToUrl("Home.aspx");
        return;
    }

    public override string GetMenuKey()
    {
        return "";
    }
}
