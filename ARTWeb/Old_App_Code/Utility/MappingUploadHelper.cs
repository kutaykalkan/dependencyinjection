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
using SkyStem.ART.Client.Model.MappingUpload;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Params.MappingUpload;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for MappingUploadHelper
    /// </summary>
    public class MappingUploadHelper
    {
        public MappingUploadHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static List<MappingUploadInfo> GetMappingUploadInfoList(int? ReconciliationPeriodID,int? CompanyID)
        {
            IMappingUpload oMappingUpload = RemotingHelper.GetMappingUploadObject();
            List<MappingUploadInfo> oMappingUploadInfo = oMappingUpload.GetMappingUploadInfoList(ReconciliationPeriodID, CompanyID, Helper.GetAppUserInfo());
            return oMappingUploadInfo;
        }

        public static bool SaveMappingUploadInfoList(List<MappingUploadInfo> oMappingUploadInfoList, string loginID)
        {
            //if (!Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
            //    return false;
            MappingUploadParamInfo oMappingUploadInfo = new MappingUploadParamInfo();
            Helper.FillCommonServiceParams(oMappingUploadInfo);
            oMappingUploadInfo.UserLoginID = loginID;
            oMappingUploadInfo.DateRevised = DateTime.Now;
            oMappingUploadInfo.MappingUploadInfoList = oMappingUploadInfoList;
            IMappingUpload oMappingUpload = RemotingHelper.GetMappingUploadObject();
            return oMappingUpload.SaveMappingUploadInfoList(oMappingUploadInfo, Helper.GetAppUserInfo());
        }
    }
}
