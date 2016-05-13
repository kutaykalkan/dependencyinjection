


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using System.Data.SqlClient;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class ReconciliationCategoryMstDAO : ReconciliationCategoryMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationCategoryMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public IList<ReconciliationCategoryMstInfo> GetAllOpenItemClassification()
        {
            IDbCommand oCommand = this.GetAllOpenItemClassificationCommand();
            oCommand.Connection = this.CreateConnection();
            return this.Select(oCommand);
        }
        private IDbCommand GetAllOpenItemClassificationCommand()
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_AllOpenItemClassification");
            oCommand.CommandType = CommandType.StoredProcedure;

            return oCommand;
        }


        public List<ReconciliationCategoryMstInfo> GetAllReconciliationCategory(int? recPeriodID, long? AccountID, ref int? RecTemplateID)
        {
            List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = new List<ReconciliationCategoryMstInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetAllReconciliationCategoryCommand(recPeriodID, AccountID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                ReconciliationCategoryMstInfo oReconciliationCategoryMstInfo = null;
                while (reader.Read())
                {
                    oReconciliationCategoryMstInfo = this.MapObject(reader);
                    oReconciliationCategoryMstInfo.ReconciliationCategoryType = reader.GetStringValue("ReconciliationCategoryType");
                    oReconciliationCategoryMstInfo.ReconciliationCategoryTypeID = reader.GetInt16Value("ReconciliationCategoryTypeID");
                    oReconciliationCategoryMstInfo.ReconciliationCategoryTypeLabelID = reader.GetInt32Value("ReconciliationCategoryTypeLabelID");
                    oReconciliationCategoryMstInfo.RecItemControlID = reader.GetInt16Value("RecItemControlID");
                    oReconciliationCategoryMstInfoCollection.Add(oReconciliationCategoryMstInfo);
                    RecTemplateID = reader.GetInt32Value("ReconciliationTemplateID");
                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }
            return oReconciliationCategoryMstInfoCollection;
        }
        private IDbCommand GetAllReconciliationCategoryCommand(int? recPeriodID, long? AccountID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_ReconciliationCategoryByAccountID");
            oCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = oCommand.Parameters;
            IDbDataParameter paramAccountID = oCommand.CreateParameter();
            paramAccountID.ParameterName = "@AccountID";
            paramAccountID.Value = AccountID;
            paramCollection.Add(paramAccountID);

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            paramCollection.Add(paramRecPeriodID);

            return oCommand;
        }
    }
}