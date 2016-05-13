


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
    public class HolidayCalendarDAO : HolidayCalendarDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HolidayCalendarDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<HolidayCalendarInfo> GetHolidayCalendarByCompanyID(int companyID)
        {
            IDbConnection oConnection = null;
            IDbCommand oIDBCommand = null;
            List<HolidayCalendarInfo> oHolidayCalendarCollection = new List<HolidayCalendarInfo>();
            try
            {
                HolidayCalendarInfo oHolidayCalendar;
                oConnection = CreateConnection();
                oConnection.Open();
                oIDBCommand = this.CreateHolidayCalendarCompanyIDCommand(companyID);
                oIDBCommand.Connection = oConnection;
                IDataReader reader = oIDBCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oHolidayCalendar = (HolidayCalendarInfo)MapObject(reader);
                    oHolidayCalendarCollection.Add(oHolidayCalendar);
                }
                reader.Close();
                return oHolidayCalendarCollection;
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State != ConnectionState.Closed))
                {
                    oConnection.Dispose();
                }
            }

        }
        public int? InsertHolidayCalendarDataTable(DataTable dtHolidayCalendar
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertHolidayCalendarIDBCommand(dtHolidayCalendar, companyID);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            //IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }
        #region " Create Commands"
        protected IDbCommand CreateHolidayCalendarCompanyIDCommand(int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_SEL_HolidayCalendarByCompanyID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = oIDBCommand.Parameters;
            IDbDataParameter parCompanyID = oIDBCommand.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);
            return oIDBCommand;
        }

        protected IDbCommand InsertHolidayCalendarIDBCommand(DataTable dtHolidayCal, int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_HolidayCalendar");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamHolidayCalendarTable = oIDBCommand.CreateParameter();
            cmdParamHolidayCalendarTable.ParameterName = "@HolidayCalendarTable";
            cmdParamHolidayCalendarTable.Value = dtHolidayCal;
            cmdParams.Add(cmdParamHolidayCalendarTable);

            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@companyID";
            cmdParamCompanyID.Value = companyID;
            cmdParams.Add(cmdParamCompanyID);

            return oIDBCommand;
        }

        #endregion
    }
}