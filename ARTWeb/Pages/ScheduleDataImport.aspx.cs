using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Pages_ScheduleDataImport : PageBaseCompany
{
    #region Variables & Constants
    private bool selectOption = true;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 2883);
            Helper.ShowInputRequirementSection(this, 2882);
            if (!IsPostBack)
            {
                string CollapsedText = LanguageUtil.GetValue(1908);
                string ExpandedText = LanguageUtil.GetValue(1260);
                cpeScheduleDataImport.CollapsedText = CollapsedText;
                cpeScheduleDataImport.ExpandedText = ExpandedText;
                BindGrid();
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
    }
    #endregion

    #region Grid Events
    protected void ucSkyStemARTGridScheduleDataImport_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Header)
        {
            ucSkyStemARTGridScheduleDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
        }
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            DataImportTypeMstInfo oDataImportTypeMstInfo = (DataImportTypeMstInfo)e.Item.DataItem;
            ExLabel lblDataImportType = (ExLabel)e.Item.FindControl("lblDataImportType");
            ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
            ExLabel lblEvery = (ExLabel)e.Item.FindControl("lblEvery");
            ExLabel lblHours = (ExLabel)e.Item.FindControl("lblHours");
            ExLabel lblAnd = (ExLabel)e.Item.FindControl("lblAnd");
            ExLabel lblMinutes = (ExLabel)e.Item.FindControl("lblMinutes");
            TextBox txtEvery = (TextBox)e.Item.FindControl("txtEvery");
            TextBox txtMinutes = (TextBox)e.Item.FindControl("txtMinutes");
            ExLabel lblDataImportScheduleID = (ExLabel)e.Item.FindControl("lblDataImportScheduleID");
            ExLabel lblDefaultValue = (ExLabel)e.Item.FindControl("lblDefaultValue");

            lblDescription.Text = Helper.GetLabelIDValue(2884);
            lblDataImportType.Text = oDataImportTypeMstInfo.DataImportType;
            lblEvery.Text = Helper.GetLabelIDValue(1863);
            lblHours.Text = Helper.GetLabelIDValue(1864);
            lblAnd.Text = Helper.GetLabelIDValue(2336);
            lblMinutes.Text = Helper.GetLabelIDValue(2885);

            int totalminutes = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.TIMER_INTERVAL_DATA_PROCESSING));
            //total hours
            int hours = totalminutes / 60;
            //total minutes
            int minutes = totalminutes % 60;
            TimeSpan ts = new TimeSpan(hours, minutes, 0);
            lblDefaultValue.Text = string.Format("{0:%h} hours {0:%m} minutes", ts);

            List<DataImportScheduleInfo> oDataImportScheduleInfolst = DataImportTemplateHelper.GetDataImportSchedule(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID);
            if (oDataImportScheduleInfolst.Count > 0)
            {
                var oDataImpot = oDataImportScheduleInfolst.Where(x => x.DataImportTypeID == oDataImportTypeMstInfo.DataImportTypeID).FirstOrDefault();
                if (oDataImpot != null)
                {
                    if (oDataImpot.Hours.HasValue)
                        txtEvery.Text = Helper.GetDisplayIntegerValueForTextBox(oDataImpot.Hours);
                    if (oDataImpot.Minutes.HasValue)
                        txtMinutes.Text = Helper.GetDisplayIntegerValueForTextBox(oDataImpot.Minutes);
                    if (oDataImpot.DataImportScheduleID > 0)
                    {
                        lblDataImportScheduleID.Text = Helper.GetDisplayIntegerValue(oDataImpot.DataImportScheduleID);
                    }
                    else
                    {
                        lblDataImportScheduleID.Text = Helper.GetDisplayIntegerValue(0);
                    }
                }
                else
                {
                    lblDataImportScheduleID.Text = Helper.GetDisplayIntegerValue(0);
                }
            }
            else
            {
                lblDataImportScheduleID.Text = Helper.GetDisplayIntegerValue(0);
            }
        }
    }
    #endregion

    #region Other Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            DataImportScheduleInfo oDataImportScheduleInfo = new DataImportScheduleInfo();
            IList<DataImportScheduleInfo> oDataImportScheduleInfoLst = this.GetSelectedScheduleDataImportInformation();
            if (oDataImportScheduleInfoLst.Count > 0)
            {
                DataTable dt = WebPartHelper.CreateDataTableDataImportSchedule(oDataImportScheduleInfoLst);
                oDataImportScheduleInfo.UserID = SessionHelper.CurrentUserID.Value;
                oDataImportScheduleInfo.RoleID = SessionHelper.CurrentRoleID.Value;
                oDataImportScheduleInfo.IsActive = true;
                oDataImportScheduleInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oDataImportScheduleInfo.DateAdded = DateTime.Now;
                result = DataImportTemplateHelper.SaveDataImportSchedule(oDataImportScheduleInfo, dt);
                if (result > 0)
                {
                    int LabelID = 2887;
                    //Response.Redirect("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
                    SessionHelper.RedirectToUrl("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
                    return;
                }
            }
            else
            {
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowErrorMessage(2013);
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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.Redirect("Home.aspx");
            SessionHelper.RedirectToUrl("Home.aspx");
            return;
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
    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            DataImportScheduleInfo oDataImportScheduleInfo = new DataImportScheduleInfo();
            IList<DataImportScheduleInfo> oDataImportScheduleInfoLst = this.GetSelectedScheduleDataImportInformationReset();
            if (oDataImportScheduleInfoLst.Count > 0)
            {
                DataTable dt = WebPartHelper.CreateDataTableDataImportSchedule(oDataImportScheduleInfoLst);
                oDataImportScheduleInfo.UserID = SessionHelper.CurrentUserID.Value;
                oDataImportScheduleInfo.RoleID = SessionHelper.CurrentRoleID.Value;
                oDataImportScheduleInfo.IsActive = true;
                oDataImportScheduleInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oDataImportScheduleInfo.DateAdded = DateTime.Now;
                result = DataImportTemplateHelper.SaveDataImportSchedule(oDataImportScheduleInfo, dt);
                if (result > 0)
                {
                    int LabelID = 2887;
                    //Response.Redirect("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
                    SessionHelper.RedirectToUrl("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
                    return;
                }
            }
            else
            {
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowErrorMessage(2013);
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
    }


    #endregion

    #region Validation Control Events
    protected void NonEmptyAndPositive_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Regex.IsMatch(args.Value, @"^[1-9]+[0-9]*$"))
            args.IsValid = true;
        else
            args.IsValid = false;
    }
    #endregion

    #region Private Methods
    private void BindGrid()
    {
        IList<DataImportTypeMstInfo> objDataImportTypeMstInfolist = SessionHelper.GetAllDataImportType();
        objDataImportTypeMstInfolist = objDataImportTypeMstInfolist.Where(x => x.DataImportTypeID == 1 || x.DataImportTypeID == 3).ToList();
        ucSkyStemARTGridScheduleDataImport.DataSource = objDataImportTypeMstInfolist;
        ucSkyStemARTGridScheduleDataImport.DataBind();
    }
    private List<DataImportScheduleInfo> GetSelectedScheduleDataImportInformation()
    {
        List<DataImportScheduleInfo> oDataImportScheduleInfoLst = new List<DataImportScheduleInfo>();
        foreach (GridDataItem item in ucSkyStemARTGridScheduleDataImport.SelectedItems)
        {
            DataImportScheduleInfo oDataImportScheduleInfo = new DataImportScheduleInfo();
            CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
            ExLabel lblDescription = (ExLabel)item.FindControl("lblDescription");
            ExLabel lblDataImportScheduleID = (ExLabel)item.FindControl("lblDataImportScheduleID");
            TextBox txtEvery = (TextBox)item.FindControl("txtEvery");
            TextBox txtMinutes = (TextBox)item.FindControl("txtMinutes");
            oDataImportScheduleInfo.Description = lblDescription.Text;
            if (txtEvery.Text != "")
            {
                oDataImportScheduleInfo.Hours = Convert.ToInt16(txtEvery.Text);
            }
            else
            {
                oDataImportScheduleInfo.Hours = null;
            }
            if (txtMinutes.Text != "")
            {
                oDataImportScheduleInfo.Minutes = Convert.ToInt16(txtMinutes.Text);
            }
            else
            {
                oDataImportScheduleInfo.Minutes = null;
            }
            double DataImportScheduleID;
            double.TryParse(lblDataImportScheduleID.Text, out DataImportScheduleID);
            oDataImportScheduleInfo.DataImportScheduleID = Convert.ToInt32(DataImportScheduleID);
            if (chkSelectItem != null && chkSelectItem.Checked)
            {
                oDataImportScheduleInfo.DataImportTypeID = Convert.ToInt16(item.GetDataKeyValue("DataImportTypeID"));
            }
            oDataImportScheduleInfoLst.Add(oDataImportScheduleInfo);
        }
        return oDataImportScheduleInfoLst;
    }
    private List<DataImportScheduleInfo> GetSelectedScheduleDataImportInformationReset()
    {
        List<DataImportScheduleInfo> oDataImportScheduleInfoLst = new List<DataImportScheduleInfo>();
        foreach (GridDataItem item in ucSkyStemARTGridScheduleDataImport.SelectedItems)
        {
            DataImportScheduleInfo oDataImportScheduleInfo = new DataImportScheduleInfo();
            CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
            ExLabel lblDescription = (ExLabel)item.FindControl("lblDescription");
            ExLabel lblDataImportScheduleID = (ExLabel)item.FindControl("lblDataImportScheduleID");
            TextBox txtEvery = (TextBox)item.FindControl("txtEvery");
            TextBox txtMinutes = (TextBox)item.FindControl("txtMinutes");
            oDataImportScheduleInfo.Description = lblDescription.Text;
            if (txtEvery.Text != "")
            {
                oDataImportScheduleInfo.Hours = null;
            }
            if (txtMinutes.Text != "")
            {
                oDataImportScheduleInfo.Minutes = null;
            }
            double DataImportScheduleID;
            double.TryParse(lblDataImportScheduleID.Text, out DataImportScheduleID);
            oDataImportScheduleInfo.DataImportScheduleID = Convert.ToInt32(DataImportScheduleID);
            if (chkSelectItem != null && chkSelectItem.Checked)
            {
                oDataImportScheduleInfo.DataImportTypeID = Convert.ToInt16(item.GetDataKeyValue("DataImportTypeID"));
            }
            oDataImportScheduleInfoLst.Add(oDataImportScheduleInfo);
        }
        return oDataImportScheduleInfoLst;
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "ScheduleDataImport";
    }
    #endregion

}