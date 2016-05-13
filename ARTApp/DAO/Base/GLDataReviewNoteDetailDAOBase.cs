

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

    public abstract class GLDataReviewNoteDetailDAOBase : CustomAbstractDAO<GLDataReviewNoteDetailInfo>
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
        /// A static representation of column GLDataReviewNoteDetailID
        /// </summary>
        public static readonly string COLUMN_GLDATAREVIEWNOTEDETAILID = "GLDataReviewNoteDetailID";
        /// <summary>
        /// A static representation of column GLDataReviewNoteID
        /// </summary>
        public static readonly string COLUMN_GLDATAREVIEWNOTEID = "GLDataReviewNoteID";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column ReviewNote
        /// </summary>
        public static readonly string COLUMN_REVIEWNOTE = "ReviewNote";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataReviewNoteDetailID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataReviewNoteDetailID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataReviewNoteDetail";

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
        public GLDataReviewNoteDetailDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataReviewNoteDetail", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataReviewNoteDetailInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataReviewNoteDetailInfo</returns>
        protected override GLDataReviewNoteDetailInfo MapObject(System.Data.IDataReader r)
        {
            GLDataReviewNoteDetailInfo entity = new GLDataReviewNoteDetailInfo();
            entity.GLDataReviewNoteDetailID = r.GetInt64Value("GLDataReviewNoteDetailID");
            entity.GLDataReviewNoteID = r.GetInt64Value("GLDataReviewNoteID");
            entity.ReviewNote = r.GetStringValue("ReviewNote");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.AddedByUserID = r.GetInt32Value("AddedByUserID");

            // Get the Details for the Name
            entity.AddedByUserInfo = new UserHdrInfo();
            entity.AddedByUserInfo.FirstName = r.GetStringValue("AddedByFirstName");
            entity.AddedByUserInfo.LastName = r.GetStringValue("AddedByLastName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataReviewNoteDetailInfo object
        /// </summary>
        /// <param name="o">A GLDataReviewNoteDetailInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataReviewNoteDetailInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataReviewNoteDetail");
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

            System.Data.IDbDataParameter parGLDataReviewNoteID = cmd.CreateParameter();
            parGLDataReviewNoteID.ParameterName = "@GLDataReviewNoteID";
            if (!entity.IsGLDataReviewNoteIDNull)
                parGLDataReviewNoteID.Value = entity.GLDataReviewNoteID;
            else
                parGLDataReviewNoteID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataReviewNoteID);

            System.Data.IDbDataParameter parReviewNote = cmd.CreateParameter();
            parReviewNote.ParameterName = "@ReviewNote";
            if (!entity.IsReviewNoteNull)
                parReviewNote.Value = entity.ReviewNote;
            else
                parReviewNote.Value = System.DBNull.Value;
            cmdParams.Add(parReviewNote);


            System.Data.IDbDataParameter parAddedByRole = cmd.CreateParameter();
            parAddedByRole.ParameterName = "@AddedByRole";
            if (entity.AddedByRole != null)
                parAddedByRole.Value = entity.AddedByRole;
            else
                parAddedByRole.Value = System.DBNull.Value;
            cmdParams.Add(parAddedByRole);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataReviewNoteDetailInfo object
        /// </summary>
        /// <param name="o">A GLDataReviewNoteDetailInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataReviewNoteDetailInfo entity)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataReviewNoteDetail");
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

            System.Data.IDbDataParameter parGLDataReviewNoteID = cmd.CreateParameter();
            parGLDataReviewNoteID.ParameterName = "@GLDataReviewNoteID";
            if (!entity.IsGLDataReviewNoteIDNull)
                parGLDataReviewNoteID.Value = entity.GLDataReviewNoteID;
            else
                parGLDataReviewNoteID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataReviewNoteID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parReviewNote = cmd.CreateParameter();
            parReviewNote.ParameterName = "@ReviewNote";
            if (!entity.IsReviewNoteNull)
                parReviewNote.Value = entity.ReviewNote;
            else
                parReviewNote.Value = System.DBNull.Value;
            cmdParams.Add(parReviewNote);

            System.Data.IDbDataParameter pkparGLDataReviewNoteDetailID = cmd.CreateParameter();
            pkparGLDataReviewNoteDetailID.ParameterName = "@GLDataReviewNoteDetailID";
            pkparGLDataReviewNoteDetailID.Value = entity.GLDataReviewNoteDetailID;
            cmdParams.Add(pkparGLDataReviewNoteDetailID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataReviewNoteDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataReviewNoteDetailID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataReviewNoteDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataReviewNoteDetailID";
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
        public IList<GLDataReviewNoteDetailInfo> SelectAllByGLDataReviewNoteID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataReviewNoteDetailByGLDataReviewNoteID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataReviewNoteID";
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
        public IList<GLDataReviewNoteDetailInfo> SelectAllByAddedByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataReviewNoteDetailByAddedByUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AddedByUserID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataReviewNoteDetailInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataReviewNoteDetailDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataReviewNoteDetailInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataReviewNoteDetailDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataReviewNoteDetailInfo entity, object id)
        {
            entity.GLDataReviewNoteDetailID = Convert.ToInt64(id);
        }




    }
}
