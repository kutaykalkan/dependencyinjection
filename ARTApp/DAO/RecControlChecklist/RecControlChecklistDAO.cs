using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.RecControlCheckList;

namespace SkyStem.ART.App.DAO
{
    public class RecControlChecklistDAO : RecControlChecklistDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RecControlChecklistDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<RecControlCheckListInfo> SaveRecControlChecklist(List<RecControlCheckListInfo> oRecControlCheckListInfoList,
           int? recPeriodID, int? dataImportID, string modifiedBy, DateTime? dateModified,
           IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.SaveRecControlChecklistCommand(oRecControlCheckListInfoList, recPeriodID, dataImportID, modifiedBy, dateModified);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    int? id = dr.GetInt32Value("ID");
                    int? recControlChecklistID = dr.GetInt32Value("RecControlChecklistID");
                    RecControlCheckListInfo oRecControlCheckListInfo = oRecControlCheckListInfoList.Find(T => T.RowNumber == id.Value);
                    if (oRecControlCheckListInfo != null)
                        oRecControlCheckListInfo.RecControlCheckListID = recControlChecklistID;
                }
            }
            return oRecControlCheckListInfoList;
        }
        protected IDbCommand SaveRecControlChecklistCommand(List<RecControlCheckListInfo> oRecControlCheckListInfoList,
            int? recPeriodID, int? dataImportID, string modifiedBy, DateTime? dateModified)
        {
            IDbCommand cmd = this.CreateCommand("usp_MERGE_RecControlChecklist");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamRecControlChecklistTable = cmd.CreateParameter();
            cmdParamRecControlChecklistTable.ParameterName = "@udtRecControlChecklistTableType";
            cmdParamRecControlChecklistTable.Value = ServiceHelper.ConvertRecControlChecklistToDataTable(oRecControlCheckListInfoList);
            cmdParams.Add(cmdParamRecControlChecklistTable);

            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);

            IDbDataParameter cmdParamDataImportID = cmd.CreateParameter();
            cmdParamDataImportID.ParameterName = "@DataImportID";
            if (dataImportID.HasValue)
                cmdParamDataImportID.Value = dataImportID.Value;
            else
                cmdParamDataImportID.Value = DBNull.Value;
            cmdParams.Add(cmdParamDataImportID);

            IDbDataParameter cmdParamDateModified = cmd.CreateParameter();
            cmdParamDateModified.ParameterName = "@DateModified";
            cmdParamDateModified.Value = dateModified.Value;
            cmdParams.Add(cmdParamDateModified);

            IDbDataParameter cmdParamModifiedBy = cmd.CreateParameter();
            cmdParamModifiedBy.ParameterName = "@ModifiedBy";
            cmdParamModifiedBy.Value = modifiedBy;
            cmdParams.Add(cmdParamModifiedBy);
            return cmd;
        }
        internal List<RecControlCheckListInfo> GetRecControlCheckListInfoList(long? GlDataID, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            List<RecControlCheckListInfo> oRecControlCheckListInfoCollection = new List<RecControlCheckListInfo>();
            try
            {

                cmd = CreateGetRecControlCheckListInfoListCommand(GlDataID, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        RecControlCheckListInfo oRecControlCheckListInfo = MapObject(dr);
                        if (oRecControlCheckListInfo != null)
                        {
                            GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo = MapObjectGLDataRecControlCheckListInfo(dr);
                            oRecControlCheckListInfo.oGLDataRecControlCheckListInfo = oGLDataRecControlCheckListInfo;
                            oRecControlCheckListInfoCollection.Add(oRecControlCheckListInfo);
                        }
                    }
                }
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
            return oRecControlCheckListInfoCollection;
        }
        private IDbCommand CreateGetRecControlCheckListInfoListCommand(long? GLDataId, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RecControlCheckList");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parTblGlDataID = cmd.CreateParameter();
            parTblGlDataID.ParameterName = "@GLDataID";
            if (GLDataId.HasValue)
                parTblGlDataID.Value = GLDataId.Value;
            else
                parTblGlDataID.Value = DBNull.Value;
            cmdParams.Add(parTblGlDataID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;

            cmdParams.Add(parRecPeriodID);
            return cmd;
        }
        protected GLDataRecControlCheckListInfo MapObjectGLDataRecControlCheckListInfo(System.Data.IDataReader r)
        {

            GLDataRecControlCheckListInfo entity = new GLDataRecControlCheckListInfo();
            entity.GLDataRecControlCheckListID = r.GetInt64Value("GLDataRecControlCheckListID");
            entity.RecControlCheckListID = r.GetInt32Value("RecControlCheckListID");
            entity.CompletedRecStatus = r.GetInt16Value("CompletedRecStatus");
            entity.CompletedBy = r.GetInt32Value("CompletedBy");
            entity.ReviewedRecStatus = r.GetInt16Value("ReviewedRecStatus");
            entity.ReviewedBy = r.GetInt32Value("ReviewedBy");
            entity.IsCommentAvailable = r.GetBooleanValue("IsCommentAvailable");
            return entity;
        }
        public void DeleteRecControlChecklist(List<RecControlCheckListInfo> oRecControlCheckListInfoList, string modifiedBy, DateTime? dateModified, int? RecPeriodID,
         IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.DeleteRecControlChecklistCommand(oRecControlCheckListInfoList, modifiedBy, dateModified ,RecPeriodID);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }
        protected IDbCommand DeleteRecControlChecklistCommand(List<RecControlCheckListInfo> oRecControlCheckListInfoList,
             string modifiedBy, DateTime? dateModified, int? RecPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_RecControlChecklist");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamRecControlChecklistTable = cmd.CreateParameter();
            cmdParamRecControlChecklistTable.ParameterName = "@udtRecControlChecklistTableType";
            cmdParamRecControlChecklistTable.Value = ServiceHelper.ConvertRecControlChecklistToDataTable(oRecControlCheckListInfoList);
            cmdParams.Add(cmdParamRecControlChecklistTable);

            IDbDataParameter cmdParamDateModified = cmd.CreateParameter();
            cmdParamDateModified.ParameterName = "@DateModified";
            cmdParamDateModified.Value = dateModified.Value;
            cmdParams.Add(cmdParamDateModified);

            IDbDataParameter cmdParamModifiedBy = cmd.CreateParameter();
            cmdParamModifiedBy.ParameterName = "@ModifiedBy";
            cmdParamModifiedBy.Value = modifiedBy;
            cmdParams.Add(cmdParamModifiedBy);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }
        internal List<GLDataRecControlCheckListCommentInfo> GetGLDataRecControlCheckListCommentInfoList(long? GLDataRecControlCheckListID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            List<GLDataRecControlCheckListCommentInfo> oGLDataRecControlCheckListCommentInfoCollection = new List<GLDataRecControlCheckListCommentInfo>();
            try
            {

                cmd = CreateGetGLDataRecControlCheckListCommentInfoListCommand(GLDataRecControlCheckListID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo = MapObjectGLDataRecControlCheckListCommentInfo(dr);
                        if (oGLDataRecControlCheckListCommentInfo != null)
                        {
                            oGLDataRecControlCheckListCommentInfoCollection.Add(oGLDataRecControlCheckListCommentInfo);
                        }
                    }
                }
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
            return oGLDataRecControlCheckListCommentInfoCollection;
        }
        private IDbCommand CreateGetGLDataRecControlCheckListCommentInfoListCommand(long? GLDataRecControlCheckListID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataRecControlCheckListComment");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parGLDataRecControlCheckListID = cmd.CreateParameter();
            parGLDataRecControlCheckListID.ParameterName = "@GLDataRecControlCheckListID";
            if (GLDataRecControlCheckListID.HasValue)
                parGLDataRecControlCheckListID.Value = GLDataRecControlCheckListID.Value;
            else
                parGLDataRecControlCheckListID.Value = DBNull.Value;

            cmdParams.Add(parGLDataRecControlCheckListID);
            return cmd;
        }
        protected GLDataRecControlCheckListCommentInfo MapObjectGLDataRecControlCheckListCommentInfo(System.Data.IDataReader r)
        {

            GLDataRecControlCheckListCommentInfo entity = new GLDataRecControlCheckListCommentInfo();
            entity.GLDataRecControlCheckListID = r.GetInt64Value("GLDataRecControlCheckListID");
            entity.GLDataRecControlCheckListCommentID = r.GetInt64Value("GLDataRecControlCheckListCommentID");
            entity.Comments = r.GetStringValue("Comments");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
            entity.AddedByUserName = r.GetStringValue("AddedByUserName");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            return entity;
        }
        internal void SaveGLDataRecControlCheckListComment(GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.SaveGLDataRecControlCheckListCommentCommand(oGLDataRecControlCheckListCommentInfo);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }
        protected IDbCommand SaveGLDataRecControlCheckListCommentCommand(GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_INS_GLDataRecControlChecklistComment");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamGLDataRecControlCheckListID = cmd.CreateParameter();
            cmdParamGLDataRecControlCheckListID.ParameterName = "@GLDataRecControlCheckListID";
            if (oGLDataRecControlCheckListCommentInfo.GLDataRecControlCheckListID.HasValue)
                cmdParamGLDataRecControlCheckListID.Value = oGLDataRecControlCheckListCommentInfo.GLDataRecControlCheckListID.Value;
            else
                cmdParamGLDataRecControlCheckListID.Value = DBNull.Value;
            cmdParams.Add(cmdParamGLDataRecControlCheckListID);

            IDbDataParameter cmdParamComments = cmd.CreateParameter();
            cmdParamComments.ParameterName = "@Comments";
            if (oGLDataRecControlCheckListCommentInfo.Comments != null)
                cmdParamComments.Value = oGLDataRecControlCheckListCommentInfo.Comments;
            else
                cmdParamComments.Value = DBNull.Value;
            cmdParams.Add(cmdParamComments);

            IDbDataParameter cmdParamAddedByUserID = cmd.CreateParameter();
            cmdParamAddedByUserID.ParameterName = "@AddedByUserID";
            if (oGLDataRecControlCheckListCommentInfo.AddedByUserID.HasValue)
                cmdParamAddedByUserID.Value = oGLDataRecControlCheckListCommentInfo.AddedByUserID.Value;
            else
                cmdParamAddedByUserID.Value = DBNull.Value;
            cmdParams.Add(cmdParamAddedByUserID);

            IDbDataParameter cmdParamRoleID = cmd.CreateParameter();
            cmdParamRoleID.ParameterName = "@RoleID";
            if (oGLDataRecControlCheckListCommentInfo.RoleID.HasValue)
                cmdParamRoleID.Value = oGLDataRecControlCheckListCommentInfo.RoleID.Value;
            else
                cmdParamRoleID.Value = DBNull.Value;
            cmdParams.Add(cmdParamRoleID);


            IDbDataParameter cmdParamAddedBy = cmd.CreateParameter();
            cmdParamAddedBy.ParameterName = "@AddedBy";
            if (oGLDataRecControlCheckListCommentInfo.AddedBy != null)
                cmdParamAddedBy.Value = oGLDataRecControlCheckListCommentInfo.AddedBy;
            else
                cmdParamAddedBy.Value = DBNull.Value;
            cmdParams.Add(cmdParamAddedBy);

            IDbDataParameter cmdParamDateAdded = cmd.CreateParameter();
            cmdParamDateAdded.ParameterName = "@DateAdded";
            if (oGLDataRecControlCheckListCommentInfo.DateAdded.HasValue)
                cmdParamDateAdded.Value = oGLDataRecControlCheckListCommentInfo.DateAdded;
            else
                cmdParamDateAdded.Value = DBNull.Value;
            cmdParams.Add(cmdParamDateAdded);


            return cmd;
        }
        public List<GLDataRecControlCheckListInfo> SaveGLDataRecControlChecklist(List<GLDataRecControlCheckListInfo> oGLDataRecControlCheckListInfoList, string modifiedBy, DateTime? dateModified,
        IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.SaveGLDataRecControlChecklistCommand(oGLDataRecControlCheckListInfoList, modifiedBy, dateModified);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    long? GLDataID = dr.GetInt64Value("GLDataID");
                    int? RecControlCheckListID = dr.GetInt32Value("RecControlCheckListID");
                    long? GLDataRecControlCheckListID = dr.GetInt64Value("GLDataRecControlCheckListID");
                    GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo = oGLDataRecControlCheckListInfoList.Find(T => T.GLDataID == GLDataID && T.RecControlCheckListID == RecControlCheckListID);
                    if (oGLDataRecControlCheckListInfo != null)
                        oGLDataRecControlCheckListInfo.GLDataRecControlCheckListID = GLDataRecControlCheckListID;
                }
            }
            return oGLDataRecControlCheckListInfoList;
        }
        protected IDbCommand SaveGLDataRecControlChecklistCommand(List<GLDataRecControlCheckListInfo> oGLDataRecControlCheckListInfoList, string modifiedBy, DateTime? dateModified)
        {
            IDbCommand cmd = this.CreateCommand("usp_MERGE_GLDataRecControlChecklist");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamGLDataRecControlChecklistTable = cmd.CreateParameter();
            cmdParamGLDataRecControlChecklistTable.ParameterName = "@udtGLDataRecControlChecklistTableType";
            cmdParamGLDataRecControlChecklistTable.Value = ServiceHelper.ConvertGLDataRecControlCheckListInfoListToDataTable(oGLDataRecControlCheckListInfoList);
            cmdParams.Add(cmdParamGLDataRecControlChecklistTable);

            IDbDataParameter cmdParamDateModified = cmd.CreateParameter();
            cmdParamDateModified.ParameterName = "@DateModified";
            cmdParamDateModified.Value = dateModified.Value;
            cmdParams.Add(cmdParamDateModified);

            IDbDataParameter cmdParamModifiedBy = cmd.CreateParameter();
            cmdParamModifiedBy.ParameterName = "@ModifiedBy";
            cmdParamModifiedBy.Value = modifiedBy;
            cmdParams.Add(cmdParamModifiedBy);
            return cmd;
        }

        public List<RecControlCheckListAccountInfo> SaveAccountRecControlChecklist(List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList,
          int? recPeriodID, int? dataImportID, string modifiedBy, DateTime? dateModified, long? GLDataID,
          IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.SaveAccountRecControlChecklistCommand(oRecControlCheckListAccountInfoList, recPeriodID, dataImportID, modifiedBy, dateModified, GLDataID);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            using (IDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    int? id = dr.GetInt32Value("RowNumber");
                    long? RecControlCheckListAccountID = dr.GetInt64Value("RecControlCheckListAccountID");
                    RecControlCheckListAccountInfo oRecControlCheckListAccountInfo = oRecControlCheckListAccountInfoList.Find(T => T.RowNumber == id.Value);
                    if (oRecControlCheckListAccountInfo != null)
                        oRecControlCheckListAccountInfo.RecControlCheckListAccountID = RecControlCheckListAccountID;
                }
            }
            return oRecControlCheckListAccountInfoList;
        }
        protected IDbCommand SaveAccountRecControlChecklistCommand(List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList,
            int? recPeriodID, int? dataImportID, string modifiedBy, DateTime? dateModified, long? GLDataID)
        {
            IDbCommand cmd = this.CreateCommand("usp_MERGE_AccountRecControlChecklist");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter cmdParamAccountRecControlChecklistTable = cmd.CreateParameter();
            cmdParamAccountRecControlChecklistTable.ParameterName = "@udtAccountRecControlChecklistTableType";
            cmdParamAccountRecControlChecklistTable.Value = ServiceHelper.ConvertRecControlCheckListAccountInfolistToDataTable(oRecControlCheckListAccountInfoList);
            cmdParams.Add(cmdParamAccountRecControlChecklistTable);

            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);

            IDbDataParameter cmdParamGLDataID = cmd.CreateParameter();
            cmdParamGLDataID.ParameterName = "@GLDataID";
            if (GLDataID.HasValue)
                cmdParamGLDataID.Value = GLDataID.Value;
            else
                cmdParamGLDataID.Value = DBNull.Value;
            cmdParams.Add(cmdParamGLDataID);


            IDbDataParameter cmdParamDataImportID = cmd.CreateParameter();
            cmdParamDataImportID.ParameterName = "@DataImportID";
            if (dataImportID.HasValue)
                cmdParamDataImportID.Value = dataImportID.Value;
            else
                cmdParamDataImportID.Value = DBNull.Value;
            cmdParams.Add(cmdParamDataImportID);

            IDbDataParameter cmdParamDateModified = cmd.CreateParameter();
            cmdParamDateModified.ParameterName = "@DateModified";
            cmdParamDateModified.Value = dateModified.Value;
            cmdParams.Add(cmdParamDateModified);

            IDbDataParameter cmdParamModifiedBy = cmd.CreateParameter();
            cmdParamModifiedBy.ParameterName = "@ModifiedBy";
            cmdParamModifiedBy.Value = modifiedBy;
            cmdParams.Add(cmdParamModifiedBy);
            return cmd;
        }

        internal GLDataRecControlCheckListInfo GetRecControlCheckListCount(long? GlDataID, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo = new GLDataRecControlCheckListInfo();
            try
            {

                cmd = CreateGetRecControlCheckListCountCommand(GlDataID, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oGLDataRecControlCheckListInfo.CompletedCount = dr.GetInt32Value("CompletedCount");
                        oGLDataRecControlCheckListInfo.ReviewedCount = dr.GetInt32Value("ReviewedCount");
                        oGLDataRecControlCheckListInfo.TotalCount = dr.GetInt32Value("TotalCount");
                    }
                }
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
            return oGLDataRecControlCheckListInfo;
        }

        private IDbCommand CreateGetRecControlCheckListCountCommand(long? GLDataID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_GLDataRecControlCheckListCount");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (GLDataID.HasValue)
                parGLDataID.Value = GLDataID.Value;
            else
                parGLDataID.Value = DBNull.Value;

            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;

            cmdParams.Add(parRecPeriodID);
            return cmd;
        }

        internal List<RCCValidationTypeMstInfo> GetRCCValidationTypeMstInfoList()
        {
            List<RCCValidationTypeMstInfo> oRCCValidationTypeMstInfoList = new List<RCCValidationTypeMstInfo>();
            RCCValidationTypeMstInfo oRCCValidationTypeMstInfo;
            using (IDbConnection con = this.CreateConnection())
            {
                try
                {
                    con.Open();
                    using (IDbCommand cmd = CreateGetRCCValidationTypeMstInfoListCommand())
                    {
                        cmd.Connection = con;
                        using (IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                oRCCValidationTypeMstInfo = new RCCValidationTypeMstInfo();
                                oRCCValidationTypeMstInfo.RCCValidationTypeID = dr.GetInt16Value("RCCValidationTypeID");
                                oRCCValidationTypeMstInfo.RCCValidationTypeName = dr.GetStringValue("RCCValidationTypeName");
                                oRCCValidationTypeMstInfo.RCCValidationTypeNameLabelID = dr.GetInt32Value("RCCValidationTypeNameLabelID");
                                oRCCValidationTypeMstInfoList.Add(oRCCValidationTypeMstInfo);
                            }
                        }
                    }
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            return oRCCValidationTypeMstInfoList;
        }
        private IDbCommand CreateGetRCCValidationTypeMstInfoListCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RCCValidationType");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        protected override RecControlCheckListInfo MapObject(IDataReader r)
        {
            RecControlCheckListInfo entity = base.MapObject(r);
            entity.DataImportTypeID = r.GetInt16Value("DataImportTypeID");
            return entity;
        }
    }
}