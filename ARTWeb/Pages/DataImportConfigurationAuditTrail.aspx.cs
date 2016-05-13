using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Pages_DataImportConfigurationAuditTrail : PageBaseCompany
{

    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region  Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2900);
        Helper.SetBreadcrumbs(this, 1205, 2847,2900);
        if (!IsPostBack)
        {
            BindAudit(SessionHelper.CurrentCompanyID.Value,SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value);
        }
    }
    #endregion

    #region Grid Events
    protected void rgWarningAuditTrail_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgWarningAuditTrail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            DataImportWarningPreferencesAuditInfo oDataImportWarningPreferencesAuditInfo = (DataImportWarningPreferencesAuditInfo)e.Item.DataItem;

            ExLabel lblDataImportType = (ExLabel)e.Item.FindControl("lblDataImportType");
            ExLabel lblDataImportMessage = (ExLabel)e.Item.FindControl("lblDataImportMessage");
            ExLabel lblIsEnabled = (ExLabel)e.Item.FindControl("lblIsEnabled");
            ExLabel lblFirstName = (ExLabel)e.Item.FindControl("lblFirstName");
            ExLabel lblChangeDate = (ExLabel)e.Item.FindControl("lblChangeDate");
            ExLabel lblRoleName = (ExLabel)e.Item.FindControl("lblRoleName");

            lblDataImportType.Text = oDataImportWarningPreferencesAuditInfo.DataImportTypeLabel;
            lblDataImportMessage.Text = oDataImportWarningPreferencesAuditInfo.DataImportMessageLabel;
            lblFirstName.Text = Helper.GetDisplayUserFullName(oDataImportWarningPreferencesAuditInfo.FirstName, oDataImportWarningPreferencesAuditInfo.LastName);
            lblChangeDate.Text = Helper.GetDisplayDateTime(oDataImportWarningPreferencesAuditInfo.ChangeDate);
            lblRoleName.Text = oDataImportWarningPreferencesAuditInfo.RoleName;
            if (oDataImportWarningPreferencesAuditInfo.IsEnabled == true)
                lblIsEnabled.Text = "Enabled";
            else
                lblIsEnabled.Text = "Disabled";

        }
        if(e.Item is GridGroupHeaderItem)
        {
            GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
            DataRowView groupDataRow = (DataRowView)e.Item.DataItem;
            item.DataCell.Text = groupDataRow["DataImportTypeLabel"].ToString();
        }
    }
    protected void rgWarningAuditTrail_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            GridHelper.ExportGridToPDF(rgWarningAuditTrail, 2900);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            GridHelper.ExportGridToExcel(rgWarningAuditTrail, 2900);

        }
    }
    protected void rgWarningAuditTrail_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        rgWarningAuditTrail.CurrentPageIndex = e.NewPageIndex;
        BindAudit(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value);
    }
    #endregion

    #region Other Events
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Pages/DataImportConfiguration.aspx");
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
    #endregion

    #region Private Methods
    private void BindAudit(int CurrentCompanyID, int CurrentUserID, short CurrentRoleID)
    {
        List<DataImportWarningPreferencesAuditInfo> oDataImportWarningPreferencesAuditLst = DataImportTemplateHelper.GetAllWarningAuditList(CurrentCompanyID,CurrentUserID, CurrentRoleID);
        rgWarningAuditTrail.DataSource = oDataImportWarningPreferencesAuditLst;
        rgWarningAuditTrail.DataBind();
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "DataImportConfigurationAuditTrail";
    }
    #endregion


 
}