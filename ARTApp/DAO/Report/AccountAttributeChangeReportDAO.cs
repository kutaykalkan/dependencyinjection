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
    public class AccountAttributeChangeReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountAttributeChangeReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        internal List<AccountAttributeChangeReportInfo> GetReportAccountAttributeChangeReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            List<AccountAttributeChangeReportInfo> oAccountAttributeChangeReportInfoList = new List<AccountAttributeChangeReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportAccountAttributeChangeReportCommand(oReportSearchCriteria, tblEntitySearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                AccountAttributeChangeReportInfo oAccountAttributeChangeReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                // Accounts
                while (reader.Read())
                {
                    oAccountAttributeChangeReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountAttributeChangeReportInfo);
                    oAccountAttributeChangeReportInfoList.Add(oAccountAttributeChangeReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oAccountAttributeChangeReportInfoList;
        }

        private IDbCommand CreateGetReportAccountAttributeChangeReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_AccountAttributeChangeReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, false, cmd, cmdParams);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            IDbDataParameter cmdFromChangeDate = cmd.CreateParameter();
            cmdFromChangeDate.ParameterName = "@FromChangeDate";
            cmdFromChangeDate.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.FromChangeDate);
            cmdParams.Add(cmdFromChangeDate);

            IDbDataParameter cmdToChangeDate = cmd.CreateParameter();
            cmdToChangeDate.ParameterName = "@ToChangeDate";
            cmdToChangeDate.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ToChangeDate);
            cmdParams.Add(cmdToChangeDate);

            return cmd;
        }


        private AccountAttributeChangeReportInfo MapAccountObject(IDataReader r)
        {
            AccountAttributeChangeReportInfo entity = new AccountAttributeChangeReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");

            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");
            entity.AccountTypeLabelID = r.GetInt32Value("AccountTypeLabelID");

            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            entity.FSCaptionLabelID = r.GetInt32Value("FSCaptionLabelID");

            entity.AccountAttributeID = r.GetInt16Value("AccountAttributeID");
            entity.AccountAttribute = r.GetStringValue("AccountAttribute");
            entity.AccountAttributeLabelID = r.GetInt32Value("AccountAttributeLabelID");
            entity.Value = r.GetStringValue("Value");
            entity.ValueLabelID = r.GetInt32Value("ValueLabelID");
            entity.ValidFrom = r.GetDateValue("ValidFrom");
            entity.ValidUntil = r.GetDateValue("ValidUntil");
            entity.ChangeDate = r.GetDateValue("ChangeDate");
            entity.ChangePeriod = r.GetDateValue("ChangePeriod");
            entity.UpdatedBy = r.GetStringValue("UpdatedBy");

            return entity;
        }
    }//end of class
}//end of namespace


