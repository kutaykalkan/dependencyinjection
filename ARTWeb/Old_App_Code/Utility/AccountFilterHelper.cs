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
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Web.Utility
{
    public class AccountFilterHelper
    {

        public AccountFilterHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static readonly string AccountFilterValueSeparator = AppSettingHelper.GetAppSettingValue(AppSettingConstants.ACCOUNT_FILTER_VALUE_SEPARATOR);

        #region "Public Methods"
        public static ListItemCollection GetAllFields(ARTEnums.Grid eGrid)
        {
            ListItemCollection oFieldCollection = new ListItemCollection();
            //Vinay: Commented below as it is creating duplicate column names
            //List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = GetDynamicFieldNames();
            //foreach (GeographyStructureHdrInfo oGeoStructInfo in oGeographyStructureHdrInfoCollection)
            //{
            //    ListItem oItem = new ListItem(oGeoStructInfo.Name, oGeoStructInfo.GeographyClassID.Value.ToString ());
            //    oFieldCollection.Add(oItem);
            //}


            List<GridColumnInfo> oGridColumnInfoCollection = GetStaticFieldNames(eGrid);
            foreach (GridColumnInfo oColumn in oGridColumnInfoCollection)
            {
                ListItem oItem = new ListItem(oColumn.Name, oColumn.ColumnID.Value.ToString());
                oFieldCollection.Add(oItem);
            }

            return oFieldCollection;
        }

        public static List<OperatorMstInfo> GetOperatorsByColumnID(short columnID)
        {
            //TODO: Need to pick from SessionHelper
            List<OperatorMstInfo> oOperatorCollection = null;
            IOperator oOperatorClient = RemotingHelper.GetOperatorObject();
            oOperatorCollection = oOperatorClient.GetOperatorsByColumnID(columnID, Helper.GetAppUserInfo());
            TranslateOperatorInfo(oOperatorCollection);
            return oOperatorCollection;
        }

        public static ListItemCollection GetListItemForAccountType()
        {
            List<AccountTypeMstInfo> oAcctTypeInfoCollection = SessionHelper.SelectAllAccountTypeMstInfoWithDisplayText();
            ListItemCollection itemCollection = new ListItemCollection();
            foreach (AccountTypeMstInfo oAcctTypeInfo in oAcctTypeInfoCollection)
            {
                ListItem item = new ListItem();
                item.Text = oAcctTypeInfo.AccountType;
                item.Value = oAcctTypeInfo.AccountTypeID.Value.ToString();
                itemCollection.Add(item);
            }
            return itemCollection;
        }

        public static ListItemCollection GetListItemForCertStatus()
        {
            List<CertificationStatusMstInfo> oCertStatusCollection = SessionHelper.GetCertificationStatus();
            ListItemCollection itemCollection = new ListItemCollection();
            foreach (CertificationStatusMstInfo oCertStatusInfo in oCertStatusCollection)
            {
                ListItem item = new ListItem();
                item.Text = oCertStatusInfo.CertificationStatus;
                item.Value = oCertStatusInfo.CertificationStatusID.Value.ToString();
                itemCollection.Add(item);
            }
            return itemCollection;
        }

        public static ListItemCollection GetListItemsForUsers(int companyID)
        {
            ListItemCollection itemCollection = new ListItemCollection();
            IUser oUserClient = RemotingHelper.GetUserObject();
            Dictionary<int, string> oDictUser = new Dictionary<int, string>();

            List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectAllUsersRolesAndEmailID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());

            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                if (!oDictUser.ContainsKey(oUserHdrInfo.UserID.Value))
                {
                    oDictUser.Add(oUserHdrInfo.UserID.Value, oUserHdrInfo.FirstName);
                    ListItem item = new ListItem();
                    item.Value = oUserHdrInfo.UserID.Value.ToString();
                    item.Text = Helper.GetDisplayUserFullName(oUserHdrInfo.FirstName, oUserHdrInfo.LastName);
                    itemCollection.Add(item);
                }
            }
            return itemCollection;
        }

        public static ListItemCollection GetListItemsForRole(int companyID, int recPeriodID, short acctAttrIDForRole)
        {
            ListItemCollection itemCollection = new ListItemCollection();
            IUser oUserClient = RemotingHelper.GetUserObject();
            List<UserHdrInfo> oUserHdrCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDRecPeriodIDAcctAttrIDForRole(companyID, recPeriodID, acctAttrIDForRole, Helper.GetAppUserInfo());
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrCollection)
            {
                ListItem item = new ListItem();
                item.Value = oUserHdrInfo.UserID.Value.ToString();
                item.Text = Helper.GetDisplayUserFullName(oUserHdrInfo.FirstName, oUserHdrInfo.LastName);
                itemCollection.Add(item);
            }
            return itemCollection;
        }

        public static string GetColumnNameFromColumnID(ARTEnums.Grid eGrid, short columnID)
        {
            string retVal = "";
            ListItemCollection itemCollection = GetAllFields(eGrid);
            ListItem item = itemCollection.FindByValue(columnID.ToString());
            if (item != null)
                retVal = item.Text;
            return retVal;
        }

        public static string GetOperatorNameByOperatorID(short columnID, short operatorID)
        {
            string retVal = "";
            List<OperatorMstInfo> oOperatorCollection = GetOperatorsByColumnID(columnID);
            OperatorMstInfo operatorMstInfo = oOperatorCollection.Find(r => r.OperatorID.Value == operatorID);
            if (operatorMstInfo != null)
                retVal = operatorMstInfo.OperatorName;
            return retVal;
        }

        public static string MapIDsToValues(short columnID, short operatorID, string value)
        {
            return value;
        }

        public static void AddCriteriaToSessionByDashBoardRecStatus(short columnID, short operatorID, string value, ARTEnums.Grid eGrid)
        {
            string sessionKey = SessionHelper.GetSessionKeyForGridFilter(eGrid);

            FilterCriteria fltr = new FilterCriteria();
            fltr.ParameterID = columnID;
            fltr.OperatorID = operatorID;
            fltr.Value = value;

            string[] RecStatusIDArr = value.Split(AccountFilterHelper.AccountFilterValueSeparator.ToCharArray());
            string displayValue = string.Empty;
            foreach (string RecStatusID in RecStatusIDArr)
            {
                short TempRecStatusID = Convert.ToInt16(RecStatusID);
                ReconciliationStatusMstInfo oReconciliationStatusMstInfo = CacheHelper.GetAllRecStatus().Where(recStat => recStat.ReconciliationStatusID == TempRecStatusID).FirstOrDefault();

                if (oReconciliationStatusMstInfo != null)
                {
                    if (displayValue == string.Empty)
                    {
                        displayValue = Helper.GetLabelIDValue(oReconciliationStatusMstInfo.ReconciliationStatusLabelID.HasValue ? oReconciliationStatusMstInfo.ReconciliationStatusLabelID.Value : 0);
                    }
                    else
                    {
                        displayValue = displayValue + "," + Helper.GetLabelIDValue(oReconciliationStatusMstInfo.ReconciliationStatusLabelID.HasValue ? oReconciliationStatusMstInfo.ReconciliationStatusLabelID.Value : 0);
                    }
                }
            }
            fltr.DisplayValue = displayValue;

            List<FilterCriteria> oFltrCriteriaCollection = HttpContext.Current.Session[sessionKey] as List<FilterCriteria>;
            if (oFltrCriteriaCollection == null)
                oFltrCriteriaCollection = new List<FilterCriteria>();
            oFltrCriteriaCollection.Add(fltr);
            HttpContext.Current.Session[sessionKey] = oFltrCriteriaCollection;
        }

        public static void AddCriteriaToSessionByDashBoardFSCaption(short columnID, short operatorID, string value, ARTEnums.Grid eGrid)
        {
            string sessionKey = SessionHelper.GetSessionKeyForGridFilter(eGrid);

            FilterCriteria fltr = new FilterCriteria();
            fltr.ParameterID = columnID;
            fltr.OperatorID = operatorID;
            fltr.Value = value;
            fltr.DisplayValue = value;
            List<FilterCriteria> oFltrCriteriaCollection = HttpContext.Current.Session[sessionKey] as List<FilterCriteria>;
            if (oFltrCriteriaCollection == null)
                oFltrCriteriaCollection = new List<FilterCriteria>();
            oFltrCriteriaCollection.Add(fltr);
            HttpContext.Current.Session[sessionKey] = oFltrCriteriaCollection;
        }

        #region Task Master"
        public static ListItemCollection GetListItemsForRecurrenceType()
        {
            ListItemCollection itemCollection = new ListItemCollection();
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();

            List<TaskRecurrenceTypeMstInfo> TaskRecurrenceInfoList = oTaskMasterClient.GetTaskRecurrenceTypeMstInfoCollection(Helper.GetAppUserInfo());
            foreach (TaskRecurrenceTypeMstInfo oTaskRecurrenceTypeMstInfo in TaskRecurrenceInfoList)
            {
                ListItem item = new ListItem();
                item.Value = oTaskRecurrenceTypeMstInfo.TaskRecurrenceTypeID.Value.ToString();
                item.Text = LanguageUtil.GetValue(oTaskRecurrenceTypeMstInfo.RecurrenceTypeLabelID.Value);

                itemCollection.Add(item);
            }
            return itemCollection;
        }

        public static ListItemCollection GetListItemsForTaskList()
        {
            ListItemCollection itemCollection = new ListItemCollection();
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();

            List<TaskListHdrInfo> TaskListHdrInfoList = oTaskMasterClient.GetTaskListHdrInfoCollection(SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
            foreach (TaskListHdrInfo oTaskListHdrInfo in TaskListHdrInfoList)
            {
                ListItem item = new ListItem();
                item.Value = oTaskListHdrInfo.TaskListID.Value.ToString();
                item.Text = oTaskListHdrInfo.TaskListName;

                itemCollection.Add(item);
            }
            return itemCollection;
        }

        public static ListItemCollection GetListItemsForTaskSubList()
        {
            ListItemCollection itemCollection = new ListItemCollection();
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();

            List<TaskSubListHdrInfo> TaskSubListHdrInfoList = oTaskMasterClient.GetTaskSubListHdrInfoCollection(SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
            foreach (TaskSubListHdrInfo oTaskSubListHdrInfo in TaskSubListHdrInfoList)
            {
                ListItem item = new ListItem();
                item.Value = oTaskSubListHdrInfo.TaskSubListID.Value.ToString();
                item.Text = oTaskSubListHdrInfo.TaskSubListName;

                itemCollection.Add(item);
            }
            return itemCollection;
        }

        public static ListItemCollection GetListItemsForTaskStatus()
        {
            ListItemCollection itemCollection = new ListItemCollection();
            List<TaskStatusMstInfo> oTaskStatusMstInfoList = SessionHelper.GetTaskStatus();
            foreach (TaskStatusMstInfo oEntity in oTaskStatusMstInfoList)
            {
                ListItem item = new ListItem();
                item.Text = oEntity.Name;
                item.Value = oEntity.TaskStatusID.Value.ToString();
                itemCollection.Add(item);
            }

            return itemCollection;

        }

        #endregion

        #endregion

        #region "Private Methods"
        private static List<GridColumnInfo> GetStaticFieldNames(ARTEnums.Grid eGrid)
        {
            //TODO: Need to get it from sessionhelper
            IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            List<GridColumnInfo> oGridColumnInfoCollection = oReconciliationPeriodClient.GetAllGridColumnsForRecPeriod(eGrid, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            TranslateGridColumnInfo(oGridColumnInfoCollection);
            GridHelper.UpdateGridColumnInfoForCustomField(oGridColumnInfoCollection);
            return oGridColumnInfoCollection;

        }

        private static List<GeographyStructureHdrInfo> GetDynamicFieldNames()
        {
            List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(SessionHelper.CurrentCompanyID);
            oGeographyStructureHdrInfoCollection = oGeographyStructureHdrInfoCollection.Where(a => a.GeographyClassID != (int)WebEnums.GeographyClass.Company).ToList();
            return oGeographyStructureHdrInfoCollection;
        }

        private static void TranslateGridColumnInfo(List<GridColumnInfo> oGridColumnInfoCollection)
        {
            for (int i = 0; i < oGridColumnInfoCollection.Count; i++)
            {
                oGridColumnInfoCollection[i].Name = LanguageUtil.GetValue(oGridColumnInfoCollection[i].LabelID.Value);
            }
        }

        private static void TranslateOperatorInfo(List<OperatorMstInfo> oOperatorCollection)
        {
            for (int i = 0; i < oOperatorCollection.Count; i++)
            {
                oOperatorCollection[i].OperatorName = LanguageUtil.GetValue(oOperatorCollection[i].OperatorNameLabelID.Value);
            }
        }

        #endregion

        public static List<OperatorMstInfo> GetOperatorsByDynamicColumnID(short selectedValue)
        {
            List<OperatorMstInfo> oOperatorCollection = null;
            IOperator oOperatorClient = RemotingHelper.GetOperatorObject();
            oOperatorCollection = oOperatorClient.GetOperatorsByDynamicColumnID(selectedValue, Helper.GetAppUserInfo());
            TranslateOperatorInfo(oOperatorCollection);
            return oOperatorCollection;
        }
    }
}
