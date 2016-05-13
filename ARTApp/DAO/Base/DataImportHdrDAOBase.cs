

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

    public abstract class DataImportHdrDAOBase : CustomAbstractDAO<DataImportHdrInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// A static representation of column DataImportID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
        /// <summary>
        /// A static representation of column DataImportName
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTNAME = "DataImportName";
        /// <summary>
        /// A static representation of column DataImportStatusID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTSTATUSID = "DataImportStatusID";
        /// <summary>
        /// A static representation of column DataImportTypeID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTTYPEID = "DataImportTypeID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column FileName
        /// </summary>
        public static readonly string COLUMN_FILENAME = "FileName";
        /// <summary>
        /// A static representation of column FileSize
        /// </summary>
        public static readonly string COLUMN_FILESIZE = "FileSize";
        /// <summary>
        /// A static representation of column ForceCommitDate
        /// </summary>
        public static readonly string COLUMN_FORCECOMMITDATE = "ForceCommitDate";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column IsForceCommit
        /// </summary>
        public static readonly string COLUMN_ISFORCECOMMIT = "IsForceCommit";
        /// <summary>
        /// A static representation of column PhysicalPath
        /// </summary>
        public static readonly string COLUMN_PHYSICALPATH = "PhysicalPath";
        /// <summary>
        /// A static representation of column RecordsImported
        /// </summary>
        public static readonly string COLUMN_RECORDSIMPORTED = "RecordsImported";
        /// <summary>
        /// A static representation of column ReonciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_REONCILIATIONPERIODID = "ReonciliationPeriodID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";

        public static readonly string COLUMN_NOTIFYSUCESSEMAILIDS = "NotifySuccessEmailIDs";
        public static readonly string COLUMN_NOTIFYSUCESSUSEREMAILIDS = "NotifySuccessUserEmailIDs";
        public static readonly string COLUMN_NOTIFYFAILUREEMAILIDS = "NotifyFailureEmailIDs";
        public static readonly string COLUMN_NOTIFYFAILUREUSEREMAILIDS = "NotifyFailureUserEmailIDs";
        /// <summary>
        /// Provides access to the name of the primary key column (DataImportID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DataImportID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DataImportHdr";

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
        public DataImportHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DataImportHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DataImportHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DataImportHdrInfo</returns>
        protected override DataImportHdrInfo MapObject(System.Data.IDataReader r)
        {

            DataImportHdrInfo entity = new DataImportHdrInfo();

            entity.DataImportID = r.GetInt32Value("DataImportID");
            entity.DataImportName = r.GetStringValue("DataImportName");
            entity.FileName = r.GetStringValue("FileName");
            entity.PhysicalPath = r.GetStringValue("PhysicalPath");
            entity.FileSize = r.GetDecimalValue("FileSize");
            entity.CompanyID = r.GetInt32Value("CompanyID");
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.DataImportTypeID = r.GetInt16Value("DataImportTypeID");
            entity.DataImportTypeLabelID = r.GetInt16Value("DataImportTypeLabelID");
            entity.DataImportStatusID = r.GetInt16Value("DataImportStatusID");
            entity.DataImportStatusLabelID = r.GetInt16Value("DataImportStatusLabelID");
            entity.RecordsImported = r.GetInt32Value("RecordsImported");
            entity.IsForceCommit = r.GetBooleanValue("IsForceCommit");
            entity.ForceCommitDate = r.GetDateValue("ForceCommitDate");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.NetValue = r.GetDecimalValue("NetValue");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.NotifySuccessEmailIDs = r.GetStringValue("NotifySuccessEmailIDs");
            entity.NotifySuccessUserEmailIDs = r.GetStringValue("NotifySuccessUserEmailIDs");
            entity.NotifyFailureEmailIDs = r.GetStringValue("NotifyFailureEmailIDs");
            entity.NotifyFailureUserEmailIDs = r.GetStringValue("NotifyFailureUserEmailIDs");
            entity.DataImportFailureMessageInfo = new DataImportFailureMessageInfo();
            entity.DataImportFailureMessageInfo.DataImportFailureMessageID = r.GetInt32Value("DataImportFailureMessageID");
            entity.DataImportFailureMessageInfo.FailureMessage = r.GetStringValue("FailureMessage");
            entity.IsHidden = (r.GetBooleanValue("IsHidden") != null ? r.GetBooleanValue("IsHidden") : false);
            entity.IsMultiVersionUpload = r.GetBooleanValue("IsMultiVersionUpload");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.TemplateName = r.GetStringValue("TemplateName");
            entity.RecordSourceTypeID = r.GetInt16Value("RecordSourceTypeID");
            return entity;
        }

        ///// <summary>
        ///// Maps the IDataReader values to a DataImportHdrInfo object
        ///// Apoorv: DO NOT USE
        ///// </summary>
        ///// <param name="r">The IDataReader to map</param>
        ///// <returns>DataImportHdrInfo</returns>
        //protected DataImportHdrInfo MapObject_DO_NOT_USE(System.Data.IDataReader r)
        //{

        //    DataImportHdrInfo entity = new DataImportHdrInfo();

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("DataImportID");
        //        if (!r.IsDBNull(ordinal)) entity.DataImportID = ((System.Int32)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("DataImportName");
        //        if (!r.IsDBNull(ordinal)) entity.DataImportName = ((System.String)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("FileName");
        //        if (!r.IsDBNull(ordinal)) entity.FileName = ((System.String)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("PhysicalPath");
        //        if (!r.IsDBNull(ordinal)) entity.PhysicalPath = ((System.String)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("FileSize");
        //        if (!r.IsDBNull(ordinal)) entity.FileSize = ((System.Decimal)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("CompanyID");
        //        if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("ReconciliationPeriodID");
        //        if (!r.IsDBNull(ordinal)) entity.ReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("DataImportTypeID");
        //        if (!r.IsDBNull(ordinal)) entity.DataImportTypeID = ((System.Int16)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("DataImportStatusID");
        //        if (!r.IsDBNull(ordinal)) entity.DataImportStatusID = ((System.Int16)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("RecordsImported");
        //        if (!r.IsDBNull(ordinal)) entity.RecordsImported = ((System.Int32)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("IsForceCommit");
        //        if (!r.IsDBNull(ordinal)) entity.IsForceCommit = ((System.Boolean)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("ForceCommitDate");
        //        if (!r.IsDBNull(ordinal)) entity.ForceCommitDate = ((System.DateTime)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("IsActive");
        //        if (!r.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("DateAdded");
        //        if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("AddedBy");
        //        if (!r.IsDBNull(ordinal)) entity.AddedBy = ((System.String)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("DateRevised");
        //        if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("RevisedBy");
        //        if (!r.IsDBNull(ordinal)) entity.RevisedBy = ((System.String)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    try
        //    {
        //        int ordinal = r.GetOrdinal("HostName");
        //        if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
        //    }
        //    catch (Exception) { }

        //    return entity;
        //}

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in DataImportHdrInfo object
        /// </summary>
        /// <param name="o">A DataImportHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DataImportHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_DataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity.CompanyID.HasValue)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parDataImportName = cmd.CreateParameter();
            parDataImportName.ParameterName = "@DataImportName";
            if (!entity.IsDataImportNameNull)
                parDataImportName.Value = entity.DataImportName;
            else
                parDataImportName.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportName);

            System.Data.IDbDataParameter parDataImportStatusID = cmd.CreateParameter();
            parDataImportStatusID.ParameterName = "@DataImportStatusID";
            if (!entity.IsDataImportStatusIDNull)
                parDataImportStatusID.Value = entity.DataImportStatusID;
            else
                parDataImportStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatusID);

            System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
            parDataImportTypeID.ParameterName = "@DataImportTypeID";
            if (!entity.IsDataImportTypeIDNull)
                parDataImportTypeID.Value = entity.DataImportTypeID;
            else
                parDataImportTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportTypeID);

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

            System.Data.IDbDataParameter parForceCommitDate = cmd.CreateParameter();
            parForceCommitDate.ParameterName = "@ForceCommitDate";
            if (!entity.IsForceCommitDateNull)
                parForceCommitDate.Value = entity.ForceCommitDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parForceCommitDate.Value = System.DBNull.Value;
            cmdParams.Add(parForceCommitDate);

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

            System.Data.IDbDataParameter parIsForceCommit = cmd.CreateParameter();
            parIsForceCommit.ParameterName = "@IsForceCommit";
            if (!entity.IsIsForceCommitNull)
                parIsForceCommit.Value = entity.IsForceCommit;
            else
                parIsForceCommit.Value = System.DBNull.Value;
            cmdParams.Add(parIsForceCommit);

            System.Data.IDbDataParameter parPhysicalPath = cmd.CreateParameter();
            parPhysicalPath.ParameterName = "@PhysicalPath";
            if (!entity.IsPhysicalPathNull)
                parPhysicalPath.Value = entity.PhysicalPath;
            else
                parPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parPhysicalPath);

            System.Data.IDbDataParameter parRecordsImported = cmd.CreateParameter();
            parRecordsImported.ParameterName = "@RecordsImported";
            if (!entity.IsRecordsImportedNull)
                parRecordsImported.Value = entity.RecordsImported;
            else
                parRecordsImported.Value = System.DBNull.Value;
            cmdParams.Add(parRecordsImported);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (entity.ReconciliationPeriodID.HasValue)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID.Value;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parNotifySuccessEmailIDs = cmd.CreateParameter();
            parNotifySuccessEmailIDs.ParameterName = "@NotifySuccessEmailIDs";
            if (!string.IsNullOrEmpty(entity.NotifySuccessEmailIDs))
                parNotifySuccessEmailIDs.Value = entity.NotifySuccessEmailIDs;
            else
                parNotifySuccessEmailIDs.Value = System.DBNull.Value;
            cmdParams.Add(parNotifySuccessEmailIDs);

            System.Data.IDbDataParameter parNotifySuccessUserEmailIDs = cmd.CreateParameter();
            parNotifySuccessUserEmailIDs.ParameterName = "@NotifySuccessUserEmailIDs";
            if (!string.IsNullOrEmpty(entity.NotifySuccessUserEmailIDs))
                parNotifySuccessUserEmailIDs.Value = entity.NotifySuccessUserEmailIDs;
            else
                parNotifySuccessUserEmailIDs.Value = System.DBNull.Value;
            cmdParams.Add(parNotifySuccessUserEmailIDs);

            System.Data.IDbDataParameter parNotifyFailureEmailIDs = cmd.CreateParameter();
            parNotifyFailureEmailIDs.ParameterName = "@NotifyFailureEmailIDs";
            if (!string.IsNullOrEmpty(entity.NotifyFailureEmailIDs))
                parNotifyFailureEmailIDs.Value = entity.NotifyFailureEmailIDs;
            else
                parNotifyFailureEmailIDs.Value = System.DBNull.Value;
            cmdParams.Add(parNotifyFailureEmailIDs);



            System.Data.IDbDataParameter parNotifyFailureUserEmailIDs = cmd.CreateParameter();
            parNotifyFailureUserEmailIDs.ParameterName = "@NotifyFailureUserEmailIDs";
            if (!string.IsNullOrEmpty(entity.NotifyFailureUserEmailIDs))
                parNotifyFailureUserEmailIDs.Value = entity.NotifyFailureUserEmailIDs;
            else
                parNotifyFailureUserEmailIDs.Value = System.DBNull.Value;
            cmdParams.Add(parNotifyFailureUserEmailIDs);

            System.Data.IDbDataParameter parLanguageId = cmd.CreateParameter();
            parLanguageId.ParameterName = "@LanguageID";
            if (entity.LanguageID.HasValue && entity.LanguageID.Value > 0)
                parLanguageId.Value = entity.LanguageID;
            else
                parLanguageId.Value = System.DBNull.Value;
            cmdParams.Add(parLanguageId);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity.RoleID.HasValue && entity.RoleID.Value > 0)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);


            System.Data.IDbDataParameter parIsMultiVersionUpload = cmd.CreateParameter();
            parIsMultiVersionUpload.ParameterName = "@IsMultiVersionUpload";
            if (entity.IsMultiVersionUpload.HasValue)
                parIsMultiVersionUpload.Value = entity.IsMultiVersionUpload.Value;
            else
                parIsMultiVersionUpload.Value = System.DBNull.Value;
            cmdParams.Add(parIsMultiVersionUpload);

            System.Data.IDbDataParameter parImportTemplateID = cmd.CreateParameter();
            parImportTemplateID.ParameterName = "@ImportTemplateID";
            if (entity.ImportTemplateID.HasValue)
                parImportTemplateID.Value = entity.ImportTemplateID.Value;
            else
                parImportTemplateID.Value = System.DBNull.Value;
            cmdParams.Add(parImportTemplateID);

            System.Data.IDbDataParameter parRecordSourceTypeID = cmd.CreateParameter();
            parRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            if (entity.RecordSourceTypeID.HasValue)
                parRecordSourceTypeID.Value = entity.RecordSourceTypeID.Value;
            else
                parRecordSourceTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecordSourceTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in DataImportHdrInfo object
        /// </summary>
        /// <param name="o">A DataImportHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DataImportHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parDataImportName = cmd.CreateParameter();
            parDataImportName.ParameterName = "@DataImportName";
            if (!entity.IsDataImportNameNull)
                parDataImportName.Value = entity.DataImportName;
            else
                parDataImportName.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportName);

            System.Data.IDbDataParameter parDataImportStatusID = cmd.CreateParameter();
            parDataImportStatusID.ParameterName = "@DataImportStatusID";
            if (!entity.IsDataImportStatusIDNull)
                parDataImportStatusID.Value = entity.DataImportStatusID;
            else
                parDataImportStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatusID);

            System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
            parDataImportTypeID.ParameterName = "@DataImportTypeID";
            if (!entity.IsDataImportTypeIDNull)
                parDataImportTypeID.Value = entity.DataImportTypeID;
            else
                parDataImportTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportTypeID);

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

            System.Data.IDbDataParameter parForceCommitDate = cmd.CreateParameter();
            parForceCommitDate.ParameterName = "@ForceCommitDate";
            if (!entity.IsForceCommitDateNull)
                parForceCommitDate.Value = entity.ForceCommitDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parForceCommitDate.Value = System.DBNull.Value;
            cmdParams.Add(parForceCommitDate);

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

            System.Data.IDbDataParameter parIsForceCommit = cmd.CreateParameter();
            parIsForceCommit.ParameterName = "@IsForceCommit";
            if (!entity.IsIsForceCommitNull)
                parIsForceCommit.Value = entity.IsForceCommit;
            else
                parIsForceCommit.Value = System.DBNull.Value;
            cmdParams.Add(parIsForceCommit);

            System.Data.IDbDataParameter parPhysicalPath = cmd.CreateParameter();
            parPhysicalPath.ParameterName = "@PhysicalPath";
            if (!entity.IsPhysicalPathNull)
                parPhysicalPath.Value = entity.PhysicalPath;
            else
                parPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parPhysicalPath);

            System.Data.IDbDataParameter parRecordsImported = cmd.CreateParameter();
            parRecordsImported.ParameterName = "@RecordsImported";
            if (!entity.IsRecordsImportedNull)
                parRecordsImported.Value = entity.RecordsImported;
            else
                parRecordsImported.Value = System.DBNull.Value;
            cmdParams.Add(parRecordsImported);

            System.Data.IDbDataParameter parReonciliationPeriodID = cmd.CreateParameter();
            parReonciliationPeriodID.ParameterName = "@ReonciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReonciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReonciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReonciliationPeriodID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparDataImportID = cmd.CreateParameter();
            pkparDataImportID.ParameterName = "@DataImportID";
            pkparDataImportID.Value = entity.DataImportID;
            cmdParams.Add(pkparDataImportID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
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
        public IList<DataImportHdrInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportHdrByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
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
        public IList<DataImportHdrInfo> SelectAllByReonciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportHdrByReonciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReonciliationPeriodID";
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
        public IList<DataImportHdrInfo> SelectAllByDataImportTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportHdrByDataImportTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportTypeID";
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
        public IList<DataImportHdrInfo> SelectAllByDataImportStatusID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportHdrByDataImportStatusID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(DataImportHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(DataImportHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(DataImportHdrInfo entity, object id)
        {
            entity.DataImportID = Convert.ToInt32(id);
        }










        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToAccountHdrByGLDataHdr(AccountHdrInfo entity)
        {
            return this.SelectDataImportHdrDetailsAssociatedToAccountHdrByGLDataHdr(entity.AccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToAccountHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportHdr] INNER JOIN [GLDataHdr] ON [DataImportHdr].[DataImportID] = [GLDataHdr].[DataImportID] INNER JOIN [AccountHdr] ON [GLDataHdr].[AccountID] = [AccountHdr].[AccountID]  WHERE  [AccountHdr].[AccountID] = @AccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportHdrInfo> objDataImportHdrEntityColl = new List<DataImportHdrInfo>(this.Select(cmd));
            return objDataImportHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationStatusMstByGLDataHdr(ReconciliationStatusMstInfo entity)
        {
            return this.SelectDataImportHdrDetailsAssociatedToReconciliationStatusMstByGLDataHdr(entity.ReconciliationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportHdr] INNER JOIN [GLDataHdr] ON [DataImportHdr].[DataImportID] = [GLDataHdr].[DataImportID] INNER JOIN [ReconciliationStatusMst] ON [GLDataHdr].[ReconciliationStatusID] = [ReconciliationStatusMst].[ReconciliationStatusID]  WHERE  [ReconciliationStatusMst].[ReconciliationStatusID] = @ReconciliationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportHdrInfo> objDataImportHdrEntityColl = new List<DataImportHdrInfo>(this.Select(cmd));
            return objDataImportHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToCertificationStatusMstByGLDataHdr(CertificationStatusMstInfo entity)
        {
            return this.SelectDataImportHdrDetailsAssociatedToCertificationStatusMstByGLDataHdr(entity.CertificationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToCertificationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportHdr] INNER JOIN [GLDataHdr] ON [DataImportHdr].[DataImportID] = [GLDataHdr].[DataImportID] INNER JOIN [CertificationStatusMst] ON [GLDataHdr].[CertificationStatusID] = [CertificationStatusMst].[CertificationStatusID]  WHERE  [CertificationStatusMst].[CertificationStatusID] = @CertificationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportHdrInfo> objDataImportHdrEntityColl = new List<DataImportHdrInfo>(this.Select(cmd));
            return objDataImportHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationPeriodByGLDataHdr(ReconciliationPeriodInfo entity)
        {
            return this.SelectDataImportHdrDetailsAssociatedToReconciliationPeriodByGLDataHdr(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationPeriodByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportHdr] INNER JOIN [GLDataHdr] ON [DataImportHdr].[DataImportID] = [GLDataHdr].[DataImportID] INNER JOIN [ReconciliationPeriod] ON [GLDataHdr].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportHdrInfo> objDataImportHdrEntityColl = new List<DataImportHdrInfo>(this.Select(cmd));
            return objDataImportHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(GLDataHdrInfo entity)
        {
            return this.SelectDataImportHdrDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportHdr] INNER JOIN [GLReconciliationItemInput] ON [DataImportHdr].[DataImportID] = [GLReconciliationItemInput].[DataImportID] INNER JOIN [GLDataHdr] ON [GLReconciliationItemInput].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportHdrInfo> objDataImportHdrEntityColl = new List<DataImportHdrInfo>(this.Select(cmd));
            return objDataImportHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(ReconciliationCategoryMstInfo entity)
        {
            return this.SelectDataImportHdrDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(entity.ReconciliationCategoryID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportHdr] INNER JOIN [GLReconciliationItemInput] ON [DataImportHdr].[DataImportID] = [GLReconciliationItemInput].[DataImportID] INNER JOIN [ReconciliationCategoryMst] ON [GLReconciliationItemInput].[ReconciliationCategoryID] = [ReconciliationCategoryMst].[ReconciliationCategoryID]  WHERE  [ReconciliationCategoryMst].[ReconciliationCategoryID] = @ReconciliationCategoryID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportHdrInfo> objDataImportHdrEntityColl = new List<DataImportHdrInfo>(this.Select(cmd));
            return objDataImportHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(ReconciliationCategoryTypeMstInfo entity)
        {
            return this.SelectDataImportHdrDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(entity.ReconciliationCategoryTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportHdrInfo> SelectDataImportHdrDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportHdr] INNER JOIN [GLReconciliationItemInput] ON [DataImportHdr].[DataImportID] = [GLReconciliationItemInput].[DataImportID] INNER JOIN [ReconciliationCategoryTypeMst] ON [GLReconciliationItemInput].[ReconciliationCategoryTypeID] = [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID]  WHERE  [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID] = @ReconciliationCategoryTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportHdrInfo> objDataImportHdrEntityColl = new List<DataImportHdrInfo>(this.Select(cmd));
            return objDataImportHdrEntityColl;
        }

    }
}
