using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageClient.Model;
using System.Data;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Shared.Utility;

public partial class Pages_Multilingual_DownloadMultilingualData : PopupPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 1697);
        PopupHelper.ShowInputRequirementSection(this, 2479, 2481);
        SetErrorMessage();
        if (!Page.IsPostBack)
        {
            ListControlHelper.BindLanguageDropdown(ddlFromLanguage, true, false, true);
            ListControlHelper.BindLanguageDropdown(ddlToLanguage, false, false, true);
        }
    }
    /// <summary>
    /// Sets the error message.
    /// </summary>
    private void SetErrorMessage()
    {
        rfvFromLanguage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblFromLanguage.LabelID);
        rfvFromLanguage.InitialValue = WebConstants.SELECT_ONE;
        rfvToLanguage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblToLanguage.LabelID);
        rfvToLanguage.InitialValue = WebConstants.SELECT_ONE;
    }

    protected void btnDownload_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int applicationID = AppSettingHelper.GetApplicationID();
            int businessEntityID = AppSettingHelper.GetDefaultBusinessEntityID();
            if (SessionHelper.CurrentRoleID != (short)WebEnums.UserRole.SKYSTEM_ADMIN)
                businessEntityID = SessionHelper.GetBusinessEntityID();
            int fromLanguageID = Convert.ToInt32(ddlFromLanguage.SelectedValue);
            int toLanguageID = Convert.ToInt32(ddlToLanguage.SelectedValue);
            int startLabelID = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.APP_KEY_START_LABEL_ID));
            List<TranslationInfo> oTranslationInfoList = LanguageUtil.GetTranslations(applicationID, businessEntityID, fromLanguageID, toLanguageID, startLabelID, null);
            ConvertToExcelAndDownload(oTranslationInfoList);
        }
    }

    private void ConvertToExcelAndDownload(List<TranslationInfo> oTranslationInfoList)
    {
        DataTable dt = new DataTable(MultilingualUploadConstants.SheetName);
        DataRow dr;
        dt.Columns.Add(new DataColumn(MultilingualUploadConstants.Fields.LabelID));
        dt.Columns.Add(new DataColumn(MultilingualUploadConstants.Fields.FromLanguage));
        dt.Columns.Add(new DataColumn(MultilingualUploadConstants.Fields.ToLanguage));
        foreach (TranslationInfo item in oTranslationInfoList)
        {
            dr = dt.NewRow();
            dr[MultilingualUploadConstants.Fields.LabelID] = item.LabelID;
            dr[MultilingualUploadConstants.Fields.FromLanguage] = DataHelper.ReplaceSpecialChars(item.FromLanguagePhrase);
            dr[MultilingualUploadConstants.Fields.ToLanguage] = DataHelper.ReplaceSpecialChars(item.ToLanguagePhrase);
            dt.Rows.Add(dr);
        }
        string filePath = ExportHelper.GenerateTempFilePath(MultilingualUploadConstants.TemplateName, MultilingualUploadConstants.TemplateExt);
        ExcelHelper.ExportDataToExcel(dt, filePath, MultilingualUploadConstants.SheetName);
        ExportHelper.DownloadAttachment(filePath, MultilingualUploadConstants.TemplateName, true);
    }
}
