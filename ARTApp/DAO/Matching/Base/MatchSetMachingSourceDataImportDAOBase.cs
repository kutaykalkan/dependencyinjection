

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching.Base
{

    public abstract class MatchSetMachingSourceDataImportDAOBase : CustomAbstractDAO<MatchSetMatchingSourceDataImportInfo>
    {

        /// <summary>
        /// A static representation of column FilterCriteria
        /// </summary>
        public static readonly string COLUMN_FILTERCRITERIA = "FilterCriteria";
        /// <summary>
        /// A static representation of column MatchingSourceDataImportID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCEDATAIMPORTID = "MatchingSourceDataImportID";
        /// <summary>
        /// A static representation of column MatchSetID
        /// </summary>
        public static readonly string COLUMN_MATCHSETID = "MatchSetID";
        /// <summary>
        /// A static representation of column MatchSetMachingSourceDataImportID
        /// </summary>
        public static readonly string COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORTID = "MatchSetMatchingSourceDataImportID";
        /// <summary>
        /// A static representation of column SubSetData
        /// </summary>
        public static readonly string COLUMN_SUBSETDATA = "SubSetData";
        /// <summary>
        /// A static representation of column SubSetName
        /// </summary>
        public static readonly string COLUMN_SUBSETNAME = "SubSetName";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchSetMachingSourceDataImportID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchSetMachingSourceDataImportID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchSetMachingSourceDataImport";

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
        public MatchSetMachingSourceDataImportDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchSetMachingSourceDataImport", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchSetMachingSourceDataImportInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchSetMachingSourceDataImportInfo</returns>
        protected override MatchSetMatchingSourceDataImportInfo MapObject(System.Data.IDataReader r)
        {

            MatchSetMatchingSourceDataImportInfo entity = new MatchSetMatchingSourceDataImportInfo();

            entity.MatchSetMatchingSourceDataImportID = r.GetInt64Value("MatchSetMatchingSourceDataImportID");
            entity.MatchSetID = r.GetInt64Value("MatchSetID");
            entity.MatchingSourceDataImportID = r.GetInt64Value("MatchingSourceDataImportID");
            entity.SubSetName = r.GetStringValue("SubSetName");
            entity.SubSetData = r.GetStringValue("SubSetData");
            entity.FilterCriteria = r.GetStringValue("FilterCriteria");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchSetMachingSourceDataImportInfo object
        /// </summary>
        /// <param name="o">A MatchSetMachingSourceDataImportInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchSetMatchingSourceDataImportInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchSetMachingSourceDataImport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parFilterCriteria = cmd.CreateParameter();
            parFilterCriteria.ParameterName = "@FilterCriteria";
            if (entity != null)
                parFilterCriteria.Value = entity.FilterCriteria;
            else
                parFilterCriteria.Value = System.DBNull.Value;
            cmdParams.Add(parFilterCriteria);

            System.Data.IDbDataParameter parMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
            if (entity != null)
                parMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            else
                parMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceDataImportID);

            System.Data.IDbDataParameter parMatchSetID = cmd.CreateParameter();
            parMatchSetID.ParameterName = "@MatchSetID";
            if (entity != null)
                parMatchSetID.Value = entity.MatchSetID;
            else
                parMatchSetID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetID);

            System.Data.IDbDataParameter parSubSetData = cmd.CreateParameter();
            parSubSetData.ParameterName = "@SubSetData";
            if (entity != null)
                parSubSetData.Value = entity.SubSetData;
            else
                parSubSetData.Value = System.DBNull.Value;
            cmdParams.Add(parSubSetData);

            System.Data.IDbDataParameter parSubSetName = cmd.CreateParameter();
            parSubSetName.ParameterName = "@SubSetName";
            if (entity != null)
                parSubSetName.Value = entity.SubSetName;
            else
                parSubSetName.Value = System.DBNull.Value;
            cmdParams.Add(parSubSetName);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchSetMachingSourceDataImportInfo object
        /// </summary>
        /// <param name="o">A MatchSetMachingSourceDataImportInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchSetMatchingSourceDataImportInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchSetMachingSourceDataImport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parFilterCriteria = cmd.CreateParameter();
            parFilterCriteria.ParameterName = "@FilterCriteria";
            if (entity != null)
                parFilterCriteria.Value = entity.FilterCriteria;
            else
                parFilterCriteria.Value = System.DBNull.Value;
            cmdParams.Add(parFilterCriteria);

            System.Data.IDbDataParameter parMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
            if (entity != null)
                parMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            else
                parMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceDataImportID);

            System.Data.IDbDataParameter parMatchSetID = cmd.CreateParameter();
            parMatchSetID.ParameterName = "@MatchSetID";
            if (entity != null)
                parMatchSetID.Value = entity.MatchSetID;
            else
                parMatchSetID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetID);

            System.Data.IDbDataParameter parSubSetData = cmd.CreateParameter();
            parSubSetData.ParameterName = "@SubSetData";
            if (entity != null)
                parSubSetData.Value = entity.SubSetData;
            else
                parSubSetData.Value = System.DBNull.Value;
            cmdParams.Add(parSubSetData);

            System.Data.IDbDataParameter parSubSetName = cmd.CreateParameter();
            parSubSetName.ParameterName = "@SubSetName";
            if (entity != null)
                parSubSetName.Value = entity.SubSetName;
            else
                parSubSetName.Value = System.DBNull.Value;
            cmdParams.Add(parSubSetName);

            System.Data.IDbDataParameter pkparMatchSetMachingSourceDataImportID = cmd.CreateParameter();
            pkparMatchSetMachingSourceDataImportID.ParameterName = "@MatchSetMachingSourceDataImportID";
            pkparMatchSetMachingSourceDataImportID.Value = entity.MatchSetMatchingSourceDataImportID;
            cmdParams.Add(pkparMatchSetMachingSourceDataImportID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchSetMachingSourceDataImport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetMachingSourceDataImportID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchSetMachingSourceDataImport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetMachingSourceDataImportID";
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
        public IList<MatchSetMatchingSourceDataImportInfo> SelectAllByMatchSetID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetMachingSourceDataImportByMatchSetID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetID";
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
        public IList<MatchSetMatchingSourceDataImportInfo> SelectAllByMatchingSourceDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetMachingSourceDataImportByMatchingSourceDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceDataImportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchSetMatchingSourceDataImportInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetMachingSourceDataImportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchSetMatchingSourceDataImportInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetMachingSourceDataImportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchSetMatchingSourceDataImportInfo entity, object id)
        {
            entity.MatchSetMatchingSourceDataImportID = Convert.ToInt64(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchSetMatchingSourceDataImportInfo> SelectMatchSetMachingSourceDataImportDetailsAssociatedToMatchSetMachingSourceDataImportByMatchSetSubSetCombination(MatchSetMatchingSourceDataImportInfo entity)
        {
            return this.SelectMatchSetMachingSourceDataImportDetailsAssociatedToMatchSetMachingSourceDataImportByMatchSetSubSetCombination(entity.MatchSetMatchingSourceDataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchSetMatchingSourceDataImportInfo> SelectMatchSetMachingSourceDataImportDetailsAssociatedToMatchSetMachingSourceDataImportByMatchSetSubSetCombination(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchSetMachingSourceDataImport] INNER JOIN [MatchSetSubSetCombination] ON [MatchSetMachingSourceDataImport].[MatchSetMachingSourceDataImportID] = [MatchSetSubSetCombination].[MatchSetMatchingSourceDataImport1ID] INNER JOIN [MatchSetMachingSourceDataImport] ON [MatchSetSubSetCombination].[MatchSetMatchingSourceDataImport2ID] = [MatchSetMachingSourceDataImport].[MatchSetMachingSourceDataImportID]  WHERE  [MatchSetMachingSourceDataImport].[MatchSetMachingSourceDataImportID] = @MatchSetMachingSourceDataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetMachingSourceDataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchSetMatchingSourceDataImportInfo> objMatchSetMatchingSourceDataImportEntityColl = new List<MatchSetMatchingSourceDataImportInfo>(this.Select(cmd));
            return objMatchSetMatchingSourceDataImportEntityColl;
        }

    }
}
