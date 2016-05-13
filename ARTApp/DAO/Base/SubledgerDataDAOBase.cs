

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

    public abstract class SubledgerDataDAOBase : CustomAbstractDAO<SubledgerDataInfo>
    {

        /// <summary>
        /// A static representation of column AccountID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTID = "AccountID";
        /// <summary>
        /// A static representation of column BaseCCY
        /// </summary>
        public static readonly string COLUMN_BASECCY = "BaseCCY";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column ReportingCCY
        /// </summary>
        public static readonly string COLUMN_REPORTINGCCY = "ReportingCCY";
        /// <summary>
        /// A static representation of column SubledgerBalanceBaseCCY
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERBALANCEBASECCY = "SubledgerBalanceBaseCCY";
        /// <summary>
        /// A static representation of column SubledgerBalanceReportingCCY
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERBALANCEREPORTINGCCY = "SubledgerBalanceReportingCCY";
        /// <summary>
        /// A static representation of column SubledgerDataID
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERDATAID = "SubledgerDataID";
        /// <summary>
        /// Provides access to the name of the primary key column (SubledgerDataID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "SubledgerDataID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "SubledgerData";

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
        public SubledgerDataDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "SubledgerData", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a SubledgerDataInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>SubledgerDataInfo</returns>
        protected override SubledgerDataInfo MapObject(System.Data.IDataReader r)
        {

            SubledgerDataInfo entity = new SubledgerDataInfo();


            try
            {
                int ordinal = r.GetOrdinal("SubledgerDataID");
                if (!r.IsDBNull(ordinal)) entity.SubledgerDataID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AccountID");
                if (!r.IsDBNull(ordinal)) entity.AccountID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SubledgerBalanceBaseCCY");
                if (!r.IsDBNull(ordinal)) entity.SubledgerBalanceBaseCCY = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("BaseCCY");
                if (!r.IsDBNull(ordinal)) entity.BaseCCY = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SubledgerBalanceReportingCCY");
                if (!r.IsDBNull(ordinal)) entity.SubledgerBalanceReportingCCY = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportingCCY");
                if (!r.IsDBNull(ordinal)) entity.ReportingCCY = ((System.String)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("RecPeriodID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("DataImportID");
                if (!r.IsDBNull(ordinal)) entity.DataImportID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PhysicalPath");
                if (!r.IsDBNull(ordinal)) entity.PhysicalPath = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }



            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in SubledgerDataInfo object
        /// </summary>
        /// <param name="o">A SubledgerDataInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(SubledgerDataInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_SubledgerData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (!entity.IsAccountIDNull)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parBaseCCY = cmd.CreateParameter();
            parBaseCCY.ParameterName = "@BaseCCY";
            if (!entity.IsBaseCCYNull)
                parBaseCCY.Value = entity.BaseCCY;
            else
                parBaseCCY.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCCY);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReportingCCY = cmd.CreateParameter();
            parReportingCCY.ParameterName = "@ReportingCCY";
            if (!entity.IsReportingCCYNull)
                parReportingCCY.Value = entity.ReportingCCY;
            else
                parReportingCCY.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCCY);

            System.Data.IDbDataParameter parSubledgerBalanceBaseCCY = cmd.CreateParameter();
            parSubledgerBalanceBaseCCY.ParameterName = "@SubledgerBalanceBaseCCY";
            if (!entity.IsSubledgerBalanceBaseCCYNull)
                parSubledgerBalanceBaseCCY.Value = entity.SubledgerBalanceBaseCCY;
            else
                parSubledgerBalanceBaseCCY.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerBalanceBaseCCY);

            System.Data.IDbDataParameter parSubledgerBalanceReportingCCY = cmd.CreateParameter();
            parSubledgerBalanceReportingCCY.ParameterName = "@SubledgerBalanceReportingCCY";
            if (!entity.IsSubledgerBalanceReportingCCYNull)
                parSubledgerBalanceReportingCCY.Value = entity.SubledgerBalanceReportingCCY;
            else
                parSubledgerBalanceReportingCCY.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerBalanceReportingCCY);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in SubledgerDataInfo object
        /// </summary>
        /// <param name="o">A SubledgerDataInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(SubledgerDataInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_SubledgerData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (!entity.IsAccountIDNull)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parBaseCCY = cmd.CreateParameter();
            parBaseCCY.ParameterName = "@BaseCCY";
            if (!entity.IsBaseCCYNull)
                parBaseCCY.Value = entity.BaseCCY;
            else
                parBaseCCY.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCCY);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReportingCCY = cmd.CreateParameter();
            parReportingCCY.ParameterName = "@ReportingCCY";
            if (!entity.IsReportingCCYNull)
                parReportingCCY.Value = entity.ReportingCCY;
            else
                parReportingCCY.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCCY);

            System.Data.IDbDataParameter parSubledgerBalanceBaseCCY = cmd.CreateParameter();
            parSubledgerBalanceBaseCCY.ParameterName = "@SubledgerBalanceBaseCCY";
            if (!entity.IsSubledgerBalanceBaseCCYNull)
                parSubledgerBalanceBaseCCY.Value = entity.SubledgerBalanceBaseCCY;
            else
                parSubledgerBalanceBaseCCY.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerBalanceBaseCCY);

            System.Data.IDbDataParameter parSubledgerBalanceReportingCCY = cmd.CreateParameter();
            parSubledgerBalanceReportingCCY.ParameterName = "@SubledgerBalanceReportingCCY";
            if (!entity.IsSubledgerBalanceReportingCCYNull)
                parSubledgerBalanceReportingCCY.Value = entity.SubledgerBalanceReportingCCY;
            else
                parSubledgerBalanceReportingCCY.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerBalanceReportingCCY);

            System.Data.IDbDataParameter pkparSubledgerDataID = cmd.CreateParameter();
            pkparSubledgerDataID.ParameterName = "@SubledgerDataID";
            pkparSubledgerDataID.Value = entity.SubledgerDataID;
            cmdParams.Add(pkparSubledgerDataID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_SubledgerData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerDataID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_SubledgerData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerDataID";
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
        public IList<SubledgerDataInfo> SelectAllByAccountID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SubledgerDataByAccountID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
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
        public IList<SubledgerDataInfo> SelectAllByGLDataID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SubledgerDataByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(SubledgerDataInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SubledgerDataDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(SubledgerDataInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SubledgerDataDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(SubledgerDataInfo entity, object id)
        {
            entity.SubledgerDataID = Convert.ToInt32(id);
        }




    }
}
