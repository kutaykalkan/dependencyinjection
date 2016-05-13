using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IGeography" here, you must also update the reference to "IGeography" in Web.config.
    [ServiceContract]
    public interface IGeography
    {
        ///// <summary>
        ///// Selects all geography structure for a company
        ///// </summary>
        ///// <param name="companyID">Unique identifier of a company</param>
        ///// <returns>List of geography structures</returns>
        //[OperationContract]
        //List<GeographyStructureHdrInfo> SelectAllGeographyStructureByCompanyID(int companyID);
    }
}
