using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;

public partial class Pages_Configuration_RulesAndChecklists : PageBaseRecPeriod
{
    List<CompanySystemReconciliationRuleInfo> _CompanySRARuleInfoCollection = new List<CompanySystemReconciliationRuleInfo>();

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 2420);
            Helper.SetBreadcrumbs(this, 1207, 2420);
          
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        ucSRARuleSelection.PopulateData();
        ucQualityScoreSelection.PopulateData(); 
        if (Helper.IsFeatureActivated(WebEnums.Feature.TaskMaster, SessionHelper.CurrentReconciliationPeriodID))
        {
            trTaskCustomField.Visible = true;
            ucTaskCustomField.PopulateData();
        }
        else
            trTaskCustomField.Visible = false;

        IDataImport objDataImport = RemotingHelper.GetDataImportObject();
        short? keyCount = objDataImport.isKeyMappingDoneByCompanyID((int)SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());

        if (keyCount.HasValue)
        {
            ucMappingUpload.Visible = true;
            ucMappingUpload.PopulateData();
        }
        
    }

    public override string GetMenuKey()
    {
        return "CompanyRulesAndCheckLists";
    }
}

