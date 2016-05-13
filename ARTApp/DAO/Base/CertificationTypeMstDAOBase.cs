

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

    public abstract class CertificationTypeMstDAOBase : CustomAbstractDAO<CertificationTypeMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CertificationType
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONTYPE = "CertificationType";
        /// <summary>
        /// A static representation of column CertificationTypeID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONTYPEID = "CertificationTypeID";
        /// <summary>
        /// A static representation of column CertificationTypeLabelID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONTYPELABELID = "CertificationTypeLabelID";
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
        /// Provides access to the name of the primary key column (CertificationTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CertificationTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CertificationTypeMst";

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
        public CertificationTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CertificationTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CertificationTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CertificationTypeMstInfo</returns>
        protected override CertificationTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            CertificationTypeMstInfo entity = new CertificationTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("CertificationTypeID");
                if (!r.IsDBNull(ordinal)) entity.CertificationTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationType");
                if (!r.IsDBNull(ordinal)) entity.CertificationType = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationTypeLabelID");
                if (!r.IsDBNull(ordinal)) entity.CertificationTypeLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Description");
                if (!r.IsDBNull(ordinal)) entity.Description = ((System.String)(r.GetValue(ordinal)));
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
        /// in CertificationTypeMstInfo object
        /// </summary>
        /// <param name="o">A CertificationTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CertificationTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CertificationTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCertificationType = cmd.CreateParameter();
            parCertificationType.ParameterName = "@CertificationType";
            if (!entity.IsCertificationTypeNull)
                parCertificationType.Value = entity.CertificationType;
            else
                parCertificationType.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationType);

            System.Data.IDbDataParameter parCertificationTypeLabelID = cmd.CreateParameter();
            parCertificationTypeLabelID.ParameterName = "@CertificationTypeLabelID";
            if (!entity.IsCertificationTypeLabelIDNull)
                parCertificationTypeLabelID.Value = entity.CertificationTypeLabelID;
            else
                parCertificationTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationTypeLabelID);

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

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CertificationTypeMstInfo object
        /// </summary>
        /// <param name="o">A CertificationTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CertificationTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CertificationTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCertificationType = cmd.CreateParameter();
            parCertificationType.ParameterName = "@CertificationType";
            if (!entity.IsCertificationTypeNull)
                parCertificationType.Value = entity.CertificationType;
            else
                parCertificationType.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationType);

            System.Data.IDbDataParameter parCertificationTypeLabelID = cmd.CreateParameter();
            parCertificationTypeLabelID.ParameterName = "@CertificationTypeLabelID";
            if (!entity.IsCertificationTypeLabelIDNull)
                parCertificationTypeLabelID.Value = entity.CertificationTypeLabelID;
            else
                parCertificationTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationTypeLabelID);

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

            System.Data.IDbDataParameter pkparCertificationTypeID = cmd.CreateParameter();
            pkparCertificationTypeID.ParameterName = "@CertificationTypeID";
            pkparCertificationTypeID.Value = entity.CertificationTypeID;
            cmdParams.Add(pkparCertificationTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CertificationTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CertificationTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }

        private void MapIdentity(CertificationTypeMstInfo entity, object id)
        {
            entity.CertificationTypeID = Convert.ToInt16(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(CompanyHdrInfo entity)
        {
            return this.SelectCertificationTypeMstDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationTypeMst] INNER JOIN [CertificationSignOffStatus] ON [CertificationTypeMst].[CertificationTypeID] = [CertificationSignOffStatus].[CertificationTypeID] INNER JOIN [CompanyHdr] ON [CertificationSignOffStatus].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationTypeMstInfo> objCertificationTypeMstEntityColl = new List<CertificationTypeMstInfo>(this.Select(cmd));
            return objCertificationTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(ReconciliationPeriodInfo entity)
        {
            return this.SelectCertificationTypeMstDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationTypeMst] INNER JOIN [CertificationSignOffStatus] ON [CertificationTypeMst].[CertificationTypeID] = [CertificationSignOffStatus].[CertificationTypeID] INNER JOIN [ReconciliationPeriod] ON [CertificationSignOffStatus].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationTypeMstInfo> objCertificationTypeMstEntityColl = new List<CertificationTypeMstInfo>(this.Select(cmd));
            return objCertificationTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToUserHdrByCertificationSignOffStatus(UserHdrInfo entity)
        {
            return this.SelectCertificationTypeMstDetailsAssociatedToUserHdrByCertificationSignOffStatus(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToUserHdrByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationTypeMst] INNER JOIN [CertificationSignOffStatus] ON [CertificationTypeMst].[CertificationTypeID] = [CertificationSignOffStatus].[CertificationTypeID] INNER JOIN [UserHdr] ON [CertificationSignOffStatus].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationTypeMstInfo> objCertificationTypeMstEntityColl = new List<CertificationTypeMstInfo>(this.Select(cmd));
            return objCertificationTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToCompanyHdrByCertificationVerbiage(CompanyHdrInfo entity)
        {
            return this.SelectCertificationTypeMstDetailsAssociatedToCompanyHdrByCertificationVerbiage(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToCompanyHdrByCertificationVerbiage(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationTypeMst] INNER JOIN [CertificationVerbiage] ON [CertificationTypeMst].[CertificationTypeID] = [CertificationVerbiage].[CertificationTypeID] INNER JOIN [CompanyHdr] ON [CertificationVerbiage].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationTypeMstInfo> objCertificationTypeMstEntityColl = new List<CertificationTypeMstInfo>(this.Select(cmd));
            return objCertificationTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToRoleMstByCertificationVerbiage(RoleMstInfo entity)
        {
            return this.SelectCertificationTypeMstDetailsAssociatedToRoleMstByCertificationVerbiage(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationTypeMstInfo> SelectCertificationTypeMstDetailsAssociatedToRoleMstByCertificationVerbiage(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationTypeMst] INNER JOIN [CertificationVerbiage] ON [CertificationTypeMst].[CertificationTypeID] = [CertificationVerbiage].[CertificationTypeID] INNER JOIN [RoleMst] ON [CertificationVerbiage].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationTypeMstInfo> objCertificationTypeMstEntityColl = new List<CertificationTypeMstInfo>(this.Select(cmd));
            return objCertificationTypeMstEntityColl;
        }

    }
}
