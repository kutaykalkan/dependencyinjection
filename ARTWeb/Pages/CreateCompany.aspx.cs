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
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.IO;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;

public partial class CreateCompany : PageBase
{
    #region Variables & Constants
    static int ContactInfoID = 0;
    static int? DisplayNameLblID = null;
    bool IsPackageUpdated = false;
    #endregion

    #region Properties
    public CompanyHdrInfo CurrentCompanyHdrInfo
    {
        get
        {
            CompanyHdrInfo oCompanyHdrInfo = null;
            if (ViewState["CurrentCompanyHdrInfo"] != null)
                oCompanyHdrInfo = (CompanyHdrInfo)ViewState["CurrentCompanyHdrInfo"];
            return oCompanyHdrInfo;
        }
        set { ViewState["CurrentCompanyHdrInfo"] = value; }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += ompage_ReconciliationPeriodChangedEventHandler;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
        {
            Helper.SetPageTitle(this, 1423);
        }
        else
        {
            Helper.SetPageTitle(this, 1284);
        }

        if (!Page.IsPostBack)
        {
            OnPageLoad();
        }

        Page.SetFocus(txtName);
        imgWebsite.Attributes.Add("onclick", "CheckWebsite(" + "'" + txtWebSite.ClientID + "'" + ")");

        txtNumOfDays.Attributes.Add("onBlur ", "javascript:calDate(" + "'" + txtNumOfDays.ClientID + "/" + calSubscriptionEndDate.ClientID + "/" + calSubscriptionStartDate.ClientID + "'" + ")");
        calSubscriptionEndDate.JSCallbackFunction = "CallbackFromCalendar();";
        calSubscriptionEndDate.Attributes.Add("onBlur ", "javascript:calNoDays('" + txtNumOfDays.ClientID + "', '" + calSubscriptionEndDate.ClientID + "', '" + calSubscriptionStartDate.ClientID + "')");

        calSubscriptionStartDate.JSCallbackFunction = "CallbackFromCalendar();";
        calSubscriptionStartDate.Attributes.Add("onBlur ", "javascript:calNoDays('" + txtNumOfDays.ClientID + "', '" + calSubscriptionEndDate.ClientID + "', '" + calSubscriptionStartDate.ClientID + "')");

        // Based on Role
        if (SessionHelper.CurrentRoleID != CacheHelper.GetRoleID(ARTConstants.ROLE_TEXT_SKYSTEM_ADMIN))
        {
            pnlConfiguration.Enabled = false;
            optNo.Enabled = false;
            optYes.Enabled = false;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
            Helper.SetBreadcrumbs(this, 1207, 1423);
    }

    #endregion

    #region Grid Events
    #endregion

    #region Other Events

    void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            OnPageLoad();
        }
        catch (ARTException oARTException)
        {
            Helper.ShowErrorMessage(this, oARTException);
        }
        catch (Exception ex)
        {
            Helper.LogException(ex);
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string url = "";
            CompanyHdrInfo oCompanyHdrInfo = new CompanyHdrInfo();
            string tempFolderPath = CreateAndGetFolderPath();
            oCompanyHdrInfo.CompanyName = txtName.Text;
            oCompanyHdrInfo.WebSite = txtWebSite.Text;
            oCompanyHdrInfo.DisplayName = txtDisplayName.Text;
            oCompanyHdrInfo.CompanyAlias = txtCompanyAlias.Text.Trim();
            oCompanyHdrInfo.Address.Address1 = txtAddress.Text;
            oCompanyHdrInfo.Address.City = txtCity.Text;
            oCompanyHdrInfo.Address.State = txtState.Text;
            oCompanyHdrInfo.Address.Zip = txtZip.Text;
            oCompanyHdrInfo.Address.Country = txtCountry.Text;
            oCompanyHdrInfo.SubscriptionStartDate = Convert.ToDateTime(calSubscriptionStartDate.Text);
            oCompanyHdrInfo.SubscriptionEndDate = Convert.ToDateTime(calSubscriptionEndDate.Text);
            oCompanyHdrInfo.PackageID = Convert.ToInt16(ddlPackage.SelectedValue);
            oCompanyHdrInfo.ShowLogoOnMasterPage = chkShowLogo.Checked;
            int noOfLicensedUsers = Convert.ToInt32(txtLicencedUser.Text);
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            if (noOfLicensedUsers > 0)
            {
                oCompanyHdrInfo.NoOfLicensedUsers = noOfLicensedUsers;
            }
            else
            {
                oMasterPageBase.ShowErrorMessage(5000018);
            }
            int NoOfSubscriptionDays = Convert.ToInt32(txtNumOfDays.Text);
            if (NoOfSubscriptionDays > 0)
            {
                oCompanyHdrInfo.NoOfSubscriptionDays = NoOfSubscriptionDays;
            }
            else
            {
                oMasterPageBase.ShowErrorMessage(5000142);
            }
            Decimal dataStorageCapacity = Convert.ToDecimal(txtDataStorage.Text);
            if (dataStorageCapacity > 0)
            {
                oCompanyHdrInfo.DataStorageCapacity = dataStorageCapacity;
            }
            else
            {
                oMasterPageBase.ShowErrorMessage(5000019);
            }

            if (optYes.Checked)
                oCompanyHdrInfo.IsActive = true;
            else
                oCompanyHdrInfo.IsActive = false;

            if (optSeparateDatabaseYes.Checked)
                oCompanyHdrInfo.IsSeparateDatabase = true;
            else
                oCompanyHdrInfo.IsSeparateDatabase = false;

            if (optEnableFTPYes.Checked)
                oCompanyHdrInfo.IsFTPEnabled = true;
            else if (optEnableFTPNo.Checked)
                oCompanyHdrInfo.IsFTPEnabled = false;

            ContactInfo objContactInfo = new ContactInfo();
            objContactInfo.Name = txtContactName.Text;
            objContactInfo.Email = txtEmail.Text;
            objContactInfo.Phone = txtTelephone.Text;
            objContactInfo.IsActive = true;

            UploadedFile validFile = null;
            string targetFolder = string.Empty;
            string filePath = string.Empty;
            string fileName = string.Empty;
            if (RadFileUpload.UploadedFiles.Count > 0)
            {
                try
                {
                    validFile = RadFileUpload.UploadedFiles[0];
                    fileName = Helper.GetFileName(validFile);
                    oCompanyHdrInfo.LogoFileName = fileName;

                }
                finally
                {
                    this.RadFileUpload.UploadedFiles.Clear();
                }
            }

            if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
            {
                // EDIT Mode
                try
                {
                    oCompanyHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                    oCompanyHdrInfo.DateRevised = DateTime.Now;
                    objContactInfo.DateRevised = DateTime.Now;
                    objContactInfo.RevisedBy = oCompanyHdrInfo.RevisedBy;

                    int Cmp_id = 0;
                    Cmp_id = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);
                    oCompanyHdrInfo.CompanyID = Cmp_id;
                    oCompanyHdrInfo.DisplayNameLabelID = LanguageUtil.InsertPhrase(oCompanyHdrInfo.DisplayName, null, AppSettingHelper.GetApplicationID(), oCompanyHdrInfo.CompanyID.Value, SessionHelper.GetUserLanguage(), 4, DisplayNameLblID);

                    targetFolder =SharedDataImportHelper.GetFolderForImport(Cmp_id, (short)ARTEnums.DataImportType.CompanyLogo);
                    //save file
                    if (fileName != string.Empty)
                    {
                        filePath = Path.Combine(targetFolder, fileName);
                        validFile.SaveAs(filePath, true);
                        oCompanyHdrInfo.LogoPhysicalPath = filePath;
                    }
                    else
                    {
                        oCompanyHdrInfo.LogoFileName = CurrentCompanyHdrInfo.LogoFileName;
                        oCompanyHdrInfo.LogoPhysicalPath = CurrentCompanyHdrInfo.LogoPhysicalPath;
                    }
                    objContactInfo.ContactID = ContactInfoID;
                    objContactInfo.CompanyID = Cmp_id;

                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();

                    CompanyHdrInfo oTempCompanyHdrInfo = oCompanyClient.GetCompanyDetail(oCompanyHdrInfo.CompanyID, null, Helper.GetAppUserInfo());

                    if (oTempCompanyHdrInfo.PackageID != Convert.ToInt16(ddlPackage.SelectedValue))
                    {
                        IsPackageUpdated = true;
                    }
                    else
                    {
                        IsPackageUpdated = false;
                    }

                    ClearSessionAndCacheData(oCompanyHdrInfo);

                    AppUserInfo oAppUserInfo = Helper.GetAppUserInfo();
                    oAppUserInfo.CompanyID = oCompanyHdrInfo.CompanyID;
                    oAppUserInfo.IsDatabaseExists = Helper.IsCompanyDatabaseExists(oCompanyHdrInfo.CompanyID);
                    bool companyupdatesucess = oCompanyClient.UpdateCompany(oCompanyHdrInfo, objContactInfo, SessionHelper.GetUserLanguage(), IsPackageUpdated, oAppUserInfo);
                    // Reload Companies
                    oMasterPageBase.ReloadCompanies(oCompanyHdrInfo.CompanyID);
                    // Redirect to appropriate Page
                    if (Request.QueryString[QueryStringConstants.FROM_PAGE] != null)
                    {
                        WebEnums.ARTPages ePages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), Request.QueryString[QueryStringConstants.FROM_PAGE].ToString());

                        switch (ePages)
                        {
                            case WebEnums.ARTPages.Home:
                                url = "~/Pages/Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1539";
                                break;
                            case WebEnums.ARTPages.CompanyList:
                                url = "~/Pages/CompanyList.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1539"
                                        + "&" + QueryStringConstants.SHOW_SEARCH_RESULTS + "=1";
                                break;
                        }
                        Response.Redirect(Page.ResolveUrl(url), false);
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
            else
            {
                // ADD Mode
                try
                {
                    //save file to temp Folder 
                    if (fileName != string.Empty)
                    {
                        validFile.SaveAs(Path.Combine(tempFolderPath, fileName), true);
                    }
                    oCompanyHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                    oCompanyHdrInfo.DateAdded = DateTime.Now;
                    objContactInfo.DateAdded = DateTime.Now;
                    objContactInfo.AddedBy = oCompanyHdrInfo.AddedBy;
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    bool companycreatedsucess = oCompanyClient.CreateNewCompany(oCompanyHdrInfo, objContactInfo, SessionHelper.GetUserLanguage(), Helper.GetAppUserInfo());

                    if (fileName != string.Empty)
                    {
                        targetFolder = SharedDataImportHelper.GetFolderForImport(oCompanyHdrInfo.CompanyID.Value, (short)ARTEnums.DataImportType.CompanyLogo);
                        //Move file to TargetFolder
                        filePath = Path.Combine(targetFolder, fileName);
                        MoveFileFromTempToTargetFolder(fileName, tempFolderPath, filePath);
                        //validFile.SaveAs(filePath, true);
                        oCompanyHdrInfo.LogoPhysicalPath = filePath;
                        //update LogoPhysicalPath  
                        oCompanyClient.UpdateCompanyLogoPhysicalPath(oCompanyHdrInfo, Helper.GetAppUserInfo());
                    }
                    // Reload Companies
                    oMasterPageBase.ReloadCompanies(oCompanyHdrInfo.CompanyID);
                    // Redirect to Create User Page
                    Response.Redirect("~/Pages/CreateUser.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1922&" + QueryStringConstants.FROMPAGE + "=" + (int)WebEnums.ARTPages.CreateCompany);
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
        }
    }

    protected void ddlPackagea_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int32 selectedPackageId = Convert.ToInt32(ddlPackage.SelectedValue);
        PackageMstInfo oPackageMstInfo = Helper.GetPackageMstInfoByPackageId(selectedPackageId);
        if (oPackageMstInfo != null)
        {
            txtDataStorage.Text = Convert.ToString(oPackageMstInfo.DefaultDiskSpace);
            txtLicencedUser.Text = Convert.ToString(oPackageMstInfo.DefaultNumberOfUsers);
        }
        else
        {
            txtDataStorage.Text = string.Empty;
            txtLicencedUser.Text = string.Empty;
        }

        string packagePopupUrl = "~/Pages/PackageFeatureList.aspx?" + QueryStringConstants.POSTBACK_PACKAGEID + "=" + ddlPackage.SelectedValue.ToString() + "&" + QueryStringConstants.POSTBACK_ISCUSTOMIZED_PACKAGE + "=-2";
        hlPackageMatrix.Attributes.Add("onclick", "javascript:return OpenRadWindow('" + Page.ResolveClientUrl(packagePopupUrl) + "', " + 500 + " , " + 620 + ");");
    }

    #endregion

    #region Validation Control Events
    protected void cvCompanyLogo_OnServerValidate(object source, ServerValidateEventArgs e)
    {
        CompanyHdrInfo oCompanyHdrInfo = null;

        if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
        {
            oCompanyHdrInfo = CurrentCompanyHdrInfo; //SessionHelper.GetCurrentCompanyHdrInfo();

            if (oCompanyHdrInfo != null)
            {
                if (chkShowLogo.Checked)
                {
                    if (oCompanyHdrInfo.LogoPhysicalPath == null || oCompanyHdrInfo.LogoPhysicalPath == string.Empty)
                    {
                        if (RadFileUpload.UploadedFiles.Count > 0)
                            e.IsValid = true;
                        else
                            e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
                }
            }

        }
        else
        {
            if (chkShowLogo.Checked)
            {
                if (RadFileUpload.UploadedFiles.Count > 0)
                    e.IsValid = true;
                else
                    e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
    }
    #endregion

    #region Private Methods

    private void OnPageLoad()
    {
        string packagePopupUrl;
        Helper.ShowInputRequirementSection(this, 1202, 1234);
        btnCancel.PostBackUrl = Helper.GetHomePageUrl();
        txtDataStorage.Text = AppSettingHelper.GetAppSettingValue("defaultStorageCapacity");
        SetErrorMessages();
        ListControlHelper.BindPackageDropdown(ddlPackage);
        CurrentCompanyHdrInfo = null;
        DateTime? periodEndDate = null;
        // Check for Edit Company
        if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
        {
            int companyID = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);

            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            if (companyID == SessionHelper.CurrentCompanyID.GetValueOrDefault())
                periodEndDate = SessionHelper.CurrentReconciliationPeriodEndDate;

            CompanyHdrInfo oCompanyHdrInfo = oCompanyClient.GetCompanyDetail(companyID, periodEndDate, Helper.GetAppUserInfo());
            SelectPackageDropdownItem(Convert.ToInt16(oCompanyHdrInfo.PackageID));
            //
            packagePopupUrl = "~/Pages/PackageFeatureList.aspx?" + QueryStringConstants.POSTBACK_PACKAGEID + "=" + ddlPackage.SelectedValue.ToString() + "&" + QueryStringConstants.POSTBACK_ISCUSTOMIZED_PACKAGE + "=" + oCompanyHdrInfo.IsCustomizedPackage.ToString();
            hlPackageMatrix.Attributes.Add("onclick", "javascript:return OpenRadWindow('" + Page.ResolveClientUrl(packagePopupUrl) + "', " + 500 + " , " + 620 + ");");

            //
            DisplayNameLblID = oCompanyHdrInfo.DisplayNameLabelID;
            txtName.Text = oCompanyHdrInfo.CompanyName;
            txtDisplayName.Text = oCompanyHdrInfo.DisplayName;
            txtCompanyAlias.Text = oCompanyHdrInfo.CompanyAlias;
            txtWebSite.Text = oCompanyHdrInfo.WebSite;
            txtAddress.Text = oCompanyHdrInfo.Address.Address1;
            txtCity.Text = oCompanyHdrInfo.Address.City;
            txtState.Text = oCompanyHdrInfo.Address.State;
            txtCountry.Text = oCompanyHdrInfo.Address.Country;
            txtZip.Text = oCompanyHdrInfo.Address.Zip;

            optYes.Checked = oCompanyHdrInfo.IsActive.Value;
            optNo.Checked = !oCompanyHdrInfo.IsActive.Value;

            optSeparateDatabaseYes.Checked = oCompanyHdrInfo.IsSeparateDatabase.Value;
            optSeparateDatabaseNo.Checked = !oCompanyHdrInfo.IsSeparateDatabase.Value;

            calSubscriptionStartDate.Text = Helper.GetDisplayDate(oCompanyHdrInfo.SubscriptionStartDate);
            calSubscriptionEndDate.Text = Helper.GetDisplayDate(oCompanyHdrInfo.SubscriptionEndDate);
            if (oCompanyHdrInfo.NoOfSubscriptionDays.HasValue)
                txtNumOfDays.Text = oCompanyHdrInfo.NoOfSubscriptionDays.Value.ToString();
            txtDataStorage.Text = Helper.GetDecimalValueForTextBox(oCompanyHdrInfo.DataStorageCapacity.Value, 2);
            if (oCompanyHdrInfo.NoOfLicensedUsers.HasValue)
                txtLicencedUser.Text = oCompanyHdrInfo.NoOfLicensedUsers.ToString();


            ContactInfo objContactInfo = oCompanyClient.GetContactInfo(companyID, Helper.GetAppUserInfo());
            ContactInfoID = objContactInfo.ContactID.Value;
            txtContactName.Text = objContactInfo.Name;
            txtEmail.Text = objContactInfo.Email;
            txtTelephone.Text = objContactInfo.Phone;
            chkShowLogo.Checked = Convert.ToBoolean(oCompanyHdrInfo.ShowLogoOnMasterPage);

            if (oCompanyHdrInfo.IsFTPEnabled.HasValue)
            {
                optEnableFTPYes.Checked = oCompanyHdrInfo.IsFTPEnabled.Value;
                optEnableFTPNo.Checked = !oCompanyHdrInfo.IsFTPEnabled.Value;
            }

            optSeparateDatabaseYes.Enabled = false;
            optSeparateDatabaseNo.Enabled = false;
            //txtDataStorage.Text = oCompanyHdrInfo.d
            //trUseSeparateDatabase.Visible = false;
            CurrentCompanyHdrInfo = oCompanyHdrInfo;
        }
        else
        {
            packagePopupUrl = "~/Pages/PackageFeatureList.aspx?" + QueryStringConstants.POSTBACK_PACKAGEID + "=" + ddlPackage.SelectedValue.ToString() + "&" + QueryStringConstants.POSTBACK_ISCUSTOMIZED_PACKAGE + "=-2";
            hlPackageMatrix.Attributes.Add("onclick", "javascript:return OpenRadWindow('" + Page.ResolveClientUrl(packagePopupUrl) + "', " + 500 + " , " + 620 + ");");

            Helper.SetPageTitle(this, 1284);
        }
    }

    private void SelectPackageDropdownItem(Int16 packageId)
    {
        ListItem oListItem = null;
        oListItem = ddlPackage.Items.FindByValue(packageId.ToString());
        if (oListItem != null)
        {
            ddlPackage.SelectedItem.Selected = false;
            oListItem.Selected = true;
        }
    }

    private void SetErrorMessages()
    {
        // Set Error Messages

        txtName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1328);
        txtDisplayName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1329);
        txtCompanyAlias.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblCompanyAlias.LabelID);
        rfvCalenderStartDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1330);
        //rfvSubscriptionEnddate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1331);
        //txtDataStorage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1332);
        txtContactName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2022);
        requiredFieldLicencedUser.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1302);
        requiredFieldDataStorage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1332);
        cmpvldNumericValidator.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1302);
        //cmpvldDataStorage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1303);

        cvNumOfDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1300);

        cvCompareWithSubscriptionStartDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1298, 1299);

        cvMandatoryEndDateAndEndDates.ErrorMessage = LanguageUtil.GetValue(1234);
        cvLicencedUser.ErrorMessage = LanguageUtil.GetValue(1939);
        cvDataStorage.ErrorMessage = LanguageUtil.GetValue(1941);

        cvCalenderStartDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1298);
        cvcalEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1299);
        cvIsActive.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1274);
        rfvPackage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2172);
        cvCompanyLogo.ErrorMessage = LanguageUtil.GetValue(5000228);
        cvUseSeparateDatabase.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2659);
        cvEnableFTP.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2902);
    }
    private static void ClearSessionAndCacheData(CompanyHdrInfo oCompanyHdrInfo)
    {
        CacheHelper.ClearFeaturesByCompanyID(oCompanyHdrInfo.CompanyID);
        CacheHelper.ClearAllRecStatus();
        CacheHelper.ClearAllRoleByCompanyID(oCompanyHdrInfo.CompanyID);
        SessionHelper.ClearRecStatusFromSession();
        SessionHelper.ClearAllRolesFromSession();
        SessionHelper.ClearCompanyHdrInfoFromSession();
    }

    private void MoveFileFromTempToTargetFolder(string FileName, string SourceDirPath, string TargetDirPath)
    {
        DirectoryInfo SourceDir = new DirectoryInfo(SourceDirPath);
        FileInfo[] AllFiles;
        AllFiles = SourceDir.GetFiles("*.*");
        foreach (FileInfo singleFile in AllFiles)
        {
            string singleFileName = singleFile.Name.ToString();
            if (singleFileName == FileName)
            {
                singleFile.CopyTo(TargetDirPath, true);
                singleFile.Delete();
            }

        }
    }

    private string CreateAndGetFolderPath()
    {
        String BaseFolderPath = SharedDataImportHelper.GetBaseFolder();
        String FolderName = AppSettingHelper.GetAppSettingValue(AppSettingConstants.TEMP_FOLDER_FOR_COMPANY_LOGO);
        string NewFolderPath = BaseFolderPath + FolderName;

        //if folder Folder exist or not ,if not create it.
        if (!Directory.Exists(NewFolderPath))
        {
            Directory.CreateDirectory(NewFolderPath);
        }
        NewFolderPath += @"\";
        return NewFolderPath;
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "CreateCompany";
    }
    #endregion


}
