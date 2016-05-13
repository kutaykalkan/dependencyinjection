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


public partial class Pages_ConfigureJEColumns : PageBaseRecPeriod
{
    List<CompanyGLToolColumnInfo> oCompanyGLToolColumnInfoCollection = null;
    public DataTable dt;

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Helper.SetPageTitle(this, 2101);
            Helper.SetBreadcrumbs(this, 1207, 2101);
            Helper.ShowInputRequirementSection(this, 2102);
           
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
        if (ViewState["oCompanyGLToolColumnInfoCollection"] == null)
        {
            oCompanyGLToolColumnInfoCollection = GetGLToolColumnsByRecPeriodID();
            ViewState["oCompanyGLToolColumnInfoCollection"] = oCompanyGLToolColumnInfoCollection;
        }
        else
        {
            oCompanyGLToolColumnInfoCollection = (List<CompanyGLToolColumnInfo>)ViewState["oCompanyGLToolColumnInfoCollection"];
        }

        rgJEColumns.MasterTableView.DataSource = oCompanyGLToolColumnInfoCollection;

    }
    private List<CompanyGLToolColumnInfo> GetGLToolColumnsByRecPeriodID()
    {
        int DefaultNoOfRows = Convert.ToInt32(AppSettingHelper.GetAppSettingValue("DefaultNoOfRows"));
        oCompanyGLToolColumnInfoCollection = new List<CompanyGLToolColumnInfo>();

        IJournalEntry oJournalEntryClient = RemotingHelper.GetJournalEntryObject();
        oCompanyGLToolColumnInfoCollection = oJournalEntryClient.GetGLToolColumnsByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        // put Database call here
        if (oCompanyGLToolColumnInfoCollection.Count >= DefaultNoOfRows)
            DefaultNoOfRows = 0;
        else
            DefaultNoOfRows = DefaultNoOfRows - oCompanyGLToolColumnInfoCollection.Count;
        CompanyGLToolColumnInfo oCompanyGLToolColumnInfo;
        for (int i = 0; i < DefaultNoOfRows; i++)
        {
            oCompanyGLToolColumnInfo = new CompanyGLToolColumnInfo();
            //oCompanyGLToolColumnInfo.GLToolColumnName = "Column" + i;
            //oCompanyGLToolColumnInfo.GLToolColumnLength = (Int16)i;
            oCompanyGLToolColumnInfoCollection.Add(oCompanyGLToolColumnInfo);
        }
        return oCompanyGLToolColumnInfoCollection;


    }
    int Sno = 0;
    protected void rgJEColumns_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            CompanyGLToolColumnInfo oCompanyGLToolColumnInfo = (CompanyGLToolColumnInfo)e.Item.DataItem;
            ExLabel lblSNo = (ExLabel)e.Item.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(++Sno);
           TextBox txtColumnName = (TextBox)e.Item.FindControl("txtColumnName");
            txtColumnName.Text = oCompanyGLToolColumnInfo.GLToolColumnName;
            TextBox txtLenth = (TextBox)e.Item.FindControl("txtLenth");
            txtLenth.Text = oCompanyGLToolColumnInfo.GLToolColumnLength.ToString();

        }
    }


    protected void rgJEColumns_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

        if (ViewState["oCompanyGLToolColumnInfoCollection"] == null)
        {
            oCompanyGLToolColumnInfoCollection = GetGLToolColumnsByRecPeriodID();
            ViewState["oCompanyGLToolColumnInfoCollection"] = oCompanyGLToolColumnInfoCollection;
        }
        else
        {
            oCompanyGLToolColumnInfoCollection = (List<CompanyGLToolColumnInfo>)ViewState["oCompanyGLToolColumnInfoCollection"];
        }
        rgJEColumns.MasterTableView.DataSource = oCompanyGLToolColumnInfoCollection;

    }



    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
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

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            IJournalEntry oJournalEntryClient = RemotingHelper.GetJournalEntryObject();
            DateTime updateTime = DateTime.Now;
            oCompanyGLToolColumnInfoCollection = new List<CompanyGLToolColumnInfo>();
            CompanyGLToolColumnInfo oCompanyGLToolColumnInfo;
            foreach (GridDataItem dataItem in rgJEColumns.Items)
            {
                oCompanyGLToolColumnInfo = new CompanyGLToolColumnInfo();
                string tempColumnName = ((TextBox)dataItem["col1"].FindControl("txtColumnName")).Text;

                if (tempColumnName != null && tempColumnName != "")               
                    oCompanyGLToolColumnInfo.GLToolColumnName = tempColumnName;                
                string tempColumnLength = ((TextBox)dataItem["col2"].FindControl("txtLenth")).Text;
                if (tempColumnLength != null && tempColumnLength != "")
                    oCompanyGLToolColumnInfo.GLToolColumnLength = Convert.ToInt16(tempColumnLength);
                if (tempColumnName != "" && tempColumnLength != "")                
                    oCompanyGLToolColumnInfoCollection.Add(oCompanyGLToolColumnInfo);
                
            }

            oJournalEntryClient.SaveCompanyGLToolColumns(oCompanyGLToolColumnInfoCollection, SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserLoginID, updateTime, SessionHelper.CurrentUserLoginID, updateTime, Helper.GetAppUserInfo());

            Helper.RedirectToHomePage(2122);
           
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

    public override string GetMenuKey()
    {
        return "ConfigureJounralEntrySetUp";
    }
    protected void AddRowClick()
    {


        oCompanyGLToolColumnInfoCollection = new List<CompanyGLToolColumnInfo>();
        CompanyGLToolColumnInfo oCompanyGLToolColumnInfo;


        foreach (GridDataItem dataItem in rgJEColumns.Items)
        {
            oCompanyGLToolColumnInfo = new CompanyGLToolColumnInfo();
            oCompanyGLToolColumnInfo.GLToolColumnName = ((TextBox)dataItem["col1"].FindControl("txtColumnName")).Text;
            string tempColumnName=((TextBox)dataItem["col2"].FindControl("txtLenth")).Text;
            if ( tempColumnName!= null && tempColumnName !="")
                oCompanyGLToolColumnInfo.GLToolColumnLength = Convert.ToInt16(((TextBox)dataItem["col2"].FindControl("txtLenth")).Text);
            oCompanyGLToolColumnInfoCollection.Add(oCompanyGLToolColumnInfo);
        }
        oCompanyGLToolColumnInfo = new CompanyGLToolColumnInfo();
        oCompanyGLToolColumnInfoCollection.Add(oCompanyGLToolColumnInfo);
        ViewState["oCompanyGLToolColumnInfoCollection"] = oCompanyGLToolColumnInfoCollection;
        rgJEColumns.Rebind();

    }

    protected void rgJEColumns_OnItemCommand(object sender, GridCommandEventArgs e)
    {

        if (e.CommandName == TelerikConstants.GridAddNewRowCommandName)
        {
            AddRowClick();
        }

    }


   

}
