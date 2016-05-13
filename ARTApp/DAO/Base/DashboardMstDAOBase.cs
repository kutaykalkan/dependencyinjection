

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

    public abstract class DashboardMstDAOBase : CustomAbstractDAO<DashboardMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DashboardID
        /// </summary>
        public static readonly string COLUMN_DASHBOARDID = "DashboardID";
        /// <summary>
        /// A static representation of column DashboardTitle
        /// </summary>
        public static readonly string COLUMN_DASHBOARDTITLE = "DashboardTitle";
        /// <summary>
        /// A static representation of column DashboardTitleLabelID
        /// </summary>
        public static readonly string COLUMN_DASHBOARDTITLELABELID = "DashboardTitleLabelID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column DescriptionLabelID
        /// </summary>
        public static readonly string COLUMN_DESCRIPTIONLABELID = "DescriptionLabelID";
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
        /// Provides access to the name of the primary key column (DashboardID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DashboardID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DashboardMst";

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
        public DashboardMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DashboardMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DashboardMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DashboardMstInfo</returns>
        protected override DashboardMstInfo MapObject(System.Data.IDataReader r)
        {
            DashboardMstInfo entity = new DashboardMstInfo();

            entity.DashboardID = r.GetInt16Value("DashboardID");
            entity.DashboardTitle = r.GetStringValue("DashboardTitle");
            entity.DashboardTitleLabelID = r.GetInt32Value("DashboardTitleLabelID");
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            entity.UserControlUrl = r.GetStringValue("UserControlUrl");
            entity.CapabilityID = r.GetInt16Value("CapabilityID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in DashboardMstInfo object
        /// </summary>
        /// <param name="o">A DashboardMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DashboardMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DashboardMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDashboardTitle = cmd.CreateParameter();
            parDashboardTitle.ParameterName = "@DashboardTitle";
            if (!entity.IsDashboardTitleNull)
                parDashboardTitle.Value = entity.DashboardTitle;
            else
                parDashboardTitle.Value = System.DBNull.Value;
            cmdParams.Add(parDashboardTitle);

            System.Data.IDbDataParameter parDashboardTitleLabelID = cmd.CreateParameter();
            parDashboardTitleLabelID.ParameterName = "@DashboardTitleLabelID";
            if (!entity.IsDashboardTitleLabelIDNull)
                parDashboardTitleLabelID.Value = entity.DashboardTitleLabelID;
            else
                parDashboardTitleLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDashboardTitleLabelID);

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

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (!entity.IsDescriptionLabelIDNull)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

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
        /// in DashboardMstInfo object
        /// </summary>
        /// <param name="o">A DashboardMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DashboardMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DashboardMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDashboardTitle = cmd.CreateParameter();
            parDashboardTitle.ParameterName = "@DashboardTitle";
            if (!entity.IsDashboardTitleNull)
                parDashboardTitle.Value = entity.DashboardTitle;
            else
                parDashboardTitle.Value = System.DBNull.Value;
            cmdParams.Add(parDashboardTitle);

            System.Data.IDbDataParameter parDashboardTitleLabelID = cmd.CreateParameter();
            parDashboardTitleLabelID.ParameterName = "@DashboardTitleLabelID";
            if (!entity.IsDashboardTitleLabelIDNull)
                parDashboardTitleLabelID.Value = entity.DashboardTitleLabelID;
            else
                parDashboardTitleLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDashboardTitleLabelID);

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

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (!entity.IsDescriptionLabelIDNull)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

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

            System.Data.IDbDataParameter pkparDashboardID = cmd.CreateParameter();
            pkparDashboardID.ParameterName = "@DashboardID";
            pkparDashboardID.Value = entity.DashboardID;
            cmdParams.Add(pkparDashboardID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DashboardMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DashboardID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DashboardMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DashboardID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(DashboardMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DashboardMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(DashboardMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DashboardMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(DashboardMstInfo entity, object id)
        {
            entity.DashboardID = Convert.ToInt16(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DashboardMstInfo> SelectDashboardMstDetailsAssociatedToRoleMstByDashboardRole(RoleMstInfo entity)
        {
            return this.SelectDashboardMstDetailsAssociatedToRoleMstByDashboardRole(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DashboardMstInfo> SelectDashboardMstDetailsAssociatedToRoleMstByDashboardRole(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DashboardMst] INNER JOIN [DashboardRole] ON [DashboardMst].[DashboardID] = [DashboardRole].[DashboardID] INNER JOIN [RoleMst] ON [DashboardRole].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<DashboardMstInfo> objDashboardMstEntityColl = new List<DashboardMstInfo>(this.Select(cmd));
            return objDashboardMstEntityColl;
        }

    }
}
