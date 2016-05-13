


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
    public class CompanyAlertDAO : CompanyAlertDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyAlertDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        #region SelectComapnyAlertByCompanyIDAndRecPeriodID

        public List<CompanyAlertInfo> SelectComapnyAlertByCompanyIDAndRecPeriodID(int companyID, int? recPeriodID)
        {
            List<CompanyAlertInfo> oCompanyAlertInfoCollection = new List<CompanyAlertInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.createSelectComapnyAlertByCompanyIDAndRecPeriodIDCommand(companyID, recPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    oCompanyAlertInfoCollection.Add(this.MapObject(reader));
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oCompanyAlertInfoCollection;
        }

        private IDbCommand createSelectComapnyAlertByCompanyIDAndRecPeriodIDCommand(int companyID, int? recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_ComapnyAlertsByCompanyIDAndRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramCompanyID = cmd.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = companyID;
            cmdParams.Add(paramCompanyID);

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";

            if (recPeriodID.HasValue && recPeriodID.Value > 0)
            {
                paramRecPeriodID.Value = recPeriodID;
            }
            else
            {
                paramRecPeriodID.Value = DBNull.Value;
            }
            cmdParams.Add(paramRecPeriodID);

            return cmd;
        }

        #endregion

        #region UpdateCompanyAlert

        public void UpdateCompanyAlert(DataTable dtComapnyAlert, int recPeriodID, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);
            try
            {
                cmd = this.CreateUpdateCompanyAlertCommand(dtComapnyAlert, recPeriodID);

                if (isConnectionNull)
                {
                    con = this.CreateConnection();
                    con.Open();
                }

                cmd.Connection = con;

                if (!isConnectionNull)
                {
                    cmd.Transaction = oTransaction;
                }

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (isConnectionNull && con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        private IDbCommand CreateUpdateCompanyAlertCommand(DataTable dtComapnyAlert, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_CompanyAlert");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramtblCompanyAlert = cmd.CreateParameter();
            paramtblCompanyAlert.ParameterName = "@tblCompanyAlert";
            paramtblCompanyAlert.Value = dtComapnyAlert;
            cmdParams.Add(paramtblCompanyAlert);

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            return cmd;
        }

        #endregion
        public List<CompanyAlertInfo> GetRaiseAlertData()
        {
            List<CompanyAlertInfo> oCompanyAlertInfoList = new List<CompanyAlertInfo>();
            IDbConnection con = this.CreateConnection();
            try
            {
                con.Open();
                IDbCommand oCommand = CreateGetCompanyAlertsCommand();
                oCommand.Connection = con;
                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oCompanyAlertInfoList.Add(MapObjectCompanyAlertInfo(reader));
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oCompanyAlertInfoList;
        }
        private IDbCommand CreateGetCompanyAlertsCommand()
        {
            IDbCommand cmd = this.CreateCommand("usp_SRV_SEL_CompanyAlerts");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
        private CompanyAlertInfo MapObjectCompanyAlertInfo(IDataReader reader)
        {
            CompanyAlertInfo oCompanyAlertInfo = new CompanyAlertInfo();
            int ordinal;
            ordinal = reader.GetOrdinal("CompanyID");
            if (!reader.IsDBNull(ordinal))
                oCompanyAlertInfo.CompanyID = ((System.Int32)(reader.GetValue(ordinal)));

            ordinal = reader.GetOrdinal("ReconciliationPeriodID");
            if (!reader.IsDBNull(ordinal))
                oCompanyAlertInfo.RecPeriodID = ((System.Int32)(reader.GetValue(ordinal)));

            ordinal = reader.GetOrdinal("AlertID");
            if (!reader.IsDBNull(ordinal))
                oCompanyAlertInfo.AlertID = ((System.Int16)(reader.GetValue(ordinal)));

            ordinal = reader.GetOrdinal("AlertLabelID");
            if (!reader.IsDBNull(ordinal))
                oCompanyAlertInfo.AlertLabelID = ((System.Int32)(reader.GetValue(ordinal)));

            ordinal = reader.GetOrdinal("CompanyAlertID");
            if (!reader.IsDBNull(ordinal))
                oCompanyAlertInfo.CompanyAlertID = ((System.Int32)(reader.GetValue(ordinal)));

            ordinal = reader.GetOrdinal("AlertSubjectLabelID");
            if (!reader.IsDBNull(ordinal))
                oCompanyAlertInfo.AlertSubjectLabelID = ((System.Int32)(reader.GetValue(ordinal)));

            ordinal = reader.GetOrdinal("CompanyNameLabelID");
            if (!reader.IsDBNull(ordinal))
                oCompanyAlertInfo.CompanyNameLabelID = ((System.Int32)(reader.GetValue(ordinal)));

            return oCompanyAlertInfo;
        }
        public void CreateDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.CreateMakeDataForCompanyAlertIDCommand(oCompanyAlertInfo);
            cmd.Connection = con;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }
        private IDbCommand CreateMakeDataForCompanyAlertIDCommand(CompanyAlertInfo oCompanyAlertInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_SRV_INS_ProcessAlerts");
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
            IDbDataParameter paramAlertLabelID = oCommand.CreateParameter();
            paramAlertLabelID.ParameterName = "@AlertLabelID";
            paramAlertLabelID.Value = oCompanyAlertInfo.AlertLabelID;
            cmdParams.Add(paramAlertLabelID);
            IDbDataParameter paramCompanyAlertID = oCommand.CreateParameter();
            paramCompanyAlertID.ParameterName = "@CompanyAlertID";
            paramCompanyAlertID.Value = oCompanyAlertInfo.CompanyAlertID;
            cmdParams.Add(paramCompanyAlertID);           
        }     
    }
}