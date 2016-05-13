


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
    public class DelinquentAccountByUserReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DelinquentAccountByUserReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<DelinquentAccountByUserReportInfo> GetReportDelinquentAccountByUserReport(ReportSearchCriteria oReportSearchCriteria,  DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            List<DelinquentAccountByUserReportInfo> oDelinquentAccountByUserReportInfoCollection = new List<DelinquentAccountByUserReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportDelinquentAccountByUserReportCommand(oReportSearchCriteria,  tblUserSearch,  tblRoleSearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                DelinquentAccountByUserReportInfo oDelinquentAccountByUserReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    oDelinquentAccountByUserReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oDelinquentAccountByUserReportInfo);
                    oDelinquentAccountByUserReportInfoCollection.Add(oDelinquentAccountByUserReportInfo);
                    //this.MapAccountAttributeInfo(reader, oDelinquentAccountByUserReportInfo);
                } 
                //while (reader.NextResult())
                //{
                //    reader.ClearColumnHash();
                //    while (reader.Read())
                //    {
                //        oDelinquentAccountByUserReportInfo = MapAccountObject(reader);
                //        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oDelinquentAccountByUserReportInfo);
                //        oDelinquentAccountByUserReportInfoCollection.Add(oDelinquentAccountByUserReportInfo);
                //        //this.MapAccountAttributeInfo(reader, oDelinquentAccountByUserReportInfo);
                //    }
                //}
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oDelinquentAccountByUserReportInfoCollection;
        }

        private IDbCommand CreateGetReportDelinquentAccountByUserReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch)
        {
            //IDbCommand cmd = this.CreateCommand("usp_RPT_DelinquentAccountByUserReport");
            //IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_DelinquentAccountByUserReport");
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_DelinquentAccountByUserReport_New");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, false, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForUserRoleSearchInReport(tblUserSearch, tblRoleSearch, cmd, cmdParams);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            return cmd;
        }


        private DelinquentAccountByUserReportInfo MapAccountObject(IDataReader r)
        {
            DelinquentAccountByUserReportInfo entity = new DelinquentAccountByUserReportInfo();

            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.RoleLabelID = r.GetInt32Value("RoleLabelID");



            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            //entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            //entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");
            entity.GLBalanceReportingCurrency = r.GetDecimalValue("GLBalanceReportingCurrency");
            entity.GLBalanceBaseCurrency = r.GetDecimalValue("GLBalanceBaseCurrency");

            entity.DueDate = r.GetDateValue ("DueDate");
            entity.DaysLate = r.GetInt32Value("DaysLate");
            entity.CountDelinquentAccount = r.GetInt32Value("CountDelinquentAccount");

            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");
            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            return entity;
        }




    }//end of class
}//end of namespace


