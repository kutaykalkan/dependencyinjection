


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SkyStem.ART.App.DAO
{   
    public class WizardStepDAO : WizardStepDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WizardStepDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public IList<WizardStepInfo> GetWizardStepsByTypeID(int? WizardTypeID)
        {
            WizardStepInfo oWizardStepInfo = null;
            IDbConnection oConnection = null;
            IDbCommand oCommand;
            IList<WizardStepInfo> oWizardStepInfoList = new List<WizardStepInfo>();
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();

                oCommand = CreateGetWizardStepsCommand(WizardTypeID);
                oCommand.Connection = oConnection;
                IDataReader oReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oReader.Read())
                {
                    oWizardStepInfo = (WizardStepInfo)MapObjectWizardStepInfo(oReader);
                    oWizardStepInfoList.Add(oWizardStepInfo);
                }
                oReader.Close();
            }
            finally
            {
                try
                {
                    if (oConnection != null)
                    {
                        oConnection.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }

            return oWizardStepInfoList;

        }

        protected IDbCommand CreateGetWizardStepsCommand(int? WizardTypeID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_GET_WizardStepsByWizardTypeID");
            oCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection oParamCollection = oCommand.Parameters;

            SqlParameter paramWizardTypeID = new SqlParameter("@WizardTypeID", WizardTypeID);
            oParamCollection.Add(paramWizardTypeID);
          

            return oCommand;
        }


        protected WizardStepInfo MapObjectWizardStepInfo(System.Data.IDataReader r)
        {
            WizardStepInfo entity = new WizardStepInfo();
            entity.WizardStepID = r.GetInt16Value("WizardStepID");
            entity.WizardStepNameLabelID = r.GetInt32Value("WizardStepNameLabelID");
            entity.WizardStepName = r.GetStringValue("WizardStepName");
            entity.WizardStepURL = r.GetStringValue("WizardStepURL");           
            return entity;
        }

    }
}