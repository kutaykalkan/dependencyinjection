

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model.Matching;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class RecItemColumnMstDAOBase : CustomAbstractDAO<RecItemColumnMstInfo>
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
        /// A static representation of column RecItemColumnID
        /// </summary>
        public static readonly string COLUMN_RECITEMCOLUMNID = "RecItemColumnID";
        /// <summary>
        /// A static representation of column RecItemColumnName
        /// </summary>
        public static readonly string COLUMN_RECITEMCOLUMNNAME = "RecItemColumnName";
        /// <summary>
        /// A static representation of column RecItemColumnNameLabelID
        /// </summary>
        public static readonly string COLUMN_RECITEMCOLUMNNAMELABELID = "RecItemColumnNameLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (RecItemColumnID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RecItemColumnID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RecItemColumnMst";

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
        public RecItemColumnMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RecItemColumnMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RecItemColumnMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RecItemColumnMstInfo</returns>
        protected override RecItemColumnMstInfo MapObject(System.Data.IDataReader r)
        {

            RecItemColumnMstInfo entity = new RecItemColumnMstInfo();

            entity.RecItemColumnID = r.GetInt32Value("RecItemColumnID");
            entity.RecItemColumnName = r.GetStringValue("RecItemColumnName");
            entity.RecItemColumnNameLabelID = r.GetInt32Value("RecItemColumnNameLabelID");
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
        /// in RecItemColumnMstInfo object
        /// </summary>
        /// <param name="o">A RecItemColumnMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RecItemColumnMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RecItemColumnMst");
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

            System.Data.IDbDataParameter parRecItemColumnID = cmd.CreateParameter();
            parRecItemColumnID.ParameterName = "@RecItemColumnID";
            if (entity != null)
                parRecItemColumnID.Value = entity.RecItemColumnID;
            else
                parRecItemColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parRecItemColumnID);

            System.Data.IDbDataParameter parRecItemColumnName = cmd.CreateParameter();
            parRecItemColumnName.ParameterName = "@RecItemColumnName";
            if (entity != null)
                parRecItemColumnName.Value = entity.RecItemColumnName;
            else
                parRecItemColumnName.Value = System.DBNull.Value;
            cmdParams.Add(parRecItemColumnName);

            System.Data.IDbDataParameter parRecItemColumnNameLabelID = cmd.CreateParameter();
            parRecItemColumnNameLabelID.ParameterName = "@RecItemColumnNameLabelID";
            if (entity != null)
                parRecItemColumnNameLabelID.Value = entity.RecItemColumnNameLabelID;
            else
                parRecItemColumnNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parRecItemColumnNameLabelID);

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
        /// in RecItemColumnMstInfo object
        /// </summary>
        /// <param name="o">A RecItemColumnMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RecItemColumnMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RecItemColumnMst");
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

            System.Data.IDbDataParameter parRecItemColumnName = cmd.CreateParameter();
            parRecItemColumnName.ParameterName = "@RecItemColumnName";
            if (entity != null)
                parRecItemColumnName.Value = entity.RecItemColumnName;
            else
                parRecItemColumnName.Value = System.DBNull.Value;
            cmdParams.Add(parRecItemColumnName);

            System.Data.IDbDataParameter parRecItemColumnNameLabelID = cmd.CreateParameter();
            parRecItemColumnNameLabelID.ParameterName = "@RecItemColumnNameLabelID";
            if (entity != null)
                parRecItemColumnNameLabelID.Value = entity.RecItemColumnNameLabelID;
            else
                parRecItemColumnNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parRecItemColumnNameLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparRecItemColumnID = cmd.CreateParameter();
            pkparRecItemColumnID.ParameterName = "@RecItemColumnID";
            pkparRecItemColumnID.Value = entity.RecItemColumnID;
            cmdParams.Add(pkparRecItemColumnID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RecItemColumnMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecItemColumnID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RecItemColumnMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecItemColumnID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }












        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RecItemColumnMstInfo> SelectRecItemColumnMstDetailsAssociatedToMatchingConfigurationByMatchingConfigurationRecItemColumn(MatchingConfigurationInfo entity)
        {
            return this.SelectRecItemColumnMstDetailsAssociatedToMatchingConfigurationByMatchingConfigurationRecItemColumn(entity.MatchingConfigurationID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RecItemColumnMstInfo> SelectRecItemColumnMstDetailsAssociatedToMatchingConfigurationByMatchingConfigurationRecItemColumn(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RecItemColumnMst] INNER JOIN [MatchingConfigurationRecItemColumn] ON [RecItemColumnMst].[RecItemColumnID] = [MatchingConfigurationRecItemColumn].[RecItemColumnID] INNER JOIN [MatchingConfiguration] ON [MatchingConfigurationRecItemColumn].[MatchingConfigurationID] = [MatchingConfiguration].[MachingConfigurationID]  WHERE  [MatchingConfiguration].[MachingConfigurationID] = @MachingConfigurationID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MachingConfigurationID";
            par.Value = id;

            cmdParams.Add(par);
            List<RecItemColumnMstInfo> objRecItemColumnMstEntityColl = new List<RecItemColumnMstInfo>(this.Select(cmd));
            return objRecItemColumnMstEntityColl;
        }

    }
}
