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
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using System.Data.SqlClient;
using SkyStem.ART.Client.Model.RecControlCheckList;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class UtilityDAO : UtilityDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UtilityDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal int? GetRoleID(string RoleDesc)
        {
            System.Data.IDbCommand oCommand = null;

            try
            {
                oCommand = CreateGetRoleIDCommand(RoleDesc);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                int? roleID = Convert.ToInt32(oCommand.ExecuteScalar());
                return roleID;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }

        }

        private IDbCommand CreateGetRoleIDCommand(string RoleDesc)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("SELECT dbo.fn_GET_RoleID('" + RoleDesc + "')");
            oCommand.CommandType = CommandType.Text;
            return oCommand;
        }

        protected System.Data.IDbCommand CreateGetRecProcessStatusCommand(int? recPeriodID)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("usp_GET_RecProcessStatus");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            System.Data.IDbDataParameter parRecPeriodID = oCommand.CreateParameter();
            parRecPeriodID.ParameterName = "@ReconciliationPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);

            return oCommand;
        }

        internal List<AppSettingsInfo> GetAllAppSettings()
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<AppSettingsInfo> oAppSettingsInfoCollection = null;
            try
            {
                cmd = CreateGetAllAppSettingsCommand();
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //oAppSettingsInfoCollection = MapObjectForAppSettings(dr);
                while (dr.Read())
                {
                    AppSettingsInfo oAppSettingsInfo = this.MapObjectForAppSettings(dr);
                    oAppSettingsInfoCollection.Add(oAppSettingsInfo);
                }
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

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
            return oAppSettingsInfoCollection;


        }

        private IDbCommand CreateGetAllAppSettingsCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AppSettings");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
        protected AppSettingsInfo MapObjectForAppSettings(System.Data.IDataReader r)
        {
            AppSettingsInfo entity = new AppSettingsInfo();
            entity.AppSettingID = r.GetInt16Value("AppSettingID");
            entity.AppSettingName = r.GetStringValue("AppSettingName");
            entity.AppSettingValue = r.GetStringValue("AppSettingValue");
            return entity;
        }

        #region SelectAccountAttributeMstForMassUpdate

        //public List<AccountAttributeMstInfo> SelectAccountAttributeMstForMassUpdate(List<ARTEnums.AccountAttribute> oAccountAttributeCollection)
        //{
        //    List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection = new List<AccountAttributeMstInfo>();
        //    IDbCommand cmd = null;

        //    try
        //    {
        //        cmd = this.CreateSelectAccountAttributeMstForMassUpdateCommand(oAccountAttributeCollection);
        //        cmd.Connection = this.CreateConnection();
        //        cmd.Connection.Open();

        //        IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (reader.Read())
        //        {
        //            oAccountAttributeMstInfoCollection.Add(this.MapAccountAttributeInfo(reader));
        //        }
        //    }
        //    finally
        //    {
        //        if (cmd != null)
        //        {
        //            if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Closed)
        //            {
        //                cmd.Connection.Dispose();
        //            }
        //            cmd.Dispose();
        //        }
        //    }

        //    return oAccountAttributeMstInfoCollection;
        //}

        public List<AccountAttributeMstInfo> SelectAccountAttributeMstForMassUpdate(int? iCompanyId, int? iRecPeriodID)
        {
            List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection = new List<AccountAttributeMstInfo>();
            IDbCommand cmd = null;

            try
            {
                cmd = this.CreateSelectAccountAttributeMstForMassUpdateCommand(iCompanyId, iRecPeriodID);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    oAccountAttributeMstInfoCollection.Add(this.MapAccountAttributeInfo(reader));
                }
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Closed)
                    {
                        cmd.Connection.Dispose();
                    }
                    cmd.Dispose();
                }
            }

            return oAccountAttributeMstInfoCollection;
        }

        private AccountAttributeMstInfo MapAccountAttributeInfo(IDataReader reader)
        {
            AccountAttributeMstInfo entity = new AccountAttributeMstInfo();

            entity.AccountAttributeID = reader.GetInt16Value("AccountAttributeID");
            entity.AccountAttribute = reader.GetStringValue("AccountAttribute"); ;
            entity.AccountAttributeLabelID = reader.GetInt32Value("AccountAttributeLabelID");

            return entity;
        }

        private IDbCommand CreateSelectAccountAttributeMstForMassUpdateCommand(int? iCompanyId, int? iRecPeriodID)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("usp_SEL_AccountAttributeMstForMassUpdate");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            System.Data.IDbDataParameter parCompanyId = oCommand.CreateParameter();
            parCompanyId.ParameterName = "@CompanyId";
            parCompanyId.Value = iCompanyId;
            cmdParams.Add(parCompanyId);

            System.Data.IDataParameter parRecPeriodID = oCommand.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = iRecPeriodID;
            cmdParams.Add(parRecPeriodID);

            return oCommand;
        }
        //private IDbCommand CreateSelectAccountAttributeMstForMassUpdateCommand(List<ARTEnums.AccountAttribute> oAccountAttributeCollection)
        //{
        //    System.Data.IDbCommand oCommand = this.CreateCommand("usp_SEL_AccountAttributeMstForMassUpdate");
        //    oCommand.CommandType = CommandType.StoredProcedure;

        //    IDataParameterCollection cmdParams = oCommand.Parameters;

        //    foreach (ARTEnums.AccountAttribute oAccountAttribute in oAccountAttributeCollection)
        //    {
        //        switch (oAccountAttribute)
        //        {
        //            case ARTEnums.AccountAttribute.IsKeyAccount:
        //                System.Data.IDbDataParameter parKeyAccount = oCommand.CreateParameter();
        //                parKeyAccount.ParameterName = "@IsKeyAccountAttributeID";
        //                parKeyAccount.Value = (short)oAccountAttribute;
        //                cmdParams.Add(parKeyAccount);
        //                break;

        //            case ARTEnums.AccountAttribute.IsZeroBalanceAccount:
        //                System.Data.IDbDataParameter parZeroBalanceAccount = oCommand.CreateParameter();
        //                parZeroBalanceAccount.ParameterName = "@IsZeroBalanceAttributeID";
        //                parZeroBalanceAccount.Value = (short)oAccountAttribute;
        //                cmdParams.Add(parZeroBalanceAccount);
        //                break;

        //            case ARTEnums.AccountAttribute.RiskRating:
        //                System.Data.IDbDataParameter parRiskRating = oCommand.CreateParameter();
        //                parRiskRating.ParameterName = "@RiskRatingAttributeID";
        //                parRiskRating.Value = (short)oAccountAttribute;
        //                cmdParams.Add(parRiskRating);
        //                break;

        //            //case ARTEnums.AccountAttribute.AccountType:
        //            //    System.Data.IDbDataParameter parAccountType = oCommand.CreateParameter();
        //            //    parAccountType.ParameterName = "@AccountTypeAttributeID";
        //            //    parAccountType.Value = (short)oAccountAttribute;
        //            //    cmdParams.Add(parAccountType);
        //            //    break;
        //        }
        //    }

        //    return oCommand;
        //}

        public string GetKeyFieldsByCompanyID(int companyID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetKeyFieldsByCompanyIDCommand(companyID);

                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                return reader.GetStringValue("KeyFields");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }

        public string GetAccountUniqueSubsetKeys(int companyID, int recPeriodID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetAccountUniqueSubsetKeysCommand(companyID, recPeriodID);

                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                return reader.GetStringValue("AccountUniqueSubsetKeys");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }

        private IDbCommand GetKeyFieldsByCompanyIDCommand(int companyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_AllKeyFieldsByCompanyID");

            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramCompanyID = cmd.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = companyID;
            cmdParams.Add(paramCompanyID);

            return cmd;
        }
        private IDbCommand GetAccountUniqueSubsetKeysCommand(int companyID, int recPeriodID)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("[MappingUpload].[usp_GET_AllAccountUniqueSubsetKeys]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter parCompanyID = oCommand.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;

            IDbDataParameter parRecPeriodID = oCommand.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parRecPeriodID);

            return oCommand;
        }
        #endregion


        public void ReCreateIndexes(int? companyID)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandReCreateIndexes())
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private IDbCommand CreateCommandReCreateIndexes()
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_SVC_UPD_RebuildIndexes]");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public IList<DueDaysBasisInfo> SelectAllDueDaysBasisMst()
        {
            List<DueDaysBasisInfo> oDueDaysBasisInfoCollection = new List<DueDaysBasisInfo>();


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                IDataReader reader;
                cmd = this.CreateCommandSelectAllDueDaysBasisMst();
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                DueDaysBasisInfo oDueDaysBasisInfo = null;
                while (reader.Read())
                {
                    oDueDaysBasisInfo = new DueDaysBasisInfo();
                    oDueDaysBasisInfo.DueDaysBasisID = reader.GetInt16Value("DueDaysBasisID");
                    oDueDaysBasisInfo.DueDaysBasis = reader.GetStringValue("DueDaysBasis");
                    oDueDaysBasisInfo.DueDaysBasisLabelID = reader.GetInt32Value("DueDaysBasisLabelID");
                    oDueDaysBasisInfoCollection.Add(oDueDaysBasisInfo);
                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oDueDaysBasisInfoCollection;

        }
        private IDbCommand CreateCommandSelectAllDueDaysBasisMst()
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_SEL_AllDueDaysBasis]");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        internal List<ReconciliationCheckListStatusInfo> GetReconciliationCheckListStatus()
        {
            List<ReconciliationCheckListStatusInfo> ReconciliationCheckListStatusInfoCollection = new List<ReconciliationCheckListStatusInfo>();

            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                IDataReader reader;
                cmd = this.CreateCommandSelectReconciliationCheckListStatusMst();
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                ReconciliationCheckListStatusInfo oReconciliationCheckListStatusInfo = null;
                while (reader.Read())
                {
                    oReconciliationCheckListStatusInfo = new ReconciliationCheckListStatusInfo();
                    oReconciliationCheckListStatusInfo.ReconciliationCheckListStatusID = reader.GetInt16Value("ReconciliationCheckListStatusID");
                    oReconciliationCheckListStatusInfo.ReconciliationCheckListStatus = reader.GetStringValue("ReconciliationCheckListStatus");
                    oReconciliationCheckListStatusInfo.ReconciliationCheckListStatusLabelID = reader.GetInt32Value("ReconciliationCheckListStatusLabelID");
                    ReconciliationCheckListStatusInfoCollection.Add(oReconciliationCheckListStatusInfo);
                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return ReconciliationCheckListStatusInfoCollection;

        }

        private IDbCommand CreateCommandSelectReconciliationCheckListStatusMst()
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_SEL_GLDataRecControlCheckList]");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public List<string> GetImportTemplateMandatoryFields(int? companyID, int? ImportTemplateID, List<string> MandatoryFieldList)
        {
            List<string> oTemplateMandatoryFieldList = new List<string>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetImportTemplateMandatoryFieldsCommand(companyID, ImportTemplateID, MandatoryFieldList);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oTemplateMandatoryFieldList.Add(reader.GetStringValue("ImportTemplateField"));
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oTemplateMandatoryFieldList;
        }
        private IDbCommand GetImportTemplateMandatoryFieldsCommand(int? companyID, int? ImportTemplateID, List<string> MandatoryFieldList)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("[dbo].[usp_SEL_ImportTemplateMandatoryFields]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter cmdParamMandatoryFieldsTable = oCommand.CreateParameter();
            cmdParamMandatoryFieldsTable.ParameterName = "@tblMandatoryFields";
            cmdParamMandatoryFieldsTable.Value = ServiceHelper.ConvertStringListToDataTable(MandatoryFieldList);
            cmdParams.Add(cmdParamMandatoryFieldsTable);

            IDbDataParameter parCompanyID = oCommand.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (companyID.HasValue)
                parCompanyID.Value = companyID.Value;
            else
                parCompanyID.Value = DBNull.Value;

            IDbDataParameter parImportTemplateID = oCommand.CreateParameter();
            parImportTemplateID.ParameterName = "@ImportTemplateID";
            if (ImportTemplateID.HasValue)
                parImportTemplateID.Value = ImportTemplateID.Value;
            else
                parImportTemplateID.Value = DBNull.Value;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parImportTemplateID);

            return oCommand;
        }

        public IList<DaysTypeInfo> SelectAllDaysType()
        {
            List<DaysTypeInfo> oDaysTypeInfoList = new List<DaysTypeInfo>();


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                IDataReader reader;
                cmd = this.CreateCommandSelectAllDaysType();
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                DaysTypeInfo oDaysTypeInfo = null;
                while (reader.Read())
                {
                    oDaysTypeInfo = new DaysTypeInfo();
                    oDaysTypeInfo.DaysTypeID = reader.GetInt16Value("DaysTypeID");
                    oDaysTypeInfo.DaysType = reader.GetStringValue("DaysType");
                    oDaysTypeInfo.DaysTypeLabelID = reader.GetInt32Value("DaysTypeLabelID");
                    oDaysTypeInfoList.Add(oDaysTypeInfo);
                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oDaysTypeInfoList;

        }
        private IDbCommand CreateCommandSelectAllDaysType()
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_SEL_AllDaysType]");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}
