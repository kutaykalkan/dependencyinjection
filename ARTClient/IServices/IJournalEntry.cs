using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IJournalEntry" here, you must also update the reference to "IJournalEntry" in Web.config.
    [ServiceContract]
    public interface IJournalEntry
    {
        
        [OperationContract]
        List<CompanyGLToolColumnInfo> GetGLToolColumnsByRecPeriodID(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo);


        [OperationContract]
        List<UserHdrInfo> SelectWriteOffOnApproversByCompanyID(int? companyId, AppUserInfo oAppUserInfo);

        [OperationContract]
        void SaveCompanyGLToolColumns(List<CompanyGLToolColumnInfo> oCompanyGLToolColumnInfoCollection, int? CompanyID, int? StartRecPeriodID, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyJEWriteOffOnApproverInfo> GetCompanyJEWriteOffOnApproversByRecPeriodID(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo);


        [OperationContract]
        void SaveCompanyJEWriteOffOnApprovers(List<CompanyJEWriteOffOnApproverInfo> oCompanyJEWriteOffOnApproverInfoCollection, int? CompanyID, int? StartRecPeriodID, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised, AppUserInfo oAppUserInfo);

    }
}
