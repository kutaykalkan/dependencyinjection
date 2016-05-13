using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Data;
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IReportArchive" here, you must also update the reference to "IReportArchive" in Web.config.
    [ServiceContract]
    public interface IReportArchive
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void SaveArchivedReport(ReportArchiveInfo oRptArchiveInfo, List<ReportArchiveParameterInfo> oRptArchiveParamInfoCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReportArchiveInfo> GetRptActivityByReportIDUserIDRoleIDRecPeriodID(short reportID
            , int userID, short roleID, int recPeriodID, int languageID, int defaultLanguageID, int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        ReportArchiveInfo GetArchivedReportByReportArchiveID(int ReportArchiveID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReportArchiveInfo> GetReportsActivity(int recPeriodID, int languageID, int defaultLanguageID, int companyID, short RoleID, AppUserInfo oAppUserInfo);
    }
}
