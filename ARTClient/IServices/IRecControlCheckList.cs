using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Model.RecControlCheckList;

namespace SkyStem.ART.App.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRecControlCheckList" in both code and config file together.
    [ServiceContract]
    public interface IRecControlCheckList
    {
        [OperationContract]
        List<RecControlCheckListInfo> GetRecControlCheckListInfoList(long? GlDataID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<RecControlCheckListInfo> InsertDataImportRecControlChecklist(DataImportHdrInfo oDataImportHdrInfo
          , List<RecControlCheckListInfo> oRecControlCheckListInfoList, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteRecControlCheckListItems(List<RecControlCheckListInfo> SelectedRecControlCheckListInfoList, int? RecPeridID, string RevisedBy, DateTime DateRevised, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataRecControlCheckListCommentInfo> GetGLDataRecControlCheckListCommentInfoList(long? GLDataRecControlCheckListID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void SaveGLDataRecControlCheckListComment(GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        long? SaveGLDataRecControlCheckListAndComment(GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo, GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<RecControlCheckListAccountInfo> InsertAccountRecControlChecklist(List<RecControlCheckListInfo> oRecControlCheckListInfoList, List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList, long? GLDataID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<RecControlCheckListInfo> InsertDataImportRecControlChecklistAccount(DataImportHdrInfo oDataImportHdrInfo
       , List<RecControlCheckListInfo> oRecControlCheckListInfoList, string failureMsg, out int rowAffected, long? AccountID, int? NetAccountID, long? GLDataID, AppUserInfo oAppUserInfo);
        [OperationContract]
        GLDataRecControlCheckListInfo GetRecControlCheckListCount(long? GlDataID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<RCCValidationTypeMstInfo> GetRCCValidationTypeMstInfoList(AppUserInfo oAppUserInfo);

    }
}
