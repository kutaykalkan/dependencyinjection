

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

    public abstract class CompanyWeekDayDAOBase : CustomAbstractDAO<CompanyWeekDayInfo>
    {

        /// <summary>
        /// A static representation of column CompanyRecPeriodSetID
        /// </summary>
        public static readonly string COLUMN_COMPANYRECPERIODSETID = "CompanyRecPeriodSetID";
        /// <summary>
        /// A static representation of column CompanyWeekDayID
        /// </summary>
        public static readonly string COLUMN_COMPANYWEEKDAYID = "CompanyWeekDayID";
        /// <summary>
        /// A static representation of column WeekDayID
        /// </summary>
        public static readonly string COLUMN_WEEKDAYID = "WeekDayID";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyWeekDayID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyWeekDayID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyWeekDay";

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
        public CompanyWeekDayDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyWeekDay", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyWeekDayInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyWeekDayInfo</returns>
        protected override CompanyWeekDayInfo MapObject(System.Data.IDataReader r)
        {

            CompanyWeekDayInfo entity = new CompanyWeekDayInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyWeekDayID");
                if (!r.IsDBNull(ordinal)) entity.CompanyWeekDayID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyRecPeriodSetID");
                if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("WeekDayID");
                if (!r.IsDBNull(ordinal)) entity.WeekDayID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyWeekDayInfo object
        /// </summary>
        /// <param name="o">A CompanyWeekDayInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyWeekDayInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyWeekDay");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
            parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
            if (entity != null)
                parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
            else
                parCompanyRecPeriodSetID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetID);

            System.Data.IDbDataParameter parWeekDayID = cmd.CreateParameter();
            parWeekDayID.ParameterName = "@WeekDayID";
            if (entity != null)
                parWeekDayID.Value = entity.WeekDayID;
            else
                parWeekDayID.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyWeekDayInfo object
        /// </summary>
        /// <param name="o">A CompanyWeekDayInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyWeekDayInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyWeekDay");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
            parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
            if (entity != null)
                parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
            else
                parCompanyRecPeriodSetID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetID);

            System.Data.IDbDataParameter parWeekDayID = cmd.CreateParameter();
            parWeekDayID.ParameterName = "@WeekDayID";
            if (entity != null)
                parWeekDayID.Value = entity.WeekDayID;
            else
                parWeekDayID.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayID);

            System.Data.IDbDataParameter pkparCompanyWeekDayID = cmd.CreateParameter();
            pkparCompanyWeekDayID.ParameterName = "@CompanyWeekDayID";
            pkparCompanyWeekDayID.Value = entity.CompanyWeekDayID;
            cmdParams.Add(pkparCompanyWeekDayID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyWeekDay");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyWeekDayID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyWeekDay");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyWeekDayID";
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
        public IList<CompanyWeekDayInfo> SelectAllByCompanyRecPeriodSetID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyWeekDayByCompanyRecPeriodSetID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyRecPeriodSetID";
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
        public IList<CompanyWeekDayInfo> SelectAllByWeekDayID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyWeekDayByWeekDayID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@WeekDayID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(CompanyWeekDayInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyWeekDayDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanyWeekDayInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyWeekDayDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanyWeekDayInfo entity, object id)
        {
            entity.CompanyWeekDayID = Convert.ToInt32(id);
        }




    }
}
