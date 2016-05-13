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


public partial class Pages_ConfigureWorkWeek : PageBaseRecPeriod
{
    #region Variables & Constants
    #endregion

    #region Properties
    IList<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection = null;
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {



        try
        {
            Helper.SetPageTitle(this, 2129);
            Helper.ShowInputRequirementSection(this, 2077, 2078, 2079);
            // GetComapnyWorkWeek();


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
    #endregion

    #region Grid Events
    protected void rptWorkWeek_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                WeekDayMstInfo oWeekDayMstInfo = (WeekDayMstInfo)e.Item.DataItem;

                ExCheckBoxWithLabel chkWeekDay = (ExCheckBoxWithLabel)e.Item.FindControl("chkWeekDay");
                chkWeekDay.LabelID = oWeekDayMstInfo.WeekDayNameLabelID.Value;

                HtmlInputControl hdnWeekDayID = (HtmlInputControl)e.Item.FindControl("hdnWeekDayID");
                hdnWeekDayID.Value = oWeekDayMstInfo.WeekDayID.ToString();
                short? WeekDayID = null;
                if (oCompanyWorkWeekInfoCollection != null)
                    WeekDayID = (from oCompanyWorkWeekInfo in this.oCompanyWorkWeekInfoCollection
                                 where oCompanyWorkWeekInfo.WeekDayID == oWeekDayMstInfo.WeekDayID
                                 select oCompanyWorkWeekInfo.WeekDayID).FirstOrDefault();

                //if (SessionHelper.CurrentRecProcessStatusEnum != WebEnums.RecPeriodStatus.NotStarted)
                //{
                //    chkWeekDay.Enabled = false;
                //}

                if (WeekDayID != null)
                {
                    chkWeekDay.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    #endregion

    #region Other Events
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {


            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            List<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection = new List<CompanyWeekDayInfo>();
            DateTime updateTime = DateTime.Now;
            foreach (RepeaterItem item in rptWorkWeek.Items)
            {
                ExCheckBoxWithLabel chkWeekDay = (ExCheckBoxWithLabel)item.FindControl("chkWeekDay");
                HtmlInputControl hdnWeekDayID = (HtmlInputControl)item.FindControl("hdnWeekDayID");

                if (chkWeekDay.Checked)
                {
                    short WeekDayID = Convert.ToInt16(hdnWeekDayID.Value);
                    CompanyWeekDayInfo oCompanyWorkWeekInfo = new CompanyWeekDayInfo();
                    //oCompanyWorkWeekInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                    //oCompanyWorkWeekInfo.CompanyID = SessionHelper.CurrentCompanyID;
                    oCompanyWorkWeekInfo.WeekDayID = WeekDayID;
                    //oCompanyWorkWeekInfo.DateAdded = updateTime;
                    //oCompanyWorkWeekInfo.RevisedBy  = SessionHelper.CurrentUserLoginID;
                    //oCompanyWorkWeekInfo.DateRevised = updateTime;
                    oCompanyWorkWeekInfo.StartRecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                    //oCompanyWorkWeekInfo.IsActive = chkWeekDay.Checked;
                    oCompanyWorkWeekInfo.EndRecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                    oCompanyWorkWeekInfoCollection.Add(oCompanyWorkWeekInfo);
                }
            }

            oCompanyClient.SaveWorkWeek(oCompanyWorkWeekInfoCollection, SessionHelper.CurrentCompanyID, SessionHelper.CurrentUserLoginID, updateTime, SessionHelper.CurrentUserLoginID, updateTime, Helper.GetAppUserInfo());






            int LabelID = 2076;
            Response.Redirect("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());

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
        Response.Redirect("Home.aspx");
    }
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        Sel.Value = string.Empty;
        GetComapnyWorkWeek();
        LoadWorkWeek();
        //GetComapnyWorkWeek();
        if (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted)
        {
            this.pnlWorkWeek.Enabled = false;
            btnSave.Visible = false;
        }
        else
        {
            this.pnlWorkWeek.Enabled = true;
            btnSave.Visible = true;
        }
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void LoadWorkWeek()
    {
        IList<WeekDayMstInfo> oWeekDayMstInfoInfoCollection = null;
        oWeekDayMstInfoInfoCollection = SessionHelper.GetAllWeekDays();
        rptWorkWeek.DataSource = oWeekDayMstInfoInfoCollection;
        rptWorkWeek.DataBind();
    }
    private IList<CompanyWeekDayInfo> GetComapnyWorkWeek()
    {
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        if (oCompanyWorkWeekInfoCollection == null)
            oCompanyWorkWeekInfoCollection = oCompanyClient.GetComapnyWorkWeek(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        return oCompanyWorkWeekInfoCollection;
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {

        return "ConfigureWorkWeek";
    }
    #endregion

}
