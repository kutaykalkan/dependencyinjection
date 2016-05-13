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
    public class GLDataRecurringItemScheduleIntervalDetailDAO : GLDataRecurringItemScheduleIntervalDetailDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataRecurringItemScheduleIntervalDetailDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<GLDataRecurringItemScheduleIntervalDetailInfo> SelectRecurringItemScheduleIntervalDetailRecurringItemScheduleID(long? GLDataRecurringItemScheduleID)
        {
            List<GLDataRecurringItemScheduleIntervalDetailInfo> oGLDataRecurringItemScheduleIntervalDetailInfoList = new List<GLDataRecurringItemScheduleIntervalDetailInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_SEL_GLDataRecurringItemScheduleIntervalDetailByGLDataRecurringItemScheduleID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdGLDataRecurringItemScheduleID = cmd.CreateParameter();
                cmdGLDataRecurringItemScheduleID.ParameterName = "@GLDataRecurringItemScheduleID";
                cmdGLDataRecurringItemScheduleID.Value = GLDataRecurringItemScheduleID;
                cmdParams.Add(cmdGLDataRecurringItemScheduleID);


                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataRecurringItemScheduleIntervalDetailInfo oGLDataRecurringItemScheduleIntervalDetailInfo = null;
                while (reader.Read())
                {
                    oGLDataRecurringItemScheduleIntervalDetailInfo = this.MapObject(reader);
                    oGLDataRecurringItemScheduleIntervalDetailInfoList.Add(oGLDataRecurringItemScheduleIntervalDetailInfo);
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

            return oGLDataRecurringItemScheduleIntervalDetailInfoList;
        }

        protected override GLDataRecurringItemScheduleIntervalDetailInfo MapObject(System.Data.IDataReader r)
        {
            GLDataRecurringItemScheduleIntervalDetailInfo entity = new GLDataRecurringItemScheduleIntervalDetailInfo();
            entity = base.MapObject(r);
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate");
            return entity;
        }
    }
}