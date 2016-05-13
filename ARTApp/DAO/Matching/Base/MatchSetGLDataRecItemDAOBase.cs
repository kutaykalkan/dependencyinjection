

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.App.Utility;
using SkyStem.ART.App.DAO.Base;

namespace SkyStem.ART.App.DAO.Matching.Base
{

    public abstract class MatchSetGLDataRecItemDAOBase : CustomAbstractDAO<MatchSetGLDataRecItemInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column AddedByUserID
        /// </summary>
        public static readonly string COLUMN_ADDEDBYUSERID = "AddedByUserID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column ExcelRowNumber
        /// </summary>
        public static readonly string COLUMN_EXCELROWNUMBER = "ExcelRowNumber";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column MatchSetGLDataRecItemID
        /// </summary>
        public static readonly string COLUMN_MATCHSETGLDATARECITEMID = "MatchSetGLDataRecItemID";
        /// <summary>
        /// A static representation of column MatchSetMatchingSourceDataImportID
        /// </summary>
        public static readonly string COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORTID = "MatchSetMatchingSourceDataImportID";
        /// <summary>
        /// A static representation of column MatchSetSubSetCombinationID
        /// </summary>
        public static readonly string COLUMN_MATCHSETSUBSETCOMBINATIONID = "MatchSetSubSetCombinationID";
        /// <summary>
        /// A static representation of column RecCategoryID
        /// </summary>
        public static readonly string COLUMN_RECCATEGORYID = "RecCategoryID";
        /// <summary>
        /// A static representation of column RecItemNumber
        /// </summary>
        public static readonly string COLUMN_RECITEMNUMBER = "RecItemNumber";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchSetGLDataRecItemID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchSetGLDataRecItemID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchSetGLDataRecItem";

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
        public MatchSetGLDataRecItemDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchSetGLDataRecItem", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchSetGLDataRecItemInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchSetGLDataRecItemInfo</returns>
        protected override MatchSetGLDataRecItemInfo MapObject(System.Data.IDataReader r)
        {

            MatchSetGLDataRecItemInfo entity = new MatchSetGLDataRecItemInfo();

            entity.MatchSetGLDataRecItemID = r.GetInt64Value("MatchSetGLDataRecItemID");
            entity.MatchSetSubSetCombinationID = r.GetInt64Value("MatchSetSubSetCombinationID");
            entity.MatchSetMatchingSourceDataImportID = r.GetInt64Value("MatchSetMatchingSourceDataImportID");
            entity.ExcelRowNumber = r.GetInt32Value("ExcelRowNumber");
            entity.RecCategoryID = r.GetInt16Value("RecCategoryID");
            entity.RecItemNumber = r.GetStringValue("RecItemNumber");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchSetGLDataRecItemInfo object
        /// </summary>
        /// <param name="o">A MatchSetGLDataRecItemInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchSetGLDataRecItemInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchSetGLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            if (entity != null)
                parAddedByUserID.Value = entity.AddedByUserID;
            else
                parAddedByUserID.Value = System.DBNull.Value;
            cmdParams.Add(parAddedByUserID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value;
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value;
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parExcelRowNumber = cmd.CreateParameter();
            parExcelRowNumber.ParameterName = "@ExcelRowNumber";
            if (entity != null)
                parExcelRowNumber.Value = entity.ExcelRowNumber;
            else
                parExcelRowNumber.Value = System.DBNull.Value;
            cmdParams.Add(parExcelRowNumber);

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

            System.Data.IDbDataParameter parMatchSetMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchSetMatchingSourceDataImportID.ParameterName = "@MatchSetMatchingSourceDataImportID";
            if (entity != null)
                parMatchSetMatchingSourceDataImportID.Value = entity.MatchSetMatchingSourceDataImportID;
            else
                parMatchSetMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetMatchingSourceDataImportID);

            System.Data.IDbDataParameter parMatchSetSubSetCombinationID = cmd.CreateParameter();
            parMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            if (entity != null)
                parMatchSetSubSetCombinationID.Value = entity.MatchSetSubSetCombinationID;
            else
                parMatchSetSubSetCombinationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetSubSetCombinationID);

            System.Data.IDbDataParameter parRecCategoryID = cmd.CreateParameter();
            parRecCategoryID.ParameterName = "@RecCategoryID";
            if (entity != null)
                parRecCategoryID.Value = entity.RecCategoryID;
            else
                parRecCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryID);

            System.Data.IDbDataParameter parRecItemNumber = cmd.CreateParameter();
            parRecItemNumber.ParameterName = "@RecItemNumber";
            if (entity != null)
                parRecItemNumber.Value = entity.RecItemNumber;
            else
                parRecItemNumber.Value = System.DBNull.Value;
            cmdParams.Add(parRecItemNumber);

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
        /// in MatchSetGLDataRecItemInfo object
        /// </summary>
        /// <param name="o">A MatchSetGLDataRecItemInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchSetGLDataRecItemInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchSetGLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            if (entity != null)
                parAddedByUserID.Value = entity.AddedByUserID;
            else
                parAddedByUserID.Value = System.DBNull.Value;
            cmdParams.Add(parAddedByUserID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value;
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value;
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parExcelRowNumber = cmd.CreateParameter();
            parExcelRowNumber.ParameterName = "@ExcelRowNumber";
            if (entity != null)
                parExcelRowNumber.Value = entity.ExcelRowNumber;
            else
                parExcelRowNumber.Value = System.DBNull.Value;
            cmdParams.Add(parExcelRowNumber);

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

            System.Data.IDbDataParameter parMatchSetMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchSetMatchingSourceDataImportID.ParameterName = "@MatchSetMatchingSourceDataImportID";
            if (entity != null)
                parMatchSetMatchingSourceDataImportID.Value = entity.MatchSetMatchingSourceDataImportID;
            else
                parMatchSetMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetMatchingSourceDataImportID);

            System.Data.IDbDataParameter parMatchSetSubSetCombinationID = cmd.CreateParameter();
            parMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            if (entity != null)
                parMatchSetSubSetCombinationID.Value = entity.MatchSetSubSetCombinationID;
            else
                parMatchSetSubSetCombinationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetSubSetCombinationID);

            System.Data.IDbDataParameter parRecCategoryID = cmd.CreateParameter();
            parRecCategoryID.ParameterName = "@RecCategoryID";
            if (entity != null)
                parRecCategoryID.Value = entity.RecCategoryID;
            else
                parRecCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryID);

            System.Data.IDbDataParameter parRecItemNumber = cmd.CreateParameter();
            parRecItemNumber.ParameterName = "@RecItemNumber";
            if (entity != null)
                parRecItemNumber.Value = entity.RecItemNumber;
            else
                parRecItemNumber.Value = System.DBNull.Value;
            cmdParams.Add(parRecItemNumber);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMatchSetGLDataRecItemID = cmd.CreateParameter();
            pkparMatchSetGLDataRecItemID.ParameterName = "@MatchSetGLDataRecItemID";
            pkparMatchSetGLDataRecItemID.Value = entity.MatchSetGLDataRecItemID;
            cmdParams.Add(pkparMatchSetGLDataRecItemID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchSetGLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetGLDataRecItemID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchSetGLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetGLDataRecItemID";
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
        public IList<MatchSetGLDataRecItemInfo> SelectAllByMatchSetSubSetCombinationID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetGLDataRecItemByMatchSetSubSetCombinationID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
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
        public IList<MatchSetGLDataRecItemInfo> SelectAllByMatchSetMatchingSourceDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetGLDataRecItemByMatchSetMatchingSourceDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetMatchingSourceDataImportID";
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
        public IList<MatchSetGLDataRecItemInfo> SelectAllByRecCategoryID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetGLDataRecItemByRecCategoryID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecCategoryID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchSetGLDataRecItemInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetGLDataRecItemDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchSetGLDataRecItemInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetGLDataRecItemDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchSetGLDataRecItemInfo entity, object id)
        {
            entity.MatchSetGLDataRecItemID = Convert.ToInt64(id);
        }




    }
}
