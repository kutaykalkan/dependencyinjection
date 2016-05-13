


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
    public class ReconciliationTemplateMstDAO : ReconciliationTemplateMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationTemplateMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<ReconciliationTemplateMstInfo> GetAllReconciliationTemplateMstInfo()
        {
            List<ReconciliationTemplateMstInfo> oReconciliationTemplateMstInfoCollection = new List<ReconciliationTemplateMstInfo>();
            IDbConnection oConnection = null;
            IDbCommand oCommand = null;

            try
            {
                oConnection = this.CreateConnection();
                oCommand = this.CreateCommand("usp_SEL_AllReconcilliationTemplate");
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Connection = oConnection;
                oConnection.Open();
                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oReconciliationTemplateMstInfoCollection.Add(this.MapObject(reader));
                }
                reader.Close();
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State != ConnectionState.Closed))
                    oConnection.Dispose();
            }

            return oReconciliationTemplateMstInfoCollection;
        }
    }
}