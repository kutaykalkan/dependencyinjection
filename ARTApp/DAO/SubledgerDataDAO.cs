


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.DAO
{
    public class SubledgerDataDAO : SubledgerDataDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SubledgerDataDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public SubledgerDataInfo GetSubledgerDataInfoByAccountIDRecPeriodID(long? AccountID, int? RecPeriodID)
        {

            System.Data.IDbCommand cmd = null;
            IDbConnection con = null;
            SubledgerDataInfo oSubledgerDataInfo = null;

            try
            {

                con = this.CreateConnection();
                cmd = CreateGetSubledgerDataInfoByAccountIDRecPeriodIDCommand(AccountID, RecPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    oSubledgerDataInfo = this.MapObject(reader);
                }
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }
            }
            return oSubledgerDataInfo;

        }
        private IDbCommand CreateGetSubledgerDataInfoByAccountIDRecPeriodIDCommand(long? AccountID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_SubledgerDataInfoByAccountIDAndRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            parAccountID.Value = AccountID.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }


        public List<SubledgerDataArchiveInfo> GetSubledgerVersionByGLDataID(GLDataParamInfo oGLDataParamInfo)
        {
            List<SubledgerDataArchiveInfo> oSubledgerDataArchiveInfoCollection = new List<SubledgerDataArchiveInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            SubledgerDataArchiveInfo oSubledgerDataArchiveInfo = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_SEL_SubledgerDataArchiveByGLDataID");
                cmd.Connection = con;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;
                System.Data.IDbDataParameter par = cmd.CreateParameter();
                par.ParameterName = "@GLDataID";
                par.Value = oGLDataParamInfo.GLDataID.Value;
                cmdParams.Add(par);
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oSubledgerDataArchiveInfo = MapSubledgerDataArchiveObject(reader);
                    oSubledgerDataArchiveInfoCollection.Add(oSubledgerDataArchiveInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oSubledgerDataArchiveInfoCollection;
        }

        private SubledgerDataArchiveInfo MapSubledgerDataArchiveObject(IDataReader r)
        {
            SubledgerDataArchiveInfo entity = new SubledgerDataArchiveInfo();

            entity.SubledgerDataArchiveID = r.GetInt64Value("SubledgerDataArchiveID");
            entity.GLDataID = r.GetInt64Value("GLDataID");
            entity.AccountID = r.GetInt64Value("AccountID");
            entity.ReconciliationPeriodID = r.GetInt32Value("RecPeriodID");
            entity.DataImportID = r.GetInt32Value("DataImportID");
            entity.SubledgerBalanceBaseCCY = r.GetDecimalValue("SubledgerBalanceBaseCCY");
            entity.SubledgerBalanceReportingCCY = r.GetDecimalValue("SubledgerBalanceReportingCCY");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            entity.ReportingCurrencyCode = r.GetStringValue("ReportingCurrencyCode");
            entity.ReconciliationStatus = r.GetStringValue("ReconciliationStatus");

            return entity;
        }

        public SubledgerDataInfo GetSubledgerDataImportIDByNetAccountIDRecPeriodID(int? NetAccountID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = null;
            IDbConnection con = null;
             SubledgerDataInfo oSubledgerDataInfo = null;
            try
            {
                con = this.CreateConnection();
                cmd = CreateGetSubledgerDataImportIDByNetAccountIDRecPeriodIDCommand(NetAccountID, RecPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    oSubledgerDataInfo = this.MapObject(reader);
                }
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }
            }
             return oSubledgerDataInfo;
          

        }
        private IDbCommand CreateGetSubledgerDataImportIDByNetAccountIDRecPeriodIDCommand(int? NetAccountID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_SubledgerDataInfoByNetAccountIDAndRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            if (NetAccountID.HasValue)
                parNetAccountID.Value = NetAccountID.Value;
            else
                parNetAccountID.Value = DBNull.Value;
            cmdParams.Add(parNetAccountID);
            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }
    }
}