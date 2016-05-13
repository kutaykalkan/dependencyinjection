

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

    public abstract class CompanyHdrDAOBase : CustomAbstractDAO<CompanyHdrInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column Address1
        /// </summary>
        public static readonly string COLUMN_ADDRESS1 = "Address1";
        /// <summary>
        /// A static representation of column Address2
        /// </summary>
        public static readonly string COLUMN_ADDRESS2 = "Address2";
        /// <summary>
        /// A static representation of column City
        /// </summary>
        public static readonly string COLUMN_CITY = "City";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// A static representation of column CompanyName
        /// </summary>
        public static readonly string COLUMN_COMPANYNAME = "CompanyName";
        /// <summary>
        /// A static representation of column Country
        /// </summary>
        public static readonly string COLUMN_COUNTRY = "Country";
        /// <summary>
        /// A static representation of column DataStorageCapacity
        /// </summary>
        public static readonly string COLUMN_DATASTORAGECAPACITY = "DataStorageCapacity";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column DisplayName
        /// </summary>
        public static readonly string COLUMN_DISPLAYNAME = "DisplayName";
        /// <summary>
        /// A static representation of column DisplayNameLabelID
        /// </summary>
        public static readonly string COLUMN_DISPLAYNAMELABELID = "DisplayNameLabelID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column LogoFileName
        /// </summary>
        public static readonly string COLUMN_LOGOFILENAME = "LogoFileName";
        /// <summary>
        /// A static representation of column LogoPhysicalPath
        /// </summary>
        public static readonly string COLUMN_LOGOPHYSICALPATH = "LogoPhysicalPath";
        /// <summary>
        /// A static representation of column NoOfLicensedUsers
        /// </summary>
        public static readonly string COLUMN_NOOFLICENSEDUSERS = "NoOfLicensedUsers";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column State
        /// </summary>
        public static readonly string COLUMN_STATE = "State";
        /// <summary>
        /// A static representation of column SubscriptionEndDate
        /// </summary>
        public static readonly string COLUMN_SUBSCRIPTIONENDDATE = "SubscriptionEndDate";
        /// <summary>
        /// A static representation of column SubscriptionStartDate
        /// </summary>
        public static readonly string COLUMN_SUBSCRIPTIONSTARTDATE = "SubscriptionStartDate";
        /// <summary>
        /// A static representation of column WebSite
        /// </summary>
        public static readonly string COLUMN_WEBSITE = "WebSite";
        /// <summary>
        /// A static representation of column Zip
        /// </summary>
        public static readonly string COLUMN_ZIP = "Zip";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyHdr";

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
        public CompanyHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyHdrInfo</returns>
        protected override CompanyHdrInfo MapObject(System.Data.IDataReader r)
        {
            CompanyHdrInfo entity = new CompanyHdrInfo();
            entity.Address = new AddressInfo();

            entity.CompanyID = r.GetInt32Value("CompanyID");
            entity.CompanyName = r.GetStringValue("CompanyName");

            entity.Address.Address1 = r.GetStringValue("Address1");
            entity.Address.Address2 = r.GetStringValue("Address2");
            entity.Address.City = r.GetStringValue("City");
            entity.Address.State = r.GetStringValue("State");
            entity.Address.Zip = r.GetStringValue("Zip");
            entity.Address.Country = r.GetStringValue("Country");

            entity.WebSite = r.GetStringValue("WebSite");
            entity.LogoFileName = r.GetStringValue("LogoFileName");
            entity.LogoPhysicalPath = r.GetStringValue("LogoPhysicalPath");
            entity.DisplayName = r.GetStringValue("DisplayName");
            entity.DisplayNameLabelID = r.GetInt32Value("DisplayNameLabelID");
            entity.SubscriptionStartDate = r.GetDateValue("SubscriptionStartDate");
            entity.SubscriptionEndDate = r.GetDateValue("SubscriptionEndDate");
            entity.NoOfLicensedUsers = r.GetInt32Value("NoOfLicensedUsers");
            entity.DataStorageCapacity = r.GetDecimalValue("DataStorageCapacity");
            entity.IsActive = r.GetBooleanValue("IsActive");

            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.NoOfSubscriptionDays = r.GetInt32Value("SubscriptionNoOfDays");
            entity.ActualNoOfUsers = r.GetInt32Value("ActualNoOfUsers");
            entity.CurrentUsage = r.GetDecimalValue("CurrentUsage");
            entity.PackageID = r.GetInt16Value("PackageID");
            entity.IsCustomizedPackage = r.GetBooleanValue("IsCustomizedPackage");
            entity.ShowLogoOnMasterPage = r.GetBooleanValue("ShowLogoOnMasterPage");
            entity.IsFTPEnabled = r.GetBooleanValue("IsFTPEnabled");
            entity.CompanyAlias = r.GetStringValue("CompanyAlias");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyHdrInfo object
        /// </summary>
        /// <param name="o">A CompanyHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAddress1 = cmd.CreateParameter();
            parAddress1.ParameterName = "@Address1";
            if (!string.IsNullOrEmpty(entity.Address.Address1))
                parAddress1.Value = entity.Address.Address1;
            else
                parAddress1.Value = System.DBNull.Value;
            cmdParams.Add(parAddress1);

            System.Data.IDbDataParameter parAddress2 = cmd.CreateParameter();
            parAddress2.ParameterName = "@Address2";
            if (!string.IsNullOrEmpty(entity.Address.Address2))
                parAddress2.Value = entity.Address.Address2;
            else
                parAddress2.Value = System.DBNull.Value;
            cmdParams.Add(parAddress2);

            System.Data.IDbDataParameter parCity = cmd.CreateParameter();
            parCity.ParameterName = "@City";
            if (!string.IsNullOrEmpty(entity.Address.City))
                parCity.Value = entity.Address.City;
            else
                parCity.Value = System.DBNull.Value;
            cmdParams.Add(parCity);

            System.Data.IDbDataParameter parCompanyName = cmd.CreateParameter();
            parCompanyName.ParameterName = "@CompanyName";
            if (!entity.IsCompanyNameNull)
                parCompanyName.Value = entity.CompanyName;
            else
                parCompanyName.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyName);

            System.Data.IDbDataParameter parCountry = cmd.CreateParameter();
            parCountry.ParameterName = "@Country";
            if (!string.IsNullOrEmpty(entity.Address.Country))
                parCountry.Value = entity.Address.Country;
            else
                parCountry.Value = System.DBNull.Value;
            cmdParams.Add(parCountry);

            System.Data.IDbDataParameter parDataStorageCapacity = cmd.CreateParameter();
            parDataStorageCapacity.ParameterName = "@DataStorageCapacity";
            if (!entity.IsDataStorageCapacityNull)
                parDataStorageCapacity.Value = entity.DataStorageCapacity;
            else
                parDataStorageCapacity.Value = System.DBNull.Value;
            cmdParams.Add(parDataStorageCapacity);

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

            System.Data.IDbDataParameter parDisplayName = cmd.CreateParameter();
            parDisplayName.ParameterName = "@DisplayName";
            if (!entity.IsDisplayNameNull)
                parDisplayName.Value = entity.DisplayName;
            else
                parDisplayName.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayName);

            System.Data.IDbDataParameter parDisplayNameLabelID = cmd.CreateParameter();
            parDisplayNameLabelID.ParameterName = "@DisplayNameLabelID";
            if (!entity.IsDisplayNameLabelIDNull)
                parDisplayNameLabelID.Value = entity.DisplayNameLabelID;
            else
                parDisplayNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayNameLabelID);

            //System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            //parHostName.ParameterName = "@HostName";
            //if (!entity.IsHostNameNull)
            //    parHostName.Value = entity.HostName;
            //else
            //    parHostName.Value = System.DBNull.Value;
            //cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parLogoFileName = cmd.CreateParameter();
            parLogoFileName.ParameterName = "@LogoFileName";
            if (!entity.IsLogoFileNameNull)
                parLogoFileName.Value = entity.LogoFileName;
            else
                parLogoFileName.Value = System.DBNull.Value;
            cmdParams.Add(parLogoFileName);

            System.Data.IDbDataParameter parLogoPhysicalPath = cmd.CreateParameter();
            parLogoPhysicalPath.ParameterName = "@LogoPhysicalPath";
            if (!entity.IsLogoPhysicalPathNull)
                parLogoPhysicalPath.Value = entity.LogoPhysicalPath;
            else
                parLogoPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parLogoPhysicalPath);

            System.Data.IDbDataParameter parNoOfLicensedUsers = cmd.CreateParameter();
            parNoOfLicensedUsers.ParameterName = "@NoOfLicensedUsers";
            if (!entity.IsNoOfLicensedUsersNull)
                parNoOfLicensedUsers.Value = entity.NoOfLicensedUsers;
            else
                parNoOfLicensedUsers.Value = System.DBNull.Value;
            cmdParams.Add(parNoOfLicensedUsers);


            System.Data.IDbDataParameter parNoOfSubscriptionDays = cmd.CreateParameter();
            parNoOfSubscriptionDays.ParameterName = "@NoOfSubscriptionDays";
            if (!entity.IsNoOfSubscriptionDaysNull)
                parNoOfSubscriptionDays.Value = entity.NoOfSubscriptionDays;
            else
                parNoOfSubscriptionDays.Value = System.DBNull.Value;
            cmdParams.Add(parNoOfSubscriptionDays);


            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parState = cmd.CreateParameter();
            parState.ParameterName = "@State";
            if (!string.IsNullOrEmpty(entity.Address.State))
                parState.Value = entity.Address.State;
            else
                parState.Value = System.DBNull.Value;
            cmdParams.Add(parState);

            System.Data.IDbDataParameter parSubscriptionEndDate = cmd.CreateParameter();
            parSubscriptionEndDate.ParameterName = "@SubscriptionEndDate";
            if (!entity.IsSubscriptionEndDateNull)
                parSubscriptionEndDate.Value = entity.SubscriptionEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parSubscriptionEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parSubscriptionEndDate);

            System.Data.IDbDataParameter parSubscriptionStartDate = cmd.CreateParameter();
            parSubscriptionStartDate.ParameterName = "@SubscriptionStartDate";
            if (!entity.IsSubscriptionStartDateNull)
                parSubscriptionStartDate.Value = entity.SubscriptionStartDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parSubscriptionStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parSubscriptionStartDate);

            System.Data.IDbDataParameter parWebSite = cmd.CreateParameter();
            parWebSite.ParameterName = "@WebSite";
            if (!entity.IsWebSiteNull)
                parWebSite.Value = entity.WebSite;
            else
                parWebSite.Value = System.DBNull.Value;
            cmdParams.Add(parWebSite);

            System.Data.IDbDataParameter parZip = cmd.CreateParameter();
            parZip.ParameterName = "@Zip";
            if (!string.IsNullOrEmpty(entity.Address.Zip))
                parZip.Value = entity.Address.Zip;
            else
                parZip.Value = System.DBNull.Value;
            cmdParams.Add(parZip);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyHdrInfo object
        /// </summary>
        /// <param name="o">A CompanyHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAddress1 = cmd.CreateParameter();
            parAddress1.ParameterName = "@Address1";
            if (!string.IsNullOrEmpty(entity.Address.Address1))
                parAddress1.Value = entity.Address.Address1;
            else
                parAddress1.Value = System.DBNull.Value;
            cmdParams.Add(parAddress1);

            System.Data.IDbDataParameter parAddress2 = cmd.CreateParameter();
            parAddress2.ParameterName = "@Address2";
            if (!string.IsNullOrEmpty(entity.Address.Address2))
                parAddress2.Value = entity.Address.Address2;
            else
                parAddress2.Value = System.DBNull.Value;
            cmdParams.Add(parAddress2);

            System.Data.IDbDataParameter parCity = cmd.CreateParameter();
            parCity.ParameterName = "@City";
            if (!string.IsNullOrEmpty(entity.Address.City))
                parCity.Value = entity.Address.City;
            else
                parCity.Value = System.DBNull.Value;
            cmdParams.Add(parCity);

            System.Data.IDbDataParameter parCompanyName = cmd.CreateParameter();
            parCompanyName.ParameterName = "@CompanyName";
            if (!entity.IsCompanyNameNull)
                parCompanyName.Value = entity.CompanyName;
            else
                parCompanyName.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyName);

            System.Data.IDbDataParameter parCountry = cmd.CreateParameter();
            parCountry.ParameterName = "@Country";
            if (!string.IsNullOrEmpty(entity.Address.Country))
                parCountry.Value = entity.Address.Country;
            else
                parCountry.Value = System.DBNull.Value;
            cmdParams.Add(parCountry);

            System.Data.IDbDataParameter parDataStorageCapacity = cmd.CreateParameter();
            parDataStorageCapacity.ParameterName = "@DataStorageCapacity";
            if (!entity.IsDataStorageCapacityNull)
                parDataStorageCapacity.Value = entity.DataStorageCapacity;
            else
                parDataStorageCapacity.Value = System.DBNull.Value;
            cmdParams.Add(parDataStorageCapacity);

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

            System.Data.IDbDataParameter parDisplayName = cmd.CreateParameter();
            parDisplayName.ParameterName = "@DisplayName";
            if (!entity.IsDisplayNameNull)
                parDisplayName.Value = entity.DisplayName;
            else
                parDisplayName.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayName);

            System.Data.IDbDataParameter parDisplayNameLabelID = cmd.CreateParameter();
            parDisplayNameLabelID.ParameterName = "@DisplayNameLabelID";
            if (!entity.IsDisplayNameLabelIDNull)
                parDisplayNameLabelID.Value = entity.DisplayNameLabelID;
            else
                parDisplayNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayNameLabelID);

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

            System.Data.IDbDataParameter parLogoFileName = cmd.CreateParameter();
            parLogoFileName.ParameterName = "@LogoFileName";
            if (!entity.IsLogoFileNameNull)
                parLogoFileName.Value = entity.LogoFileName;
            else
                parLogoFileName.Value = System.DBNull.Value;
            cmdParams.Add(parLogoFileName);

            System.Data.IDbDataParameter parLogoPhysicalPath = cmd.CreateParameter();
            parLogoPhysicalPath.ParameterName = "@LogoPhysicalPath";
            if (!entity.IsLogoPhysicalPathNull)
                parLogoPhysicalPath.Value = entity.LogoPhysicalPath;
            else
                parLogoPhysicalPath.Value = System.DBNull.Value;
            cmdParams.Add(parLogoPhysicalPath);

            System.Data.IDbDataParameter parNoOfLicensedUsers = cmd.CreateParameter();
            parNoOfLicensedUsers.ParameterName = "@NoOfLicensedUsers";
            if (!entity.IsNoOfLicensedUsersNull)
                parNoOfLicensedUsers.Value = entity.NoOfLicensedUsers;
            else
                parNoOfLicensedUsers.Value = System.DBNull.Value;
            cmdParams.Add(parNoOfLicensedUsers);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parState = cmd.CreateParameter();
            parState.ParameterName = "@State";
            if (!string.IsNullOrEmpty(entity.Address.State))
                parState.Value = entity.Address.State;
            else
                parState.Value = System.DBNull.Value;
            cmdParams.Add(parState);

            System.Data.IDbDataParameter parSubscriptionEndDate = cmd.CreateParameter();
            parSubscriptionEndDate.ParameterName = "@SubscriptionEndDate";
            if (!entity.IsSubscriptionEndDateNull)
                parSubscriptionEndDate.Value = entity.SubscriptionEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parSubscriptionEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parSubscriptionEndDate);

            System.Data.IDbDataParameter parSubscriptionStartDate = cmd.CreateParameter();
            parSubscriptionStartDate.ParameterName = "@SubscriptionStartDate";
            if (!entity.IsSubscriptionStartDateNull)
                parSubscriptionStartDate.Value = entity.SubscriptionStartDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parSubscriptionStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parSubscriptionStartDate);

            System.Data.IDbDataParameter parWebSite = cmd.CreateParameter();
            parWebSite.ParameterName = "@WebSite";
            if (!entity.IsWebSiteNull)
                parWebSite.Value = entity.WebSite;
            else
                parWebSite.Value = System.DBNull.Value;
            cmdParams.Add(parWebSite);

            System.Data.IDbDataParameter parZip = cmd.CreateParameter();
            parZip.ParameterName = "@Zip";
            if (!string.IsNullOrEmpty(entity.Address.Zip))
                parZip.Value = entity.Address.Zip;
            else
                parZip.Value = System.DBNull.Value;
            cmdParams.Add(parZip);

            System.Data.IDbDataParameter pkparCompanyID = cmd.CreateParameter();
            pkparCompanyID.ParameterName = "@CompanyID";
            pkparCompanyID.Value = entity.CompanyID;
            cmdParams.Add(pkparCompanyID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(CompanyHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanyHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanyHdrInfo entity, object id)
        {
            entity.CompanyID = Convert.ToInt32(id);
        }
























        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(CertificationTypeMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(entity.CertificationTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CertificationSignOffStatus] ON [CompanyHdr].[CompanyID] = [CertificationSignOffStatus].[CompanyID] INNER JOIN [CertificationTypeMst] ON [CertificationSignOffStatus].[CertificationTypeID] = [CertificationTypeMst].[CertificationTypeID]  WHERE  [CertificationTypeMst].[CertificationTypeID] = @CertificationTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(ReconciliationPeriodInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CertificationSignOffStatus] ON [CompanyHdr].[CompanyID] = [CertificationSignOffStatus].[CompanyID] INNER JOIN [ReconciliationPeriod] ON [CertificationSignOffStatus].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToUserHdrByCertificationSignOffStatus(UserHdrInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToUserHdrByCertificationSignOffStatus(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToUserHdrByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CertificationSignOffStatus] ON [CompanyHdr].[CompanyID] = [CertificationSignOffStatus].[CompanyID] INNER JOIN [UserHdr] ON [CertificationSignOffStatus].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToCertificationTypeMstByCertificationVerbiage(CertificationTypeMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToCertificationTypeMstByCertificationVerbiage(entity.CertificationTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToCertificationTypeMstByCertificationVerbiage(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CertificationVerbiage] ON [CompanyHdr].[CompanyID] = [CertificationVerbiage].[CompanyID] INNER JOIN [CertificationTypeMst] ON [CertificationVerbiage].[CertificationTypeID] = [CertificationTypeMst].[CertificationTypeID]  WHERE  [CertificationTypeMst].[CertificationTypeID] = @CertificationTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRoleMstByCertificationVerbiage(RoleMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToRoleMstByCertificationVerbiage(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRoleMstByCertificationVerbiage(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CertificationVerbiage] ON [CompanyHdr].[CompanyID] = [CertificationVerbiage].[CompanyID] INNER JOIN [RoleMst] ON [CertificationVerbiage].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToAlertMstByCompanyAlert(AlertMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToAlertMstByCompanyAlert(entity.AlertID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToAlertMstByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CompanyAlert] ON [CompanyHdr].[CompanyID] = [CompanyAlert].[CompanyID] INNER JOIN [AlertMst] ON [CompanyAlert].[AlertID] = [AlertMst].[AlertID]  WHERE  [AlertMst].[AlertID] = @AlertID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToDateBasisMstByCompanyAlert(DateBasisMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToDateBasisMstByCompanyAlert(entity.DateBasisID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToDateBasisMstByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CompanyAlert] ON [CompanyHdr].[CompanyID] = [CompanyAlert].[CompanyID] INNER JOIN [DateBasisMst] ON [CompanyAlert].[DateBasisID] = [DateBasisMst].[DateBasisID]  WHERE  [DateBasisMst].[DateBasisID] = @DateBasisID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DateBasisID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToAlertScheduleTypeMstByCompanyAlert(AlertScheduleTypeMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToAlertScheduleTypeMstByCompanyAlert(entity.AlertScheduleTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToAlertScheduleTypeMstByCompanyAlert(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CompanyAlert] ON [CompanyHdr].[CompanyID] = [CompanyAlert].[CompanyID] INNER JOIN [AlertScheduleTypeMst] ON [CompanyAlert].[AlertScheduleTypeID] = [AlertScheduleTypeMst].[AlertScheduleTypeID]  WHERE  [AlertScheduleTypeMst].[AlertScheduleTypeID] = @AlertScheduleTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AlertScheduleTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToCapabilityMstByCompanyCapability(CapabilityMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToCapabilityMstByCompanyCapability(entity.CapabilityID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToCapabilityMstByCompanyCapability(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CompanyCapability] ON [CompanyHdr].[CompanyID] = [CompanyCapability].[CompanyID] INNER JOIN [CapabilityMst] ON [CompanyCapability].[CapabilityID] = [CapabilityMst].[CapabilityID]  WHERE  [CapabilityMst].[CapabilityID] = @CapabilityID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByCompanySetting(ReconciliationPeriodInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByCompanySetting(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByCompanySetting(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CompanySetting] ON [CompanyHdr].[CompanyID] = [CompanySetting].[CompanyID] INNER JOIN [ReconciliationPeriod] ON [CompanySetting].[OpenReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToMaterialityTypeMstByCompanySetting(MaterialityTypeMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToMaterialityTypeMstByCompanySetting(entity.MaterialityTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToMaterialityTypeMstByCompanySetting(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CompanySetting] ON [CompanyHdr].[CompanyID] = [CompanySetting].[CompanyID] INNER JOIN [MaterialityTypeMst] ON [CompanySetting].[MaterialityTypeID] = [MaterialityTypeMst].[MaterialityTypeID]  WHERE  [MaterialityTypeMst].[MaterialityTypeID] = @MaterialityTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MaterialityTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToSystemReconciliationRuleMstByCompanySystemReconciliationRule(SystemReconciliationRuleMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToSystemReconciliationRuleMstByCompanySystemReconciliationRule(entity.SystemReconciliationRuleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToSystemReconciliationRuleMstByCompanySystemReconciliationRule(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [CompanySystemReconciliationRule] ON [CompanyHdr].[CompanyID] = [CompanySystemReconciliationRule].[CompanyID] INNER JOIN [SystemReconciliationRuleMst] ON [CompanySystemReconciliationRule].[SystemReconciliationRuleID] = [SystemReconciliationRuleMst].[SystemReconciliationRuleID]  WHERE  [SystemReconciliationRuleMst].[SystemReconciliationRuleID] = @SystemReconciliationRuleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SystemReconciliationRuleID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByDataImportHdr(ReconciliationPeriodInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByDataImportHdr(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [DataImportHdr] ON [CompanyHdr].[CompanyID] = [DataImportHdr].[CompanyID] INNER JOIN [ReconciliationPeriod] ON [DataImportHdr].[ReonciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToDataImportTypeMstByDataImportHdr(DataImportTypeMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToDataImportTypeMstByDataImportHdr(entity.DataImportTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToDataImportTypeMstByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [DataImportHdr] ON [CompanyHdr].[CompanyID] = [DataImportHdr].[CompanyID] INNER JOIN [DataImportTypeMst] ON [DataImportHdr].[DataImportTypeID] = [DataImportTypeMst].[DataImportTypeID]  WHERE  [DataImportTypeMst].[DataImportTypeID] = @DataImportTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToDataImportStatusMstByDataImportHdr(DataImportStatusMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToDataImportStatusMstByDataImportHdr(entity.DataImportStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToDataImportStatusMstByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [DataImportHdr] ON [CompanyHdr].[CompanyID] = [DataImportHdr].[CompanyID] INNER JOIN [DataImportStatusMst] ON [DataImportHdr].[DataImportStatusID] = [DataImportStatusMst].[DataImportStatusID]  WHERE  [DataImportStatusMst].[DataImportStatusID] = @DataImportStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyStructureHdrByGeographyObjectHdr(GeographyStructureHdrInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToGeographyStructureHdrByGeographyObjectHdr(entity.GeographyStructureID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyStructureHdrByGeographyObjectHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [GeographyObjectHdr] ON [CompanyHdr].[CompanyID] = [GeographyObjectHdr].[CompanyID] INNER JOIN [GeographyStructureHdr] ON [GeographyObjectHdr].[GeographyStructureID] = [GeographyStructureHdr].[GeographyStructureID]  WHERE  [GeographyStructureHdr].[GeographyStructureID] = @GeographyStructureID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyStructureID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyObjectHdrByGeographyObjectHdr(GeographyObjectHdrInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToGeographyObjectHdrByGeographyObjectHdr(entity.GeographyObjectID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyObjectHdrByGeographyObjectHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [GeographyObjectHdr] ON [CompanyHdr].[CompanyID] = [GeographyObjectHdr].[CompanyID] INNER JOIN [GeographyObjectHdr] ON [GeographyObjectHdr].[ParentGeographyObjectID] = [GeographyObjectHdr].[GeographyObjectID]  WHERE  [GeographyObjectHdr].[GeographyObjectID] = @GeographyObjectID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyClassMstByGeographyStructureHdr(GeographyClassMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToGeographyClassMstByGeographyStructureHdr(entity.GeographyClassID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyClassMstByGeographyStructureHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [GeographyStructureHdr] ON [CompanyHdr].[CompanyID] = [GeographyStructureHdr].[CompanyID] INNER JOIN [GeographyClassMst] ON [GeographyStructureHdr].[GeographyClassID] = [GeographyClassMst].[GeographyClassID]  WHERE  [GeographyClassMst].[GeographyClassID] = @GeographyClassID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyClassID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyStructureHdrByGeographyStructureHdr(GeographyStructureHdrInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToGeographyStructureHdrByGeographyStructureHdr(entity.GeographyStructureID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToGeographyStructureHdrByGeographyStructureHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [GeographyStructureHdr] ON [CompanyHdr].[CompanyID] = [GeographyStructureHdr].[CompanyID] INNER JOIN [GeographyStructureHdr] ON [GeographyStructureHdr].[ParentGeographyStructureID] = [GeographyStructureHdr].[GeographyStructureID]  WHERE  [GeographyStructureHdr].[GeographyStructureID] = @GeographyStructureID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyStructureID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToMaterialityTypeMstByReconciliationPeriod(MaterialityTypeMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToMaterialityTypeMstByReconciliationPeriod(entity.MaterialityTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToMaterialityTypeMstByReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [ReconciliationPeriod] ON [CompanyHdr].[CompanyID] = [ReconciliationPeriod].[CompanyID] INNER JOIN [MaterialityTypeMst] ON [ReconciliationPeriod].[MaterialityTypeID] = [MaterialityTypeMst].[MaterialityTypeID]  WHERE  [MaterialityTypeMst].[MaterialityTypeID] = @MaterialityTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MaterialityTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationFrequency(RiskRatingMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationFrequency(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationFrequency(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [RiskRatingReconciliationFrequency] ON [CompanyHdr].[CompanyID] = [RiskRatingReconciliationFrequency].[CompanyID] INNER JOIN [RiskRatingMst] ON [RiskRatingReconciliationFrequency].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationFrequencyMstByRiskRatingReconciliationFrequency(ReconciliationFrequencyMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToReconciliationFrequencyMstByRiskRatingReconciliationFrequency(entity.ReconciliationFrequencyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationFrequencyMstByRiskRatingReconciliationFrequency(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [RiskRatingReconciliationFrequency] ON [CompanyHdr].[CompanyID] = [RiskRatingReconciliationFrequency].[CompanyID] INNER JOIN [ReconciliationFrequencyMst] ON [RiskRatingReconciliationFrequency].[ReconciliationFrequencyID] = [ReconciliationFrequencyMst].[ReconciliationFrequencyID]  WHERE  [ReconciliationFrequencyMst].[ReconciliationFrequencyID] = @ReconciliationFrequencyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationPeriod(RiskRatingMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationPeriod(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [RiskRatingReconciliationPeriod] ON [CompanyHdr].[CompanyID] = [RiskRatingReconciliationPeriod].[CompanyID] INNER JOIN [RiskRatingMst] ON [RiskRatingReconciliationPeriod].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByRiskRatingReconciliationPeriod(ReconciliationPeriodInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByRiskRatingReconciliationPeriod(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReconciliationPeriodByRiskRatingReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [RiskRatingReconciliationPeriod] ON [CompanyHdr].[CompanyID] = [RiskRatingReconciliationPeriod].[CompanyID] INNER JOIN [ReconciliationPeriod] ON [RiskRatingReconciliationPeriod].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRoleMstByRoleMandatoryReport(RoleMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToRoleMstByRoleMandatoryReport(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToRoleMstByRoleMandatoryReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [RoleMandatoryReport] ON [CompanyHdr].[CompanyID] = [RoleMandatoryReport].[CompanyID] INNER JOIN [RoleMst] ON [RoleMandatoryReport].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReportMstByRoleMandatoryReport(ReportMstInfo entity)
        {
            return this.SelectCompanyHdrDetailsAssociatedToReportMstByRoleMandatoryReport(entity.ReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<CompanyHdrInfo> SelectCompanyHdrDetailsAssociatedToReportMstByRoleMandatoryReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [CompanyHdr] INNER JOIN [RoleMandatoryReport] ON [CompanyHdr].[CompanyID] = [RoleMandatoryReport].[CompanyID] INNER JOIN [ReportMst] ON [RoleMandatoryReport].[ReportID] = [ReportMst].[ReportID]  WHERE  [ReportMst].[ReportID] = @ReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<CompanyHdrInfo> objCompanyHdrEntityColl = new List<CompanyHdrInfo>(this.Select(cmd));
            return objCompanyHdrEntityColl;
        }

    }
}
