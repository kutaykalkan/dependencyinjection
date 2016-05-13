


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
    public class DataTypeMstDAO : DataTypeMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataTypeMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<DataTypeMstInfo> GetAllDataType()
        {
            List<DataTypeMstInfo> oDataTypeMstInfoCollection = new List<DataTypeMstInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_SEL_AllDataType");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTypeMstInfo oDataTypeMstInfo = null;
                while (reader.Read())
                {
                    oDataTypeMstInfo = this.MapObject(reader);
                    oDataTypeMstInfoCollection.Add(oDataTypeMstInfo);
                }
                reader.Close();

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oDataTypeMstInfoCollection;
        }

    }
}