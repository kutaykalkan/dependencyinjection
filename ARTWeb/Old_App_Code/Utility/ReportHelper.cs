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
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using System.Collections;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model.Report;
using SkyStem.Library.Controls.TelerikWebControls;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SkyStem.ART.App.Services;
using SkyStem.ART.Client.Exception;
using Telerik.Web.UI;
using System.Text;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for ReportHelper
    /// </summary>
    public class ReportHelper
    {
        public ReportHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static readonly string FilterValueSeparator = Helper.FilterValueSeparator;

        /// <summary>
        /// Returns report parameters
        /// </summary>
        /// <param name="reportID"></param>
        public static IList<ReportParameterInfo> GetReportParameterInfoCollectionByReportID(short reportID)
        {
            ReportParameterParamInfo oReportParameterParamInfo = new ReportParameterParamInfo();
            string reportRecPeriodID = ReportHelper.GetSelectedRecPeriodID();
            int? recPeriodID = null;
            if (string.IsNullOrEmpty(reportRecPeriodID))
            {
                recPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            }
            else
            {
                recPeriodID = Convert.ToInt32(reportRecPeriodID);
            }

            oReportParameterParamInfo.RecPeriodID = recPeriodID;
            oReportParameterParamInfo.ReportID = reportID;
            oReportParameterParamInfo.CompanyID = SessionHelper.CurrentCompanyID;

            IReportParameter oReportParameter = RemotingHelper.GetReportParameterObject();
            return oReportParameter.GetReportParametersByReportID(oReportParameterParamInfo, Helper.GetAppUserInfo());

        }


        public static ReportMstInfo GetReportInfoByReportID(short? reportID, int languageID, int businessEntityID, int defaultLanguageID)
        {
            IReport oReportClient = RemotingHelper.GetReportObject();
            return oReportClient.GetReportByID(reportID, languageID, businessEntityID, defaultLanguageID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        }

        //Get IDs of Controls to be loaded
        public static List<ReportParameterInfo> GetRptParamControls(short reportID)
        {
            List<ReportParameterInfo> oReportParams = null;
            oReportParams = (List<ReportParameterInfo>)ReportHelper.GetReportParameterInfoCollectionByReportID(reportID);
            return oReportParams;
        }

        public static List<short> GetRptParamControlIDs(short reportID)
        {
            List<short> oRptParamControlIDCollection = new List<short>();
            List<ReportParameterInfo> oReportParams = null;
            oReportParams = (List<ReportParameterInfo>)ReportHelper.GetReportParameterInfoCollectionByReportID(reportID);

            foreach (ReportParameterInfo oRptParamInfo in oReportParams)
            {
                short rptParamID = oRptParamInfo.ParameterID;
                if (rptParamID > 0)
                {
                    oRptParamControlIDCollection.Add(rptParamID);
                }
            }
            return oRptParamControlIDCollection;
        }

        public static ListItemCollection GetListItemCollectionForReason()
        {
            List<ReasonMstInfo> oReasonCollection = SessionHelper.GetAllReasons();
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (ReasonMstInfo oReason in oReasonCollection)
            {
                ListItem item = new ListItem();
                item.Value = oReason.ReasonID.Value.ToString();
                item.Text = oReason.Reason;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }
        public static ListItemCollection GetListItemCollectionForAging()
        {
            List<AgingCategoryMstInfo> oAgingCategoryCollection = SessionHelper.GetAllAgingCategories();
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (AgingCategoryMstInfo oAging in oAgingCategoryCollection)
            {
                ListItem item = new ListItem();
                item.Value = oAging.AgingCategoryID.ToString();
                item.Text = oAging.AgingCategoryName;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }

        public static ListItemCollection GetListItemCollectionForRangeOfScore()
        {
            List<RangeOfScoreMstInfo> oRangeCollection = SessionHelper.GetRangeOfScores();
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (RangeOfScoreMstInfo oRange in oRangeCollection)
            {
                ListItem item = new ListItem();
                item.Value = oRange.RangeOfScoreCategoryID.ToString();
                item.Text = oRange.RangeOfScoreCategoryName;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }

        public static ListItemCollection GetListItemCollectionForQualityScoreChecklist(int RecPeriodID)
        {
            List<QualityScoreChecklistInfo> oChecklistCollection = SessionHelper.GetQualityScoreChecklist(RecPeriodID);
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (QualityScoreChecklistInfo oCheckList in oChecklistCollection)
            {
                ListItem item = new ListItem();
                item.Value = oCheckList.QualityScoreID.ToString();
                item.Text = oCheckList.QualityScoreDescription;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }

        public static ListItemCollection GetListItemCollectionForOpenItemClassification()
        {
            List<ReconciliationCategoryMstInfo> oOpenItemCollection = SessionHelper.GetOpenItemClassifications();
            ListItemCollection lstListItem = new ListItemCollection();
            foreach (ReconciliationCategoryMstInfo oOpenItem in oOpenItemCollection)
            {
                ListItem item = new ListItem();
                item.Value = oOpenItem.ReconciliationCategoryID.Value.ToString();
                //2703:-Write offs
                if (oOpenItem.ReconciliationCategoryID.Value == (short)WebEnums.RecCategory.VariancesWriteOffOn)
                    item.Text = LanguageUtil.GetValue(2703);
                else
                    item.Text = oOpenItem.ReconciliationCategory;
                lstListItem.Add(item);
            }
            return lstListItem;
        }
        public static ListItemCollection GetListItemCollectionForRecStatus()
        {
            List<ReconciliationStatusMstInfo> oRecStatusCollection = SessionHelper.GetAllRecStatus();
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (ReconciliationStatusMstInfo oRecStatus in oRecStatusCollection)
            {
                ListItem item = new ListItem();
                item.Value = oRecStatus.ReconciliationStatusID.Value.ToString();
                item.Text = oRecStatus.ReconciliationStatus;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }

        public static ListItemCollection GetListItemCollectionForTaskStatus()
        {
            List<TaskStatusMstInfo> oTaskStatusMstInfoList = SessionHelper.GetTaskStatus();
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (TaskStatusMstInfo oTaskStatus in oTaskStatusMstInfoList)
            {
                if (oTaskStatus.TaskStatusID.Value != (short)ARTEnums.TaskStatus.Deleted)
                {
                    ListItem item = new ListItem();
                    item.Value = oTaskStatus.TaskStatusID.Value.ToString();
                    item.Text = oTaskStatus.TaskStatus;
                    oLstCollection.Add(item);
                }
            }
            return oLstCollection;
        }

        public static ListItemCollection GetListItemCollectionForTaskListName(int recPeriodID)
        {
            List<TaskListHdrInfo> oTaskListHdrInfoList = TaskMasterHelper.GetTaskListHdrInfoCollection(recPeriodID);
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (TaskListHdrInfo oItem in oTaskListHdrInfoList)
            {
                ListItem item = new ListItem();
                item.Value = oItem.TaskListID.Value.ToString();
                item.Text = oItem.TaskListName;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }

        public static ListItemCollection GetListItemCollectionForDisplayColumn(Int16 reportID, bool? IsOptional, bool? IsRemoveAccountTaskColumn)
        {
            List<ReportColumnInfo> oReportColumnInfoList = new List<ReportColumnInfo>();
            // Hide Account related Columns
            List<ReportColumnInfo> oAllReportColumnInfoList = SessionHelper.GetReportColumnInfoList(reportID, IsOptional);
            if (IsRemoveAccountTaskColumn.Value)
            {
                oReportColumnInfoList = (from item in oAllReportColumnInfoList
                                         where item.ColumnTypeID != 1  //1 for Account column
                                         select item).ToList();
            }
            else
            {
                oReportColumnInfoList = oAllReportColumnInfoList;
            }

            ListItemCollection oLstCollection = new ListItemCollection();
            if (oReportColumnInfoList != null && oReportColumnInfoList.Count > 0)
            {
                foreach (ReportColumnInfo oItem in oReportColumnInfoList)
                {
                    ListItem item = new ListItem();
                    item.Value = oItem.ColumnID.ToString();
                    item.Text = oItem.ColumnName;
                    oLstCollection.Add(item);
                }
            }
            return oLstCollection;
        }

        public static void BindDropDownListForTaskType(DropDownList ddlTaskType)
        {
            ListControlHelper.BindDropDownListForTaskType(ddlTaskType, true, false);
        }

        public static ListItemCollection GetListItemCollectionForRiskRating()
        {
            List<RiskRatingMstInfo> oRiskRatingCollection = SessionHelper.GetAllRiskRating();
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (RiskRatingMstInfo oRiskRating in oRiskRatingCollection)
            {
                ListItem item = new ListItem();
                item.Value = oRiskRating.RiskRatingID.Value.ToString();
                item.Text = oRiskRating.Name;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }
        public static ListItemCollection GetListItemCollectionForUser(List<short> selectedRoleIDs, string selectedRecPeriod)
        {
            ListItemCollection oLstCollection = new ListItemCollection();
            IUser oUserClient = RemotingHelper.GetUserObject();
            int recPeriodID;
            if (!string.IsNullOrEmpty(selectedRecPeriod) && (selectedRecPeriod == WebConstants.CURRENT_REC_PERIOD_INDEX))
            {
                //SelectAllUsersByCompanyIDAndRoleIDsForCurrentRecPeriod
                List<UserHdrInfo> oUserCollection = oUserClient.SelectAllUsersByCompanyIDAndRoleIDsForCurrentRecPeriod(SessionHelper.CurrentCompanyID.Value, selectedRoleIDs, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());


                foreach (UserHdrInfo oUser in oUserCollection)
                {
                    ListItem item = new ListItem();
                    item.Value = oUser.UserID.Value.ToString();
                    item.Text = oUser.FirstName + " " + oUser.LastName;
                    oLstCollection.Add(item);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(selectedRecPeriod))
                {
                    recPeriodID = Convert.ToInt32(selectedRecPeriod);
                    List<UserHdrInfo> oUserCollection = oUserClient.SelectAllUsersByCompanyIDRoleIDs(SessionHelper.CurrentCompanyID.Value, selectedRoleIDs, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, recPeriodID, Helper.GetAppUserInfo());


                    foreach (UserHdrInfo oUser in oUserCollection)
                    {
                        ListItem item = new ListItem();
                        item.Value = oUser.UserID.Value.ToString();
                        item.Text = oUser.FirstName + " " + oUser.LastName;
                        oLstCollection.Add(item);
                    }
                }
            }
            return oLstCollection;
        }
        public static ListItemCollection GetListItemCollectionForRole()
        {
            List<RoleMstInfo> oRoleMstInfoCollection = SessionHelper.GetAllRoles();
            ListItemCollection lstListItem = new ListItemCollection();
            foreach (RoleMstInfo oRoleMstInfo in oRoleMstInfoCollection)
            {
                lstListItem.Add(new ListItem(oRoleMstInfo.Role, oRoleMstInfo.RoleID.Value.ToString()));
            }
            return lstListItem;
        }
        public static ListItemCollection GetListItemCollectionForRoleByPermittedRoles(List<short> permittedRolesList)
        {
            List<RoleMstInfo> oRoleMstInfoCollection = SessionHelper.GetAllRoles();
            ListItemCollection lstListItem = new ListItemCollection();
            foreach (RoleMstInfo oRoleMstInfo in oRoleMstInfoCollection)
            {
                short roleID = oRoleMstInfo.RoleID.Value;
                if (permittedRolesList.Contains(roleID))
                {
                    if ((roleID == (short)ARTEnums.UserRole.BACKUP_PREPARER || roleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER
                        || roleID == (short)ARTEnums.UserRole.BACKUP_APPROVER) && !Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                    { }
                    else
                    {
                        lstListItem.Add(new ListItem(oRoleMstInfo.Role, oRoleMstInfo.RoleID.Value.ToString()));
                    }
                }
            }
            return lstListItem;
        }

        public static List<short> GetPermittedRolesByReportID(short reportID, short currentUserRole, int? recPeriodIDForReport)
        {
            bool isDualReview = false;
            isDualReview = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.DualLevelReview, recPeriodIDForReport.Value, false);

            IReportParameter oReportParameter = RemotingHelper.GetReportParameterObject();
            return oReportParameter.GetPermittedRolesByReportID(reportID, currentUserRole, recPeriodIDForReport, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());

        }
        public static ListItemCollection GetListItemCollectionForPreparer()
        {
            List<RoleMstInfo> oRoleMstInfoCollection = SessionHelper.GetAllRoles();
            ListItemCollection lstListItem = new ListItemCollection();
            foreach (RoleMstInfo oRoleMstInfo in oRoleMstInfoCollection)
            {
                if ((oRoleMstInfo.RoleID.Value == (short)ARTEnums.UserRole.PREPARER)
                    || (oRoleMstInfo.RoleID.Value == (short)ARTEnums.UserRole.BACKUP_PREPARER
                    && Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID)))
                {
                    lstListItem.Add(new ListItem(oRoleMstInfo.Role, oRoleMstInfo.RoleID.Value.ToString()));
                }
            }
            return lstListItem;
        }

        public static string GetCriteriaForCriteriaKey(Dictionary<string, string> oRptCriteria, string criteriaKey)
        {
            string result = string.Empty;
            if (oRptCriteria.ContainsKey(criteriaKey))
            {
                result = oRptCriteria[criteriaKey];
            }
            return result;
        }

        public static string SetYesNoCodeBasedOnBool(bool? input)
        {
            if (input == null)
            {
                return WebConstants.HYPHEN;
            }
            else if (input == true)
            {
                return LanguageUtil.GetValue(WebConstants.LABEL_ID_YES);
            }
            else
            {
                return LanguageUtil.GetValue(WebConstants.LABEL_ID_NO);
            }
        }

        public static string SetMarkIfItemMissingBasedOnBool(bool? input)
        {
            if (input == null || input == true)
            {
                return " x ";
            }
            else
            {
                return "";
            }
        }


        public static DataTable GetEntitySearchCriteria(Dictionary<string, string> oCriteriaCollection)
        {
            DataTable dt = new DataTable("IDTable");
            DataColumn dcKeyID = dt.Columns.Add("KeyID");
            DataColumn dcValue = dt.Columns.Add("Value");
            DataRow dr = null;
            foreach (KeyValuePair<string, string> keyValuePair in oCriteriaCollection)
            {
                switch (keyValuePair.Key)
                {
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key2);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY3:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key3);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY4:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key4);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY5:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key5);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY6:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key6);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY7:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key7);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY8:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key8);
                        break;
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY9:
                        AddSameLevelEntityInTable(keyValuePair, dt, dr, WebEnums.GeographyClass.Key9);
                        break;
                }
            }
            return dt;
        }

        public static DataTable GetUserSearchCriteria(Dictionary<string, string> oCriteriaCollection)
        {
            if (oCriteriaCollection.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER))
                return ConvertStringToIDDataTable(oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER]);
            else
                return new DataTable();

        }


        public static DataTable GetRoleSearchCriteria(Dictionary<string, string> oCriteriaCollection)
        {
            if (oCriteriaCollection.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE))
                return ConvertStringToIDDataTable(oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE]);
            else
                return new DataTable();

        }

        private static DataTable ConvertStringToIDDataTable(string stringValue)
        {
            DataTable dt = new DataTable("IDTable");
            string columnName = "ID";
            DataColumn dcKeyID = dt.Columns.Add(columnName);
            DataRow dr = null;
            if (!string.IsNullOrEmpty(stringValue))
            {
                string[] arrayKeyValue = stringValue.Split(ReportHelper.FilterValueSeparator.ToCharArray());
                if (arrayKeyValue != null && arrayKeyValue.Length > 0)
                {
                    for (int i = 0; i < arrayKeyValue.Length; i++)
                    {
                        dr = dt.NewRow();
                        dr[columnName] = arrayKeyValue[i];
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }



        private static Dictionary<string, string> GetReportCriteria()
        {
            Dictionary<string, string> oCriteriaCollection = (Dictionary<string, string>)HttpContext.Current.Session[SessionConstants.REPORT_CRITERIA];
            return oCriteriaCollection;
        }

        private static string GetSelectedRecPeriodID()
        {
            Dictionary<string, string> oCriteriaCollection = ReportHelper.GetReportCriteria();
            if (oCriteriaCollection != null)
            {
                return oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD];
            }
            return null;
        }

        private static string GetSelectedCriteriaValueForKey(string criteriaKey)
        {
            Dictionary<string, string> oCriteriaCollection = ReportHelper.GetReportCriteria();
            return oCriteriaCollection[criteriaKey];
        }

        private static Dictionary<string, string> GetUserSearchCriteriaWhenNoUserSelected(short reportID)
        {
            int recPeriodID = Convert.ToInt32(ReportHelper.GetSelectedRecPeriodID());
            Dictionary<string, string> _oCriteriaCollection = new Dictionary<string, string>();

            List<short> oRoleCollection = ReportHelper.GetPermittedRolesByReportID(reportID, SessionHelper.CurrentRoleID.Value, recPeriodID);
            ListItemCollection oListItemUserCollection = ReportHelper.GetListItemCollectionForUser(oRoleCollection, recPeriodID.ToString());
            StringBuilder strRoleList = new StringBuilder();
            StringBuilder strUserList = new StringBuilder();
            if (oRoleCollection != null && oRoleCollection.Count > 0)
            {
                foreach (short roleID in oRoleCollection)
                {
                    strRoleList.Append(roleID + ReportHelper.FilterValueSeparator);
                }
                strRoleList.Remove(strRoleList.Length - 1, 1);
            }
            if (oListItemUserCollection != null && oListItemUserCollection.Count > 0)
            {
                foreach (ListItem oListItem in oListItemUserCollection)
                {
                    strUserList.Append(oListItem.Value + ReportHelper.FilterValueSeparator);
                }
                strUserList.Remove(strUserList.Length - 1, 1);
            }
            _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER, strUserList.ToString());
            _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE, strRoleList.ToString());
            //And all others, if null is not allowed
            return _oCriteriaCollection;
        }

        private static void GetUserSearchCriteriaWhenOnlyRoleSelected()
        {
            Dictionary<string, string> oCriteriaCollection = ReportHelper.GetReportCriteria();
            List<Int16> oRoleCollection = new List<short>();

            string roleIDList = ReportHelper.GetSelectedCriteriaValueForKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE);
            string[] roleIDArray = roleIDList.Split(FilterValueSeparator.ToCharArray());
            for (int i = 0; i < roleIDArray.Length; i++)
            {
                oRoleCollection.Add(Convert.ToInt16(roleIDArray[i]));
            }
            ListItemCollection oListItemUserCollection = ReportHelper.GetListItemCollectionForUser(oRoleCollection, ReportHelper.GetSelectedRecPeriodID());

            StringBuilder strUserList = new StringBuilder();
            if (oListItemUserCollection != null && oListItemUserCollection.Count > 0)
            {
                foreach (ListItem oListItem in oListItemUserCollection)
                {
                    strUserList.Append(oListItem.Value + FilterValueSeparator);
                }
                strUserList.Remove(strUserList.Length - 1, 1);
            }
            oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER] = strUserList.ToString();
        }

        public static void SendUserSearchCriteriaWhenNoUserSelected(short reportID, ref DataTable dtUser, ref DataTable dtRole)
        {
            // If both Roles and User are Not selected then pick the Default Role and User List
            if (dtUser != null && dtUser.Rows.Count <= 0)
            {
                if (dtRole != null && dtRole.Rows.Count <= 0)
                {
                    Dictionary<string, string> _oCriteriaCollectionNotFromSession = ReportHelper.GetUserSearchCriteriaWhenNoUserSelected(reportID);
                    dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollectionNotFromSession);
                    dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollectionNotFromSession);
                }
                else
                {
                    // means only Roles selected, so pick users based on Selected Roles
                    ReportHelper.GetUserSearchCriteriaWhenOnlyRoleSelected();
                    dtUser = ReportHelper.GetUserSearchCriteria(ReportHelper.GetReportCriteria());
                }
            }
        }

        private static void AddSameLevelEntityInTable(KeyValuePair<string, string> keyValuePair, DataTable dt, DataRow dr, WebEnums.GeographyClass eGeographyClass)
        {

            if (!string.IsNullOrEmpty(keyValuePair.Value))
            {
                string[] arrayKeyValue = keyValuePair.Value.Split(',');
                if (arrayKeyValue != null && arrayKeyValue.Length > 0)
                {
                    for (int i = 0; i < arrayKeyValue.Length; i++)
                    {
                        dr = dt.NewRow();
                        //dr["KeyID"] = keyValuePair.Key.Substring(keyValuePair.Key.Length - 1, 1);
                        dr["KeyID"] = (short)eGeographyClass;
                        dr["Value"] = arrayKeyValue[i];
                        dt.Rows.Add(dr);
                    }
                }
            }
        }

        public static void SetTextForLabel(ExLabel oExLabel, string value)
        {
            if (oExLabel != null)
            {
                oExLabel.Text = value;
            }
        }

        public static short GetRptParamKeyIDForParamKey(string keyName)
        {
            List<ReportParameterKeyMstInfo> oRptParamKeyInfoCollection = CacheHelper.GetAllReportParameterKeys();
            ReportParameterKeyMstInfo oRptParamKeyInfo = oRptParamKeyInfoCollection.Find(r => r.ReportParameterKeyName == keyName);
            return oRptParamKeyInfo.ReportParameterKeyID.Value;
        }

        public static void SetTextForLabel(ExLabel oExLabel, Int16? value)
        {
            if (value != null)
            {
                ReportHelper.SetTextForLabel(oExLabel, Helper.GetDisplayStringValue(value.Value.ToString()));
            }
        }

        public static void SetTextForLabel(ExLabel oExLabel, Decimal? value)
        {
            if (value != null)
            {
                ReportHelper.SetTextForLabel(oExLabel, Helper.GetDisplayStringValue(value.Value.ToString()));
            }
        }
        public static bool? GetBoolValueFromKeyValue(string value)
        {
            bool? returnValue = null;
            if (!string.IsNullOrEmpty(value))// && (value is bool ))
            {
                switch (value)
                {
                    case WebConstants.RPT_PRM_NO:
                        returnValue = false;
                        break;
                    case WebConstants.RPT_PRM_YES:
                        returnValue = true;
                        break;
                    case WebConstants.RPT_PRM_ALL:
                    default:
                        returnValue = null;
                        break;
                }

            }
            return returnValue;
        }
        public static string GetDisplayCertificationTrackingReportDate(DateTime? oDate, DateTime? CertificationStartDate)
        {
            if (oDate == null || oDate == default(DateTime))
            {
                if (CertificationStartDate.HasValue && CertificationStartDate.Value < DateTime.Now)
                {
                    return LanguageUtil.GetValue(1904);
                }
                else
                {
                    return WebConstants.HYPHEN;
                }
            }
            else
            {
                // Todo: This would change to have Multi-lingual Phrase
                return Helper.GetDisplayDateTime(oDate);
            }
        }
        public static void SetParameterValueForRequesterUserAndLanguage(ReportSearchCriteria oReportSearchCriteria)
        {
            oReportSearchCriteria.LCID = SessionHelper.GetUserLanguage();
            oReportSearchCriteria.BusinessEntityID = SessionHelper.GetBusinessEntityID();
            oReportSearchCriteria.DefaultLanguageID = AppSettingHelper.GetDefaultLanguageID();
            oReportSearchCriteria.RequesterUserID = SessionHelper.CurrentUserID.Value;
            oReportSearchCriteria.RequesterRoleID = SessionHelper.CurrentRoleID.Value;
        }

        public static void SetReportRadGridProperty(ExRadGrid rgReport)
        {
            rgReport.AllowCustomPaging = false;
            rgReport.AllowPaging = true;
            rgReport.PagerStyle.AlwaysVisible = true;
            rgReport.AllowSorting = true;
        }

        public static DateTime? GetDateValueFromKeyValue(string value)
        {
            DateTime? returnValue = null;
            if (!string.IsNullOrEmpty(value))// && (value is DateTime)
            {
                returnValue = Convert.ToDateTime(value);
            }
            return returnValue;
        }

        public static byte[] GetBinarySerializedReportData(object obj)
        {

            MemoryStream oMemStream = new MemoryStream();
            BinaryFormatter oBinaryFormatter = new BinaryFormatter();
            byte[] oByteArry = null;
            try
            {
                #region "Old Code"
                //switch (reportID)
                //{
                //    case (short)WebEnums.Reports.UNUSUAL_BALANCES:
                //        oBinaryFormatter.Serialize(oMemStream, (List<UnusualBalancesReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    case (short)WebEnums.Reports.OPEN_ITEM:
                //        oBinaryFormatter.Serialize(oMemStream, (List<OpenItemsReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    //case (short)WebEnums.Reports.EXCEPTION_STATUS:
                //    //    oBinaryFormatter.Serialize(oMemStream, (List<ExceptionS>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //    //    break;
                //    case (short)WebEnums.Reports.ACCOUNT_STATUS:
                //        oBinaryFormatter.Serialize(oMemStream, (List<AccountStatusReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    case (short)WebEnums.Reports.CERTIFICATION_TRACKING:
                //        oBinaryFormatter.Serialize(oMemStream, (List<CertificationTrackingReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    case (short)WebEnums.Reports.RECONCILIATION_STATUS_COUNT_BY_USER_ROLE:
                //        oBinaryFormatter.Serialize(oMemStream, (List<ReconciliationStatusCountReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    case (short)WebEnums.Reports.ACCOUNT_OWNERSHIP:
                //        oBinaryFormatter.Serialize(oMemStream, (List<AccountOwnershipReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    case (short)WebEnums.Reports.UNASSIGNED_ACCOUNTS:
                //        oBinaryFormatter.Serialize(oMemStream, (List<UnassignedAccountsReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    case (short)WebEnums.Reports.INCOMPLETE_ACCOUNT_ATTRIBUTE:
                //        oBinaryFormatter.Serialize(oMemStream, (List<IncompleteAccountAttributeReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //    case (short)WebEnums.Reports.DELINQUENT_ACCOUNTS_REPORT:
                //        oBinaryFormatter.Serialize(oMemStream, (List<DelinquentAccountByUserReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA]);
                //        break;
                //}
                #endregion
                if (obj != null)
                {
                    oBinaryFormatter.Serialize(oMemStream, obj);
                    oMemStream.Seek(0, 0);
                }
                oByteArry = oMemStream.ToArray();
            }
            finally
            {
                if (oMemStream != null)
                    oMemStream.Close();
            }

            return oByteArry;
        }
        public static object GetBinaryDeSerializedReportData(byte[] obyteArray)
        {
            MemoryStream oMemStream = new MemoryStream();
            BinaryFormatter oBinaryFormatter = new BinaryFormatter();
            object obj;

            try
            {
                oMemStream.Write(obyteArray, 0, obyteArray.Length);
                oMemStream.Seek(0, 0);
                obj = oBinaryFormatter.Deserialize(oMemStream);
            }
            finally
            {
                if (oMemStream != null)
                    oMemStream.Close();
            }
            return obj;
        }

        public static void SaveArchivedReport(ReportArchiveInfo oRptArchiveInfo, List<ReportArchiveParameterInfo> oRptArchiveParameterCollection)
        {
            IReportArchive oRptArchiveClient = RemotingHelper.GetReportArchiveObject();
            oRptArchiveClient.SaveArchivedReport(oRptArchiveInfo, oRptArchiveParameterCollection, Helper.GetAppUserInfo());
        }


        public static ListItemCollection GetListItemCollectionForExceptionType()
        {
            List<ExceptionTypeMstInfo> oExceptionTypeMstInfoCollection = SessionHelper.GetAllExceptionTypes();
            ListItemCollection oLstCollection = new ListItemCollection();
            foreach (ExceptionTypeMstInfo oExceptionTypeMstInfo in oExceptionTypeMstInfoCollection)
            {
                ListItem item = new ListItem();
                item.Value = oExceptionTypeMstInfo.ExceptionTypeID.ToString();
                item.Text = oExceptionTypeMstInfo.ExceptionType;
                oLstCollection.Add(item);
            }
            return oLstCollection;
        }

        public static DataTable GetDataTableForParamViewer(ReportArchiveInfo oReportArchiveInfo)
        {
            IList<ReportArchiveParameterInfo> oRptArchiveParamInfoList = oReportArchiveInfo.ReportArchiveParameterByRptArchiveID;
            DataTable dt = new DataTable();
            dt.Columns.Add("ParamMstID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("ParamDisplayName", System.Type.GetType("System.String"));
            dt.Columns.Add("ParamDisplayValue", System.Type.GetType("System.String"));
            string filterExpression = "";
            foreach (ReportArchiveParameterInfo oRptArchiveParamInfo in oRptArchiveParamInfoList)
            {
                DataRow dr = null;
                short paramID = oRptArchiveParamInfo.ParameterID.Value;//parameterMstID
                filterExpression = "ParamMstID = " + paramID.ToString();
                DataRow[] rowArry = dt.Select(filterExpression);
                string fromValue = "";
                string toValue = "";
                if (paramID != (short)WebEnums.ReportParameter.Entity && rowArry.Length > 0)//paramid = 1 means entity
                {
                    fromValue = rowArry[0]["ParamDisplayValue"].ToString();
                    toValue = oRptArchiveParamInfo.ParameterValue;
                    if (!string.IsNullOrEmpty(fromValue + toValue))
                        rowArry[0]["ParamDisplayValue"] = rowArry[0]["ParamDisplayValue"].ToString() + " To " + oRptArchiveParamInfo.ParameterValue;
                }
                else
                {

                    dr = dt.NewRow();
                    dr["ParamMstID"] = oRptArchiveParamInfo.ParameterID.Value;
                    dr["ParamDisplayName"] = oRptArchiveParamInfo.ReportParameterDisplayName;
                    dr["ParamDisplayValue"] = ReportHelper.MapIDsWithDisplayValues(oReportArchiveInfo, oRptArchiveParamInfo);//oRptArchiveInfo.ParameterValue;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static string GetRptParamKeyNameForParamKeyID(short keyID)
        {

            List<ReportParameterKeyMstInfo> oRptParamKeyInfoCollection = CacheHelper.GetAllReportParameterKeys();
            ReportParameterKeyMstInfo oRptParamKeyInfo = oRptParamKeyInfoCollection.Find(r => r.ReportParameterKeyID == keyID);
            return oRptParamKeyInfo.ReportParameterKeyName;
        }

        public static string CalculateAging(short? agingDays)
        {
            //<= 30
            // >= 31 AND <=60
            //>=61 AND <=90
            //>=91
            string aging = string.Empty;

            if (agingDays != null)
            {
                if (agingDays < 0)
                    aging = "";
                else if (agingDays <= 30)
                    aging = "<= 30";
                else if (agingDays >= 31 && agingDays <= 60)
                    aging = ">=31 AND <=60";
                else if (agingDays >= 61 && agingDays <= 90)
                    aging = ">=61 AND <=90";
                else
                    aging = ">=91";
            }

            return aging;
        }


        public static string GetSessionConstantByReportID(short reportID)
        {
            string sessionConstant = "";
            switch (reportID)
            {
                case (short)WebEnums.Reports.UNUSUAL_BALANCES:
                    sessionConstant = SessionConstants.REPORT_DATA_UNUSUAL_BALANCE;
                    break;
                case (short)WebEnums.Reports.ACCOUNT_OWNERSHIP:
                    sessionConstant = SessionConstants.REPORT_DATA_ACCOUNT_OWNERSHIP;
                    break;
                case (short)WebEnums.Reports.ACCOUNT_STATUS:
                    sessionConstant = SessionConstants.REPORT_DATA_ACCOUNT_STATUS;
                    break;
                case (short)WebEnums.Reports.CERTIFICATION_TRACKING:
                    sessionConstant = SessionConstants.REPORT_DATA_CERTIFICATION_TRACKING;
                    break;
                case (short)WebEnums.Reports.DELINQUENT_ACCOUNTS_REPORT:
                    sessionConstant = SessionConstants.REPORT_DATA_DELINQUENT_ACCOUNT;
                    break;
                case (short)WebEnums.Reports.EXCEPTION_STATUS:
                    sessionConstant = SessionConstants.REPORT_DATA_EXCEPTION_STATUS;
                    break;
                case (short)WebEnums.Reports.INCOMPLETE_ACCOUNT_ATTRIBUTE:
                    sessionConstant = SessionConstants.REPORT_DATA_INCOMPLETE_ACCOUNT_ATTRIBUTE;
                    break;
                case (short)WebEnums.Reports.OPEN_ITEM:
                    sessionConstant = SessionConstants.REPORT_DATA_OPEN_ITEM;
                    break;
                case (short)WebEnums.Reports.RECONCILIATION_STATUS_COUNT_BY_USER_ROLE:
                    sessionConstant = SessionConstants.REPORT_DATA_REC_STATUS_COUNT;
                    break;
                case (short)WebEnums.Reports.UNASSIGNED_ACCOUNTS:
                    sessionConstant = SessionConstants.REPORT_DATA_UNASSIGNED_ACCOUNT;
                    break;
                case (short)WebEnums.Reports.QUALITY_SCORE_REPORT:
                    sessionConstant = SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM;
                    break;
                case (short)WebEnums.Reports.REVIEW_NOTES_REPORT:
                    sessionConstant = SessionConstants.REPORT_DATA_REVIEW_NOTES;
                    break;
                case (short)WebEnums.Reports.COMPLETION_DATE_REPORT:
                    sessionConstant = SessionConstants.REPORT_DATA_COMPLETION_DATE;
                    break;
                case (short)WebEnums.Reports.NEW_ACCOUNT_REPORT:
                    sessionConstant = SessionConstants.REPORT_DATA_NEW_ACCOUNT;
                    break;
                case (short)WebEnums.Reports.ACCOUNT_ATTRIBUTE_CHANGE_REPORT:
                    sessionConstant = SessionConstants.REPORT_DATA_ACCOUNT_ATTRIBUTE_CHANGE;
                    break;
                case (short)WebEnums.Reports.TASK_COMPLETION_REPORT:
                    sessionConstant = SessionConstants.REPORT_DATA_TASK_COMPLETION_REPORT;
                    break;
            }
            return sessionConstant;
        }

        public static ReportArchiveInfo GetReportArchivedDataByReportArchiveID(int ReportArchiveID)
        {
            IReportArchive oRptArchiveClient = RemotingHelper.GetReportArchiveObject();
            return oRptArchiveClient.GetArchivedReportByReportArchiveID(ReportArchiveID, Helper.GetAppUserInfo());
        }

        public static Dictionary<string, string> GetReportCriteriaFromParamInfoCollection(IList<ReportArchiveParameterInfo> ArchiveRptParamCollection)
        {
            Dictionary<string, string> oArchiveRptParamColection = new Dictionary<string, string>();
            foreach (ReportArchiveParameterInfo oArchRptParam in ArchiveRptParamCollection)
            {

                string keyName = oArchRptParam.ReportParameterKeyName;
                string keyValue = oArchRptParam.ParameterValue;

                oArchiveRptParamColection.Add(keyName, keyValue);
            }


            return oArchiveRptParamColection;
        }


        public static string AddCommonQueryStringParameter(string url)
        {
            url += "?" + QueryStringConstants.IS_REPORT + "=1";
            return url;
        }

        public static int? GetRecPeriodIDFromReportCriteria()
        {
            int? recPeriodID = null;

            Dictionary<string, string> _oCriteriaCollection = (Dictionary<string, string>)HttpContext.Current.Session[SessionConstants.REPORT_CRITERIA];
            if (_oCriteriaCollection != null)
            {
                recPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
            }
            return recPeriodID;
        }

        public static void EnableDisableReportOptions(short ReportType, ExButton btnArchive, ExButton btnSignOff, ExButton btnSaveMyReport, ExHyperLink changeParams)
        {
            switch (ReportType)
            {

                case (short)WebEnums.ReportType.StandardReport:
                    changeParams.Enabled = true;
                    btnSaveMyReport.Enabled = true;
                    btnArchive.Enabled = true;
                    btnSignOff.Enabled = true;
                    break;
                case (short)WebEnums.ReportType.ArchivedReport:
                    changeParams.Enabled = false;
                    btnSaveMyReport.Enabled = false;
                    break;
                case (short)WebEnums.ReportType.UserSavedReport:
                    changeParams.Enabled = true;
                    btnSaveMyReport.Enabled = false;
                    btnArchive.Enabled = true;
                    btnSignOff.Enabled = true;
                    break;

                case (short)WebEnums.ReportType.UserSavedReportChangedParams:
                    changeParams.Enabled = true;
                    btnSaveMyReport.Enabled = true;
                    btnArchive.Enabled = true;
                    btnSignOff.Enabled = true;
                    break;

            }
        }

        private static string MapIDsWithDisplayValues(ReportArchiveInfo oReportArchiveInfo, ReportArchiveParameterInfo oRptArchiveParamInfo)
        {
            string displayValues = "";
            short paramID = -1;
            string keyName = oRptArchiveParamInfo.ReportParameterKeyName;
            if (oRptArchiveParamInfo.ParameterID.HasValue)
                paramID = oRptArchiveParamInfo.ParameterID.Value;
            switch (paramID)
            {
                case (short)WebEnums.ReportParameter.Aging:
                    displayValues = ReportHelper.GetDisplayValueForAging(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Key:
                    displayValues = ReportHelper.GetDisplayValueForKeyAccount(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Material:
                    displayValues = ReportHelper.GetDisplayValueForMaterialAccount(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.OpenItemClassification:
                    displayValues = ReportHelper.GetDisplayvalueForOpenItemClassification(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Period:
                    displayValues = ReportHelper.GetDisplayvalueForRecPeriod(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Reason:
                    displayValues = ReportHelper.GetDisplayValueForReason(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.RecStatus:
                    displayValues = ReportHelper.GetDisplayValuesForRecStatus(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.RiskRating:
                    displayValues = ReportHelper.GetDisplayValuesForRiskRating(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.TypeOfException:
                    displayValues = ReportHelper.GetDisplayValuesForTypeOfException(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.User:
                    displayValues = ReportHelper.GetDisplayValuesForUserList(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.FinancialYear:
                    displayValues = ReportHelper.GetDisplayValuesForFinancialYear(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.ChecklistItem:
                    displayValues = ReportHelper.GetDisplayValuesForQualityCheckList(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.DisplayColumn:
                    displayValues = ReportHelper.GetDisplayValuesForDisplayColumnList(oReportArchiveInfo.ReportID, oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Role:
                    displayValues = ReportHelper.GetDisplayValuesForRoleList(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.TaskStatus:
                    displayValues = ReportHelper.GetDisplayValuesForTaskStatusList(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.TaskType:
                    displayValues = ReportHelper.GetDisplayValuesForTaskTypeList(oRptArchiveParamInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.TaskListName:
                    displayValues = ReportHelper.GetDisplayValuesForTaskListName(oRptArchiveParamInfo.ParameterValue);
                    break;
                default:
                    displayValues = oRptArchiveParamInfo.ParameterValue;
                    break;
            }
            return displayValues;
        }
        private static string GetDisplayValuesForRiskRating(string IDs)
        {
            string displayValues = "";
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<RiskRatingMstInfo> oRiskRatingCollection = SessionHelper.GetAllRiskRating();
            foreach (string id in arrIDs)
            {
                RiskRatingMstInfo oRiskRating = oRiskRatingCollection.Find(r => r.RiskRatingID.Value == Convert.ToInt16(id));
                if (oRiskRating != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = oRiskRating.RiskRating;
                    else
                        displayValues = displayValues + "," + oRiskRating.RiskRating;
                }
            }

            return displayValues;
        }
        private static string[] GetArrySeperator(string IDs)
        {
            string[] arrySeperator = new string[1];
            if (IDs.Contains(","))
                arrySeperator[0] = ",";
            else
                arrySeperator[0] = AppSettingHelper.GetAppSettingValue(AppSettingConstants.FILTER_VALUE_SEPARATOR);
            return arrySeperator;

        }
        private static string GetDisplayValuesForTypeOfException(string IDs)
        {
            string displayValues = "";
            //string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<ExceptionTypeMstInfo> oExceptionTypeMstInfoCollection = SessionHelper.GetAllExceptionTypes();
            foreach (string id in arrIDs)
            {
                ExceptionTypeMstInfo oExceptionTypeMstInfo = oExceptionTypeMstInfoCollection.Find(r => r.ExceptionTypeID.Value == Convert.ToInt16(id));
                if (oExceptionTypeMstInfo != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = oExceptionTypeMstInfo.ExceptionType;
                    else
                        displayValues = displayValues + "," + oExceptionTypeMstInfo.ExceptionType;
                }
            }
            return displayValues;
        }
        private static string GetDisplayValuesForRecStatus(string IDs)
        {
            string displayValues = "";
            // string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<ReconciliationStatusMstInfo> oRecStatusCollection = SessionHelper.GetAllRecStatus();
            foreach (string id in arrIDs)
            {
                ReconciliationStatusMstInfo oRecStatus = oRecStatusCollection.Find(r => r.ReconciliationStatusID.Value == Convert.ToInt16(id));
                if (oRecStatus != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = oRecStatus.ReconciliationStatus;
                    else
                        displayValues = displayValues + "," + oRecStatus.ReconciliationStatus;
                }
            }
            return displayValues;
        }


        private static string GetDisplayValuesForUserList(string IDs)
        {
            string displayValues = "";
            //string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<int> userIDs = new List<int>();
            foreach (string s in arrIDs)
            {
                userIDs.Add(Convert.ToInt32(s));
            }
            IUser oUserClient = RemotingHelper.GetUserObject();
            List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectUserByUserID(userIDs, Helper.GetAppUserInfo());
            //foreach (string id in arrIDs)
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                if (oUserHdrInfo != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = Helper.GetDisplayUserFullName(oUserHdrInfo.FirstName, oUserHdrInfo.LastName);
                    else
                        displayValues = displayValues + "," + Helper.GetDisplayUserFullName(oUserHdrInfo.FirstName, oUserHdrInfo.LastName);
                }
            }
            return displayValues;
        }

        private static string GetDisplayValuesForRoleList(string IDs)
        {
            string displayValues = "";
            //string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<short> roleIDs = new List<short>();
            foreach (string s in arrIDs)
            {
                roleIDs.Add(Convert.ToInt16(s));
            }
            List<RoleMstInfo> oRoleMstInfoList = SessionHelper.GetAllRoles();
            //foreach (string id in arrIDs)
            if (oRoleMstInfoList != null && oRoleMstInfoList.Count > 0)
            {
                foreach (RoleMstInfo oRoleMstInfo in oRoleMstInfoList)
                {
                    if (oRoleMstInfo != null && roleIDs.IndexOf(oRoleMstInfo.RoleID.GetValueOrDefault()) >= 0)
                    {
                        if (string.IsNullOrEmpty(displayValues))
                            displayValues = oRoleMstInfo.Role;
                        else
                            displayValues = displayValues + "," + oRoleMstInfo.Role;
                    }
                }
            }
            return displayValues;
        }

        private static string GetDisplayValuesForTaskStatusList(string IDs)
        {
            string displayValues = "";
            //string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<short> taskStatusIDs = new List<short>();
            foreach (string s in arrIDs)
            {
                taskStatusIDs.Add(Convert.ToInt16(s));
            }
            List<TaskStatusMstInfo> oTaskStatusMstInfoList = SessionHelper.GetTaskStatus();
            //foreach (string id in arrIDs)
            if (oTaskStatusMstInfoList != null && oTaskStatusMstInfoList.Count > 0)
            {
                foreach (TaskStatusMstInfo oTaskStatusMstInfo in oTaskStatusMstInfoList)
                {
                    if (oTaskStatusMstInfo != null && taskStatusIDs.IndexOf(oTaskStatusMstInfo.TaskStatusID.GetValueOrDefault()) >= 0)
                    {
                        if (string.IsNullOrEmpty(displayValues))
                            displayValues = oTaskStatusMstInfo.TaskStatus;
                        else
                            displayValues = displayValues + "," + oTaskStatusMstInfo.TaskStatus;
                    }
                }
            }
            return displayValues;
        }

        private static string GetDisplayValuesForTaskTypeList(string IDs)
        {
            string displayValues = "";
            //string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<short> taskTypeIDs = new List<short>();
            foreach (string s in arrIDs)
            {
                taskTypeIDs.Add(Convert.ToInt16(s));
            }
            List<TaskTypeMstInfo> oTaskTypeMstInfoList = SessionHelper.GetAllTaskType();
            //foreach (string id in arrIDs)
            if (oTaskTypeMstInfoList != null && oTaskTypeMstInfoList.Count > 0)
            {
                foreach (TaskTypeMstInfo oTaskTypeMstInfo in oTaskTypeMstInfoList)
                {
                    if (oTaskTypeMstInfo != null && taskTypeIDs.IndexOf(oTaskTypeMstInfo.TaskTypeID.GetValueOrDefault()) >= 0)
                    {
                        if (string.IsNullOrEmpty(displayValues))
                            displayValues = oTaskTypeMstInfo.TaskType;
                        else
                            displayValues = displayValues + "," + oTaskTypeMstInfo.TaskType;
                    }
                }
            }
            return displayValues;
        }

        private static string GetDisplayValuesForTaskListName(string IDs)
        {
            string displayValues = "";
            //string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<short> taskListIDs = new List<short>();
            foreach (string s in arrIDs)
            {
                taskListIDs.Add(Convert.ToInt16(s));
            }
            List<TaskListHdrInfo> oTaskListHdrInfoList = TaskMasterHelper.GetTaskListHdrInfoCollection(SessionHelper.CurrentReconciliationPeriodID.Value);
            //foreach (string id in arrIDs)
            if (oTaskListHdrInfoList != null && oTaskListHdrInfoList.Count > 0)
            {
                foreach (TaskListHdrInfo oTaskListHdrInfo in oTaskListHdrInfoList)
                {
                    if (oTaskListHdrInfo != null && taskListIDs.IndexOf(oTaskListHdrInfo.TaskListID.GetValueOrDefault()) >= 0)
                    {
                        if (string.IsNullOrEmpty(displayValues))
                            displayValues = oTaskListHdrInfo.TaskListName;
                        else
                            displayValues = displayValues + "," + oTaskListHdrInfo.TaskListName;
                    }
                }
            }
            return displayValues;
        }

        private static string GetDisplayValuesForQualityCheckList(string IDs)
        {
            string displayValues = "";
            // string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<int> qualityScoreIDs = new List<int>();
            foreach (string s in arrIDs)
            {
                qualityScoreIDs.Add(Convert.ToInt32(s));
            }
            IQualityScoreReports oQualityScoreClient = RemotingHelper.GetQualityScoreReportObject();
            List<QualityScoreChecklistInfo> oQualityScoreInfoCollection = oQualityScoreClient.GetQualityScoreChecklistByQualityScoreIDs(qualityScoreIDs, Helper.GetAppUserInfo());
            //foreach (string id in arrIDs)
            foreach (QualityScoreChecklistInfo oQualityScoreInfo in oQualityScoreInfoCollection)
            {
                if (oQualityScoreInfo != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = Convert.ToString(oQualityScoreInfo.QualityScoreNumber);
                    else
                        displayValues = displayValues + "," + Convert.ToString(oQualityScoreInfo.QualityScoreNumber);
                }
            }
            return displayValues;
        }

        private static string GetDisplayValuesForDisplayColumnList(short? reportID, string IDs)
        {
            string displayValues = "";
            // string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<int> displayColumnIDs = new List<int>();
            foreach (string s in arrIDs)
            {
                displayColumnIDs.Add(Convert.ToInt32(s));
            }
            if (reportID.HasValue)
            {
                List<ReportColumnInfo> oAllReportColumnInfoList = SessionHelper.GetReportColumnInfoList(reportID.Value, true);
                //foreach (string id in arrIDs)
                if (oAllReportColumnInfoList != null && oAllReportColumnInfoList.Count > 0)
                {
                    foreach (ReportColumnInfo oReportColumnInfo in oAllReportColumnInfoList)
                    {
                        if (oReportColumnInfo != null && displayColumnIDs.IndexOf(oReportColumnInfo.ColumnID) >= 0)
                        {
                            if (string.IsNullOrEmpty(displayValues))
                                displayValues = Convert.ToString(oReportColumnInfo.ColumnName);
                            else
                                displayValues = displayValues + "," + Convert.ToString(oReportColumnInfo.ColumnName);
                        }
                    }
                }
            }
            return displayValues;
        }

        private static string GetDisplayValueForReason(string IDs)
        {
            string displayValues = "";
            // string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<ReasonMstInfo> oReasonMstInfoCollection = SessionHelper.GetAllReasons();
            foreach (string id in arrIDs)
            {
                ReasonMstInfo oReasonMstInfo = oReasonMstInfoCollection.Find(r => r.ReasonID.Value == Convert.ToInt16(id));
                if (oReasonMstInfo != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = oReasonMstInfo.Reason;
                    else
                        displayValues = displayValues + "," + oReasonMstInfo.Reason;
                }

            }
            return displayValues;
        }
        private static string GetDisplayvalueForRecPeriod(string ID)
        {
            string displayValue = "";
            List<ReconciliationPeriodInfo> oRecPeriodIfnfoCollection = CacheHelper.GetAllReconciliationPeriods(null);
            ReconciliationPeriodInfo oRecPeriodInfo = oRecPeriodIfnfoCollection.Find(r => r.ReconciliationPeriodID.Value == Convert.ToInt32(ID));
            if (oRecPeriodInfo != null)
                displayValue = Helper.GetDisplayDate(oRecPeriodInfo.PeriodEndDate);
            return displayValue;
        }
        private static string GetDisplayvalueForOpenItemClassification(string IDs)
        {
            string displayValues = "";
            // string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<ReconciliationCategoryMstInfo> oOpenItemCollection = SessionHelper.GetOpenItemClassifications();
            foreach (string id in arrIDs)
            {
                ReconciliationCategoryMstInfo oOpenItem = oOpenItemCollection.Find(r => r.ReconciliationCategoryID.Value == Convert.ToInt16(id));
                if (oOpenItem != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = oOpenItem.ReconciliationCategory;
                    else
                        displayValues = displayValues + "," + oOpenItem.ReconciliationCategory;
                }
            }
            return displayValues;
        }
        private static string GetDisplayValueForAging(string IDs)
        {
            string displayValues = "";
            //string[] arrySeperator = { "," };
            string[] arrIDs = IDs.Split(GetArrySeperator(IDs), StringSplitOptions.RemoveEmptyEntries);
            List<AgingCategoryMstInfo> oAgingCategoryMstInfoCollection = SessionHelper.GetAllAgingCategories();
            foreach (string id in arrIDs)
            {
                AgingCategoryMstInfo oAgingCategoryMstInfo = oAgingCategoryMstInfoCollection.Find(r => r.AgingCategoryID == Convert.ToInt16(id));
                if (oAgingCategoryMstInfo != null)
                {
                    if (string.IsNullOrEmpty(displayValues))
                        displayValues = oAgingCategoryMstInfo.AgingCategoryName;
                    else
                        displayValues = displayValues + "," + oAgingCategoryMstInfo.AgingCategoryName;
                }
            }
            return displayValues;
        }
        private static string GetDisplayValueForKeyAccount(string IDs)
        {
            string displayValues = "";
            if (IDs == WebConstants.RPT_PRM_ALL)
                displayValues = LanguageUtil.GetValue(1262);
            if (IDs == WebConstants.RPT_PRM_YES)
                displayValues = LanguageUtil.GetValue(1252);
            if (IDs == WebConstants.RPT_PRM_NO)
                displayValues = LanguageUtil.GetValue(1251);

            return displayValues;
        }
        private static string GetDisplayValueForMaterialAccount(string IDs)
        {
            string displayValues = "";
            if (IDs == WebConstants.RPT_PRM_ALL)
                displayValues = LanguageUtil.GetValue(1262);
            if (IDs == WebConstants.RPT_PRM_YES)
                displayValues = LanguageUtil.GetValue(1252);
            if (IDs == WebConstants.RPT_PRM_NO)
                displayValues = LanguageUtil.GetValue(1251);

            return displayValues;
        }

        public static string GetDisplayValuesForFinancialYear(string ID)
        {
            string displayValue = "";
            List<FinancialYearHdrInfo> oFinancialYearHdrInfoList = CacheHelper.GetAllFinancialYears();
            FinancialYearHdrInfo oFinancialYearHdrInfo = oFinancialYearHdrInfoList.Find(r => r.FinancialYearID.Value == Convert.ToInt32(ID));
            if (oFinancialYearHdrInfo != null)
                displayValue = Helper.GetDisplayStringValue(oFinancialYearHdrInfo.FinancialYear);
            return displayValue;
        }

        public static DataTable GetDataTableForSavedRptParamViewer(IList<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterInfoCollection)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ParamMstID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("ParamDisplayName", System.Type.GetType("System.String"));
            dt.Columns.Add("ParamDisplayValue", System.Type.GetType("System.String"));
            string filterExpression = "";
            foreach (UserMyReportSavedReportParameterInfo oUserMyReportSavedReportParameterInfo in oUserMyReportSavedReportParameterInfoCollection)
            {
                DataRow dr = null;
                short paramID = oUserMyReportSavedReportParameterInfo.ParameterMstID.Value;//parameterMstID
                filterExpression = "ParamMstID = " + paramID.ToString();
                DataRow[] rowArry = dt.Select(filterExpression);
                string fromValue = "";
                string toValue = "";
                if (paramID != (short)WebEnums.ReportParameter.Entity && rowArry.Length > 0)//paramid = 1 means entity
                {
                    fromValue = rowArry[0]["ParamDisplayValue"].ToString();
                    toValue = oUserMyReportSavedReportParameterInfo.ParameterValue;
                    if (!string.IsNullOrEmpty(fromValue + toValue))
                        rowArry[0]["ParamDisplayValue"] = rowArry[0]["ParamDisplayValue"].ToString() + " To " + oUserMyReportSavedReportParameterInfo.ParameterValue;
                }
                else
                {

                    dr = dt.NewRow();
                    dr["ParamMstID"] = oUserMyReportSavedReportParameterInfo.ParameterMstID.Value;
                    dr["ParamDisplayName"] = oUserMyReportSavedReportParameterInfo.ReportParameterDisplayName;
                    dr["ParamDisplayValue"] = ReportHelper.MapReportSavedReportParameteIDsWithDisplayValues(oUserMyReportSavedReportParameterInfo);//oRptArchiveInfo.ParameterValue;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        private static string MapReportSavedReportParameteIDsWithDisplayValues(UserMyReportSavedReportParameterInfo oUserMyReportSavedReportParameterInfo)
        {
            string displayValues = "";
            short paramID = -1;
            string keyName = oUserMyReportSavedReportParameterInfo.ReportParameterKeyName;
            if (oUserMyReportSavedReportParameterInfo.ParameterMstID.HasValue)
                paramID = oUserMyReportSavedReportParameterInfo.ParameterMstID.Value;
            switch (paramID)
            {
                case (short)WebEnums.ReportParameter.Aging:
                    displayValues = ReportHelper.GetDisplayValueForAging(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Key:
                    displayValues = ReportHelper.GetDisplayValueForKeyAccount(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Material:
                    displayValues = ReportHelper.GetDisplayValueForMaterialAccount(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.OpenItemClassification:
                    displayValues = ReportHelper.GetDisplayvalueForOpenItemClassification(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.Period:
                    if (oUserMyReportSavedReportParameterInfo.ParameterValue == WebConstants.CURRENT_REC_PERIOD_INDEX)
                    {
                        displayValues = WebConstants.CURRENT_REC_PERIOD;
                    }
                    else
                    {
                        displayValues = ReportHelper.GetDisplayvalueForRecPeriod(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    }
                    break;
                case (short)WebEnums.ReportParameter.Reason:
                    displayValues = ReportHelper.GetDisplayValueForReason(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.RecStatus:
                    displayValues = ReportHelper.GetDisplayValuesForRecStatus(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.RiskRating:
                    //displayValues = oRptArchiveInfo.ParameterValue;
                    displayValues = ReportHelper.GetDisplayValuesForRiskRating(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.TypeOfException:
                    displayValues = ReportHelper.GetDisplayValuesForTypeOfException(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.FinancialYear:
                    displayValues = ReportHelper.GetDisplayValuesForFinancialYear(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.User:
                    displayValues = ReportHelper.GetDisplayValuesForUserList(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                case (short)WebEnums.ReportParameter.ChecklistItem:
                    displayValues = ReportHelper.GetDisplayValuesForQualityCheckList(oUserMyReportSavedReportParameterInfo.ParameterValue);
                    break;
                default:
                    displayValues = oUserMyReportSavedReportParameterInfo.ParameterValue;
                    break;
            }
            return displayValues;
        }




        public static void ItemDataBoundExceptionStatus(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                // TODO: Change from Session
                ////((LinkButton)(e.Item as GridHeaderItem)["WriteOnOffAmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1425) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                ////((LinkButton)(e.Item as GridHeaderItem)["UnexplainedVarianceReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1678) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                ////((LinkButton)(e.Item as GridHeaderItem)["DelinquentAmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1907) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                Control oControlWriteOnOffAmountReportingCurrency = new Control();
                Control oControlUnexplainedVarianceReportingCurrency = new Control();
                Control oControlDelinquentAmountReportingCurrency = new Control();

                oControlWriteOnOffAmountReportingCurrency = (e.Item as GridHeaderItem)["WriteOnOffAmountReportingCurrency"].Controls[0];
                oControlUnexplainedVarianceReportingCurrency = (e.Item as GridHeaderItem)["UnexplainedVarianceReportingCurrency"].Controls[0];
                oControlDelinquentAmountReportingCurrency = (e.Item as GridHeaderItem)["DelinquentAmountReportingCurrency"].Controls[0];

                if (oControlWriteOnOffAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlWriteOnOffAmountReportingCurrency).Text = Helper.GetLabelIDValue(1425) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
                else
                {
                    if (oControlWriteOnOffAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlWriteOnOffAmountReportingCurrency).Text = Helper.GetLabelIDValue(1425) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }

                if (oControlUnexplainedVarianceReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlUnexplainedVarianceReportingCurrency).Text = Helper.GetLabelIDValue(1678) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
                else
                {
                    if (oControlUnexplainedVarianceReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlUnexplainedVarianceReportingCurrency).Text = Helper.GetLabelIDValue(1678) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }

                if (oControlDelinquentAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlDelinquentAmountReportingCurrency).Text = Helper.GetLabelIDValue(1907) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
                else
                {
                    if (oControlDelinquentAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlDelinquentAmountReportingCurrency).Text = Helper.GetLabelIDValue(1907) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }

                //******************************************************************************************************************************************

            }

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                ExceptionStatusReportInfo oExceptionStatusReportInfo = (ExceptionStatusReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                ExLabel lblWriteOnOff = (ExLabel)e.Item.FindControl("lblWriteOnOff");
                ExLabel lblUnexplainedVariance = (ExLabel)e.Item.FindControl("lblUnexplainedVariance");
                ExLabel lblDelinquentAmount = (ExLabel)e.Item.FindControl("lblDelinquentAmount");

                ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                ExLabel lblIsMaterial = (ExLabel)e.Item.FindControl("lblIsMaterial");
                ExLabel lblIsKeyAccount = (ExLabel)e.Item.FindControl("lblIsKeyAccount");

                ExLabel lblPreparerFullName = (ExLabel)e.Item.FindControl("lblPreparerFullName");

                lblAccountNumber.Text = oExceptionStatusReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oExceptionStatusReportInfo.AccountName);
                lblWriteOnOff.Text = Helper.GetDisplayDecimalValue(oExceptionStatusReportInfo.WriteOnOffReportingCurrency);
                lblUnexplainedVariance.Text = Helper.GetDisplayDecimalValue(oExceptionStatusReportInfo.UnexpVarReportingCurrency);

                if (oExceptionStatusReportInfo.IsDueDatePast == null
                    || oExceptionStatusReportInfo.IsDueDatePast.Value)
                {
                    lblDelinquentAmount.Text = Helper.GetDisplayDecimalValue(oExceptionStatusReportInfo.DelinquentAmountReportingCurrency);
                }
                else
                {
                    lblDelinquentAmount.Text = WebConstants.HYPHEN;
                }

                ReportHelper.SetTextForLabel(lblRiskRating, Helper.GetDisplayStringValue(oExceptionStatusReportInfo.RiskRating));
                ReportHelper.SetTextForLabel(lblIsMaterial, Helper.GetDisplayStringValue(ReportHelper.SetYesNoCodeBasedOnBool(oExceptionStatusReportInfo.IsMaterial)));
                ReportHelper.SetTextForLabel(lblIsKeyAccount, Helper.GetDisplayStringValue(ReportHelper.SetYesNoCodeBasedOnBool(oExceptionStatusReportInfo.IsKeyAccount)));

                lblPreparerFullName.Text = Helper.GetDisplayStringValue(oExceptionStatusReportInfo.PreparerFullName);
            }
        }




        public static void ItemDataBoundUnusualBalancesReport(Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Header)
            {
                UnusualBalancesReportInfo oUnusualBalancesReportInfo = (UnusualBalancesReportInfo)e.Item.DataItem;
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                Control oControlAmountReportingCurrency = new Control();
                Control oControlAmountBaseCurrency = new Control();

                oControlAmountReportingCurrency = (e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0];
                oControlAmountBaseCurrency = (e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0];

                if (oControlAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }
                if (oControlAmountBaseCurrency is LinkButton)
                {
                    //BCCY Changes
                    //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                    ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876);
                }
                else
                {
                    if (oControlAmountBaseCurrency is LiteralControl)
                    {
                        //BCCY Changes
                        //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                        ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876);

                    }
                }
                //******************************************************************************************************************************************

            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                UnusualBalancesReportInfo oUnusualBalancesReportInfo = (UnusualBalancesReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                ExLabel lblGLBalanceRC = (ExLabel)e.Item.FindControl("lblGLBalanceRC");
                //BCCY Changes

                ExLabel lblGLBalanceBC = (ExLabel)e.Item.FindControl("lblGLBalanceBC");

                ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                ExLabel lblIsMaterial = (ExLabel)e.Item.FindControl("lblIsMaterial");
                ExLabel lblIsKeyAccount = (ExLabel)e.Item.FindControl("lblIsKeyAccount");

                ExLabel lblPreparer = (ExLabel)e.Item.FindControl("lblPreparer");
                ExLabel lblReason = (ExLabel)e.Item.FindControl("lblReason");

                lblAccountNumber.Text = oUnusualBalancesReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oUnusualBalancesReportInfo.AccountName);
                lblGLBalanceRC.Text = Helper.GetDisplayDecimalValue(oUnusualBalancesReportInfo.GLBalanceReportingCurrency);

                lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(oUnusualBalancesReportInfo.BaseCurrencyCode, (oUnusualBalancesReportInfo.GLBalanceBaseCurrency));



                lblRiskRating.Text = Helper.GetDisplayStringValue(oUnusualBalancesReportInfo.RiskRating);
                lblIsMaterial.Text = ReportHelper.SetYesNoCodeBasedOnBool(oUnusualBalancesReportInfo.IsMaterial);
                lblIsKeyAccount.Text = ReportHelper.SetYesNoCodeBasedOnBool(oUnusualBalancesReportInfo.IsKeyAccount);

                lblPreparer.Text = Helper.GetDisplayStringValue(Helper.GetDisplayUserFullName(oUnusualBalancesReportInfo.PreparerFirstName, oUnusualBalancesReportInfo.PreparerLastName));
                lblReason.Text = Helper.GetDisplayStringValue(oUnusualBalancesReportInfo.Reason);
            }
        }


        public static void ItemDataBoundAccountOwnershipReport(Telerik.Web.UI.GridItemEventArgs e, ref short? SumTotalAccountAssigned,
            ref short? SumCountHighAccounts, ref short? SumCountMediumAccounts, ref short? SumCountLowAccounts, ref short? SumCountKeyAccounts, ref short? SumCountMaterialAccounts)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountOwnershipReportInfo oAccountOwnershipReportInfo = (AccountOwnershipReportInfo)e.Item.DataItem;

                SumTotalAccountAssigned += oAccountOwnershipReportInfo.CountTotalAccountAssigned;
                SumCountHighAccounts += oAccountOwnershipReportInfo.CountHighAccounts;
                SumCountMediumAccounts += oAccountOwnershipReportInfo.CountMediumAccounts;
                SumCountLowAccounts += oAccountOwnershipReportInfo.CountLowAccounts;
                SumCountKeyAccounts += oAccountOwnershipReportInfo.CountKeyAccounts;
                SumCountMaterialAccounts += oAccountOwnershipReportInfo.CountMaterialAccounts;

                ExLabel lblRole = (ExLabel)e.Item.FindControl("lblRole");
                ExLabel lblUserName = (ExLabel)e.Item.FindControl("lblUserName");

                ExLabel lblCountTotalAccountAssigned = (ExLabel)e.Item.FindControl("lblCountTotalAccountAssigned");
                ExLabel lblPercentAccountAssigned = (ExLabel)e.Item.FindControl("lblPercentAccountAssigned");
                ExLabel lblCountKeyAccounts = (ExLabel)e.Item.FindControl("lblCountKeyAccounts");
                ExLabel lblPercentKeyAccounts = (ExLabel)e.Item.FindControl("lblPercentKeyAccounts");
                ExLabel lblCountHighAccounts = (ExLabel)e.Item.FindControl("lblCountHighAccounts");
                ExLabel lblPercentHighAccounts = (ExLabel)e.Item.FindControl("lblPercentHighAccounts");
                ExLabel lblCountMediumAccounts = (ExLabel)e.Item.FindControl("lblCountMediumAccounts");
                ExLabel lblPercentMediumAccounts = (ExLabel)e.Item.FindControl("lblPercentMediumAccounts");
                ExLabel lblCountLowAccounts = (ExLabel)e.Item.FindControl("lblCountLowAccounts");
                ExLabel lblPercentLowAccounts = (ExLabel)e.Item.FindControl("lblPercentLowAccounts");
                ExLabel lblCountMaterialAccounts = (ExLabel)e.Item.FindControl("lblCountMaterialAccounts");
                ExLabel lblPercentMaterialAccounts = (ExLabel)e.Item.FindControl("lblPercentMaterialAccounts");

                lblRole.Text = Helper.GetDisplayStringValue(oAccountOwnershipReportInfo.Role);
                lblUserName.Text = Helper.GetDisplayStringValue(oAccountOwnershipReportInfo.Name);

                lblCountTotalAccountAssigned.Text = oAccountOwnershipReportInfo.CountTotalAccountAssigned.Value.ToString();
                lblPercentAccountAssigned.Text = Helper.GetDisplayDecimalValue(oAccountOwnershipReportInfo.PercentAccountAssigned);
                lblCountKeyAccounts.Text = oAccountOwnershipReportInfo.CountKeyAccounts.Value.ToString();
                lblPercentKeyAccounts.Text = Helper.GetDisplayDecimalValue(oAccountOwnershipReportInfo.PercentKeyAccounts);
                lblCountHighAccounts.Text = oAccountOwnershipReportInfo.CountHighAccounts.Value.ToString();
                lblPercentHighAccounts.Text = Helper.GetDisplayDecimalValue(oAccountOwnershipReportInfo.PercentHighAccounts);
                lblCountMediumAccounts.Text = oAccountOwnershipReportInfo.CountMediumAccounts.Value.ToString();
                lblPercentMediumAccounts.Text = Helper.GetDisplayDecimalValue(oAccountOwnershipReportInfo.PercentMediumAccounts);
                lblCountLowAccounts.Text = oAccountOwnershipReportInfo.CountLowAccounts.Value.ToString();
                lblPercentLowAccounts.Text = Helper.GetDisplayDecimalValue(oAccountOwnershipReportInfo.PercentLowAccounts);
                lblCountMaterialAccounts.Text = oAccountOwnershipReportInfo.CountMaterialAccounts.Value.ToString();
                lblPercentMaterialAccounts.Text = Helper.GetDisplayDecimalValue(oAccountOwnershipReportInfo.PercentMaterialAccounts);
            }


        }


        public static void ItemDataBoundAccountStatusReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                AccountStatusReportInfo oAccountStatusReportInfo = (AccountStatusReportInfo)e.Item.DataItem;
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";

                //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                Control oControlAmountReportingCurrency = new Control();
                Control oControlAmountBaseCurrency = new Control();

                oControlAmountReportingCurrency = (e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0];
                oControlAmountBaseCurrency = (e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0];

                if (oControlAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
                else
                {
                    if (oControlAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }


                if (oControlAmountBaseCurrency is LinkButton)
                {
                    //BCCY Changes
                    //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                    ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876);
                }
                else
                {
                    if (oControlAmountBaseCurrency is LiteralControl)
                    {
                        //BCCY Changes
                        //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                        ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876);

                    }
                }
                //******************************************************************************************************************************************



            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountStatusReportInfo oAccountStatusReportInfo = (AccountStatusReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                ExLabel lblGLBalanceRC = (ExLabel)e.Item.FindControl("lblGLBalanceRC");
                ExLabel lblGLBalanceBC = (ExLabel)e.Item.FindControl("lblGLBalanceBC");
                //BCCY Changes


                ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                ExLabel lblIsMaterial = (ExLabel)e.Item.FindControl("lblIsMaterial");
                ExLabel lblIsKeyAccount = (ExLabel)e.Item.FindControl("lblIsKeyAccount");

                ExLabel lblReconciliationStatus = (ExLabel)e.Item.FindControl("lblReconciliationStatus");

                lblAccountNumber.Text = oAccountStatusReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oAccountStatusReportInfo.AccountName);

                lblGLBalanceRC.Text = Helper.GetDisplayDecimalValue(oAccountStatusReportInfo.GLBalanceReportingCurrency);
                lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(oAccountStatusReportInfo.BaseCurrencyCode, (oAccountStatusReportInfo.GLBalanceBaseCurrency));


                lblRiskRating.Text = Helper.GetDisplayStringValue(oAccountStatusReportInfo.RiskRating);
                lblIsMaterial.Text = Helper.GetDisplayStringValue(ReportHelper.SetYesNoCodeBasedOnBool(oAccountStatusReportInfo.IsMaterial));
                lblIsKeyAccount.Text = Helper.GetDisplayStringValue(ReportHelper.SetYesNoCodeBasedOnBool(oAccountStatusReportInfo.IsKeyAccount));

                lblReconciliationStatus.Text = Helper.GetDisplayStringValue(oAccountStatusReportInfo.ReconciliationStatus);
            }
        }

        public static void ItemDataBoundNewAccountReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                NewAccountReportInfo oNewAccountReportInfo = (NewAccountReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                ExLabel lblCreationPeriod = (ExLabel)e.Item.FindControl("lblCreationPeriod");
                ExLabel lblProfileName = (ExLabel)e.Item.FindControl("lblProfileName");
                ExLabel lblUploadedBy = (ExLabel)e.Item.FindControl("lblUploadedBy");
                ExLabel lblDateUploaded = (ExLabel)e.Item.FindControl("lblDateUploaded");

                lblAccountNumber.Text = oNewAccountReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oNewAccountReportInfo.AccountName);
                lblCreationPeriod.Text = Helper.GetDisplayDate(oNewAccountReportInfo.CreationPeriod);
                lblProfileName.Text = Helper.GetDisplayStringValue(oNewAccountReportInfo.ProfileName);
                lblUploadedBy.Text = Helper.GetDisplayStringValue(oNewAccountReportInfo.UploadedBy);
                lblDateUploaded.Text = Helper.GetDisplayDateTime(oNewAccountReportInfo.DateUploaded);
            }
        }

        public static void ItemDataBoundAccountAttributeChangeReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountAttributeChangeReportInfo oAccountAttributeChangeReportInfo = (AccountAttributeChangeReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                ExLabel lblAccountAttribute = (ExLabel)e.Item.FindControl("lblAccountAttribute");
                ExLabel lblValue = (ExLabel)e.Item.FindControl("lblValue");
                ExLabel lblValidFrom = (ExLabel)e.Item.FindControl("lblValidFrom");
                ExLabel lblValidUntil = (ExLabel)e.Item.FindControl("lblValidUntil");
                ExLabel lblChangeDate = (ExLabel)e.Item.FindControl("lblChangeDate");
                ExLabel lblChangePeriod = (ExLabel)e.Item.FindControl("lblChangePeriod");
                ExLabel lblUpdatedBy = (ExLabel)e.Item.FindControl("lblUpdatedBy");
                //string attVal = oAccountAttributeChangeReportInfo.Value;

                lblAccountNumber.Text = oAccountAttributeChangeReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oAccountAttributeChangeReportInfo.AccountName);
                lblAccountAttribute.Text = Helper.GetDisplayStringValue(oAccountAttributeChangeReportInfo.AccountAttribute);
                lblValue.Text = HttpContext.Current.Server.HtmlDecode(oAccountAttributeChangeReportInfo.Value);
                lblValidFrom.Text = Helper.GetDisplayDate(oAccountAttributeChangeReportInfo.ValidFrom);
                lblValidUntil.Text = Helper.GetDisplayDate(oAccountAttributeChangeReportInfo.ValidUntil);
                lblChangeDate.Text = Helper.GetDisplayDateTime(oAccountAttributeChangeReportInfo.ChangeDate);
                lblChangePeriod.Text = Helper.GetDisplayDate(oAccountAttributeChangeReportInfo.ChangePeriod);
                lblUpdatedBy.Text = Helper.GetDisplayStringValue(oAccountAttributeChangeReportInfo.UpdatedBy);
            }
        }

        public static void ItemDataBoundCertificationTrackingReport(Telerik.Web.UI.GridItemEventArgs e, DateTime? _CertificationStartDate)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                CertificationTrackingReportInfo oCertificationTrackingReportInfo = (CertificationTrackingReportInfo)e.Item.DataItem;

                ExLabel lblRole = (ExLabel)e.Item.FindControl("lblRole");
                ExLabel lblUserName = (ExLabel)e.Item.FindControl("lblUserName");

                ExLabel lblCountTotalAccountAssigned = (ExLabel)e.Item.FindControl("lblCountTotalAccountAssigned");
                ExLabel lblMadatoryReportSignOffDate = (ExLabel)e.Item.FindControl("lblMadatoryReportSignOffDate");
                ExLabel lblCertificationBalancesDate = (ExLabel)e.Item.FindControl("lblCertificationBalancesDate");
                ExLabel lblExceptionCertificationDate = (ExLabel)e.Item.FindControl("lblExceptionCertificationDate");
                ExLabel lblAccountCertificationDate = (ExLabel)e.Item.FindControl("lblAccountCertificationDate");

                lblRole.Text = oCertificationTrackingReportInfo.Role;
                lblUserName.Text = Helper.GetDisplayUserFullName(oCertificationTrackingReportInfo.FirstName, oCertificationTrackingReportInfo.LastName);
                if ((oCertificationTrackingReportInfo.RoleID.Value == (short)ARTEnums.UserRole.REVIEWER) ||
                    (oCertificationTrackingReportInfo.RoleID.Value == (short)ARTEnums.UserRole.APPROVER))
                {
                    lblMadatoryReportSignOffDate.Text = Helper.GetDisplayStringValue(ReportHelper.GetDisplayCertificationTrackingReportDate(oCertificationTrackingReportInfo.MadatoryReportSignOffDate, _CertificationStartDate));
                }
                else
                {
                    lblMadatoryReportSignOffDate.Text = WebConstants.NOT_AVAILABLE;
                }
                lblCountTotalAccountAssigned.Text = oCertificationTrackingReportInfo.CountTotalAccountAssigned.Value.ToString();
                lblCertificationBalancesDate.Text = Helper.GetDisplayStringValue(ReportHelper.GetDisplayCertificationTrackingReportDate(oCertificationTrackingReportInfo.CertificationBalancesDate, _CertificationStartDate));
                lblExceptionCertificationDate.Text = Helper.GetDisplayStringValue(ReportHelper.GetDisplayCertificationTrackingReportDate(oCertificationTrackingReportInfo.ExceptionCertificationDate, _CertificationStartDate));
                lblAccountCertificationDate.Text = Helper.GetDisplayStringValue(ReportHelper.GetDisplayCertificationTrackingReportDate(oCertificationTrackingReportInfo.AccountCertificationDate, _CertificationStartDate));

            }
        }


        public static void ItemDataBoundDelinquentAccountByUserReport(Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Header)
            {
                DelinquentAccountByUserReportInfo oDelinquentAccountByUserReportInfo = (DelinquentAccountByUserReportInfo)e.Item.DataItem;
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                Control oControlAmountReportingCurrency = new Control();
                Control oControlAmountBaseCurrency = new Control();

                oControlAmountReportingCurrency = (e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0];
                oControlAmountBaseCurrency = (e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0];


                if (oControlAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
                else
                {
                    if (oControlAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }


                if (oControlAmountBaseCurrency is LinkButton)
                {
                    //BCCY Changes
                    //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                    ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876);
                }
                else
                {
                    if (oControlAmountBaseCurrency is LiteralControl)
                    {
                        //BCCY Changes
                        //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876) + " (" + SessionHelper.BaseCurrencyCode + ")";
                        ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1876);
                    }
                }
                //******************************************************************************************************************************************

            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DelinquentAccountByUserReportInfo oDelinquentAccountByUserReportInfo = (DelinquentAccountByUserReportInfo)e.Item.DataItem;

                ExLabel lblRole = (ExLabel)e.Item.FindControl("lblRole");
                ExLabel lblUserName = (ExLabel)e.Item.FindControl("lblUserName");

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                ExLabel lblGLBalanceRC = (ExLabel)e.Item.FindControl("lblGLBalanceRC");
                ExLabel lblGLBalanceBC = (ExLabel)e.Item.FindControl("lblGLBalanceBC");
                //BCCY Changes


                ExLabel lblDueDate = (ExLabel)e.Item.FindControl("lblDueDate");
                ExLabel lblDaysLate = (ExLabel)e.Item.FindControl("lblDaysLate");
                ExLabel lblCountDelinquentAccount = (ExLabel)e.Item.FindControl("lblCountDelinquentAccount");

                lblRole.Text = Helper.GetDisplayStringValue(oDelinquentAccountByUserReportInfo.Role);
                lblUserName.Text = Helper.GetDisplayStringValue(Helper.GetDisplayUserFullName(oDelinquentAccountByUserReportInfo.FirstName, oDelinquentAccountByUserReportInfo.LastName));

                lblAccountNumber.Text = Helper.GetDisplayStringValue(oDelinquentAccountByUserReportInfo.AccountNumber);
                lblAccountName.Text = Helper.GetDisplayStringValue(oDelinquentAccountByUserReportInfo.AccountName);

                lblGLBalanceRC.Text = Helper.GetDisplayDecimalValue(oDelinquentAccountByUserReportInfo.GLBalanceReportingCurrency);
                lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(oDelinquentAccountByUserReportInfo.BaseCurrencyCode, (oDelinquentAccountByUserReportInfo.GLBalanceBaseCurrency));


                lblDueDate.Text = Helper.GetDisplayDate(oDelinquentAccountByUserReportInfo.DueDate);
                lblDaysLate.Text = Helper.GetDisplayStringValue(oDelinquentAccountByUserReportInfo.DaysLate.Value.ToString());
                lblCountDelinquentAccount.Text = Helper.GetDisplayStringValue(oDelinquentAccountByUserReportInfo.CountDelinquentAccount.Value.ToString());
            }
        }


        public static void ItemDataBoundIncompleteAccountAttributeReport(Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                IncompleteAccountAttributeReportInfo oIncompleteAccountAttributeReportInfo = (IncompleteAccountAttributeReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                ExLabel lblIsKeyAccount = (ExLabel)e.Item.FindControl("lblIsKeyAccount");
                ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                ExLabel lblFrequency = (ExLabel)e.Item.FindControl("lblFrequency");
                ExLabel lblIsZeroBalance = (ExLabel)e.Item.FindControl("lblIsZeroBalance");
                ExLabel lblRecFormType = (ExLabel)e.Item.FindControl("lblRecFormType");
                ExLabel lblIsPreparerDueDaysMissing = (ExLabel)e.Item.FindControl("lblIsPreparerDueDaysMissing");
                ExLabel lblIsReviewerDueDaysMissing = (ExLabel)e.Item.FindControl("lblIsReviewerDueDaysMissing");
                ExLabel lblIsApproverDueDaysMissing = (ExLabel)e.Item.FindControl("lblIsApproverDueDaysMissing");

                lblAccountNumber.Text = oIncompleteAccountAttributeReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oIncompleteAccountAttributeReportInfo.AccountName);

                lblIsKeyAccount.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsKeyAccountAttributeMissing);
                lblRiskRating.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsRiskRatingAttributeMissing);
                lblFrequency.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsFrequencyAttributeMissing);
                lblIsZeroBalance.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsZeroBalanceAttributeMissing);
                lblRecFormType.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsTemplateAttributeMissing);
                lblIsPreparerDueDaysMissing.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsPreparerDueDaysAttributeMissing);
                lblIsReviewerDueDaysMissing.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsReviewerDueDaysAttributeMissing);
                lblIsApproverDueDaysMissing.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(oIncompleteAccountAttributeReportInfo.IsApproverDueDaysAttributeMissing);
            }
        }


        public static void ItemDataBoundOpenItemsReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                OpenItemsReportInfo oOpenItemsReportInfo = (OpenItemsReportInfo)e.Item.DataItem;
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1900) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                ////((LinkButton)(e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1901) + " (" + SessionHelper.BaseCurrencyCode + ")";

                //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                Control oControlAmountReportingCurrency = new Control();
                Control oControlAmountBaseCurrency = new Control();

                oControlAmountReportingCurrency = (e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0];
                oControlAmountBaseCurrency = (e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0];


                if (oControlAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1900) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
                else
                {
                    if (oControlAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1900) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }

                if (oControlAmountBaseCurrency is LinkButton)
                {
                    //BCCY Changes
                    //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1901) + " (" + SessionHelper.BaseCurrencyCode + ")";
                    ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1901);
                }
                else
                {
                    if (oControlAmountBaseCurrency is LiteralControl)
                    {
                        //BCCY Changes
                        //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1901) + " (" + SessionHelper.BaseCurrencyCode + ")";
                        ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1901);

                    }
                }
                //******************************************************************************************************************************************

            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                OpenItemsReportInfo oOpenItemsReportInfo = (OpenItemsReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                ExLabel lblGLBalanceRC = (ExLabel)e.Item.FindControl("lblGLBalanceRC");
                ExLabel lblGLBalanceBC = (ExLabel)e.Item.FindControl("lblGLBalanceBC");


                ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                ExLabel lblIsMaterial = (ExLabel)e.Item.FindControl("lblIsMaterial");
                ExLabel lblIsKeyAccount = (ExLabel)e.Item.FindControl("lblIsKeyAccount");

                ExLabel lblPreparer = (ExLabel)e.Item.FindControl("lblPreparer");
                ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                ExLabel lblClassification = (ExLabel)e.Item.FindControl("lblClassification");

                lblAccountNumber.Text = oOpenItemsReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oOpenItemsReportInfo.AccountName);

                lblGLBalanceRC.Text = Helper.GetDisplayDecimalValue(oOpenItemsReportInfo.RecItemAmountReportingCurrency);
                lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(oOpenItemsReportInfo.BaseCurrencyCode, (oOpenItemsReportInfo.RecItemAmountBaseCurrency));


                lblRiskRating.Text = Helper.GetDisplayStringValue(oOpenItemsReportInfo.RiskRating);
                lblIsMaterial.Text = Helper.GetDisplayStringValue(ReportHelper.SetYesNoCodeBasedOnBool(oOpenItemsReportInfo.IsMaterial));
                lblIsKeyAccount.Text = Helper.GetDisplayStringValue(ReportHelper.SetYesNoCodeBasedOnBool(oOpenItemsReportInfo.IsKeyAccount));

                lblPreparer.Text = Helper.GetDisplayStringValue(Helper.GetDisplayUserFullName(oOpenItemsReportInfo.FirstName, oOpenItemsReportInfo.LastName));
                lblOpenDate.Text = Helper.GetDisplayDate(oOpenItemsReportInfo.OpenDate);
                lblAging.Text = Helper.GetDisplayStringValue(oOpenItemsReportInfo.AgingInDays.Value.ToString());
                lblClassification.Text = Helper.GetDisplayStringValue(oOpenItemsReportInfo.OpenItemClassification);

                ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                lblRecItemNumber.Text = Helper.GetDisplayStringValue(oOpenItemsReportInfo.RecItemNumber);

                ExLabel lblPeriodEndDate = (ExLabel)e.Item.FindControl("lblPeriodEndDate");
                if (lblPeriodEndDate != null)
                {
                    if (oOpenItemsReportInfo.PeriodEndDate != null)
                    {
                        lblPeriodEndDate.Text = Helper.GetDisplayDate(oOpenItemsReportInfo.PeriodEndDate);
                    }
                }

            }
        }


        public static void ItemDataBoundReconciliationStatusCountReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                ReconciliationStatusCountReportInfo oReconciliationStatusCountReportInfo = (ReconciliationStatusCountReportInfo)e.Item.DataItem;

                ExLabel lblRole = (ExLabel)e.Item.FindControl("lblRole");
                ExLabel lblUserName = (ExLabel)e.Item.FindControl("lblUserName");

                ExLabel lblCountPrepared = (ExLabel)e.Item.FindControl("lblCountPrepared");
                ExLabel lblCountInProgress = (ExLabel)e.Item.FindControl("lblCountInProgress");
                ExLabel lblCountPendingReview = (ExLabel)e.Item.FindControl("lblCountPendingReview");
                ExLabel lblCountPendingModificationPreparer = (ExLabel)e.Item.FindControl("lblCountPendingModificationPreparer");
                ExLabel lblCountReviewed = (ExLabel)e.Item.FindControl("lblCountReviewed");
                ExLabel lblCountPendingApproval = (ExLabel)e.Item.FindControl("lblCountPendingApproval");
                ExLabel lblCountApproved = (ExLabel)e.Item.FindControl("lblCountApproved");
                ExLabel lblCountNotstarted = (ExLabel)e.Item.FindControl("lblCountNotstarted");
                ExLabel lblCountSysReconciled = (ExLabel)e.Item.FindControl("lblCountSysReconciled");
                ExLabel lblCountReconciled = (ExLabel)e.Item.FindControl("lblCountReconciled");
                ExLabel lblCountPendingModificationReviewer = (ExLabel)e.Item.FindControl("lblCountPendingModificationReviewer");

                lblRole.Text = Helper.GetDisplayStringValue(oReconciliationStatusCountReportInfo.Role);
                lblUserName.Text = Helper.GetDisplayStringValue(Helper.GetDisplayUserFullName(oReconciliationStatusCountReportInfo.FirstName, oReconciliationStatusCountReportInfo.LastName));

                lblCountPrepared.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountPrepared);
                lblCountInProgress.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountInProgress);
                lblCountPendingReview.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountPendingReview);
                lblCountPendingModificationPreparer.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountPendingModificationPreparer);
                lblCountReviewed.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountReviewed);
                lblCountPendingApproval.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountPendingApproval);
                lblCountApproved.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountApproved);
                lblCountNotstarted.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountNotStarted);
                lblCountSysReconciled.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountSysReconciled);
                lblCountReconciled.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountReconciled);
                lblCountPendingModificationReviewer.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusCountReportInfo.CountPendingModificationReviewer);

            }
        }


        public static void ItemDataBoundUnassignedAccountsReport(Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                UnassignedAccountsReportInfo oUnassignedAccountsReportInfo = (UnassignedAccountsReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                ExLabel lblNetAcct = (ExLabel)e.Item.FindControl("lblNetAcct");
                ExLabel lblPreparer = (ExLabel)e.Item.FindControl("lblPreparer");
                ExLabel lblReviewer = (ExLabel)e.Item.FindControl("lblReviewer");
                ExLabel lblApprover = (ExLabel)e.Item.FindControl("lblApprover");

                lblAccountNumber.Text = oUnassignedAccountsReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oUnassignedAccountsReportInfo.AccountName);

                lblNetAcct.Text = ReportHelper.SetYesNoCodeBasedOnBool(oUnassignedAccountsReportInfo.IsNetAccount);
                lblPreparer.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(!oUnassignedAccountsReportInfo.IsPreparerSet);
                lblReviewer.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(!oUnassignedAccountsReportInfo.IsReviewerSet);
                lblApprover.Text = ReportHelper.SetMarkIfItemMissingBasedOnBool(!oUnassignedAccountsReportInfo.IsApproverSet);
            }
        }

        public static void ItemDataBoundQualityScoreReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {

                }
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    QualityScoreReportInfo oOpenItemsReportInfo = (QualityScoreReportInfo)e.Item.DataItem;
                    LanguageHelper.TranslateLabelsQualityScoreReportInfo(oOpenItemsReportInfo);
                    if (e.Item.OwnerTableView.Name == "QualityScoreDetails")
                    {
                        ((ExLabel)e.Item.FindControl("lblQualityScoreNumber")).Text = oOpenItemsReportInfo.QualityScoreNumber;
                        ((ExLabel)e.Item.FindControl("lblComments")).Text = oOpenItemsReportInfo.Comments;
                        ((ExLabel)e.Item.FindControl("lblQualityScoreDescription")).Text = oOpenItemsReportInfo.QualityScoreDesc;
                        if (oOpenItemsReportInfo.SystemQualityScoreStatusID.HasValue)
                            ((ExLabel)e.Item.FindControl("lblSystemQualityScoreStatus")).Text = LanguageUtil.GetValue((int)oOpenItemsReportInfo.SystemQualityScoreStatusID.Value);
                        if (oOpenItemsReportInfo.UserQualityScoreStatusID.HasValue)
                            ((ExLabel)e.Item.FindControl("lblUserQualityScoreStatus")).Text = LanguageUtil.GetValue((int)oOpenItemsReportInfo.UserQualityScoreStatusID.Value);
                    }


                    ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                    ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");

                    ExLabel lblGLBalanceRC = (ExLabel)e.Item.FindControl("lblGLBalanceRC");
                    ExLabel lblGLBalanceBC = (ExLabel)e.Item.FindControl("lblGLBalanceBC");


                    ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                    ExLabel lblIsMaterial = (ExLabel)e.Item.FindControl("lblIsMaterial");
                    ExLabel lblIsKeyAccount = (ExLabel)e.Item.FindControl("lblIsKeyAccount");

                    ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                    ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                    ExLabel lblClassification = (ExLabel)e.Item.FindControl("lblClassification");

                    ExLabel lblSystemScore = (ExLabel)e.Item.FindControl("lblSystemScore");
                    ExLabel lblUserScore = (ExLabel)e.Item.FindControl("lblUserScore");

                    lblSystemScore.Text = oOpenItemsReportInfo.SystemQualityScore.Value.ToString();
                    lblUserScore.Text = oOpenItemsReportInfo.UserQualityScore.Value.ToString();

                    lblAccountNumber.Text = oOpenItemsReportInfo.AccountNumber;
                    lblAccountName.Text = Helper.GetDisplayStringValue(oOpenItemsReportInfo.AccountName);

                }
            }
            catch (Exception)
            {
            }
        }

        public static void ItemDataBoundCompletionDateReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                CompletionDateReportInfo oCompletionDateReportInfo = (CompletionDateReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                ExLabel lblReconciliationStatus = (ExLabel)e.Item.FindControl("lblReconciliationStatus");
                ExLabel lblIsSRA = (ExLabel)e.Item.FindControl("lblIsSRA");

                ExLabel lblPreparedBy = (ExLabel)e.Item.FindControl("lblPreparedBy");
                ExLabel lblDatePrepared = (ExLabel)e.Item.FindControl("lblDatePrepared");

                ExLabel lblReviewedBy = (ExLabel)e.Item.FindControl("lblReviewedBy");
                ExLabel lblDateReviewed = (ExLabel)e.Item.FindControl("lblDateReviewed");

                ExLabel lblApprovedBy = (ExLabel)e.Item.FindControl("lblApprovedBy");
                ExLabel lblDateApproved = (ExLabel)e.Item.FindControl("lblDateApproved");

                ExLabel lblReconciledBy = (ExLabel)e.Item.FindControl("lblReconciledBy");
                ExLabel lblDateReconciled = (ExLabel)e.Item.FindControl("lblDateReconciled");
                ExLabel lblSysReconciledBy = (ExLabel)e.Item.FindControl("lblSysReconciledBy");


                lblAccountNumber.Text = oCompletionDateReportInfo.AccountNumber;
                lblAccountName.Text = Helper.GetDisplayStringValue(oCompletionDateReportInfo.AccountName);
                lblReconciliationStatus.Text = Helper.GetDisplayStringValue(oCompletionDateReportInfo.ReconciliationStatus);

                lblIsSRA.Text = Helper.GetDisplayStringValue(ReportHelper.SetYesNoCodeBasedOnBool(oCompletionDateReportInfo.IsSRA));

                lblPreparedBy.Text = Helper.GetDisplayStringValue(oCompletionDateReportInfo.PreparedBy);
                lblDatePrepared.Text = Helper.GetDisplayDate(oCompletionDateReportInfo.DatePrepared);

                lblReviewedBy.Text = Helper.GetDisplayStringValue(oCompletionDateReportInfo.ReviewedBy);
                lblDateReviewed.Text = Helper.GetDisplayDate(oCompletionDateReportInfo.DateReviewed);

                lblApprovedBy.Text = Helper.GetDisplayStringValue(oCompletionDateReportInfo.ApprovedBy);
                lblDateApproved.Text = Helper.GetDisplayDate(oCompletionDateReportInfo.DateApproved);

                lblReconciledBy.Text = Helper.GetDisplayStringValue(oCompletionDateReportInfo.ReconciledBy);
                lblDateReconciled.Text = Helper.GetDisplayDate(oCompletionDateReportInfo.DateReconciled);

                lblSysReconciledBy.Text = Helper.GetDisplayStringValue(oCompletionDateReportInfo.SysReconciledBy);
            }
        }

        public static void ClearReportSessions()
        {
            if (HttpContext.Current.Session[SessionConstants.REPORT_CRITERIA] != null)
                HttpContext.Current.Session.Remove(SessionConstants.REPORT_CRITERIA);
            if (HttpContext.Current.Session[SessionConstants.REPORT_INFO_OBJECT] != null)
                HttpContext.Current.Session.Remove(SessionConstants.REPORT_INFO_OBJECT);
        }


        public static void ShowCompanyLogo(Image imgCompanyLogo, Page ePage)
        {
            try
            {
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                string logoPath = oCompanyClient.GetCompanyLogo(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
                if (string.IsNullOrEmpty(logoPath))
                {
                    //imgCompanyLogo.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/NoLogoAvailable.jpg";
                    imgCompanyLogo.Visible = false;
                }
                else
                {
                    string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadCompanyLogo);
                    imgCompanyLogo.ImageUrl = url; //ePage.ResolveUrl("~/Pages/DownloadAttachment.aspx") + "?" + QueryStringConstants.FILE_PATH + "=" + logoPath;
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
        }

        public static void SetReportsBreadCrumb(PageBase oPageBase)
        {
            if (HttpContext.Current.Request.QueryString[QueryStringConstants.REPORT_SECTION_ID] != null)
            {
                Int32 reportSectionID;
                string IsReportActivity = string.Empty;
                if (HttpContext.Current.Request.QueryString[QueryStringConstants.IS_REPORT_ACTIVITY] != null)
                    IsReportActivity = HttpContext.Current.Request.QueryString[QueryStringConstants.IS_REPORT_ACTIVITY];
                if (Int32.TryParse(HttpContext.Current.Request.QueryString[QueryStringConstants.REPORT_SECTION_ID], out reportSectionID))
                {
                    PageBaseReport oPageBaseReport = oPageBase as PageBaseReport;
                    switch ((WebEnums.ReportSection)reportSectionID)
                    {
                        case WebEnums.ReportSection.MY_REPORT:
                            {
                                if (IsReportActivity == "1")
                                {
                                    if (oPageBaseReport == null)
                                        Helper.SetBreadcrumbs(oPageBase, 1073, 1077, oPageBase.PageTitleLabelID);
                                    else
                                        Helper.SetBreadcrumbs(oPageBase, 1073, 1077, 1616, oPageBase.PageTitleLabelID);

                                }
                                else
                                    Helper.SetBreadcrumbs(oPageBase, 1073, 1077, 1609, oPageBase.PageTitleLabelID);
                            }
                            break;
                        case WebEnums.ReportSection.STANDARD_REPORT:
                            {
                                if (IsReportActivity == "1" && oPageBaseReport != null)
                                    Helper.SetBreadcrumbs(oPageBase, 1073, 1076, 1616, oPageBase.PageTitleLabelID);
                                else
                                    Helper.SetBreadcrumbs(oPageBase, 1073, 1076, oPageBase.PageTitleLabelID);
                            }
                            break;
                    }
                }
            }
        }

        public static void ItemDataBoundReviewNotesByUserReport(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                ReviewNotesReportInfo oReviewNotesByUserReportInfo = (ReviewNotesReportInfo)e.Item.DataItem;

                ExLabel lblPeriod = (ExLabel)e.Item.FindControl("lblPeriod");
                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                ExLabel lblPreparer = (ExLabel)e.Item.FindControl("lblPreparer");
                ExLabel lblReviewNoteDate = (ExLabel)e.Item.FindControl("lblReviewNoteDate");
                ExLabel lblSubject = (ExLabel)e.Item.FindControl("lblSubject");
                ExLabel lblEnteredBy = (ExLabel)e.Item.FindControl("lblEnteredBy");
                ExLabel lblRole = (ExLabel)e.Item.FindControl("lblRole");
                ExLabel lblReviewNotes = (ExLabel)e.Item.FindControl("lblReviewNotes");
                ExLabel lblReviewer = (ExLabel)e.Item.FindControl("lblReviewer");


                lblPeriod.Text = Helper.GetDisplayDate(oReviewNotesByUserReportInfo.Period);
                lblAccountNumber.Text = Helper.GetDisplayStringValue(oReviewNotesByUserReportInfo.AccountNumber);
                lblAccountName.Text = Helper.GetDisplayStringValue(oReviewNotesByUserReportInfo.AccountName);
                lblPreparer.Text = Helper.GetDisplayStringValue(oReviewNotesByUserReportInfo.Perparer);
                lblReviewer.Text = Helper.GetDisplayStringValue(oReviewNotesByUserReportInfo.Reviewer);
                lblReviewNoteDate.Text = Helper.GetDisplayDate(oReviewNotesByUserReportInfo.ReviewNoteDate);
                lblSubject.Text = Helper.GetDisplayStringValue(oReviewNotesByUserReportInfo.Subject);
                lblEnteredBy.Text = Helper.GetDisplayStringValue(oReviewNotesByUserReportInfo.AddedByFullName);

                if (oReviewNotesByUserReportInfo.AddedByUserRole != null)
                    lblRole.Text = Helper.GetRoleName(oReviewNotesByUserReportInfo.AddedByUserRole);
                else
                    lblRole.Text = "-";
                lblReviewNotes.Text = Helper.GetDisplayStringValue(oReviewNotesByUserReportInfo.ReviewNote);
            }
        }
        public static void ItemDataBoundTaskCompletionReport(Telerik.Web.UI.GridItemEventArgs e, RadGrid rg)
        {

            if (e.Item.ItemType == GridItemType.Header)
            {
                List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = TaskMasterHelper.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID);
                GridColumn oGridColumn1 = rg.MasterTableView.Columns.FindByUniqueName("TaskCustomField1");
                GridColumn oGridColumn2 = rg.MasterTableView.Columns.FindByUniqueName("TaskCustomField2");
                if (oTaskCustomFieldInfoList != null && oTaskCustomFieldInfoList.Count > 0)
                {
                    GridHeaderItem item = e.Item as GridHeaderItem;

                    if (oGridColumn1 != null)
                    {
                        string CustomField1 = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == (short)WebEnums.TaskCustomField.CustomField1).CustomFieldValue;
                        if (!string.IsNullOrEmpty(CustomField1))
                        {
                            item["TaskCustomField1"].Text = CustomField1;
                        }
                        else
                        {
                            oGridColumn1.Visible = false;
                        }
                    }

                    if (oGridColumn2 != null)
                    {
                        string CustomField2 = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == (short)WebEnums.TaskCustomField.CustomField2).CustomFieldValue;
                        if (!string.IsNullOrEmpty(CustomField2))
                        {
                            item["TaskCustomField2"].Text = CustomField2;
                        }
                        else
                        {
                            oGridColumn2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (oGridColumn1 != null)
                        oGridColumn1.Visible = false;
                    if (oGridColumn2 != null)
                        oGridColumn2.Visible = false;
                }

            }

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                TaskCompletionReportInfo oTaskCompletionReportInfo = (TaskCompletionReportInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                ExLabel lblTaskListName = (ExLabel)e.Item.FindControl("lblTaskListName");
                ExLabel lblTaskNumber = (ExLabel)e.Item.FindControl("lblTaskNumber");

                ExLabel lblTaskName = (ExLabel)e.Item.FindControl("lblTaskName");
                ExLabel lblTaskDescription = (ExLabel)e.Item.FindControl("lblTaskDescription");

                ExLabel lblTaskStatus = (ExLabel)e.Item.FindControl("lblTaskStatus");
                ExLabel lblAssignedTo = (ExLabel)e.Item.FindControl("lblAssignedTo");
                ExLabel lblTaskReviewer = (ExLabel)e.Item.FindControl("lblTaskReviewer");
                ExLabel lblTaskApprover = (ExLabel)e.Item.FindControl("lblTaskApprover");
                ExLabel lblCreatedBy = (ExLabel)e.Item.FindControl("lblCreatedBy");

                ExLabel lblDateCreated = (ExLabel)e.Item.FindControl("lblDateCreated");
                ExLabel lblDoneBy = (ExLabel)e.Item.FindControl("lblDoneBy");
                ExLabel lblDateDone = (ExLabel)e.Item.FindControl("lblDateDone");
                ExLabel lblApprovedBy = (ExLabel)e.Item.FindControl("lblApprovedBy");
                ExLabel lblDateApproved = (ExLabel)e.Item.FindControl("lblDateApproved");
                ExLabel lblReviewedBy = (ExLabel)e.Item.FindControl("lblReviewedBy");
                ExLabel lblDateReviewed = (ExLabel)e.Item.FindControl("lblDateReviewed");
                ExLabel lblTaskCustomField1 = (ExLabel)e.Item.FindControl("lblTaskCustomField1");
                ExLabel lblTaskCustomField2 = (ExLabel)e.Item.FindControl("lblTaskCustomField2");




                lblAccountNumber.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.AccountNumber);
                lblAccountName.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.AccountName);
                lblTaskListName.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskList.TaskListName);
                lblTaskNumber.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskNumber);
                lblTaskName.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskName);
                lblTaskDescription.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskDescription);
                lblTaskStatus.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskStatus);
                lblDateCreated.Text = Helper.GetDisplayDate(oTaskCompletionReportInfo.DateAdded);
                //lblAssignedTo.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.AssignedTo.FirstName, oTaskCompletionReportInfo.AssignedTo.LastName);
                //lblTaskApprover.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.Approver.FirstName, oTaskCompletionReportInfo.Approver.LastName);
                lblAssignedTo.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskOwner);
                lblTaskReviewer.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskReviewer);
                lblTaskApprover.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.TaskApprover);
                lblCreatedBy.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.TaskDetailAddedByUser.FirstName, oTaskCompletionReportInfo.TaskDetailAddedByUser.LastName);

                //if (oTaskCompletionReportInfo.Approver.UserID.HasValue && oTaskCompletionReportInfo.Approver.UserID > 0)
                //{
                //    lblDoneBy.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.TaskDetailDoneByUser.FirstName, oTaskCompletionReportInfo.TaskDetailDoneByUser.LastName);
                //    lblApprovedBy.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.TaskDetailApprovedByUser.FirstName, oTaskCompletionReportInfo.TaskDetailApprovedByUser.LastName);
                //    lblDateApproved.Text = Helper.GetDisplayDate(oTaskCompletionReportInfo.DateApproved);
                //    lblDateDone.Text = Helper.GetDisplayDate(oTaskCompletionReportInfo.DateDone);
                //}
                //else
                //{
                //    lblDoneBy.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.TaskDetailApprovedByUser.FirstName, oTaskCompletionReportInfo.TaskDetailApprovedByUser.LastName);
                //    lblApprovedBy.Text = Helper.GetDisplayStringValue(null);
                //    lblDateApproved.Text = Helper.GetDisplayStringValue(null);
                //    lblDateDone.Text = Helper.GetDisplayDate(oTaskCompletionReportInfo.DateApproved);
                //}

                if (oTaskCompletionReportInfo.TaskDetailDoneByUser != null && oTaskCompletionReportInfo.TaskDetailDoneByUser.UserID.HasValue)
                {
                    lblDoneBy.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.TaskDetailDoneByUser.FirstName, oTaskCompletionReportInfo.TaskDetailDoneByUser.LastName);
                    lblDateDone.Text = Helper.GetDisplayDate(oTaskCompletionReportInfo.DateDone);
                }
                else
                {
                    lblDoneBy.Text = Helper.GetDisplayStringValue(null);
                    lblDateDone.Text = Helper.GetDisplayDate(null);
                }
                if (oTaskCompletionReportInfo.TaskDetailReviewedByUser != null && oTaskCompletionReportInfo.TaskDetailReviewedByUser.UserID.HasValue)
                {
                    lblReviewedBy.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.TaskDetailReviewedByUser.FirstName, oTaskCompletionReportInfo.TaskDetailReviewedByUser.LastName);
                    lblDateReviewed.Text = Helper.GetDisplayDate(oTaskCompletionReportInfo.DateReviewed);
                }
                else
                {
                    lblReviewedBy.Text = Helper.GetDisplayStringValue(null);
                    lblDateReviewed.Text = Helper.GetDisplayDate(null);
                }
                if (oTaskCompletionReportInfo.TaskDetailApprovedByUser != null && oTaskCompletionReportInfo.TaskDetailApprovedByUser.UserID.HasValue)
                {
                    lblApprovedBy.Text = Helper.GetDisplayUserFullName(oTaskCompletionReportInfo.TaskDetailApprovedByUser.FirstName, oTaskCompletionReportInfo.TaskDetailApprovedByUser.LastName);
                    lblDateApproved.Text = Helper.GetDisplayDate(oTaskCompletionReportInfo.DateApproved);
                }
                else
                {
                    lblApprovedBy.Text = Helper.GetDisplayStringValue(null);
                    lblDateApproved.Text = Helper.GetDisplayDate(null);
                }

                if (lblTaskCustomField1 != null)
                    lblTaskCustomField1.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.CustomField1);
                if (lblTaskCustomField2 != null)
                    lblTaskCustomField2.Text = Helper.GetDisplayStringValue(oTaskCompletionReportInfo.CustomField2);


            }
        }

        public static void ShowHideGridColumns(ExRadGrid rg, short ReportID, string SelectedDisplaycolumn, short taskType)
        {
            List<ReportColumnInfo> oReportColumnInfoList = SessionHelper.GetReportColumnInfoList(ReportID, true);
            GridHelper.HideColumns(rg.Columns, oReportColumnInfoList);
            // Hide  FSCaption and AccountType
            List<string> ColumnNameList = new List<string>();
            ColumnNameList.Add("FSCaption");
            ColumnNameList.Add("AccountType");
            GridHelper.HideColumns(rg.Columns, ColumnNameList);
            Char FilterValueSeparator = Convert.ToChar(AppSettingHelper.GetAppSettingValue(AppSettingConstants.FILTER_VALUE_SEPARATOR));
            if (!string.IsNullOrEmpty(SelectedDisplaycolumn))
            {
                List<ReportColumnInfo> oSelectedDisplaycolumnList = new List<ReportColumnInfo>();
                string[] ids = SelectedDisplaycolumn.Split(FilterValueSeparator);
                oSelectedDisplaycolumnList = (from item in oReportColumnInfoList
                                              where ids.Contains(item.ColumnID.ToString())
                                              select item).ToList();
                if (oSelectedDisplaycolumnList != null && oSelectedDisplaycolumnList.Count > 0)
                    GridHelper.ShowColumns(rg.Columns, oSelectedDisplaycolumnList);
            }
            if (taskType == (short)ARTEnums.TaskType.GeneralTask)
            {
                // Hide Account related Columns
                List<ReportColumnInfo> oAllReportColumnInfoList = SessionHelper.GetReportColumnInfoList(ReportID, null);
                List<ReportColumnInfo> oAccountRelatedColumnInfoList = (from item in oAllReportColumnInfoList
                                                                        where item.ColumnTypeID == 1  //1 for Account
                                                                        select item).ToList();
                if (oAccountRelatedColumnInfoList != null && oAccountRelatedColumnInfoList.Count > 0)
                    GridHelper.HideColumns(rg.Columns, oAccountRelatedColumnInfoList);
            }
        }


    }
}
