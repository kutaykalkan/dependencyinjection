using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class FinancialYearHdrDAO : FinancialYearHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FinancialYearHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public bool InsertNewFinancialYear(FinancialYearHdrInfo oFinancialYearHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            try
            {
                FinancialYearHdrDAO oFinancialYearHdrDAO = new FinancialYearHdrDAO(this.CurrentAppUserInfo);
                int FinancialYearID = oFinancialYearHdrDAO.SaveNewFinancialYearHdrInfo(oFinancialYearHdrInfo, oConnection, oTransaction);
                oFinancialYearHdrInfo.FinancialYearID = FinancialYearID;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public int SaveNewFinancialYearHdrInfo(FinancialYearHdrInfo oFinancialYearHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int NewFinancialYear = -1;
            System.Data.IDbCommand cmd = null;
            cmd = GetNewFinancialYearInsertCommand(oFinancialYearHdrInfo);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            NewFinancialYear = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            return NewFinancialYear;
        }


        private System.Data.IDbCommand GetNewFinancialYearInsertCommand(FinancialYearHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_FinancialYearHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value;
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
            parEndDate.ParameterName = "@EndDate";
            if (!entity.IsEndDateNull)
                parEndDate.Value = Convert.ToDateTime(entity.EndDate).ToString("MM/dd/yyyy");


            else
                parEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parEndDate);

            System.Data.IDbDataParameter parFinancialYear = cmd.CreateParameter();
            parFinancialYear.ParameterName = "@FinancialYear";
            if (!entity.IsFinancialYearNull)
                parFinancialYear.Value = entity.FinancialYear;
            else
                parFinancialYear.Value = System.DBNull.Value;
            cmdParams.Add(parFinancialYear);

            System.Data.IDbDataParameter parStartDate = cmd.CreateParameter();
            parStartDate.ParameterName = "@StartDate";
            if (!entity.IsStartDateNull)
                parStartDate.Value = Convert.ToDateTime(entity.StartDate).ToString("MM/dd/yyyy");
            else
                parStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parStartDate);

            return cmd;

        }



        public bool UpdateFinancialYear(FinancialYearHdrInfo objFinancialYearHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            try
            {
                FinancialYearHdrDAO oFinancialYearHdrDAO = new FinancialYearHdrDAO(this.CurrentAppUserInfo);
                oFinancialYearHdrDAO.UpdateFinancialYearHdrInfo(objFinancialYearHdrInfo, oConnection, oTransaction);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public void UpdateFinancialYearHdrInfo(FinancialYearHdrInfo oFinancialYearHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            System.Data.IDbCommand cmd = null;
            cmd = GetUpdateCommand(oFinancialYearHdrInfo);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }

        private System.Data.IDbCommand GetUpdateCommand(FinancialYearHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_FinancialYearHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised;
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
            parEndDate.ParameterName = "@EndDate";
            if (!entity.IsEndDateNull)
                parEndDate.Value = Convert.ToDateTime(entity.EndDate).ToString("MM/dd/yyyy");
            else
                parEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parEndDate);

            System.Data.IDbDataParameter parFinancialYear = cmd.CreateParameter();
            parFinancialYear.ParameterName = "@FinancialYear";
            if (!entity.IsFinancialYearNull)
                parFinancialYear.Value = entity.FinancialYear;
            else
                parFinancialYear.Value = System.DBNull.Value;
            cmdParams.Add(parFinancialYear);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parStartDate = cmd.CreateParameter();
            parStartDate.ParameterName = "@StartDate";
            if (!entity.IsStartDateNull)
                parStartDate.Value = Convert.ToDateTime(entity.StartDate).ToString("MM/dd/yyyy");
            else
                parStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parStartDate);

            System.Data.IDbDataParameter pkparFinancialYearID = cmd.CreateParameter();
            pkparFinancialYearID.ParameterName = "@FinancialYearID";
            pkparFinancialYearID.Value = entity.FinancialYearID;
            cmdParams.Add(pkparFinancialYearID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);


            return cmd;

        }

        public IList<FinancialYearHdrInfo> SelectAllFinancialYearByCompanyID(int? CompanyID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllFinancialYear");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = CompanyID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }
        public IList<FinancialYearHdrInfo> SelectFinancialYearByID(int? FinancialYearID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_FinancialYearByID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FinancialYearID";
            if (FinancialYearID == null)
            {
                par.Value = (object)DBNull.Value;
            }
            else
            {
                par.Value = FinancialYearID;
            }
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        public bool IsExclusiveDateRange(FinancialYearHdrInfo objFinancialYearHdrInfo)
        {


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateIsExclusiveDateRangeCommand(objFinancialYearHdrInfo);
                cmd.Connection = con;
                con.Open();
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }


        }
        protected IDbCommand CreateIsExclusiveDateRangeCommand(FinancialYearHdrInfo objFinancialYearHdrInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsDateRangeExclusiveForFinancialYear");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = objFinancialYearHdrInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            IDbDataParameter parFinancialYearID = cmd.CreateParameter();
            parFinancialYearID.ParameterName = "@FinancialYearID";
            if (objFinancialYearHdrInfo.FinancialYearID != null)
            {
                parFinancialYearID.Value = objFinancialYearHdrInfo.FinancialYearID;
            }
            else
            {
                parFinancialYearID.Value = DBNull.Value;
            }
            cmdParams.Add(parFinancialYearID);

            System.Data.IDbDataParameter parStartDate = cmd.CreateParameter();
            parStartDate.ParameterName = "@StartDate";
            if (!objFinancialYearHdrInfo.IsStartDateNull)
                parStartDate.Value = Convert.ToDateTime(objFinancialYearHdrInfo.StartDate).ToString("MM/dd/yyyy");
            else
                parStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parStartDate);

            System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
            parEndDate.ParameterName = "@EndDate";
            if (!objFinancialYearHdrInfo.IsEndDateNull)
                parEndDate.Value = Convert.ToDateTime(objFinancialYearHdrInfo.EndDate).ToString("MM/dd/yyyy");
            else
                parEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parEndDate);


            return cmd;
        }

        public bool IsUniqueFiancialYear(FinancialYearHdrInfo objFinancialYearHdrInfo)
        {


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateIsUniqueFinancialYearCommand(objFinancialYearHdrInfo);
                cmd.Connection = con;
                con.Open();
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }


        }
        protected IDbCommand CreateIsUniqueFinancialYearCommand(FinancialYearHdrInfo objFinancialYearHdrInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsUniqueFinancialYear");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parFinancialYearName = cmd.CreateParameter();
            parFinancialYearName.ParameterName = "@FinancialYearName";
            parFinancialYearName.Value = objFinancialYearHdrInfo.FinancialYear;
            cmdParams.Add(parFinancialYearName);

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = objFinancialYearHdrInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            IDbDataParameter parFinancialYearID = cmd.CreateParameter();
            parFinancialYearID.ParameterName = "@FinancialYearID";
            if (objFinancialYearHdrInfo.FinancialYearID != null)
            {
                parFinancialYearID.Value = objFinancialYearHdrInfo.FinancialYearID;
            }
            else
            {
                parFinancialYearID.Value = DBNull.Value;
            }
            cmdParams.Add(parFinancialYearID);


            return cmd;
        }



    }
}