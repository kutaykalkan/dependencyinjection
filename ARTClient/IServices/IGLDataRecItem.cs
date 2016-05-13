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
    [ServiceContract]
    public interface IGLDataRecItem
    {
        [OperationContract]
        List<GLDataRecItemInfo> GetRecItem(AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataRecItemInfo> GetRecItem(int RecPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets rec item input details for the given GLData and category type
        /// </summary>
        /// <param name="accountID">Unique identifier for account</param>
        /// <param name="recPeriodID">Unique identifier for current Rec Period</param>
        /// <param name="recCategoryTypeID">Unique identifier for rec category type</param>
        /// <param name="glReconciliationItemInputRecordTypeID">Unique identifier for record type for ReconciliationItemInput</param>
        /// <param name="AccountTemplateAttributeID">Unique identifier for Account template Attribute</param>
        /// <returns>List of Item Input information</returns>
        [OperationContract]
        List<GLDataRecItemInfo> GetRecItem(long glDataID, int recPeriodID, short recCategoryTypeID, short glReconciliationItemInputRecordTypeID, short accountTemplateAttributeID, AppUserInfo oAppUserInfo);

        //[OperationContract]
        //List<RecurringItemScheduleReconciliationPeriodInfo> GetItemScheduleDetail(int RecPeriodID, int ScheduleID);

        //[OperationContract]
        //List<RecurringItemScheduleReconciliationPeriodInfo> GetItemScheduleDetail(int RecPeriodID);

        //[OperationContract]
        //List<GLDataRecItemInfo> GetRecInputItemByItemID(long GLRecInputItemID);

        [OperationContract]
        void UpdateRecInputItem(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertRecInputItem(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, int recPeriodID, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteRecInputItem(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, DataTable dtGLDataParams, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataRecItemInfo> GetTotalByReconciliationCategoryTypeID(long? gLDataID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Updates GL Rec Item Input with close date
        /// </summary>
        /// <param name="glDataID">Unique identifier for GLData</param>
        /// <param name="glRecItemInputIDCollection">List of Unique identifier for GL Rec Item input</param>
        /// <param name="closeDate">Date when the rec item is closed</param>
        /// <param name="closeComments">Comments for closing the item</param>
        /// <param name="journalEntryRef">Journal entry reference number</param>
        /// <param name="comments">Comments entered by the user</param>
        /// <param name="recCategoryTypeID">Unique identifier for Reconciliation category type</param> 
        /// <param name="recTemplateAttributeID">Unique identifier for Rec Tempalte Attribute</param>
        /// <param name="revisedBy">Login id of the user who is updating the record</param>
        /// <param name="dateRevised">current date</param>
        [OperationContract]

        void UpdateGLRecItemCloseDate(long glDataID, List<long> glRecItemInputIDCollection, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised, int recPeriodID, AppUserInfo oAppUserInfo);

        //[OperationContract]
        //List<GLDataRecItemInfo> GetRecItemRecurringSchedule(long accountID, int recPeriodID, short recCategoryTypeID, short glReconciliationItemInputRecordTypeID, short accountTemplateAttributeID);
        [OperationContract]
        List<ReconciliationCategoryMstInfo> GetAllReconciliationCategory(int? recPeriodID, long? AccountID, ref int? RecTemplateID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<GLDataRecurringItemScheduleInfo> GetGLDataRecItemsListByMatchSetSubSetCombinationID(long? MatchSetSubSetCombinationID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<GLDataRecItemInfo> GetClosedGLDataRecItem(long? GLDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID, AppUserInfo oAppUserInfo);
        [OperationContract]
        short? GetGLRecItemTypeID(long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<RecItemCommentInfo> GetRecItemCommentList(long? RecordID, short? RecordTypeID, AppUserInfo oAppUserInfo);
        [OperationContract]
        void SaveRecItemComment(RecItemCommentInfo oRecItemCommentInfo, AppUserInfo oAppUserInfo);
        [OperationContract]
        GLDataRecItemInfo GetGLDataRecItem(long? RecordID, short? RecordTypeID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<AttachmentInfo> CopyRecInputItem(GLDataRecItemInfo oGLRecItemInput, DataTable dtGLDataParams, int? recPeriodID,bool CloseSourceRecItem, AppUserInfo oAppUserInfo);
    }
}
