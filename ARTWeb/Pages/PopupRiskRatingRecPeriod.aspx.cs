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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using System.Collections.Generic;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;

public partial class Pages_PopupRiskRatingRecPeriod : PopupPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Int16? riskRatingID = Convert.ToInt16(Request.QueryString[QueryStringConstants.RISKRATING_ID]);
        lblMessage.Text = string.Format(LanguageUtil.GetValue(1921), Helper.GetRiskRating(riskRatingID)) + ":";
        PopupHelper.SetPageTitle(this, 1920);
        this.BindRecFrequencyGrid();
    }

    private void BindRecFrequencyGrid()
    {
        if (Request.QueryString[QueryStringConstants.RISKRATING_ID] != null)
        {
            IRiskRating oRiskRatingClient = RemotingHelper.GetRiskRatingObject();
            IList<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = oRiskRatingClient.SelectAllRiskRatingReconciliationPeriodByRiskRatingIDAndReconciliationPeriodID(SessionHelper.CurrentReconciliationPeriodID, Convert.ToInt16(Request.QueryString[QueryStringConstants.RISKRATING_ID].ToString()), Helper.GetAppUserInfo());
            radRecFrequency.DataSource = oRiskRatingReconciliationPeriodInfoCollection;
            radRecFrequency.DataBind();
        }
    }

    protected void radRecFrequency_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
                ExLabel lblFinancialYear = (ExLabel)e.Item.FindControl("lblFinancialYear");

                RiskRatingReconciliationPeriodInfo oRiskRatingReconciliationPeriodInfo = (RiskRatingReconciliationPeriodInfo)e.Item.DataItem;
                lblDate.Text = Helper.GetDisplayDate(oRiskRatingReconciliationPeriodInfo.PeriodEndDate);
                lblFinancialYear.Text = Helper.GetDisplayStringValue(oRiskRatingReconciliationPeriodInfo.FinancialYear);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
}
