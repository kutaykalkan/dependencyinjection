


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.DAO
{
    public class NetAccountHdrDAO : NetAccountHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NetAccountHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public int? UpdateNetAccountAttributeValue(NetAccountHdrInfo oNetAccountHdrInfo, List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo, int recPeriodID)
        {

            IDbCommand cmd = null;
            IDbCommand cmdValues = null;
            IDbTransaction oTransaction = null;
            try
            {
                //Begin Transaction

                //cmd = this.CreateInsertCommand(oNetAccountHdrInfo);
                //cmd.Connection = this.CreateConnection();
                //oTransaction = cmd.Connection.BeginTransaction();
                //cmd.Transaction = oTransaction;
                //IDbDataParameter parNetAccountID = (IDbDataParameter)cmd.Parameters["@NetAccountID"];
                //oNetAccountHdrInfo.NetAccountID = Convert.ToInt32(parNetAccountID.Value);
                //cmd.ExecuteNonQuery();

                NetAccountAttributeValueDAO oNetAccountAttributeValueDAO = new NetAccountAttributeValueDAO(this.CurrentAppUserInfo);
                cmdValues = oNetAccountAttributeValueDAO.CreateUpdateNetAccountAttributesValueCommand(lstNetAccountAttributeValueInfo, oNetAccountHdrInfo, recPeriodID);
                cmdValues.Connection = this.CreateConnection();
                cmdValues.Connection.Open();
                oTransaction = cmdValues.Connection.BeginTransaction();
                cmdValues.Transaction = oTransaction;
                cmdValues.ExecuteNonQuery();

                oTransaction.Commit();
                //End Transaction
                return oNetAccountHdrInfo.NetAccountID;
            }
            catch (Exception)
            {
                oTransaction.Rollback();
                return 0;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null && cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                oTransaction = null;
            }


        }

        public int? UpdateNetAccountAttributeValue(NetAccountHdrInfo oNetAccountHdrInfo, List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo, int recPeriodID, IDbConnection con, IDbTransaction oTransaction)
        {

            IDbCommand cmdValues = null;
            NetAccountAttributeValueDAO oNetAccountAttributeValueDAO = new NetAccountAttributeValueDAO(this.CurrentAppUserInfo);
            cmdValues = oNetAccountAttributeValueDAO.CreateUpdateNetAccountAttributesValueCommand(lstNetAccountAttributeValueInfo, oNetAccountHdrInfo, recPeriodID);
            cmdValues.Connection = con;
            cmdValues.Transaction = oTransaction;
            cmdValues.ExecuteNonQuery();

            return oNetAccountHdrInfo.NetAccountID;

        }

        public int? DeleteNetAccount(NetAccountParamInfo oNetAccountParamInfo, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            //Delete Net Account here
            cmd = this.CreateDeleteNetAccountCommand(oNetAccountParamInfo);
            cmd.Connection = con;
            cmd.Transaction = oTransaction;
            int? iCount = cmd.ExecuteNonQuery();
            return iCount;
        }

        public int? UpdateNetAccount(NetAccountHdrInfo oNetAccountHdrInfo, List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo, int recPeriodID)
        {

            IDbCommand cmd = null;
            IDbCommand cmdValues = null;
            IDbTransaction oTransaction = null;
            try
            {
                //Begin Transaction
                cmd = this.CreateUpdateCommand(oNetAccountHdrInfo);
                cmd.Connection = this.CreateConnection();
                oTransaction = cmd.Connection.BeginTransaction();
                cmd.Transaction = oTransaction;
                cmd.ExecuteNonQuery();

                NetAccountAttributeValueDAO oNetAccountAttributeValueDAO = new NetAccountAttributeValueDAO(this.CurrentAppUserInfo);
                cmdValues = oNetAccountAttributeValueDAO.CreateUpdateNetAccountAttributesValueCommand(lstNetAccountAttributeValueInfo, oNetAccountHdrInfo, recPeriodID);
                cmdValues.Transaction = oTransaction;
                cmdValues.ExecuteNonQuery();

                oTransaction.Commit();
                //End Transaction
                return oNetAccountHdrInfo.NetAccountID;
            }
            catch (Exception)
            {
                oTransaction.Rollback();
                return 0;
            }
            finally
            {
                if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                oTransaction = null;
            }


        }

        private IDbCommand SearchNetAccountsCommand(NetAccountSearchParamInfo oNetAccountSearchParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_NetAccountHDR");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter cmdParamCompanyID = cmd.CreateParameter();

            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = oNetAccountSearchParamInfo.CompanyID;
            cmdParams.Add(cmdParamCompanyID);

            ServiceHelper.AddCommonUserRoleAndRecPeriodParameters(oNetAccountSearchParamInfo, cmd);

            IDbDataParameter cmdParamLanguageID = cmd.CreateParameter();
            cmdParamLanguageID.ParameterName = "@languageID";
            cmdParamLanguageID.Value = oNetAccountSearchParamInfo.UserLanguageID;
            cmdParams.Add(cmdParamLanguageID);

            return cmd;
        }


        public List<NetAccountHdrInfo> GetNetAccountHdrInfoCollection(NetAccountSearchParamInfo oNetAccountSearchParamInfo)
        {
            List<NetAccountHdrInfo> oNetAccountHdrInfoCollection = new List<NetAccountHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.SearchNetAccountsCommand(oNetAccountSearchParamInfo);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    oNetAccountHdrInfoCollection.Add(this.MapObject(reader));
                }
                reader.ClearColumnHash();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oNetAccountHdrInfoCollection;
        }

        protected override NetAccountHdrInfo MapObject(IDataReader r)
        {
            NetAccountHdrInfo entity = base.MapObject(r);
            entity.IsLocked = r.GetBooleanValue("IsLocked");
            entity.ReconciliationStatusID = r.GetInt16Value("ReconciliationStatusID");
            entity.IsSystemReconciled = r.GetBooleanValue("IsSystemReconcilied");
            return entity;
        }

        public string GetNetAccountNameByNetAccountID(int netAccountID, int companyID, int lcID, int businessEntityID, int defaultLCID)
        {
            string netAccountName = null;
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateCommand("usp_GET_NetAccountNameByNetAccountID");
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdCompanyID = cmd.CreateParameter();
                cmdCompanyID.ParameterName = "@CompanyID";
                cmdCompanyID.Value = companyID;
                cmdParams.Add(cmdCompanyID);

                IDbDataParameter cmdNetAccountID = cmd.CreateParameter();
                cmdNetAccountID.ParameterName = "@NetAccountID";
                cmdNetAccountID.Value = netAccountID;
                cmdParams.Add(cmdNetAccountID);

                ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, lcID, businessEntityID, defaultLCID);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                netAccountName = (string)cmd.ExecuteScalar();

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return netAccountName;
        }

        private IDbCommand CreateDeleteNetAccountCommand(NetAccountParamInfo oNetAccountParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_NetAccountByRecPeriod");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamNetAccountID = cmd.CreateParameter();
            cmdParamNetAccountID.ParameterName = "@NetAccountID";
            cmdParamNetAccountID.Value = oNetAccountParamInfo.NetAccountID.Value;
            cmdParams.Add(cmdParamNetAccountID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = oNetAccountParamInfo.CompanyID.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = oNetAccountParamInfo.RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);

            IDbDataParameter cmdUserLoginID = cmd.CreateParameter();
            cmdUserLoginID.ParameterName = "@UserLoginID";
            cmdUserLoginID.Value = oNetAccountParamInfo.UserLoginID;
            cmdParams.Add(cmdUserLoginID);

            IDbDataParameter cmdUpdateTime = cmd.CreateParameter();
            cmdUpdateTime.ParameterName = "@UpdateTime";
            cmdUpdateTime.Value = oNetAccountParamInfo.DateRevised;
            cmdParams.Add(cmdUpdateTime);


            return cmd;
        }

        public bool IsNetAccountDuplicate(string netAccountName, int companyID, int ReconciliationPeriodID)
        {
            bool IsDuplicate = true;
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateCommand("usp_GET_IsNetAccountDuplicate");
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdCompanyID = cmd.CreateParameter();
                cmdCompanyID.ParameterName = "@CompanyID";
                cmdCompanyID.Value = companyID;
                cmdParams.Add(cmdCompanyID);

                IDbDataParameter cmdNetAccountName = cmd.CreateParameter();
                cmdNetAccountName.ParameterName = "@NetAccountName";
                cmdNetAccountName.Value = netAccountName;
                cmdParams.Add(cmdNetAccountName);

                IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
                cmdRecPeriodID.ParameterName = "@RecPeriodID";
                cmdRecPeriodID.Value = ReconciliationPeriodID;
                cmdParams.Add(cmdRecPeriodID);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IsDuplicate = (bool)cmd.ExecuteScalar();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return IsDuplicate;
        }

    }
}