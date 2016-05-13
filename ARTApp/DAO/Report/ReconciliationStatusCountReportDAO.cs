


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.DAO.Report
{
    public class ReconciliationStatusCountReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationStatusCountReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<ReconciliationStatusCountReportInfo> GetReportReconciliationStatusCountReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            List<ReconciliationStatusCountReportInfo> oReconciliationStatusCountReportInfoCollection = new List<ReconciliationStatusCountReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportReconciliationStatusCountReportCommand(oReportSearchCriteria, tblUserSearch, tblRoleSearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ReconciliationStatusCountReportInfo oReconciliationStatusCountReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    oReconciliationStatusCountReportInfo = MapAccountObject(reader);
                    //oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oReconciliationStatusCountReportInfo);
                    oReconciliationStatusCountReportInfoCollection.Add(oReconciliationStatusCountReportInfo);
                    //this.MapAccountAttributeInfo(reader, oReconciliationStatusCountReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oReconciliationStatusCountReportInfoCollection;
        }

        private IDbCommand CreateGetReportReconciliationStatusCountReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_ReconciliationStatusCountReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForUserRoleSearchInReport(tblUserSearch, tblRoleSearch, cmd, cmdParams);

            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@ReconciliationPeriodID";
            cmdRecPeriod.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ReconciliationPeriodID);
            cmdParams.Add(cmdRecPeriod);


            IDbDataParameter cmdRequesterUserID = cmd.CreateParameter();
            cmdRequesterUserID.ParameterName = "@RequesterUserID";
            cmdRequesterUserID.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.RequesterUserID);
            cmdParams.Add(cmdRequesterUserID);

            IDbDataParameter cmdRequesterRoleID = cmd.CreateParameter();
            cmdRequesterRoleID.ParameterName = "@RequesterRoleID";
            cmdRequesterRoleID.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.RequesterRoleID);
            cmdParams.Add(cmdRequesterRoleID);


            
            return cmd;
        }



        private ReconciliationStatusCountReportInfo MapAccountObject(IDataReader r)
        {
            ReconciliationStatusCountReportInfo entity = new ReconciliationStatusCountReportInfo();

            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.RoleLabelID = r.GetInt32Value("RoleLabelID");
            entity.CountTotalAccountAssigned = r.GetInt16Value("CountTotalAccountAssigned");

            entity.CountApproved = r.GetInt16Value("CountApproved");
            entity.CountInProgress = r.GetInt16Value("CountInProgress");
            entity.CountNotStarted = r.GetInt16Value("CountNotStarted");
            entity.CountPendingApproval = r.GetInt16Value("CountPendingApproval");
            entity.CountPendingModificationPreparer = r.GetInt16Value("CountPendingModificationPreparer");
            entity.CountPendingModificationReviewer = r.GetInt16Value("CountPendingModificationReviewer");
            entity.CountPendingReview = r.GetInt16Value("CountPendingReview");
            entity.CountPrepared = r.GetInt16Value("CountPrepared");
            entity.CountReconciled = r.GetInt16Value("CountReconciled");
            entity.CountReviewed = r.GetInt16Value("CountReviewed");
            entity.CountSysReconciled = r.GetInt16Value("CountSysReconciled");
            return entity;
        }




    }//end of class
}//end of namespace


