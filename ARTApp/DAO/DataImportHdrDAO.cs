


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params.RecItemUpload;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.DAO
{
    public class DataImportHdrDAO : DataImportHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataImportHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public long GetAvailableFileStorageSpaceByCompanyID(int companyID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateAvailableSpaceByCompanyIDCommand(companyID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();
                return Convert.ToInt64(oIDBCommand.ExecuteScalar());
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
        }

        public decimal? GetMaxFileSizeByCompanyID(int companyID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateMaxFileSizeByCompanyIDCommand(companyID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();
                Object obj = oIDBCommand.ExecuteScalar();
                if (obj == DBNull.Value)
                    return null;
                else
                    return Convert.ToDecimal(obj);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
        }

        public int GetMaxFileSizeByCompanyIDInt(int companyID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateMaxFileSizeByCompanyIDCommand(companyID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();
                Object obj = oIDBCommand.ExecuteScalar();
                if (obj == DBNull.Value)
                    return 0;
                else
                    return Convert.ToInt32(obj);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
        }

        public short? isKeyMappingDoneByCompanyID(int companyID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            short? retVal = null;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateIsKeyMappingDoneByCompanyIDCommand(companyID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();
                Object obj = oIDBCommand.ExecuteScalar();
                string result = obj.ToString();
                short keyCount;
                if (short.TryParse(result, out keyCount))
                    retVal = keyCount;
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return retVal;
        }

        public bool IsFirstTimeGLDataImportByCompanyID(int companyID, short DataImportTypeID, short failureStatusID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            bool retVal = false;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateIsFirstTimeGLDataImportByCompanyID(companyID, DataImportTypeID, failureStatusID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();
                Object obj = oIDBCommand.ExecuteScalar();
                string result = obj.ToString();
                if (bool.TryParse(result, out retVal))
                    return retVal;
            }

            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return retVal;
        }
        #region " Create Commands"
        protected IDbCommand CreateAvailableSpaceByCompanyIDCommand(int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_AvailableFileStorageByCompanyID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = oIDBCommand.Parameters;
            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = companyID;
            cmdParams.Add(cmdParamCompanyID);
            return oIDBCommand;
        }
        protected IDbCommand CreateMaxFileSizeByCompanyIDCommand(int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_MaxFileSizeByCompanyID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = oIDBCommand.Parameters;
            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = companyID;
            cmdParams.Add(cmdParamCompanyID);
            return oIDBCommand;
        }
        protected IDbCommand CreateIsFirstTimeGLDataImportByCompanyID(int companyID, short DataImportTypeID, short failureStatusID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_IsFirstTimeGLDataImportByCompanyID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = companyID;

            IDbDataParameter cmdParamDataImportTypeID = oIDBCommand.CreateParameter();
            cmdParamDataImportTypeID.ParameterName = "@DataImportTypeID";
            cmdParamDataImportTypeID.Value = DataImportTypeID;

            IDbDataParameter cmdParamFailureStatusID = oIDBCommand.CreateParameter();
            cmdParamFailureStatusID.ParameterName = "@FailureStatusID";
            cmdParamFailureStatusID.Value = failureStatusID;

            //IDbDataParameter paramReturnValue = oIDBCommand.CreateParameter();
            //paramReturnValue.ParameterName = "@ReturnValue";
            ////paramReturnValue.Value = test;
            //paramReturnValue.DbType = System.Data.DbType.String ;
            //paramReturnValue.Direction = ParameterDirection.ReturnValue;

            cmdParams.Add(cmdParamCompanyID);
            cmdParams.Add(cmdParamDataImportTypeID);
            cmdParams.Add(cmdParamFailureStatusID);
            //cmdParams.Add(paramReturnValue);

            return oIDBCommand;

        }
        protected IDbCommand CreateIsKeyMappingDoneByCompanyIDCommand(int companyID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_GET_IsKeyMappingDoneByCompanyID");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter cmdParamCompanyID = oCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = companyID;
            cmdParams.Add(cmdParamCompanyID);
            return oCommand;
        }
        #endregion

        #region GetDataImportStatusByCompanyID
        internal List<DataImportHdrInfo> GetDataImportStatusByCompanyID(int? CompanyID, int? UserID, short? RoleID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<DataImportHdrInfo> oDataImportHdrInfoCollection = null;

            try
            {
                cmd = CreateGetDataImportStatusByCompanyIDCommand(CompanyID, UserID, RoleID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oDataImportHdrInfoCollection = MapObjects(dr);
                dr.ClearColumnHash();
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
            return oDataImportHdrInfoCollection;
        }

        private IDbCommand CreateGetDataImportStatusByCompanyIDCommand(int? CompanyID, int? UserID, short? RoleID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_DataImportHdrByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = RoleID.Value;
            cmdParams.Add(parRoleID);


            return cmd;
        }
        #endregion


        #region "GetDataImportInfo"
        internal DataImportHdrInfo GetDataImportInfo(int? DataImportID)
        {
            DataImportHdrInfo oDataImportHdrInfo = null;
            using (IDbConnection con = this.CreateConnection())
            {
                con.Open();
                using (IDbCommand cmd = CreateGetDataImportInfoCommand(DataImportID))
                {
                    cmd.Connection = con;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        if (oDataImportHdrInfo == null)
                        {
                            oDataImportHdrInfo = MapObject(dr);
                            oDataImportHdrInfo.EmailID = dr.GetStringValue("EmailID");
                        }
                    }
                    dr.ClearColumnHash();
                }
            }
            return oDataImportHdrInfo;
        }

        private IDbCommand CreateGetDataImportInfoCommand(int? DataImportID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_DataImportInfo");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            parDataImportID.Value = DataImportID.Value;
            cmdParams.Add(parDataImportID);

            return cmd;
        }

        internal List<DataImportMessageDetailInfo> GetDataImportMessageDetailInfoList(int? DataImportID)
        {
            List<DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = new List<DataImportMessageDetailInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                con.Open();
                using (IDbCommand cmd = CreateGetDataImportMessageDetailInfoListCommand(DataImportID))
                {
                    cmd.Connection = con;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        oDataImportMessageDetailInfoList.Add(MapObjectDataImportMessage(dr));
                    }
                    dr.ClearColumnHash();
                }
            }
            return oDataImportMessageDetailInfoList;
        }


        private IDbCommand CreateGetDataImportMessageDetailInfoListCommand(int? DataImportID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_DataImportMessageDetailInfo");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            parDataImportID.Value = DataImportID.Value;
            cmdParams.Add(parDataImportID);

            return cmd;
        }

        private DataImportMessageDetailInfo MapObjectDataImportMessage(IDataReader dr)
        {
            DataImportMessageDetailInfo entity = new DataImportMessageDetailInfo();
            entity.DataImportMessageDetailID = dr.GetInt64Value("DataImportMessageDetailID");
            entity.DataImportID = dr.GetInt32Value("DataImportID");
            entity.DataImportMessageID = dr.GetInt16Value("DataImportMessageID");
            entity.DataImportMessage = dr.GetStringValue("DataImportMessage");
            entity.DataImportMessageLabelID = dr.GetInt32Value("DataImportMessageLabelID");
            entity.DataImportMessageTypeID = dr.GetInt16Value("DataImportMessageTypeID");
            entity.ExcelRowNumber = dr.GetInt32Value("ExcelRowNumber");
            entity.MessageSchema = dr.GetStringValue("MessageSchema");
            entity.MessageData = dr.GetStringValue("MessageData");
            entity.IsActive = dr.GetBooleanValue("IsActive");
            entity.DateAdded = dr.GetDateValue("DateAdded");
            entity.DescriptionLabelID = dr.GetInt32Value("DescriptionLabelID");
            entity.DataImportMessageCategoryID = dr.GetInt16Value("DataImportMessageCategoryID");
            Int64? accountID = dr.GetInt64Value("AccountID");
            if (accountID.HasValue)
            {
                entity.AccountInfo = new AccountHdrInfo();
                entity.AccountInfo.AccountID = accountID;
                entity.AccountInfo.FSCaptionLabelID = dr.GetInt32Value("FSCaptionLabelID");
                entity.AccountInfo.AccountTypeLabelID = dr.GetInt32Value("AccountTypeLabelID");
                entity.AccountInfo.Key2LabelID = dr.GetInt32Value("Key2LabelID");
                entity.AccountInfo.Key3LabelID = dr.GetInt32Value("Key3LabelID");
                entity.AccountInfo.Key4LabelID = dr.GetInt32Value("Key4LabelID");
                entity.AccountInfo.Key5LabelID = dr.GetInt32Value("Key5LabelID");
                entity.AccountInfo.Key6LabelID = dr.GetInt32Value("Key6LabelID");
                entity.AccountInfo.Key7LabelID = dr.GetInt32Value("Key7LabelID");
                entity.AccountInfo.Key8LabelID = dr.GetInt32Value("Key8LabelID");
                entity.AccountInfo.Key9LabelID = dr.GetInt32Value("Key9LabelID");
                entity.AccountInfo.AccountNumber = dr.GetStringValue("AccountNumber");
                entity.AccountInfo.AccountNameLabelID = dr.GetInt32Value("AccountNameLabelID");
            }
            return entity;
        }

        #endregion

        #region "UpdateDataImportForForceCommit"
        internal void UpdateDataImportForForceCommit(DataImportHdrInfo oDataImportHdrInfo, IDbConnection cnn, IDbTransaction tran)
        {
            using (IDbCommand cmd = CreateUpdateDataImportForForceCommitCommand(oDataImportHdrInfo))
            {
                this.ExecuteNonQuery(cmd, cnn, tran);
            }
        }

        private IDbCommand CreateUpdateDataImportForForceCommitCommand(DataImportHdrInfo oDataImportHdrInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_DataImportForForceCommit");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            parDataImportID.Value = oDataImportHdrInfo.DataImportID.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parDataImportStatusID = cmd.CreateParameter();
            parDataImportStatusID.ParameterName = "@DataImportStatusID";
            parDataImportStatusID.Value = oDataImportHdrInfo.DataImportStatusID.Value;
            cmdParams.Add(parDataImportStatusID);

            System.Data.IDbDataParameter parForceCommitDate = cmd.CreateParameter();
            parForceCommitDate.ParameterName = "@ForceCommitDate";
            parForceCommitDate.Value = oDataImportHdrInfo.ForceCommitDate.Value;
            cmdParams.Add(parForceCommitDate);

            System.Data.IDbDataParameter parIsForceCommit = cmd.CreateParameter();
            parIsForceCommit.ParameterName = "@IsForceCommit";
            parIsForceCommit.Value = oDataImportHdrInfo.IsForceCommit;
            cmdParams.Add(parIsForceCommit);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = oDataImportHdrInfo.DateRevised.Value;
            cmdParams.Add(parDateRevised);


            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = oDataImportHdrInfo.RevisedBy;
            cmdParams.Add(parRevisedBy);

            return cmd;
        }
        #endregion

        #region "Delete DataImport By List Of DataImportIDs"
        public int? DeleteDataImportByDataImportID(DataTable dtDataImportIDs, int companyID
            , int recPeriodID
            , string revisedBy
            , DateTime dateRevised
            , short NotStartedRecPeriodStatusID)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.GetDataImportDeleteCommand(dtDataImportIDs, companyID, recPeriodID, revisedBy
                                            , dateRevised, NotStartedRecPeriodStatusID);
            oDBCommand.Connection = this.CreateConnection();
            oDBCommand.Connection.Open();
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }
        public IDbCommand GetDataImportDeleteCommand(DataTable dtDataImportIDs, int companyID
            , int recPeriodID
            , string revisedBy
            , DateTime dateRevised
            , short NotStartedRecPeriodStatusID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_DataImport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDataImportIDTable = cmd.CreateParameter();
            parDataImportIDTable.ParameterName = "@DataImportIDTable";
            parDataImportIDTable.Value = dtDataImportIDs;
            cmdParams.Add(parDataImportIDTable);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parNotStartedRecPeriodStatusID = cmd.CreateParameter();
            parNotStartedRecPeriodStatusID.ParameterName = "@NotStartedRecPeriodStatusID";
            parNotStartedRecPeriodStatusID.Value = NotStartedRecPeriodStatusID;
            cmdParams.Add(parNotStartedRecPeriodStatusID);

            return cmd;
        }
        #endregion


        #region Update DataImport HiddenStatus

        internal void UpdateDataImportHiddenStatusByDataImportID(int dataImportID, bool isHidden)
        {
            IDbCommand cmd = null;
            try
            {
                cmd = CreateUpdateDataImportHiddenStatusByDataImportIDCommand(dataImportID, isHidden);
                this.ExecuteNonQuery(cmd);
            }
            finally
            {
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


        }


        private IDbCommand CreateUpdateDataImportHiddenStatusByDataImportIDCommand(int dataImportID, bool isHidden)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_DataImportHdrForHiddenStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            parDataImportID.Value = dataImportID;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parHiddenStatus = cmd.CreateParameter();
            parHiddenStatus.ParameterName = "@IsHidden";
            parHiddenStatus.Value = isHidden;
            cmdParams.Add(parHiddenStatus);

            return cmd;
        }



        #endregion




        public bool IsGLDataUploaded(int recPeriodID, byte DataImportID, byte DataImportStatusID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            byte objVal;
            bool retVal = false;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateIsGLDataUploadedCommand(recPeriodID, DataImportID, DataImportStatusID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();
                Object obj = oIDBCommand.ExecuteScalar();
                if (byte.TryParse(obj.ToString(), out objVal))
                    retVal = (objVal > 0);
            }

            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return retVal;
        }
        private IDbCommand CreateIsGLDataUploadedCommand(int recPeriodID, byte DataImportID, byte DataImportStatusID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_CHK_IsGLDataUploadedByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDataImportIDTable = cmd.CreateParameter();

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
            parDataImportTypeID.ParameterName = "@DataImportTypeID";
            parDataImportTypeID.Value = DataImportID;
            cmdParams.Add(parDataImportTypeID);

            System.Data.IDbDataParameter parDataImportStatusID = cmd.CreateParameter();
            parDataImportStatusID.ParameterName = "@DataImportStatusID";
            parDataImportStatusID.Value = DataImportStatusID;
            cmdParams.Add(parDataImportStatusID);

            return cmd;
        }

        internal List<DataImportHdrInfo> GetDataImportStatusByUserID(int? RecPeriodID, bool showHiddenRows, int? UserID, short? RoleID)
        {
            System.Data.IDbCommand cmd = null;

            try
            {
                cmd = CreateGetDataImportStatusByUserIDCommand(RecPeriodID, showHiddenRows, UserID, RoleID);
                return this.Select(cmd);
            }
            finally
            {
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
        }
        private IDbCommand CreateGetDataImportStatusByUserIDCommand(int? RecPeriodID, bool showHiddenRows, int? UserID, short? RoleID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_DataImportHdrByRecPeriodIDAndUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodID);


            System.Data.IDbDataParameter parShowHiddenRows = cmd.CreateParameter();
            parShowHiddenRows.ParameterName = "@ShowHiddenRows";
            parShowHiddenRows.Value = showHiddenRows;
            cmdParams.Add(parShowHiddenRows);

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


        public bool IsAnyAccountAssigned(short? roleID, int? userID, int? recPeriodID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            byte objVal;
            bool retVal = false;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateIsAnyAccountAssignedCommand(roleID, userID, recPeriodID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();

                Object obj = oIDBCommand.ExecuteScalar();
                if (byte.TryParse(obj.ToString(), out objVal))
                    retVal = (objVal > 0);

            }

            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return retVal;
        }

        private IDbCommand CreateIsAnyAccountAssignedCommand(short? roleID, int? userID, int? recPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_CHK_AnyAccountAssignedByUserIDRoleIDRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = roleID;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);


            return cmd;
        }

        internal List<DataImportHdrInfo> GetRecItemDataImportStatus(int? RecPeriodID, int? UserID, short? RoleID, long? GLDataID, short? RecCategoryID, short? RecCategoryTypeID)
        {
            System.Data.IDbCommand cmd = null;

            try
            {
                cmd = CreateGetRecItemDataImportStatusCommand(RecPeriodID, UserID, RoleID, GLDataID, RecCategoryID, RecCategoryTypeID);
                return this.Select(cmd);
            }
            finally
            {
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
        }
        private IDbCommand CreateGetRecItemDataImportStatusCommand(int? RecPeriodID, int? UserID, short? RoleID, long? GLDataID, short? RecCategoryID, short? RecCategoryTypeID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RecItemDataImportStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonUserRoleAndRecPeriodParameters(UserID, RoleID, RecPeriodID, cmd, cmdParams);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            parGLDataID.Value = GLDataID.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parRecCategoryID = cmd.CreateParameter();
            parRecCategoryID.ParameterName = "@RecCategoryID";
            parRecCategoryID.Value = RecCategoryID.Value;
            cmdParams.Add(parRecCategoryID);

            System.Data.IDbDataParameter parRecCategoryTypeID = cmd.CreateParameter();
            parRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            parRecCategoryTypeID.Value = RecCategoryTypeID.Value;
            cmdParams.Add(parRecCategoryTypeID);

            return cmd;
        }

        internal List<DataImportHdrInfo> GetGeneralTaskImportStatus(int? RecPeriodID, int? UserID, short? RoleID, short? DataImportTypeID)
        {
            System.Data.IDbCommand cmd = null;

            try
            {
                cmd = CreateGetGeneralTaskImportStatusCommand(RecPeriodID, UserID, RoleID, DataImportTypeID);
                return this.Select(cmd);
            }
            finally
            {
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
        }
        private IDbCommand CreateGetGeneralTaskImportStatusCommand(int? RecPeriodID, int? UserID, short? RoleID, short? DataImportTypeID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[usp_SEL_DataImportStatus]");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonUserRoleAndRecPeriodParameters(UserID, RoleID, RecPeriodID, cmd, cmdParams);

            System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
            parDataImportTypeID.ParameterName = "@DataImportTypeID";
            parDataImportTypeID.Value = DataImportTypeID.Value;
            cmdParams.Add(parDataImportTypeID);

            return cmd;
        }
        public string GetImportTemplateSheetName(int ImportTemplateID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            string retVal = string.Empty;
            try
            {
                oConnection = this.CreateConnection();
                oIDBCommand = this.CreateGetImportTemplateSheetNameCommand(ImportTemplateID);
                oIDBCommand.Connection = oConnection;
                oConnection.Open();
                Object obj = oIDBCommand.ExecuteScalar();
                if (obj != null)
                    retVal = obj.ToString();
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return retVal;
        }
        protected IDbCommand CreateGetImportTemplateSheetNameCommand(int ImportTemplateID)
        {
            IDbCommand oCommand = this.CreateCommand("[dbo].[usp_GET_GetImportTemplateSheetName]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter cmdParamImportTemplateID = oCommand.CreateParameter();
            cmdParamImportTemplateID.ParameterName = "@ImportTemplateID";
            cmdParamImportTemplateID.Value = ImportTemplateID;
            cmdParams.Add(cmdParamImportTemplateID);
            return oCommand;
        }
        public List<ImportTemplateFieldMappingInfo> GetImportTemplateFieldMappingInfoList(int? ImportTemplateID)
        {
            List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = new List<ImportTemplateFieldMappingInfo>();
            ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo;
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetImportTemplateFieldMappingInfoListCommand(ImportTemplateID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oImportTemplateFieldMappingInfo = MapImportTemplateFieldMappingInfoObject(reader);
                    oImportTemplateFieldMappingInfoList.Add(oImportTemplateFieldMappingInfo);
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oImportTemplateFieldMappingInfoList;
        }

        private ImportTemplateFieldMappingInfo MapImportTemplateFieldMappingInfoObject(IDataReader r)
        {
            ImportTemplateFieldMappingInfo entity = new ImportTemplateFieldMappingInfo();
            entity.ImportTemplateFieldID = r.GetInt32Value("ImportTemplateFieldID");
            entity.ImportTemplateField = r.GetStringValue("ImportTemplateField");
            entity.ImportFieldID = r.GetInt16Value("ImportFieldID");
            entity.ImportField = r.GetStringValue("ImportField");
            entity.ImportFieldLabelID = r.GetInt32Value("ImportFieldLabelID");
            entity.MessageLabelID = r.GetInt32Value("MessageLabelID");
            return entity;
        }
        private IDbCommand GetImportTemplateFieldMappingInfoListCommand(int? ImportTemplateID)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("[dbo].[usp_SEL_ImportTemplateFieldMapping]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter parImportTemplateID = oCommand.CreateParameter();
            parImportTemplateID.ParameterName = "@ImportTemplateID";
            if (ImportTemplateID.HasValue)
                parImportTemplateID.Value = ImportTemplateID.Value;
            else
                parImportTemplateID.Value = DBNull.Value;
            cmdParams.Add(parImportTemplateID);

            return oCommand;
        }

    }
}