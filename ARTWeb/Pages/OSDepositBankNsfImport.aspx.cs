using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using System.Text;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility.Classes;

public partial class Pages_OSDepositBankNsfImport : PageBaseRecPeriod
{
    #region Variables & Constants
    short? _RecCategoryTypeID = 0;
    bool _IsMultiCurrencyEnabled = false;
    bool isError = false;
    #endregion

    #region Properties
    private GLDataHdrInfo _GLDataHdrInfo;
    private string CurrentBCCY
    {
        get
        {
            if (this.GLDataHdrInfo != null)
                return this.GLDataHdrInfo.BaseCurrencyCode;
            return string.Empty;
        }
    }

    private GLDataHdrInfo GLDataHdrInfo
    {
        get
        {
            if (this._GLDataHdrInfo == null)
            {
                if (ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] == null)
                {
                    if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
                    {
                        long? glDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                        this._GLDataHdrInfo = Helper.GetGLDataHdrInfo(glDataID);
                    }
                    ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = this._GLDataHdrInfo;
                }
                else
                {
                    this._GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
                }
            }
            return this._GLDataHdrInfo;
        }
    }
    public string ReturnUrl
    {
        get
        {
            return ViewState["returnUrl"].ToString();
        }
        set
        {
            ViewState["returnUrl"] = value;
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        ////RecPeriodMasterPageBase oRecPeriodMasterPageBase = (RecPeriodMasterPageBase)this.Master;
        ////UpdatePanel updpnlMain = oRecPeriodMasterPageBase.UpdatePanelOnMasterPage;

        ////if (updpnlMain != null)
        ////{
        ////    updpnlMain.Triggers.Add(new PostBackTrigger { ControlID = btnPreview.UniqueID.ToString()});
        ////}

        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        ScriptManager scriptManager = oMasterPageBase.GetScriptManager();
        scriptManager.RegisterPostBackControl(btnPreview);
        scriptManager.RegisterPostBackControl(btnImportAll);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            SetPrivateVariables();
            PopulateItemsOnPage();

            if (Request.UrlReferrer == null)
            {
                //Handle the case where the page is requested directly
                //throw new Exception("This page has been called without a referring page");
            }
            else
            {
                if (!IsPostBack)
                {
                    string isRedirectedFromStatusPage = Convert.ToString(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE]);

                    if (string.IsNullOrEmpty(isRedirectedFromStatusPage))
                    {
                        ReturnUrl = Request.UrlReferrer.PathAndQuery;
                        Session["RedirectUrlForImportPage"] = ReturnUrl;
                    }
                    else
                    {
                        string confirmStatusMessageID = Convert.ToString(Request.QueryString[QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE]);
                        ReturnUrl = Convert.ToString(Session["RedirectUrlForImportPage"]);

                        if (!string.IsNullOrEmpty(confirmStatusMessageID) && Convert.ToInt32(confirmStatusMessageID) == 3)
                        {
                            ViewState["DataTable"] = Session["DataTableForImport"];
                            ImportAllUploadData();
                        }
                        else if (!string.IsNullOrEmpty(confirmStatusMessageID) && Convert.ToInt32(confirmStatusMessageID) == 2)
                        {
                            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
                            oMasterPageBase.ShowConfirmationMessage(2399);
                        }
                    }

                    //Setting allowed file extensions.
                    this.radFileUpload.AllowedFileExtensions = DataImportHelper.GetAllowedFileExtensions();

                    //Set allowed file size in bytes
                    this.radFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);

                    //setting error messages to be shown at runtime
                    this.cvFileUpload.Attributes.Add("fileNameErrorMessage", LanguageUtil.GetValue(5000035));
                    this.cvFileUpload.Attributes.Add("fileExtensionErrorMessage", LanguageUtil.GetValue(5000036));

                    //Hide all Preview Grids
                    pnlImportGrid.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        if (Request.QueryString[QueryStringConstants.REC_STATUS_ID] != null)
        {
            WebEnums.ReconciliationStatus eRecStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), Request.QueryString[QueryStringConstants.REC_STATUS_ID]);
            if (Helper.GetFormMode(WebEnums.ARTPages.OSDepositBankNsfImport, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
            {
                txtImportName.Enabled = true;
                radFileUpload.Enabled = true;
            }
            else
            {
                txtImportName.Enabled = false;
                radFileUpload.Enabled = false;
                btnCancel.Enabled = false;
                btnPreview.Enabled = false;
                btnImportAll.Enabled = false;
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        int importSourceSectionLabelID = 2038;
        //TODO: VINAY Uncomment In Next Release v1.6
        //if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IMPORT_SOURCE_PAGE_SECTION_LABEL_ID]))
        //    importSourceSectionLabelID = Int32.Parse(Request.QueryString[QueryStringConstants.IMPORT_SOURCE_PAGE_SECTION_LABEL_ID]);
        int lastPagePhreaseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
        if (lastPagePhreaseID != -1)
        {
            Helper.SetBreadcrumbs(this, 1071, 1187, lastPagePhreaseID, importSourceSectionLabelID);
        }
    }
    #endregion

    #region Grid Events
    /// <summary>
    /// Deletes file from specified path.
    /// </summary>
    /// <param name="filePath">file physical path</param>
    /// <param name="exceptionMessage"></param>
    protected void rgImportList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
            ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
            ExLabel lblLocalCurrencyCode = (ExLabel)e.Item.FindControl("lblLocalCurrencyCode");
            ExLabel lblLocalCCY = (ExLabel)e.Item.FindControl("lblLocalCCY");
            ExLabel lbllblRefNo = (ExLabel)e.Item.FindControl("lblRefNo");
            ExLabel lblError = (ExLabel)e.Item.FindControl("lblError");
            CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

            StringBuilder oSBError = new StringBuilder();
            string errorFormat = Helper.GetLabelIDValue(2495);

            DateTime? openDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Date"));

            if (openDate == default(DateTime))
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1399)));
                oSBError.Append("<br/>");
            }

            // additional Condition added by Prafull for date validation
            else if (openDate > Convert.ToDateTime(DateTime.Now))
            {
                isError = true;
                lblDate.Text = Helper.GetDisplayDate(openDate);
                oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1511, 2062));
                oSBError.Append("<br/>");

            }
            else
            {
                lblDate.Text = Helper.GetDisplayDate(openDate);
            }

            string description = (string)DataBinder.Eval(e.Item.DataItem, "Description");
            if (!string.IsNullOrEmpty(description))
            {
                lblDescription.Text = (string)DataBinder.Eval(e.Item.DataItem, "Description");
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1408)));
                oSBError.Append("<br/>");
            }

            string localCurrencyCode = (string)DataBinder.Eval(e.Item.DataItem, "L-CCY Code");

            if (!string.IsNullOrEmpty(localCurrencyCode))
            {
                lblLocalCurrencyCode.Text = localCurrencyCode;
                try
                {
                    if (localCurrencyCode.Length > 3)
                        throw new ARTException(5000348);
                    try
                    {
                        //if (localCurrencyCode.Length > 3)
                        //    throw new ARTException(5000348);
                        decimal? baseCurrencyExRate = null;
                        if (this._IsMultiCurrencyEnabled) // && this.oExchangeRateInfoCollection != null
                        {
                            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                            {
                                //ExchangeRateInfo oBCCYExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == this.CurrentBCCY);
                                //if (oBCCYExchangeRateInfo != null)
                                //    baseCurrencyExRate = oBCCYExchangeRateInfo.ExchangeRate.Value;
                                //else
                                //{
                                //    throw new ARTException(5000098);
                                //}
                                try
                                {
                                    decimal? exRateBCCY = CacheHelper.GetExchangeRate(localCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                                    if (exRateBCCY.HasValue)
                                        baseCurrencyExRate = exRateBCCY.Value;
                                    else
                                    {
                                        throw new ARTException(5000098);
                                    }
                                }
                                catch (Exception)
                                {

                                    throw new ARTException(5000098);
                                }
                            }
                        }
                        else
                        {
                            if (!SessionHelper.ReportingCurrencyCode.ToLower().Equals(localCurrencyCode.ToLower())) //reporting currency and local currency from excel import must be same
                                throw new ARTException(5000348);
                            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                                baseCurrencyExRate = 1;
                        }
                        (e.Item as GridDataItem)["BaseCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();
                    }
                    catch (ARTException art_ex)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000098));
                        oSBError.Append("<br/>");
                    }

                    try
                    {
                        //decimal reportingCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                        //    , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled);
                        decimal reportingCurrencyExRate = 1;
                        if (this._IsMultiCurrencyEnabled)
                        {
                            //ExchangeRateInfo oBCCYExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == SessionHelper.ReportingCurrencyCode);
                            //if (oBCCYExchangeRateInfo != null)
                            //    reportingCurrencyExRate = oBCCYExchangeRateInfo.ExchangeRate.Value;
                            //else
                            //    throw new ARTException(5000099);
                            try
                            {
                                decimal? exRateRCCY = CacheHelper.GetExchangeRate(localCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                                if (exRateRCCY.HasValue)
                                    reportingCurrencyExRate = exRateRCCY.Value;
                                else
                                    throw new ARTException(5000099);
                            }
                            catch (Exception)
                            {
                                throw new ARTException(5000099);
                            }

                        }
                        (e.Item as GridDataItem)["ReportingCurrencyExchangeRate"].Text = reportingCurrencyExRate.ToString();
                    }
                    catch (ARTException art_ex)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000099));
                        oSBError.Append("<br/>");
                    }
                }
                catch (ARTException art_ex)
                {
                    isError = true;
                    oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                    oSBError.Append("<br/>");
                }
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1773)));
                oSBError.Append("<br/>");
            }

            decimal amountLocalCurrency = (decimal)DataBinder.Eval(e.Item.DataItem, "AmountLocalCurrency");

            if (amountLocalCurrency != 0)
            {
                lblLocalCCY.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency);
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1675)));
                oSBError.Append("<br/>");
            }

            string refNumber = (string)DataBinder.Eval(e.Item.DataItem, "RefNo");
            if (!string.IsNullOrEmpty(refNumber))
            {
                lbllblRefNo.Text = refNumber;
            }

            lblError.Text = oSBError.ToString();

            if (!string.IsNullOrEmpty(oSBError.ToString()))
            {
                checkBox.Enabled = false;
                Sel.Value += e.Item.ItemIndex.ToString() + ":";
            }
        }
    }


    protected void rgImportList_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
        hdnNewPageSize.Value = e.NewPageSize.ToString();

    }

    protected void rgImportList_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
            if (rgImportList.AllowCustomPaging)
            {
                GridHelper.BindPageSizeGrid(oRadComboBox);
                oRadComboBox.SelectedValue = hdnNewPageSize.Value.ToString();
                oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "');");
                oRadComboBox.Visible = true;
            }
            else
            {
                Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                pnlPageSizeDDL.Visible = false;
            }
            Control numericPagerControl = gridPager.GetNumericPager();
            Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
            placeHolder.Controls.Add(numericPagerControl);

        }

    }
    protected void rgImportList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            int pageIndex = rgImportList.CurrentPageIndex;
            int pageSize = Convert.ToInt32(hdnNewPageSize.Value);
            int count;
            int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
            count = ((((pageIndex + 1) * pageSize) / defaultItemCount) + 1) * defaultItemCount;

            object obj = ViewState["DataTable"];
            if (obj != null)
            {
                DataTable objectCollection = (DataTable)obj;
                if (objectCollection.Rows.Count % defaultItemCount == 0)
                    rgImportList.VirtualItemCount = objectCollection.Rows.Count + 1;
                else
                    rgImportList.VirtualItemCount = objectCollection.Rows.Count;
                rgImportList.MasterTableView.DataSource = objectCollection;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rgImportList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        Sel.Value = string.Empty;
    }
    #endregion

    #region Other Events
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            StringBuilder oSBErrors = null;
            short importType = -1;
            DataTable dt = null;
            //DataTable dtPeriodEndDates;
            try
            {
                UploadedFile validFile = radFileUpload.UploadedFiles[0];
                ImportFileAttributes ImportFile;
                importType = (short)ARTEnums.DataImportType.RecItems;
                //Get folder path and name as per companyid and import type
                targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                filePath = Path.Combine(targetFolder, fileName);

                //save file
                validFile.SaveAs(filePath, true);

                //Save file attributes, they will be used at the time of importing file records.
                ImportFile.FileOriginalName = validFile.GetName();
                ImportFile.FileModifiedName = fileName;
                ImportFile.FilePhysicalPath = filePath;
                ImportFile.FileSize = validFile.ContentLength;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                {
                    string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                    //Once file is saved, get records in a DataTable.
                    //Show records in grid.
                    oSBErrors = new StringBuilder();
                    dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                    if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                        Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                    {
                        rgImportList.PageSize = Convert.ToInt32(hdnNewPageSize.Value);
                        rgImportList.VirtualItemCount = dt.Rows.Count;
                        ViewState["DataTable"] = dt;
                        this.ShowGridAndImportButtons(dt);
                    }
                }
            }
            catch (ARTException ex)
            {
                switch (ex.ExceptionPhraseID)
                {
                    //Invalid File. All Mandatory fields not present
                    //Save import and failure message to database
                    case 5000037:
                        InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                    //Invalid Data. 
                    //Save import and failure message to database
                    case 5000047:
                        InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                }
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            finally
            {
                this.radFileUpload.UploadedFiles.Clear();
            }
        }
        else
        {
            pnlImportGrid.Visible = false;
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlImportGrid.Visible = false;
    }

    protected void btnPageCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl, true);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            GLDataRecItemInfo oGLDataRecItemInfo = null;

            DataImportHdrInfo oDataImportHrdInfo = GetDataImportHdrDetails();
            short RefNo = 1;
            foreach (GridDataItem item in rgImportList.SelectedItems)
            {
                oGLDataRecItemInfo = GetGLDataRecItemDetail(item, RefNo);
                oGLDataRecItemInfoCollection.Add(oGLDataRecItemInfo);
                RefNo++;
            }

            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;

            oDataImportClient.InsertDataImportGLDataRecItem(oDataImportHrdInfo, oGLDataRecItemInfoCollection, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Success), out rowsAffected, Helper.GetAppUserInfo());

            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
            oMasterPageBase.ShowConfirmationMessage(1608);

            pnlImportGrid.Visible = false;
            Reset();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnImportAll_Click(object sender, EventArgs e)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            StringBuilder oSBErrors = null;
            short importType = -1;
            DataTable dt = null;
            try
            {
                UploadedFile validFile = radFileUpload.UploadedFiles[0];
                ImportFileAttributes ImportFile;
                importType = (short)ARTEnums.DataImportType.RecItems;
                //Get folder path and name as per companyid and import type
                targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                filePath = Path.Combine(targetFolder, fileName);


                //save file
                validFile.SaveAs(filePath, true);

                //Save file attributes, they will be used at the time of importing file records.
                ImportFile.FileOriginalName = validFile.GetName();
                ImportFile.FileModifiedName = fileName;
                ImportFile.FilePhysicalPath = filePath;
                ImportFile.FileSize = validFile.ContentLength;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                {
                    string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                    //Once file is saved, get records in a DataTable.
                    //Show records in grid.
                    oSBErrors = new StringBuilder();
                    dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                    if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                        Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                    {
                        rgImportList.PageSize = Convert.ToInt32(hdnNewPageSize.Value);
                        rgImportList.VirtualItemCount = dt.Rows.Count;
                        Session["DataImportHeaderInfo"] = GetDataImportHdrDetails();

                        DataTable dtValidData = GetValidRows(dt);
                        if (dtValidData.Select("IsValidRow = false").Count() > 0)
                        {
                            Session["DataTableForImport"] = dtValidData;
                            Response.Redirect(GetUrlForRecItemImportStatusPage());
                        }
                        else
                        {
                            ViewState["DataTable"] = dtValidData;
                            ImportAllUploadData();
                        }
                    }
                }
            }
            catch (ARTException ex)
            {
                switch (ex.ExceptionPhraseID)
                {
                    //Invalid File. All Mandatory fields not present
                    //Save import and failure message to database
                    case 5000037:
                        InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                    //Invalid Data. 
                    //Save import and failure message to database
                    case 5000047:
                        InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                }
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            finally
            {
                this.radFileUpload.UploadedFiles.Clear();
            }
        }
        else
        {
            pnlImportGrid.Visible = false;
        }
    }

    #endregion

    #region Validation Control Events

    /// <summary>
    /// Insert records for failure message to database
    /// Table affected: DataIMportHDR, DataImportFailureMessage
    /// </summary>
    protected void cvFileUpload_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.radFileUpload.InvalidFiles.Count > 0)
        {
            //Set default error message
            this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000037);

            //Check if there is enough space for this company to save this file
            UploadedFile invalidFile = radFileUpload.InvalidFiles[0];
            if (invalidFile.ContentLength > DataImportHelper.GetAvailableDataStorageByCompanyId(SessionHelper.CurrentCompanyID.Value))
            {
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000034);
                //throw new ARTException(5000034);//File size exceeds available space
            }
            //File size exceeds maximum file upload size.
            if (invalidFile.ContentLength > DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value))
            {
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000038);//File size is more than maximum allowed file size
            }
            //file extension validation 
            string[] arr = DataImportHelper.GetAllowedFileExtensions();
            string invalidFileExtension = invalidFile.GetExtension();
            bool valid = false;
            foreach (string str in arr)
            {
                if (str == invalidFileExtension)
                {
                    valid = true;
                    break;
                }
            }
            if (!valid)
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000036);//Invalid file extension 
            args.IsValid = false;

            //disable import and cancel buttons and remove grid
            pnlImportGrid.Visible = false;
        }

    }
    #endregion

    #region Private Methods
    private void Reset()
    {
        txtImportName.Text = string.Empty;
    }

    private string GetUrlForRecItemImportStatusPage()
    {
        return "RecItemImportStatusMessage.aspx";
    }

    private DataTable GetValidRows(DataTable dt)
    {
        //DataTable dtTempValidTable = new DataTable();
        //dtTempValidTable = dt.Clone();
        //DataRow[] drValidRows = dt.Select("IsValidRow = true");
        //if (drValidRows != null)
        //{
        //    dtTempValidTable = drValidRows.CopyToDataTable();
        //}

        DataTable dtValidTable = ValidateForMandatoryFields(dt);

        return dtValidTable;
    }

    private DataTable ValidateForMandatoryFields(DataTable dtImport)
    {
        DataTable dtTempValidTable = new DataTable();
        dtTempValidTable = dtImport.Clone();
        DataRow[] drValidRows = dtImport.Select();
        if (drValidRows != null)
        {
            dtTempValidTable = drValidRows.CopyToDataTable();
        }

        dtTempValidTable.Columns.Add("ErrorMessage");

        string errorFormat = Helper.GetLabelIDValue(2495);
        StringBuilder oSBError;
        int rowCount = 0;

        foreach (DataRow dr in dtTempValidTable.Rows)
        {
            oSBError = new StringBuilder();
            rowCount += 1;
            //oSBError.Append("Error in Row" + rowCount + ":");
            //oSBError.Append("<br/>");
            DateTime? openDate = Convert.ToDateTime(dr["Date"]);

            if (openDate == default(DateTime))
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1399)));
                oSBError.Append("<br/>");
            }

            // additional Condition added by Prafull for date validation
            else if (openDate > Convert.ToDateTime(DateTime.Now))
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1511, 2062));
                oSBError.Append("<br/>");
            }

            string description = dr["Description"].ToString();
            if (string.IsNullOrEmpty(description))
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1408)));
                oSBError.Append("<br/>");
            }

            string localCurrencyCode = dr["L-CCY Code"].ToString();

            if (!string.IsNullOrEmpty(localCurrencyCode))
            {
                try
                {
                    if (localCurrencyCode.Length > 3)
                        throw new ARTException(5000348);
                    try
                    {
                        //if (localCurrencyCode.Length > 3)
                        //    throw new ARTException(5000348);
                        //decimal baseCurrencyExRate = 1;
                        //if (!string.IsNullOrEmpty(this.CurrentBCCY))
                        //    baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                        //        , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled);
                        if (this._IsMultiCurrencyEnabled)
                        {
                            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                            {
                                //ExchangeRateInfo oBccyExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == this.CurrentBCCY);
                                //if (oBccyExchangeRateInfo == null)
                                //  throw new ARTException(5000098);
                                try
                                {
                                    decimal? exRateBCCY = CacheHelper.GetExchangeRate(localCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                                    if (exRateBCCY == null)
                                        throw new ARTException(5000098);
                                }
                                catch (Exception)
                                {
                                    throw new ARTException(5000098);
                                }
                            }
                        }
                        else
                        {
                            if (!SessionHelper.ReportingCurrencyCode.ToLower().Equals(localCurrencyCode.ToLower())) //reporting currency and local currency from excel import must be same
                                throw new ARTException(5000348);

                        }

                    }
                    catch (ARTException art_ex)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(5000098));
                        oSBError.Append("<br/>");
                    }

                    try
                    {
                        //decimal baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                        //    , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled);
                        if (this._IsMultiCurrencyEnabled)
                        {
                            //ExchangeRateInfo oRCCYExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == SessionHelper.ReportingCurrencyCode);
                            //if (oRCCYExchangeRateInfo == null)
                            //  throw new ARTException(5000099);
                            try
                            {
                                decimal? exRateRCCY = CacheHelper.GetExchangeRate(localCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                                if (exRateRCCY == null)
                                    throw new ARTException(5000099);
                            }
                            catch (Exception)
                            {
                                throw new ARTException(5000099);
                            }
                        }
                    }
                    catch (ARTException art_ex)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(5000099));
                        oSBError.Append("<br/>");
                    }
                }
                catch (ARTException art_ex)
                {
                    dr["IsValidRow"] = "false";
                    oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                    oSBError.Append("<br/>");
                }
            }
            else
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1773)));
                oSBError.Append("<br/>");
            }

            decimal amountLocalCurrency = Convert.ToDecimal(dr["AmountLocalCurrency"]);

            if (amountLocalCurrency == 0)
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1675)));
                oSBError.Append("<br/>");
            }

            dr["ErrorMessage"] = oSBError.ToString();
        }


        return dtTempValidTable;
    }

    private void ImportAllUploadData()
    {
        List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
        GLDataRecItemInfo oGLDataRecItemInfo = null;
        DataTable dtTempValidTable = (DataTable)ViewState["DataTable"];

        DataTable dtUploadData = new DataTable();
        dtUploadData = dtTempValidTable.Clone();
        DataRow[] drValidRows = dtTempValidTable.Select("IsValidRow = true");
        if (drValidRows != null && drValidRows.Count() > 0)
        {
            dtUploadData = drValidRows.CopyToDataTable();
        }


        DataImportHdrInfo oDataImportHrdInfo = null;
        if (Session["DataImportHeaderInfo"] != null)
        {
            oDataImportHrdInfo = (DataImportHdrInfo)Session["DataImportHeaderInfo"];
        }
        else
        {
            oDataImportHrdInfo = GetDataImportHdrDetails();
        }

        short RefNo = 1;
        foreach (DataRow rowUpload in dtUploadData.Rows)
        {
            oGLDataRecItemInfo = GetGLDataRecItemDetailForImportALL(rowUpload, RefNo);
            oGLDataRecItemInfoCollection.Add(oGLDataRecItemInfo);
            RefNo++;
        }

        IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
        int rowsAffected;

        oDataImportClient.InsertDataImportGLDataRecItem(oDataImportHrdInfo, oGLDataRecItemInfoCollection, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Success), out rowsAffected, Helper.GetAppUserInfo());

        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        oMasterPageBase.ShowConfirmationMessage(1608);

        pnlImportGrid.Visible = false;
        Reset();
    }

    private GLDataRecItemInfo GetGLDataRecItemDetailForImportALL(DataRow drUpload, short? RefNo)
    {
        GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();

        oGLDataRecItemInfo.AddedBy = oUserHdrInfo.LoginID;
        oGLDataRecItemInfo.AddedByUserID = oUserHdrInfo.UserID;

        decimal amountLocalCurrency = Convert.ToDecimal(drUpload["AmountLocalCurrency"]);
        oGLDataRecItemInfo.Amount = amountLocalCurrency;

        #region Calculate Base currency and Reporting currency exchange rates
        string localCurrencyCode = (string)drUpload["L-CCY Code"];
        decimal? baseCurrencyExRate = null;
        decimal reportingCurrencyExRate = 1;

        if (!string.IsNullOrEmpty(localCurrencyCode))
        {
            //IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
            //try
            //{
            //    if (!string.IsNullOrEmpty(this.CurrentBCCY))
            //        baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
            //            , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled);
            //}
            //catch (Exception ex) { }

            //try
            //{
            //    reportingCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
            //        , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled);
            //}
            //catch (Exception ex) { }
            if (this._IsMultiCurrencyEnabled)
            {
                if (!String.IsNullOrEmpty(this.CurrentBCCY))
                {
                    //ExchangeRateInfo bccyExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == this.CurrentBCCY);
                    //if (bccyExchangeRateInfo != null)
                    //    baseCurrencyExRate = bccyExchangeRateInfo.ExchangeRate.Value;
                    decimal? exRateBCCY = CacheHelper.GetExchangeRate(localCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                    if (exRateBCCY.HasValue)
                        baseCurrencyExRate = exRateBCCY.Value;
                }
                else
                    baseCurrencyExRate = null;

                //ExchangeRateInfo rccyExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == SessionHelper.ReportingCurrencyCode);
                //if (rccyExchangeRateInfo != null)
                //    reportingCurrencyExRate = rccyExchangeRateInfo.ExchangeRate.Value;
                decimal? exRateRCCY = CacheHelper.GetExchangeRate(localCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                if (exRateRCCY.HasValue)
                    reportingCurrencyExRate = exRateRCCY.Value;
            }
            else
            {
                if (!String.IsNullOrEmpty(this.CurrentBCCY))
                {
                    baseCurrencyExRate = 1;
                }
                else
                {
                    baseCurrencyExRate = null;
                }
            }
        }
        #endregion


        //if (!this.GLDataHdrInfo.NetAccountID.HasValue || this.GLDataHdrInfo.NetAccountID.Value == 0)
        //{
        //    decimal baseCurrencyExchangeRate = baseCurrencyExRate;
        //    oGLDataRecItemInfo.AmountBaseCurrency = Math.Round(amountLocalCurrency * baseCurrencyExchangeRate, 2);
        //}
        if (baseCurrencyExRate.HasValue)
            oGLDataRecItemInfo.AmountBaseCurrency = Math.Round(amountLocalCurrency * baseCurrencyExRate.Value, 2);

        oGLDataRecItemInfo.AmountReportingCurrency = Math.Round(amountLocalCurrency * reportingCurrencyExRate, 2);

        oGLDataRecItemInfo.Comments = Convert.ToString(drUpload["Description"]);

        oGLDataRecItemInfo.DateAdded = DateTime.Now;
        oGLDataRecItemInfo.GLDataID = this.GLDataHdrInfo.GLDataID;
        oGLDataRecItemInfo.ImportName = txtImportName.Text;
        oGLDataRecItemInfo.IsActive = true;
        oGLDataRecItemInfo.IsAttachmentAvailable = false;

        oGLDataRecItemInfo.LocalCurrencyCode = localCurrencyCode;

        oGLDataRecItemInfo.OpenDate = Convert.ToDateTime(drUpload["Date"]);

        oGLDataRecItemInfo.ReconciliationCategoryTypeID = this._RecCategoryTypeID;
        oGLDataRecItemInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.DataImport;
        oGLDataRecItemInfo.IndexID = RefNo;

        return oGLDataRecItemInfo;
    }

    private void InsertDataImportHdrWithFailureMsg(string failureMsg, short dataImportTypeID, short dataImportStatusID)
    {
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.DataImportName = this.txtImportName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = dataImportTypeID;
        oDataImportHrdInfo.DataImportStatusID = dataImportStatusID;
        oDataImportHrdInfo.RecordsImported = 0;
        if (dataImportTypeID == (short)ARTEnums.DataImportType.GLData)
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;

        oDataImport.InsertDataImportWithFailureMsg(oDataImportHrdInfo, failureMsg, Helper.GetAppUserInfo());

    }

    private DataImportHdrInfo GetDataImportHdrDetails()
    {
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            //ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.DataImportName = this.txtImportName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.RecItems;
        oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Success;
        oDataImportHrdInfo.RecordsImported = rgImportList.SelectedItems.Count;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;


        return oDataImportHrdInfo;
    }

    private GLDataRecItemInfo GetGLDataRecItemDetail(GridDataItem item, short? RefNo)
    {
        GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();

        oGLDataRecItemInfo.AddedBy = oUserHdrInfo.LoginID;
        oGLDataRecItemInfo.AddedByUserID = oUserHdrInfo.UserID;

        ExLabel lblAmountLocalCurrency = (ExLabel)item.FindControl("lblLocalCCY");
        decimal amountLocalCurrency = Convert.ToDecimal(lblAmountLocalCurrency.Text);
        oGLDataRecItemInfo.Amount = amountLocalCurrency;

        //if (!this.GLDataHdrInfo.NetAccountID.HasValue || this.GLDataHdrInfo.NetAccountID.Value == 0)
        //{
        //    decimal baseCurrencyExchangeRate = Convert.ToDecimal(item["BaseCurrencyExchangeRate"].Text);
        //    oGLDataRecItemInfo.AmountBaseCurrency = Math.Round(amountLocalCurrency * baseCurrencyExchangeRate, 2);
        //}
        if (!String.IsNullOrEmpty(this.CurrentBCCY))
        {
            decimal baseCurrencyExchangeRate = Convert.ToDecimal(item["BaseCurrencyExchangeRate"].Text);
            oGLDataRecItemInfo.AmountBaseCurrency = Math.Round(amountLocalCurrency * baseCurrencyExchangeRate, 2);
        }
        decimal reportingCurrencyExchangeRate = Convert.ToDecimal(item["ReportingCurrencyExchangeRate"].Text);
        oGLDataRecItemInfo.AmountReportingCurrency = Math.Round(amountLocalCurrency * reportingCurrencyExchangeRate, 2);

        ExLabel description = (ExLabel)item.FindControl("lblDescription");
        oGLDataRecItemInfo.Comments = description.Text;

        oGLDataRecItemInfo.DateAdded = DateTime.Now;
        oGLDataRecItemInfo.GLDataID = this.GLDataHdrInfo.GLDataID;
        oGLDataRecItemInfo.ImportName = txtImportName.Text;
        oGLDataRecItemInfo.IsActive = true;
        oGLDataRecItemInfo.IsAttachmentAvailable = false;

        ExLabel lblLocalCurrencyCode = (ExLabel)item.FindControl("lblLocalCurrencyCode");
        oGLDataRecItemInfo.LocalCurrencyCode = lblLocalCurrencyCode.Text;

        //******Commented by Prafull on 17-Feb-2011
        //ExLabel lblRefNumber = (ExLabel)item.FindControl("lblRefNo");
        //oGLDataRecItemInfo.JournalEntryRef = lblRefNumber.Text;


        ExLabel lblOpenDate = (ExLabel)item.FindControl("lblDate");
        oGLDataRecItemInfo.OpenDate = Convert.ToDateTime(lblOpenDate.Text);

        oGLDataRecItemInfo.ReconciliationCategoryTypeID = this._RecCategoryTypeID;
        oGLDataRecItemInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.DataImport;
        oGLDataRecItemInfo.IndexID = RefNo;

        return oGLDataRecItemInfo;
    }

    private string GetPreviousPageName()
    {
        string PName = "";
        if (Request.UrlReferrer != null)
        {
            PName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        return PName;
    }

    private void PopulateItemsOnPage()
    {
        hlOpenExcelFile.NavigateUrl = "~/Templates/OSBANKNSIMPORT.xlsx";
        Helper.SetPageTitle(this, 2038);
        Helper.ShowInputRequirementSection(this, 1641, 1647);
    }

    private void SetPrivateVariables()
    {


        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            _RecCategoryTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);

        this._IsMultiCurrencyEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);

        // Set the Master Page Properties for GL Data ID
        RecHelper.SetRecStatusBarPropertiesForOtherPages(this, this.GLDataHdrInfo.GLDataID);

        //Get Exchange Rate for currencies when MultiCurrency capability is used
        //if (this._IsMultiCurrencyEnabled)
        //{
        //    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        //    oExchangeRateInfoCollection = oUtilityClient.GetCurrencyExchangeRateByRecPeriod(SessionHelper.CurrentReconciliationPeriodID.Value);
        //}
    }

    private void DeleteFile(string filePath)
    {
        //Delete the saved file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        //Send exception to user interface
    }

    private void ShowGridAndImportButtons(DataTable dt)
    {
        Sel.Value = string.Empty;
        pnlImportGrid.Visible = true;
        //DataView dv = dt.DefaultView;
        //dv.RowFilter = "IsDuplicate = false";
        this.rgImportList.DataSource = dt;
        this.rgImportList.DataBind();
        GridColumn oGridColumn = this.rgImportList.Columns.FindByUniqueNameSafe("Error");

        if (isError == false)
        {
            if (oGridColumn != null)
                oGridColumn.Visible = false;
            //this.rgImportList.DataBind();
        }
        else
        {
            if (oGridColumn != null)
                oGridColumn.Visible = true;

        }
    }

    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "AccountViewer";
    }
    #endregion

}
