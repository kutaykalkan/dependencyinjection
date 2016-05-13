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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using Telerik.Web.UI;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Language.LanguageUtility;

public partial class Pages_JournalEntry_ConfigureJEWriteOnOffApprover : PageBaseRecPeriod
{
    List<CompanyJEWriteOffOnApproverInfo> oCompanyJEWriteOffOnApproverInfoCollection = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 2113);
            Helper.SetBreadcrumbs(this, 1207, 2113);
            Helper.ShowInputRequirementSection(this, 2117);
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

    private void LoadData()
    {
        if (ViewState["oCompanyJEWriteOffOnApproverInfoCollection"] == null)
        {
            oCompanyJEWriteOffOnApproverInfoCollection = GetCompanyJEWriteOffOnApproversByRecPeriodID();
            ViewState["oCompanyJEWriteOffOnApproverInfoCollection"] = oCompanyJEWriteOffOnApproverInfoCollection;
        }
        else
        {
            oCompanyJEWriteOffOnApproverInfoCollection = (List<CompanyJEWriteOffOnApproverInfo>)ViewState["oCompanyJEWriteOffOnApproverInfoCollection"];
        }

        rgConfigureJEWriteOffOnApprover.MasterTableView.DataSource = oCompanyJEWriteOffOnApproverInfoCollection;

    }
    private List<CompanyJEWriteOffOnApproverInfo> GetCompanyJEWriteOffOnApproversByRecPeriodID()
    {
        int DefaultNoOfRows = Convert.ToInt32(AppSettingHelper.GetAppSettingValue("DefaultNoOfRows"));
        oCompanyJEWriteOffOnApproverInfoCollection = new List<CompanyJEWriteOffOnApproverInfo>();

        IJournalEntry oJournalEntryClient = RemotingHelper.GetJournalEntryObject();
        oCompanyJEWriteOffOnApproverInfoCollection = oJournalEntryClient.GetCompanyJEWriteOffOnApproversByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        // put Database call here
        if (oCompanyJEWriteOffOnApproverInfoCollection.Count >= DefaultNoOfRows)
            DefaultNoOfRows = 0;
        else
            DefaultNoOfRows = DefaultNoOfRows - oCompanyJEWriteOffOnApproverInfoCollection.Count;
        CompanyJEWriteOffOnApproverInfo oCompanyJEWriteOffOnApproverInfo;
        for (int i = 0; i < DefaultNoOfRows; i++)
        {
            oCompanyJEWriteOffOnApproverInfo = new CompanyJEWriteOffOnApproverInfo();
            //oCompanyJEWriteOffOnApproverInfo.GLToolColumnName = "Column" + i;
            //oCompanyJEWriteOffOnApproverInfo.GLToolColumnLength = (Int16)i;
            oCompanyJEWriteOffOnApproverInfoCollection.Add(oCompanyJEWriteOffOnApproverInfo);
        }
        return oCompanyJEWriteOffOnApproverInfoCollection;


    }





    protected void rgConfigureJEWriteOffOnApprover_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

        if (ViewState["oCompanyJEWriteOffOnApproverInfoCollection"] == null)
        {
            oCompanyJEWriteOffOnApproverInfoCollection = GetCompanyJEWriteOffOnApproversByRecPeriodID();
            ViewState["oCompanyJEWriteOffOnApproverInfoCollection"] = oCompanyJEWriteOffOnApproverInfoCollection;
        }
        else
        {
            oCompanyJEWriteOffOnApproverInfoCollection = (List<CompanyJEWriteOffOnApproverInfo>)ViewState["oCompanyJEWriteOffOnApproverInfoCollection"];
        }
        rgConfigureJEWriteOffOnApprover.MasterTableView.DataSource = oCompanyJEWriteOffOnApproverInfoCollection;


    }

    protected void rgConfigureJEWriteOffOnApprover_OnItemCommand(object sender, GridCommandEventArgs e)
    {


        if (e.CommandName == TelerikConstants.GridAddNewRowCommandName)
        {
            AddRowClick();
        }

    }
    int Sno = 0;
    protected void rgConfigureJEWriteOffOnApprover_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            CompanyJEWriteOffOnApproverInfo oCompanyJEWriteOffOnApproverInfo = (CompanyJEWriteOffOnApproverInfo)e.Item.DataItem;
            ExLabel lblSNo = (ExLabel)e.Item.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(++Sno);
            TextBox txtFromAmount = (TextBox)e.Item.FindControl("txtFromAmount");
            txtFromAmount.Text = Helper.GetDecimalValueForTextBox(oCompanyJEWriteOffOnApproverInfo.FromAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
            TextBox txtToAmount = (TextBox)e.Item.FindControl("txtToAmount");
            txtToAmount.Text =Helper.GetDecimalValueForTextBox ( oCompanyJEWriteOffOnApproverInfo.ToAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX) ;

            DropDownList ddlPrimaryApprover = (DropDownList)e.Item.FindControl("ddlPrimaryApprover");
            ListControlHelper.BindJEWriteOffOnApproverDropDown(ddlPrimaryApprover);
            if (oCompanyJEWriteOffOnApproverInfo.PrimaryApproverUserID != null )
           ddlPrimaryApprover.SelectedValue  = oCompanyJEWriteOffOnApproverInfo.PrimaryApproverUserID.ToString () ;

            DropDownList ddlSecondaryApprover = (DropDownList)e.Item.FindControl("ddlSecondaryApprover");
            ListControlHelper.BindJEWriteOffOnApproverDropDown(ddlSecondaryApprover);
            if (oCompanyJEWriteOffOnApproverInfo.PrimaryApproverUserID != null)
            ddlSecondaryApprover.SelectedValue = oCompanyJEWriteOffOnApproverInfo.SecondaryApproverUserID .ToString();

        }
    }




    #region Methods

    void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        LoadData();
        if (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted)
        {
            btnSave.Visible = false;
        }
        else
        {
            btnSave.Visible = true;
        }
    }



    public override string GetMenuKey()
    {
        return "ConfigureJounralEntryWriteOff/OnApprover";
    }

    #endregion

    protected void btnSave_OnClick(object sender, EventArgs e)
    {

        try
        {
            IJournalEntry oJournalEntryClient = RemotingHelper.GetJournalEntryObject();
            DateTime updateTime = DateTime.Now;
            oCompanyJEWriteOffOnApproverInfoCollection = new List<CompanyJEWriteOffOnApproverInfo>();
            CompanyJEWriteOffOnApproverInfo oCompanyJEWriteOffOnApproverInfo;
            foreach (GridDataItem dataItem in rgConfigureJEWriteOffOnApprover.Items)
            {
                oCompanyJEWriteOffOnApproverInfo = new CompanyJEWriteOffOnApproverInfo();
               
                string tempFromAmount = ((TextBox)dataItem["col1"].FindControl("txtFromAmount")).Text;
                if (tempFromAmount != null && tempFromAmount != "")
                    oCompanyJEWriteOffOnApproverInfo.FromAmount = Convert.ToDecimal(tempFromAmount);
                string tempToAmount = ((TextBox)dataItem["col2"].FindControl("txtToAmount")).Text;
                if (tempToAmount != null && tempToAmount != "")
                    oCompanyJEWriteOffOnApproverInfo.ToAmount = Convert.ToDecimal(tempToAmount);

                string ddlPrimaryApproverSelectedVal = ((DropDownList)dataItem["col4"].FindControl("ddlPrimaryApprover")).SelectedValue;
                if (ddlPrimaryApproverSelectedVal != null && ddlPrimaryApproverSelectedVal != "-2")
                    oCompanyJEWriteOffOnApproverInfo.PrimaryApproverUserID = Convert.ToInt32(ddlPrimaryApproverSelectedVal);

                string ddlSecondaryApproverSelectedVal = ((DropDownList)dataItem["col5"].FindControl("ddlSecondaryApprover")).SelectedValue;
                if (ddlSecondaryApproverSelectedVal != null && ddlSecondaryApproverSelectedVal != "-2")
                    oCompanyJEWriteOffOnApproverInfo.SecondaryApproverUserID = Convert.ToInt32(ddlSecondaryApproverSelectedVal);

                if (tempToAmount != "" && ddlPrimaryApproverSelectedVal != "-2")
                    oCompanyJEWriteOffOnApproverInfoCollection.Add(oCompanyJEWriteOffOnApproverInfo);

            }

            oJournalEntryClient.SaveCompanyJEWriteOffOnApprovers(oCompanyJEWriteOffOnApproverInfoCollection, SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserLoginID, updateTime, SessionHelper.CurrentUserLoginID, updateTime, Helper.GetAppUserInfo());

            Helper.RedirectToHomePage(2121);

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

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }
    protected void AddRowClick()
    {


        oCompanyJEWriteOffOnApproverInfoCollection = new List<CompanyJEWriteOffOnApproverInfo>();
        CompanyJEWriteOffOnApproverInfo oCompanyJEWriteOffOnApproverInfo;


        foreach (GridDataItem dataItem in rgConfigureJEWriteOffOnApprover.Items)
        {
            oCompanyJEWriteOffOnApproverInfo = new CompanyJEWriteOffOnApproverInfo();
            string tempFromAmount = ((TextBox)dataItem["col1"].FindControl("txtFromAmount")).Text;
            if (tempFromAmount != null && tempFromAmount != "")
                oCompanyJEWriteOffOnApproverInfo.FromAmount = Convert.ToDecimal(tempFromAmount);
            string tempToAmount = ((TextBox)dataItem["col2"].FindControl("txtToAmount")).Text;
            if (tempToAmount != null && tempToAmount != "")
                oCompanyJEWriteOffOnApproverInfo.ToAmount = Convert.ToDecimal(tempToAmount);

            string ddlPrimaryApproverSelectedVal = ((DropDownList)dataItem["col4"].FindControl("ddlPrimaryApprover")).SelectedValue ;
            if (ddlPrimaryApproverSelectedVal != null && ddlPrimaryApproverSelectedVal != "")
                oCompanyJEWriteOffOnApproverInfo.PrimaryApproverUserID  = Convert.ToInt32 (ddlPrimaryApproverSelectedVal);

            string ddlSecondaryApproverSelectedVal = ((DropDownList)dataItem["col5"].FindControl("ddlSecondaryApprover")).SelectedValue;
            if (ddlSecondaryApproverSelectedVal != null && ddlSecondaryApproverSelectedVal != "")
                oCompanyJEWriteOffOnApproverInfo.SecondaryApproverUserID  = Convert.ToInt32(ddlSecondaryApproverSelectedVal);
          


            oCompanyJEWriteOffOnApproverInfoCollection.Add(oCompanyJEWriteOffOnApproverInfo);

        }

        oCompanyJEWriteOffOnApproverInfo = new CompanyJEWriteOffOnApproverInfo();
        oCompanyJEWriteOffOnApproverInfoCollection.Add(oCompanyJEWriteOffOnApproverInfo);
        ViewState["oCompanyJEWriteOffOnApproverInfoCollection"] = oCompanyJEWriteOffOnApproverInfoCollection;
        rgConfigureJEWriteOffOnApprover.Rebind();

    }

}
