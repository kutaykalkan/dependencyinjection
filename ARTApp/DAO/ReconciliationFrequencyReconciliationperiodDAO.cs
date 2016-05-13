


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
    public class ReconciliationFrequencyReconciliationperiodDAO : ReconciliationFrequencyReconciliationperiodDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationFrequencyReconciliationperiodDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<ReconciliationFrequencyReconciliationperiodInfo> GetAllRecPeriodByRecFrequencyID(int RecFrequencyID)
        {
            List<ReconciliationFrequencyReconciliationperiodInfo> oReconciliationFrequencyReconciliationperiodInfocollection = new List<ReconciliationFrequencyReconciliationperiodInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.SelectAllByRecPeriodID(RecFrequencyID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ReconciliationFrequencyReconciliationperiodInfo oReconciliationFrequencyReconciliationperiodInfo = new ReconciliationFrequencyReconciliationperiodInfo();
                while (reader.Read())
                {
                    oReconciliationFrequencyReconciliationperiodInfo = this.MapObject(reader);
                    oReconciliationFrequencyReconciliationperiodInfocollection.Add(oReconciliationFrequencyReconciliationperiodInfo);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oReconciliationFrequencyReconciliationperiodInfocollection;
        }

        private IDbCommand SelectAllByRecPeriodID(int RecFrequencyID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_ReconciliationFrequencyReconciliationperiodInfo");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecFrequencyID";
            par.Value = RecFrequencyID;
            cmdParams.Add(par);
            return cmd;


        }
    }
}