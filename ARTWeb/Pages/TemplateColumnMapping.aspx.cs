using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.MappingUpload;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Pages_TemplateColumnMapping : PageBaseCompany
{
    #region Variables & Constants
    private bool selectOption = true;
    Int32 TemplateId = 0;
    List<String> MandaortyColumns = null;
    short? TemplateTypeID;
    #endregion

    #region Properties
    public bool ShowSelectCheckBoxColum
    {
        get { return selectOption; }
        set { selectOption = value; }
    }
    public ExRadGrid Grid
    {
        get
        {
            return ucSkyStemARTGridMapping;
        }
    }

    private List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList
    {
        get { return DataImportTemplateHelper.GetTemplateFieldMappingData(Convert.ToInt32(Request.Params["TemplateId"])); }
        set { oImportTemplateFieldMappingInfoList = value; }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 2872);
            Helper.ShowInputRequirementSection(this, 2871);
            if (!IsPostBack)
            {
                if (Request.Params["TemplateId"] != null)
                {
                    TemplateId = Convert.ToInt32(Request.Params["TemplateId"]);
                    BindTemplateData(TemplateId);
                    if (TemplateTypeID.HasValue && TemplateTypeID.Value == (short)ARTEnums.DataImportType.GLData)
                        MandaortyColumns = DataImportHelper.GetGLDataImportAllMandatoryFields(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
                    else if (TemplateTypeID.HasValue && TemplateTypeID.Value == (short)ARTEnums.DataImportType.SubledgerData)
                        MandaortyColumns = DataImportHelper.GetSubledgerDataImportAllMandatoryFields(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
                    BindGrid();
                    btnSave.Enabled = true;
                }
                if (Request.Params["View"] != null)
                {
                    btnSave.Enabled = false;
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
    protected void ucSkyStemARTGridMapping_GridItemDataBound(object sender, GridItemEventArgs e)
    {
        //if (e.Item.ItemType == GridItemType.Header)
        //{
        //    ucSkyStemARTGridMapping.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
        //}
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            ImportFieldMstInfo oImportFieldMstInfo = (ImportFieldMstInfo)e.Item.DataItem;
            ExLabel lblTemplateFields = (ExLabel)e.Item.FindControl("lblTemplateFields");
            DropDownList ddlTemplateFields = (DropDownList)e.Item.FindControl("ddlTemplateFields");
            RequiredFieldValidator rfvTemplateFields = (RequiredFieldValidator)e.Item.FindControl("rfvTemplateFields");
            ExLabel lblMandatory = (ExLabel)e.Item.FindControl("lblMandatory");

            BindTemplateData(ddlTemplateFields);

            List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = DataImportTemplateHelper.GetTemplateFieldMappingData(this.TemplateId);
            if (oImportTemplateFieldMappingInfoList != null && oImportTemplateFieldMappingInfoList.Count > 0)
                SetTemplateColumnMapping(ddlTemplateFields, oImportFieldMstInfo.ImportFieldID, oImportTemplateFieldMappingInfoList);

            lblTemplateFields.Text = Convert.ToString(oImportFieldMstInfo.Description);

            if (oImportFieldMstInfo.GeographyClassID == 0)
            {
                rfvTemplateFields.Enabled = true;
                lblMandatory.Visible = true;
            }

            if (MandaortyColumns != null)
            {
                bool IsMapped = MandaortyColumns.Contains(oImportFieldMstInfo.Description);
                if (IsMapped == true)
                {
                    rfvTemplateFields.Enabled = true;
                    lblMandatory.Visible = true;
                }
                else
                {
                    if (oImportFieldMstInfo.Description == GLDataImportFields.GLACCOUNTNAME)
                    {
                        rfvTemplateFields.Enabled = true;
                        lblMandatory.Visible = true;
                    }
                    else
                    {
                        rfvTemplateFields.Enabled = false;
                        lblMandatory.Visible = false;
                    }
                }
            }
            else
            {
                rfvTemplateFields.Enabled = true;
                lblMandatory.Visible = true;
            }
            if (Request.Params["View"] != null)
            {
                ddlTemplateFields.Enabled = false;
            }
            else
            {
                ddlTemplateFields.Enabled = true;
            }
        }
    }
    #endregion

    #region Other Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int result = 0;
            ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo = new ImportTemplateFieldMappingInfo();
            List<ImportTemplateFieldMappingInfo> oNewImportTemplateFieldMappingInfoLst = this.GetSelectedAccountInformation();
            bool IsValueChanged = IsMappinChanged(oNewImportTemplateFieldMappingInfoLst);
            if (oNewImportTemplateFieldMappingInfoLst.Count > 0 && IsValueChanged == true)
            {
                if (Request.Params["TemplateId"] != null)
                    TemplateId = Convert.ToInt32(Request.Params["TemplateId"]);

                DataTable dt = WebPartHelper.CreateDataTableTempalteMapping(oNewImportTemplateFieldMappingInfoLst);
                oImportTemplateFieldMappingInfo.DateAdded = DateTime.Now;
                oImportTemplateFieldMappingInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oImportTemplateFieldMappingInfo.ImportTemplateID = TemplateId;
                result = DataImportTemplateHelper.SaveImportTemplateMapping(dt, oImportTemplateFieldMappingInfo);
                if (result > -1)
                {    //Response.Redirect("~/Pages/CreateDataImportTemplate.aspx");
                    SessionHelper.RedirectToUrl("~/Pages/CreateDataImportTemplate.aspx");
                    return;
                }
            }
            else
            {
                if (IsValueChanged == false)
                {
                    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                    oMasterPageBase.ShowErrorMessage(2877);
                }
                else
                {
                    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                    oMasterPageBase.ShowErrorMessage(2013);
                }
            }

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Pages/CreateDataImportTemplate.aspx");
        SessionHelper.RedirectToUrl("~/Pages/CreateDataImportTemplate.aspx");
        return;
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void BindTemplateData(int TemplateId)
    {
        ImportTemplateHdrInfo oImportTemplateInfoList = DataImportTemplateHelper.GetTemplateFields(TemplateId);
        Session["oImportTemplateInfoList"] = oImportTemplateInfoList;
        lbTemplateName.Text = Helper.GetDisplayStringValue(oImportTemplateInfoList.TemplateName);
        lbDataImport.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(oImportTemplateInfoList.DataImportTypeLabelID.GetValueOrDefault()));
        lbCreatedBy.Text = Helper.GetDisplayStringValue(oImportTemplateInfoList.AddedBy);
        lbDateCreated.Text = Helper.GetDisplayDate(oImportTemplateInfoList.DateAdded);
        TemplateTypeID = oImportTemplateInfoList.DataImportTypeID;
    }

    private void BindTemplateData(DropDownList ddlTemplateFields)
    {
        if (Session["oImportTemplateInfoList"] != null)
        {
            ImportTemplateHdrInfo oImportTemplateInfoList = Session["oImportTemplateInfoList"] as ImportTemplateHdrInfo;
            ddlTemplateFields.DataSource = oImportTemplateInfoList.ImportTemplateFieldsInfoList;
            ddlTemplateFields.DataTextField = "FieldName";
            ddlTemplateFields.DataValueField = "ImportTemplateFieldID";
            ddlTemplateFields.DataBind();
            ListControlHelper.AddListItemForSelectColumn(ddlTemplateFields);
        }
    }

    private void BindGrid()
    {
        ImportTemplateHdrInfo oImportTemplateInfoList = new ImportTemplateHdrInfo();
        if (Session["oImportTemplateInfoList"] == null)
            oImportTemplateInfoList = DataImportTemplateHelper.GetTemplateFields(TemplateId);
        else
            oImportTemplateInfoList = Session["oImportTemplateInfoList"] as ImportTemplateHdrInfo;
        List<ImportFieldMstInfo> ImportFieldMstInfoLst = DataImportTemplateHelper.GetFieldsMst(SessionHelper.CurrentCompanyID.Value, oImportTemplateInfoList.DataImportTypeID);
        ucSkyStemARTGridMapping.DataSource = ImportFieldMstInfoLst;
        ucSkyStemARTGridMapping.DataBind();
    }

    private IList<MappingUploadInfo> LoadComapnyMappingUploadKeys()
    {
        IList<MappingUploadInfo> MappingUploadInfoList = null;
        IMappingUpload oMappingUploadClient = RemotingHelper.GetMappingUploadObject();
        if (MappingUploadInfoList == null)
            MappingUploadInfoList = oMappingUploadClient.GetMappingUploadInfoList(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        return MappingUploadInfoList;
    }

    private List<int> GridSelectedItems()
    {
        List<int> oSelecteducSkyStemARTGridMappingIDList = new List<int>();
        int MappingListID;
        foreach (GridDataItem item in ucSkyStemARTGridMapping.SelectedItems)
        {
            CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
            if (chkSelectItem != null && chkSelectItem.Checked)
            {
                MappingListID = Convert.ToInt32(item.GetDataKeyValue("ImportFieldID"));
                oSelecteducSkyStemARTGridMappingIDList.Add(MappingListID);
            }


        }
        return oSelecteducSkyStemARTGridMappingIDList;
    }

    private List<ImportTemplateFieldMappingInfo> GetSelectedAccountInformation()
    {
        List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoLst = new List<ImportTemplateFieldMappingInfo>();
        foreach (GridDataItem item in ucSkyStemARTGridMapping.Items)
        {
            ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo = new ImportTemplateFieldMappingInfo();
            DropDownList ddlTemplateFieldMapping = (DropDownList)item.FindControl("ddlTemplateFields");
            if (ddlTemplateFieldMapping.SelectedValue != WebConstants.SELECT_ONE)
                oImportTemplateFieldMappingInfo.ImportTemplateFieldID = Convert.ToInt16(ddlTemplateFieldMapping.SelectedValue);

            //CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
            //if (chkSelectItem != null && chkSelectItem.Checked)
            //{
            oImportTemplateFieldMappingInfo.ImportFieldID = Convert.ToInt32(item.GetDataKeyValue("ImportFieldID"));
            //}
            oImportTemplateFieldMappingInfoLst.Add(oImportTemplateFieldMappingInfo);
            //oImportTemplateFieldMappingInfoLst = oImportTemplateFieldMappingInfoLst.Where(x => x.ImportTemplateFieldID > -2).ToList();
        }
        return oImportTemplateFieldMappingInfoLst;
    }

    private void SetTemplateColumnMapping(DropDownList ddlTemplateFields, Int32 ImportFieldID, List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
    {
        var oTemplateField = oImportTemplateFieldMappingInfoList.Where(x => x.ImportFieldID == ImportFieldID).FirstOrDefault();
        if (oTemplateField != null)
        {
            if (oTemplateField.ImportTemplateFieldID.HasValue)
            {
                ListItem oListItem = ddlTemplateFields.Items.FindByValue(oTemplateField.ImportTemplateFieldID.Value.ToString());
                if (oListItem != null)
                    oListItem.Selected = true;
            }
        }
    }
    private bool IsMappinChanged(List<ImportTemplateFieldMappingInfo> oNewImportTemplateFieldMappingInfoLst)
    {
        bool IsValueChanged = false;
        foreach (var oNewImportTemplateFieldMappingInfo in oNewImportTemplateFieldMappingInfoLst)
        {
            var oOldImportTemplateFieldMappingInfo = oImportTemplateFieldMappingInfoList.Find(obj => obj.ImportTemplateFieldID.GetValueOrDefault() == oNewImportTemplateFieldMappingInfo.ImportTemplateFieldID.GetValueOrDefault() && obj.ImportTemplateID.GetValueOrDefault() == oNewImportTemplateFieldMappingInfo.ImportTemplateID.GetValueOrDefault());
            if (oOldImportTemplateFieldMappingInfo == null)
                IsValueChanged = true;
        }
        return IsValueChanged;
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "TemplateColumnMapping";
    }
    #endregion

}