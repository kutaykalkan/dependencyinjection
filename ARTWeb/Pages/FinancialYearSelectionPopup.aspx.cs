using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Classes;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Text;
using SkyStem.ART.Client.Data;

public partial class FinancialYearSelectionPopup : PopupPageBase
{
    string _postBackControlID = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.POSTBACK_CONTROL_ID] != null)
        {
            _postBackControlID = Request.QueryString[QueryStringConstants.POSTBACK_CONTROL_ID];
        }

        PopupHelper.SetPageTitle(this, 2018);
        if (!IsPostBack)
        {
            lblCurrentFYValue.Text = WebConstants.HYPHEN;
            SetErrorMessage();
            BindDropDownList();
            SetControlData();
        }

    }

    /// <summary>
    /// SetErrorMessage() is used to set error messages to validators.
    /// </summary>
    private void SetErrorMessage()
    {
        rfvFY.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblFYSelection.LabelID);
        cvRecPeriodExist.ErrorMessage = LanguageUtil.GetValue(5000205);
        cvRecPeriodSelected.ErrorMessage = LanguageUtil.GetValue(5000207);
    }

    /// <summary>
    /// Set control's data in page.
    /// </summary>
    private void SetControlData()
    {
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        FinancialYearHdrInfo oFinancialYearHdrInfo = oCompanyClient.GetFinancialYearByID(SessionHelper.CurrentFinancialYearID, Helper.GetAppUserInfo());
        if (oFinancialYearHdrInfo != null)
            lblCurrentFYValue.Text = oFinancialYearHdrInfo.FinancialYear;
        else
            lblCurrentFYValue.Text = WebConstants.HYPHEN ;

        // show the Current FY in Dropdown
        if (SessionHelper.CurrentFinancialYearID != null)
        {
            ListItem oListItem = ddlFySelection.Items.FindByValue(SessionHelper.CurrentFinancialYearID.Value.ToString());
            if (oListItem != null)
            {
                ddlFySelection.ClearSelection();
                oListItem.Selected = true;
            }
        }
    }

    /// <summary>
    /// Bind Drop Down List
    /// </summary>
    private void BindDropDownList()
    {
        IList<FinancialYearHdrInfo> oFinancialYearHdrInfoCollection = CacheHelper.GetAllFinancialYears();
        ddlFySelection.DataSource = oFinancialYearHdrInfoCollection;
        ddlFySelection.DataTextField = "FinancialYear";
        ddlFySelection.DataValueField = "FinancialYearID";
        ddlFySelection.DataBind();
        ListControlHelper.AddListItemForSelectOne(ddlFySelection);
    }
    /// <summary>
    /// btnSetFY Click event handler.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetFY_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            // Clear the FY
            HttpContext.Current.Session.Remove(SessionConstants.CURRENT_FINANCIAL_YEAR_ID);

            Int32 currentFYID = Convert.ToInt32(ddlFySelection.SelectedValue);
            if (currentFYID > 0)
            {
                SessionHelper.CurrentFinancialYearID = currentFYID;
                lblCurrentFYValue.Text = ddlFySelection.SelectedItem.Text;
                // Save Auto Save Attribute Values
                List<ARTEnums.AutoSaveAttribute> eAutoSaveEnumList = new List<ARTEnums.AutoSaveAttribute>();
                eAutoSaveEnumList.Add(ARTEnums.AutoSaveAttribute.AutoSaveFinancialYearSelection);
                Helper.SaveAutoSaveAttributeValues(eAutoSaveEnumList);

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ParentPageCallbackFunction", ScriptHelper.GetJSForParentPageCallbackFunction("SetIsPostBackFromFinancialYearSelectionScreen"));

                if (_postBackControlID != null)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndPostBackParentPage", ScriptHelper.GetJSForClosePopupAndPostbackParentPage(_postBackControlID));
                }
                else
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndPostBackParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
                }
            }
        }
    }

    /// <summary>
    /// HasRecPeriodInFY() is used to check that FY has RecPeriods or not.
    /// </summary>
    /// <param name="fyID"></param>
    /// <returns></returns>
    private bool HasRecPeriodInFY()
    {
        try
        {
            Int32 selectedFY = Convert.ToInt32(ddlFySelection.SelectedValue);
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, selectedFY, Helper.GetAppUserInfo());
            if (oReconciliationPeriodInfoCollection.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// cvRecPeriodExist On Server Validate event handler.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvRecPeriodExist_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Page.IsValid)
        {
            if (HasRecPeriodInFY())
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    /// <summary>
    /// Validation to check current and selected FY is same or different.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvRecPeriodSelected_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Page.IsValid)
        {
            if (SessionHelper.CurrentFinancialYearID != Convert.ToInt32( ddlFySelection.SelectedValue))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    

}
