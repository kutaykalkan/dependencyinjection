using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "ISubledger" here, you must also update the reference to "ISubledger" in Web.config.
    [ServiceContract]
    public interface ISubledger
    {
        /// <summary>
        /// Selects all subledger source by company id
        /// </summary>
        /// <param name="companyID">Unique identifier of a company</param>
        /// <returns>List of subledger source</returns>
        [OperationContract]
        List<SubledgerSourceInfo> SelectAllByCompanyID(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        SubledgerDataInfo GetSubledgerDataInfoByAccountIDRecPeriodID(long? AccountID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<SubledgerDataArchiveInfo> GetSubledgerVersionByGLDataID(GLDataParamInfo oGLDataParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        SubledgerDataInfo GetSubledgerDataImportIDByNetAccountIDRecPeriodID(int? NetAccountID, int? RecPeriodID, AppUserInfo oAppUserInfo);

    }
}
