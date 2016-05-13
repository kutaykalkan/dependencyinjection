

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

    public abstract class MatchingSourceDataImportHdrDAOBase : CustomAbstractDAO<MatchingSourceDataImportHdrInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DataImportStatusID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTSTATUSID = "DataImportStatusID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column FileName
        /// </summary>
        public static readonly string COLUMN_FILENAME = "FileName";
        /// <summary>
        /// A static representation of column FileSize
        /// </summary>
        public static readonly string COLUMN_FILESIZE = "FileSize";
        /// <summary>
        /// A static representation of column ForceCommitDate
        /// </summary>
        public static readonly string COLUMN_FORCECOMMITDATE = "ForceCommitDate";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsForceCommit
        /// </summary>
        public static readonly string COLUMN_ISFORCECOMMIT = "IsForceCommit";
        /// <summary>
        /// A static representation of column LanguageID
        /// </summary>
        public static readonly string COLUMN_LANGUAGEID = "LanguageID";
        /// <summary>
        /// A static representation of column MatchingSourceDataImportID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCEDATAIMPORTID = "MatchingSourceDataImportID";
        /// <summary>
        /// A static representation of column MatchingSourceName
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCENAME = "MatchingSourceName";
        /// <summary>
        /// A static representation of column MatchingSourceTypeID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCETYPEID = "MatchingSourceTypeID";
        /// <summary>
        /// A static representation of column Message
        /// </summary>
        public static readonly string COLUMN_MESSAGE = "Message";
        /// <summary>
        /// A static representation of column PhysicalPath
        /// </summary>
        public static readonly string COLUMN_PHYSICALPATH = "PhysicalPath";
        /// <summary>
        /// A static representation of column RecordsImported
        /// </summary>
        public static readonly string COLUMN_RECORDSIMPORTED = "RecordsImported";
        /// <summary>
        /// A static representation of column RecPeriodID
        /// </summary>
        public static readonly string COLUMN_RECPERIODID = "RecPeriodID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchingSourceDataImportID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchingSourceDataImportID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingSourceDataImportHdr";

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
        public MatchingSourceDataImportHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingSourceDataImportHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingSourceDataImportHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingSourceDataImportHdrInfo</returns>
        protected override MatchingSourceDataImportHdrInfo MapObject(System.Data.IDataReader r)
        {

            MatchingSourceDataImportHdrInfo entity = new MatchingSourceDataImportHdrInfo();

            entity.MatchingSourceDataImportID = r.GetInt64Value("MatchingSourceDataImportID");
            entity.MatchingSourceName = r.GetStringValue("MatchingSourceName");
            entity.FileName = r.GetStringValue("FileName");
            entity.PhysicalPath = r.GetStringValue("PhysicalPath");
            entity.FileSize = r.GetDecimalValue("FileSize");
            entity.MatchingSourceTypeID = r.GetInt16Value("MatchingSourceTypeID");
            entity.RecPeriodID = r.GetInt32Value("RecPeriodID");
            entity.DataImportStatusID = r.GetInt16Value("DataImportStatusID");
            entity.RecordsImported = r.GetInt32Value("RecordsImported");
            entity.IsForceCommit = r.GetBooleanValue("IsForceCommit");
            entity.ForceCommitDate = r.GetDateValue("ForceCommitDate");
            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.LanguageID = r.GetInt32Value("LanguageID");
            entity.Message = r.GetStringValue("Message");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingSourceDataImportHdrInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceDataImportHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingSourceDataImportHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingSourceDataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportStatusID = cmd.CreateParameter();
            parDataImportStatusID.ParameterName = "@DataImportStatusID";
            if (entity != null)
                parDataImportStatusID.Value = entity.DataImportStatusID;
            else
                parDataImportStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatusID);

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

            System.Data.IDbDataParameter parFileName = cmd.CreateParameter();
            parFileName.ParameterName = "@FileName";
            if (entity != null)
                parFileName.Value = entity.FileName;
            else
                parFileName.Value = System.DBNull.Value;
            cmdParams.Add(parFileName);

            System.Data.IDbDataParameter parFileSize = cmd.CreateParameter();
            parFileSize.ParameterName = "@FileSize";
            if (entity != null)
                parFileSize.Value = entity.FileSize;
            else
                parFileSize.Value = System.DBNull.Value;
            cmdParams.Add(parFileSize);

            System.Data.IDbDataParameter parForceCommitDate = cmd.CreateParameter();
            parForceCommitDate.ParameterName = "@ForceCommitDate";
            if (entity != null)
                parForceCommitDate.Value = entity.ForceCommitDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parForceCommitDate.Value = System.DBNull.Value;
            cmdParams.Add(parForceCommitDate);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsForceCommit = cmd.CreateParameter();
            parIsForceCommit.ParameterName = "@IsForceCommit";
            if (entity != null)
                parIsForceCommit.Value = entity.IsForceCommit;
            else
                parIsForceCommit.Value = System.DBNull.Value;
            cmdParams.Add(parIsForceCommit);

            System.Data.IDbDataParameter parLanguageID = cmd.CreateParameter();
            parLanguageID.ParameterName = "@LanguageID";
            if (entity != null)
                parLanguageID.Value = entity.LanguageID;
            else
                parLanguageID.Value = System.DBNull.Value;
            cmdParams.Add(parLanguageID);

            System.Data.IDbDataParameter parMatchingSourceName = cmd.CreateParameter();
            parMatchingSourceName.ParameterName = "@MatchingSourceName";
            if (entity != null)
                parMatchingSourceName.Value = entity.MatchingSourceName;
            else
                parMatchingSourceName.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceName);

            System.Data.IDbDataParameter parMatchingSourceTypeID = cmd.CreateParameter();
            parMatchingSourceTypeID.ParameterName = "@MatchingSourceTypeID";
            if (entity != null)
                parMatchingSourceTypeID.Value = entity.MatchingSourceTypeID;
            else
                parMatchingSourceTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceTypeID);

            System.Data.IDbDataParameter parMessage = cmd.CreateParameter();
            parMessage.ParameterName = "@Message";
            if (entity != null)
                parMessage.Value = entity.Message;
            else
                parMessage.Value = System.DBNull.Value;
            cmdParams.Add(parMessage);

            System.Data.IDbDataParameter parPhysicalPath = cmd.CreateParameter();
            parPhysicalPath.ParameterName = "@PhysicalPath";
            if (entity != null)
                parPhysicalPath.Value = entity.PhysicalPath;
            else
                parPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parPhysicalPath);

            System.Data.IDbDataParameter parRecordsImported = cmd.CreateParameter();
            parRecordsImported.ParameterName = "@RecordsImported";
            if (entity != null)
                parRecordsImported.Value = entity.RecordsImported;
            else
                parRecordsImported.Value = System.DBNull.Value;
            cmdParams.Add(parRecordsImported);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (entity != null)
                parRecPeriodID.Value = entity.RecPeriodID;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity != null)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (entity != null)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchingSourceDataImportHdrInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceDataImportHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingSourceDataImportHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingSourceDataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportStatusID = cmd.CreateParameter();
            parDataImportStatusID.ParameterName = "@DataImportStatusID";
            if (entity != null)
                parDataImportStatusID.Value = entity.DataImportStatusID;
            else
                parDataImportStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatusID);

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

            System.Data.IDbDataParameter parFileName = cmd.CreateParameter();
            parFileName.ParameterName = "@FileName";
            if (entity != null)
                parFileName.Value = entity.FileName;
            else
                parFileName.Value = System.DBNull.Value;
            cmdParams.Add(parFileName);

            System.Data.IDbDataParameter parFileSize = cmd.CreateParameter();
            parFileSize.ParameterName = "@FileSize";
            if (entity != null)
                parFileSize.Value = entity.FileSize;
            else
                parFileSize.Value = System.DBNull.Value;
            cmdParams.Add(parFileSize);

            System.Data.IDbDataParameter parForceCommitDate = cmd.CreateParameter();
            parForceCommitDate.ParameterName = "@ForceCommitDate";
            if (entity != null)
                parForceCommitDate.Value = entity.ForceCommitDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parForceCommitDate.Value = System.DBNull.Value;
            cmdParams.Add(parForceCommitDate);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsForceCommit = cmd.CreateParameter();
            parIsForceCommit.ParameterName = "@IsForceCommit";
            if (entity != null)
                parIsForceCommit.Value = entity.IsForceCommit;
            else
                parIsForceCommit.Value = System.DBNull.Value;
            cmdParams.Add(parIsForceCommit);

            System.Data.IDbDataParameter parLanguageID = cmd.CreateParameter();
            parLanguageID.ParameterName = "@LanguageID";
            if (entity != null)
                parLanguageID.Value = entity.LanguageID;
            else
                parLanguageID.Value = System.DBNull.Value;
            cmdParams.Add(parLanguageID);

            System.Data.IDbDataParameter parMatchingSourceName = cmd.CreateParameter();
            parMatchingSourceName.ParameterName = "@MatchingSourceName";
            if (entity != null)
                parMatchingSourceName.Value = entity.MatchingSourceName;
            else
                parMatchingSourceName.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceName);

            System.Data.IDbDataParameter parMatchingSourceTypeID = cmd.CreateParameter();
            parMatchingSourceTypeID.ParameterName = "@MatchingSourceTypeID";
            if (entity != null)
                parMatchingSourceTypeID.Value = entity.MatchingSourceTypeID;
            else
                parMatchingSourceTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceTypeID);

            System.Data.IDbDataParameter parMessage = cmd.CreateParameter();
            parMessage.ParameterName = "@Message";
            if (entity != null)
                parMessage.Value = entity.Message;
            else
                parMessage.Value = System.DBNull.Value;
            cmdParams.Add(parMessage);

            System.Data.IDbDataParameter parPhysicalPath = cmd.CreateParameter();
            parPhysicalPath.ParameterName = "@PhysicalPath";
            if (entity != null)
                parPhysicalPath.Value = entity.PhysicalPath;
            else
                parPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parPhysicalPath);

            System.Data.IDbDataParameter parRecordsImported = cmd.CreateParameter();
            parRecordsImported.ParameterName = "@RecordsImported";
            if (entity != null)
                parRecordsImported.Value = entity.RecordsImported;
            else
                parRecordsImported.Value = System.DBNull.Value;
            cmdParams.Add(parRecordsImported);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (entity != null)
                parRecPeriodID.Value = entity.RecPeriodID;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity != null)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (entity != null)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter pkparMatchingSourceDataImportID = cmd.CreateParameter();
            pkparMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
            pkparMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            cmdParams.Add(pkparMatchingSourceDataImportID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingSourceDataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceDataImportID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingSourceDataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceDataImportID";
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
        public IList<MatchingSourceDataImportHdrInfo> SelectAllByMatchingSourceTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceDataImportHdrByMatchingSourceTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceTypeID";
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
        public IList<MatchingSourceDataImportHdrInfo> SelectAllByRecPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceDataImportHdrByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecPeriodID";
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
        public IList<MatchingSourceDataImportHdrInfo> SelectAllByDataImportStatusID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceDataImportHdrByDataImportStatusID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportStatusID";
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
        public IList<MatchingSourceDataImportHdrInfo> SelectAllByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceDataImportHdrByUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
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
        public IList<MatchingSourceDataImportHdrInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceDataImportHdrByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
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
        public IList<MatchingSourceDataImportHdrInfo> SelectAllByLanguageID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceDataImportHdrByLanguageID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@LanguageID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchingSourceDataImportHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceDataImportHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchingSourceDataImportHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceDataImportHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchingSourceDataImportHdrInfo entity, object id)
        {
            entity.MatchingSourceDataImportID = Convert.ToInt64(id);
        }











    }
}
