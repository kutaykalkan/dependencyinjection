

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

    public abstract class WizardStepDAOBase : CustomAbstractDAO<WizardStepInfo>
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
        /// A static representation of column DisplayOrder
        /// </summary>
        public static readonly string COLUMN_DISPLAYORDER = "DisplayOrder";
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
        /// A static representation of column WizardStepID
        /// </summary>
        public static readonly string COLUMN_WIZARDSTEPID = "WizardStepID";
        /// <summary>
        /// A static representation of column WizardStepName
        /// </summary>
        public static readonly string COLUMN_WIZARDSTEPNAME = "WizardStepName";
        /// <summary>
        /// A static representation of column WizardStepNameLabelID
        /// </summary>
        public static readonly string COLUMN_WIZARDSTEPNAMELABELID = "WizardStepNameLabelID";
        /// <summary>
        /// A static representation of column WizardStepURL
        /// </summary>
        public static readonly string COLUMN_WIZARDSTEPURL = "WizardStepURL";
        /// <summary>
        /// A static representation of column WizardTypeID
        /// </summary>
        public static readonly string COLUMN_WIZARDTYPEID = "WizardTypeID";
        /// <summary>
        /// Provides access to the name of the primary key column (WizardStepID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "WizardStepID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "WizardStep";

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
        public WizardStepDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "WizardStep", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a WizardStepInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>WizardStepInfo</returns>
        protected override WizardStepInfo MapObject(System.Data.IDataReader r)
        {

            WizardStepInfo entity = new WizardStepInfo();

            entity.WizardStepID = r.GetInt32Value("WizardStepID");
            entity.WizardStepName = r.GetStringValue("WizardStepName");
            entity.WizardStepNameLabelID = r.GetInt32Value("WizardStepNameLabelID");
            entity.WizardStepURL = r.GetStringValue("WizardStepURL");
            entity.DisplayOrder = r.GetInt32Value("DisplayOrder");
            entity.WizardTypeID = r.GetInt16Value("WizardTypeID");
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
        /// in WizardStepInfo object
        /// </summary>
        /// <param name="o">A WizardStepInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(WizardStepInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_WizardStep");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDisplayOrder = cmd.CreateParameter();
            parDisplayOrder.ParameterName = "@DisplayOrder";
            if (entity != null)
                parDisplayOrder.Value = entity.DisplayOrder;
            else
                parDisplayOrder.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayOrder);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parWizardStepID = cmd.CreateParameter();
            parWizardStepID.ParameterName = "@WizardStepID";
            if (entity != null)
                parWizardStepID.Value = entity.WizardStepID;
            else
                parWizardStepID.Value = System.DBNull.Value;
            cmdParams.Add(parWizardStepID);

            System.Data.IDbDataParameter parWizardStepName = cmd.CreateParameter();
            parWizardStepName.ParameterName = "@WizardStepName";
            if (entity != null)
                parWizardStepName.Value = entity.WizardStepName;
            else
                parWizardStepName.Value = System.DBNull.Value;
            cmdParams.Add(parWizardStepName);

            System.Data.IDbDataParameter parWizardStepNameLabelID = cmd.CreateParameter();
            parWizardStepNameLabelID.ParameterName = "@WizardStepNameLabelID";
            if (entity != null)
                parWizardStepNameLabelID.Value = entity.WizardStepNameLabelID;
            else
                parWizardStepNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parWizardStepNameLabelID);

            System.Data.IDbDataParameter parWizardStepURL = cmd.CreateParameter();
            parWizardStepURL.ParameterName = "@WizardStepURL";
            if (entity != null)
                parWizardStepURL.Value = entity.WizardStepURL;
            else
                parWizardStepURL.Value = System.DBNull.Value;
            cmdParams.Add(parWizardStepURL);

            System.Data.IDbDataParameter parWizardTypeID = cmd.CreateParameter();
            parWizardTypeID.ParameterName = "@WizardTypeID";
            if (entity != null)
                parWizardTypeID.Value = entity.WizardTypeID;
            else
                parWizardTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parWizardTypeID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in WizardStepInfo object
        /// </summary>
        /// <param name="o">A WizardStepInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(WizardStepInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_WizardStep");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDisplayOrder = cmd.CreateParameter();
            parDisplayOrder.ParameterName = "@DisplayOrder";
            if (entity != null)
                parDisplayOrder.Value = entity.DisplayOrder;
            else
                parDisplayOrder.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayOrder);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parWizardStepName = cmd.CreateParameter();
            parWizardStepName.ParameterName = "@WizardStepName";
            if (entity != null)
                parWizardStepName.Value = entity.WizardStepName;
            else
                parWizardStepName.Value = System.DBNull.Value;
            cmdParams.Add(parWizardStepName);

            System.Data.IDbDataParameter parWizardStepNameLabelID = cmd.CreateParameter();
            parWizardStepNameLabelID.ParameterName = "@WizardStepNameLabelID";
            if (entity != null)
                parWizardStepNameLabelID.Value = entity.WizardStepNameLabelID;
            else
                parWizardStepNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parWizardStepNameLabelID);

            System.Data.IDbDataParameter parWizardStepURL = cmd.CreateParameter();
            parWizardStepURL.ParameterName = "@WizardStepURL";
            if (entity != null)
                parWizardStepURL.Value = entity.WizardStepURL;
            else
                parWizardStepURL.Value = System.DBNull.Value;
            cmdParams.Add(parWizardStepURL);

            System.Data.IDbDataParameter parWizardTypeID = cmd.CreateParameter();
            parWizardTypeID.ParameterName = "@WizardTypeID";
            if (entity != null)
                parWizardTypeID.Value = entity.WizardTypeID;
            else
                parWizardTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parWizardTypeID);

            System.Data.IDbDataParameter pkparWizardStepID = cmd.CreateParameter();
            pkparWizardStepID.ParameterName = "@WizardStepID";
            pkparWizardStepID.Value = entity.WizardStepID;
            cmdParams.Add(pkparWizardStepID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_WizardStep");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@WizardStepID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_WizardStep");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@WizardStepID";
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
        public IList<WizardStepInfo> SelectAllByWizardTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_WizardStepByWizardTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@WizardTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }
    }
}
