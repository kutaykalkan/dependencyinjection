

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

    public abstract class AlertTypeMstDAOBase : CustomAbstractDAO<AlertTypeMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column AlertType
        /// </summary>
        public static readonly string COLUMN_ALERTTYPE = "AlertType";
        /// <summary>
        /// A static representation of column AlertTypeID
        /// </summary>
        public static readonly string COLUMN_ALERTTYPEID = "AlertTypeID";
        /// <summary>
        /// A static representation of column AlertTypeLabelID
        /// </summary>
        public static readonly string COLUMN_ALERTTYPELABELID = "AlertTypeLabelID";
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
        /// Provides access to the name of the primary key column (AlertTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AlertTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "AlertTypeMst";

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
        public AlertTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AlertTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AlertTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AlertTypeMstInfo</returns>
        protected override AlertTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            AlertTypeMstInfo entity = new AlertTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("AlertTypeID");
                if (!r.IsDBNull(ordinal)) entity.AlertTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertType");
                if (!r.IsDBNull(ordinal)) entity.AlertType = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertTypeLabelID");
                if (!r.IsDBNull(ordinal)) entity.AlertTypeLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in AlertTypeMstInfo object
        /// </summary>
        /// <param name="o">A AlertTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AlertTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AlertTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlertType = cmd.CreateParameter();
            parAlertType.ParameterName = "@AlertType";
            if (!entity.IsAlertTypeNull)
                parAlertType.Value = entity.AlertType;
            else
                parAlertType.Value = System.DBNull.Value;
            cmdParams.Add(parAlertType);

            System.Data.IDbDataParameter parAlertTypeLabelID = cmd.CreateParameter();
            parAlertTypeLabelID.ParameterName = "@AlertTypeLabelID";
            if (!entity.IsAlertTypeLabelIDNull)
                parAlertTypeLabelID.Value = entity.AlertTypeLabelID;
            else
                parAlertTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertTypeLabelID);

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
        /// in AlertTypeMstInfo object
        /// </summary>
        /// <param name="o">A AlertTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AlertTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AlertTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlertType = cmd.CreateParameter();
            parAlertType.ParameterName = "@AlertType";
            if (!entity.IsAlertTypeNull)
                parAlertType.Value = entity.AlertType;
            else
                parAlertType.Value = System.DBNull.Value;
            cmdParams.Add(parAlertType);

            System.Data.IDbDataParameter parAlertTypeLabelID = cmd.CreateParameter();
            parAlertTypeLabelID.ParameterName = "@AlertTypeLabelID";
            if (!entity.IsAlertTypeLabelIDNull)
                parAlertTypeLabelID.Value = entity.AlertTypeLabelID;
            else
                parAlertTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertTypeLabelID);

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

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparAlertTypeID = cmd.CreateParameter();
            pkparAlertTypeID.ParameterName = "@AlertTypeID";
            pkparAlertTypeID.Value = entity.AlertTypeID;
            cmdParams.Add(pkparAlertTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AlertTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AlertTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }


        private void MapIdentity(AlertTypeMstInfo entity, object id)
        {
            entity.AlertTypeID = Convert.ToInt16(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertTypeMstInfo> SelectAlertTypeMstDetailsAssociatedToAlertResponseTypeMstByAlertMst(AlertResponseTypeMstInfo entity)
        {
            return this.SelectAlertTypeMstDetailsAssociatedToAlertResponseTypeMstByAlertMst(entity.AlertResponseTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertTypeMstInfo> SelectAlertTypeMstDetailsAssociatedToAlertResponseTypeMstByAlertMst(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertTypeMst] INNER JOIN [AlertMst] ON [AlertTypeMst].[AlertTypeID] = [AlertMst].[AlertTypeID] INNER JOIN [AlertResponseTypeMst] ON [AlertMst].[AlertResponseTypeID] = [AlertResponseTypeMst].[AlertResponseTypeID]  WHERE  [AlertResponseTypeMst].[AlertResponseTypeID] = @AlertResponseTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertResponseTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<AlertTypeMstInfo> objAlertTypeMstEntityColl = new List<AlertTypeMstInfo>(this.Select(cmd));
            return objAlertTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertTypeMstInfo> SelectAlertTypeMstDetailsAssociatedToAlertGenerationLocationMstByAlertMst(AlertGenerationLocationMstInfo entity)
        {
            return this.SelectAlertTypeMstDetailsAssociatedToAlertGenerationLocationMstByAlertMst(entity.AlertGenerationLocationID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertTypeMstInfo> SelectAlertTypeMstDetailsAssociatedToAlertGenerationLocationMstByAlertMst(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertTypeMst] INNER JOIN [AlertMst] ON [AlertTypeMst].[AlertTypeID] = [AlertMst].[AlertTypeID] INNER JOIN [AlertGenerationLocationMst] ON [AlertMst].[AlertGenerationLocationID] = [AlertGenerationLocationMst].[AlertGenerationLocationID]  WHERE  [AlertGenerationLocationMst].[AlertGenerationLocationID] = @AlertGenerationLocationID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertGenerationLocationID";
            par.Value = id;

            cmdParams.Add(par);
            List<AlertTypeMstInfo> objAlertTypeMstEntityColl = new List<AlertTypeMstInfo>(this.Select(cmd));
            return objAlertTypeMstEntityColl;
        }


    }
}
