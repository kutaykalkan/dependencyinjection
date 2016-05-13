using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IGeographyStructureHdr" here, you must also update the reference to "IGeographyStructureHdr" in Web.config.
    [ServiceContract]
    public interface IGeographyStructureHdr
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        int InsertGeographyStructureHdr(List<GeographyStructureHdrInfo> oGeoStructHdrInfoCollection
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, bool isActive
            , DateTime dateAdded, string addedBy, short companyGeographyClassID, AppUserInfo oAppUserInfo);

        bool IsGeographyStructurePresentByCompanyID(int companyID, AppUserInfo oAppUserInfo);
    }
}
