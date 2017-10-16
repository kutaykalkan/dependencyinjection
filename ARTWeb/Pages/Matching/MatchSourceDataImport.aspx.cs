using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using System.IO;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Text;
using SkyStem.ART.Client.Data;

public partial class Pages_Matching_MatchSourceDataImport : PageBaseMatching
{
    long FileSize = 0;
    List<MatchingSourceDataImportHdrInfo> _MatchingSourceDataImportHdrInfoList = null;
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMaster = (MasterPageBase)this.Master.Master;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _MatchingSourceDataImportHdrInfoList = new List<MatchingSourceDataImportHdrInfo>();
        Helper.ShowInputRequirementSection(this, 2285, 2286, 2387, 2358, 2359, 2391, 2393);
        Helper.SetPageTitle(this, 2194);
        Helper.SetBreadcrumbs(this, 2194);
        if (!Page.IsPostBack)
        {
            BindGrid();
            this.ReturnUrl =  Helper.ReturnURL(this.Page);
        }
        GridHelper.SetRecordCount(radMatchSourceDataImport);
    }

    protected void BindGrid()
    {
        List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfo = MatchingHelper.GetMatchSource();
        radMatchSourceDataImport.DataSource = oMatchingSourceDataImportHdrInfo;
        radMatchSourceDataImport.DataBind();
    }

    protected void SetMatchingSourceType(DropDownList oDropDownList)
    {
        try
        {
            List<MatchingSourceTypeInfo> oMatchingSourceTypeInfoList = SessionHelper.GeAlltMatchingSourceType();
            IList<MatchingSourceTypeInfo> oMatchingSourceTypeInfoCollection = null;
            oMatchingSourceTypeInfoCollection = (IList<MatchingSourceTypeInfo>)Helper.DeepClone(oMatchingSourceTypeInfoList);
            this.FormMode = MatchingHelper.GetFormModeForMatching(WebEnums.ARTPages.MatchingDataImport, null, null, this.GLDataID, null);
            if (oMatchingSourceTypeInfoCollection != null)
            {
                if (this.FormMode == WebEnums.FormMode.ReadOnly)
                {
                    for (int j = 0; j < oMatchingSourceTypeInfoCollection.Count; j++)
                    {
                        MatchingSourceTypeInfo oMatchingSourceTypeInfo = oMatchingSourceTypeInfoCollection[j];
                        if (oMatchingSourceTypeInfo.MatchingSourceTypeID == (short)ARTEnums.DataImportType.GLTBS)
                        {
                            oMatchingSourceTypeInfoCollection.Remove(oMatchingSourceTypeInfo);
                            j--;
                        }
                    }
                }
                oDropDownList.DataSource = oMatchingSourceTypeInfoCollection;
                oDropDownList.DataTextField = "MatchingSourceTypeName";
                oDropDownList.DataValueField = "MatchingSourceTypeID";
                oDropDownList.DataBind();
            }
            ListControlHelper.AddListItemForSelectOne(oDropDownList);
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
    protected void radMatchSourceDataImport_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        DropDownList ddlMatchingSourceType = (DropDownList)e.Item.FindControl("ddlMatchingSourceType");
        if (ddlMatchingSourceType != null)
        { SetMatchingSourceType(ddlMatchingSourceType); }

        RadUpload RadFileUpload = (RadUpload)e.Item.FindControl("RadFileUpload");
        if (RadFileUpload != null)
        { 
            //Setting allowed file extensions.
            RadFileUpload.AllowedFileExtensions = DataImportHelper.GetAllowedFileExtensions();
            
            //Set allowed file size in bytes
            RadFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);
        }

        CustomValidator cvFileUpload = (CustomValidator)e.Item.FindControl("cvFileUpload");

        //setting error messages to be shown at runtime
        if (cvFileUpload != null)
        {
            cvFileUpload.Attributes.Add("fileNameErrorMessage", LanguageUtil.GetValue(5000035));
            cvFileUpload.Attributes.Add("fileExtensionErrorMessage", LanguageUtil.GetValue(5000036));
        }
    }

    private bool ValidateAllFileForMatchingDataImport()
    {
        bool IsValid = true;
        try
        {
            StringBuilder oSbError = new StringBuilder();
            foreach (GridDataItem oGridDataItem in radMatchSourceDataImport.MasterTableView.Items)
            {
                string exceptionMessage = "";
                DropDownList ddlMatchingSourceType = (DropDownList)oGridDataItem.FindControl("ddlMatchingSourceType");
                TextBox txtName = (TextBox)oGridDataItem.FindControl("txtName");
                RadUpload RadFileUpload = (RadUpload)oGridDataItem.FindControl("RadFileUpload");
                if (RadFileUpload.UploadedFiles.Count > 0 || RadFileUpload.InvalidFiles.Count > 0)
                {
                    //**Validate Matching Source Type
                    if (ddlMatchingSourceType.SelectedIndex == 0)
                    {
                        exceptionMessage = Helper.GetLabelIDValue(5000258);
                        throw new Exception(exceptionMessage);
                    }
                    //** End
                    //**Validate Name
                    if (txtName.Text.Trim() == "")
                    {
                        exceptionMessage = Helper.GetLabelIDValue(5000259);
                        throw new Exception(exceptionMessage);
                    }
                    //** End
                    //**Validate Invalid
                    if (RadFileUpload.InvalidFiles.Count > 0)
                    {
                        exceptionMessage = Helper.GetLabelIDValue(5000036);
                        throw new Exception(exceptionMessage);
                    }
                    //** End

                    //** End
                    //**Validate Data Storage Capacity
                    decimal? dataStorageCapacity;
                    decimal? currentUsage;
                    FileSize = FileSize + RadFileUpload.UploadedFiles[0].ContentLength;
                    DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);
                    if (((decimal)(FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                    {
                        exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                        throw new Exception(exceptionMessage);
                    }
                    //** End

                    //**Validate GLTBS All Mandatory Columns Present
                    short importType = Convert.ToInt16(ddlMatchingSourceType.SelectedValue);
                    UploadedFile validFile = RadFileUpload.UploadedFiles[0];
                    string targetFolder = MatchingHelper.GetMatchingFolderForImport(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
                    string fileName = MatchingHelper.GetMatchingFileName(validFile, SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value);
                    string filePath = Path.Combine(targetFolder, fileName);
                    validFile.SaveAs(filePath, true);
                    if (MatchingHelper.ValidateFileForMatchingDataImport(filePath, validFile.GetName(), validFile.GetExtension(), (ARTEnums.DataImportType)importType, out exceptionMessage))
                    {
                        MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = new MatchingSourceDataImportHdrInfo();
                        oMatchingSourceDataImportHdrInfo.MatchingSourceName = txtName.Text;
                        oMatchingSourceDataImportHdrInfo.FileName = validFile.GetName();
                        oMatchingSourceDataImportHdrInfo.PhysicalPath = filePath;
                        oMatchingSourceDataImportHdrInfo.FileSize = validFile.ContentLength;
                        oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID = importType;
                        oMatchingSourceDataImportHdrInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Draft;
                        oMatchingSourceDataImportHdrInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                        oMatchingSourceDataImportHdrInfo.UserID = SessionHelper.CurrentUserID;
                        oMatchingSourceDataImportHdrInfo.RoleID = SessionHelper.CurrentRoleID;
                        oMatchingSourceDataImportHdrInfo.LanguageID = SessionHelper.GetUserLanguage();
                        oMatchingSourceDataImportHdrInfo.DateAdded = DateTime.Now;
                        oMatchingSourceDataImportHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                        oMatchingSourceDataImportHdrInfo.IsActive = true;
                        _MatchingSourceDataImportHdrInfoList.Add(oMatchingSourceDataImportHdrInfo);
                    }
                    else
                    {
                     //   throw new Exception(exceptionMessage);
                        if (oSbError.ToString().Equals(string.Empty))
                            oSbError.Append(exceptionMessage);
                        else
                            oSbError.Append("<br/>" + exceptionMessage);
                    }
                    //** End
                }
            }
            if (!oSbError.ToString().Equals(string.Empty))
                throw new Exception(oSbError.ToString());
        }
        catch (Exception ex)
        {
            if (_MatchingSourceDataImportHdrInfoList != null && _MatchingSourceDataImportHdrInfoList.Count > 0)
            {
                foreach (MatchingSourceDataImportHdrInfo oItem in _MatchingSourceDataImportHdrInfoList)
                {
                    this.DeleteFile(oItem.PhysicalPath);
                }
            }
            Helper.ShowErrorMessage(this, ex);
            IsValid = false;
        }
        return IsValid;
    }


    protected void btnUploadNewFiles_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateAllFileForMatchingDataImport())
            {
                SaveMatchingSource();
            }
        }
        catch (Exception ex)
        {
            Helper.LogException(ex);
        }
    }


    private void SaveMatchingSource()
    {
        if (_MatchingSourceDataImportHdrInfoList != null && _MatchingSourceDataImportHdrInfoList.Count > 0)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfo = MatchingHelper.SaveMatchingSource(SessionHelper.CurrentCompanyID, _MatchingSourceDataImportHdrInfoList);
            if (oMatchingSourceDataImportHdrInfo.Count > 0)
            {
                Session[SessionConstants.MATCHING_SOURCE_DATA] = oMatchingSourceDataImportHdrInfo;
                //Response.Redirect("MatchSourceDataTypeMapping.aspx");
                SessionHelper.RedirectToUrl("MatchSourceDataTypeMapping.aspx");
                return;
            }
        }
    }

    private void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    public override string GetMenuKey()
    {
        return "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        short matchingTypeID = 2; // 2 is for data matching
        if (Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID] != null)        
            matchingTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]);        
        string navUrl = URLConstants.URL_MATCHING_VIEW_MATCH_SET + "?" + QueryStringConstants.MATCHING_TYPE_ID + "=" + matchingTypeID;
        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
            navUrl += "&" + QueryStringConstants.GLDATA_ID + "=" + Request.QueryString[QueryStringConstants.GLDATA_ID].ToString();
        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID ] != null)
            navUrl += "&" + QueryStringConstants.ACCOUNT_ID + "=" + Request.QueryString[QueryStringConstants.ACCOUNT_ID].ToString();
        //Response.Redirect(navUrl, true);
        SessionHelper.RedirectToUrl(navUrl);
        return;
    }
}

