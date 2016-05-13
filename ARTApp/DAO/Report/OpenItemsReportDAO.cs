


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
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.DAO.Report
{
    public class OpenItemsReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OpenItemsReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<OpenItemsReportInfo> GetReportOpenItemsReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            List<OpenItemsReportInfo> oOpenItemsReportInfoCollection = new List<OpenItemsReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportOpenItemsReportCommand(oReportSearchCriteria, tblEntitySearch,  tblUserSearch,  tblRoleSearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                OpenItemsReportInfo oOpenItemsReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    oOpenItemsReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oOpenItemsReportInfo);
                    oOpenItemsReportInfoCollection.Add(oOpenItemsReportInfo);
                    //this.MapAccountAttributeInfo(reader, oOpenItemsReportInfo);
                }
                while (reader.NextResult())
                {
                    reader.ClearColumnHash();
                    while (reader.Read())
                    {
                        oOpenItemsReportInfo = MapAccountObject(reader);
                        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oOpenItemsReportInfo);
                        oOpenItemsReportInfoCollection.Add(oOpenItemsReportInfo);
                        //this.MapAccountAttributeInfo(reader, oOpenItemsReportInfo);
                    }
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oOpenItemsReportInfoCollection;
        }

        internal List<OpenItemsReportInfo> GetReportOpenItemsReportForCurrentRecperiod(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            List<OpenItemsReportInfo> oOpenItemsReportInfoCollection = new List<OpenItemsReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportOpenItemsReportForCurrentRecPeriodCommand(oReportSearchCriteria, tblEntitySearch, tblUserSearch, tblRoleSearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                OpenItemsReportInfo oOpenItemsReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    oOpenItemsReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oOpenItemsReportInfo);
                    oOpenItemsReportInfoCollection.Add(oOpenItemsReportInfo);
                    //this.MapAccountAttributeInfo(reader, oOpenItemsReportInfo);
                }
                while (reader.NextResult())
                {
                    reader.ClearColumnHash();
                    while (reader.Read())
                    {
                        oOpenItemsReportInfo = MapAccountObject(reader);
                        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oOpenItemsReportInfo);
                        oOpenItemsReportInfoCollection.Add(oOpenItemsReportInfo);
                        //this.MapAccountAttributeInfo(reader, oOpenItemsReportInfo);
                    }
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oOpenItemsReportInfoCollection;
        }

        private IDbCommand CreateGetReportOpenItemsReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            //IDbCommand cmd = this.CreateCommand("usp_RPT_OpenItemsReport");
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_OpenItemsReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdTableKeyValue = cmd.CreateParameter();
            cmdTableKeyValue.ParameterName = "@tblKeyValue";
            cmdTableKeyValue.Value = tblEntitySearch;
            cmdParams.Add(cmdTableKeyValue);

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, true, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForUserRoleSearchInReport (tblUserSearch,  tblRoleSearch, cmd, cmdParams);

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


            IDbDataParameter cmdKeyAccountAttributeId = cmd.CreateParameter();
            cmdKeyAccountAttributeId.ParameterName = "@KeyAccountAttributeId";
            cmdKeyAccountAttributeId.Value = (short)ARTEnums.AccountAttribute.IsKeyAccount;
            cmdParams.Add(cmdKeyAccountAttributeId);

            IDbDataParameter cmdRiskRatingAttributeId = cmd.CreateParameter();
            cmdRiskRatingAttributeId.ParameterName = "@RiskRatingAttributeId";
            cmdRiskRatingAttributeId.Value = (short)ARTEnums.AccountAttribute.RiskRating;
            cmdParams.Add(cmdRiskRatingAttributeId);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            IDbDataParameter cmdExcludeNetAccount = cmd.CreateParameter();
            cmdExcludeNetAccount.ParameterName = "@ExcludeNetAccount";
            cmdExcludeNetAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ExcludeNetAccount);
            cmdParams.Add(cmdExcludeNetAccount);

            IDbDataParameter cmdIsRequesterUserIDToBeConsideredForPermission = cmd.CreateParameter();
            cmdIsRequesterUserIDToBeConsideredForPermission.ParameterName = "@IsRequesterUserIDToBeConsideredForPermission";
            cmdIsRequesterUserIDToBeConsideredForPermission.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission);
            cmdParams.Add(cmdIsRequesterUserIDToBeConsideredForPermission);




            IDbDataParameter cmdOpenItemClassificationIDs = cmd.CreateParameter();
            cmdOpenItemClassificationIDs.ParameterName = "@OpenItemClassificationIDs";
            cmdOpenItemClassificationIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.OpenItemClassificationIDs);
            cmdParams.Add(cmdOpenItemClassificationIDs);

            IDbDataParameter cmdAgingIDs = cmd.CreateParameter();
            cmdAgingIDs.ParameterName = "@AgingIDs";
            cmdAgingIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.AgingIDs);
            cmdParams.Add(cmdAgingIDs);

            IDbDataParameter cmdFromOpenDate = cmd.CreateParameter();
            cmdFromOpenDate.ParameterName = "@FromOpenDate";
            cmdFromOpenDate.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.FromOpenDate);
            cmdParams.Add(cmdFromOpenDate);

            IDbDataParameter cmdToOpenDate = cmd.CreateParameter();
            cmdToOpenDate.ParameterName = "@ToOpenDate";
            cmdToOpenDate.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ToOpenDate);
            cmdParams.Add(cmdToOpenDate);

            IDbDataParameter cmdAsOnDate = cmd.CreateParameter();
            cmdAsOnDate.ParameterName = "@AsOnDate";
            cmdAsOnDate.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.AsOnDate);
            cmdParams.Add(cmdAsOnDate);

            

            return cmd;
        }

        private IDbCommand CreateGetReportOpenItemsReportForCurrentRecPeriodCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            //IDbCommand cmd = this.CreateCommand("usp_RPT_OpenItemsReport");
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_OpenItemsReportForCurrentRecPeriod");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdTableKeyValue = cmd.CreateParameter();
            cmdTableKeyValue.ParameterName = "@tblKeyValue";
            cmdTableKeyValue.Value = tblEntitySearch;
            cmdParams.Add(cmdTableKeyValue);

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, true, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForUserRoleSearchInReport(tblUserSearch, tblRoleSearch, cmd, cmdParams);

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


            IDbDataParameter cmdKeyAccountAttributeId = cmd.CreateParameter();
            cmdKeyAccountAttributeId.ParameterName = "@KeyAccountAttributeId";
            cmdKeyAccountAttributeId.Value = (short)ARTEnums.AccountAttribute.IsKeyAccount;
            cmdParams.Add(cmdKeyAccountAttributeId);

            IDbDataParameter cmdRiskRatingAttributeId = cmd.CreateParameter();
            cmdRiskRatingAttributeId.ParameterName = "@RiskRatingAttributeId";
            cmdRiskRatingAttributeId.Value = (short)ARTEnums.AccountAttribute.RiskRating;
            cmdParams.Add(cmdRiskRatingAttributeId);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            IDbDataParameter cmdExcludeNetAccount = cmd.CreateParameter();
            cmdExcludeNetAccount.ParameterName = "@ExcludeNetAccount";
            cmdExcludeNetAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ExcludeNetAccount);
            cmdParams.Add(cmdExcludeNetAccount);

            IDbDataParameter cmdIsRequesterUserIDToBeConsideredForPermission = cmd.CreateParameter();
            cmdIsRequesterUserIDToBeConsideredForPermission.ParameterName = "@IsRequesterUserIDToBeConsideredForPermission";
            cmdIsRequesterUserIDToBeConsideredForPermission.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission);
            cmdParams.Add(cmdIsRequesterUserIDToBeConsideredForPermission);




            IDbDataParameter cmdOpenItemClassificationIDs = cmd.CreateParameter();
            cmdOpenItemClassificationIDs.ParameterName = "@OpenItemClassificationIDs";
            cmdOpenItemClassificationIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.OpenItemClassificationIDs);
            cmdParams.Add(cmdOpenItemClassificationIDs);

            IDbDataParameter cmdAgingIDs = cmd.CreateParameter();
            cmdAgingIDs.ParameterName = "@AgingIDs";
            cmdAgingIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.AgingIDs);
            cmdParams.Add(cmdAgingIDs);

            IDbDataParameter cmdFromOpenDate = cmd.CreateParameter();
            cmdFromOpenDate.ParameterName = "@FromOpenDate";
            cmdFromOpenDate.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.FromOpenDate);
            cmdParams.Add(cmdFromOpenDate);

            IDbDataParameter cmdToOpenDate = cmd.CreateParameter();
            cmdToOpenDate.ParameterName = "@ToOpenDate";
            cmdToOpenDate.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ToOpenDate);
            cmdParams.Add(cmdToOpenDate);


            return cmd;
        }

        private OpenItemsReportInfo MapAccountObject(IDataReader r)
        {
            OpenItemsReportInfo entity = new OpenItemsReportInfo();

            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.RoleLabelID = r.GetInt32Value("RoleLabelID");
            
            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            //entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            //entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");
            entity.RecItemAmountReportingCurrency = r.GetDecimalValue("RecItemAmountReportingCurrency");
            entity.RecItemAmountBaseCurrency = r.GetDecimalValue("RecItemAmountBaseCurrency");

            entity.GLDataID = r.GetInt64Value("GLDataID");
            entity.RecItemID = r.GetInt64Value("RecItemID");

            entity.IsKeyAccount = r.GetBooleanValue("IsKeyAccount");
            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");
            entity.IsMaterial = r.GetBooleanValue("IsMaterial");
            entity.OpenItemClassificationID = r.GetInt16Value("OpenItemClassificationID");
            entity.OpenItemClassificationLabelID = r.GetInt32Value("OpenItemClassificationLabelID");
            entity.OpenDate = r.GetDateValue("OpenDate");
            entity.AgingInDays = r.GetInt16Value("AgingInDays");

            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");
            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            entity.RecItemNumber = r.GetStringValue ("RecItemNumber");
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate");
            return entity;
        }




    }//end of class
}//end of namespace


