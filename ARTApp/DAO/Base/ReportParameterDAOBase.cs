

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class ReportParameterDAOBase : CustomAbstractDAO<ReportParameterInfo>
    {

        /// <summary>
        /// A static representation of column IsMandatory
        /// </summary>
        public static readonly string COLUMN_ISMANDATORY = "IsMandatory";
        /// <summary>
        /// A static representation of column ParameterID
        /// </summary>
        public static readonly string COLUMN_PARAMETERID = "ParameterID";
        /// <summary>
        /// A static representation of column ReportID
        /// </summary>
        public static readonly string COLUMN_REPORTID = "ReportID";
        /// <summary>
        /// A static representation of column ReportParameterID
        /// </summary>
        public static readonly string COLUMN_REPORTPARAMETERID = "ReportParameterID";
        /// <summary>
        /// Provides access to the name of the primary key column (ReportParameterID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReportParameterID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReportParameter";

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
        public ReportParameterDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReportParameter", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReportParameterInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReportParameterInfo</returns>
        protected override ReportParameterInfo MapObject(System.Data.IDataReader r)
        {

            ReportParameterInfo entity = new ReportParameterInfo();


            try
            {
                int ordinal = r.GetOrdinal("ReportParameterID");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportID");
                if (!r.IsDBNull(ordinal)) entity.ReportID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ParameterID");
                if (!r.IsDBNull(ordinal)) entity.ParameterID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsMandatory");
                if (!r.IsDBNull(ordinal)) entity.IsMandatory = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            //Added By Harsh
            try
            {
                int ordinal = r.GetOrdinal("ParameterName");
                if (!r.IsDBNull(ordinal)) entity.ParameterName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ParameterURL");
                if (!r.IsDBNull(ordinal)) entity.ParameterUrl = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReportParameterInfo object
        /// </summary>
        /// <param name="o">A ReportParameterInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReportParameterInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parIsMandatory = cmd.CreateParameter();
            parIsMandatory.ParameterName = "@IsMandatory";
            if (!entity.IsIsMandatoryNull)
                parIsMandatory.Value = entity.IsMandatory;
            else
                parIsMandatory.Value = System.DBNull.Value;
            cmdParams.Add(parIsMandatory);

            System.Data.IDbDataParameter parParameterID = cmd.CreateParameter();
            parParameterID.ParameterName = "@ParameterID";
            if (!entity.IsParameterIDNull)
                parParameterID.Value = entity.ParameterID;
            else
                parParameterID.Value = System.DBNull.Value;
            cmdParams.Add(parParameterID);

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (!entity.IsReportIDNull)
                parReportID.Value = entity.ReportID;
            else
                parReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in ReportParameterInfo object
        /// </summary>
        /// <param name="o">A ReportParameterInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReportParameterInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parIsMandatory = cmd.CreateParameter();
            parIsMandatory.ParameterName = "@IsMandatory";
            if (!entity.IsIsMandatoryNull)
                parIsMandatory.Value = entity.IsMandatory;
            else
                parIsMandatory.Value = System.DBNull.Value;
            cmdParams.Add(parIsMandatory);

            System.Data.IDbDataParameter parParameterID = cmd.CreateParameter();
            parParameterID.ParameterName = "@ParameterID";
            if (!entity.IsParameterIDNull)
                parParameterID.Value = entity.ParameterID;
            else
                parParameterID.Value = System.DBNull.Value;
            cmdParams.Add(parParameterID);

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (!entity.IsReportIDNull)
                parReportID.Value = entity.ReportID;
            else
                parReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportID);

            System.Data.IDbDataParameter pkparReportParameterID = cmd.CreateParameter();
            pkparReportParameterID.ParameterName = "@ReportParameterID";
            pkparReportParameterID.Value = entity.ReportParameterID;
            cmdParams.Add(pkparReportParameterID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportParameterID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportParameterID";
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
        public IList<ReportParameterInfo> SelectAllByReportID(ReportParameterParamInfo oReportParameterParamInfo)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_ReportParameterByReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = oReportParameterParamInfo.ReportID;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = oReportParameterParamInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = oReportParameterParamInfo.RecPeriodID;
            cmdParams.Add(parRecPeriodID);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<ReportParameterInfo> SelectAllByParameterID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReportParameterByParameterID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ParameterID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(ReportParameterInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReportParameterDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReportParameterInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReportParameterDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReportParameterInfo entity, object id)
        {
            entity.ReportParameterID = Convert.ToInt32(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportParameterInfo> SelectReportParameterDetailsAssociatedToUserMyReportSavedReportByUserMyReportSavedReportParameter(UserMyReportSavedReportInfo entity)
        {
            return this.SelectReportParameterDetailsAssociatedToUserMyReportSavedReportByUserMyReportSavedReportParameter(entity.UserMyReportSavedReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReportParameterInfo> SelectReportParameterDetailsAssociatedToUserMyReportSavedReportByUserMyReportSavedReportParameter(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReportParameter] INNER JOIN [UserMyReportSavedReportParameter] ON [ReportParameter].[ReportParameterID] = [UserMyReportSavedReportParameter].[ReportParameterID] INNER JOIN [UserMyReportSavedReport] ON [UserMyReportSavedReportParameter].[UserMyReportSavedReportID] = [UserMyReportSavedReport].[UserMyReportSavedReportID]  WHERE  [UserMyReportSavedReport].[UserMyReportSavedReportID] = @UserMyReportSavedReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportSavedReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReportParameterInfo> objReportParameterEntityColl = new List<ReportParameterInfo>(this.Select(cmd));
            return objReportParameterEntityColl;
        }

    }
}
