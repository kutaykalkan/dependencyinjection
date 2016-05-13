
using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using System.EnterpriseServices;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class AppSettingsDAO : AppSettingsDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingsDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public AppSettingsInfo SelectAppSettingByAppSettingID(int AppSettingID)
        {
            AppSettingsInfo oAppSettingsInfo = new AppSettingsInfo();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.CreateSelectAppSettingCommand(AppSettingID))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                    while (reader.Read())
                    {
                        oAppSettingsInfo = this.MapObject(reader);

                    }
                }
            }
            return oAppSettingsInfo;
        }


        private IDbCommand CreateSelectAppSettingCommand(int AppSettingID)
        {
            IDbCommand cmd = this.CreateCommand("USP_GET_AppSettingByAppSettingID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAppSettingID = cmd.CreateParameter();
            cmdAppSettingID.ParameterName = "@AppSettingID";
            cmdAppSettingID.Value = AppSettingID;
            cmdParams.Add(cmdAppSettingID);
            return cmd;
        }

    }

}