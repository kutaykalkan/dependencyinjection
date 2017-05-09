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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.Base;
using SkyStem.Language.LanguageUtility;
using System.Globalization;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.MappingUpload;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.Client.Model.RecControlCheckList;
using SkyStem.ART.Client.Model.Matching;

namespace SkyStem.ART.Web.Utility
{

    /// <summary>
    /// Summary description for LanguageHelper
    /// </summary>
    public class LanguageHelper
    {
        public LanguageHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void LoadLanguagePhrases()
        {
            int defaultLCID = AppSettingHelper.GetDefaultLanguageID();
            int defaultBusinessEntityID = AppSettingHelper.GetDefaultBusinessEntityID();
            int applicationID = AppSettingHelper.GetApplicationID();
            int lcid = SessionHelper.GetUserLanguage();
            int businessEntityID = SessionHelper.GetBusinessEntityID();
            LanguageUtil.LoadPhrases(applicationID, lcid, businessEntityID, defaultLCID, defaultBusinessEntityID);//TODO replace hard coded values
        }



        #region TranslateLabel() for listControls

        public static MultilingualInfo TranslateLabelInfo(MultilingualInfo oMultilingualInfo)
        {
            oMultilingualInfo.Name = LanguageUtil.GetValue(oMultilingualInfo.LabelID.Value);
            return oMultilingualInfo;
        }

        public static IList<MaterialityTypeMstInfo> TranslateLabelMaterialityTypeMstInfo(IList<MaterialityTypeMstInfo> lstMaterialityTypeMstInfo)
        {
            for (int i = 0; i < lstMaterialityTypeMstInfo.Count; i++)
            {
                lstMaterialityTypeMstInfo[i].Name = LanguageUtil.GetValue(lstMaterialityTypeMstInfo[i].LabelID.Value);
            }
            return lstMaterialityTypeMstInfo;
        }
        public static IList<DualLevelReviewTypeMstInfo> TranslateLabelDualLevelReviewTypeMstInfo(IList<DualLevelReviewTypeMstInfo> lstDualLevelReviewTypeMstInfo)
        {
            for (int i = 0; i < lstDualLevelReviewTypeMstInfo.Count; i++)
            {
                lstDualLevelReviewTypeMstInfo[i].DualLevelReviewType = LanguageUtil.GetValue(lstDualLevelReviewTypeMstInfo[i].DualLevelReviewTypeLabelID.Value);
            }
            return lstDualLevelReviewTypeMstInfo;
        }
        public static IList<FSCaptionInfo_ExtendedWithMaterialityInfo> TranslateLabelFSCaptionInfo(IList<FSCaptionInfo_ExtendedWithMaterialityInfo> oFSCaptionInfoCollection)
        {
            for (int i = 0; i < oFSCaptionInfoCollection.Count; i++)
            {
                oFSCaptionInfoCollection[i].Name = LanguageUtil.GetValue(oFSCaptionInfoCollection[i].LabelID.Value);
            }
            return oFSCaptionInfoCollection;
        }

        public static List<RiskRatingMstInfo> TranslateLabelRiskRatingMstInfo(List<RiskRatingMstInfo> lstRiskRatingMstInfo)
        {
            for (int i = 0; i < lstRiskRatingMstInfo.Count; i++)
            {
                lstRiskRatingMstInfo[i].Name = LanguageUtil.GetValue(lstRiskRatingMstInfo[i].LabelID.Value);
            }
            return lstRiskRatingMstInfo;
        }

        public static IList<ReconciliationFrequencyMstInfo> TranslateLabelReconciliationFrequencyMstInfo(IList<ReconciliationFrequencyMstInfo> lstReconciliationFrequencyMstInfo)
        {
            for (int i = 0; i < lstReconciliationFrequencyMstInfo.Count; i++)
            {
                lstReconciliationFrequencyMstInfo[i].Name = LanguageUtil.GetValue(lstReconciliationFrequencyMstInfo[i].LabelID.Value);
            }
            return lstReconciliationFrequencyMstInfo;
        }

        public static IList<WeekDayMstInfo> TranslateLabeloWeekDayMstInfo(IList<WeekDayMstInfo> lstWeekDayMstInfo)
        {
            for (int i = 0; i < lstWeekDayMstInfo.Count; i++)
            {
                lstWeekDayMstInfo[i].WeekDayName = LanguageUtil.GetValue(lstWeekDayMstInfo[i].WeekDayNameLabelID.Value);
            }
            return lstWeekDayMstInfo;
        }

        public static IList<AttributeConfigMstInfo> TranslateLabelRoleConfigMstInfo(IList<AttributeConfigMstInfo> lstRoleConfigMstInfo)
        {
            for (int i = 0; i < lstRoleConfigMstInfo.Count; i++)
            {
                lstRoleConfigMstInfo[i].RoleConfigName = LanguageUtil.GetValue(lstRoleConfigMstInfo[i].RoleConfigNameLabelID.Value);
            }
            return lstRoleConfigMstInfo;
        }
        public static List<UserRoleInfo> TranslateLabelUserRoleInfo(List<UserRoleInfo> lstUserRoleInfo)
        {
            for (int i = 0; i < lstUserRoleInfo.Count; i++)
            {
                if (lstUserRoleInfo[i].RoleLabelID.HasValue)
                    lstUserRoleInfo[i].Role = LanguageUtil.GetValue(lstUserRoleInfo[i].RoleLabelID.Value);
            }
            return lstUserRoleInfo;
        }
        public static List<UserStatusDetailInfo> TranslateLabelUserStatusDetailInfo(List<UserStatusDetailInfo> lstUserStatusDetailInfo)
        {
            for (int i = 0; i < lstUserStatusDetailInfo.Count; i++)
            {
                if (lstUserStatusDetailInfo[i].UserStatusLabelID.HasValue)
                    lstUserStatusDetailInfo[i].UserStatus = LanguageUtil.GetValue(lstUserStatusDetailInfo[i].UserStatusLabelID.Value);
            }
            return lstUserStatusDetailInfo;
        }
        public static List<UserHdrInfo> TranslateLabelUserStatus(List<UserHdrInfo> lstUserHdrInfo)
        {
            for (int i = 0; i < lstUserHdrInfo.Count; i++)
            {
                if (lstUserHdrInfo[i].UserStatusLabelID.HasValue)
                    lstUserHdrInfo[i].UserStatus = LanguageUtil.GetValue(lstUserHdrInfo[i].UserStatusLabelID.Value);
            }
            return lstUserHdrInfo;
        }


        public static IList<MappingUploadMasterInfo> TranslateLabeloMappingUploadMstInfo(IList<MappingUploadMasterInfo> lstMappingUploadMstInfo)
        {
            for (int i = 0; i < lstMappingUploadMstInfo.Count; i++)
            {
                lstMappingUploadMstInfo[i].AccountMappingKeyName = LanguageUtil.GetValue(lstMappingUploadMstInfo[i].AccountMappingKeyNameLabelID.Value);
            }
            return lstMappingUploadMstInfo;
        }

        public static List<MultilingualInfo> TranslateLabelGeneric(List<MultilingualInfo> lstMultilingualInfo)
        {
            for (int i = 0; i < lstMultilingualInfo.Count; i++)
            {
                lstMultilingualInfo[i].Name = LanguageUtil.GetValue(lstMultilingualInfo[i].LabelID.Value);
            }
            return lstMultilingualInfo;
        }
        public static IList<DataImportTypeMstInfo> TranslateLabelDataImportTypeMstInfo(IList<DataImportTypeMstInfo> oDataImportTypeMstInfoCollection)
        {
            for (int i = 0; i < oDataImportTypeMstInfoCollection.Count; i++)
            {
                oDataImportTypeMstInfoCollection[i].Name = LanguageUtil.GetValue(oDataImportTypeMstInfoCollection[i].LabelID.Value);
            }
            return oDataImportTypeMstInfoCollection;
        }

        /// <summary>
        /// Translate the Role Collection
        /// </summary>
        /// <param name="oRoleMstInfoCollection"></param>
        /// <returns></returns>
        public static List<RoleMstInfo> TranslateRoleCollection(List<RoleMstInfo> oRoleMstInfoCollection)
        {
            for (int i = 0; i < oRoleMstInfoCollection.Count; i++)
            {
                oRoleMstInfoCollection[i].Name = LanguageUtil.GetValue(oRoleMstInfoCollection[i].LabelID.Value);
            }
            return oRoleMstInfoCollection;
        }


        internal static List<GeographyClassMstInfo> TranslateKeysCollection(List<GeographyClassMstInfo> oGeographyClassMstInfoCollection)
        {
            for (int i = 0; i < oGeographyClassMstInfoCollection.Count; i++)
            {
                oGeographyClassMstInfoCollection[i].Name = LanguageUtil.GetValue(oGeographyClassMstInfoCollection[i].LabelID.Value);
            }
            return oGeographyClassMstInfoCollection;
        }


        #endregion

        /// <summary>
        /// Translates Account type lable id for current language and fills it in 
        /// Name property of Account type object
        /// </summary>
        /// <param name="oAccountTypeMstInfoCollection">List of Account type objects</param>
        /// <returns>List of Account type object containing diaplay text</returns>
        public static List<AccountTypeMstInfo> TranslateLabelAccountType(List<AccountTypeMstInfo> oAccountTypeMstInfoCollection)
        {
            foreach (AccountTypeMstInfo oAccountTypeMstInfo in oAccountTypeMstInfoCollection)
            {
                if (oAccountTypeMstInfo.AccountTypeLabelID.HasValue)
                {
                    oAccountTypeMstInfo.Name = LanguageUtil.GetValue(oAccountTypeMstInfo.AccountTypeLabelID.Value);
                    oAccountTypeMstInfo.AccountType = oAccountTypeMstInfo.Name;
                }
                else
                    oAccountTypeMstInfo.Name = oAccountTypeMstInfo.AccountType;
            }

            return oAccountTypeMstInfoCollection;
        }

        /// <summary>
        /// Translates Subledger Source lable id for current language and fills it in 
        /// Name property of Subledger Source object
        /// </summary>
        /// <param name="oSubledgerSourceInfoCollection">List of Subledger Source objects</param>
        /// <returns>List of Subledger Source object containing diaplay text</returns>
        public static List<SubledgerSourceInfo> TranslateLabelSubledgerSource(List<SubledgerSourceInfo> oSubledgerSourceInfoCollection)
        {
            foreach (SubledgerSourceInfo oSubledgerSourceInfo in oSubledgerSourceInfoCollection)
            {
                if (oSubledgerSourceInfo.SubledgerSourceLabelID.HasValue)
                    oSubledgerSourceInfo.Name = LanguageUtil.GetValue(oSubledgerSourceInfo.SubledgerSourceLabelID.Value);
                else
                    oSubledgerSourceInfo.Name = oSubledgerSourceInfo.SubledgerSource;
            }

            return oSubledgerSourceInfoCollection;
        }

        /// <summary>
        /// Translate System Lockdown Reason Mst
        /// </summary>
        /// <param name="oSystemLockdownReasonMstInfoList"></param>
        /// <returns></returns>
        public static List<SystemLockdownReasonMstInfo> TranslateLabelSystemLockdownReasons(List<SystemLockdownReasonMstInfo> oSystemLockdownReasonMstInfoList)
        {
            foreach (SystemLockdownReasonMstInfo oSystemLockdownReasonMstInfo in oSystemLockdownReasonMstInfoList)
            {
                if (oSystemLockdownReasonMstInfo.DescriptionLabelID.HasValue)
                    oSystemLockdownReasonMstInfo.Description = LanguageUtil.GetValue(oSystemLockdownReasonMstInfo.DescriptionLabelID.Value);
            }
            return oSystemLockdownReasonMstInfoList;
        }

        /// <summary>
        /// Translates Reconciliation Templates lable id for current language and fills it in 
        /// Name property of Subledger Source object
        /// </summary>
        /// <param name="oReconciliationTemplateMstInfoCollection">List of Reconciliation Templates objects</param>
        /// <returns>List of Reconciliation Templates object containing diaplay text</returns>
        public static List<ReconciliationTemplateMstInfo> TranslateLabelReconciliationTemplates(List<ReconciliationTemplateMstInfo> oReconciliationTemplateMstInfoCollection)
        {
            foreach (ReconciliationTemplateMstInfo oReconciliationTemplateMstInfo in oReconciliationTemplateMstInfoCollection)
            {
                if (oReconciliationTemplateMstInfo.ReconciliationTemplateLabelID.HasValue)
                    oReconciliationTemplateMstInfo.Name = LanguageUtil.GetValue(oReconciliationTemplateMstInfo.ReconciliationTemplateLabelID.Value);
                else
                    oReconciliationTemplateMstInfo.Name = oReconciliationTemplateMstInfo.ReconciliationTemplate;
            }

            return oReconciliationTemplateMstInfoCollection;
        }

        /// <summary>
        /// Translates Geography Structure lable id for current language and fills it in 
        /// Name property of Subledger Source object
        /// </summary>
        /// <param name="oGeographyStructureHdrInfoCollection">List of Geography Structure objects</param>
        /// <returns>List of Geography Structure object containing diaplay text</returns>
        public static List<GeographyStructureHdrInfo> TranslateLabelGeographyStructure(List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection)
        {
            foreach (GeographyStructureHdrInfo oGeographyStructureHdrInfo in oGeographyStructureHdrInfoCollection)
            {
                if (oGeographyStructureHdrInfo.GeographyStructureLabelID.HasValue)
                    oGeographyStructureHdrInfo.Name = LanguageUtil.GetValue(oGeographyStructureHdrInfo.GeographyStructureLabelID.Value);
                else
                    oGeographyStructureHdrInfo.Name = oGeographyStructureHdrInfo.GeographyStructure;
            }

            return oGeographyStructureHdrInfoCollection;
        }

        /// <summary>
        /// Translates Account Attribute lable id for current language and fills it in 
        /// Name property of Account Attribute object
        /// </summary>
        /// <param name="oAccountAttributeMstInfoCollection">List of Account Attribute objects</param>
        /// <returns>List of Account Attribute object containing diaplay text</returns>
        public static List<AccountAttributeMstInfo> TranslateLabelAccountAttribute(List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection)
        {
            foreach (AccountAttributeMstInfo oAccountAttributeMstInfo in oAccountAttributeMstInfoCollection)
            {
                if (oAccountAttributeMstInfo.AccountAttributeLabelID.HasValue)
                    oAccountAttributeMstInfo.Name = LanguageUtil.GetValue(oAccountAttributeMstInfo.AccountAttributeLabelID.Value);
                else
                    oAccountAttributeMstInfo.Name = oAccountAttributeMstInfo.AccountAttribute;
            }

            return oAccountAttributeMstInfoCollection;
        }

        public static List<GLDataHdrInfo> TranslateLabelsGLData(List<GLDataHdrInfo> oGLDataHdrInfoCollection)
        {
            foreach (GLDataHdrInfo oGLDataHdrInfo in oGLDataHdrInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oGLDataHdrInfo);

                if (!oGLDataHdrInfo.IsAccountNameLabelIDNull)
                {
                    oGLDataHdrInfo.AccountName = LanguageUtil.GetValue(oGLDataHdrInfo.AccountNameLabelID.Value);
                }
                else
                {
                    oGLDataHdrInfo.AccountName = "-";
                }


                if (oGLDataHdrInfo.NetAccountID != null)
                {
                    oGLDataHdrInfo.AccountNumber = LanguageUtil.GetValue(1257);
                    if (oGLDataHdrInfo.NetAccountLabelID.HasValue)
                    {
                        oGLDataHdrInfo.AccountName = LanguageUtil.GetValue(oGLDataHdrInfo.NetAccountLabelID.Value);
                    }
                }



                if (!oGLDataHdrInfo.IsApproverUserIDNull)
                {
                    oGLDataHdrInfo.ApproverFullName = (from user in CacheHelper.SelectAllApproversForCurrentCompany()
                                                       where user.UserID == oGLDataHdrInfo.ApproverUserID
                                                       select user.Name).FirstOrDefault();

                    if (string.IsNullOrEmpty(oGLDataHdrInfo.ApproverFullName))
                    {
                        oGLDataHdrInfo.ApproverFullName = "-";
                    }
                }
                else
                {
                    oGLDataHdrInfo.ApproverFullName = "-";
                }


                //For backup Approver
                if (!oGLDataHdrInfo.IsBackupApproverUserIDNull)
                {
                    oGLDataHdrInfo.BackupApproverFullName = (from user in CacheHelper.SelectAllBackupApproversForCurrentCompany()
                                                             where user.UserID == oGLDataHdrInfo.BackupApproverUserID
                                                             select user.Name).FirstOrDefault();

                    if (string.IsNullOrEmpty(oGLDataHdrInfo.BackupApproverFullName))
                    {
                        oGLDataHdrInfo.BackupApproverFullName = "-";
                    }
                }
                else
                {
                    oGLDataHdrInfo.BackupApproverFullName = "-";
                }



                if (!oGLDataHdrInfo.IsCertificationStatusLabelIDNull)
                {
                    oGLDataHdrInfo.CertificationStatus = LanguageUtil.GetValue(oGLDataHdrInfo.CertificationStatusLabelID.Value);
                }
                else
                {
                    oGLDataHdrInfo.CertificationStatus = "-";
                }

                if (!oGLDataHdrInfo.IsKeyAccountNull)
                {
                    if (oGLDataHdrInfo.IsKeyAccount.Value)
                    {
                        oGLDataHdrInfo.KeyAccount = LanguageUtil.GetValue(1252);
                    }
                    else
                    {
                        oGLDataHdrInfo.KeyAccount = LanguageUtil.GetValue(1251);
                    }
                }
                else
                {
                    oGLDataHdrInfo.KeyAccount = "-";
                }

                if (oGLDataHdrInfo.IsSystemReconcilied != null)
                {
                    if (oGLDataHdrInfo.IsSystemReconcilied.Value)
                    {
                        oGLDataHdrInfo.SystemReconciled = LanguageUtil.GetValue(1252);
                    }
                    else
                    {
                        oGLDataHdrInfo.SystemReconciled = LanguageUtil.GetValue(1251);
                    }
                }
                else
                {
                    oGLDataHdrInfo.SystemReconciled = "-";
                }

                if (!oGLDataHdrInfo.IsZeroBalanceNull)
                {
                    if (oGLDataHdrInfo.IsZeroBalance.Value)
                    {
                        oGLDataHdrInfo.ZeroBalance = LanguageUtil.GetValue(1252);
                    }
                    else
                    {
                        oGLDataHdrInfo.ZeroBalance = LanguageUtil.GetValue(1251);
                    }
                }
                else
                {
                    oGLDataHdrInfo.ZeroBalance = "-";
                }

                // we are returning AccountMateriality phrase from sp itself
                //if (!oGLDataHdrInfo.IsMaterialityNull)
                //{
                //    if (oGLDataHdrInfo.Materiality.Value)
                //    {
                //        oGLDataHdrInfo.AccountMateriality = LanguageUtil.GetValue(1252);
                //    }
                //    else
                //    {
                //        oGLDataHdrInfo.AccountMateriality = LanguageUtil.GetValue(1251);
                //    }
                //}
                //else
                //{
                //    oGLDataHdrInfo.AccountMateriality = "-";
                //}





                //if (!oGLDataHdrInfo.IsNetAccountLabelIDNull)
                //{
                //    oGLDataHdrInfo.NetAccount = LanguageUtil.GetValue(oGLDataHdrInfo.NetAccountLabelID.Value);
                //}

                if (!oGLDataHdrInfo.IsPreparerUserIDNull)
                {
                    oGLDataHdrInfo.PreparerFullName = (from user in CacheHelper.SelectAllPreparersForCurrentCompany()
                                                       where user.UserID == oGLDataHdrInfo.PreparerUserID
                                                       select user.Name).FirstOrDefault();

                    if (string.IsNullOrEmpty(oGLDataHdrInfo.PreparerFullName))
                    {
                        oGLDataHdrInfo.PreparerFullName = "-";
                    }
                }
                else
                {
                    oGLDataHdrInfo.PreparerFullName = "-";
                }

                //For Backup Preparer
                if (!oGLDataHdrInfo.IsBackupPreparerUserIDNull)
                {
                    oGLDataHdrInfo.BackupPreparerFullName = (from user in CacheHelper.SelectAllBackupPreparersForCurrentCompany()
                                                             where user.UserID == oGLDataHdrInfo.BackupPreparerUserID
                                                             select user.Name).FirstOrDefault();

                    if (string.IsNullOrEmpty(oGLDataHdrInfo.BackupPreparerFullName))
                    {
                        oGLDataHdrInfo.BackupPreparerFullName = "-";
                    }
                }
                else
                {
                    oGLDataHdrInfo.BackupPreparerFullName = "-";
                }

                if (!oGLDataHdrInfo.IsReconciliationStatusLabelIDNull)
                {
                    oGLDataHdrInfo.ReconciliationStatus = LanguageUtil.GetValue(oGLDataHdrInfo.ReconciliationStatusLabelID.Value);
                }
                else
                {
                    oGLDataHdrInfo.ReconciliationStatus = "-";
                }

                if (!oGLDataHdrInfo.IsReviewerUserIDNull)
                {
                    oGLDataHdrInfo.ReviewerFullName = (from user in CacheHelper.SelectAllReviewersForCurrentCompany()
                                                       where user.UserID == oGLDataHdrInfo.ReviewerUserID
                                                       select user.Name).FirstOrDefault();

                    if (string.IsNullOrEmpty(oGLDataHdrInfo.ReviewerFullName))
                        oGLDataHdrInfo.ReviewerFullName = "-";
                }
                else
                {
                    oGLDataHdrInfo.ReviewerFullName = "-";
                }

                //For Backup Reviewer 
                if (!oGLDataHdrInfo.IsBackupReviewerUserIDNull)
                {
                    oGLDataHdrInfo.BackupReviewerFullName = (from user in CacheHelper.SelectAllBackupReviewersForCurrentCompany()
                                                             where user.UserID == oGLDataHdrInfo.BackupReviewerUserID
                                                             select user.Name).FirstOrDefault();

                    if (string.IsNullOrEmpty(oGLDataHdrInfo.BackupReviewerFullName))
                        oGLDataHdrInfo.BackupReviewerFullName = "-";
                }
                else
                {
                    oGLDataHdrInfo.BackupReviewerFullName = "-";
                }

                if (!oGLDataHdrInfo.IsRiskRatingIDNull)
                {
                    oGLDataHdrInfo.RiskRating = (from riskRating in SessionHelper.GetAllRiskRating()
                                                 where riskRating.RiskRatingID == oGLDataHdrInfo.RiskRatingID.Value
                                                 select riskRating.Name).FirstOrDefault();

                    if (string.IsNullOrEmpty(oGLDataHdrInfo.RiskRating))
                        oGLDataHdrInfo.RiskRating = "-";
                }
                else
                {
                    oGLDataHdrInfo.RiskRating = "-";
                }

                if (oGLDataHdrInfo.FSCaptionLabelID.HasValue)
                {
                    oGLDataHdrInfo.FSCaption = LanguageUtil.GetValue(oGLDataHdrInfo.FSCaptionLabelID.Value);
                }
                else
                {
                    oGLDataHdrInfo.FSCaption = "-";
                }

                if (oGLDataHdrInfo.AccountTypeID.HasValue && oGLDataHdrInfo.AccountTypeID.Value > 0)
                {
                    oGLDataHdrInfo.AccountType = (from accType in SessionHelper.SelectAllAccountTypeMstInfoWithDisplayText()
                                                  where accType.AccountTypeID == oGLDataHdrInfo.AccountTypeID.Value
                                                  select accType.Name).FirstOrDefault();
                }
                else if (oGLDataHdrInfo.AccountTypeLabelID.HasValue)
                {
                    oGLDataHdrInfo.AccountType = LanguageUtil.GetValue(oGLDataHdrInfo.AccountTypeLabelID.Value);
                }
                else
                {
                    oGLDataHdrInfo.AccountType = "-";
                }
            }

            return oGLDataHdrInfoCollection;
        }

        public static void TranslateLabelDataImportHdr(DataImportHdrInfo oDataImportHdrInfo)
        {
            if (oDataImportHdrInfo != null)
            {
                if (oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
                {
                    foreach (DataImportMessageDetailInfo oDataImportMessageDetailInfo in oDataImportHdrInfo.DataImportMessageDetailInfoList)
                    {
                        if (oDataImportMessageDetailInfo.DescriptionLabelID.HasValue)
                            oDataImportMessageDetailInfo.Description = LanguageUtil.GetValue(oDataImportMessageDetailInfo.DescriptionLabelID.Value);
                        if (oDataImportMessageDetailInfo.DataImportMessageLabelID.HasValue)
                            oDataImportMessageDetailInfo.DataImportMessage = LanguageUtil.GetValue(oDataImportMessageDetailInfo.DataImportMessageLabelID.Value);
                    }
                }
                if (oDataImportHdrInfo.DataImportAccountMessageDetailInfoList != null && oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Count > 0)
                {
                    foreach (DataImportMessageDetailInfo oDataImportMessageDetailInfo in oDataImportHdrInfo.DataImportAccountMessageDetailInfoList)
                    {
                        if (oDataImportMessageDetailInfo.DescriptionLabelID.HasValue)
                            oDataImportMessageDetailInfo.Description = LanguageUtil.GetValue(oDataImportMessageDetailInfo.DescriptionLabelID.Value);
                        if (oDataImportMessageDetailInfo.DataImportMessageLabelID.HasValue)
                            oDataImportMessageDetailInfo.DataImportMessage = LanguageUtil.GetValue(oDataImportMessageDetailInfo.DataImportMessageLabelID.Value);
                        if (oDataImportMessageDetailInfo.AccountInfo != null)
                            TranslateLabelsAccountHdr(oDataImportMessageDetailInfo.AccountInfo);
                    }
                }
            }
        }

        public static List<AccountHdrInfo> TranslateLabelsAccountHdr(List<AccountHdrInfo> oAccountHdrInfoCollection)
        {
            foreach (AccountHdrInfo oAccountHdrInfo in oAccountHdrInfoCollection)
            {
                TranslateLabelsAccountHdr(oAccountHdrInfo);
            }
            return oAccountHdrInfoCollection;
        }

        public static void TranslateLabelsAccountHdr(AccountHdrInfo oAccountHdrInfo)
        {
            TranslateLabelOrganizationalHierarchyInfo(oAccountHdrInfo);

            if (oAccountHdrInfo.AccountNameLabelID.HasValue)
            {
                oAccountHdrInfo.AccountName = LanguageUtil.GetValue(oAccountHdrInfo.AccountNameLabelID.Value);
            }

            if (oAccountHdrInfo.DescriptionLabelID.HasValue && oAccountHdrInfo.DescriptionLabelID.Value > 0)
            {
                oAccountHdrInfo.Description = LanguageUtil.GetValue(oAccountHdrInfo.DescriptionLabelID.Value);
            }

            if (oAccountHdrInfo.NatureOfAccountLabelID.HasValue)
            {
                oAccountHdrInfo.NatureOfAccount = LanguageUtil.GetValue(oAccountHdrInfo.NatureOfAccountLabelID.Value);
            }

            if (oAccountHdrInfo.NetAccountLabelID.HasValue && oAccountHdrInfo.NetAccountLabelID.Value > 0)
            {
                oAccountHdrInfo.NetAccount = LanguageUtil.GetValue(oAccountHdrInfo.NetAccountLabelID.Value);
            }

            if (oAccountHdrInfo.ReconciliationProcedureLabelID.HasValue && oAccountHdrInfo.ReconciliationProcedureLabelID.Value > 0)
            {
                oAccountHdrInfo.ReconciliationProcedure = LanguageUtil.GetValue(oAccountHdrInfo.ReconciliationProcedureLabelID.Value);
            }

            if (oAccountHdrInfo.AccountPolicyUrlLabelID.HasValue && oAccountHdrInfo.AccountPolicyUrlLabelID.Value > 0)
            {
                oAccountHdrInfo.AccountPolicyUrl = LanguageUtil.GetValue(oAccountHdrInfo.AccountPolicyUrlLabelID.Value);
            }

            if (oAccountHdrInfo.IsKeyAccount.HasValue)
            {
                if (oAccountHdrInfo.IsKeyAccount.Value)
                {
                    oAccountHdrInfo.KeyAccount = LanguageUtil.GetValue(1252);
                }
                else
                {
                    oAccountHdrInfo.KeyAccount = LanguageUtil.GetValue(1251);
                }
            }

            if (oAccountHdrInfo.IsZeroBalance.HasValue)
            {
                if (oAccountHdrInfo.IsZeroBalance.Value)
                {
                    oAccountHdrInfo.ZeroBalance = LanguageUtil.GetValue(1252);
                }
                else
                {
                    oAccountHdrInfo.ZeroBalance = LanguageUtil.GetValue(1251);
                }
            }
            //Reconcilable
            if (oAccountHdrInfo.IsReconcilable.HasValue)
            {
                if (oAccountHdrInfo.IsReconcilable.Value)
                {
                    oAccountHdrInfo.Reconcilable = LanguageUtil.GetValue(1252);
                }
                else
                {
                    oAccountHdrInfo.Reconcilable = LanguageUtil.GetValue(1251);
                }
            }

            if (oAccountHdrInfo.RiskRatingID.HasValue && oAccountHdrInfo.RiskRatingID.Value > 0)
            {
                oAccountHdrInfo.RiskRating = (from riskRating in SessionHelper.GetAllRiskRating()
                                              where riskRating.RiskRatingID == oAccountHdrInfo.RiskRatingID.Value
                                              select riskRating.Name).FirstOrDefault();
            }
            else
            {
                oAccountHdrInfo.RiskRating = "-";
            }

            if (oAccountHdrInfo.ReconciliationTemplateID.HasValue && oAccountHdrInfo.ReconciliationTemplateID.Value > 0)
            {
                oAccountHdrInfo.ReconciliationTemplate = (from recTemplate in SessionHelper.SelectAllReconciliationTemplateMstInfoWithDisplayText()
                                                          where recTemplate.ReconciliationTemplateID == oAccountHdrInfo.ReconciliationTemplateID.Value
                                                          select recTemplate.Name).FirstOrDefault();
            }
            else
            {
                oAccountHdrInfo.ReconciliationTemplate = "-";
            }

            if (oAccountHdrInfo.SubLedgerSourceID.HasValue && oAccountHdrInfo.SubLedgerSourceID.Value > 0)
            {
                oAccountHdrInfo.SubLedgerSource = (from subLedger in SessionHelper.GetAllSubLedgerSources()
                                                   where subLedger.SubledgerSourceID == oAccountHdrInfo.SubLedgerSourceID.Value
                                                   select subLedger.Name).FirstOrDefault();
            }

            if (oAccountHdrInfo.AccountTypeLabelID.HasValue && oAccountHdrInfo.AccountTypeLabelID.Value > 0)
            {
                oAccountHdrInfo.AccountType = LanguageUtil.GetValue(oAccountHdrInfo.AccountTypeLabelID.Value);
            }
            else if (string.IsNullOrEmpty(oAccountHdrInfo.AccountType))
            {
                oAccountHdrInfo.AccountType = "-";
            }
            oAccountHdrInfo.PreparerFullName = Helper.GetPreparerNameByID(oAccountHdrInfo.PreparerUserID);
            oAccountHdrInfo.ReviewerFullName = Helper.GetReviewerNameByID(oAccountHdrInfo.ReviewerUserID);
            oAccountHdrInfo.ApproverFullName = Helper.GetApproverNameByID(oAccountHdrInfo.ApproverUserID);

            oAccountHdrInfo.BackupPreparerFullName = Helper.GetBackupPreparerNameByID(oAccountHdrInfo.BackupPreparerUserID);
            oAccountHdrInfo.BackupReviewerFullName = Helper.GetBackupReviewerNameByID(oAccountHdrInfo.BackupReviewerUserID);
            oAccountHdrInfo.BackupApproverFullName = Helper.GetBackupApproverNameByID(oAccountHdrInfo.BackupApproverUserID);
            //if (oAccountHdrInfo.DayTypeID.HasValue && oAccountHdrInfo.DayTypeID.Value > 0)
            //{
            //    oAccountHdrInfo.DayType = (from dayType in SessionHelper.GetAllDaysType()
            //                               where dayType.DaysTypeID == oAccountHdrInfo.DayTypeID.Value
            //                               select dayType.DaysType).FirstOrDefault();
            //}
            //else
            //    oAccountHdrInfo.DayType = "-";
        }

        public static void TranslateLabelOrganizationalHierarchyInfo(OrganizationalHierarchyInfo oOrganizationalHierarchyInfo)
        {
            if (oOrganizationalHierarchyInfo.FSCaptionLabelID.HasValue && oOrganizationalHierarchyInfo.FSCaptionLabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.FSCaption = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.FSCaptionLabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key1LabelID.HasValue && oOrganizationalHierarchyInfo.Key1LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key1 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key1LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key2LabelID.HasValue && oOrganizationalHierarchyInfo.Key2LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key2 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key2LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key3LabelID.HasValue && oOrganizationalHierarchyInfo.Key3LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key3 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key3LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key4LabelID.HasValue && oOrganizationalHierarchyInfo.Key4LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key4 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key4LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key5LabelID.HasValue && oOrganizationalHierarchyInfo.Key5LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key5 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key5LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key6LabelID.HasValue && oOrganizationalHierarchyInfo.Key6LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key6 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key6LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key7LabelID.HasValue && oOrganizationalHierarchyInfo.Key7LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key7 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key7LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key8LabelID.HasValue && oOrganizationalHierarchyInfo.Key8LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key8 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key8LabelID.Value).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key9LabelID.HasValue && oOrganizationalHierarchyInfo.Key9LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key9 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key9LabelID.Value).Trim();
            }
        }

        /// <summary>
        /// Function to Set Current Culture on the UI thread
        /// </summary>
        public static void SetCurrentCulture()
        {
            // Set the Language for the Current Thread
            int lcid = SessionHelper.GetUserLanguage();
            CultureInfo oCurrentCultureInfo = new CultureInfo(lcid);
            System.Threading.Thread.CurrentThread.CurrentCulture = oCurrentCultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = oCurrentCultureInfo;
        }

        /// <summary>
        /// Sets the current culture and load phrases.
        /// </summary>
        public static void SetCurrentCultureAndLoadPhrases()
        {
            SetCurrentCulture();
            int lcid = System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
            int businessEntityID = AppSettingHelper.GetDefaultBusinessEntityID();
            if (SessionHelper.CurrentCompanyID.HasValue && SessionHelper.CurrentCompanyID.Value > 0)
            {
                businessEntityID = SessionHelper.CurrentCompanyID.Value;
                LanguageUtil.SetBusinessEntityPhrasesDictionaryToNull(businessEntityID);
                LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), lcid, businessEntityID, AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());
                // Load all available Phrases based on the Language Settings 
                LanguageHelper.LoadLanguagePhrases();
            }
            else if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SKYSTEM_ADMIN)
            {
                LanguageUtil.SetBusinessEntityPhrasesDictionaryToNull(businessEntityID);
                LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), lcid, businessEntityID, AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());
                // Load all available Phrases based on the Language Settings 
                LanguageHelper.LoadLanguagePhrases();
            }
        }

        /// <summary>
        /// Set Language and Load Phrases
        /// </summary>
        /// <param name="lcid"></param>
        public static void SetLanguageAndLoadPhrases(int lcid)
        {
            // Clear the translated master data from session
            SessionHelper.ClearMasterDataFromSession();

            // Get the User/Browser Language and store in Session
            if (lcid == 0)
                lcid = System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
            SessionHelper.SetUserLanguage(lcid);
            // Check for Test LCID, if set
            LanguageHelper.SetTestLanguage();

            // Set Current Culture and Load Phrases
            LanguageHelper.SetCurrentCultureAndLoadPhrases();
        }

        /// <summary>
        /// Gets the multilingual attribute info.
        /// </summary>
        /// <param name="lcid">The lcid.</param>
        /// <returns></returns>
        public static MultilingualAttributeInfo GetMultilingualAttributeInfo(int? companyID, int? lcid)
        {
            MultilingualAttributeInfo oMultilingualAttributeInfo = new MultilingualAttributeInfo();
            int businessEntityID = AppSettingHelper.GetDefaultBusinessEntityID();
            if (companyID.HasValue)
            {
                businessEntityID = companyID.Value;
            }
            oMultilingualAttributeInfo.ApplicationID = AppSettingHelper.GetApplicationID();
            if (lcid.HasValue)
                oMultilingualAttributeInfo.LanguageID = lcid.Value;
            else
                oMultilingualAttributeInfo.LanguageID = System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
            oMultilingualAttributeInfo.BusinessEntityID = businessEntityID;
            oMultilingualAttributeInfo.DefaultLanguageID = AppSettingHelper.GetDefaultLanguageID();
            oMultilingualAttributeInfo.DefaultBusinessEntityID = AppSettingHelper.GetDefaultBusinessEntityID();
            return oMultilingualAttributeInfo;
        }

        public static void TranslateOrganizationalHierarchy(List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection)
        {
            for (int i = 0; i < oGeographyStructureHdrInfoCollection.Count; i++)
            {
                oGeographyStructureHdrInfoCollection[i].GeographyStructure = LanguageUtil.GetValue(oGeographyStructureHdrInfoCollection[i].GeographyStructureLabelID.Value);
            }
        }

        /// <summary>
        /// Translate the Companies Collection for Display Name 
        /// </summary>
        /// <param name="oCompanyHdrInfoCollection"></param>
        /// <returns></returns>
        public static void TranslateCompaniesCollection(List<CompanyHdrInfo> oCompanyHdrInfoCollection)
        {
            for (int i = 0; i < oCompanyHdrInfoCollection.Count; i++)
            {
                TranslateCompanyInfo(oCompanyHdrInfoCollection[i]);
            }
        }
        /// <summary>
        /// Translate Company Info
        /// </summary>
        /// <param name="oCompanyHdrInfo"></param>
        public static void TranslateCompanyInfo(CompanyHdrInfo oCompanyHdrInfo)
        {
            if (oCompanyHdrInfo.DisplayNameLabelID.HasValue)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(oCompanyHdrInfo.CompanyID, SessionHelper.GetUserLanguage());
                oCompanyHdrInfo.DisplayName = LanguageUtil.GetValue(oCompanyHdrInfo.DisplayNameLabelID.Value, oMultilingualAttributeInfo);
            }
        }

        public static void TranslateAuditTrailData(List<GLDataStatusInfo> oGLDataStatusInfoCollection)
        {
            for (int i = 0; i < oGLDataStatusInfoCollection.Count; i++)
            {
                oGLDataStatusInfoCollection[i].Status = LanguageUtil.GetValue(oGLDataStatusInfoCollection[i].StatusLabelID.Value);
            }
        }

        public static void TranslateLabelFSCaptionExceptionInfo(List<ExceptionByFSCaptionNetAccountInfo> oExceptionByFSCaptionNetAccountInfoCollection)
        {
            for (int i = 0; i < oExceptionByFSCaptionNetAccountInfoCollection.Count; i++)
            {
                oExceptionByFSCaptionNetAccountInfoCollection[i].Name = LanguageUtil.GetValue(oExceptionByFSCaptionNetAccountInfoCollection[i].LabelID.Value);
            }
        }

        public static void TranslateLabelReconciliationStatusFSCaptionInfo(List<ReconciliationStatusFSCaptionInfo> oReconciliationStatusFSCaptionInfoCollection)
        {
            for (int i = 0; i < oReconciliationStatusFSCaptionInfoCollection.Count; i++)
            {
                oReconciliationStatusFSCaptionInfoCollection[i].FSCaption = LanguageUtil.GetValue(oReconciliationStatusFSCaptionInfoCollection[i].FSCaptionLabelID.Value);
            }
        }

        public static void TranslateCertVerbiage(List<CertificationVerbiageInfo> oCertificationVerbiageInfoCollection)
        {
            for (int i = 0; i < oCertificationVerbiageInfoCollection.Count; i++)
            {
                oCertificationVerbiageInfoCollection[i].CertificationVerbiage = LanguageUtil.GetValue(oCertificationVerbiageInfoCollection[i].CertificationVerbiageLabelID.Value);
            }
        }

        internal static void TranslateExceptionTypes(List<ExceptionTypeMstInfo> oExceptionTypeMstInfoCollection)
        {
            for (int i = 0; i < oExceptionTypeMstInfoCollection.Count; i++)
            {
                oExceptionTypeMstInfoCollection[i].ExceptionType = LanguageUtil.GetValue(oExceptionTypeMstInfoCollection[i].ExceptionTypeLabelID.Value);
            }
        }
        internal static void TranslateAgingTypes(List<AgingCategoryMstInfo> oAgingCategoryInfoCollection)
        {
            for (int i = 0; i < oAgingCategoryInfoCollection.Count; i++)
            {
                oAgingCategoryInfoCollection[i].AgingCategoryName = LanguageUtil.GetValue(oAgingCategoryInfoCollection[i].AgingCategoryLabelID);
            }
        }
        internal static void TranslateOpenItemClassificationTypes(List<ReconciliationCategoryMstInfo> oRecCategoryInfoCollection)
        {

            for (int i = 0; i < oRecCategoryInfoCollection.Count; i++)
            {
                oRecCategoryInfoCollection[i].ReconciliationCategory = LanguageUtil.GetValue(oRecCategoryInfoCollection[i].ReconciliationCategoryLabelID.Value);
            }
        }
        internal static void TranslateCertficationStatus(List<CertificationStatusMstInfo> oCertStatusMstInfoCollection)
        {
            for (int i = 0; i < oCertStatusMstInfoCollection.Count; i++)
            {
                oCertStatusMstInfoCollection[i].CertificationStatus = LanguageUtil.GetValue(oCertStatusMstInfoCollection[i].CertificationStatusLabelID.Value);
            }
        }
        internal static void TranslateUserRoles(List<RoleMstInfo> oUserRoleCollection)
        {
            for (int i = 0; i < oUserRoleCollection.Count; i++)
            {
                oUserRoleCollection[i].Role = LanguageUtil.GetValue(oUserRoleCollection[i].RoleLabelID.Value);
            }
        }
        internal static void TranslateRiskRating(List<RiskRatingMstInfo> oRiskRatingMstInfoCollection)
        {
            for (int i = 0; i < oRiskRatingMstInfoCollection.Count; i++)
            {
                oRiskRatingMstInfoCollection[i].RiskRating = LanguageUtil.GetValue(oRiskRatingMstInfoCollection[i].RiskRatingLabelID.Value);
            }
        }
        internal static void TranslateReasonTypes(List<ReasonMstInfo> oReasonMstInfoCollection)
        {
            for (int i = 0; i < oReasonMstInfoCollection.Count; i++)
            {
                oReasonMstInfoCollection[i].Reason = LanguageUtil.GetValue(oReasonMstInfoCollection[i].ReasonLabelID.Value);
            }
        }
        internal static void TranslateRecStatusTypes(List<ReconciliationStatusMstInfo> oRecStatusCollection)
        {
            for (int i = 0; i < oRecStatusCollection.Count; i++)
            {
                oRecStatusCollection[i].ReconciliationStatus = LanguageUtil.GetValue(oRecStatusCollection[i].ReconciliationStatusLabelID.Value);
            }
        }

        internal static void TranslateCapabilities(List<CapabilityMstInfo> oCapabilityMstInfoList)
        {
            for (int i = 0; i < oCapabilityMstInfoList.Count; i++)
            {
                oCapabilityMstInfoList[i].Capability = LanguageUtil.GetValue(oCapabilityMstInfoList[i].CapabilityLabelID.Value);
            }
        }


        #region QualityScore

        /// <summary>
        /// Translates the quality score status.
        /// </summary>
        /// <param name="oQualityScoreStatusMstInfoList">The o quality score status MST info list.</param>
        internal static void TranslateQualityScoreStatus(List<QualityScoreStatusMstInfo> oQualityScoreStatusMstInfoList)
        {
            for (int i = 0; i < oQualityScoreStatusMstInfoList.Count; i++)
            {
                oQualityScoreStatusMstInfoList[i].QualityScoreStatus = LanguageUtil.GetValue(oQualityScoreStatusMstInfoList[i].QualityScoreStatusLabelID.Value);
            }
        }

        /// <summary>
        /// Translates the labels company quality score.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfo">The o company quality score info.</param>
        internal static void TranslateLabelsCompanyQualityScore(CompanyQualityScoreInfo oCompanyQualityScoreInfo)
        {
            if (oCompanyQualityScoreInfo.QualityScoreID.HasValue)
            {
                oCompanyQualityScoreInfo.Description = LanguageUtil.GetValue(oCompanyQualityScoreInfo.DescriptionLabelID.Value);
                if (oCompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.GLAdjustmentsAging
                       || oCompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.WriteOffOnAging
                       || oCompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.TimingDiffAging)
                {
                    oCompanyQualityScoreInfo.Description = string.Format(oCompanyQualityScoreInfo.Description, QualityScoreHelper.GetAgingDays());
                }
            }
        }
        /// <summary>
        /// Translates the labels company quality score.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfo">The o company quality score info.</param>
        internal static void TranslateLabelsQualityScoreReportInfo(QualityScoreReportInfo oQualityScoreReportInfo)
        {

            oQualityScoreReportInfo.QualityScoreDesc = LanguageUtil.GetValue(oQualityScoreReportInfo.QualityScoreDescLabelID.Value);
            if (oQualityScoreReportInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.GLAdjustmentsAging
                       || oQualityScoreReportInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.WriteOffOnAging
                       || oQualityScoreReportInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.TimingDiffAging)
            {
                oQualityScoreReportInfo.QualityScoreDesc = string.Format(oQualityScoreReportInfo.QualityScoreDesc, QualityScoreHelper.GetAgingDays());
            }

        }

        /// <summary>
        /// Translates the labels company Role Config data.
        /// </summary>
        /// <param name="oCompanyRoleConfigInfo">The o company role config info.</param>
        internal static void TranslateLabelsCompanyRoleConfig(CompanyAttributeConfigInfo oCompanyRoleConfigInfo)
        {
            if (oCompanyRoleConfigInfo.AttributeID.HasValue)
            {
                oCompanyRoleConfigInfo.Description = LanguageUtil.GetValue(oCompanyRoleConfigInfo.DescriptionLabelID.Value);
            }
        }

        /// <summary>
        /// Translates the labels company quality score.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfo">The o company quality score info.</param>
        internal static void TranslateLabelsCompanyQualityScoreForReports(QualityScoreChecklistInfo oCompanyQualityScoreInfo)
        {
            if (oCompanyQualityScoreInfo.QualityScoreID.HasValue)
            {
                oCompanyQualityScoreInfo.QualityScoreDescription = LanguageUtil.GetValue(oCompanyQualityScoreInfo.QualityScoreDescriptionLabelID.Value);
                if (oCompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.GLAdjustmentsAging
                       || oCompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.WriteOffOnAging
                       || oCompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.TimingDiffAging)
                {
                    oCompanyQualityScoreInfo.QualityScoreDescription = string.Format(oCompanyQualityScoreInfo.QualityScoreDescription, QualityScoreHelper.GetAgingDays());
                }
            }
        }



        /// <summary>
        /// Translates the labels company quality score data.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfoList">The o company quality score info list.</param>
        internal static void TranslateLabelsCompanyQualityScoreData(List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList)
        {
            if (oCompanyQualityScoreInfoList != null && oCompanyQualityScoreInfoList.Count > 0)
            {
                foreach (CompanyQualityScoreInfo item in oCompanyQualityScoreInfoList)
                {
                    TranslateLabelsCompanyQualityScore(item);
                }
            }
        }

        /// <summary>
        /// Translates the labels company quality score data.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfoList">The o company quality score info list.</param>
        internal static void TranslateLabelsCompanyRoleConfigData(List<CompanyAttributeConfigInfo> oCompanyRoleConfigInfoList)
        {
            if (oCompanyRoleConfigInfoList != null && oCompanyRoleConfigInfoList.Count > 0)
            {
                foreach (CompanyAttributeConfigInfo item in oCompanyRoleConfigInfoList)
                {
                    TranslateLabelsCompanyRoleConfig(item);
                }
            }
        }

        /// <summary>
        /// Translates the labels company quality score data.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfoList">The o company quality score info list.</param>
        internal static void TranslateLabelsCompanyQualityScoreDataForReports(List<QualityScoreChecklistInfo> oCompanyQualityScoreInfoList)
        {
            if (oCompanyQualityScoreInfoList != null && oCompanyQualityScoreInfoList.Count > 0)
            {
                foreach (QualityScoreChecklistInfo item in oCompanyQualityScoreInfoList)
                {
                    TranslateLabelsCompanyQualityScoreForReports(item);
                }
            }
        }


        /// <summary>
        /// Translates the Quality Score GL Data labels.
        /// </summary>
        /// <param name="oGLDataQualityScoreInfoList">The o GL data quality score info list.</param>
        internal static void TranslateLabelsQualityScoreGLData(List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList)
        {
            if (oGLDataQualityScoreInfoList != null && oGLDataQualityScoreInfoList.Count > 0)
            {
                foreach (GLDataQualityScoreInfo item in oGLDataQualityScoreInfoList)
                {
                    TranslateLabelsCompanyQualityScore(item.CompanyQualityScoreInfo);
                }
            }
        }

        #endregion

        #region TranslateLabels For Reports

        public static void TranslateLabelsExceptionStatusReport(List<ExceptionStatusReportInfo> oExceptionStatusReportInfoCollection)
        {
            for (int i = 0; i < oExceptionStatusReportInfoCollection.Count; i++)
            {
                TranslateLabelOrganizationalHierarchyInfo(oExceptionStatusReportInfoCollection[i]);
                if (oExceptionStatusReportInfoCollection[i].AccountNameLabelID != null)
                {
                    oExceptionStatusReportInfoCollection[i].AccountName = LanguageUtil.GetValue(oExceptionStatusReportInfoCollection[i].AccountNameLabelID.Value);
                }

                if (oExceptionStatusReportInfoCollection[i].NetAccountLabelID != null)
                {
                    oExceptionStatusReportInfoCollection[i].AccountName = LanguageUtil.GetValue(oExceptionStatusReportInfoCollection[i].NetAccountLabelID.Value);
                    oExceptionStatusReportInfoCollection[i].AccountNumber = LanguageUtil.GetValue(1257);
                }

                oExceptionStatusReportInfoCollection[i].RiskRating = Helper.GetRiskRating(oExceptionStatusReportInfoCollection[i].RiskRatingID);
            }
        }
        public static List<UnusualBalancesReportInfo> TranslateLabelsUnusualBalancesReport(List<UnusualBalancesReportInfo> oUnusualBalancesReportInfoCollection)
        {
            foreach (UnusualBalancesReportInfo oUnusualBalancesReportInfo in oUnusualBalancesReportInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oUnusualBalancesReportInfo);
                //oUnusualBalancesReportInfo.AccountName = GetDisplayTextFromLabelIDWithDefaultText(oUnusualBalancesReportInfo.AccountNameLabelID);
                oUnusualBalancesReportInfo.RiskRating = Helper.GetRiskRating(oUnusualBalancesReportInfo.RiskRatingID);
                //oUnusualBalancesReportInfo.AccountType = GetDisplayTextFromLabelIDWithDefaultText(oUnusualBalancesReportInfo.AccountTypeLabelID);
                oUnusualBalancesReportInfo.Reason = GetDisplayTextFromLabelIDWithDefaultText(oUnusualBalancesReportInfo.ReasonLabelID);
            }
            return oUnusualBalancesReportInfoCollection;
        }
        public static List<AccountOwnershipReportInfo> TranslateLabelsAccountOwnershipReport(List<AccountOwnershipReportInfo> oAccountOwnershipReportInfoCollection)
        {
            foreach (AccountOwnershipReportInfo oAccountOwnershipReportInfo in oAccountOwnershipReportInfoCollection)
            {
                oAccountOwnershipReportInfo.Role = GetDisplayTextFromLabelIDWithDefaultText(oAccountOwnershipReportInfo.RoleLabelID);
            }
            return oAccountOwnershipReportInfoCollection;
        }

        public static List<AccountStatusReportInfo> TranslateLabelsAccountStatusReport(List<AccountStatusReportInfo> oAccountStatusReportInfoCollection)
        {

            DateTime dtStart = DateTime.Now;
            Helper.LogInfo("Translate -> Start Time = " + dtStart.ToString() + " ");
            for (int i = 0; i < oAccountStatusReportInfoCollection.Count; i++)
            {
                TranslateLabelOrganizationalHierarchyInfo(oAccountStatusReportInfoCollection[i]);

                if (oAccountStatusReportInfoCollection[i].NetAccountLabelID != null)
                {
                    oAccountStatusReportInfoCollection[i].AccountName = LanguageUtil.GetValue(oAccountStatusReportInfoCollection[i].NetAccountLabelID.Value);
                    oAccountStatusReportInfoCollection[i].AccountNumber = LanguageUtil.GetValue(1257);
                }
                oAccountStatusReportInfoCollection[i].RiskRating = Helper.GetRiskRating(oAccountStatusReportInfoCollection[i].RiskRatingID);
                oAccountStatusReportInfoCollection[i].ReconciliationStatus = LanguageUtil.GetValue(oAccountStatusReportInfoCollection[i].ReconciliationStatusLabelID.Value);
            }

            DateTime dtEnd = DateTime.Now;
            Helper.LogInfo("Translate -> End Time = " + dtEnd.ToString() + " ");
            long time = (dtEnd.Ticks - dtStart.Ticks) / 10000;
            Helper.LogInfo("Translate -> Time Diff = " + time.ToString() + "ms");
            return oAccountStatusReportInfoCollection;
        }

        public static List<NewAccountReportInfo> TranslateLabelsNewAccountReport(List<NewAccountReportInfo> oNewAccountReportInfoCollection)
        {
            DateTime dtStart = DateTime.Now;
            Helper.LogInfo("Translate -> Start Time = " + dtStart.ToString() + " ");
            for (int i = 0; i < oNewAccountReportInfoCollection.Count; i++)
            {
                TranslateLabelOrganizationalHierarchyInfo(oNewAccountReportInfoCollection[i]);
            }
            DateTime dtEnd = DateTime.Now;
            Helper.LogInfo("Translate -> End Time = " + dtEnd.ToString() + " ");
            long time = (dtEnd.Ticks - dtStart.Ticks) / 10000;
            Helper.LogInfo("Translate -> Time Diff = " + time.ToString() + "ms");
            return oNewAccountReportInfoCollection;
        }

        public static List<AccountAttributeChangeReportInfo> TranslateLabelsAccountAttributeChangeReport(List<AccountAttributeChangeReportInfo> oAccountAttributeChangeReportInfoList)
        {
            DateTime dtStart = DateTime.Now;
            Helper.LogInfo("Translate -> Start Time = " + dtStart.ToString() + " ");
            for (int i = 0; i < oAccountAttributeChangeReportInfoList.Count; i++)
            {
                TranslateLabelOrganizationalHierarchyInfo(oAccountAttributeChangeReportInfoList[i]);
                AccountAttributeChangeReportInfo oAccountAttributeChangeReportInfo = oAccountAttributeChangeReportInfoList[i];
                if (oAccountAttributeChangeReportInfo.AccountAttributeLabelID.HasValue)
                    oAccountAttributeChangeReportInfo.AccountAttribute = LanguageUtil.GetValue(oAccountAttributeChangeReportInfo.AccountAttributeLabelID.Value);
                if (oAccountAttributeChangeReportInfo.ValueLabelID.HasValue)
                    oAccountAttributeChangeReportInfo.Value = LanguageUtil.GetValue(oAccountAttributeChangeReportInfo.ValueLabelID.Value);
            }
            DateTime dtEnd = DateTime.Now;
            Helper.LogInfo("Translate -> End Time = " + dtEnd.ToString() + " ");
            long time = (dtEnd.Ticks - dtStart.Ticks) / 10000;
            Helper.LogInfo("Translate -> Time Diff = " + time.ToString() + "ms");
            return oAccountAttributeChangeReportInfoList;
        }

        public static List<CertificationTrackingReportInfo> TranslateLabelsCertificationTrackingReport(List<CertificationTrackingReportInfo> oCertificationTrackingReportInfoCollection)
        {
            foreach (CertificationTrackingReportInfo oCertificationTrackingReportInfo in oCertificationTrackingReportInfoCollection)
            {
                oCertificationTrackingReportInfo.Role = GetDisplayTextFromLabelIDWithDefaultText(oCertificationTrackingReportInfo.RoleLabelID);
            }
            return oCertificationTrackingReportInfoCollection;
        }
        public static List<DelinquentAccountByUserReportInfo> TranslateLabelsDelinquentAccountByUserReport(List<DelinquentAccountByUserReportInfo> oDelinquentAccountByUserReportInfoCollection)
        {
            foreach (DelinquentAccountByUserReportInfo oDelinquentAccountByUserReportInfo in oDelinquentAccountByUserReportInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oDelinquentAccountByUserReportInfo);
                //if (oDelinquentAccountByUserReportInfo.AccountNameLabelID != null)
                //{
                //    oDelinquentAccountByUserReportInfo.AccountName = LanguageUtil.GetValue(oDelinquentAccountByUserReportInfo.AccountNameLabelID.Value);
                //}

                if (oDelinquentAccountByUserReportInfo.NetAccountLabelID != null)
                {
                    oDelinquentAccountByUserReportInfo.AccountName = LanguageUtil.GetValue(oDelinquentAccountByUserReportInfo.NetAccountLabelID.Value);
                    oDelinquentAccountByUserReportInfo.AccountNumber = LanguageUtil.GetValue(1257);
                }
                oDelinquentAccountByUserReportInfo.Role = GetDisplayTextFromLabelIDWithDefaultText(oDelinquentAccountByUserReportInfo.RoleLabelID);
            }
            return oDelinquentAccountByUserReportInfoCollection;
        }
        public static List<QualityScoreReportInfo> TranslateLabelsQualityScoreReport(List<QualityScoreReportInfo> oQualityScoreReportInfoCollection)
        {
            foreach (QualityScoreReportInfo oQualityScoreReportInfo in oQualityScoreReportInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oQualityScoreReportInfo);

                if (oQualityScoreReportInfo.AccountNameLabelID.HasValue)
                {
                    oQualityScoreReportInfo.AccountName = LanguageUtil.GetValue(oQualityScoreReportInfo.AccountNameLabelID.Value);
                }
                else if (oQualityScoreReportInfo.NetAccountLabelID.HasValue)
                {
                    oQualityScoreReportInfo.AccountName = LanguageUtil.GetValue(oQualityScoreReportInfo.NetAccountLabelID.Value);
                    oQualityScoreReportInfo.AccountNumber = LanguageUtil.GetValue(1257);
                }
                //oDelinquentAccountByUserReportInfo.Role = GetDisplayTextFromLabelIDWithDefaultText(oDelinquentAccountByUserReportInfo.RoleLabelID);
            }
            return oQualityScoreReportInfoCollection;
        }
        public static List<IncompleteAccountAttributeReportInfo> TranslateLabelsIncompleteAccountAttributeReport(List<IncompleteAccountAttributeReportInfo> oIncompleteAccountAttributeReportInfoCollection)
        {
            foreach (IncompleteAccountAttributeReportInfo oIncompleteAccountAttributeReportInfo in oIncompleteAccountAttributeReportInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oIncompleteAccountAttributeReportInfo);
            }
            return oIncompleteAccountAttributeReportInfoCollection;
        }
        public static List<OpenItemsReportInfo> TranslateLabelsOpenItemsReport(List<OpenItemsReportInfo> oOpenItemsReportInfoCollection)
        {
            foreach (OpenItemsReportInfo oOpenItemsReportInfo in oOpenItemsReportInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oOpenItemsReportInfo);
                //if (oOpenItemsReportInfo.AccountNameLabelID != null)
                //{
                //    oOpenItemsReportInfo.AccountName = LanguageUtil.GetValue(oOpenItemsReportInfo.AccountNameLabelID.Value);
                //}

                if (oOpenItemsReportInfo.NetAccountLabelID != null)
                {
                    oOpenItemsReportInfo.AccountName = LanguageUtil.GetValue(oOpenItemsReportInfo.NetAccountLabelID.Value);
                    oOpenItemsReportInfo.AccountNumber = LanguageUtil.GetValue(1257);
                }
                oOpenItemsReportInfo.RiskRating = Helper.GetRiskRating(oOpenItemsReportInfo.RiskRatingID);
                oOpenItemsReportInfo.OpenItemClassification = GetDisplayTextFromLabelIDWithDefaultText(oOpenItemsReportInfo.OpenItemClassificationLabelID);
            }
            return oOpenItemsReportInfoCollection;
        }
        public static List<ReconciliationStatusCountReportInfo> TranslateLabelsReconciliationStatusCountReport(List<ReconciliationStatusCountReportInfo> oReconciliationStatusCountReportInfoCollection)
        {
            foreach (ReconciliationStatusCountReportInfo oReconciliationStatusCountReportInfo in oReconciliationStatusCountReportInfoCollection)
            {
                oReconciliationStatusCountReportInfo.Role = GetDisplayTextFromLabelIDWithDefaultText(oReconciliationStatusCountReportInfo.RoleLabelID);
            }
            return oReconciliationStatusCountReportInfoCollection;
        }

        public static List<UnassignedAccountsReportInfo> TranslateLabelsUnassignedAccountsReport(List<UnassignedAccountsReportInfo> oUnassignedAccountsReportInfoCollection)
        {
            foreach (UnassignedAccountsReportInfo oUnassignedAccountsReportInfo in oUnassignedAccountsReportInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oUnassignedAccountsReportInfo);
            }
            return oUnassignedAccountsReportInfoCollection;
        }

        public static List<CompletionDateReportInfo> TranslateLabelsCompletionDateReport(List<CompletionDateReportInfo> oCompletionDateReportInfoCollection)
        {

            DateTime dtStart = DateTime.Now;
            Helper.LogInfo("Translate -> Start Time = " + dtStart.ToString() + " ");
            for (int i = 0; i < oCompletionDateReportInfoCollection.Count; i++)
            {
                TranslateLabelOrganizationalHierarchyInfo(oCompletionDateReportInfoCollection[i]);

                if (oCompletionDateReportInfoCollection[i].NetAccountLabelID != null)
                {
                    oCompletionDateReportInfoCollection[i].AccountName = LanguageUtil.GetValue(oCompletionDateReportInfoCollection[i].NetAccountLabelID.Value);
                    oCompletionDateReportInfoCollection[i].AccountNumber = LanguageUtil.GetValue(1257);
                }
                oCompletionDateReportInfoCollection[i].ReconciliationStatus = LanguageUtil.GetValue(oCompletionDateReportInfoCollection[i].ReconciliationStatusLabelID.Value);

                if (oCompletionDateReportInfoCollection[i].AccountTypeLabelID.HasValue)
                    oCompletionDateReportInfoCollection[i].AccountType = LanguageUtil.GetValue(oCompletionDateReportInfoCollection[i].AccountTypeLabelID.Value);

                if (oCompletionDateReportInfoCollection[i].FSCaptionLabelID.HasValue)
                    oCompletionDateReportInfoCollection[i].FSCaption = LanguageUtil.GetValue(oCompletionDateReportInfoCollection[i].FSCaptionLabelID.Value);

            }

            DateTime dtEnd = DateTime.Now;
            Helper.LogInfo("Translate -> End Time = " + dtEnd.ToString() + " ");
            long time = (dtEnd.Ticks - dtStart.Ticks) / 10000;
            Helper.LogInfo("Translate -> Time Diff = " + time.ToString() + "ms");
            return oCompletionDateReportInfoCollection;
        }

        public static List<TaskCompletionReportInfo> TranslateLabelsTaskCompletionReport(List<TaskCompletionReportInfo> oTaskCompletionReportInfoCollection)
        {
            for (int i = 0; i < oTaskCompletionReportInfoCollection.Count; i++)
            {
                TranslateLabelOrganizationalHierarchyInfo(oTaskCompletionReportInfoCollection[i]);
                if (oTaskCompletionReportInfoCollection[i].AccountNameLabelID.HasValue)
                    oTaskCompletionReportInfoCollection[i].AccountName = LanguageUtil.GetValue(oTaskCompletionReportInfoCollection[i].AccountNameLabelID.Value);
                if (oTaskCompletionReportInfoCollection[i].AccountTypeLabelID.HasValue)
                    oTaskCompletionReportInfoCollection[i].AccountType = LanguageUtil.GetValue(oTaskCompletionReportInfoCollection[i].AccountTypeLabelID.Value);
                if (oTaskCompletionReportInfoCollection[i].TaskStatusLabelID.HasValue)
                    oTaskCompletionReportInfoCollection[i].TaskStatus = LanguageUtil.GetValue(oTaskCompletionReportInfoCollection[i].TaskStatusLabelID.Value);
            }
            return oTaskCompletionReportInfoCollection;
        }

        #endregion

        private static string GetDisplayTextFromLabelIDWithDefaultText(int? labelID)
        {
            string returnText = "";
            if (labelID.HasValue && labelID.Value > 0)
            {
                returnText = LanguageUtil.GetValue(labelID.Value);
            }
            else if (string.IsNullOrEmpty(returnText))
            {
                returnText = "-";
            }
            return returnText;
        }

        #region Package

        public static List<PackageMstInfo> TranslatePackage(List<PackageMstInfo> oPackageMstInfoCollection)
        {
            foreach (PackageMstInfo oPackageMstInfo in oPackageMstInfoCollection)
            {
                oPackageMstInfo.PackageName = LanguageUtil.GetValue(Convert.ToInt32(oPackageMstInfo.PackageNameLabelID));
            }
            return oPackageMstInfoCollection;
        }

        #endregion

        public static void SetTestLanguage()
        {
            int? testLCID = AppSettingHelper.GetTestLCID();
            if (testLCID != null)
                HttpContext.Current.Session[SessionConstants.CURRENT_LANGUAGE] = testLCID;
        }
        /// <summary>
        /// Translate the TaskRecurrenceTypeMst Collection
        /// </summary>
        /// <param name="oTaskRecurrenceTypeMst"></param>
        /// <returns></returns>

        #region "Task Master"
        internal static List<TaskRecurrenceTypeMstInfo> TranslateTaskRecurrenceTypeMstInfoCollection(List<TaskRecurrenceTypeMstInfo> oTaskRecurrenceTypeMstInfoCollection)
        {
            for (int i = 0; i < oTaskRecurrenceTypeMstInfoCollection.Count; i++)
            {
                oTaskRecurrenceTypeMstInfoCollection[i].RecurrenceType = LanguageUtil.GetValue(oTaskRecurrenceTypeMstInfoCollection[i].RecurrenceTypeLabelID.Value);
            }
            return oTaskRecurrenceTypeMstInfoCollection;
        }
        public static List<TaskHdrInfo> TranslateLabelsTaskHdrInfo(List<TaskHdrInfo> oTaskHdrInfoCollection)
        {
            foreach (TaskHdrInfo oTaskHdrInfo in oTaskHdrInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oTaskHdrInfo);

                if (oTaskHdrInfo.AccountNameLabelID.HasValue)
                {
                    oTaskHdrInfo.AccountName = LanguageUtil.GetValue(oTaskHdrInfo.AccountNameLabelID.Value);
                }
                else
                {
                    oTaskHdrInfo.AccountName = "-";
                }

                if (oTaskHdrInfo.AccountTypeLabelID.HasValue)
                {
                    oTaskHdrInfo.AccountType = LanguageUtil.GetValue(oTaskHdrInfo.AccountTypeLabelID.Value);
                }
                else
                {
                    oTaskHdrInfo.AccountType = "-";
                }
                if (oTaskHdrInfo.TaskStatusLabelID.HasValue)
                {
                    oTaskHdrInfo.TaskStatus = LanguageUtil.GetValue(oTaskHdrInfo.TaskStatusLabelID.Value);
                }
                if (oTaskHdrInfo.RecurrenceType != null && oTaskHdrInfo.RecurrenceType.RecurrenceTypeLabelID.HasValue)
                    oTaskHdrInfo.RecurrenceType.RecurrenceType = LanguageUtil.GetValue(oTaskHdrInfo.RecurrenceType.RecurrenceTypeLabelID.Value);
            }

            return oTaskHdrInfoCollection;
        }
        internal static void TranslateTaskCategoryType(List<TaskCategoryMstInfo> oTaskCategoryMstInfoList)
        {
            if (oTaskCategoryMstInfoList != null && oTaskCategoryMstInfoList.Count > 0)
            {
                for (int i = 0; i < oTaskCategoryMstInfoList.Count; i++)
                {
                    oTaskCategoryMstInfoList[i].Name = LanguageUtil.GetValue(oTaskCategoryMstInfoList[i].LabelID.Value);
                }
            }
        }
        internal static void TranslateTaskStatusType(List<TaskStatusMstInfo> oTaskStatusMstInfoList)
        {
            if (oTaskStatusMstInfoList != null && oTaskStatusMstInfoList.Count > 0)
            {
                for (int i = 0; i < oTaskStatusMstInfoList.Count; i++)
                {
                    oTaskStatusMstInfoList[i].LabelID = oTaskStatusMstInfoList[i].TaskStatusLabelID;
                    oTaskStatusMstInfoList[i].Name = LanguageUtil.GetValue(oTaskStatusMstInfoList[i].LabelID.Value);
                }
            }
        }
        public static void TranslateMonthName(List<TaskStatusCountInfo> oTaskStatusCountInfoList)
        {
            for (int i = 0; i < oTaskStatusCountInfoList.Count; i++)
            {
                oTaskStatusCountInfoList[i].MonthName = LanguageUtil.GetValue(oTaskStatusCountInfoList[i].MonthNameLabelID.Value);
            }
        }

        internal static void TranslateTaskType(List<TaskTypeMstInfo> oTaskTypeMstInfoList)
        {
            for (int i = 0; i < oTaskTypeMstInfoList.Count; i++)
            {
                if (oTaskTypeMstInfoList[i].TaskTypeLabelID.HasValue)
                    oTaskTypeMstInfoList[i].TaskType = LanguageUtil.GetValue(oTaskTypeMstInfoList[i].TaskTypeLabelID.Value);
            }
        }

        #endregion

        public static List<ReviewNotesReportInfo> TranslateLabelsReviewNotesReport(List<ReviewNotesReportInfo> oReviewNotesReportInfoCollection)
        {
            foreach (ReviewNotesReportInfo oReviewNotesReportInfo in oReviewNotesReportInfoCollection)
            {
                TranslateLabelOrganizationalHierarchyInfo(oReviewNotesReportInfo);
            }
            return oReviewNotesReportInfoCollection;
        }

        public static IList<RoleReportInfo_ExtendedWithReportName> TranslateLabelRoleReportInfo_ExtendedWithReportName(IList<RoleReportInfo_ExtendedWithReportName> oRoleReportInfo_ExtendedWithReportNameCollection)
        {
            foreach (RoleReportInfo_ExtendedWithReportName oRoleReportInfo_ExtendedWithReportName in oRoleReportInfo_ExtendedWithReportNameCollection)
            {
                if (oRoleReportInfo_ExtendedWithReportName.ReportLabelID.HasValue)
                    oRoleReportInfo_ExtendedWithReportName.Report = LanguageUtil.GetValue(oRoleReportInfo_ExtendedWithReportName.ReportLabelID.Value);
                else
                    oRoleReportInfo_ExtendedWithReportName.Report = oRoleReportInfo_ExtendedWithReportName.Report;
            }

            return oRoleReportInfo_ExtendedWithReportNameCollection;
        }

        public static List<ReportColumnInfo> TranslateLabelReportColumnInfo(List<ReportColumnInfo> oReportColumnInfoList)
        {
            for (int i = 0; i < oReportColumnInfoList.Count; i++)
            {
                oReportColumnInfoList[i].ColumnName = LanguageUtil.GetValue(oReportColumnInfoList[i].ColumnNameLabelID);
            }
            return oReportColumnInfoList;
        }
        public static IList<DueDaysBasisInfo> TranslateLabelDueDaysBasisInfo(IList<DueDaysBasisInfo> oDueDaysBasisInfoList)
        {
            for (int i = 0; i < oDueDaysBasisInfoList.Count; i++)
            {
                oDueDaysBasisInfoList[i].DueDaysBasis = LanguageUtil.GetValue(oDueDaysBasisInfoList[i].DueDaysBasisLabelID.Value);
            }
            return oDueDaysBasisInfoList;
        }
        public static IList<DaysTypeInfo> TranslateLabelDaysTypeInfo(IList<DaysTypeInfo> oDaysTypeInfoList)
        {
            for (int i = 0; i < oDaysTypeInfoList.Count; i++)
            {
                oDaysTypeInfoList[i].DaysType = LanguageUtil.GetValue(oDaysTypeInfoList[i].DaysTypeLabelID.Value);
            }
            return oDaysTypeInfoList;
        }
        public static void TranslateLabelRequest(List<BulkExportToExcelInfo> oBulkExportToExcelInfoCollection)
        {
            for (int i = 0; i < oBulkExportToExcelInfoCollection.Count; i++)
            {
                if (oBulkExportToExcelInfoCollection[i].RequestTypeLabelID.HasValue)
                    oBulkExportToExcelInfoCollection[i].RequestType = LanguageUtil.GetValue(oBulkExportToExcelInfoCollection[i].RequestTypeLabelID.Value);
                oBulkExportToExcelInfoCollection[i].GridName = LanguageUtil.GetValue(oBulkExportToExcelInfoCollection[i].GridNameLabelID.Value);
                if (oBulkExportToExcelInfoCollection[i].RequestStatusLabelID.HasValue)
                    oBulkExportToExcelInfoCollection[i].RequestStatus = LanguageUtil.GetValue(oBulkExportToExcelInfoCollection[i].RequestStatusLabelID.Value);
                oBulkExportToExcelInfoCollection[i].MonthYear = Helper.GetDisplayMonthYear(oBulkExportToExcelInfoCollection[i].PeriodEndDate);
                if (oBulkExportToExcelInfoCollection[i].RequestErrorLabelID.HasValue)
                    oBulkExportToExcelInfoCollection[i].StatusMessage = LanguageUtil.GetValue(oBulkExportToExcelInfoCollection[i].RequestErrorLabelID.Value);
            }
        }
        public static void TranslateLabelRecControlCheckListInfoList(List<RecControlCheckListInfo> oRecControlCheckListInfoCollection)
        {
            for (int i = 0; i < oRecControlCheckListInfoCollection.Count; i++)
            {
                oRecControlCheckListInfoCollection[i].Description = LanguageUtil.GetValue(oRecControlCheckListInfoCollection[i].DescriptionLabelID.Value);
            }
        }
        public static void TranslateLabelDataTypeMstInfoList(List<DataTypeMstInfo> oDataTypeMstInfoCollection)
        {
            for (int i = 0; i < oDataTypeMstInfoCollection.Count; i++)
            {
                if (oDataTypeMstInfoCollection[i].DataTypeNameLabelID.HasValue)
                    oDataTypeMstInfoCollection[i].DataTypeName = LanguageUtil.GetValue(oDataTypeMstInfoCollection[i].DataTypeNameLabelID.Value);
                else
                    oDataTypeMstInfoCollection[i].DataTypeName = oDataTypeMstInfoCollection[i].DataType;
            }
        }
        public static void TranslateLabelMatchingSourceTypeInfoList(List<MatchingSourceTypeInfo> oMatchingSourceTypeInfoList)
        {
            for (int i = 0; i < oMatchingSourceTypeInfoList.Count; i++)
            {
                if (oMatchingSourceTypeInfoList[i].MatchingSourceTypeNameLabelID.HasValue)
                    oMatchingSourceTypeInfoList[i].MatchingSourceTypeName = LanguageUtil.GetValue(oMatchingSourceTypeInfoList[i].MatchingSourceTypeNameLabelID.Value);
                else
                    oMatchingSourceTypeInfoList[i].MatchingSourceTypeName = oMatchingSourceTypeInfoList[i].MatchingSourceTypeName;
            }
        }

        public static void TranslateOperatorInfo(List<OperatorMstInfo> oOperatorCollection)
        {
            for (int i = 0; i < oOperatorCollection.Count; i++)
            {
                oOperatorCollection[i].OperatorName = LanguageUtil.GetValue(oOperatorCollection[i].OperatorNameLabelID.Value);
            }
        }


        /// <summary>
        /// Translate the ReconciliationCheckListStatus Collection
        /// </summary>
        /// <param name="oRoleMstInfoCollection"></param>
        /// <returns></returns>
        public static List<ReconciliationCheckListStatusInfo> TranslateReconciliationCheckListStatusCollection(List<ReconciliationCheckListStatusInfo> oReconciliationCheckListStatusInfoCollection)
        {
            for (int i = 0; i < oReconciliationCheckListStatusInfoCollection.Count; i++)
            {
                if (oReconciliationCheckListStatusInfoCollection[i].ReconciliationCheckListStatusLabelID.HasValue)
                    oReconciliationCheckListStatusInfoCollection[i].ReconciliationCheckListStatus = LanguageUtil.GetValue(oReconciliationCheckListStatusInfoCollection[i].ReconciliationCheckListStatusLabelID.Value);
            }
            return oReconciliationCheckListStatusInfoCollection;
        }

        /// <summary>
        /// Translate the RCCValidationTypeMstInfo Collection
        /// </summary>
        /// <param name="oRoleMstInfoCollection"></param>
        /// <returns></returns>
        public static List<RCCValidationTypeMstInfo> TranslateLabelRCCValidationTypeMstInfo(List<RCCValidationTypeMstInfo> oRCCValidationTypeMstInfoList)
        {
            for (int i = 0; i < oRCCValidationTypeMstInfoList.Count; i++)
            {
                if (oRCCValidationTypeMstInfoList[i].RCCValidationTypeNameLabelID.HasValue)
                    oRCCValidationTypeMstInfoList[i].RCCValidationTypeName = LanguageUtil.GetValue(oRCCValidationTypeMstInfoList[i].RCCValidationTypeNameLabelID.Value);
            }
            return oRCCValidationTypeMstInfoList;
        }

        /// <summary>
        /// Translate the Import Field Mst Collection
        /// </summary>
        /// <param name="oRoleMstInfoCollection"></param>
        /// <returns></returns>
        public static List<ImportFieldMstInfo> TranslateImportFieldCollection(List<ImportFieldMstInfo> oImportFieldMstInfoLst)
        {
            for (int i = 0; i < oImportFieldMstInfoLst.Count; i++)
            {
                if (oImportFieldMstInfoLst[i].DescriptionLabelID > 0)
                    oImportFieldMstInfoLst[i].Description = LanguageUtil.GetValue(oImportFieldMstInfoLst[i].DescriptionLabelID);
            }
            return oImportFieldMstInfoLst;
        }

        public static List<ImportTemplateHdrInfo> TranslateLabelImportTemplateHdrInfo(List<ImportTemplateHdrInfo> oImportTemplateInfoLst)
        {
            for (int i = 0; i < oImportTemplateInfoLst.Count; i++)
            {
                if (oImportTemplateInfoLst[i].DataImportTypeLabelID.HasValue)
                    oImportTemplateInfoLst[i].DataImportType = LanguageUtil.GetValue(oImportTemplateInfoLst[i].DataImportTypeLabelID.Value);
            }
            return oImportTemplateInfoLst;
        }

        public static List<DataImportMessageInfo> TranslateLabelDataImportMessageLst(List<DataImportMessageInfo> oDataImportMessageInfoLst)
        {
            for (int i = 0; i < oDataImportMessageInfoLst.Count; i++)
            {
                if (oDataImportMessageInfoLst[i].DescriptionLabelID > 0)
                    oDataImportMessageInfoLst[i].Description = LanguageUtil.GetValue(oDataImportMessageInfoLst[i].DescriptionLabelID);

                if (oDataImportMessageInfoLst[i].DataImportMessageLabelID.HasValue)
                    oDataImportMessageInfoLst[i].DataImportMessageLabel = LanguageUtil.GetValue(oDataImportMessageInfoLst[i].DataImportMessageLabelID.Value);
            }
            return oDataImportMessageInfoLst;
        }
        public static List<DataImportWarningPreferencesAuditInfo> TranslateLabelAllWarningAuditList(List<DataImportWarningPreferencesAuditInfo> oDataImportMessageInfoLst)
        {
            for (int i = 0; i < oDataImportMessageInfoLst.Count; i++)
            {
                if (oDataImportMessageInfoLst[i].DataImportMessageLabelID > 0)
                    oDataImportMessageInfoLst[i].DataImportMessageLabel = LanguageUtil.GetValue(oDataImportMessageInfoLst[i].DataImportMessageLabelID);

                if (oDataImportMessageInfoLst[i].DataImportTypeLabelID > 0)
                    oDataImportMessageInfoLst[i].DataImportTypeLabel = LanguageUtil.GetValue(oDataImportMessageInfoLst[i].DataImportTypeLabelID);

                if (oDataImportMessageInfoLst[i].RoleLabelID > 0)
                    oDataImportMessageInfoLst[i].RoleName = LanguageUtil.GetValue(oDataImportMessageInfoLst[i].RoleLabelID);
            }
            return oDataImportMessageInfoLst;
        }
        public static List<TaskCustomFieldInfo> TranslateLabelsTaskCustomFieldInfo(List<TaskCustomFieldInfo> oTaskCustomFieldInfoList)
        {
            foreach (TaskCustomFieldInfo oTaskCustomFieldInfo in oTaskCustomFieldInfoList)
            {
                if (oTaskCustomFieldInfo.CustomFieldValueLabelID.HasValue)
                    oTaskCustomFieldInfo.CustomFieldValue = LanguageUtil.GetValue(oTaskCustomFieldInfo.CustomFieldValueLabelID.Value);
            }

            return oTaskCustomFieldInfoList;
        }
        public static void TranslateRecPeriodStatusDetailInfoData(List<RecPeriodStatusDetailInfo> oRecPeriodStatusDetailInfoList)
        {
            for (int i = 0; i < oRecPeriodStatusDetailInfoList.Count; i++)
            {
                oRecPeriodStatusDetailInfoList[i].ReconciliationPeriodStatus = LanguageUtil.GetValue(oRecPeriodStatusDetailInfoList[i].ReconciliationPeriodStatusLabelID.Value);
            }
        }
    }
}