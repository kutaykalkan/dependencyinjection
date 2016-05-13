using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.CompanyDatabase;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Shared.Data;

namespace SkyStem.ART.Service.APP.DAO
{
    public class DatabaseBackupDAO: AbstractDAO
    {
        public DatabaseBackupDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }

        public bool BackupDatabaseSkyStemARTBase()
        {
            string cnnStr = this.GetConnectionStringBase();
            string dbName = this.GetDatabaseName(cnnStr);
            string BackupDBPath = SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.SKYSTEMART_BASE_DATABASE_PATH);
            using (IDbConnection cnn = this.GetConnectionWithoutDatabase(cnnStr))
            {
                cnn.Open();
                using (IDbCommand cmd = GetBackupDatabaseCommand(dbName, BackupDBPath))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
                return true;
            }
        }

        private IDbCommand GetBackupDatabaseCommand(string dbName, string BackupDBPath)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BACKUP DATABASE ");
            sb.Append(dbName);
            sb.Append(" TO DISK=N'");
            sb.Append(BackupDBPath);
            sb.Append("' WITH  NOFORMAT, INIT, ");
            sb.Append(" NAME =N'");
            sb.Append(dbName);
            sb.Append("-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10");
            System.Data.IDbCommand cmd = this.CreateCommand(sb.ToString());
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
    }
}
