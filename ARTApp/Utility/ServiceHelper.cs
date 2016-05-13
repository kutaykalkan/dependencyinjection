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
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using System.Data.SqlClient;
using System.Collections;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Client.Model.MappingUpload;
using SkyStem.ART.App.DAO.CompanyDatabase;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.IServices;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.Globalization;
using SkyStem.ART.Client.Model.RecControlCheckList;
using SkyStem.ART.App.Data;

namespace SkyStem.ART.App.Utility
{
    public class ServiceHelper
    {
        public static readonly string FilterValueSeparator = AppSettingHelper.GetAppSettingValue(AppSettingConstants.FILTER_VALUE_SEPARATOR);
        internal static DataTable ConvertIDCollectionToDataTable(List<int> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertIDCollectionToDataTable(List<Int16> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertIDCollectionToDataTable(List<Int64> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertFSCaptionWithMaterialityInfoCollectionToDataTable(IList<FSCaptionMaterialityInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                //DataColumn dc1 = dt.Columns.Add("FSCaptionID");
                //DataColumn dc2 = dt.Columns.Add("MaterialityValue");
                dt.Columns.Add("FSCaptionID", System.Type.GetType("System.Int32"));
                dt.Columns.Add("MaterialityValue", System.Type.GetType("System.Decimal"));
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["FSCaptionID"] = oIDCollection[i].FSCaptionID;
                    //dr[1] = oIDCollection[i].MaterialityThreshold.Value.ToString("#.#");
                    if (oIDCollection[i].MaterialityThreshold.HasValue)
                    {
                        dr["MaterialityValue"] = oIDCollection[i].MaterialityThreshold.Value;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertCompanyCapabilityInfoCollectionToDataTable(IList<CompanyCapabilityInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("CapabilityID");
                DataColumn dc2 = dt.Columns.Add("IsActivated");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i].CapabilityID;
                    dr[1] = oIDCollection[i].IsActivated;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertCompanyCapabilityAttributeInfoCollectionToDataTable(IList<CompanyCapabilityInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("CapabilityAttributeID");
                DataColumn dc2 = dt.Columns.Add("Value");
                DataColumn dc3 = dt.Columns.Add("ReferenceID");
                DataRow dr;
                foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oIDCollection)
                {
                    if (oCompanyCapabilityInfo.CapabilityAttributeValueInfoList != null && oCompanyCapabilityInfo.CapabilityAttributeValueInfoList.Count > 0)
                    {
                        foreach (CapabilityAttributeValueInfo oCapabilityAttributeValueInfo in oCompanyCapabilityInfo.CapabilityAttributeValueInfoList)
                        {
                            dr = dt.NewRow();
                            dr["CapabilityAttributeID"] = oCapabilityAttributeValueInfo.CapabilityAttributeID;
                            if (string.IsNullOrEmpty(oCapabilityAttributeValueInfo.Value))
                                dr["Value"] = DBNull.Value;
                            else
                                dr["Value"] = oCapabilityAttributeValueInfo.Value;
                            if (oCapabilityAttributeValueInfo.ReferenceID.HasValue)
                                dr["ReferenceID"] = oCapabilityAttributeValueInfo.ReferenceID.Value;
                            else
                                dr["ReferenceID"] = DBNull.Value;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            return dt;
        }

        internal static DataTable ConvertNetAccountAttributeValueInfoToDataTable(IList<NetAccountAttributeValueInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("AccountAttributeID");
                dt.Columns.Add("AccountAttributeValue");
                dt.Columns.Add("AccountAttributeValueLabelID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i].AccountAttributeID;
                    dr[1] = oIDCollection[i].Value;
                    dr[2] = oIDCollection[i].ValueLabelID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertRiskRatingReconciliationPeriodInfoCollectionToDataTable(IList<RiskRatingReconciliationPeriodInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("CompanyID");
                DataColumn dc2 = dt.Columns.Add("RiskRatingID");
                DataColumn dc3 = dt.Columns.Add("ReconciliationPeriodID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i].CompanyID;
                    dr[1] = oIDCollection[i].RiskRatingID;
                    dr[2] = oIDCollection[i].ReconciliationPeriodID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertRiskRatingReconciliationFrequencyInfoCollectionToDataTable(IList<RiskRatingReconciliationFrequencyInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("RiskRatingID");
                DataColumn dc2 = dt.Columns.Add("ReconciliationFrequencyID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i].RiskRatingID;
                    dr[1] = oIDCollection[i].ReconciliationFrequencyID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertRoleMandatoryReportInfoCollectionToDataTable(IList<RoleMandatoryReportInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("RoleID");
                DataColumn dc2 = dt.Columns.Add("ReportID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i].RoleID;
                    dr[1] = oIDCollection[i].ReportID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertAccountHdrInfoCollectionToDataTable(List<AccountHdrInfo> oAccountHdrInfoCollection)
        {
            DataTable dt = null;

            if (oAccountHdrInfoCollection != null && oAccountHdrInfoCollection.Count > 0)
            {
                dt = new DataTable("AccountHdrTableType");
                DataColumn dc1 = dt.Columns.Add("AccountID");
                DataColumn dc2 = dt.Columns.Add("AccountTypeID");
                DataColumn dc3 = dt.Columns.Add("ReconciliationTemplateID");
                DataColumn dc4 = dt.Columns.Add("IsKeyAccount");
                DataColumn dc5 = dt.Columns.Add("IsZeroBalance");
                DataColumn dc6 = dt.Columns.Add("FSCaptionID");
                DataColumn dc7 = dt.Columns.Add("RiskRatingID");
                DataColumn dc8 = dt.Columns.Add("SubledgerSourceID");
                DataColumn dc9 = dt.Columns.Add("PreparerUserID");
                DataColumn dc10 = dt.Columns.Add("ReviewerUserID");
                DataColumn dc11 = dt.Columns.Add("ApproverUserID");
                DataColumn dc12 = dt.Columns.Add("BackupPreparerUserID");
                DataColumn dc13 = dt.Columns.Add("BackupReviewerUserID");
                DataColumn dc14 = dt.Columns.Add("BackupApproverUserID");
                DataColumn dc15 = dt.Columns.Add("IsExcludeOwnershipForZBA");

                DataRow dr;

                for (int i = 0; i < oAccountHdrInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["AccountID"] = oAccountHdrInfoCollection[i].AccountID;
                    dr["AccountTypeID"] = oAccountHdrInfoCollection[i].AccountTypeID;
                    dr["ReconciliationTemplateID"] = oAccountHdrInfoCollection[i].ReconciliationTemplateID;
                    dr["IsKeyAccount"] = oAccountHdrInfoCollection[i].IsKeyAccount;
                    dr["IsZeroBalance"] = oAccountHdrInfoCollection[i].IsZeroBalance;
                    dr["FSCaptionID"] = oAccountHdrInfoCollection[i].FSCaptionID;
                    dr["RiskRatingID"] = oAccountHdrInfoCollection[i].RiskRatingID;
                    dr["SubledgerSourceID"] = oAccountHdrInfoCollection[i].SubLedgerSourceID;
                    if (oAccountHdrInfoCollection[i].PreparerUserID.HasValue && oAccountHdrInfoCollection[i].PreparerUserID.Value > 0)
                    {
                        dr["PreparerUserID"] = oAccountHdrInfoCollection[i].PreparerUserID;
                    }
                    else
                    {
                        dr["PreparerUserID"] = DBNull.Value;
                    }
                    if (oAccountHdrInfoCollection[i].ReviewerUserID.HasValue && oAccountHdrInfoCollection[i].ReviewerUserID.Value > 0)
                    {
                        dr["ReviewerUserID"] = oAccountHdrInfoCollection[i].ReviewerUserID;
                    }
                    else
                    {
                        dr["ReviewerUserID"] = DBNull.Value;
                    }
                    if (oAccountHdrInfoCollection[i].ApproverUserID.HasValue && oAccountHdrInfoCollection[i].ApproverUserID.Value > 0)
                    {
                        dr["ApproverUserID"] = oAccountHdrInfoCollection[i].ApproverUserID;
                    }
                    else
                    {
                        dr["ApproverUserID"] = DBNull.Value;
                    }

                    #region BackupRoles

                    if (oAccountHdrInfoCollection[i].BackupPreparerUserID.HasValue && oAccountHdrInfoCollection[i].BackupPreparerUserID.Value > 0)
                    {
                        dr["BackupPreparerUserID"] = oAccountHdrInfoCollection[i].BackupPreparerUserID;
                    }
                    else
                    {
                        dr["BackupPreparerUserID"] = DBNull.Value;
                    }
                    if (oAccountHdrInfoCollection[i].BackupReviewerUserID.HasValue && oAccountHdrInfoCollection[i].BackupReviewerUserID.Value > 0)
                    {
                        dr["BackupReviewerUserID"] = oAccountHdrInfoCollection[i].BackupReviewerUserID;
                    }
                    else
                    {
                        dr["BackupReviewerUserID"] = DBNull.Value;
                    }
                    if (oAccountHdrInfoCollection[i].BackupApproverUserID.HasValue && oAccountHdrInfoCollection[i].BackupApproverUserID.Value > 0)
                    {
                        dr["BackupApproverUserID"] = oAccountHdrInfoCollection[i].BackupApproverUserID;
                    }
                    else
                    {
                        dr["BackupApproverUserID"] = DBNull.Value;
                    }

                    #endregion


                    if (oAccountHdrInfoCollection[i].IsExcludeOwnershipForZBA.HasValue)
                    {
                        dr["IsExcludeOwnershipForZBA"] = oAccountHdrInfoCollection[i].IsExcludeOwnershipForZBA;
                    }
                    else
                    {
                        dr["IsExcludeOwnershipForZBA"] = DBNull.Value;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #region Account Attribute Value

        /// <summary>
        /// Convert Account Hdr Object to account attribute value table
        /// </summary>
        /// <param name="oAccountHdrInfoList"></param>
        /// <returns></returns>
        internal static DataTable ConvertAccountHdrInfoListToAccountAttributeValueDataTable(List<AccountHdrInfo> oAccountHdrInfoList)
        {
            DataTable dt = null;
            if (oAccountHdrInfoList != null && oAccountHdrInfoList.Count > 0)
            {
                dt = CreateAccountAttributeValueTable();

                foreach (AccountHdrInfo oAccountHdrInfo in oAccountHdrInfoList)
                {
                    if (!string.IsNullOrEmpty(oAccountHdrInfo.AccountPolicyUrl))
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.AccountPolicyURL, oAccountHdrInfo.AccountPolicyUrl, oAccountHdrInfo.AccountPolicyUrlLabelID);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.AccountPolicyURL, null, null);

                    if (!string.IsNullOrEmpty(oAccountHdrInfo.Description))
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Description, oAccountHdrInfo.Description, oAccountHdrInfo.DescriptionLabelID.Value);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Description, null, null);

                    if (!string.IsNullOrEmpty(oAccountHdrInfo.ReconciliationProcedure))
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ReconciliationProcedure, oAccountHdrInfo.ReconciliationProcedure, oAccountHdrInfo.ReconciliationProcedureLabelID.Value);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ReconciliationProcedure, null, null);

                    if (oAccountHdrInfo.IsKeyAccount.HasValue)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.IsKeyAccount, oAccountHdrInfo.IsKeyAccount.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.IsKeyAccount, null, null);

                    if (oAccountHdrInfo.IsZeroBalance.HasValue)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.IsZeroBalanceAccount, oAccountHdrInfo.IsZeroBalance.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.IsZeroBalanceAccount, null, null);

                    if (oAccountHdrInfo.IsReconcilable.HasValue)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.IsReconcilable, oAccountHdrInfo.IsReconcilable.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.IsReconcilable, null, null);

                    if (oAccountHdrInfo.RiskRatingID.HasValue)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.RiskRating, oAccountHdrInfo.RiskRatingID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.RiskRating, null, null);

                    if (oAccountHdrInfo.ReconciliationTemplateID.HasValue)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, oAccountHdrInfo.ReconciliationTemplateID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, null, null);

                    if (oAccountHdrInfo.SubLedgerSourceID.HasValue)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.SubledgerSource, oAccountHdrInfo.SubLedgerSourceID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.SubledgerSource, null, null);

                    if (oAccountHdrInfo.PreparerUserID.HasValue && oAccountHdrInfo.PreparerUserID.Value > 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Preparer, oAccountHdrInfo.PreparerUserID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Preparer, null, null);

                    if (oAccountHdrInfo.ReviewerUserID.HasValue && oAccountHdrInfo.ReviewerUserID.Value > 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Reviewer, oAccountHdrInfo.ReviewerUserID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Reviewer, null, null);

                    if (oAccountHdrInfo.ApproverUserID.HasValue && oAccountHdrInfo.ApproverUserID.Value > 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Approver, oAccountHdrInfo.ApproverUserID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.Approver, null, null);

                    if (oAccountHdrInfo.BackupPreparerUserID.HasValue && oAccountHdrInfo.BackupPreparerUserID.Value > 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.BackupPreparer, oAccountHdrInfo.BackupPreparerUserID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.BackupPreparer, null, null);

                    if (oAccountHdrInfo.BackupReviewerUserID.HasValue && oAccountHdrInfo.BackupReviewerUserID.Value > 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.BackupReviewer, oAccountHdrInfo.BackupReviewerUserID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.BackupReviewer, null, null);

                    if (oAccountHdrInfo.BackupApproverUserID.HasValue && oAccountHdrInfo.BackupApproverUserID.Value > 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.BackupApprover, oAccountHdrInfo.BackupApproverUserID.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.BackupApprover, null, null);

                    if (oAccountHdrInfo.PreparerDueDays.HasValue && oAccountHdrInfo.PreparerDueDays.Value != 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.PreparerDueDays, oAccountHdrInfo.PreparerDueDays.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.PreparerDueDays, null, null);

                    if (oAccountHdrInfo.ReviewerDueDays.HasValue && oAccountHdrInfo.ReviewerDueDays.Value != 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ReviewerDueDays, oAccountHdrInfo.ReviewerDueDays.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ReviewerDueDays, null, null);

                    if (oAccountHdrInfo.ApproverDueDays.HasValue && oAccountHdrInfo.ApproverDueDays.Value != 0)
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ApproverDueDays, oAccountHdrInfo.ApproverDueDays.Value.ToString(), null);
                    else
                        AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.ApproverDueDays, null, null);
                    //if (oAccountHdrInfo.DayTypeID.HasValue && oAccountHdrInfo.DayTypeID.Value != 0)
                    //    AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.DayType, oAccountHdrInfo.DayTypeID.Value.ToString(), null);
                    //else
                    //    AddAccountAttributeRow(dt, oAccountHdrInfo.AccountID.Value, (short)ARTEnums.AccountAttribute.DayType, null, null);
                }
            }
            return dt;
        }
        /// <summary>
        /// Convert Account ID list to Account Attribute Value Table
        /// </summary>
        /// <param name="oAccountIDList"></param>
        /// <param name="accountAttributeID"></param>
        /// <param name="value"></param>
        /// <param name="valueLabelID"></param>
        /// <returns></returns>
        internal static DataTable ConvertAccountIDListToAccountAttributeValueDataTable(List<long> oAccountIDList, short accountAttributeID, string value, int? valueLabelID)
        {
            DataTable dt = null;
            if (oAccountIDList != null && oAccountIDList.Count > 0)
            {
                dt = CreateAccountAttributeValueTable();
                foreach (long accountID in oAccountIDList)
                {
                    AddAccountAttributeRow(dt, accountID, accountAttributeID, value, valueLabelID);
                }
            }
            return dt;
        }
        /// <summary>
        /// Create Account Attribute Table
        /// </summary>
        /// <returns></returns>
        internal static DataTable CreateAccountAttributeValueTable()
        {
            DataTable dt = new DataTable("AccountAttributeValue");
            DataColumn dc1 = dt.Columns.Add("AccountID");
            DataColumn dc2 = dt.Columns.Add("AccountAttributeID");
            DataColumn dc3 = dt.Columns.Add("Value");
            DataColumn dc4 = dt.Columns.Add("ValueLabelID");
            return dt;
        }
        /// <summary>
        /// Add Row to account attribute value table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="accountID"></param>
        /// <param name="accountAttributeID"></param>
        /// <param name="value"></param>
        /// <param name="valueLabelID"></param>
        internal static void AddAccountAttributeRow(DataTable dt, long accountID, short accountAttributeID, string value, int? valueLabelID)
        {
            DataRow dr = dt.NewRow();
            dr["AccountID"] = accountID;
            dr["AccountAttributeID"] = accountAttributeID;
            if (string.IsNullOrEmpty(value))
                dr["Value"] = DBNull.Value;
            else
                dr["Value"] = value;
            if (valueLabelID.HasValue)
                dr["ValueLabelID"] = valueLabelID;
            else
                dr["ValueLabelID"] = DBNull.Value;
            dt.Rows.Add(dr);
        }

        #endregion

        #region "Logging"
        internal static void LogException(Exception ex, AppUserInfo oAppUser)
        {
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(ARTConstants.LOGGER_NAME);
            //oLogger.Error("", ex);
            LogExceptionViaService(ex, oAppUser);
        }

        internal static void LogAndThrowGenericException(Exception ex, AppUserInfo oAppUser)
        {
            ServiceHelper.LogException(ex, oAppUser);
            // Throw Custom Error
            throw new ARTException(5000030);
        }

        internal static void LogAndThrowGenericSqlException(SqlException ex, AppUserInfo oAppUser)
        {
            ServiceHelper.LogException(ex, oAppUser);
            // Throw Custom Error
            throw new ARTSystemException(5000011);
        }
        internal static void LogExceptionViaService(Exception ex, AppUserInfo oAppUser)
        {
            IUtility Utility = new App.Services.Utility();
            LogInfo oLog = new LogInfo();
            oLog.Message = ex.Message;
            oLog.StackTrace = ex.StackTrace;
            oLog.LogDate = DateTime.Now;
            oLog.LogLevel = ARTConstants.LOG_ERROR;
            oLog.Source = ARTConstants.BLL_SOURCE_NAME;
            Utility.LogError(oLog, oAppUser);
        }
        #endregion


        internal static DataTable ConvertNetAccounthdrtoDataTable(IList<AccountHdrInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("AccountID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i].AccountID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        internal static DataTable ConvertLongIDCollectionToDataTable(List<long> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("BigIntIDTableType");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertShortIDCollectionToDataTable(List<short> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("SmallIntIDTableType");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertLongIDCollectionShortToDataTable(List<long> oIDCollection, short ID)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("udt_BigInt_SmallInt");
                DataColumn dc1 = dt.Columns.Add("ID1");
                DataColumn dc2 = dt.Columns.Add("ID2");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dr[0] = ID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertReconciliationPeriodDatesCollectionToDataTable(List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection)
        {
            DataTable dt = null;

            if (oReconciliationPeriodInfoCollection != null && oReconciliationPeriodInfoCollection.Count > 0)
            {
                dt = new DataTable("ReconciliationPeriodDatesTableType");
                DataColumn dc1 = dt.Columns.Add("ReconciliationPeriodID", System.Type.GetType("System.Int32"));
                DataColumn dc2 = dt.Columns.Add("PrepareDueDate", System.Type.GetType("System.DateTime"));
                DataColumn dc3 = dt.Columns.Add("ReviewerDueDate", System.Type.GetType("System.DateTime"));
                DataColumn dc4 = dt.Columns.Add("ApproverDueDate", System.Type.GetType("System.DateTime"));
                DataColumn dc5 = dt.Columns.Add("CertificationStartDate", System.Type.GetType("System.DateTime"));
                DataColumn dc6 = dt.Columns.Add("PeriodCloseDate", System.Type.GetType("System.DateTime"));
                DataColumn dc7 = dt.Columns.Add("CertificationLockDownDate", System.Type.GetType("System.DateTime"));
                DataColumn dc8 = dt.Columns.Add("AllowCertificationLockdown", System.Type.GetType("System.Boolean"));
                DataRow dr;

                for (int i = 0; i < oReconciliationPeriodInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();

                    //dr["ReconciliationPeriodID"] = oReconciliationPeriodInfoCollection[i].ReconciliationPeriodID;

                    //if (oReconciliationPeriodInfoCollection[i].PreparerDueDate.HasValue)
                    //{
                    //    dr["PrepareDueDate"] = oReconciliationPeriodInfoCollection[i].PreparerDueDate;
                    //}
                    //else
                    //{
                    //    dr["PrepareDueDate"] = System.DBNull.Value;
                    //}

                    //if (oReconciliationPeriodInfoCollection[i].ReviewerDueDate.HasValue)
                    //{
                    //    dr["ReviewerDueDate"] = oReconciliationPeriodInfoCollection[i].ReviewerDueDate;
                    //}
                    //else
                    //{
                    //    dr["ReviewerDueDate"] = System.DBNull.Value;
                    //}

                    //if (oReconciliationPeriodInfoCollection[i].ApproverDueDate.HasValue)
                    //{
                    //    dr["ApproverDueDate"] = oReconciliationPeriodInfoCollection[i].ApproverDueDate;
                    //}
                    //else
                    //{
                    //    dr["ApproverDueDate"] = System.DBNull.Value;
                    //}

                    //if (oReconciliationPeriodInfoCollection[i].CertificationStartDate.HasValue)
                    //{
                    //    dr["CertificationStartDate"] = oReconciliationPeriodInfoCollection[i].CertificationStartDate;
                    //}
                    //else
                    //{
                    //    dr["CertificationStartDate"] = System.DBNull.Value;
                    //}

                    //if (oReconciliationPeriodInfoCollection[i].ReconciliationCloseDate.HasValue)
                    //{
                    //    dr["PeriodCloseDate"] = oReconciliationPeriodInfoCollection[i].ReconciliationCloseDate;
                    //}
                    //else
                    //{
                    //    dr["PeriodCloseDate"] = System.DBNull.Value;
                    //}

                    //if (oReconciliationPeriodInfoCollection[i].CertificationLockDownDate.HasValue)
                    //{
                    //    dr["CertificationLockDownDate"] = oReconciliationPeriodInfoCollection[i].CertificationLockDownDate;
                    //}
                    //else
                    //{
                    //    dr["CertificationLockDownDate"] = System.DBNull.Value;
                    //}

                    //if (oReconciliationPeriodInfoCollection[i].AllowCertificationLockdown.HasValue)
                    //{
                    //    dr["AllowCertificationLockdown"] = oReconciliationPeriodInfoCollection[i].AllowCertificationLockdown;
                    //}
                    //else
                    //{
                    //    dr["AllowCertificationLockdown"] = System.DBNull.Value;
                    //}


                    dr["ReconciliationPeriodID"] = oReconciliationPeriodInfoCollection[i].ReconciliationPeriodID;
                    if (oReconciliationPeriodInfoCollection[i].PreparerDueDate.HasValue)
                    {
                        dr["PrepareDueDate"] = oReconciliationPeriodInfoCollection[i].PreparerDueDate;
                    }

                    if (oReconciliationPeriodInfoCollection[i].ReviewerDueDate.HasValue)
                    {
                        dr["ReviewerDueDate"] = oReconciliationPeriodInfoCollection[i].ReviewerDueDate;
                    }


                    if (oReconciliationPeriodInfoCollection[i].ApproverDueDate.HasValue)
                    {
                        dr["ApproverDueDate"] = oReconciliationPeriodInfoCollection[i].ApproverDueDate;
                    }


                    if (oReconciliationPeriodInfoCollection[i].CertificationStartDate.HasValue)
                    {
                        dr["CertificationStartDate"] = oReconciliationPeriodInfoCollection[i].CertificationStartDate;
                    }


                    if (oReconciliationPeriodInfoCollection[i].ReconciliationCloseDate.HasValue)
                    {
                        dr["PeriodCloseDate"] = oReconciliationPeriodInfoCollection[i].ReconciliationCloseDate;
                    }


                    if (oReconciliationPeriodInfoCollection[i].CertificationLockDownDate.HasValue)
                    {
                        dr["CertificationLockDownDate"] = oReconciliationPeriodInfoCollection[i].CertificationLockDownDate;
                    }


                    if (oReconciliationPeriodInfoCollection[i].AllowCertificationLockdown.HasValue)
                    {
                        dr["AllowCertificationLockdown"] = oReconciliationPeriodInfoCollection[i].AllowCertificationLockdown;
                    }

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #region "Common Command Parameters"
        internal static void AddCommonSortParameters(IDbCommand cmd, IDataParameterCollection cmdParams,
            string SortExpression, string SortDirection)
        {
            IDbDataParameter parSortExpression = cmd.CreateParameter();
            parSortExpression.ParameterName = "@SortExpression";
            if (string.IsNullOrEmpty(SortExpression))
            {
                parSortExpression.Value = DBNull.Value;
            }
            else
            {
                parSortExpression.Value = SortExpression;
            }
            cmdParams.Add(parSortExpression);

            IDbDataParameter parSortDirection = cmd.CreateParameter();
            parSortDirection.ParameterName = "@SortDirection";
            parSortDirection.Value = SortDirection;
            cmdParams.Add(parSortDirection);
        }


        internal static void AddCommonLanguageParameters(IDbCommand cmd, IDataParameterCollection cmdParams,
            int LCID, int BusinessEntityID, int DefaultLCID)
        {
            IDbDataParameter parLCID = cmd.CreateParameter();
            parLCID.ParameterName = "@LCID";
            parLCID.Value = LCID;
            cmdParams.Add(parLCID);

            IDbDataParameter parBusinessEntityID = cmd.CreateParameter();
            parBusinessEntityID.ParameterName = "@BusinessEntityID";
            parBusinessEntityID.Value = BusinessEntityID;
            cmdParams.Add(parBusinessEntityID);

            IDbDataParameter parDefaultLCID = cmd.CreateParameter();
            parDefaultLCID.ParameterName = "@DefaultLCID";
            parDefaultLCID.Value = DefaultLCID;
            cmdParams.Add(parDefaultLCID);
        }

        internal static void AddCommonParametersForGLDataIDAndRecPeriodID(long? GLDataID, int? RecPeriodID, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            AddCommonParametersForGLDataID(GLDataID, cmd, cmdParams);
            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);
        }

        internal static void AddCommonParametersForGLDataID(long? GLDataID, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            parGLDataID.Value = GLDataID.Value;
            cmdParams.Add(parGLDataID);
        }

        internal static void AddCommonParametersForAccountID(Int64? AccountID, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            parAccountID.Value = AccountID.Value;
            cmdParams.Add(parAccountID);
        }

        internal static void SetParamValueForNullableInt(IDbDataParameter param, Int32? intValue)
        {
            if (intValue.HasValue)
                param.Value = intValue.Value;
            else
                param.Value = DBNull.Value;
        }

        internal static void SetParamValueForNullableDateTime(IDbDataParameter param, DateTime? dateValue)
        {
            if (dateValue.HasValue)
                param.Value = dateValue.Value;
            else
                param.Value = DBNull.Value;
        }

        internal static void AddCommonParametersForRecPeriodID(Int32? recPeriodID, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@RecPeriodID";
            SetParamValueForNullableInt(param, recPeriodID);
            cmdParams.Add(param);
        }

        internal static void AddCommonParametersForCompanyID(Int32? companyID, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@CompanyID";
            SetParamValueForNullableInt(param, companyID);
            cmdParams.Add(param);
        }

        internal static void AddCommonParametersForAddedBy(string addedBy, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@AddedBy";
            param.Value = addedBy;
            cmdParams.Add(param);
        }

        internal static void AddCommonParametersForDateAdded(DateTime? dateAdded, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@DateAdded";
            SetParamValueForNullableDateTime(param, dateAdded);
            cmdParams.Add(param);
        }

        internal static void AddCommonParametersForAddedUserID(Int32? addedByUserID, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@AddedByUserID";
            SetParamValueForNullableInt(param, addedByUserID);
            cmdParams.Add(param);
        }

        internal static void AddCommonParametersForRevisedBy(string revisedBy, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@RevisedBy";
            param.Value = revisedBy;
            cmdParams.Add(param);
        }

        internal static void AddCommonParametersForDateRevised(DateTime? dateRevised, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = "@DateRevised";
            SetParamValueForNullableDateTime(param, dateRevised);
            cmdParams.Add(param);
        }

        internal static void AddCommonUserRoleAndRecPeriodParameters(int? UserID, short? RoleID, int? RecPeriodID, System.Data.IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            AddCommonUserAndRoleParameters(UserID, RoleID, cmd, cmdParams);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;

            cmdParams.Add(parRecPeriodID);
        }

        internal static void AddCommonUserAndRoleParameters(int? UserID, short? RoleID, System.Data.IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (UserID.HasValue)
                parUserID.Value = UserID.Value;
            else
                parUserID.Value = DBNull.Value;

            cmdParams.Add(parUserID);
            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (RoleID.HasValue)
                parRoleID.Value = RoleID.Value;
            else
                parRoleID.Value = DBNull.Value;
            cmdParams.Add(parRoleID);
        }

        internal static void AddCommonParametersForActionTypeID(short actionTypeID, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter cmdParamActionTypeID = cmd.CreateParameter();
            cmdParamActionTypeID.ParameterName = "@ActionTypeID";
            cmdParamActionTypeID.Value = actionTypeID;
            cmdParams.Add(cmdParamActionTypeID);
        }

        internal static void AddCommonParametersForChangeSourceIDSRA(short changeSourceIDSRA, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter cmdParamChangeSourceIDSRA = cmd.CreateParameter();
            cmdParamChangeSourceIDSRA.ParameterName = "@ChangeSourceIDSRA";
            cmdParamChangeSourceIDSRA.Value = changeSourceIDSRA;
            cmdParams.Add(cmdParamChangeSourceIDSRA);
        }

        internal static void AddCommonUserRoleAndRecPeriodParameters(ParamInfoBase oParamInfoBase, System.Data.IDbCommand cmd)
        {
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = oParamInfoBase.UserID.Value;
            cmdParams.Add(parUserID);

            AddCommonRoleAndRecPeriodParameters(oParamInfoBase, cmd);
        }

        internal static void AddCommonRoleAndRecPeriodParameters(ParamInfoBase oParamInfoBase, System.Data.IDbCommand cmd)
        {
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = oParamInfoBase.RoleID.Value;
            cmdParams.Add(parRoleID);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (oParamInfoBase.RecPeriodID != null)
            {
                parRecPeriodID.Value = oParamInfoBase.RecPeriodID.Value;
            }
            else
            {
                parRecPeriodID.Value = DBNull.Value;
            }
            cmdParams.Add(parRecPeriodID);
        }

        internal static void AddCommonParametersForAccountEntitySearchInReport(ReportSearchCriteria oReportSearchCriteria, bool isAccountRangeSearch, IDbCommand cmd, IDataParameterCollection cmdParams)
        {

            //IDbDataParameter cmdTableKeyValue = cmd.CreateParameter();
            //cmdTableKeyValue.ParameterName = "@tblKeyValue";
            //cmdTableKeyValue.Value = tblEntitySearch;
            //cmdParams.Add(cmdTableKeyValue);

            IDbDataParameter cmdRequesterUserID = cmd.CreateParameter();
            cmdRequesterUserID.ParameterName = "@RequesterUserID";
            cmdRequesterUserID.Value = ReturnDBNullWhenNull(oReportSearchCriteria.RequesterUserID);
            cmdParams.Add(cmdRequesterUserID);

            IDbDataParameter cmdRequesterRoleID = cmd.CreateParameter();
            cmdRequesterRoleID.ParameterName = "@RequesterRoleID";
            cmdRequesterRoleID.Value = ReturnDBNullWhenNull(oReportSearchCriteria.RequesterRoleID);
            cmdParams.Add(cmdRequesterRoleID);


            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@ReconciliationPeriodID";
            cmdRecPeriod.Value = ReturnDBNullWhenNull(oReportSearchCriteria.ReconciliationPeriodID);
            cmdParams.Add(cmdRecPeriod);

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = ReturnDBNullWhenNull(oReportSearchCriteria.CompanyID);
            cmdParams.Add(cmdCompanyId);

            if (isAccountRangeSearch)
            {
                IDbDataParameter cmdFromAccountNumber = cmd.CreateParameter();
                cmdFromAccountNumber.ParameterName = "@FromAccountNumber";
                if (!string.IsNullOrEmpty(oReportSearchCriteria.FromAccountNumber))
                    cmdFromAccountNumber.Value = oReportSearchCriteria.FromAccountNumber;
                else
                    cmdFromAccountNumber.Value = DBNull.Value;
                cmdParams.Add(cmdFromAccountNumber);

                IDbDataParameter cmdToAccountNumber = cmd.CreateParameter();
                cmdToAccountNumber.ParameterName = "@ToAccountNumber";
                if (!string.IsNullOrEmpty(oReportSearchCriteria.ToAccountNumber))
                    cmdToAccountNumber.Value = oReportSearchCriteria.ToAccountNumber;
                else
                    cmdToAccountNumber.Value = DBNull.Value;
                cmdParams.Add(cmdToAccountNumber);
            }
        }

        internal static void AddCommonParametersForUserRoleSearchInReport(DataTable tblUserSearch, DataTable tblRoleSearch, IDbCommand cmd, IDataParameterCollection cmdParams)
        {
            IDbDataParameter cmdTableUserIDs = cmd.CreateParameter();
            cmdTableUserIDs.ParameterName = "@tblUserIDs";
            cmdTableUserIDs.Value = tblUserSearch;
            cmdParams.Add(cmdTableUserIDs);

            IDbDataParameter cmdTableRoleIDs = cmd.CreateParameter();
            cmdTableRoleIDs.ParameterName = "@tblRoleIDs";
            cmdTableRoleIDs.Value = tblRoleSearch;
            cmdParams.Add(cmdTableRoleIDs);
        }

        #endregion



        internal static DataTable ConvertAttachmentInfoCollectionToDataTable(List<AttachmentInfo> oAttachmentInfoCollection)
        {
            DataTable dt = null;

            if (oAttachmentInfoCollection != null && oAttachmentInfoCollection.Count > 0)
            {
                dt = new DataTable("udt_AttachmentTableType");
                DataColumn dc1 = dt.Columns.Add("RecordID");
                DataColumn dc2 = dt.Columns.Add("FileName");
                DataColumn dc3 = dt.Columns.Add("PhysicalPath");
                DataColumn dc4 = dt.Columns.Add("FileSize");
                DataColumn dc5 = dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));
                DataColumn dc6 = dt.Columns.Add("UserID");
                DataColumn dc7 = dt.Columns.Add("Comments");
                DataColumn dc8 = dt.Columns.Add("IsPermanentOrTemporary");
                DataColumn dc9 = dt.Columns.Add("RecordTypeID");
                DataColumn dc10 = dt.Columns.Add("DocumentName");
                DataColumn dc11 = dt.Columns.Add("IsActive");

                DataRow dr;

                for (int i = 0; i < oAttachmentInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["RecordID"] = oAttachmentInfoCollection[i].RecordID;
                    dr["FileName"] = oAttachmentInfoCollection[i].FileName;
                    dr["PhysicalPath"] = oAttachmentInfoCollection[i].PhysicalPath;
                    dr["FileSize"] = oAttachmentInfoCollection[i].FileSize;
                    dr["Date"] = oAttachmentInfoCollection[i].Date;
                    dr["UserID"] = oAttachmentInfoCollection[i].UserID;
                    dr["Comments"] = oAttachmentInfoCollection[i].Comments;
                    dr["IsPermanentOrTemporary"] = oAttachmentInfoCollection[i].IsPermanentOrTemporary;
                    dr["RecordTypeID"] = oAttachmentInfoCollection[i].RecordTypeID;
                    dr["DocumentName"] = oAttachmentInfoCollection[i].DocumentName;
                    dr["IsActive"] = oAttachmentInfoCollection[i].IsActive;

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertCompanySRARuleInfoCollectionToDataTable(IList<CompanySystemReconciliationRuleInfo> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("CapabilityID");
                DataColumn dc2 = dt.Columns.Add("IsActivated");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i].SystemReconciliationRuleID;
                    dr[1] = oIDCollection[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertCompanyAlertInfoCollectionToDataTable(List<CompanyAlertInfo> oCompanyAlertInfoCollection)
        {
            DataTable dtCompanyAlert = new DataTable("udt_CompanyAlertType");
            DataColumn dc1 = dtCompanyAlert.Columns.Add("CompanyID");
            DataColumn dc2 = dtCompanyAlert.Columns.Add("AlertID");
            DataColumn dc3 = dtCompanyAlert.Columns.Add("Threshold");
            DataColumn dc4 = dtCompanyAlert.Columns.Add("ThresholdTypeID");
            DataColumn dc5 = dtCompanyAlert.Columns.Add("NoOfHours");
            DataColumn dc6 = dtCompanyAlert.Columns.Add("IsActive");
            DataColumn dc7 = dtCompanyAlert.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            DataColumn dc8 = dtCompanyAlert.Columns.Add("AddedBy");
            DataRow dr;
            for (int i = 0; i < oCompanyAlertInfoCollection.Count; i++)
            {
                dr = dtCompanyAlert.NewRow();
                dr["CompanyID"] = oCompanyAlertInfoCollection[i].CompanyID;
                dr["AlertID"] = oCompanyAlertInfoCollection[i].AlertID;

                if (oCompanyAlertInfoCollection[i].Threshold.HasValue && oCompanyAlertInfoCollection[i].Threshold.Value > 0)
                {
                    dr["Threshold"] = oCompanyAlertInfoCollection[i].Threshold;
                }
                else
                {
                    dr["Threshold"] = DBNull.Value;
                }

                if (oCompanyAlertInfoCollection[i].ThresholdTypeID.HasValue && oCompanyAlertInfoCollection[i].ThresholdTypeID.Value > 0)
                {
                    dr["ThresholdTypeID"] = oCompanyAlertInfoCollection[i].ThresholdTypeID;
                }
                else
                {
                    dr["ThresholdTypeID"] = DBNull.Value;
                }

                if (oCompanyAlertInfoCollection[i].NoOfHours.HasValue && oCompanyAlertInfoCollection[i].NoOfHours.Value > 0)
                {
                    dr["NoOfHours"] = oCompanyAlertInfoCollection[i].NoOfHours;
                }
                else
                {
                    dr["NoOfHours"] = DBNull.Value;
                }
                dr["IsActive"] = oCompanyAlertInfoCollection[i].IsActive;
                dr["DateAdded"] = oCompanyAlertInfoCollection[i].DateAdded;
                dr["AddedBy"] = oCompanyAlertInfoCollection[i].AddedBy;
                dtCompanyAlert.Rows.Add(dr);
            }

            return dtCompanyAlert;
        }


        internal static DataTable ConvertCompanyAlertDetailInfoCollectionToDataTable(List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection)
        {
            DataTable dtCompanyAlert = new DataTable("udt_CompanyAlertDetailType");
            DataColumn dc1 = dtCompanyAlert.Columns.Add("CompanyAlertID");
            DataColumn dc2 = dtCompanyAlert.Columns.Add("RecPeriodID");
            DataColumn dc3 = dtCompanyAlert.Columns.Add("Description");
            DataColumn dc4 = dtCompanyAlert.Columns.Add("AlertExpectedDateTime", System.Type.GetType("System.DateTime"));
            DataColumn dc5 = dtCompanyAlert.Columns.Add("URL");
            DataColumn dc6 = dtCompanyAlert.Columns.Add("UserID");
            DataColumn dc7 = dtCompanyAlert.Columns.Add("RoleID");
            DataColumn dc9 = dtCompanyAlert.Columns.Add("AddedBy");
            DataColumn dc8 = dtCompanyAlert.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            DataColumn dc10 = dtCompanyAlert.Columns.Add("AccountID");
            DataColumn dc11 = dtCompanyAlert.Columns.Add("NetAccountID");
            DataColumn dc12 = dtCompanyAlert.Columns.Add("TaskID");
            DataColumn dc13 = dtCompanyAlert.Columns.Add("TaskDetailID");
            DataColumn dc14 = dtCompanyAlert.Columns.Add("NumberValue");
            DataColumn dc15 = dtCompanyAlert.Columns.Add("DateValue", System.Type.GetType("System.DateTime"));
            DataRow dr;
            for (int i = 0; i < oCompanyAlertDetailInfoCollection.Count; i++)
            {
                dr = dtCompanyAlert.NewRow();
                dr["CompanyAlertID"] = oCompanyAlertDetailInfoCollection[i].CompanyAlertID;

                if (oCompanyAlertDetailInfoCollection[i].ReconciliationPeriodID.HasValue && oCompanyAlertDetailInfoCollection[i].ReconciliationPeriodID.Value > 0)
                {
                    dr["RecPeriodID"] = oCompanyAlertDetailInfoCollection[i].ReconciliationPeriodID;
                }
                else
                {
                    dr["RecPeriodID"] = DBNull.Value;
                }

                dr["Description"] = oCompanyAlertDetailInfoCollection[i].AlertDescription;

                if (oCompanyAlertDetailInfoCollection[i].AlertExpectedDateTime.HasValue && oCompanyAlertDetailInfoCollection[i].AlertExpectedDateTime.Value > DateTime.MinValue)
                {
                    dr["AlertExpectedDateTime"] = oCompanyAlertDetailInfoCollection[i].AlertExpectedDateTime;
                }
                else
                {
                    dr["AlertExpectedDateTime"] = DBNull.Value;
                }

                if (!string.IsNullOrEmpty(oCompanyAlertDetailInfoCollection[i].Url))
                {
                    dr["URL"] = oCompanyAlertDetailInfoCollection[i].Url;
                }
                else
                {
                    dr["URL"] = DBNull.Value;
                }
                dr["UserID"] = oCompanyAlertDetailInfoCollection[i].UserID;
                dr["RoleID"] = oCompanyAlertDetailInfoCollection[i].RoleID;
                dr["AddedBy"] = oCompanyAlertDetailInfoCollection[i].AddedBy;
                dr["DateAdded"] = oCompanyAlertDetailInfoCollection[i].DateAdded;
                dr["AccountID"] = DBNull.Value;
                dr["NetAccountID"] = DBNull.Value;
                dr["TaskID"] = DBNull.Value;
                dr["TaskDetailID"] = DBNull.Value;
                dr["NumberValue"] = DBNull.Value;
                dr["DateValue"] = DBNull.Value;
                dtCompanyAlert.Rows.Add(dr);
            }

            return dtCompanyAlert;
        }

        internal static object ReturnDBNullWhenNull(object o)
        {
            if (o != null)
            {
                return o;
            }
            else
            {
                return DBNull.Value;
            }
        }
        public static DataTable ConvertCompanyAlertDetailUserToDataTable(List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList)
        {
            DataTable dt = null;
            if (oCompanyAlertDetailUserInfoList != null && oCompanyAlertDetailUserInfoList.Count > 0)
            {
                dt = new DataTable("udt_BigIntIDTableType");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oCompanyAlertDetailUserInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oCompanyAlertDetailUserInfoList[i].CompanyAlertDetailUserID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        internal static DataTable ConvertRptCriteriaDictionaryToDataTable(int userMySaveReportId, Dictionary<string, string> oRptCriteriaCollection)
        {
            DataTable dt = null;

            if (oRptCriteriaCollection != null && oRptCriteriaCollection.Count > 0)
            {
                dt = new DataTable("RptCriteriaCollectionTableType");
                DataColumn dc = dt.Columns.Add("UserMyReportSavedReportID");
                DataColumn dc1 = dt.Columns.Add("ParameterID");
                DataColumn dc2 = dt.Columns.Add("ParameterValue");
                DataRow dr;


                foreach (KeyValuePair<string, string> kvp in oRptCriteriaCollection)
                {
                    if (kvp.Key != null)
                    {
                        dr = dt.NewRow();
                        dr[0] = userMySaveReportId;
                        dr[1] = 1;
                        dr[2] = kvp.Value;
                        dt.Rows.Add(dr);
                    }

                }

            }
            return dt;
        }


        internal static DataTable ConvertStringIDListToDataTable(string oStringIDList)
        {
            DataTable dt = null;
            if (!string.IsNullOrEmpty(oStringIDList))
            {
                String[] oIDCollection = oStringIDList.Split(FilterValueSeparator.ToCharArray());
                if (oIDCollection != null && oIDCollection.Length > 0)
                {
                    dt = new DataTable("IDTable");
                    DataColumn dc = dt.Columns.Add("ID");
                    DataRow dr;
                    for (int i = 0; i < oIDCollection.Length; i++)
                    {
                        dr = dt.NewRow();
                        dr[0] = oIDCollection[i];
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        internal static DataTable ConvertFilterCriteriaIntoDataTable(List<FilterCriteria> oFilterCriteriaCollection)
        {
            DataTable dtFilterCriteria = new DataTable("udt_FilterCriteriaTableType");
            DataColumn dc1 = dtFilterCriteria.Columns.Add("ParameterID");
            DataColumn dc2 = dtFilterCriteria.Columns.Add("OperatorID");
            DataColumn dc3 = dtFilterCriteria.Columns.Add("Value");
            DataRow dr;

            if (oFilterCriteriaCollection != null)
            {
                for (int i = 0; i < oFilterCriteriaCollection.Count; i++)
                {
                    dr = dtFilterCriteria.NewRow();
                    dr["ParameterID"] = oFilterCriteriaCollection[i].ParameterID;
                    dr["OperatorID"] = oFilterCriteriaCollection[i].OperatorID;
                    dr["Value"] = oFilterCriteriaCollection[i].Value;

                    dtFilterCriteria.Rows.Add(dr);
                }
            }

            return dtFilterCriteria;
        }


        internal static DataTable ConvertCompanyWorkWeekInfoToDataTable(List<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection)
        {


            DataTable dt = null;
            if (oCompanyWorkWeekInfoCollection != null && oCompanyWorkWeekInfoCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("WeekDayID");
                DataColumn dc2 = dt.Columns.Add("IsActivated");
                DataRow dr;
                for (int i = 0; i < oCompanyWorkWeekInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oCompanyWorkWeekInfoCollection[i].WeekDayID;
                    dr[1] = Convert.ToBoolean(1);
                    dt.Rows.Add(dr);
                }
            }
            return dt;

        }

        internal static DataTable ConvertCompanyGLToolColumnInfoToDataTable(List<CompanyGLToolColumnInfo> oCompanyGLToolColumnInfoCollection)
        {


            DataTable dt = null;
            if (oCompanyGLToolColumnInfoCollection != null && oCompanyGLToolColumnInfoCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("GLToolColumnName");
                DataColumn dc2 = dt.Columns.Add("GLToolColumnLength");
                DataRow dr;
                for (int i = 0; i < oCompanyGLToolColumnInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oCompanyGLToolColumnInfoCollection[i].GLToolColumnName;
                    dr[1] = oCompanyGLToolColumnInfoCollection[i].GLToolColumnLength;
                    dt.Rows.Add(dr);
                }
            }
            return dt;

        }

        internal static DataTable ConvertCompanyJEWriteOffOnApproverInfoToDataTable(List<CompanyJEWriteOffOnApproverInfo> oCompanyJEWriteOffOnApproverInfoCollection)
        {


            DataTable dt = null;
            if (oCompanyJEWriteOffOnApproverInfoCollection != null && oCompanyJEWriteOffOnApproverInfoCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("FromAmount", typeof(System.Decimal));
                DataColumn dc2 = dt.Columns.Add("ToAmount", typeof(System.Decimal));
                DataColumn dc3 = dt.Columns.Add("PrimaryApproverUserID");
                DataColumn dc4 = dt.Columns.Add("SecondaryApproverUserID");
                DataRow dr;
                for (int i = 0; i < oCompanyJEWriteOffOnApproverInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = (decimal)oCompanyJEWriteOffOnApproverInfoCollection[i].FromAmount;
                    dr[1] = (decimal)oCompanyJEWriteOffOnApproverInfoCollection[i].ToAmount;
                    dr[2] = oCompanyJEWriteOffOnApproverInfoCollection[i].PrimaryApproverUserID;
                    dr[3] = oCompanyJEWriteOffOnApproverInfoCollection[i].SecondaryApproverUserID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;

        }



        internal static DataTable ConvertUserGeographyObjectInfoCollectionToDataTable(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection)
        {
            DataTable dt = null;

            if (oUserGeographyObjectInfoCollection != null && oUserGeographyObjectInfoCollection.Count > 0)
            {
                dt = new DataTable("UserGeographyObjectTableType");
                DataColumn dc1 = dt.Columns.Add("UserID");
                DataColumn dc2 = dt.Columns.Add("GeographyObjectID");
                DataColumn dc3 = dt.Columns.Add("IsActive");
                DataColumn dc4 = dt.Columns.Add("KeySize");
                DataColumn dc5 = dt.Columns.Add("RoleID");


                DataRow dr;

                for (int i = 0; i < oUserGeographyObjectInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();

                    dr["UserID"] = oUserGeographyObjectInfoCollection[i].UserID;
                    dr["GeographyObjectID"] = oUserGeographyObjectInfoCollection[i].GeographyObjectID;
                    dr["IsActive"] = oUserGeographyObjectInfoCollection[i].IsActive;
                    dr["KeySize"] = oUserGeographyObjectInfoCollection[i].KeySize;
                    dr["RoleID"] = oUserGeographyObjectInfoCollection[i].RoleID;

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertUserAccountObjectInfoCollectionToDataTable(List<UserAccountInfo> oUserAccountInfoCollection)
        {
            DataTable dt = null;

            if (oUserAccountInfoCollection != null && oUserAccountInfoCollection.Count > 0)
            {
                dt = new DataTable("UserAccountTableType");
                DataColumn dc1 = dt.Columns.Add("UserID");
                DataColumn dc2 = dt.Columns.Add("AccountID");
                DataColumn dc3 = dt.Columns.Add("IsActive");
                DataColumn dc4 = dt.Columns.Add("RoleID");
                DataRow dr;
                for (int i = 0; i < oUserAccountInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();

                    dr["UserID"] = oUserAccountInfoCollection[i].UserID;
                    dr["AccountID"] = oUserAccountInfoCollection[i].AccountID;
                    dr["IsActive"] = oUserAccountInfoCollection[i].IsActive;
                    dr["RoleID"] = oUserAccountInfoCollection[i].RoleID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        internal static DataTable ConvertGLDataRecurringItemScheduleIntervalDetailToDataTable(IList<GLDataRecurringItemScheduleIntervalDetailInfo> GLDataRecurringItemScheduleIntervalDetailInfoList)
        {
            DataTable dt = null;

            if (GLDataRecurringItemScheduleIntervalDetailInfoList != null && GLDataRecurringItemScheduleIntervalDetailInfoList.Count > 0)
            {
                dt = new DataTable("GLDataRecurringItemScheduleIntervalDetailTableType");
                DataColumn dc1 = dt.Columns.Add("ID");
                DataColumn dc2 = dt.Columns.Add("Amount", typeof(System.Decimal));
                DataColumn dc3 = dt.Columns.Add("SystemAmount", typeof(System.Decimal));

                DataRow dr;
                for (int i = 0; i < GLDataRecurringItemScheduleIntervalDetailInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["ID"] = GLDataRecurringItemScheduleIntervalDetailInfoList[i].ReconciliationPeriodID;
                    dr["Amount"] = GLDataRecurringItemScheduleIntervalDetailInfoList[i].IntervalAmount;
                    dr["SystemAmount"] = GLDataRecurringItemScheduleIntervalDetailInfoList[i].SystemIntervalAmount;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertGLDataRecurringItemScheduleIntervalDetailToDataTable(IList<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoList)
        {
            DataTable dt = null;

            if (oGLDataRecurringItemScheduleInfoList != null && oGLDataRecurringItemScheduleInfoList.Count > 0)
            {
                dt = new DataTable("GLDataRecurringItemScheduleIntervalDetailTableType");
                dt.Columns.Add("ID", typeof(System.Int64));
                dt.Columns.Add("GLDataRecurringItemScheduleID", typeof(System.Int64));
                dt.Columns.Add("ReconciliationPeriodID", typeof(System.Int32));
                dt.Columns.Add("IntervalAmount", typeof(System.Decimal));
                dt.Columns.Add("SystemIntervalAmount", typeof(System.Decimal));
                dt.Columns.Add("RowNumber", typeof(System.Int64));
                DataRow dr;
                for (int i = 0; i < oGLDataRecurringItemScheduleInfoList.Count; i++)
                {
                    GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = oGLDataRecurringItemScheduleInfoList[i];
                    for (int j = 0; j < oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList.Count; j++)
                    {
                        dr = dt.NewRow();
                        dr["ID"] = ReturnDBNullWhenNull(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList[j].GLDataRecurringItemScheduleIntervalDetailID);
                        dr["GLDataRecurringItemScheduleID"] = ReturnDBNullWhenNull(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID);
                        dr["ReconciliationPeriodID"] = oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList[j].ReconciliationPeriodID;
                        dr["IntervalAmount"] = ReturnDBNullWhenNull(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList[j].IntervalAmount);
                        dr["SystemIntervalAmount"] = ReturnDBNullWhenNull(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList[j].SystemIntervalAmount);
                        dr["RowNumber"] = oGLDataRecurringItemScheduleInfo.ExcelRowNumber;
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        #region For Matching Table
        internal static DataTable ConvertMatchingSourceDataImportHdrToDataTable(IList<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoCollection)
        {
            DataTable dt = null;

            if (oMatchingSourceDataImportHdrInfoCollection != null && oMatchingSourceDataImportHdrInfoCollection.Count > 0)
            {
                dt = new DataTable("MatchingSourceDataImportHdrToDataTable");
                DataColumn dc1 = dt.Columns.Add("MatchingSourceName");
                DataColumn dc2 = dt.Columns.Add("FileName");
                DataColumn dc3 = dt.Columns.Add("PhysicalPath");
                DataColumn dc4 = dt.Columns.Add("FileSize");
                DataColumn dc5 = dt.Columns.Add("MatchingSourceTypeID");
                DataColumn dc6 = dt.Columns.Add("RecPeriodID");
                DataColumn dc7 = dt.Columns.Add("DataImportStatusID");
                DataColumn dc8 = dt.Columns.Add("UserID");
                DataColumn dc9 = dt.Columns.Add("RoleID");
                DataColumn dc10 = dt.Columns.Add("LanguageID");
                DataColumn dc11 = dt.Columns.Add("ForceCommitDate", typeof(System.DateTime));
                DataColumn dc12 = dt.Columns.Add("IsForceCommit");
                DataColumn dc13 = dt.Columns.Add("IsActive");
                DataColumn dc14 = dt.Columns.Add("DateAdded", typeof(System.DateTime));
                DataColumn dc15 = dt.Columns.Add("AddedBy");
                DataRow dr;

                for (int i = 0; i < oMatchingSourceDataImportHdrInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["MatchingSourceName"] = oMatchingSourceDataImportHdrInfoCollection[i].MatchingSourceName;
                    dr["FileName"] = oMatchingSourceDataImportHdrInfoCollection[i].FileName;
                    dr["PhysicalPath"] = oMatchingSourceDataImportHdrInfoCollection[i].PhysicalPath;
                    dr["FileSize"] = oMatchingSourceDataImportHdrInfoCollection[i].FileSize;
                    dr["MatchingSourceTypeID"] = oMatchingSourceDataImportHdrInfoCollection[i].MatchingSourceTypeID;
                    dr["RecPeriodID"] = oMatchingSourceDataImportHdrInfoCollection[i].RecPeriodID;
                    dr["DataImportStatusID"] = oMatchingSourceDataImportHdrInfoCollection[i].DataImportStatusID;
                    dr["UserID"] = oMatchingSourceDataImportHdrInfoCollection[i].UserID;
                    dr["RoleID"] = oMatchingSourceDataImportHdrInfoCollection[i].RoleID;
                    dr["LanguageID"] = oMatchingSourceDataImportHdrInfoCollection[i].LanguageID;

                    if (oMatchingSourceDataImportHdrInfoCollection[i].ForceCommitDate.HasValue)
                    {
                        dr["ForceCommitDate"] = oMatchingSourceDataImportHdrInfoCollection[i].ForceCommitDate;
                    }
                    else
                    {
                        dr["ForceCommitDate"] = DBNull.Value;
                    }
                    if (oMatchingSourceDataImportHdrInfoCollection[i].IsForceCommit.HasValue)
                    {
                        dr["IsForceCommit"] = oMatchingSourceDataImportHdrInfoCollection[i].IsForceCommit;
                    }
                    else
                    {
                        dr["IsForceCommit"] = DBNull.Value;
                    }

                    dr["DateAdded"] = oMatchingSourceDataImportHdrInfoCollection[i].DateAdded;
                    dr["AddedBy"] = oMatchingSourceDataImportHdrInfoCollection[i].AddedBy;
                    dr["IsActive"] = oMatchingSourceDataImportHdrInfoCollection[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        internal static DataTable ConvertMatchingSourceColumnToDataTable(IList<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection)
        {
            DataTable dt = null;

            if (oMatchingSourceColumnInfoCollection != null && oMatchingSourceColumnInfoCollection.Count > 0)
            {
                dt = new DataTable("MatchingSourceColumn");
                DataColumn dc1 = dt.Columns.Add("MatchingSourceColumnID");
                DataColumn dc2 = dt.Columns.Add("MatchingSourceDataImportID");
                DataColumn dc3 = dt.Columns.Add("ColumnName");
                DataColumn dc4 = dt.Columns.Add("DataTypeID");
                DataRow dr;

                for (int i = 0; i < oMatchingSourceColumnInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["MatchingSourceColumnID"] = oMatchingSourceColumnInfoCollection[i].MatchingSourceColumnID;
                    dr["MatchingSourceDataImportID"] = oMatchingSourceColumnInfoCollection[i].MatchingSourceDataImportID;
                    dr["ColumnName"] = oMatchingSourceColumnInfoCollection[i].ColumnName;
                    dr["DataTypeID"] = oMatchingSourceColumnInfoCollection[i].DataTypeID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertMatchSetMatchingSourceToDataTable(IList<MatchSetMatchingSourceDataImportInfo> oMatchSetMatchingSourceDataImportInfoCollection)
        {
            DataTable dt = null;

            if (oMatchSetMatchingSourceDataImportInfoCollection != null && oMatchSetMatchingSourceDataImportInfoCollection.Count > 0)
            {
                dt = new DataTable("MatchingSourceColumn");
                DataColumn dc1 = dt.Columns.Add("MatchSetMatchingSourceDataImportID");
                DataColumn dc2 = dt.Columns.Add("MatchSetID");
                DataColumn dc3 = dt.Columns.Add("MatchingSourceDataImportID");
                DataColumn dc4 = dt.Columns.Add("SubSetName");
                DataColumn dc5 = dt.Columns.Add("SubSetData");
                DataRow dr;

                for (int i = 0; i < oMatchSetMatchingSourceDataImportInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["MatchSetMatchingSourceDataImportID"] = oMatchSetMatchingSourceDataImportInfoCollection[i].MatchSetMatchingSourceDataImportID;
                    dr["MatchSetID"] = oMatchSetMatchingSourceDataImportInfoCollection[i].MatchSetID;
                    dr["MatchingSourceDataImportID"] = oMatchSetMatchingSourceDataImportInfoCollection[i].MatchingSourceDataImportID;
                    dr["SubSetName"] = oMatchSetMatchingSourceDataImportInfoCollection[i].SubSetName;
                    dr["SubSetData"] = oMatchSetMatchingSourceDataImportInfoCollection[i].SubSetData;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertMatchSetSubSetCombinationToDataTable(IList<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection)
        {
            DataTable dt = null;

            if (oMatchSetSubSetCombinationInfoCollection != null && oMatchSetSubSetCombinationInfoCollection.Count > 0)
            {
                dt = new DataTable("MatchSetSubSetCombination");
                DataColumn dc = dt.Columns.Add("MatchSetSubSetCombinationID");
                DataColumn dc1 = dt.Columns.Add("MatchSetMatchingSourceDataImport1ID");
                DataColumn dc2 = dt.Columns.Add("MatchSetMatchingSourceDataImport2ID");
                DataColumn dc3 = dt.Columns.Add("MatchSetSubSetCombinationName");
                DataColumn dc4 = dt.Columns.Add("IsConfigurationComplete");
                DataColumn dc5 = dt.Columns.Add("IsActive");
                DataColumn dc6 = dt.Columns.Add("DateAdded", typeof(System.DateTime));
                DataColumn dc7 = dt.Columns.Add("AddedBy");
                DataColumn dc8 = dt.Columns.Add("DateRevised", typeof(System.DateTime));
                DataColumn dc9 = dt.Columns.Add("RevisedBy");
                DataRow dr;

                for (int i = 0; i < oMatchSetSubSetCombinationInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["MatchSetSubSetCombinationID"] = oMatchSetSubSetCombinationInfoCollection[i].MatchSetSubSetCombinationID;
                    dr["MatchSetMatchingSourceDataImport1ID"] = oMatchSetSubSetCombinationInfoCollection[i].MatchSetMatchingSourceDataImport1ID;
                    dr["MatchSetMatchingSourceDataImport2ID"] = oMatchSetSubSetCombinationInfoCollection[i].MatchSetMatchingSourceDataImport2ID;
                    dr["MatchSetSubSetCombinationName"] = oMatchSetSubSetCombinationInfoCollection[i].MatchSetSubSetCombinationName;
                    dr["IsConfigurationComplete"] = oMatchSetSubSetCombinationInfoCollection[i].IsConfigurationComplete;
                    dr["IsActive"] = oMatchSetSubSetCombinationInfoCollection[i].IsActive;
                    if (oMatchSetSubSetCombinationInfoCollection[i].DateAdded.HasValue)
                        dr["DateAdded"] = oMatchSetSubSetCombinationInfoCollection[i].DateAdded;
                    dr["AddedBy"] = oMatchSetSubSetCombinationInfoCollection[i].AddedBy;
                    if (oMatchSetSubSetCombinationInfoCollection[i].DateRevised.HasValue)
                        dr["DateRevised"] = oMatchSetSubSetCombinationInfoCollection[i].DateRevised;
                    dr["RevisedBy"] = oMatchSetSubSetCombinationInfoCollection[i].RevisedBy;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertMatchingParamInfoTORecItemColumnMappingDataTable(IList<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection, bool ForClean)
        {
            DataTable dt = null;
            dt = new DataTable("RecItemColumnMapping");

            if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
            {
                DataColumn dc1 = dt.Columns.Add("MatchingConfigurationID");
                if (!ForClean)
                {
                    DataColumn dc2 = dt.Columns.Add("RecItemColumnID");
                }
                DataRow dr;
                for (int i = 0; i < oMatchingConfigurationInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["MatchingConfigurationID"] = oMatchingConfigurationInfoCollection[i].MatchingConfigurationID;
                    if (!ForClean)
                        dr["RecItemColumnID"] = oMatchingConfigurationInfoCollection[i].RecItemColumnID;
                    if (ForClean || oMatchingConfigurationInfoCollection[i].RecItemColumnID != null)
                        dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertMatchingConfigurationToDataTable(IList<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection, bool createChild, out DataTable dtConfigurationRule)
        {
            DataTable dtConfig = null;
            DataTable dtConfigRule = null;

            if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
            {
                // Create Configuration Table
                dtConfig = new DataTable("MatchingConfiguration");
                dtConfig.Columns.Add("MatchingConfigurationID");
                dtConfig.Columns.Add("MatchSetSubSetCombinationID");
                dtConfig.Columns.Add("MatchingSource1ColumnID");
                dtConfig.Columns.Add("MatchingSource2ColumnID");
                dtConfig.Columns.Add("IsMatching");
                dtConfig.Columns.Add("IsPartialMatching");
                dtConfig.Columns.Add("IsDisplayColumn");
                dtConfig.Columns.Add("DataTypeID");
                dtConfig.Columns.Add("DisplayColumnName");
                dtConfig.Columns.Add("IsAmountColumn");
                dtConfig.Columns.Add("SequenceNumber");


                if (createChild)
                {
                    // Create Configuration Rules Tables
                    dtConfigRule = new DataTable("MatchingConfigurationRule");
                    dtConfigRule.Columns.Add("MatchingConfigurationRuleID");
                    dtConfigRule.Columns.Add("MatchingConfigurationID");
                    dtConfigRule.Columns.Add("OperatorID");
                    dtConfigRule.Columns.Add("ThresholdTypeID");
                    dtConfigRule.Columns.Add("LowerBound");
                    dtConfigRule.Columns.Add("UpperBound");
                    dtConfigRule.Columns.Add("Keywords");
                    dtConfigRule.Columns.Add("SequenceNumber");
                }

                DataRow drConfig;

                DataRow drConfigRule;

                for (int i = 0; i < oMatchingConfigurationInfoCollection.Count; i++)
                {
                    // Parent Row
                    MatchingConfigurationInfo oMatchingConfigurationInfo = oMatchingConfigurationInfoCollection[i];
                    drConfig = dtConfig.NewRow();
                    drConfig["MatchingConfigurationID"] = oMatchingConfigurationInfo.MatchingConfigurationID;
                    drConfig["MatchSetSubSetCombinationID"] = oMatchingConfigurationInfo.MatchSetSubSetCombinationID;
                    if (oMatchingConfigurationInfo.MatchingSource1ColumnID.HasValue && oMatchingConfigurationInfo.MatchingSource1ColumnID.Value > 0)
                        drConfig["MatchingSource1ColumnID"] = oMatchingConfigurationInfo.MatchingSource1ColumnID;
                    if (oMatchingConfigurationInfo.MatchingSource2ColumnID.HasValue && oMatchingConfigurationInfo.MatchingSource2ColumnID.Value > 0)
                        drConfig["MatchingSource2ColumnID"] = oMatchingConfigurationInfo.MatchingSource2ColumnID;
                    drConfig["IsMatching"] = oMatchingConfigurationInfo.IsMatching;
                    drConfig["IsPartialMatching"] = oMatchingConfigurationInfo.IsPartialMatching;
                    drConfig["IsDisplayColumn"] = oMatchingConfigurationInfo.IsDisplayedColumn;
                    drConfig["DataTypeID"] = oMatchingConfigurationInfo.DataTypeID;
                    drConfig["DisplayColumnName"] = oMatchingConfigurationInfo.DisplayColumnName;
                    drConfig["IsAmountColumn"] = oMatchingConfigurationInfo.IsAmountColumn;
                    drConfig["SequenceNumber"] = i + 1;
                    dtConfig.Rows.Add(drConfig);

                    if (createChild)
                    {
                        List<MatchingConfigurationRuleInfo> oMatchingConfigurationRuleInfoList = oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection;
                        if (oMatchingConfigurationRuleInfoList != null && oMatchingConfigurationRuleInfoList.Count > 0)
                        {

                            for (int j = 0; j < oMatchingConfigurationRuleInfoList.Count; j++)
                            {
                                // Map Child Row
                                MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfo = oMatchingConfigurationRuleInfoList[j];
                                drConfigRule = dtConfigRule.NewRow();
                                if (oMatchingConfigurationRuleInfo.MatchingConfigurationRuleID != null)
                                    drConfigRule["MatchingConfigurationRuleID"] = oMatchingConfigurationRuleInfo.MatchingConfigurationRuleID;
                                else
                                    drConfigRule["MatchingConfigurationRuleID"] = 0;
                                // Map Configuration ID from Parent
                                drConfigRule["MatchingConfigurationID"] = oMatchingConfigurationInfo.MatchingConfigurationID;
                                drConfigRule["OperatorID"] = oMatchingConfigurationRuleInfo.OperatorID;
                                drConfigRule["ThresholdTypeID"] = oMatchingConfigurationRuleInfo.ThresholdTypeID;
                                drConfigRule["LowerBound"] = oMatchingConfigurationRuleInfo.LowerBound;
                                drConfigRule["UpperBound"] = oMatchingConfigurationRuleInfo.UpperBound;
                                drConfigRule["Keywords"] = oMatchingConfigurationRuleInfo.Keywords;
                                // Sequence Number should be same as in Parent
                                drConfigRule["SequenceNumber"] = i + 1;
                                dtConfigRule.Rows.Add(drConfigRule);
                            }
                        }
                    }
                }
            }
            dtConfigurationRule = dtConfigRule;
            return dtConfig;
        }


        internal static DataTable ConvertGLDataRecurringItemScheduleToDataTable(IList<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection)
        {
            DataTable dt = null;

            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                dt = new DataTable("GLDataRecurringItemSchedule");

                dt.Columns.Add("ID", typeof(System.Int64));
                dt.Columns.Add("GLDataRecurringItemScheduleID", typeof(System.Int64));
                dt.Columns.Add("GLDataID", typeof(System.Int64));
                dt.Columns.Add("ScheduleName", typeof(System.String));
                dt.Columns.Add("ScheduleBeginDate", typeof(System.DateTime));
                dt.Columns.Add("ScheduleEndDate", typeof(System.DateTime));
                dt.Columns.Add("ScheduleAmount", typeof(System.Decimal));
                dt.Columns.Add("ScheduleAmountBaseCurrency", typeof(System.Decimal));
                dt.Columns.Add("ScheduleAmountReportingCurrency", typeof(System.Decimal));
                dt.Columns.Add("LocalCurrencyCode", typeof(System.String));
                dt.Columns.Add("OriginalGLDataRecurringItemScheduleID", typeof(System.Int64));
                dt.Columns.Add("PreviousGLDataRecurringItemScheduleID", typeof(System.Int64));
                dt.Columns.Add("OpenDate", typeof(System.DateTime));
                dt.Columns.Add("CloseDate", typeof(System.DateTime));
                dt.Columns.Add("ReconciliationCategoryTypeID", typeof(System.Int16));
                dt.Columns.Add("RecPeriodAmountLocalCurrency", typeof(System.Decimal));
                dt.Columns.Add("RecPeriodAmountBaseCurrency", typeof(System.Decimal));
                dt.Columns.Add("RecPeriodAmountReportingCurrency", typeof(System.Decimal));
                dt.Columns.Add("BalanceLocalCurrency", typeof(System.Decimal));
                dt.Columns.Add("BalanceBaseCurrency", typeof(System.Decimal));
                dt.Columns.Add("BalanceReportingCurrency", typeof(System.Decimal));
                dt.Columns.Add("JournalEntryRef", typeof(System.String));
                dt.Columns.Add("Comments", typeof(System.String));
                dt.Columns.Add("CloseComments", typeof(System.String));
                dt.Columns.Add("RecItemNumber", typeof(System.String));
                dt.Columns.Add("DataImportID", typeof(System.Int32));
                dt.Columns.Add("CalculationFrequencyID", typeof(System.Int16));
                dt.Columns.Add("TotalIntervals", typeof(System.Int32));
                dt.Columns.Add("CurrentInterval", typeof(System.Int32));
                dt.Columns.Add("StartIntervalRecPeriodID", typeof(System.Int32));
                dt.Columns.Add("RecordSourceTypeID", typeof(System.Int16));
                dt.Columns.Add("RecordSourceID", typeof(System.Int64));
                dt.Columns.Add("ExRateLCCYtoBCCY", typeof(System.Decimal));
                dt.Columns.Add("ExRateLCCYtoRCCY", typeof(System.Decimal));
                dt.Columns.Add("IgnoreInCalculation", typeof(System.Boolean));
                dt.Columns.Add("IsActive", typeof(System.Boolean));
                dt.Columns.Add("DateAdded", typeof(System.DateTime));
                dt.Columns.Add("AddedBy", typeof(System.String));
                dt.Columns.Add("AddedByUserID", typeof(System.Int32));
                dt.Columns.Add("DateRevised", typeof(System.DateTime));
                dt.Columns.Add("RevisedBy", typeof(System.String));
                dt.Columns.Add("ExcelRowNumber", typeof(System.Int64));
                dt.Columns.Add("MatchSetMatchingSourceDataImportID", typeof(System.Int64));
                dt.Columns.Add("IndexID", typeof(System.Int16));
                DataRow dr;

                for (int i = 0; i < oGLDataRecurringItemScheduleInfoCollection.Count; i++)
                {
                    GLDataRecurringItemScheduleInfo oItem = oGLDataRecurringItemScheduleInfoCollection[i];
                    dr = dt.NewRow();

                    dr["ID"] = i + 1;
                    dr["GLDataRecurringItemScheduleID"] = ReturnDBNullWhenNull(oItem.GLDataRecurringItemScheduleID);
                    dr["GLDataID"] = oItem.GLDataID;
                    dr["ScheduleName"] = oItem.ScheduleName;
                    dr["ScheduleBeginDate"] = oItem.ScheduleBeginDate;
                    dr["ScheduleEndDate"] = oItem.ScheduleEndDate;
                    dr["ScheduleAmount"] = oItem.ScheduleAmount;
                    dr["ScheduleAmountBaseCurrency"] = ReturnDBNullWhenNull(oItem.ScheduleAmountBaseCurrency);
                    dr["ScheduleAmountReportingCurrency"] = oItem.ScheduleAmountReportingCurrency;
                    dr["LocalCurrencyCode"] = oItem.LocalCurrencyCode;
                    dr["OriginalGLDataRecurringItemScheduleID"] = ReturnDBNullWhenNull(oItem.OriginalGLDataRecurringItemScheduleID);
                    dr["PreviousGLDataRecurringItemScheduleID"] = ReturnDBNullWhenNull(oItem.PreviousGLDataRecurringItemScheduleID);
                    dr["OpenDate"] = oItem.OpenDate;
                    dr["CloseDate"] = ReturnDBNullWhenNull(oItem.CloseDate);
                    dr["ReconciliationCategoryTypeID"] = oItem.ReconciliationCategoryTypeID;
                    dr["RecPeriodAmountLocalCurrency"] = oItem.RecPeriodAmountLocalCurrency;
                    dr["RecPeriodAmountBaseCurrency"] = ReturnDBNullWhenNull(oItem.RecPeriodAmountBaseCurrency);
                    dr["RecPeriodAmountReportingCurrency"] = oItem.RecPeriodAmountReportingCurrency;
                    dr["BalanceLocalCurrency"] = oItem.BalanceLocalCurrency;
                    dr["BalanceBaseCurrency"] = ReturnDBNullWhenNull(oItem.BalanceBaseCurrency);
                    dr["BalanceReportingCurrency"] = oItem.BalanceReportingCurrency;
                    dr["JournalEntryRef"] = oItem.JournalEntryRef;
                    dr["Comments"] = oItem.Comments;
                    dr["CloseComments"] = oItem.CloseComments;
                    dr["RecItemNumber"] = oItem.RecItemNumber;
                    dr["DataImportID"] = ReturnDBNullWhenNull(oItem.DataImportID);
                    dr["CalculationFrequencyID"] = oItem.CalculationFrequencyID;
                    dr["TotalIntervals"] = ReturnDBNullWhenNull(oItem.TotalIntervals);
                    if (oItem.CurrentInterval.GetValueOrDefault() > 0)
                        dr["CurrentInterval"] = ReturnDBNullWhenNull(oItem.CurrentInterval);
                    dr["StartIntervalRecPeriodID"] = ReturnDBNullWhenNull(oItem.StartIntervalRecPeriodID);
                    dr["RecordSourceTypeID"] = oItem.RecordSourceTypeID;
                    dr["RecordSourceID"] = oItem.RecordSourceID;
                    dr["ExRateLCCYtoBCCY"] = ReturnDBNullWhenNull(oItem.ExRateLCCYtoBCCY);
                    dr["ExRateLCCYtoRCCY"] = ReturnDBNullWhenNull(oItem.ExRateLCCYtoRCCY);
                    dr["IgnoreInCalculation"] = ReturnDBNullWhenNull(oItem.IgnoreInCalculation);
                    dr["IsActive"] = oItem.IsActive;
                    dr["DateAdded"] = oItem.DateAdded;
                    dr["AddedBy"] = oItem.AddedBy;
                    dr["AddedByUserID"] = ReturnDBNullWhenNull(oItem.AddedByUserID);
                    dr["DateRevised"] = ReturnDBNullWhenNull(oItem.DateRevised);
                    dr["RevisedBy"] = oItem.RevisedBy;
                    dr["ExcelRowNumber"] = oItem.ExcelRowNumber;
                    dr["MatchSetMatchingSourceDataImportID"] = ReturnDBNullWhenNull(oItem.MatchSetMatchingSourceDataImportID);
                    dr["IndexID"] = ReturnDBNullWhenNull(oItem.IndexID);

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #endregion

        internal static DataTable ConvertDashboardMstInfoToDataTable(List<DashboardMstInfo> oDashboardMstInfoCollection)
        {
            DataTable dt = null;
            if (oDashboardMstInfoCollection != null && oDashboardMstInfoCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc1 = dt.Columns.Add("WeekDayID");
                DataColumn dc2 = dt.Columns.Add("IsActivated");
                DataRow dr;
                for (int i = 0; i < oDashboardMstInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oDashboardMstInfoCollection[i].DashboardID;
                    dr[1] = oDashboardMstInfoCollection[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #region Role Configuration

        /// <summary>
        /// Convert Company Attribute Info class to table
        /// </summary>
        /// <param name="oCompanyAttributeConfigInfoList"></param>
        /// <returns></returns>
        internal static DataTable ConvertCompanyAttributeInfoToDataTable(List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList)
        {
            DataTable dt = null;
            if (oCompanyAttributeConfigInfoList != null && oCompanyAttributeConfigInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("AttributeSetValueID");
                dt.Columns.Add("AttributeSetID");
                dt.Columns.Add("AttributeID");
                dt.Columns.Add("ReferenceID");
                dt.Columns.Add("Value");
                dt.Columns.Add("ValueLabelID");
                dt.Columns.Add("IsEnabled");
                dt.Columns.Add("IsActive");
                DataRow dr;
                for (int i = 0; i < oCompanyAttributeConfigInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    if (oCompanyAttributeConfigInfoList[i].AttributeSetValueID.HasValue)
                        dr["AttributeSetValueID"] = oCompanyAttributeConfigInfoList[i].AttributeSetValueID;
                    else
                        dr["AttributeSetValueID"] = DBNull.Value;
                    if (oCompanyAttributeConfigInfoList[i].AttributeSetID.HasValue)
                        dr["AttributeSetID"] = oCompanyAttributeConfigInfoList[i].AttributeSetID;
                    else
                        dr["AttributeSetID"] = DBNull.Value;
                    dr["AttributeID"] = oCompanyAttributeConfigInfoList[i].AttributeID;
                    if (oCompanyAttributeConfigInfoList[i].ReferenceID.HasValue)
                        dr["ReferenceID"] = oCompanyAttributeConfigInfoList[i].ReferenceID;
                    else
                        dr["ReferenceID"] = DBNull.Value;
                    if (!string.IsNullOrEmpty(oCompanyAttributeConfigInfoList[i].Value))
                        dr["Value"] = oCompanyAttributeConfigInfoList[i].Value;
                    else
                        dr["Value"] = DBNull.Value;
                    if (oCompanyAttributeConfigInfoList[i].ValueLabelID.HasValue)
                        dr["ValueLabelID"] = oCompanyAttributeConfigInfoList[i].ValueLabelID;
                    else
                        dr["ValueLabelID"] = DBNull.Value;
                    dr["IsEnabled"] = oCompanyAttributeConfigInfoList[i].IsEnabled;
                    dr["IsActive"] = oCompanyAttributeConfigInfoList[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #endregion

        internal static DataTable ConvertAutoSaveAttributeValueInfoToDataTable(List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoList)
        {
            DataTable dt = null;
            if (oAutoSaveAttributeValueInfoList != null && oAutoSaveAttributeValueInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("AutoSaveAttributeValueID");
                dt.Columns.Add("AutoSaveAttributeID");
                dt.Columns.Add("UserID");
                dt.Columns.Add("RoleID");
                dt.Columns.Add("ReferenceID");
                dt.Columns.Add("Value");
                dt.Columns.Add("IsActive");
                DataRow dr;
                for (int i = 0; i < oAutoSaveAttributeValueInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    if (oAutoSaveAttributeValueInfoList[i].AutoSaveAttributeValueID.HasValue)
                        dr["AutoSaveAttributeValueID"] = oAutoSaveAttributeValueInfoList[i].AutoSaveAttributeValueID;
                    else
                        dr["AutoSaveAttributeValueID"] = DBNull.Value;

                    dr["AutoSaveAttributeID"] = oAutoSaveAttributeValueInfoList[i].AutoSaveAttributeID;
                    dr["UserID"] = oAutoSaveAttributeValueInfoList[i].UserID;
                    dr["RoleID"] = oAutoSaveAttributeValueInfoList[i].RoleID;

                    if (oAutoSaveAttributeValueInfoList[i].ReferenceID.HasValue)
                        dr["ReferenceID"] = oAutoSaveAttributeValueInfoList[i].ReferenceID;
                    else
                        dr["ReferenceID"] = DBNull.Value;
                    if (!string.IsNullOrEmpty(oAutoSaveAttributeValueInfoList[i].Value))
                        dr["Value"] = oAutoSaveAttributeValueInfoList[i].Value;
                    else
                        dr["Value"] = DBNull.Value;
                    dr["IsActive"] = oAutoSaveAttributeValueInfoList[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #region Quality Score

        /// <summary>
        /// Converts the company quality score to data table.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfoList">The o company quality score info list.</param>
        /// <returns></returns>
        internal static DataTable ConvertCompanyQualityScoreToDataTable(List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList)
        {
            DataTable dt = null;
            if (oCompanyQualityScoreInfoList != null && oCompanyQualityScoreInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("CompanyQualityScoreID");
                dt.Columns.Add("CompanyRecPeriodSetID");
                dt.Columns.Add("QualityScoreID");
                dt.Columns.Add("QualityScoreNumber");
                dt.Columns.Add("Descritpion");
                dt.Columns.Add("DescritpionLabelID");
                dt.Columns.Add("IsApplicableForSRA");
                dt.Columns.Add("IsUserScoreEnabled");
                dt.Columns.Add("Weightage", typeof(decimal));
                dt.Columns.Add("SortOrder");
                dt.Columns.Add("IsEnabled");
                dt.Columns.Add("IsActive");
                DataRow dr;
                for (int i = 0; i < oCompanyQualityScoreInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    if (oCompanyQualityScoreInfoList[i].CompanyQualityScoreID.HasValue)
                        dr["CompanyQualityScoreID"] = oCompanyQualityScoreInfoList[i].CompanyQualityScoreID;
                    else
                        dr["CompanyQualityScoreID"] = DBNull.Value;
                    if (oCompanyQualityScoreInfoList[i].CompanyRecPeriodSetID.HasValue)
                        dr["CompanyRecPeriodSetID"] = oCompanyQualityScoreInfoList[i].CompanyRecPeriodSetID;
                    else
                        dr["CompanyRecPeriodSetID"] = DBNull.Value;
                    if (oCompanyQualityScoreInfoList[i].QualityScoreID.HasValue)
                        dr["QualityScoreID"] = oCompanyQualityScoreInfoList[i].QualityScoreID;
                    else
                        dr["QualityScoreID"] = DBNull.Value;
                    dr["QualityScoreNumber"] = oCompanyQualityScoreInfoList[i].QualityScoreNumber;
                    dr["Descritpion"] = oCompanyQualityScoreInfoList[i].Description;
                    dr["DescritpionLabelID"] = oCompanyQualityScoreInfoList[i].DescriptionLabelID;
                    dr["IsApplicableForSRA"] = oCompanyQualityScoreInfoList[i].IsApplicableForSRA;
                    dr["IsUserScoreEnabled"] = oCompanyQualityScoreInfoList[i].IsUserScoreEnabled;
                    dr["Weightage"] = oCompanyQualityScoreInfoList[i].Weightage;
                    dr["SortOrder"] = oCompanyQualityScoreInfoList[i].SortOrder;
                    dr["IsEnabled"] = oCompanyQualityScoreInfoList[i].IsEnabled;
                    dr["IsActive"] = oCompanyQualityScoreInfoList[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// Converts the mapping upload info to data table.
        /// </summary>
        /// <param name="oMappingUploadInfoList">The o mapping upload info list.</param>
        /// <returns></returns>
        internal static DataTable ConvertMappingUploadToDataTable(List<MappingUploadInfo> oMappingUploadInfoList)
        {
            DataTable dt = null;
            if (oMappingUploadInfoList != null && oMappingUploadInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("AccountMappingKeyID");

                DataRow dr;
                for (int i = 0; i < oMappingUploadInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["AccountMappingKeyID"] = oMappingUploadInfoList[i].AccountMappingKeyID;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// Converts the GL data quality score to data table.
        /// </summary>
        /// <param name="oGLDataQualityScoreInfoList">The o GL data quality score info list.</param>
        /// <returns></returns>
        internal static DataTable ConvertGLDataQualityScoreToDataTable(List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList)
        {
            DataTable dt = null;
            if (oGLDataQualityScoreInfoList != null && oGLDataQualityScoreInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("GLDataQualityScoreID");
                dt.Columns.Add("GLDataID");
                dt.Columns.Add("CompanyQualityScoreID");
                dt.Columns.Add("SystemQualityScoreStatusID");
                dt.Columns.Add("UserQualityScoreStatusID");
                dt.Columns.Add("Comments");
                dt.Columns.Add("IsActive");
                DataRow dr;
                for (int i = 0; i < oGLDataQualityScoreInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    if (oGLDataQualityScoreInfoList[i].GLDataQualityScoreID.HasValue)
                        dr["GLDataQualityScoreID"] = oGLDataQualityScoreInfoList[i].GLDataQualityScoreID;
                    else
                        dr["GLDataQualityScoreID"] = DBNull.Value;

                    if (oGLDataQualityScoreInfoList[i].GLDataID.HasValue)
                        dr["GLDataID"] = oGLDataQualityScoreInfoList[i].GLDataID;
                    else
                        dr["GLDataID"] = DBNull.Value;

                    if (oGLDataQualityScoreInfoList[i].CompanyQualityScoreID.HasValue)
                        dr["CompanyQualityScoreID"] = oGLDataQualityScoreInfoList[i].CompanyQualityScoreID;
                    else
                        dr["CompanyQualityScoreID"] = DBNull.Value;

                    if (oGLDataQualityScoreInfoList[i].SystemQualityScoreStatusID.HasValue)
                        dr["SystemQualityScoreStatusID"] = oGLDataQualityScoreInfoList[i].SystemQualityScoreStatusID;
                    else
                        dr["SystemQualityScoreStatusID"] = DBNull.Value;

                    if (oGLDataQualityScoreInfoList[i].UserQualityScoreStatusID.HasValue)
                        dr["UserQualityScoreStatusID"] = oGLDataQualityScoreInfoList[i].UserQualityScoreStatusID;
                    else
                        dr["UserQualityScoreStatusID"] = DBNull.Value;

                    dr["Comments"] = oGLDataQualityScoreInfoList[i].Comments;
                    dr["IsActive"] = oGLDataQualityScoreInfoList[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #endregion

        #region "Task Master"
        internal static DataTable ConvertTaskHdrToDataTable(List<TaskHdrInfo> oTasksHdrList)
        {
            DataTable dt = null;
            if (oTasksHdrList != null && oTasksHdrList.Count > 0)
            {
                Dictionary<string, System.Type> fieldDictionary = new Dictionary<string, Type>();
                fieldDictionary.Add("TaskID", typeof(System.Int64));
                fieldDictionary.Add("TaskNumber", typeof(System.String));
                fieldDictionary.Add("TaskTypeID", typeof(System.Int16));
                fieldDictionary.Add("RecPeriodID", typeof(System.Int32));
                fieldDictionary.Add("DataImportID", typeof(System.Int32));
                fieldDictionary.Add("IsActive", typeof(System.Boolean));
                fieldDictionary.Add("DateAdded", typeof(System.DateTime));
                fieldDictionary.Add("AddedBy", typeof(System.String));
                fieldDictionary.Add("DateRevised", typeof(System.DateTime));
                fieldDictionary.Add("RevisedBy", typeof(System.String));
                fieldDictionary.Add("TempTaskSequenceNumber", typeof(System.Int32));
                dt = GetDataTable(fieldDictionary, false);

                DataRow dr;
                foreach (TaskHdrInfo oTask in oTasksHdrList)
                {
                    dr = dt.NewRow();
                    if (oTask.TaskID.HasValue)
                        dr["TaskID"] = oTask.TaskID.Value;

                    dr["TaskNumber"] = oTask.TaskNumber;

                    if (oTask.TaskTypeID.HasValue)
                        dr["TaskTypeID"] = oTask.TaskTypeID.Value;

                    if (oTask.RecPeriodID.HasValue)
                        dr["RecPeriodID"] = oTask.RecPeriodID.Value;

                    if (oTask.DataImportID.HasValue)
                        dr["DataImportID"] = oTask.DataImportID.Value;

                    if (oTask.TempTaskSequenceNumber.HasValue)
                        dr["TempTaskSequenceNumber"] = oTask.TempTaskSequenceNumber.Value;

                    dr["IsActive"] = oTask.IsActive;

                    if (!String.IsNullOrEmpty(oTask.AddedBy))
                        dr["AddedBy"] = oTask.AddedBy;
                    if (!String.IsNullOrEmpty(oTask.RevisedBy))
                        dr["RevisedBy"] = oTask.RevisedBy;
                    if (oTask.DateAdded.HasValue)
                        dr["DateAdded"] = oTask.DateAdded.Value.ToShortDateString();
                    if (oTask.DateRevised.HasValue)
                        dr["DateRevised"] = oTask.DateRevised.Value.ToShortDateString();

                    dt.Rows.Add(dr);
                }


            }
            return dt;
        }

        internal static DataTable ConvertTaskAttributeToDataTable(List<TaskHdrInfo> oTasksHdrList)
        {
            DataTable dt = null;
            if (oTasksHdrList != null && oTasksHdrList.Count > 0)
            {
                Dictionary<string, System.Type> fieldDictionary = new Dictionary<string, Type>();
                fieldDictionary.Add("TempTaskSequenceNumber", typeof(System.Int32));
                fieldDictionary.Add("TaskAttributeRecPeriodSetID", typeof(System.Int64));
                fieldDictionary.Add("TaskID", typeof(System.Int64));
                fieldDictionary.Add("TaskAttributeID", typeof(System.Int16));
                fieldDictionary.Add("StartRecPeriodID", typeof(System.Int32));
                fieldDictionary.Add("EndRecPeriodID", typeof(System.Int32));
                fieldDictionary.Add("ReferenceID", typeof(System.Int32));
                fieldDictionary.Add("Value", typeof(System.String));
                fieldDictionary.Add("IsActive", typeof(System.Boolean));
                dt = GetDataTable(fieldDictionary, false);

                foreach (TaskHdrInfo oTask in oTasksHdrList)
                {
                    foreach (int attrID in Enum.GetValues(typeof(ARTEnums.TaskAttribute)))
                    {
                        switch (attrID)
                        {
                            case (int)ARTEnums.TaskAttribute.AssignedTo:
                                List<UserHdrInfo> AssignedToUserList = oTask.AssignedTo;
                                if (AssignedToUserList != null && AssignedToUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in AssignedToUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.Reviewer:
                                List<UserHdrInfo> ReviewerUserList = oTask.Reviewer;
                                if (ReviewerUserList != null && ReviewerUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in ReviewerUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.Approver:
                                List<UserHdrInfo> ApproverUserList = oTask.Approver;
                                if (ApproverUserList != null && ApproverUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in ApproverUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;


                            case (int)ARTEnums.TaskAttribute.AttachedDocuments:
                                List<AttachmentInfo> AttachmentInfoList = oTask.CreationAttachment;
                                if (AttachmentInfoList != null && AttachmentInfoList.Count > 0)
                                {
                                    foreach (AttachmentInfo oAttachmentInfo in AttachmentInfoList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oAttachmentInfo.AttachmentID.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.ReviewerDueDate:
                                if (oTask.ReviewerDueDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.ReviewerDueDate.Value.ToString(new CultureInfo(1033)));
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.ReviewerDueDays:
                                if (oTask.ReviewerDueDays.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.ReviewerDueDays.Value.ToString());
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.AssigneeDueDate:
                                if (oTask.AssigneeDueDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.AssigneeDueDate.Value.ToString(new CultureInfo(1033)));
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.AssigneeDueDays:
                                if (oTask.AssigneeDueDays.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.AssigneeDueDays.Value.ToString());
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.Description:
                                if (!string.IsNullOrEmpty(oTask.TaskDescription))
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskDescription);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.Notify:
                                List<UserHdrInfo> notifyUserList = oTask.Notify;
                                if (notifyUserList != null && notifyUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in notifyUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.RecurrenceFrequency:
                                List<ReconciliationPeriodInfo> recPeriodIDList = oTask.RecurrenceFrequency;
                                if (recPeriodIDList != null && recPeriodIDList.Count > 0)
                                {
                                    foreach (ReconciliationPeriodInfo oRecPeriod in recPeriodIDList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oRecPeriod.ReconciliationPeriodID.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.RecurrencePeriodNumber:
                                List<ReconciliationPeriodInfo> RecurrencePeriodNumberList = oTask.RecurrencePeriodNumberList;
                                if (RecurrencePeriodNumberList != null && RecurrencePeriodNumberList.Count > 0)
                                {
                                    foreach (ReconciliationPeriodInfo oRecPeriod in RecurrencePeriodNumberList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oRecPeriod.PeriodNumber.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.RecurrenceType:
                                if (oTask.RecurrenceType != null)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.RecurrenceType.TaskRecurrenceTypeID.Value, null);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.TaskAccount:
                                List<AccountHdrInfo> taskAccountsList = oTask.TaskAccount;
                                if (taskAccountsList != null && taskAccountsList.Count > 0)
                                {
                                    foreach (AccountHdrInfo oAccount in taskAccountsList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oAccount.AccountID.Value, null);
                                    }
                                }
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskDueDate:
                                if (oTask.TaskDueDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskDueDate.Value.ToString(new CultureInfo(1033)));
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskDueDays:
                                if (oTask.TaskDueDays.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskDueDays.Value.ToString());
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskDueDaysBasis:
                                if (oTask.TaskDueDaysBasis.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.TaskDueDaysBasis.Value, null);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskDueDaysBasisSkipNumber:
                                if (oTask.TaskDueDaysBasisSkipNumber.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskDueDaysBasisSkipNumber.Value.ToString());
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.AssigneeDueDaysBasis:
                                if (oTask.AssigneeDueDaysBasis.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.AssigneeDueDaysBasis.Value, null);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.AssigneeDueDaysBasisSkipNumber:
                                if (oTask.AssigneeDueDaysBasisSkipNumber.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.AssigneeDueDaysBasisSkipNumber.Value.ToString());
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.ReviewerDueDaysBasis:
                                if (oTask.ReviewerDueDaysBasis.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.ReviewerDueDaysBasis.Value, null);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.ReviewerDueDaysBasisSkipNumber:
                                if (oTask.ReviewerDueDaysBasisSkipNumber.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.ReviewerDueDaysBasisSkipNumber.Value.ToString());
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskListID:
                                if (oTask.TaskList != null)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.TaskList.TaskListID, oTask.TaskList.TaskListName);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.TaskName:
                                if (!string.IsNullOrEmpty(oTask.TaskName))
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskName);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskStartDate:
                                if (oTask.TaskStartDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskStartDate.Value.ToString(new CultureInfo(1033)));
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.IsDeleted:
                                if (oTask.IsDeleted.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.IsDeleted.Value.ToString());
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskSubListID:
                                if (oTask.TaskSubList != null)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.TaskSubList.TaskSubListID, oTask.TaskSubList.TaskSubListName);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.CustomField1:
                                if (!string.IsNullOrEmpty(oTask.CustomField1))
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.CustomField1);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.CustomField2:
                                if (!string.IsNullOrEmpty(oTask.CustomField2))
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.CustomField2);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;
                            case (int)ARTEnums.TaskAttribute.DaysType:
                                if (oTask.DaysTypeID.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.DaysTypeID.Value, null);
                                else
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, null);
                                break;

                        }
                    }

                }

            }
            return dt;
        }
        internal static DataTable ConvertTaskAttributeToDataTableBulkEdit(List<TaskHdrInfo> oTasksHdrList)
        {
            DataTable dt = null;
            if (oTasksHdrList != null && oTasksHdrList.Count > 0)
            {
                Dictionary<string, System.Type> fieldDictionary = new Dictionary<string, Type>();
                fieldDictionary.Add("TempTaskSequenceNumber", typeof(System.Int32));
                fieldDictionary.Add("TaskAttributeRecPeriodSetID", typeof(System.Int64));
                fieldDictionary.Add("TaskID", typeof(System.Int64));
                fieldDictionary.Add("TaskAttributeID", typeof(System.Int16));
                fieldDictionary.Add("StartRecPeriodID", typeof(System.Int32));
                fieldDictionary.Add("EndRecPeriodID", typeof(System.Int32));
                fieldDictionary.Add("ReferenceID", typeof(System.Int32));
                fieldDictionary.Add("Value", typeof(System.String));
                fieldDictionary.Add("IsActive", typeof(System.Boolean));
                dt = GetDataTable(fieldDictionary, false);

                foreach (TaskHdrInfo oTask in oTasksHdrList)
                {
                    foreach (int attrID in Enum.GetValues(typeof(ARTEnums.TaskAttribute)))
                    {
                        switch (attrID)
                        {
                            case (int)ARTEnums.TaskAttribute.AssignedTo:
                                List<UserHdrInfo> AssignedToUserList = oTask.AssignedTo;
                                if (AssignedToUserList != null && AssignedToUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in AssignedToUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                break;
                            case (int)ARTEnums.TaskAttribute.Reviewer:
                                List<UserHdrInfo> ReviewerUserList = oTask.Reviewer;
                                if (ReviewerUserList != null && ReviewerUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in ReviewerUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                break;


                            case (int)ARTEnums.TaskAttribute.Approver:
                                List<UserHdrInfo> ApproverUserList = oTask.Approver;
                                if (ApproverUserList != null && ApproverUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in ApproverUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                break;

                            case (int)ARTEnums.TaskAttribute.AttachedDocuments:
                                List<AttachmentInfo> AttachmentInfoList = oTask.CreationAttachment;
                                if (AttachmentInfoList != null && AttachmentInfoList.Count > 0)
                                {
                                    foreach (AttachmentInfo oAttachmentInfo in AttachmentInfoList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oAttachmentInfo.AttachmentID.Value, null);
                                    }
                                }
                                break;

                            case (int)ARTEnums.TaskAttribute.ReviewerDueDate:
                                if (oTask.ReviewerDueDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.ReviewerDueDate.Value.ToShortDateString());
                                break;

                            case (int)ARTEnums.TaskAttribute.ReviewerDueDays:
                                if (oTask.ReviewerDueDays.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.ReviewerDueDays.Value.ToString());
                                break;

                            case (int)ARTEnums.TaskAttribute.AssigneeDueDate:
                                if (oTask.AssigneeDueDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.AssigneeDueDate.Value.ToShortDateString());
                                break;

                            case (int)ARTEnums.TaskAttribute.AssigneeDueDays:
                                if (oTask.AssigneeDueDays.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.AssigneeDueDays.Value.ToString());
                                break;

                            case (int)ARTEnums.TaskAttribute.Description:
                                if (!string.IsNullOrEmpty(oTask.TaskDescription))
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskDescription);
                                break;

                            case (int)ARTEnums.TaskAttribute.Notify:
                                List<UserHdrInfo> notifyUserList = oTask.Notify;
                                if (notifyUserList != null && notifyUserList.Count > 0)
                                {
                                    foreach (UserHdrInfo oUser in notifyUserList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oUser.UserID.Value, null);
                                    }
                                }
                                break;
                            case (int)ARTEnums.TaskAttribute.RecurrenceFrequency:
                                List<ReconciliationPeriodInfo> recPeriodIDList = oTask.RecurrenceFrequency;
                                if (recPeriodIDList != null && recPeriodIDList.Count > 0)
                                {
                                    foreach (ReconciliationPeriodInfo oRecPeriod in recPeriodIDList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oRecPeriod.ReconciliationPeriodID.Value, null);
                                    }
                                }
                                break;

                            case (int)ARTEnums.TaskAttribute.RecurrenceType:
                                if (oTask.RecurrenceType != null)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.RecurrenceType.TaskRecurrenceTypeID.Value, null);
                                break;

                            case (int)ARTEnums.TaskAttribute.TaskAccount:
                                List<AccountHdrInfo> taskAccountsList = oTask.TaskAccount;
                                if (taskAccountsList != null && taskAccountsList.Count > 0)
                                {
                                    foreach (AccountHdrInfo oAccount in taskAccountsList)
                                    {
                                        AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oAccount.AccountID.Value, null);
                                    }
                                }
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskDueDate:
                                if (oTask.TaskDueDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskDueDate.Value.ToShortDateString());
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskDueDays:
                                if (oTask.TaskDueDays.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskDueDays.Value.ToString());
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskListID:
                                if (oTask.TaskList != null)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, oTask.TaskList.TaskListID, oTask.TaskList.TaskListName);
                                break;

                            case (int)ARTEnums.TaskAttribute.TaskName:
                                if (!string.IsNullOrEmpty(oTask.TaskName))
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskName);
                                break;
                            case (int)ARTEnums.TaskAttribute.TaskStartDate:
                                if (oTask.TaskStartDate.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.TaskStartDate.Value.ToShortDateString());
                                break;
                            case (int)ARTEnums.TaskAttribute.IsDeleted:
                                if (oTask.IsDeleted.HasValue)
                                    AddNewDataRowToTaskAttrDataTable(dt, oTask, attrID, null, oTask.IsDeleted.Value.ToString());
                                break;

                        }
                    }

                }

            }
            return dt;
        }

        internal static DataTable ConvertTaskHdrInfoToRecordDataTable(List<TaskHdrInfo> oTaskHdrInfoList)
        {
            DataTable dtRecord = null;
            Dictionary<string, System.Type> fieldDictionary = new Dictionary<string, Type>();
            fieldDictionary.Add("ID1", typeof(System.Int64));
            fieldDictionary.Add("ID2", typeof(System.Int16));
            dtRecord = GetDataTable(fieldDictionary, false);

            Dictionary<long, short> taskHdrDictionary = new Dictionary<long, short>();
            Dictionary<long, short> taskDetailDictionary = new Dictionary<long, short>();

            DataRow dr = null;
            foreach (TaskHdrInfo oTask in oTaskHdrInfoList)
            {
                long taskID = oTask.TaskID.Value;
                long taskDetailID = oTask.TaskDetailID.Value;

                if (!taskHdrDictionary.ContainsKey(taskID))
                {
                    taskHdrDictionary.Add(taskID, 3);

                    dr = dtRecord.NewRow();
                    dr["ID1"] = taskID;
                    dr["ID2"] = (int)ARTEnums.RecordType.TaskCreation;
                    dtRecord.Rows.Add(dr);
                }

                dr = dtRecord.NewRow();
                dr["ID1"] = taskDetailID;
                dr["ID2"] = (int)ARTEnums.RecordType.TaskComplition;
                dtRecord.Rows.Add(dr);

            }
            return dtRecord;
        }

        private static void AddNewDataRowToTaskAttrDataTable(DataTable dtTaskAttr, TaskHdrInfo oTask, int taskAttr, long? referenceID, string value)
        {
            DataRow dr = dtTaskAttr.NewRow();
            if (oTask.TempTaskSequenceNumber.HasValue)
                dr["TempTaskSequenceNumber"] = oTask.TempTaskSequenceNumber.Value;

            if (oTask.TaskID.HasValue)
                dr["TaskID"] = oTask.TaskID.Value;


            dr["TaskAttributeID"] = taskAttr;

            if (oTask.RecPeriodID.HasValue)
                dr["StartRecPeriodID"] = oTask.RecPeriodID.Value;
            if (referenceID.HasValue)
                dr["ReferenceID"] = referenceID;
            else
                dr["ReferenceID"] = DBNull.Value;
            if (value != null)
                dr["Value"] = value;
            else
                dr["Value"] = DBNull.Value;
            dr["IsActive"] = true;
            dtTaskAttr.Rows.Add(dr);
        }

        internal static DataTable ConvertTaskStatusMstInfoListToDataTable(List<TaskStatusMstInfo> taskStatusInfoList)
        {
            Dictionary<string, System.Type> fieldDictionary = new Dictionary<string, Type>();
            fieldDictionary.Add("ID", typeof(System.Int16));
            DataTable dt = GetDataTable(fieldDictionary, false);

            foreach (TaskStatusMstInfo oTaskStatus in taskStatusInfoList)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = oTaskStatus.TaskStatusID.Value;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        internal static DataTable ConvertTaskUserVisibilityToDataTable(List<TaskHdrInfo> oTasksHdrList, Int32? CurrentUserID)
        {
            DataTable dt = null;
            if (oTasksHdrList != null && oTasksHdrList.Count > 0)
            {
                Dictionary<string, System.Type> fieldDictionary = new Dictionary<string, Type>();
                fieldDictionary.Add("UserId", typeof(System.Int64));
                fieldDictionary.Add("TaskID", typeof(System.Int64));
                fieldDictionary.Add("TaskDetailId", typeof(System.Int64));
                dt = GetDataTable(fieldDictionary, false);

                DataRow dr;
                foreach (TaskHdrInfo oTask in oTasksHdrList)
                {
                    dr = dt.NewRow();
                    if (CurrentUserID.HasValue)
                        dr["UserId"] = CurrentUserID;

                    if (oTask.TaskID.HasValue)
                        dr["TaskID"] = oTask.TaskID.Value;

                    if (oTask.TaskDetailID.HasValue)
                        dr["TaskDetailId"] = oTask.TaskDetailID.Value;
                    dt.Rows.Add(dr);
                }


            }
            return dt;
        }
        #endregion

        internal static DataTable ConvertCompanyUserToDataTable(List<CompanyUserInfo> oCompanyUserInfoList)
        {
            DataTable dt = null;
            if (oCompanyUserInfoList != null && oCompanyUserInfoList.Count > 0)
            {
                Dictionary<string, System.Type> fieldDictionary = new Dictionary<string, Type>();
                fieldDictionary.Add("[CompanyID]", typeof(System.Int32));
                fieldDictionary.Add("[UserID]", typeof(System.Int32));
                fieldDictionary.Add("[LoginID]", typeof(System.String));
                fieldDictionary.Add("[FTPLoginID]", typeof(System.String));
                fieldDictionary.Add("[EmailID]", typeof(System.String));
                fieldDictionary.Add("[IsActive]", typeof(System.Boolean));
                dt = GetDataTable(fieldDictionary, false);

                DataRow dr;
                foreach (CompanyUserInfo oCompanyUserInfo in oCompanyUserInfoList)
                {
                    dr = dt.NewRow();
                    if (oCompanyUserInfo.CompanyID.HasValue)
                        dr["[CompanyID]"] = oCompanyUserInfo.CompanyID.Value;
                    if (oCompanyUserInfo.UserID.HasValue)
                        dr["[UserID]"] = oCompanyUserInfo.UserID.Value;
                    dr["[LoginID]"] = oCompanyUserInfo.LoginID;
                    if (!string.IsNullOrEmpty(oCompanyUserInfo.FTPLoginID))
                        dr["[FTPLoginID]"] = oCompanyUserInfo.FTPLoginID;
                    else
                        dr["[FTPLoginID]"] = DBNull.Value;
                    dr["[EmailID]"] = oCompanyUserInfo.EmailID;
                    dr["[IsActive]"] = oCompanyUserInfo.IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private static DataTable GetDataTable(Dictionary<string, System.Type> fieldDictionary, bool generateAuditFileds)
        {
            DataTable dt = new DataTable();
            if (generateAuditFileds)
            {

                fieldDictionary.Add("IsActive", typeof(System.Boolean));
                fieldDictionary.Add("DateAdded", typeof(System.DateTime));
                fieldDictionary.Add("AddedBy", typeof(System.String));
                fieldDictionary.Add("DateRevised", typeof(System.DateTime));
                fieldDictionary.Add("RevisedBy", typeof(System.Boolean));

            }
            foreach (KeyValuePair<string, System.Type> fieldDescription in fieldDictionary)
            {
                dt.Columns.Add(fieldDescription.Key, fieldDescription.Value);
            }
            return dt;

        }

        #region Convert to XML Methods

        public static string ConvertUserHdrInfoListToXML(List<UserHdrInfo> oUserHdrInfoList)
        {
            return SerializeToXml<List<UserHdrInfo>>(oUserHdrInfoList, "Root");
        }

        public static string SerializeToXml<T>(T obj, string root)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (XmlWriter xw = XmlWriter.Create(sb))
            {
                xs.Serialize(xw, obj);
            }
            return sb.ToString();
        }

        #endregion

        #region Connection string methods

        // Get connection string for company
        public static void SetConnectionString(AppUserInfo oAppUserInfo)
        {
            string cnnString = null;
            // Get Connection String by CompanyID if company ID is present
            if (oAppUserInfo.CompanyID.GetValueOrDefault() > 0)
            {
                // Get Connection string from Cache
                cnnString = CacheHelper.GetCompanyConnectionString(oAppUserInfo.CompanyID);
                if (!string.IsNullOrEmpty(cnnString))
                {
                    oAppUserInfo.IsDatabaseExists = true;
                }
                else
                {
                    // Get Database Details from Sql Server
                    ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(oAppUserInfo);
                    ServerCompanyInfo oServerCompanyInfo = oServerCompanyDAO.GetCompanyServer(oAppUserInfo.CompanyID);
                    if (oServerCompanyInfo != null)
                    {
                        // Check if database has been created for this company
                        if (oServerCompanyInfo.IsDatabaseExists.GetValueOrDefault())
                        {
                            cnnString = DbHelper.GetConnectionString(oServerCompanyInfo.ServerName, oServerCompanyInfo.Instance, oServerCompanyInfo.DatabaseName, oServerCompanyInfo.DBUserID, oServerCompanyInfo.DBPassword);
                            CacheHelper.SetCompanyConnectionString(oServerCompanyInfo.CompanyID, cnnString);
                            oAppUserInfo.IsDatabaseExists = true;
                        }
                        else
                        {   // Database not yet created, return core connection
                            cnnString = DbConstants.ConnectionStringCore;
                            oAppUserInfo.IsDatabaseExists = false;
                        }
                    }
                }
            }
            // If company id not present or unable to get connection string via company
            if (string.IsNullOrEmpty(cnnString) && !string.IsNullOrEmpty(oAppUserInfo.LoginID))
            {
                // Get connection string via login id
                CompanyUserDAO oCompanyUserDAO = new CompanyUserDAO(oAppUserInfo);
                CompanyUserInfo oCompanyUserInfo = oCompanyUserDAO.GetUserDatabase(oAppUserInfo.LoginID);
                if (oCompanyUserInfo != null)
                {
                    if (oCompanyUserInfo.CompanyID.GetValueOrDefault() > 0)
                    {
                        cnnString = DbHelper.GetConnectionString(oCompanyUserInfo.ServerName, oCompanyUserInfo.Instance, oCompanyUserInfo.DatabaseName, oCompanyUserInfo.DBUserID, oCompanyUserInfo.DBPassword);
                        CacheHelper.SetCompanyConnectionString(oCompanyUserInfo.CompanyID, cnnString);
                    }
                    else
                    {   // Required for SkyStemAdmin as this user don't have any company
                        cnnString = DbConstants.ConnectionString;
                    }
                }
            }
            oAppUserInfo.ConnectionString = cnnString;
        }
        /// <summary>
        /// Get Connection string for Core
        /// </summary>
        /// <param name="oAppUserInfo"></param>
        public static void SetConnectionStringCore(AppUserInfo oAppUserInfo)
        {
            oAppUserInfo.ConnectionString = DbConstants.ConnectionStringCore;
        }
        /// <summary>
        /// Connection String for Company Creation
        /// </summary>
        /// <param name="oServerCompanyInfo"></param>
        /// <param name="oAppUserInfo"></param>
        public static void SetConnectionStringCreateCompany(ServerCompanyInfo oServerCompanyInfo, AppUserInfo oAppUserInfo)
        {
            string cnnString = null;
            if (oServerCompanyInfo.IsDatabaseExists.HasValue && !oServerCompanyInfo.IsDatabaseExists.Value)
            {
                cnnString = DbHelper.GetConnectionStringCreateCompany(oServerCompanyInfo.ServerName, oServerCompanyInfo.Instance, oServerCompanyInfo.DBUserID, oServerCompanyInfo.DBPassword);
            }
            oAppUserInfo.ConnectionString = cnnString;
        }

        #endregion

        internal static DataTable ConvertAccountIDNetAccountIDCollectionToDataTable(List<AccountAttributeWarningInfo> oAccountAttributeWarningInfoollection)
        {
            DataTable dt = null;
            if (oAccountAttributeWarningInfoollection != null && oAccountAttributeWarningInfoollection.Count > 0)
            {
                dt = new DataTable("udt_BigInt_Int");
                DataColumn dc1 = dt.Columns.Add("ID1");
                DataColumn dc2 = dt.Columns.Add("ID2");
                DataRow dr;
                for (int i = 0; i < oAccountAttributeWarningInfoollection.Count; i++)
                {
                    dr = dt.NewRow();
                    if (oAccountAttributeWarningInfoollection[i].AccountID.HasValue)
                        dr["ID1"] = oAccountAttributeWarningInfoollection[i].AccountID;
                    if (oAccountAttributeWarningInfoollection[i].NetAccountID.HasValue)
                        dr["ID2"] = oAccountAttributeWarningInfoollection[i].NetAccountID.Value;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertStringIDListToDataTable(List<string> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }


        internal static DataTable ConvertRecControlChecklistToDataTable(List<RecControlCheckListInfo> oRecControlCheckListInfoList)
        {
            DataTable dt = null;
            if (oRecControlCheckListInfoList != null && oRecControlCheckListInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("RecControlCheckListID", typeof(Int32));
                dt.Columns.Add("ChecklistNumber", typeof(String));
                dt.Columns.Add("RowNumber", typeof(Int32));
                dt.Columns.Add("Description", typeof(String));
                dt.Columns.Add("DescriptionLabelID", typeof(Int32));
                dt.Columns.Add("StartRecPeriodID", typeof(Int32));
                dt.Columns.Add("EndRecPeriodID", typeof(Int32));
                dt.Columns.Add("DataImportID", typeof(Int32));
                dt.Columns.Add("AddedByUserID", typeof(Int32));
                dt.Columns.Add("RoleID", typeof(Int16));
                dt.Columns.Add("DateAdded", typeof(DateTime));
                dt.Columns.Add("AddedBy", typeof(String));
                dt.Columns.Add("DateRevised", typeof(DateTime));
                dt.Columns.Add("RevisedBy", typeof(String));
                dt.Columns.Add("IsActive", typeof(Boolean));
                DataRow dr;
                for (int i = 0; i < oRecControlCheckListInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    oRecControlCheckListInfoList[i].RowNumber = i + 1;
                    dr["RowNumber"] = oRecControlCheckListInfoList[i].RowNumber;
                    if (oRecControlCheckListInfoList[i].RecControlCheckListID.HasValue)
                        dr["RecControlCheckListID"] = oRecControlCheckListInfoList[i].RecControlCheckListID;
                    else
                        dr["RecControlCheckListID"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oRecControlCheckListInfoList[i].CheckListNumber))
                        dr["CheckListNumber"] = oRecControlCheckListInfoList[i].CheckListNumber;
                    else
                        dr["CheckListNumber"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oRecControlCheckListInfoList[i].Description))
                        dr["Description"] = oRecControlCheckListInfoList[i].Description;
                    else
                        dr["Description"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].DescriptionLabelID.HasValue)
                        dr["DescriptionLabelID"] = oRecControlCheckListInfoList[i].DescriptionLabelID;
                    else
                        dr["DescriptionLabelID"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].StartRecPeriodID.HasValue)
                        dr["StartRecPeriodID"] = oRecControlCheckListInfoList[i].StartRecPeriodID;
                    else
                        dr["StartRecPeriodID"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].EndRecPeriodID.HasValue)
                        dr["EndRecPeriodID"] = oRecControlCheckListInfoList[i].EndRecPeriodID;
                    else
                        dr["EndRecPeriodID"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].DataImportID.HasValue)
                        dr["DataImportID"] = oRecControlCheckListInfoList[i].DataImportID;
                    else
                        dr["DataImportID"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].AddedByUserID.HasValue)
                        dr["AddedByUserID"] = oRecControlCheckListInfoList[i].AddedByUserID;
                    else
                        dr["AddedByUserID"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].RoleID.HasValue)
                        dr["RoleID"] = oRecControlCheckListInfoList[i].RoleID;
                    else
                        dr["RoleID"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].DateAdded.HasValue)
                        dr["DateAdded"] = oRecControlCheckListInfoList[i].DateAdded;
                    else
                        dr["DateAdded"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oRecControlCheckListInfoList[i].AddedBy))
                        dr["AddedBy"] = oRecControlCheckListInfoList[i].AddedBy;
                    else
                        dr["AddedBy"] = DBNull.Value;

                    if (oRecControlCheckListInfoList[i].DateRevised.HasValue)
                        dr["DateRevised"] = oRecControlCheckListInfoList[i].DateRevised;
                    else
                        dr["DateRevised"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oRecControlCheckListInfoList[i].RevisedBy))
                        dr["RevisedBy"] = oRecControlCheckListInfoList[i].RevisedBy;
                    else
                        dr["RevisedBy"] = DBNull.Value;

                    dr["IsActive"] = oRecControlCheckListInfoList[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertGLDataRecControlCheckListInfoListToDataTable(List<GLDataRecControlCheckListInfo> oGLDataRecControlCheckListInfoList)
        {
            DataTable dt = null;
            if (oGLDataRecControlCheckListInfoList != null && oGLDataRecControlCheckListInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("GLDataRecControlCheckListID", typeof(Int64));
                dt.Columns.Add("RecControlCheckListID", typeof(Int32));
                dt.Columns.Add("GLDataID", typeof(Int64));
                dt.Columns.Add("CompletedRecStatus", typeof(Int16));
                dt.Columns.Add("CompletedBy", typeof(Int32));
                dt.Columns.Add("DateCompleted", typeof(DateTime));
                dt.Columns.Add("ReviewedRecStatus", typeof(Int16));
                dt.Columns.Add("ReviewedBy", typeof(Int32));
                dt.Columns.Add("DateReviewed", typeof(DateTime));
                dt.Columns.Add("IsActive", typeof(Boolean));
                dt.Columns.Add("DateAdded", typeof(DateTime));
                dt.Columns.Add("AddedBy", typeof(String));
                dt.Columns.Add("DateRevised", typeof(DateTime));
                dt.Columns.Add("RevisedBy", typeof(String));

                DataRow dr;
                for (int i = 0; i < oGLDataRecControlCheckListInfoList.Count; i++)
                {
                    dr = dt.NewRow();

                    if (oGLDataRecControlCheckListInfoList[i].GLDataRecControlCheckListID.HasValue)
                        dr["GLDataRecControlCheckListID"] = oGLDataRecControlCheckListInfoList[i].GLDataRecControlCheckListID;
                    else
                        dr["GLDataRecControlCheckListID"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].RecControlCheckListID.HasValue)
                        dr["RecControlCheckListID"] = oGLDataRecControlCheckListInfoList[i].RecControlCheckListID;
                    else
                        dr["RecControlCheckListID"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].GLDataID.HasValue)
                        dr["GLDataID"] = oGLDataRecControlCheckListInfoList[i].GLDataID;
                    else
                        dr["GLDataID"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].CompletedRecStatus.HasValue)
                        dr["CompletedRecStatus"] = oGLDataRecControlCheckListInfoList[i].CompletedRecStatus;
                    else
                        dr["CompletedRecStatus"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].CompletedBy.HasValue)
                        dr["CompletedBy"] = oGLDataRecControlCheckListInfoList[i].CompletedBy;
                    else
                        dr["CompletedBy"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].DateCompleted.HasValue)
                        dr["DateCompleted"] = oGLDataRecControlCheckListInfoList[i].DateCompleted;
                    else
                        dr["DateCompleted"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].ReviewedRecStatus.HasValue)
                        dr["ReviewedRecStatus"] = oGLDataRecControlCheckListInfoList[i].ReviewedRecStatus;
                    else
                        dr["ReviewedRecStatus"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].ReviewedBy.HasValue)
                        dr["ReviewedBy"] = oGLDataRecControlCheckListInfoList[i].ReviewedBy;
                    else
                        dr["ReviewedBy"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].DateReviewed.HasValue)
                        dr["DateReviewed"] = oGLDataRecControlCheckListInfoList[i].DateReviewed;
                    else
                        dr["DateReviewed"] = DBNull.Value;

                    dr["IsActive"] = oGLDataRecControlCheckListInfoList[i].IsActive;

                    if (oGLDataRecControlCheckListInfoList[i].DateAdded.HasValue)
                        dr["DateAdded"] = oGLDataRecControlCheckListInfoList[i].DateAdded;
                    else
                        dr["DateAdded"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oGLDataRecControlCheckListInfoList[i].AddedBy))
                        dr["AddedBy"] = oGLDataRecControlCheckListInfoList[i].AddedBy;
                    else
                        dr["AddedBy"] = DBNull.Value;

                    if (oGLDataRecControlCheckListInfoList[i].DateRevised.HasValue)
                        dr["DateRevised"] = oGLDataRecControlCheckListInfoList[i].DateRevised;
                    else
                        dr["DateRevised"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oGLDataRecControlCheckListInfoList[i].RevisedBy))
                        dr["RevisedBy"] = oGLDataRecControlCheckListInfoList[i].RevisedBy;
                    else
                        dr["RevisedBy"] = DBNull.Value;


                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertRecControlCheckListAccountInfolistToDataTable(List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList)
        {
            DataTable dt = null;
            if (oRecControlCheckListAccountInfoList != null && oRecControlCheckListAccountInfoList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("RowNumber", typeof(Int32));
                dt.Columns.Add("RecControlCheckListAccountID", typeof(Int64));
                dt.Columns.Add("RecControlCheckListID", typeof(Int32));
                dt.Columns.Add("AccountID", typeof(Int64));
                dt.Columns.Add("NetAccountID", typeof(Int32));
                dt.Columns.Add("StartRecPeriodID", typeof(Int32));
                dt.Columns.Add("EndRecPeriodID", typeof(Int32));
                dt.Columns.Add("DataImportID", typeof(Int32));
                dt.Columns.Add("AddedByUserID", typeof(Int32));
                dt.Columns.Add("RoleID", typeof(Int16));
                dt.Columns.Add("DateAdded", typeof(DateTime));
                dt.Columns.Add("AddedBy", typeof(String));
                dt.Columns.Add("DateRevised", typeof(DateTime));
                dt.Columns.Add("RevisedBy", typeof(String));
                dt.Columns.Add("IsActive", typeof(Boolean));
                DataRow dr;
                for (int i = 0; i < oRecControlCheckListAccountInfoList.Count; i++)
                {
                    dr = dt.NewRow();
                    oRecControlCheckListAccountInfoList[i].RowNumber = i + 1;
                    dr["RowNumber"] = oRecControlCheckListAccountInfoList[i].RowNumber;

                    if (oRecControlCheckListAccountInfoList[i].RecControlCheckListAccountID.HasValue)
                        dr["RecControlCheckListAccountID"] = oRecControlCheckListAccountInfoList[i].RecControlCheckListAccountID;
                    else
                        dr["RecControlCheckListAccountID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].RecControlCheckListID.HasValue)
                        dr["RecControlCheckListID"] = oRecControlCheckListAccountInfoList[i].RecControlCheckListID;
                    else
                        dr["RecControlCheckListID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].AccountID.HasValue && oRecControlCheckListAccountInfoList[i].AccountID.Value > 0)
                        dr["AccountID"] = oRecControlCheckListAccountInfoList[i].AccountID;
                    else
                        dr["AccountID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].NetAccountID.HasValue && oRecControlCheckListAccountInfoList[i].NetAccountID.Value > 0)
                        dr["NetAccountID"] = oRecControlCheckListAccountInfoList[i].NetAccountID;
                    else
                        dr["NetAccountID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].StartRecPeriodID.HasValue)
                        dr["StartRecPeriodID"] = oRecControlCheckListAccountInfoList[i].StartRecPeriodID;
                    else
                        dr["StartRecPeriodID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].EndRecPeriodID.HasValue && oRecControlCheckListAccountInfoList[i].EndRecPeriodID.Value > 0)
                        dr["EndRecPeriodID"] = oRecControlCheckListAccountInfoList[i].EndRecPeriodID;
                    else
                        dr["EndRecPeriodID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].DataImportID.HasValue && oRecControlCheckListAccountInfoList[i].DataImportID.Value > 0)
                        dr["DataImportID"] = oRecControlCheckListAccountInfoList[i].DataImportID;
                    else
                        dr["DataImportID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].AddedByUserID.HasValue && oRecControlCheckListAccountInfoList[i].AddedByUserID.Value > 0)
                        dr["AddedByUserID"] = oRecControlCheckListAccountInfoList[i].AddedByUserID;
                    else
                        dr["AddedByUserID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].RoleID.HasValue)
                        dr["RoleID"] = oRecControlCheckListAccountInfoList[i].RoleID;
                    else
                        dr["RoleID"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].DateAdded.HasValue)
                        dr["DateAdded"] = oRecControlCheckListAccountInfoList[i].DateAdded;
                    else
                        dr["DateAdded"] = DBNull.Value;
                    if (!string.IsNullOrEmpty(oRecControlCheckListAccountInfoList[i].AddedBy))
                        dr["AddedBy"] = oRecControlCheckListAccountInfoList[i].AddedBy;
                    else
                        dr["AddedBy"] = DBNull.Value;
                    if (oRecControlCheckListAccountInfoList[i].DateRevised.HasValue)
                        dr["DateRevised"] = oRecControlCheckListAccountInfoList[i].DateRevised;
                    else
                        dr["DateRevised"] = DBNull.Value;
                    if (!string.IsNullOrEmpty(oRecControlCheckListAccountInfoList[i].RevisedBy))
                        dr["RevisedBy"] = oRecControlCheckListAccountInfoList[i].RevisedBy;
                    else
                        dr["RevisedBy"] = DBNull.Value;
                    dr["IsActive"] = oRecControlCheckListAccountInfoList[i].IsActive;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        internal static DataTable ConvertStringListToDataTable(List<string> oStringList)
        {
            DataTable dt = null;
            if (oStringList != null && oStringList.Count > 0)
            {
                dt = new DataTable("IDTable");
                dt.Columns.Add("ColumnName", typeof(String));
                DataRow dr;
                for (int i = 0; i < oStringList.Count; i++)
                {
                    dr = dt.NewRow();
                    if (!string.IsNullOrEmpty(oStringList[i]))
                        dr["ColumnName"] = oStringList[i];
                    else
                        dr["ColumnName"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        #region "FTP Configuration"
        internal static DataTable ConvertUserFTPConfigurationInfoListToDataTable(List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList)
        {
            DataTable dt = null;

            if (oUserFTPConfigurationInfoList != null && oUserFTPConfigurationInfoList.Count > 0)
            {
                dt = new DataTable("dtUserFTPConfiguration");
                dt.Columns.Add("UserFTPConfigurationID", typeof(System.Int32));
                dt.Columns.Add("UserID", typeof(System.Int32));
                dt.Columns.Add("FTPUploadRoleID", typeof(System.Int16));
                dt.Columns.Add("DataImportTypeID", typeof(System.Int16));
                dt.Columns.Add("ImportTemplateID", typeof(System.Int16));
                dt.Columns.Add("ProfileName", typeof(System.String));
                dt.Columns.Add("IsFTPEnabled", typeof(System.Boolean));
                DataRow dr;

                for (int i = 0; i < oUserFTPConfigurationInfoList.Count; i++)
                {

                    dr = dt.NewRow();
                    if (oUserFTPConfigurationInfoList[i].UserFTPConfigurationID.HasValue)
                        dr["UserFTPConfigurationID"] = oUserFTPConfigurationInfoList[i].UserFTPConfigurationID.Value;
                    else
                        dr["UserFTPConfigurationID"] = DBNull.Value;

                    if (oUserFTPConfigurationInfoList[i].UserID.HasValue)
                        dr["UserID"] = oUserFTPConfigurationInfoList[i].UserID.Value;
                    else
                        dr["UserID"] = DBNull.Value;

                    if (oUserFTPConfigurationInfoList[i].FTPUploadRoleID.HasValue)
                        dr["FTPUploadRoleID"] = oUserFTPConfigurationInfoList[i].FTPUploadRoleID.Value;
                    else
                        dr["FTPUploadRoleID"] = DBNull.Value;

                    if (oUserFTPConfigurationInfoList[i].DataImportTypeID.HasValue)
                        dr["DataImportTypeID"] = oUserFTPConfigurationInfoList[i].DataImportTypeID.Value;
                    else
                        dr["DataImportTypeID"] = DBNull.Value;

                    if (oUserFTPConfigurationInfoList[i].ImportTemplateID.HasValue)
                        dr["ImportTemplateID"] = oUserFTPConfigurationInfoList[i].ImportTemplateID.Value;
                    else
                        dr["ImportTemplateID"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oUserFTPConfigurationInfoList[i].ProfileName))
                        dr["ProfileName"] = oUserFTPConfigurationInfoList[i].ProfileName;
                    else
                        dr["ProfileName"] = DBNull.Value;

                    if (oUserFTPConfigurationInfoList[i].IsFTPEnabled.HasValue)
                        dr["IsFTPEnabled"] = oUserFTPConfigurationInfoList[i].IsFTPEnabled.Value;
                    else
                        dr["IsFTPEnabled"] = DBNull.Value;

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        #endregion
        internal static DataTable ConvertTaskCustomFieldInfoCollectionToDataTable(List<TaskCustomFieldInfo> oTaskCustomFieldInfoCollection)
        {
            DataTable dt = null;

            if (oTaskCustomFieldInfoCollection != null && oTaskCustomFieldInfoCollection.Count > 0)
            {
                dt = new DataTable("udt_TaskCustomFieldValueTableType");
                DataColumn dc1 = dt.Columns.Add("TaskCustomFieldValueID", typeof(Int32));
                DataColumn dc2 = dt.Columns.Add("TaskCustomFieldID", typeof(Int16));
                DataColumn dc3 = dt.Columns.Add("CustomFieldValue", typeof(String));
                DataColumn dc4 = dt.Columns.Add("CustomFieldValueLabelID", typeof(Int32));

                DataRow dr;

                for (int i = 0; i < oTaskCustomFieldInfoCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["TaskCustomFieldValueID"] = i;
                    dr["TaskCustomFieldID"] = oTaskCustomFieldInfoCollection[i].TaskCustomFieldID;
                    if (oTaskCustomFieldInfoCollection[i].CustomFieldValue != null)
                        dr["CustomFieldValue"] = oTaskCustomFieldInfoCollection[i].CustomFieldValue;
                    else
                        dr["CustomFieldValue"] = DBNull.Value;
                    if (oTaskCustomFieldInfoCollection[i].CustomFieldValueLabelID.HasValue)
                        dr["CustomFieldValueLabelID"] = oTaskCustomFieldInfoCollection[i].CustomFieldValueLabelID.Value;
                    else
                        dr["CustomFieldValue"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }//end of class
}//end of namespace
