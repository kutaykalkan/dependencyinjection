


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
    public class WeekDayMstDAO : WeekDayMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WeekDayMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<WeekDayMstInfo> GetAllWeekDayList()
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<WeekDayMstInfo> oWeekDayMstInfoCollection = null;

            try
            {
                cmd = CreateGetAllWeekDayListCommand();
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oWeekDayMstInfoCollection = MapObjects(dr);

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
            return oWeekDayMstInfoCollection;
        }
        private IDbCommand CreateGetAllWeekDayListCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllWeekDayList");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }


    }

}