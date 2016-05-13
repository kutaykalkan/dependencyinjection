

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class GLDataWriteOnOffDAOBase : CustomAbstractDAO<GLDataWriteOnOffInfo>
    {

        /// <summary>
        /// A static representation of column Amount
        /// </summary>
        public static readonly string COLUMN_AMOUNT = "Amount";
        /// <summary>
        /// A static representation of column Comments
        /// </summary>
        public static readonly string COLUMN_COMMENTS = "Comments";
        /// <summary>
        /// A static representation of column Date
        /// </summary>
        public static readonly string COLUMN_DATE = "Date";
        /// <summary>
        /// A static representation of column DebitCredit
        /// </summary>
        public static readonly string COLUMN_DEBITCREDIT = "DebitCredit";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column GLDataWriteOnOffID
        /// </summary>
        public static readonly string COLUMN_GLDATAWRITEONOFFID = "GLDataWriteOnOffID";
        /// <summary>
        /// A static representation of column JournalEntryRef
        /// </summary>
        public static readonly string COLUMN_JOURNALENTRYREF = "JournalEntryRef";
        /// <summary>
        /// A static representation of column LocalCurrency
        /// </summary>
        public static readonly string COLUMN_LOCALCURRENCY = "LocalCurrency";
        /// <summary>
        /// A static representation of column ProposedEntryAccountRef
        /// </summary>
        public static readonly string COLUMN_PROPOSEDENTRYACCOUNTREF = "ProposedEntryAccountRef";
        /// <summary>
        /// A static representation of column CloseComments
        /// </summary>
        public static readonly string COLUMN_CloseCOMMENTS = "CloseComments";
        /// <summary>
        /// A static representation of column ResolutionDate
        /// </summary>
        public static readonly string COLUMN_RESOLUTIONDATE = "ResolutionDate";
        /// <summary>
        /// A static representation of column TransactionDate
        /// </summary>
        public static readonly string COLUMN_TRANSACTIONDATE = "TransactionDate";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// A static representation of column WriteOnOff
        /// </summary>
        public static readonly string COLUMN_WRITEONOFF = "WriteOnOff";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataWriteOnOffID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataWriteOnOffID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataWriteOnOff";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";
        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataWriteOnOffDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataWriteOnOff", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataWriteOnOffInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataWriteOnOffInfo</returns>
        protected override GLDataWriteOnOffInfo MapObject(System.Data.IDataReader r)
        {

            GLDataWriteOnOffInfo entity = new GLDataWriteOnOffInfo();


            try
            {
                int ordinal = r.GetOrdinal("GLDataWriteOnOffID");
                if (!r.IsDBNull(ordinal)) entity.GLDataWriteOnOffID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("GLDataID");
                if (!r.IsDBNull(ordinal)) entity.GLDataID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Amount");
                if (!r.IsDBNull(ordinal)) entity.Amount = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AmountBaseCurrency");
                if (!r.IsDBNull(ordinal)) entity.AmountBaseCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AmountReportingCurrency");
                if (!r.IsDBNull(ordinal)) entity.AmountReportingCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("LocalCurrencyCode");
                if (!r.IsDBNull(ordinal)) entity.LocalCurrencyCode = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            //try{
            //    int ordinal = r.GetOrdinal("WriteOnOff");
            //    if (!r.IsDBNull(ordinal)) entity.WriteOnOff = ((System.Decimal)(r.GetValue(ordinal)));
            //}
            //catch(Exception){}

            try
            {
                int ordinal = r.GetOrdinal("OpenDate");
                if (!r.IsDBNull(ordinal)) entity.OpenDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("JournalEntryRef");
                if (!r.IsDBNull(ordinal)) entity.JournalEntryRef = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CloseDate");
                if (!r.IsDBNull(ordinal)) entity.CloseDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CloseComments");
                if (!r.IsDBNull(ordinal)) entity.CloseComments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            //try{
            //    int ordinal = r.GetOrdinal("ProposedEntryAccountRef");
            //    if (!r.IsDBNull(ordinal)) entity.ProposedEntryAccountRef = ((System.Int32)(r.GetValue(ordinal)));
            //}
            //catch(Exception){}

            try
            {
                int ordinal = r.GetOrdinal("WriteOnOffID");
                if (!r.IsDBNull(ordinal)) entity.WriteOnOffID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch
            {
            }

            //try{
            //    int ordinal = r.GetOrdinal("Date");
            //    if (!r.IsDBNull(ordinal)) entity.Date = ((System.DateTime)(r.GetValue(ordinal)));
            //}
            //catch(Exception){}

            try
            {
                int ordinal = r.GetOrdinal("AddedByUserID");
                if (!r.IsDBNull(ordinal)) entity.AddedByUserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateAdded");
                if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AddedBy");
                if (!r.IsDBNull(ordinal)) entity.AddedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Comments");
                if (!r.IsDBNull(ordinal)) entity.Comments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsForwardedItem");
                if (!r.IsDBNull(ordinal)) entity.IsForwardedItem = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserName");
                if (!r.IsDBNull(ordinal)) entity.UserName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RecItemNumber");
                if (!r.IsDBNull(ordinal)) entity.RecItemNumber = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataWriteOnOffInfo object
        /// </summary>
        /// <param name="o">A GLDataWriteOnOffInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataWriteOnOffInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GLDataWriteOnOff");
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
            if (!entity.IsAmountNull)
                parAmountInBaseCurrency.Value = entity.AmountBaseCurrency;
            else
                parAmountInBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInBaseCurrency);

            System.Data.IDbDataParameter parAmountInReportingCurrency = cmd.CreateParameter();
            parAmountInReportingCurrency.ParameterName = "@AmountReportingCurrency";
            if (!entity.IsAmountNull)
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

            //System.Data.IDbDataParameter parDate = cmd.CreateParameter();
            //parDate.ParameterName = "@Date";
            //if(!entity.IsDateNull)
            //    parDate.Value = entity.Date.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parDate.Value = System.DBNull.Value;
            //cmdParams.Add(parDate);

            System.Data.IDbDataParameter parDebitCredit = cmd.CreateParameter();
            parDebitCredit.ParameterName = "@WriteOnOffID";
            if (!entity.IsWriteOnOffIDNull)
                parDebitCredit.Value = entity.WriteOnOffID;
            else
                parDebitCredit.Value = System.DBNull.Value;
            cmdParams.Add(parDebitCredit);

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

            //System.Data.IDbDataParameter parProposedEntryAccountRef = cmd.CreateParameter();
            //parProposedEntryAccountRef.ParameterName = "@ProposedEntryAccountRef";
            //if(!entity.IsProposedEntryAccountRefNull)
            //    parProposedEntryAccountRef.Value = entity.ProposedEntryAccountRef;
            //else
            //    parProposedEntryAccountRef.Value = System.DBNull.Value;
            //cmdParams.Add(parProposedEntryAccountRef);

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
                parCloseDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parCloseDate);

            System.Data.IDbDataParameter parTransactionDate = cmd.CreateParameter();
            parTransactionDate.ParameterName = "@OpenDate";
            if (!entity.IsTransactionDateNull)
                parTransactionDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parTransactionDate.Value = System.DBNull.Value;
            cmdParams.Add(parTransactionDate);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@AddedByUserID";
            if (!entity.IsAddedByUserIDNull)
                parUserID.Value = entity.AddedByUserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            //System.Data.IDbDataParameter parWriteOnOff = cmd.CreateParameter();
            //parWriteOnOff.ParameterName = "@WriteOnOff";
            //if(!entity.IsWriteOnOffNull)
            //    parWriteOnOff.Value = entity.WriteOnOff;
            //else
            //    parWriteOnOff.Value = System.DBNull.Value;
            //cmdParams.Add(parWriteOnOff);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataWriteOnOffInfo object
        /// </summary>
        /// <param name="o">A GLDataWriteOnOffInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataWriteOnOffInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataWriteOnOff");

            //THis sp updates if record present other wise Inserts new one based on 
            // if GLDataWriteOnOffID == null or not
            //System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataWriteOnOff");
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
            if (!entity.IsAmountNull)
                parAmountInBaseCurrency.Value = entity.AmountBaseCurrency;
            else
                parAmountInBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInBaseCurrency);

            System.Data.IDbDataParameter parAmountInReportingCurrency = cmd.CreateParameter();
            parAmountInReportingCurrency.ParameterName = "@AmountReportingCurrency";
            if (!entity.IsAmountNull)
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

            //System.Data.IDbDataParameter parDate = cmd.CreateParameter();
            //parDate.ParameterName = "@Date";
            //if(!entity.IsDateNull)
            //    parDate.Value = entity.Date.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parDate.Value = System.DBNull.Value;
            //cmdParams.Add(parDate);

            System.Data.IDbDataParameter parDebitCredit = cmd.CreateParameter();
            parDebitCredit.ParameterName = "@WriteOnOffID";
            if (!entity.IsWriteOnOffIDNull)
                parDebitCredit.Value = entity.WriteOnOffID;
            else
                parDebitCredit.Value = System.DBNull.Value;
            cmdParams.Add(parDebitCredit);

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

            //System.Data.IDbDataParameter parProposedEntryAccountRef = cmd.CreateParameter();
            //parProposedEntryAccountRef.ParameterName = "@ProposedEntryAccountRef";
            //if(!entity.IsProposedEntryAccountRefNull)
            //    parProposedEntryAccountRef.Value = entity.ProposedEntryAccountRef;
            //else
            //    parProposedEntryAccountRef.Value = System.DBNull.Value;
            //cmdParams.Add(parProposedEntryAccountRef);

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
                parCloseDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parCloseDate);

            System.Data.IDbDataParameter parTransactionDate = cmd.CreateParameter();
            parTransactionDate.ParameterName = "@OpenDate";
            if (!entity.IsTransactionDateNull)
                parTransactionDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parTransactionDate.Value = System.DBNull.Value;
            cmdParams.Add(parTransactionDate);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@AddedByUserID";
            if (!entity.IsAddedByUserIDNull)
                parUserID.Value = entity.AddedByUserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            //System.Data.IDbDataParameter parWriteOnOff = cmd.CreateParameter();
            //parWriteOnOff.ParameterName = "@WriteOnOff";
            //if(!entity.IsWriteOnOffNull)
            //    parWriteOnOff.Value = entity.WriteOnOff;
            //else
            //    parWriteOnOff.Value = System.DBNull.Value;
            //cmdParams.Add(parWriteOnOff);

            System.Data.IDbDataParameter pkparGLDataWriteOnOffID = cmd.CreateParameter();
            pkparGLDataWriteOnOffID.ParameterName = "@GLDataWriteOnOffID";
            pkparGLDataWriteOnOffID.Value = entity.GLDataWriteOnOffID;
            cmdParams.Add(pkparGLDataWriteOnOffID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_GLDataWriteOnOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataWriteOnOffID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }


        /// <summary>
        /// Creates the sql select command, using the passed in primary key
        /// </summary>
        /// <param name="o">The primary key of the object to select</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateSelectOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataWriteOnOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataWriteOnOffID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<GLDataWriteOnOffInfo> SelectAllByGLDataID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataWriteOnOffByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<GLDataWriteOnOffInfo> SelectAllByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataWriteOnOffByUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataWriteOnOffInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataWriteOnOffDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataWriteOnOffInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataWriteOnOffDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);
        }

        private void MapIdentity(GLDataWriteOnOffInfo entity, object id)
        {
            entity.GLDataWriteOnOffID = Convert.ToInt32(id);
        }




    }
}
