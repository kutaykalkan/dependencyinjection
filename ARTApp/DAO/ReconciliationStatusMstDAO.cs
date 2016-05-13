


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;

namespace SkyStem.ART.App.DAO
{
    public class ReconciliationStatusMstDAO : ReconciliationStatusMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationStatusMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        //public List<ReconciliationStatusMstInfo> GetAllRecStatus(int? companyID, int? recPeriodID)
        //{
        //    IDbCommand oCommand = this.GetAllRecStatusCommand(companyID, recPeriodID);
        //    oCommand.Connection = this.CreateConnection();


        //    return this.Select(oCommand);
        //}



        public List<ReconciliationStatusMstInfo> GetAllRecStatus(int? companyID, int? recPeriodID)
        {
            List<ReconciliationStatusMstInfo> oReconciliationStatusMstInfoCollection = new List<ReconciliationStatusMstInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.GetAllRecStatusCommand(companyID, recPeriodID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    ReconciliationStatusMstInfo oReconciliationStatusMstInfo = null;
                    oReconciliationStatusMstInfo = this.ReconciliationStatusMapObject(reader);
                    oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oReconciliationStatusMstInfoCollection;
        }
        private ReconciliationStatusMstInfo ReconciliationStatusMapObject(System.Data.IDataReader r)
        {
            ReconciliationStatusMstInfo oReconciliationStatusMstInfo = null;
            oReconciliationStatusMstInfo = base.MapObject(r);
            oReconciliationStatusMstInfo.StatusColor = r.GetStringValue("StatusColor");

            return oReconciliationStatusMstInfo;

        }

        private IDbCommand GetAllRecStatusCommand(int? companyID, int? recPeriodID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_AllRecStatus");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter cmdCompanyId = oCommand.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            if (companyID != null)
                cmdCompanyId.Value = companyID;
            else
                cmdCompanyId.Value = System.DBNull.Value;
            cmdParams.Add(cmdCompanyId);

            IDbDataParameter cmdRecPeriodId = oCommand.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            if (recPeriodID != null)
                cmdRecPeriodId.Value = recPeriodID;
            else
                cmdRecPeriodId.Value = System.DBNull.Value;
            cmdParams.Add(cmdRecPeriodId);

            return oCommand;
        }
    }
}