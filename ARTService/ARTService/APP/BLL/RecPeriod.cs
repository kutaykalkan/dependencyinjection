using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Service.APP.BLL
{
    public class RecPeriod
    {
        private SqlConnection oConnection = null;
        private CompanyUserInfo CompanyUserInfo;


        public RecPeriod(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }

        public void SetRecPeriodStatus()
        {
            List<int> oRecPeriodIDList = GetOpenInProgressRecPeriodIDList();
            if (oRecPeriodIDList.Count > 0)
            {
                for (int i = 0; i < oRecPeriodIDList.Count; i++)
                {
                    SetRecPeriodStatus(oRecPeriodIDList[i]);
                }
            }
        }

        public void SetRecPeriodStatus(int ReconciliationPeriodID)
        {
            SqlTransaction oTrans = null;
            try
            {
                //Transaction put in query execution as now it contains copy open items also in MOP
                oConnection = Helper.CreateConnection(this.CompanyUserInfo);
                oConnection.Open();
                oTrans = oConnection.BeginTransaction();
                SqlCommand oCommand = CreateSetRecPeriodStatusCommand(DateTime.Today, ReconciliationPeriodID, (short)ARTEnums.ActionType.CloseRecPeriodFromService, (short)ARTEnums.ChangeSource.RecPeriodStatusChange);
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTrans;
                oCommand.ExecuteNonQuery();
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                    oTrans.Rollback();
                Helper.LogError(string.Format("{0} -> Error:: {1}", ServiceConstants.SERVICE_NAME_REC_PERIOD, ex.Message),this.CompanyUserInfo );
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();

                // TODO: Do we need mail?
            }
        }

        private SqlCommand CreateSetRecPeriodStatusCommand(DateTime currentDate, int ReconciliationPeriodID, short actionTypeID, short changeSourceIDSRA)
        {
            SqlCommand oCommand = Helper.CreateCommand(this.CompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SVC_UPD_RecPeriodStatus";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter parCurrentDate = new SqlParameter("@CurrentDate", currentDate.Date);
            cmdParamCollection.Add(parCurrentDate);

            SqlParameter parRecPeriodID = new SqlParameter("@RecPeriodID", ReconciliationPeriodID);
            cmdParamCollection.Add(parRecPeriodID);

            SqlParameter parActionTypeID = new SqlParameter("@ActionTypeID", actionTypeID);
            cmdParamCollection.Add(parActionTypeID);

            SqlParameter parChangeSourceIDSRA = new SqlParameter("@ChangeSourceIDSRA", changeSourceIDSRA);
            cmdParamCollection.Add(parChangeSourceIDSRA);

            SqlParameter parRevisedBy = new SqlParameter("@RevisedBy", Helper.GetUserIDForServiceProcessing());
            cmdParamCollection.Add(parRevisedBy);

            return oCommand;
        }

        public List<int> GetOpenInProgressRecPeriodIDList()
        {
            List<int> oRecPeriodIDList = new List<int>();
            SqlCommand oCommand = null;
            SqlConnection oConnection = null;
            SqlDataReader reader = null;
            SqlTransaction oTransaction = null;
            try
            {
                oCommand = CreateGetOpenInProgressRecPeriodIDListCommand();
                oConnection = Helper.CreateConnection(this.CompanyUserInfo);
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;
                reader = oCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int ReconciliationPeriodID = MapObject(reader);
                        if (ReconciliationPeriodID > 0)
                            oRecPeriodIDList.Add(ReconciliationPeriodID);
                    }
                }
                else
                {
                    Helper.LogInfo(@"There is No Open / In Progress Rec Period.", this.CompanyUserInfo);
                }
                reader.Close();
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
                return oRecPeriodIDList;
            }
            catch (Exception ex)
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }

        private SqlCommand CreateGetOpenInProgressRecPeriodIDListCommand()
        {
            SqlCommand oCommand = Helper.CreateCommand(this.CompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SVC_GET_GetAllOpenInProgressRecPeriodID";
            return oCommand;
        }
        private int MapObject(IDataReader reader)
        {
            int ReconciliationPeriodID = 0;
            int ordinal;
            ordinal = reader.GetOrdinal("ReconciliationPeriodID");
            if (!reader.IsDBNull(ordinal)) ReconciliationPeriodID = ((System.Int32)(reader.GetValue(ordinal)));
            return ReconciliationPeriodID;
        }


    }
}
