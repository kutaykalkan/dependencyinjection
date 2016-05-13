using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class AlertMstDAO : AlertMstDAOBase
    {
         /// <summary>
        /// Constructor
        /// </summary>
        public AlertMstDAO(AppUserInfo oAppUserInfo) :
            base( oAppUserInfo)
        {           
        }

        #region SelectAllAlertMstInfo

        public List<AlertMstInfo> SelectAllAlertMstInfo(int languageID, int businessEntityID, int defaultLanguageID, int? compnayID, int? recPeriodId)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            List<AlertMstInfo> oAlertMstInfoCollection = new List<AlertMstInfo>();

            try
            {
                cmd = this.CreateCommand("usp_SEL_AllAlertMst");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdPrams = cmd.Parameters;
                ServiceHelper.AddCommonLanguageParameters(cmd, cmdPrams, languageID, businessEntityID, defaultLanguageID);

                IDbDataParameter parCompanyID = cmd.CreateParameter();
                parCompanyID.ParameterName = "@CompanyID";
                if (compnayID != null)
                    parCompanyID.Value = compnayID;
                else
                    parCompanyID.Value = System.DBNull.Value;
                cmdPrams.Add(parCompanyID);

                IDbDataParameter parRecPeriodID = cmd.CreateParameter();
                parRecPeriodID.ParameterName = "@RecPeriodID";
                if (recPeriodId != null)
                    parRecPeriodID.Value = recPeriodId;
                else
                    parRecPeriodID.Value = System.DBNull.Value;
                cmdPrams.Add(parRecPeriodID);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    AlertMstInfo oAlertMstInfo = this.MapObject(reader);
                    oAlertMstInfo.AlertType = reader.GetStringValue("AlertType");
                    oAlertMstInfo.ThresholdType = reader.GetStringValue("ThresholdType");
                    oAlertMstInfoCollection.Add(oAlertMstInfo);
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oAlertMstInfoCollection;
        }

        #endregion

        #region GetAlertDescriptionAndReplacementString

        public int? GetAlertDescriptionAndReplacementString(short alertID, int recPeriodID, DataTable dtAccountID, out string replacement)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            int? alertLabelID = null;
            replacement = null;
            try
            {
                cmd = this.CreateCommand("usp_GET_AlertDescriptionAndReplacementString");
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramAlertID = cmd.CreateParameter();
                paramAlertID.ParameterName = "@AlertID";
                paramAlertID.Value = alertID;
                cmdParams.Add(paramAlertID);

                IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = recPeriodID;
                cmdParams.Add(paramRecPeriodID);

                IDbDataParameter paramAccountIDTable = cmd.CreateParameter();
                paramAccountIDTable.ParameterName = "@AccountIDTable";
                paramAccountIDTable.Value = dtAccountID;
                cmdParams.Add(paramAccountIDTable);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    alertLabelID = reader.GetInt32Value("AlertLabelID");
                    replacement = reader.GetStringValue("Replacement");
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return alertLabelID;
        }

        #endregion
    }
}