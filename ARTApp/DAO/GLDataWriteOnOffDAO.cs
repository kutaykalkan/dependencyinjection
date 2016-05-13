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
    public class GLDataWriteOnOffDAO : GLDataWriteOnOffDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataWriteOnOffDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public IList<GLDataWriteOnOffInfo> SelectAllOpenGLDataWriteOnOffInfoItemByGLDataID(long? gLDataID, int? templateAttributeID)
        {
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataWriteOnOffByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDbConnection oConnection = this.CreateConnection();
            oConnection.Open();
            cmd.Connection = oConnection;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamGLDataID = cmd.CreateParameter();
            cmdParamGLDataID.ParameterName = "@GLDataID";
            cmdParamGLDataID.Value = gLDataID;
            cmdParams.Add(cmdParamGLDataID);

            IDbDataParameter cmdTemplateAttributeID = cmd.CreateParameter();
            cmdTemplateAttributeID.ParameterName = "@AccountAttributeIDForAccountTemplateID";
            cmdTemplateAttributeID.Value = templateAttributeID;
            cmdParams.Add(cmdTemplateAttributeID);

            IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = null;
            while (reader.Read())
            {
                oGLDataWriteOnOffInfo = this.MapObject(reader);
                oGLDataWriteOnOffInfoCollection.Add(oGLDataWriteOnOffInfo);
            }

            return oGLDataWriteOnOffInfoCollection;
        }


        public List<GLDataWriteOnOffInfo> GetTotalGLDataWriteOnByGLDataID(long? gLDataID, int? templateAttributeID)
        {
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            IDbCommand oDBCommand = null;
            IDbConnection oConnection = null;
            try
            {
                oDBCommand = this.TotalGLDataWriteOnByGLDataIDCommand(gLDataID, templateAttributeID);
                oConnection = this.CreateConnection();
                oConnection.Open();
                oDBCommand.Connection = oConnection;
                IDataReader reader = oDBCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    //oIncompleteRequirementCollection.Add(this.MapIncompleteRequirementToMarkOpen(reader));
                    oGLDataWriteOnOffInfoCollection.Add(this.MapTotalGLDataWriteOnByGLDataIDCommand(reader));
                }
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

            return oGLDataWriteOnOffInfoCollection;
        }

        //protected IDbCommand TotalByReconciliationCategoryTypeIDCommand(int? reconciliationCategoryTypeID, int? gLDataID)
        protected IDbCommand TotalGLDataWriteOnByGLDataIDCommand(long? gLDataID, int? templateAttributeID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_TotalGLDataWriteOnOff");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdTemplateAttributeID = oIDBCommand.CreateParameter();
            cmdTemplateAttributeID.ParameterName = "@AccountAttributeIDForAccountTemplateID";
            cmdTemplateAttributeID.Value = templateAttributeID;
            cmdParams.Add(cmdTemplateAttributeID);

            IDbDataParameter cmdParamGLDataID = oIDBCommand.CreateParameter();
            cmdParamGLDataID.ParameterName = "@GLDataID";
            cmdParamGLDataID.Value = gLDataID;
            cmdParams.Add(cmdParamGLDataID);

            return oIDBCommand;

        }

        private GLDataWriteOnOffInfo MapTotalGLDataWriteOnByGLDataIDCommand(IDataReader reader)
        {
            GLDataWriteOnOffInfo entity = new GLDataWriteOnOffInfo();

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

            return entity;
        }

        public void UpdateGLDataWriteOnOffCloseDate(long glDataID, DataTable dtGLDataWriteOnOffIDs, DateTime? closeDate, string closeComments, String journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateUpdateGLDataWriteOnOffCloseDateCommand(glDataID, dtGLDataWriteOnOffIDs, closeDate, closeComments, journalEntryRef, comments, recCategoryTypeID, recTemplateAttributeID, revisedBy, dateRevised);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;

            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateUpdateGLDataWriteOnOffCloseDateCommand(long glDataID, DataTable dtGLDataWriteOnOffIDs, DateTime? closeDate, string closeComments, String journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataWriteOnOffCloseDate");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramGLDataID = cmd.CreateParameter();
            paramGLDataID.ParameterName = "@GLDataID";
            paramGLDataID.Value = glDataID;
            paramCollection.Add(paramGLDataID);

            IDbDataParameter paramGLRecItemInputIDTable = cmd.CreateParameter();
            paramGLRecItemInputIDTable.ParameterName = "@GLDataWriteOnOffIDTable";
            paramGLRecItemInputIDTable.Value = dtGLDataWriteOnOffIDs;
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


        #region InsertGLDataWriteOnOff

        public void InsertGLDataWriteOnOff(GLDataWriteOnOffInfo oGLDataWriteOnOffInfo, short? recCategoryID, short? recCategoryTypeID, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);

            try
            {
                cmd = this.CreateInsertGLDataWriteOnOffCommand(oGLDataWriteOnOffInfo, recCategoryID, recCategoryTypeID);

                if (isConnectionNull)
                {
                    con = this.CreateConnection();
                    con.Open();
                }
                else
                {
                    cmd.Transaction = oTransaction;
                }

                cmd.Connection = con;

                IDbDataParameter parID = cmd.CreateParameter();
                parID.ParameterName = "@ID";
                parID.Direction = ParameterDirection.Output;
                parID.Size = 8;
                cmd.Parameters.Add(parID);

                cmd.ExecuteNonQuery();

                oGLDataWriteOnOffInfo.GLDataWriteOnOffID = Convert.ToInt64(parID.Value);
            }
            finally
            {
                if (isConnectionNull && con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        private IDbCommand CreateInsertGLDataWriteOnOffCommand(GLDataWriteOnOffInfo entity, short? recCategoryID, short? recCategoryTypeID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataWriteOnOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAmount = cmd.CreateParameter();
            parAmount.ParameterName = "@Amount";
            if (!entity.IsAmountNull)
                parAmount.Value = entity.Amount;
            else
                parAmount.Value = System.DBNull.Value;
            cmdParams.Add(parAmount);

            System.Data.IDbDataParameter parAmountInBaseCurrency = cmd.CreateParameter();
            parAmountInBaseCurrency.ParameterName = "@AmountBaseCurrency";
            if (entity.AmountBaseCurrency.HasValue)
                parAmountInBaseCurrency.Value = entity.AmountBaseCurrency.Value;
            else
                parAmountInBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInBaseCurrency);

            System.Data.IDbDataParameter parAmountInReportingCurrency = cmd.CreateParameter();
            parAmountInReportingCurrency.ParameterName = "@AmountReportingCurrency";
            if (entity.AmountReportingCurrency.HasValue)
                parAmountInReportingCurrency.Value = entity.AmountReportingCurrency.Value;
            else
                parAmountInReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInReportingCurrency);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parJournalEntryRef = cmd.CreateParameter();
            parJournalEntryRef.ParameterName = "@JournalEntryRef";
            if (!entity.IsJournalEntryRefNull)
                parJournalEntryRef.Value = entity.JournalEntryRef;
            else
                parJournalEntryRef.Value = System.DBNull.Value;
            cmdParams.Add(parJournalEntryRef);

            System.Data.IDbDataParameter parLocalCurrency = cmd.CreateParameter();
            parLocalCurrency.ParameterName = "@LocalCurrencyCode";
            if (!entity.IsLocalCurrencyCodeNull)
                parLocalCurrency.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrency);


            System.Data.IDbDataParameter parCloseComments = cmd.CreateParameter();
            parCloseComments.ParameterName = "@CloseComments";
            if (!entity.IsCloseCommentsNull)
                parCloseComments.Value = entity.CloseComments;
            else
                parCloseComments.Value = System.DBNull.Value;
            cmdParams.Add(parCloseComments);

            System.Data.IDbDataParameter parCloseDate = cmd.CreateParameter();
            parCloseDate.ParameterName = "@CloseDate";
            if (!entity.IsCloseDateNull)
                parCloseDate.Value = entity.CloseDate.Value;
            else
                parCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parCloseDate);

            System.Data.IDbDataParameter parTransactionDate = cmd.CreateParameter();
            parTransactionDate.ParameterName = "@OpenDate";
            if (entity.OpenDate.HasValue)
                parTransactionDate.Value = entity.OpenDate.Value;
            else
                parTransactionDate.Value = System.DBNull.Value;
            cmdParams.Add(parTransactionDate);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!string.IsNullOrEmpty(entity.AddedBy))
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@AddedByUserID";
            if (!entity.IsAddedByUserIDNull)
                parUserID.Value = entity.AddedByUserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity.DateAdded.HasValue)
                parDateAdded.Value = entity.DateAdded.Value;
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDebitCredit = cmd.CreateParameter();
            parDebitCredit.ParameterName = "@WriteOnOffID";
            if (!entity.IsWriteOnOffIDNull)
                parDebitCredit.Value = entity.WriteOnOffID;
            else
                parDebitCredit.Value = System.DBNull.Value;
            cmdParams.Add(parDebitCredit);

            System.Data.IDbDataParameter parRecCategoryTypeID = cmd.CreateParameter();
            parRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            if (recCategoryTypeID.HasValue && recCategoryTypeID.Value > 0)
                parRecCategoryTypeID.Value = recCategoryTypeID.Value;
            else
                parRecCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryTypeID);

            System.Data.IDbDataParameter parRecCategoryID = cmd.CreateParameter();
            parRecCategoryID.ParameterName = "@RecCategoryID";
            if (recCategoryID.HasValue && recCategoryID.Value > 0)
                parRecCategoryID.Value = recCategoryID.Value;
            else
                parRecCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryID);

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

            //System.Data.IDbDataParameter parWriteOnOff = cmd.CreateParameter();
            //parWriteOnOff.ParameterName = "@WriteOnOff";
            //if(!entity.IsWriteOnOffNull)
            //    parWriteOnOff.Value = entity.WriteOnOff;
            //else
            //    parWriteOnOff.Value = System.DBNull.Value;
            //cmdParams.Add(parWriteOnOff);

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

        #region UpdateGLDataWriteOnOff

        public void UpdateGLDataWriteOnOff(GLDataWriteOnOffInfo oGLDataWriteOnOffInfo, short? recCategoryTypeID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateUpdateGLDataWriteOnOffCommand(oGLDataWriteOnOffInfo, recCategoryTypeID);
                con = this.CreateConnection();
                con.Open();
                cmd.Connection = con;

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        private IDbCommand CreateUpdateGLDataWriteOnOffCommand(GLDataWriteOnOffInfo entity, short? recCategoryTypeID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataWriteOnOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parGLDataWriteOffOnID = cmd.CreateParameter();
            parGLDataWriteOffOnID.ParameterName = "@GLDataWriteOffOnID";
            parGLDataWriteOffOnID.Value = entity.GLDataWriteOnOffID;
            cmdParams.Add(parGLDataWriteOffOnID);

            System.Data.IDbDataParameter parAmount = cmd.CreateParameter();
            parAmount.ParameterName = "@Amount";
            if (!entity.IsAmountNull)
                parAmount.Value = entity.Amount;
            else
                parAmount.Value = System.DBNull.Value;
            cmdParams.Add(parAmount);

            System.Data.IDbDataParameter parAmountInBaseCurrency = cmd.CreateParameter();
            parAmountInBaseCurrency.ParameterName = "@AmountBaseCurrency";
            if (entity.AmountBaseCurrency.HasValue)
                parAmountInBaseCurrency.Value = entity.AmountBaseCurrency;
            else
                parAmountInBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInBaseCurrency);

            System.Data.IDbDataParameter parAmountInReportingCurrency = cmd.CreateParameter();
            parAmountInReportingCurrency.ParameterName = "@AmountReportingCurrency";
            if (entity.AmountReportingCurrency.HasValue)
                parAmountInReportingCurrency.Value = entity.AmountReportingCurrency;
            else
                parAmountInReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInReportingCurrency);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parLocalCurrency = cmd.CreateParameter();
            parLocalCurrency.ParameterName = "@LocalCurrencyCode";
            if (!entity.IsLocalCurrencyCodeNull)
                parLocalCurrency.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrency);

            System.Data.IDbDataParameter parTransactionDate = cmd.CreateParameter();
            parTransactionDate.ParameterName = "@OpenDate";
            if (entity.OpenDate.HasValue)
                parTransactionDate.Value = entity.OpenDate.Value;
            else
                parTransactionDate.Value = System.DBNull.Value;
            cmdParams.Add(parTransactionDate);

            System.Data.IDbDataParameter parDebitCredit = cmd.CreateParameter();
            parDebitCredit.ParameterName = "@WriteOnOffID";
            if (!entity.IsWriteOnOffIDNull)
                parDebitCredit.Value = entity.WriteOnOffID;
            else
                parDebitCredit.Value = System.DBNull.Value;
            cmdParams.Add(parDebitCredit);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@RevisedBy";
            if (!string.IsNullOrEmpty(entity.RevisedBy))
                parUserID.Value = entity.RevisedBy;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parDateRevised.Value = entity.DateRevised.Value;
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parRecCategoryTypeID = cmd.CreateParameter();
            parRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            if (recCategoryTypeID.HasValue && recCategoryTypeID.Value > 0)
                parRecCategoryTypeID.Value = recCategoryTypeID.Value;
            else
                parRecCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryTypeID);

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

        public void Delete(object id, long? glDataID, short? recCategoryTypeID, string revisedBy, DateTime dateRevised, DataTable dtGLDataWO)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateDeleteOneCommand(id, glDataID, recCategoryTypeID, revisedBy, dateRevised, dtGLDataWO);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        protected System.Data.IDbCommand CreateDeleteOneCommand(object id, long? glDataID, short? recCategoryTypeID, string revisedBy, DateTime dateRevised, DataTable dtGLDataWO)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_GLDataWriteOnOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@tblGLRecItemIDs";
            par.Value = dtGLDataWO;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (glDataID.HasValue)
                parGLDataID.Value = glDataID.Value;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (recCategoryTypeID.HasValue)
                parReconciliationCategoryTypeID.Value = recCategoryTypeID.Value;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = revisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;
            cmdParams.Add(paramDateRevised);

            return cmd;

        }

        #endregion


        public List<GLDataWriteOnOffInfo> BulkInsertGLDataWriteOnOffRecItems(DataTable dtGLDataWriteOnOffRecItem, GLDataWriteOnOffInfo oGLDataWriteOnOffInfo, short recTemplateAttributeID, out int? rowsAffected, IDbConnection con, IDbTransaction oTransaction)
        {
            List<GLDataWriteOnOffInfo> oTempGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            IDbCommand cmd = null;
            IDataReader dr = null;
            try
            {

                cmd = GetBulkInsertRecInputItemCommand(dtGLDataWriteOnOffRecItem, oGLDataWriteOnOffInfo, recTemplateAttributeID);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                dr = cmd.ExecuteReader();
                GLDataWriteOnOffInfo objGLDataWriteOnOffInfo;
                while (dr.Read())
                {
                    objGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
                    objGLDataWriteOnOffInfo.IndexID = dr.GetInt16Value("IndexID");
                    objGLDataWriteOnOffInfo.RecItemNumber = dr.GetStringValue("RecItemNumber");
                    oTempGLDataWriteOnOffInfoCollection.Add(objGLDataWriteOnOffInfo);
                }
                rowsAffected = oTempGLDataWriteOnOffInfoCollection.Count;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }

            return oTempGLDataWriteOnOffInfoCollection;
        }


        private System.Data.IDbCommand GetBulkInsertRecInputItemCommand(DataTable dtGLDataWriteOnOffRecItem, GLDataWriteOnOffInfo entity, short recTemplateAttributeID)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_BulkInsertGLDataWriteOnOffRecItem");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamdtGLDataRecItemTable = cmd.CreateParameter();
            cmdParamdtGLDataRecItemTable.ParameterName = "@GLDataRecItem";
            cmdParamdtGLDataRecItemTable.Value = dtGLDataWriteOnOffRecItem;
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
            if (entity.ReconciliationCategoryID.HasValue)
                parReconciliationCategoryID.Value = entity.ReconciliationCategoryID.Value;
            else
                parReconciliationCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (entity.ReconciliationCategoryTypeID.HasValue)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID.Value;
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

        protected override GLDataWriteOnOffInfo MapObject(System.Data.IDataReader r)
        {
            GLDataWriteOnOffInfo entity = new GLDataWriteOnOffInfo();
            entity = base.MapObject(r);
            entity.MatchSetRefNumber = r.GetStringValue("MatchSetRef");
            entity.MatchSetID = r.GetInt64Value("MatchSetID");
            entity.MatchSetSubSetCombinationID = r.GetInt64Value("MatchSetSubSetCombinationID");
            entity.ExRateLCCYtoBCCY = r.GetDecimalValue("ExRateLCCYtoBCCY");
            entity.ExRateLCCYtoRCCY = r.GetDecimalValue("ExRateLCCYtoRCCY");

            return entity;
        }
        public List<GLDataWriteOnOffInfo> GetClosedGLDataWriteOnOffItemByMatching(long? GlDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID)
        {
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGeGetClosedGLDataWriteOnOffItemCommand(GlDataID, MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID, GLDataRecItemTypeID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = null;
                while (reader.Read())
                {
                    oGLDataWriteOnOffInfo = this.MapObject(reader);
                    oGLDataWriteOnOffInfoCollection.Add(oGLDataWriteOnOffInfo);
                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }
            return oGLDataWriteOnOffInfoCollection;
        }

        private IDbCommand CreateGeGetClosedGLDataWriteOnOffItemCommand(long? glDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].[usp_SEL_ClosedGLDataWriteOnOffByMatching]");
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
        public List<GLDataWriteOnOffInfo> CloseMatchingGLDataWriteOnOffItem(DataTable dtMatchSetGLDataRecItem, GLDataWriteOnOffInfo oGLDataWriteOnOffInfo, short recTemplateAttributeID, DateTime? CloseDate, out int? rowsAffected, IDbConnection con, IDbTransaction oTransaction)
        {
            List<GLDataWriteOnOffInfo> oTempGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            IDbCommand cmd = null;
            IDataReader dr = null;
            try
            {

                cmd = GetCloseMatchingGLDataWriteOnOffItemCommand(dtMatchSetGLDataRecItem, oGLDataWriteOnOffInfo, recTemplateAttributeID, CloseDate);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                dr = cmd.ExecuteReader();
                GLDataWriteOnOffInfo objGLDataWriteOnOffInfo;
                while (dr.Read())
                {
                    objGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
                    objGLDataWriteOnOffInfo.GLDataWriteOnOffID = dr.GetInt64Value("GLDataRecItemID");
                    objGLDataWriteOnOffInfo.CloseDate = dr.GetDateValue("CloseDate");
                    oTempGLDataWriteOnOffInfoCollection.Add(objGLDataWriteOnOffInfo);
                }
                rowsAffected = oTempGLDataWriteOnOffInfoCollection.Count;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }

            return oTempGLDataWriteOnOffInfoCollection;
        }

        private System.Data.IDbCommand GetCloseMatchingGLDataWriteOnOffItemCommand(DataTable dtMatchSetGLDataRecItem, GLDataWriteOnOffInfo entity, short recTemplateAttributeID, DateTime? CloseDate)
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
            if (entity.ReconciliationCategoryID.HasValue)
                parReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
            else
                parReconciliationCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (entity.ReconciliationCategoryTypeID.HasValue)
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


    }
}