

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

    public abstract class GLDataUnexplainedVarianceDAOBase : CustomAbstractDAO<GLDataUnexplainedVarianceInfo>
    {

        /// <summary>
        /// A static representation of column Amount
        /// </summary>
        public static readonly string COLUMN_AMOUNT = "Amount";
        /// <summary>
        /// A static representation of column CommentDate
        /// </summary>
        public static readonly string COLUMN_COMMENTDATE = "CommentDate";
        /// <summary>
        /// A static representation of column Comments
        /// </summary>
        public static readonly string COLUMN_COMMENTS = "Comments";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column GLDataUnexplainedVarianceID
        /// </summary>
        public static readonly string COLUMN_GLDATAUNEXPLAINEDVARIANCEID = "GLDataUnexplainedVarianceID";
        /// <summary>
        /// A static representation of column LocalCurrency
        /// </summary>
        public static readonly string COLUMN_LOCALCURRENCY = "LocalCurrency";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataUnexplainedVarianceID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataUnexplainedVarianceID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataUnexplainedVariance";

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
        public GLDataUnexplainedVarianceDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataUnexplainedVariance", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataUnexplainedVarianceInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataUnexplainedVarianceInfo</returns>
        protected override GLDataUnexplainedVarianceInfo MapObject(System.Data.IDataReader r)
        {

            GLDataUnexplainedVarianceInfo entity = new GLDataUnexplainedVarianceInfo();

            try
            {
                int ordinal = r.GetOrdinal("GLDataUnexplainedVarianceID");
                if (!r.IsDBNull(ordinal)) entity.GLDataUnexplainedVarianceID = ((System.Int64)(r.GetValue(ordinal)));
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

            //try{
            //    int ordinal = r.GetOrdinal("LocalCurrency");
            //    if (!r.IsDBNull(ordinal)) entity.LocalCurrency = ((System.String)(r.GetValue(ordinal)));
            //}
            //catch(Exception){}

            try
            {
                int ordinal = r.GetOrdinal("Comments");
                if (!r.IsDBNull(ordinal)) entity.Comments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CommentDate");
                if (!r.IsDBNull(ordinal)) entity.CommentDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");

            entity.AddedByUserInfo = new UserHdrInfo();
            try
            {
                int ordinal = r.GetOrdinal("AddedByFirstName");
                if (!r.IsDBNull(ordinal)) entity.AddedByUserInfo.FirstName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AddedByLastName");
                if (!r.IsDBNull(ordinal)) entity.AddedByUserInfo.LastName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PeriodEndDate");
                if (!r.IsDBNull(ordinal)) entity.PeriodEndDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportingCurrencyCode");
                if (!r.IsDBNull(ordinal)) entity.ReportingCurrencyCode = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataUnexplainedVarianceInfo object
        /// </summary>
        /// <param name="o">A GLDataUnexplainedVarianceInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataUnexplainedVarianceInfo entity)
        {


            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GLDataUnexplainedVariance");
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataUnexplainedVariance");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


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

            //System.Data.IDbDataParameter parCommentDate = cmd.CreateParameter();
            //parCommentDate.ParameterName = "@CommentDate";
            //if(!entity.IsCommentDateNull)
            //    parCommentDate.Value = entity.CommentDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parCommentDate.Value = System.DBNull.Value;
            //cmdParams.Add(parCommentDate);


            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
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

            System.Data.IDbDataParameter parRecCategoryTypeID = cmd.CreateParameter();
            parRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            if (!entity.IsGLDataIDNull)
                parRecCategoryTypeID.Value = entity.RecCategoryTypeID;
            else
                parRecCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryTypeID);

            System.Data.IDbDataParameter parRecCategoryID = cmd.CreateParameter();
            parRecCategoryID.ParameterName = "@RecCategoryID";
            if (!entity.IsGLDataIDNull)
                parRecCategoryID.Value = entity.RecCategoryID;
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
            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataUnexplainedVarianceInfo object
        /// </summary>
        /// <param name="o">A GLDataUnexplainedVarianceInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataUnexplainedVarianceInfo entity)
        {

            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataUnexplainedVariance");
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataUnexplainedVariance");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

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


            //System.Data.IDbDataParameter parCommentDate = cmd.CreateParameter();
            //parCommentDate.ParameterName = "@CommentDate";
            //if(!entity.IsCommentDateNull)
            //    parCommentDate.Value = entity.CommentDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parCommentDate.Value = System.DBNull.Value;
            //cmdParams.Add(parCommentDate);


            //System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            //parGLDataID.ParameterName = "@GLDataID";
            //if(!entity.IsGLDataIDNull)
            //    parGLDataID.Value = entity.GLDataID;
            //else
            //    parGLDataID.Value = System.DBNull.Value;
            //cmdParams.Add(parGLDataID);

            //System.Data.IDbDataParameter parLocalCurrency = cmd.CreateParameter();
            //parLocalCurrency.ParameterName = "@LocalCurrency";
            //if(!entity.IsLocalCurrencyNull)
            //    parLocalCurrency.Value = entity.LocalCurrency;
            //else
            //    parLocalCurrency.Value = System.DBNull.Value;
            //cmdParams.Add(parLocalCurrency);

            System.Data.IDbDataParameter pkparGLDataUnexplainedVarianceID = cmd.CreateParameter();
            pkparGLDataUnexplainedVarianceID.ParameterName = "@GLDataUnexplainedVarianceID";
            pkparGLDataUnexplainedVarianceID.Value = entity.GLDataUnexplainedVarianceID;
            cmdParams.Add(pkparGLDataUnexplainedVarianceID);

            System.Data.IDbDataParameter parRecCategoryTypeID = cmd.CreateParameter();
            parRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            if (!entity.IsGLDataIDNull)
                parRecCategoryTypeID.Value = entity.RecCategoryTypeID;
            else
                parRecCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryTypeID);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = entity.AddedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = entity.DateAdded;
            cmdParams.Add(paramDateRevised);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_GLDataUnexplainedVariance");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataUnexplainedVarianceID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataUnexplainedVariance");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataUnexplainedVarianceID";
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
        public IList<GLDataUnexplainedVarianceInfo> SelectAllByGLDataID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataUnexplainedVarianceByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataUnexplainedVarianceInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataUnexplainedVarianceDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataUnexplainedVarianceInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataUnexplainedVarianceDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataUnexplainedVarianceInfo entity, object id)
        {
            entity.GLDataUnexplainedVarianceID = Convert.ToInt64(id);
        }




    }
}
