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
    // NOTE: If you change the interface name "IGLDataWriteOnOff" here, you must also update the reference to "IGLDataWriteOnOff" in Web.config.
    [ServiceContract]
    public interface IGLDataWriteOnOff
    {
        [OperationContract]
        List<GLDataWriteOnOffInfo> GetTotalGLDataWriteOnByGLDataID(long? gLDataID, int? templateAttributeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataWriteOnOffInfo> GetGLDataWriteOnOffInfoCollectionByGLDataID(long? gLDataID, int? templateAttributeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertGLDataWriteOnOff(GLDataWriteOnOffInfo GLDataWriteOnOffInfo, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Inserts GL Data Write Off/On and recalculates balances in gl data
        /// </summary>
        /// <param name="oGLDataWriteOnOffInfo">Object containing Write Off/On info</param>
        /// <param name="recCategoryTypeID">Category type of write off/on</param>
        /// <param name="recPeriodID">Unique identifier for current rec period Id</param>
        [OperationContract]
        void InsertGLDataWriteOnOff(GLDataWriteOnOffInfo oGLDataWriteOnOffInfo, short? recCategoryID, short? recCategoryTypeID, int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateGLDataWriteOnOff(GLDataWriteOnOffInfo GLDataWriteOnOffInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateGLDataWriteOnOff(GLDataWriteOnOffInfo GLDataWriteOnOffInfo, short? recCategoryTypeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteGLDataWriteOnOff(long? gLDataWriteOnOffID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteGLDataWriteOnOff(long? gLDataWriteOnOffID, long? glDataID, short? recCategoryTypeID, string revisedBy, DateTime dateRevised, DataTable dtGLDataWO, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateGLDataWriteOnOffCloseDate(long glDataID
            , List<long> glGLDataWriteOnOffIDCollection
            , DateTime? closeDate
            , string closeComments
            , string journalEntryRef
            , string comments
            , short recCategoryTypeID
            , short recTemplateAttributeID
            , string revisedBy
            , DateTime? dateRevised
            , int recPeriodID
            , AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataWriteOnOffInfo> GetClosedGLDataWriteOnOffItemByMatching(long? GLDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID, AppUserInfo oAppUserInfo);

    }
}
