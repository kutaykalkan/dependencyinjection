using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IColumnOperatorMapping" here, you must also update the reference to "IColumnOperatorMapping" in Web.config.
    [ServiceContract]
    public interface IColumnOperatorMapping
    {
        [OperationContract]
        void DoWork();

        
    }
}
