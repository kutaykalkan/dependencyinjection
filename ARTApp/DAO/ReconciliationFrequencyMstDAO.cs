


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class ReconciliationFrequencyMstDAO : ReconciliationFrequencyMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationFrequencyMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        internal List<ReconciliationFrequencyMstInfo> GetAllReconciliationFrequencyMstInfo()
        {
            List<ReconciliationFrequencyMstInfo> oReconciliationFrequencyMstInfoCollection = new List<ReconciliationFrequencyMstInfo>();
            System.Data.IDbCommand oCommand = null;

            try
            {
                oCommand = CreateGetAllReconciliationFrequencyMstInfoCommand();
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                IDataReader reader = oCommand.ExecuteReader();
                while (reader.Read())
                {
                    oReconciliationFrequencyMstInfoCollection.Add(MapObject(reader));
                }
                return oReconciliationFrequencyMstInfoCollection;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }
        }


        protected System.Data.IDbCommand CreateGetAllReconciliationFrequencyMstInfoCommand()
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("usp_SEL_ReconciliationFrequencyMst");
            oCommand.CommandType = CommandType.StoredProcedure;

            return oCommand;
        }


    }//end of class
}//end of namespace