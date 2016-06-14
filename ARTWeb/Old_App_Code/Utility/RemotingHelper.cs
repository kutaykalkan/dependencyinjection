using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.Services;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.IServices.BulkExportToExcel;
using SkyStem.ART.App.Services.BulkExportToExcel;
/// <summary>
/// Summary description for RemotingHelper
/// </summary>
public class RemotingHelper
{
    public RemotingHelper()
    {
        //
        // TODO: Add constructor logic here
        // asdhas
    }
   
    public static ICompany GetCompanyObject()
    {
        return new Company();
    }   

    public static IUser GetUserObject()
    {
        return new SkyStem.ART.App.Services.User();
    }

    public static IUtility GetUtilityObject()
    {
        return new Utility();
    }

    public static IRole GetRoleObject()
    {
        return new Role();
    }
    public static IGLDataRecItem GetGLDataRecItemObject()
    {
        return new GLDataRecItem();
    }
    public static IGLDataRecItemSchedule GetGLDataRecItemScheduleObject()
    {
        return new GLDataRecItemSchedule();
    }
    public static IRiskRating GetRiskRatingObject()
    {
        return new RiskRating();
    }

    public static IFSCaption GetFSCaptioneObject()
    {
        return new FSCaption();
    }

    public static IReport GetReportObject()
    {
        return new Report();
    }
    public static IUnexplainedVariance GetUnexplainedVarianceObject()
    {
        return new UnexplainedVariance();
    }

    public static IGLData GetGLDataObject()
    {
        return new GLData();
    }
    public static IGLDataWriteOnOff GetGLDataWriteOnOffObject()
    {
        return new GLDataWriteOnOff();
    }

    public static ICertificationStatus GetCertificationStatusObject()
    {
        return new CertificationStatus();
    }

    public static IAccount GetAccountObject()
    {
        return new Account();
    }
    public static IAttachment GetAttachmentObject()
    {
        return new Attachment();
    }

    //public static IReviewNote GetReviewNoteObject()
    //{
    //    return new ReviewNote();
    //}

    public static IDataImport GetDataImportObject()
    {
        return new DataImport();
    }

    public static ISubledger GetSubledgerObject()
    {
        return new Subledger();
    }

    public static IReconciliation GetReconciliationObject()
    {
        return new Reconciliation();
    }

    public static IGeography GetGeographyObject()
    {
        return new Geography();
    }
    public static IReconciliationPeriod GetReconciliationPeriodObject()
    {
        return new ReconciliationPeriod();
    }
    public static IGeographyStructureHdr GetGeographyStructureHdrObject()
    {
        return new GeographyStructureHdr();
    }
    public static IHolidayCalendar GetHolidayCalendarObject()
    {
        return new HolidayCalendar();
    }
    public static IReason GetReasonObject()
    {
        return new Reason();
    }
    public static ICertification GetCertificationObject()
    {
        return new Certification();
    }

     public static IDashboard GetDashboardObject()
    {
        return new Dashboard();
    }


    public static IReportParameter GetReportParameterObject()
    {
        return new ReportParameter();
    }

    public static IAlert GetAlertObject()
    {
        return new Alert();
    }
    public static IReconciliationStatus GetRecStatusObject()
    {
        return new ReconciliationStatus();
    }
    public static IReconciliationCategory GetReconciliationCategoryObject()
    {
        return new ReconciliationCategory();
    }
    public static IAging GetAgingObject()
    {
        return new Aging();
    }
    public static IReportParameterKey  GetReportParameterKeyObject()
    {
        return new ReportParameterKey();
    }
    public static IReportArchive GetReportArchiveObject()
    {
        return new ReportArchive();
    }
    public static IOperator GetOperatorObject()
    {
        return new Operator ();
    }

    public static IAppSetting GetAppSettingObject()
    {
        return new AppSetting();
    }


    public static IWeekDay GetWorkweekObject()
    {
        return new WeekDay();
    }

    public static IAttributeConfiguration GetRoleConfigObject()
    {
        return new AttributeConfiguration();
    }

    public static IPackage GetPackageObject()
    {
        return new Package();
    }

    public static IJournalEntry GetJournalEntryObject()
    {
        return new JournalEntry();
    }

    public static IMatching GetMatchingObject()
    {
        return new Matching();
    }
    public static IWizard GetWizardObject()
    {
        return new SkyStem.ART.App.Services.Wizard();
    }
    public static IQualityScore GetQualityScoreObject()
    {
        return new QualityScore();
    }

    public static IMappingUpload GetMappingUploadObject()
    {
        return new MappingUpload();
    }
    public static IQualityScoreReports GetQualityScoreReportObject()
    {
        return new QualityScoreReports();
    }
    public static IBulkExportToExcel GetBulkExportObject()
    {
        return new ExportToExcel();
    }

    public static ISystemLockdown GetSystemLockdownObject()
    {
        return new SystemLockdown();
    }
    public static ITaskMaster GetTaskMasterObject()
    {
        return new TaskMaster();
    }
    public static IRecControlCheckList GetRecControlCheckListObject()
    {
        return new RecControlCheckList();
    }
}//end of class
