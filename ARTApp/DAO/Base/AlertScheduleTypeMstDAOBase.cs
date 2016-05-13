

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

    public abstract class AlertScheduleTypeMstDAOBase : CustomAbstractDAO<AlertScheduleTypeMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column AlertScheduleType
        /// </summary>
        public static readonly string COLUMN_ALERTSCHEDULETYPE = "AlertScheduleType";
        /// <summary>
        /// A static representation of column AlertScheduleTypeID
        /// </summary>
        public static readonly string COLUMN_ALERTSCHEDULETYPEID = "AlertScheduleTypeID";
        /// <summary>
        /// A static representation of column AlertScheduleTypeLabelID
        /// </summary>
        public static readonly string COLUMN_ALERTSCHEDULETYPELABELID = "AlertScheduleTypeLabelID";
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
        /// Provides access to the name of the primary key column (AlertScheduleTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AlertScheduleTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "AlertScheduleTypeMst";

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
        public AlertScheduleTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AlertScheduleTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AlertScheduleTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AlertScheduleTypeMstInfo</returns>
        protected override AlertScheduleTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            AlertScheduleTypeMstInfo entity = new AlertScheduleTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("AlertScheduleTypeID");
                if (!r.IsDBNull(ordinal)) entity.AlertScheduleTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertScheduleType");
                if (!r.IsDBNull(ordinal)) entity.AlertScheduleType = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertScheduleTypeLabelID");
                if (!r.IsDBNull(ordinal)) entity.AlertScheduleTypeLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in AlertScheduleTypeMstInfo object
        /// </summary>
        /// <param name="o">A AlertScheduleTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AlertScheduleTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AlertScheduleTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlertScheduleType = cmd.CreateParameter();
            parAlertScheduleType.ParameterName = "@AlertScheduleType";
            if (!entity.IsAlertScheduleTypeNull)
                parAlertScheduleType.Value = entity.AlertScheduleType;
            else
                parAlertScheduleType.Value = System.DBNull.Value;
            cmdParams.Add(parAlertScheduleType);

            System.Data.IDbDataParameter parAlertScheduleTypeLabelID = cmd.CreateParameter();
            parAlertScheduleTypeLabelID.ParameterName = "@AlertScheduleTypeLabelID";
            if (!entity.IsAlertScheduleTypeLabelIDNull)
                parAlertScheduleTypeLabelID.Value = entity.AlertScheduleTypeLabelID;
            else
                parAlertScheduleTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertScheduleTypeLabelID);

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
        /// in AlertScheduleTypeMstInfo object
        /// </summary>
        /// <param name="o">A AlertScheduleTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AlertScheduleTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AlertScheduleTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlertScheduleType = cmd.CreateParameter();
            parAlertScheduleType.ParameterName = "@AlertScheduleType";
            if (!entity.IsAlertScheduleTypeNull)
                parAlertScheduleType.Value = entity.AlertScheduleType;
            else
                parAlertScheduleType.Value = System.DBNull.Value;
            cmdParams.Add(parAlertScheduleType);

            System.Data.IDbDataParameter parAlertScheduleTypeLabelID = cmd.CreateParameter();
            parAlertScheduleTypeLabelID.ParameterName = "@AlertScheduleTypeLabelID";
            if (!entity.IsAlertScheduleTypeLabelIDNull)
                parAlertScheduleTypeLabelID.Value = entity.AlertScheduleTypeLabelID;
            else
                parAlertScheduleTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertScheduleTypeLabelID);

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

            System.Data.IDbDataParameter pkparAlertScheduleTypeID = cmd.CreateParameter();
            pkparAlertScheduleTypeID.ParameterName = "@AlertScheduleTypeID";
            pkparAlertScheduleTypeID.Value = entity.AlertScheduleTypeID;
            cmdParams.Add(pkparAlertScheduleTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AlertScheduleTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertScheduleTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AlertScheduleTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertScheduleTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }

        private void MapIdentity(AlertScheduleTypeMstInfo entity, object id)
        {
            entity.AlertScheduleTypeID = Convert.ToInt16(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertScheduleTypeMstInfo> SelectAlertScheduleTypeMstDetailsAssociatedToCompanyHdrByCompanyAlert(CompanyHdrInfo entity)
        {
            return this.SelectAlertScheduleTypeMstDetailsAssociatedToCompanyHdrByCompanyAlert(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertScheduleTypeMstInfo> SelectAlertScheduleTypeMstDetailsAssociatedToCompanyHdrByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertScheduleTypeMst] INNER JOIN [CompanyAlert] ON [AlertScheduleTypeMst].[AlertScheduleTypeID] = [CompanyAlert].[AlertScheduleTypeID] INNER JOIN [CompanyHdr] ON [CompanyAlert].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<AlertScheduleTypeMstInfo> objAlertScheduleTypeMstEntityColl = new List<AlertScheduleTypeMstInfo>(this.Select(cmd));
            return objAlertScheduleTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertScheduleTypeMstInfo> SelectAlertScheduleTypeMstDetailsAssociatedToAlertMstByCompanyAlert(AlertMstInfo entity)
        {
            return this.SelectAlertScheduleTypeMstDetailsAssociatedToAlertMstByCompanyAlert(entity.AlertID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertScheduleTypeMstInfo> SelectAlertScheduleTypeMstDetailsAssociatedToAlertMstByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertScheduleTypeMst] INNER JOIN [CompanyAlert] ON [AlertScheduleTypeMst].[AlertScheduleTypeID] = [CompanyAlert].[AlertScheduleTypeID] INNER JOIN [AlertMst] ON [CompanyAlert].[AlertID] = [AlertMst].[AlertID]  WHERE  [AlertMst].[AlertID] = @AlertID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertID";
            par.Value = id;

            cmdParams.Add(par);
            List<AlertScheduleTypeMstInfo> objAlertScheduleTypeMstEntityColl = new List<AlertScheduleTypeMstInfo>(this.Select(cmd));
            return objAlertScheduleTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertScheduleTypeMstInfo> SelectAlertScheduleTypeMstDetailsAssociatedToDateBasisMstByCompanyAlert(DateBasisMstInfo entity)
        {
            return this.SelectAlertScheduleTypeMstDetailsAssociatedToDateBasisMstByCompanyAlert(entity.DateBasisID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AlertScheduleTypeMstInfo> SelectAlertScheduleTypeMstDetailsAssociatedToDateBasisMstByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertScheduleTypeMst] INNER JOIN [CompanyAlert] ON [AlertScheduleTypeMst].[AlertScheduleTypeID] = [CompanyAlert].[AlertScheduleTypeID] INNER JOIN [DateBasisMst] ON [CompanyAlert].[DateBasisID] = [DateBasisMst].[DateBasisID]  WHERE  [DateBasisMst].[DateBasisID] = @DateBasisID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DateBasisID";
            par.Value = id;

            cmdParams.Add(par);
            List<AlertScheduleTypeMstInfo> objAlertScheduleTypeMstEntityColl = new List<AlertScheduleTypeMstInfo>(this.Select(cmd));
            return objAlertScheduleTypeMstEntityColl;
        }

    }
}
