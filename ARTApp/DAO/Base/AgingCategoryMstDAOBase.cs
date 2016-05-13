

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

    public abstract class AgingCategoryMstDAOBase : CustomAbstractDAO<AgingCategoryMstInfo>
    {

        /// <summary>
        /// A static representation of column AgingCategoryID
        /// </summary>
        public static readonly string COLUMN_AGINGCATEGORYID = "AgingCategoryID";
        /// <summary>
        /// A static representation of column AgingCategoryLabelID
        /// </summary>
        public static readonly string COLUMN_AGINGCATEGORYLABELID = "AgingCategoryLabelID";
        /// <summary>
        /// A static representation of column AgingCategoryName
        /// </summary>
        public static readonly string COLUMN_AGINGCATEGORYNAME = "AgingCategoryName";
        /// <summary>
        /// A static representation of column FromDays
        /// </summary>
        public static readonly string COLUMN_FROMDAYS = "FromDays";
        /// <summary>
        /// A static representation of column ToDays
        /// </summary>
        public static readonly string COLUMN_TODAYS = "ToDays";
        /// <summary>
        /// Provides access to the name of the primary key column (AgingCategoryID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AgingCategoryID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "AgingCategoryMst";

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
        public AgingCategoryMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AgingCategoryMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AgingCategoryMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AgingCategoryMstInfo</returns>
        protected override AgingCategoryMstInfo MapObject(System.Data.IDataReader r)
        {

            AgingCategoryMstInfo entity = new AgingCategoryMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("AgingCategoryID");
                if (!r.IsDBNull(ordinal)) entity.AgingCategoryID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AgingCategoryName");
                if (!r.IsDBNull(ordinal)) entity.AgingCategoryName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AgingCategoryLabelID");
                if (!r.IsDBNull(ordinal)) entity.AgingCategoryLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FromDays");
                if (!r.IsDBNull(ordinal)) entity.FromDays = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ToDays");
                if (!r.IsDBNull(ordinal)) entity.ToDays = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in AgingCategoryMstInfo object
        /// </summary>
        /// <param name="o">A AgingCategoryMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AgingCategoryMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AgingCategoryMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAgingCategoryLabelID = cmd.CreateParameter();
            parAgingCategoryLabelID.ParameterName = "@AgingCategoryLabelID";
            if (!entity.IsAgingCategoryLabelIDNull)
                parAgingCategoryLabelID.Value = entity.AgingCategoryLabelID;
            else
                parAgingCategoryLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAgingCategoryLabelID);

            System.Data.IDbDataParameter parAgingCategoryName = cmd.CreateParameter();
            parAgingCategoryName.ParameterName = "@AgingCategoryName";
            if (!entity.IsAgingCategoryNameNull)
                parAgingCategoryName.Value = entity.AgingCategoryName;
            else
                parAgingCategoryName.Value = System.DBNull.Value;
            cmdParams.Add(parAgingCategoryName);

            System.Data.IDbDataParameter parFromDays = cmd.CreateParameter();
            parFromDays.ParameterName = "@FromDays";
            if (!entity.IsFromDaysNull)
                parFromDays.Value = entity.FromDays;
            else
                parFromDays.Value = System.DBNull.Value;
            cmdParams.Add(parFromDays);

            System.Data.IDbDataParameter parToDays = cmd.CreateParameter();
            parToDays.ParameterName = "@ToDays";
            if (!entity.IsToDaysNull)
                parToDays.Value = entity.ToDays;
            else
                parToDays.Value = System.DBNull.Value;
            cmdParams.Add(parToDays);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in AgingCategoryMstInfo object
        /// </summary>
        /// <param name="o">A AgingCategoryMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AgingCategoryMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AgingCategoryMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAgingCategoryLabelID = cmd.CreateParameter();
            parAgingCategoryLabelID.ParameterName = "@AgingCategoryLabelID";
            if (!entity.IsAgingCategoryLabelIDNull)
                parAgingCategoryLabelID.Value = entity.AgingCategoryLabelID;
            else
                parAgingCategoryLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAgingCategoryLabelID);

            System.Data.IDbDataParameter parAgingCategoryName = cmd.CreateParameter();
            parAgingCategoryName.ParameterName = "@AgingCategoryName";
            if (!entity.IsAgingCategoryNameNull)
                parAgingCategoryName.Value = entity.AgingCategoryName;
            else
                parAgingCategoryName.Value = System.DBNull.Value;
            cmdParams.Add(parAgingCategoryName);

            System.Data.IDbDataParameter parFromDays = cmd.CreateParameter();
            parFromDays.ParameterName = "@FromDays";
            if (!entity.IsFromDaysNull)
                parFromDays.Value = entity.FromDays;
            else
                parFromDays.Value = System.DBNull.Value;
            cmdParams.Add(parFromDays);

            System.Data.IDbDataParameter parToDays = cmd.CreateParameter();
            parToDays.ParameterName = "@ToDays";
            if (!entity.IsToDaysNull)
                parToDays.Value = entity.ToDays;
            else
                parToDays.Value = System.DBNull.Value;
            cmdParams.Add(parToDays);

            System.Data.IDbDataParameter pkparAgingCategoryID = cmd.CreateParameter();
            pkparAgingCategoryID.ParameterName = "@AgingCategoryID";
            pkparAgingCategoryID.Value = entity.AgingCategoryID;
            cmdParams.Add(pkparAgingCategoryID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AgingCategoryMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AgingCategoryID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AgingCategoryMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AgingCategoryID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }

        private void MapIdentity(AgingCategoryMstInfo entity, object id)
        {
            entity.AgingCategoryID = Convert.ToInt16(id);
        }

    }
}
