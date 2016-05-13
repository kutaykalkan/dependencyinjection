


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class NetAccountAttributeValueDAO : NetAccountAttributeValueDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NetAccountAttributeValueDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<NetAccountAttributeValueInfo> GetNetAccountAttributesValue(int netAccountID, int companyID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateGetNetAccountAttributesValueCommand(netAccountID, companyID, recPeriodID);
            cmd.Connection = this.CreateConnection();
            List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo = this.Select(cmd);
            return lstNetAccountAttributeValueInfo;
        }

        protected override System.Data.IDbCommand CreateUpdateCommand(NetAccountAttributeValueInfo oNetAccountAttributeValueInfo)
        {
            System.Data.IDbCommand oDbCommand = null;
            return oDbCommand;
        }



        public System.Data.IDbCommand CreateUpdateNetAccountAttributesValueCommand(List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo, NetAccountHdrInfo oNetAccountHdrInfo, int recPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_NetAccountAttributesValue");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            if (!oNetAccountHdrInfo.IsNetAccountIDNull)
                parNetAccountID.Value = oNetAccountHdrInfo.NetAccountID;
            else
                parNetAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = oNetAccountHdrInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parNetAccountAttributeValue = cmd.CreateParameter();
            parNetAccountAttributeValue.ParameterName = "tbNetAccountAttributeValue";
            parNetAccountAttributeValue.Value = ServiceHelper.ConvertNetAccountAttributeValueInfoToDataTable(lstNetAccountAttributeValueInfo);
            cmdParams.Add(parNetAccountAttributeValue);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@UserLoginID";
            if (!oNetAccountHdrInfo.IsAddedByNull)
                parAddedBy.Value = oNetAccountHdrInfo.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@UpdateTime";
            if (!oNetAccountHdrInfo.IsDateAddedNull)
                parDateAdded.Value = oNetAccountHdrInfo.DateAdded;
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            //System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            //parAddedBy.ParameterName = "@UserLoginID";
            //if (!entity.IsAddedByNull)
            //    parAddedBy.Value = entity.AddedBy;
            //else
            //    parAddedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedBy);

            //System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            //parDataImportID.ParameterName = "@DataImportID";
            //if (!entity.IsDataImportIDNull)
            //    parDataImportID.Value = entity.DataImportID;
            //else
            //    parDataImportID.Value = System.DBNull.Value;
            //cmdParams.Add(parDataImportID);

            //System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            //parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            //if (!entity.IsEndReconciliationPeriodIDNull)
            //    parEndReconciliationPeriodID.Value = entity.EndReconciliationPeriodID;
            //else
            //    parEndReconciliationPeriodID.Value = System.DBNull.Value;
            //cmdParams.Add(parEndReconciliationPeriodID);

            //System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            //parHostName.ParameterName = "@HostName";
            //if (!entity.IsHostNameNull)
            //    parHostName.Value = entity.HostName;
            //else
            //    parHostName.Value = System.DBNull.Value;
            //cmdParams.Add(parHostName);

            //System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            //parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            //if (!entity.IsStartReconciliationPeriodIDNull)
            //    parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            //else
            //    parStartReconciliationPeriodID.Value = System.DBNull.Value;
            //cmdParams.Add(parStartReconciliationPeriodID);

            //System.Data.IDbDataParameter parValue = cmd.CreateParameter();
            //parValue.ParameterName = "@Value";
            //if (!entity.IsValueNull)
            //    parValue.Value = entity.Value;
            //else
            //    parValue.Value = System.DBNull.Value;
            //cmdParams.Add(parValue);

            //System.Data.IDbDataParameter parValueLabelID = cmd.CreateParameter();
            //parValueLabelID.ParameterName = "@ValueLabelID";
            //if (!entity.IsValueLabelIDNull)
            //    parValueLabelID.Value = entity.ValueLabelID;
            //else
            //    parValueLabelID.Value = System.DBNull.Value;
            //cmdParams.Add(parValueLabelID);

            return cmd;

        }

        protected System.Data.IDbCommand CreateGetNetAccountAttributesValueCommand(int netAccountID, int companyID, int recPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_NetAccountAttributeValuesByNetAccountIDAndRecPeriodEndDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            parNetAccountID.Value = netAccountID;
            cmdParams.Add(parNetAccountID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);
            return cmd;

        }
    }
}