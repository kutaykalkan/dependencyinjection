using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.DAO.Base
{
    public abstract class DataImportTemplateDAOBase : CustomAbstractDAO<ImportTemplateHdrInfo>
    {
        /// <summary>
        /// A static representation of column PhysicalPath
        /// </summary>
        public static readonly string COLUMN_PHYSICALPATH = "PhysicalPath";

        /// <summary>
        /// A static representation of column TemplateFileName
        /// </summary>
        public static readonly string COLUMN_TEMPLATEFILENAME = "TemplateFileName";

        /// <summary>
        /// A static representation of column SheetName
        /// </summary>
        public static readonly string COLUMN_SHEETNAME = "SheetName";
        /// <summary>
        /// A static representation of column LanguageID
        /// </summary>
        public static readonly string COLUMN_LANGUAGEID = "LanguageID";
        /// <summary>
        /// A static representation of column TemplateName
        /// </summary>
        public static readonly string COLUMN_TEMPLATENAME = "TemplateName";
        /// <summary>
        /// A static representation of column DataImportTypeID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTTYPEID = "DataImportTypeID";
        /// <summary>
        /// Provides access to the name of the primary key column (ImportTemplateID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ImportTemplateID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ImportTemplateHdr";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";

        public AppUserInfo CurrentAppUserInfo { get; set; }


        public DataImportTemplateDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RoleConfigMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }


        protected override ImportTemplateHdrInfo MapObject(System.Data.IDataReader r)
        {
            ImportTemplateHdrInfo entity = new ImportTemplateHdrInfo();
            entity.TemplateName = r.GetStringValue("TemplateName");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DataImportTypeID = r.GetInt16Value("DataImportTypeID");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.DataImportType = r.GetStringValue("DataImportType");
            entity.DataImportTypeLabelID = r.GetInt32Value("DataImportTypeLabelID");
            return entity;
        }

        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ImportTemplateHdr");
            return cmd;
        }

        protected override System.Data.IDbCommand CreateInsertCommand(ImportTemplateHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ImportTemplateHdr");
            return cmd;
        }

        protected override System.Data.IDbCommand CreateUpdateCommand(ImportTemplateHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ImportTemplateHdr");
            return cmd;
        }

        protected override System.Data.IDbCommand CreateSelectOneCommand(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ImportTemplateHdr");
            return cmd;
        }
        private void MapIdentity(WeekDayMstInfo entity, object id)
        {
        }

        public ImportTemplateFieldsInfo MapTemplateFieldsObject(IDataReader r)
        {
            ImportTemplateFieldsInfo entity = new ImportTemplateFieldsInfo();
            entity.FieldName = r.GetStringValue("FieldName");
            entity.ImportTemplateFieldID = Convert.ToInt32(r.GetInt32Value("ImportTemplateFieldID"));
            return entity;
        }

        public ImportFieldMstInfo MapFieldsObject(IDataReader r)
        {
            ImportFieldMstInfo entity = new ImportFieldMstInfo();
            entity.ImportFieldID = r.GetInt16Value("ImportFieldID").GetValueOrDefault();
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID").GetValueOrDefault();
            entity.GeographyClassID = r.GetInt32Value("GeographyClassID");
            return entity;
        }

        public ImportTemplateHdrInfo MapAllTemplateObject(IDataReader r)
        {
            ImportTemplateHdrInfo entity = new ImportTemplateHdrInfo();
            entity.TemplateName = r.GetStringValue("TemplateName");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.LanguageID = r.GetInt32Value("LanguageID");
            entity.SheetName = r.GetStringValue("SheetName");
            entity.TemplateFileName = r.GetStringValue("TemplateFileName");
            entity.ImportTemplateID = r.GetInt32Value("ImportTemplateID");
            entity.LanguageName = r.GetStringValue("Language");
            entity.DataImportTypeID = r.GetInt16Value("DataImportTypeID");
            entity.DataImportTypeLabelID = r.GetInt16Value("DataImportTypeLabelID");
            entity.PhysicalPath = r.GetStringValue("PhysicalPath");
            entity.DataImportTemplateID = r.GetInt32Value("DataImportTemplateID");
            entity.NumberOfMappedColumns = r.GetInt32Value("NumberOfMappedColumns");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");

            return entity;
        }

        public ImportTemplateFieldMappingInfo MapObjectMapping(IDataReader r)
        {
            ImportTemplateFieldMappingInfo entity = new ImportTemplateFieldMappingInfo();
            entity.ImportTemplateFieldID = r.GetInt32Value("ImportTemplateFieldID");
            entity.ImportFieldID = r.GetInt16Value("ImportFieldID");
            entity.ImportTemplateField = r.GetStringValue("ImportTemplateField");
            entity.ImportField = r.GetStringValue("ImportField");
            entity.ImportFieldLabelID = r.GetInt32Value("ImportFieldLabelID");
            return entity;
        }

        public DataImportScheduleInfo MapObjectDataImportSchedule(IDataReader r)
        {
            DataImportScheduleInfo entity = new DataImportScheduleInfo();
            entity.DataImportScheduleID = r.GetInt32Value("DataImportScheduleID").GetValueOrDefault();
            entity.DataImportTypeID = r.GetInt16Value("DataImportTypeID").GetValueOrDefault();
            entity.UserID = r.GetInt16Value("UserID").GetValueOrDefault();
            entity.RoleID = r.GetInt16Value("RoleID").GetValueOrDefault();
            entity.Description = r.GetStringValue("Description");
            entity.Hours = r.GetInt16Value("Hours");
            entity.Minutes = r.GetInt16Value("Minutes");
            entity.HoursDefaultValue = r.GetInt16Value("HoursDefaultValue");
            entity.MinutesDefaultValue = r.GetInt16Value("MinutesDefaultValue");
            return entity;
        }

        public DataImportMessageInfo MapObjectDataImportMessage(IDataReader r)
        {
            DataImportMessageInfo entity = new DataImportMessageInfo();
            entity.DataImportMessageID = r.GetInt16Value("DataImportMessageID").GetValueOrDefault();
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID").GetValueOrDefault();
            entity.DataImportMessageLabelID = r.GetInt32Value("DataImportMessageLabelID").GetValueOrDefault();
            return entity;
        }

        public DataImportWarningPreferencesInfo MapObjectoDataImportWarningPreferences(IDataReader r)
        {
            DataImportWarningPreferencesInfo entity = new DataImportWarningPreferencesInfo();
            entity.DataImportWarningPreferencesID = r.GetInt32Value("DataImportWarningPreferencesID").GetValueOrDefault();
            entity.DataImportMessageID = r.GetInt16Value("DataImportMessageID").GetValueOrDefault();
            entity.IsEnabled = r.GetBooleanValue("IsEnabled").GetValueOrDefault();
            return entity;
        }

        public DataImportMessageInfo MapObjectDataImportMessageList(IDataReader r)
        {
            DataImportMessageInfo entity = new DataImportMessageInfo();
            entity.DataImportMessageID = r.GetInt16Value("DataImportMessageID").GetValueOrDefault();
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID").GetValueOrDefault();
            entity.DataImportMessageLabelID = r.GetInt32Value("DataImportMessageLabelID").GetValueOrDefault();
            entity.DataImportMessageTypeID = r.GetInt32Value("DataImportMessageTypeID").GetValueOrDefault();
            return entity;
        }

        public DataImportWarningPreferencesAuditInfo MapObjectDataImportWarningPreferencesAuditList(IDataReader r)
        {
            DataImportWarningPreferencesAuditInfo entity = new DataImportWarningPreferencesAuditInfo();
            entity.DataImportWarningPreferencesAuditId = r.GetInt32Value("DataImportWarningPreferencesAuditId").GetValueOrDefault();
            entity.DataImportMessageLabelID = r.GetInt32Value("DataImportMessageLabelID").GetValueOrDefault();
            entity.IsEnabled = r.GetBooleanValue("IsEnabled").GetValueOrDefault();
            entity.ChangeDate = r.GetDateValue("ChangeDate");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.DataImportTypeLabelID = r.GetInt32Value("DataImportTypeLabelID").GetValueOrDefault();
            entity.RoleLabelID = r.GetInt32Value("RoleLabelID").GetValueOrDefault();
            return entity;
        }
    }
}