


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class GLDataRecItemDAO : GLDataRecItemDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataRecItemDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public void InsertRecInputItem(GLDataRecItemInfo oGLDataRecItemInfo, short recTemplateAttributeID, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd;
            //if (oGLDataRecItemInfo.IsScheduleItem.HasValue && oGLDataRecItemInfo.IsScheduleItem.Value)
            //{
            //    cmd = this.CreateInsertWithScheduleCommand(oGLDataRecItemInfo);
            //}
            //else
            //{
            cmd = this.CreateInsertCommand(oGLDataRecItemInfo);
            //}
            cmd.Connection = con;
            cmd.Transaction = oTransaction;

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmd.Parameters.Add(paramRecTemplateAttributeID);

            IDbDataParameter parID = cmd.CreateParameter();
            parID.ParameterName = "@ID";
            parID.Direction = ParameterDirection.Output;
            parID.Size = 8;
            cmd.Parameters.Add(parID);

            System.Data.IDbDataParameter parRecordSourceTypeID = cmd.CreateParameter();
            parRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            if (oGLDataRecItemInfo.RecordSourceTypeID.HasValue)
                parRecordSourceTypeID.Value = oGLDataRecItemInfo.RecordSourceTypeID;
            else
                parRecordSourceTypeID.Value = System.DBNull.Value;
            cmd.Parameters.Add(parRecordSourceTypeID);

            System.Data.IDbDataParameter parRecordSourceID = cmd.CreateParameter();
            parRecordSourceID.ParameterName = "@RecordSourceID";
            if (oGLDataRecItemInfo.RecordSourceID.HasValue)
                parRecordSourceID.Value = oGLDataRecItemInfo.RecordSourceID;
            else
                parRecordSourceID.Value = System.DBNull.Value;
            cmd.Parameters.Add(parRecordSourceID);

            System.Data.IDbDataParameter parExRateLCCYtoBCCY = cmd.CreateParameter();
            parExRateLCCYtoBCCY.ParameterName = "@ExRateLCCYtoBCCY";
            if (oGLDataRecItemInfo.ExRateLCCYtoBCCY.HasValue)
                parExRateLCCYtoBCCY.Value = oGLDataRecItemInfo.ExRateLCCYtoBCCY;
            else
                parExRateLCCYtoBCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoBCCY);

            System.Data.IDbDataParameter parExRateLCCYtoRCCY = cmd.CreateParameter();
            parExRateLCCYtoRCCY.ParameterName = "@ExRateLCCYtoRCCY";
            if (oGLDataRecItemInfo.ExRateLCCYtoRCCY.HasValue)
                parExRateLCCYtoRCCY.Value = oGLDataRecItemInfo.ExRateLCCYtoRCCY;
            else
                parExRateLCCYtoRCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoRCCY);


            cmd.ExecuteNonQuery();

            oGLDataRecItemInfo.GLDataRecItemID = Convert.ToInt64(parID.Value);
        }

        public List<GLDataRecItemInfo> GetTotalByReconciliationCategoryTypeID(long? gLDataID)
        {
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            IDbCommand oDBCommand = null;
            IDbConnection oConnection = null;
            try
            {
                oDBCommand = this.TotalByReconciliationCategoryTypeIDCommand(gLDataID);
                oConnection = this.CreateConnection();
                oConnection.Open();
                oDBCommand.Connection = oConnection;
                IDataReader reader = oDBCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    //oIncompleteRequirementCollection.Add(this.MapIncompleteRequirementToMarkOpen(reader));
                    oGLDataRecItemInfoCollection.Add(this.MapTotalByReconciliationCategoryTypeID(reader));
                }
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

            return oGLDataRecItemInfoCollection;
        }

        //protected IDbCommand TotalByReconciliationCategoryTypeIDCommand(int? reconciliationCategoryTypeID, int? gLDataID)
        protected IDbCommand TotalByReconciliationCategoryTypeIDCommand(long? gLDataID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_TotalGroupByReconciliationCategoryTypeID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            //IDbDataParameter cmdParamCategoryTypeID = oIDBCommand.CreateParameter();
            //cmdParamCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            //cmdParamCategoryTypeID.Value = reconciliationCategoryTypeID;
            //cmdParams.Add(cmdParamCategoryTypeID);

            IDbDataParameter cmdParamGLDataID = oIDBCommand.CreateParameter();
            cmdParamGLDataID.ParameterName = "@GLDataID";
            cmdParamGLDataID.Value = gLDataID;
            cmdParams.Add(cmdParamGLDataID);

            return oIDBCommand;

        }

        private GLDataRecItemInfo MapTotalByReconciliationCategoryTypeID(IDataReader reader)
        {
            GLDataRecItemInfo entity = new GLDataRecItemInfo();

            try
            {
                int ordinal = reader.GetOrdinal("TotalBaseCurrency");
                if (!reader.IsDBNull(ordinal)) entity.AmountBaseCurrency = ((System.Decimal)(reader.GetValue(ordinal)));
            }
            catch { }

            try
            {
                int ordinal = reader.GetOrdinal("TotalReportingCurrency");
                if (!reader.IsDBNull(ordinal)) entity.AmountReportingCurrency = ((System.Decimal)(reader.GetValue(ordinal)));
            }
            catch { }

            try
            {
                int ordinal = reader.GetOrdinal("ReconciliationCategoryTypeID");
                if (!reader.IsDBNull(ordinal)) entity.ReconciliationCategoryTypeID = ((System.Int16)(reader.GetValue(ordinal)));
            }
            catch { }

            return entity;
        }

        #region GetRecItem

        public List<GLDataRecItemInfo> GetRecItem(long glDataID, int recPeriodID, short recCategoryTypeID, short glReconciliationItemInputRecordTypeID, short accountTemplateAttributeID)
        {
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetRecItemCommand(glDataID, recPeriodID, recCategoryTypeID, glReconciliationItemInputRecordTypeID, accountTemplateAttributeID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataRecItemInfo oGLDataRecItemInfo = null;
                while (reader.Read())
                {
                    oGLDataRecItemInfo = this.MapObject(reader);
                    oGLDataRecItemInfo.AttachmentCount = reader.GetInt32Value("AttachmentCount");
                    oGLDataRecItemInfo.IsForwardedItem = reader.GetBooleanValue("IsForwardedItem");
                    oGLDataRecItemInfo.UserName = reader.GetStringValue("UserName");
                    oGLDataRecItemInfo.MatchSetRefNumber = reader.GetStringValue("MatchSetRef");
                    oGLDataRecItemInfo.MatchSetID = reader.GetInt64Value("MatchSetID");
                    oGLDataRecItemInfo.MatchSetSubSetCombinationID = reader.GetInt64Value("MatchSetSubSetCombinationID");
                    oGLDataRecItemInfo.PreviousGLDataRecItemID = reader.GetInt64Value("PreviousGLDataRecItemID");
                    oGLDataRecItemInfo.OriginalGLDataRecItemID = reader.GetInt64Value("OriginalGLDataRecItemID");
                    oGLDataRecItemInfo.ExRateLCCYtoBCCY = reader.GetDecimalValue("ExRateLCCYtoBCCY");
                    oGLDataRecItemInfo.ExRateLCCYtoRCCY = reader.GetDecimalValue("ExRateLCCYtoRCCY");
                    oGLDataRecItemInfo.PhysicalPath = reader.GetStringValue("PhysicalPath");
                    oGLDataRecItemInfo.IsCommentAvailable = reader.GetBooleanValue("IsCommentAvailable");
                    oGLDataRecItemInfo.DataImportTypeID = reader.GetInt16Value("DataImportTypeID");
                    oGLDataRecItemInfoCollection.Add(oGLDataRecItemInfo);
                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oGLDataRecItemInfoCollection;
        }

        private IDbCommand CreateGetRecItemCommand(long glDataID, int recPeriodID, short recCategoryTypeID, short glReconciliationItemInputRecordTypeID, short accountTemplateAttributeID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramAccountID = cmd.CreateParameter();
            paramAccountID.ParameterName = "@GLDataID";
            paramAccountID.Value = glDataID;
            paramCollection.Add(paramAccountID);

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            paramCollection.Add(paramRecPeriodID);

            IDbDataParameter paramRecCategoryTypeID = cmd.CreateParameter();
            paramRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            paramRecCategoryTypeID.Value = recCategoryTypeID;
            paramCollection.Add(paramRecCategoryTypeID);

            IDbDataParameter paramGLReconciliationItemInputRecordTypeID = cmd.CreateParameter();
            paramGLReconciliationItemInputRecordTypeID.ParameterName = "@GLReconciliationItemInputRecordTypeID";
            paramGLReconciliationItemInputRecordTypeID.Value = glReconciliationItemInputRecordTypeID;
            paramCollection.Add(paramGLReconciliationItemInputRecordTypeID);

            IDbDataParameter paramAccountTemplateAttributeID = cmd.CreateParameter();
            paramAccountTemplateAttributeID.ParameterName = "@AccountTemplateAttributeID";
            paramAccountTemplateAttributeID.Value = accountTemplateAttributeID;
            paramCollection.Add(paramAccountTemplateAttributeID);

            return cmd;
        }

        public List<GLDataRecItemInfo> GetClosedGLDataRecItem(long? GlDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID)
        {
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetClosedGLDataRecItemCommand(GlDataID, MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID, GLDataRecItemTypeID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataRecItemInfo oGLDataRecItemInfo = null;
                while (reader.Read())
                {
                    oGLDataRecItemInfo = this.MapObject(reader);
                    oGLDataRecItemInfo.AttachmentCount = reader.GetInt32Value("AttachmentCount");
                    oGLDataRecItemInfo.IsForwardedItem = reader.GetBooleanValue("IsForwardedItem");
                    oGLDataRecItemInfo.UserName = reader.GetStringValue("UserName");
                    oGLDataRecItemInfo.MatchSetRefNumber = reader.GetStringValue("MatchSetRef");
                    oGLDataRecItemInfo.MatchSetID = reader.GetInt64Value("MatchSetID");
                    oGLDataRecItemInfo.MatchSetSubSetCombinationID = reader.GetInt64Value("MatchSetSubSetCombinationID");
                    oGLDataRecItemInfo.PreviousGLDataRecItemID = reader.GetInt64Value("PreviousGLDataRecItemID");
                    oGLDataRecItemInfo.OriginalGLDataRecItemID = reader.GetInt64Value("OriginalGLDataRecItemID");
                    oGLDataRecItemInfo.ExRateLCCYtoBCCY = reader.GetDecimalValue("ExRateLCCYtoBCCY");
                    oGLDataRecItemInfo.ExRateLCCYtoRCCY = reader.GetDecimalValue("ExRateLCCYtoRCCY");
                    oGLDataRecItemInfoCollection.Add(oGLDataRecItemInfo);
                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oGLDataRecItemInfoCollection;
        }

        private IDbCommand CreateGetClosedGLDataRecItemCommand(long? glDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].[usp_SEL_ClosedGLDataRecItemByMatching]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramAccountID = cmd.CreateParameter();
            paramAccountID.ParameterName = "@GLDataID";
            paramAccountID.Value = glDataID;
            paramCollection.Add(paramAccountID);

            IDbDataParameter paramMatchsetSubSetCombinationID = cmd.CreateParameter();
            paramMatchsetSubSetCombinationID.ParameterName = "@MatchsetSubSetCombinationID";
            paramMatchsetSubSetCombinationID.Value = MatchsetSubSetCombinationID;
            paramCollection.Add(paramMatchsetSubSetCombinationID);

            IDbDataParameter paramExCelRowNumber = cmd.CreateParameter();
            paramExCelRowNumber.ParameterName = "@ExCelRowNumber";
            paramExCelRowNumber.Value = ExCelRowNumber;
            paramCollection.Add(paramExCelRowNumber);

            IDbDataParameter paramMatchSetMatchingSourceDataImportID = cmd.CreateParameter();
            paramMatchSetMatchingSourceDataImportID.ParameterName = "@MatchSetMatchingSourceDataImportID";
            paramMatchSetMatchingSourceDataImportID.Value = MatchSetMatchingSourceDataImportID;
            paramCollection.Add(paramMatchSetMatchingSourceDataImportID);

            IDbDataParameter paramGLDataRecItemTypeID = cmd.CreateParameter();
            paramGLDataRecItemTypeID.ParameterName = "@GLDataRecItemTypeID";
            paramGLDataRecItemTypeID.Value = GLDataRecItemTypeID;
            paramCollection.Add(paramGLDataRecItemTypeID);

            return cmd;
        }

        #endregion

        #region InsertRecInputItem

        //public void InsertRecInputItem(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, IDbConnection con, IDbTransaction oTransaction)
        //{
        //    IDbCommand cmd = this.CreateInsertCommand(oGLRecItemInput);
        //    cmd.Connection = con;
        //    cmd.Transaction = oTransaction;

        //    IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
        //    paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
        //    paramRecTemplateAttributeID.Value = recTemplateAttributeID;
        //    cmd.Parameters.Add(paramRecTemplateAttributeID);

        //    cmd.ExecuteNonQuery();
        //}

        #endregion

        #region UpdateGLRecItemCloseDate

        public void UpdateGLRecItemCloseDate(long glDataID, DataTable dtGLRecItemInputIDs, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateUpdateGLRecItemCloseDateCommand(glDataID, dtGLRecItemInputIDs, closeDate, closeComments, journalEntryRef, comments, recCategoryTypeID, recTemplateAttributeID, revisedBy, dateRevised);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;

            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateUpdateGLRecItemCloseDateCommand(long glDataID, DataTable dtGLRecItemInputIDs, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataRecItemCloseDate");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramGLDataID = cmd.CreateParameter();
            paramGLDataID.ParameterName = "@GLDataID";
            paramGLDataID.Value = glDataID;
            paramCollection.Add(paramGLDataID);

            IDbDataParameter paramGLRecItemInputIDTable = cmd.CreateParameter();
            paramGLRecItemInputIDTable.ParameterName = "@GLRecItemInputIDTable";
            paramGLRecItemInputIDTable.Value = dtGLRecItemInputIDs;
            paramCollection.Add(paramGLRecItemInputIDTable);

            IDbDataParameter paramCloseDate = cmd.CreateParameter();
            paramCloseDate.ParameterName = "@CloseDate";
            if (closeDate != null)
            {
                paramCloseDate.Value = closeDate;
            }
            else
            {
                paramCloseDate.Value = DBNull.Value;
            }
            paramCollection.Add(paramCloseDate);

            IDbDataParameter paramCloseComments = cmd.CreateParameter();
            paramCloseComments.ParameterName = "@CloseComments";
            if (!string.IsNullOrEmpty(closeComments))
            {
                paramCloseComments.Value = closeComments;
            }
            else
            {
                paramCloseComments.Value = DBNull.Value;
            }
            paramCollection.Add(paramCloseComments);

            IDbDataParameter paramAccountJournalEntryRef = cmd.CreateParameter();
            paramAccountJournalEntryRef.ParameterName = "@JournalEntryRef";
            if (journalEntryRef != null)
            {
                paramAccountJournalEntryRef.Value = journalEntryRef;
            }
            else
            {
                paramAccountJournalEntryRef.Value = DBNull.Value;
            }
            paramCollection.Add(paramAccountJournalEntryRef);

            IDbDataParameter paramComments = cmd.CreateParameter();
            paramComments.ParameterName = "@Comments";
            if (!string.IsNullOrEmpty(comments))
            {
                paramComments.Value = comments;
            }
            else
            {
                paramComments.Value = DBNull.Value;
            }
            paramCollection.Add(paramComments);

            IDbDataParameter paramRecCategoryTypeID = cmd.CreateParameter();
            paramRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            paramRecCategoryTypeID.Value = recCategoryTypeID;
            paramCollection.Add(paramRecCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            paramCollection.Add(paramRecTemplateAttributeID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            paramCollection.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (dateRevised.HasValue)
                parDateRevised.Value = dateRevised.Value;
            else
                parDateRevised.Value = DBNull.Value;
            paramCollection.Add(parDateRevised);

            return cmd;
        }

        #endregion

        #region UpdateRecItemInput

        public void UpdateRecItemInput(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateUpdateRecItemInputCommand(oGLRecItemInput, recTemplateAttributeID);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;

            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateUpdateRecItemInputCommand(GLDataRecItemInfo entity, short recTemplateAttributeID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAmount = cmd.CreateParameter();
            parAmount.ParameterName = "@Amount";
            if (!entity.IsAmountNull)
                parAmount.Value = entity.Amount;
            else
                parAmount.Value = System.DBNull.Value;
            cmdParams.Add(parAmount);

            System.Data.IDbDataParameter parAmountBaseCurrency = cmd.CreateParameter();
            parAmountBaseCurrency.ParameterName = "@AmountBaseCurrency";
            if (!entity.IsAmountBaseCurrencyNull)
                parAmountBaseCurrency.Value = entity.AmountBaseCurrency;
            else
                parAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountBaseCurrency);

            System.Data.IDbDataParameter parAmountReportingCurrency = cmd.CreateParameter();
            parAmountReportingCurrency.ParameterName = "@AmountReportingCurrency";
            if (!entity.IsAmountReportingCurrencyNull)
                parAmountReportingCurrency.Value = entity.AmountReportingCurrency;
            else
                parAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountReportingCurrency);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            //System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            //parDataImportID.ParameterName = "@DataImportID";
            //if (!entity.IsDataImportIDNull)
            //    parDataImportID.Value = entity.DataImportID;
            //else
            //    parDataImportID.Value = System.DBNull.Value;
            //cmdParams.Add(parDataImportID);


            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parGLRecItemInputID = cmd.CreateParameter();
            parGLRecItemInputID.ParameterName = "@GLRecItemInputID";
            parGLRecItemInputID.Value = entity.GLDataRecItemID;
            cmdParams.Add(parGLRecItemInputID);

            System.Data.IDbDataParameter parIsAttachmentAvailable = cmd.CreateParameter();
            parIsAttachmentAvailable.ParameterName = "@IsAttachmentAvailable";
            if (!entity.IsIsAttachmentAvailableNull)
                parIsAttachmentAvailable.Value = entity.IsAttachmentAvailable;
            else
                parIsAttachmentAvailable.Value = System.DBNull.Value;
            cmdParams.Add(parIsAttachmentAvailable);

            System.Data.IDbDataParameter parLocalCurrencyCode = cmd.CreateParameter();
            parLocalCurrencyCode.ParameterName = "@LocalCurrencyCode";
            if (!entity.IsLocalCurrencyCodeNull)
                parLocalCurrencyCode.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrencyCode);

            System.Data.IDbDataParameter parOpenDate = cmd.CreateParameter();
            parOpenDate.ParameterName = "@OpenDate";
            if (!entity.IsOpenDateNull)
                parOpenDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parOpenDate.Value = System.DBNull.Value;
            cmdParams.Add(parOpenDate);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (!entity.IsReconciliationCategoryTypeIDNull)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmdParams.Add(paramRecTemplateAttributeID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = entity.RevisedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parDateRevised.Value = entity.DateRevised.Value;
            else
                parDateRevised.Value = DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parExRateLCCYtoBCCY = cmd.CreateParameter();
            parExRateLCCYtoBCCY.ParameterName = "@ExRateLCCYtoBCCY";
            if (entity.ExRateLCCYtoBCCY.HasValue)
                parExRateLCCYtoBCCY.Value = entity.ExRateLCCYtoBCCY;
            else
                parExRateLCCYtoBCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoBCCY);

            System.Data.IDbDataParameter parExRateLCCYtoRCCY = cmd.CreateParameter();
            parExRateLCCYtoRCCY.ParameterName = "@ExRateLCCYtoRCCY";
            if (entity.ExRateLCCYtoRCCY.HasValue)
                parExRateLCCYtoRCCY.Value = entity.ExRateLCCYtoRCCY;
            else
                parExRateLCCYtoRCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoRCCY);
            return cmd;
        }

        #endregion

        #region DeleteRecItemInput

        public void DeleteRecItemInput(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, DataTable dtGLDataParams, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateDeleteRecItemInputCommand(oGLRecItemInput, recTemplateAttributeID, dtGLDataParams);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;

            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateDeleteRecItemInputCommand(GLDataRecItemInfo entity, short recTemplateAttributeID, DataTable dtGLDataParams)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_GLRecItemInput");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parGLRecItemInputID = cmd.CreateParameter();
            parGLRecItemInputID.ParameterName = "@tblGLRecItemIDs";
            parGLRecItemInputID.Value = dtGLDataParams;
            cmdParams.Add(parGLRecItemInputID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (!entity.IsReconciliationCategoryTypeIDNull)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmdParams.Add(paramRecTemplateAttributeID);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = entity.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = entity.DateRevised;
            cmdParams.Add(paramDateRevised);

            return cmd;
        }

        #endregion



        public List<GLDataRecItemInfo> BulkInsertRecInputItem(DataTable dtGLDataRecItem, GLDataRecItemInfo oGLDataRecItemInfo, short recTemplateAttributeID, out int? rowsAffected, IDbConnection con, IDbTransaction oTransaction)
        {
            List<GLDataRecItemInfo> oTempGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            IDbCommand cmd = null;
            IDataReader dr = null;
            try
            {

                cmd = GetBulkInsertRecInputItemCommand(dtGLDataRecItem, oGLDataRecItemInfo, recTemplateAttributeID);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                dr = cmd.ExecuteReader();
                GLDataRecItemInfo objGLDataRecItemInfo;
                while (dr.Read())
                {
                    objGLDataRecItemInfo = new GLDataRecItemInfo();
                    objGLDataRecItemInfo.IndexID = dr.GetInt16Value("IndexID");
                    objGLDataRecItemInfo.RecItemNumber = dr.GetStringValue("RecItemNumber");
                    oTempGLDataRecItemInfoCollection.Add(objGLDataRecItemInfo);
                }
                rowsAffected = oTempGLDataRecItemInfoCollection.Count;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }

            return oTempGLDataRecItemInfoCollection;
        }


        private System.Data.IDbCommand GetBulkInsertRecInputItemCommand(DataTable dtGLDataRecItem, GLDataRecItemInfo entity, short recTemplateAttributeID)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_BulkInsertGLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamdtGLDataRecItemTable = cmd.CreateParameter();
            cmdParamdtGLDataRecItemTable.ParameterName = "@GLDataRecItem";
            cmdParamdtGLDataRecItemTable.Value = dtGLDataRecItem;
            cmdParams.Add(cmdParamdtGLDataRecItemTable);

            System.Data.IDbDataParameter parRecordSourceTypeID = cmd.CreateParameter();
            parRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            if (entity.RecordSourceTypeID.HasValue)
                parRecordSourceTypeID.Value = entity.RecordSourceTypeID;
            else
                parRecordSourceTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecordSourceTypeID);

            System.Data.IDbDataParameter parRecordSourceID = cmd.CreateParameter();
            parRecordSourceID.ParameterName = "@RecordSourceID";
            if (entity.RecordSourceID.HasValue)
                parRecordSourceID.Value = entity.RecordSourceID;
            else
                parRecordSourceID.Value = System.DBNull.Value;
            cmdParams.Add(parRecordSourceID);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReconciliationCategoryID = cmd.CreateParameter();
            parReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
            if (!entity.IsReconciliationCategoryIDNull)
                parReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
            else
                parReconciliationCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (!entity.IsReconciliationCategoryTypeIDNull)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmd.Parameters.Add(paramRecTemplateAttributeID);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            parAddedByUserID.Value = entity.AddedByUserID.Value;
            cmdParams.Add(parAddedByUserID);

            System.Data.IDbDataParameter parDataImportId = cmd.CreateParameter();
            parDataImportId.ParameterName = "@DataImportId";
            if (entity.DataImportID.HasValue)
                parDataImportId.Value = entity.DataImportID.Value;
            else
                parDataImportId.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportId);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);





            return cmd;

        }
        public List<GLDataRecItemInfo> CloseMatchingGLDataRecItem(DataTable dtMatchSetGLDataRecItem, GLDataRecItemInfo oGLDataRecItemInfo, short recTemplateAttributeID, DateTime? CloseDate, out int? rowsAffected, IDbConnection con, IDbTransaction oTransaction)
        {
            List<GLDataRecItemInfo> oTempGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            IDbCommand cmd = null;
            IDataReader dr = null;
            try
            {

                cmd = GetCloseMatchingGLDataRecItemCommand(dtMatchSetGLDataRecItem, oGLDataRecItemInfo, recTemplateAttributeID, CloseDate);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                dr = cmd.ExecuteReader();
                GLDataRecItemInfo objGLDataRecItemInfo;
                while (dr.Read())
                {
                    objGLDataRecItemInfo = new GLDataRecItemInfo();
                    objGLDataRecItemInfo.GLDataRecItemID = dr.GetInt64Value("GLDataRecItemID");
                    objGLDataRecItemInfo.CloseDate = dr.GetDateValue("CloseDate");
                    oTempGLDataRecItemInfoCollection.Add(objGLDataRecItemInfo);
                }
                rowsAffected = oTempGLDataRecItemInfoCollection.Count;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }

            return oTempGLDataRecItemInfoCollection;
        }

        private System.Data.IDbCommand GetCloseMatchingGLDataRecItemCommand(DataTable dtMatchSetGLDataRecItem, GLDataRecItemInfo entity, short recTemplateAttributeID, DateTime? CloseDate)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("Matching.usp_UPD_CloseGLDataRecItemByMatching");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamdtMatchSetGLDataRecItem = cmd.CreateParameter();
            cmdParamdtMatchSetGLDataRecItem.ParameterName = "@MatchSetGLDataRecItem";
            cmdParamdtMatchSetGLDataRecItem.Value = dtMatchSetGLDataRecItem;
            cmdParams.Add(cmdParamdtMatchSetGLDataRecItem);


            IDbDataParameter paramCloseDate = cmd.CreateParameter();
            paramCloseDate.ParameterName = "@CloseDate";
            if (CloseDate.HasValue)
            {
                paramCloseDate.Value = CloseDate;
            }
            else
            {
                paramCloseDate.Value = DBNull.Value;
            }
            cmdParams.Add(paramCloseDate);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);


            System.Data.IDbDataParameter parReconciliationCategoryID = cmd.CreateParameter();
            parReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
            if (!entity.IsReconciliationCategoryIDNull)
                parReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
            else
                parReconciliationCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (!entity.IsReconciliationCategoryTypeIDNull)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmd.Parameters.Add(paramRecTemplateAttributeID);

            IDbDataParameter paramGLDataRecItemTypeID = cmd.CreateParameter();
            paramGLDataRecItemTypeID.ParameterName = "@GLDataRecItemTypeID";
            if (entity.ReconciliationCategoryTypeID.HasValue)
                paramGLDataRecItemTypeID.Value = entity.GLDataRecItemTypeID;
            else
                paramGLDataRecItemTypeID.Value = System.DBNull.Value;
            cmd.Parameters.Add(paramGLDataRecItemTypeID);

            System.Data.IDbDataParameter ParmRevisedBy = cmd.CreateParameter();
            ParmRevisedBy.ParameterName = "@RevisedBy";
            if (entity.RevisedBy != null)
                ParmRevisedBy.Value = entity.RevisedBy;
            else
                ParmRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(ParmRevisedBy);

            System.Data.IDbDataParameter parmDateRevised = cmd.CreateParameter();
            parmDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parmDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parmDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parmDateRevised);
            return cmd;

        }
        internal short? GetGLRecItemTypeID(long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID)
        {
            System.Data.IDbCommand oCommand = null;
            try
            {
                oCommand = GetGLRecItemTypeIDCommand(MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                short? GLRecItemTypeID = Convert.ToInt16(oCommand.ExecuteScalar());
                return GLRecItemTypeID;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }
        }
        public System.Data.IDbCommand GetGLRecItemTypeIDCommand(long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[Matching].[usp_GET_GLRecItemTypeID]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parMatchsetSubSetCombinationID = cmd.CreateParameter();
            parMatchsetSubSetCombinationID.ParameterName = "@MatchsetSubSetCombinationID";
            parMatchsetSubSetCombinationID.Value = MatchsetSubSetCombinationID;
            cmdParams.Add(parMatchsetSubSetCombinationID);

            System.Data.IDbDataParameter parExCelRowNumber = cmd.CreateParameter();
            parExCelRowNumber.ParameterName = "@ExCelRowNumber";
            parExCelRowNumber.Value = ExCelRowNumber;
            cmdParams.Add(parExCelRowNumber);

            System.Data.IDbDataParameter parMatchSetMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchSetMatchingSourceDataImportID.ParameterName = "@MatchSetMatchingSourceDataImportID";
            parMatchSetMatchingSourceDataImportID.Value = MatchSetMatchingSourceDataImportID;
            cmdParams.Add(parMatchSetMatchingSourceDataImportID);

            return cmd;
        }

        public GLDataRecItemInfo GetGLDataRecItemInfo(long? RecordID, short? RecordTypeID)
        {
            GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetGLDataRecItemInfoCommand(RecordID, RecordTypeID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oGLDataRecItemInfo = this.MapObject(reader);
                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oGLDataRecItemInfo;
        }

        private IDbCommand CreateGetGLDataRecItemInfoCommand(long? RecordID, short? RecordTypeID)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_GET_GLDataRecItem]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramRecordID = cmd.CreateParameter();
            paramRecordID.ParameterName = "@RecordID";
            paramRecordID.Value = RecordID;
            paramCollection.Add(paramRecordID);

            IDbDataParameter paramRecordTypeID = cmd.CreateParameter();
            paramRecordTypeID.ParameterName = "@RecordTypeID";
            paramRecordTypeID.Value = RecordTypeID;
            paramCollection.Add(paramRecordTypeID);

            return cmd;
        }


        public List<AttachmentInfo> CopyRecInputItem(GLDataRecItemInfo oGLRecItemInput, DataTable dtGLDataParams, int? recPeriodID, bool CloseSourceRecItem, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            List<AttachmentInfo> oAttachmentInfoList = null;
            IDbCommand cmd = null;
            IDataReader reader=null;
            try
            {
                cmd = this.CreateCopyRecItemCommand(oGLRecItemInput, dtGLDataParams, recPeriodID, CloseSourceRecItem);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                reader = cmd.ExecuteReader();
                oAttachmentInfoList = new List<AttachmentInfo>();
                while (reader.Read())
                {
                    oAttachmentInfoList.Add(MapObjectAttachmentInfo(reader));
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            return oAttachmentInfoList;
        }

        private IDbCommand CreateCopyRecItemCommand(GLDataRecItemInfo entity, DataTable dtGLDataParams, int? recPeriodID, bool CloseSourceRecItem)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CopyGLRecItem");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTblSourceRecItemID = cmd.CreateParameter();
            parTblSourceRecItemID.ParameterName = "@tblSourceRecItemID";
            parTblSourceRecItemID.Value = dtGLDataParams;
            cmdParams.Add(parTblSourceRecItemID);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (recPeriodID.HasValue)
                parRecPeriodID.Value = recPeriodID.Value;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            parAddedByUserID.Value = entity.AddedByUserID.Value;
            cmdParams.Add(parAddedByUserID);

            IDbDataParameter paramAddedBy = cmd.CreateParameter();
            paramAddedBy.ParameterName = "@AddedBy";
            paramAddedBy.Value = entity.AddedBy;
            cmdParams.Add(paramAddedBy);

            IDbDataParameter paramDateAdded = cmd.CreateParameter();
            paramDateAdded.ParameterName = "@DateAdded";
            paramDateAdded.Value = entity.DateAdded;
            cmdParams.Add(paramDateAdded);

            IDbDataParameter paramCloseSourceRecItem = cmd.CreateParameter();
            paramCloseSourceRecItem.ParameterName = "@CloseSourceRecItem";
            paramCloseSourceRecItem.Value = CloseSourceRecItem;
            cmdParams.Add(paramCloseSourceRecItem);


            return cmd;
        }


        public AttachmentInfo MapObjectAttachmentInfo(System.Data.IDataReader reader)
        {

            AttachmentInfo entity = new AttachmentInfo();
            entity.RecordID = reader.GetInt64Value("RecordID");
            entity.FileName = reader.GetStringValue("FileName");
            entity.FileSize = reader.GetInt32Value("FileSize");
            entity.PhysicalPath = reader.GetStringValue("PhysicalPath");
            entity.Comments = reader.GetStringValue("Comments");
            entity.IsPermanentOrTemporary = reader.GetBooleanValue("IsPermanentOrTemporary");
            entity.RecordTypeID = reader.GetInt16Value("RecordTypeID");
            entity.DocumentName = reader.GetStringValue("DocumentName");

            return entity;
        }



    }//end of the class
}//end of namespace