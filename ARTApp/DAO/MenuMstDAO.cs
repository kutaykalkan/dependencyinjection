


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Params;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class MenuMstDAO : MenuMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MenuMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<MenuMstInfo> GetMenuByRoleID(MenuParamInfo oMenuParamInfo)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<MenuMstInfo> oMenuMstInfoCollection = null;

            try
            {
                cmd = CreateGetMenuByRoleIDCommand(oMenuParamInfo);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oMenuMstInfoCollection = MapObjects(dr);
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

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
            return oMenuMstInfoCollection;

        }

        private IDbCommand CreateGetMenuByRoleIDCommand(MenuParamInfo oMenuParamInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_MenuMstByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonRoleAndRecPeriodParameters(oMenuParamInfo, cmd);

            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@CompanyID";
            parRoleID.Value = oMenuParamInfo.CompanyID.Value;
            cmdParams.Add(parRoleID);
            return cmd;
        }
    }
}