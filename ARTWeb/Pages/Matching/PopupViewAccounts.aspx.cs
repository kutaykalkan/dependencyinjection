using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;

public partial class Pages_Matching_PopupViewAccounts : PopupPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = LanguageUtil.GetValue(2222);
        string matchingSourceName = "";
        long matchingSourceDataImportID = 0;
        if (Request.QueryString[QueryStringConstants.MATCHING_SOURCE_NAME] != null)
        {
            matchingSourceName = Request.QueryString[QueryStringConstants.MATCHING_SOURCE_NAME];
        }
        if (Request.QueryString[QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID] != null)
        {
            matchingSourceDataImportID =Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID]);
        }
      
          lblDataSourceName.Text = LanguageUtil.GetValue(2191) + " : " + matchingSourceName;
          BindAccountsGrid(matchingSourceDataImportID);
          GridHelper.SetRecordCount(radViewAccounts);
    }

    private void BindAccountsGrid(long matchingSourceDataImportID)
    {
        List<GLDataHdrInfo> oGLDataHdrInfoCollection=new List<GLDataHdrInfo>();
        MatchingParamInfo oMatchingParamInfo=new MatchingParamInfo();
        oMatchingParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oMatchingParamInfo.MatchingSourceDataImportID = matchingSourceDataImportID;
        oGLDataHdrInfoCollection = MatchingHelper.GetAccountDetails(oMatchingParamInfo);
        if (oGLDataHdrInfoCollection != null)
        {
            radViewAccounts.DataSource = MatchingHelper.GetAccountDetails(oMatchingParamInfo);
            radViewAccounts.EntityNameLabelID = 2223;
            radViewAccounts.DataBind();
        }
    }

    protected void radViewAccounts_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataHdrInfo oGLDataHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

            ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
            ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
            ExLabel lblGLBalance = (ExLabel)e.Item.FindControl("lblGLBalance");

            if (lblAccountNumber != null)
            {
                lblAccountNumber.Text = Helper.GetDisplayStringValue(oGLDataHdrInfo.AccountNumber);

            }

            if (lblAccountName != null)
            {
                lblAccountName.Text = Helper.GetDisplayStringValue(oGLDataHdrInfo.AccountName);

            }

            if (lblGLBalance != null)
            {
                lblGLBalance.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfo.GLBalanceReportingCurrency);

            }
            

        }
    }


}
