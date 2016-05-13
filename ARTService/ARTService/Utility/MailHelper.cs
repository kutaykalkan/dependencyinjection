using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Data;
using System.Net.Mail;
using SkyStem.ART.Service.Utility;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.Service.DAO;
using SkyStem.ART.Service.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Model.CompanyDatabase;
using ClientModel = SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SharedUtility = SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Service.Utility
{
    public class MailHelper
    {
        public static void SendEmailToUserByDataImportStatus(string dataImportStatus, string successEmailIds
            , string failureEmailIds, short dataImportType, int recordsAffected, string profileName
            , string errorMessage, DataImportHdrInfo oDataImportHdrInfoService
            , CompanyUserInfo oCompanyUserInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {

            string typeOfDataImport;
            switch (dataImportType)
            {
                case (short)Enums.DataImportType.GLData:
                    typeOfDataImport = ServiceConstants.GLDATA;
                    break;
                case (short)Enums.DataImportType.SubledgerData:
                    typeOfDataImport = ServiceConstants.SUBLEDGER;
                    break;
                case (short)Enums.DataImportType.AccountAttributeList:
                    typeOfDataImport = ServiceConstants.ACCOUNT_ATTRIBUTE_DATA;
                    break;
                default:
                    typeOfDataImport = "";
                    break;
            }
            switch (dataImportStatus)
            {
                case DataImportStatus.DATAIMPORTSUCCESS:
                    SendSuccessEmailToUsers(successEmailIds, typeOfDataImport, profileName, recordsAffected, oDataImportHdrInfoService, oCompanyUserInfo, oMultilingualAttributeInfo);
                    break;
                case DataImportStatus.DATAIMPORTWARNING:
                    SendWarningEmailToUsers(failureEmailIds, typeOfDataImport, profileName, errorMessage, oDataImportHdrInfoService, oCompanyUserInfo, oMultilingualAttributeInfo);
                    break;
                case DataImportStatus.DATAIMPORTFAIL:
                    SendFailureEmailToUsers(failureEmailIds, typeOfDataImport, profileName, errorMessage, oDataImportHdrInfoService, oCompanyUserInfo, oMultilingualAttributeInfo);
                    break;
            }
        }

        public static void SendEmailToUserByDataImportStatus(string dataImportStatus, string successEmailIds
            , string failureEmailIds, string warningEmailIds, short dataImportType, int recordsAffected, string profileName
            , string errorMessage, int businessEntityID, int languageID, int defaultLanguageID
            , DateTime? dateAdded, int? UserID, short? RoleID, int RecPeriodID, int CompanyID
            , DataImportHdrInfo oDataImportHdrInfoService, CompanyUserInfo oCompanyUserInfo, int DataImportID)
        {

            string typeOfDataImport;

            errorMessage = FormatFailureMessage(errorMessage);

            switch (dataImportType)
            {
                case (short)Enums.DataImportType.GLData:
                    typeOfDataImport = ServiceConstants.GLDATA;
                    break;
                case (short)Enums.DataImportType.AccountDataImport:
                    typeOfDataImport = ServiceConstants.ACCOUNTDATA;
                    break;
                case (short)Enums.DataImportType.SubledgerData:
                    typeOfDataImport = ServiceConstants.SUBLEDGER;
                    break;
                case (short)Enums.DataImportType.AccountAttributeList:
                    typeOfDataImport = ServiceConstants.ACCOUNT_ATTRIBUTE_DATA;
                    break;
                case (short)Enums.DataImportType.UserUpload:
                    typeOfDataImport = ServiceConstants.USER_UPLOAD_DATA;
                    break;
                case (short)Enums.DataImportType.ScheduleRecItems:
                    typeOfDataImport = ServiceConstants.SCHEDULE_REC_ITEM_UPLOAD_DATA;
                    break;
                default:
                    typeOfDataImport = "";
                    break;
            }
            switch (dataImportStatus)
            {
                case DataImportStatus.DATAIMPORTSUCCESS:

                    SendSuccessEmailToUsers(successEmailIds, typeOfDataImport
                        , profileName, recordsAffected, businessEntityID, languageID, defaultLanguageID
                        , dateAdded, UserID, RoleID, RecPeriodID, CompanyID, oDataImportHdrInfoService, oCompanyUserInfo, DataImportID);
                    break;
                case DataImportStatus.DATAIMPORTWARNING:
                    SendWarningEmailToUsers(warningEmailIds, typeOfDataImport, profileName, errorMessage
                        , businessEntityID, languageID, defaultLanguageID, dateAdded, DataImportID, RoleID, CompanyID
                        , oDataImportHdrInfoService, oCompanyUserInfo);
                    break;
                case DataImportStatus.DATAIMPORTFAIL:
                case DataImportStatus.DATAIMPORTERRORS:
                    SendFailureEmailToUsers(failureEmailIds, typeOfDataImport, profileName, errorMessage
                        , businessEntityID, languageID, defaultLanguageID, dateAdded, DataImportID, RoleID, CompanyID, oDataImportHdrInfoService, oCompanyUserInfo);
                    break;
            }
        }

        public static void SendEmailToUserByDataImportStatusMultiVersion(string dataImportStatus, string successEmailIds
            , string failureEmailIds, string warningEmailIds, short dataImportType, int recordsAffected, string profileName
            , string errorMessage, int businessEntityID, int languageID, int defaultLanguageID
            , DateTime? dateAdded, List<ClientModel.UserAccountInfo> oUserAccountInfoCollection
            , int? UserID, short? RoleID, int RecPeriodID, int CompanyID, CompanyUserInfo oCompanyUserInfo, int DataImportID
            , DataImportHdrInfo oDataImportHdrInfoService)
        {

            string typeOfDataImport;
            errorMessage = FormatFailureMessage(errorMessage);

            switch (dataImportType)
            {
                case (short)Enums.DataImportType.GLData:
                    typeOfDataImport = ServiceConstants.GLDATA;
                    break;
                case (short)Enums.DataImportType.SubledgerData:
                    typeOfDataImport = ServiceConstants.SUBLEDGER;
                    break;
                case (short)Enums.DataImportType.CurrencyExchangeRateData:
                    typeOfDataImport = ServiceConstants.CURRENCYLOAD;
                    break;
                default:
                    typeOfDataImport = "";
                    break;
            }
            switch (dataImportStatus)
            {
                case DataImportStatus.DATAIMPORTSUCCESS:
                    ClientModel.UserAccountInfo oUserAccountInfo = new ClientModel.UserAccountInfo();
                    if (oUserAccountInfoCollection.Count > 0)
                    {
                        oUserAccountInfo.EmailID = successEmailIds;
                        List<string> AllAccountInfoCollection = new List<string>();
                        foreach (var AccountInfoCollection in oUserAccountInfoCollection)
                        {
                            if (AccountInfoCollection != null && AccountInfoCollection.AccountInfoCollection != null)
                            {
                                foreach (var AccountInfo in AccountInfoCollection.AccountInfoCollection)
                                {
                                    if (!AllAccountInfoCollection.Contains(AccountInfo))
                                        AllAccountInfoCollection.Add(AccountInfo);
                                }
                            }
                        }
                        // AllAccountInfoCollection.Add("398609 - EUR 30.0000 - EUR 789456.00 - INR 5676.0000 - INR 5676.00");
                        oUserAccountInfo.AccountInfoCollection = AllAccountInfoCollection;
                        oUserAccountInfo.RoleID = 2;
                        oUserAccountInfoCollection.Add(oUserAccountInfo);
                    }
                    else
                    {
                        oUserAccountInfo.EmailID = successEmailIds;
                        List<string> AllAccountInfoCollection = new List<string>();
                        // AllAccountInfoCollection.Add("398609 - EUR 30.0000 - EUR 789456.00 - INR 5676.0000 - INR 5676.00");
                        oUserAccountInfo.AccountInfoCollection = AllAccountInfoCollection;
                        oUserAccountInfo.RoleID = 2;
                        oUserAccountInfoCollection.Add(oUserAccountInfo);
                    }
                    SendSuccessEmailToUsersForMultiversion(typeOfDataImport
                       , businessEntityID, languageID, defaultLanguageID
                        , dateAdded, oUserAccountInfoCollection, UserID, RoleID, RecPeriodID, CompanyID
                        , oCompanyUserInfo, DataImportID, oDataImportHdrInfoService);
                    break;
                case DataImportStatus.DATAIMPORTWARNING:
                    SendWarningEmailToUsers(warningEmailIds, typeOfDataImport, profileName, errorMessage
                        , businessEntityID, languageID, defaultLanguageID, dateAdded, DataImportID, RoleID, CompanyID
                        , oDataImportHdrInfoService, oCompanyUserInfo);
                    break;
                case DataImportStatus.DATAIMPORTFAIL:
                case DataImportStatus.DATAIMPORTERRORS:
                    SendFailureEmailToUsers(failureEmailIds, typeOfDataImport, profileName, errorMessage
                        , businessEntityID, languageID, defaultLanguageID, dateAdded, DataImportID, RoleID, CompanyID
                        , oDataImportHdrInfoService, oCompanyUserInfo);
                    break;
            }
        }

        private static string ConvertDataImportMessageDetailToHtml(List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList, short? RoleID
            , int businessEntityID, int languageID, bool showAccountColumns, ClientModel.DataImportHdrInfo oDataImportHdrInfo)
        {
            StringBuilder sbMessages = new StringBuilder();
            if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(languageID, businessEntityID);
                List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = DataImportHelper.GetAllDataImportFieldsWithMapping(oDataImportHdrInfo.DataImportID.GetValueOrDefault(), businessEntityID);
                Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns = GetAccountColumnsDictionary(oImportTemplateFieldMappingInfoList);

                sbMessages.Append(SharedUtility.MailHelper.GetBeginTableHTML());
                sbMessages.Append(SharedUtility.MailHelper.GetBeginHaderRowHTML());
                sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                sbMessages.Append(LanguageUtil.GetValue(1690, oMultilingualAttributeInfo));
                sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                sbMessages.Append(LanguageUtil.GetValue(2836, oMultilingualAttributeInfo));
                sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                //sbMessages.Append(GetBeginColumnHTML());
                //sbMessages.Append(LanguageUtil.GetValue(1408, oMultilingualAttributeInfo));
                //sbMessages.Append(GetEndColumnHTML());
                sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                sbMessages.Append(LanguageUtil.GetValue(2863, oMultilingualAttributeInfo));
                sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                if (showAccountColumns)
                {
                    sbMessages.Append(GetHtmlForAccountHeaderRow(dictAcctColumns, oMultilingualAttributeInfo));
                }
                sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                foreach (ClientModel.DataImportMessageDetailInfo oItem in oDataImportMessageDetailInfoList)
                {
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginRowHTML("style='height:25px;'"));
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                    switch ((ARTEnums.DataImportMessageType)oItem.DataImportMessageTypeID)
                    {
                        case ARTEnums.DataImportMessageType.Error:
                            sbMessages.Append(LanguageUtil.GetValue(1051, oMultilingualAttributeInfo));
                            break;
                        case ARTEnums.DataImportMessageType.Warning:
                            sbMessages.Append(LanguageUtil.GetValue(1546, oMultilingualAttributeInfo));
                            break;
                        case ARTEnums.DataImportMessageType.Success:
                            sbMessages.Append(LanguageUtil.GetValue(1050, oMultilingualAttributeInfo));
                            break;
                    }
                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                    if (oItem.DataImportMessageLabelID.HasValue)
                        sbMessages.Append(LanguageUtil.GetValue(oItem.DataImportMessageLabelID.Value, oMultilingualAttributeInfo));
                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                    //sbMessages.Append(GetBeginColumnHTML());
                    //sbMessages.Append(LanguageUtil.GetValue(oItem.DescriptionLabelID.Value, oMultilingualAttributeInfo));
                    //sbMessages.Append(GetEndColumnHTML());
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                    if (oItem.ExcelRowNumber.HasValue)
                        sbMessages.Append(oItem.ExcelRowNumber.Value.ToString());
                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                    if (showAccountColumns && oItem.AccountInfo != null)
                    {
                        sbMessages.Append(GetHtmlForAccountInfoRow(dictAcctColumns, oItem.AccountInfo, oMultilingualAttributeInfo));
                    }
                    sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                    if (!string.IsNullOrEmpty(oItem.MessageSchema) && !string.IsNullOrEmpty(oItem.MessageData))
                    {
                        sbMessages.Append(SharedUtility.MailHelper.GetBeginRowHTML());
                        sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                        sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                        int colSpan = 2;
                        if (showAccountColumns)
                            colSpan += dictAcctColumns.Keys.Count;
                        sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML("colspan='" + colSpan.ToString() + "' padding='0px'"));
                        sbMessages.Append(ConvertDataImportMessageInnerDetailToHtml(oItem, oMultilingualAttributeInfo));
                        sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                        sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                    }
                }
                sbMessages.Append(SharedUtility.MailHelper.GetEndTableHTML());
            }
            return sbMessages.ToString();
        }

        private static string ConvertDataImportMessageInnerDetailToHtml(ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo,
            MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            StringBuilder sbMessages = new StringBuilder();
            if (oDataImportMessageDetailInfo != null
                && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageSchema)
                && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageData))
            {
                DataSet dsData = SharedUtility.DataHelper.GetDataSet(oDataImportMessageDetailInfo.MessageSchema);
                if (dsData != null)
                {
                    SharedUtility.DataHelper.LoadXmlToDataSet(dsData, oDataImportMessageDetailInfo.MessageData);
                    Dictionary<string, string> dictTranslate = new Dictionary<string, string>();
                    List<string> visibleColumns = new List<string>();
                    DataColumn dcImportField = null;
                    if (dsData != null && dsData.Tables.Count > 0)
                    {
                        sbMessages.Append(SharedUtility.MailHelper.GetBeginTableHTML());
                        sbMessages.Append(SharedUtility.MailHelper.GetBeginHaderRowHTML());
                        for (int i = 0; i < dsData.Tables[0].Columns.Count; i++)
                        {
                            DataColumn dc = dsData.Tables[0].Columns[i];
                            if (dc.ExtendedProperties.ContainsKey("LabelFieldName") && !string.IsNullOrEmpty(dc.ExtendedProperties["LabelFieldName"].ToString()))
                                dictTranslate.Add(dc.ColumnName, dc.ExtendedProperties["LabelFieldName"].ToString());
                            if (dc.ExtendedProperties.ContainsKey("IsVisible") && Convert.ToBoolean(dc.ExtendedProperties["IsVisible"]))
                            {
                                visibleColumns.Add(dc.ColumnName);
                                int labelID = Convert.ToInt32(dc.ExtendedProperties["HeaderLabelID"]);
                                sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                                sbMessages.Append(LanguageUtil.GetValue(labelID, oMultilingualAttributeInfo));
                                sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                            }
                            if (dc.ColumnName == "ImportFieldID")
                                dcImportField = dc;
                        }
                        sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                        if (visibleColumns.Count > 0)
                        {
                            foreach (DataRow dr in dsData.Tables[0].Rows)
                            {
                                sbMessages.Append(SharedUtility.MailHelper.GetBeginRowHTML("style='height:25px;'"));
                                foreach (string colName in visibleColumns)
                                {
                                    if (dictTranslate.ContainsKey(colName))
                                    {
                                        int labelID = 0;
                                        if (Int32.TryParse(dr[dictTranslate[colName]].ToString(), out labelID))
                                            dr[colName] = LanguageUtil.GetValue(labelID, oMultilingualAttributeInfo);
                                    }
                                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                                    if (dcImportField != null)
                                        sbMessages.Append(SharedUtility.SharedHelper.GetDisplayValueByImportFieldID(dr[colName].ToString(), dr[dcImportField].ToString(), oMultilingualAttributeInfo));
                                    else
                                        sbMessages.Append(SharedUtility.SharedHelper.GetDisplayValueByImportFieldID(dr[colName].ToString(), null, null));
                                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                                }
                                sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                            }
                        }
                    }
                }
            }
            sbMessages.Append(SharedUtility.MailHelper.GetEndTableHTML());
            return sbMessages.ToString();
        }

        private static Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> GetAccountColumnsDictionary(List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            ClientModel.ImportTemplateFieldMappingInfo oMappintInfo;
            Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns = new Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo>();
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.FSCaption);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.FSCaption, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.AccountType);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.AccountType, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key2);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key2, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key3);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key3, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key4);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key4, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key5);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key5, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key6);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key6, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key7);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key7, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key8);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key8, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.Key9);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.Key9, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.AccountNumber);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.AccountNumber, oMappintInfo);
            oMappintInfo = oImportTemplateFieldMappingInfoList.Find(T => T.ImportFieldID == (short)ARTEnums.DataImportFields.AccountName);
            if (oMappintInfo != null)
                dictAcctColumns.Add(ARTEnums.DataImportFields.AccountName, oMappintInfo);
            return dictAcctColumns;
        }

        private static Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> GetDefaultAccountColumnsDictionary(DataImportHdrInfo oDataImportHdrInfoService)
        {
            ClientModel.ImportTemplateFieldMappingInfo oMappintInfo;
            Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns = new Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo>();
            oMappintInfo = new ClientModel.ImportTemplateFieldMappingInfo();
            oMappintInfo.ImportFieldID = (int)ARTEnums.DataImportFields.FSCaption;
            oMappintInfo.ImportFieldLabelID = 1337;
            dictAcctColumns.Add(ARTEnums.DataImportFields.FSCaption, oMappintInfo);

            oMappintInfo = new ClientModel.ImportTemplateFieldMappingInfo();
            oMappintInfo.ImportFieldID = (int)ARTEnums.DataImportFields.AccountType;
            oMappintInfo.ImportFieldLabelID = 1363;
            dictAcctColumns.Add(ARTEnums.DataImportFields.AccountType, oMappintInfo);

            List<string> keys = DataImportHelper.GetAccountKeyFields(oDataImportHdrInfoService);
            if (keys != null && keys.Count > 0)
            {
                ARTEnums.DataImportFields eFieldID;
                for (int i = 0; i < keys.Count; i++)
                {
                    if (Enum.TryParse("Key" + (i + 2), out eFieldID))
                    {
                        oMappintInfo = new ClientModel.ImportTemplateFieldMappingInfo();
                        oMappintInfo.ImportFieldID = (int)eFieldID;
                        oMappintInfo.ImportTemplateField = keys[i];
                        dictAcctColumns.Add(eFieldID, oMappintInfo);
                    }
                }
            }
            oMappintInfo = new ClientModel.ImportTemplateFieldMappingInfo();
            oMappintInfo.ImportFieldID = (int)ARTEnums.DataImportFields.AccountNumber;
            oMappintInfo.ImportFieldLabelID = 1491;
            dictAcctColumns.Add(ARTEnums.DataImportFields.AccountNumber, oMappintInfo);

            oMappintInfo = new ClientModel.ImportTemplateFieldMappingInfo();
            oMappintInfo.ImportFieldID = (int)ARTEnums.DataImportFields.AccountName;
            oMappintInfo.ImportFieldLabelID = 2867;
            dictAcctColumns.Add(ARTEnums.DataImportFields.AccountName, oMappintInfo);

            return dictAcctColumns;
        }

        private static string GetHtmlForAccountHeaderRow(Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns
                    , MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            StringBuilder sbMessages = new StringBuilder();
            ClientModel.ImportTemplateFieldMappingInfo oMappintInfo;
            if (dictAcctColumns != null && dictAcctColumns.Keys.Count > 0)
            {
                foreach (ARTEnums.DataImportFields key in dictAcctColumns.Keys)
                {
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                    oMappintInfo = dictAcctColumns[key];
                    if (oMappintInfo != null)
                    {
                        if (string.IsNullOrEmpty(oMappintInfo.ImportTemplateField))
                            sbMessages.Append(LanguageUtil.GetValue(oMappintInfo.ImportFieldLabelID.GetValueOrDefault(), oMultilingualAttributeInfo));
                        else
                            sbMessages.Append(oMappintInfo.ImportTemplateField);
                    }
                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                }
            }
            return sbMessages.ToString();
        }

        private static string GetHtmlForAccountInfoRow(Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns
                        , ClientModel.AccountHdrInfo oAccountHdrInfo
                        , MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            StringBuilder sbMessages = new StringBuilder();
            if (dictAcctColumns != null && dictAcctColumns.Keys.Count > 0)
            {
                foreach (ARTEnums.DataImportFields key in dictAcctColumns.Keys)
                {
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                    string value = string.Empty;
                    switch (key)
                    {
                        case ARTEnums.DataImportFields.FSCaption:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.FSCaptionLabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.AccountType:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.AccountTypeLabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key2:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key2LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key3:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key3LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key4:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key4LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key5:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key5LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key6:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key6LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key7:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key7LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key8:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key8LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.Key9:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.Key9LabelID.Value, oMultilingualAttributeInfo);
                            break;
                        case ARTEnums.DataImportFields.AccountNumber:
                            value = oAccountHdrInfo.AccountNumber;
                            break;
                        case ARTEnums.DataImportFields.AccountName:
                            value = LanguageUtil.GetValue(oAccountHdrInfo.AccountNameLabelID.Value, oMultilingualAttributeInfo);
                            break;
                    }
                    sbMessages.Append(value);
                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                }
            }
            return sbMessages.ToString();
        }

        private static string ConvertAccountInfoToHtml(List<ClientModel.AccountHdrInfo> oAccountHdrInfoList,
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList,
            MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns = GetAccountColumnsDictionary(oImportTemplateFieldMappingInfoList);
            return ConvertAccountInfoToHtml(oAccountHdrInfoList, dictAcctColumns, oMultilingualAttributeInfo);
        }

        private static string ConvertAccountInfoToHtml(List<ClientModel.AccountHdrInfo> oAccountHdrInfoList,
            Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns,
            MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            StringBuilder sbMessages = new StringBuilder();
            if (oAccountHdrInfoList != null && oAccountHdrInfoList.Count > 0)
            {
                sbMessages.Append(SharedUtility.MailHelper.GetBeginTableHTML());
                // Generate grid header row
                sbMessages.Append(SharedUtility.MailHelper.GetBeginHaderRowHTML());
                sbMessages.Append(GetHtmlForAccountHeaderRow(dictAcctColumns, oMultilingualAttributeInfo));
                if (oAccountHdrInfoList[0].ChangeTypeLabelID.HasValue && oAccountHdrInfoList[0].ChangeTypeLabelID.Value > 0)
                {
                    AddCustomColumn(2812, null, sbMessages, oMultilingualAttributeInfo);
                }
                if (oAccountHdrInfoList[0].ShowBalanceChangeColumnInMail.HasValue && oAccountHdrInfoList[0].ShowBalanceChangeColumnInMail.Value)
                {
                    AddCustomColumn(2894, null, sbMessages, oMultilingualAttributeInfo);
                    AddCustomColumn(2895, null, sbMessages, oMultilingualAttributeInfo);
                    AddCustomColumn(2896, null, sbMessages, oMultilingualAttributeInfo);
                    AddCustomColumn(2897, null, sbMessages, oMultilingualAttributeInfo);
                }
                sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                foreach (var oAccountHdrInfo in oAccountHdrInfoList)
                {
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginRowHTML());
                    sbMessages.Append(GetHtmlForAccountInfoRow(dictAcctColumns, oAccountHdrInfo, oMultilingualAttributeInfo));
                    if (oAccountHdrInfo.ChangeTypeLabelID.HasValue)
                    {
                        AddCustomColumn(oAccountHdrInfo.ChangeTypeLabelID, null, sbMessages, oMultilingualAttributeInfo);
                    }
                    if (oAccountHdrInfo.ShowBalanceChangeColumnInMail.HasValue && oAccountHdrInfo.ShowBalanceChangeColumnInMail.Value)
                    {
                        AddCustomColumn(null, oAccountHdrInfo.ExistingGLBalanceRCCY, sbMessages, oMultilingualAttributeInfo);
                        AddCustomColumn(null, oAccountHdrInfo.CurrentGLBalanceRCCY, sbMessages, oMultilingualAttributeInfo);
                        AddCustomColumn(null, oAccountHdrInfo.ExistingGLBalanceBCCY, sbMessages, oMultilingualAttributeInfo);
                        AddCustomColumn(null, oAccountHdrInfo.CurrentGLBalanceBCCY, sbMessages, oMultilingualAttributeInfo);
                    }
                    sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                }
                sbMessages.Append(SharedUtility.MailHelper.GetEndTableHTML());
            }
            return sbMessages.ToString();
        }

        private static string ConvertAccountInfoToHtmlWithChangeDetail(List<ClientModel.AccountHdrInfo> oAccountHdrInfoList,
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList,
            Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns,
            List<int> oDataImportMessageIDFilter,
            MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            StringBuilder sbMessages = new StringBuilder();
            if (oAccountHdrInfoList != null && oAccountHdrInfoList.Count > 0)
            {
                sbMessages.Append(SharedUtility.MailHelper.GetBeginTableHTML());

                // Generate grid header row
                sbMessages.Append(SharedUtility.MailHelper.GetBeginHaderRowHTML());
                if (oAccountHdrInfoList[0].ChangeTypeLabelID.HasValue && oAccountHdrInfoList[0].ChangeTypeLabelID.Value > 0)
                {
                    AddCustomColumn(2812, null, sbMessages, oMultilingualAttributeInfo);
                }
                sbMessages.Append(GetHtmlForAccountHeaderRow(dictAcctColumns, oMultilingualAttributeInfo));
                sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                foreach (var oAccountHdrInfo in oAccountHdrInfoList)
                {
                    sbMessages.Append(SharedUtility.MailHelper.GetBeginRowHTML());
                    if (oAccountHdrInfo.ChangeTypeLabelID.HasValue)
                    {
                        AddCustomColumn(oAccountHdrInfo.ChangeTypeLabelID, null, sbMessages, oMultilingualAttributeInfo);
                    }
                    sbMessages.Append(GetHtmlForAccountInfoRow(dictAcctColumns, oAccountHdrInfo, oMultilingualAttributeInfo));
                    sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                    if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0
                        && oDataImportMessageIDFilter != null && oDataImportMessageIDFilter.Count > 0)
                    {
                        List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDtl = oDataImportMessageDetailInfoList.FindAll(T =>
                            (T.AccountInfo != null && T.AccountInfo.AccountID == oAccountHdrInfo.AccountID)
                            && (oDataImportMessageIDFilter == null || oDataImportMessageIDFilter.Exists(D => D == T.DataImportMessageID)));
                        if (oDataImportMessageDtl != null && oDataImportMessageDtl.Count > 0)
                        {
                            foreach (ClientModel.DataImportMessageDetailInfo oDetail in oDataImportMessageDtl)
                            {
                                if (!string.IsNullOrEmpty(oDetail.MessageSchema) && !string.IsNullOrEmpty(oDetail.MessageData))
                                {
                                    sbMessages.Append(SharedUtility.MailHelper.GetBeginRowHTML());
                                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
                                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                                    int colSpan = dictAcctColumns.Keys.Count + 1;
                                    sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML("colspan='" + colSpan.ToString() + "' padding='0px'"));
                                    sbMessages.Append(ConvertDataImportMessageInnerDetailToHtml(oDetail, oMultilingualAttributeInfo));
                                    sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
                                    sbMessages.Append(SharedUtility.MailHelper.GetEndRowHTML());
                                }
                            }
                        }
                    }
                }
                sbMessages.Append(Shared.Utility.MailHelper.GetEndTableHTML());
            }
            return sbMessages.ToString();
        }
        private static void AddCustomColumn(int? TdDataLabelID, string TdData, StringBuilder sbMessages, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            sbMessages.Append(SharedUtility.MailHelper.GetBeginColumnHTML());
            if (TdDataLabelID.HasValue)
                sbMessages.Append(LanguageUtil.GetValue(TdDataLabelID.Value, oMultilingualAttributeInfo));
            else
                sbMessages.Append(TdData);
            sbMessages.Append(SharedUtility.MailHelper.GetEndColumnHTML());
        }


        private static string FormatFailureMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                msg = string.Empty;
            msg = msg.Replace(" , ", "<br>");
            msg = msg.Replace(" ,", "<br>");
            msg = msg.Replace(",", "<br>");
            return msg;
        }

        private static DataTable GetLableIDDataTable()
        {
            DataTable dtLableID = new DataTable();
            dtLableID.Columns.Add("ID", typeof(int));

            DataRow dr1 = dtLableID.NewRow();
            dr1["ID"] = 1743;
            DataRow dr2 = dtLableID.NewRow();
            dr2["ID"] = 1744;
            DataRow dr3 = dtLableID.NewRow();
            dr3["ID"] = 1308;
            DataRow dr4 = dtLableID.NewRow();
            dr4["ID"] = 1745;
            DataRow dr5 = dtLableID.NewRow();
            dr5["ID"] = 1399;

            DataRow dr6 = dtLableID.NewRow();
            dr6["ID"] = 1752;
            DataRow dr7 = dtLableID.NewRow();
            dr7["ID"] = 1753;
            DataRow dr8 = dtLableID.NewRow();
            dr8["ID"] = 1308;
            DataRow dr9 = dtLableID.NewRow();
            dr9["ID"] = 1528;
            DataRow dr10 = dtLableID.NewRow();
            dr10["ID"] = 1051;
            DataRow dr11 = dtLableID.NewRow();
            dr11["ID"] = 1623;

            DataRow dr12 = dtLableID.NewRow();
            dr12["ID"] = 2177;

            DataRow dr13 = dtLableID.NewRow();
            dr13["ID"] = 2178;

            DataRow dr14 = dtLableID.NewRow();
            dr14["ID"] = 2179;//Following warnings occurred during upload
            // For Multiversion mail
            DataRow dr15 = dtLableID.NewRow();
            dr15["ID"] = 2312;
            DataRow dr16 = dtLableID.NewRow();
            dr16["ID"] = 2313;

            // For Matching Process
            DataRow dr17 = dtLableID.NewRow();
            dr17["ID"] = 2360;

            DataRow dr18 = dtLableID.NewRow();
            dr18["ID"] = 2361;

            DataRow dr19 = dtLableID.NewRow();
            dr19["ID"] = 2362;

            DataRow dr20 = dtLableID.NewRow();
            dr20["ID"] = 2363;

            DataRow dr21 = dtLableID.NewRow();
            dr21["ID"] = 2186;

            // For Multiversion Subledger mail
            DataRow dr22 = dtLableID.NewRow();
            dr22["ID"] = 2377;
            DataRow dr23 = dtLableID.NewRow();
            dr23["ID"] = 2378;

            DataRow dr24 = dtLableID.NewRow();
            dr24["ID"] = 2667;

            DataRow dr25 = dtLableID.NewRow();
            dr25["ID"] = 1357;

            DataRow dr26 = dtLableID.NewRow();
            dr26["ID"] = 1346;

            DataRow dr27 = dtLableID.NewRow();
            dr27["ID"] = 2723;

            DataRow dr28 = dtLableID.NewRow();
            dr28["ID"] = 2994;

            DataRow dr29 = dtLableID.NewRow();
            dr29["ID"] = 2995;

            dtLableID.Rows.Add(dr1);
            dtLableID.Rows.Add(dr2);
            dtLableID.Rows.Add(dr3);
            dtLableID.Rows.Add(dr4);
            dtLableID.Rows.Add(dr5);
            dtLableID.Rows.Add(dr6);
            dtLableID.Rows.Add(dr7);
            dtLableID.Rows.Add(dr8);
            dtLableID.Rows.Add(dr9);
            dtLableID.Rows.Add(dr10);
            dtLableID.Rows.Add(dr11);
            dtLableID.Rows.Add(dr12);
            dtLableID.Rows.Add(dr13);
            dtLableID.Rows.Add(dr14);
            dtLableID.Rows.Add(dr15);
            dtLableID.Rows.Add(dr16);
            dtLableID.Rows.Add(dr17);
            dtLableID.Rows.Add(dr18);
            dtLableID.Rows.Add(dr19);
            dtLableID.Rows.Add(dr20);
            dtLableID.Rows.Add(dr21);
            dtLableID.Rows.Add(dr22);
            dtLableID.Rows.Add(dr23);
            dtLableID.Rows.Add(dr24);
            dtLableID.Rows.Add(dr25);
            dtLableID.Rows.Add(dr26);
            dtLableID.Rows.Add(dr27);
            dtLableID.Rows.Add(dr28);
            dtLableID.Rows.Add(dr29);

            return dtLableID;
        }

        private static string GetEmailPhraseByLableID(DataTable dt, int LableID, CompanyUserInfo oCompanyUserInfo)
        {

            DataRow[] dr = dt.Select("LabelId=" + LableID.ToString());
            string phrase = string.Empty;
            if (dr.Length > 0)
            {
                phrase = dr[0]["Phrase"].ToString();
                if (string.IsNullOrEmpty(phrase))
                    Helper.LogError("LabelID found but phrase is empty or null for LabelID: " + LableID.ToString(), oCompanyUserInfo);
            }
            else
            {
                Helper.LogError("LabelID Not Found Label: " + LableID.ToString(), oCompanyUserInfo);
                return "";
            }
            return phrase;
        }

        private static void SendSuccessEmailToUsersForMultiversion(string dataImportType
          , int businessEntityID, int languageID, int defaultLanguageID
           , DateTime? dateAdded, List<ClientModel.UserAccountInfo> oUserAccountInfoCollection, int? UserID, short? RoleID, int RecPeriodID
           , int CompanyID, CompanyUserInfo oCompanyUserInfo, int DataImportID, DataImportHdrInfo oDataImportHdrInfoService)
        {

            string toEmailIds = "";

            SqlConnection oConnection = null;
            DataTable dtPhrases = null;
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(languageID, CompanyID);
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = DataImportHelper.GetAllDataImportFieldsWithMapping(DataImportID, CompanyID);
            Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns = GetAccountColumnsDictionary(oImportTemplateFieldMappingInfoList);
            try
            {
                oConnection = Helper.CreateConnection(oCompanyUserInfo);
                dtPhrases = Helper.GetPhrases(GetLableIDDataTable(), businessEntityID, languageID, defaultLanguageID, oConnection, oCompanyUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

            if (dataImportType == ServiceConstants.GLDATA)
            {
                ClientModel.DataImportHdrInfo oDataImportHdrInfo = DataImportHelper.GetDataImportHdrInfo(DataImportID, CompanyID);
                List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = oDataImportHdrInfo.DataImportMessageDetailInfoList;
                List<ClientModel.DataImportMessageDetailInfo> oDataImportAccountMessageDetailInfoList = oDataImportHdrInfo.DataImportAccountMessageDetailInfoList;
                List<SkyStem.ART.Client.Model.AccountHdrInfo> oNewAccountHdrInfoCollection = DataImportHelper.GetNewAccounts(DataImportID, CompanyID);
                List<SkyStem.ART.Client.Model.AccountHdrInfo> oListAccountHdrInfo = DataImportHelper.GetAccountInformationWithoutGL(UserID, RoleID, RecPeriodID, CompanyID);
                List<SkyStem.ART.Client.Model.AccountHdrInfo> oNewAccountMailSentCollection = new List<ClientModel.AccountHdrInfo>();
                List<int> oDataImportMessageIDFilter = new List<int>();
                oDataImportMessageIDFilter.Add((int)Enums.DataImportMessage.WillUpdatExistingAccount);
                ClientModel.UserHdrInfo oUserHdrInfo = null;
                if (UserID.HasValue)
                    oUserHdrInfo = DataImportHelper.GetUserDetail(UserID.Value, CompanyID);
                for (int i = 0; i < oUserAccountInfoCollection.Count; i++)
                {
                    toEmailIds = oUserAccountInfoCollection[i].EmailID;
                    string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
                    string emailSubject = string.Empty;
                    StringBuilder emailBody = new StringBuilder();
                    if (oUserAccountInfoCollection[i].RoleID == 2)
                    {
                        if (UserID.HasValue)
                        {
                            emailBody.Append(GetDearString(oUserHdrInfo, oMultilingualAttributeInfo));
                        }
                        emailSubject = GetEmailSubject(2312, oDataImportHdrInfoService, oMultilingualAttributeInfo);
                        emailBody.Append(string.Format("{0} {1}.", dataImportType, GetEmailPhraseByLableID(dtPhrases, 1744, oCompanyUserInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1528, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDateTime(dateAdded, oMultilingualAttributeInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2888, oMultilingualAttributeInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportAccountMessageDetailInfoList, RoleID, CompanyID, languageID, true, oDataImportHdrInfo));
                        if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                        {
                            emailBody.Append("<br>");
                            emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2889, oMultilingualAttributeInfo)));
                            emailBody.Append("<br>");
                            emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportMessageDetailInfoList, RoleID, CompanyID, languageID, false, oDataImportHdrInfo));
                        }
                        // Append Details of Account For which GL is not Loaded
                        if (oListAccountHdrInfo != null && oListAccountHdrInfo.Count > 0)
                        {
                            StringBuilder oAccountDetails = new StringBuilder();
                            oAccountDetails.Append("<br/><br/><b>");
                            oAccountDetails.Append(LanguageUtil.GetValue(2667, oMultilingualAttributeInfo));
                            oAccountDetails.Append("</b><br/><br/>");
                            oAccountDetails.Append(ConvertAccountInfoToHtml(oListAccountHdrInfo, oImportTemplateFieldMappingInfoList, oMultilingualAttributeInfo));
                            //Helper.GetAccountDetailsForMail(oAccountDetails, oListAccountHdrInfo, languageID, CompanyID, GetEmailPhraseByLableID(dtPhrases, 1357, oCompanyUserInfo), GetEmailPhraseByLableID(dtPhrases, 1346, oCompanyUserInfo), oMultilingualAttributeInfo);
                            emailBody.Append(oAccountDetails.ToString());
                        }
                        // Append Details of new created/ updated Accounts    
                        if (oNewAccountHdrInfoCollection != null && oNewAccountHdrInfoCollection.Count > 0)
                        {
                            StringBuilder oAccountDetails = new StringBuilder();
                            oAccountDetails.Append("<br/><br/><b>");
                            oAccountDetails.Append(LanguageUtil.GetValue(2743, oMultilingualAttributeInfo));
                            oAccountDetails.Append("</b><br/><br/>");
                            oAccountDetails.Append(ConvertAccountInfoToHtml(oNewAccountHdrInfoCollection, oImportTemplateFieldMappingInfoList, oMultilingualAttributeInfo));
                            //Helper.GetAccountDetailsForMail(oAccountDetails, oNewAccountHdrInfoCollection, languageID, CompanyID, GetEmailPhraseByLableID(dtPhrases, 1357, oCompanyUserInfo), GetEmailPhraseByLableID(dtPhrases, 1346, oCompanyUserInfo), oMultilingualAttributeInfo);
                            emailBody.Append(oAccountDetails.ToString());
                        }
                        emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserHdrInfo.CompanyDisplayNameLabelID, oMultilingualAttributeInfo, null));
                    }
                    if (oUserAccountInfoCollection[i].RoleID != 2)
                    {
                        List<SkyStem.ART.Client.Model.AccountHdrInfo> oUserNewAccountHdrInfoCollection = null;

                        MultilingualAttributeInfo oUserMultilingualAttributeInfo;
                        if (oUserAccountInfoCollection[i].DefaultLanguageID.HasValue && oUserAccountInfoCollection[i].DefaultLanguageID.Value > 0)
                            oUserMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserAccountInfoCollection[i].DefaultLanguageID.Value, CompanyID);
                        else
                            oUserMultilingualAttributeInfo = oMultilingualAttributeInfo;
                        emailSubject = GetEmailSubject(2312, oDataImportHdrInfoService, oUserMultilingualAttributeInfo);
                        if (oUserAccountInfoCollection[i].AccountInfoCollection.Count > 0)
                        {
                            if (oUserAccountInfoCollection[i].UserID.HasValue)
                            {
                                ClientModel.UserHdrInfo oUserInfo = DataImportHelper.GetUserDetail(oUserAccountInfoCollection[i].UserID.Value, CompanyID);
                                emailBody.Append(GetDearString(oUserInfo, oUserMultilingualAttributeInfo));
                            }
                            emailBody.Append("<b>");
                            emailBody.Append(LanguageUtil.GetValue(2899, oUserMultilingualAttributeInfo));
                            emailBody.Append("</b><br/><br/>");
                            emailBody.Append(ConvertAccountInfoToHtml(DataImportHelper.GetAccountInformationWithBalanceChange(oUserAccountInfoCollection[i].AccountInfoCollection, CompanyID), oImportTemplateFieldMappingInfoList, oUserMultilingualAttributeInfo));
                            if (oUserAccountInfoCollection[i].UserID.HasValue && oNewAccountHdrInfoCollection != null && oNewAccountHdrInfoCollection.Count > 0)
                            {
                                if (oUserAccountInfoCollection[i].RoleID == 3)
                                {
                                    oUserNewAccountHdrInfoCollection = oNewAccountHdrInfoCollection.FindAll(obj => obj.PreparerUserID.HasValue && obj.PreparerUserID.Value == oUserAccountInfoCollection[i].UserID.Value && obj.ActionTypeID.HasValue && obj.ActionTypeID.Value == 21);
                                }
                                if (oUserAccountInfoCollection[i].RoleID == 4)
                                {
                                    oUserNewAccountHdrInfoCollection = oNewAccountHdrInfoCollection.FindAll(obj => obj.ReviewerUserID.HasValue && obj.ReviewerUserID.Value == oUserAccountInfoCollection[i].UserID.Value && obj.ActionTypeID.HasValue && obj.ActionTypeID.Value == 21);
                                }
                                if (oUserAccountInfoCollection[i].RoleID == 5)
                                {
                                    oUserNewAccountHdrInfoCollection = oNewAccountHdrInfoCollection.FindAll(obj => obj.ApproverUserID.HasValue && obj.ApproverUserID.Value == oUserAccountInfoCollection[i].UserID.Value && obj.ActionTypeID.HasValue && obj.ActionTypeID.Value == 21);
                                }
                                if (oUserNewAccountHdrInfoCollection != null && oUserNewAccountHdrInfoCollection.Count > 0)
                                {
                                    StringBuilder oAccountDetails = new StringBuilder();
                                    oAccountDetails.Append("<br/><br/><b>");
                                    oAccountDetails.Append(LanguageUtil.GetValue(2313, oUserMultilingualAttributeInfo));
                                    oAccountDetails.Append("</b><br/><br/>");
                                    oAccountDetails.Append(ConvertAccountInfoToHtmlWithChangeDetail(oUserNewAccountHdrInfoCollection
                                        , oDataImportAccountMessageDetailInfoList, dictAcctColumns, oDataImportMessageIDFilter, oUserMultilingualAttributeInfo));
                                    emailBody.Append(oAccountDetails.ToString());
                                    if (!string.IsNullOrEmpty(oAccountDetails.ToString()))
                                    {
                                        oNewAccountMailSentCollection.AddRange(oUserNewAccountHdrInfoCollection);
                                    }
                                }
                            }
                        }
                        emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserHdrInfo.CompanyDisplayNameLabelID, oUserMultilingualAttributeInfo, null));
                    }

                    SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
                }

                if (oNewAccountHdrInfoCollection != null && oNewAccountHdrInfoCollection.Count > 0 && oNewAccountHdrInfoCollection.Count != oNewAccountMailSentCollection.Count)
                {
                    //Remove All Records Email alresddy sent                    
                    oNewAccountHdrInfoCollection.RemoveAll(obj => oNewAccountMailSentCollection.Contains(obj));
                    // Get Distinct User ID
                    List<int?> PreparerUserIDList = oNewAccountHdrInfoCollection.GroupBy(Item => Item.PreparerUserID)
                                                                                .Select(grp => grp.First().PreparerUserID)
                                                                                .ToList();
                    List<int?> ReviewerUserIDList = oNewAccountHdrInfoCollection.GroupBy(Item => Item.ReviewerUserID)
                                                                                .Select(grp => grp.First().ReviewerUserID)
                                                                                .ToList();
                    List<int?> ApproverUserIDList = oNewAccountHdrInfoCollection.GroupBy(Item => Item.ApproverUserID)
                                                                                .Select(grp => grp.First().ApproverUserID)
                                                                                .ToList();
                    if (PreparerUserIDList != null && PreparerUserIDList.Count > 0)
                        SendModifyAccountMailForPRA(CompanyID, 3, PreparerUserIDList, oNewAccountHdrInfoCollection, oDataImportAccountMessageDetailInfoList, dictAcctColumns, oDataImportMessageIDFilter, oMultilingualAttributeInfo, oDataImportHdrInfoService, oCompanyUserInfo);
                    if (ReviewerUserIDList != null && ReviewerUserIDList.Count > 0)
                        SendModifyAccountMailForPRA(CompanyID, 4, ReviewerUserIDList, oNewAccountHdrInfoCollection, oDataImportAccountMessageDetailInfoList, dictAcctColumns, oDataImportMessageIDFilter, oMultilingualAttributeInfo, oDataImportHdrInfoService, oCompanyUserInfo);
                    if (ApproverUserIDList != null && ApproverUserIDList.Count > 0)
                        SendModifyAccountMailForPRA(CompanyID, 5, ApproverUserIDList, oNewAccountHdrInfoCollection, oDataImportAccountMessageDetailInfoList, dictAcctColumns, oDataImportMessageIDFilter, oMultilingualAttributeInfo, oDataImportHdrInfoService, oCompanyUserInfo);
                }

            }
            else if (dataImportType == ServiceConstants.ACCOUNTDATA)
            {
                for (int i = 0; i < oUserAccountInfoCollection.Count; i++)
                {
                    toEmailIds = oUserAccountInfoCollection[i].EmailID;
                    string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
                    string emailSubject = GetEmailSubject(2312, oDataImportHdrInfoService, oMultilingualAttributeInfo);
                    StringBuilder emailBody = new StringBuilder();
                    emailBody.Append(string.Format("{0} {1}.", dataImportType, GetEmailPhraseByLableID(dtPhrases, 1744, oCompanyUserInfo)));
                    emailBody.Append("<br>");
                    emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1528, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDateTime(dateAdded, oMultilingualAttributeInfo)));
                    emailBody.Append("<br>");
                    emailBody.Append(string.Format("{0} ", GetEmailPhraseByLableID(dtPhrases, 2313, oCompanyUserInfo)));
                    for (int j = 0; j < oUserAccountInfoCollection[i].AccountInfoCollection.Count; j++)
                    {
                        emailBody.Append("<br>");
                        emailBody.Append(string.Format("{0}", oUserAccountInfoCollection[i].AccountInfoCollection[j]));
                    }

                    SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
                }
            }
            else if (dataImportType == ServiceConstants.SUBLEDGER)
            {
                ClientModel.DataImportHdrInfo oDataImportHdrInfo = DataImportHelper.GetDataImportHdrInfo(DataImportID, CompanyID);
                List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = oDataImportHdrInfo.DataImportMessageDetailInfoList;
                List<ClientModel.DataImportMessageDetailInfo> oDataImportAccountMessageDetailInfoList = oDataImportHdrInfo.DataImportAccountMessageDetailInfoList;
                ClientModel.UserHdrInfo oUserHdrInfo = null;
                if (UserID.HasValue)
                    oUserHdrInfo = DataImportHelper.GetUserDetail(UserID.Value, CompanyID);
                for (int i = 0; i < oUserAccountInfoCollection.Count; i++)
                {
                    toEmailIds = oUserAccountInfoCollection[i].EmailID;
                    string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
                    string emailSubject = GetEmailSubject(2377, oDataImportHdrInfoService, oMultilingualAttributeInfo);
                    StringBuilder emailBody = new StringBuilder();
                    if (oUserAccountInfoCollection[i].RoleID == 2)
                    {
                        if (UserID.HasValue)
                        {
                            emailBody.Append(GetDearString(oUserHdrInfo, oMultilingualAttributeInfo));
                        }

                        emailBody.Append(string.Format("{0} {1}.", dataImportType, GetEmailPhraseByLableID(dtPhrases, 1744, oCompanyUserInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1528, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDateTime(dateAdded, oMultilingualAttributeInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2888, oMultilingualAttributeInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportAccountMessageDetailInfoList, RoleID, CompanyID, languageID, true, oDataImportHdrInfo));
                        emailBody.Append("<br>");
                        if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                        {
                            emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2889, oMultilingualAttributeInfo)));
                            emailBody.Append("<br>");
                            emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportMessageDetailInfoList, RoleID, CompanyID, languageID, false, oDataImportHdrInfo));
                        }
                        emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserHdrInfo.CompanyDisplayNameLabelID, oMultilingualAttributeInfo, null));
                    }
                    if (oUserAccountInfoCollection[i].RoleID != 2)
                    {
                        MultilingualAttributeInfo oUserMultilingualAttributeInfo = null;
                        if (oUserAccountInfoCollection[i].AccountInfoCollection.Count > 0)
                        {
                            if (oUserAccountInfoCollection[i].DefaultLanguageID.HasValue && oUserAccountInfoCollection[i].DefaultLanguageID.Value > 0)
                                oUserMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserAccountInfoCollection[i].DefaultLanguageID.Value, CompanyID);
                            else
                                oUserMultilingualAttributeInfo = oMultilingualAttributeInfo;
                            if (oUserAccountInfoCollection[i].UserID.HasValue)
                            {
                                ClientModel.UserHdrInfo oUserInfo = DataImportHelper.GetUserDetail(oUserAccountInfoCollection[i].UserID.Value, CompanyID);
                                emailBody.Append(GetDearString(oUserInfo, oUserMultilingualAttributeInfo));
                            }
                            emailBody.Append("<b>");
                            emailBody.Append(LanguageUtil.GetValue(2378, oUserMultilingualAttributeInfo));
                            emailBody.Append("</b><br/><br/>");
                            emailBody.Append(ConvertAccountInfoToHtml(DataImportHelper.GetAccountInformationWithBalanceChange(oUserAccountInfoCollection[i].AccountInfoCollection, CompanyID), oImportTemplateFieldMappingInfoList, oUserMultilingualAttributeInfo));
                        }
                        emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserHdrInfo.CompanyDisplayNameLabelID, oUserMultilingualAttributeInfo, null));
                    }

                    SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
                }
            }
            else if (dataImportType == ServiceConstants.CURRENCYLOAD)
            {
                ClientModel.DataImportHdrInfo oDataImportHdrInfo = DataImportHelper.GetDataImportHdrInfo(DataImportID, CompanyID);
                List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = oDataImportHdrInfo.DataImportMessageDetailInfoList;
                List<ClientModel.DataImportMessageDetailInfo> oDataImportAccountMessageDetailInfoList = oDataImportHdrInfo.DataImportAccountMessageDetailInfoList;
                List<SkyStem.ART.Client.Model.AccountHdrInfo> oNewAccountMailSentCollection = new List<ClientModel.AccountHdrInfo>();
                List<int> oDataImportMessageIDFilter = new List<int>();
                oDataImportMessageIDFilter.Add((int)Enums.DataImportMessage.WillUpdatExistingAccount);
                ClientModel.UserHdrInfo oUserHdrInfo = null;
                if (UserID.HasValue)
                    oUserHdrInfo = DataImportHelper.GetUserDetail(UserID.Value, CompanyID);
                for (int i = 0; i < oUserAccountInfoCollection.Count; i++)
                {
                    toEmailIds = oUserAccountInfoCollection[i].EmailID;
                    string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
                    string emailSubject = string.Empty;
                    StringBuilder emailBody = new StringBuilder();
                    if (oUserAccountInfoCollection[i].RoleID == 2)
                    {
                        if (UserID.HasValue)
                        {
                            emailBody.Append(GetDearString(oUserHdrInfo, oMultilingualAttributeInfo));
                        }
                        emailSubject = GetEmailSubject(2994, oDataImportHdrInfoService, oMultilingualAttributeInfo);
                        emailBody.Append(string.Format("{0} {1}.", dataImportType, GetEmailPhraseByLableID(dtPhrases, 1744, oCompanyUserInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1528, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDateTime(dateAdded, oMultilingualAttributeInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2888, oMultilingualAttributeInfo)));
                        emailBody.Append("<br>");
                        emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportAccountMessageDetailInfoList, RoleID, CompanyID, languageID, true, oDataImportHdrInfo));
                        if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                        {
                            emailBody.Append("<br>");
                            emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2889, oMultilingualAttributeInfo)));
                            emailBody.Append("<br>");
                            emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportMessageDetailInfoList, RoleID, CompanyID, languageID, false, oDataImportHdrInfo));
                        }
                        emailBody.Append("<br/><b>");
                        emailBody.Append(LanguageUtil.GetValue(2995, oMultilingualAttributeInfo));
                        emailBody.Append("</b><br/><br/>");
                        List<string> accountInfoList = new List<string>();
                        oUserAccountInfoCollection.ForEach(T =>
                        {
                            accountInfoList.AddRange(T.AccountInfoCollection);
                        });
                        emailBody.Append(ConvertAccountInfoToHtml(DataImportHelper.GetAccountInformationWithKeyValue(accountInfoList, CompanyID), GetDefaultAccountColumnsDictionary(oDataImportHdrInfoService), oMultilingualAttributeInfo));
                        emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserHdrInfo.CompanyDisplayNameLabelID, oMultilingualAttributeInfo, null));
                    }
                    if (oUserAccountInfoCollection[i].RoleID != 2)
                    {
                        MultilingualAttributeInfo oUserMultilingualAttributeInfo;
                        if (oUserAccountInfoCollection[i].DefaultLanguageID.HasValue && oUserAccountInfoCollection[i].DefaultLanguageID.Value > 0)
                            oUserMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserAccountInfoCollection[i].DefaultLanguageID.Value, CompanyID);
                        else
                            oUserMultilingualAttributeInfo = oMultilingualAttributeInfo;
                        emailSubject = GetEmailSubject(2994, oDataImportHdrInfoService, oUserMultilingualAttributeInfo);
                        if (oUserAccountInfoCollection[i].AccountInfoCollection.Count > 0)
                        {
                            if (oUserAccountInfoCollection[i].UserID.HasValue)
                            {
                                ClientModel.UserHdrInfo oUserInfo = DataImportHelper.GetUserDetail(oUserAccountInfoCollection[i].UserID.Value, CompanyID);
                                emailBody.Append(GetDearString(oUserInfo, oUserMultilingualAttributeInfo));
                            }
                            emailBody.Append("<b>");
                            emailBody.Append(LanguageUtil.GetValue(2995, oUserMultilingualAttributeInfo));
                            emailBody.Append("</b><br/><br/>");
                            emailBody.Append(ConvertAccountInfoToHtml(DataImportHelper.GetAccountInformationWithKeyValue(oUserAccountInfoCollection[i].AccountInfoCollection, CompanyID), GetDefaultAccountColumnsDictionary(oDataImportHdrInfoService), oUserMultilingualAttributeInfo));
                        }
                        emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserHdrInfo.CompanyDisplayNameLabelID, oUserMultilingualAttributeInfo, null));
                    }
                    SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
                }
            }
        }
        private static void SendModifyAccountMailForPRA(int CompanyID, short RoleID, List<int?> userIDList,
            List<SkyStem.ART.Client.Model.AccountHdrInfo> oNewAccountHdrInfoCollection,
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList,
            Dictionary<ARTEnums.DataImportFields, ClientModel.ImportTemplateFieldMappingInfo> dictAcctColumns,
            List<int> oDataImportMessageIDFilter,
            MultilingualAttributeInfo oMultilingualAttributeInfo,
            DataImportHdrInfo oDataImportHdrInfoService,
            CompanyUserInfo oCompanyUserInfo)
        {
            StringBuilder emailBody = new StringBuilder();
            string toEmailIds = "";
            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
            string emailSubject = string.Empty;
            List<SkyStem.ART.Client.Model.AccountHdrInfo> oUserNewAccountHdrInfoCollection = null;
            ClientModel.UserHdrInfo oUserInfo = null;
            MultilingualAttributeInfo oUserMultilingualAttributeInfo = null;
            foreach (int? userID in userIDList)
            {

                if (userID.HasValue)
                    oUserInfo = DataImportHelper.GetUserDetail(userID.Value, CompanyID);
                if (oUserInfo != null)
                {

                    if (oNewAccountHdrInfoCollection != null && oNewAccountHdrInfoCollection.Count > 0)
                    {
                        if (RoleID == 3)
                        {
                            oUserNewAccountHdrInfoCollection = oNewAccountHdrInfoCollection.FindAll(obj => obj.PreparerUserID.HasValue && obj.PreparerUserID.Value == userID.Value && obj.ActionTypeID.HasValue && obj.ActionTypeID.Value == 21);
                            if (oUserNewAccountHdrInfoCollection != null && oUserNewAccountHdrInfoCollection.Count > 0 && oUserNewAccountHdrInfoCollection[0].PreparerLanguageID.HasValue && oUserNewAccountHdrInfoCollection[0].PreparerLanguageID.Value > 0)
                                oUserMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserNewAccountHdrInfoCollection[0].PreparerLanguageID.Value, CompanyID);
                            else
                                oUserMultilingualAttributeInfo = oMultilingualAttributeInfo;
                        }
                        if (RoleID == 4)
                        {
                            oUserNewAccountHdrInfoCollection = oNewAccountHdrInfoCollection.FindAll(obj => obj.ReviewerUserID.HasValue && obj.ReviewerUserID.Value == userID.Value && obj.ActionTypeID.HasValue && obj.ActionTypeID.Value == 21);
                            if (oUserNewAccountHdrInfoCollection != null && oUserNewAccountHdrInfoCollection.Count > 0 && oUserNewAccountHdrInfoCollection[0].ReviewerLanguageID.HasValue && oUserNewAccountHdrInfoCollection[0].ReviewerLanguageID.Value > 0)
                                oUserMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserNewAccountHdrInfoCollection[0].ReviewerLanguageID.Value, CompanyID);
                            else
                                oUserMultilingualAttributeInfo = oMultilingualAttributeInfo;
                        }
                        if (RoleID == 5)
                        {
                            oUserNewAccountHdrInfoCollection = oNewAccountHdrInfoCollection.FindAll(obj => obj.ApproverUserID.HasValue && obj.ApproverUserID.Value == userID.Value && obj.ActionTypeID.HasValue && obj.ActionTypeID.Value == 21);
                            if (oUserNewAccountHdrInfoCollection != null && oUserNewAccountHdrInfoCollection.Count > 0 && oUserNewAccountHdrInfoCollection[0].ApproverLanguageID.HasValue && oUserNewAccountHdrInfoCollection[0].ApproverLanguageID.Value > 0)
                                oUserMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserNewAccountHdrInfoCollection[0].ApproverLanguageID.Value, CompanyID);
                            else
                                oUserMultilingualAttributeInfo = oMultilingualAttributeInfo;
                        }
                        if (oUserNewAccountHdrInfoCollection != null && oUserNewAccountHdrInfoCollection.Count > 0)
                        {
                            emailSubject = GetEmailSubject(2312, oDataImportHdrInfoService, oUserMultilingualAttributeInfo);
                            emailBody.Append(GetDearString(oUserInfo, oUserMultilingualAttributeInfo));
                            toEmailIds = oUserInfo.EmailID;
                            StringBuilder oAccountDetails = new StringBuilder();
                            oAccountDetails.Append("<br/><br/><b>");
                            oAccountDetails.Append(LanguageUtil.GetValue(2313, oUserMultilingualAttributeInfo));
                            oAccountDetails.Append("</b><br/><br/>");
                            oAccountDetails.Append(ConvertAccountInfoToHtmlWithChangeDetail(oUserNewAccountHdrInfoCollection, oDataImportMessageDetailInfoList, dictAcctColumns, oDataImportMessageIDFilter, oUserMultilingualAttributeInfo));
                            emailBody.Append(oAccountDetails.ToString());
                            emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserInfo.CompanyDisplayNameLabelID, oUserMultilingualAttributeInfo, null));
                            SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
                        }
                    }
                }
            }
        }

        private static void SendSuccessEmailToUsers(string successEmailIds, string dataImportType
            , string profileName, int recordsAffected, int businessEntityID, int languageID, int defaultLanguageID
            , DateTime? dateAdded, int? UserID, short? RoleID, int RecPeriodID, int CompanyID
            , DataImportHdrInfo oDataImportHdrInfoService, CompanyUserInfo oCompanyUserInfo, int DataImportID)
        {

            ClientModel.UserHdrInfo oUserHdrInfo = null;

            string toEmailIds = successEmailIds;
            SqlConnection oConnection = null;
            DataTable dtPhrases = null;
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(languageID, CompanyID);
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = DataImportHelper.GetAllDataImportFieldsWithMapping(DataImportID, CompanyID);
            try
            {
                oConnection = Helper.CreateConnection(oCompanyUserInfo);
                dtPhrases = Helper.GetPhrases(GetLableIDDataTable(), businessEntityID, languageID, defaultLanguageID, oConnection, oCompanyUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
            string emailSubject = GetEmailSubject(1743, oDataImportHdrInfoService, oMultilingualAttributeInfo);
            StringBuilder emailBody = new StringBuilder();
            if (UserID.HasValue)
            {
                oUserHdrInfo = DataImportHelper.GetUserDetail(UserID.Value, CompanyID);
                emailBody.Append(GetDearString(oUserHdrInfo, oMultilingualAttributeInfo));
            }
            emailBody.Append(string.Format("{0} {1}.", dataImportType, GetEmailPhraseByLableID(dtPhrases, 1744, oCompanyUserInfo)));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1308, oCompanyUserInfo), profileName));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}:{1} ", GetEmailPhraseByLableID(dtPhrases, 1745, oCompanyUserInfo), recordsAffected));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1528, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDateTime(dateAdded, oMultilingualAttributeInfo)));

            if (dataImportType == ServiceConstants.GLDATA
              || dataImportType == ServiceConstants.SUBLEDGER)
            {
                ClientModel.DataImportHdrInfo oDataImportHdrInfo = DataImportHelper.GetDataImportHdrInfo(DataImportID, CompanyID);
                emailBody.Append("<br>");
                emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2888, oMultilingualAttributeInfo)));
                emailBody.Append("<br>");
                emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportHdrInfo.DataImportAccountMessageDetailInfoList, RoleID, CompanyID, languageID, true, oDataImportHdrInfo));
                emailBody.Append("<br>");
                if (oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
                {
                    emailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2889, oMultilingualAttributeInfo)));
                    emailBody.Append("<br>");
                    emailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportHdrInfo.DataImportMessageDetailInfoList, RoleID, CompanyID, languageID, false, oDataImportHdrInfo));
                }
            }

            // Append Details of Account For which GL is not Loaded
            if (dataImportType == ServiceConstants.GLDATA)
            {
                List<SkyStem.ART.Client.Model.AccountHdrInfo> oListAccountHdrInfo = DataImportHelper.GetAccountInformationWithoutGL(UserID, RoleID, RecPeriodID, CompanyID);
                if (oListAccountHdrInfo != null && oListAccountHdrInfo.Count > 0)
                {
                    StringBuilder oAccountDetails = new StringBuilder();
                    oAccountDetails.Append("<br/><br/><b>");
                    oAccountDetails.Append(LanguageUtil.GetValue(2667, oMultilingualAttributeInfo));
                    oAccountDetails.Append("</b><br/><br/>");
                    oAccountDetails.Append(ConvertAccountInfoToHtml(oListAccountHdrInfo, oImportTemplateFieldMappingInfoList, oMultilingualAttributeInfo));
                    // Helper.GetAccountDetailsForMail(oAccountDetails, oListAccountHdrInfo, languageID, CompanyID, GetEmailPhraseByLableID(dtPhrases, 1357, oCompanyUserInfo), GetEmailPhraseByLableID(dtPhrases, 1346, oCompanyUserInfo), oMultilingualAttributeInfo);
                    emailBody.Append(oAccountDetails.ToString());
                }

                // Append Details of new created/ updated Accounts               
                List<SkyStem.ART.Client.Model.AccountHdrInfo> oNewAccountHdrInfoCollection = DataImportHelper.GetNewAccounts(DataImportID, CompanyID);
                if (oNewAccountHdrInfoCollection != null && oNewAccountHdrInfoCollection.Count > 0)
                {
                    StringBuilder oAccountDetails = new StringBuilder();
                    oAccountDetails.Append("<br/><br/><b>");
                    oAccountDetails.Append(LanguageUtil.GetValue(2743, oMultilingualAttributeInfo));
                    oAccountDetails.Append("</b><br/><br/>");
                    oAccountDetails.Append(ConvertAccountInfoToHtml(oNewAccountHdrInfoCollection, oImportTemplateFieldMappingInfoList, oMultilingualAttributeInfo));
                    //Helper.GetAccountDetailsForMail(oAccountDetails, oNewAccountHdrInfoCollection, languageID, CompanyID, GetEmailPhraseByLableID(dtPhrases, 1357, oCompanyUserInfo), GetEmailPhraseByLableID(dtPhrases, 1346, oCompanyUserInfo), oMultilingualAttributeInfo);
                    emailBody.Append(oAccountDetails.ToString());
                }
            }
            emailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailId, oUserHdrInfo.CompanyDisplayNameLabelID, oMultilingualAttributeInfo, null));
            SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
        }

        private static void SendWarningEmailToUsers(string warningEmailIds, string dataImportType
             , string profileName, string errorMessage, int businessEntityID, int languageID, int defaultLanguageID
            , DateTime? dateAdded, int dataImportID, short? roleID, int CompanyID, DataImportHdrInfo oDataImportHdrInfoService,
            CompanyUserInfo oCompanyUserInfo)
        {
            string toEmailIds = warningEmailIds;
            SqlConnection oConnection = null;
            DataTable dtPhrases = null;
            try
            {
                oConnection = Helper.CreateConnection(oCompanyUserInfo);
                dtPhrases = Helper.GetPhrases(GetLableIDDataTable(), businessEntityID, languageID, defaultLanguageID, oConnection, oCompanyUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(languageID, businessEntityID);
            string errMessage = Helper.FormatFailureMessage(errorMessage);
            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");//AppSettingHelper.GetAppSettingValue(con.EMAIL_FROM_DEFAULT);
            string mailSubject = GetEmailSubject(2177, oDataImportHdrInfoService, oMultilingualAttributeInfo);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} {1}.", dataImportType, GetEmailPhraseByLableID(dtPhrases, 2178, oCompanyUserInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1308, oCompanyUserInfo), profileName));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1528, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDateTime(dateAdded, oMultilingualAttributeInfo)));

            if (dataImportType == ServiceConstants.GLDATA
                || dataImportType == ServiceConstants.SUBLEDGER
                || dataImportType == ServiceConstants.CURRENCYLOAD)
            {
                ClientModel.DataImportHdrInfo oDataImportHdrInfo = DataImportHelper.GetDataImportHdrInfo(dataImportID, CompanyID);
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2888, oMultilingualAttributeInfo)));
                oMailBody.Append("<br>");
                oMailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportHdrInfo.DataImportAccountMessageDetailInfoList, roleID, CompanyID, languageID, true, oDataImportHdrInfo));
                oMailBody.Append("<br>");
                if (oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
                {
                    oMailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2889, oMultilingualAttributeInfo)));
                    oMailBody.Append("<br>");
                    oMailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportHdrInfo.DataImportMessageDetailInfoList, roleID, CompanyID, languageID, false, oDataImportHdrInfo));
                }
            }
            else
            {
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: <br>{1} ", GetEmailPhraseByLableID(dtPhrases, 2179, oCompanyUserInfo), errorMessage));
            }

            SendEmail(fromEmailId, toEmailIds, mailSubject, oMailBody.ToString(), oCompanyUserInfo);
        }

        private static void SendFailureEmailToUsers(string failureEmailIds, string dataImportType
             , string profileName, string errorMessage, int businessEntityID, int languageID, int defaultLanguageID
            , DateTime? dateAdded, int dataImportID, short? roleID, int CompanyID
            , DataImportHdrInfo oDataImportHdrInfoService, CompanyUserInfo oCompanyUserInfo)
        {
            string toEmailIds = failureEmailIds;
            SqlConnection oConnection = null;
            DataTable dtPhrases = null;
            try
            {
                oConnection = Helper.CreateConnection(oCompanyUserInfo);
                dtPhrases = Helper.GetPhrases(GetLableIDDataTable(), businessEntityID, languageID, defaultLanguageID, oConnection, oCompanyUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(languageID, businessEntityID);

            string errMessage = Helper.FormatFailureMessage(errorMessage);
            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");//AppSettingHelper.GetAppSettingValue(con.EMAIL_FROM_DEFAULT);
            string mailSubject = GetEmailSubject(1752, oDataImportHdrInfoService, oMultilingualAttributeInfo);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} {1}.", dataImportType, GetEmailPhraseByLableID(dtPhrases, 1753, oCompanyUserInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 1308, oCompanyUserInfo), profileName));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}:{1} ", GetEmailPhraseByLableID(dtPhrases, 1528, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDateTime(dateAdded, oMultilingualAttributeInfo)));

            if (dataImportType == ServiceConstants.GLDATA
                || dataImportType == ServiceConstants.SUBLEDGER
                || dataImportType == ServiceConstants.CURRENCYLOAD
                )
            {
                ClientModel.DataImportHdrInfo oDataImportHdrInfo = DataImportHelper.GetDataImportHdrInfo(dataImportID, CompanyID);
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2888, oMultilingualAttributeInfo)));
                oMailBody.Append("<br>");
                oMailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportHdrInfo.DataImportAccountMessageDetailInfoList, roleID, CompanyID, languageID, true, oDataImportHdrInfo));
                oMailBody.Append("<br>");
                if (oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
                {
                    oMailBody.Append(string.Format("{0}:", LanguageUtil.GetValue(2889, oMultilingualAttributeInfo)));
                    oMailBody.Append("<br>");
                    oMailBody.Append(ConvertDataImportMessageDetailToHtml(oDataImportHdrInfo.DataImportMessageDetailInfoList, roleID, CompanyID, languageID, false, oDataImportHdrInfo));
                }
            }
            else
            {
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: <br>{1} ", GetEmailPhraseByLableID(dtPhrases, 1623, oCompanyUserInfo), errorMessage));
            }
            SendEmail(fromEmailId, toEmailIds, mailSubject, oMailBody.ToString(), oCompanyUserInfo);
        }

        private static void SendSuccessEmailToUsers(string successEmailIds, string dataImportType
            , string profileName, int recordsAffected, DataImportHdrInfo oDataImportHdrInfoService
            , CompanyUserInfo oCompanyUserInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {

            string toEmailIds = successEmailIds;


            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
            string emailSubject = GetEmailSubject(1743, oDataImportHdrInfoService, oMultilingualAttributeInfo);
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append(string.Format("{0} {1}", dataImportType, LanguageUtil.GetValue(1744, oMultilingualAttributeInfo)));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1308, oMultilingualAttributeInfo), profileName));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}:{1} ", LanguageUtil.GetValue(1745, oMultilingualAttributeInfo), recordsAffected));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1399, oMultilingualAttributeInfo), Helper.GetDisplayDate(DateTime.Now)));
            SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
        }

        private static void SendWarningEmailToUsers(string failureEmailIds, string dataImportType
             , string profileName, string errorMessage, DataImportHdrInfo oDataImportHdrInfoService
            , CompanyUserInfo oCompanyUserInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string toEmailIds = failureEmailIds;
            string errMessage = Helper.FormatFailureMessage(errorMessage);
            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");//AppSettingHelper.GetAppSettingValue(con.EMAIL_FROM_DEFAULT);
            string mailSubject = GetEmailSubject(1752, oDataImportHdrInfoService, oMultilingualAttributeInfo);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} {1}", dataImportType, LanguageUtil.GetValue(1753, oMultilingualAttributeInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1308, oMultilingualAttributeInfo), profileName));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}:{1} ", LanguageUtil.GetValue(1399, oMultilingualAttributeInfo), Helper.GetDisplayDate(DateTime.Now)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1051, oMultilingualAttributeInfo), errorMessage));
            SendEmail(fromEmailId, toEmailIds, mailSubject, oMailBody.ToString(), oCompanyUserInfo);
        }

        private static void SendFailureEmailToUsers(string failureEmailIds, string dataImportType
             , string profileName, string errorMessage, DataImportHdrInfo oDataImportHdrInfoService
            , CompanyUserInfo oCompanyUserInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string toEmailIds = failureEmailIds;
            string errMessage = Helper.FormatFailureMessage(errorMessage);
            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");//AppSettingHelper.GetAppSettingValue(con.EMAIL_FROM_DEFAULT);
            string mailSubject = GetEmailSubject(1752, oDataImportHdrInfoService, oMultilingualAttributeInfo);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} {1}", dataImportType, LanguageUtil.GetValue(1753, oMultilingualAttributeInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1308, oMultilingualAttributeInfo), profileName));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}:{1} ", LanguageUtil.GetValue(1399, oMultilingualAttributeInfo), Helper.GetDisplayDate(DateTime.Now)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1051, oMultilingualAttributeInfo), errorMessage));
            SendEmail(fromEmailId, toEmailIds, mailSubject, oMailBody.ToString(), oCompanyUserInfo);
        }

        public static void SendEmail(string strFromAddress, string strToAddress, string strSubject, string strBody, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                string toAddressTest = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_USE_TEST_ACCOUNT);
                if (!string.IsNullOrEmpty(strToAddress.Trim() + toAddressTest.Trim()))
                {

                    //smtpserver from web.config
                    string smtpServer = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_SMTP_SERVER);
                    //smtpPort from web.config
                    string smtpPort = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_SMTP_PORT);
                    //Network Credentials from web.config
                    string userName = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_USER_NAME);
                    string pwd = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_PASSWORD);


                    bool bEnableSSL = false;
                    if (Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_ENABLE_SSL) != null)
                    {
                        bEnableSSL = Convert.ToBoolean(Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_ENABLE_SSL));
                    }

                    //new instance of smtp client
                    SmtpClient oSmtpClient = new SmtpClient();
                    //Host
                    oSmtpClient.Host = smtpServer;

                    //Port
                    oSmtpClient.Port = Convert.ToInt32(smtpPort);
                    //
                    oSmtpClient.EnableSsl = bEnableSSL;

                    if (!string.IsNullOrEmpty(userName))
                    {
                        //userName and password of network
                        oSmtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);
                    }
                    else
                    {
                        oSmtpClient.UseDefaultCredentials = true;
                    }

                    // new instance of MailMessage           
                    MailMessage mailMessage = new MailMessage();
                    // Sender Address        
                    mailMessage.From = new MailAddress(strFromAddress);
                    // Recepient Address     

                    if (string.IsNullOrEmpty(toAddressTest))
                    {
                        //mailMessage.To.Add(new MailAddress(strToAddress));
                        AddToAddress(strToAddress, mailMessage);
                    }
                    else
                    {
                        // Send to test user
                        AddToAddress(toAddressTest, mailMessage);
                    }
                    Helper.LogInfo("Emails to be sent to: " + mailMessage.To.ToString(), oCompanyUserInfo);
                    // Subject         
                    mailMessage.Subject = strSubject;
                    // Body         
                    mailMessage.Body = strBody;
                    // format of mail message      
                    mailMessage.IsBodyHtml = true;

                    //mail sent
                    oSmtpClient.SendCompleted -= OnAsyncMailSendCompleted;
                    oSmtpClient.SendCompleted += new SendCompletedEventHandler(OnAsyncMailSendCompleted);
                    oSmtpClient.SendAsync(mailMessage, mailMessage);
                }
                else
                    throw new Exception("No email address to send to. Mail could not be sent");
            }
            catch (Exception ex)
            {
                //Helper.LogException(ex);
                throw ex;
            }
        }

        static void OnAsyncMailSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MailMessage msg = (MailMessage)e.UserState;
                Helper.LogError("Error while sending email to " + msg.To.ToString(), null);
                Helper.LogError("Error Message => " + e.Error.Message + "Stack Trace => " + e.Error.StackTrace, null);
            }
        }

        private static void AddToAddress(string strToAddress, MailMessage mailMessage)
        {
            char[] separators = new char[2];
            separators[0] = ',';
            separators[1] = ';';

            string[] toAddressArray = strToAddress.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            // Send to test user
            foreach (var toAddr in toAddressArray)
            {
                if (!string.IsNullOrEmpty(toAddr))
                    mailMessage.To.Add(new MailAddress(toAddr));
            }
        }

        //For Matching 
        public static void SendEmailToUserByMatchingResult(MatchSetHdrInfo oMatchSetHdrInfo, CompanyUserInfo oCompanyUserInfo)
        {
            string errorMessage = FormatFailureMessage(oMatchSetHdrInfo.Message);

            switch (oMatchSetHdrInfo.MatchingStatusID)
            {
                case (short)Enums.MatchingStatus.Success:
                    SendSuccessEmailToUsersForMatchingResult(oMatchSetHdrInfo
                       , ServiceConstants.DEFAULTBUSINESSENTITYID
                       , ServiceConstants.DEFAULTLANGUAGEID
                       , oCompanyUserInfo);
                    break;

                case (short)Enums.MatchingStatus.Error:
                    SendFailureEmailToUsersForMatchingResult(oMatchSetHdrInfo
                       , ServiceConstants.DEFAULTBUSINESSENTITYID
                       , ServiceConstants.DEFAULTLANGUAGEID
                       , errorMessage
                       , oCompanyUserInfo);
                    break;
            }
        }

        private static void SendSuccessEmailToUsersForMatchingResult(MatchSetHdrInfo oMatchSetHdrInfo,
            int businessEntityID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oMatchSetHdrInfo.UserLanguageID, businessEntityID);
            string toEmailIds = oMatchSetHdrInfo.UserEmailId;

            SqlConnection oConnection = null;
            DataTable dtPhrases = null;
            try
            {
                oConnection = Helper.CreateConnection(oCompanyUserInfo);
                dtPhrases = Helper.GetPhrases(GetLableIDDataTable(), businessEntityID,
                                              oMatchSetHdrInfo.UserLanguageID,
                                              defaultLanguageID, oConnection, oCompanyUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
            string emailSubject = string.Format("{0}", GetEmailPhraseByLableID(dtPhrases, 2360, oCompanyUserInfo));
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append(string.Format("{0}.", GetEmailPhraseByLableID(dtPhrases, 2360, oCompanyUserInfo)));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 2186, oCompanyUserInfo), oMatchSetHdrInfo.MatchSetName));
            emailBody.Append("<br>");
            emailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 2361, oCompanyUserInfo), SharedUtility.SharedHelper.GetDisplayDate(DateTime.Now, oMultilingualAttributeInfo)));

            SendEmail(fromEmailId, toEmailIds, emailSubject, emailBody.ToString(), oCompanyUserInfo);
        }

        private static void SendFailureEmailToUsersForMatchingResult(MatchSetHdrInfo oMatchSetHdrInfo,
            int businessEntityID, int defaultLanguageID, string errorMessage, CompanyUserInfo oCompanyUserInfo)
        {

            string toEmailIds = oMatchSetHdrInfo.UserEmailId;
            SqlConnection oConnection = null;
            DataTable dtPhrases = null;
            try
            {
                oConnection = Helper.CreateConnection(oCompanyUserInfo);
                dtPhrases = Helper.GetPhrases(GetLableIDDataTable(), businessEntityID, oMatchSetHdrInfo.UserLanguageID, defaultLanguageID, oConnection, oCompanyUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
            string mailSubject = string.Format("{0}", GetEmailPhraseByLableID(dtPhrases, 2362, oCompanyUserInfo));
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0}", GetEmailPhraseByLableID(dtPhrases, 2362, oCompanyUserInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 2186, oCompanyUserInfo), oMatchSetHdrInfo.MatchSetName));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}:{1} ", GetEmailPhraseByLableID(dtPhrases, 2361, oCompanyUserInfo), Helper.GetDisplayDate(DateTime.Now)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", GetEmailPhraseByLableID(dtPhrases, 2363, oCompanyUserInfo), errorMessage));
            SendEmail(fromEmailId, toEmailIds, mailSubject, oMailBody.ToString(), oCompanyUserInfo);
        }

        public static void SendMailToNewUser(ClientModel.UserHdrInfo oUser, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                StringBuilder oMailBody = new StringBuilder();

                // Create multilingual attribute info
                MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUser.DefaultLanguageID.Value, oUser.CompanyID.Value);

                oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)));
                oMailBody.Append(oUser.FirstName + " " + oUser.LastName);
                oMailBody.Append(",");
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1325, oMultilingualAttributeInfo)));
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1269, oMultilingualAttributeInfo)));
                oMailBody.Append(oUser.LoginID);
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1004, oMultilingualAttributeInfo)));
                oMailBody.Append(oUser.Password);
                oMailBody.Append("<br>");
                oMailBody.Append("<br>");
                String msg;
                msg = LanguageUtil.GetValue(2384, oMultilingualAttributeInfo);

                oMailBody.Append(string.Format(msg, Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_SYSTEM_URL)));

                string fromAddress = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_FROM_DEFAULT);
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromAddress, oUser.CompanyDisplayNameLabelID, oMultilingualAttributeInfo, null));

                string mailSubject = string.Format("{0}: {1}", LanguageUtil.GetValue(1327, oMultilingualAttributeInfo), LanguageUtil.GetValue(1326, oMultilingualAttributeInfo));

                string toAddress = oUser.EmailID;
                MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString(), oCompanyUserInfo);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, oCompanyUserInfo);
            }
        }

        /// <summary>
        /// GetEmailSignature() is used to append signature in mail.
        /// </summary>
        /// <param name="oSignatureEnum"></param>
        /// <returns></returns>
        public static string GetEmailSignature(Enums.SignatureEnum oSignatureEnum, string fromAddress, int? CompanyDisplayNameLabelID, MultilingualAttributeInfo oMultilingualAttributeInfo, int? UserRoleLabelID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<br/><br/>");

            switch (oSignatureEnum)
            {
                case Enums.SignatureEnum.SendBySystemAdmin:
                    //2019 -- Regards
                    //2020 -- administrator
                    //2021 -- Please note: This is an auto-generated email. Please do not reply directly to this email.
                    sb.Append("<br/><b>" + LanguageUtil.GetValue(2019, oMultilingualAttributeInfo) + "</b>");
                    if (UserRoleLabelID.HasValue)
                        sb.Append("<br/><b>" + LanguageUtil.GetValue(UserRoleLabelID.Value, oMultilingualAttributeInfo) + "</b>");
                    else
                        sb.Append("<br/><b>" + LanguageUtil.GetValue(1133, oMultilingualAttributeInfo) + "</b>");
                    if (CompanyDisplayNameLabelID.HasValue)
                        sb.Append("<br/>" + LanguageUtil.GetValue(CompanyDisplayNameLabelID.Value, oMultilingualAttributeInfo));
                    break;

            }

            string fromEmailAddressConfig = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_FROM_DEFAULT);

            if (fromAddress != fromEmailAddressConfig)
            {
                sb.Append("<br/><br/>" + LanguageUtil.GetValue(2042, oMultilingualAttributeInfo));
            }
            else
            {
                sb.Append("<br/><br/>" + LanguageUtil.GetValue(2021, oMultilingualAttributeInfo));
            }

            return sb.ToString();
        }
        private static string GetDearString(ClientModel.UserHdrInfo oUserHdrInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append(LanguageUtil.GetValue(1845, oMultilingualAttributeInfo));
            mailBody.Append(" ");
            mailBody.Append(oUserHdrInfo.Name);
            mailBody.Append("<br>");
            mailBody.Append("<br>");
            return mailBody.ToString();

        }

        private static string GetEmailSubject(int labelID, DataImportHdrInfo oDataImportHdrInfoService, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            return string.Format("{0} ({1})", LanguageUtil.GetValue(labelID, oMultilingualAttributeInfo), SharedUtility.SharedHelper.GetDisplayDate(oDataImportHdrInfoService.PeriodEndDate, oMultilingualAttributeInfo));
        }

        /// <summary>
        /// Get the file name from full path and remove timestamp/suffix added by the application during upload
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetOriginalFileName(string fileName)
        {
            string actFileName = string.Empty;
            int fileNameStartIndex = fileName.LastIndexOf(@"\") + 1;
            int fileNameEndIndex = fileName.LastIndexOf("_");
            if (fileNameStartIndex >= 0 && fileNameEndIndex >= 0)
            {
                actFileName = fileName.Substring(fileNameStartIndex, fileNameEndIndex - fileNameStartIndex);
                int dotIndex = fileName.LastIndexOf(".");
                if (dotIndex >= 0)
                    actFileName += fileName.Substring(dotIndex);
            }
            return actFileName;
        }
        public static void SendEmail(string strFromAddress, string strToAddress, string strSubject, string strBody, List<string> oFilePathCollection, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                string toAddressTest = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_USE_TEST_ACCOUNT);
                if (!string.IsNullOrEmpty(strToAddress.Trim() + toAddressTest.Trim()))
                {

                    //smtpserver from web.config
                    string smtpServer = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_SMTP_SERVER);
                    //smtpPort from web.config
                    string smtpPort = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_SMTP_PORT);
                    //Network Credentials from web.config
                    string userName = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_USER_NAME);
                    string pwd = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_PASSWORD);


                    bool bEnableSSL = false;
                    if (Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_ENABLE_SSL) != null)
                    {
                        bEnableSSL = Convert.ToBoolean(Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_ENABLE_SSL));
                    }

                    //new instance of smtp client
                    SmtpClient oSmtpClient = new SmtpClient();
                    //Host
                    oSmtpClient.Host = smtpServer;

                    //Port
                    oSmtpClient.Port = Convert.ToInt32(smtpPort);
                    //
                    oSmtpClient.EnableSsl = bEnableSSL;

                    if (!string.IsNullOrEmpty(userName))
                    {
                        //userName and password of network
                        oSmtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);
                    }
                    else
                    {
                        oSmtpClient.UseDefaultCredentials = true;
                    }

                    // new instance of MailMessage           
                    MailMessage mailMessage = new MailMessage();
                    // Sender Address        
                    mailMessage.From = new MailAddress(strFromAddress);
                    // Recepient Address     

                    if (string.IsNullOrEmpty(toAddressTest))
                    {
                        //mailMessage.To.Add(new MailAddress(strToAddress));
                        AddToAddress(strToAddress, mailMessage);
                    }
                    else
                    {
                        // Send to test user
                        AddToAddress(toAddressTest, mailMessage);
                    }
                    Helper.LogInfo("Emails to be sent to: " + mailMessage.To.ToString(), oCompanyUserInfo);
                    // Subject         
                    mailMessage.Subject = strSubject;
                    // Body         
                    mailMessage.Body = strBody;
                    // format of mail message      
                    mailMessage.IsBodyHtml = true;
                    // Append Attachment
                    if (oFilePathCollection != null)
                    {
                        for (int i = 0; i < oFilePathCollection.Count; i++)
                        {
                            string fileName = oFilePathCollection[i];
                            Attachment oAttachment = new Attachment(fileName);
                            string actFileName = GetOriginalFileName(fileName);
                            if (!string.IsNullOrEmpty(actFileName))
                            {
                                oAttachment.ContentDisposition.FileName = actFileName;
                            }
                            mailMessage.Attachments.Add(oAttachment);
                        }
                    }
                    // oSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //mail sent
                    oSmtpClient.SendCompleted -= OnAsyncMailSendCompleted;
                    oSmtpClient.SendCompleted += new SendCompletedEventHandler(OnAsyncMailSendCompleted);
                    oSmtpClient.SendAsync(mailMessage, mailMessage);
                }
                else
                    throw new Exception("No email address to send to. Mail could not be sent");
            }
            catch (Exception ex)
            {
                //Helper.LogException(ex);
                throw ex;
            }
        }


    }
}
