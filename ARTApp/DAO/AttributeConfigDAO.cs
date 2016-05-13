using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Data;
using System.Transactions;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class AttributeConfigDAO : AttributeConfigDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AttributeConfigDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public void SaveAttributes(List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList, int? companyID, string userLoginID, DateTime dateRevised, int? userID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    cmd = CreateSaveCommandAttributeInfoList(oCompanyAttributeConfigInfoList, companyID, userLoginID, dateRevised, userID);
                    cnn = this.CreateConnection();
                    cnn.Open();
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                    scope.Complete();
                }
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        private IDbCommand CreateSaveCommandAttributeInfoList(List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList,
            int? companyID, string userLoginID, DateTime dateRevised, int? userID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_Attributes");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            DataTable dt = ServiceHelper.ConvertCompanyAttributeInfoToDataTable(oCompanyAttributeConfigInfoList);
            IDbDataParameter paramCompanyRoleList = cmd.CreateParameter();
            paramCompanyRoleList.ParameterName = "@udtAttributeValue";
            paramCompanyRoleList.Value = dt;
            cmdParams.Add(paramCompanyRoleList);

            ServiceHelper.AddCommonParametersForCompanyID(companyID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(dateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedUserID(userID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateRevised(dateRevised, cmd, cmdParams);

            return cmd;
        }

        public List<CompanyAttributeConfigInfo> GetAttributeInfoList(int? companyID, int? attributeSetTypeID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList = new List<CompanyAttributeConfigInfo>();
            try
            {
                if (companyID != null)
                {
                    cmd = CreateSelectCommandAttributeInfoList(companyID, attributeSetTypeID);
                    cnn = this.CreateConnection();
                    cnn.Open();
                    cmd.Connection = cnn;
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    CompanyAttributeConfigInfo oCompanyAttributeConfigInfo;
                    int rowNumber = 1;
                    while (dr.Read())
                    {
                        oCompanyAttributeConfigInfo = this.MapAttributeInfoObject(dr);
                        oCompanyAttributeConfigInfo.RowNumber = rowNumber++;
                        oCompanyAttributeConfigInfoList.Add(oCompanyAttributeConfigInfo);
                    }
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return oCompanyAttributeConfigInfoList;
        }

        private IDbCommand CreateSelectCommandAttributeInfoList(int? companyID, int? attributeSetTypeID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_Attributes");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdCompany = cmd.CreateParameter();
            cmdCompany.ParameterName = "@CompanyID";
            cmdCompany.Value = companyID.Value;
            cmdParams.Add(cmdCompany);

            IDbDataParameter cmdAttributeSetTypeID = cmd.CreateParameter();
            cmdAttributeSetTypeID.ParameterName = "@AttributeSetTypeID";
            cmdAttributeSetTypeID.Value = attributeSetTypeID.Value;
            cmdParams.Add(cmdAttributeSetTypeID);

            return cmd;
        }
    }
}
