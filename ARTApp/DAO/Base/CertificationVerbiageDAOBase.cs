

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

    public abstract class CertificationVerbiageDAOBase : CustomAbstractDAO<CertificationVerbiageInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CertificationTypeID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONTYPEID = "CertificationTypeID";
        /// <summary>
        /// A static representation of column CertificationVerbiage
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONVERBIAGE = "CertificationVerbiage";
        /// <summary>
        /// A static representation of column CertificationVerbiageID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONVERBIAGEID = "CertificationVerbiageID";
        /// <summary>
        /// A static representation of column CertificationVerbiageLabelID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONVERBIAGELABELID = "CertificationVerbiageLabelID";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
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
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// Provides access to the name of the primary key column (CertificationVerbiageID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CertificationVerbiageID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CertificationVerbiage";

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
        public CertificationVerbiageDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CertificationVerbiage", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CertificationVerbiageInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CertificationVerbiageInfo</returns>
        protected override CertificationVerbiageInfo MapObject(System.Data.IDataReader r)
        {

            CertificationVerbiageInfo entity = new CertificationVerbiageInfo();


            try
            {
                int ordinal = r.GetOrdinal("CertificationVerbiageID");
                if (!r.IsDBNull(ordinal)) entity.CertificationVerbiageID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyID");
                if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationVerbiage");
                if (!r.IsDBNull(ordinal)) entity.CertificationVerbiage = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationVerbiageLabelID");
                if (!r.IsDBNull(ordinal)) entity.CertificationVerbiageLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationTypeID");
                if (!r.IsDBNull(ordinal)) entity.CertificationTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
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
        /// in CertificationVerbiageInfo object
        /// </summary>
        /// <param name="o">A CertificationVerbiageInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CertificationVerbiageInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CertificationVerbiage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCertificationTypeID = cmd.CreateParameter();
            parCertificationTypeID.ParameterName = "@CertificationTypeID";
            if (!entity.IsCertificationTypeIDNull)
                parCertificationTypeID.Value = entity.CertificationTypeID;
            else
                parCertificationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationTypeID);

            System.Data.IDbDataParameter parCertificationVerbiage = cmd.CreateParameter();
            parCertificationVerbiage.ParameterName = "@CertificationVerbiage";
            if (!entity.IsCertificationVerbiageNull)
                parCertificationVerbiage.Value = entity.CertificationVerbiage;
            else
                parCertificationVerbiage.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationVerbiage);

            System.Data.IDbDataParameter parCertificationVerbiageLabelID = cmd.CreateParameter();
            parCertificationVerbiageLabelID.ParameterName = "@CertificationVerbiageLabelID";
            if (!entity.IsCertificationVerbiageLabelIDNull)
                parCertificationVerbiageLabelID.Value = entity.CertificationVerbiageLabelID;
            else
                parCertificationVerbiageLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationVerbiageLabelID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

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

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CertificationVerbiageInfo object
        /// </summary>
        /// <param name="o">A CertificationVerbiageInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CertificationVerbiageInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CertificationVerbiage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCertificationTypeID = cmd.CreateParameter();
            parCertificationTypeID.ParameterName = "@CertificationTypeID";
            if (!entity.IsCertificationTypeIDNull)
                parCertificationTypeID.Value = entity.CertificationTypeID;
            else
                parCertificationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationTypeID);

            System.Data.IDbDataParameter parCertificationVerbiage = cmd.CreateParameter();
            parCertificationVerbiage.ParameterName = "@CertificationVerbiage";
            if (!entity.IsCertificationVerbiageNull)
                parCertificationVerbiage.Value = entity.CertificationVerbiage;
            else
                parCertificationVerbiage.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationVerbiage);

            System.Data.IDbDataParameter parCertificationVerbiageLabelID = cmd.CreateParameter();
            parCertificationVerbiageLabelID.ParameterName = "@CertificationVerbiageLabelID";
            if (!entity.IsCertificationVerbiageLabelIDNull)
                parCertificationVerbiageLabelID.Value = entity.CertificationVerbiageLabelID;
            else
                parCertificationVerbiageLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationVerbiageLabelID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

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

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter pkparCertificationVerbiageID = cmd.CreateParameter();
            pkparCertificationVerbiageID.ParameterName = "@CertificationVerbiageID";
            pkparCertificationVerbiageID.Value = entity.CertificationVerbiageID;
            cmdParams.Add(pkparCertificationVerbiageID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CertificationVerbiage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationVerbiageID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CertificationVerbiage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationVerbiageID";
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
        public IList<CertificationVerbiageInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CertificationVerbiageByCompanyID");
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
        public IList<CertificationVerbiageInfo> SelectAllByCertificationTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CertificationVerbiageByCertificationTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
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
        public IList<CertificationVerbiageInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CertificationVerbiageByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(CertificationVerbiageInfo entity, object id)
        {
            entity.CertificationVerbiageID = Convert.ToInt32(id);
        }

    }
}
