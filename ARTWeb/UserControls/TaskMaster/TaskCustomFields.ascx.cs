using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;

public partial class UserControls_TaskCustomFields : UserControlBase
{

  
    List<TaskCustomFieldInfo> _oTaskCustomFieldInfoList;

    protected void Page_Load(object sender, EventArgs e)
    {
        ucInputRequirements.ShowInputRequirements(2693);
    }

    public void PopulateData()
    {
        List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = TaskMasterHelper.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID);      
        ViewState[ViewStateConstants.TASK_CUSTOM_FIELDS_CURRENT_DB_VAL] = oTaskCustomFieldInfoList;
        rgTaskCustomFields.DataSource = oTaskCustomFieldInfoList;
        rgTaskCustomFields.DataBind();     
    }

    protected void rgTaskCustomFields_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                TaskCustomFieldInfo oTaskCustomFieldInfo = (TaskCustomFieldInfo)e.Item.DataItem;

                ExLabel lblTaskCustomField = (ExLabel)e.Item.FindControl("lblTaskCustomField");
                ExTextBox txtTaskCustomFieldValue = (ExTextBox)e.Item.FindControl("txtTaskCustomFieldValue");
                lblTaskCustomField.Text = Helper.GetDiscriptionStringValue(oTaskCustomFieldInfo.CustomField);
                if (!string.IsNullOrEmpty(oTaskCustomFieldInfo.CustomFieldValue))
                    txtTaskCustomFieldValue.Text = oTaskCustomFieldInfo.CustomFieldValue;
                else
                    txtTaskCustomFieldValue.Text = "";
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            TaskCustomFieldInfo oTaskCustomFieldInfoTamp = null;
            List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = new List<TaskCustomFieldInfo>();
            _oTaskCustomFieldInfoList = (List<TaskCustomFieldInfo>)ViewState[ViewStateConstants.TASK_CUSTOM_FIELDS_CURRENT_DB_VAL];
            DateTime updateTime = DateTime.Now;
            foreach (GridDataItem item in rgTaskCustomFields.Items)
            {
                short TaskCustomFieldID = (short)item.GetDataKeyValue("TaskCustomFieldID");
                ExTextBox txtTaskCustomFieldValue = (ExTextBox)item.FindControl("txtTaskCustomFieldValue");
                if (TaskCustomFieldID > 0 && txtTaskCustomFieldValue != null)
                {
                    TaskCustomFieldInfo oTaskCustomFieldInfo = new TaskCustomFieldInfo();
                    oTaskCustomFieldInfo.TaskCustomFieldID = TaskCustomFieldID;
                    oTaskCustomFieldInfo.CustomFieldValue = txtTaskCustomFieldValue.Text;

                    if (_oTaskCustomFieldInfoList != null)
                        oTaskCustomFieldInfoTamp = _oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == TaskCustomFieldID);
                    if (oTaskCustomFieldInfoTamp != null && !string.IsNullOrEmpty(txtTaskCustomFieldValue.Text) && oTaskCustomFieldInfoTamp.CustomFieldValue != txtTaskCustomFieldValue.Text)
                        oTaskCustomFieldInfo.CustomFieldValueLabelID = LanguageUtil.InsertPhrase(txtTaskCustomFieldValue.Text, null, AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value, SessionHelper.GetUserLanguage(), 4, null);
                    else
                        oTaskCustomFieldInfo.CustomFieldValueLabelID = oTaskCustomFieldInfoTamp.CustomFieldValueLabelID;
                    if (string.IsNullOrEmpty(txtTaskCustomFieldValue.Text))
                    {
                        oTaskCustomFieldInfo.CustomFieldValueLabelID = null;
                        oTaskCustomFieldInfo.CustomFieldValue = null;
                    }
                    oTaskCustomFieldInfoList.Add(oTaskCustomFieldInfo);
                }
            }
            if (oTaskCustomFieldInfoList.Count > 0)
            {
                TaskMasterHelper.SaveTaskCustomFields(oTaskCustomFieldInfoList, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, SessionHelper.CurrentUserLoginID, DateTime.Now);
                SessionHelper.ClearTaskCustomFieldInfoList();
                SessionHelper.ClearSessionReportColumnInfoList((short)WebEnums.Reports.TASK_COMPLETION_REPORT, true);
                CacheHelper.ClearCacheReportColumnInfoList((short)WebEnums.Reports.TASK_COMPLETION_REPORT, true);
            }
            Helper.RedirectToHomePage(2953);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        }
    }
}
