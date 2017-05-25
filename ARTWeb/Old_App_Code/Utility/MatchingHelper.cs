using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using System.IO;
using System.Text;
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using System.Text.RegularExpressions;
using SkyStem.Library.Controls.WebControls;
using System.ComponentModel;


namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for MatchingHelper
    /// </summary>
    public class MatchingHelper
    {
        private MatchingHelper()
        { }

        #region Service Calls
        public static List<MatchingSourceDataImportHdrInfo> SaveMatchingSource(int? companyID, List<MatchingSourceDataImportHdrInfo> matchingSourceDataImportHdrInfoList)
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            oMatchingParamInfo.CompanyID = companyID;
            oMatchingParamInfo.oMatchingSourceDataImportHdrInfoList = matchingSourceDataImportHdrInfoList;
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.SaveMatchingSource(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static List<MatchingSourceColumnInfo> GetMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.GetMatchingSourceColumn(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static List<DataTypeMstInfo> GetAllDataType()
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.GetAllDataType( Helper.GetAppUserInfo());
        }
        public static bool SaveMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.SaveMatchingSourceColumn(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static List<MatchingSourceDataImportHdrInfo> GetMatchingSources(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.GetMatchingSources(oMatchingParamInfo,Helper.GetAppUserInfo());
        }
        public static List<MatchingSourceDataImportHdrInfo> GetAllMatchSetMatchingSources(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.GetAllMatchSetMatchingSources(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static bool DeleteMatchingSourceData(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.DeleteMatchingSourceData(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static MatchingSourceDataImportHdrInfo GetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.GetMatchingSourceDataImportInfo(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static bool UpdateMatchingSourceDataImportForForceCommit(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.UpdateMatchingSourceDataImportForForceCommit(oMatchingParamInfo, Helper.GetAppUserInfo());
        }


        public static MatchSetMatchingSourceDataImportInfo GetMatchSetMatchingSourceDataImport(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.GetMatchSetMatchingSourceDataImport(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static List<MatchingSourceDataInfo> GeMatchingSourceData(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.GetMatchingSourceData(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static long SaveMatchSet(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.SaveMatchSet(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static MatchSetHdrInfo GetMatchSet(long? matchSetID, long? glDataID, long? accountID, int? recPeriodID)
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            Helper.FillCommonServiceParams(oMatchingParamInfo);
            oMatchingParamInfo.MatchSetID = matchSetID;
            oMatchingParamInfo.GLDataID = glDataID;
            oMatchingParamInfo.AccountID = accountID;
            oMatchingParamInfo.RecPeriodID = recPeriodID;
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            MatchSetHdrInfo oMatchSetHdrInfo = oMatching.GetMatchSet(oMatchingParamInfo, Helper.GetAppUserInfo());
            return oMatchSetHdrInfo;
        }

        public static List<MatchSetHdrInfo> SelectAllMatchSetHdrInfo(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            List<MatchSetHdrInfo> oMatchSetHdrInfoCollection = null;
            oMatchSetHdrInfoCollection = oMatching.SelectAllMatchSetHdrInfo(oMatchingParamInfo, Helper.GetAppUserInfo());
            return oMatchSetHdrInfoCollection;
        }

        public static List<GLDataHdrInfo> GetAccountDetails(MatchingParamInfo oMatchingParamInfo)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();

            IMatching oMatching = RemotingHelper.GetMatchingObject();
            oGLDataHdrInfoCollection = oMatching.GetAccountDetails(oMatchingParamInfo, Helper.GetAppUserInfo());

            return oGLDataHdrInfoCollection;
        }

        public static void DeleteMatchSet(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            oMatching.DeleteMatchSet(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static void EditMatchSet(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            oMatching.EditMatchSet(oMatchingParamInfo, Helper.GetAppUserInfo());
        }
        public static List<MatchSetSubSetCombinationInfo> UpdateMatchSetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.UpdateMatchSetMatchingSourceDataImportInfo(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static bool IsRecItemCreated(MatchingParamInfo oMatchingParamInfo)
        {
            bool bRecItemCreated = false;
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            bRecItemCreated = oMatching.IsRecItemCreated(oMatchingParamInfo, Helper.GetAppUserInfo());
            return bRecItemCreated;

        }

        public static List<RecItemColumnMstInfo> GetAllRecItemColumns()
        {

            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            return oMatchingClient.GetAllRecItemColumns(Helper.GetAppUserInfo());

        }


        public static bool SaveRecItemColumnMapping(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.SaveRecItemColumnMapping(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static int SaveMatchingConfiguration(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.SaveMatchingConfiguration(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static bool CleanRecItemColumnMapping(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.CleanRecItemColumnMapping(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static List<MatchingConfigurationInfo> GetAllMatchingConfiguration(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            return oMatchingClient.GetAllMatchingConfiguration(oMatchingParamInfo, Helper.GetAppUserInfo());
        }


        public static List<MatchSetSubSetCombinationInfo> GetAllMatchSetSubSetCombination(long? matchSetID, long? glDataID, int? recPeriodID, bool? isConfigurationComplete)
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            oMatchingParamInfo.MatchSetID = matchSetID;
            oMatchingParamInfo.GLDataID = glDataID;
            oMatchingParamInfo.RecPeriodID = recPeriodID;
            oMatchingParamInfo.IsConfigurationComplete = isConfigurationComplete;
            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            return oMatchingClient.GetAllMatchSetSubSetCombination(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static MatchSetHdrInfo GetMatchSetResults(long? matchSetID, long? glDataID, int? recPeriodID, bool? isConfigurationComplete)
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            oMatchingParamInfo.MatchSetID = matchSetID;
            oMatchingParamInfo.GLDataID = glDataID;
            oMatchingParamInfo.RecPeriodID = recPeriodID;
            oMatchingParamInfo.IsConfigurationComplete = isConfigurationComplete;
            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            return oMatchingClient.GetMatchSetResults(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static int UpdateMatchingConfigurationDisplayedColumn(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            return oMatchingClient.UpdateMatchingConfigurationDisplayedColumn(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static bool UpdateMatchingSourceDataImportHiddenStatus(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            return oMatchingClient.UpdateMatchingSourceDataImportHiddenStatus(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static bool UpdateMatchSetSubSetCombinationForConfigStatus(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
            return oMatchingClient.UpdateMatchSetSubSetCombinationForConfigStatus(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        public static bool UpdateMatchSetResults(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            return oMatching.UpdateMatchSetResults(oMatchingParamInfo, Helper.GetAppUserInfo());
        }

        #endregion

        #region Other Utility
        /// <summary>
        /// 
        /// </summary>
        /// <returns>folder list of null MatchingSourceDataImportHdrInfo object.</returns>
        public static List<MatchingSourceDataImportHdrInfo> GetMatchSource()
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoCollection = new List<MatchingSourceDataImportHdrInfo>();
            int MatchingDataImportRow = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.MATCHING_DATA_IMPORT_ROW));
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = null;
            for (int i = 0; i < MatchingDataImportRow; i++)
                oMatchingSourceDataImportHdrInfoCollection.Add(oMatchingSourceDataImportHdrInfo);

            return oMatchingSourceDataImportHdrInfoCollection;
        }

        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public static string GetMatchingFolderForImport(int companyID, int ReconciliationPeriodID)
        {
            string baseFolderPath = SharedDataImportHelper.GetBaseFolderForCompany(companyID);
            string importFolder = @"";
            importFolder = baseFolderPath + @"\Matching\" + ReconciliationPeriodID;

            if (!Directory.Exists(importFolder))
                Directory.CreateDirectory(importFolder);

            return importFolder + @"\";
        }

        /// <summary>
        /// Formulates new filename for Matching file.
        /// </summary>
        /// <param name="validFile">File object</param>
        /// <param name="importType">import type</param>
        /// <returns>new file name</returns>
        public static string GetMatchingFileName(UploadedFile validFile, int UserID, short RoleID)
        {
            StringBuilder oSb = new StringBuilder();
            oSb.Append(validFile.GetNameWithoutExtension());
            oSb.Append("_");
            oSb.Append(Helper.GetDisplayDateTime(DateTime.Now));
            oSb.Append("_");
            oSb.Append(RoleID.ToString());
            oSb.Append("_");
            oSb.Append(UserID.ToString());
            oSb.Append(validFile.GetExtension());
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                oSb.Replace(ch.ToString(), "");
            }
            oSb.Replace(" ", "");
            return oSb.ToString();
        }

        /// <summary>
        /// Get Form Mode for Matching
        /// </summary>
        /// <param name="oMatchSetHdrInfo"></param>
        /// <returns></returns>
        public static WebEnums.FormMode GetFormModeForMatching(WebEnums.ARTPages ePage, ARTEnums.MatchingType? eMatchingType,
            WebEnums.ReconciliationStatus? eRecStatus, long? glDataID, MatchSetHdrInfo oMatchSetHdrInfo)
        {
            // Get Certification Started or not
            bool isCertificationStarted = CertificationHelper.IsCertificationStarted();
            // Get User Role
            ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)SessionHelper.CurrentRoleID.Value;
            // Get Matching Status
            ARTEnums.MatchingStatus? eMatchingStatus = null;
            int? addedByUserID = null;
            int? preparerUserID = null;
            int? backupPreparerUserID = null;
            if (oMatchSetHdrInfo != null)
            {
                if (oMatchSetHdrInfo.MatchingStatusID.GetValueOrDefault() > 0)
                    eMatchingStatus = (ARTEnums.MatchingStatus)oMatchSetHdrInfo.MatchingStatusID;
                if (oMatchSetHdrInfo.AddedByUserID.GetValueOrDefault() > 0)
                    addedByUserID = oMatchSetHdrInfo.AddedByUserID;
                if (oMatchSetHdrInfo.PreparerUserID.GetValueOrDefault() > 0)
                    preparerUserID = oMatchSetHdrInfo.PreparerUserID;
                if (oMatchSetHdrInfo.BackupPreparerUserID.GetValueOrDefault() > 0)
                    backupPreparerUserID = oMatchSetHdrInfo.BackupPreparerUserID;
            }

            if (eMatchingType.HasValue && eMatchingType.Value == ARTEnums.MatchingType.AccountMatching)
            {
                // Check for GL Data Availability
                if (glDataID.GetValueOrDefault() == 0)
                    return WebEnums.FormMode.ReadOnly;

                //Check for MOP
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                if (!oGLDataClient.IsGLDataIDEditable(glDataID.Value, Helper.GetAppUserInfo()))
                    return WebEnums.FormMode.ReadOnly;

                // If Certification Started
                if (isCertificationStarted)
                    return WebEnums.FormMode.ReadOnly;
                WebEnums.RecPeriodStatus CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
                // Check for Rec Period Status
                if (CurrentRecProcessStatus != WebEnums.RecPeriodStatus.Open
                    && CurrentRecProcessStatus != WebEnums.RecPeriodStatus.InProgress)
                    return WebEnums.FormMode.ReadOnly;

                // Check for User Role
                if (!(eUserRole == ARTEnums.UserRole.PREPARER || eUserRole == ARTEnums.UserRole.BACKUP_PREPARER))
                    return WebEnums.FormMode.ReadOnly;

                // Check for Rec Status
                if (eRecStatus != WebEnums.ReconciliationStatus.NotStarted
                        && eRecStatus != WebEnums.ReconciliationStatus.InProgress
                        && eRecStatus != WebEnums.ReconciliationStatus.PendingModificationPreparer)
                    return WebEnums.FormMode.ReadOnly;
                // Check the for backups and the creator
                if (addedByUserID.GetValueOrDefault() > 0)
                {
                    if (!(addedByUserID.GetValueOrDefault() == SessionHelper.CurrentUserID
                        || preparerUserID.GetValueOrDefault() == SessionHelper.CurrentUserID
                        || backupPreparerUserID.GetValueOrDefault() == SessionHelper.CurrentUserID))
                        return WebEnums.FormMode.ReadOnly;
                }
            }
            else
            {
                // Check the creator
                if (addedByUserID.GetValueOrDefault() > 0
                    && addedByUserID != SessionHelper.CurrentUserID)
                    return WebEnums.FormMode.ReadOnly;
            }

            switch (ePage)
            {
                case WebEnums.ARTPages.MatchingWizard:
                    if (eMatchingStatus.HasValue && eMatchingStatus.Value != ARTEnums.MatchingStatus.Draft)
                        return WebEnums.FormMode.ReadOnly;
                    break;
                case WebEnums.ARTPages.MatchingDataImport:
                    {
                        // Check Certification
                        if (isCertificationStarted)
                            return WebEnums.FormMode.ReadOnly;
                        WebEnums.RecPeriodStatus CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
                        // Check Rec Period Status
                        if (CurrentRecProcessStatus != WebEnums.RecPeriodStatus.NotStarted
                        && CurrentRecProcessStatus != WebEnums.RecPeriodStatus.Open
                        && CurrentRecProcessStatus != WebEnums.RecPeriodStatus.InProgress)
                            return WebEnums.FormMode.ReadOnly;
                        // Check User Role
                        if (eUserRole != ARTEnums.UserRole.SYSTEM_ADMIN
                            && eUserRole != ARTEnums.UserRole.BUSINESS_ADMIN
                            && eUserRole != ARTEnums.UserRole.PREPARER
                            && eUserRole != ARTEnums.UserRole.BACKUP_PREPARER)
                            return WebEnums.FormMode.ReadOnly;
                    }
                    break;
                case WebEnums.ARTPages.CreateRecItem:
                    if (eRecStatus != WebEnums.ReconciliationStatus.InProgress
                         && eRecStatus != WebEnums.ReconciliationStatus.NotStarted
                         && eRecStatus != WebEnums.ReconciliationStatus.PendingModificationPreparer)
                        return WebEnums.FormMode.ReadOnly;
                    break;
            }
            return WebEnums.FormMode.Edit;
        }

        /// <summary>
        /// Show Hide Back To Rec Form Btn
        /// </summary>
        /// <param name="wzMatching"></param>

        public static void ShowHideBackToRecFormBtn(ExButton btn)
        {

            if (HttpContext.Current.Session[SessionConstants.PARENT_PAGE_URL] != null)
                btn.Visible = true;
            else
                btn.Visible = false;
        }

        /// <summary>
        /// Disables navigation buttons for matching wizard
        /// </summary>
        /// <param name="wzMatching"></param>
        public static void DisableNavigationButtons(Wizard wzMatching)
        {
            Button btnUploadDataSources = (Button)wzMatching.FindControl("StartNavigationTemplateContainerID").FindControl("btnUploadNewDataSources");
            Button btnStepNextButton = (Button)wzMatching.FindControl("StepNavigationTemplateContainerID").FindControl("btnContinueLater");
            Button btnStepDiscardButton = (Button)wzMatching.FindControl("StepNavigationTemplateContainerID").FindControl("btnDiscard");
            Button btnFinishDiscardButton = (Button)wzMatching.FindControl("FinishNavigationTemplateContainerID").FindControl("btnDiscard");
            Button btnFinishSubmitButton = (Button)wzMatching.FindControl("FinishNavigationTemplateContainerID").FindControl("btnSubmit");

            btnFinishDiscardButton.Enabled = false;
            btnFinishSubmitButton.Enabled = false;
            btnStepDiscardButton.Enabled = false;
            btnStepNextButton.Enabled = false;
            btnUploadDataSources.Enabled = false;
        }
        #endregion

        #region MatchSetResults Section

        /// <summary>
        /// Creates the grid columns.
        /// </summary>
        /// <param name="oExRadGrid">The ex RAD grid.</param>
        /// <param name="oMatchingConfigurationInfoList">The matching configuration info list.</param>
        public static void CreateGridColumns(ExRadGrid oExRadGrid, List<MatchingConfigurationInfo> oMatchingConfigurationInfoList)
        {
            try
            {
                if (oExRadGrid != null && oMatchingConfigurationInfoList != null && oMatchingConfigurationInfoList.Count > 0)
                {
                    foreach (MatchingConfigurationInfo oMatchingConfigurationInfo in oMatchingConfigurationInfoList)
                    {
                        CreateGridColumn(oExRadGrid, oMatchingConfigurationInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        public static void CreateGridColumn(ExRadGrid oExRadGrid, MatchingConfigurationInfo oMatchingConfigurationInfo)
        {
            try
            {
                if (oExRadGrid != null && oMatchingConfigurationInfo != null &&
                    oMatchingConfigurationInfo.IsDisplayedColumn.GetValueOrDefault() &&
                    oMatchingConfigurationInfo.MatchingConfigurationID.HasValue &&
                    !String.IsNullOrEmpty(oMatchingConfigurationInfo.DisplayColumnName))
                {
                    string colName = GridHelper.RemoveSpecialChars(oMatchingConfigurationInfo.DisplayColumnName);
                    string dataFieldName = oMatchingConfigurationInfo.DisplayColumnName;
                    ExGridTemplateColumn col = (ExGridTemplateColumn)oExRadGrid.Columns.FindByUniqueNameSafe(colName);
                    if (col == null)
                    {
                        col = Helper.CreateDynamicExGridTemplateColumn(dataFieldName, GetColumnDataType(oMatchingConfigurationInfo), oMatchingConfigurationInfo.MatchingConfigurationID, colName, dataFieldName);
                        oExRadGrid.Columns.Add(col);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Creates the matching source name column.
        /// </summary>
        /// <param name="oExRadGrid">The o ex RAD grid.</param>
        public static void CreateMatchingSourceNameColumn(ExRadGrid oExRadGrid)
        {
            try
            {
                string hdrText = LanguageUtil.GetValue(2191);
                ExGridTemplateColumn col = (ExGridTemplateColumn)oExRadGrid.Columns.FindByUniqueNameSafe(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME);
                if (col == null)
                {
                    col = Helper.CreateDynamicExGridTemplateColumn(hdrText, WebEnums.DataType.String, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_ID_DATA_SOURCE_NAME, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME);
                    oExRadGrid.Columns.Add(col);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Creates Rec Item PairID column
        /// </summary>
        /// <param name="oExRadGrid"></param>
        public static void CreateRecItemPairIdColumn(ExRadGrid oExRadGrid)
        {
            try
            {
                string hdrText = LanguageUtil.GetValue(2376);
                ExGridTemplateColumn col = (ExGridTemplateColumn)oExRadGrid.Columns.FindByUniqueNameSafe(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID);
                if (col == null)
                {
                    col = Helper.CreateDynamicExGridTemplateColumn(hdrText, WebEnums.DataType.Integer, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_ID_PAIR_ID, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID);
                    col.ItemStyle.CssClass = "GridClientSelectColumnCSS";
                    oExRadGrid.Columns.Add(col);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Creates the rec item number column.
        /// </summary>
        /// <param name="oExRadGrid">The o ex RAD grid.</param>
        public static void CreateRecItemNumberColumn(ExRadGrid oExRadGrid)
        {
            try
            {
                string hdrText = LanguageUtil.GetValue(2118);
                ExGridTemplateColumn col = (ExGridTemplateColumn)oExRadGrid.Columns.FindByUniqueNameSafe(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER);
                if (col == null)
                {
                    col = Helper.CreateDynamicExGridTemplateColumn(hdrText, WebEnums.DataType.String, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_ID_REC_ITEM_NUMBER, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER);
                    oExRadGrid.Columns.Add(col);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Creates the Close Date column.
        /// </summary>
        /// <param name="oExRadGrid">The o ex RAD grid.</param>
        public static void CreateCloseDateColumn(ExRadGrid oExRadGrid)
        {
            try
            {
                string hdrText = LanguageUtil.GetValue(1411);
                ExGridTemplateColumn col = (ExGridTemplateColumn)oExRadGrid.Columns.FindByUniqueNameSafe(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE);
                if (col == null)
                {
                    col = Helper.CreateDynamicExGridTemplateColumn(hdrText, WebEnums.DataType.DataTime, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_ID_CLOSE_DATE, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE);
                    oExRadGrid.Columns.Add(col);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Creates the matching result grid columns.
        /// </summary>
        /// <param name="oExRadGrid">The o ex RAD grid.</param>
        /// <param name="oMatchingConfigurationInfoList">The o matching configuration info list.</param>
        public static void CreateGridPairColumns(ExRadGrid oExRadGrid, List<MatchingConfigurationInfo> oMatchingConfigurationInfoList, WebEnums.MatchingResultType matchingResultType)
        {
            try
            {
                if (oExRadGrid != null && oMatchingConfigurationInfoList != null && oMatchingConfigurationInfoList.Count > 0)
                {
                    foreach (MatchingConfigurationInfo oMatchingConfigurationInfo in oMatchingConfigurationInfoList)
                    {
                        CreateGridPairColumn(oExRadGrid, oMatchingConfigurationInfo, matchingResultType);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Creates the grid pair column.
        /// </summary>
        /// <param name="oExRadGrid">The ex RAD grid.</param>
        /// <param name="oMatchingConfigurationInfo">The o matching configuration info.</param>
        public static void CreateGridPairColumn(ExRadGrid oExRadGrid, MatchingConfigurationInfo oMatchingConfigurationInfo, WebEnums.MatchingResultType matchingResultType)
        {
            try
            {
                if (oExRadGrid != null && oMatchingConfigurationInfo != null &&
                    oMatchingConfigurationInfo.IsDisplayedColumn.GetValueOrDefault() &&
                    oMatchingConfigurationInfo.MatchingConfigurationID.HasValue &&
                    !String.IsNullOrEmpty(oMatchingConfigurationInfo.DisplayColumnName))
                {
                    string colName = GridHelper.RemoveSpecialChars(oMatchingConfigurationInfo.DisplayColumnName);
                    string dataFieldName = oMatchingConfigurationInfo.DisplayColumnName;
                    ExGridTemplateColumn col = (ExGridTemplateColumn)oExRadGrid.Columns.FindByUniqueNameSafe(colName);
                    GridViewItemPairTemplate oGridViewItemPairTemplate = null;
                    if (col == null)
                    {
                        col = new ExGridTemplateColumn();

                        oGridViewItemPairTemplate = new GridViewItemPairTemplate(ListItemType.Item, colName, dataFieldName, oMatchingConfigurationInfo.MatchingConfigurationID.Value, GetColumnDataType(oMatchingConfigurationInfo));
                        col.HeaderText = oMatchingConfigurationInfo.DisplayColumnName;
                        col.UniqueName = colName;
                        col.IsDynamicColumn = true;
                        col.ItemStyle.Wrap = false;
                        //oGridViewItemPairTemplate.XPathSource1 = String.Format(MatchSetResultConstants.MATCH_SET_RESULT_XPATH_SOURCEROWS, 1, colName);
                        //oGridViewItemPairTemplate.XPathSource2 = String.Format(MatchSetResultConstants.MATCH_SET_RESULT_XPATH_SOURCEROWS, 2, colName);
                        oGridViewItemPairTemplate.RelationNameSource1 = MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1;
                        oGridViewItemPairTemplate.RelationNameSource2 = MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2;

                        //oGridViewItemPairTemplate.XPathValue = string.Format(MatchSetResultConstants.MATCH_SET_RESULT_XPATH_VALUE, colName);
                        col.ItemTemplate = oGridViewItemPairTemplate;
                        if ((oMatchingConfigurationInfo.IsMatching.GetValueOrDefault(false) && matchingResultType == WebEnums.MatchingResultType.Matched)
                            || (oMatchingConfigurationInfo.IsPartialMatching.GetValueOrDefault(false) && matchingResultType == WebEnums.MatchingResultType.PartialMatched))
                        {
                            col.ItemStyle.CssClass = "MatchingGridCss";
                            oGridViewItemPairTemplate.CssClassSource1 = "MatchingResultSourceMatchPartialMatch1";
                            oGridViewItemPairTemplate.CssClassSource2 = "MatchingResultSourceMatchPartialMatch2";
                        }
                        else
                        {
                            oGridViewItemPairTemplate.CssClassSource1 = "MatchingResultSource1";
                            oGridViewItemPairTemplate.CssClassSource2 = "MatchingResultSource2";
                        }
                        oExRadGrid.Columns.Add(col);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Adds the matching relations.
        /// </summary>
        /// <param name="oDataSet">The data set.</param>
        public static void AddMatchingRelations(DataSet oDataSet)
        {
            try
            {
                if (oDataSet != null)
                {
                    if (!oDataSet.Relations.Contains(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1) &&
                        !oDataSet.Relations.Contains(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2))
                    {
                        DataTable dtPair = Helper.FindDataTable(oDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                        DataTable dtSource1 = Helper.FindDataTable(oDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                        DataTable dtSource2 = Helper.FindDataTable(oDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

                        if (dtPair != null && dtSource1 != null && dtSource2 != null)
                        {
                            DataColumn dcPair = Helper.FindDataColumn(dtPair, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID);
                            DataColumn dcSource1 = Helper.FindDataColumn(dtSource1, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID);
                            DataColumn dcSource2 = Helper.FindDataColumn(dtSource2, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID);

                            if (dcPair != null && dcSource1 != null && dcSource2 != null)
                            {
                                oDataSet.Relations.Add(new DataRelation(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1, dcPair, dcSource1));
                                oDataSet.Relations.Add(new DataRelation(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2, dcPair, dcSource2));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000287);
            }
        }

        /// <summary>
        /// Moves the pairs.
        /// </summary>
        /// <param name="dsSource">The ds source.</param>
        /// <param name="dsTarget">The ds target.</param>
        /// <param name="pairIDStr">The pair ID STR.</param>
        public static void MovePairs(DataSet dsSource, DataSet dsTarget, string pairIDStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(pairIDStr))
                {
                    // Get Source Tables
                    DataTable tblFromPair = Helper.FindDataTable(dsSource, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                    DataTable tblFromSource1 = Helper.FindDataTable(dsSource, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                    DataTable tblFromSource2 = Helper.FindDataTable(dsSource, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                    // Get Target Tables
                    DataTable tblToPair = Helper.FindDataTable(dsTarget, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                    DataTable tblToSource1 = Helper.FindDataTable(dsTarget, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                    DataTable tblToSource2 = Helper.FindDataTable(dsTarget, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

                    if (tblFromPair != null && tblFromSource1 != null && tblFromSource2 != null && tblToPair != null && tblToSource1 != null && tblToSource2 != null)
                    {
                        DataRow[] drFromPairRows = GetPairRows(tblFromPair, pairIDStr);
                        // Copy Pair Rows to Target
                        if (drFromPairRows != null && drFromPairRows.Length > 0)
                        {
                            drFromPairRows.CopyToDataTable(tblToPair, LoadOption.OverwriteChanges);
                            foreach (DataRow drPair in drFromPairRows)
                            {
                                // Move Source1 Child Rows
                                DataRow[] childRowsSource1 = drPair.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1);
                                Helper.MoveDataRows(tblFromSource1, tblToSource1, childRowsSource1);

                                // Move Source2 Child Rows
                                DataRow[] childRowsSource2 = drPair.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2);
                                Helper.MoveDataRows(tblFromSource2, tblToSource2, childRowsSource2);
                                // Remove Pair Row from Source
                                tblFromPair.Rows.Remove(drPair);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000288);
            }
        }

        /// <summary>
        /// Gets the selected pairs.
        /// </summary>
        /// <param name="oGrid">The o grid.</param>
        /// <returns></returns>
        public static string GetSelectedPairIDStr(ExRadGrid oGrid)
        {
            string pairIDStr = string.Empty;
            try
            {
                foreach (GridDataItem gItem in oGrid.SelectedItems)
                {
                    long pairID;
                    if (long.TryParse(gItem.GetDataKeyValue(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID).ToString(), out pairID))
                    {
                        pairIDStr += "," + pairID.ToString();
                    }
                }
                if (pairIDStr != string.Empty)
                    pairIDStr = pairIDStr.Substring(1);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000289);
            }
            return pairIDStr;
        }

        /// <summary>
        /// Gets the selected pair rows.
        /// </summary>
        /// <param name="oGrid">The o grid.</param>
        /// <param name="oDataSet">The o data set.</param>
        /// <returns></returns>
        public static DataRow[] GetSelectedPairRows(ExRadGrid oGrid, DataSet oDataSet)
        {
            try
            {
                string pairIDStr = MatchingHelper.GetSelectedPairIDStr(oGrid);
                if (pairIDStr != string.Empty)
                {
                    DataTable tblPair = Helper.FindDataTable(oDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                    return GetPairRows(tblPair, pairIDStr);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000289);
            }
            return null;
        }

        /// <summary>
        /// Gets the selected unmatched rows.
        /// </summary>
        /// <param name="oGrid">The o grid.</param>
        /// <param name="tblSource">The TBL source.</param>
        /// <returns></returns>
        public static DataRow[] GetSelectedUnmatchedRows(ExRadGrid oGrid, DataTable tblSource)
        {
            List<DataRow> oDataRowList = new List<DataRow>();
            try
            {
                if (oGrid != null && oGrid.Items.Count > 0 && tblSource != null && tblSource.Rows.Count > 0)
                {
                    string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
                        MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";

                    foreach (GridDataItem gItem in oGrid.SelectedItems)
                    {
                        long excelRowNumber;
                        long matchSetMatchingSourceDataImportID;
                        if (long.TryParse(gItem.GetDataKeyValue(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER).ToString(), out excelRowNumber) &&
                            long.TryParse(gItem.GetDataKeyValue(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID).ToString(), out matchSetMatchingSourceDataImportID))
                        {
                            oDataRowList.AddRange(tblSource.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000289);
            }
            return oDataRowList.ToArray();
        }


        ////public static DataRow[] GetSelectedUnmatchedRows(ExRadGrid oGrid, DataTable tblSource)
        ////{
        ////    List<DataRow> oDataRowList = new List<DataRow>();
        ////    try
        ////    {
        ////        SaveCheckBoxStates(oGrid, tblSource);
        ////        Dictionary<string, long> oSelectedItems = null;
        ////        if (HttpContext.Current.Session[getCheckedItemsSessionKey(oGrid)] != null)
        ////            oSelectedItems = (Dictionary<string, long>)HttpContext.Current.Session[getCheckedItemsSessionKey(oGrid)];

        ////        if (oSelectedItems != null && oSelectedItems.Count > 0 && tblSource != null && tblSource.Rows.Count > 0)
        ////        {
        ////            string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
        ////                MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";
        ////            foreach (KeyValuePair<string, Int64> item in oSelectedItems)
        ////            {
        ////                long excelRowNumber;
        ////                long matchSetMatchingSourceDataImportID;
        ////                excelRowNumber = Convert.ToInt64(item.Value);
        ////                matchSetMatchingSourceDataImportID = Convert.ToInt64(item.Key.Split('~')[0]);
        ////                if (long.TryParse(item.Value.ToString(), out excelRowNumber) &&
        ////                    long.TryParse(item.Key.Split('~')[0], out matchSetMatchingSourceDataImportID))
        ////                {
        ////                    oDataRowList.AddRange(tblSource.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID)));
        ////                }
        ////            }
        ////        }

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Helper.LogException(ex);
        ////        throw new ARTException(5000289);
        ////    }
        ////    return oDataRowList.ToArray();
        ////}
        ////public static void SaveCheckBoxStates(ExRadGrid oGrid, DataTable tblSource)
        ////{
        ////    Dictionary<string, long> oSelectedItems = null;
        ////    if (HttpContext.Current.Session[getCheckedItemsSessionKey(oGrid)] != null)
        ////        oSelectedItems = (Dictionary<string, long>)HttpContext.Current.Session[getCheckedItemsSessionKey(oGrid)];
        ////    else
        ////        oSelectedItems = new Dictionary<string, long>();
        ////    try
        ////    {
        ////        if (oGrid != null && oGrid.Items.Count > 0 && tblSource != null && tblSource.Rows.Count > 0)
        ////        {
        ////            foreach (GridDataItem gItem in oGrid.Items)
        ////            {
        ////                long excelRowNumber;
        ////                long matchSetMatchingSourceDataImportID;
        ////                if (long.TryParse(gItem.GetDataKeyValue(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER).ToString(), out excelRowNumber) &&
        ////                    long.TryParse(gItem.GetDataKeyValue(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID).ToString(), out matchSetMatchingSourceDataImportID))
        ////                {
        ////                    string key = matchSetMatchingSourceDataImportID.ToString() + "~" + excelRowNumber.ToString();
        ////                    if (gItem.Selected == true)
        ////                    {
        ////                        if (oSelectedItems != null && !oSelectedItems.ContainsKey(key))
        ////                            oSelectedItems.Add(key, excelRowNumber);
        ////                    }
        ////                    else
        ////                    {
        ////                        if (oSelectedItems != null && oSelectedItems.ContainsKey(key))
        ////                            oSelectedItems.Remove(key);
        ////                    }
        ////                }

        ////            }
        ////            if (oSelectedItems != null && oSelectedItems.Count > 0)
        ////            {
        ////                HttpContext.Current.Session[getCheckedItemsSessionKey(oGrid)] = oSelectedItems;
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Helper.LogException(ex);
        ////        throw new ARTException(5000289);
        ////    }

        ////}
        ////public static void RePopulateCheckBoxStates(ExRadGrid oGrid, DataTable tblSource)
        ////{

        ////    Dictionary<string, long> oSelectedItems = null;
        ////    if (HttpContext.Current.Session[getCheckedItemsSessionKey(oGrid)] != null)
        ////        oSelectedItems = (Dictionary<string, long>)HttpContext.Current.Session[getCheckedItemsSessionKey(oGrid)];
        ////    if (oSelectedItems != null && oSelectedItems.Count > 0)
        ////    {
        ////        foreach (GridDataItem gItem in oGrid.Items)
        ////        {
        ////            long excelRowNumber;
        ////            long matchSetMatchingSourceDataImportID;
        ////            if (long.TryParse(gItem.GetDataKeyValue(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER).ToString(), out excelRowNumber) &&
        ////                long.TryParse(gItem.GetDataKeyValue(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID).ToString(), out matchSetMatchingSourceDataImportID))
        ////            {
        ////                string key = matchSetMatchingSourceDataImportID.ToString() + "~" + excelRowNumber.ToString();
        ////                if (oSelectedItems.ContainsKey(key))
        ////                    gItem.Selected = true;
        ////            }
        ////        }
        ////    }
        ////}
        ////public static string getCheckedItemsSessionKey(ExRadGrid oGrid)
        ////{
        ////    return SessionConstants.CHECKED_ITEMS + oGrid.ClientID;
        ////}


        /// <summary>
        /// Gets the pair rows.
        /// </summary>
        /// <param name="tblPair">The TBL pair.</param>
        /// <param name="pairIDStr">The pair ID STR.</param>
        /// <returns></returns>
        public static DataRow[] GetPairRows(DataTable tblPair, string pairIDStr)
        {
            DataRow[] drPairRows = null;
            try
            {
                if (tblPair != null && tblPair.Rows.Count > 0)
                {
                    drPairRows = tblPair.Select(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID + " IN (" + pairIDStr + ")");
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000289);
            }
            return drPairRows;
        }

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <param name="oMatchingConfigurationInfo">The o matching configuration info.</param>
        /// <returns></returns>
        public static WebEnums.DataType GetColumnDataType(MatchingConfigurationInfo oMatchingConfigurationInfo)
        {
            WebEnums.DataType eDataType = WebEnums.DataType.String;
            try
            {
                if (oMatchingConfigurationInfo != null)
                {
                    if (oMatchingConfigurationInfo.MatchingSource1ColumnID.HasValue)
                        eDataType = (WebEnums.DataType)oMatchingConfigurationInfo.Source1ColumnDataTypeID.Value;
                    else if (oMatchingConfigurationInfo.Source2ColumnDataTypeID.HasValue)
                        eDataType = (WebEnums.DataType)oMatchingConfigurationInfo.Source2ColumnDataTypeID.Value;
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000290);
            }
            return eDataType;
        }

        /// <summary>
        /// Gets the next pair ID.
        /// </summary>
        /// <param name="ds1">The DS1.</param>
        /// <param name="ds2">The DS2.</param>
        /// <returns></returns>
        public static long GetNextPairID(DataSet ds1, DataSet ds2)
        {
            long pairID = 1;
            try
            {
                long maxPairID = 1;
                DataTable tblPair = Helper.FindDataTable(ds1, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                DataTable tblWorkspacePair = Helper.FindDataTable(ds2, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                if (tblPair != null && tblPair.Rows.Count > 0)
                {
                    maxPairID = tblPair.AsEnumerable().Max<DataRow>(T => Convert.ToInt64(T[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID]));
                    if (maxPairID >= pairID)
                        pairID = maxPairID + 1;
                }
                if (tblWorkspacePair != null && tblWorkspacePair.Rows.Count > 0)
                {
                    maxPairID = tblWorkspacePair.AsEnumerable().Max<DataRow>(T => Convert.ToInt64(T[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID]));
                    if (maxPairID >= pairID)
                        pairID = maxPairID + 1;
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000291);
            }
            return pairID;
        }

        /// <summary>
        /// Creates the pair table.
        /// </summary>
        /// <returns></returns>
        public static DataTable CreatePairTable()
        {
            DataTable dtlPair = new DataTable(MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            dtlPair.Columns.Add(new DataColumn(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID, typeof(long)));
            return dtlPair;
        }

        /// <summary>
        /// Adds the pair row.
        /// </summary>
        /// <param name="dtlPair">The DTL pair.</param>
        /// <param name="pairID">The pair ID.</param>
        public static void AddPairRow(DataTable dtlPair, long pairID)
        {
            if (dtlPair != null)
            {
                DataRow dr = dtlPair.NewRow();
                dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] = pairID;
                dtlPair.Rows.Add(dr);
            }
        }

        /// <summary>
        /// Sets the pair ID.
        /// </summary>
        /// <param name="drArray">The dr array.</param>
        /// <param name="pairID">The pair ID.</param>
        public static void SetPairID(DataRow[] drArray, long? pairID)
        {
            try
            {
                if (drArray != null && drArray.Length > 0)
                {
                    foreach (DataRow dr in drArray)
                    {
                        if (pairID.HasValue)
                            dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] = pairID.Value;
                        else
                            dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] = DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000292);
            }
        }

        /// <summary>
        /// Updates the pair ID and move rows.
        /// </summary>
        /// <param name="dtSource">The dt source.</param>
        /// <param name="dtTarget">The dt target.</param>
        /// <param name="dataRowsToMove">The data rows to move.</param>
        /// <param name="pairID">The pair ID.</param>
        public static void UpdatePairIDAndMoveRows(DataTable dtSource, DataTable dtTarget, DataRow[] dataRowsToMove, long? pairID)
        {
            try
            {
                if (dtSource != null && dtTarget != null && dataRowsToMove != null && dataRowsToMove.Length > 0)
                {
                    // Update Pair ID
                    MatchingHelper.SetPairID(dataRowsToMove, null);
                    // Copy Rows to Target
                    Helper.AddDataRows(dtTarget, dataRowsToMove);
                    // Select Newly Added Rows
                    DataRow[] dataRowsTarget = dtTarget.Select(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID + " IS NULL");
                    // Set New Pair ID
                    MatchingHelper.SetPairID(dataRowsTarget, pairID);
                    // Remove Rows from Source
                    Helper.RemoveDataRows(dtSource, dataRowsToMove);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000288);
            }
        }

        /// <summary>
        /// Updates the rec item number.
        /// </summary>
        /// <param name="oDataSet">The o data set.</param>
        /// <param name="oMatchSetGLDataRecItemInfoList">The o match set GL data rec item info list.</param>
        public static void UpdateRecItemNumber(DataSet oDataSet, List<MatchSetGLDataRecItemInfo> oMatchSetGLDataRecItemInfoListSource1, List<MatchSetGLDataRecItemInfo> oMatchSetGLDataRecItemInfoListSource2)
        {
            try
            {
                if (oDataSet != null)
                {
                    DataTable dtlSource1 = Helper.FindDataTable(oDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                    DataTable dtlSource2 = Helper.FindDataTable(oDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                    UpdateRecItemNumber(dtlSource1, oMatchSetGLDataRecItemInfoListSource1);
                    UpdateRecItemNumber(dtlSource2, oMatchSetGLDataRecItemInfoListSource2);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000299);
            }
        }

        /// <summary>
        /// Updates the rec item number.
        /// </summary>
        /// <param name="oDataTable">The o data table.</param>
        /// <param name="oMatchSetGLDataRecItemInfoList">The o match set GL data rec item info list.</param>
        public static void UpdateRecItemNumber(DataTable oDataTable, List<MatchSetGLDataRecItemInfo> oMatchSetGLDataRecItemInfoList)
        {
            try
            {
                if (oDataTable != null && oDataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in oDataTable.Rows)
                    {
                        dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = DBNull.Value;
                        dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE] = DBNull.Value;
                        long? excelRowNumber = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER]);
                        long? matchingSourceDataImportID = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCHING_SOURCE_DATA_IMPORT_ID]);
                        if (excelRowNumber.HasValue && matchingSourceDataImportID.HasValue && oMatchSetGLDataRecItemInfoList != null && oMatchSetGLDataRecItemInfoList.Count > 0)
                        {
                            MatchSetGLDataRecItemInfo oMatchSetGLDataRecItemInfo = oMatchSetGLDataRecItemInfoList.Find(T => T.ExcelRowNumber == excelRowNumber.Value && T.MatchingSourceDataImportID == matchingSourceDataImportID.Value);
                            if (oMatchSetGLDataRecItemInfo != null)
                            {
                                if (!string.IsNullOrEmpty(oMatchSetGLDataRecItemInfo.RecItemNumber))
                                    dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oMatchSetGLDataRecItemInfo.RecItemNumber;
                                if (oMatchSetGLDataRecItemInfo.CloseDate.HasValue)
                                    dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE] = oMatchSetGLDataRecItemInfo.CloseDate;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000299);
            }
        }

        /// <summary>
        /// Gets the data key names for un matched.
        /// </summary>
        /// <returns></returns>
        public static string[] GetDataKeyNamesForUnMatched()
        {
            return new string[] { MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER };
        }
        /// <summary>
        /// Gets the Client data key names for un matched.
        /// </summary>
        /// <returns></returns>
        public static string[] GetClientDataKeyNamesForUnMatched(List<MatchingConfigurationInfo> MatchingConfigurationInfoList)
        {
            string[] DataKeyNamesArr = new string[1];
            string AmountColumnKey = GetAmountColumnName(MatchingConfigurationInfoList);
            if (!string.IsNullOrEmpty(AmountColumnKey))
            {
                DataKeyNamesArr[0] = AmountColumnKey;
                return DataKeyNamesArr;
            }
            return null;
        }

        /// <summary>
        /// Gets the Amount Column Name.
        /// </summary>
        /// <returns></returns>
        public static string GetAmountColumnName(List<MatchingConfigurationInfo> MatchingConfigurationInfoList)
        {
            string AmountColumnName = null;
            if (MatchingConfigurationInfoList!=null)
            AmountColumnName = (from oMatchingConfigurationInfo in MatchingConfigurationInfoList
                                where oMatchingConfigurationInfo.IsAmountColumn.HasValue && oMatchingConfigurationInfo.IsAmountColumn.Value == true && oMatchingConfigurationInfo.IsDisplayedColumn.HasValue && oMatchingConfigurationInfo.IsDisplayedColumn.Value == true
                                select oMatchingConfigurationInfo.DisplayColumnName).FirstOrDefault();

            return AmountColumnName;
        }
        /// <summary>
        /// Gets the data key names for un matched.
        /// </summary>
        /// <returns></returns>
        public static string[] GetDataKeyNamesForMatched()
        {
            return new string[] { MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID };
        }
        #endregion

        #region Matching Data Import Section
        public static bool ValidateFileForMatchingDataImport(string filePath, string fileName, string fileExtension, ARTEnums.DataImportType dataImportType, out string exceptionMessage)
        {
            exceptionMessage = "";
            //** Validate Sheet Name
            string SheetName;
            SheetName = DataImportHelper.GetSheetName(dataImportType, null);
            if (!SharedDataImportHelper.IsDataSheetPresent(filePath, fileExtension, SheetName))
            {
                exceptionMessage = string.Format(Helper.GetLabelIDValue(5000307), fileName, SheetName);
                return false;
            }
            //** End
            //** Validate Column Name
            if (!MatchingHelper.IsValidColumnName(filePath, fileName, fileExtension, dataImportType, out exceptionMessage))
            {
                //exceptionMessage = string.Format(Helper.GetLabelIDValue(5000279), ": " + fileName);
                return false;
            }
            //** End
            if (dataImportType == ARTEnums.DataImportType.GLTBS)
            {
                if (!DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(dataImportType, filePath, fileExtension, null, SheetName, out exceptionMessage))
                    return false;
            }
            return true;
        }

        public static bool IsValidColumnName(string filePath, string fileName, string fileExtension, ARTEnums.DataImportType dataImportType, out string exceptionMessage)
        {
            string sheetName = WebConstants.MATCHING_SHEETNAME;
            string msg = Helper.GetLabelIDValue(5000306);
            StringBuilder invalidColumnName = new StringBuilder();
            string[] arryMandatoryFields = null;
            if (dataImportType == ARTEnums.DataImportType.GLTBS)
                arryMandatoryFields = DataImportHelper.GetGLTBSDataLoadMandatoryFields();

            DataTable dtFileColumn = DataImportHelper.GetSchemaDataTableForExcelFile(filePath, fileExtension, sheetName, false);
            if (dtFileColumn != null && dtFileColumn.Rows.Count > 0)
            {
                Regex reg = new Regex(@"^[a-zA-Z\s][a-zA-Z0-9-_#&\s]{0,100}$");
                for (int columnCount = 0; columnCount < dtFileColumn.Rows.Count; columnCount++)
                {
                    if (dataImportType == ARTEnums.DataImportType.GLTBS)
                    {
                        if (arryMandatoryFields != null
                            && !arryMandatoryFields.Contains<String>(dtFileColumn.Rows[columnCount]["COLUMN_NAME"].ToString().Trim()))
                        {
                            if (!reg.IsMatch(dtFileColumn.Rows[columnCount]["COLUMN_NAME"].ToString()))
                            {
                                if (invalidColumnName.ToString().Equals(string.Empty))
                                    invalidColumnName.Append(dtFileColumn.Rows[columnCount]["COLUMN_NAME"].ToString());
                                else
                                    invalidColumnName.Append(", " + dtFileColumn.Rows[columnCount]["COLUMN_NAME"].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (!reg.IsMatch(dtFileColumn.Rows[columnCount]["COLUMN_NAME"].ToString()))
                        {
                            if (invalidColumnName.ToString().Equals(string.Empty))
                                invalidColumnName.Append(dtFileColumn.Rows[columnCount]["COLUMN_NAME"].ToString());
                            else
                                invalidColumnName.Append(", " + dtFileColumn.Rows[columnCount]["COLUMN_NAME"].ToString());
                        }
                    }

                }
            }
            else
            {
                invalidColumnName.Append(LanguageUtil.GetValue(5000071));
            }
            exceptionMessage = String.Format(msg, fileName, invalidColumnName.ToString());
            return (invalidColumnName.ToString().Equals(string.Empty));
        }

        /// <summary>
        /// Get Rows Count 
        /// </summary>
        /// <returns></returns>
        public static int GetRowsCount(GridDataItem oGridDataItem, out int S1Rows, out int S2Rows)
        {
            int Source1Rows = 0;
            int Source2Rows = 0;
            int MaxRowsInSource = 0;
            //  row Count in both data sources
            int RowCount = 0;
            DataRow dr;
            DataRow[] childRowsSource1;
            DataRow[] childRowsSource2;
            if (oGridDataItem != null && oGridDataItem.DataItem != null)
            {
                dr = ((DataRowView)oGridDataItem.DataItem).Row;
                if (dr != null)
                {
                    childRowsSource1 = dr.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1);
                    childRowsSource2 = dr.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2);
                    RowCount += childRowsSource1.Length;
                    RowCount += childRowsSource2.Length;
                    Source1Rows = childRowsSource1.Length;
                    Source2Rows = childRowsSource2.Length;
                }
            }
            int MaxRowsInPair = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DISPLAY_RECORDS_LIMIT_IN_A_MATCHING_PAIR));
            MaxRowsInSource = MaxRowsInPair / 2;
            if (RowCount > MaxRowsInPair)
            {
                int ReminingS1 = 0;
                int ReminingS2 = 0;
                if (Source1Rows > MaxRowsInSource)
                    Source1Rows = MaxRowsInSource;
                else
                    ReminingS1 = MaxRowsInSource - Source1Rows;
                if (Source2Rows > MaxRowsInSource)
                    Source2Rows = MaxRowsInSource;
                else
                    ReminingS2 = MaxRowsInSource - Source2Rows;
                Source1Rows += ReminingS2;
                Source2Rows += ReminingS1;
            }
            S1Rows = Source1Rows;
            S2Rows = Source2Rows;
            return RowCount;
        }
        #endregion
        public static MatchSetHdrInfo GetMatchSetStatusMessage(MatchingParamInfo oMatchingParamInfo)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            MatchSetHdrInfo oMatchSetHdrInfo = oMatching.GetMatchSetStatusMessage(oMatchingParamInfo, Helper.GetAppUserInfo());
            return oMatchSetHdrInfo;
        }

        /// <summary>
        /// Convert GLDataRecItemInfo List To DataTable .
        /// </summary>

        public static DataTable ListToDataTable(IList<GLDataRecItemInfo> list)
        {
            DataTable table = new DataTable();
            if (list.Count > 0)
            {
                object obj = list[0];
                if (obj.GetType() == typeof(GLDataRecItemInfo))
                {
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(GLDataRecItemInfo));
                    for (int i = 0; i < props.Count; i++)
                    {
                        PropertyDescriptor prop = props[i];
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                    object[] values = new object[props.Count];
                    foreach (GLDataRecItemInfo item in list)
                    {
                        for (int i = 0; i < values.Length; i++)
                            values[i] = props[i].GetValue(item) ?? DBNull.Value; table.Rows.Add(values);
                    }
                }
            }
            return table;
        }
        /// <summary>
        /// Convert GLDataWriteOnOffInfo List To DataTable .
        /// </summary>

        public static DataTable ListToDataTable(IList<GLDataWriteOnOffInfo> list)
        {
            DataTable table = new DataTable();
            if (list.Count > 0)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(GLDataWriteOnOffInfo));
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                object[] values = new object[props.Count];
                foreach (GLDataWriteOnOffInfo item in list)
                {
                    for (int i = 0; i < values.Length; i++)
                        values[i] = props[i].GetValue(item) ?? DBNull.Value; table.Rows.Add(values);
                }
            }
            return table;
        }
        /// <summary>
        /// Convert GLDataRecurringItemScheduleInfo List To DataTable .
        /// </summary>

        public static DataTable ListToDataTable(IList<GLDataRecurringItemScheduleInfo> list)
        {
            DataTable table = new DataTable();
            if (list.Count > 0)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(GLDataRecurringItemScheduleInfo));
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                object[] values = new object[props.Count];
                foreach (GLDataRecurringItemScheduleInfo item in list)
                {
                    for (int i = 0; i < values.Length; i++)
                        values[i] = props[i].GetValue(item) ?? DBNull.Value; table.Rows.Add(values);
                }
            }
            return table;
        }

        /// <summary>
        /// Add Custom Column in Data Set Schema.
        /// </summary>
        /// <param name="oDataTable">The o data table.</param>
        /// <param name="oMatchSetGLDataRecItemInfoList">The o match set GL data rec item info list.</param>
        public static void AddCustomColumns(DataSet oDataSet)
        {
            try
            {
                for (int i = 0; i < oDataSet.Tables.Count; i++)
                {
                    DataTable oDataTable = oDataSet.Tables[i];
                    if (oDataTable != null)
                        AddCustomColumns(oDataTable);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000318);
            }
        }

        /// <summary>
        /// Add Custom Column in Data Table.
        /// </summary>
        /// <param name="oDataTable">The o data table.</param>
        /// <param name="oMatchSetGLDataRecItemInfoList">The o match set GL data rec item info list.</param>
        public static void AddCustomColumns(DataTable oDataTable)
        {
            try
            {
                if (!(oDataTable.Columns.IndexOf(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE) >= 0))
                    oDataTable.Columns.Add(new DataColumn(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE, typeof(string)));
                if (!(oDataTable.Columns.IndexOf(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE) >= 0))
                    oDataTable.Columns.Add(new DataColumn(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE, typeof(decimal)));
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000318);
            }
        }
        public static DataTable ManageColumnsForExporToExcel(DataTable CombinedDataTable, List<MatchingConfigurationInfo> MatchingConfigurationInfoList)
        {
            DataTable FormateDataTable = null;
            List<MatchingConfigurationInfo> NoNDisplayMatchingConfigurationInfoList;
            NoNDisplayMatchingConfigurationInfoList = MatchingConfigurationInfoList.FindAll(o => o.IsDisplayedColumn.HasValue && o.IsDisplayedColumn == false);
            for (int i = 0; i < NoNDisplayMatchingConfigurationInfoList.Count - 1; i++)
            {
                string DispalycolumnName1 = NoNDisplayMatchingConfigurationInfoList[i].ColumnName1;
                MatchingHelper.TestAndRemove(CombinedDataTable, DispalycolumnName1);
                string DispalycolumnName2 = NoNDisplayMatchingConfigurationInfoList[i].ColumnName2;
                MatchingHelper.TestAndRemove(CombinedDataTable, DispalycolumnName2);

            }
            MatchingHelper.TestAndRemove(CombinedDataTable, "__ExcelRowNumber");
            MatchingHelper.TestAndRemove(CombinedDataTable, "__MatchSetMatchingSourceDataImportID");
            MatchingHelper.TestAndRemove(CombinedDataTable, "__IsAutomaticMatch");
            MatchingHelper.TestAndRemove(CombinedDataTable, "__MatchingSourceDataImportID");
            MatchingHelper.TestAndRemove(CombinedDataTable, "__RecItemNumber");
            MatchingHelper.TestAndRemove(CombinedDataTable, "__CloseDate");
            //convert DataTable to DataView   
            if (CombinedDataTable.Rows.Count > 0)
            {
                DataView dv = CombinedDataTable.DefaultView;
                //apply the sort on CustomerSurname column   
                dv.Sort = "__PairID";
                //save our newly ordered results back into our datatable   
                CombinedDataTable = dv.ToTable();
            }
            FormateDataTable = GetFormateDataTable(CombinedDataTable);
            FormateDataTable.Columns["__PairID"].SetOrdinal(0);
            FormateDataTable.Columns["__NetValue"].SetOrdinal(1);
            FormateDataTable.AcceptChanges();
            FormateDataTable.Columns["__PairID"].ColumnName = "PairID";
            FormateDataTable.Columns["__DataSourceName"].ColumnName = "DataSourceName";
            FormateDataTable.Columns["__NetValue"].ColumnName = "NetValue";
            return FormateDataTable;
        }
        public static void TestAndRemove(DataTable Dt, string colToRemove)
        {
            if (colToRemove != null)
            {
                DataColumnCollection columns;
                // Get the DataColumnCollection from a DataTable in a DataSet.
                columns = Dt.Columns;
                if (columns.Contains(colToRemove))
                {
                    columns.Remove(colToRemove);
                }
            }
        }
        public static void ExportToExcel(DataTable dt, string filename1)
        {
            string filename = filename1 + ".xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();
            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.               
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            dgGrid.EnableViewState = false;
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();

        }
        public static DataTable GetFormateDataTable(DataTable dt)
        {
            DataTable FormatedTable = new DataTable();
            foreach (DataColumn dc in dt.Columns)
            {
                string DataColumnName = dc.ColumnName.ToString();
                FormatedTable.Columns.Add(new DataColumn(DataColumnName, typeof(string)));
            }
            if (dt.Rows.Count > 0)
            {
                DataRow row = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = FormatedTable.NewRow();
                    foreach (DataColumn dc in dt.Rows[i].Table.Columns)
                    {
                        string DataColumnDataType = dc.DataType.ToString();
                        string DataColumnName = dc.ColumnName.ToString();
                        string ColumnData;
                        switch (DataColumnDataType)
                        {
                            case "System.DateTime":
                                if (dt.Rows[i][DataColumnName] != DBNull.Value)
                                {
                                    ColumnData = Helper.GetDisplayDate(Convert.ToDateTime(dt.Rows[i][DataColumnName]));
                                    row[DataColumnName] = ColumnData;
                                }
                                break;
                            case "System.Decimal":
                                if (dt.Rows[i][DataColumnName] != DBNull.Value)
                                {
                                    ColumnData = Helper.GetDisplayDecimalValue(Convert.ToDecimal(dt.Rows[i][DataColumnName]));
                                    row[DataColumnName] = ColumnData;
                                }
                                break;
                            case "System.Int32":
                                if (dt.Rows[i][DataColumnName] != DBNull.Value)
                                {
                                    ColumnData = Helper.GetDisplayIntegerValue(Convert.ToInt32(dt.Rows[i][DataColumnName]));
                                    row[DataColumnName] = ColumnData;
                                }
                                break;
                            default:
                                if (dt.Rows[i][DataColumnName] != DBNull.Value)
                                {
                                    ColumnData = Convert.ToString(dt.Rows[i][DataColumnName]);
                                    row[DataColumnName] = ColumnData;
                                }
                                break;
                        }
                    }
                    FormatedTable.Rows.Add(row);
                }
            }
            return FormatedTable;
        }

        /// <summary>
        /// Creates Rec Item Net Value column
        /// </summary>
        /// <param name="oExRadGrid"></param>
        public static void CreateRecItemNetValueColumn(ExRadGrid oExRadGrid)
        {
            try
            {
                string hdrText = LanguageUtil.GetValue(2026);
                ExGridTemplateColumn col = (ExGridTemplateColumn)oExRadGrid.Columns.FindByUniqueNameSafe(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE);
                if (col == null)
                {
                    col = Helper.CreateDynamicExGridTemplateColumn(hdrText, WebEnums.DataType.Decimal, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_ID_DATA_SOURCE_NAME, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE, MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE);
                    col.ItemStyle.CssClass = "GridClientSelectColumnCSS";
                    oExRadGrid.Columns.Add(col);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000286);
            }
        }

        /// <summary>
        /// Calculate NetValue for each matchset pair
        /// </summary>
        /// <param name="sourceDS"></param>
        /// <param name="sourceTable"></param>
        /// <param name="amountColumn"></param>
        /// <returns></returns>

        public static DataTable CalculatePairNetValue(DataSet sourceDS, DataTable sourceTable, string amountColumn)
        {
            if (amountColumn != null)
            {
                decimal tmpVal;
                decimal tmpVal2;
                DataTable dtSource1 = Helper.FindDataTable(sourceDS, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);

                var groupQuery = from table in dtSource1.AsEnumerable()
                                 group table by table[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] into groupedTable
                                 select new
                                 {
                                     x = groupedTable.Key,
                                     y = groupedTable.Sum(o => (o[amountColumn] != System.DBNull.Value && decimal.TryParse(o[amountColumn].ToString(), out tmpVal) ? tmpVal : 0))
                                 };


                DataTable dtSource2 = Helper.FindDataTable(sourceDS, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

                var groupQuery1 = from table in dtSource2.AsEnumerable()
                                  group table by table[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] into groupedTable
                                  select new
                                  {
                                      x = groupedTable.Key,
                                      y = groupedTable.Sum(o => (o[amountColumn] != System.DBNull.Value && decimal.TryParse(o[amountColumn].ToString(), out tmpVal2) ? tmpVal2 : 0))
                                  };



                var quryfinal = from R1 in groupQuery.AsEnumerable()
                                join R2 in groupQuery1.AsEnumerable()
                                on R1.x equals R2.x
                                select new
                                {
                                    A = R1.x,
                                    B = R1.y - R2.y
                                };

                var dataFinal = from p in sourceTable.AsEnumerable()
                                join f in quryfinal.AsEnumerable()
                                on p[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] equals f.A
                                select new { p, f };
                foreach (var netValues in dataFinal)
                {
                    netValues.p[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE] = netValues.f.B;
                }
                sourceTable.AcceptChanges();
            }
            return sourceTable;

        }

        public static MatchSetSubSetCombinationInfoForNetAmountCalculation GetMatchSetSubSetCombinationForNetAmountCalculationByID(Int64? CombinationID)
        {
            IMatching oMatching = RemotingHelper.GetMatchingObject();
            MatchSetSubSetCombinationInfoForNetAmountCalculation oMatchSetSubSetCombinationInfoForNetAmountCalculation = oMatching.GetMatchSetSubSetCombinationForNetAmountCalculationByID(CombinationID, Helper.GetAppUserInfo());
            return oMatchSetSubSetCombinationInfoForNetAmountCalculation;
        }
        public static void UpdateNetValue(DataTable sourceDT, DataTable DestDT)
        {
            var enumDest = DestDT.AsEnumerable();
            var enumSourceDT = sourceDT.AsEnumerable();
            var employeeDept = from empl in enumDest join d in enumSourceDT on empl.Field<int>(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID) equals d.Field<int>(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID) select new { enumDest = empl, enumSourceDT = d };
            foreach (var ed in employeeDept)
            {
                ed.enumDest.SetField<decimal?>(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE, ed.enumSourceDT.Field<decimal?>(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE));
            }        

        }


    }
}
