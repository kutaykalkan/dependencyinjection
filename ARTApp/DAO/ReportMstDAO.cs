


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.DAO
{
    public class ReportMstDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReportMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public IList<ReportMstInfo> GetAllReportByRoleID(short? roleID, int? RecPeriodID, int? companyID)
        {
            IList<ReportMstInfo> oReportMstInfoCollection = new List<ReportMstInfo>();
            ReportMstInfo oReportMstInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                oCommand = CreateSelectAllReportByRoleIDCommand(roleID, RecPeriodID, companyID);
                oCommand.Connection = oConnection;
                IDataReader oDataReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDataReader.Read())
                {
                    oReportMstInfo = (ReportMstInfo)MapObjectAllReportbyRoleID(oDataReader);
                    oReportMstInfoCollection.Add(oReportMstInfo);
                }
                oDataReader.Close();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oReportMstInfoCollection;
        }


        public IList<ReportMstInfo> GetAllMyReportByRoleID(short? roleID, int? userID, int? RecPeriodID)
        {
            IList<ReportMstInfo> oReportMstInfoCollection = new List<ReportMstInfo>();
            ReportMstInfo oReportMstInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                oCommand = CreateSelectAllMyReportByRoleIDCommand(roleID, userID, RecPeriodID);
                oCommand.Connection = oConnection;
                IDataReader oDataReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDataReader.Read())
                {
                    oReportMstInfo = (ReportMstInfo)MapObjectAllReportbyRoleID(oDataReader);
                    oReportMstInfoCollection.Add(oReportMstInfo);
                }
                oDataReader.Close();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oReportMstInfoCollection;

        }
        public IList<ReportMstInfo> GetAllMandatoryReportList(short? roleID, int? userID, int? recPeriodID)
        {
            IList<ReportMstInfo> oReportMstInfoCollection = new List<ReportMstInfo>();
            ReportMstInfo oReportMstInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                oCommand = CreateSelectAllMandatoryReportCommand(roleID, userID, recPeriodID);
                oCommand.Connection = oConnection;
                IDataReader oDataReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDataReader.Read())
                {
                    oReportMstInfo = (ReportMstInfo)MapObjectAllReportbyRoleID(oDataReader);
                    oReportMstInfoCollection.Add(oReportMstInfo);
                }
                oDataReader.Close();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oReportMstInfoCollection;
        }

        public List<ReportMstInfo> GetAllReportsByPackageId(short? iPackageId)
        {
            IDbCommand oCommand = this.GetReportsByPackageIdCommand(iPackageId);
            oCommand.Connection = this.CreateConnection();
            List<ReportMstInfo> lstReportMstInfo = this.Select(oCommand);
            return lstReportMstInfo;
        }

        public ReportMstInfo GetReportByReportID(short? reportID, int languageID, int businessEntityID, int defaultLanguageID, int? companyID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            IDataReader reader = null;
            try
            {
                oCommand = this.GetReportByReportIDCommand(reportID, languageID, businessEntityID, defaultLanguageID, companyID);
                oConnection = this.CreateConnection();
                oCommand.Connection = oConnection;
                oCommand.Connection.Open();
                reader = oCommand.ExecuteReader();
                reader.Read();
                return this.MapObject(reader);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }


        private IDbCommand GetReportsByPackageIdCommand(short? iPackageId)
        {
            IDbCommand oCommand = this.CreateCommand("usp_GET_ReportsByPackageId");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramPackageId = oCommand.CreateParameter();
            paramPackageId.ParameterName = "@PackageId";
            paramPackageId.Value = iPackageId;
            cmdParams.Add(paramPackageId);

            return oCommand;
        }

        private IDbCommand GetReportByReportIDCommand(short? reportID, int languageID, int businessEntityID, int defaultLanguageID, int? companyID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_GET_ReportByReportID");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramReportID = oCommand.CreateParameter();
            paramReportID.ParameterName = "@ReportID";
            paramReportID.Value = reportID.Value;

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LanguageID";
            paramLanguageID.Value = languageID;

            IDbDataParameter paramBusinessEntityID = oCommand.CreateParameter();
            paramBusinessEntityID.ParameterName = "@BusinessEntityID";
            paramBusinessEntityID.Value = businessEntityID;

            IDbDataParameter paramDefaultLanguageID = oCommand.CreateParameter();
            paramDefaultLanguageID.ParameterName = "@DefaultLanguageID";
            paramDefaultLanguageID.Value = defaultLanguageID;

            cmdParams.Add(paramReportID);
            cmdParams.Add(paramLanguageID);
            cmdParams.Add(paramBusinessEntityID);
            cmdParams.Add(paramDefaultLanguageID);

            return oCommand;
        }

        protected System.Data.IDbCommand CreateSelectAllReportByRoleIDCommand(short? roleID, int? RecPeriodID, int? companyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_StandardReportsByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = roleID;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);

            return cmd;
        }

        protected System.Data.IDbCommand CreateSelectAllMyReportByRoleIDCommand(short? roleID, int? userID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_MyReportsByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = roleID;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }
        protected System.Data.IDbCommand CreateSelectAllMandatoryReportCommand(short? roleID, int? userID, int? recPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_MandatoryReportList");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = roleID;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            return cmd;
        }

        protected ReportMstInfo MapObjectAllReportbyRoleID(System.Data.IDataReader r)
        {
            ReportMstInfo entity = new ReportMstInfo();
            entity.ReportID = r.GetInt16Value("ReportID");
            entity.Report = r.GetStringValue("Report");
            entity.ReportLabelID = r.GetInt32Value("ReportLabelID");
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            entity.ReportTypeLabelID = r.GetInt32Value("ReportTypeLabelID");
            entity.ReportType = r.GetStringValue("ReportType");
            entity.SignOffDate = r.GetDateValue("SignOffDate");
            entity.UserMyReportID = r.GetInt32Value("UserMyReportID");
            entity.ReportRoleMandatoryReportID = r.GetInt32Value("ReportRoleMandatoryReportID");
            return entity;

        }

        public int InsertUserMyReport(short? roleID, int? userID, ReportMstInfo oReportInfo, string myReportName, IDbConnection oConnection, IDbTransaction oTransaction)
        {

            int newMyReoprtSavedReportID = -1;
            try
            {
                System.Data.IDbCommand cmd = null;
                cmd = GetNewMyReportInsertCommand(roleID, userID, oReportInfo, myReportName);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                newMyReoprtSavedReportID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                return newMyReoprtSavedReportID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private System.Data.IDbCommand GetNewMyReportInsertCommand(short? roleID, int? userID, ReportMstInfo entity, string myReportName)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_UserMyReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parMyReportName = cmd.CreateParameter();
            parMyReportName.ParameterName = "@MyReportName";
            if (!string.IsNullOrEmpty(myReportName))
                parMyReportName.Value = myReportName;
            else
                parMyReportName.Value = System.DBNull.Value;
            cmdParams.Add(parMyReportName);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@roleID";
            if (roleID.HasValue)
                parRoleID.Value = roleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@userID";
            if (userID.HasValue)
                parUserID.Value = userID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);


            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (!entity.IsReportIDNull)
                parReportID.Value = entity.ReportID;
            else
                parReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportID);

            return cmd;

        }

        public bool InsertUserMyReportParameter(int userMySaveReportId, List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterCollection, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            System.Data.IDbCommand cmd = null;
            bool result = false;
            try
            {
                cmd = InsertUserMyReportParameter(userMySaveReportId, oUserMyReportSavedReportParameterCollection);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }



        private IDbCommand InsertUserMyReportParameter(int userMySaveReportId, List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterCollection)
        {


            DataTable dtUserMyReportSavedReportParams = ReportServiceHelper.ConvertUserMyReportSavedReportParameterListToDataTable(oUserMyReportSavedReportParameterCollection, userMySaveReportId);
            IDbCommand cmd = null;
            cmd = this.CreateCommand("usp_INS_UserMyReportSavedReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parmRptCriteriaCollectionTable = cmd.CreateParameter();
            parmRptCriteriaCollectionTable.ParameterName = "@RptCriteriaCollectionTable";
            parmRptCriteriaCollectionTable.Value = dtUserMyReportSavedReportParams;
            cmdParams.Add(parmRptCriteriaCollectionTable);

            return cmd;
        }



        public bool DeleteMyReportByReportID(short? roleID, int? userID, short? reportID)
        {
            System.Data.IDbCommand cmd = null;
            bool result = false;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                cmd = DeleteMyReportByReportIDCommand(roleID, userID, reportID);
                cmd.Connection = oConnection;
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }


            return result;


        }

        protected System.Data.IDbCommand DeleteMyReportByReportIDCommand(short? roleID, int? userID, short? reportID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_MyReportsByReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = roleID;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            parReportID.Value = reportID.Value;
            cmdParams.Add(parReportID);

            return cmd;
        }

    }




}