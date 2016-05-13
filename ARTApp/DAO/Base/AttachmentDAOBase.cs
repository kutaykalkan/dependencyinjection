

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

    public abstract class AttachmentDAOBase : CustomAbstractDAO<AttachmentInfo>
    {

        /// <summary>
        /// A static representation of column AttachmentID
        /// </summary>
        public static readonly string COLUMN_ATTACHMENTID = "AttachmentID";
        /// <summary>
        /// A static representation of column Comments
        /// </summary>
        public static readonly string COLUMN_COMMENTS = "Comments";
        /// <summary>
        /// A static representation of column Date
        /// </summary>
        public static readonly string COLUMN_DATE = "Date";
        /// <summary>
        /// A static representation of column DocumentName
        /// </summary>
        public static readonly string COLUMN_DOCUMENTNAME = "DocumentName";
        /// <summary>
        /// A static representation of column FileName
        /// </summary>
        public static readonly string COLUMN_FILENAME = "FileName";
        /// <summary>
        /// A static representation of column FileSize
        /// </summary>
        public static readonly string COLUMN_FILESIZE = "FileSize";
        /// <summary>
        /// A static representation of column IsPermanentOrTemporary
        /// </summary>
        public static readonly string COLUMN_ISPERMANENTORTEMPORARY = "IsPermanentOrTemporary";
        /// <summary>
        /// A static representation of column PhysicalPath
        /// </summary>
        public static readonly string COLUMN_PHYSICALPATH = "PhysicalPath";
        /// <summary>
        /// A static representation of column RecordID
        /// </summary>
        public static readonly string COLUMN_RECORDID = "RecordID";
        /// <summary>
        /// A static representation of column RecordTypeID
        /// </summary>
        public static readonly string COLUMN_RECORDTYPEID = "RecordTypeID";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// Provides access to the name of the primary key column (AttachmentID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AttachmentID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "Attachment";

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
        public AttachmentDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "Attachment", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AttachmentInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AttachmentInfo</returns>
        protected override AttachmentInfo MapObject(System.Data.IDataReader r)
        {

            AttachmentInfo entity = new AttachmentInfo();


            try
            {
                int ordinal = r.GetOrdinal("AttachmentID");
                if (!r.IsDBNull(ordinal)) entity.AttachmentID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RecordID");
                if (!r.IsDBNull(ordinal)) entity.RecordID = r.GetInt64(ordinal);
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FileName");
                if (!r.IsDBNull(ordinal)) entity.FileName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PhysicalPath");
                if (!r.IsDBNull(ordinal)) entity.PhysicalPath = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FileSize");
                if (!r.IsDBNull(ordinal)) entity.FileSize = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Date");
                if (!r.IsDBNull(ordinal)) entity.Date = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserID");
                if (!r.IsDBNull(ordinal)) entity.UserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Comments");
                if (!r.IsDBNull(ordinal)) entity.Comments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsPermanentOrTemporary");
                if (!r.IsDBNull(ordinal)) entity.IsPermanentOrTemporary = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RecordTypeID");
                if (!r.IsDBNull(ordinal)) entity.RecordTypeID = r.GetInt16(ordinal);
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DocumentName");
                if (!r.IsDBNull(ordinal)) entity.DocumentName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserFullName");
                if (!r.IsDBNull(ordinal)) entity.UserFullName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("PreviousAttachmentID");
                if (!r.IsDBNull(ordinal)) entity.PreviousAttachmentID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("OriginalAttachmentID");
                if (!r.IsDBNull(ordinal)) entity.OriginalAttachmentID = ((System.Int64)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("StartRecPeriodID");
                if (!r.IsDBNull(ordinal)) entity.StartRecPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in AttachmentInfo object
        /// </summary>
        /// <param name="o">A AttachmentInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AttachmentInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_Attachment");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parDate = cmd.CreateParameter();
            parDate.ParameterName = "@Date";
            if (!entity.IsDateNull)
                parDate.Value = entity.Date.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDate.Value = System.DBNull.Value;
            cmdParams.Add(parDate);

            System.Data.IDbDataParameter parDocumentName = cmd.CreateParameter();
            parDocumentName.ParameterName = "@DocumentName";
            if (!entity.IsDocumentNameNull)
                parDocumentName.Value = entity.DocumentName;
            else
                parDocumentName.Value = System.DBNull.Value;
            cmdParams.Add(parDocumentName);

            System.Data.IDbDataParameter parFileName = cmd.CreateParameter();
            parFileName.ParameterName = "@FileName";
            if (!entity.IsFileNameNull)
                parFileName.Value = entity.FileName;
            else
                parFileName.Value = System.DBNull.Value;
            cmdParams.Add(parFileName);

            System.Data.IDbDataParameter parFileSize = cmd.CreateParameter();
            parFileSize.ParameterName = "@FileSize";
            if (!entity.IsFileSizeNull)
                parFileSize.Value = entity.FileSize;
            else
                parFileSize.Value = System.DBNull.Value;
            cmdParams.Add(parFileSize);

            System.Data.IDbDataParameter parIsPermanentOrTemporary = cmd.CreateParameter();
            parIsPermanentOrTemporary.ParameterName = "@IsPermanentOrTemporary";
            if (!entity.IsIsPermanentOrTemporaryNull)
                parIsPermanentOrTemporary.Value = entity.IsPermanentOrTemporary;
            else
                parIsPermanentOrTemporary.Value = System.DBNull.Value;
            cmdParams.Add(parIsPermanentOrTemporary);

            System.Data.IDbDataParameter parPhysicalPath = cmd.CreateParameter();
            parPhysicalPath.ParameterName = "@PhysicalPath";
            if (!entity.IsPhysicalPathNull)
                parPhysicalPath.Value = entity.PhysicalPath;
            else
                parPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parPhysicalPath);

            System.Data.IDbDataParameter parRecordID = cmd.CreateParameter();
            parRecordID.ParameterName = "@RecordID";
            if (!entity.IsRecordIDNull)
                parRecordID.Value = entity.RecordID;
            else
                parRecordID.Value = System.DBNull.Value;
            cmdParams.Add(parRecordID);

            System.Data.IDbDataParameter parRecordTypeID = cmd.CreateParameter();
            parRecordTypeID.ParameterName = "@RecordTypeID";
            if (!entity.IsRecordTypeIDNull)
                parRecordTypeID.Value = entity.RecordTypeID;
            else
                parRecordTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecordTypeID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            //IsActive Param Added for Attachment
            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (entity.RecPeriodID.HasValue)
                parRecPeriodID.Value = entity.RecPeriodID.Value;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in AttachmentInfo object
        /// </summary>
        /// <param name="o">A AttachmentInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AttachmentInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_Attachment");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parDate = cmd.CreateParameter();
            parDate.ParameterName = "@Date";
            if (!entity.IsDateNull)
                parDate.Value = entity.Date.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDate.Value = System.DBNull.Value;
            cmdParams.Add(parDate);

            System.Data.IDbDataParameter parDocumentName = cmd.CreateParameter();
            parDocumentName.ParameterName = "@DocumentName";
            if (!entity.IsDocumentNameNull)
                parDocumentName.Value = entity.DocumentName;
            else
                parDocumentName.Value = System.DBNull.Value;
            cmdParams.Add(parDocumentName);

            System.Data.IDbDataParameter parFileName = cmd.CreateParameter();
            parFileName.ParameterName = "@FileName";
            if (!entity.IsFileNameNull)
                parFileName.Value = entity.FileName;
            else
                parFileName.Value = System.DBNull.Value;
            cmdParams.Add(parFileName);

            System.Data.IDbDataParameter parFileSize = cmd.CreateParameter();
            parFileSize.ParameterName = "@FileSize";
            if (!entity.IsFileSizeNull)
                parFileSize.Value = entity.FileSize;
            else
                parFileSize.Value = System.DBNull.Value;
            cmdParams.Add(parFileSize);

            System.Data.IDbDataParameter parIsPermanentOrTemporary = cmd.CreateParameter();
            parIsPermanentOrTemporary.ParameterName = "@IsPermanentOrTemporary";
            if (!entity.IsIsPermanentOrTemporaryNull)
                parIsPermanentOrTemporary.Value = entity.IsPermanentOrTemporary;
            else
                parIsPermanentOrTemporary.Value = System.DBNull.Value;
            cmdParams.Add(parIsPermanentOrTemporary);

            System.Data.IDbDataParameter parPhysicalPath = cmd.CreateParameter();
            parPhysicalPath.ParameterName = "@PhysicalPath";
            if (!entity.IsPhysicalPathNull)
                parPhysicalPath.Value = entity.PhysicalPath;
            else
                parPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parPhysicalPath);

            System.Data.IDbDataParameter parRecordID = cmd.CreateParameter();
            parRecordID.ParameterName = "@RecordID";
            if (!entity.IsRecordIDNull)
                parRecordID.Value = entity.RecordID;
            else
                parRecordID.Value = System.DBNull.Value;
            cmdParams.Add(parRecordID);

            System.Data.IDbDataParameter parRecordTypeID = cmd.CreateParameter();
            parRecordTypeID.ParameterName = "@RecordTypeID";
            if (!entity.IsRecordTypeIDNull)
                parRecordTypeID.Value = entity.RecordTypeID;
            else
                parRecordTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecordTypeID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter pkparAttachmentID = cmd.CreateParameter();
            pkparAttachmentID.ParameterName = "@AttachmentID";
            pkparAttachmentID.Value = entity.AttachmentID;
            cmdParams.Add(pkparAttachmentID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_Attachment");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AttachmentID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_Attachment");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AttachmentID";
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
        public IList<AttachmentInfo> SelectAllByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AttachmentByUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(AttachmentInfo entity, object id)
        {
            entity.AttachmentID = Convert.ToInt64(id);
        }
    }
}
