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
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes.UserControl;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using System.Collections.Generic;
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Client.Model.RecControlCheckList;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.Services;
namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for RecControlCheckListHelper
    /// </summary>
    public class RecControlCheckListHelper
    {
        public RecControlCheckListHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Service Calls"
        /// <summary>
        /// Gets the GLData RecControl CheckList Comment Info list.
        /// </summary>
        /// <param name="GLDataRecControlCheckListID">GLDataRecControlCheckListID.</param>
        /// <returns></returns>
        public static List<GLDataRecControlCheckListCommentInfo> GetGLDataRecControlCheckListCommentInfoList(long? GLDataRecControlCheckListID)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.RecControlChecklist, SessionHelper.CurrentReconciliationPeriodID))
                return null;
            IRecControlCheckList oRecControlCheckListClient = RemotingHelper.GetRecControlCheckListObject();
            List<GLDataRecControlCheckListCommentInfo> oGLDataRecControlCheckListCommentInfoList = oRecControlCheckListClient.GetGLDataRecControlCheckListCommentInfoList(GLDataRecControlCheckListID, Helper.GetAppUserInfo());
            return oGLDataRecControlCheckListCommentInfoList;
        }
        /// <summary>
        /// Gets the GLData RecControl CheckList Comment Info list.
        /// </summary>
        /// <param name="GLDataRecControlCheckListID">GLDataRecControlCheckListID.</param>
        /// <returns></returns>
        public static void SaveGLDataRecControlCheckListComment(GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo)
        {
            IRecControlCheckList oRecControlCheckList = RemotingHelper.GetRecControlCheckListObject();
            oRecControlCheckList.SaveGLDataRecControlCheckListComment(oGLDataRecControlCheckListCommentInfo, Helper.GetAppUserInfo());
        }
        /// <summary>
        /// Gets the GLData RecControl CheckList Comment Info list.
        /// </summary>
        /// <param name="GLDataRecControlCheckListID">GLDataRecControlCheckListID.</param>
        /// <returns></returns>
        public static List<RecControlCheckListInfo> GetRecControlCheckListInfoList(long? GlDataID, int? RecPeriodID)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.RecControlChecklist, SessionHelper.CurrentReconciliationPeriodID))
                return null;
            List<RecControlCheckListInfo> oRecControlCheckListInfoList = null;
            IRecControlCheckList oRecControlCheckListClient = RemotingHelper.GetRecControlCheckListObject();
            oRecControlCheckListInfoList = oRecControlCheckListClient.GetRecControlCheckListInfoList(GlDataID, RecPeriodID, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelRecControlCheckListInfoList(oRecControlCheckListInfoList);
            return oRecControlCheckListInfoList;

        }

        public static void InsertDataImportRecControlChecklist(DataImportHdrInfo oDataImportHdrInfo, List<RecControlCheckListInfo> oRecControlCheckListInfoList, out int rowAffected)
        {
            IRecControlCheckList oRecControlCheckList = RemotingHelper.GetRecControlCheckListObject();
            oRecControlCheckList.InsertDataImportRecControlChecklist(oDataImportHdrInfo, oRecControlCheckListInfoList, Helper.GetLabelIDValue(1050), out rowAffected, Helper.GetAppUserInfo());
        }
        #endregion

        public static void DeleteRecControlCheckListItems(List<RecControlCheckListInfo> SelectedRecControlCheckListInfoList)
        {
            IRecControlCheckList oRecControlCheckList = RemotingHelper.GetRecControlCheckListObject();
            oRecControlCheckList.DeleteRecControlCheckListItems(SelectedRecControlCheckListInfoList, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserLoginID, System.DateTime.Now, Helper.GetAppUserInfo());
        }

        public static long? SaveGLDataRecControlCheckListAndComment(GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo, GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo)
        {
            IRecControlCheckList oRecControlCheckList = RemotingHelper.GetRecControlCheckListObject();
            return oRecControlCheckList.SaveGLDataRecControlCheckListAndComment(oGLDataRecControlCheckListInfo, oGLDataRecControlCheckListCommentInfo, Helper.GetAppUserInfo());
        }
        public static List<RecControlCheckListAccountInfo> InsertAccountRecControlChecklist(List<RecControlCheckListInfo> oRecControlCheckListInfoList, List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList, long? GLDataID)
        {
            IRecControlCheckList oRecControlCheckList = RemotingHelper.GetRecControlCheckListObject();
            return oRecControlCheckList.InsertAccountRecControlChecklist(oRecControlCheckListInfoList, oRecControlCheckListAccountInfoList, GLDataID, Helper.GetAppUserInfo());
        }
        public static void InsertDataImportRecControlChecklistAccount(DataImportHdrInfo oDataImportHdrInfo, List<RecControlCheckListInfo> oRecControlCheckListInfoList,
            long? AccountID, int? NetAccountID, long? GLDataID, out int rowAffected)
        {
            IRecControlCheckList oRecControlCheckList = RemotingHelper.GetRecControlCheckListObject();
            oRecControlCheckList.InsertDataImportRecControlChecklistAccount(oDataImportHdrInfo, oRecControlCheckListInfoList, Helper.GetLabelIDValue(1050), out rowAffected, AccountID, NetAccountID, GLDataID, Helper.GetAppUserInfo());
        }
        public static GLDataRecControlCheckListInfo GetRecControlCheckListCount(long? GLDataID)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.RecControlChecklist, SessionHelper.CurrentReconciliationPeriodID))
                return null;
            IRecControlCheckList oRecControlCheckListClient = RemotingHelper.GetRecControlCheckListObject();
            return oRecControlCheckListClient.GetRecControlCheckListCount(GLDataID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        }
        public static void SetRecControlCheckListCounts(long? GLDataID, ExLabel lblRecControlTotalValue, ExLabel lblRecControlCompletedValue, HiddenField lblReviewedRecStatusCount)
        {
            GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo = GetRecControlCheckListCount(GLDataID);
            if (oGLDataRecControlCheckListInfo != null)
            {
                lblRecControlTotalValue.Text = Helper.GetDisplayIntegerValue(oGLDataRecControlCheckListInfo.TotalCount);
                lblRecControlCompletedValue.Text = Helper.GetDisplayIntegerValue(oGLDataRecControlCheckListInfo.CompletedCount);
                lblReviewedRecStatusCount.Value = Helper.GetDisplayIntegerValue(oGLDataRecControlCheckListInfo.ReviewedCount);
            }
            else
            {
                lblRecControlTotalValue.Text = Helper.GetDisplayIntegerValue(0);
                lblRecControlCompletedValue.Text = Helper.GetDisplayIntegerValue(0);
                lblReviewedRecStatusCount.Value = Helper.GetDisplayIntegerValue(0);
            }
        }
        public static List<RCCValidationTypeMstInfo> GetRCCValidationTypeMstInfoList()
        {
            IRecControlCheckList oRecControlCheckList = RemotingHelper.GetRecControlCheckListObject();
            return oRecControlCheckList.GetRCCValidationTypeMstInfoList(Helper.GetAppUserInfo());
        }
    }
}