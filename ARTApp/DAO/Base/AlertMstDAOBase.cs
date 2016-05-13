

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

    public abstract class AlertMstDAOBase : CustomAbstractDAO<AlertMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column Alert
        /// </summary>
        public static readonly string COLUMN_ALERT = "Alert";
        /// <summary>
        /// A static representation of column AlertGenerationLocationID
        /// </summary>
        public static readonly string COLUMN_ALERTGENERATIONLOCATIONID = "AlertGenerationLocationID";
        /// <summary>
        /// A static representation of column AlertID
        /// </summary>
        public static readonly string COLUMN_ALERTID = "AlertID";
        /// <summary>
        /// A static representation of column AlertLabelID
        /// </summary>
        public static readonly string COLUMN_ALERTLABELID = "AlertLabelID";
        /// <summary>
        /// A static representation of column AlertResponseTypeID
        /// </summary>
        public static readonly string COLUMN_ALERTRESPONSETYPEID = "AlertResponseTypeID";
        /// <summary>
        /// A static representation of column AlertTypeID
        /// </summary>
        public static readonly string COLUMN_ALERTTYPEID = "AlertTypeID";
        /// <summary>
        /// A static representation of column BaseUrl
        /// </summary>
        public static readonly string COLUMN_BASEURL = "BaseUrl";
        /// <summary>
        /// A static representation of column CapabilityID
        /// </summary>
        public static readonly string COLUMN_CAPABILITYID = "CapabilityID";
        /// <summary>
        /// A static representation of column Condition
        /// </summary>
        public static readonly string COLUMN_CONDITION = "Condition";
        /// <summary>
        /// A static representation of column ConditionLabelID
        /// </summary>
        public static readonly string COLUMN_CONDITIONLABELID = "ConditionLabelID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column DefaultThreshold
        /// </summary>
        public static readonly string COLUMN_DEFAULTTHRESHOLD = "DefaultThreshold";
        /// <summary>
        /// A static representation of column DefaultThresholdTypeID
        /// </summary>
        public static readonly string COLUMN_DEFAULTTHRESHOLDTYPEID = "DefaultThresholdTypeID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column IsSystemAdminAlert
        /// </summary>
        public static readonly string COLUMN_ISSYSTEMADMINALERT = "IsSystemAdminAlert";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (AlertID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AlertID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "AlertMst";

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
        public AlertMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AlertMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AlertMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AlertMstInfo</returns>
        protected override AlertMstInfo MapObject(System.Data.IDataReader r)
        {

            AlertMstInfo entity = new AlertMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("AlertID");
                if (!r.IsDBNull(ordinal)) entity.AlertID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Alert");
                if (!r.IsDBNull(ordinal)) entity.Alert = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertDisplay");
                if (!r.IsDBNull(ordinal)) entity.AlertDisplay = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertLabelID");
                if (!r.IsDBNull(ordinal)) entity.AlertLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Condition");
                if (!r.IsDBNull(ordinal)) entity.Condition = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ConditionLabelID");
                if (!r.IsDBNull(ordinal)) entity.ConditionLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertTypeID");
                if (!r.IsDBNull(ordinal)) entity.AlertTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DefaultThreshold");
                if (!r.IsDBNull(ordinal)) entity.DefaultThreshold = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DefaultThresholdTypeID");
                if (!r.IsDBNull(ordinal)) entity.DefaultThresholdTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertResponseTypeID");
                if (!r.IsDBNull(ordinal)) entity.AlertResponseTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertGenerationLocationID");
                if (!r.IsDBNull(ordinal)) entity.AlertGenerationLocationID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsSystemAdminAlert");
                if (!r.IsDBNull(ordinal)) entity.IsSystemAdminAlert = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CapabilityID");
                if (!r.IsDBNull(ordinal)) entity.CapabilityID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("BaseUrl");
                if (!r.IsDBNull(ordinal)) entity.BaseUrl = ((System.String)(r.GetValue(ordinal)));
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

            try
            {
                int ordinal = r.GetOrdinal("AlertCategoryID");
                if (!r.IsDBNull(ordinal)) entity.AlertCategoryID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in AlertMstInfo object
        /// </summary>
        /// <param name="o">A AlertMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AlertMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AlertMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlert = cmd.CreateParameter();
            parAlert.ParameterName = "@Alert";
            if (!entity.IsAlertNull)
                parAlert.Value = entity.Alert;
            else
                parAlert.Value = System.DBNull.Value;
            cmdParams.Add(parAlert);

            System.Data.IDbDataParameter parAlertGenerationLocationID = cmd.CreateParameter();
            parAlertGenerationLocationID.ParameterName = "@AlertGenerationLocationID";
            if (!entity.IsAlertGenerationLocationIDNull)
                parAlertGenerationLocationID.Value = entity.AlertGenerationLocationID;
            else
                parAlertGenerationLocationID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertGenerationLocationID);

            System.Data.IDbDataParameter parAlertID = cmd.CreateParameter();
            parAlertID.ParameterName = "@AlertID";
            if (!entity.IsAlertIDNull)
                parAlertID.Value = entity.AlertID;
            else
                parAlertID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertID);

            System.Data.IDbDataParameter parAlertLabelID = cmd.CreateParameter();
            parAlertLabelID.ParameterName = "@AlertLabelID";
            if (!entity.IsAlertLabelIDNull)
                parAlertLabelID.Value = entity.AlertLabelID;
            else
                parAlertLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertLabelID);

            System.Data.IDbDataParameter parAlertResponseTypeID = cmd.CreateParameter();
            parAlertResponseTypeID.ParameterName = "@AlertResponseTypeID";
            if (!entity.IsAlertResponseTypeIDNull)
                parAlertResponseTypeID.Value = entity.AlertResponseTypeID;
            else
                parAlertResponseTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertResponseTypeID);

            System.Data.IDbDataParameter parAlertTypeID = cmd.CreateParameter();
            parAlertTypeID.ParameterName = "@AlertTypeID";
            if (!entity.IsAlertTypeIDNull)
                parAlertTypeID.Value = entity.AlertTypeID;
            else
                parAlertTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertTypeID);

            System.Data.IDbDataParameter parBaseUrl = cmd.CreateParameter();
            parBaseUrl.ParameterName = "@BaseUrl";
            if (!entity.IsBaseUrlNull)
                parBaseUrl.Value = entity.BaseUrl;
            else
                parBaseUrl.Value = System.DBNull.Value;
            cmdParams.Add(parBaseUrl);

            System.Data.IDbDataParameter parCapabilityID = cmd.CreateParameter();
            parCapabilityID.ParameterName = "@CapabilityID";
            if (!entity.IsCapabilityIDNull)
                parCapabilityID.Value = entity.CapabilityID;
            else
                parCapabilityID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityID);

            System.Data.IDbDataParameter parCondition = cmd.CreateParameter();
            parCondition.ParameterName = "@Condition";
            if (!entity.IsConditionNull)
                parCondition.Value = entity.Condition;
            else
                parCondition.Value = System.DBNull.Value;
            cmdParams.Add(parCondition);

            System.Data.IDbDataParameter parConditionLabelID = cmd.CreateParameter();
            parConditionLabelID.ParameterName = "@ConditionLabelID";
            if (!entity.IsConditionLabelIDNull)
                parConditionLabelID.Value = entity.ConditionLabelID;
            else
                parConditionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parConditionLabelID);

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

            System.Data.IDbDataParameter parDefaultThreshold = cmd.CreateParameter();
            parDefaultThreshold.ParameterName = "@DefaultThreshold";
            if (!entity.IsDefaultThresholdNull)
                parDefaultThreshold.Value = entity.DefaultThreshold;
            else
                parDefaultThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultThreshold);

            System.Data.IDbDataParameter parDefaultThresholdTypeID = cmd.CreateParameter();
            parDefaultThresholdTypeID.ParameterName = "@DefaultThresholdTypeID";
            if (!entity.IsDefaultThresholdTypeIDNull)
                parDefaultThresholdTypeID.Value = entity.DefaultThresholdTypeID;
            else
                parDefaultThresholdTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultThresholdTypeID);

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

            System.Data.IDbDataParameter parIsSystemAdminAlert = cmd.CreateParameter();
            parIsSystemAdminAlert.ParameterName = "@IsSystemAdminAlert";
            if (!entity.IsIsSystemAdminAlertNull)
                parIsSystemAdminAlert.Value = entity.IsSystemAdminAlert;
            else
                parIsSystemAdminAlert.Value = System.DBNull.Value;
            cmdParams.Add(parIsSystemAdminAlert);

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
        /// in AlertMstInfo object
        /// </summary>
        /// <param name="o">A AlertMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AlertMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AlertMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlert = cmd.CreateParameter();
            parAlert.ParameterName = "@Alert";
            if (!entity.IsAlertNull)
                parAlert.Value = entity.Alert;
            else
                parAlert.Value = System.DBNull.Value;
            cmdParams.Add(parAlert);

            System.Data.IDbDataParameter parAlertGenerationLocationID = cmd.CreateParameter();
            parAlertGenerationLocationID.ParameterName = "@AlertGenerationLocationID";
            if (!entity.IsAlertGenerationLocationIDNull)
                parAlertGenerationLocationID.Value = entity.AlertGenerationLocationID;
            else
                parAlertGenerationLocationID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertGenerationLocationID);

            System.Data.IDbDataParameter parAlertLabelID = cmd.CreateParameter();
            parAlertLabelID.ParameterName = "@AlertLabelID";
            if (!entity.IsAlertLabelIDNull)
                parAlertLabelID.Value = entity.AlertLabelID;
            else
                parAlertLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertLabelID);

            System.Data.IDbDataParameter parAlertResponseTypeID = cmd.CreateParameter();
            parAlertResponseTypeID.ParameterName = "@AlertResponseTypeID";
            if (!entity.IsAlertResponseTypeIDNull)
                parAlertResponseTypeID.Value = entity.AlertResponseTypeID;
            else
                parAlertResponseTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertResponseTypeID);

            System.Data.IDbDataParameter parAlertTypeID = cmd.CreateParameter();
            parAlertTypeID.ParameterName = "@AlertTypeID";
            if (!entity.IsAlertTypeIDNull)
                parAlertTypeID.Value = entity.AlertTypeID;
            else
                parAlertTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parAlertTypeID);

            System.Data.IDbDataParameter parBaseUrl = cmd.CreateParameter();
            parBaseUrl.ParameterName = "@BaseUrl";
            if (!entity.IsBaseUrlNull)
                parBaseUrl.Value = entity.BaseUrl;
            else
                parBaseUrl.Value = System.DBNull.Value;
            cmdParams.Add(parBaseUrl);

            System.Data.IDbDataParameter parCapabilityID = cmd.CreateParameter();
            parCapabilityID.ParameterName = "@CapabilityID";
            if (!entity.IsCapabilityIDNull)
                parCapabilityID.Value = entity.CapabilityID;
            else
                parCapabilityID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityID);

            System.Data.IDbDataParameter parCondition = cmd.CreateParameter();
            parCondition.ParameterName = "@Condition";
            if (!entity.IsConditionNull)
                parCondition.Value = entity.Condition;
            else
                parCondition.Value = System.DBNull.Value;
            cmdParams.Add(parCondition);

            System.Data.IDbDataParameter parConditionLabelID = cmd.CreateParameter();
            parConditionLabelID.ParameterName = "@ConditionLabelID";
            if (!entity.IsConditionLabelIDNull)
                parConditionLabelID.Value = entity.ConditionLabelID;
            else
                parConditionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parConditionLabelID);

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

            System.Data.IDbDataParameter parDefaultThreshold = cmd.CreateParameter();
            parDefaultThreshold.ParameterName = "@DefaultThreshold";
            if (!entity.IsDefaultThresholdNull)
                parDefaultThreshold.Value = entity.DefaultThreshold;
            else
                parDefaultThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultThreshold);

            System.Data.IDbDataParameter parDefaultThresholdTypeID = cmd.CreateParameter();
            parDefaultThresholdTypeID.ParameterName = "@DefaultThresholdTypeID";
            if (!entity.IsDefaultThresholdTypeIDNull)
                parDefaultThresholdTypeID.Value = entity.DefaultThresholdTypeID;
            else
                parDefaultThresholdTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultThresholdTypeID);

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

            System.Data.IDbDataParameter parIsSystemAdminAlert = cmd.CreateParameter();
            parIsSystemAdminAlert.ParameterName = "@IsSystemAdminAlert";
            if (!entity.IsIsSystemAdminAlertNull)
                parIsSystemAdminAlert.Value = entity.IsSystemAdminAlert;
            else
                parIsSystemAdminAlert.Value = System.DBNull.Value;
            cmdParams.Add(parIsSystemAdminAlert);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparAlertID = cmd.CreateParameter();
            pkparAlertID.ParameterName = "@AlertID";
            pkparAlertID.Value = entity.AlertID;
            cmdParams.Add(pkparAlertID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AlertMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AlertMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertID";
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
        public IList<AlertMstInfo> SelectAllByAlertTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AlertMstByAlertTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertTypeID";
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
        public IList<AlertMstInfo> SelectAllByDefaultThresholdTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AlertMstByDefaultThresholdTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DefaultThresholdTypeID";
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
        public IList<AlertMstInfo> SelectAllByAlertResponseTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AlertMstByAlertResponseTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertResponseTypeID";
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
        public IList<AlertMstInfo> SelectAllByAlertGenerationLocationID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AlertMstByAlertGenerationLocationID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertGenerationLocationID";
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
        public IList<AlertMstInfo> SelectAllByCapabilityID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AlertMstByCapabilityID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }











    }
}
