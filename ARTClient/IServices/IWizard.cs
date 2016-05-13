using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IWizard" here, you must also update the reference to "IWizard" in Web.config.
    [ServiceContract]
    public interface IWizard
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        IList<WizardStepInfo> GetWizardStepsByTypeID(int? WizardTypeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<WizardStepDependencyInfo> GetAllDependentSteps( AppUserInfo oAppUserInfo);
    }
}
