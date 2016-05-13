using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.APP.DAO;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public class MatchingSourceAccountDAO : AbstractDAO
    {

        public MatchingSourceAccountDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }
        public DataTable GetAccountIDs(DataTable oDataTable)
        {
            SqlConnection oConnection = null;
            SqlCommand oCommand = null;
            SqlDataReader reader = null;
            try
            {
                DataTable oDT = new DataTable();
                oConnection = this.GetConnection();
                oCommand = GetAccountIdProcessingCommand(oDataTable);
                oConnection.Open();
                oCommand.Connection = oConnection;
                SqlDataAdapter oDataAdapter = new SqlDataAdapter(oCommand);
                oDataAdapter.Fill(oDT);
                oDataAdapter = null;
                return oDT;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }


        private SqlCommand GetAccountIdProcessingCommand(DataTable oDataTable)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "Matching.usp_SRV_SEL_GLTBSAccountID";
            return oCommand;
        }
    }
}
