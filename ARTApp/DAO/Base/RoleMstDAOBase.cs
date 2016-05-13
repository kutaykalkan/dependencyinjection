

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

    public abstract class RoleMstDAOBase : CustomAbstractDAO<RoleMstInfo>
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
        /// A static representation of column Role
        /// </summary>
        public static readonly string COLUMN_ROLE = "Role";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// A static representation of column RoleLabelID
        /// </summary>
        public static readonly string COLUMN_ROLELABELID = "RoleLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (RoleID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RoleID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RoleMst";

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
        public RoleMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RoleMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RoleMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RoleMstInfo</returns>
        protected override RoleMstInfo MapObject(System.Data.IDataReader r)
        {
            RoleMstInfo entity = new RoleMstInfo();
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.Role = r.GetStringValue("Role");
            entity.RoleLabelID = r.GetInt32Value("RoleLabelID");
            entity.Description = r.GetStringValue("Description");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in RoleMstInfo object
        /// </summary>
        /// <param name="o">A RoleMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RoleMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RoleMst");
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

            System.Data.IDbDataParameter parRole = cmd.CreateParameter();
            parRole.ParameterName = "@Role";
            if (!entity.IsRoleNull)
                parRole.Value = entity.Role;
            else
                parRole.Value = System.DBNull.Value;
            cmdParams.Add(parRole);

            System.Data.IDbDataParameter parRoleLabelID = cmd.CreateParameter();
            parRoleLabelID.ParameterName = "@RoleLabelID";
            if (!entity.IsRoleLabelIDNull)
                parRoleLabelID.Value = entity.RoleLabelID;
            else
                parRoleLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleLabelID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in RoleMstInfo object
        /// </summary>
        /// <param name="o">A RoleMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RoleMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RoleMst");
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

            System.Data.IDbDataParameter parRole = cmd.CreateParameter();
            parRole.ParameterName = "@Role";
            if (!entity.IsRoleNull)
                parRole.Value = entity.Role;
            else
                parRole.Value = System.DBNull.Value;
            cmdParams.Add(parRole);

            System.Data.IDbDataParameter parRoleLabelID = cmd.CreateParameter();
            parRoleLabelID.ParameterName = "@RoleLabelID";
            if (!entity.IsRoleLabelIDNull)
                parRoleLabelID.Value = entity.RoleLabelID;
            else
                parRoleLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleLabelID);

            System.Data.IDbDataParameter pkparRoleID = cmd.CreateParameter();
            pkparRoleID.ParameterName = "@RoleID";
            pkparRoleID.Value = entity.RoleID;
            cmdParams.Add(pkparRoleID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RoleMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_RoleMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(RoleMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RoleMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RoleMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RoleMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RoleMstInfo entity, object id)
        {
            entity.RoleID = Convert.ToInt16(id);
        }

















        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToCompanyHdrByCertificationVerbiage(CompanyHdrInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToCompanyHdrByCertificationVerbiage(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToCompanyHdrByCertificationVerbiage(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [CertificationVerbiage] ON [RoleMst].[RoleID] = [CertificationVerbiage].[RoleID] INNER JOIN [CompanyHdr] ON [CertificationVerbiage].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToCertificationTypeMstByCertificationVerbiage(CertificationTypeMstInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToCertificationTypeMstByCertificationVerbiage(entity.CertificationTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToCertificationTypeMstByCertificationVerbiage(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [CertificationVerbiage] ON [RoleMst].[RoleID] = [CertificationVerbiage].[RoleID] INNER JOIN [CertificationTypeMst] ON [CertificationVerbiage].[CertificationTypeID] = [CertificationTypeMst].[CertificationTypeID]  WHERE  [CertificationTypeMst].[CertificationTypeID] = @CertificationTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToDashboardMstByDashboardRole(DashboardMstInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToDashboardMstByDashboardRole(entity.DashboardID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToDashboardMstByDashboardRole(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [DashboardRole] ON [RoleMst].[RoleID] = [DashboardRole].[RoleID] INNER JOIN [DashboardMst] ON [DashboardRole].[DashboardID] = [DashboardMst].[DashboardID]  WHERE  [DashboardMst].[DashboardID] = @DashboardID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DashboardID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToUserHdrByGLDataReconciliationSubmissionDate(UserHdrInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToUserHdrByGLDataReconciliationSubmissionDate(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToUserHdrByGLDataReconciliationSubmissionDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [GLDataReconciliationSubmissionDate] ON [RoleMst].[RoleID] = [GLDataReconciliationSubmissionDate].[RoleID] INNER JOIN [UserHdr] ON [GLDataReconciliationSubmissionDate].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToGLDataHdrByGLDataReconciliationSubmissionDate(GLDataHdrInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToGLDataHdrByGLDataReconciliationSubmissionDate(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToGLDataHdrByGLDataReconciliationSubmissionDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [GLDataReconciliationSubmissionDate] ON [RoleMst].[RoleID] = [GLDataReconciliationSubmissionDate].[RoleID] INNER JOIN [GLDataHdr] ON [GLDataReconciliationSubmissionDate].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToGLDataHdrByGLDataRoleCertificationDate(GLDataHdrInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToGLDataHdrByGLDataRoleCertificationDate(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToGLDataHdrByGLDataRoleCertificationDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [GLDataRoleCertificationDate] ON [RoleMst].[RoleID] = [GLDataRoleCertificationDate].[RoleID] INNER JOIN [GLDataHdr] ON [GLDataRoleCertificationDate].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToMenuMstByMenuRole(MenuMstInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToMenuMstByMenuRole(entity.MenuID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToMenuMstByMenuRole(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [MenuRole] ON [RoleMst].[RoleID] = [MenuRole].[RoleID] INNER JOIN [MenuMst] ON [MenuRole].[MenuID] = [MenuMst].[MenuID]  WHERE  [MenuMst].[MenuID] = @MenuID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MenuID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToCompanyHdrByRoleMandatoryReport(CompanyHdrInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToCompanyHdrByRoleMandatoryReport(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToCompanyHdrByRoleMandatoryReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [RoleMandatoryReport] ON [RoleMst].[RoleID] = [RoleMandatoryReport].[RoleID] INNER JOIN [CompanyHdr] ON [RoleMandatoryReport].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToReportMstByRoleMandatoryReport(ReportMstInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToReportMstByRoleMandatoryReport(entity.ReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToReportMstByRoleMandatoryReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [RoleMandatoryReport] ON [RoleMst].[RoleID] = [RoleMandatoryReport].[RoleID] INNER JOIN [ReportMst] ON [RoleMandatoryReport].[ReportID] = [ReportMst].[ReportID]  WHERE  [ReportMst].[ReportID] = @ReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToRoleReconciliationDueDateByRoleReconciliationDueDate(RoleReconciliationDueDateInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToRoleReconciliationDueDateByRoleReconciliationDueDate(entity.RoleReconciliationDueDateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToRoleReconciliationDueDateByRoleReconciliationDueDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [RoleReconciliationDueDate] ON [RoleMst].[RoleID] = [RoleReconciliationDueDate].[RoleID] INNER JOIN [RoleReconciliationDueDate] ON [RoleReconciliationDueDate].[RoleReconciliationDueDateID] = [RoleReconciliationDueDate].[RoleReconciliationDueDateID]  WHERE  [RoleReconciliationDueDate].[RoleReconciliationDueDateID] = @RoleReconciliationDueDateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleReconciliationDueDateID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToReconciliationPeriodByRoleReconciliationDueDate(ReconciliationPeriodInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToReconciliationPeriodByRoleReconciliationDueDate(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToReconciliationPeriodByRoleReconciliationDueDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [RoleReconciliationDueDate] ON [RoleMst].[RoleID] = [RoleReconciliationDueDate].[RoleID] INNER JOIN [ReconciliationPeriod] ON [RoleReconciliationDueDate].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToReportMstByRoleReport(ReportMstInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToReportMstByRoleReport(entity.ReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToReportMstByRoleReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleMst] INNER JOIN [RoleReport] ON [RoleMst].[RoleID] = [RoleReport].[RoleID] INNER JOIN [ReportMst] ON [RoleReport].[ReportID] = [ReportMst].[ReportID]  WHERE  [ReportMst].[ReportID] = @ReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToUserHdrByUserRole(UserHdrInfo entity)
        {
            return this.SelectRoleMstDetailsAssociatedToUserHdrByUserRole(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleMstInfo> SelectRoleMstDetailsAssociatedToUserHdrByUserRole(int? id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RoleMstByUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleMstInfo> objRoleMstEntityColl = new List<RoleMstInfo>(this.Select(cmd));
            return objRoleMstEntityColl;
        }

    }
}
