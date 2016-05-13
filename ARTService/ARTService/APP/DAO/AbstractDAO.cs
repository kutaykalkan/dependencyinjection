using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public abstract class AbstractDAO
    {
        //private string GetConnectionString()
        //{
        //    string conStr = "";
        //    conStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        //    return conStr;
        //}

        //public SqlConnection GetConnection()
        //{
        //    return new SqlConnection(GetConnectionString());
        //}

        protected CompanyUserInfo CompanyUserInfo;

        public AbstractDAO(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }
        public string strCommandTimeOut
        {
            get
            {
                return Helper.GetAppSettingFromKey("DBCommandTimeOut");
            }
        }
        
        public SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = Convert.ToInt32(strCommandTimeOut);
            return cmd;
        }

        public SqlCommand CreateCommand(string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandTimeout = Convert.ToInt32(strCommandTimeOut);
            return cmd;
        }

        public string GetConnectionStringBase()
        {
            string commonConnectionString = ConfigurationManager.AppSettings[ConnectionStringConstants.CONNECTION_STRING_SKYSTEMART_BASE];
            return commonConnectionString;
        }

        public string GetConnectionStringCore()
        {
            string commonConnectionString = ConfigurationManager.AppSettings[ConnectionStringConstants.CONNECTION_STRING_SKYSTEMART_CORE];
            return commonConnectionString;
        }

        public string GetConnectionStringCreateCompany()
        {
            string commonConnectionString = ConfigurationManager.AppSettings[ConnectionStringConstants.CONNECTION_STRING_SKYSTEMART_CREATE_COMPANY];
            return commonConnectionString;
        }

        public string GetConnectionString()
        {
            string formatedCommonConnectionString = "";
            string commonConnectionString = ConfigurationManager.AppSettings[ConnectionStringConstants.CONNECTION_STRING_SKYSTEMART_SPECIFIC];
            if (!String.IsNullOrEmpty (this.CompanyUserInfo.Instance))
                formatedCommonConnectionString = String.Format(commonConnectionString, this.CompanyUserInfo.ServerName + @"\" + this.CompanyUserInfo.Instance, this.CompanyUserInfo.DatabaseName, this.CompanyUserInfo.DBUserID, this.CompanyUserInfo.DBPassword);
            else
                formatedCommonConnectionString = String.Format(commonConnectionString, this.CompanyUserInfo.ServerName, this.CompanyUserInfo.DatabaseName, this.CompanyUserInfo.DBUserID, this.CompanyUserInfo.DBPassword);
            return formatedCommonConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public string GetDatabaseName(string cnnstr)
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(cnnstr);
            return csb.InitialCatalog;
        }

        public SqlConnection GetConnectionWithoutDatabase(string cnnstr)
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(cnnstr);
            string str = GetConnectionStringCreateCompany();
            string ConStr = string.Format(str, csb.DataSource, csb.UserID, csb.Password);
            return new SqlConnection(ConStr);
        }
    }
}
