

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

    public abstract class CompanyJEWriteOffOnApproverDAOBase : CustomAbstractDAO<CompanyJEWriteOffOnApproverInfo>
    {

        /// <summary>
        /// A static representation of column CompanyJEWriteOffOnApproverID
        /// </summary>
        public static readonly string COLUMN_COMPANYJEWRITEOFFONAPPROVERID = "CompanyJEWriteOffOnApproverID";
        /// <summary>
        /// A static representation of column CompanyRecPeriodSetID
        /// </summary>
        public static readonly string COLUMN_COMPANYRECPERIODSETID = "CompanyRecPeriodSetID";
        /// <summary>
        /// A static representation of column FromAmount
        /// </summary>
        public static readonly string COLUMN_FROMAMOUNT = "FromAmount";
        /// <summary>
        /// A static representation of column PrimaryApproverUserID
        /// </summary>
        public static readonly string COLUMN_PRIMARYAPPROVERUSERID = "PrimaryApproverUserID";
        /// <summary>
        /// A static representation of column SecondaryApproverUserID
        /// </summary>
        public static readonly string COLUMN_SECONDARYAPPROVERUSERID = "SecondaryApproverUserID";
        /// <summary>
        /// A static representation of column ToAmount
        /// </summary>
        public static readonly string COLUMN_TOAMOUNT = "ToAmount";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyJEWriteOffOnApproverID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyJEWriteOffOnApproverID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyJEWriteOffOnApprover";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemART";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyJEWriteOffOnApproverDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyJEWriteOffOnApprover", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyJEWriteOffOnApproverInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyJEWriteOffOnApproverInfo</returns>
        protected override CompanyJEWriteOffOnApproverInfo MapObject(System.Data.IDataReader r)
        {

            CompanyJEWriteOffOnApproverInfo entity = new CompanyJEWriteOffOnApproverInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyJEWriteOffOnApproverID");
                if (!r.IsDBNull(ordinal)) entity.CompanyJEWriteOffOnApproverID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyRecPeriodSetID");
                if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FromAmount");
                if (!r.IsDBNull(ordinal)) entity.FromAmount = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ToAmount");
                if (!r.IsDBNull(ordinal)) entity.ToAmount = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PrimaryApproverUserID");
                if (!r.IsDBNull(ordinal)) entity.PrimaryApproverUserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SecondaryApproverUserID");
                if (!r.IsDBNull(ordinal)) entity.SecondaryApproverUserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyJEWriteOffOnApproverInfo object
        /// </summary>
        /// <param name="o">A CompanyJEWriteOffOnApproverInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyJEWriteOffOnApproverInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyJEWriteOffOnApprover");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
            parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
            if (entity != null)
                parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
            else
                parCompanyRecPeriodSetID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetID);

            System.Data.IDbDataParameter parFromAmount = cmd.CreateParameter();
            parFromAmount.ParameterName = "@FromAmount";
            if (entity != null)
                parFromAmount.Value = entity.FromAmount;
            else
                parFromAmount.Value = System.DBNull.Value;
            cmdParams.Add(parFromAmount);

            System.Data.IDbDataParameter parPrimaryApproverUserID = cmd.CreateParameter();
            parPrimaryApproverUserID.ParameterName = "@PrimaryApproverUserID";
            if (entity != null)
                parPrimaryApproverUserID.Value = entity.PrimaryApproverUserID;
            else
                parPrimaryApproverUserID.Value = System.DBNull.Value;
            cmdParams.Add(parPrimaryApproverUserID);

            System.Data.IDbDataParameter parSecondaryApproverUserID = cmd.CreateParameter();
            parSecondaryApproverUserID.ParameterName = "@SecondaryApproverUserID";
            if (entity != null)
                parSecondaryApproverUserID.Value = entity.SecondaryApproverUserID;
            else
                parSecondaryApproverUserID.Value = System.DBNull.Value;
            cmdParams.Add(parSecondaryApproverUserID);

            System.Data.IDbDataParameter parToAmount = cmd.CreateParameter();
            parToAmount.ParameterName = "@ToAmount";
            if (entity != null)
                parToAmount.Value = entity.ToAmount;
            else
                parToAmount.Value = System.DBNull.Value;
            cmdParams.Add(parToAmount);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyJEWriteOffOnApproverInfo object
        /// </summary>
        /// <param name="o">A CompanyJEWriteOffOnApproverInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyJEWriteOffOnApproverInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyJEWriteOffOnApprover");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
            parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
            if (entity != null)
                parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
            else
                parCompanyRecPeriodSetID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetID);

            System.Data.IDbDataParameter parFromAmount = cmd.CreateParameter();
            parFromAmount.ParameterName = "@FromAmount";
            if (entity != null)
                parFromAmount.Value = entity.FromAmount;
            else
                parFromAmount.Value = System.DBNull.Value;
            cmdParams.Add(parFromAmount);

            System.Data.IDbDataParameter parPrimaryApproverUserID = cmd.CreateParameter();
            parPrimaryApproverUserID.ParameterName = "@PrimaryApproverUserID";
            if (entity != null)
                parPrimaryApproverUserID.Value = entity.PrimaryApproverUserID;
            else
                parPrimaryApproverUserID.Value = System.DBNull.Value;
            cmdParams.Add(parPrimaryApproverUserID);

            System.Data.IDbDataParameter parSecondaryApproverUserID = cmd.CreateParameter();
            parSecondaryApproverUserID.ParameterName = "@SecondaryApproverUserID";
            if (entity != null)
                parSecondaryApproverUserID.Value = entity.SecondaryApproverUserID;
            else
                parSecondaryApproverUserID.Value = System.DBNull.Value;
            cmdParams.Add(parSecondaryApproverUserID);

            System.Data.IDbDataParameter parToAmount = cmd.CreateParameter();
            parToAmount.ParameterName = "@ToAmount";
            if (entity != null)
                parToAmount.Value = entity.ToAmount;
            else
                parToAmount.Value = System.DBNull.Value;
            cmdParams.Add(parToAmount);

            System.Data.IDbDataParameter pkparCompanyJEWriteOffOnApproverID = cmd.CreateParameter();
            pkparCompanyJEWriteOffOnApproverID.ParameterName = "@CompanyJEWriteOffOnApproverID";
            pkparCompanyJEWriteOffOnApproverID.Value = entity.CompanyJEWriteOffOnApproverID;
            cmdParams.Add(pkparCompanyJEWriteOffOnApproverID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyJEWriteOffOnApprover");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyJEWriteOffOnApproverID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyJEWriteOffOnApprover");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyJEWriteOffOnApproverID";
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
        public IList<CompanyJEWriteOffOnApproverInfo> SelectAllByCompanyRecPeriodSetID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyJEWriteOffOnApproverByCompanyRecPeriodSetID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyRecPeriodSetID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(CompanyJEWriteOffOnApproverInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyJEWriteOffOnApproverDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanyJEWriteOffOnApproverInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyJEWriteOffOnApproverDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanyJEWriteOffOnApproverInfo entity, object id)
        {
            entity.CompanyJEWriteOffOnApproverID = Convert.ToInt32(id);
        }




    }
}
