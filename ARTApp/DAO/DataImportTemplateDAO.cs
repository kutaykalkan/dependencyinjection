using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.DAO
{
    public class DataImportTemplateDAO : DataImportTemplateDAOBase
    {

        public DataImportTemplateDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public int SaveImportTemplate(ImportTemplateHdrInfo oImportTemplateInfo, DataTable dt, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            using (IDbCommand cmd = CreateSaveImportTemplate(oImportTemplateInfo, dt))
            {
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }

        private IDbCommand CreateSaveImportTemplate(ImportTemplateHdrInfo oImportTemplateInfo, DataTable dt)
        {
            IDbCommand cmd = CreateCommand("usp_INS_ImportTemplate");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTemplateName = cmd.CreateParameter();
            parTemplateName.ParameterName = "@TemplateName";
            parTemplateName.Value = oImportTemplateInfo.TemplateName;
            cmdParams.Add(parTemplateName);

            System.Data.IDbDataParameter parLanguageID = cmd.CreateParameter();
            parLanguageID.ParameterName = "@LanguageID";
            parLanguageID.Value = oImportTemplateInfo.LanguageID;
            cmdParams.Add(parLanguageID);

            System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
            parDataImportTypeID.ParameterName = "@DataImportTypeID";
            parDataImportTypeID.Value = oImportTemplateInfo.DataImportTypeID;
            cmdParams.Add(parDataImportTypeID);

            System.Data.IDbDataParameter parSheetName = cmd.CreateParameter();
            parSheetName.ParameterName = "@SheetName";
            parSheetName.Value = oImportTemplateInfo.SheetName;
            cmdParams.Add(parSheetName);

            System.Data.IDbDataParameter parTemplateFileName = cmd.CreateParameter();
            parTemplateFileName.ParameterName = "@TemplateFileName";
            parTemplateFileName.Value = oImportTemplateInfo.TemplateFileName;
            cmdParams.Add(parTemplateFileName);

            System.Data.IDbDataParameter parPhysicalPath = cmd.CreateParameter();
            parPhysicalPath.ParameterName = "@PhysicalPath";
            parPhysicalPath.Value = oImportTemplateInfo.PhysicalPath;
            cmdParams.Add(parPhysicalPath);

            System.Data.IDbDataParameter parImportTemplateFieldsTable = cmd.CreateParameter();
            parImportTemplateFieldsTable.ParameterName = "@ImportTemplateFieldsTable";
            parImportTemplateFieldsTable.Value = dt;
            cmdParams.Add(parImportTemplateFieldsTable);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = oImportTemplateInfo.AddedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = oImportTemplateInfo.DateAdded;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = oImportTemplateInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parUserID= cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = oImportTemplateInfo.UserID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleId";
            parRoleID.Value = oImportTemplateInfo.RoleId;
            cmdParams.Add(parRoleID);

            return cmd;
        }

        public ImportTemplateHdrInfo GetTemplateFields(int TemplateId)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader dr = null;
            ImportTemplateHdrInfo oImportTemplateInfoList = null;
            try
            {
                oImportTemplateInfoList = new ImportTemplateHdrInfo();
                cmd = CreateGetTemplate(TemplateId);
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oImportTemplateInfoList = this.MapObject(dr);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return oImportTemplateInfoList;
        }

        private IDbCommand CreateGetTemplate(int TemplateId)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllTemplateList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@TemplateId";
            cmdRecPeriod.Value = TemplateId;
            cmdParams.Add(cmdRecPeriod);

            return cmd;
        }


        internal List<ImportTemplateFieldsInfo> GetTemplateFieldsLst(int TemplateId)
        {
            List<ImportTemplateFieldsInfo> oImportTemplateFieldsInfoCollection = new List<ImportTemplateFieldsInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            ImportTemplateFieldsInfo oImportTemplateFieldsInfo = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_SEL_AllTemplateFieldsList");
                cmd.Connection = con;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;
                System.Data.IDbDataParameter par = cmd.CreateParameter();
                par.ParameterName = "@ImportTemplateID";
                par.Value = TemplateId;
                cmdParams.Add(par);
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oImportTemplateFieldsInfo = this.MapTemplateFieldsObject(reader);
                    oImportTemplateFieldsInfoCollection.Add(oImportTemplateFieldsInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oImportTemplateFieldsInfoCollection;
        }


        internal List<ImportFieldMstInfo> GetFieldsMst(int CompanyID,short? DataImportTypeID)
        {
            List<ImportFieldMstInfo> oImportFieldMstInfoCollection = new List<ImportFieldMstInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            ImportFieldMstInfo oImportFieldMstInfo = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_Get_AllImportFieldMst");
                cmd.Connection = con;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;
                System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
                parCompanyID.ParameterName = "@CompanyId";
                parCompanyID.Value = CompanyID;
                cmdParams.Add(parCompanyID);

                System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
                parDataImportTypeID.ParameterName = "@DataImportTypeID";
                parDataImportTypeID.Value = DataImportTypeID;
                cmdParams.Add(parDataImportTypeID);

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oImportFieldMstInfo = this.MapFieldsObject(reader);
                    oImportFieldMstInfoCollection.Add(oImportFieldMstInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oImportFieldMstInfoCollection;
        }

        internal int SaveImportTemplateMapping(DataTable dt, ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            using (IDbCommand cmd = CreateSaveImportTemplateMapping(dt, oImportTemplateFieldMappingInfo))
            {
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }

        private IDbCommand CreateSaveImportTemplateMapping(DataTable dt, ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo)
        {
            IDbCommand cmd = CreateCommand("usp_MERGE_ImportTemplateFieldMapping");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTemplatedt = cmd.CreateParameter();
            parTemplatedt.ParameterName = "@udtImportTemplateFieldMappingTable";
            parTemplatedt.Value = dt;
            cmdParams.Add(parTemplatedt);

            System.Data.IDbDataParameter parModifiedBy = cmd.CreateParameter();
            parModifiedBy.ParameterName = "@ModifiedBy";
            parModifiedBy.Value = oImportTemplateFieldMappingInfo.AddedBy;
            cmdParams.Add(parModifiedBy);

            System.Data.IDbDataParameter parDateModified = cmd.CreateParameter();
            parDateModified.ParameterName = "@DateModified";
            parDateModified.Value = oImportTemplateFieldMappingInfo.DateAdded;
            cmdParams.Add(parDateModified);

            System.Data.IDbDataParameter parImportTemplateID = cmd.CreateParameter();
            parImportTemplateID.ParameterName = "@ImportTemplateID";
            parImportTemplateID.Value = oImportTemplateFieldMappingInfo.ImportTemplateID;
            cmdParams.Add(parImportTemplateID);

            return cmd;
        }

        internal List<ImportTemplateHdrInfo> GetAllTemplateImport(int CompanyID,int UserId,int RoleId)
        {
            List<ImportTemplateHdrInfo> oImportTemplateInfoCollection = new List<ImportTemplateHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            ImportTemplateHdrInfo oImportTemplateInfo = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_Get_AllImportTemplate");
                cmd.Connection = con;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;
                System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
                parCompanyID.ParameterName = "@CompanyID";
                parCompanyID.Value = CompanyID;
                cmdParams.Add(parCompanyID);

                System.Data.IDbDataParameter parUserId = cmd.CreateParameter();
                parUserId.ParameterName = "@UserId";
                parUserId.Value = UserId;
                cmdParams.Add(parUserId);

                System.Data.IDbDataParameter parRoleId = cmd.CreateParameter();
                parRoleId.ParameterName = "@RoleId";
                parRoleId.Value = RoleId;
                cmdParams.Add(parRoleId);

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oImportTemplateInfo = this.MapAllTemplateObject(reader);
                    oImportTemplateInfoCollection.Add(oImportTemplateInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oImportTemplateInfoCollection;
        }

        internal void DeleteMappingData(DataTable dt, ImportTemplateHdrInfo oImportTemplateInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            using (IDbCommand cmd = CreateDeleteMappingData(dt, oImportTemplateInfo))
            {
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                cmd.ExecuteNonQuery();
            }
        }

        private IDbCommand CreateDeleteMappingData(DataTable dt, ImportTemplateHdrInfo oImportTemplateInfo)
        {
            IDbCommand cmd = CreateCommand("usp_DEL_ImportTemplatelist");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTemplatedt = cmd.CreateParameter();
            parTemplatedt.ParameterName = "@udtImportTemplateTableType";
            parTemplatedt.Value = dt;
            cmdParams.Add(parTemplatedt);

            System.Data.IDbDataParameter parModifiedBy = cmd.CreateParameter();
            parModifiedBy.ParameterName = "@ModifiedBy";
            parModifiedBy.Value = oImportTemplateInfo.RevisedBy;
            cmdParams.Add(parModifiedBy);

            System.Data.IDbDataParameter parDateModified = cmd.CreateParameter();
            parDateModified.ParameterName = "@DateModified";
            parDateModified.Value = oImportTemplateInfo.DateRevised;
            cmdParams.Add(parDateModified);

            return cmd;
        }

        internal List<ImportTemplateFieldMappingInfo> GetAllDataImportFieldsWithMapping(int dataImportID)
        {
            List<ImportTemplateFieldMappingInfo> oImportTemplateInfoList = new List<ImportTemplateFieldMappingInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader dr = null;
            ImportTemplateFieldMappingInfo oImportTemplateInfo = null;
            try
            {
                oImportTemplateInfo = new ImportTemplateFieldMappingInfo();
                cmd = CreateGetAllDataImportFieldsWithMappingCommand(dataImportID);
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oImportTemplateInfo = this.MapObjectMapping(dr);
                    oImportTemplateInfoList.Add(oImportTemplateInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return oImportTemplateInfoList;
        }

        private IDbCommand CreateGetAllDataImportFieldsWithMappingCommand(int dataImportID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_DataImportFieldsWithMapping");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdDataImportID = cmd.CreateParameter();
            cmdDataImportID.ParameterName = "@DataImportID";
            cmdDataImportID.Value = dataImportID;
            cmdParams.Add(cmdDataImportID);

            return cmd;
        }

        internal int SaveDataImportSchedule(DataImportScheduleInfo oDataImportScheduleInfo, DataTable dt, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            using (IDbCommand cmd = CreateSaveDataImportSchedule(oDataImportScheduleInfo, dt))
            {
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }

        private IDbCommand CreateSaveDataImportSchedule(DataImportScheduleInfo oDataImportScheduleInfo, DataTable dt)
        {
            IDbCommand cmd = CreateCommand("usp_INS_DataImportSchedule");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = oDataImportScheduleInfo.UserID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = oDataImportScheduleInfo.RoleID;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            parIsActive.Value = oDataImportScheduleInfo.IsActive;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parDataImportScheduleTable = cmd.CreateParameter();
            parDataImportScheduleTable.ParameterName = "@DataImportScheduleTable";
            parDataImportScheduleTable.Value = dt;
            cmdParams.Add(parDataImportScheduleTable);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = oDataImportScheduleInfo.AddedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = oDataImportScheduleInfo.DateAdded;
            cmdParams.Add(parDateAdded);

            return cmd;
        }

        internal List<DataImportScheduleInfo> GetDataImportSchedule(int? UserID, short? RoleID)
        {
            List<DataImportScheduleInfo> oDataImportScheduleInfoCollection = new List<DataImportScheduleInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader dr = null;
            DataImportScheduleInfo oDataImportScheduleInfo = null;
            try
            {
                oDataImportScheduleInfo = new DataImportScheduleInfo();
                cmd = CreateDataImportSchedule(UserID, RoleID);
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oDataImportScheduleInfo = this.MapObjectDataImportSchedule(dr);
                    oDataImportScheduleInfoCollection.Add(oDataImportScheduleInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return oDataImportScheduleInfoCollection;
        }
        private IDbCommand CreateDataImportSchedule(int? UserID, short? RoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllDataImportScheduleList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = RoleID;
            cmdParams.Add(parRoleID);
            return cmd;
        }

        internal List<DataImportMessageInfo> GetAllWarningMsg(short DataImportTypeId)
        {
            List<DataImportMessageInfo> oDataImportMessageInfoCollection = new List<DataImportMessageInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader dr = null;
            DataImportMessageInfo oDataImportMessageInfo = null;
            try
            {
                oDataImportMessageInfo = new DataImportMessageInfo();
                cmd = CreateAllWarningMsg(DataImportTypeId);
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oDataImportMessageInfo = this.MapObjectDataImportMessage(dr);
                    oDataImportMessageInfoCollection.Add(oDataImportMessageInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return oDataImportMessageInfoCollection;
        }

        private IDbCommand CreateAllWarningMsg(short DataImportTypeId)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllDataImportMessageList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDataImportTypeId = cmd.CreateParameter();
            parDataImportTypeId.ParameterName = "@DataImportTypeID";
            parDataImportTypeId.Value = DataImportTypeId;
            cmdParams.Add(parDataImportTypeId);

            return cmd;
        }

        internal int SaveDataImportWarningPreferences(DataTable dt, DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            using (IDbCommand cmd = CreateSaveDataImportWarningPreferences(dt, oDataImportWarningPreferencesInfo))
            {
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }

        private IDbCommand CreateSaveDataImportWarningPreferences(DataTable dt, DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo)
        {
            IDbCommand cmd = CreateCommand("usp_INS_DataImportWarningPreferences");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = oDataImportWarningPreferencesInfo.UserID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = oDataImportWarningPreferencesInfo.RoleID;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parDataImportScheduleTable = cmd.CreateParameter();
            parDataImportScheduleTable.ParameterName = "@WarningPreferenceseTable";
            parDataImportScheduleTable.Value = dt;
            cmdParams.Add(parDataImportScheduleTable);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = oDataImportWarningPreferencesInfo.AddedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = oDataImportWarningPreferencesInfo.DateAdded;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
            parDataImportTypeID.ParameterName = "@DataImportTypeID";
            parDataImportTypeID.Value = oDataImportWarningPreferencesInfo.DataImportTypeID;
            cmdParams.Add(parDataImportTypeID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = oDataImportWarningPreferencesInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            return cmd;
        }

        internal List<DataImportWarningPreferencesInfo> GetDataImportWarningPreferences(int? CurrentCompanyID, short DataImportTypeId)
        {
            List<DataImportWarningPreferencesInfo> oDataImportWarningPreferencesInfoCollection = new List<DataImportWarningPreferencesInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader dr = null;
            DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo = null;
            try
            {
                oDataImportWarningPreferencesInfo = new DataImportWarningPreferencesInfo();
                cmd = CreateDataImportWarningPreferences(CurrentCompanyID, DataImportTypeId);
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oDataImportWarningPreferencesInfo = this.MapObjectoDataImportWarningPreferences(dr);
                    oDataImportWarningPreferencesInfoCollection.Add(oDataImportWarningPreferencesInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return oDataImportWarningPreferencesInfoCollection;
        }

        private IDbCommand CreateDataImportWarningPreferences(int? CurrentCompanyID,short DataImportTypeId)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllDataImportWarningPreferencesList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCurrentCompanyID = cmd.CreateParameter();
            parCurrentCompanyID.ParameterName = "@CurrentCompanyID";
            parCurrentCompanyID.Value = CurrentCompanyID;
            cmdParams.Add(parCurrentCompanyID);

            System.Data.IDbDataParameter parDataImportTypeId = cmd.CreateParameter();
            parDataImportTypeId.ParameterName = "@DataImportTypeId";
            parDataImportTypeId.Value = DataImportTypeId;
            cmdParams.Add(parDataImportTypeId);

            return cmd;
        }

        internal List<DataImportMessageInfo> GetDataImportMessageList()
        {
            List<DataImportMessageInfo> oDataImportMessageInfoCollection = new List<DataImportMessageInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader dr = null;
            DataImportMessageInfo oDataImportMessageInfo = null;
            try
            {
                oDataImportMessageInfo = new DataImportMessageInfo();
                cmd = CreateGetDataImportMessageList();
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oDataImportMessageInfo = this.MapObjectDataImportMessageList(dr);
                    oDataImportMessageInfoCollection.Add(oDataImportMessageInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return oDataImportMessageInfoCollection;
        }
        private IDbCommand CreateGetDataImportMessageList()
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_AllDataImportMessageList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            return cmd;
        }

        internal List<DataImportWarningPreferencesAuditInfo> GetAllWarningAuditList(int CurrentCompanyID,int CurrentUserID, short CurrentRoleID)
        {
            List<DataImportWarningPreferencesAuditInfo> oDataImportWarningPreferencesAuditInfoCollection = new List<DataImportWarningPreferencesAuditInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader dr = null;
            DataImportWarningPreferencesAuditInfo oDataImportWarningPreferencesAuditInfo = null;
            try
            {
                oDataImportWarningPreferencesAuditInfo = new DataImportWarningPreferencesAuditInfo();
                cmd = CreateGetAllWarningAuditList(CurrentCompanyID,CurrentUserID, CurrentRoleID);
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oDataImportWarningPreferencesAuditInfo = this.MapObjectDataImportWarningPreferencesAuditList(dr);
                    oDataImportWarningPreferencesAuditInfoCollection.Add(oDataImportWarningPreferencesAuditInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return oDataImportWarningPreferencesAuditInfoCollection;
        }
        private IDbCommand CreateGetAllWarningAuditList(int CurrentCompanyID, int CurrentUserID, short CurrentRoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_DataImportWarningPreferencesAuditList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = CurrentUserID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = CurrentRoleID;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CurrentCompanyID;
            cmdParams.Add(parCompanyID);

            return cmd;
        }
    }
}