


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class UserMyReportDAO : UserMyReportDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserMyReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public int NoOfSavedMyReportByReportID(short? reportID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            int noOfSavedMyReport = 0;
            try
            {
                cmd = NoOfSavedMyReportByReportIDCommand(reportID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                noOfSavedMyReport = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            finally
            {

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }

            return noOfSavedMyReport;

        }





        private IDbCommand NoOfSavedMyReportByReportIDCommand(short? reportID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_NoOfSavedMyReportByReportID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            parReportID.Value = reportID.Value;
            cmdParams.Add(parReportID);
            return cmd;
        }



    }
}