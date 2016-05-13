

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

    public abstract class MenuMstDAOBase : CustomAbstractDAO<MenuMstInfo>
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
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column Menu
        /// </summary>
        public static readonly string COLUMN_MENU = "Menu";
        /// <summary>
        /// A static representation of column MenuID
        /// </summary>
        public static readonly string COLUMN_MENUID = "MenuID";
        /// <summary>
        /// A static representation of column MenuLabelID
        /// </summary>
        public static readonly string COLUMN_MENULABELID = "MenuLabelID";
        /// <summary>
        /// A static representation of column ParentMenuID
        /// </summary>
        public static readonly string COLUMN_PARENTMENUID = "ParentMenuID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MenuID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MenuID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MenuMst";

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
        public MenuMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MenuMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MenuMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MenuMstInfo</returns>
        protected override MenuMstInfo MapObject(System.Data.IDataReader r)
        {
            MenuMstInfo entity = new MenuMstInfo();
            entity.MenuID = r.GetInt16Value("MenuID");
            entity.MenuKey = r.GetStringValue("MenuKey");
            entity.Menu = r.GetStringValue("Menu");
            entity.MenuLabelID = r.GetInt32Value("MenuLabelID");
            entity.MenuURL = r.GetStringValue("MenuURL");
            entity.Description = r.GetStringValue("Description");
            entity.ParentMenuID = r.GetInt16Value("ParentMenuID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MenuMstInfo object
        /// </summary>
        /// <param name="o">A MenuMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MenuMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MenuMst");
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

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

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

            System.Data.IDbDataParameter parMenu = cmd.CreateParameter();
            parMenu.ParameterName = "@Menu";
            if (!entity.IsMenuNull)
                parMenu.Value = entity.Menu;
            else
                parMenu.Value = System.DBNull.Value;
            cmdParams.Add(parMenu);

            System.Data.IDbDataParameter parMenuLabelID = cmd.CreateParameter();
            parMenuLabelID.ParameterName = "@MenuLabelID";
            if (!entity.IsMenuLabelIDNull)
                parMenuLabelID.Value = entity.MenuLabelID;
            else
                parMenuLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMenuLabelID);

            System.Data.IDbDataParameter parParentMenuID = cmd.CreateParameter();
            parParentMenuID.ParameterName = "@ParentMenuID";
            if (!entity.IsParentMenuIDNull)
                parParentMenuID.Value = entity.ParentMenuID;
            else
                parParentMenuID.Value = System.DBNull.Value;
            cmdParams.Add(parParentMenuID);

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
        /// in MenuMstInfo object
        /// </summary>
        /// <param name="o">A MenuMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MenuMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MenuMst");
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

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

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

            System.Data.IDbDataParameter parMenu = cmd.CreateParameter();
            parMenu.ParameterName = "@Menu";
            if (!entity.IsMenuNull)
                parMenu.Value = entity.Menu;
            else
                parMenu.Value = System.DBNull.Value;
            cmdParams.Add(parMenu);

            System.Data.IDbDataParameter parMenuLabelID = cmd.CreateParameter();
            parMenuLabelID.ParameterName = "@MenuLabelID";
            if (!entity.IsMenuLabelIDNull)
                parMenuLabelID.Value = entity.MenuLabelID;
            else
                parMenuLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMenuLabelID);

            System.Data.IDbDataParameter parParentMenuID = cmd.CreateParameter();
            parParentMenuID.ParameterName = "@ParentMenuID";
            if (!entity.IsParentMenuIDNull)
                parParentMenuID.Value = entity.ParentMenuID;
            else
                parParentMenuID.Value = System.DBNull.Value;
            cmdParams.Add(parParentMenuID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMenuID = cmd.CreateParameter();
            pkparMenuID.ParameterName = "@MenuID";
            pkparMenuID.Value = entity.MenuID;
            cmdParams.Add(pkparMenuID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MenuMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MenuID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MenuMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MenuID";
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
        public IList<MenuMstInfo> SelectAllByParentMenuID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MenuMstByParentMenuID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ParentMenuID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MenuMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MenuMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MenuMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MenuMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MenuMstInfo entity, object id)
        {
            entity.MenuID = Convert.ToInt16(id);
        }










        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MenuMstInfo> SelectMenuMstDetailsAssociatedToCapabilityMstByMenuCapability(CapabilityMstInfo entity)
        {
            return this.SelectMenuMstDetailsAssociatedToCapabilityMstByMenuCapability(entity.CapabilityID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MenuMstInfo> SelectMenuMstDetailsAssociatedToCapabilityMstByMenuCapability(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MenuMst] INNER JOIN [MenuCapability] ON [MenuMst].[MenuID] = [MenuCapability].[MenuID] INNER JOIN [CapabilityMst] ON [MenuCapability].[CapabilityID] = [CapabilityMst].[CapabilityID]  WHERE  [CapabilityMst].[CapabilityID] = @CapabilityID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
            par.Value = id;

            cmdParams.Add(par);
            List<MenuMstInfo> objMenuMstEntityColl = new List<MenuMstInfo>(this.Select(cmd));
            return objMenuMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MenuMstInfo> SelectMenuMstDetailsAssociatedToRoleMstByMenuRole(RoleMstInfo entity)
        {
            return this.SelectMenuMstDetailsAssociatedToRoleMstByMenuRole(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MenuMstInfo> SelectMenuMstDetailsAssociatedToRoleMstByMenuRole(short? id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_MenuMstByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<MenuMstInfo> objMenuMstEntityColl = new List<MenuMstInfo>(this.Select(cmd));
            return objMenuMstEntityColl;
        }

    }
}
