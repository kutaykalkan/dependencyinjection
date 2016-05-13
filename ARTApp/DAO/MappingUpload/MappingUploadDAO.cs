using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Client.Model.MappingUpload;
using System.Collections.Generic;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.MappingUpload.Base;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.MappingUpload
{
    public class MappingUploadDAO : MappingUploadDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingUploadDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<MappingUploadMasterInfo> GetAllMappingUploadInfoList()
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<MappingUploadMasterInfo> oMappingUploadMstInfoList = null;
            try
            {
                oMappingUploadMstInfoList = new List<MappingUploadMasterInfo>();
                cmd = CreateSelectCommandAllMappingUploadKeys();
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //MappingUploadMasterInfo oMappingUploadMstInfo;
                while (dr.Read())
                {
                    //oMappingUploadMstInfo = this.MapAllKeyObject(dr);
                    //oMappingUploadMstInfoList.Add(oMappingUploadMstInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return oMappingUploadMstInfoList;
        }

        public List<MappingUploadInfo> GetMappingUploadInfoList(int? ReconciliationPeriodID, int? CompanyID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<MappingUploadInfo> oMappingUploadInfoList = null;
            try
            {
                oMappingUploadInfoList = new List<MappingUploadInfo>();
                cmd = CreateSelectCommandMappingUploadInfoList(ReconciliationPeriodID, CompanyID);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MappingUploadInfo oMappingUploadInfo;
                while (dr.Read())
                {
                    oMappingUploadInfo = this.MapObject(dr);
                    oMappingUploadInfoList.Add(oMappingUploadInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return oMappingUploadInfoList;
        }

        private IDbCommand CreateSelectCommandAllMappingUploadKeys()
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllMappingUploadKeyList");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        private IDbCommand CreateSelectCommandMappingUploadInfoList(int? ReconciliationPeriodID, int? CompanyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllMappingUploadKeyList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@RecPeriodID";
            cmdRecPeriod.Value = ReconciliationPeriodID.Value;
            cmdParams.Add(cmdRecPeriod);

            IDbDataParameter cmdCompanyID = cmd.CreateParameter();
            cmdCompanyID.ParameterName = "@CompanyID";
            cmdCompanyID.Value = CompanyID.Value;
            cmdParams.Add(cmdCompanyID);

            return cmd;
        }

        public string SaveMappingUploadInfoList(List<MappingUploadInfo> oMappingUploadInfoList, int? recPeriodID, int? companyID, string userLoginID, DateTime dateRevised, int? userID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            try
            {
                cmd = CreateSaveCommandMappingUploadInfoList(oMappingUploadInfoList, recPeriodID, companyID, userLoginID, dateRevised, userID);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
                IDbDataParameter paramOutputParam = (IDbDataParameter)cmd.Parameters["@ReturnValue"];
                string returnValue = paramOutputParam.Value.ToString();
                return returnValue;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        private IDbCommand CreateSaveCommandMappingUploadInfoList(List<MappingUploadInfo> oMappingUploadInfoList, int? recPeriodID, int? companyID, string userLoginID, DateTime dateRevised, int? userID)
        {
            IDbCommand cmd = this.CreateCommand("[MappingUpload].[usp_SAV_MappingUpload]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            DataTable dt = ServiceHelper.ConvertMappingUploadToDataTable(oMappingUploadInfoList);
            IDbDataParameter paramMappingUploadList = cmd.CreateParameter();
            paramMappingUploadList.ParameterName = "@udtMappingUpload";
            paramMappingUploadList.Value = dt;
            cmdParams.Add(paramMappingUploadList);

            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForCompanyID(companyID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(dateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedUserID(userID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateRevised(dateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(userLoginID, cmd, cmdParams);

            IDbDataParameter paramReturnValue = cmd.CreateParameter();
            paramReturnValue.ParameterName = "@ReturnValue";
            paramReturnValue.Value = string.Empty;
            paramReturnValue.Direction = ParameterDirection.Output;
            paramReturnValue.Size = 200;
            cmdParams.Add(paramReturnValue);

            return cmd;
        }
    }
}
