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
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.DAO.Report
{
    public class NewAccountReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NewAccountReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        internal List<NewAccountReportInfo> GetReportNewAccountReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            List<NewAccountReportInfo> oNewAccountReportInfoList = new List<NewAccountReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportNewAccountReportCommand(oReportSearchCriteria, tblEntitySearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                NewAccountReportInfo oNewAccountReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                // Accounts
                while (reader.Read())
                {
                    oNewAccountReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oNewAccountReportInfo);
                    oNewAccountReportInfoList.Add(oNewAccountReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oNewAccountReportInfoList;
        }

        private IDbCommand CreateGetReportNewAccountReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_NewAccountReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, false, cmd, cmdParams);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            IDbDataParameter cmdFromDateCreated = cmd.CreateParameter();
            cmdFromDateCreated.ParameterName = "@FromDateCreated";
            cmdFromDateCreated.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.FromDateCreated);
            cmdParams.Add(cmdFromDateCreated);

            IDbDataParameter cmdToDateCreated = cmd.CreateParameter();
            cmdToDateCreated.ParameterName = "@ToDateCreated";
            cmdToDateCreated.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ToDateCreated);
            cmdParams.Add(cmdToDateCreated);

            return cmd;
        }


        private NewAccountReportInfo MapAccountObject(IDataReader r)
        {
            NewAccountReportInfo entity = new NewAccountReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");

            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");
            entity.AccountTypeLabelID = r.GetInt32Value("AccountTypeLabelID");

            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            entity.FSCaptionLabelID = r.GetInt32Value("FSCaptionLabelID");
            entity.CreationPeriod = r.GetDateValue("CreationPeriod");
            entity.ProfileName = r.GetStringValue("ProfileName");
            entity.UploadedBy = r.GetStringValue("UploadedBy");
            entity.DateUploaded = r.GetDateValue("DateUploaded");
            return entity;
        }
    }//end of class
}//end of namespace


