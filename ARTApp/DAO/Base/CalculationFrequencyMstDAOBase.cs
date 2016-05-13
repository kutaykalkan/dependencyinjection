

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class CalculationFrequencyMstDAOBase : CustomAbstractDAO<CalculationFrequencyMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CalculationFrequency
        /// </summary>
        public static readonly string COLUMN_CALCULATIONFREQUENCY = "CalculationFrequency";
        /// <summary>
        /// A static representation of column CalculationFrequencyID
        /// </summary>
        public static readonly string COLUMN_CALCULATIONFREQUENCYID = "CalculationFrequencyID";
        /// <summary>
        /// A static representation of column CalculationFrequencyLabelID
        /// </summary>
        public static readonly string COLUMN_CALCULATIONFREQUENCYLABELID = "CalculationFrequencyLabelID";
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
        /// Provides access to the name of the primary key column (CalculationFrequencyID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CalculationFrequencyID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CalculationFrequencyMst";

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
        public CalculationFrequencyMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CalculationFrequencyMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CalculationFrequencyMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CalculationFrequencyMstInfo</returns>
        protected override CalculationFrequencyMstInfo MapObject(System.Data.IDataReader r)
        {

            CalculationFrequencyMstInfo entity = new CalculationFrequencyMstInfo();

            entity.CalculationFrequencyID = r.GetInt16Value("CalculationFrequencyID");
            entity.CalculationFrequency = r.GetStringValue("CalculationFrequency");
            entity.CalculationFrequencyLabelID = r.GetInt32Value("CalculationFrequencyLabelID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CalculationFrequencyMstInfo object
        /// </summary>
        /// <param name="o">A CalculationFrequencyMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CalculationFrequencyMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CalculationFrequencyMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCalculationFrequency = cmd.CreateParameter();
            parCalculationFrequency.ParameterName = "@CalculationFrequency";
            if (entity != null)
                parCalculationFrequency.Value = entity.CalculationFrequency;
            else
                parCalculationFrequency.Value = System.DBNull.Value;
            cmdParams.Add(parCalculationFrequency);

            System.Data.IDbDataParameter parCalculationFrequencyID = cmd.CreateParameter();
            parCalculationFrequencyID.ParameterName = "@CalculationFrequencyID";
            if (entity != null)
                parCalculationFrequencyID.Value = entity.CalculationFrequencyID;
            else
                parCalculationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parCalculationFrequencyID);

            System.Data.IDbDataParameter parCalculationFrequencyLabelID = cmd.CreateParameter();
            parCalculationFrequencyLabelID.ParameterName = "@CalculationFrequencyLabelID";
            if (entity != null)
                parCalculationFrequencyLabelID.Value = entity.CalculationFrequencyLabelID;
            else
                parCalculationFrequencyLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCalculationFrequencyLabelID);

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
        /// in CalculationFrequencyMstInfo object
        /// </summary>
        /// <param name="o">A CalculationFrequencyMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CalculationFrequencyMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CalculationFrequencyMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCalculationFrequency = cmd.CreateParameter();
            parCalculationFrequency.ParameterName = "@CalculationFrequency";
            if (entity != null)
                parCalculationFrequency.Value = entity.CalculationFrequency;
            else
                parCalculationFrequency.Value = System.DBNull.Value;
            cmdParams.Add(parCalculationFrequency);

            System.Data.IDbDataParameter parCalculationFrequencyLabelID = cmd.CreateParameter();
            parCalculationFrequencyLabelID.ParameterName = "@CalculationFrequencyLabelID";
            if (entity != null)
                parCalculationFrequencyLabelID.Value = entity.CalculationFrequencyLabelID;
            else
                parCalculationFrequencyLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCalculationFrequencyLabelID);

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

            System.Data.IDbDataParameter pkparCalculationFrequencyID = cmd.CreateParameter();
            pkparCalculationFrequencyID.ParameterName = "@CalculationFrequencyID";
            pkparCalculationFrequencyID.Value = entity.CalculationFrequencyID;
            cmdParams.Add(pkparCalculationFrequencyID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CalculationFrequencyMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CalculationFrequencyID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CalculationFrequencyMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CalculationFrequencyID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }











    }
}
