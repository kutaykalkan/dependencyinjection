

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

    public abstract class RecItemCommentDAOBase : CustomAbstractDAO<RecItemCommentInfo>
    {

      

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";
        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RecItemCommentHdr";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RecItemCommentDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RecItemCommentHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RecItemCommentInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RecItemCommentInfo</returns>
        protected override RecItemCommentInfo MapObject(System.Data.IDataReader r)
        {
            RecItemCommentInfo entity = new RecItemCommentInfo();
            entity.RecItemCommentID = r.GetInt64Value("RecItemCommentID");
            entity.RecItemID = r.GetInt64Value("RecItemID");
            entity.RecordTypeID = r.GetInt16Value("RecordTypeID");
            entity.Comment = r.GetStringValue("Comment");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
            entity.AddedByUserRoleID = r.GetInt32Value("AddedByUserRoleID");
            entity.RecItemNumber = r.GetStringValue("RecItemNumber");
            entity.RecItemDescription = r.GetStringValue("RecItemDescription");

            // Get the Details for the Name
            entity.AddedByUserInfo = new UserHdrInfo();
            entity.AddedByUserInfo.UserID = r.GetInt32Value("AddedByUserID");
            entity.AddedByUserInfo.FirstName = r.GetStringValue("AddedByFirstName");
            entity.AddedByUserInfo.LastName = r.GetStringValue("AddedByLastName");

            return entity;
        }       

        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<RecItemCommentInfo> SelectAllByAddedByUserID(object id)
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







        protected override void CustomSave(RecItemCommentInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RecItemCommentDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RecItemCommentInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RecItemCommentDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RecItemCommentInfo entity, object id)
        {
            entity.RecItemCommentID = Convert.ToInt64(id);
        }





        protected override IDbCommand CreateDeleteOneCommand(object id)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand CreateInsertCommand(RecItemCommentInfo o)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand CreateSelectOneCommand(object id)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand CreateUpdateCommand(RecItemCommentInfo o)
        {
            throw new NotImplementedException();
        }
    }
}
