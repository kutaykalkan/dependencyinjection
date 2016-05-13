

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

    public abstract class GLDataRecItemDAOBase : CustomAbstractDAO<GLDataRecItemInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column Amount
        /// </summary>
        public static readonly string COLUMN_AMOUNT = "Amount";
        /// <summary>
        /// A static representation of column AmountInBaseCurrency
        /// </summary>
        public static readonly string COLUMN_AMOUNTINBASECURRENCY = "AmountInBaseCurrency";
        /// <summary>
        /// A static representation of column AmountInReportingCurrency
        /// </summary>
        public static readonly string COLUMN_AMOUNTINREPORTINGCURRENCY = "AmountInReportingCurrency";
        /// <summary>
        /// A static representation of column Comments
        /// </summary>
        public static readonly string COLUMN_COMMENTS = "Comments";
        /// <summary>
        /// A static representation of column DataImportID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column ExchangeRateBaseCurrency
        /// </summary>
        public static readonly string COLUMN_EXCHANGERATEBASECURRENCY = "ExchangeRateBaseCurrency";
        /// <summary>
        /// A static representation of column ExchangeRateReportingCurrency
        /// </summary>
        public static readonly string COLUMN_EXCHANGERATEREPORTINGCURRENCY = "ExchangeRateReportingCurrency";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column GLReconciliationItemInputID
        /// </summary>
        public static readonly string COLUMN_GLRECONCILIATIONITEMINPUTID = "GLReconciliationItemInputID";
        /// <summary>
        /// A static representation of column IsAttachmentAvailable
        /// </summary>
        public static readonly string COLUMN_ISATTACHMENTAVAILABLE = "IsAttachmentAvailable";
        /// <summary>
        /// A static representation of column JournalEntryRef
        /// </summary>
        public static readonly string COLUMN_JOURNALENTRYREF = "JournalEntryRef";
        /// <summary>
        /// A static representation of column LocalCurrency
        /// </summary>
        public static readonly string COLUMN_LOCALCURRENCY = "LocalCurrency";
        /// <summary>
        /// A static representation of column OpenDate
        /// </summary>
        public static readonly string COLUMN_OPENDATE = "OpenDate";
        /// <summary>
        /// A static representation of column ReconciliationCategoryID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYID = "ReconciliationCategoryID";
        /// <summary>
        /// A static representation of column ReconciliationCategoryTypeID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYTYPEID = "ReconciliationCategoryTypeID";
        /// <summary>
        /// A static representation of column ResolutionComments
        /// </summary>
        public static readonly string COLUMN_RESOLUTIONCOMMENTS = "ResolutionComments";
        /// <summary>
        /// A static representation of column ResolutionDate
        /// </summary>
        public static readonly string COLUMN_RESOLUTIONDATE = "ResolutionDate";
        /// <summary>
        /// Provides access to the name of the primary key column (GLReconciliationItemInputID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLReconciliationItemInputID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLReconciliationItemInput";

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
        public GLDataRecItemDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLReconciliationItemInput", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLReconciliationItemInputInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLReconciliationItemInputInfo</returns>
        protected override GLDataRecItemInfo MapObject(System.Data.IDataReader r)
        {

            GLDataRecItemInfo entity = new GLDataRecItemInfo();

            entity.GLDataRecItemID = r.GetInt64Value("GLDataRecItemID");
            entity.GLDataID = r.GetInt64Value("GLDataID");
            entity.ReconciliationCategoryID = r.GetInt16Value("ReconciliationCategoryID");
            entity.ReconciliationCategoryTypeID = r.GetInt16Value("ReconciliationCategoryTypeID");
            entity.DataImportID = r.GetInt32Value("DataImportID");
            entity.IsAttachmentAvailable = r.GetBooleanValue("IsAttachmentAvailable");
            entity.Amount = r.GetDecimalValue("Amount");
            entity.LocalCurrencyCode = r.GetStringValue("LocalCurrencyCode");
            entity.AmountBaseCurrency = r.GetDecimalValue("AmountBaseCurrency");
            entity.AmountReportingCurrency = r.GetDecimalValue("AmountReportingCurrency");
            entity.OpenDate = r.GetDateValue("OpenDate");
            entity.CloseDate = r.GetDateValue("CloseDate");
            entity.JournalEntryRef = r.GetStringValue("JournalEntryRef");
            entity.Comments = r.GetStringValue("Comments");
            entity.CloseComments = r.GetStringValue("CloseComments");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.RecItemNumber = r.GetStringValue("RecItemNumber");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLReconciliationItemInputInfo object
        /// </summary>
        /// <param name="o">A GLReconciliationItemInputInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataRecItemInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAmount = cmd.CreateParameter();
            parAmount.ParameterName = "@Amount";
            if (!entity.IsAmountNull)
                parAmount.Value = entity.Amount;
            else
                parAmount.Value = System.DBNull.Value;
            cmdParams.Add(parAmount);

            System.Data.IDbDataParameter parAmountInBaseCurrency = cmd.CreateParameter();
            parAmountInBaseCurrency.ParameterName = "@AmountInBaseCurrency";
            if (!entity.IsAmountBaseCurrencyNull)
                parAmountInBaseCurrency.Value = entity.AmountBaseCurrency;
            else
                parAmountInBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInBaseCurrency);

            System.Data.IDbDataParameter parAmountInReportingCurrency = cmd.CreateParameter();
            parAmountInReportingCurrency.ParameterName = "@AmountInReportingCurrency";
            if (!entity.IsAmountReportingCurrencyNull)
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

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (!entity.IsDataImportIDNull)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);


            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parIsAttachmentAvailable = cmd.CreateParameter();
            parIsAttachmentAvailable.ParameterName = "@IsAttachmentAvailable";
            if (!entity.IsIsAttachmentAvailableNull)
                parIsAttachmentAvailable.Value = entity.IsAttachmentAvailable;
            else
                parIsAttachmentAvailable.Value = System.DBNull.Value;
            cmdParams.Add(parIsAttachmentAvailable);

            System.Data.IDbDataParameter parJournalEntryRef = cmd.CreateParameter();
            parJournalEntryRef.ParameterName = "@JournalEntryRef";
            if (!entity.IsJournalEntryRefNull)
                parJournalEntryRef.Value = entity.JournalEntryRef;
            else
                parJournalEntryRef.Value = System.DBNull.Value;
            cmdParams.Add(parJournalEntryRef);

            System.Data.IDbDataParameter parLocalCurrency = cmd.CreateParameter();
            parLocalCurrency.ParameterName = "@LocalCurrency";
            if (!entity.IsLocalCurrencyCodeNull)
                parLocalCurrency.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrency);

            System.Data.IDbDataParameter parOpenDate = cmd.CreateParameter();
            parOpenDate.ParameterName = "@OpenDate";
            if (!entity.IsOpenDateNull)
                parOpenDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parOpenDate.Value = System.DBNull.Value;
            cmdParams.Add(parOpenDate);

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

            System.Data.IDbDataParameter parResolutionComments = cmd.CreateParameter();
            parResolutionComments.ParameterName = "@ResolutionComments";
            if (!entity.IsCloseCommentsNull)
                parResolutionComments.Value = entity.CloseComments;
            else
                parResolutionComments.Value = System.DBNull.Value;
            cmdParams.Add(parResolutionComments);

            System.Data.IDbDataParameter parResolutionDate = cmd.CreateParameter();
            parResolutionDate.ParameterName = "@ResolutionDate";
            if (!entity.IsCloseDateNull)
                parResolutionDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parResolutionDate.Value = System.DBNull.Value;
            cmdParams.Add(parResolutionDate);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            parAddedByUserID.Value = entity.AddedByUserID.Value;
            cmdParams.Add(parAddedByUserID);



            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLReconciliationItemInputInfo object
        /// </summary>
        /// <param name="o">A GLReconciliationItemInputInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataRecItemInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLReconciliationItemInput");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAmount = cmd.CreateParameter();
            parAmount.ParameterName = "@Amount";
            if (!entity.IsAmountNull)
                parAmount.Value = entity.Amount;
            else
                parAmount.Value = System.DBNull.Value;
            cmdParams.Add(parAmount);

            System.Data.IDbDataParameter parAmountInBaseCurrency = cmd.CreateParameter();
            parAmountInBaseCurrency.ParameterName = "@AmountInBaseCurrency";
            if (!entity.IsAmountBaseCurrencyNull)
                parAmountInBaseCurrency.Value = entity.AmountBaseCurrency;
            else
                parAmountInBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parAmountInBaseCurrency);

            System.Data.IDbDataParameter parAmountInReportingCurrency = cmd.CreateParameter();
            parAmountInReportingCurrency.ParameterName = "@AmountInReportingCurrency";
            if (!entity.IsAmountReportingCurrencyNull)
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

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (!entity.IsDataImportIDNull)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);


            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parIsAttachmentAvailable = cmd.CreateParameter();
            parIsAttachmentAvailable.ParameterName = "@IsAttachmentAvailable";
            if (!entity.IsIsAttachmentAvailableNull)
                parIsAttachmentAvailable.Value = entity.IsAttachmentAvailable;
            else
                parIsAttachmentAvailable.Value = System.DBNull.Value;
            cmdParams.Add(parIsAttachmentAvailable);

            System.Data.IDbDataParameter parJournalEntryRef = cmd.CreateParameter();
            parJournalEntryRef.ParameterName = "@JournalEntryRef";
            if (!entity.IsJournalEntryRefNull)
                parJournalEntryRef.Value = entity.JournalEntryRef;
            else
                parJournalEntryRef.Value = System.DBNull.Value;
            cmdParams.Add(parJournalEntryRef);

            System.Data.IDbDataParameter parLocalCurrency = cmd.CreateParameter();
            parLocalCurrency.ParameterName = "@LocalCurrency";
            if (!entity.IsLocalCurrencyCodeNull)
                parLocalCurrency.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrency);

            System.Data.IDbDataParameter parOpenDate = cmd.CreateParameter();
            parOpenDate.ParameterName = "@OpenDate";
            if (!entity.IsOpenDateNull)
                parOpenDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parOpenDate.Value = System.DBNull.Value;
            cmdParams.Add(parOpenDate);

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

            System.Data.IDbDataParameter parResolutionComments = cmd.CreateParameter();
            parResolutionComments.ParameterName = "@ResolutionComments";
            if (!entity.IsCloseCommentsNull)
                parResolutionComments.Value = entity.CloseComments;
            else
                parResolutionComments.Value = System.DBNull.Value;
            cmdParams.Add(parResolutionComments);

            System.Data.IDbDataParameter parResolutionDate = cmd.CreateParameter();
            parResolutionDate.ParameterName = "@ResolutionDate";
            if (!entity.IsCloseDateNull)
                parResolutionDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parResolutionDate.Value = System.DBNull.Value;
            cmdParams.Add(parResolutionDate);

            System.Data.IDbDataParameter pkparGLReconciliationItemInputID = cmd.CreateParameter();
            pkparGLReconciliationItemInputID.ParameterName = "@GLReconciliationItemInputID";
            pkparGLReconciliationItemInputID.Value = entity.GLDataRecItemID;
            cmdParams.Add(pkparGLReconciliationItemInputID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLReconciliationItemInput");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLReconciliationItemInputID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLReconciliationItemInput");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLReconciliationItemInputID";
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
        public IList<GLDataRecItemInfo> SelectAllByGLDataID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLReconciliationItemInputByGLDataID");
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
        public IList<GLDataRecItemInfo> SelectAllByReconciliationCategoryID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLReconciliationItemInputByReconciliationCategoryID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryID";
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
        public IList<GLDataRecItemInfo> SelectAllByReconciliationCategoryTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLReconciliationItemInputByReconciliationCategoryTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryTypeID";
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
        public IList<GLDataRecItemInfo> SelectAllByDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLReconciliationItemInputByDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataRecItemInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRecItemDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataRecItemInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRecItemDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataRecItemInfo entity, object id)
        {
            entity.GLDataRecItemID = Convert.ToInt64(id);
        }





    }
}
