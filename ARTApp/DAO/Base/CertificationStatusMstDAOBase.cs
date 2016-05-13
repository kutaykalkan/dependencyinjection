

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

    public abstract class CertificationStatusMstDAOBase : CustomAbstractDAO<CertificationStatusMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CertificationStatus
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONSTATUS = "CertificationStatus";
        /// <summary>
        /// A static representation of column CertificationStatusID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONSTATUSID = "CertificationStatusID";
        /// <summary>
        /// A static representation of column CertificationStatusLabelID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONSTATUSLABELID = "CertificationStatusLabelID";
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
        /// Provides access to the name of the primary key column (CertificationStatusID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CertificationStatusID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CertificationStatusMst";

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
        public CertificationStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CertificationStatusMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CertificationStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CertificationStatusMstInfo</returns>
        protected override CertificationStatusMstInfo MapObject(System.Data.IDataReader r)
        {

            CertificationStatusMstInfo entity = new CertificationStatusMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("CertificationStatusID");
                if (!r.IsDBNull(ordinal)) entity.CertificationStatusID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationStatus");
                if (!r.IsDBNull(ordinal)) entity.CertificationStatus = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationStatusLabelID");
                if (!r.IsDBNull(ordinal)) entity.CertificationStatusLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in CertificationStatusMstInfo object
        /// </summary>
        /// <param name="o">A CertificationStatusMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CertificationStatusMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CertificationStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCertificationStatus = cmd.CreateParameter();
            parCertificationStatus.ParameterName = "@CertificationStatus";
            if (!entity.IsCertificationStatusNull)
                parCertificationStatus.Value = entity.CertificationStatus;
            else
                parCertificationStatus.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStatus);

            System.Data.IDbDataParameter parCertificationStatusLabelID = cmd.CreateParameter();
            parCertificationStatusLabelID.ParameterName = "@CertificationStatusLabelID";
            if (!entity.IsCertificationStatusLabelIDNull)
                parCertificationStatusLabelID.Value = entity.CertificationStatusLabelID;
            else
                parCertificationStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStatusLabelID);

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
        /// in CertificationStatusMstInfo object
        /// </summary>
        /// <param name="o">A CertificationStatusMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CertificationStatusMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CertificationStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCertificationStatus = cmd.CreateParameter();
            parCertificationStatus.ParameterName = "@CertificationStatus";
            if (!entity.IsCertificationStatusNull)
                parCertificationStatus.Value = entity.CertificationStatus;
            else
                parCertificationStatus.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStatus);

            System.Data.IDbDataParameter parCertificationStatusLabelID = cmd.CreateParameter();
            parCertificationStatusLabelID.ParameterName = "@CertificationStatusLabelID";
            if (!entity.IsCertificationStatusLabelIDNull)
                parCertificationStatusLabelID.Value = entity.CertificationStatusLabelID;
            else
                parCertificationStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStatusLabelID);

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

            System.Data.IDbDataParameter pkparCertificationStatusID = cmd.CreateParameter();
            pkparCertificationStatusID.ParameterName = "@CertificationStatusID";
            pkparCertificationStatusID.Value = entity.CertificationStatusID;
            cmdParams.Add(pkparCertificationStatusID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CertificationStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationStatusID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CertificationStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }

        private void MapIdentity(CertificationStatusMstInfo entity, object id)
        {
            entity.CertificationStatusID = Convert.ToInt16(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToAccountHdrByGLDataHdr(AccountHdrInfo entity)
        {
            return this.SelectCertificationStatusMstDetailsAssociatedToAccountHdrByGLDataHdr(entity.AccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToAccountHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationStatusMst] INNER JOIN [GLDataHdr] ON [CertificationStatusMst].[CertificationStatusID] = [GLDataHdr].[CertificationStatusID] INNER JOIN [AccountHdr] ON [GLDataHdr].[AccountID] = [AccountHdr].[AccountID]  WHERE  [AccountHdr].[AccountID] = @AccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationStatusMstInfo> objCertificationStatusMstEntityColl = new List<CertificationStatusMstInfo>(this.Select(cmd));
            return objCertificationStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToReconciliationStatusMstByGLDataHdr(ReconciliationStatusMstInfo entity)
        {
            return this.SelectCertificationStatusMstDetailsAssociatedToReconciliationStatusMstByGLDataHdr(entity.ReconciliationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToReconciliationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationStatusMst] INNER JOIN [GLDataHdr] ON [CertificationStatusMst].[CertificationStatusID] = [GLDataHdr].[CertificationStatusID] INNER JOIN [ReconciliationStatusMst] ON [GLDataHdr].[ReconciliationStatusID] = [ReconciliationStatusMst].[ReconciliationStatusID]  WHERE  [ReconciliationStatusMst].[ReconciliationStatusID] = @ReconciliationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationStatusMstInfo> objCertificationStatusMstEntityColl = new List<CertificationStatusMstInfo>(this.Select(cmd));
            return objCertificationStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToDataImportHdrByGLDataHdr(DataImportHdrInfo entity)
        {
            return this.SelectCertificationStatusMstDetailsAssociatedToDataImportHdrByGLDataHdr(entity.DataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToDataImportHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationStatusMst] INNER JOIN [GLDataHdr] ON [CertificationStatusMst].[CertificationStatusID] = [GLDataHdr].[CertificationStatusID] INNER JOIN [DataImportHdr] ON [GLDataHdr].[DataImportID] = [DataImportHdr].[DataImportID]  WHERE  [DataImportHdr].[DataImportID] = @DataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationStatusMstInfo> objCertificationStatusMstEntityColl = new List<CertificationStatusMstInfo>(this.Select(cmd));
            return objCertificationStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToReconciliationPeriodByGLDataHdr(ReconciliationPeriodInfo entity)
        {
            return this.SelectCertificationStatusMstDetailsAssociatedToReconciliationPeriodByGLDataHdr(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CertificationStatusMstInfo> SelectCertificationStatusMstDetailsAssociatedToReconciliationPeriodByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CertificationStatusMst] INNER JOIN [GLDataHdr] ON [CertificationStatusMst].[CertificationStatusID] = [GLDataHdr].[CertificationStatusID] INNER JOIN [ReconciliationPeriod] ON [GLDataHdr].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<CertificationStatusMstInfo> objCertificationStatusMstEntityColl = new List<CertificationStatusMstInfo>(this.Select(cmd));
            return objCertificationStatusMstEntityColl;
        }

    }
}
