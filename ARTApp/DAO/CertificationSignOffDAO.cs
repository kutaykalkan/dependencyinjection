


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class CertificationSignOffDAO : CertificationSignOffDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CertificationSignOffDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<CertificationSignOffInfo> GetCertificationSignOff(int? reconciliationPeriodID, int? userID, short? roleID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CertificationSignOff");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = roleID;
            cmdParams.Add(parRoleID);


            return this.Select(cmd);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="reconciliationPeriodID"></param>
        /// <param name="userID"> to calculated the accounts under access</param>
        /// <param name="roleID"></param>
        /// <param name="userIDJunior">the parent user for juniors</param>
        /// <param name="roleIDJunior"></param>
        /// <returns></returns>
        public List<CertificationSignOffInfo> GetCertificationSignOffForJuniors(int? reconciliationPeriodID, int? userID, short? roleID, int? UserIDForAccess, short? RoleIDForAccess)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CertificationSignOffForJuniors");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@RecPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = roleID;
            cmdParams.Add(parRoleID);



            System.Data.IDbDataParameter parUserIDForAccess = cmd.CreateParameter();
            parUserIDForAccess.ParameterName = "@UserIDForAccess";
            parUserIDForAccess.Value = UserIDForAccess;
            cmdParams.Add(parUserIDForAccess);


            System.Data.IDbDataParameter parRoleIDForAccess = cmd.CreateParameter();
            parRoleIDForAccess.ParameterName = "@RoleIDForAccess";
            parRoleIDForAccess.Value = RoleIDForAccess;
            cmdParams.Add(parRoleIDForAccess);

            return this.Select(cmd);
        }

        public List<CertificationSignOffInfo> GetCertificationSignOffForJuniorsOfControllerAndCEOCFO(int? reconciliationPeriodID, int? userID, short? roleID, int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CertificationSignOffForJuniorsOfControllerAndCEOCFO");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@RecPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = roleID;
            cmdParams.Add(parRoleID);


            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID;
            cmdParams.Add(parCompanyID);

            return this.Select(cmd);
        }

        public bool GetIsCertificationStarted(int? reconciliationPeriodID)
        {
            bool IsCertificationStarted = false;
            IDbConnection oConnection = null;

            try
            {
                oConnection = this.CreateConnection();
                oConnection.Open();
                IDbCommand IDBCmmd = this.CreateCommand("usp_GET_IsCertificationStarted");
                IDBCmmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = IDBCmmd.Parameters;
                IDbDataParameter cmdParamRecPeriodID = IDBCmmd.CreateParameter();
                cmdParamRecPeriodID.ParameterName = "@RecPeriodID";
                cmdParamRecPeriodID.Value = reconciliationPeriodID;
                cmdParams.Add(cmdParamRecPeriodID);
                IDBCmmd.Connection = oConnection;
                object o = IDBCmmd.ExecuteScalar();
                if (o != null)
                    IsCertificationStarted = Convert.ToBoolean(o);

                return IsCertificationStarted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override CertificationSignOffInfo MapObject(IDataReader r)
        {
            CertificationSignOffInfo oCertificationSignOffInfo = base.MapObject(r);
            oCertificationSignOffInfo.BackupUserID = r.GetInt32Value("BackupUserID");
            oCertificationSignOffInfo.BackupRoleID = r.GetInt16Value("BackupRoleID");
            oCertificationSignOffInfo.BackupFirstName = r.GetStringValue("BackupFirstName");
            oCertificationSignOffInfo.BackupLastName = r.GetStringValue("BackupLastName");
            oCertificationSignOffInfo.BackupMadatoryReportSignOffDate = r.GetDateValue("BackupMadatoryReportSignOffDate");
            oCertificationSignOffInfo.BackupCertificationBalancesDate = r.GetDateValue("BackupCertificationBalancesDate");
            oCertificationSignOffInfo.BackupCertificationBalancesComments = r.GetStringValue("BackupCertificationBalancesComments");
            oCertificationSignOffInfo.BackupExceptionCertificationDate = r.GetDateValue("BackupExceptionCertificationDate");
            oCertificationSignOffInfo.BackupExceptionCertificationComments = r.GetStringValue("BackupExceptionCertificationComments");
            oCertificationSignOffInfo.BackupAccountCertificationDate = r.GetDateValue("BackupAccountCertificationDate");
            oCertificationSignOffInfo.BackupAccountCertificationComments = r.GetStringValue("BackupAccountCertificationComments");

            return oCertificationSignOffInfo;
        }

    }//End of class
}//end of namespace