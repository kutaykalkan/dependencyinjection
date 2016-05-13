

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

    public abstract class CertificationSignOffDAOBase : CustomAbstractDAO<CertificationSignOffInfo>
    {

        /// <summary>
        /// A static representation of column AccountCertificationComments
        /// </summary>
        public static readonly string COLUMN_ACCOUNTCERTIFICATIONCOMMENTS = "AccountCertificationComments";
        /// <summary>
        /// A static representation of column AccountCertificationDate
        /// </summary>
        public static readonly string COLUMN_ACCOUNTCERTIFICATIONDATE = "AccountCertificationDate";
        /// <summary>
        /// A static representation of column CertificationBalancesComments
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONBALANCESCOMMENTS = "CertificationBalancesComments";
        /// <summary>
        /// A static representation of column CertificationBalancesDate
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONBALANCESDATE = "CertificationBalancesDate";
        /// <summary>
        /// A static representation of column CertificationSignOffID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONSIGNOFFID = "CertificationSignOffID";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// A static representation of column ExceptionCertificationComments
        /// </summary>
        public static readonly string COLUMN_EXCEPTIONCERTIFICATIONCOMMENTS = "ExceptionCertificationComments";
        /// <summary>
        /// A static representation of column ExceptionCertificationDate
        /// </summary>
        public static readonly string COLUMN_EXCEPTIONCERTIFICATIONDATE = "ExceptionCertificationDate";
        /// <summary>
        /// A static representation of column MadatoryReportSignOffDate
        /// </summary>
        public static readonly string COLUMN_MADATORYREPORTSIGNOFFDATE = "MadatoryReportSignOffDate";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// Provides access to the name of the primary key column (CertificationSignOffID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CertificationSignOffID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CertificationSignOff";

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
        public CertificationSignOffDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CertificationSignOff", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CertificationSignOffInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CertificationSignOffInfo</returns>
        protected override CertificationSignOffInfo MapObject(System.Data.IDataReader r)
        {

            CertificationSignOffInfo entity = new CertificationSignOffInfo();


            try
            {
                int ordinal = r.GetOrdinal("CertificationSignOffID");
                if (!r.IsDBNull(ordinal)) entity.CertificationSignOffID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("ReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MadatoryReportSignOffDate");
                if (!r.IsDBNull(ordinal)) entity.MadatoryReportSignOffDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationBalancesDate");
                if (!r.IsDBNull(ordinal)) entity.CertificationBalancesDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationBalancesComments");
                if (!r.IsDBNull(ordinal)) entity.CertificationBalancesComments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ExceptionCertificationDate");
                if (!r.IsDBNull(ordinal)) entity.ExceptionCertificationDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ExceptionCertificationComments");
                if (!r.IsDBNull(ordinal)) entity.ExceptionCertificationComments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AccountCertificationDate");
                if (!r.IsDBNull(ordinal)) entity.AccountCertificationDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AccountCertificationComments");
                if (!r.IsDBNull(ordinal)) entity.AccountCertificationComments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserID");
                if (!r.IsDBNull(ordinal)) entity.UserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            //Extra fields
            try
            {
                int ordinal = r.GetOrdinal("FirstName");
                if (!r.IsDBNull(ordinal)) entity.UserFirstName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("LastName");
                if (!r.IsDBNull(ordinal)) entity.UserLastName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("IsSameAccess");
                if (!r.IsDBNull(ordinal)) entity.IsSameAccess = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CertificationSignOffInfo object
        /// </summary>
        /// <param name="o">A CertificationSignOffInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CertificationSignOffInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CertificationSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCertificationTypeID = cmd.CreateParameter();
            parCertificationTypeID.ParameterName = "@CertificationTypeID";
            if (!entity.IsCertificationTypeIDNull)
                parCertificationTypeID.Value = entity.CertificationTypeID;
            else
                parCertificationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationTypeID);

            System.Data.IDbDataParameter parSignOffComments = cmd.CreateParameter();
            parSignOffComments.ParameterName = "@SignOffComments";
            if (!entity.IsSignOffCommentsNull)
                parSignOffComments.Value = entity.SignOffComments;
            else
                parSignOffComments.Value = System.DBNull.Value;
            cmdParams.Add(parSignOffComments);

            System.Data.IDbDataParameter parSignOffDate = cmd.CreateParameter();
            parSignOffDate.ParameterName = "@SignOffDate";
            if (!entity.IsSignOffDateNull)
                parSignOffDate.Value = entity.SignOffDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parSignOffDate.Value = System.DBNull.Value;
            cmdParams.Add(parSignOffDate);


            //System.Data.IDbDataParameter parAccountCertificationComments = cmd.CreateParameter();
            //parAccountCertificationComments.ParameterName = "@AccountCertificationComments";
            //if(!entity.IsAccountCertificationCommentsNull)
            //    parAccountCertificationComments.Value = entity.AccountCertificationComments;
            //else
            //    parAccountCertificationComments.Value = System.DBNull.Value;
            //cmdParams.Add(parAccountCertificationComments);

            //System.Data.IDbDataParameter parAccountCertificationDate = cmd.CreateParameter();
            //parAccountCertificationDate.ParameterName = "@AccountCertificationDate";
            //if(!entity.IsAccountCertificationDateNull)
            //    parAccountCertificationDate.Value = entity.AccountCertificationDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parAccountCertificationDate.Value = System.DBNull.Value;
            //cmdParams.Add(parAccountCertificationDate);

            //System.Data.IDbDataParameter parCertificationBalancesComments = cmd.CreateParameter();
            //parCertificationBalancesComments.ParameterName = "@CertificationBalancesComments";
            //if(!entity.IsCertificationBalancesCommentsNull)
            //    parCertificationBalancesComments.Value = entity.CertificationBalancesComments;
            //else
            //    parCertificationBalancesComments.Value = System.DBNull.Value;
            //cmdParams.Add(parCertificationBalancesComments);

            //System.Data.IDbDataParameter parCertificationBalancesDate = cmd.CreateParameter();
            //parCertificationBalancesDate.ParameterName = "@CertificationBalancesDate";
            //if(!entity.IsCertificationBalancesDateNull)
            //    parCertificationBalancesDate.Value = entity.CertificationBalancesDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parCertificationBalancesDate.Value = System.DBNull.Value;
            //cmdParams.Add(parCertificationBalancesDate);

            //System.Data.IDbDataParameter parExceptionCertificationComments = cmd.CreateParameter();
            //parExceptionCertificationComments.ParameterName = "@ExceptionCertificationComments";
            //if(!entity.IsExceptionCertificationCommentsNull)
            //    parExceptionCertificationComments.Value = entity.ExceptionCertificationComments;
            //else
            //    parExceptionCertificationComments.Value = System.DBNull.Value;
            //cmdParams.Add(parExceptionCertificationComments);

            //System.Data.IDbDataParameter parExceptionCertificationDate = cmd.CreateParameter();
            //parExceptionCertificationDate.ParameterName = "@ExceptionCertificationDate";
            //if(!entity.IsExceptionCertificationDateNull)
            //    parExceptionCertificationDate.Value = entity.ExceptionCertificationDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parExceptionCertificationDate.Value = System.DBNull.Value;
            //cmdParams.Add(parExceptionCertificationDate);

            //System.Data.IDbDataParameter parMadatoryReportSignOffDate = cmd.CreateParameter();
            //parMadatoryReportSignOffDate.ParameterName = "@MadatoryReportSignOffDate";
            //if(!entity.IsMadatoryReportSignOffDateNull)
            //    parMadatoryReportSignOffDate.Value = entity.MadatoryReportSignOffDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parMadatoryReportSignOffDate.Value = System.DBNull.Value;
            //cmdParams.Add(parMadatoryReportSignOffDate);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CertificationSignOffInfo object
        /// </summary>
        /// <param name="o">A CertificationSignOffInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CertificationSignOffInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CertificationSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountCertificationComments = cmd.CreateParameter();
            parAccountCertificationComments.ParameterName = "@AccountCertificationComments";
            if (!entity.IsAccountCertificationCommentsNull)
                parAccountCertificationComments.Value = entity.AccountCertificationComments;
            else
                parAccountCertificationComments.Value = System.DBNull.Value;
            cmdParams.Add(parAccountCertificationComments);

            System.Data.IDbDataParameter parAccountCertificationDate = cmd.CreateParameter();
            parAccountCertificationDate.ParameterName = "@AccountCertificationDate";
            if (!entity.IsAccountCertificationDateNull)
                parAccountCertificationDate.Value = entity.AccountCertificationDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parAccountCertificationDate.Value = System.DBNull.Value;
            cmdParams.Add(parAccountCertificationDate);

            System.Data.IDbDataParameter parCertificationBalancesComments = cmd.CreateParameter();
            parCertificationBalancesComments.ParameterName = "@CertificationBalancesComments";
            if (!entity.IsCertificationBalancesCommentsNull)
                parCertificationBalancesComments.Value = entity.CertificationBalancesComments;
            else
                parCertificationBalancesComments.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationBalancesComments);

            System.Data.IDbDataParameter parCertificationBalancesDate = cmd.CreateParameter();
            parCertificationBalancesDate.ParameterName = "@CertificationBalancesDate";
            if (!entity.IsCertificationBalancesDateNull)
                parCertificationBalancesDate.Value = entity.CertificationBalancesDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCertificationBalancesDate.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationBalancesDate);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parExceptionCertificationComments = cmd.CreateParameter();
            parExceptionCertificationComments.ParameterName = "@ExceptionCertificationComments";
            if (!entity.IsExceptionCertificationCommentsNull)
                parExceptionCertificationComments.Value = entity.ExceptionCertificationComments;
            else
                parExceptionCertificationComments.Value = System.DBNull.Value;
            cmdParams.Add(parExceptionCertificationComments);

            System.Data.IDbDataParameter parExceptionCertificationDate = cmd.CreateParameter();
            parExceptionCertificationDate.ParameterName = "@ExceptionCertificationDate";
            if (!entity.IsExceptionCertificationDateNull)
                parExceptionCertificationDate.Value = entity.ExceptionCertificationDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parExceptionCertificationDate.Value = System.DBNull.Value;
            cmdParams.Add(parExceptionCertificationDate);

            System.Data.IDbDataParameter parMadatoryReportSignOffDate = cmd.CreateParameter();
            parMadatoryReportSignOffDate.ParameterName = "@MadatoryReportSignOffDate";
            if (!entity.IsMadatoryReportSignOffDateNull)
                parMadatoryReportSignOffDate.Value = entity.MadatoryReportSignOffDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parMadatoryReportSignOffDate.Value = System.DBNull.Value;
            cmdParams.Add(parMadatoryReportSignOffDate);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter pkparCertificationSignOffID = cmd.CreateParameter();
            pkparCertificationSignOffID.ParameterName = "@CertificationSignOffID";
            pkparCertificationSignOffID.Value = entity.CertificationSignOffID;
            cmdParams.Add(pkparCertificationSignOffID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CertificationSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationSignOffID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CertificationSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationSignOffID";
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
        public IList<CertificationSignOffInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CertificationSignOffByCompanyID");
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
        public List<CertificationSignOffInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CertificationSignOffByReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
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
        public IList<CertificationSignOffInfo> SelectAllByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CertificationSignOffByUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
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
        public IList<CertificationSignOffInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CertificationSignOffByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(CertificationSignOffInfo entity, object id)
        {
            entity.CertificationSignOffID = Convert.ToInt32(id);
        }
    }
}
