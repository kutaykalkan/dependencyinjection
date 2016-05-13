using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model.CompanyDatabase;
using System.Text;

namespace SkyStem.ART.App.DAO
{
    public class CompanyHdrDAO : CompanyHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }


        public bool CreateNewCompany(CompanyHdrInfo oCompanyHdrInfo, ContactInfo oCompanyContact, int languageID)
        {
            CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(this.CurrentAppUserInfo);
            int companyID = oCompanyHdrDAO.SaveNewCompanyHdrInfo(oCompanyHdrInfo, languageID);
            oCompanyHdrInfo.CompanyID = companyID;
            if (oCompanyContact != null)
            {
                oCompanyContact.CompanyID = oCompanyHdrInfo.CompanyID;
                ContactDAO oContactDAO = new ContactDAO(this.CurrentAppUserInfo);
                oContactDAO.Save(oCompanyContact);
            }
            return true;
        }

        public int SaveNewCompanyHdrInfo(CompanyHdrInfo objCompanyHdrInfo, int languageID)
        {
            int newCompanyID = -1;
            System.Data.IDbCommand cmd = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cmd = GetNewCompanyInsertCommand(objCompanyHdrInfo, languageID);
                cnn.Open();
                cmd.Connection = cnn;
                newCompanyID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            return newCompanyID;
        }

        private System.Data.IDbCommand GetNewCompanyInsertCommand(CompanyHdrInfo entity, int languageID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CompanyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity.CompanyID.HasValue)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

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
            if (!entity.IsDateRevisedNull && entity.DateRevised.HasValue)
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

            System.Data.IDbDataParameter parlanguageID = cmd.CreateParameter();
            parlanguageID.ParameterName = "@languageID";
            parlanguageID.Value = languageID;
            cmdParams.Add(parlanguageID);

            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity.PackageID != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parShowLogoOnMasterPage = cmd.CreateParameter();
            parShowLogoOnMasterPage.ParameterName = "@ShowLogoOnMasterPage";
            if (entity.ShowLogoOnMasterPage != null)
                parShowLogoOnMasterPage.Value = entity.ShowLogoOnMasterPage;
            else
                parShowLogoOnMasterPage.Value = System.DBNull.Value;
            cmdParams.Add(parShowLogoOnMasterPage);

            System.Data.IDbDataParameter parIsSeparateDatabase = cmd.CreateParameter();
            parIsSeparateDatabase.ParameterName = "@IsSeparateDatabase";
            if (entity.IsSeparateDatabase.HasValue)
                parIsSeparateDatabase.Value = entity.IsSeparateDatabase;
            else
                parIsSeparateDatabase.Value = System.DBNull.Value;
            cmdParams.Add(parIsSeparateDatabase);

            System.Data.IDbDataParameter parIsFTPEnabled = cmd.CreateParameter();
            parIsFTPEnabled.ParameterName = "@IsFTPEnabled";
            if (entity.IsFTPEnabled.HasValue)
                parIsFTPEnabled.Value = entity.IsFTPEnabled;
            else
                parIsFTPEnabled.Value = System.DBNull.Value;
            cmdParams.Add(parIsFTPEnabled);

            System.Data.IDbDataParameter parCompanyAlias = cmd.CreateParameter();
            parCompanyAlias.ParameterName = "@CompanyAlias";
            if (!string.IsNullOrEmpty(entity.CompanyAlias))
                parCompanyAlias.Value = entity.CompanyAlias;
            else
                parCompanyAlias.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyAlias);
            return cmd;

        }

        public int? CreateCompanyInCore(CompanyHdrInfo oCompanyHdrInfo, ContactInfo oCompanyContact, int languageID)
        {
            CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(this.CurrentAppUserInfo);
            int? companyID = null;
            System.Data.IDbCommand cmd = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cmd = GetCreateCompanyInCoreCommand(oCompanyHdrInfo, languageID);
                cnn.Open();
                cmd.Connection = cnn;
                companyID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            return companyID;
        }

        private System.Data.IDbCommand GetCreateCompanyInCoreCommand(CompanyHdrInfo entity, int languageID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CoreCompanyHdr");
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

            System.Data.IDbDataParameter parlanguageID = cmd.CreateParameter();
            parlanguageID.ParameterName = "@languageID";
            parlanguageID.Value = languageID;
            cmdParams.Add(parlanguageID);

            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity.PackageID != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parShowLogoOnMasterPage = cmd.CreateParameter();
            parShowLogoOnMasterPage.ParameterName = "@ShowLogoOnMasterPage";
            if (entity.ShowLogoOnMasterPage != null)
                parShowLogoOnMasterPage.Value = entity.ShowLogoOnMasterPage;
            else
                parShowLogoOnMasterPage.Value = System.DBNull.Value;
            cmdParams.Add(parShowLogoOnMasterPage);

            System.Data.IDbDataParameter parIsSeparateDatabase = cmd.CreateParameter();
            parIsSeparateDatabase.ParameterName = "@IsSeparateDatabase";
            if (entity.IsSeparateDatabase.HasValue)
                parIsSeparateDatabase.Value = entity.IsSeparateDatabase;
            else
                parIsSeparateDatabase.Value = System.DBNull.Value;
            cmdParams.Add(parIsSeparateDatabase);

            System.Data.IDbDataParameter parIsFTPEnabled = cmd.CreateParameter();
            parIsFTPEnabled.ParameterName = "@IsFTPEnabled";
            if (entity.IsFTPEnabled.HasValue)
                parIsFTPEnabled.Value = entity.IsFTPEnabled;
            else
                parIsFTPEnabled.Value = System.DBNull.Value;
            cmdParams.Add(parIsFTPEnabled);

            System.Data.IDbDataParameter parCompanyAlias = cmd.CreateParameter();
            parCompanyAlias.ParameterName = "@CompanyAlias";
            if (!string.IsNullOrEmpty(entity.CompanyAlias))
                parCompanyAlias.Value = entity.CompanyAlias;
            else
                parCompanyAlias.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyAlias);
            return cmd;

        }

        public bool UpdateCompany(CompanyHdrInfo objCompanyHdrInfo, ContactInfo objCompanyContact, int languageID)
        {
            CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(this.CurrentAppUserInfo);
            oCompanyHdrDAO.UpdateCompanyHdrInfo(objCompanyHdrInfo, languageID);
            ContactDAO oContactDAO = new ContactDAO(this.CurrentAppUserInfo);
            oContactDAO.UpdateContactInfo(objCompanyContact);
            return true;
        }

        public void UpdateCompanyHdrInfo(CompanyHdrInfo objCompanyHdrInfo, int languageID)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = GetUpdateCommand(objCompanyHdrInfo, languageID))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private System.Data.IDbCommand GetUpdateCommand(CompanyHdrInfo entity, int languageID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_CompanyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

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
            System.Data.IDbDataParameter parLanguageID = cmd.CreateParameter();
            parLanguageID.ParameterName = "@lLanguageID";
            parLanguageID.Value = languageID;
            cmdParams.Add(parLanguageID);

            System.Data.IDbDataParameter parNoOfSubscriptionDays = cmd.CreateParameter();
            parNoOfSubscriptionDays.ParameterName = "@NoOfSubscriptionDays";
            parNoOfSubscriptionDays.Value = entity.NoOfSubscriptionDays;
            cmdParams.Add(parNoOfSubscriptionDays);

            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity.PackageID != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parShowLogoOnMasterPage = cmd.CreateParameter();
            parShowLogoOnMasterPage.ParameterName = "@ShowLogoOnMasterPage";
            if (entity.ShowLogoOnMasterPage != null)
                parShowLogoOnMasterPage.Value = entity.ShowLogoOnMasterPage;
            else
                parShowLogoOnMasterPage.Value = System.DBNull.Value;
            cmdParams.Add(parShowLogoOnMasterPage);


            System.Data.IDbDataParameter parIsFTPEnabled = cmd.CreateParameter();
            parIsFTPEnabled.ParameterName = "@IsFTPEnabled";
            if (entity.IsFTPEnabled.HasValue)
                parIsFTPEnabled.Value = entity.IsFTPEnabled;
            else
                parIsFTPEnabled.Value = System.DBNull.Value;
            cmdParams.Add(parIsFTPEnabled);

            System.Data.IDbDataParameter parCompanyAlias = cmd.CreateParameter();
            parCompanyAlias.ParameterName = "@CompanyAlias";
            if (!string.IsNullOrEmpty(entity.CompanyAlias))
                parCompanyAlias.Value = entity.CompanyAlias;
            else
                parCompanyAlias.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyAlias);
            return cmd;

        }

        public bool UpdateCompanyInCore(CompanyHdrInfo objCompanyHdrInfo, int languageID)
        {
            CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(this.CurrentAppUserInfo);
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = GetUpdateCompanyInCoreCommand(objCompanyHdrInfo, languageID))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        private System.Data.IDbCommand GetUpdateCompanyInCoreCommand(CompanyHdrInfo entity, int languageID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_CoreCompanyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

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
            System.Data.IDbDataParameter parLanguageID = cmd.CreateParameter();
            parLanguageID.ParameterName = "@lLanguageID";
            parLanguageID.Value = languageID;
            cmdParams.Add(parLanguageID);

            System.Data.IDbDataParameter parNoOfSubscriptionDays = cmd.CreateParameter();
            parNoOfSubscriptionDays.ParameterName = "@NoOfSubscriptionDays";
            parNoOfSubscriptionDays.Value = entity.NoOfSubscriptionDays;
            cmdParams.Add(parNoOfSubscriptionDays);

            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity.PackageID != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parShowLogoOnMasterPage = cmd.CreateParameter();
            parShowLogoOnMasterPage.ParameterName = "@ShowLogoOnMasterPage";
            if (entity.ShowLogoOnMasterPage != null)
                parShowLogoOnMasterPage.Value = entity.ShowLogoOnMasterPage;
            else
                parShowLogoOnMasterPage.Value = System.DBNull.Value;
            cmdParams.Add(parShowLogoOnMasterPage);

            System.Data.IDbDataParameter parIsFTPEnabled = cmd.CreateParameter();
            parIsFTPEnabled.ParameterName = "@IsFTPEnabled";
            if (entity.IsFTPEnabled.HasValue)
                parIsFTPEnabled.Value = entity.IsFTPEnabled;
            else
                parIsFTPEnabled.Value = System.DBNull.Value;
            cmdParams.Add(parIsFTPEnabled);

            System.Data.IDbDataParameter parCompanyAlias = cmd.CreateParameter();
            parCompanyAlias.ParameterName = "@CompanyAlias";
            if (!string.IsNullOrEmpty(entity.CompanyAlias))
                parCompanyAlias.Value = entity.CompanyAlias;
            else
                parCompanyAlias.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyAlias);
            return cmd;

        }

        public bool UpdateCompanyLogoPhysicalPath(CompanyHdrInfo objCompanyHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            try
            {
                System.Data.IDbCommand cmd = null;
                cmd = GetUpdateCompanyLogoPhysicalPathCommand(objCompanyHdrInfo);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private IDbCommand GetUpdateCompanyLogoPhysicalPathCommand(CompanyHdrInfo objCompanyHdrInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_CompanyLogoPhysicalPath");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = objCompanyHdrInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter LogoPhysicalPath = cmd.CreateParameter();
            LogoPhysicalPath.ParameterName = "@LogoPhysicalPath";
            LogoPhysicalPath.Value = objCompanyHdrInfo.LogoPhysicalPath;
            cmdParams.Add(LogoPhysicalPath);

            return cmd;
        }

        public List<CompanyHdrInfo> GetAllCompaniesLiteObject()
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<CompanyHdrInfo> oCompanyHdrInfoCollection = null;

            try
            {
                cmd = CreateGetAllCompaniesLiteObjectCommand();
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oCompanyHdrInfoCollection = MapObjects(dr);
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oCompanyHdrInfoCollection;

        }

        public IList<CompanyHdrInfo> GetAllCompaniesList(string CompanyName, string DisplayName, bool? IsActive, bool? IsFTPActive, bool IsShowActivationHistory, short ActivationHistoryVal)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<CompanyHdrInfo> oCompanyHdrInfoCollection = null;

            try
            {
                cmd = CreateGetAllCompaniesLiistCommand(CompanyName, DisplayName, IsActive, IsFTPActive, IsShowActivationHistory, ActivationHistoryVal);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oCompanyHdrInfoCollection = MapObjects(dr);
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oCompanyHdrInfoCollection;
        }

        public CompanyHdrInfo GetCompanyDetail(int? CompanyID, DateTime? periodEndDate)
        {

            CompanyHdrInfo oCompanyHdrInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                oCommand = CreateGetCompanyinfoCommand(CompanyID, periodEndDate);
                oCommand.Connection = oConnection;
                IDataReader oDataReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDataReader.Read())
                {
                    oCompanyHdrInfo = MapObject(oDataReader);

                }
                oDataReader.Close();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oCompanyHdrInfo;

        }

        private IDbCommand CreateGetAllCompaniesLiteObjectCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CompaniesLiteObject");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        private IDbCommand CreateGetAllCompaniesLiistCommand(string CompanyName, string DisplayName, bool? IsActive, bool? IsFTPActive, bool? IsShowActivationHistory, short ActivationHistoryVal)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllCompaniesList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyName = cmd.CreateParameter();
            parCompanyName.ParameterName = "@CompanyName";
            parCompanyName.Value = CompanyName;
            cmdParams.Add(parCompanyName);

            IDbDataParameter parDisplayName = cmd.CreateParameter();
            parDisplayName.ParameterName = "@DisplayName";
            parDisplayName.Value = DisplayName;
            cmdParams.Add(parDisplayName);

            IDbDataParameter parisActive = cmd.CreateParameter();
            parisActive.ParameterName = "@IsActive";
            if (IsActive.HasValue)
                parisActive.Value = IsActive.Value;
            else
                parisActive.Value = DBNull.Value;
            cmdParams.Add(parisActive);

            IDbDataParameter parisFTPActive = cmd.CreateParameter();
            parisFTPActive.ParameterName = "@IsFTPActive";
            if (IsFTPActive.HasValue)
                parisFTPActive.Value = IsFTPActive.Value;
            else
                parisFTPActive.Value = DBNull.Value;
            cmdParams.Add(parisFTPActive);

            IDbDataParameter parIsShowActivationHistory = cmd.CreateParameter();
            parIsShowActivationHistory.ParameterName = "@IsShowActivationHistory";

            if (IsShowActivationHistory.HasValue)
                parIsShowActivationHistory.Value = IsShowActivationHistory;
            else
                parIsShowActivationHistory.Value = DBNull.Value;
            cmdParams.Add(parIsShowActivationHistory);

            IDbDataParameter parActivationHistoryVal = cmd.CreateParameter();
            parActivationHistoryVal.ParameterName = "@ActivationHistoryVal";
            if (ActivationHistoryVal != 0)
                parActivationHistoryVal.Value = ActivationHistoryVal;
            else
                parActivationHistoryVal.Value = DBNull.Value;
            cmdParams.Add(parActivationHistoryVal);

            return cmd;
        }
        private IDbCommand CreateGetCompanyinfoCommand(int? CompanyID, DateTime? periodEndDate)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CompanyInfoByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parPeriodEndDate = cmd.CreateParameter();
            parPeriodEndDate.ParameterName = "@PeriodEndDate";
            if (periodEndDate.HasValue)
                parPeriodEndDate.Value = periodEndDate.Value;
            else
                parPeriodEndDate.Value = DBNull.Value;
            cmdParams.Add(parPeriodEndDate);

            return cmd;
        }

        #region HasUserLimitReached
        internal bool HasUserLimitReached(int? CompanyID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            bool bLimitReached = false;

            try
            {
                cmd = CreateHasUserLimitReachedCommand(CompanyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                bLimitReached = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return bLimitReached;

        }

        private IDbCommand CreateHasUserLimitReachedCommand(int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_HasUserLimitReached");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID.Value;
            cmdParams.Add(parCompanyID);

            return cmd;
        }
        #endregion


        #region GetCompanyDataStorageCapacityAndCurrentUsage

        public void GetCompanyDataStorageCapacityAndCurrentUsage(int companyID, out decimal? dataStorageCapacity, out decimal? currentUsage)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            dataStorageCapacity = 0M;
            currentUsage = 0M;
            try
            {
                cmd = this.CreateGetCompanyDataStorageCapacityAndCurrentUsageCommand(companyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    dataStorageCapacity = reader.GetDecimalValue("DataStorageCapacity");
                    currentUsage = reader.GetDecimalValue("CurrentUsage");
                }

            }
            finally
            {

                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }
            }
        }

        private IDbCommand CreateGetCompanyDataStorageCapacityAndCurrentUsageCommand(int companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_CompanyDataStorageCapacityAndCurrentUsage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramCompanyID = cmd.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = companyID;
            cmdParams.Add(paramCompanyID);

            return cmd;
        }

        #endregion


        #region GetCompanyLogo
        internal string GetCompanyLogo(int? CompanyID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            string logoPath = null;

            try
            {
                cmd = CreateGetCompanyLogoCommand(CompanyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                logoPath = Convert.ToString(cmd.ExecuteScalar());
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return logoPath;
        }

        private IDbCommand CreateGetCompanyLogoCommand(int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_CompanyLogo");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID.Value;
            cmdParams.Add(parCompanyID);

            return cmd;
        }
        #endregion


        #region GetFeaturesByCompanyID
        internal List<FeatureMstInfo> GetFeaturesByCompanyID(int? CompanyID, int? recPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;

            List<FeatureMstInfo> oFeatureMstInfoCollection = null;
            try
            {
                FeatureMstDAO oFeatureMstDAO = new FeatureMstDAO(this.CurrentAppUserInfo);

                cmd = CreateGetFeaturesByCompanyIDCommand(CompanyID, recPeriodID);
                con = CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oFeatureMstInfoCollection = MapObjectsForFeatureMst(dr);
                dr.ClearColumnHash();
                dr.Close();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }
            }
            return oFeatureMstInfoCollection;
        }


        private IDbCommand CreateGetFeaturesByCompanyIDCommand(int? CompanyID, int? recPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_FeaturesByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID.Value;
            cmdParams.Add(parCompanyID);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (recPeriodID != null)
            {
                parRecPeriodID.Value = recPeriodID.Value;
            }
            else
            {
                parRecPeriodID.Value = DBNull.Value;
            }
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }

        private List<FeatureMstInfo> MapObjectsForFeatureMst(IDataReader dr)
        {
            List<FeatureMstInfo> oFeatureMstInfoCollection = new List<FeatureMstInfo>();
            FeatureMstInfo oFeatureMstInfo = null;

            while (dr.Read())
            {
                oFeatureMstInfo = new FeatureMstInfo();
                oFeatureMstInfo.FeatureID = dr.GetInt16Value("FeatureID");
                oFeatureMstInfoCollection.Add(oFeatureMstInfo);
            }
            return oFeatureMstInfoCollection;
        }

        #endregion


        #region ComapnyWorkWeek

        public IList<CompanyWeekDayInfo> GetComapnyWorkWeek(int? RecPeriodID, int? CompanyID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<CompanyWeekDayInfo> oCompanyWeekDayInfoCollection = new List<CompanyWeekDayInfo>();

            try
            {
                cmd = CreateCompanyWorkWeekCommand(RecPeriodID, CompanyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    CompanyWeekDayInfo oCompanyWorkWeekInfo = null;
                    oCompanyWorkWeekInfo = this.MapForCompanyWorkWeek(dr);
                    oCompanyWeekDayInfoCollection.Add(oCompanyWorkWeekInfo);
                }

            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oCompanyWeekDayInfoCollection;
        }

        private IDbCommand CreateCompanyWorkWeekCommand(int? RecPeriodID, int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_WorkWeekByCompanyIDAndRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID;
            cmdParams.Add(parCompanyID);
            return cmd;
        }

        private CompanyWeekDayInfo MapForCompanyWorkWeek(IDataReader r)
        {
            CompanyWeekDayInfo entity = new CompanyWeekDayInfo();

            entity.CompanyWeekDayID = r.GetInt32Value("CompanyWeekDayID");
            entity.WeekDayID = r.GetInt16Value("WeekDayID");
            entity.StartRecPeriodID = r.GetInt32Value("StartRecPeriodID");
            entity.EndRecPeriodID = r.GetInt32Value("EndRecPeriodID");

            return entity;
        }
        #endregion

        protected override CompanyHdrInfo MapObject(IDataReader r)
        {
            CompanyHdrInfo oCompanyHdrInfo = base.MapObject(r);
            oCompanyHdrInfo.IsSeparateDatabase = r.GetBooleanValue("IsSeparateDatabase");
            oCompanyHdrInfo.IsDatabaseExists = r.GetBooleanValue("IsDatabaseExists");
            oCompanyHdrInfo.IsFTPEnabled = r.GetBooleanValue("IsFTPEnabled");
            oCompanyHdrInfo.CompanyStatusID = r.GetInt16Value("CompanyStatusID");
            oCompanyHdrInfo.CompanyStatusDate = r.GetDateValue("CompanyStatusDate");
            oCompanyHdrInfo.CompanyAlias = r.GetStringValue("CompanyAlias");
            return oCompanyHdrInfo;
        }

        public List<CompanyHdrInfo> SelectAllCompaniesForDatabaseCreation()
        {
            List<CompanyHdrInfo> oCompanyHdrInfoList = new List<CompanyHdrInfo>();
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = GetSelectAllCompaniesForDatabaseCreationCommand())
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oCompanyHdrInfoList.Add(MapObject(dr));
                    }
                }
            }
            return oCompanyHdrInfoList;
        }

        private IDbCommand GetSelectAllCompaniesForDatabaseCreationCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllCompaniesForDatabaseCreation");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public void CreateCompanyDatabase(ServerCompanyInfo oServerCompanyInfo, string BaseDBPath)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = GetCreateCompanyDatabaseCommand(oServerCompanyInfo, BaseDBPath))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }

        private IDbCommand GetCreateCompanyDatabaseCommand(ServerCompanyInfo oServerCompanyInfo, string BaseDBPath)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RESTORE DATABASE ");
            sb.Append(oServerCompanyInfo.DatabaseName);
            sb.Append(" FROM DISK=N'");
            sb.Append(BaseDBPath);
            sb.Append("' WITH  FILE = 1,");
            sb.Append(" MOVE N'SkyStemARTBase' TO N'");
            sb.Append(oServerCompanyInfo.MdfPath);
            sb.Append(oServerCompanyInfo.DatabaseName);
            sb.Append(".mdf',");
            sb.Append(" MOVE N'SkyStemARTBase_log' TO N'");
            sb.Append(oServerCompanyInfo.LdfPath);
            sb.Append(oServerCompanyInfo.DatabaseName);
            sb.Append("_log.ldf',  NOUNLOAD,  STATS = 10");
            System.Data.IDbCommand cmd = this.CreateCommand(sb.ToString());
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void DropCompanyDatabase(ServerCompanyInfo oServerCompanyInfo)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = GetDropCompanyDatabaseCommand(oServerCompanyInfo))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }

        private IDbCommand GetDropCompanyDatabaseCommand(ServerCompanyInfo oServerCompanyInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" ALTER DATABASE " + oServerCompanyInfo.DatabaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            sb.AppendLine(" DROP DATABASE " + oServerCompanyInfo.DatabaseName);
            System.Data.IDbCommand cmd = this.CreateCommand(sb.ToString());
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public List<FTPServerInfo> GetAllFTPServerListObject(int? CompanyID)
        {
            List<FTPServerInfo> oFTPServerInfoList = new List<FTPServerInfo>();
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateGetAllFTPServerListObjectCommand(CompanyID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oFTPServerInfoList.Add(MapObjectFTPServer(dr));
                    }
                }
            }
            return oFTPServerInfoList;
        }
        private IDbCommand CreateGetAllFTPServerListObjectCommand(int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_FTPServerList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyId";
            parCompanyID.Value = CompanyID;
            cmdParams.Add(parCompanyID);

            return cmd;
        }

        public bool? IsCompanyAliasUnique(string companyAlias, int? CompanyID)
        {
            bool? isUnique = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandIsCompanyAliasUnique(companyAlias, CompanyID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        isUnique = dr.GetBooleanValue("IsUnique");
                    }
                }
            }
            return isUnique;
        }

        private IDbCommand CreateCommandIsCompanyAliasUnique(string CompanyAlias, int? companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsUniqueCompanyAlias");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (companyID.HasValue && companyID > 0)
                parCompanyID.Value = companyID;
            else
                parCompanyID.Value = DBNull.Value;
            cmdParams.Add(parCompanyID);

            IDbDataParameter parCompanyAlias = cmd.CreateParameter();
            parCompanyAlias.ParameterName = "@CompanyAlias";
            parCompanyAlias.Value = CompanyAlias;
            cmdParams.Add(parCompanyAlias);
            return cmd;
        }

        private FTPServerInfo MapObjectFTPServer(IDataReader r)
        {
            FTPServerInfo oFTPServerInfo = new FTPServerInfo();
            oFTPServerInfo.FTPServerId = r.GetInt16Value("FTPServerId").GetValueOrDefault();
            oFTPServerInfo.ServerName = r.GetStringValue("ServerName");
            oFTPServerInfo.Port = r.GetInt32Value("Port").GetValueOrDefault();
            oFTPServerInfo.FTPUrl = r.GetStringValue("FTPUrl");
            return oFTPServerInfo;
        }
    }
}