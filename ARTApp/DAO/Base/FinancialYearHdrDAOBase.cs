

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

    public abstract class FinancialYearHdrDAOBase : CustomAbstractDAO<FinancialYearHdrInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
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
        /// A static representation of column EndDate
        /// </summary>
        public static readonly string COLUMN_ENDDATE = "EndDate";
        /// <summary>
        /// A static representation of column FinancialYear
        /// </summary>
        public static readonly string COLUMN_FINANCIALYEAR = "FinancialYear";
        /// <summary>
        /// A static representation of column FinancialYearID
        /// </summary>
        public static readonly string COLUMN_FINANCIALYEARID = "FinancialYearID";
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
        /// A static representation of column StartDate
        /// </summary>
        public static readonly string COLUMN_STARTDATE = "StartDate";
        /// <summary>
        /// Provides access to the name of the primary key column (FinancialYearID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "FinancialYearID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "FinancialYearHdr";

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
        public FinancialYearHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "FinancialYearHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a FinancialYearHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>FinancialYearHdrInfo</returns>
        protected override FinancialYearHdrInfo MapObject(System.Data.IDataReader r)
        {

            FinancialYearHdrInfo entity = new FinancialYearHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("FinancialYearID");
                if (!r.IsDBNull(ordinal)) entity.FinancialYearID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("FinancialYear");
                if (!r.IsDBNull(ordinal)) entity.FinancialYear = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("StartDate");
                if (!r.IsDBNull(ordinal)) entity.StartDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("EndDate");
                if (!r.IsDBNull(ordinal)) entity.EndDate = ((System.DateTime)(r.GetValue(ordinal)));
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
        /// in FinancialYearHdrInfo object
        /// </summary>
        /// <param name="o">A FinancialYearHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(FinancialYearHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_FinancialYearHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

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

            System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
            parEndDate.ParameterName = "@EndDate";
            if (!entity.IsEndDateNull)
                parEndDate.Value = entity.EndDate;
            else
                parEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parEndDate);

            System.Data.IDbDataParameter parFinancialYear = cmd.CreateParameter();
            parFinancialYear.ParameterName = "@FinancialYear";
            if (!entity.IsFinancialYearNull)
                parFinancialYear.Value = entity.FinancialYear;
            else
                parFinancialYear.Value = System.DBNull.Value;
            cmdParams.Add(parFinancialYear);

            System.Data.IDbDataParameter parFinancialYearID = cmd.CreateParameter();
            parFinancialYearID.ParameterName = "@FinancialYearID";
            if (!entity.IsFinancialYearIDNull)
                parFinancialYearID.Value = entity.FinancialYearID;
            else
                parFinancialYearID.Value = System.DBNull.Value;
            cmdParams.Add(parFinancialYearID);

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

            System.Data.IDbDataParameter parStartDate = cmd.CreateParameter();
            parStartDate.ParameterName = "@StartDate";
            if (!entity.IsStartDateNull)
                parStartDate.Value = entity.StartDate;
            else
                parStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parStartDate);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in FinancialYearHdrInfo object
        /// </summary>
        /// <param name="o">A FinancialYearHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(FinancialYearHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_FinancialYearHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

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

            System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
            parEndDate.ParameterName = "@EndDate";
            if (!entity.IsEndDateNull)
                parEndDate.Value = entity.EndDate;
            else
                parEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parEndDate);

            System.Data.IDbDataParameter parFinancialYear = cmd.CreateParameter();
            parFinancialYear.ParameterName = "@FinancialYear";
            if (!entity.IsFinancialYearNull)
                parFinancialYear.Value = entity.FinancialYear;
            else
                parFinancialYear.Value = System.DBNull.Value;
            cmdParams.Add(parFinancialYear);

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

            System.Data.IDbDataParameter parStartDate = cmd.CreateParameter();
            parStartDate.ParameterName = "@StartDate";
            if (!entity.IsStartDateNull)
                parStartDate.Value = entity.StartDate;
            else
                parStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parStartDate);

            System.Data.IDbDataParameter pkparFinancialYearID = cmd.CreateParameter();
            pkparFinancialYearID.ParameterName = "@FinancialYearID";
            pkparFinancialYearID.Value = entity.FinancialYearID;
            cmdParams.Add(pkparFinancialYearID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_FinancialYearHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FinancialYearID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_FinancialYearHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FinancialYearID";
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
        public IList<FinancialYearHdrInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_FinancialYearHdrByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }















    }
}
