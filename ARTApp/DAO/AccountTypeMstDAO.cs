 


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
    public class AccountTypeMstDAO : AccountTypeMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountTypeMstDAO(AppUserInfo oAppUserInfo) :
            base( oAppUserInfo)
        {           
        }
        #region SelectAllAccountTypeMstInfo

        /// <summary>
        /// Selects all account types for the system
        /// </summary>
        /// <returns>List of account types</returns>
        internal List<AccountTypeMstInfo> SelectAllAccountTypeMstInfo()
        {
            List<AccountTypeMstInfo> oAccountTypeMstInfoCollection = new List<AccountTypeMstInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_SEL_AllAccountTypeMst");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oAccountTypeMstInfoCollection.Add(this.MapObject(reader));
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

            return oAccountTypeMstInfoCollection;
        }

        #endregion

    }
}