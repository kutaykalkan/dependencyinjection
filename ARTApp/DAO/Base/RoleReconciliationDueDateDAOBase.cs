

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

    public abstract class RoleReconciliationDueDateDAOBase : CustomAbstractDAO<RoleReconciliationDueDateInfo>
    {

        /// <summary>
        /// A static representation of column ReconciliationDueDate
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONDUEDATE = "ReconciliationDueDate";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// A static representation of column RoleReconciliationDueDateID
        /// </summary>
        public static readonly string COLUMN_ROLERECONCILIATIONDUEDATEID = "RoleReconciliationDueDateID";
        /// <summary>
        /// Provides access to the name of the primary key column (RoleReconciliationDueDateID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RoleReconciliationDueDateID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RoleReconciliationDueDate";

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
        public RoleReconciliationDueDateDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RoleReconciliationDueDate", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RoleReconciliationDueDateInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RoleReconciliationDueDateInfo</returns>
        protected override RoleReconciliationDueDateInfo MapObject(System.Data.IDataReader r)
        {

            RoleReconciliationDueDateInfo entity = new RoleReconciliationDueDateInfo();


            try
            {
                int ordinal = r.GetOrdinal("RoleReconciliationDueDateID");
                if (!r.IsDBNull(ordinal)) entity.RoleReconciliationDueDateID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("ReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationDueDate");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationDueDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in RoleReconciliationDueDateInfo object
        /// </summary>
        /// <param name="o">A RoleReconciliationDueDateInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RoleReconciliationDueDateInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RoleReconciliationDueDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parReconciliationDueDate = cmd.CreateParameter();
            parReconciliationDueDate.ParameterName = "@ReconciliationDueDate";
            if (!entity.IsReconciliationDueDateNull)
                parReconciliationDueDate.Value = entity.ReconciliationDueDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parReconciliationDueDate.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationDueDate);

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

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in RoleReconciliationDueDateInfo object
        /// </summary>
        /// <param name="o">A RoleReconciliationDueDateInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RoleReconciliationDueDateInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RoleReconciliationDueDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parReconciliationDueDate = cmd.CreateParameter();
            parReconciliationDueDate.ParameterName = "@ReconciliationDueDate";
            if (!entity.IsReconciliationDueDateNull)
                parReconciliationDueDate.Value = entity.ReconciliationDueDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parReconciliationDueDate.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationDueDate);

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

            System.Data.IDbDataParameter pkparRoleReconciliationDueDateID = cmd.CreateParameter();
            pkparRoleReconciliationDueDateID.ParameterName = "@RoleReconciliationDueDateID";
            pkparRoleReconciliationDueDateID.Value = entity.RoleReconciliationDueDateID;
            cmdParams.Add(pkparRoleReconciliationDueDateID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RoleReconciliationDueDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleReconciliationDueDateID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RoleReconciliationDueDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleReconciliationDueDateID";
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
        public IList<RoleReconciliationDueDateInfo> SelectAllByRoleReconciliationDueDateID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RoleReconciliationDueDateByRoleReconciliationDueDateID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleReconciliationDueDateID";
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
        public IList<RoleReconciliationDueDateInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RoleReconciliationDueDateByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
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
        public IList<RoleReconciliationDueDateInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RoleReconciliationDueDateByReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(RoleReconciliationDueDateInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RoleReconciliationDueDateDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RoleReconciliationDueDateInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RoleReconciliationDueDateDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RoleReconciliationDueDateInfo entity, object id)
        {
            entity.RoleReconciliationDueDateID = Convert.ToInt32(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleReconciliationDueDateInfo> SelectRoleReconciliationDueDateDetailsAssociatedToRoleMstByRoleReconciliationDueDate(RoleMstInfo entity)
        {
            return this.SelectRoleReconciliationDueDateDetailsAssociatedToRoleMstByRoleReconciliationDueDate(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleReconciliationDueDateInfo> SelectRoleReconciliationDueDateDetailsAssociatedToRoleMstByRoleReconciliationDueDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleReconciliationDueDate] INNER JOIN [RoleReconciliationDueDate] ON [RoleReconciliationDueDate].[RoleReconciliationDueDateID] = [RoleReconciliationDueDate].[RoleReconciliationDueDateID] INNER JOIN [RoleMst] ON [RoleReconciliationDueDate].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleReconciliationDueDateInfo> objRoleReconciliationDueDateEntityColl = new List<RoleReconciliationDueDateInfo>(this.Select(cmd));
            return objRoleReconciliationDueDateEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleReconciliationDueDateInfo> SelectRoleReconciliationDueDateDetailsAssociatedToReconciliationPeriodByRoleReconciliationDueDate(ReconciliationPeriodInfo entity)
        {
            return this.SelectRoleReconciliationDueDateDetailsAssociatedToReconciliationPeriodByRoleReconciliationDueDate(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RoleReconciliationDueDateInfo> SelectRoleReconciliationDueDateDetailsAssociatedToReconciliationPeriodByRoleReconciliationDueDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RoleReconciliationDueDate] INNER JOIN [RoleReconciliationDueDate] ON [RoleReconciliationDueDate].[RoleReconciliationDueDateID] = [RoleReconciliationDueDate].[RoleReconciliationDueDateID] INNER JOIN [ReconciliationPeriod] ON [RoleReconciliationDueDate].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<RoleReconciliationDueDateInfo> objRoleReconciliationDueDateEntityColl = new List<RoleReconciliationDueDateInfo>(this.Select(cmd));
            return objRoleReconciliationDueDateEntityColl;
        }

    }
}
