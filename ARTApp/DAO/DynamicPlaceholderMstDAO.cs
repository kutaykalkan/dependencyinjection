 


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
    public class DynamicPlaceholderMstDAO : DynamicPlaceholderMstDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicPlaceholderMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<DynamicPlaceholderMstInfo> GetAllDynamicPlaceholderMstInfo()
        {
            DynamicPlaceholderMstInfo oDynamicPlaceholderMstInfo = null;
            IDbConnection conn = null;
            List<DynamicPlaceholderMstInfo> objDynamicPlaceholderMstInfocollection = new List<DynamicPlaceholderMstInfo>();
            
            try
            {
                conn = CreateConnection();
                conn.Open();

                IDbCommand cmd;
                cmd = CreateSelectCommand();
                cmd.Connection = conn;

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oDynamicPlaceholderMstInfo = (DynamicPlaceholderMstInfo)MapObject(reader);
                    objDynamicPlaceholderMstInfocollection.Add(oDynamicPlaceholderMstInfo);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (conn != null)
                        conn.Close();
                }
                catch (Exception)
                {
                }
            }
            return objDynamicPlaceholderMstInfocollection;

        }

        protected System.Data.IDbCommand CreateSelectCommand()
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_DynamicPlaceHolderMst");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;

        }
    }
}