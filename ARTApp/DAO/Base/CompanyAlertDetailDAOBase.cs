

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

    public abstract class CompanyAlertDetailDAOBase : CustomAbstractDAO<CompanyAlertDetailInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column AlertDescription
        /// </summary>
        public static readonly string COLUMN_ALERTDESCRIPTION = "AlertDescription";
        /// <summary>
        /// A static representation of column AlertExpectedDateTime
        /// </summary>
        public static readonly string COLUMN_ALERTEXPECTEDDATETIME = "AlertExpectedDateTime";
        /// <summary>
        /// A static representation of column CompanyAlertDetailID
        /// </summary>
        public static readonly string COLUMN_COMPANYALERTDETAILID = "CompanyAlertDetailID";
        /// <summary>
        /// A static representation of column CompanyAlertID
        /// </summary>
        public static readonly string COLUMN_COMPANYALERTID = "CompanyAlertID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column Url
        /// </summary>
        public static readonly string COLUMN_URL = "Url";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyAlertDetailID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyAlertDetailID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyAlertDetail";

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
        public CompanyAlertDetailDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyAlertDetail", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyAlertDetailInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyAlertDetailInfo</returns>
        protected override CompanyAlertDetailInfo MapObject(System.Data.IDataReader r)
        {

            CompanyAlertDetailInfo entity = new CompanyAlertDetailInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyAlertDetailID");
                if (!r.IsDBNull(ordinal)) entity.CompanyAlertDetailID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyAlertID");
                if (!r.IsDBNull(ordinal)) entity.CompanyAlertID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertDescription");
                if (!r.IsDBNull(ordinal)) entity.AlertDescription = ((System.String)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("AlertExpectedDateTime");
                if (!r.IsDBNull(ordinal)) entity.AlertExpectedDateTime = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Url");
                if (!r.IsDBNull(ordinal)) entity.Url = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyAlertDetailInfo object
        /// </summary>
        /// <param name="o">A CompanyAlertDetailInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyAlertDetailInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyAlertDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlertDescription = cmd.CreateParameter();
            parAlertDescription.ParameterName = "@AlertDescription";
            if (!entity.IsAlertDescriptionNull)
                parAlertDescription.Value = entity.AlertDescription;
            else
                parAlertDescription.Value = System.DBNull.Value;
            cmdParams.Add(parAlertDescription);

            System.Data.IDbDataParameter parAlertExpectedDateTime = cmd.CreateParameter();
            parAlertExpectedDateTime.ParameterName = "@AlertExpectedDateTime";
            if (!entity.IsAlertExpectedDateTimeNull)
                parAlertExpectedDateTime.Value = entity.AlertExpectedDateTime.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parAlertExpectedDateTime.Value = System.DBNull.Value;
            cmdParams.Add(parAlertExpectedDateTime);

            System.Data.IDbDataParameter parCompanyAlertID = cmd.CreateParameter();
            parCompanyAlertID.ParameterName = "@CompanyAlertID";
            if (!entity.IsCompanyAlertIDNull)
                parCompanyAlertID.Value = entity.CompanyAlertID;
            else
                parCompanyAlertID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyAlertID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parUrl = cmd.CreateParameter();
            parUrl.ParameterName = "@Url";
            if (!entity.IsUrlNull)
                parUrl.Value = entity.Url;
            else
                parUrl.Value = System.DBNull.Value;
            cmdParams.Add(parUrl);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyAlertDetailInfo object
        /// </summary>
        /// <param name="o">A CompanyAlertDetailInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyAlertDetailInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyAlertDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAlertDescription = cmd.CreateParameter();
            parAlertDescription.ParameterName = "@AlertDescription";
            if (!entity.IsAlertDescriptionNull)
                parAlertDescription.Value = entity.AlertDescription;
            else
                parAlertDescription.Value = System.DBNull.Value;
            cmdParams.Add(parAlertDescription);

            System.Data.IDbDataParameter parAlertExpectedDateTime = cmd.CreateParameter();
            parAlertExpectedDateTime.ParameterName = "@AlertExpectedDateTime";
            if (!entity.IsAlertExpectedDateTimeNull)
                parAlertExpectedDateTime.Value = entity.AlertExpectedDateTime.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parAlertExpectedDateTime.Value = System.DBNull.Value;
            cmdParams.Add(parAlertExpectedDateTime);

            System.Data.IDbDataParameter parCompanyAlertID = cmd.CreateParameter();
            parCompanyAlertID.ParameterName = "@CompanyAlertID";
            if (!entity.IsCompanyAlertIDNull)
                parCompanyAlertID.Value = entity.CompanyAlertID;
            else
                parCompanyAlertID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyAlertID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parUrl = cmd.CreateParameter();
            parUrl.ParameterName = "@Url";
            if (!entity.IsUrlNull)
                parUrl.Value = entity.Url;
            else
                parUrl.Value = System.DBNull.Value;
            cmdParams.Add(parUrl);

            System.Data.IDbDataParameter pkparCompanyAlertDetailID = cmd.CreateParameter();
            pkparCompanyAlertDetailID.ParameterName = "@CompanyAlertDetailID";
            pkparCompanyAlertDetailID.Value = entity.CompanyAlertDetailID;
            cmdParams.Add(pkparCompanyAlertDetailID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyAlertDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyAlertDetailID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyAlertDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyAlertDetailID";
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
        public IList<CompanyAlertDetailInfo> SelectAllByCompanyAlertID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyAlertDetailByCompanyAlertID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyAlertID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(CompanyAlertDetailInfo entity, object id)
        {
            entity.CompanyAlertDetailID = Convert.ToInt64(id);
        }
    }
}
