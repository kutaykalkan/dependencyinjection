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
    public class WizardStepDependencyDAO : WizardStepDependencyDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WizardStepDependencyDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<WizardStepDependencyInfo> GetAllDependentSteps()
        {
            WizardStepDependencyInfo oWizardStepDependencyInfo = null;
            IDbConnection oConnection = null;
            IDbCommand oCommand;
            List<WizardStepDependencyInfo> oWizardStepDependencyInfoCollection = new List<WizardStepDependencyInfo>();
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                oCommand = CreateGetAllDependentStepsCommand();
                oCommand.Connection = oConnection;
                IDataReader oReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oReader.Read())
                {
                    oWizardStepDependencyInfo = (WizardStepDependencyInfo)MapObjectWizardStepDependencyInfo(oReader);
                    oWizardStepDependencyInfoCollection.Add(oWizardStepDependencyInfo);
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

            return oWizardStepDependencyInfoCollection;

        }

        protected IDbCommand CreateGetAllDependentStepsCommand()
        {
            IDbCommand oCommand = this.CreateCommand("usp_GET_AllDependentSteps");
            oCommand.CommandType = CommandType.StoredProcedure;
            return oCommand;
        }


        protected WizardStepDependencyInfo MapObjectWizardStepDependencyInfo(System.Data.IDataReader r)
        {
            WizardStepDependencyInfo entity = new WizardStepDependencyInfo();
            entity.WizardStepID = r.GetInt32Value("WizardStepID");
            entity.DependentWizardStepID = r.GetInt32Value("DependentWizardStepID");
            entity.WizardStepDependencyID = r.GetInt64Value("WizardStepDependencyID");
            return entity;
        }
    }
}