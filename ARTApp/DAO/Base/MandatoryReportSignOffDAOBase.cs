

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

    public abstract class MandatoryReportSignOffDAOBase : CustomAbstractDAO<MandatoryReportSignOffInfo>
    {

        /// <summary>
        /// A static representation of column Comments
        /// </summary>
        public static readonly string COLUMN_COMMENTS = "Comments";
        /// <summary>
        /// A static representation of column MandatoryReportSignOffID
        /// </summary>
        public static readonly string COLUMN_MANDATORYREPORTSIGNOFFID = "MandatoryReportSignOffID";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// A static representation of column ReportRoleMandatoryReportID
        /// </summary>
        public static readonly string COLUMN_REPORTROLEMANDATORYREPORTID = "ReportRoleMandatoryReportID";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// A static representation of column SignOffDate
        /// </summary>
        public static readonly string COLUMN_SIGNOFFDATE = "SignOffDate";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// Provides access to the name of the primary key column (MandatoryReportSignOffID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MandatoryReportSignOffID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MandatoryReportSignOff";

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
        public MandatoryReportSignOffDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MandatoryReportSignOff", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MandatoryReportSignOffInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MandatoryReportSignOffInfo</returns>
        protected override MandatoryReportSignOffInfo MapObject(System.Data.IDataReader r)
        {

            MandatoryReportSignOffInfo entity = new MandatoryReportSignOffInfo();

            entity.MandatoryReportSignOffID = r.GetInt32Value("MandatoryReportSignOffID");
            entity.ReportRoleMandatoryReportID = r.GetInt32Value("ReportRoleMandatoryReportID");
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.SignOffDate = r.GetDateValue("SignOffDate");
            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.Comments = r.GetStringValue("Comments");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MandatoryReportSignOffInfo object
        /// </summary>
        /// <param name="o">A MandatoryReportSignOffInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MandatoryReportSignOffInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_MandatoryReportSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCertificationTypeID = cmd.CreateParameter();
            parCertificationTypeID.ParameterName = "@CertificationTypeID";
            if (!entity.IsCertificationTypeIDNull)
                parCertificationTypeID.Value = entity.CertificationTypeID;
            else
                parCertificationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationTypeID);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parReportRoleMandatoryReportID = cmd.CreateParameter();
            parReportRoleMandatoryReportID.ParameterName = "@ReportRoleMandatoryReportID";
            if (!entity.IsReportRoleMandatoryReportIDNull)
                parReportRoleMandatoryReportID.Value = entity.ReportRoleMandatoryReportID;
            else
                parReportRoleMandatoryReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportRoleMandatoryReportID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parSignOffDate = cmd.CreateParameter();
            parSignOffDate.ParameterName = "@SignOffDate";
            if (!entity.IsSignOffDateNull)
                parSignOffDate.Value = entity.SignOffDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parSignOffDate.Value = System.DBNull.Value;
            cmdParams.Add(parSignOffDate);

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
        /// in MandatoryReportSignOffInfo object
        /// </summary>
        /// <param name="o">A MandatoryReportSignOffInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MandatoryReportSignOffInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MandatoryReportSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parReportRoleMandatoryReportID = cmd.CreateParameter();
            parReportRoleMandatoryReportID.ParameterName = "@ReportRoleMandatoryReportID";
            if (!entity.IsReportRoleMandatoryReportIDNull)
                parReportRoleMandatoryReportID.Value = entity.ReportRoleMandatoryReportID;
            else
                parReportRoleMandatoryReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportRoleMandatoryReportID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parSignOffDate = cmd.CreateParameter();
            parSignOffDate.ParameterName = "@SignOffDate";
            if (!entity.IsSignOffDateNull)
                parSignOffDate.Value = entity.SignOffDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parSignOffDate.Value = System.DBNull.Value;
            cmdParams.Add(parSignOffDate);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter pkparMandatoryReportSignOffID = cmd.CreateParameter();
            pkparMandatoryReportSignOffID.ParameterName = "@MandatoryReportSignOffID";
            pkparMandatoryReportSignOffID.Value = entity.MandatoryReportSignOffID;
            cmdParams.Add(pkparMandatoryReportSignOffID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MandatoryReportSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MandatoryReportSignOffID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MandatoryReportSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MandatoryReportSignOffID";
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
        public List<MandatoryReportSignOffInfo> SelectAllByReportRoleMandatoryReportIDUserIDRecPeriodID(int? ReportRoleMandatoryReportID, int? userID, int? reconciliationPeriodID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_MandatoryReportSignoffStatusByReportRoleMandatoryReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReportRoleMandatoryReportID = cmd.CreateParameter();
            parReportRoleMandatoryReportID.ParameterName = "@ReportRoleMandatoryReportID";
            parReportRoleMandatoryReportID.Value = ReportRoleMandatoryReportID;
            cmdParams.Add(parReportRoleMandatoryReportID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<MandatoryReportSignOffInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MandatoryReportSignOffByReconciliationPeriodID");
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
        public IList<MandatoryReportSignOffInfo> SelectAllByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MandatoryReportSignOffByUserID");
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
        public IList<MandatoryReportSignOffInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MandatoryReportSignOffByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MandatoryReportSignOffInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MandatoryReportSignOffDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MandatoryReportSignOffInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MandatoryReportSignOffDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MandatoryReportSignOffInfo entity, object id)
        {
            entity.MandatoryReportSignOffID = Convert.ToInt32(id);
        }




    }
}
