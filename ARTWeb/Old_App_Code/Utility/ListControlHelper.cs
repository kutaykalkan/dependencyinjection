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
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.Language.LanguageClient.Model;
using SkyStem.ART.Client.Model.RecControlCheckList;

/// <summary>
/// Summary description for ListControlHelper
/// </summary>
public class ListControlHelper
{
    public ListControlHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void BindCompanyDropdown(DropDownList ddlCompany)
    {
        ddlCompany.DataSource = SessionHelper.GetAllCompaniesLiteObject();
        ddlCompany.DataTextField = "DisplayName";
        ddlCompany.DataValueField = "CompanyID";
        ddlCompany.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ddl"></param>
    /// 
    public static void AddListItemForCCY(DropDownList ddl)
    {
        string selectOneText = "-";
        string selectOneValue = WebConstants.SELECT_ONE;
        ListItem oListItem = new ListItem(selectOneText, selectOneValue);
        ddl.Items.Insert(0, oListItem);
    }
    public static void AddListItemForSelectOne(DropDownList ddl)
    {
        string selectOneText = LanguageUtil.GetValue(1343);
        string selectOneValue = WebConstants.SELECT_ONE;
        ListItem oListItem = new ListItem(selectOneText, selectOneValue);
        ddl.Items.Insert(0, oListItem);
    }

    public static void AddListItemForSelectAll(DropDownList ddl)
    {
        string selectOneText = LanguageUtil.GetValue(1262);
        string selectOneValue = WebConstants.SELECT_ALL;
        ListItem oListItem = new ListItem(selectOneText, selectOneValue);
        ddl.Items.Insert(ddl.Items.Count, oListItem);
    }

    public static void AddListItemForSelectOneAndAll(DropDownList ddl)
    {
        AddListItemForSelectOne(ddl);
        AddListItemForSelectAll(ddl);
    }

    /// <summary>
    /// Add List Item as "Create New"
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="index">Position at which to show "Create New"</param>
    public static void AddListItemForCreateNew(DropDownList ddl, int index)
    {
        ListItem li = new System.Web.UI.WebControls.ListItem(LanguageUtil.GetValue(1622), WebConstants.CREATE_NEW);
        ddl.Items.Insert(index, li);
    }

    public static void BindRoleDropdown(DropDownList ddlRole)
    {
        ddlRole.DataSource = SessionHelper.GetUserRole();
        ddlRole.DataTextField = "Role";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
    }

    public static void BindReconciliationFrequency(DropDownList ddlReconciliationFrequency)
    {
        ddlReconciliationFrequency.DataSource = SessionHelper.GetAllReconciliationFrequency();
        ddlReconciliationFrequency.DataTextField = "ReconciliationFrequency";
        ddlReconciliationFrequency.DataValueField = "ReconciliationFrequencyID";
        ddlReconciliationFrequency.DataTextFormatString = "{0:d}";
        ddlReconciliationFrequency.DataBind();
    }

    public static void BindMaterialityType(DropDownList ddlMaterialityType, bool bShowSelectOne)
    {
        ddlMaterialityType.DataSource = SessionHelper.GetAllMaterialityType();
        ddlMaterialityType.DataTextField = "MaterialityType";
        ddlMaterialityType.DataValueField = "MaterialityTypeID";
        ddlMaterialityType.DataBind();

        if (bShowSelectOne)
        {
            ListControlHelper.AddListItemForSelectOne(ddlMaterialityType);
        }
    }

    public static void BindReconciliationPeriod(DropDownList ddlReconciliationPeriod, int? currentFinancialYearID)
    {
        if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.AUDIT)
        {
            ddlReconciliationPeriod.DataSource = CacheHelper.GetAllReconciliationPeriodsForAuditRole(currentFinancialYearID);
        }
        else
        {
            ddlReconciliationPeriod.DataSource = CacheHelper.GetAllReconciliationPeriods(currentFinancialYearID);
        }
        ddlReconciliationPeriod.DataTextField = "PeriodEndDate";
        ddlReconciliationPeriod.DataValueField = "ReconciliationPeriodID";
        ddlReconciliationPeriod.DataTextFormatString = "{0:d}";
        ddlReconciliationPeriod.DataBind();
    }

    public static void BindReason(ListControl ddlReason)
    {
        //ddlReason.DataSource = CacheHelper.GetAllReasons();
        ddlReason.DataSource = SessionHelper.GetAllReasons();
        ddlReason.DataTextField = "Reason";
        ddlReason.DataValueField = "ReasonID";
        ddlReason.DataBind();
    }

    public static void BindReconciliationPeriodForRiskRating(CheckBoxList cblReconciliationPeriod, int? financialYearID)
    {
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, financialYearID, Helper.GetAppUserInfo());

        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollectionWithCondition = new List<ReconciliationPeriodInfo>();
        foreach (ReconciliationPeriodInfo item in oReconciliationPeriodInfoCollection)
        {
            if (item.PeriodEndDate >= SessionHelper.CurrentReconciliationPeriodEndDate)
            {
                oReconciliationPeriodInfoCollectionWithCondition.Add(item);
            }
        }
        cblReconciliationPeriod.DataSource = oReconciliationPeriodInfoCollectionWithCondition;
        cblReconciliationPeriod.DataTextField = "PeriodEndDate";
        cblReconciliationPeriod.DataValueField = "ReconciliationPeriodID";
        //cblReconciliationPeriod.DataTextFormatString = "{0:d}";
        cblReconciliationPeriod.DataBind();
    }


    public static void BindOrganizationalHierarchyKeysListBox(ListBox lstHierarchyKeys)
    {
        lstHierarchyKeys.DataSource = SessionHelper.GetAllOrganizationalHierarchyKeys(GeographyClassCompanyKey.GEOGRAPHYCLASSID);
        lstHierarchyKeys.DataTextField = "GeographyClassName";
        lstHierarchyKeys.DataValueField = "GeographyClassID";
        lstHierarchyKeys.DataBind();
    }

    public static void BindRiskRatingDropdown(DropDownList ddlRiskRating)
    {
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        List<RiskRatingMstInfo> lstRiskRatingMstInfo = oUtilityClient.SelectAllRiskRating(Helper.GetAppUserInfo());
        ddlRiskRating.DataSource = lstRiskRatingMstInfo;
        ddlRiskRating.DataTextField = "RiskRating";
        ddlRiskRating.DataValueField = "RiskRatingID";
        ddlRiskRating.DataBind();
    }
    public static void BindRiskRatingListControl(ListControl ddlRiskRating)
    {
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        List<RiskRatingMstInfo> lstRiskRatingMstInfo = oUtilityClient.SelectAllRiskRating(Helper.GetAppUserInfo());
        ddlRiskRating.DataSource = lstRiskRatingMstInfo;
        ddlRiskRating.DataTextField = "RiskRating";
        ddlRiskRating.DataValueField = "RiskRatingID";
        ddlRiskRating.DataBind();
    }
    public static void BindYesNoDropdown(DropDownList ddlYesNo)
    {
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        List<ReconciliationCheckListStatusInfo> lstReconciliationCheckListStatusInfo = oUtilityClient.GetReconciliationCheckListStatus(Helper.GetAppUserInfo());
        lstReconciliationCheckListStatusInfo = LanguageHelper.TranslateReconciliationCheckListStatusCollection(lstReconciliationCheckListStatusInfo);
        ddlYesNo.DataSource = lstReconciliationCheckListStatusInfo;
        ddlYesNo.DataTextField = "ReconciliationCheckListStatus";
        ddlYesNo.DataValueField = "ReconciliationCheckListStatusLabelID";
        ddlYesNo.DataBind();
    }

    public static void BindCurrencyDropdown(DropDownList ddlLocalCurrency, string mode, bool isMultiCurrencyEnabled, GLDataRecItemInfo oGLDataRecItemInfo, GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, GLDataWriteOnOffInfo oGLDataWriteOnOffInfo)
    {
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        List<string> localCurrencyCollection = oGLDataClient.SelectLocalCurrency(SessionHelper.CurrentReconciliationPeriodID.Value, isMultiCurrencyEnabled, Helper.GetAppUserInfo());
        ddlLocalCurrency.DataSource = localCurrencyCollection;
        ddlLocalCurrency.DataBind();

        if (isMultiCurrencyEnabled && mode != QueryStringConstants.EDIT)
        {
            ListControlHelper.AddListItemForCCY(ddlLocalCurrency);
        }
        if (oGLDataWriteOnOffInfo != null && isMultiCurrencyEnabled && mode == QueryStringConstants.EDIT)
        {
            ddlLocalCurrency.SelectedValue = oGLDataWriteOnOffInfo.LocalCurrencyCode;//TODO: on edit
        }
        else if (oGLDataRecItemInfo != null && isMultiCurrencyEnabled && mode == QueryStringConstants.EDIT)
        {
            ddlLocalCurrency.SelectedValue = oGLDataRecItemInfo.LocalCurrencyCode;//TODO: on edit
        }
        else if (oGLDataRecurringItemScheduleInfo != null && isMultiCurrencyEnabled && mode == QueryStringConstants.EDIT)
        {
            ddlLocalCurrency.SelectedValue = oGLDataRecurringItemScheduleInfo.LocalCurrencyCode;//TODO: on edit
        }
    }
    public static void BindLocalCurrencyDropDown(DropDownList ddlLocalCurrency, long gldataID, bool isMultiCurrencyEnabled)
    {
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        List<string> localCurrencyCollection = oGLDataClient.SelectLocalCurrencyByAccountID(gldataID, isMultiCurrencyEnabled, Helper.GetAppUserInfo());
        ddlLocalCurrency.DataSource = localCurrencyCollection;
        ddlLocalCurrency.DataBind();

    }
    //TODO: perhaps not in use so remove it
    public static void BindCurrencyTypeDropdown(DropDownList ddlYesNo)
    {
        ListItemCollection lstLI = new ListItemCollection();
        ListItem li1 = new ListItem(LanguageUtil.GetValue(1409), "1");
        ListItem li2 = new ListItem(LanguageUtil.GetValue(1493), "2");
        ListItem li3 = new ListItem(LanguageUtil.GetValue(1424), "3");

        lstLI.Add(li1);
        lstLI.Add(li2);
        lstLI.Add(li3);
        ddlYesNo.DataSource = lstLI;
        ddlYesNo.DataBind();
        AddListItemForSelectOne(ddlYesNo);
    }

    public static void BindYesNoDropdown(DropDownList ddlYesNo, bool bAddSelectOne)
    {
        ListItemCollection lstLI = new ListItemCollection();
        ListItem liYes = new ListItem(LanguageUtil.GetValue((int)ARTEnums.YesNoLabel.Yes), ((short)ARTEnums.YesNo.Yes).ToString());
        ListItem liNo = new ListItem(LanguageUtil.GetValue((int)ARTEnums.YesNoLabel.No), ((short)ARTEnums.YesNo.No).ToString());

        lstLI.Add(liYes);
        lstLI.Add(liNo);
        ddlYesNo.DataSource = lstLI;
        ddlYesNo.DataTextField = "Text";
        ddlYesNo.DataValueField = "Value";
        ddlYesNo.DataBind();
        if (bAddSelectOne)
            AddListItemForSelectOne(ddlYesNo);
    }

    public static void ShowSelectAllAsSelected(DropDownList ddl)
    {
        // Set the All as selected Node
        ListItem oListItem = ddl.Items.FindByValue(WebConstants.SELECT_ALL.ToString());
        if (oListItem != null)
        {
            ddl.SelectedItem.Selected = false;
            oListItem.Selected = true;
        }
    }

    public static void BindReconciliationStatusDropdown(DropDownList ddlReconciliationStatus)
    {
        List<ReconciliationStatusMstInfo> oReconciliationStatusMstInfoCollection = SessionHelper.GetAllRecStatus();
        ddlReconciliationStatus.DataSource = oReconciliationStatusMstInfoCollection;
        ddlReconciliationStatus.DataTextField = "ReconciliationStatus";
        ddlReconciliationStatus.DataValueField = "ReconciliationStatusID";
        ddlReconciliationStatus.DataBind();
        AddListItemForSelectAll(ddlReconciliationStatus);
        ShowSelectAllAsSelected(ddlReconciliationStatus);
    }

    #region Package

    /// <summary>
    /// Data Bind method for Package DropDownList
    /// </summary>
    /// <param name="ddlPackage"></param>
    public static void BindPackageDropdown(DropDownList ddlPackage)
    {
        List<PackageMstInfo> oPackageMstInfoCollection = SessionHelper.GetAllPackage();
        List<PackageMstInfo> oDataImportTypeMstInfoCollection = null;
        oDataImportTypeMstInfoCollection = (List<PackageMstInfo>)Helper.DeepClone(oPackageMstInfoCollection);
        oPackageMstInfoCollection = LanguageHelper.TranslatePackage(oDataImportTypeMstInfoCollection);
        ddlPackage.DataSource = oPackageMstInfoCollection;
        ddlPackage.DataTextField = "PackageName";
        ddlPackage.DataValueField = "PackageID";
        ddlPackage.DataBind();
        AddListItemForSelectOne(ddlPackage);
    }

    #endregion

    #region Role

    public static void BindRoleDropDownList(DropDownList ddl)
    {
        IRole oRoleClient = RemotingHelper.GetRoleObject();
        IList<RoleMstInfo> ListRoles = SessionHelper.GetAllRoles();
        List<ListItem> lstListItem = new List<ListItem>();

        foreach (RoleMstInfo role in ListRoles)
        {

            if (
                     (
                        SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BUSINESS_ADMIN
                        && (role.RoleID != (short)ARTEnums.UserRole.SYSTEM_ADMIN
                            && role.RoleID != (short)ARTEnums.UserRole.CEO_CFO
                            && role.RoleID != (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                            && role.RoleID != (short)ARTEnums.UserRole.USER_ADMIN
                            )
                       )
                   ||
                        (
                            SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN
                            && role.RoleID != (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                        )
                   ||
                        (
                            SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.USER_ADMIN
                            && role.RoleID != (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                        )
                )
            {
                lstListItem.Add(new ListItem(LanguageUtil.GetValue(role.RoleLabelID.Value), role.RoleID.Value.ToString()));
            }
        }

        ddl.DataSource = lstListItem;
        ddl.DataTextField = "text";
        ddl.DataValueField = "value";
        ddl.DataBind();

        AddListItemForSelectAll(ddl);
        ShowSelectAllAsSelected(ddl);
    }

    public static void BindRoleDropDownListForAccountAssociation(DropDownList ddl)
    {
        IRole oRoleClient = RemotingHelper.GetRoleObject();
        IList<RoleMstInfo> ListRoles = SessionHelper.GetAllRoles();
        List<ListItem> lstListItem = new List<ListItem>();

        foreach (RoleMstInfo role in ListRoles)
        {

            if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN
                            && role.IsVisibleForAccountAssociationByUserRole.GetValueOrDefault())
            {
                lstListItem.Add(new ListItem(LanguageUtil.GetValue(role.RoleLabelID.Value), role.RoleID.Value.ToString()));
            }
        }

        ddl.DataSource = lstListItem;
        ddl.DataTextField = "text";
        ddl.DataValueField = "value";
        ddl.DataBind();

        AddListItemForSelectAll(ddl);
        ShowSelectAllAsSelected(ddl);
    }

    public static void BindStatusDropDown(DropDownList ddl)
    {
        List<ListItem> lstItem = new List<ListItem>();
        foreach (ARTEnums.Status value in Enum.GetValues(typeof(ARTEnums.Status)))
        {
            int categoryvalue = (int)Enum.Parse(typeof(ARTEnums.Status), Enum.GetName(typeof(ARTEnums.Status), value));
            lstItem.Add(new ListItem(Helper.GetLabelIDValueEnum(categoryvalue), categoryvalue.ToString()));

            // do something interesting with the value...
        }
        ddl.DataSource = lstItem;
        ddl.DataTextField = "text";
        ddl.DataValueField = "value";
        ddl.DataBind();
        ddl.SelectedValue = ((short)ARTEnums.Status.Active).ToString();
    }
    public static void BindActivationHistoryDropDown(DropDownList ddl)
    {
        List<ListItem> lstItem = new List<ListItem>();
        foreach (ARTEnums.UserStatus value in Enum.GetValues(typeof(ARTEnums.UserStatus)))
        {
            int categoryvalue = (int)Enum.Parse(typeof(ARTEnums.UserStatus), Enum.GetName(typeof(ARTEnums.UserStatus), value));
            lstItem.Add(new ListItem(Helper.GetLabelIDValueUserStatusEnum(categoryvalue), categoryvalue.ToString()));
        }
        ddl.DataSource = lstItem;
        ddl.DataTextField = "text";
        ddl.DataValueField = "value";
        ddl.DataBind();
    }

    public static void BindActivationStatusHistoryDropDown(DropDownList ddl)
    {
        List<ListItem> lstItem = new List<ListItem>();
        foreach (ARTEnums.ActivationStatus value in Enum.GetValues(typeof(ARTEnums.ActivationStatus)))
        {
            int categoryvalue = (int)Enum.Parse(typeof(ARTEnums.ActivationStatus), Enum.GetName(typeof(ARTEnums.ActivationStatus), value));
            lstItem.Add(new ListItem(Helper.GetLabelIDValueActivationStatusEnum(categoryvalue), categoryvalue.ToString()));
        }
        ddl.DataSource = lstItem;
        ddl.DataTextField = "text";
        ddl.DataValueField = "value";
        ddl.DataBind();
    }

    public static void BindCompanyStatusDropDown(DropDownList ddl)
    {
        List<ListItem> lstItem = new List<ListItem>();
        foreach (ARTEnums.Status value in Enum.GetValues(typeof(ARTEnums.Status)))
        {
            int categoryvalue = (int)Enum.Parse(typeof(ARTEnums.Status), Enum.GetName(typeof(ARTEnums.Status), value));
            lstItem.Add(new ListItem(Helper.GetLabelIDValueEnum(categoryvalue), categoryvalue.ToString()));

            // do something interesting with the value...
        }
        ddl.DataSource = lstItem;
        ddl.DataTextField = "text";
        ddl.DataValueField = "value";
        ddl.DataBind();
        ddl.SelectedValue = ((short)ARTEnums.Status.All).ToString();
    }
    #endregion

    public static void BindFinancialYearDropdown(DropDownList ddlFinancialYear, bool bShowSelectOne)
    {
        IList<FinancialYearHdrInfo> oFinancialYearHdrInfoFromDB = CacheHelper.GetAllFinancialYears();
        ddlFinancialYear.DataSource = oFinancialYearHdrInfoFromDB;
        ddlFinancialYear.DataTextField = "FinancialYear";
        ddlFinancialYear.DataValueField = "FinancialYearID";
        ddlFinancialYear.DataBind();

        if (bShowSelectOne)
        {
            ListControlHelper.AddListItemForSelectOne(ddlFinancialYear);
        }
    }

    #region JEWriteOffOnApprover

    public static void BindJEWriteOffOnApproverDropDown(DropDownList ddl)
    {
        List<UserHdrInfo> oUserHdrInfoCollection = SessionHelper.GetAllJEWriteOffOnApprover();

        ddl.DataSource = oUserHdrInfoCollection;
        ddl.DataTextField = "FirstName";
        ddl.DataValueField = "UserID";
        ddl.DataBind();

        ListControlHelper.AddListItemForSelectOne(ddl);
    }

    #endregion

    #region QualityScore
    public static void BindUserQualityScoreStatusDropdown(DropDownList ddlUserQualityScoreStatus, bool bShowSelectOne)
    {
        List<QualityScoreStatusMstInfo> oQualityScoreStatusMstInfoList = SessionHelper.GetAllQualityScoreStatuses();
        ddlUserQualityScoreStatus.DataSource = oQualityScoreStatusMstInfoList;
        ddlUserQualityScoreStatus.DataTextField = "QualityScoreStatus";
        ddlUserQualityScoreStatus.DataValueField = "QualityScoreStatusID";
        ddlUserQualityScoreStatus.DataBind();
        if (bShowSelectOne)
            AddListItemForSelectOne(ddlUserQualityScoreStatus);
    }
    #endregion

    public static void BindLanguageDropdown(DropDownList ddlLanguage, bool bAddDefaultLanguage, bool bShowDefaultLanguage, bool bShowSelectOne)
    {
        int applicationID = AppSettingHelper.GetApplicationID();
        int businessEntityID = SessionHelper.GetBusinessEntityID();
        int defaultLanguageID = AppSettingHelper.GetDefaultLanguageID();
        List<LanguageInfo> oLanguageInfoList = LanguageUtil.GetLanguages(applicationID, businessEntityID);
        if (!bAddDefaultLanguage && oLanguageInfoList != null && oLanguageInfoList.Count > 0)
            oLanguageInfoList.RemoveAll(T => T.LanguageID == defaultLanguageID);
        ddlLanguage.DataTextField = "Name";
        ddlLanguage.DataValueField = "LanguageID";
        ddlLanguage.DataSource = oLanguageInfoList;
        ddlLanguage.DataBind();
        if (bShowSelectOne)
            AddListItemForSelectOne(ddlLanguage);
        if (bShowDefaultLanguage)
            ddlLanguage.SelectedValue = SessionHelper.GetUserLanguage().ToString();
    }

    #region "Task Master"
    public static void BindTaskCategoryDropDopwn(DropDownList ddlTaskCategory)
    {
        List<TaskCategoryMstInfo> oTaskCategoryMstInfoList = SessionHelper.GetTaskCategory();
        ddlTaskCategory.DataSource = oTaskCategoryMstInfoList;
        ddlTaskCategory.DataTextField = "Name";
        ddlTaskCategory.DataValueField = "TaskCategoryID";

        //AddListItemForSelectAll(ddlTaskCategory);

        ddlTaskCategory.DataBind();
        string selectOneText = LanguageUtil.GetValue(1262);
        string selectOneValue = WebConstants.SELECT_ALL;
        ListItem oListItem = new ListItem(selectOneText, selectOneValue);
        ddlTaskCategory.Items.Insert(0, oListItem);

    }
    public static void BindReconciliationPeriodForTaskMaster(CheckBoxList cblReconciliationPeriod, int? financialYearID, DateTime? TaskRecPeriodEndDate)
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = new List<ReconciliationPeriodInfo>();

        if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.AUDIT)
        {
            oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriodsForAuditRole(financialYearID);
        }
        else
        {
            oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(financialYearID);
        }
        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollectionWithCondition = new List<ReconciliationPeriodInfo>();
        foreach (ReconciliationPeriodInfo item in oReconciliationPeriodInfoCollection)
        {
            if (item.PeriodEndDate >= TaskRecPeriodEndDate)
            {
                oReconciliationPeriodInfoCollectionWithCondition.Add(item);
            }
        }
        cblReconciliationPeriod.DataSource = oReconciliationPeriodInfoCollectionWithCondition;
        cblReconciliationPeriod.DataTextField = "PeriodEndDate";
        cblReconciliationPeriod.DataValueField = "ReconciliationPeriodID";
        cblReconciliationPeriod.DataBind();
    }
    public static void BindRecurrenceDropdown(DropDownList ddlRecurrence)
    {
        ddlRecurrence.DataSource = TaskMasterHelper.GetTaskRecurrenceTypeMstInfoCollection();
        ddlRecurrence.DataTextField = "RecurrenceType";
        ddlRecurrence.DataValueField = "TaskRecurrenceTypeID";
        ddlRecurrence.DataBind();
    }
    public static void BindTaskListNameDropdown(DropDownList ddlTaskListName)
    {
        ddlTaskListName.DataSource = TaskMasterHelper.GetTaskListHdrInfoCollection(SessionHelper.CurrentReconciliationPeriodID.Value);
        ddlTaskListName.DataTextField = "TaskListName";
        ddlTaskListName.DataValueField = "TaskListID";
        ddlTaskListName.DataBind();
        ListControlHelper.AddListItemForSelectOne(ddlTaskListName);
        ListControlHelper.AddListItemForCreateNew(ddlTaskListName, 1);
    }
    public static void BindTaskSubListNameDropdown(DropDownList ddlTaskSubListName)
    {
        ddlTaskSubListName.DataSource = TaskMasterHelper.GetTaskSubListHdrInfoCollection(SessionHelper.CurrentReconciliationPeriodID.Value);
        ddlTaskSubListName.DataTextField = "TaskSubListName";
        ddlTaskSubListName.DataValueField = "TaskSubListID";
        ddlTaskSubListName.DataBind();
        ListControlHelper.AddListItemForSelectOne(ddlTaskSubListName);
        ListControlHelper.AddListItemForCreateNew(ddlTaskSubListName, 1);
    }

    public static void BindDropDownListForTaskType(DropDownList ddlTaskType, bool bAddSelectOne, bool bAddSelectAll)
    {
        ddlTaskType.DataSource = SessionHelper.GetAllTaskType();
        ddlTaskType.DataTextField = "TaskType";
        ddlTaskType.DataValueField = "TaskTypeID";
        ddlTaskType.DataBind();
        if (bAddSelectOne)
            ListControlHelper.AddListItemForSelectOne(ddlTaskType);
        if (bAddSelectAll)
            ListControlHelper.AddListItemForSelectAll(ddlTaskType);
    }
    public static void BindRecPeriodNumberForTaskMaster(CheckBoxList cblMRRecurrencePeriodNumber, DropDownList ddlRecurrencePeriodNumber, CheckBoxList cblReconciliationPeriod, int? companyID, DateTime? TaskRecPeriodEndDate)
    {
        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoList = TaskMasterHelper.GetRecPeriodNumberList(companyID, TaskRecPeriodEndDate);
        cblReconciliationPeriod.DataSource = oReconciliationPeriodInfoList;
        cblReconciliationPeriod.DataTextField = "PeriodNumber";
        cblReconciliationPeriod.DataValueField = "PeriodNumber";
        cblReconciliationPeriod.DataBind();

        cblMRRecurrencePeriodNumber.DataSource = oReconciliationPeriodInfoList;
        cblMRRecurrencePeriodNumber.DataTextField = "PeriodNumber";
        cblMRRecurrencePeriodNumber.DataValueField = "PeriodNumber";
        cblMRRecurrencePeriodNumber.DataBind();

        ddlRecurrencePeriodNumber.DataSource = oReconciliationPeriodInfoList;
        ddlRecurrencePeriodNumber.DataTextField = "PeriodNumber";
        ddlRecurrencePeriodNumber.DataValueField = "PeriodNumber";
        ddlRecurrencePeriodNumber.DataBind();

        AddListItemForSelectOne(ddlRecurrencePeriodNumber);
    }
    public static void BindDualLevelReviewType(DropDownList ddlDualLevelReviewType, bool bShowSelectOne)
    {
        ddlDualLevelReviewType.DataSource = SessionHelper.GetAllDualLevelReviewType();
        ddlDualLevelReviewType.DataTextField = "DualLevelReviewType";
        ddlDualLevelReviewType.DataValueField = "DualLevelReviewTypeID";
        ddlDualLevelReviewType.DataBind();

        if (bShowSelectOne)
        {
            ListControlHelper.AddListItemForSelectOne(ddlDualLevelReviewType);
        }
    }
    public static void BindDueDaysBasisDropdown(DropDownList ddlTaskDueDaysBasis, DropDownList ddlAssigneeDueDaysBasis, DropDownList ddlReviewerDueDaysBasis)
    {
        List<DueDaysBasisInfo> oDueDaysBasisInfoList = (List<DueDaysBasisInfo>)SessionHelper.GetAllDueDaysBasisType();
        BindDropdown(ddlTaskDueDaysBasis, oDueDaysBasisInfoList, "DueDaysBasis", "DueDaysBasisID");
        //ddlTaskDueDaysBasis.DataSource = oDueDaysBasisInfoList;
        //ddlTaskDueDaysBasis.DataTextField = "DueDaysBasis";
        //ddlTaskDueDaysBasis.DataValueField = "DueDaysBasisID";
        //ddlTaskDueDaysBasis.DataBind();
        ListControlHelper.AddListItemForSelectOne(ddlTaskDueDaysBasis);

        BindDropdown(ddlAssigneeDueDaysBasis, oDueDaysBasisInfoList, "DueDaysBasis", "DueDaysBasisID");
        //ddlAssigneeDueDaysBasis.DataSource = oDueDaysBasisInfoList;
        //ddlAssigneeDueDaysBasis.DataTextField = "DueDaysBasis";
        //ddlAssigneeDueDaysBasis.DataValueField = "DueDaysBasisID";
        //ddlAssigneeDueDaysBasis.DataBind();
        ListControlHelper.AddListItemForSelectOne(ddlAssigneeDueDaysBasis);

        BindDropdown(ddlReviewerDueDaysBasis, oDueDaysBasisInfoList, "DueDaysBasis", "DueDaysBasisID");
        //ddlReviewerDueDaysBasis.DataSource = oDueDaysBasisInfoList;
        //ddlReviewerDueDaysBasis.DataTextField = "DueDaysBasis";
        //ddlReviewerDueDaysBasis.DataValueField = "DueDaysBasisID";
        //ddlReviewerDueDaysBasis.DataBind();
        ListControlHelper.AddListItemForSelectOne(ddlReviewerDueDaysBasis);
    }
    public static void BindddlDaysType(DropDownList ddlDaysType, bool bShowSelectOne)
    {
        IList<DaysTypeInfo> oDaysTypeInfoList = SessionHelper.GetAllDaysType();
        ddlDaysType.DataSource = oDaysTypeInfoList;
        ddlDaysType.DataTextField = "DaysType";
        ddlDaysType.DataValueField = "DaysTypeID";
        ddlDaysType.DataBind();
        if (bShowSelectOne)
            ListControlHelper.AddListItemForSelectOne(ddlDaysType);
    }


    #endregion

    public static void BindFileTypeDropdown(DropDownList ddlFileType, bool bShowSelectOne)
    {
        ddlFileType.Items.Add(new ListItem(LanguageUtil.GetValue(1684), ((short)ARTEnums.FileType.Permanent).ToString()));
        ddlFileType.Items.Add(new ListItem(LanguageUtil.GetValue(1685), ((short)ARTEnums.FileType.Temporary).ToString()));
        if (bShowSelectOne)
            AddListItemForSelectOne(ddlFileType);
    }
    public static void BindRCCLValidationType(DropDownList ddlRCCLValidationType, bool bShowSelectOne)
    {
        ddlRCCLValidationType.DataSource = SessionHelper.GetAllRCCLValidationType();
        ddlRCCLValidationType.DataTextField = "RCCValidationTypeName";
        ddlRCCLValidationType.DataValueField = "RCCValidationTypeID";
        ddlRCCLValidationType.DataBind();

        if (bShowSelectOne)
        {
            ListControlHelper.AddListItemForSelectOne(ddlRCCLValidationType);
        }
    }
    public static void AddListItemForSelectColumn(DropDownList ddl)
    {
        string selectOneText = LanguageUtil.GetValue(2875);
        string selectOneValue = WebConstants.SELECT_COLUMN;
        ListItem oListItem = new ListItem(selectOneText, selectOneValue);
        ddl.Items.Insert(0, oListItem);
    }
    public static void BindDataimportTemplate(DropDownList ddlDataimportTemplate, short? DataImportType)
    {
        ddlDataimportTemplate.Items.Clear();
        List<ImportTemplateHdrInfo> oImportTemplateHdrInfoList = DataImportHelper.GetAllDataImportTemplate(DataImportType);
        if (oImportTemplateHdrInfoList != null && oImportTemplateHdrInfoList.Count > 0)
        {
            ddlDataimportTemplate.DataSource = oImportTemplateHdrInfoList;
            ddlDataimportTemplate.DataTextField = "TemplateName";
            ddlDataimportTemplate.DataValueField = "ImportTemplateID";
            ddlDataimportTemplate.DataBind();
        }
        AddListItemForARTTemplate(ddlDataimportTemplate);
    }
    public static void AddListItemForARTTemplate(DropDownList ddl)
    {
        string ArtTemplate = LanguageUtil.GetValue(2866);
        string ArtTemplateValue = WebConstants.ART_TEMPLATE;
        ListItem oListItem = new ListItem(ArtTemplate, ArtTemplateValue);
        ddl.Items.Insert(0, oListItem);
    }

    public static void BindFTPServerDropdown(DropDownList ddlFTPServer, int? CompanyID)
    {
        //ddlFTPServer.DataSource = SessionHelper.GetAllFTPServerObject(CompanyID);
        //ddlFTPServer.DataTextField = "FTPUrl";
        //ddlFTPServer.DataValueField = "FTPServerId";
        //ddlFTPServer.DataBind();

        BindDropdown(ddlFTPServer, SessionHelper.GetAllFTPServerObject(CompanyID), "FTPUrl", "FTPServerId");
        AddListItemForSelectOne(ddlFTPServer);
    }
    public static void BindFTPRoleDropdown(DropDownList ddlRole)
    {
        //List<RoleMstInfo> oRoleMstInfoList = SessionHelper.GetUserRole();
        //ddlRole.DataSource = oRoleMstInfoList.FindAll(obj => obj.RoleID.Value == (short)ARTEnums.UserRole.SYSTEM_ADMIN || obj.RoleID.Value == (short)ARTEnums.UserRole.BUSINESS_ADMIN);
        //ddlRole.DataTextField = "Role";
        //ddlRole.DataValueField = "RoleID";
        //ddlRole.DataBind();
        BindDropdown(ddlRole, SessionHelper.GetUserRole(), "Role", "RoleID");

    }
    public static void BindDropdown<T>(DropDownList ddl, List<T> oList, string TextFieldName, string ValueFieldName)
    {
        if (ddl != null && oList != null && oList.Count > 0)
        {
            ddl.DataSource = oList;
            ddl.DataTextField = TextFieldName;
            ddl.DataValueField = ValueFieldName;
            ddl.DataBind();
        }
    }
}
