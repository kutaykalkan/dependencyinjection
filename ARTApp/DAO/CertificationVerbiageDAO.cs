


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class CertificationVerbiageDAO : CertificationVerbiageDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CertificationVerbiageDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<CertificationVerbiageInfo> GetCertificationVerbiage(int? reconciliationPeriodID, int? companyID, short? certificationTypeID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CertificationVerbiage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parCertificationTypeID = cmd.CreateParameter();
            parCertificationTypeID.ParameterName = "@CertificationTypeID";
            parCertificationTypeID.Value = certificationTypeID;
            cmdParams.Add(parCertificationTypeID);

            return this.Select(cmd);
        }

        public void saveCertificationVerbiage(CertificationVerbiageInfo entity, int RecPeriodID, IDbTransaction oTransaction, IDbConnection oConnection)
        {

            try
            {
                IDbCommand cmd;
                cmd = CreateInsertCertificationVerbiageInfoCommand(entity, RecPeriodID);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                cmd.ExecuteScalar();


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CertificationVerbiageInfo> GetCertificationVerbiageByCompanyIDRecPeriodID(int companyID, int RecPeriodID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            IDataReader reader = null;
            List<CertificationVerbiageInfo> oCertVerbiageCollection = new List<CertificationVerbiageInfo>();
            try
            {
                oCommand = this.CreateSelectCertVerbiageByCompanyIDRecPeriodIDCommand(companyID, RecPeriodID);
                oConnection = this.CreateConnection();
                oCommand.Connection = oConnection;
                oConnection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oCertVerbiageCollection = this.MapObjects(reader);
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
            return oCertVerbiageCollection;
        }
        protected IDbCommand CreateSelectCertVerbiageByCompanyIDRecPeriodIDCommand(int companyID, int recPeriodID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_CertificationVerbiageByRecPeriodIDCompanyID");
            oCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramCompanyID = oCommand.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = companyID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@ReconciliationPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            cmdParams.Add(paramCompanyID);
            cmdParams.Add(paramRecPeriodID);

            return oCommand;
        }
        protected IDbCommand CreateInsertCertificationVerbiageInfoCommand(CertificationVerbiageInfo entity, int RecPeriodID)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CertificationVerbiage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCertificationTypeID = cmd.CreateParameter();
            parCertificationTypeID.ParameterName = "@CertificationTypeID";
            if (!entity.IsCertificationTypeIDNull)
                parCertificationTypeID.Value = entity.CertificationTypeID;
            else
                parCertificationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationTypeID);

            System.Data.IDbDataParameter parCertificationVerbiage = cmd.CreateParameter();
            parCertificationVerbiage.ParameterName = "@CertificationVerbiage";
            if (!entity.IsCertificationVerbiageNull)
                parCertificationVerbiage.Value = entity.CertificationVerbiage;
            else
                parCertificationVerbiage.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationVerbiage);

            System.Data.IDbDataParameter parCertificationVerbiageLabelID = cmd.CreateParameter();
            parCertificationVerbiageLabelID.ParameterName = "@CertificationVerbiageLabelID";
            if (!entity.IsCertificationVerbiageLabelIDNull)
                parCertificationVerbiageLabelID.Value = entity.CertificationVerbiageLabelID;
            else
                parCertificationVerbiageLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationVerbiageLabelID);

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

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parInputReconciliationPeriodID = cmd.CreateParameter();
            parInputReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";

            parInputReconciliationPeriodID.Value = RecPeriodID;

            cmdParams.Add(parInputReconciliationPeriodID);


            return cmd;

        }
    }
}