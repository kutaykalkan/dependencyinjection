

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

    public abstract class PackageMstDAOBase : CustomAbstractDAO<PackageMstInfo>
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
        /// A static representation of column DefaultDiskSpace
        /// </summary>
        public static readonly string COLUMN_DEFAULTDISKSPACE = "DefaultDiskSpace";
        /// <summary>
        /// A static representation of column DefaultNumberOfUsers
        /// </summary>
        public static readonly string COLUMN_DEFAULTNUMBEROFUSERS = "DefaultNumberOfUsers";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column PackageDescription
        /// </summary>
        public static readonly string COLUMN_PACKAGEDESCRIPTION = "PackageDescription";
        /// <summary>
        /// A static representation of column PackageDescriptionLabelID
        /// </summary>
        public static readonly string COLUMN_PACKAGEDESCRIPTIONLABELID = "PackageDescriptionLabelID";
        /// <summary>
        /// A static representation of column PackageID
        /// </summary>
        public static readonly string COLUMN_PACKAGEID = "PackageID";
        /// <summary>
        /// A static representation of column PackageName
        /// </summary>
        public static readonly string COLUMN_PACKAGENAME = "PackageName";
        /// <summary>
        /// A static representation of column PackageNameLabelID
        /// </summary>
        public static readonly string COLUMN_PACKAGENAMELABELID = "PackageNameLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (PackageID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "PackageID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "PackageMst";

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
        public PackageMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "PackageMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a PackageMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>PackageMstInfo</returns>
        protected override PackageMstInfo MapObject(System.Data.IDataReader r)
        {

            PackageMstInfo entity = new PackageMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("PackageID");
                if (!r.IsDBNull(ordinal)) entity.PackageID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PackageName");
                if (!r.IsDBNull(ordinal)) entity.PackageName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PackageNameLabelID");
                if (!r.IsDBNull(ordinal)) entity.PackageNameLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PackageDescription");
                if (!r.IsDBNull(ordinal)) entity.PackageDescription = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PackageDescriptionLabelID");
                if (!r.IsDBNull(ordinal)) entity.PackageDescriptionLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DefaultDiskSpace");
                if (!r.IsDBNull(ordinal)) entity.DefaultDiskSpace = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DefaultNumberOfUsers");
                if (!r.IsDBNull(ordinal)) entity.DefaultNumberOfUsers = ((System.Int16)(r.GetValue(ordinal)));
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
        /// in PackageMstInfo object
        /// </summary>
        /// <param name="o">A PackageMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(PackageMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_PackageMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

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

            System.Data.IDbDataParameter parDefaultDiskSpace = cmd.CreateParameter();
            parDefaultDiskSpace.ParameterName = "@DefaultDiskSpace";
            if (entity != null)
                parDefaultDiskSpace.Value = entity.DefaultDiskSpace;
            else
                parDefaultDiskSpace.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultDiskSpace);

            System.Data.IDbDataParameter parDefaultNumberOfUsers = cmd.CreateParameter();
            parDefaultNumberOfUsers.ParameterName = "@DefaultNumberOfUsers";
            if (entity != null)
                parDefaultNumberOfUsers.Value = entity.DefaultNumberOfUsers;
            else
                parDefaultNumberOfUsers.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultNumberOfUsers);

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

            System.Data.IDbDataParameter parPackageDescription = cmd.CreateParameter();
            parPackageDescription.ParameterName = "@PackageDescription";
            if (entity != null)
                parPackageDescription.Value = entity.PackageDescription;
            else
                parPackageDescription.Value = System.DBNull.Value;
            cmdParams.Add(parPackageDescription);

            System.Data.IDbDataParameter parPackageDescriptionLabelID = cmd.CreateParameter();
            parPackageDescriptionLabelID.ParameterName = "@PackageDescriptionLabelID";
            if (entity != null)
                parPackageDescriptionLabelID.Value = entity.PackageDescriptionLabelID;
            else
                parPackageDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageDescriptionLabelID);

            System.Data.IDbDataParameter parPackageName = cmd.CreateParameter();
            parPackageName.ParameterName = "@PackageName";
            if (entity != null)
                parPackageName.Value = entity.PackageName;
            else
                parPackageName.Value = System.DBNull.Value;
            cmdParams.Add(parPackageName);

            System.Data.IDbDataParameter parPackageNameLabelID = cmd.CreateParameter();
            parPackageNameLabelID.ParameterName = "@PackageNameLabelID";
            if (entity != null)
                parPackageNameLabelID.Value = entity.PackageNameLabelID;
            else
                parPackageNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageNameLabelID);

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
        /// in PackageMstInfo object
        /// </summary>
        /// <param name="o">A PackageMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(PackageMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_PackageMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

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

            System.Data.IDbDataParameter parDefaultDiskSpace = cmd.CreateParameter();
            parDefaultDiskSpace.ParameterName = "@DefaultDiskSpace";
            if (entity != null)
                parDefaultDiskSpace.Value = entity.DefaultDiskSpace;
            else
                parDefaultDiskSpace.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultDiskSpace);

            System.Data.IDbDataParameter parDefaultNumberOfUsers = cmd.CreateParameter();
            parDefaultNumberOfUsers.ParameterName = "@DefaultNumberOfUsers";
            if (entity != null)
                parDefaultNumberOfUsers.Value = entity.DefaultNumberOfUsers;
            else
                parDefaultNumberOfUsers.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultNumberOfUsers);

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

            System.Data.IDbDataParameter parPackageDescription = cmd.CreateParameter();
            parPackageDescription.ParameterName = "@PackageDescription";
            if (entity != null)
                parPackageDescription.Value = entity.PackageDescription;
            else
                parPackageDescription.Value = System.DBNull.Value;
            cmdParams.Add(parPackageDescription);

            System.Data.IDbDataParameter parPackageDescriptionLabelID = cmd.CreateParameter();
            parPackageDescriptionLabelID.ParameterName = "@PackageDescriptionLabelID";
            if (entity != null)
                parPackageDescriptionLabelID.Value = entity.PackageDescriptionLabelID;
            else
                parPackageDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageDescriptionLabelID);

            System.Data.IDbDataParameter parPackageName = cmd.CreateParameter();
            parPackageName.ParameterName = "@PackageName";
            if (entity != null)
                parPackageName.Value = entity.PackageName;
            else
                parPackageName.Value = System.DBNull.Value;
            cmdParams.Add(parPackageName);

            System.Data.IDbDataParameter parPackageNameLabelID = cmd.CreateParameter();
            parPackageNameLabelID.ParameterName = "@PackageNameLabelID";
            if (entity != null)
                parPackageNameLabelID.Value = entity.PackageNameLabelID;
            else
                parPackageNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageNameLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparPackageID = cmd.CreateParameter();
            pkparPackageID.ParameterName = "@PackageID";
            pkparPackageID.Value = entity.PackageID;
            cmdParams.Add(pkparPackageID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_PackageMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_PackageMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(PackageMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(PackageMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(PackageMstInfo entity, object id)
        {
            entity.PackageID = Convert.ToInt16(id);
        }










        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<PackageMstInfo> SelectPackageMstDetailsAssociatedToFeatureMstByPackageFeature(FeatureMstInfo entity)
        {
            return this.SelectPackageMstDetailsAssociatedToFeatureMstByPackageFeature(entity.FeatureID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<PackageMstInfo> SelectPackageMstDetailsAssociatedToFeatureMstByPackageFeature(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [PackageMst] INNER JOIN [PackageFeature] ON [PackageMst].[PackageID] = [PackageFeature].[PackageID] INNER JOIN [FeatureMst] ON [PackageFeature].[FeatureID] = [FeatureMst].[FeatureID]  WHERE  [FeatureMst].[FeatureID] = @FeatureID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FeatureID";
            par.Value = id;

            cmdParams.Add(par);
            List<PackageMstInfo> objPackageMstEntityColl = new List<PackageMstInfo>(this.Select(cmd));
            return objPackageMstEntityColl;
        }



    }
}
