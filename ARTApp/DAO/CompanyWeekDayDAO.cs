


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class CompanyWeekDayDAO : CompanyWeekDayDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyWeekDayDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public void SaveCompanyWorkWeek(DataTable dtCompanyWorkWeek, int? companyID, int? startRecPeriodID, int? endRecPeriodID, string addedBy, DateTime? dateAdded, string revisedBy, DateTime? dateRevised, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);

            cmd = this.CreateSaveCompanyWorkWeekCommand(dtCompanyWorkWeek, companyID, startRecPeriodID, endRecPeriodID, addedBy, dateAdded, revisedBy, dateRevised);
            cmd.Transaction = oTransaction;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateSaveCompanyWorkWeekCommand(DataTable dtCompanyWorkWeek, int? companyID, int? startRecPeriodID, int? endRecPeriodID, string addedBy, DateTime? dateAdded, string revisedBy, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_CompanyWorkWeek");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTBLCompanyWorkWeek = cmd.CreateParameter();
            parTBLCompanyWorkWeek.ParameterName = "@TBLCompanyWorkWeek";
            parTBLCompanyWorkWeek.Value = dtCompanyWorkWeek;
            cmdParams.Add(parTBLCompanyWorkWeek);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parStartRecPeriodID = cmd.CreateParameter();
            parStartRecPeriodID.ParameterName = "@StartRecPeriodID";
            parStartRecPeriodID.Value = startRecPeriodID.Value;
            cmdParams.Add(parStartRecPeriodID);

            System.Data.IDbDataParameter parEndRecPeriodID = cmd.CreateParameter();
            parEndRecPeriodID.ParameterName = "@EndRecPeriodID";
            parEndRecPeriodID.Value = startRecPeriodID.Value;
            cmdParams.Add(parEndRecPeriodID);


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = addedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = dateAdded.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised.Value;
            cmdParams.Add(parDateRevised);

            return cmd;
        }

        public bool AdjustDueDates(int? companyID, int? recPeriodID, IDbConnection con, IDbTransaction oTransaction)
        {
            bool isAdjustDueDate = false;
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);
            cmd = this.CreateAdjustDueDateCommand(companyID, recPeriodID);
            cmd.Transaction = oTransaction;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            return isAdjustDueDate;
        }

        private IDbCommand CreateAdjustDueDateCommand(int? companyID, int? recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_AdjustDueDatesByWorkWeek");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID.Value;
            cmdParams.Add(parCompanyID);
            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID.Value;
            cmdParams.Add(parRecPeriodID);
            return cmd;
        }
    }
}