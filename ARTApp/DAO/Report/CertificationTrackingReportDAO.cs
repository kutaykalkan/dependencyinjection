


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
    public class CertificationTrackingReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CertificationTrackingReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<CertificationTrackingReportInfo> GetReportCertificationTrackingReport(ReportSearchCriteria oReportSearchCriteria,  DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            List<CertificationTrackingReportInfo> oCertificationTrackingReportInfoCollection = new List<CertificationTrackingReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportCertificationTrackingReportCommand(oReportSearchCriteria,  tblUserSearch,  tblRoleSearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                CertificationTrackingReportInfo oCertificationTrackingReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    oCertificationTrackingReportInfo = MapAccountObject(reader);
                    //oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oCertificationTrackingReportInfo);
                    oCertificationTrackingReportInfoCollection.Add(oCertificationTrackingReportInfo);
                    //this.MapAccountAttributeInfo(reader, oCertificationTrackingReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oCertificationTrackingReportInfoCollection;
        }

        private IDbCommand CreateGetReportCertificationTrackingReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_CertificationTrackingReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForUserRoleSearchInReport(tblUserSearch, tblRoleSearch, cmd, cmdParams);

            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@ReconciliationPeriodID";
            cmdRecPeriod.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ReconciliationPeriodID);
            cmdParams.Add(cmdRecPeriod);
            
            return cmd;
        }


        private CertificationTrackingReportInfo MapAccountObject(IDataReader r)
        {
            CertificationTrackingReportInfo entity = new CertificationTrackingReportInfo();

            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.RoleLabelID = r.GetInt32Value("RoleLabelID");
            entity.CountTotalAccountAssigned = r.GetInt16Value("CountTotalAccountAssigned");

            entity.MadatoryReportSignOffDate = r.GetDateValue("MadatoryReportSignOffDate");
            entity.CertificationBalancesDate = r.GetDateValue("CertificationBalancesDate");
            entity.ExceptionCertificationDate = r.GetDateValue("ExceptionCertificationDate");
            entity.AccountCertificationDate = r.GetDateValue("AccountCertificationDate");
            return entity;
        }




    }//end of class
}//end of namespace


