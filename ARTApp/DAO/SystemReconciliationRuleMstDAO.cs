


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
    public class SystemReconciliationRuleMstDAO : SystemReconciliationRuleMstDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public SystemReconciliationRuleMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<SystemReconciliationRuleMstInfo> GetAllSystemReconciliationRules()
        {
            List<SystemReconciliationRuleMstInfo> oSystemReconciliationRuleMstInfoList = null;
            IDbCommand oIDbCommand = null;
            IDataReader oIDataReader = null;
            try
            {
                oIDbCommand = CreateCustomSelectAllCommand();
                oIDbCommand.Connection = CreateConnection();
                oIDbCommand.Connection.Open();
                oIDataReader = oIDbCommand.ExecuteReader(CommandBehavior.CloseConnection);

                oSystemReconciliationRuleMstInfoList = new List<SystemReconciliationRuleMstInfo>();
                while (oIDataReader.Read())
                {
                    oSystemReconciliationRuleMstInfoList.Add(MapObject(oIDataReader));
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
            return oSystemReconciliationRuleMstInfoList;
        }

        protected override SystemReconciliationRuleMstInfo MapObject(System.Data.IDataReader r)
        {
            SystemReconciliationRuleMstInfo entity = new SystemReconciliationRuleMstInfo();
            entity.SystemReconciliationRuleID = r.GetInt16Value("SystemReconciliationRuleID");
            entity.SystemReconciliationRule = r.GetStringValue("SystemReconciliationRule");
            entity.SystemReconciliationRuleLabelID = r.GetInt32Value("SystemReconciliationRuleLabelID");
            entity.CapabilityID = r.GetInt16Value("CapabilityID");
            entity.SystemReconciliationRuleNumber = r.GetStringValue("SystemReconciliationRuleNumber");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");
            return entity;
        }

        protected System.Data.IDbCommand CreateCustomSelectAllCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllSystemReconciliationRule");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}