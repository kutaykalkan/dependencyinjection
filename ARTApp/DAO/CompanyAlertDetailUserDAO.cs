


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class CompanyAlertDetailUserDAO : CompanyAlertDetailUserDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyAlertDetailUserDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<CompanyAlertDetailUserInfo> GetAlertMailDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo)
        {
            List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList = new List<CompanyAlertDetailUserInfo>();
            IDbConnection con = this.CreateConnection();
            try
            {
                con.Open();
                IDbCommand oCommand = CreateGetAlertMailDataForCompanyAlertIDCommand(oCompanyAlertInfo);
                oCommand.Connection = con;
                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oCompanyAlertDetailUserInfoList.Add(MapCompanyAlertDetailUserInfoObject(reader));
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oCompanyAlertDetailUserInfoList;
        }
        private IDbCommand CreateGetAlertMailDataForCompanyAlertIDCommand(CompanyAlertInfo oCompanyAlertInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_SRV_SEL_AlertMailData");
            cmd.CommandType = CommandType.StoredProcedure;
            AddParamsToCommandForAlertProcessing(oCompanyAlertInfo, cmd);
            return cmd;
        }
        private void AddParamsToCommandForAlertProcessing(CompanyAlertInfo oCompanyAlertInfo, IDbCommand oCommand)
        {
            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter paramtblcompanyID = oCommand.CreateParameter();
            paramtblcompanyID.ParameterName = "@companyID";
            paramtblcompanyID.Value = oCompanyAlertInfo.CompanyID;
            cmdParams.Add(paramtblcompanyID);
            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = oCompanyAlertInfo.RecPeriodID;
            cmdParams.Add(paramRecPeriodID);
            IDbDataParameter paramAlertID = oCommand.CreateParameter();
            paramAlertID.ParameterName = "@AlertID";
            paramAlertID.Value = oCompanyAlertInfo.AlertID;
            cmdParams.Add(paramAlertID);
            IDbDataParameter paramCompanyAlertID = oCommand.CreateParameter();
            paramCompanyAlertID.ParameterName = "@CompanyAlertID";
            paramCompanyAlertID.Value = oCompanyAlertInfo.CompanyAlertID;
            cmdParams.Add(paramCompanyAlertID);
        }
        private CompanyAlertDetailUserInfo MapCompanyAlertDetailUserInfoObject(IDataReader reader)
        {
            CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo = new CompanyAlertDetailUserInfo();
            oCompanyAlertDetailUserInfo.CompanyAlertDetailUserID = reader.GetInt64Value("CompanyAlertDetailUserID");
            oCompanyAlertDetailUserInfo.AlertDescription = reader.GetStringValue("AlertDescription");
            oCompanyAlertDetailUserInfo.FirstName = reader.GetStringValue("FirstName");
            oCompanyAlertDetailUserInfo.LastName = reader.GetStringValue("LastName");
            if (reader.GetInt32Value("CompanyID").HasValue)
                oCompanyAlertDetailUserInfo.CompanyID = reader.GetInt32Value("CompanyID").Value;
            if (reader.GetInt32Value("DefaultLanguageID").HasValue)
                oCompanyAlertDetailUserInfo.LanguageID = reader.GetInt32Value("DefaultLanguageID").Value;
            oCompanyAlertDetailUserInfo.EmailID = reader.GetStringValue("EmailID");
            oCompanyAlertDetailUserInfo.CompanyAlertDetailID = reader.GetInt64Value("CompanyAlertDetailID");
            oCompanyAlertDetailUserInfo.PeriodEndDate = reader.GetDateValue("PeriodEndDate");
            oCompanyAlertDetailUserInfo.UserID = reader.GetInt32Value("UserID").Value;
            oCompanyAlertDetailUserInfo.RoleID = reader.GetInt16Value("RoleID").Value;
            if (reader.GetInt16Value("AlertID").HasValue)
                oCompanyAlertDetailUserInfo.AlertID = reader.GetInt16Value("AlertID").Value;
            if (reader.GetInt32Value("RecPeriodID").HasValue)
                oCompanyAlertDetailUserInfo.RecPeriodID = reader.GetInt32Value("RecPeriodID").Value;
            if (reader.GetInt32Value("AlertSubjectLabelID").HasValue)
                oCompanyAlertDetailUserInfo.AlertSubjectLabelID = reader.GetInt32Value("AlertSubjectLabelID").Value;
            if (reader.GetInt32Value("CompanyNameLabelID").HasValue)
                oCompanyAlertDetailUserInfo.CompanyNameLabelID = reader.GetInt32Value("CompanyNameLabelID").Value;

            return oCompanyAlertDetailUserInfo;
        }
        public void UpdateSentMailStatus(DataTable dtCompanyAlertDetailUserIDTable, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.CreateUpdateSentMailStatusCommand(dtCompanyAlertDetailUserIDTable);
            cmd.Connection = con;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }
        private IDbCommand CreateUpdateSentMailStatusCommand(DataTable dtCompanyAlertDetailUserIDTable)
        {
            IDbCommand oCommand = this.CreateCommand("usp_SRV_UPD_CompanyAlertDetailUserEmailSentDate");
            oCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramCompanyAlertDetailUserIDTable = oCommand.CreateParameter();
            paramCompanyAlertDetailUserIDTable.ParameterName = "@CompanyAlertDetailUserIDTableType";
            paramCompanyAlertDetailUserIDTable.Value = dtCompanyAlertDetailUserIDTable;
            cmdParams.Add(paramCompanyAlertDetailUserIDTable);

            IDbDataParameter paramEmailSentDateTime = oCommand.CreateParameter();
            paramEmailSentDateTime.ParameterName = "@EmailSentDateTime";
            paramEmailSentDateTime.Value = DateTime.Now;
            cmdParams.Add(paramEmailSentDateTime);

            IDbDataParameter paramRevisedBY = oCommand.CreateParameter();
            paramRevisedBY.ParameterName = "@RevisedBY";
            paramRevisedBY.Value = DateTime.Now;
            cmdParams.Add(paramRevisedBY);
            return oCommand;
        }
        public List<CompanyAlertDetailUserInfo> GetUserListForNewAccountAlert(int dataImportID, int companyID)
        {
            List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList = new List<CompanyAlertDetailUserInfo>();
            IDbConnection con = this.CreateConnection();
            try
            {
                con.Open();
                IDbCommand oCommand = CreateGetUserListForNewAccountAlertCommand(dataImportID, companyID);
                oCommand.Connection = con;
                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oCompanyAlertDetailUserInfoList.Add(MapCompanyAlertDetailUserInfoObject(reader));
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oCompanyAlertDetailUserInfoList;
        }
        private IDbCommand CreateGetUserListForNewAccountAlertCommand(int dataImportID, int companyID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_CompanyAlertDetailUserForNewAccountAvailableAlert");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter paramtbldataImportID = oCommand.CreateParameter();
            paramtbldataImportID.ParameterName = "@DataImportID";
            paramtbldataImportID.Value = dataImportID;
            cmdParams.Add(paramtbldataImportID);
            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@CompanyID";
            paramRecPeriodID.Value = companyID;
            cmdParams.Add(paramRecPeriodID);
            return oCommand;
        }
    }
}