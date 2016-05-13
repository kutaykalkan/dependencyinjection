using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using System.Data.SqlClient;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.Data;


namespace SkyStem.ART.Service.APP.DAO
{
    public class CapabilityDAO : AbstractDAO
    {
        public CapabilityDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }

        public List<CapabilityInfo> SelectAllCompanyCapabilityByReconciliationPeriodID(int recPeriodID)
        {
            List<CapabilityInfo> oCapabilityInfoCollection = new List<CapabilityInfo>();
            SqlConnection oConnection = null;
            SqlCommand oCommand = null;
            SqlDataReader oDataReader = null;
            CapabilityInfo oCapabilityInfo = null;
            try
            {
                oConnection = this.GetConnection();
                oCommand = GetSelectAllCompanyCapabilityByReconciliationPeriodIDCommand(recPeriodID);
                oConnection.Open();
                oCommand.Connection = oConnection;
                oDataReader = oCommand.ExecuteReader();

                while (oDataReader.Read())
                {
                    oCapabilityInfo = MapCapabilityObject(oDataReader);
                    oCapabilityInfoCollection.Add(oCapabilityInfo);
                    //this.MapAccountAttributeInfo(reader, oAccountStatusReportInfo);
                }

                return oCapabilityInfoCollection;
            }
            finally
            {
                if (oDataReader != null && !oDataReader.IsClosed)
                    oDataReader.Close();

                if (oConnection != null && oConnection.State == ConnectionState.Open)
                    oConnection.Dispose();
            }
        }

        private static CapabilityInfo MapCapabilityObject(SqlDataReader r)
        {
            CapabilityInfo oCapabilityInfo = new CapabilityInfo();
            try
            {
                int ordinal = r.GetOrdinal("CapabilityID");
                if (!r.IsDBNull(ordinal)) oCapabilityInfo.CapabilityID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsActivated");
                if (!r.IsDBNull(ordinal)) oCapabilityInfo.IsActivated = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return oCapabilityInfo;
        }

        private SqlCommand GetSelectAllCompanyCapabilityByReconciliationPeriodIDCommand(int recPeriodID)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SEL_CompanyCapabilityActivationByRecPeriodID";
            SqlParameterCollection cmdParamCapability = oCommand.Parameters;
            SqlParameter paramReconciliationPeriodID = new SqlParameter("@ReconciliationPeriodID", recPeriodID);
            cmdParamCapability.Add(paramReconciliationPeriodID);

            return oCommand;
        }
    }
}
