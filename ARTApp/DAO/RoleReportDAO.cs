using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Params;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class RoleReportDAO : RoleReportDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public IList<RoleReportInfo_ExtendedWithReportName> GetAllRoleReportByRoleID(ReportParamInfo oReportParamInfo)
        {
            IList<RoleReportInfo_ExtendedWithReportName> oRoleReportInfoCollection = new List<RoleReportInfo_ExtendedWithReportName>();
            RoleReportInfo_ExtendedWithReportName oRoleReportInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                oCommand = CreateSelectAllRoleReportByRoleIDCommand(oReportParamInfo);
                oCommand.Connection = oConnection;
                IDataReader oDataReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDataReader.Read())
                {
                    oRoleReportInfo = (RoleReportInfo_ExtendedWithReportName)MapObjectRoleReport(oDataReader);
                    oRoleReportInfoCollection.Add(oRoleReportInfo);
                }
                oDataReader.Close();
            }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oRoleReportInfoCollection;
        }

        protected System.Data.IDbCommand CreateSelectAllRoleReportByRoleIDCommand(ReportParamInfo oReportParamInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RoleReportByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonRoleAndRecPeriodParameters(oReportParamInfo, cmd);

            System.Data.IDbDataParameter parCompanyId = cmd.CreateParameter();
            parCompanyId.ParameterName = "@CompanyID";
            parCompanyId.Value = oReportParamInfo.CompanyID;
            cmdParams.Add(parCompanyId);

            return cmd;
        }


        protected RoleReportInfo_ExtendedWithReportName MapObjectRoleReport(System.Data.IDataReader r)
        {
            RoleReportInfo_ExtendedWithReportName entity = new RoleReportInfo_ExtendedWithReportName();
            try
            {
                int ordinal = r.GetOrdinal("RoleReportID");
                if (!r.IsDBNull(ordinal)) entity.RoleReportID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportID");
                if (!r.IsDBNull(ordinal)) entity.ReportID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Report");
                if (!r.IsDBNull(ordinal)) entity.Report = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReportLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }


    }//end of class
}//end of namespace