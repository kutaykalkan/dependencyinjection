using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public class MatchSetResultDAO : AbstractDAO
    {
        public MatchSetResultDAO(CompanyUserInfo oCompnayUserInfo)
            : base(oCompnayUserInfo)
        {
        }
        public int SaveMatchSetResultInfoList(DataTable oDTMatchSetResult, string AddedBy, DateTime DateAdded, CompanyUserInfo oCompanyUserInfo)
        {
            SqlConnection oConnection = null;
            SqlTransaction oTransaction = null;
            SqlCommand oCmd = null;
            try
            {
                oConnection = this.GetConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCmd = this.GetSaveMatchSetResultInfoListCommand(oDTMatchSetResult, AddedBy, DateAdded);
                oCmd.Connection = oConnection;
                oCmd.Transaction = oTransaction;
                int retVal = oCmd.ExecuteNonQuery();
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
                return retVal;
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                Helper.LogError(@"Error while saving data to database for Match Results: " + ex.Message, this.CompanyUserInfo );
                
                throw new Exception("Error while saving data to database for Match Results.");
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }

        private SqlCommand GetSaveMatchSetResultInfoListCommand(DataTable oDTMatchSetResult, string AddedBy, DateTime DateAdded)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = System.Data.CommandType.StoredProcedure;
            oCommand.CommandText = "Matching.usp_SVC_INS_MatchSetResult";

            SqlParameterCollection oParams = oCommand.Parameters;

            SqlParameter oParamMatchSetResultCollection = new SqlParameter("@udtMatchSetResult", oDTMatchSetResult);
            SqlParameter oParamAddedBy = new SqlParameter("@AddedBy", AddedBy);
            SqlParameter oParamDateAdded = new SqlParameter("@DateAdded", DateAdded);

            oParams.Add(oParamMatchSetResultCollection);
            oParams.Add(oParamAddedBy);
            oParams.Add(oParamDateAdded);

            return oCommand;
        }
    }
}
