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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using System.Collections.Generic;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using System.IO;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Shared.Utility;

public partial class Pages_DataImportGLMapping : PageBaseCompany
{
    #region Variables & Constants
    #endregion

    #region Properties
    private DataImportHdrInfo _DataImportHdrInfo
    {
        get
        {
            return (DataImportHdrInfo)ViewState["DataImportHdrInfo"];
        }
        set
        {
            ViewState["DataImportHdrInfo"] = value;
        }
    }

    private FileInfo _glDataUploadedFile
    {
        get
        {
            //if (ViewState["GLDataUploadedFile"] == null)
            //{
            //    ViewState["GLDataUploadedFile"] = new FileInfo(Session[SessionConstants.GLDATA_UPLOADFILE_PHYSICALPATH].ToString());
            //    Session.Remove("GLDataUploadFilePhysicalPath");
            //}
            return (FileInfo)ViewState["GLDataUploadedFile"];
        }
        set
        {
            ViewState["GLDataUploadedFile"] = value;
        }
    }
    private short _DataImportType
    {
        get
        {
            //if (ViewState["GLDataUploadedFile"] == null)
            //{
            //    ViewState["GLDataUploadedFile"] = new FileInfo(Session[SessionConstants.GLDATA_UPLOADFILE_PHYSICALPATH].ToString());
            //    Session.Remove("GLDataUploadFilePhysicalPath");
            //}
            return (short)ViewState["DataImportType"];
        }
        set
        {
            ViewState["DataImportType"] = value;
        }
    }

    public string InvalidSelection_WrongOrder
    {
        get
        {
            //"Invalid Selection: Selection has to be in order of Keys.";
            return Helper.GetLabelIDValue(5000302);
        }
    }
    public string InvalidSelection_NoSelection
    {
        get
        {
            //"Invalid Selection: No Selection Made";
            return Helper.GetLabelIDValue(5000303);
        }
    }

    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 1569);
            Helper.ShowInputRequirementSection(this, 1598);

            btnMap.Value = LanguageUtil.GetValue(1568);
            btnMap.Attributes.Add("onclick", "javascript:MapOrganizationalHierarchy('"
                + lstHierarchyKeys.ClientID + "', '"
                + lstHierarchyName.ClientID + "', '"
                + lstMapping.ClientID + "');");

            btnDeleteMapping.Value = LanguageUtil.GetValue(1564);
            btnDeleteMapping.Attributes.Add("onclick", "javascript:DeleteMapping('"
                + lstHierarchyKeys.ClientID + "', '"
                + lstHierarchyName.ClientID + "', '"
                + lstMapping.ClientID + "');");

            btnCancel.PostBackUrl = "~/Pages/DataImport.aspx";

            if (!Page.IsPostBack)
            {
                // Get Data From Session
                _DataImportHdrInfo = (DataImportHdrInfo)Session[SessionConstants.GLDATA_INFO];
                this._glDataUploadedFile = new FileInfo(_DataImportHdrInfo.PhysicalPath);
                this._DataImportType = _DataImportHdrInfo.DataImportTypeID.Value;
                Session.Remove(SessionConstants.GLDATA_INFO);

                //Bind list to keys from GeographyClassMst
                ListControlHelper.BindOrganizationalHierarchyKeysListBox(lstHierarchyKeys);
                this.BindOrganizationalHierarchyKeysNames(this._glDataUploadedFile);
                //Bind List from fiels from excel sheet.

                #region "OLD CODE"
                //// Dummy Data
                //ListItem oListItem = null;
                //switch (SessionHelper.CurrentCompanyID)
                //{
                //    case 1:
                //        oListItem = new ListItem("Region", "RegionID");
                //        lstHierarchyName.Items.Add(oListItem);

                //        oListItem = new ListItem("Entity", "EntityID");
                //        lstHierarchyName.Items.Add(oListItem);

                //        oListItem = new ListItem("Account #", "AccountID");
                //        lstHierarchyName.Items.Add(oListItem);
                //        break;

                //    case 2:

                //        oListItem = new ListItem("Business Unit", "BusinessUnitID");
                //        lstHierarchyName.Items.Add(oListItem);

                //        oListItem = new ListItem("Department", "DepartmentID");
                //        lstHierarchyName.Items.Add(oListItem);

                //        oListItem = new ListItem("Region", "RegionID");
                //        lstHierarchyName.Items.Add(oListItem);

                //        oListItem = new ListItem("Account #", "AccountID");
                //        lstHierarchyName.Items.Add(oListItem);
                //        break;
                //}
                #endregion
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
    #endregion

    #region Other Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //retrive mapping from HiddenField, save them to GeographyStructureHdrInfo Object
        //2^S:Key2=Account#,3^S:Key3=Entity,4^S:Key4=AccountName,5^S:Key5=AccountType

        try
        {
            IDataImport oDataImport = RemotingHelper.GetDataImportObject();
            DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
            List<GeographyStructureHdrInfo> oGeoStructHdrInfoCollection = new List<GeographyStructureHdrInfo>();
            oDataImportHrdInfo.DataImportName = this._DataImportHdrInfo.DataImportName;
            oDataImportHrdInfo.FileName = this._DataImportHdrInfo.FileName;
            oDataImportHrdInfo.PhysicalPath = this._glDataUploadedFile.ToString();
            oDataImportHrdInfo.FileSize = this._glDataUploadedFile.Length;
            oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oDataImportHrdInfo.DataImportTypeID = _DataImportType;
            oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Submitted;
            oDataImportHrdInfo.RecordsImported = 0;
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            oDataImportHrdInfo.IsActive = true;
            oDataImportHrdInfo.DateAdded = DateTime.Now;
            oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
            oDataImportHrdInfo.LanguageID = SessionHelper.GetUserLanguage();
            oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
            string mappedKeyValues = this.hdnKeyNameMapping.Value;
            string[] mappedKeyArry = mappedKeyValues.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < mappedKeyArry.Length; i++)
            {
                GeographyStructureHdrInfo oGeoStructHdrInfo = new GeographyStructureHdrInfo();
                string pair = mappedKeyArry[i];
                string[] keyPair = pair.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);
                short geoClassId;
                if (short.TryParse(keyPair[0].ToString(), out geoClassId))
                {
                    oGeoStructHdrInfo.GeographyClassID = geoClassId;
                }
                string[] geoStructure = keyPair[1].ToString().Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                oGeoStructHdrInfo.GeographyStructure = geoStructure[1].Trim();
                oGeoStructHdrInfo.GeographyStructureLabelID = LanguageUtil.InsertPhrase(geoStructure[1].Trim(), null
                    , AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.GetUserLanguage(), 4, null);
                oGeoStructHdrInfoCollection.Add(oGeoStructHdrInfo);
            }

            if (oDataImport.InsertDataImportGLData(oDataImportHrdInfo, oGeoStructHdrInfoCollection
                , Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted), GeographyClassCompanyKey.GEOGRAPHYCLASSID, Helper.GetAppUserInfo()))
                Response.Redirect("~/Pages/DataImport.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1565");
            //Save DataIMportHdr with status as "in progress"
            //Save DataImportFailureMessage Message as "In Progress"
            //Save GeographyStructureHdr

            //GeographyClassID, GeographyStructure, GeographyStructureLabelID 
            //@IsActive, @DateAdded, @AddedBy
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
    private void BindOrganizationalHierarchyKeysNames(FileInfo f1)
    {
        DataTable dtFileSchema;
        ListItemCollection lstFieldNames = null;
        //dtFileSchema = DataImportHelper.GetSchemaDataTableForExcelFile(f1.ToString(), f1.Extension, 0);
        if (f1.Extension.ToLower() == FileExtensions.csv)
            dtFileSchema = ExcelHelper.GetDelimitedFileSchema(f1.FullName);
        else
            dtFileSchema = DataImportHelper.GetSchemaDataTableForExcelFile(f1.ToString(), f1.Extension, WebConstants.GLDATA_SHEETNAME);
        lstFieldNames = GetListItems(dtFileSchema);
        if (lstFieldNames.Count > 0)
        {
            this.lstHierarchyName.DataSource = lstFieldNames;
            this.lstHierarchyName.DataBind();
        }
    }
    private ListItemCollection GetListItems(DataTable dtExcelSchema)
    {
        ListItemCollection lstItemCollection = new ListItemCollection();
        string[] GLDataMandatoryFields = {"Period End Date", "Company", "GL Account #"
                                               , "GL Account Name", "FS Caption", "Account Type", "Is P&L"
                                               , "Base CCY Code", "Balance in Base CCY"
                                               , "Balance in Reporting CCY","Reporting CCY Code"};
        foreach (DataRow dr in dtExcelSchema.Rows)
        {
            string columnName = dr["Column_Name"].ToString().Trim();
            string result = Array.Find(GLDataMandatoryFields, s => s.ToLower().Trim().Equals(columnName.ToLower().Trim()));
            if (result == null)
            {
                ListItem item = new ListItem(columnName, columnName);
                lstItemCollection.Add(item);
            }
        }
        return lstItemCollection;
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "ImportUI";
    }
    #endregion

}
