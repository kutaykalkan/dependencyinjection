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
    // NOTE: If you change the interface name "IGLDataRecItemSchedule" here, you must also update the reference to "IGLDataRecItemSchedule" in Web.config.
    [ServiceContract]
    public interface IGLDataRecItemSchedule
    {
        [OperationContract]
        List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemSchedule(long? gLDataID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, int recPeriodID, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateGLDataRecurringItemScheduleCloseDate(long glDataID, List<long> glRecItemInputIDCollection, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised, int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, DataTable dtGLdataRecurringScheduleIDs, AppUserInfo oAppUserInfo);

        [OperationContract]
        GLDataRecurringItemScheduleInfo GetGLDataRecurringItemScheduleInfo(long? GLDataRecurringItemScheduleID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<GLDataRecurringItemScheduleInfo> GetClosedGLDataRecurringItemSchedule(long? GLDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<AttachmentInfo> CopyRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, DataTable dtGLDataParams, int? recPeriodID, bool CloseSourceRecItem, AppUserInfo oAppUserInfo);

    }
}
