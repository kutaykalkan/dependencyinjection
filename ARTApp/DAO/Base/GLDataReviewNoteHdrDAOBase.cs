

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

    public abstract class GLDataReviewNoteHdrDAOBase : CustomAbstractDAO<GLDataReviewNoteHdrInfo>
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
        /// A static representation of column DeleteAfterCertification
        /// </summary>
        public static readonly string COLUMN_DELETEAFTERCERTIFICATION = "DeleteAfterCertification";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column GLDataReviewNoteID
        /// </summary>
        public static readonly string COLUMN_GLDATAREVIEWNOTEID = "GLDataReviewNoteID";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column ReviewNoteSubject
        /// </summary>
        public static readonly string COLUMN_REVIEWNOTESUBJECT = "ReviewNoteSubject";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataReviewNoteID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataReviewNoteID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataReviewNoteHdr";

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
        public GLDataReviewNoteHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataReviewNoteHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataReviewNoteHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataReviewNoteHdrInfo</returns>
        protected override GLDataReviewNoteHdrInfo MapObject(System.Data.IDataReader r)
        {

            GLDataReviewNoteHdrInfo entity = new GLDataReviewNoteHdrInfo();

            entity.GLDataReviewNoteID = r.GetInt64Value("GLDataReviewNoteID");
            entity.GLDataID = r.GetInt64Value("GLDataID");
            entity.ReviewNoteSubject = r.GetStringValue("ReviewNoteSubject");
            entity.DeleteAfterCertification = r.GetBooleanValue("DeleteAfterCertification");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.AddedByUserID = r.GetInt32Value("AddedByUserID");

            // Get the Details for the Name
            entity.AddedByUserInfo = new UserHdrInfo();
            entity.AddedByUserInfo.FirstName = r.GetStringValue("AddedByFirstName");
            entity.AddedByUserInfo.LastName = r.GetStringValue("AddedByLastName");

            entity.RevisedByUserInfo = new UserHdrInfo();
            entity.RevisedByUserInfo.FirstName = r.GetStringValue("RevisedByFirstName");
            entity.RevisedByUserInfo.LastName = r.GetStringValue("RevisedByLastName");
            entity.AttachmentCount = r.GetInt32Value("AttachmentCount");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataReviewNoteHdrInfo object
        /// </summary>
        /// <param name="o">A GLDataReviewNoteHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataReviewNoteHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataReviewNoteHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            if (!entity.IsAddedByUserIDNull)
                parAddedByUserID.Value = entity.AddedByUserID;
            else
                parAddedByUserID.Value = System.DBNull.Value;
            cmdParams.Add(parAddedByUserID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDeleteAfterCertification = cmd.CreateParameter();
            parDeleteAfterCertification.ParameterName = "@DeleteAfterCertification";
            if (!entity.IsDeleteAfterCertificationNull)
                parDeleteAfterCertification.Value = entity.DeleteAfterCertification;
            else
                parDeleteAfterCertification.Value = System.DBNull.Value;
            cmdParams.Add(parDeleteAfterCertification);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReviewNoteSubject = cmd.CreateParameter();
            parReviewNoteSubject.ParameterName = "@ReviewNoteSubject";
            if (!entity.IsReviewNoteSubjectNull)
                parReviewNoteSubject.Value = entity.ReviewNoteSubject;
            else
                parReviewNoteSubject.Value = System.DBNull.Value;
            cmdParams.Add(parReviewNoteSubject);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataReviewNoteHdrInfo object
        /// </summary>
        /// <param name="o">A GLDataReviewNoteHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataReviewNoteHdrInfo entity)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataReviewNoteHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parRevisedByUserID = cmd.CreateParameter();
            parRevisedByUserID.ParameterName = "@RevisedByUserID";
            if (entity.RevisedByUserID != null)
                parRevisedByUserID.Value = entity.RevisedByUserID;
            else
                parRevisedByUserID.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedByUserID);

            System.Data.IDbDataParameter parDeleteAfterCertification = cmd.CreateParameter();
            parDeleteAfterCertification.ParameterName = "@DeleteAfterCertification";
            if (!entity.IsDeleteAfterCertificationNull)
                parDeleteAfterCertification.Value = entity.DeleteAfterCertification;
            else
                parDeleteAfterCertification.Value = System.DBNull.Value;
            cmdParams.Add(parDeleteAfterCertification);

            System.Data.IDbDataParameter parReviewNoteSubject = cmd.CreateParameter();
            parReviewNoteSubject.ParameterName = "@ReviewNoteSubject";
            if (!entity.IsReviewNoteSubjectNull)
                parReviewNoteSubject.Value = entity.ReviewNoteSubject;
            else
                parReviewNoteSubject.Value = System.DBNull.Value;
            cmdParams.Add(parReviewNoteSubject);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparGLDataReviewNoteID = cmd.CreateParameter();
            pkparGLDataReviewNoteID.ParameterName = "@GLDataReviewNoteID";
            pkparGLDataReviewNoteID.Value = entity.GLDataReviewNoteID;
            cmdParams.Add(pkparGLDataReviewNoteID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataReviewNoteHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataReviewNoteID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataReviewNoteHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataReviewNoteID";
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
        public IList<GLDataReviewNoteHdrInfo> SelectAllByGLDataID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataReviewNoteHdrByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataReviewNoteHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataReviewNoteHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataReviewNoteHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataReviewNoteHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataReviewNoteHdrInfo entity, object id)
        {
            entity.GLDataReviewNoteID = Convert.ToInt64(id);
        }






    }
}
