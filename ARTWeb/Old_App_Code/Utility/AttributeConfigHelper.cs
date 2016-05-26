using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;

/// <summary>
/// Summary description for RoleConfigHelper
/// </summary>
public class AttributeConfigHelper
{
    private AttributeConfigHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void SaveCompanyAttributeConfigInfoList(List<CompanyAttributeConfigInfo> oCompanyRoleConfigInfoList, string loginID)
    {
        AttributeConfigParamInfo oRoleConfigParamInfo = new AttributeConfigParamInfo();
        Helper.FillCommonServiceParams(oRoleConfigParamInfo);
        oRoleConfigParamInfo.UserLoginID = loginID;
        oRoleConfigParamInfo.DateRevised = DateTime.Now;
        oRoleConfigParamInfo.AttributeSetTypeID = (int)WebEnums.AttributeSetType.RoleConfig;
        oRoleConfigParamInfo.CompanyRoleConfigInfoList = oCompanyRoleConfigInfoList;
        IAttributeConfiguration oRoleConfig = RemotingHelper.GetRoleConfigObject();
        oRoleConfig.SaveAttributeConfig(oRoleConfigParamInfo,Helper.GetAppUserInfo());
    }

    public static List<CompanyAttributeConfigInfo> GetCompanyAttributeConfigInfoList(bool? enabledOnly,WebEnums.AttributeSetType attributeTypeEnum)
    {
        AttributeConfigParamInfo oRoleConfigParamInfo = new AttributeConfigParamInfo();
        Helper.FillCommonServiceParams(oRoleConfigParamInfo);
        oRoleConfigParamInfo.AttributeSetTypeID = (int)attributeTypeEnum;
        oRoleConfigParamInfo.EnabledOnly = enabledOnly;
        IAttributeConfiguration oRoleConfig = RemotingHelper.GetRoleConfigObject();
        List<CompanyAttributeConfigInfo> oCompanyRoleConfigInfoList = oRoleConfig.GetCompanyAttributeConfigInfoList(oRoleConfigParamInfo,Helper.GetAppUserInfo());
        LanguageHelper.TranslateLabelsCompanyRoleConfigData(oCompanyRoleConfigInfoList);
        return oCompanyRoleConfigInfoList;
    }
}
