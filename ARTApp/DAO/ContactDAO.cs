using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class ContactDAO : ContactDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ContactDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public void UpdateContactInfo(ContactInfo objContactInfo)
        {
            if (objContactInfo.ContactID.HasValue)
            {
                if (objContactInfo.ContactID.Value > 0)
                {
                    using (IDbConnection cnn = this.CreateConnection())
                    {
                        cnn.Open();
                        using (IDbCommand cmd = GetUpdateCommand(objContactInfo))
                        {
                            cmd.Connection = cnn;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    ContactDAO oContactDAO = new ContactDAO(this.CurrentAppUserInfo);
                    objContactInfo.AddedBy = objContactInfo.RevisedBy;
                    objContactInfo.DateAdded = objContactInfo.DateRevised;
                    oContactDAO.Save(objContactInfo);
                }
            }
        }


        private System.Data.IDbCommand GetUpdateCommand(ContactInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_Contact");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;




            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);
            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parEmail = cmd.CreateParameter();
            parEmail.ParameterName = "@Email";
            if (!entity.IsEmailNull)
                parEmail.Value = entity.Email;
            else
                parEmail.Value = System.DBNull.Value;
            cmdParams.Add(parEmail);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsPrimaryContact = cmd.CreateParameter();
            parIsPrimaryContact.ParameterName = "@IsPrimaryContact";
            if (!entity.IsIsPrimaryContactNull)
                parIsPrimaryContact.Value = entity.IsPrimaryContact;
            else
                parIsPrimaryContact.Value = System.DBNull.Value;
            cmdParams.Add(parIsPrimaryContact);

            System.Data.IDbDataParameter parName = cmd.CreateParameter();
            parName.ParameterName = "@Name";
            if (!entity.IsNameNull)
                parName.Value = entity.Name;
            else
                parName.Value = System.DBNull.Value;
            cmdParams.Add(parName);

            System.Data.IDbDataParameter parPhone = cmd.CreateParameter();
            parPhone.ParameterName = "@Phone";
            if (!entity.IsPhoneNull)
                parPhone.Value = entity.Phone;
            else
                parPhone.Value = System.DBNull.Value;
            cmdParams.Add(parPhone);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparContactID = cmd.CreateParameter();
            pkparContactID.ParameterName = "@ContactID";
            pkparContactID.Value = entity.ContactID;
            cmdParams.Add(pkparContactID);


            return cmd;

        }

        public void InsertContactInfoInCore(ContactInfo objContactInfo)
        {
            System.Data.IDbCommand cmd = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cmd = GetInsertContactInfoInCoreCommand(objContactInfo);
                cnn.Open();
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
            }
        }

        private System.Data.IDbCommand GetInsertContactInfoInCoreCommand(ContactInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_ContactTransit");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parName = cmd.CreateParameter();
            parName.ParameterName = "@Name";
            if (!entity.IsNameNull)
                parName.Value = entity.Name;
            else
                parName.Value = System.DBNull.Value;
            cmdParams.Add(parName);

            System.Data.IDbDataParameter parPhone = cmd.CreateParameter();
            parPhone.ParameterName = "@Phone";
            if (!entity.IsPhoneNull)
                parPhone.Value = entity.Phone;
            else
                parPhone.Value = System.DBNull.Value;
            cmdParams.Add(parPhone);

            System.Data.IDbDataParameter parEmail = cmd.CreateParameter();
            parEmail.ParameterName = "@Email";
            if (!entity.IsEmailNull)
                parEmail.Value = entity.Email;
            else
                parEmail.Value = System.DBNull.Value;
            cmdParams.Add(parEmail);

            System.Data.IDbDataParameter parIsPrimaryContact = cmd.CreateParameter();
            parIsPrimaryContact.ParameterName = "@IsPrimaryContact";
            if (!entity.IsIsPrimaryContactNull)
                parIsPrimaryContact.Value = entity.IsPrimaryContact;
            else
                parIsPrimaryContact.Value = System.DBNull.Value;
            cmdParams.Add(parIsPrimaryContact);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);


            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            return cmd;
        }

        public void DeleteContactsFromTransit(int? CompanyID)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandDeleteContactsFromTransit(CompanyID))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected IDbCommand CreateCommandDeleteContactsFromTransit(int? companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_ContactTransit");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);
            return cmd;
        }
    }
}