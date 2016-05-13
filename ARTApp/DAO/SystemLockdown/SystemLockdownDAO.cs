

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Transactions;

namespace SkyStem.ART.App.DAO
{
    public class SystemLockdownDAO : SystemLockdownDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SystemLockdownDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in SystemLockdownInfo object
        /// </summary>
        /// <param name="o">A SystemLockdownInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(SystemLockdownInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_SystemLockdown");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (entity != null && entity.AccountID.GetValueOrDefault() > 0)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);
            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);
            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity != null && entity.CompanyID.GetValueOrDefault() > 0)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);
            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (entity != null && entity.DataImportID.GetValueOrDefault() > 0)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);
            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null && entity.DateAdded.HasValue)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);
            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (entity != null && entity.RecPeriodID.GetValueOrDefault() > 0)
                parRecPeriodID.Value = entity.RecPeriodID;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);
            System.Data.IDbDataParameter parSystemLockdownMessage = cmd.CreateParameter();
            parSystemLockdownMessage.ParameterName = "@SystemLockdownMessage";
            if (entity != null)
                parSystemLockdownMessage.Value = entity.SystemLockdownMessage;
            else
                parSystemLockdownMessage.Value = System.DBNull.Value;
            cmdParams.Add(parSystemLockdownMessage);
            System.Data.IDbDataParameter parSystemLockdownReasonID = cmd.CreateParameter();
            parSystemLockdownReasonID.ParameterName = "@SystemLockdownReasonID";
            if (entity != null && entity.SystemLockdownReasonID.GetValueOrDefault() > 0)
                parSystemLockdownReasonID.Value = entity.SystemLockdownReasonID;
            else
                parSystemLockdownReasonID.Value = System.DBNull.Value;
            cmdParams.Add(parSystemLockdownReasonID);
            return cmd;
        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in SystemLockdownInfo object
        /// </summary>
        /// <param name="o">A SystemLockdownInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(SystemLockdownInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_SystemLockdown");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (entity != null && entity.AccountID.GetValueOrDefault() > 0)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);
            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);
            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity != null && entity.CompanyID.GetValueOrDefault() > 0)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);
            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (entity != null && entity.DataImportID.GetValueOrDefault() > 0)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);
            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null && entity.DateAdded.HasValue)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);
            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (entity != null && entity.RecPeriodID.GetValueOrDefault() > 0)
                parRecPeriodID.Value = entity.RecPeriodID;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);
            System.Data.IDbDataParameter parSystemLockdownMessage = cmd.CreateParameter();
            parSystemLockdownMessage.ParameterName = "@SystemLockdownMessage";
            if (entity != null)
                parSystemLockdownMessage.Value = entity.SystemLockdownMessage;
            else
                parSystemLockdownMessage.Value = System.DBNull.Value;
            cmdParams.Add(parSystemLockdownMessage);
            System.Data.IDbDataParameter parSystemLockdownReasonID = cmd.CreateParameter();
            parSystemLockdownReasonID.ParameterName = "@SystemLockdownReasonID";
            if (entity != null && entity.SystemLockdownReasonID.GetValueOrDefault() > 0)
                parSystemLockdownReasonID.Value = entity.SystemLockdownReasonID;
            else
                parSystemLockdownReasonID.Value = System.DBNull.Value;
            cmdParams.Add(parSystemLockdownReasonID);
            System.Data.IDbDataParameter pkparSystemLockdownID = cmd.CreateParameter();
            pkparSystemLockdownID.ParameterName = "@SystemLockdownID";
            pkparSystemLockdownID.Value = entity.SystemLockdownID;
            cmdParams.Add(pkparSystemLockdownID);
            return cmd;
        }

        internal SystemLockdownInfo GetSystemLockdownStautsAndHandleTimeout(int? companyID, int timeOutMinutes)
        {
            SystemLockdownInfo oSystemLockdownInfo = null;
            using (TransactionScope oTransactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (IDbConnection cnn = this.CreateConnection())
                {
                    cnn.Open();
                    using (IDbCommand oCommand = this.GetRemoveSystemLockdownStautsOnTimeoutCommand(companyID, timeOutMinutes))
                    {
                        oCommand.Connection = cnn;
                        oCommand.ExecuteNonQuery();
                    }

                    using (IDbCommand oCmd = GetSystemLockdownByCompanyIDCommand(companyID))
                    {
                        oCmd.Connection = cnn;
                        IDataReader dr = oCmd.ExecuteReader();
                        if(dr.Read())
                        {
                            oSystemLockdownInfo = MapObject(dr);
                        }
                    }
                }
                oTransactionScope.Complete();
            }
            return oSystemLockdownInfo;
        }

        private IDbCommand GetRemoveSystemLockdownStautsOnTimeoutCommand(int? companyID, int timeOutMinutes)
        {
            IDbCommand oCommand = this.CreateCommand("usp_DEL_SystemLockdownOnTimeout");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter parCompannyID = oCommand.CreateParameter();
            parCompannyID.ParameterName = "@CompanyID";
            parCompannyID.Value = companyID;
            cmdParams.Add(parCompannyID);

            IDbDataParameter parTimeOutMinutes = oCommand.CreateParameter();
            parTimeOutMinutes.ParameterName = "@TimeOutMinutes";
            parTimeOutMinutes.Value = timeOutMinutes;
            cmdParams.Add(parTimeOutMinutes);

            return oCommand;
        }

        public IDbCommand GetSystemLockdownByCompanyIDCommand(int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_SystemLockdownByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = CompanyID.Value;
            cmdParams.Add(par);
            return cmd;
        }
    }
}