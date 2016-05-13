


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class ExchangeRateDAO : ExchangeRateDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public ExchangeRateDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public int? InsertInsertExchangeRateDataTable(int dataImportID, DataTable dtExchangeRate
          , IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertExchangeRateIDBCommand(dataImportID, dtExchangeRate);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            //IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }

        protected IDbCommand InsertExchangeRateIDBCommand(int dataImportID, DataTable dtExchangeRate)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_ExchangeRate");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamdDataImportID = oIDBCommand.CreateParameter();
            cmdParamdDataImportID.ParameterName = "@dataImportID";
            cmdParamdDataImportID.Value = dataImportID;
            cmdParams.Add(cmdParamdDataImportID);

            IDbDataParameter cmdParamdtExchangeRateTable = oIDBCommand.CreateParameter();
            cmdParamdtExchangeRateTable.ParameterName = "@CurrencyExchangeTable";
            cmdParamdtExchangeRateTable.Value = dtExchangeRate;
            cmdParams.Add(cmdParamdtExchangeRateTable);
            return oIDBCommand;
        }

        #region GetCurrencyExchangeRate

        public decimal GetCurrencyExchangeRate(int recPeriodID, string fromCurrencyCode, string toCurrencyCode, bool isMultiCurrencyEnabled)
        {
            decimal exchangeRate = 1;
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetCurrencyExchangeRateCommand(recPeriodID, fromCurrencyCode, toCurrencyCode, isMultiCurrencyEnabled);
                cmd.Connection = con;

                con.Open();

                exchangeRate = (decimal)cmd.ExecuteScalar();
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return exchangeRate;
        }

        private IDbCommand CreateGetCurrencyExchangeRateCommand(int recPeriodID, string fromCurrencyCode, string toCurrencyCode, bool isMultiCurrencyEnabled)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_CurrencyExchangeRate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramRecPeriodId = cmd.CreateParameter();
            paramRecPeriodId.ParameterName = "@RecPeriodID";
            paramRecPeriodId.Value = recPeriodID;
            paramCollection.Add(paramRecPeriodId);

            IDbDataParameter paramFromCurrencyCode = cmd.CreateParameter();
            paramFromCurrencyCode.ParameterName = "@FromCurrencyCode";
            paramFromCurrencyCode.Value = fromCurrencyCode;
            paramCollection.Add(paramFromCurrencyCode);

            IDbDataParameter paramToCurrecyCode = cmd.CreateParameter();
            paramToCurrecyCode.ParameterName = "@ToCurrecyCode";
            paramToCurrecyCode.Value = toCurrencyCode;
            paramCollection.Add(paramToCurrecyCode);

            IDbDataParameter paramIsMultiCurrencyEnabled = cmd.CreateParameter();
            paramIsMultiCurrencyEnabled.ParameterName = "@IsMultiCurrencyEnabled";
            paramIsMultiCurrencyEnabled.Value = isMultiCurrencyEnabled;
            paramCollection.Add(paramIsMultiCurrencyEnabled);

            return cmd;
        }


        public List<ExchangeRateInfo> GetCurrencyExchangeRateByRecPeriod(int recPeriodID)
        {
            List<ExchangeRateInfo> oExchangeRateInfoCollection = new List<ExchangeRateInfo>();

            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetCurrencyExchangeRateByRecPeriodCommand(recPeriodID);
                cmd.Connection = con;
                con.Open();
                ExchangeRateInfo oExchangeRateInfo = null;

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oExchangeRateInfo = MapExchangeRateObject(reader);
                    oExchangeRateInfoCollection.Add(oExchangeRateInfo);

                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oExchangeRateInfoCollection;
        }

        private IDbCommand CreateGetCurrencyExchangeRateByRecPeriodCommand(int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_CurrencyExchangeRateByRecPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramRecPeriodId = cmd.CreateParameter();
            paramRecPeriodId.ParameterName = "@RecPeriodID";
            paramRecPeriodId.Value = recPeriodID;
            paramCollection.Add(paramRecPeriodId);

            return cmd;
        }

        public List<ExchangeRateInfo> GetCurrencyExchangeRateArchieveByExchangeRateID(int exchangeRateID)
        {
            List<ExchangeRateInfo> oExchangeRateInfoList = new List<ExchangeRateInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                con.Open();
                using (IDbCommand cmd = this.CreateGetCurrencyExchangeRateArchieveByExchangeRateIDCommand(exchangeRateID))
                {
                    cmd.Connection = con;
                    ExchangeRateInfo oExchangeRateInfo = null;
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        oExchangeRateInfo = MapExchangeRateObject(reader);
                        oExchangeRateInfoList.Add(oExchangeRateInfo);

                    }
                }
            }
            return oExchangeRateInfoList;
        }

    private IDbCommand CreateGetCurrencyExchangeRateArchieveByExchangeRateIDCommand(int exchangeRateID)
    {
        IDbCommand cmd = this.CreateCommand("usp_SEL_CurrencyExchangeRateArchieveByExchangeRateID");
        cmd.CommandType = CommandType.StoredProcedure;

        IDataParameterCollection paramCollection = cmd.Parameters;

        IDbDataParameter paramExchangeRateID = cmd.CreateParameter();
        paramExchangeRateID.ParameterName = "@ExchangeRateID";
        paramExchangeRateID.Value = exchangeRateID;
        paramCollection.Add(paramExchangeRateID);

        return cmd;
    }

    private ExchangeRateInfo MapExchangeRateObject(IDataReader r)
    {
        ExchangeRateInfo entity = new ExchangeRateInfo();

        entity.ExchangeRateID = r.GetInt32Value("ExchangeRateID");
        entity.FromCurrencyCode = r.GetStringValue("FromCurrencyCode");
        entity.ToCurrencyCode = r.GetStringValue("ToCurrencyCode");
        entity.ExchangeRate = r.GetDecimalValue("ExchangeRate");
        entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
        entity.IsActive = r.GetBooleanValue("IsActive");
        entity.DateAdded = r.GetDateValue("DateAdded");
        entity.AddedBy = r.GetStringValue("AddedBy");
        entity.DateRevised = r.GetDateValue("DateRevised");
        entity.RevisedBy = r.GetStringValue("RevisedBy");
        return entity;
    }




    #endregion

    #region SelectLocalCurrency

    public List<string> SelectLocalCurrency(int recPeriodID, bool isMultiCurrencyEnabled)
    {
        List<string> localCurrencyCollection = new List<string>();
        IDbCommand cmd = null;
        IDbConnection con = null;
        try
        {
            con = this.CreateConnection();
            cmd = this.CreateSelectLocalCurrency(recPeriodID, isMultiCurrencyEnabled);
            cmd.Connection = con;
            con.Open();

            IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                localCurrencyCollection.Add(reader.GetStringValue("FromCurrencyCode"));
            }
        }
        finally
        {
            if (con != null && con.State != ConnectionState.Closed)
                con.Dispose();
        }

        return localCurrencyCollection;
    }

    public List<string> SelectLocalCurrencyByAccountID(long gldataID, bool isMultiCurrencyEnabled)
    {
        List<string> localCurrencyCollection = new List<string>();
        IDbCommand cmd = null;
        IDbConnection con = null;
        IDataReader reader = null;
        try
        {
            con = this.CreateConnection();
            cmd = this.CreateSelectLocalCurrencyByAccountID(gldataID, isMultiCurrencyEnabled);
            cmd.Connection = con;
            con.Open();

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                localCurrencyCollection.Add(reader.GetStringValue("FromCurrencyCode"));
            }
            reader.Close();
        }
        finally
        {
            if (reader != null && !reader.IsClosed)
                reader.Close();
            if (con != null && con.State != ConnectionState.Closed)
                con.Dispose();
        }

        return localCurrencyCollection;
    }

    private IDbCommand CreateSelectLocalCurrency(int recPeriodID, bool isMultiCurrencyEnabled)
    {
        IDbCommand cmd = this.CreateCommand("usp_SEL_LocalCurrencyWithExchangeRateAvailableForBaseAndReportingCurrency");
        cmd.CommandType = CommandType.StoredProcedure;

        IDataParameterCollection paramCollection = cmd.Parameters;

        IDbDataParameter paramRecPeriodId = cmd.CreateParameter();
        paramRecPeriodId.ParameterName = "@RecPeriodID";
        paramRecPeriodId.Value = recPeriodID;
        paramCollection.Add(paramRecPeriodId);


        IDbDataParameter paramIsMultiCurrencyEnabled = cmd.CreateParameter();
        paramIsMultiCurrencyEnabled.ParameterName = "@IsMultiCurrencyEnabled";
        paramIsMultiCurrencyEnabled.Value = isMultiCurrencyEnabled;
        paramCollection.Add(paramIsMultiCurrencyEnabled);

        return cmd;
    }
    private IDbCommand CreateSelectLocalCurrencyByAccountID(long gldataID, bool isMultiCurrencyEnabled)
    {
        IDbCommand cmd = this.CreateCommand("usp_SEL_LocalCurrencyWithExchangeRateAvailableByAccountID");
        cmd.CommandType = CommandType.StoredProcedure;

        IDataParameterCollection paramCollection = cmd.Parameters;


        IDbDataParameter paramGLDataId = cmd.CreateParameter();
        paramGLDataId.ParameterName = "@GLDataID";
        paramGLDataId.Value = gldataID;
        paramCollection.Add(paramGLDataId);

        IDbDataParameter paramIsMultiCurrencyEnabled = cmd.CreateParameter();
        paramIsMultiCurrencyEnabled.ParameterName = "@IsMultiCurrencyEnabled";
        paramIsMultiCurrencyEnabled.Value = isMultiCurrencyEnabled;
        paramCollection.Add(paramIsMultiCurrencyEnabled);




        return cmd;
    }
    #endregion
}
}