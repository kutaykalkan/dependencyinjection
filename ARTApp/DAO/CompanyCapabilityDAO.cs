


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;

using SkyStem.ART.App.Utility;

using SkyStem.ART.Client.Model;
using System.Collections.Generic;
namespace SkyStem.ART.App.DAO
{
    public class CompanyCapabilityDAO : CompanyCapabilityDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyCapabilityDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        //public IList<CompanyCapabilityInfo> SelectAllByCompanyID(object id)
        public IList<CompanyCapabilityInfo> SelectAllCompanyCapabilityByReconciliationPeriodID(int? reconciliationPeriodID)
        {
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoList = new List<CompanyCapabilityInfo>();
            CompanyCapabilityInfo oCompanyCapabilityInfo = null;
            CapabilityAttributeValueInfo oCapabilityAttributeValueInfo = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandForSelectAllCompanyCapabilityByReconciliationPeriodID(reconciliationPeriodID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        oCapabilityAttributeValueInfo = MapCapabilityAttributeValue(dr);
                        if (oCompanyCapabilityInfo == null || oCompanyCapabilityInfo.CapabilityID != oCapabilityAttributeValueInfo.CapabilityID)
                        {
                            oCompanyCapabilityInfo = this.MapObject(dr);
                            oCompanyCapabilityInfo.CapabilityAttributeValueInfoList = new List<CapabilityAttributeValueInfo>();
                            oCompanyCapabilityInfoList.Add(oCompanyCapabilityInfo);
                        }
                        oCompanyCapabilityInfo.CapabilityAttributeValueInfoList.Add(oCapabilityAttributeValueInfo);
                    }
                }
            }
            return oCompanyCapabilityInfoList;
        }

        private IDbCommand CreateCommandForSelectAllCompanyCapabilityByReconciliationPeriodID(int? reconciliationPeriodID)
        {
            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyCapabilityByCompanyID");
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CompanyCapabilityActivationByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            //System.Data.IDbDataParameter par = cmd.CreateParameter();
            //par.ParameterName = "@CompanyID";
            //par.Value = id;
            //cmdParams.Add(par);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (reconciliationPeriodID.HasValue)
                parReconciliationPeriodID.Value = reconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);
            return cmd;
        }

        private CapabilityAttributeValueInfo MapCapabilityAttributeValue(IDataReader dr)
        {
            CapabilityAttributeValueInfo entity = new CapabilityAttributeValueInfo();
            entity.CapabilityID = dr.GetInt16Value("CapabilityID");
            entity.CapabilityAttributeID = dr.GetInt32Value("CapabilityAttributeID");
            entity.ParentCapabilityAttributeID = dr.GetInt32Value("ParentCapabilityAttributeID");
            entity.StartRecPeriodID = dr.GetInt32Value("StartRecPeriodIDForAttribute");
            entity.EndRecPeriodID = dr.GetInt32Value("EndRecPeriodIDForAttribute");
            entity.IsCarryForwardedFromPreviousRecPeriod = dr.GetBooleanValue("IsCarryForwardedFromPreviousRecPeriodForAttribute");
            entity.ReferenceID = dr.GetInt32Value("ReferenceID");
            entity.Value = dr.GetStringValue("Value");
            return entity;
        }

        //public int SaveCompanyCapabilityTableValue(List<int> IDs, CompanyCapabilityInfo oCompanyCapabilityInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        public int SaveCompanyCapabilityTableValue(int? reconciliationPeriodID, IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection, IDbConnection oConnection, IDbTransaction oTransaction, DateTime dateRevised, string revisedBy)
        {
            //IDbConnection oConnection = null;
            //IDbTransaction oTransaction = null;
            int intResult = 0;
            //try
            //{
            DataTable dt = ServiceHelper.ConvertCompanyCapabilityInfoCollectionToDataTable(oCompanyCapabilityInfoCollection);
            DataTable dtCapabilityAttributeValue = ServiceHelper.ConvertCompanyCapabilityAttributeInfoCollectionToDataTable(oCompanyCapabilityInfoCollection);
            IDbCommand oCommand = CreateInsertCommandTableValue(reconciliationPeriodID, dt, dtCapabilityAttributeValue, dateRevised, revisedBy);
            //oConnection = CreateConnection();
            //oConnection.Open();
            oCommand.Connection = oConnection;
            //oTransaction = oConnection.BeginTransaction();
            oCommand.Transaction = oTransaction;
            intResult = oCommand.ExecuteNonQuery();
            //oTransaction.Commit();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    try
            //    {
            //        if (null != oConnection)
            //            oConnection.Close();
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            return intResult;
        }

        //TODO: addedby etc fields,remove oCompanyCapabilityInfo parameter etc
        protected System.Data.IDbCommand CreateInsertCommandTableValue(int? reconciliationPeriodID, DataTable capabilityIDs, DataTable dtCapabilityAttributeValue, DateTime dateRevised, string revisedBy)
        {

            //System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CompanyCapabilityTableValue");
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CompanyCapabilityActivationTableValue");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCapabilityID = cmd.CreateParameter();
            //parCapabilityID.ParameterName = "@CapabilityIDs";
            parCapabilityID.ParameterName = "@tblCapabilityIDs";
            //if (!entity.IsCapabilityIDNull)
            parCapabilityID.Value = capabilityIDs;
            //else
            //    parCapabilityID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityID);

            System.Data.IDbDataParameter parCapabilityAttributeValue = cmd.CreateParameter();
            parCapabilityAttributeValue.ParameterName = "@udtCapabilityAttributeValue";
            parCapabilityAttributeValue.Value = dtCapabilityAttributeValue;
            cmdParams.Add(parCapabilityAttributeValue);

            //System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            //parCompanyID.ParameterName = "@CompanyID";
            //if (!oCompanyCapabilityInfo.IsCompanyIDNull)
            //    parCompanyID.Value = oCompanyCapabilityInfo.CompanyID;
            //else
            //    parCompanyID.Value = System.DBNull.Value;
            //cmdParams.Add(parCompanyID);

            //System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
            //parEndDate.ParameterName = "@EndDate";
            //if (!oCompanyCapabilityInfo.IsEndDateNull)
            //    parEndDate.Value = oCompanyCapabilityInfo.EndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            //else
            //    parEndDate.Value = System.DBNull.Value;
            //cmdParams.Add(parEndDate);

            //System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            //parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            //if (!oCompanyCapabilityInfo.IsEndReconciliationPeriodIDNull)
            //    parEndReconciliationPeriodID.Value = oCompanyCapabilityInfo.EndReconciliationPeriodID.Value;
            //else
            //    parEndReconciliationPeriodID.Value = System.DBNull.Value;
            //cmdParams.Add(parEndReconciliationPeriodID);


            //System.Data.IDbDataParameter parIsConfigurationComplete = cmd.CreateParameter();
            //parIsConfigurationComplete.ParameterName = "@IsConfigurationComplete";
            //if (!oCompanyCapabilityInfo.IsIsConfigurationCompleteNull)
            //    parIsConfigurationComplete.Value = oCompanyCapabilityInfo.IsConfigurationComplete;
            //else
            //    parIsConfigurationComplete.Value = System.DBNull.Value;
            //cmdParams.Add(parIsConfigurationComplete);

            //System.Data.IDbDataParameter parIsActivated = cmd.CreateParameter();
            //parIsActivated.ParameterName = "@IsActivated";
            //if (!oCompanyCapabilityInfo.IsIsActivatedNull)
            //    parIsActivated.Value = oCompanyCapabilityInfo.IsConfigurationComplete;
            //else
            //    parIsActivated.Value = System.DBNull.Value;
            //cmdParams.Add(parIsActivated);



            //System.Data.IDbDataParameter parStartDate = cmd.CreateParameter();
            //parStartDate.ParameterName = "@StartDate";
            //if (!oCompanyCapabilityInfo.IsStartDateNull)
            //    parStartDate.Value = oCompanyCapabilityInfo.StartDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            //else
            //    parStartDate.Value = System.DBNull.Value;
            //cmdParams.Add(parStartDate);


            //System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            //parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            //if (!oCompanyCapabilityInfo.IsStartReconciliationPeriodIDNull)
            //    parStartReconciliationPeriodID.Value = oCompanyCapabilityInfo.StartReconciliationPeriodID.Value;
            //else
            //    parStartReconciliationPeriodID.Value = System.DBNull.Value;
            //cmdParams.Add(parStartReconciliationPeriodID);


            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            cmdParams.Add(parRevisedBy);


            IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised;
            cmdParams.Add(parDateRevised);

            return cmd;
        }

        public List<DualLevelReviewTypeMstInfo> SelectAllDualLevelReviewTypeMst()
        {
            List<DualLevelReviewTypeMstInfo> oDualLevelReviewTypeMstInfoList = null;
            IDbCommand oIDbCommand = null;
            IDataReader oIDataReader = null;
            try
            {
                oIDbCommand = CreateSelectAllDualLevelReviewTypeMstCommand();
                oIDbCommand.Connection = CreateConnection();
                oIDbCommand.Connection.Open();
                oIDataReader = oIDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oDualLevelReviewTypeMstInfoList = new List<DualLevelReviewTypeMstInfo>();
                while (oIDataReader.Read())
                {
                    oDualLevelReviewTypeMstInfoList.Add(MapDualLevelReviewTypeMstInfoObject(oIDataReader));
                }
            }
            finally
            {
                if (oIDataReader != null)
                {
                    oIDataReader.ClearColumnHash();
                    oIDataReader.Close();
                    oIDataReader.Dispose();
                }
                oIDbCommand.Dispose();
            }
            return oDualLevelReviewTypeMstInfoList;
        }
        protected System.Data.IDbCommand CreateSelectAllDualLevelReviewTypeMstCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllDualLevelReviewType");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        protected DualLevelReviewTypeMstInfo MapDualLevelReviewTypeMstInfoObject(System.Data.IDataReader r)
        {
            DualLevelReviewTypeMstInfo entity = new DualLevelReviewTypeMstInfo();
            entity.DualLevelReviewTypeID = r.GetInt16Value("DualLevelReviewTypeID");
            entity.DualLevelReviewType = r.GetStringValue("DualLevelReviewType");
            entity.DualLevelReviewTypeLabelID = r.GetInt32Value("DualLevelReviewTypeLabelID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            return entity;
        }

    }//end of class
}//end of namespace