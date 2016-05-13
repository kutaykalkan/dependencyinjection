

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

    public abstract class CompanyRecPeriodSetTypeMstDAOBase : CustomAbstractDAO<CompanyRecPeriodSetTypeMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CompanyRecPeriodSetType
        /// </summary>
        public static readonly string COLUMN_COMPANYRECPERIODSETTYPE = "CompanyRecPeriodSetType";
        /// <summary>
        /// A static representation of column CompanyRecPeriodSetTypeID
        /// </summary>
        public static readonly string COLUMN_COMPANYRECPERIODSETTYPEID = "CompanyRecPeriodSetTypeID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyRecPeriodSetTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyRecPeriodSetTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyRecPeriodSetTypeMst";

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
        public CompanyRecPeriodSetTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyRecPeriodSetTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyRecPeriodSetTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyRecPeriodSetTypeMstInfo</returns>
        protected override CompanyRecPeriodSetTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            CompanyRecPeriodSetTypeMstInfo entity = new CompanyRecPeriodSetTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyRecPeriodSetTypeID");
                if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyRecPeriodSetType");
                if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetType = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsActive");
                if (!r.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("DateRevised");
                if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RevisedBy");
                if (!r.IsDBNull(ordinal)) entity.RevisedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("HostName");
                if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyRecPeriodSetTypeMstInfo object
        /// </summary>
        /// <param name="o">A CompanyRecPeriodSetTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyRecPeriodSetTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyRecPeriodSetTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyRecPeriodSetType = cmd.CreateParameter();
            parCompanyRecPeriodSetType.ParameterName = "@CompanyRecPeriodSetType";
            if (entity != null)
                parCompanyRecPeriodSetType.Value = entity.CompanyRecPeriodSetType;
            else
                parCompanyRecPeriodSetType.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetType);

            System.Data.IDbDataParameter parCompanyRecPeriodSetTypeID = cmd.CreateParameter();
            parCompanyRecPeriodSetTypeID.ParameterName = "@CompanyRecPeriodSetTypeID";
            if (entity != null)
                parCompanyRecPeriodSetTypeID.Value = entity.CompanyRecPeriodSetTypeID;
            else
                parCompanyRecPeriodSetTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetTypeID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyRecPeriodSetTypeMstInfo object
        /// </summary>
        /// <param name="o">A CompanyRecPeriodSetTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyRecPeriodSetTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyRecPeriodSetTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyRecPeriodSetType = cmd.CreateParameter();
            parCompanyRecPeriodSetType.ParameterName = "@CompanyRecPeriodSetType";
            if (entity != null)
                parCompanyRecPeriodSetType.Value = entity.CompanyRecPeriodSetType;
            else
                parCompanyRecPeriodSetType.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetType);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparCompanyRecPeriodSetTypeID = cmd.CreateParameter();
            pkparCompanyRecPeriodSetTypeID.ParameterName = "@CompanyRecPeriodSetTypeID";
            pkparCompanyRecPeriodSetTypeID.Value = entity.CompanyRecPeriodSetTypeID;
            cmdParams.Add(pkparCompanyRecPeriodSetTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyRecPeriodSetTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyRecPeriodSetTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyRecPeriodSetTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyRecPeriodSetTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }












    }
}
