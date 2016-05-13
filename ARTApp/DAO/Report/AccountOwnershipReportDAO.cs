


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
    public class AccountOwnershipReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountOwnershipReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<AccountOwnershipReportInfo> GetReportAccountOwnershipReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            List<AccountOwnershipReportInfo> oAccountOwnershipReportInfoCollection = new List<AccountOwnershipReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportAccountOwnershipReportCommand(oReportSearchCriteria, tblUserSearch, tblRoleSearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                AccountOwnershipReportInfo oAccountOwnershipReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    oAccountOwnershipReportInfo = MapAccountObject(reader);
                    //oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountOwnershipReportInfo);
                    oAccountOwnershipReportInfoCollection.Add(oAccountOwnershipReportInfo);
                    //this.MapAccountAttributeInfo(reader, oAccountOwnershipReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oAccountOwnershipReportInfoCollection;
        }

        private IDbCommand CreateGetReportAccountOwnershipReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_AccountOwnershipReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForUserRoleSearchInReport(tblUserSearch, tblRoleSearch, cmd, cmdParams);

            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@ReconciliationPeriodID";
            cmdRecPeriod.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ReconciliationPeriodID);
            cmdParams.Add(cmdRecPeriod);


            IDbDataParameter cmdRequesterUserID = cmd.CreateParameter();
            cmdRequesterUserID.ParameterName = "@RequesterUserID";
            cmdRequesterUserID.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.RequesterUserID);
            cmdParams.Add(cmdRequesterUserID);

            IDbDataParameter cmdRequesterRoleID = cmd.CreateParameter();
            cmdRequesterRoleID.ParameterName = "@RequesterRoleID";
            cmdRequesterRoleID.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.RequesterRoleID);
            cmdParams.Add(cmdRequesterRoleID);


            return cmd;
        }


        private AccountOwnershipReportInfo MapAccountObject(IDataReader r)
        {
            AccountOwnershipReportInfo entity = new AccountOwnershipReportInfo();

            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.RoleLabelID = r.GetInt32Value("RoleLabelID");
            entity.CountTotalAccounts = r.GetInt16Value("CountTotalAccounts");

            entity.CountTotalAccountAssigned = r.GetInt16Value("CountTotalAccountAssigned");
            entity.CountHighAccounts = r.GetInt16Value("CountHighAccounts");
            entity.CountKeyAccounts = r.GetInt16Value("CountKeyAccounts");
            entity.CountLowAccounts = r.GetInt16Value("CountLowAccounts");
            entity.CountMaterialAccounts = r.GetInt16Value("CountMaterialAccounts");
            entity.CountMediumAccounts = r.GetInt16Value("CountMediumAccounts");
            entity.PercentAccountAssigned = r.GetDecimalValue("PercentAccountAssigned");
            entity.PercentKeyAccounts = r.GetDecimalValue("PercentKeyAccounts");

            entity.PercentHighAccounts = r.GetDecimalValue("PercentHighAccounts");
            entity.PercentMediumAccounts = r.GetDecimalValue("PercentMediumAccounts");
            entity.PercentLowAccounts = r.GetDecimalValue("PercentLowAccounts");
            entity.PercentMaterialAccounts = r.GetDecimalValue("PercentMaterialAccounts");
            return entity;
        }




    }//end of class
}//end of namespace


