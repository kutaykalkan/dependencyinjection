


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class CurrencyCodeDAO : CurrencyCodeDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyCodeDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public int? InsertCurrencyCodeDataTable(DataTable dtCurrencyCode
          , IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertCurrencyCodeIDBCommand(dtCurrencyCode);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            //IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }

        protected IDbCommand InsertCurrencyCodeIDBCommand(DataTable dtCurrencyCode)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_CurrencyCode");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamdtExchangeRateTable = oIDBCommand.CreateParameter();
            cmdParamdtExchangeRateTable.ParameterName = "@CurrencyCodeTable";
            cmdParamdtExchangeRateTable.Value = dtCurrencyCode;
            cmdParams.Add(cmdParamdtExchangeRateTable);
            return oIDBCommand;
        }

    }
}