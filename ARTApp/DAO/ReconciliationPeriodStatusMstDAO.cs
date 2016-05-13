 


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
    public class ReconciliationPeriodStatusMstDAO : ReconciliationPeriodStatusMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationPeriodStatusMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal ReconciliationPeriodStatusMstInfo GetRecPeriodStatus(int? RecPeriodID)
        {
            ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = null;
            System.Data.IDbCommand cmd = null;

            try
            {
                cmd = CreateGetRecPeriodStatusCommand(RecPeriodID);
                oReconciliationPeriodStatusMstInfo = this.Select(cmd)[0];
                return oReconciliationPeriodStatusMstInfo;
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

        }

        private IDbCommand CreateGetRecPeriodStatusCommand(int? RecPeriodID)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("usp_GET_RecPeriodStatus");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            System.Data.IDbDataParameter parRecPeriodID = oCommand.CreateParameter();
            parRecPeriodID.ParameterName = "@ReconciliationPeriodID";
            parRecPeriodID.Value = RecPeriodID;
            cmdParams.Add(parRecPeriodID);

            return oCommand;
        }

    }
}