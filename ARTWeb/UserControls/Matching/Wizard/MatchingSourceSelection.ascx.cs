using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;
using System.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.App.DAO.Matching;
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Web.UserControls.Matching.Wizard
{
    public partial class MatchingSourceSelection : UserControlMatchingWizardBase
    {
        bool IsMatchingSourceChanged = false;

        private List<MatchingSourceDataImportHdrInfo> MatchingSourceInfoListNBF
        {
            get
            {
                return (List<MatchingSourceDataImportHdrInfo>)Session[SessionConstants.MATCHING_SOURCE_INFO_LIST_NBF];
            }
            set
            {
                Session[SessionConstants.MATCHING_SOURCE_INFO_LIST_NBF] = value;
            }
        }

        private List<MatchingSourceDataImportHdrInfo> MatchingSourceInfoListGLTBS
        {
            get
            {
                return (List<MatchingSourceDataImportHdrInfo>)Session[SessionConstants.MATCHING_SOURCE_INFO_LIST_GLTBS];
            }
            set
            {
                Session[SessionConstants.MATCHING_SOURCE_INFO_LIST_GLTBS] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            MatchingWizardStepType = ARTEnums.MatchingWizardSteps.MatchingSourceSelection;
            WizardStepID = (int)ARTEnums.WizardSteps.MatchingSourceSelection;
            SetErrorMessages();
        }

        private void SetErrorMessages()
        {
            requiredFieldMatchSetName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2225);
            requiredFieldMatchSetDescription.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2226);
        }

        private void GetMatchingSources()
        {
            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            oMatchingParamInfo.UserID = SessionHelper.CurrentUserID;
            oMatchingParamInfo.RoleID = SessionHelper.CurrentRoleID;
            oMatchingParamInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oMatchingParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            oMatchingParamInfo.AccountID = AccountID;
            oMatchingParamInfo.MatchSetID = MatchSetID;
            oMatchingParamInfo.MatchingTypeID = (short)this.CurrentMatchingType;
            oMatchingParamInfo.ShowOnlySuccessfulMatchingSourceDataImport = true;
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoCollection = MatchingHelper.GetAllMatchSetMatchingSources(oMatchingParamInfo);

            //Find All NBF Type DataImport collection
            MatchingSourceInfoListNBF = new List<MatchingSourceDataImportHdrInfo>();
            MatchingSourceInfoListGLTBS = new List<MatchingSourceDataImportHdrInfo>();
            foreach (MatchingSourceDataImportHdrInfo oItem in oMatchingSourceDataImportHdrInfoCollection)
            {
                if (CurrentMatchSetHdrInfo != null)
                    oItem.IsSelected = FindMatchingSourceDataImportHdrInfo(oItem.MatchingSourceDataImportID, CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList);

                switch ((ARTEnums.MatchingSourceType)oItem.MatchingSourceTypeID)
                {
                    case ARTEnums.MatchingSourceType.GLTBS:
                        MatchingSourceInfoListGLTBS.Add(oItem);
                        break;
                    case ARTEnums.MatchingSourceType.NBF:
                        MatchingSourceInfoListNBF.Add(oItem);
                        break;
                }
            }
            MatchingSourceInfoListNBF = MatchingSourceInfoListNBF.OrderByDescending(N => N.IsSelected).ToList();
            MatchingSourceInfoListGLTBS = MatchingSourceInfoListGLTBS.OrderByDescending(N => N.IsSelected).ToList();
        }

        private void BindGrids()
        {
            if (CurrentMatchingType == ARTEnums.MatchingType.AccountMatching)
            {
                rgGLTBSFiles.CurrentPageIndex = 0;
                rgGLTBSFiles.EntityNameLabelID = 2278;
                rgGLTBSFiles.MasterTableView.VirtualItemCount = MatchingSourceInfoListGLTBS.Count;
                rgGLTBSFiles.DataSource = MatchingSourceInfoListGLTBS;
                rgGLTBSFiles.DataBind();
            }
            rgNBFFiles.CurrentPageIndex = 0;
            rgNBFFiles.EntityNameLabelID = 2279;
            rgNBFFiles.MasterTableView.VirtualItemCount = MatchingSourceInfoListNBF.Count;
            rgNBFFiles.DataSource = MatchingSourceInfoListNBF;
            rgNBFFiles.DataBind();
        }

        protected void rgGLTBSFiles_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            rgGLTBSFiles.MasterTableView.VirtualItemCount = MatchingSourceInfoListGLTBS.Count;
            rgGLTBSFiles.DataSource = MatchingSourceInfoListGLTBS;
        }

        protected void rgGLTBSFiles_SortCommand(object source, GridSortCommandEventArgs e)
        {
            GridHelper.HandleSortCommand(e);
            rgGLTBSFiles.Rebind();
        }

        protected void rgGLTBSFiles_ItemCommand(object source, GridCommandEventArgs e)
        {
        }

        protected void rgGLTBSFiles_ItemCreated(object source, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgGLTBSFiles.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    oRadComboBox.SelectedValue = hdnNewPageSize.Value.ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgGLTBSFiles.ClientID + "');");
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
        protected void rgGLTBSFiles_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            hdnNewPageSize.Value = e.NewPageSize.ToString();
        }

        protected void rgGLTBSFiles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = (MatchingSourceDataImportHdrInfo)e.Item.DataItem;
                if (oMatchingSourceDataImportHdrInfo != null && CurrentMatchSetHdrInfo != null)
                    oMatchingSourceDataImportHdrInfo.IsSelected = FindMatchingSourceDataImportHdrInfo(oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID, CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList);

                ExLabel lblMatchingSourceDataImportID = (ExLabel)e.Item.FindControl("lblMatchingSourceDataImportID");
                lblMatchingSourceDataImportID.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID.ToString());
                long matchingSourceDataImportID = Convert.ToInt64(lblMatchingSourceDataImportID.Text);

                BindCommonFields(e);

                ExRadioButton rbMatchingSource = (ExRadioButton)e.Item.FindControl("rbMatchingSource");
                if (rbMatchingSource != null)
                {
                    rbMatchingSource.GroupName = "GLTBSMatchingSourceGroup";
                    rbMatchingSource.Checked = oMatchingSourceDataImportHdrInfo.IsSelected;
                }

                string matchingSourceName = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceName);
                //ExImageButton imgBtnShowAccounts = (ExImageButton)e.Item.FindControl("imgBtnShowAccounts");
                //imgBtnShowAccounts.ToolTip = LanguageUtil.GetValue(2223);
                //imgBtnShowAccounts.PostBackUrl = "javascript:OpenRadWindowForHyperlink('PopupViewAccounts.aspx?" + QueryStringConstants.MATCHING_SOURCE_NAME + "=" + matchingSourceName + "&" + QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID + "=" + matchingSourceDataImportID + "', 350, 500, '" + imgBtnShowAccounts.ClientID + "');";

                ExHyperLink hlShowAccounts = (ExHyperLink)e.Item.FindControl("hlShowAccounts");
                hlShowAccounts.NavigateUrl = "javascript:OpenRadWindowForHyperlink('PopupViewAccounts.aspx?" + QueryStringConstants.MATCHING_SOURCE_NAME + "=" + matchingSourceName + "&" + QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID + "=" + matchingSourceDataImportID + "', 350, 500, '" + hlShowAccounts.ClientID + "');";

            }
        }

        protected void rgNBFFiles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = (MatchingSourceDataImportHdrInfo)e.Item.DataItem;
                if (oMatchingSourceDataImportHdrInfo != null && CurrentMatchSetHdrInfo != null)
                    oMatchingSourceDataImportHdrInfo.IsSelected = FindMatchingSourceDataImportHdrInfo(oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID, CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList);

                ExLabel lblMatchingSourceDataImportID = (ExLabel)e.Item.FindControl("lblMatchingSourceDataImportID");
                lblMatchingSourceDataImportID.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID.ToString());
                BindCommonFields(e);

                GridDataItem item = (GridDataItem)e.Item;
                item.Selected = oMatchingSourceDataImportHdrInfo.IsSelected;
            }
        }
        protected void rgNBFFiles_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            hdnNBFNewPazeSize.Value = e.NewPageSize.ToString();
        }
        protected void rgNBFFiles_ItemCommand(object source, GridCommandEventArgs e)
        {
        }

        protected void rgNBFFiles_ItemCreated(object source, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlNBFPageSize");
                if (rgGLTBSFiles.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    oRadComboBox.SelectedValue = hdnNBFNewPazeSize.Value.ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgNBFFiles.ClientID + "');");
                    oRadComboBox.Visible = true;
                }
                else
                {
                    Control pnlNBFPageSizeDDL = gridPager.FindControl("pnlNBFPageSizeDDL");
                    pnlNBFPageSizeDDL.Visible = false;
                }
                Control numericPagerControl = gridPager.GetNumericPager();
                Control placeHolder = gridPager.FindControl("NBFNumericPagerPlaceHolder");
                placeHolder.Controls.Add(numericPagerControl);

            }
        }

        protected void rgNBFFiles_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            rgNBFFiles.MasterTableView.VirtualItemCount = MatchingSourceInfoListNBF.Count;
            rgNBFFiles.DataSource = MatchingSourceInfoListNBF;
        }

        protected void rgNBFFiles_SortCommand(object source, GridSortCommandEventArgs e)
        {
            GridHelper.HandleSortCommand(e);
            rgNBFFiles.Rebind();
        }

        private void PopulateMatchSetHdrData(MatchSetHdrInfo oMatchSetHdrInfo)
        {
            txtName.Text = oMatchSetHdrInfo.MatchSetName;
            txtDescription.Text = oMatchSetHdrInfo.MatchSetDescription;
            tdMatchSetRef.Visible = true;
            lblMatchSetRefNumber.Text = oMatchSetHdrInfo.MatchSetRef;
        }

        private bool FindMatchingSourceDataImportHdrInfo(long? matchingSourceDataImportID, List<MatchingSourceDataImportHdrInfo> oMatchingSourceList)
        {
            if (oMatchingSourceList != null && oMatchingSourceList.Count > 0)
            {
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = oMatchingSourceList.Find(T => T.MatchingSourceDataImportID == matchingSourceDataImportID);
                if (oMatchingSourceDataImportHdrInfo != null)
                    return true;
            }
            return false;
        }

        public override void LoadData()
        {
            rgNBFFiles.PageSize = 10;
            PageBase oPageBase = (PageBase)this.Page;
            Helper.ShowInputRequirementSection(oPageBase, 2291, 2292, 2293);

            if (MatchSetID != null && (CurrentMatchSetHdrInfo == null || MatchSetID != CurrentMatchSetHdrInfo.MatchSetID))
            {
                GetMatchSetFromDB();
                if (CurrentMatchSetHdrInfo != null)
                {
                    PopulateMatchSetHdrData(CurrentMatchSetHdrInfo);
                }
            }
            GetMatchingSources();
            BindGrids();
            if (CurrentMatchingType == ARTEnums.MatchingType.AccountMatching)
                hdnMinNBFFileCount.Value = "1";
            else
                hdnMinNBFFileCount.Value = "2";
            if (CurrentMatchSetHdrInfo != null && this.IsEditMode)
            {
                string prevSelections = string.Empty;
                if (CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList != null)
                {
                    foreach (MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo in CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList)
                    {
                        prevSelections += oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID.Value + ",";
                    }
                    if (prevSelections != string.Empty)
                        prevSelections = prevSelections.Substring(0, prevSelections.Length - 1);
                }
                Page.ClientScript.RegisterOnSubmitStatement(typeof(string), "MatchingSourceChanged", "return IsMatchingSourceChanged('" + prevSelections + "','" + LanguageUtil.GetValue(5000280) + "');");
            }
        }

        /// <summary>
        /// Get Match Set from Database
        /// </summary>
        private void GetMatchSetFromDB()
        {
            //Get Value from DB
            CurrentMatchSetHdrInfo = MatchingHelper.GetMatchSet(MatchSetID, GLDataID, AccountID, SessionHelper.CurrentReconciliationPeriodID);
        }

        public override bool SaveData()
        {
            MatchingParamInfo oMatchingMatchSetParamInfo = new MatchingParamInfo();
            oMatchingMatchSetParamInfo.IDList = new List<long>();

            MatchSetHdrInfo oMatchSetHdrInfo = (MatchSetHdrInfo)Helper.DeepClone(CurrentMatchSetHdrInfo);
            if (oMatchSetHdrInfo == null)
            {
                oMatchSetHdrInfo = new MatchSetHdrInfo();
            }

            oMatchSetHdrInfo.MatchSetName = txtName.Text;
            oMatchSetHdrInfo.MatchSetDescription = txtDescription.Text;
            oMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList = new List<MatchingSourceDataImportHdrInfo>();

            GridItemCollection dataItemCollectionGLTBS = this.rgGLTBSFiles.MasterTableView.Items;

            int dataImportIDGLTBS;
            foreach (GridDataItem item in dataItemCollectionGLTBS)
            {
                ExRadioButton rbMatchingSource = (ExRadioButton)item.FindControl("rbMatchingSource");
                if (rbMatchingSource.Checked == true)
                {
                    if (Int32.TryParse(item.GetDataKeyValue("MatchingSourceDataImportID").ToString(), out dataImportIDGLTBS))
                    {
                        MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                        oMatchingParamInfo.MatchingSourceDataImportID = dataImportIDGLTBS;
                        oMatchingParamInfo.GLDataID = GLDataID;
                        MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = MatchingHelper.GetMatchingSourceDataImportInfo(oMatchingParamInfo);
                        oMatchingSourceDataImportHdrInfo.IsSelected = true;
                        oMatchingMatchSetParamInfo.IDList.Add((long)oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID);
                        oMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Add(oMatchingSourceDataImportHdrInfo);
                    }
                    break;
                }
            }

            GridDataItem[] dataItemCollectionNBF = this.rgNBFFiles.MasterTableView.GetSelectedItems();
            if (dataItemCollectionNBF.Length > 0)
            {
                int dataImportIDNBF;
                foreach (GridDataItem item in dataItemCollectionNBF)
                {
                    if (Int32.TryParse(item.GetDataKeyValue("MatchingSourceDataImportID").ToString(), out dataImportIDNBF))
                    {
                        MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                        oMatchingParamInfo.MatchingSourceDataImportID = dataImportIDNBF;
                        MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = MatchingHelper.GetMatchingSourceDataImportInfo(oMatchingParamInfo);
                        oMatchingSourceDataImportHdrInfo.IsSelected = true;
                        oMatchingMatchSetParamInfo.IDList.Add((long)oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID);
                        oMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Add(oMatchingSourceDataImportHdrInfo);
                    }
                }
            }

            //********************************************************************************************************************************
            oMatchSetHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
            oMatchSetHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oMatchSetHdrInfo.DateAdded = DateTime.Now;
            oMatchSetHdrInfo.DateRevised = DateTime.Now;
            oMatchSetHdrInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            oMatchSetHdrInfo.AddedByUserID = SessionHelper.CurrentUserID;
            oMatchSetHdrInfo.IsActive = true;
            oMatchSetHdrInfo.AccountID = AccountID;
            if (oMatchSetHdrInfo.MatchingStatusID == null)
            {
                oMatchSetHdrInfo.MatchingStatusID = (short)ARTEnums.MatchingStatus.Draft;
            }

            if (oMatchSetHdrInfo.MatchingTypeID == null)
            {
                oMatchSetHdrInfo.MatchingTypeID = (short)CurrentMatchingType;
            }

            if (oMatchSetHdrInfo.GLDataID == null)
            {
                if (GLDataID != null)
                {
                    oMatchSetHdrInfo.GLDataID = GLDataID;
                }
            }

            //****Compare New With Old and set flags
            CompareObjectWithSession(oMatchSetHdrInfo, CurrentMatchSetHdrInfo);
            if (IsDataChanged)
            {
                oMatchingMatchSetParamInfo.IsMatchingSourceSelectionChanged = IsMatchingSourceChanged;
                SaveMatchSet(oMatchSetHdrInfo, oMatchingMatchSetParamInfo);
            }
            return IsDataChanged;
        }

        private void SaveMatchSet(MatchSetHdrInfo oMatchSetHdrInfo, MatchingParamInfo oMatchingParamInfo)
        {
            oMatchingParamInfo.MatchSetID = oMatchSetHdrInfo.MatchSetID;
            oMatchingParamInfo.MatchSetName = oMatchSetHdrInfo.MatchSetName;
            oMatchingParamInfo.MatchSetDescription = oMatchSetHdrInfo.MatchSetDescription;
            oMatchingParamInfo.GLDataID = oMatchSetHdrInfo.GLDataID;
            oMatchingParamInfo.MatchingTypeID = oMatchSetHdrInfo.MatchingTypeID;
            oMatchingParamInfo.MatchingStatusID = oMatchSetHdrInfo.MatchingStatusID;
            oMatchingParamInfo.RecPeriodID = oMatchSetHdrInfo.RecPeriodID;
            oMatchingParamInfo.IsActive = oMatchSetHdrInfo.IsActive;
            oMatchingParamInfo.AddedBy = oMatchSetHdrInfo.AddedBy;
            oMatchingParamInfo.DateAdded = oMatchSetHdrInfo.DateAdded;
            oMatchingParamInfo.RevisedBy = oMatchSetHdrInfo.RevisedBy;
            if (oMatchSetHdrInfo.DateRevised.HasValue)
                oMatchingParamInfo.DateRevised = oMatchSetHdrInfo.DateRevised.Value;
            else
                oMatchingParamInfo.DateRevised = DateTime.Now;
            oMatchingParamInfo.UserID = oMatchSetHdrInfo.AddedByUserID;
            oMatchingParamInfo.AccountID = oMatchSetHdrInfo.AccountID;
            oMatchingParamInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oMatchingParamInfo.UserLanguageID = SessionHelper.GetUserLanguage();
            oMatchingParamInfo.RoleID = SessionHelper.CurrentRoleID;
            oMatchingParamInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.Matching;

            MatchSetID = MatchingHelper.SaveMatchSet(oMatchingParamInfo);

            GetMatchSetFromDB();
        }

        private void BindCommonFields(Telerik.Web.UI.GridItemEventArgs e)
        {
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = (MatchingSourceDataImportHdrInfo)e.Item.DataItem;

            ExLabel lblMatchingSourceName = (ExLabel)e.Item.FindControl("lblMatchingSourceName");
            ExLabel lblMatchingSourceType = (ExLabel)e.Item.FindControl("lblMatchingSourceType");
            ExLabel lblMatchingFileName = (ExLabel)e.Item.FindControl("lblFileName");

            ExImageButton imgDownloadFile = (ExImageButton)e.Item.FindControl("imgDownloadFile");
            ExLabel lblTotalRecords = (ExLabel)e.Item.FindControl("lblTotalRecords");
            ExLabel lblUsedRecords = (ExLabel)e.Item.FindControl("lblUsedRecords");
            ExLabel lblRecordsLeft = (ExLabel)e.Item.FindControl("lblRecordsLeft");
            ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
            ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");

            lblMatchingSourceName.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceName);
            lblMatchingSourceType.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceTypeName);
            lblMatchingFileName.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.FileName);
            lblTotalRecords.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.RecordsImported.ToString());
            lblUsedRecords.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.RecItemCreatedCount.ToString());
            lblRecordsLeft.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.RecordsLeft.ToString());
            lblAddedBy.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.AddedBy);
            lblDateAdded.Text = Helper.GetDisplayDate(oMatchingSourceDataImportHdrInfo.DateAdded);

            //string url = "../DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(oMatchingSourceDataImportHdrInfo.PhysicalPath));
            //imgDownloadFile.OnClientClick = "document.location.href = '" + url + "';return false;";
            string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadMatchingImportFile);
            url += "&" + QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID + "=" + oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID.GetValueOrDefault()
            + "&" + QueryStringConstants.MATCHING_SOURCE_TYPE_ID + "=" + oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID.GetValueOrDefault()
            + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID.GetValueOrDefault();
            imgDownloadFile.Attributes.Add("onclick", "javascript:{$get('" + ifDownloader.ClientID + "').src='" + url + "'; return false;}");

        }

        public override void SetControlStatePostLoad()
        {
            base.SetControlStatePostLoad();
            GridColumn oGridColumn = rgGLTBSFiles.MasterTableView.Columns.FindByUniqueNameSafe("CheckboxSelectColumn");
            if (oGridColumn != null)
                oGridColumn.Display = this.IsEditMode;
            oGridColumn = rgNBFFiles.MasterTableView.Columns.FindByUniqueNameSafe("CheckboxSelectColumn");
            if (oGridColumn != null)
                oGridColumn.Display = this.IsEditMode;
            txtName.Enabled = this.IsEditMode;
            txtDescription.Enabled = this.IsEditMode;
            if (CurrentMatchingType == ARTEnums.MatchingType.DataMatching)
                divGLTBSFiles.Visible = false;
            else
                divGLTBSFiles.Visible = true;
        }

        /// <summary>
        /// Detect changes
        /// </summary>
        /// <param name="oMatchSetHdrInfoSource1"></param>
        /// <param name="oMatchSetHdrInfoSource2"></param>
        /// <returns></returns>
        private void CompareObjectWithSession(MatchSetHdrInfo oMatchSetHdrInfoSource1, MatchSetHdrInfo oMatchSetHdrInfoSource2)
        {
            IsClearDataRequired = false;
            IsDataChanged = false;
            IsMatchingSourceChanged = false;
            // If both are null means no change
            if (oMatchSetHdrInfoSource1 == null && oMatchSetHdrInfoSource2 == null)
                return;
            // If any one is null means there is a change
            if (oMatchSetHdrInfoSource1 == null || oMatchSetHdrInfoSource2 == null)
            {
                IsClearDataRequired = true;
                IsDataChanged = true;
                IsMatchingSourceChanged = true;
                return;
            }

            // if list are different then set the clear dependents data flag
            IsClearDataRequired = IsListChanged(oMatchSetHdrInfoSource1.MatchingSourceDataImportHdrInfoList, oMatchSetHdrInfoSource2.MatchingSourceDataImportHdrInfoList);

            // if Clear flag is set then data is changed
            if (IsClearDataRequired)
            {
                IsDataChanged = true;
                IsMatchingSourceChanged = true;
                return;
            }

            // Check MatchSet Name
            if (!IsDataChanged && oMatchSetHdrInfoSource1.MatchSetName != oMatchSetHdrInfoSource2.MatchSetName)
                IsDataChanged = true;

            // Check MatchSet Description
            if (!IsDataChanged && oMatchSetHdrInfoSource1.MatchSetDescription != oMatchSetHdrInfoSource2.MatchSetDescription)
                IsDataChanged = true;
        }

        /// <summary>
        /// Compare Matching Data Source List
        /// </summary>
        /// <param name="oList1"></param>
        /// <param name="oList2"></param>
        /// <returns></returns>
        private bool IsListChanged(List<MatchingSourceDataImportHdrInfo> oList1, List<MatchingSourceDataImportHdrInfo> oList2)
        {
            if (oList1 == null && oList2 == null)
                return false;
            if (oList1 == null || oList2 == null)
                return true;
            if (oList1.Count != oList2.Count)
                return true;
            foreach (MatchingSourceDataImportHdrInfo oItem in oList1)
                if (oList2.Find(T => T.MatchingSourceDataImportID == oItem.MatchingSourceDataImportID) == null)
                    return true;
            foreach (MatchingSourceDataImportHdrInfo oItem in oList2)
                if (oList1.Find(T => T.MatchingSourceDataImportID == oItem.MatchingSourceDataImportID) == null)
                    return true;
            return false;
        }
    }
}