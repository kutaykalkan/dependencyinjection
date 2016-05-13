


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class DataImportFailureMessageDAO : DataImportFailureMessageDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataImportFailureMessageDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public int? InsertDataImportFailureMsg(int dataImportID, string dataImportFailureMsg
            , DateTime dateAdded, string addedBy, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.CreateDataImportFailureMsgByDataImportIDCommand(dataImportID, dataImportFailureMsg
                , dateAdded, addedBy);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }


        #region " Create Commands"
        protected IDbCommand CreateDataImportFailureMsgByDataImportIDCommand(int dataImportID
            , string failureMsg, DateTime dateAdded, string addedBy)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_DataImportFailureMsgByDataImportID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter parDataImportID = oIDBCommand.CreateParameter();
            parDataImportID.ParameterName = "@dataImportID";
            parDataImportID.Value = dataImportID;

            IDbDataParameter parFailureMessage = oIDBCommand.CreateParameter();
            parFailureMessage.ParameterName = "@message";
            parFailureMessage.Value = failureMsg;

            IDbDataParameter parDateAdded = oIDBCommand.CreateParameter();
            parDateAdded.ParameterName = "@dateAdded";
            parDateAdded.Value = dateAdded;

            IDbDataParameter parAddedBy = oIDBCommand.CreateParameter();
            parAddedBy.ParameterName = "@addedBy";
            parAddedBy.Value = addedBy;

            cmdParams.Add(parDataImportID);
            cmdParams.Add(parFailureMessage);
            cmdParams.Add(parDateAdded);
            cmdParams.Add(parAddedBy);

            return oIDBCommand;
        }
        #endregion
    }
}