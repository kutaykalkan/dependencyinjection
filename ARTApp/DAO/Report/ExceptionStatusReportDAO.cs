


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.DAO.Report
{
    public class ExceptionStatusReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionStatusReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<ExceptionStatusReportInfo> GetExceptionStatusReport(ReportSearchCriteria oReportSearchCriteria, DataTable dtEntity, DataTable dtUser, DataTable dtRole)
        {
            List<ExceptionStatusReportInfo> oExceptionStatusReportInfoCollection = new List<ExceptionStatusReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetExceptionStatusReportCommand(oReportSearchCriteria, dtEntity, dtUser, dtRole);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ExceptionStatusReportInfo oExceptionStatusReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                // Read Account Information
                while (reader.Read())
                {
                    oExceptionStatusReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oExceptionStatusReportInfo);
                    oExceptionStatusReportInfoCollection.Add(oExceptionStatusReportInfo);
                }
                reader.ClearColumnHash();
                // Net Account
                reader.NextResult();
                while (reader.Read())
                {
                    oExceptionStatusReportInfo = MapNetAccountObject(reader);
                    oExceptionStatusReportInfoCollection.Add(oExceptionStatusReportInfo);
                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oExceptionStatusReportInfoCollection;
        }

        private IDbCommand CreateGetExceptionStatusReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable dtEntity, DataTable dtUser, DataTable dtRole)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_ExceptionStatus");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdTableKeyValue = cmd.CreateParameter();
            cmdTableKeyValue.ParameterName = "@tblKeyValue";
            cmdTableKeyValue.Value = dtEntity;
            cmdParams.Add(cmdTableKeyValue);

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, true, cmd, cmdParams);

            IDbDataParameter cmdIsKeyAccount = cmd.CreateParameter();
            cmdIsKeyAccount.ParameterName = "@IsKeyAccount";
            cmdIsKeyAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsKeyccount);
            cmdParams.Add(cmdIsKeyAccount);

            IDbDataParameter cmdIsMaterialAccount = cmd.CreateParameter();
            cmdIsMaterialAccount.ParameterName = "@IsMaterialAccount";
            cmdIsMaterialAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsMaterialAccount);
            cmdParams.Add(cmdIsMaterialAccount);

            IDbDataParameter cmdRiskRatingIDs = cmd.CreateParameter();
            cmdRiskRatingIDs.ParameterName = "@RiskRatingIDs";
            cmdRiskRatingIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.RiskRatingIDs);
            cmdParams.Add(cmdRiskRatingIDs);


            IDbDataParameter cmdExcludeNetAccount = cmd.CreateParameter();
            cmdExcludeNetAccount.ParameterName = "@ExcludeNetAccount";
            cmdExcludeNetAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ExcludeNetAccount);
            cmdParams.Add(cmdExcludeNetAccount);


            IDbDataParameter cmdIsRequesterUserIDToBeConsideredForPermission = cmd.CreateParameter();
            cmdIsRequesterUserIDToBeConsideredForPermission.ParameterName = "@IsRequesterUserIDToBeConsideredForPermission";
            cmdIsRequesterUserIDToBeConsideredForPermission.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission);
            cmdParams.Add(cmdIsRequesterUserIDToBeConsideredForPermission);

            IDbDataParameter cmdReasonCodeIDs = cmd.CreateParameter();
            cmdReasonCodeIDs.ParameterName = "@tblExceptionTypeIDs";
            cmdReasonCodeIDs.Value = ServiceHelper.ConvertStringIDListToDataTable(oReportSearchCriteria.ExceptionTypeIDs);
            cmdParams.Add(cmdReasonCodeIDs);

            ServiceHelper.AddCommonParametersForUserRoleSearchInReport(dtUser, dtRole, cmd, cmdParams);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            return cmd;
        }


        private ExceptionStatusReportInfo MapAccountObject(IDataReader r)
        {
            ExceptionStatusReportInfo entity = new ExceptionStatusReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");

            entity.IsKeyAccount = r.GetBooleanValue("IsKeyAccount");
            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");
            entity.IsMaterial = r.GetBooleanValue("IsMaterial");

            entity.WriteOnOffReportingCurrency = r.GetDecimalValue("WriteOnOffAmountReportingCurrency");
            entity.UnexpVarReportingCurrency = r.GetDecimalValue("UnexplainedVarianceReportingCurrency");
            entity.DelinquentAmountReportingCurrency = r.GetDecimalValue("DelinquentAmountReportingCurrency");

            entity.PreparerFirstName = r.GetStringValue("PreparerFirstName");
            entity.PreparerLastName = r.GetStringValue("PreparerLastName");
            entity.IsDueDatePast = r.GetBooleanValue("IsDueDatePast");

            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");
            return entity;
        }

        private ExceptionStatusReportInfo MapNetAccountObject(IDataReader r)
        {
            ExceptionStatusReportInfo entity = new ExceptionStatusReportInfo();

            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");

            entity.IsKeyAccount = r.GetBooleanValue("IsKeyAccount");
            entity.IsMaterial = r.GetBooleanValue("IsMaterial");
            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");

            entity.WriteOnOffReportingCurrency = r.GetDecimalValue("WriteOnOffAmountReportingCurrency");
            entity.UnexpVarReportingCurrency = r.GetDecimalValue("UnexplainedVarianceReportingCurrency");
            entity.DelinquentAmountReportingCurrency = r.GetDecimalValue("DelinquentAmountReportingCurrency");
            entity.PreparerFirstName = r.GetStringValue("PreparerFirstName");
            entity.PreparerLastName = r.GetStringValue("PreparerLastName");
            entity.IsDueDatePast = r.GetBooleanValue("IsDueDatePast");

            return entity;
        }




    }//end of class
}//end of namespace


