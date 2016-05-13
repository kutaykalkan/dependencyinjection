using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.UserControls.Matching.Wizard;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Exception;

public partial class Pages_Matching_MatchingWizard : PageBaseMatching
{
    List<WizardStepInfo> oWizardStepInfoCollection = null;
    private long? _GLDataID = null;
    private long? _MatchSetID = null;
    private long? _AccountID = null;
    private ARTEnums.MatchingType? _CurrentMatchingType = ARTEnums.MatchingType.DataMatching;

    protected void Page_Load(object sender, EventArgs e)
    {
        SetPageSettings();
        GetQueryStringValues();
        ShowHideStep();
        LoadFirstStep();
        if (!IsPostBack)
        {
            this.ReturnUrl = Helper.ReturnURL(this.Page);
        }

    }

    /// <summary>
    /// Sets the page settings.
    /// </summary>
    private void SetPageSettings()
    {
        Helper.SetPageTitle(this, 2197);
        Helper.SetBreadcrumbs(this, 1071, 2234, 2185, 2197);
        Helper.SetWizardStepTitle(wzMatching);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRoleSelection = false;
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }

    private void DisableNavigationButtons()
    {
        try
        {
            UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[wzMatching.ActiveStepIndex]);
            if (!oUserControlMatchingWizardBase.IsEditMode)
            {
                MatchingHelper.DisableNavigationButtons(wzMatching);
            }
        }
        catch (Exception)
        {
        }
    }



    protected void Page_Init(object sender, EventArgs e)
    {
        //Rad Grid is creating problems with Dynamic Steps
        //CreateWizardSteps();
    }

    /// <summary>
    /// Create Dynamic Steps
    /// </summary>
    public void CreateWizardSteps()
    {
        wzMatching.WizardSteps.Clear();
        IWizard oWizardClient = RemotingHelper.GetWizardObject();
        oWizardStepInfoCollection = (List<WizardStepInfo>)oWizardClient.GetWizardStepsByTypeID((int)WebEnums.WizardType.Matching, Helper.GetAppUserInfo());

        for (int i = 0; i < oWizardStepInfoCollection.Count; i++)
        {
            CreateAndAddStep(oWizardStepInfoCollection[i], i, oWizardStepInfoCollection.Count - 1);
        }
    }

    /// <summary>
    /// Create Dynamic Step
    /// </summary>
    /// <param name="oWizardStepInfo"></param>
    /// <param name="i"></param>
    /// <param name="TotalSteps"></param>
    public void CreateAndAddStep(WizardStepInfo oWizardStepInfo, int i, int TotalSteps)
    {
        //create a wizardstep control
        WizardStep myStep = new WizardStep();

        //create a new usercontrol
        string ucUrl = "~/UserControls/Matching/Wizard/" + oWizardStepInfo.WizardStepURL;
        UserControlMatchingWizardBase oUcontrol = (UserControlMatchingWizardBase)Page.LoadControl(ucUrl);

        oUcontrol.ID = "ucStep" + i.ToString();
        if (i == 0)
        {
            myStep.StepType = WizardStepType.Start;
        }
        else if (i == TotalSteps)
        {
            myStep.StepType = WizardStepType.Finish;
        }
        else
        {
            myStep.StepType = WizardStepType.Step;
        }
        myStep.Controls.Add(oUcontrol);
        wzMatching.WizardSteps.Add(myStep);
    }

    protected void wzMatching_ActiveStepChanged(object sender, EventArgs e)
    {

    }


    protected void wzMatching_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        e.Cancel = !NavigateStep(e.CurrentStepIndex, e.NextStepIndex);
    }

    protected void wzMatching_OnPreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
        e.Cancel = !NavigateStep(e.CurrentStepIndex, e.NextStepIndex);
    }

    protected void wzMatching_OnSideBarButtonClick(object sender, WizardNavigationEventArgs e)
    {
        e.Cancel = !NavigateStep(e.CurrentStepIndex, e.NextStepIndex);
    }

    protected void wzMatching_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
    }

    protected void btnContinueLater_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SaveData(wzMatching.ActiveStepIndex);
            Response.Redirect(URLConstants.URL_MATCHING_VIEW_MATCH_SET, false);
        }
    }

    protected void btnDiscard_OnClick(object sender, EventArgs e)
    {
        //Discard Data
        UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[wzMatching.ActiveStepIndex]);
        if (oUserControlMatchingWizardBase.IsEditMode)
        {
            if (oUserControlMatchingWizardBase.CurrentMatchSetHdrInfo != null && oUserControlMatchingWizardBase.CurrentMatchSetHdrInfo.MatchSetID.HasValue)
            {
                MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                oMatchingParamInfo.MatchSetID = oUserControlMatchingWizardBase.CurrentMatchSetHdrInfo.MatchSetID;
                oMatchingParamInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.Matching;
                oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oMatchingParamInfo.DateRevised = DateTime.Now;
                MatchingHelper.DeleteMatchSet(oMatchingParamInfo);
            }
        }
        Response.Redirect(URLConstants.URL_MATCHING_VIEW_MATCH_SET, false);
    }
    protected void btnUploadNewDataSources_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(URLConstants.URL_MATCHING_SOURCE_DATAIMPORT, false);
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[wzMatching.ActiveStepIndex]);
        if (oUserControlMatchingWizardBase.IsEditMode)
            oUserControlMatchingWizardBase.SubmitData();
        if (_CurrentMatchingType == ARTEnums.MatchingType.AccountMatching)
        {
            if (Session[SessionConstants.PARENT_PAGE_URL] != null)
            {
                int LabelID = 2434;
                String Url = Session[SessionConstants.PARENT_PAGE_URL].ToString();
                string strUrl = QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString();
                if (Url.Contains('?'))
                {
                    if (!Url.Contains(strUrl))
                        Url = Url + "&" + strUrl;
                }
                else
                {
                    if (!Url.Contains(strUrl))
                        Url = Url + "?" + strUrl;                   
                }
                Response.Redirect(Url, true);
            }
        }
        else
        {
            int LabelID = 2434;
            Response.Redirect(URLConstants.URL_MATCHING_VIEW_MATCH_SET + "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
            //Response.Redirect(URLConstants.URL_MATCHING_VIEW_MATCH_SET, false);
        }
    }

    private bool NavigateStep(int fromIndex, int toIndex)
    {
        try
        {
            if (Page.IsValid)
            {
                UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[fromIndex]);
                //if (oUserControlMatchingWizardBase.IsDataChanged)
                //{
                if (SaveData(fromIndex))
                    ClearDataDependents(fromIndex);

                //}
                return LoadData(toIndex);
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        return false;
    }

    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Get Dependent Steps
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private List<WizardStepDependencyInfo> GetDependentSteps(int index)
    {
        List<WizardStepDependencyInfo> oWizardStepDependencyInfoCollection = null;
        UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[index]);
        oWizardStepDependencyInfoCollection = CacheHelper.GetDependentSteps(oUserControlMatchingWizardBase.WizardStepID);
        return oWizardStepDependencyInfoCollection;
    }

    /// <summary>
    /// Save Step Data
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool SaveData(int index)
    {
        UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[index]);
        if (oUserControlMatchingWizardBase.IsEditMode)
            return oUserControlMatchingWizardBase.SaveData();
        return false;
    }

    /// <summary>
    /// Load Step Data
    /// </summary>
    /// <param name="index"></param>
    private bool LoadData(int index)
    {
        UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[index]);
        if (oUserControlMatchingWizardBase.CanLoadStep())
        {
            oUserControlMatchingWizardBase.LoadData();
            oUserControlMatchingWizardBase.SetControlStatePostLoad();
            DisableNavigationButtons();

            if (oUserControlMatchingWizardBase.MatchingWizardStepType == ARTEnums.MatchingWizardSteps.PreviewConfirm
                && oUserControlMatchingWizardBase.IsEditMode)
            {
                Button btnFinishSubmitButton = (Button)wzMatching.FindControl("FinishNavigationTemplateContainerID").FindControl("btnSubmit");
                btnFinishSubmitButton.Enabled = oUserControlMatchingWizardBase.IsConfigurationComplete;

            }

            return true;
        }
        Helper.ShowErrorMessage(this, new ARTException(2310));
        return false;
    }

    /// <summary>
    /// Clear Dependent Steps
    /// </summary>
    /// <param name="index"></param>
    private void ClearDataDependents(int index)
    {
        List<WizardStepDependencyInfo> oWizardStepDependencyInfoCollection = null;
        oWizardStepDependencyInfoCollection = GetDependentSteps(index);
        for (int i = 0; i < oWizardStepDependencyInfoCollection.Count - 1; i++)
        {
            foreach (WizardStep wzStep in wzMatching.WizardSteps)
            {
                UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase(wzStep);
                if (oUserControlMatchingWizardBase.WizardStepID.Value == oWizardStepDependencyInfoCollection[i].DependentWizardStepID.Value)
                {
                    if (oUserControlMatchingWizardBase.IsEditMode)
                        oUserControlMatchingWizardBase.ClearData();
                }
            }
        }
    }

    /// <summary>
    /// Clear Step Data
    /// </summary>
    /// <param name="index"></param>
    private void ClearData(int index)
    {
        UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase((WizardStep)wzMatching.WizardSteps[index]);
        if (oUserControlMatchingWizardBase.IsEditMode)
            oUserControlMatchingWizardBase.ClearData();
    }

    /// <summary>
    /// Refresh Page on Popup Close when requested by Popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        UserControlWizardBase oUserControlWizardBase = Helper.GetUserControlWizardBase((WizardStep)wzMatching.ActiveStep);
        if (oUserControlWizardBase != null)
            oUserControlWizardBase.RefreshData();
    }

    /// <summary>
    /// Get Query String Values
    /// </summary>
    private void GetQueryStringValues()
    {
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
            _GLDataID = long.Parse(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_ID]))
            _MatchSetID = long.Parse(Request.QueryString[QueryStringConstants.MATCH_SET_ID]);
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            _AccountID = long.Parse(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]))
            _CurrentMatchingType = (ARTEnums.MatchingType?)Enum.Parse(typeof(ARTEnums.MatchingType), Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]);
    }

    /// <summary>
    /// Show Hide Step
    /// </summary>
    private void ShowHideStep()
    {
        if (_CurrentMatchingType == null || _CurrentMatchingType != ARTEnums.MatchingType.AccountMatching)
        {
            WizardStep wzStep = null;
            for (int i = 0; i < wzMatching.WizardSteps.Count; i++)
            {
                wzStep = (WizardStep)wzMatching.WizardSteps[i];
                UserControlMatchingWizardBase oUserControlMatchingWizardBase = (UserControlMatchingWizardBase)Helper.GetUserControlWizardBase(wzStep);
                if (oUserControlMatchingWizardBase.MatchingWizardStepType == ARTEnums.MatchingWizardSteps.RecItemColumnMapping)
                    break;
            }
            if (wzStep != null)
                wzMatching.WizardSteps.Remove(wzStep);
        }
    }

    /// <summary>
    /// Load First Step
    /// </summary>
    private void LoadFirstStep()
    {
        if (!Page.IsPostBack)
        {
            WizardStep wzStep = null;
            for (int i = 0; i < wzMatching.WizardSteps.Count; i++)
            {
                wzStep = (WizardStep)wzMatching.WizardSteps[i];
                if (wzStep.Visible)
                {
                    LoadData(i);
                    break;
                }
            }
        }
    }


    public override string GetMenuKey()
    {
        return "MatchingWizard";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl, true);
    }

}
