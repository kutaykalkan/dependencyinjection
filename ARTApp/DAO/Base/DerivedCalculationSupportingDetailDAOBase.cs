

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

    public abstract class DerivedCalculationSupportingDetailDAOBase : CustomAbstractDAO<DerivedCalculationSupportingDetailInfo>
    {

        /// <summary>
        /// A static representation of column BaseCurrencyBalance
        /// </summary>
        public static readonly string COLUMN_BASECURRENCYBALANCE = "BaseCurrencyBalance";
        /// <summary>
        /// A static representation of column BasisForDerivedCalculation
        /// </summary>
        public static readonly string COLUMN_BASISFORDERIVEDCALCULATION = "BasisForDerivedCalculation";
        /// <summary>
        /// A static representation of column DerivedCalculationSupportingDetailID
        /// </summary>
        public static readonly string COLUMN_DERIVEDCALCULATIONSUPPORTINGDETAILID = "DerivedCalculationSupportingDetailID";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column ReportingCurrencyBalance
        /// </summary>
        public static readonly string COLUMN_REPORTINGCURRENCYBALANCE = "ReportingCurrencyBalance";
        /// <summary>
        /// Provides access to the name of the primary key column (DerivedCalculationSupportingDetailID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DerivedCalculationSupportingDetailID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DerivedCalculationSupportingDetail";

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
        public DerivedCalculationSupportingDetailDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DerivedCalculationSupportingDetail", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DerivedCalculationSupportingDetailInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DerivedCalculationSupportingDetailInfo</returns>
        protected override DerivedCalculationSupportingDetailInfo MapObject(System.Data.IDataReader r)
        {

            DerivedCalculationSupportingDetailInfo entity = new DerivedCalculationSupportingDetailInfo();


            try
            {
                int ordinal = r.GetOrdinal("DerivedCalculationSupportingDetailID");
                if (!r.IsDBNull(ordinal)) entity.DerivedCalculationSupportingDetailID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("BaseCurrencyBalance");
                if (!r.IsDBNull(ordinal)) entity.BaseCurrencyBalance = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportingCurrencyBalance");
                if (!r.IsDBNull(ordinal)) entity.ReportingCurrencyBalance = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("BasisForDerivedCalculation");
                if (!r.IsDBNull(ordinal)) entity.BasisForDerivedCalculation = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in DerivedCalculationSupportingDetailInfo object
        /// </summary>
        /// <param name="o">A DerivedCalculationSupportingDetailInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DerivedCalculationSupportingDetailInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DerivedCalculationSupportingDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parBaseCurrencyBalance = cmd.CreateParameter();
            parBaseCurrencyBalance.ParameterName = "@BaseCurrencyBalance";
            if (!entity.IsBaseCurrencyBalanceNull)
                parBaseCurrencyBalance.Value = entity.BaseCurrencyBalance;
            else
                parBaseCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyBalance);

            System.Data.IDbDataParameter parBasisForDerivedCalculation = cmd.CreateParameter();
            parBasisForDerivedCalculation.ParameterName = "@BasisForDerivedCalculation";
            if (!entity.IsBasisForDerivedCalculationNull)
                parBasisForDerivedCalculation.Value = entity.BasisForDerivedCalculation;
            else
                parBasisForDerivedCalculation.Value = System.DBNull.Value;
            cmdParams.Add(parBasisForDerivedCalculation);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReportingCurrencyBalance = cmd.CreateParameter();
            parReportingCurrencyBalance.ParameterName = "@ReportingCurrencyBalance";
            if (!entity.IsReportingCurrencyBalanceNull)
                parReportingCurrencyBalance.Value = entity.ReportingCurrencyBalance;
            else
                parReportingCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrencyBalance);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in DerivedCalculationSupportingDetailInfo object
        /// </summary>
        /// <param name="o">A DerivedCalculationSupportingDetailInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DerivedCalculationSupportingDetailInfo entity)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DerivedCalculationSupportingDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parBaseCurrencyBalance = cmd.CreateParameter();
            parBaseCurrencyBalance.ParameterName = "@BaseCurrencyBalance";
            if (!entity.IsBaseCurrencyBalanceNull)
                parBaseCurrencyBalance.Value = entity.BaseCurrencyBalance;
            else
                parBaseCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyBalance);

            System.Data.IDbDataParameter parBasisForDerivedCalculation = cmd.CreateParameter();
            parBasisForDerivedCalculation.ParameterName = "@BasisForDerivedCalculation";
            if (!entity.IsBasisForDerivedCalculationNull)
                parBasisForDerivedCalculation.Value = entity.BasisForDerivedCalculation;
            else
                parBasisForDerivedCalculation.Value = System.DBNull.Value;
            cmdParams.Add(parBasisForDerivedCalculation);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReportingCurrencyBalance = cmd.CreateParameter();
            parReportingCurrencyBalance.ParameterName = "@ReportingCurrencyBalance";
            if (!entity.IsReportingCurrencyBalanceNull)
                parReportingCurrencyBalance.Value = entity.ReportingCurrencyBalance;
            else
                parReportingCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrencyBalance);

            System.Data.IDbDataParameter pkparDerivedCalculationSupportingDetailID = cmd.CreateParameter();
            pkparDerivedCalculationSupportingDetailID.ParameterName = "@DerivedCalculationSupportingDetailID";
            pkparDerivedCalculationSupportingDetailID.Value = entity.DerivedCalculationSupportingDetailID;
            cmdParams.Add(pkparDerivedCalculationSupportingDetailID);

            return cmd;
        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DerivedCalculationSupportingDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DerivedCalculationSupportingDetailID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DerivedCalculationSupportingDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DerivedCalculationSupportingDetailID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }









        protected override void CustomSave(DerivedCalculationSupportingDetailInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DerivedCalculationSupportingDetailDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(DerivedCalculationSupportingDetailInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DerivedCalculationSupportingDetailDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(DerivedCalculationSupportingDetailInfo entity, object id)
        {
            entity.DerivedCalculationSupportingDetailID = Convert.ToInt32(id);
        }




    }
}
