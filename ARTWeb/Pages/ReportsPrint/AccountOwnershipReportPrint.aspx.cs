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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Report;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_ReportsPrint_AccountOwnershipReportPrint : PageBaseReport
{
    #region Variable
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;

    short? dSumTotalAccountAssigned = 0;
    short? dSumCountHighAccounts = 0;
    short? dSumCountMediumAccounts = 0;
    short? dSumCountLowAccounts = 0;
    short? dSumCountKeyAccounts = 0;
    short? dSumCountMaterialAccount = 0;

    #endregion 

    #region Properties
    public string TotalReconcilableAccountsValue
    {
        set { lblTotalRecAccounts.Text = value; }
        get
        {
            return lblTotalRecAccounts.Text;

        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
        _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);

        List<AccountOwnershipReportInfo> oAccountOwnershipReportInfoCollection = null;
        oAccountOwnershipReportInfoCollection = (List<AccountOwnershipReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_ACCOUNT_OWNERSHIP];
        
        //11112010 - Set Total Reconcilable Accounts Value
        if (oAccountOwnershipReportInfoCollection != null && oAccountOwnershipReportInfoCollection[0] != null)
        {
            if (oAccountOwnershipReportInfoCollection[0].CountTotalAccounts != null)
                this.TotalReconcilableAccountsValue = oAccountOwnershipReportInfoCollection[0].CountTotalAccounts.ToString();
        }

        GridHelper.ShowHideColumnsBasedOnFeatureCapability(rgReport.MasterTableView);
        rgReport.DataSource = oAccountOwnershipReportInfoCollection;
    }


    protected void rgReport_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundAccountOwnershipReport(e, ref dSumTotalAccountAssigned,
            ref dSumCountHighAccounts, ref dSumCountMediumAccounts, ref dSumCountLowAccounts, ref dSumCountKeyAccounts,
            ref dSumCountMaterialAccount);
    }



    #region private methods related to labels showing totals (same on main and peint page of the report)
    
    private string GetTextForTotalLabels(int labelID)
    {
        string message = string.Format(LanguageUtil.GetValue(1981) + ": ", LanguageUtil.GetValue(labelID));
        return message;
    }

    #endregion





}//end of class
