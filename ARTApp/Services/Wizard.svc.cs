using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Wizard" here, you must also update the reference to "Wizard" in Web.config.
    public class Wizard : IWizard
    {
        public void DoWork()
        {
        }
        public IList<WizardStepInfo> GetWizardStepsByTypeID(int? WizardTypeID, AppUserInfo oAppUserInfo) 
        {

            IList<WizardStepInfo> oWizardStepInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                WizardStepDAO objWizardStepDAO = new WizardStepDAO(oAppUserInfo);
                oWizardStepInfoCollection = objWizardStepDAO.GetWizardStepsByTypeID(WizardTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oWizardStepInfoCollection;
        
        }

        public List<WizardStepDependencyInfo> GetAllDependentSteps(AppUserInfo oAppUserInfo)
        {

            List<WizardStepDependencyInfo> oWizardStepDependencyInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                WizardStepDependencyDAO oWizardStepDependencyDAO = new WizardStepDependencyDAO(oAppUserInfo);
                oWizardStepDependencyInfoCollection = oWizardStepDependencyDAO.GetAllDependentSteps();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oWizardStepDependencyInfoCollection;

        }
    }
}
