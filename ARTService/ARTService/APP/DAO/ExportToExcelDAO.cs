using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.Service.APP.DAO;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace ARTExportToExcelApp.APP.DAO
{
    /// <summary>
    /// 
    /// </summary>
    public class ExportToExcelDAO : AbstractDAO
    {

        public ExportToExcelDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ExportToExcelInfo> GetDataForExport()
        {
            List<ExportToExcelInfo> objExportInfoList = new List<ExportToExcelInfo>();

            IDbCommand cmd = null;
            IDataReader dr = null;
            IDbConnection cnn = null;
            try
            {
                cmd = CreateSelectCommandExportInfoList();
                cnn = this.GetConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                ExportToExcelInfo oBulkExportInfo;
                while (dr.Read())
                {
                    oBulkExportInfo = this.MapObject(dr);
                    objExportInfoList.Add(oBulkExportInfo);
                }
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return objExportInfoList;
        }
        private IDbCommand CreateSelectCommandExportInfoList()
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "[Request].[usp_SEL_ExportToExcelDetails]";
            return oCommand;
        }

        public void InsertAttachmentData(ExportToExcelParamInfo objExportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            try
            {
                cmd = CreateInsertCommandExportAttachments(objExportInfo);
                cnn = this.GetConnection();
                cnn.Open();
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            
        }

        private IDbCommand CreateInsertCommandExportAttachments(ExportToExcelParamInfo objExportInfo)
        {
            SqlCommand cmd = this.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[usp_INS_ExportToExcelAttachmentDetails]";

            IDataParameterCollection cmdParams = cmd.Parameters;
          
            IDbDataParameter paramRequestID = cmd.CreateParameter();
            paramRequestID.ParameterName = "@RequestID";
            paramRequestID.Value = objExportInfo.RequestID;
            cmdParams.Add(paramRequestID);

            IDbDataParameter paramCompanyID = cmd.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = objExportInfo.CompanyID;
            cmdParams.Add(paramCompanyID);

            IDbDataParameter paramFileName = cmd.CreateParameter();
            paramFileName.ParameterName = "@FileName";
            if (!string.IsNullOrEmpty(objExportInfo.FileName))
                paramFileName.Value = objExportInfo.FileName;
            else
                paramFileName.Value = DBNull.Value;
            cmdParams.Add(paramFileName);

            IDbDataParameter paramFilSize = cmd.CreateParameter();
            paramFilSize.ParameterName = "@FileSize";
            if (objExportInfo.FileSize.HasValue)
                paramFilSize.Value = objExportInfo.FileSize;
            else
                paramFilSize.Value = DBNull.Value;
            cmdParams.Add(paramFilSize);

            IDbDataParameter paramPhysicalPath = cmd.CreateParameter();
            paramPhysicalPath.ParameterName = "@PhysicalPath";
            if (!string.IsNullOrEmpty(objExportInfo.PhysicalPath))
                paramPhysicalPath.Value = objExportInfo.PhysicalPath;
            else
                paramPhysicalPath.Value = DBNull.Value;
            cmdParams.Add(paramPhysicalPath);

            IDbDataParameter paramIsFileDeleted = cmd.CreateParameter();
            paramIsFileDeleted.ParameterName = "@IsFileDeleted";
            if (objExportInfo.IsFileDeleted.HasValue)
                paramIsFileDeleted.Value = objExportInfo.IsFileDeleted;
            else
                paramIsFileDeleted.Value = DBNull.Value;
            cmdParams.Add(paramIsFileDeleted);

            IDbDataParameter paramIsActive = cmd.CreateParameter();
            paramIsActive.ParameterName = "@IsActive";
            if (objExportInfo.IsActive.HasValue)
                paramIsActive.Value = objExportInfo.IsActive;
            else
                paramIsActive.Value = DBNull.Value;
            cmdParams.Add(paramIsActive);

            IDbDataParameter paramRequestStatusID = cmd.CreateParameter();
            paramRequestStatusID.ParameterName = "@RequestStatusID";
            if (objExportInfo.RequestStatusID.HasValue)
                paramRequestStatusID.Value = objExportInfo.RequestStatusID;
            else
                paramRequestStatusID.Value = DBNull.Value;
            cmdParams.Add(paramRequestStatusID);

            IDbDataParameter paramRequestErrorCodeID = cmd.CreateParameter();
            paramRequestErrorCodeID.ParameterName = "@RequestErrorCodeID";
            if (objExportInfo.RequestErrorCodeID.HasValue)
                paramRequestErrorCodeID.Value = objExportInfo.RequestErrorCodeID;
            else
                paramRequestErrorCodeID.Value = DBNull.Value;
            cmdParams.Add(paramRequestErrorCodeID);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = objExportInfo.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = objExportInfo.DateRevised;
            cmdParams.Add(paramDateRevised);

            return cmd;
        }

        protected ExportToExcelInfo MapObject(System.Data.IDataReader r)
        {
            int ordinal = -1;

            ExportToExcelInfo entity = new ExportToExcelInfo();

            ordinal = r.GetOrdinal("GridID");
            entity.GridID = r.GetInt32(ordinal);

            ordinal = r.GetOrdinal("Data");
            entity.Data = r.GetString(ordinal);

            ordinal = r.GetOrdinal("EmailBody");
            entity.EmailBody = r.GetString(ordinal);

            ordinal = r.GetOrdinal("EmailSubject");
            entity.EmailSubject = r.GetString(ordinal);

            ordinal = r.GetOrdinal("FinalMessage");
            entity.FinalMessage = r.GetString(ordinal);

            ordinal = r.GetOrdinal("FromEmailID");
            entity.FromEmailID = r.GetString(ordinal);

            ordinal = r.GetOrdinal("ToEmailID");
            entity.ToEmailID = r.GetString(ordinal);

            ordinal = r.GetOrdinal("RequestID");
            entity.RequestID = r.GetInt32(ordinal);

            ordinal = r.GetOrdinal("RequestTypeID");
            entity.RequestTypeID = r.GetInt16(ordinal);

            ordinal = r.GetOrdinal("UserID");
            entity.UserID = r.GetInt32(ordinal);

            ordinal = r.GetOrdinal("RoleID");
            entity.RoleID = r.GetInt16(ordinal);

            ordinal = r.GetOrdinal("LanguageID");
            entity.LanguageID = r.GetInt32(ordinal);

            ordinal = r.GetOrdinal("RecPeriodID");
            entity.RecPeriodID = r.GetInt32(ordinal);

            ordinal = r.GetOrdinal("CompanyID");
            entity.CompanyID = r.GetInt32(ordinal);

            ordinal = r.GetOrdinal("AddedBy");
            entity.AddedBy = r.GetString(ordinal);

            ordinal = r.GetOrdinal("IsPartOfStorageSpace");
            entity.IsPartOfStorageSpace = r.GetBoolean(ordinal);

            return entity;
        }

    }
}
