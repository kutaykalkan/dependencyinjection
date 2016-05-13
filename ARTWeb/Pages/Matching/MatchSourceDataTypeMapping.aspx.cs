using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Matching;
using System.IO;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Data;

public partial class Pages_Matching_MatchSourceDataTypeMapping : PageBaseMatching
{
    List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportList = null;
    string[] arryMandatoryFields = null;
    short _MatchingSourceTypeID = 0;
    short shrtDataImportStatusID = 0;
    public short DataImportStatusID
    {
        get 
        { 
            return shrtDataImportStatusID; 
        }
        set
        {
            shrtDataImportStatusID = value;
            
        }
    }
    public short MatchingSourceTypeID
    {
        get
        {
            return _MatchingSourceTypeID;
        }
        set
        {
            _MatchingSourceTypeID = value;

        }
    }
    public bool _isErrorOnSubmit = false;
    public List<MatchingSourceDataImportHdrInfo> MatchingSourceDataImportList
    {
        get
        {
            if (Session[SessionConstants.MATCHING_SOURCE_DATA] != null)
                oMatchingSourceDataImportList = (List<MatchingSourceDataImportHdrInfo>)Session[SessionConstants.MATCHING_SOURCE_DATA];

            return oMatchingSourceDataImportList;
        
        }
    }

    public bool IsErrorOnSubmit
    {
        get
        {
            return _isErrorOnSubmit;
        }
        set
        {
            _isErrorOnSubmit = value;

        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session[SessionConstants.MATCHING_SOURCE_COLUMN] != null)
            { Session[SessionConstants.MATCHING_SOURCE_COLUMN] = null; }
            SetDataSourceNameDDL();
            BindGrid();
        }
        arryMandatoryFields = DataImportHelper.GetGLTBSDataLoadMandatoryFields();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IsErrorOnSubmit = false;
        SetPageSettings();
        oMatchingSourceDataImportList = new List<MatchingSourceDataImportHdrInfo>();

        if (Session[SessionConstants.MATCHING_SOURCE_DATA] != null)
            oMatchingSourceDataImportList = (List<MatchingSourceDataImportHdrInfo>)Session[SessionConstants.MATCHING_SOURCE_DATA];

        btnSubmitAll.Enabled = (oMatchingSourceDataImportList.Count > 1);
        if (!Page.IsPostBack)
            this.ReturnUrl = Helper.ReturnURL(this.Page);

        GridHelper.SetRecordCount(rgMappingColumns);

    }
    /// <summary>
    /// Sets the page settings.
    /// </summary>
    private void SetPageSettings()
    {
        Helper.ShowInputRequirementSection(this, 2325, 2326,2392);
        Helper.SetPageTitle(this, 2192);
        Helper.SetBreadcrumbs(this, 2192);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRoleSelection = false;
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }

    public override string GetMenuKey()
    {
        return "MatchSourceDataTypeMapping";
    }
    protected void ddlDataSourceName_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Enabled = true;
        btnSubmitAll.Enabled = true;
        btnContinueLater.Enabled = true;

        long MatchingSourceDataImportID = 0;
        long.TryParse(ddlDataSourceName.SelectedValue.ToString(), out MatchingSourceDataImportID);

        if (oMatchingSourceDataImportList.Where(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).SingleOrDefault().MatchingSourceTypeID.HasValue)
            MatchingSourceTypeID = oMatchingSourceDataImportList.Where(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).SingleOrDefault().MatchingSourceTypeID.Value;
        
        SetMatchingSourceColumn();
        BindGrid();
    }

    private void BindGrid()
    {
        try
        {
            //** Get Select MatchingSourceDataImportID from Data Source Name DropDown
            long MatchingSourceDataImportID = 0;
            long.TryParse(ddlDataSourceName.SelectedValue.ToString(), out MatchingSourceDataImportID);
            //** End

            //** Get Select MatchingSourceDataImportList from Session
            if (Session[SessionConstants.MATCHING_SOURCE_DATA] != null)
                oMatchingSourceDataImportList = (List<MatchingSourceDataImportHdrInfo>)Session[SessionConstants.MATCHING_SOURCE_DATA];
            //** End

            //** Get  and Set in DataImportStatusID 
            //** For if Data Import Status is not in Draft mode then user can not edit and submit columns data type
            if (MatchingSourceDataImportID > 0)
                short.TryParse(oMatchingSourceDataImportList.Find(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).DataImportStatusID.Value.ToString(), out shrtDataImportStatusID);
            //** End


            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoList = null;
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection = null;

            //** Get Matching Source Column List from session if exist in Session
            if (Session[SessionConstants.MATCHING_SOURCE_COLUMN] != null)
            {
                oMatchingSourceColumnInfoList = (List<MatchingSourceColumnInfo>)Session[SessionConstants.MATCHING_SOURCE_COLUMN];
                oMatchingSourceColumnInfoCollection = oMatchingSourceColumnInfoList.FindAll(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).ToList();
            }
            //** Get Matching Source Column List from session if exist in Session

            if (oMatchingSourceColumnInfoCollection != null && oMatchingSourceColumnInfoCollection.Count > 0)
            {
                rgMappingColumns.DataSource = oMatchingSourceColumnInfoCollection;
                rgMappingColumns.DataBind();
            }
            else
            {
                //** Get selected MatchingSourceDataImportHdrInfo from List
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = null;
                oMatchingSourceDataImportHdrInfo = oMatchingSourceDataImportList.Where(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).SingleOrDefault();

                string filePath = oMatchingSourceDataImportHdrInfo.PhysicalPath;
                MatchingSourceTypeID = oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID.Value;
                //** End
                //** Get Column From File
                List<MatchingSourceColumnInfo> oMatchingSourceColumnInfo_File = ReadFileColumn(filePath, MatchingSourceDataImportID);
                //**End
               
                //** Get Column From DB
                //*** IN of GLTBS and Column not in DB then get User last uploded GLTBS Column List 
                //*** for selected datatype of same column name
                MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                oMatchingParamInfo.MatchingSourceDataImportID = MatchingSourceDataImportID;
                oMatchingSourceColumnInfoList = MatchingHelper.GetMatchingSourceColumn(oMatchingParamInfo);
                //**End

                if (oMatchingSourceColumnInfoList != null && oMatchingSourceColumnInfoList.Count > 0)
                {
                    if (oMatchingSourceColumnInfoList[0].MatchingSourceDataImportID.HasValue && oMatchingSourceColumnInfoList[0].MatchingSourceDataImportID == MatchingSourceDataImportID)
                    {
                        //*** Update Datatype and there MatchingSourceColumnID in case of column saved 
                        if (oMatchingSourceColumnInfoList.Count < oMatchingSourceColumnInfo_File.Count)
                        {
                          
                            foreach (MatchingSourceColumnInfo oMSCCol in oMatchingSourceColumnInfoList)
                            {

                                int Index = oMatchingSourceColumnInfo_File.FindIndex(p => p.ColumnName == oMSCCol.ColumnName && p.MatchingSourceDataImportID == oMSCCol.MatchingSourceDataImportID);
                                if (Index >= 0)
                                {
                                    oMatchingSourceColumnInfo_File[Index].MatchingSourceColumnID = oMSCCol.MatchingSourceColumnID;
                                    oMatchingSourceColumnInfo_File[Index].DataTypeID = oMSCCol.DataTypeID;
                                }
                            }
                            UpdateColumnSession(oMatchingSourceColumnInfo_File, MatchingSourceDataImportID);
                        }
                        else
                        {
                            UpdateColumnSession(oMatchingSourceColumnInfoList, MatchingSourceDataImportID);
                            oMatchingSourceColumnInfo_File = oMatchingSourceColumnInfoList;
                        }
                        //*** End
                    }
                    else
                    {
                        //*** Update Datatype in User last uploded GLTBS Column for same Column name
                        foreach (MatchingSourceColumnInfo oMSCCol in oMatchingSourceColumnInfoList)
                        {
                            int Index = oMatchingSourceColumnInfo_File.FindIndex(p => p.ColumnName == oMSCCol.ColumnName);
                            if (Index >= 0)
                                oMatchingSourceColumnInfo_File[Index].DataTypeID = oMSCCol.DataTypeID;
                        }
                        UpdateColumnSession(oMatchingSourceColumnInfo_File, MatchingSourceDataImportID);
                        //*** End
                    }
                }
                else
                    UpdateColumnSession(oMatchingSourceColumnInfo_File, MatchingSourceDataImportID);

                //** Get Matching Source Column 
                //oMatchingSourceColumnInfo_File.Except(oMatchingSourceColumnInfo_File.FindAll(p => arryMandatoryFields.Contains(p.ColumnName))).ToList();
                rgMappingColumns.DataSource = oMatchingSourceColumnInfo_File;
                rgMappingColumns.DataBind();
            }
            //DisableButton();
            
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

    private void DisableButton()
    {
        btnSubmit.Enabled = true;
        btnSubmitAll.Enabled = true;
        btnContinueLater.Enabled = true;

        if (DataImportStatusID != (short)WebEnums.DataImportStatus.Draft 
            && DataImportStatusID != (short)WebEnums.DataImportStatus.Failure
            && MatchingSourceDataImportList.Count == 1 && !IsErrorOnSubmit)
        {
            btnSubmit.Enabled = false;
            btnSubmitAll.Enabled = false;
            btnContinueLater.Enabled = false;
            DisableGridControl();
        }
        else if (DataImportStatusID != (short)WebEnums.DataImportStatus.Draft 
            && DataImportStatusID != (short)WebEnums.DataImportStatus.Failure
            && MatchingSourceDataImportList.Count > 1 && !IsErrorOnSubmit)
        {
            btnSubmit.Enabled = false;
            DisableGridControl();
        }
    }
    private void DisableGridControl()
    {
        foreach (GridDataItem oGridDataItem in rgMappingColumns.MasterTableView.Items)
        {
            DropDownList ddlDataType = (DropDownList)oGridDataItem.FindControl("ddlDataType");
            if (ddlDataType != null)
                ddlDataType.Enabled = false;
        }
    }

    private List<MatchingSourceColumnInfo> ReadFileColumn(string PhysicalPath, long MatchingSourceDataImportID)
    {
        List<MatchingSourceColumnInfo> oMatchingSourceColumnInfo = null;
        if (File.Exists(PhysicalPath))
        {
            DataTable dtFileColumn;
            dtFileColumn = DataImportHelper.GetSchemaDataTableForExcelFile(PhysicalPath, Path.GetExtension(PhysicalPath), WebConstants.MATCHING_SHEETNAME, false);
            if (dtFileColumn != null)
            {
                if (dtFileColumn.Rows.Count > 0)
                {
                    oMatchingSourceColumnInfo = MatchingSourceColumnInfoMapping(dtFileColumn, MatchingSourceDataImportID);
                    rgMappingColumns.DataSource = oMatchingSourceColumnInfo;
                    rgMappingColumns.DataBind();
                }
            }
        }
        else
        {
            string exceptionMessage = Helper.GetLabelIDValue(5000206);
            throw new Exception(exceptionMessage);
        }
        return oMatchingSourceColumnInfo;
    }

    private void UpdateColumnSession(List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoList, long MatchingSourceDataImportID)
    {
        try
        {

            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfo = null;
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection = null;

            if (Session[SessionConstants.MATCHING_SOURCE_COLUMN] != null)
            {
                oMatchingSourceColumnInfo = (List<MatchingSourceColumnInfo>)Session[SessionConstants.MATCHING_SOURCE_COLUMN];
            }
            else
            {
                Session[SessionConstants.MATCHING_SOURCE_COLUMN] = oMatchingSourceColumnInfoList;
                return;
            }
            if (oMatchingSourceColumnInfo != null && oMatchingSourceColumnInfo.Count > 0)
                oMatchingSourceColumnInfoCollection = oMatchingSourceColumnInfo.FindAll(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).ToList();

            if (oMatchingSourceColumnInfoCollection.Count == 0)
            {
                foreach (MatchingSourceColumnInfo oMSC in oMatchingSourceColumnInfoList)
                {
                    oMatchingSourceColumnInfo.Add(oMSC);
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex.Message);
        }
    }

    protected List<MatchingSourceColumnInfo> MatchingSourceColumnInfoMapping(DataTable oDataTable, Int64 MatchingSourceDataImportID)
    {
        List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection = new List<MatchingSourceColumnInfo>();
        try
        {
            DataColumn dataColumn = new DataColumn("GridColumnID", Type.GetType("System.Int32"));
            oDataTable.Columns.Add(dataColumn);
            short index = 1;
            foreach (DataRow dr in oDataTable.Rows)
            {
                MatchingSourceColumnInfo oMatchingSourceColumnInfo = new MatchingSourceColumnInfo();
                oMatchingSourceColumnInfo.MatchingSourceColumnID = 0;
                oMatchingSourceColumnInfo.DataTypeID = 0;
                oMatchingSourceColumnInfo.ColumnName = Helper.GetDisplayStringValue(dr["COLUMN_NAME"].ToString());
                oMatchingSourceColumnInfo.MatchingSourceDataImportID = MatchingSourceDataImportID;
                oMatchingSourceColumnInfoCollection.Add(oMatchingSourceColumnInfo);
                oMatchingSourceColumnInfo.GridColumnID = index;
                index++;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex.Message);
        }
        return oMatchingSourceColumnInfoCollection;
    }

    private void SetMatchingSourceColumn()
    {
        List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoList = null;
        if (Session[SessionConstants.MATCHING_SOURCE_COLUMN] != null)
            oMatchingSourceColumnInfoList = (List<MatchingSourceColumnInfo>)Session[SessionConstants.MATCHING_SOURCE_COLUMN];

        if (oMatchingSourceColumnInfoList != null && oMatchingSourceColumnInfoList.Count > 0)
        {
            foreach (GridDataItem oGridDataItem in rgMappingColumns.MasterTableView.Items)
            {
                string MatchingSourceColumnID = oGridDataItem["ID"].Text;
                string GridColumnID = oGridDataItem["GridColumnID"].Text;
                string MatchingSourceDataImportID = oGridDataItem["MatchingSourceDataImportID"].Text;
                DropDownList ddlDataType = (DropDownList)oGridDataItem.FindControl("ddlDataType");

                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = oMatchingSourceDataImportList.Where(p => p.MatchingSourceDataImportID == Convert.ToInt64(MatchingSourceDataImportID)).SingleOrDefault();
                if (MatchingSourceColumnID != "0" && MatchingSourceColumnID != "" && MatchingSourceColumnID != "-")
                {
                    int _Index = oMatchingSourceColumnInfoList.FindIndex(p => p.MatchingSourceColumnID == Convert.ToInt64(MatchingSourceColumnID));
                    //if (Convert.ToInt16(ddlDataType.SelectedValue) == 1)
                    //    oMatchingSourceColumnInfoList[_Index].DataTypeID = 4;
                    //else
                    oMatchingSourceColumnInfoList[_Index].DataTypeID = Convert.ToInt16(ddlDataType.SelectedValue);
                }
                else
                {
                    int _Index = oMatchingSourceColumnInfoList.FindIndex(p => p.GridColumnID == Convert.ToInt16(GridColumnID) && p.MatchingSourceDataImportID == Convert.ToInt64(MatchingSourceDataImportID));
                    //if (Convert.ToInt16(ddlDataType.SelectedValue) == 1)
                    //    oMatchingSourceColumnInfoList[_Index].DataTypeID = 4;
                    //else
                    oMatchingSourceColumnInfoList[_Index].DataTypeID = Convert.ToInt16(ddlDataType.SelectedValue);
                }
            }
        }
    }
    
    protected void rgMappingColumns_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                long MatchingSourceDataImportID = 0;
                long.TryParse(ddlDataSourceName.SelectedValue.ToString(), out MatchingSourceDataImportID);
                arryMandatoryFields = DataImportHelper.GetGLTBSDataLoadMandatoryFields();
                MatchingSourceColumnInfo oMatchingSourceColumnInfo = (MatchingSourceColumnInfo)e.Item.DataItem;
                DropDownList ddlDataType = (DropDownList)e.Item.FindControl("ddlDataType");
                
                if (ddlDataType != null)
                {
                    SetDataTypeDDL(ddlDataType);
                    if (MatchingSourceTypeID == (short)ARTEnums.DataImportType.GLTBS &&
                        arryMandatoryFields != null &&
                        arryMandatoryFields.Contains(Server.HtmlDecode(oMatchingSourceColumnInfo.ColumnName.Trim())))
                    {
                        ddlDataType.SelectedValue = "2";
                        ddlDataType.Enabled = false;
                    }
                    if (oMatchingSourceColumnInfo.DataTypeID != 0)
                    {
                        ddlDataType.SelectedValue = Helper.GetDisplayIntegerValue(oMatchingSourceColumnInfo.DataTypeID);
                    }
                    //if (this.DataImportStatusID != (short)WebEnums.DataImportStatus.Draft 
                    //    && this.DataImportStatusID != (short)WebEnums.DataImportStatus.Failure)
                    //    ddlDataType.Enabled = false;
                }

                ExLabel lblColumnName = (ExLabel)e.Item.FindControl("lblColumnName");
                if (lblColumnName != null)
                {
                    lblColumnName.Text = oMatchingSourceColumnInfo.ColumnName;
                }

                if ((e.Item as GridDataItem)["ID"] != null)
                {
                    (e.Item as GridDataItem)["ID"].Text = Helper.GetDisplayStringValue(oMatchingSourceColumnInfo.MatchingSourceColumnID.Value.ToString());
                }

                if ((e.Item as GridDataItem)["GridColumnID"] != null)
                {
                    if (oMatchingSourceColumnInfo.GridColumnID != null)
                        (e.Item as GridDataItem)["GridColumnID"].Text = Helper.GetDisplayStringValue(oMatchingSourceColumnInfo.GridColumnID.Value.ToString());
                    else
                        (e.Item as GridDataItem)["GridColumnID"].Text = "0";
                }

                if ((e.Item as GridDataItem)["MatchingSourceDataImportID"] != null)
                {
                    (e.Item as GridDataItem)["MatchingSourceDataImportID"].Text = Helper.GetDisplayStringValue(oMatchingSourceColumnInfo.MatchingSourceDataImportID.Value.ToString());
                }
                
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    private MatchingParamInfo GetParmaObjectForSave(short ButtonType)
    {
        MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
        List<Int64> oIDCollection = null;
        List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection = null;
        List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoList = null;
        SetMatchingSourceColumn();

        long MatchingSourceDataImportID;
        if (Session[SessionConstants.MATCHING_SOURCE_COLUMN] != null)
            oMatchingSourceColumnInfoCollection = (List<MatchingSourceColumnInfo>)Session[SessionConstants.MATCHING_SOURCE_COLUMN];

        if (oMatchingSourceColumnInfoCollection != null && oMatchingSourceColumnInfoCollection.Count > 0)
            oMatchingSourceColumnInfoList = oMatchingSourceColumnInfoCollection.Where(p => p.DataTypeID > 0).ToList();

        switch (ButtonType)
        {
            case 1: // Submit All
                oIDCollection = new List<Int64>();
                if (oMatchingSourceColumnInfoList != null && oMatchingSourceColumnInfoList.Count > 0)
                {
                    for (int j = 0; j < ddlDataSourceName.Items.Count; j++)
                    {
                        MatchingSourceDataImportID = Convert.ToInt64(ddlDataSourceName.Items[j].Value);
                        //short shrtDataImportStatusID = 0;
                        short.TryParse(oMatchingSourceDataImportList.Find(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).DataImportStatusID.Value.ToString(), out shrtDataImportStatusID);
                        if (shrtDataImportStatusID == (short)WebEnums.DataImportStatus.Draft)
                        {
                            int count = oMatchingSourceColumnInfoList.Where(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).ToList().Count;
                            if (count > 1)
                                oIDCollection.Add(MatchingSourceDataImportID);
                            else
                            {
                                string exceptionMessage = Helper.GetLabelIDValue(5000282);
                                IsErrorOnSubmit = true;
                                throw new Exception(exceptionMessage);
                            }
                        }
                    }
                    oMatchingParamInfo.oMatchingSourceColumnInfoList = oMatchingSourceColumnInfoList;
                    oMatchingParamInfo.IDList = oIDCollection;
                    oMatchingParamInfo.IsSubmited = true;
                    oMatchingParamInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Submitted;
                }
                else
                {
                    string exceptionMessage = Helper.GetLabelIDValue(5000282);
                    IsErrorOnSubmit = true;
                    throw new Exception(exceptionMessage);
                }

                break;
            case 2: // Submit
                oIDCollection = new List<Int64>();

                if (Request.QueryString[QueryStringConstants.DATA_IMPORT_ID] != null)
                    MatchingSourceDataImportID = Convert.ToInt64(Request.QueryString[QueryStringConstants.DATA_IMPORT_ID]);
                else
                    MatchingSourceDataImportID = Convert.ToInt64(ddlDataSourceName.SelectedValue);

                if (oMatchingSourceColumnInfoList != null && oMatchingSourceColumnInfoList.Count > 0)
                {
                    int count = oMatchingSourceColumnInfoList.Where(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).ToList().Count;
                    if (count > 1)
                        oIDCollection.Add(MatchingSourceDataImportID);
                    else
                    {
                        string exceptionMessage = Helper.GetLabelIDValue(5000282);
                        IsErrorOnSubmit = true;
                        throw new Exception(exceptionMessage);
                    }
                    oMatchingParamInfo.oMatchingSourceColumnInfoList = oMatchingSourceColumnInfoList;
                    oMatchingParamInfo.IDList = oIDCollection;
                    oMatchingParamInfo.IsSubmited = true;
                    oMatchingParamInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Submitted;
                }
                else
                {
                    string exceptionMessage = Helper.GetLabelIDValue(5000282);
                    IsErrorOnSubmit = true;
                    throw new Exception(exceptionMessage);
                }
                break;
            case 3: // Continue Later
                if (oMatchingSourceColumnInfoList != null && oMatchingSourceColumnInfoList.Count > 0)
                    oMatchingParamInfo.oMatchingSourceColumnInfoList = oMatchingSourceColumnInfoList;

                long.TryParse(ddlDataSourceName.SelectedValue.ToString(), out MatchingSourceDataImportID);

                if (MatchingSourceDataImportID > 0)
                    short.TryParse(oMatchingSourceDataImportList.Find(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID).DataImportStatusID.Value.ToString(), out shrtDataImportStatusID);

                oMatchingParamInfo.IsSubmited = false;
                break;
        }

        return oMatchingParamInfo;
    }

    protected void btnSubmitAll_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (SaveMatchingSourceColumn(GetParmaObjectForSave(1)))
                Response.Redirect(URLConstants.URL_MATCHING_SOURCE_DATAIMPORT_STATUS);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex.Message);
        }
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (SaveMatchingSourceColumn(GetParmaObjectForSave(2)))
            {
                if (MatchingSourceDataImportList != null)
                {
                    long MatchingSourceDataImportID = Convert.ToInt64(ddlDataSourceName.SelectedValue);
                    MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = MatchingSourceDataImportList.Find(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID);
                    if (oMatchingSourceDataImportHdrInfo != null)
                        oMatchingSourceDataImportHdrInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Submitted;

                    btnSubmit.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex.Message);
        }
    }

    protected void btnStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect(URLConstants.URL_MATCHING_SOURCE_DATAIMPORT_STATUS);
    }

    protected void btnContinueLater_OnClick(object sender, EventArgs e)
    {
        try
        {
            SaveMatchingSourceColumn(GetParmaObjectForSave(3));
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex.Message);
        }
    }

    protected bool SaveMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo)
    {
        bool result=false;
        try
        {
            result = MatchingHelper.SaveMatchingSourceColumn(oMatchingParamInfo);
            if (result)
            {
                if (!oMatchingParamInfo.IsSubmited.Value)
                    Helper.ShowConfirmationMessage(this, Helper.GetLabelIDValue(2210));
                else
                    Helper.ShowConfirmationMessage(this, Helper.GetLabelIDValue(2337));
            }
            else
                Helper.ShowErrorMessage(this, Helper.GetLabelIDValue(5000045));
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex.Message);
        }
        return result;
    }
    protected void SetDataSourceNameDDL()
    {
        try
        {
            if (Session[SessionConstants.MATCHING_SOURCE_DATA] != null)
                oMatchingSourceDataImportList = (List<MatchingSourceDataImportHdrInfo>)Session[SessionConstants.MATCHING_SOURCE_DATA];

            ddlDataSourceName.DataSource = oMatchingSourceDataImportList;
            ddlDataSourceName.DataTextField = "MatchingSourceName";
            ddlDataSourceName.DataValueField = "MatchingSourceDataImportID";
            ddlDataSourceName.DataBind();

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
    protected void SetDataTypeDDL(DropDownList ddlDataType)
    {
        try
        {
            List<DataTypeMstInfo> oDataTypeMstInfoCollection = null;
            oDataTypeMstInfoCollection = SessionHelper.GeAllDataType();
            ddlDataType.DataSource = oDataTypeMstInfoCollection;
            ddlDataType.DataTextField = "DataTypeName";
            ddlDataType.DataValueField = "DataTypeID";
            ddlDataType.DataBind();
            ListControlHelper.AddListItemForSelectOne(ddlDataType);
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl,true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(URLConstants.URL_MATCHING_VIEW_MATCH_SET, true);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        DisableButton();
    }
}
