

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

    public abstract class DateBasisMstDAOBase : CustomAbstractDAO<DateBasisMstInfo>
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
        /// A static representation of column DateBasis
        /// </summary>
        public static readonly string COLUMN_DATEBASIS = "DateBasis";
        /// <summary>
        /// A static representation of column DateBasisID
        /// </summary>
        public static readonly string COLUMN_DATEBASISID = "DateBasisID";
        /// <summary>
        /// A static representation of column DateBasisLabelID
        /// </summary>
        public static readonly string COLUMN_DATEBASISLABELID = "DateBasisLabelID";
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
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (DateBasisID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DateBasisID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DateBasisMst";

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
        public DateBasisMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DateBasisMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DateBasisMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DateBasisMstInfo</returns>
        protected override DateBasisMstInfo MapObject(System.Data.IDataReader r)
        {

            DateBasisMstInfo entity = new DateBasisMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("DateBasisID");
                if (!r.IsDBNull(ordinal)) entity.DateBasisID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateBasis");
                if (!r.IsDBNull(ordinal)) entity.DateBasis = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateBasisLabelID");
                if (!r.IsDBNull(ordinal)) entity.DateBasisLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in DateBasisMstInfo object
        /// </summary>
        /// <param name="o">A DateBasisMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DateBasisMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DateBasisMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateBasis = cmd.CreateParameter();
            parDateBasis.ParameterName = "@DateBasis";
            if (!entity.IsDateBasisNull)
                parDateBasis.Value = entity.DateBasis;
            else
                parDateBasis.Value = System.DBNull.Value;
            cmdParams.Add(parDateBasis);

            System.Data.IDbDataParameter parDateBasisLabelID = cmd.CreateParameter();
            parDateBasisLabelID.ParameterName = "@DateBasisLabelID";
            if (!entity.IsDateBasisLabelIDNull)
                parDateBasisLabelID.Value = entity.DateBasisLabelID;
            else
                parDateBasisLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDateBasisLabelID);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in DateBasisMstInfo object
        /// </summary>
        /// <param name="o">A DateBasisMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DateBasisMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DateBasisMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateBasis = cmd.CreateParameter();
            parDateBasis.ParameterName = "@DateBasis";
            if (!entity.IsDateBasisNull)
                parDateBasis.Value = entity.DateBasis;
            else
                parDateBasis.Value = System.DBNull.Value;
            cmdParams.Add(parDateBasis);

            System.Data.IDbDataParameter parDateBasisLabelID = cmd.CreateParameter();
            parDateBasisLabelID.ParameterName = "@DateBasisLabelID";
            if (!entity.IsDateBasisLabelIDNull)
                parDateBasisLabelID.Value = entity.DateBasisLabelID;
            else
                parDateBasisLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDateBasisLabelID);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparDateBasisID = cmd.CreateParameter();
            pkparDateBasisID.ParameterName = "@DateBasisID";
            pkparDateBasisID.Value = entity.DateBasisID;
            cmdParams.Add(pkparDateBasisID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DateBasisMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DateBasisID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DateBasisMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DateBasisID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(DateBasisMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DateBasisMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(DateBasisMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DateBasisMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(DateBasisMstInfo entity, object id)
        {
            entity.DateBasisID = Convert.ToInt16(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToAlertTypeMstByAlertMst(AlertTypeMstInfo entity)
        {
            return this.SelectDateBasisMstDetailsAssociatedToAlertTypeMstByAlertMst(entity.AlertTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToAlertTypeMstByAlertMst(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DateBasisMst] INNER JOIN [AlertMst] ON [DateBasisMst].[DateBasisID] = [AlertMst].[DefaultDateBasisID] INNER JOIN [AlertTypeMst] ON [AlertMst].[AlertTypeID] = [AlertTypeMst].[AlertTypeID]  WHERE  [AlertTypeMst].[AlertTypeID] = @AlertTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<DateBasisMstInfo> objDateBasisMstEntityColl = new List<DateBasisMstInfo>(this.Select(cmd));
            return objDateBasisMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToCompanyHdrByCompanyAlert(CompanyHdrInfo entity)
        {
            return this.SelectDateBasisMstDetailsAssociatedToCompanyHdrByCompanyAlert(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToCompanyHdrByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DateBasisMst] INNER JOIN [CompanyAlert] ON [DateBasisMst].[DateBasisID] = [CompanyAlert].[DateBasisID] INNER JOIN [CompanyHdr] ON [CompanyAlert].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<DateBasisMstInfo> objDateBasisMstEntityColl = new List<DateBasisMstInfo>(this.Select(cmd));
            return objDateBasisMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToAlertMstByCompanyAlert(AlertMstInfo entity)
        {
            return this.SelectDateBasisMstDetailsAssociatedToAlertMstByCompanyAlert(entity.AlertID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToAlertMstByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DateBasisMst] INNER JOIN [CompanyAlert] ON [DateBasisMst].[DateBasisID] = [CompanyAlert].[DateBasisID] INNER JOIN [AlertMst] ON [CompanyAlert].[AlertID] = [AlertMst].[AlertID]  WHERE  [AlertMst].[AlertID] = @AlertID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertID";
            par.Value = id;

            cmdParams.Add(par);
            List<DateBasisMstInfo> objDateBasisMstEntityColl = new List<DateBasisMstInfo>(this.Select(cmd));
            return objDateBasisMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToAlertScheduleTypeMstByCompanyAlert(AlertScheduleTypeMstInfo entity)
        {
            return this.SelectDateBasisMstDetailsAssociatedToAlertScheduleTypeMstByCompanyAlert(entity.AlertScheduleTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DateBasisMstInfo> SelectDateBasisMstDetailsAssociatedToAlertScheduleTypeMstByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DateBasisMst] INNER JOIN [CompanyAlert] ON [DateBasisMst].[DateBasisID] = [CompanyAlert].[DateBasisID] INNER JOIN [AlertScheduleTypeMst] ON [CompanyAlert].[AlertScheduleTypeID] = [AlertScheduleTypeMst].[AlertScheduleTypeID]  WHERE  [AlertScheduleTypeMst].[AlertScheduleTypeID] = @AlertScheduleTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertScheduleTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<DateBasisMstInfo> objDateBasisMstEntityColl = new List<DateBasisMstInfo>(this.Select(cmd));
            return objDateBasisMstEntityColl;
        }

    }
}
