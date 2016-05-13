using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.APP.DAO;
using SkyStem.ART.Service.Utility;

namespace SkyStem.ART.Service.APP.BLL
{
    public class DatabaseBackup
    {
        public bool BackupDatabaseSkyStemARTBase(CompanyUserInfo oCompanyUserInfo)
        {
            bool status = false;
            try
            {
                Helper.LogError("START: Take Backup of SkyStemART Base Database", oCompanyUserInfo);
                DatabaseBackupDAO oDatabaseBackupDAO = new DatabaseBackupDAO(oCompanyUserInfo);
                status = oDatabaseBackupDAO.BackupDatabaseSkyStemARTBase();
                Helper.LogError("END: Take Backup of SkyStemART Base Database", oCompanyUserInfo);
            }
            catch (Exception ex)
            {
                Helper.LogError("Unable to take backup of SkyStemART Base Database. Error: " + ex.ToString(), oCompanyUserInfo);
            }
            return status;
        }
    }
}
