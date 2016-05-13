using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Client.Model;
using System.Data;

namespace SkyStem.ART.App.DAO.Base
{
    public abstract class AttributeConfigDAOBase : CustomAbstractDAO<AttributeConfigMstInfo>
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
        /// A static representation of column SortOrder
        /// </summary>
        public static readonly string COLUMN_SORTORDER = "SortOrder";
        /// <summary>
        /// A static representation of column WeekDayID
        /// </summary>
        public static readonly string COLUMN_WEEKDAYID = "WeekDayID";
        /// <summary>
        /// A static representation of column WeekDayName
        /// </summary>
        public static readonly string COLUMN_WEEKDAYNAME = "WeekDayName";
        /// <summary>
        /// A static representation of column WeekDayNameLabelID
        /// </summary>
        public static readonly string COLUMN_WEEKDAYNAMELABELID = "WeekDayNameLabelID";
        /// <summary>
        /// A static representation of column WeekDayNumber
        /// </summary>
        public static readonly string COLUMN_WEEKDAYNUMBER = "WeekDayNumber";
        /// <summary>
        /// Provides access to the name of the primary key column (WeekDayID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "WeekDayID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "WeekDayMst";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemART";


        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public AttributeConfigDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RoleConfigMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a WeekDayMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>WeekDayMstInfo</returns>

        protected override AttributeConfigMstInfo MapObject(System.Data.IDataReader r)
        {
            AttributeConfigMstInfo entity = new AttributeConfigMstInfo();
            entity.RoleConfigID = r.GetInt16Value("RoleConfigID");
            entity.RoleConfigNameLabelID = r.GetInt16Value("RoleConfigLabelID");
            return entity;
        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_WeekDayMst");
            //cmd.CommandType = CommandType.StoredProcedure;

            //IDataParameterCollection cmdParams = cmd.Parameters;

            //System.Data.IDbDataParameter par = cmd.CreateParameter();
            //par.ParameterName = "@WeekDayID";
            //par.Value = id;
            //cmdParams.Add(par);

            return cmd;

        }

        protected override System.Data.IDbCommand CreateInsertCommand(AttributeConfigMstInfo entity)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_WeekDayMst");
            return cmd;
        }

        protected override System.Data.IDbCommand CreateUpdateCommand(AttributeConfigMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_WeekDayMst");
            return cmd;
        }
        /// <summary>
        /// Creates the sql select command, using the passed in primary key
        /// </summary>
        /// <param name="o">The primary key of the object to select</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateSelectOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_WeekDayMst");
            //cmd.CommandType = CommandType.StoredProcedure;

            //IDataParameterCollection cmdParams = cmd.Parameters;

            //System.Data.IDbDataParameter par = cmd.CreateParameter();
            //par.ParameterName = "@WeekDayID";
            //par.Value = id;
            //cmdParams.Add(par);

            return cmd;

        }

        protected CompanyAttributeConfigInfo MapAttributeInfoObject(System.Data.IDataReader r)
        {
            CompanyAttributeConfigInfo entity = new CompanyAttributeConfigInfo();
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            //entity.SortOrder = r.GetInt32Value("SortOrder");
            entity.IsEnabled = r.GetBooleanValue("IsEnabled");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");

            entity.AttributeSetValueID = r.GetInt64Value("AttributeSetValueID");
            entity.AttributeSetID = r.GetInt64Value("AttributeSetID");
            entity.AttributeID = r.GetInt32Value("AttributeID");
            entity.ParentAttributeID = r.GetInt32Value("ParentAttributeID");
            entity.ReferenceID = r.GetInt32Value("ReferenceID");
            entity.Value = r.GetStringValue("Value");
            entity.ValueLabelID = r.GetInt32Value("ValueLabelID");

            return entity;
        }

        private void MapIdentity(WeekDayMstInfo entity, object id)
        {
            //entity.WeekDayID = Convert.ToInt16(id);
        }
    }
}
