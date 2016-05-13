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
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Data;

public partial class Pages_MaterialityUnexplainedthresholdDetail : PopupPageBaseRecPeriod
{

    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        lblRecPeriod.Text = string.Format(LanguageUtil.GetValue(1919), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate)) + ":";
        PopupHelper.SetPageTitle(this, 1815);
        ShowData();
    }

    #endregion

    #region Grid Events
    protected void rdFSCaptionwideMateriality_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            ExLabel lblFSCaptionValueDetail = e.Item.FindControl("lblFSCaptionValueDetail") as ExLabel;
            ExLabel lblFSCaptionName = e.Item.FindControl("lblFSCaptionName") as ExLabel;
            FSCaptionInfo_ExtendedWithMaterialityInfo oFSCaptionWithMaterialityInfo = new FSCaptionInfo_ExtendedWithMaterialityInfo();
            oFSCaptionWithMaterialityInfo = (FSCaptionInfo_ExtendedWithMaterialityInfo)e.Item.DataItem;
            lblFSCaptionName.Text = oFSCaptionWithMaterialityInfo.FSCaption;
            lblFSCaptionValueDetail.Text = Helper.GetDisplayReportingCurrencyValue(oFSCaptionWithMaterialityInfo.MaterialityThreshold);

        }
    }
    #endregion

    #region Other Events
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = "";
        url = Helper.GetHomePageUrl();

        Response.Redirect(url);
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void SetTBCompanyUnexplainedVarianceThreshold()
    {
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<CompanyUnexplainedVarianceThresholdInfo> oCompanyUnexplainedVarianceThresholdInfoCollection = oCompanyClient.GetUnexplainedVarianceThresholdByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (oCompanyUnexplainedVarianceThresholdInfoCollection != null && oCompanyUnexplainedVarianceThresholdInfoCollection.Count > 0)
        {
            if (oCompanyUnexplainedVarianceThresholdInfoCollection[0].CompanyUnexplainedVarianceThreshold.HasValue)
            {
                lblUnexplainedThresholdValue.Text = Helper.GetDisplayReportingCurrencyValue(oCompanyUnexplainedVarianceThresholdInfoCollection[0].CompanyUnexplainedVarianceThreshold);
            }
            else
            {
                //TODO: put default value
                lblUnexplainedThresholdValue.Text = WebConstants.HYPHEN;
            }
            //_isCompanyUnexplainedVarianceThresholdForwarded = oCompanyUnexplainedVarianceThresholdInfoCollection[0].IsCarryForwardedFromPreviousRecPeriod;
        }
        else
        {
            //TODO: put default value
            lblUnexplainedThresholdValue.Text = WebConstants.HYPHEN;
        }
    }
    #endregion

    #region Other Methods
    protected void BindForCompanyWideDDLOption()
    {
        IList<CompanySettingInfo> oCompanySettingInfoCollection;
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();

        oCompanySettingInfoCollection = oCompanyClient.SelectCompanyMaterialityType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].CompanyMaterialityThreshold != null)
        {
            lblCompanyWideMaterialityValue.Text = Helper.GetDisplayReportingCurrencyValue(oCompanySettingInfoCollection[0].CompanyMaterialityThreshold);
        }
        else
        {
            lblCompanyWideMaterialityValue.Text = "";
        }
    }

    protected void BindForFSCaptionDDLOption()
    {
        IFSCaption oFSCaptionClient = RemotingHelper.GetFSCaptioneObject();
        IList<FSCaptionInfo_ExtendedWithMaterialityInfo> oFSCaptionInfoCollection;
        oFSCaptionInfoCollection = oFSCaptionClient.SelectAllFSCaptionMergeMaterilityByReconciliationPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        oFSCaptionInfoCollection = LanguageHelper.TranslateLabelFSCaptionInfo(oFSCaptionInfoCollection);
        rdFSCaptionwideMateriality.DataSource = oFSCaptionInfoCollection;
        rdFSCaptionwideMateriality.DataBind();
    }
    protected bool? DecideYseNoSelectionFromDB(IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection)
    {

        bool? _isMaterialityYesChecked = null;
        foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oCompanyCapabilityInfoCollection)
        {
            ARTEnums.Capability eCapability = (ARTEnums.Capability)oCompanyCapabilityInfo.CapabilityID;

            switch (eCapability)
            {
                case ARTEnums.Capability.MaterialitySelection://Materiality Selection
                    _isMaterialityYesChecked = oCompanyCapabilityInfo.IsActivated;

                    break;
            }
        }
        return _isMaterialityYesChecked;
    }
    protected void ShowData()
    {
        SetTBCompanyUnexplainedVarianceThreshold();

        IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
        bool? Check = DecideYseNoSelectionFromDB(oCompanyCapabilityInfoCollection);
        if (Check != null)
        {

            if (Check == true)
            {
                BindForCompanyWideDDLOption();
                if (lblCompanyWideMaterialityValue.Text == "")
                {
                    BindForFSCaptionDDLOption();
                    rowCompanyWideMateriality.Visible = false;
                    rdFSCaptionwideMateriality.Visible = true;
                }
                else
                {
                    rdFSCaptionwideMateriality.Visible = false;
                    rowCompanyWideMateriality.Visible = true;
                }
                trNoMateriality.Visible = false;

            }

            else
            {
                rowCompanyWideMateriality.Visible = false;
                rdFSCaptionwideMateriality.Visible = false;

                trNoMateriality.Visible = true;
                lblMateriality.Text = Helper.GetLabelIDValue(1812);

            }

        }

        else
        {
            rowCompanyWideMateriality.Visible = false;
            rdFSCaptionwideMateriality.Visible = false;
            lblMateriality.Text = WebConstants.HYPHEN;
        }
    }
    #endregion

}
