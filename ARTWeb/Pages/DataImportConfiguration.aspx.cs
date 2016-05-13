using SkyStem.ART.Client.Data;
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Pages_DataImportConfiguration : PageBaseCompany
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
            Helper.SetPageTitle(this, 2847);
            ucInputRequirements.ShowInputRequirements(2890);
            if (InputRequirementsFTPConfig != null)
                InputRequirementsFTPConfig.ShowInputRequirements(2927, 2928, 2929);
            if (!IsPostBack)
            {
                string CollapsedText = LanguageUtil.GetValue(1908);
                string ExpandedText = LanguageUtil.GetValue(1260);
                cpeSRARuleSelection.CollapsedText = CollapsedText;
                cpeSRARuleSelection.ExpandedText = ExpandedText;
                SetImportTypeDDl();
                short DataImportType;
                short.TryParse(ddlDataImportType.SelectedValue, out DataImportType);
                BindGrid(DataImportType);

                if (FTPHelper.IsFTPActivatedCompanyAndUserLevel())
                {
                    trFTPConfiguration.Visible = true;
                    BindFTPConfigurationGrid();
                }
                else
                    trFTPConfiguration.Visible = false;

                if (SessionHelper.CurrentRoleID != 2)
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                }
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
    protected void rgDataImportWarning_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Header)
        {
            rgDataImportWarning.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
            if (SessionHelper.CurrentRoleID.HasValue && SessionHelper.CurrentRoleID.Value != (short)WebEnums.UserRole.SYSTEM_ADMIN)
            {
                CheckBox chkbx = (CheckBox)(e.Item as GridHeaderItem)["CheckboxSelectColumn"].Controls[0];
                chkbx.Enabled = false;
            }
        }
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            DataImportMessageInfo oDataImportMessageInfo = (DataImportMessageInfo)e.Item.DataItem;
            ExLabel lblDataImportWarning = (ExLabel)e.Item.FindControl("lblDataImportWarning");
            ExLabel lblCondition = (ExLabel)e.Item.FindControl("lblCondition");
            HiddenField lblDataImportWarningPreferencesID = (HiddenField)e.Item.FindControl("lblDataImportWarningPreferencesID");
            CheckBox chkbox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

            lblDataImportWarning.Text = oDataImportMessageInfo.DataImportMessageLabel;
            lblCondition.Text = oDataImportMessageInfo.Description;

            short DataImportType;
            short.TryParse(ddlDataImportType.SelectedValue, out DataImportType);
            List<DataImportWarningPreferencesInfo> oDataImportWarningPreferencesInfolst = DataImportTemplateHelper.GetDataImportWarningPreferences(SessionHelper.CurrentCompanyID.Value, DataImportType);
            if (oDataImportWarningPreferencesInfolst.Count > 0)
            {
                var objWarningPreferenceslst = oDataImportWarningPreferencesInfolst.Where(x => x.DataImportMessageID == oDataImportMessageInfo.DataImportMessageID).FirstOrDefault();
                if (objWarningPreferenceslst != null)
                {
                    if (objWarningPreferenceslst.DataImportWarningPreferencesID > 0)
                    {
                        if (objWarningPreferenceslst.IsEnabled == true)
                        {
                            e.Item.Selected = true;
                            chkbox.Checked = true;
                        }
                        lblDataImportWarningPreferencesID.Value = Helper.GetDisplayIntegerValue(objWarningPreferenceslst.DataImportWarningPreferencesID);
                    }
                    else
                    {
                        lblDataImportWarningPreferencesID.Value = Helper.GetDisplayIntegerValue(0);
                    }
                }
                else
                {
                    lblDataImportWarningPreferencesID.Value = Helper.GetDisplayIntegerValue(0);
                }
            }
            else
            {
                e.Item.Selected = true;
                chkbox.Checked = true;
                lblDataImportWarningPreferencesID.Value = Helper.GetDisplayIntegerValue(0);
            }
            if (SessionHelper.CurrentRoleID.HasValue && SessionHelper.CurrentRoleID.Value != (short)WebEnums.UserRole.SYSTEM_ADMIN)
            {
                chkbox.Enabled = false;
            }
            else
            {
                chkbox.Enabled = true;
            }
        }
    }
    protected void rgFTPConfig_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {


        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            UserFTPConfigurationInfo oUserFTPConfigurationInfo = (UserFTPConfigurationInfo)e.Item.DataItem;
            CheckBox chkSelectItem = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
            ExLabel lblDataImportType = (ExLabel)e.Item.FindControl("lblDataImportType");
            ExTextBox txttxtProfileName = (ExTextBox)e.Item.FindControl("txtProfileName");
            DropDownList ddlFTPUploadRole = (DropDownList)e.Item.FindControl("ddlFTPUploadRole");
            DropDownList ddlDataImportTemplate = (DropDownList)e.Item.FindControl("ddlDataImportTemplate");
            ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
            ExLabel lblFTPPath = (ExLabel)e.Item.FindControl("lblFTPPath");
            ListControlHelper.BindFTPRoleDropdown(ddlFTPUploadRole);
            if (oUserFTPConfigurationInfo.FTPUploadRoleID.HasValue)
                ddlFTPUploadRole.SelectedValue = oUserFTPConfigurationInfo.FTPUploadRoleID.Value.ToString();

            ListControlHelper.BindDataimportTemplate(ddlDataImportTemplate, oUserFTPConfigurationInfo.DataImportTypeID.Value);
            if (oUserFTPConfigurationInfo.ImportTemplateID.HasValue)
                ddlDataImportTemplate.SelectedValue = oUserFTPConfigurationInfo.ImportTemplateID.Value.ToString();
            else
                ddlDataImportTemplate.SelectedValue = WebConstants.ART_TEMPLATE;
            lblDataImportType.Text = Helper.GetDisplayStringValue(GetDataImportType(oUserFTPConfigurationInfo));
            txttxtProfileName.Text = oUserFTPConfigurationInfo.ProfileName;
            lblFTPPath.Text = Helper.GetDisplayStringValue(GetFTPPath(oUserFTPConfigurationInfo));
            if (oUserFTPConfigurationInfo.IsFTPEnabled.HasValue && oUserFTPConfigurationInfo.IsFTPEnabled.Value)
            {
                imgSuccess.Visible = true;
                e.Item.Selected = true;
                chkSelectItem.Checked = true;
            }
            else
            {
                imgSuccess.Visible = false;
                e.Item.Selected = false;
                chkSelectItem.Checked = false;
            }
        }


    }
    protected void rgFTPConfig_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = FTPHelper.GetUserFTPConfiguration();
        rgFTPConfig.DataSource = oUserFTPConfigurationInfoList;
        if (oUserFTPConfigurationInfoList != null)
            rgFTPConfig.VirtualItemCount = oUserFTPConfigurationInfoList.Count;

    }
    #endregion

    #region Other Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo = new DataImportWarningPreferencesInfo();
        List<DataImportWarningPreferencesInfo> oDataImportWarningPreferencesInfoLst = this.GetSelectedDataImportWarningPreferences();
        if (oDataImportWarningPreferencesInfoLst.Count > 0)
        {
            DataTable dt = WebPartHelper.CreateDataTableWarningPreferences(oDataImportWarningPreferencesInfoLst);
            oDataImportWarningPreferencesInfo.UserID = SessionHelper.CurrentUserID.Value;
            oDataImportWarningPreferencesInfo.RoleID = SessionHelper.CurrentRoleID.Value;
            oDataImportWarningPreferencesInfo.DateAdded = DateTime.Now;
            oDataImportWarningPreferencesInfo.AddedBy = SessionHelper.CurrentUserLoginID;
            oDataImportWarningPreferencesInfo.CompanyID = SessionHelper.CurrentCompanyID.Value;
            short DataImportType;
            short.TryParse(ddlDataImportType.SelectedValue, out DataImportType);
            oDataImportWarningPreferencesInfo.DataImportTypeID = DataImportType;
            result = DataImportTemplateHelper.SaveDataImportWarningPreferences(dt, oDataImportWarningPreferencesInfo);
            if (result > 0)
            {
                int LabelID = 2892;
                Response.Redirect("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
            }
        }
    }
    protected void btnSaveFTPConfig_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int result = 0;

            List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = this.GetSelectedUserFTPConfiguration();
            if (oUserFTPConfigurationInfoList.Count > 0)
            {
                result = FTPHelper.SaveUserFTPConfiguration(oUserFTPConfigurationInfoList);
                if (result > 0)
                {
                    int LabelID = 2892;
                    Response.Redirect("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx");
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

    protected void ddlDataImportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        short DataImportType = short.Parse(ddlDataImportType.SelectedValue);
        BindGrid(DataImportType);
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void BindGrid(short DataImportTypeId)
    {
        List<DataImportMessageInfo> oDataImportMessageInfo = DataImportTemplateHelper.GetAllWarningMsg(DataImportTypeId);
        rgDataImportWarning.DataSource = oDataImportMessageInfo;
        rgDataImportWarning.DataBind();
    }
    private List<DataImportWarningPreferencesInfo> GetSelectedDataImportWarningPreferences()
    {
        List<DataImportWarningPreferencesInfo> oDataImportWarningPreferencesInfoLst = new List<DataImportWarningPreferencesInfo>();
        foreach (GridDataItem item in rgDataImportWarning.Items)
        {
            DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo = new DataImportWarningPreferencesInfo();
            CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
            HiddenField lblDataImportWarningPreferencesID = (HiddenField)item.FindControl("lblDataImportWarningPreferencesID");
            double WarningPreferencesID;
            double.TryParse(lblDataImportWarningPreferencesID.Value, out WarningPreferencesID);
            oDataImportWarningPreferencesInfo.DataImportWarningPreferencesID = (Int32)WarningPreferencesID;
            oDataImportWarningPreferencesInfo.DataImportMessageID = Convert.ToInt16(item.GetDataKeyValue("DataImportMessageID"));
            oDataImportWarningPreferencesInfo.IsEnabled = item.Selected;
            oDataImportWarningPreferencesInfoLst.Add(oDataImportWarningPreferencesInfo);
        }
        return oDataImportWarningPreferencesInfoLst;
    }
    private void BindFTPConfigurationGrid()
    {
        List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = FTPHelper.GetUserFTPConfiguration();
        if (oUserFTPConfigurationInfoList.Count > 0)
        {
            FTPServerInfo oFTPServerInfo = FTPHelper.GetFTPServerInfo(oUserFTPConfigurationInfoList[0].FTPServerId, oUserFTPConfigurationInfoList[0].CompanyID);
            if (oFTPServerInfo != null)
                lblFTPServerVal.Text = Helper.GetDisplayStringValue(oFTPServerInfo.FTPUrl);
        }
        if (oUserFTPConfigurationInfoList != null)
        {
            rgFTPConfig.DataSource = oUserFTPConfigurationInfoList;
            rgFTPConfig.VirtualItemCount = oUserFTPConfigurationInfoList.Count;
            rgFTPConfig.DataBind();
        }
    }
    private string GetFTPPath(UserFTPConfigurationInfo oUserFTPConfigurationInfo)
    {
        string FTPPath = null;
        if (oUserFTPConfigurationInfo != null)
        {
            FTPServerInfo oFTPServerInfo = FTPHelper.GetFTPServerInfo(oUserFTPConfigurationInfo.FTPServerId, oUserFTPConfigurationInfo.CompanyID);
            if (oFTPServerInfo != null && !string.IsNullOrEmpty(oFTPServerInfo.FTPUrl))
                FTPPath = oFTPServerInfo.FTPUrl + "/" + SharedDataImportHelper.GetFTPLoginID(oUserFTPConfigurationInfo.FTPLoginID) + "/" + SharedDataImportHelper.GetFTPDataImportFolderName(oUserFTPConfigurationInfo);
        }
        return FTPPath;

    }
    private string GetDataImportType(UserFTPConfigurationInfo oUserFTPConfigurationInfo)
    {
        List<DataImportTypeMstInfo> objDataImportTypeMstInfolist = (List<DataImportTypeMstInfo>)SessionHelper.GetAllDataImportType();
        DataImportTypeMstInfo oDataImportTypeMstInfo = null;
        string DataImportType = null;
        if (objDataImportTypeMstInfolist != null && objDataImportTypeMstInfolist.Count > 0)
        {
            oDataImportTypeMstInfo = objDataImportTypeMstInfolist.Find(obj => obj.DataImportTypeID.GetValueOrDefault() == oUserFTPConfigurationInfo.DataImportTypeID.GetValueOrDefault());
            if (oDataImportTypeMstInfo != null)
                DataImportType = oDataImportTypeMstInfo.DataImportType;
        }
        return DataImportType;

    }
    private List<UserFTPConfigurationInfo> GetSelectedUserFTPConfiguration()
    {
        List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = new List<UserFTPConfigurationInfo>();
        foreach (GridDataItem item in rgFTPConfig.Items)
        {
            int UserFTPConfigurationID;
            UserFTPConfigurationInfo oUserFTPConfigurationInfo = new UserFTPConfigurationInfo();
            CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
            if (chkSelectItem != null)
                oUserFTPConfigurationInfo.IsFTPEnabled = chkSelectItem.Checked;
            UserFTPConfigurationID = Convert.ToInt32(item.GetDataKeyValue("UserFTPConfigurationID"));
            if (UserFTPConfigurationID > 0)
                oUserFTPConfigurationInfo.UserFTPConfigurationID = UserFTPConfigurationID;
            else
                oUserFTPConfigurationInfo.UserFTPConfigurationID = null;
            oUserFTPConfigurationInfo.DataImportTypeID = Convert.ToInt16(item.GetDataKeyValue("DataImportTypeID"));
            ExTextBox txtProfileName = (ExTextBox)item.FindControl("txtProfileName");
            DropDownList ddlFTPUploadRole = (DropDownList)item.FindControl("ddlFTPUploadRole");
            DropDownList ddlDataImportTemplate = (DropDownList)item.FindControl("ddlDataImportTemplate");
            if (txtProfileName != null)
                oUserFTPConfigurationInfo.ProfileName = txtProfileName.Text;
            if (ddlFTPUploadRole != null)
                oUserFTPConfigurationInfo.FTPUploadRoleID = Convert.ToInt16(ddlFTPUploadRole.SelectedValue);
            if (ddlDataImportTemplate != null && ddlDataImportTemplate.SelectedValue != WebConstants.ART_TEMPLATE)
                oUserFTPConfigurationInfo.ImportTemplateID = Convert.ToInt16(ddlDataImportTemplate.SelectedValue);
            else
                oUserFTPConfigurationInfo.ImportTemplateID = null;
            oUserFTPConfigurationInfo.DateModified = DateTime.Now;
            oUserFTPConfigurationInfo.ModifyBy = SessionHelper.CurrentUserLoginID;
            oUserFTPConfigurationInfo.UserID = SessionHelper.CurrentUserID;
            oUserFTPConfigurationInfoList.Add(oUserFTPConfigurationInfo);

        }
        return oUserFTPConfigurationInfoList;
    }  
    #endregion

    #region Other Methods
    protected void SetImportTypeDDl()
    {
        try
        {
            IList<DataImportTypeMstInfo> objDataImportTypeMstInfolist = SessionHelper.GetAllDataImportType();
            objDataImportTypeMstInfolist = objDataImportTypeMstInfolist.Where(x => x.DataImportTypeID == 1 || x.DataImportTypeID == 3).ToList();
            if (objDataImportTypeMstInfolist != null)
            {
                ddlDataImportType.DataSource = objDataImportTypeMstInfolist;
                ddlDataImportType.DataValueField = "DataImportTypeID";
                ddlDataImportType.DataTextField = "Name";
                ddlDataImportType.DataBind();
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
    public override string GetMenuKey()
    {
        return "DataImportConfiguration";
    }
    protected void cvSaveFTPConfig_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            String ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1308);
            List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = this.GetSelectedUserFTPConfiguration();
            var oUserFTPConfigurationInfo = oUserFTPConfigurationInfoList.Find(obj => obj.IsFTPEnabled.GetValueOrDefault() == true && string.IsNullOrEmpty(obj.ProfileName));
            if ( oUserFTPConfigurationInfo != null )
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
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

}