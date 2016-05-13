

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

    public abstract class ReportMstDAOBase : CustomAbstractDAO<ReportMstInfo>
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
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column DescriptionLabelID
        /// </summary>
        public static readonly string COLUMN_DESCRIPTIONLABELID = "DescriptionLabelID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column Report
        /// </summary>
        public static readonly string COLUMN_REPORT = "Report";
        /// <summary>
        /// A static representation of column ReportGroupID
        /// </summary>
        public static readonly string COLUMN_REPORTGROUPID = "ReportTypeId";
        /// <summary>
        /// A static representation of column ReportID
        /// </summary>
        public static readonly string COLUMN_REPORTID = "ReportID";
        /// <summary>
        /// A static representation of column ReportLabelID
        /// </summary>
        public static readonly string COLUMN_REPORTLABELID = "ReportLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (ReportID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReportID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReportMst";

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
        public ReportMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReportMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReportMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReportMstInfo</returns>
        protected override ReportMstInfo MapObject(System.Data.IDataReader r)
        {
            ReportMstInfo entity = new ReportMstInfo();
            entity.ReportID = r.GetInt16Value("ReportID");
            entity.Report = r.GetStringValue("Report");
            entity.ReportLabelID = r.GetInt32Value("ReportLabelID");
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            entity.ReportTypeId = r.GetInt16Value("ReportTypeId");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");
            entity.ReportUrl = r.GetStringValue("ReportUrl");
            entity.ReportPrintUrl = r.GetStringValue("ReportPrintUrl");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReportMstInfo object
        /// </summary>
        /// <param name="o">A ReportMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReportMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReportMst");
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

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (!entity.IsDescriptionLabelIDNull)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

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

            System.Data.IDbDataParameter parReport = cmd.CreateParameter();
            parReport.ParameterName = "@Report";
            if (!entity.IsReportNull)
                parReport.Value = entity.Report;
            else
                parReport.Value = System.DBNull.Value;
            cmdParams.Add(parReport);

            System.Data.IDbDataParameter parReportTypeId = cmd.CreateParameter();
            parReportTypeId.ParameterName = "@ReportTypeId";
            if (!entity.IsReportTypeIdNull)
                parReportTypeId.Value = entity.ReportTypeId;
            else
                parReportTypeId.Value = System.DBNull.Value;
            cmdParams.Add(parReportTypeId);

            System.Data.IDbDataParameter parReportLabelID = cmd.CreateParameter();
            parReportLabelID.ParameterName = "@ReportLabelID";
            if (!entity.IsReportLabelIDNull)
                parReportLabelID.Value = entity.ReportLabelID;
            else
                parReportLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReportLabelID);

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
        /// in ReportMstInfo object
        /// </summary>
        /// <param name="o">A ReportMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReportMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReportMst");
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

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (!entity.IsDescriptionLabelIDNull)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

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

            System.Data.IDbDataParameter parReport = cmd.CreateParameter();
            parReport.ParameterName = "@Report";
            if (!entity.IsReportNull)
                parReport.Value = entity.Report;
            else
                parReport.Value = System.DBNull.Value;
            cmdParams.Add(parReport);

            System.Data.IDbDataParameter parReportTypeId = cmd.CreateParameter();
            parReportTypeId.ParameterName = "@ReportTypeId";
            if (!entity.IsReportTypeIdNull)
                parReportTypeId.Value = entity.ReportTypeId;
            else
                parReportTypeId.Value = System.DBNull.Value;
            cmdParams.Add(parReportTypeId);

            System.Data.IDbDataParameter parReportLabelID = cmd.CreateParameter();
            parReportLabelID.ParameterName = "@ReportLabelID";
            if (!entity.IsReportLabelIDNull)
                parReportLabelID.Value = entity.ReportLabelID;
            else
                parReportLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReportLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparReportID = cmd.CreateParameter();
            pkparReportID.ParameterName = "@ReportID";
            pkparReportID.Value = entity.ReportID;
            cmdParams.Add(pkparReportID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReportMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
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
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReportMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
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
        public IList<ReportMstInfo> SelectAllByReportGroupID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReportMstByReportGroupID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportGroupID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(ReportMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReportMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReportMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReportMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReportMstInfo entity, object id)
        {
            entity.ReportID = Convert.ToInt16(id);
        }











        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToReconciliationPeriodByReportSignOffStatus(ReconciliationPeriodInfo entity)
        {
            return this.SelectReportMstDetailsAssociatedToReconciliationPeriodByReportSignOffStatus(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToReconciliationPeriodByReportSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReportMst] INNER JOIN [ReportSignOffStatus] ON [ReportMst].[ReportID] = [ReportSignOffStatus].[ReportID] INNER JOIN [ReconciliationPeriod] ON [ReportSignOffStatus].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReportMstInfo> objReportMstEntityColl = new List<ReportMstInfo>(this.Select(cmd));
            return objReportMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToUserHdrByReportSignOffStatus(UserHdrInfo entity)
        {
            return this.SelectReportMstDetailsAssociatedToUserHdrByReportSignOffStatus(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToUserHdrByReportSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReportMst] INNER JOIN [ReportSignOffStatus] ON [ReportMst].[ReportID] = [ReportSignOffStatus].[ReportID] INNER JOIN [UserHdr] ON [ReportSignOffStatus].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReportMstInfo> objReportMstEntityColl = new List<ReportMstInfo>(this.Select(cmd));
            return objReportMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToCompanyHdrByRoleMandatoryReport(CompanyHdrInfo entity)
        {
            return this.SelectReportMstDetailsAssociatedToCompanyHdrByRoleMandatoryReport(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToCompanyHdrByRoleMandatoryReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReportMst] INNER JOIN [RoleMandatoryReport] ON [ReportMst].[ReportID] = [RoleMandatoryReport].[ReportID] INNER JOIN [CompanyHdr] ON [RoleMandatoryReport].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReportMstInfo> objReportMstEntityColl = new List<ReportMstInfo>(this.Select(cmd));
            return objReportMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToRoleMstByRoleMandatoryReport(RoleMstInfo entity)
        {
            return this.SelectReportMstDetailsAssociatedToRoleMstByRoleMandatoryReport(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToRoleMstByRoleMandatoryReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReportMst] INNER JOIN [RoleMandatoryReport] ON [ReportMst].[ReportID] = [RoleMandatoryReport].[ReportID] INNER JOIN [RoleMst] ON [RoleMandatoryReport].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReportMstInfo> objReportMstEntityColl = new List<ReportMstInfo>(this.Select(cmd));
            return objReportMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToRoleMstByRoleReport(RoleMstInfo entity)
        {
            return this.SelectReportMstDetailsAssociatedToRoleMstByRoleReport(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToRoleMstByRoleReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReportMst] INNER JOIN [RoleReport] ON [ReportMst].[ReportID] = [RoleReport].[ReportID] INNER JOIN [RoleMst] ON [RoleReport].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReportMstInfo> objReportMstEntityColl = new List<ReportMstInfo>(this.Select(cmd));
            return objReportMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToUserHdrByUserFavoriteReport(UserHdrInfo entity)
        {
            return this.SelectReportMstDetailsAssociatedToUserHdrByUserFavoriteReport(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportMstInfo> SelectReportMstDetailsAssociatedToUserHdrByUserFavoriteReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReportMst] INNER JOIN [UserFavoriteReport] ON [ReportMst].[ReportID] = [UserFavoriteReport].[ReportID] INNER JOIN [UserHdr] ON [UserFavoriteReport].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReportMstInfo> objReportMstEntityColl = new List<ReportMstInfo>(this.Select(cmd));
            return objReportMstEntityColl;
        }

    }
}
