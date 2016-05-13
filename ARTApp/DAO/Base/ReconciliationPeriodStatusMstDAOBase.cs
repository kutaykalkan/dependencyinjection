

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

    public abstract class ReconciliationPeriodStatusMstDAOBase : CustomAbstractDAO<ReconciliationPeriodStatusMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
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
        /// A static representation of column ReconciliationPeriodStatus
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODSTATUS = "ReconciliationPeriodStatus";
        /// <summary>
        /// A static representation of column ReconciliationPeriodStatusID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODSTATUSID = "ReconciliationPeriodStatusID";
        /// <summary>
        /// A static representation of column ReconciliationPeriodStatusLabelID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODSTATUSLABELID = "ReconciliationPeriodStatusLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (ReconciliationPeriodStatusID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReconciliationPeriodStatusID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReconciliationPeriodStatusMst";

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
        public ReconciliationPeriodStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationPeriodStatusMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationPeriodStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationPeriodStatusMstInfo</returns>
        protected override ReconciliationPeriodStatusMstInfo MapObject(System.Data.IDataReader r)
        {
            ReconciliationPeriodStatusMstInfo entity = new ReconciliationPeriodStatusMstInfo();
            entity.ReconciliationPeriodStatusID = r.GetInt16Value("ReconciliationPeriodStatusID");
            entity.ReconciliationPeriodStatus = r.GetStringValue("ReconciliationPeriodStatus");
            entity.ReconciliationPeriodStatusLabelID = r.GetInt32Value("ReconciliationPeriodStatusLabelID");
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
        /// in ReconciliationPeriodStatusMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationPeriodStatusMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationPeriodStatusMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationPeriodStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


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

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parReconciliationPeriodStatus = cmd.CreateParameter();
            parReconciliationPeriodStatus.ParameterName = "@ReconciliationPeriodStatus";
            if (!entity.IsReconciliationPeriodStatusNull)
                parReconciliationPeriodStatus.Value = entity.ReconciliationPeriodStatus;
            else
                parReconciliationPeriodStatus.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodStatus);

            System.Data.IDbDataParameter parReconciliationPeriodStatusID = cmd.CreateParameter();
            parReconciliationPeriodStatusID.ParameterName = "@ReconciliationPeriodStatusID";
            if (!entity.IsReconciliationPeriodStatusIDNull)
                parReconciliationPeriodStatusID.Value = entity.ReconciliationPeriodStatusID;
            else
                parReconciliationPeriodStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodStatusID);

            System.Data.IDbDataParameter parReconciliationPeriodStatusLabelID = cmd.CreateParameter();
            parReconciliationPeriodStatusLabelID.ParameterName = "@ReconciliationPeriodStatusLabelID";
            if (!entity.IsReconciliationPeriodStatusLabelIDNull)
                parReconciliationPeriodStatusLabelID.Value = entity.ReconciliationPeriodStatusLabelID;
            else
                parReconciliationPeriodStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodStatusLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in ReconciliationPeriodStatusMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationPeriodStatusMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationPeriodStatusMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationPeriodStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


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

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parReconciliationPeriodStatus = cmd.CreateParameter();
            parReconciliationPeriodStatus.ParameterName = "@ReconciliationPeriodStatus";
            if (!entity.IsReconciliationPeriodStatusNull)
                parReconciliationPeriodStatus.Value = entity.ReconciliationPeriodStatus;
            else
                parReconciliationPeriodStatus.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodStatus);

            System.Data.IDbDataParameter parReconciliationPeriodStatusLabelID = cmd.CreateParameter();
            parReconciliationPeriodStatusLabelID.ParameterName = "@ReconciliationPeriodStatusLabelID";
            if (!entity.IsReconciliationPeriodStatusLabelIDNull)
                parReconciliationPeriodStatusLabelID.Value = entity.ReconciliationPeriodStatusLabelID;
            else
                parReconciliationPeriodStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodStatusLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparReconciliationPeriodStatusID = cmd.CreateParameter();
            pkparReconciliationPeriodStatusID.ParameterName = "@ReconciliationPeriodStatusID";
            pkparReconciliationPeriodStatusID.Value = entity.ReconciliationPeriodStatusID;
            cmdParams.Add(pkparReconciliationPeriodStatusID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationPeriodStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodStatusID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationPeriodStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }











    }
}
