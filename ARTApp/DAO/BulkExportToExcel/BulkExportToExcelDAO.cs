using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.App.DAO.BulkExportToExcel.Base;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.App.Utility;
using System.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO.Base;

namespace SkyStem.ART.App.DAO.BulkExportToExcel
{
    public class BulkExportToExcelDAO : BulkExportToExelDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BulkExportToExcelDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public bool SaveExportDetails(BulkExportToExcelInfo objBulkExportToExcelInfo)
        {
            //
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            try
            {
                cmd = CreateSaveCommandBulkExportInfoList(objBulkExportToExcelInfo);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return true;
        }
        private IDbCommand CreateSaveCommandBulkExportInfoList(BulkExportToExcelInfo oBulkExportInfoList)
        {
            IDbCommand cmd = this.CreateCommand("[Request].[usp_SAV_ExportToExcelDetails]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;


            ServiceHelper.AddCommonUserRoleAndRecPeriodParameters(oBulkExportInfoList.UserID, oBulkExportInfoList.RoleID,
                oBulkExportInfoList.RecperiodID, cmd, cmdParams);

            IDbDataParameter paramGridID = cmd.CreateParameter();
            paramGridID.ParameterName = "@GridID";
            paramGridID.Value = oBulkExportInfoList.GridID;
            cmdParams.Add(paramGridID);

            IDbDataParameter paramStatusID = cmd.CreateParameter();
            paramStatusID.ParameterName = "@StatusID";
            paramStatusID.Value = oBulkExportInfoList.StatusID;
            cmdParams.Add(paramStatusID);

            IDbDataParameter paramRequestTypeID = cmd.CreateParameter();
            paramRequestTypeID.ParameterName = "@RequestTypeID";
            paramRequestTypeID.Value = oBulkExportInfoList.RequestTypeID;
            cmdParams.Add(paramRequestTypeID);

            ServiceHelper.AddCommonParametersForAddedBy(oBulkExportInfoList.AddedBy, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(oBulkExportInfoList.DateAdded, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateRevised(oBulkExportInfoList.DateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(oBulkExportInfoList.RevisedBy, cmd, cmdParams);

            IDbDataParameter paramIsActive = cmd.CreateParameter();
            paramIsActive.ParameterName = "@IsActive";
            paramIsActive.Value = oBulkExportInfoList.IsActive;
            cmdParams.Add(paramIsActive);

            IDbDataParameter paramToEmailID = cmd.CreateParameter();
            paramToEmailID.ParameterName = "@ToEmailID";
            paramToEmailID.Value = oBulkExportInfoList.ToEmailID;
            cmdParams.Add(paramToEmailID);


            IDbDataParameter paramFinalMessage = cmd.CreateParameter();
            paramFinalMessage.ParameterName = "@FinalMessage";
            paramFinalMessage.Value = oBulkExportInfoList.FinalMessage;
            cmdParams.Add(paramFinalMessage);

            IDbDataParameter paramFromEmailID = cmd.CreateParameter();
            paramFromEmailID.ParameterName = "@FromEmailID";
            paramFromEmailID.Value = oBulkExportInfoList.FromEmailID;
            cmdParams.Add(paramFromEmailID);

            IDbDataParameter paramEmailBody = cmd.CreateParameter();
            paramEmailBody.ParameterName = "@EmailBody";
            paramEmailBody.Value = oBulkExportInfoList.EmailBody;
            cmdParams.Add(paramEmailBody);

            IDbDataParameter paramEmailSubject = cmd.CreateParameter();
            paramEmailSubject.ParameterName = "@EmailSubject";
            paramEmailSubject.Value = oBulkExportInfoList.EmailSubject;
            cmdParams.Add(paramEmailSubject);

            IDbDataParameter paramLanguageID = cmd.CreateParameter();
            paramLanguageID.ParameterName = "@LanguageID";
            paramLanguageID.Value = oBulkExportInfoList.LanguageID;
            cmdParams.Add(paramLanguageID);

            IDbDataParameter paramData = cmd.CreateParameter();
            paramData.ParameterName = "@Data";
            paramData.Value = oBulkExportInfoList.Data;
            cmdParams.Add(paramData);


            return cmd;
        }
        public List<BulkExportToExcelInfo> GetRequests(int? RecPeriodID, int? UserID, short? RoleID, List<short> RequestTypeList)
        {
            List<BulkExportToExcelInfo> oBulkExportToExcelInfoCollection = new List<BulkExportToExcelInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                DataTable dtRequestType = ServiceHelper.ConvertIDCollectionToDataTable(RequestTypeList);
                cmd = this.GetRequestsCommand(RecPeriodID, UserID, RoleID, dtRequestType);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oBulkExportToExcelInfoCollection.Add(this.MapObject(reader));
                }
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
            return oBulkExportToExcelInfoCollection;
        }
        private IDbCommand GetRequestsCommand(int? RecPeriodID, int? UserID, short? RoleID, DataTable dtRequestType)
        {
            IDbCommand cmd = this.CreateCommand("[Request].[usp_SEL_Requests]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonUserRoleAndRecPeriodParameters(UserID, RoleID, RecPeriodID, cmd, cmdParams);
            IDbDataParameter paramRequestType = cmd.CreateParameter();
            paramRequestType.ParameterName = "@udtRequestType";
            paramRequestType.Value = dtRequestType;
            cmdParams.Add(paramRequestType);
            return cmd;
        }

        public List<DataImportHdrInfo> DeleteRequests(List<int> SelectedRequestIDs, int CompanyID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            IDbTransaction oTrans = null;
            List<DataImportHdrInfo> oDataImportHdrInfoList = new List<DataImportHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                DataTable dtSelectedRequestIDs = ServiceHelper.ConvertIDCollectionToDataTable(SelectedRequestIDs);
                oCmd = this.GetDeleteRequestsCommand(dtSelectedRequestIDs, CompanyID, revisedBy, dateRevised);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                reader = oCmd.ExecuteReader();
                while (reader.Read())
                {
                    DataImportHdrInfo oDataImportHdrInfo = this.MapObjectDataImportHdr(reader);
                    oDataImportHdrInfoList.Add(oDataImportHdrInfo);
                }
                reader.Close();
                oTrans.Commit();
                oTrans.Dispose();
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oDataImportHdrInfoList;
        }
        private DataImportHdrInfo MapObjectDataImportHdr(System.Data.IDataReader r)
        {

            DataImportHdrInfo entity = new DataImportHdrInfo();
            entity.FileName = r.GetStringValue("FileName");
            entity.PhysicalPath = r.GetStringValue("PhysicalPath");
            entity.FileSize = r.GetDecimalValue("FileSize");

            return entity;
        }
        private IDbCommand GetDeleteRequestsCommand(DataTable dtSelectedRequestIDs, int CompanyID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCommand = this.CreateCommand("[Request].[usp_DEL_Requests]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramudtRequestIDs = oCommand.CreateParameter();
            paramudtRequestIDs.ParameterName = "@udtRequestIDs";
            paramudtRequestIDs.Value = dtSelectedRequestIDs;

            IDbDataParameter paramCompanyID = oCommand.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = CompanyID;

            IDbDataParameter paramRevisedBy = oCommand.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = revisedBy;

            IDbDataParameter paramDateRevised = oCommand.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;

            cmdParams.Add(paramudtRequestIDs);
            cmdParams.Add(paramCompanyID);
            cmdParams.Add(paramRevisedBy);
            cmdParams.Add(paramDateRevised);

            return oCommand;
        }

        public List<BulkExportToExcelInfo> GetAllRecBinders(int? companyID, int? UserID, short? RoleID, List<short> RequestTypeList)
        {
            List<BulkExportToExcelInfo> oBulkExportToExcelInfoCollection = new List<BulkExportToExcelInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                DataTable dtRequestType = ServiceHelper.ConvertIDCollectionToDataTable(RequestTypeList);
                cmd = this.GetAllRecBindersCommand(companyID, UserID, RoleID, dtRequestType);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oBulkExportToExcelInfoCollection.Add(this.MapObject(reader));
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return oBulkExportToExcelInfoCollection;
        }
        private IDbCommand GetAllRecBindersCommand(int? companyID, int? UserID, short? RoleID, DataTable dtRequestType)
        {
            IDbCommand cmd = this.CreateCommand("[Request].[usp_SEL_AllRecBinders]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForCompanyID(companyID, cmd, cmdParams);
            ServiceHelper.AddCommonUserAndRoleParameters(UserID, RoleID, cmd, cmdParams);

            IDbDataParameter paramRequestType = cmd.CreateParameter();
            paramRequestType.ParameterName = "@udtRequestType";
            paramRequestType.Value = dtRequestType;
            cmdParams.Add(paramRequestType);
            return cmd;
        }
    }
}
