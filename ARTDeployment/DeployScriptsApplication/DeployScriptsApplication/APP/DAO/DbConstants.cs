
/******************************************
 * Auto-generated by Adapdev Codus v1.4.0 - Trial Use Only
 * 
 ******************************************/
using System;
using System.Configuration;
using Adapdev.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
    
namespace SkyStem.ART {
    
    public class DbConstants {
        
        public static readonly Adapdev.Data.DbProviderType DatabaseProviderType = Adapdev.Data.DbProviderType.SQLSERVER;
        
        public static readonly Adapdev.Data.DbType DatabaseType = Adapdev.Data.DbType.SQLSERVER;
        
        //public static readonly string ConnectionString = @"Data Source=192.168.1.35\DEV; Initial Catalog=SkyStemARTCore; User ID=sa; Password=arxmind_123; Trusted_Connection=false;";
        //public static readonly string ConnectionString = ConfigurationManager.AppSettings["connectionString"];
        
        public static string ConnectionString
        {
            get 
            {

                string conn = ConfigurationManager.AppSettings["connectionStringCore"];
                if (null == conn || conn.Length == 0)
                    return @"Data Source=192.168.1.35\DEV; Initial Catalog=SkyStemARTCore; User ID=sa; Password=arxmind_123; Trusted_Connection=false;";
                else
                    return conn;
            }
        }
    }
    public class DbHelper
    {
        private DbHelper()
        {
        }

        public static string GetConnectionString(ServerCompanyInfo oServerCompanyInfo)
        {
            string cnnString = null;
            if (oServerCompanyInfo != null)
            {              
                    string serverName = oServerCompanyInfo.ServerName;
                    if (!string.IsNullOrEmpty(oServerCompanyInfo.Instance))
                        serverName = serverName + @"\" + oServerCompanyInfo.Instance;
                    cnnString = ConfigurationManager.AppSettings["connectionStringSpecific"];
                    cnnString = string.Format(cnnString, serverName, oServerCompanyInfo.DatabaseName, oServerCompanyInfo.DBUserID, oServerCompanyInfo.DBPassword);
               
            }
            return cnnString;
        }
    }
}
