using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Utility;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.DAO
{
    public class FTPDataImportDAO : DataImportHdrDAO
    {


        public FTPDataImportDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {

        }

        #region "Public Methods"

        public List<UserFTPConfigurationInfo> GetFTPUsers()
        {
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            SqlCommand oCmd = null;
            SqlDataReader reader = null;
            List<UserFTPConfigurationInfo> oFTPUserList = new List<UserFTPConfigurationInfo>();
            try
            {

                oConn = this.GetConnection();
                oCmd = this.GetFTPUsersComand();
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                reader = oCmd.ExecuteReader();
                while (reader.Read())
                {
                    oFTPUserList.Add(this.CustomMapObject(reader));
                }
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;

            }
            catch (Exception ex)
            {
                if (oTrans != null)
                    oTrans.Rollback();
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oFTPUserList;
        }

        public UserFTPConfigurationInfo CustomMapObject(SqlDataReader r)
        {
            UserFTPConfigurationInfo entity = new UserFTPConfigurationInfo();

            try
            {
                int ordinal = r.GetOrdinal("UserID");
                if (!r.IsDBNull(ordinal)) entity.UserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FirstName");
                if (!r.IsDBNull(ordinal)) entity.FirstName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("LastName");
                if (!r.IsDBNull(ordinal)) entity.LastName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("LoginID");
                if (!r.IsDBNull(ordinal)) entity.LoginID = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FTPLoginID");
                if (!r.IsDBNull(ordinal)) entity.FTPLoginID = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("EmailID");
                if (!r.IsDBNull(ordinal)) entity.EmailID = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyID");
                if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("DefaultLanguageID");
                if (!r.IsDBNull(ordinal)) entity.DefaultLanguageID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FTPActivationStatusId");
                if (!r.IsDBNull(ordinal)) entity.FTPActivationStatusId = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("FTPServerId");
                if (!r.IsDBNull(ordinal)) entity.FTPServerId = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportTypeID");
                if (!r.IsDBNull(ordinal)) entity.DataImportTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ImportTemplateID");
                if (!r.IsDBNull(ordinal)) entity.ImportTemplateID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FTPUploadRoleID");
                if (!r.IsDBNull(ordinal)) entity.FTPUploadRoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ProfileName");
                if (!r.IsDBNull(ordinal)) entity.ProfileName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyAlias");
                if (!r.IsDBNull(ordinal)) entity.CompanyAlias = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;

        }

        private SqlCommand GetFTPUsersComand()
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "[dbo].[usp_SEL_AllFTPUsers]";
            return oCommand;
        }

        #endregion
    }
}
