

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

    public abstract class CapabilityMstDAOBase : CustomAbstractDAO<CapabilityMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column Capability
        /// </summary>
        public static readonly string COLUMN_CAPABILITY = "Capability";
        /// <summary>
        /// A static representation of column CapabilityID
        /// </summary>
        public static readonly string COLUMN_CAPABILITYID = "CapabilityID";
        /// <summary>
        /// A static representation of column CapabilityLabelID
        /// </summary>
        public static readonly string COLUMN_CAPABILITYLABELID = "CapabilityLabelID";
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
        /// A static representation of column IsConfigurationRequired
        /// </summary>
        public static readonly string COLUMN_ISCONFIGURATIONREQUIRED = "IsConfigurationRequired";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (CapabilityID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CapabilityID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CapabilityMst";

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
        public CapabilityMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CapabilityMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CapabilityMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CapabilityMstInfo</returns>
        protected override CapabilityMstInfo MapObject(System.Data.IDataReader r)
        {

            CapabilityMstInfo entity = new CapabilityMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("CapabilityID");
                if (!r.IsDBNull(ordinal)) entity.CapabilityID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Capability");
                if (!r.IsDBNull(ordinal)) entity.Capability = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CapabilityLabelID");
                if (!r.IsDBNull(ordinal)) entity.CapabilityLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Description");
                if (!r.IsDBNull(ordinal)) entity.Description = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsConfigurationRequired");
                if (!r.IsDBNull(ordinal)) entity.IsConfigurationRequired = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SortOrder");
                if (!r.IsDBNull(ordinal)) entity.SortOrder = ((System.Int16)(r.GetValue(ordinal)));
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
        /// in CapabilityMstInfo object
        /// </summary>
        /// <param name="o">A CapabilityMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CapabilityMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CapabilityMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCapability = cmd.CreateParameter();
            parCapability.ParameterName = "@Capability";
            if (!entity.IsCapabilityNull)
                parCapability.Value = entity.Capability;
            else
                parCapability.Value = System.DBNull.Value;
            cmdParams.Add(parCapability);

            System.Data.IDbDataParameter parCapabilityLabelID = cmd.CreateParameter();
            parCapabilityLabelID.ParameterName = "@CapabilityLabelID";
            if (!entity.IsCapabilityLabelIDNull)
                parCapabilityLabelID.Value = entity.CapabilityLabelID;
            else
                parCapabilityLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityLabelID);

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

            System.Data.IDbDataParameter parIsConfigurationRequired = cmd.CreateParameter();
            parIsConfigurationRequired.ParameterName = "@IsConfigurationRequired";
            if (!entity.IsIsConfigurationRequiredNull)
                parIsConfigurationRequired.Value = entity.IsConfigurationRequired;
            else
                parIsConfigurationRequired.Value = System.DBNull.Value;
            cmdParams.Add(parIsConfigurationRequired);

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
        /// in CapabilityMstInfo object
        /// </summary>
        /// <param name="o">A CapabilityMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CapabilityMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CapabilityMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCapability = cmd.CreateParameter();
            parCapability.ParameterName = "@Capability";
            if (!entity.IsCapabilityNull)
                parCapability.Value = entity.Capability;
            else
                parCapability.Value = System.DBNull.Value;
            cmdParams.Add(parCapability);

            System.Data.IDbDataParameter parCapabilityLabelID = cmd.CreateParameter();
            parCapabilityLabelID.ParameterName = "@CapabilityLabelID";
            if (!entity.IsCapabilityLabelIDNull)
                parCapabilityLabelID.Value = entity.CapabilityLabelID;
            else
                parCapabilityLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityLabelID);

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

            System.Data.IDbDataParameter parIsConfigurationRequired = cmd.CreateParameter();
            parIsConfigurationRequired.ParameterName = "@IsConfigurationRequired";
            if (!entity.IsIsConfigurationRequiredNull)
                parIsConfigurationRequired.Value = entity.IsConfigurationRequired;
            else
                parIsConfigurationRequired.Value = System.DBNull.Value;
            cmdParams.Add(parIsConfigurationRequired);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparCapabilityID = cmd.CreateParameter();
            pkparCapabilityID.ParameterName = "@CapabilityID";
            pkparCapabilityID.Value = entity.CapabilityID;
            cmdParams.Add(pkparCapabilityID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CapabilityMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CapabilityMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }

        private void MapIdentity(CapabilityMstInfo entity, object id)
        {
            entity.CapabilityID = Convert.ToInt16(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CapabilityMstInfo> SelectCapabilityMstDetailsAssociatedToCompanyHdrByCompanyCapability(CompanyHdrInfo entity)
        {
            return this.SelectCapabilityMstDetailsAssociatedToCompanyHdrByCompanyCapability(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CapabilityMstInfo> SelectCapabilityMstDetailsAssociatedToCompanyHdrByCompanyCapability(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CapabilityMst] INNER JOIN [CompanyCapability] ON [CapabilityMst].[CapabilityID] = [CompanyCapability].[CapabilityID] INNER JOIN [CompanyHdr] ON [CompanyCapability].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<CapabilityMstInfo> objCapabilityMstEntityColl = new List<CapabilityMstInfo>(this.Select(cmd));
            return objCapabilityMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CapabilityMstInfo> SelectCapabilityMstDetailsAssociatedToDataImportTypeMstByDataImportTypeCapability(DataImportTypeMstInfo entity)
        {
            return this.SelectCapabilityMstDetailsAssociatedToDataImportTypeMstByDataImportTypeCapability(entity.DataImportTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CapabilityMstInfo> SelectCapabilityMstDetailsAssociatedToDataImportTypeMstByDataImportTypeCapability(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CapabilityMst] INNER JOIN [DataImportTypeCapability] ON [CapabilityMst].[CapabilityID] = [DataImportTypeCapability].[CapabilityID] INNER JOIN [DataImportTypeMst] ON [DataImportTypeCapability].[DataImportTypeID] = [DataImportTypeMst].[DataImportTypeID]  WHERE  [DataImportTypeMst].[DataImportTypeID] = @DataImportTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<CapabilityMstInfo> objCapabilityMstEntityColl = new List<CapabilityMstInfo>(this.Select(cmd));
            return objCapabilityMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CapabilityMstInfo> SelectCapabilityMstDetailsAssociatedToMenuMstByMenuCapability(MenuMstInfo entity)
        {
            return this.SelectCapabilityMstDetailsAssociatedToMenuMstByMenuCapability(entity.MenuID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CapabilityMstInfo> SelectCapabilityMstDetailsAssociatedToMenuMstByMenuCapability(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CapabilityMst] INNER JOIN [MenuCapability] ON [CapabilityMst].[CapabilityID] = [MenuCapability].[CapabilityID] INNER JOIN [MenuMst] ON [MenuCapability].[MenuID] = [MenuMst].[MenuID]  WHERE  [MenuMst].[MenuID] = @MenuID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MenuID";
            par.Value = id;

            cmdParams.Add(par);
            List<CapabilityMstInfo> objCapabilityMstEntityColl = new List<CapabilityMstInfo>(this.Select(cmd));
            return objCapabilityMstEntityColl;
        }

    }
}
