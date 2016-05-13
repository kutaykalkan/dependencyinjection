using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using System.Web.UI.HtmlControls;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;

public partial class Pages_GridApplyFilter : PopupPageBase
{

    #region Variables & Constants
    ARTEnums.Grid eGrid = ARTEnums.Grid.None;
    string sessionKey = null;
    private const string DEFAULT_STRING_FOR_SEARCH = "No Records found";
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {

        PopupHelper.ShowInputRequirementSection(this, 1971, 1972);
        MasterPage oMaster = this.Master;
        ScriptManager os = ScriptManager.GetCurrent(this.Page);
        os.RegisterPostBackControl(this.btnApplyFilter);

        eGrid = (ARTEnums.Grid)System.Enum.Parse(typeof(ARTEnums.Grid), Request.QueryString[QueryStringConstants.GRID_TYPE].ToString(), true);
        switch (eGrid)
        {
            case ARTEnums.Grid.AccountViewer:
            case ARTEnums.Grid.AccountViewerSRA:
                PopupHelper.SetPageTitle(this, 1948);//2658 
                break;
            case ARTEnums.Grid.AccountTaskCompleted:
            case ARTEnums.Grid.AccountTaskPending:
            case ARTEnums.Grid.AccountTasks:
            case ARTEnums.Grid.GeneralTaskCompleted:
            case ARTEnums.Grid.GeneralTaskPending:
                PopupHelper.SetPageTitle(this, 2658);
                break;

        }



        sessionKey = SessionHelper.GetSessionKeyForGridFilter(eGrid);

        if (!this.IsPostBack)
        {

            this.btnDummy.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            if (Request.QueryString[QueryStringConstants.GRID_TYPE] == null)
            {
                throw new ARTSystemException(5000090);
            }
            this.ShowHideFilterControls(false);
            this.PopulateFieldNameDDL();
            this.PopulateOperatorDDL();
        }
        PopupMasterPageBase oMaster1 = (PopupMasterPageBase)this.Master;
        oMaster1.HideConfirmationMessage();
        cvStringRangeDataType.ErrorMessage = LanguageUtil.GetValue(5000195);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.ReadFromSessionAndPopulateTable();
        this.btnAddMore.Enabled = (this.ddlFieldName.Items.Count > 0);
        if (this.acctfltrAutoSuggest.Visible)
        {
            //this.acctfltrAutoSuggest.CausesValidation = true;
            this.rfvFSCaption.Enabled = true;
        }
        else
        {
            //this.acctfltrAutoSuggest.CausesValidation = false;
            this.rfvFSCaption.Enabled = false;
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        //    if (Page.IsValid)
        //    {
        short columnID;
        short operatorID;
        string value = string.Empty;
        string displayText = string.Empty;
        bool hasValue = false;
        if (Int16.TryParse(this.ddlFieldName.SelectedValue, out columnID))
        {
            if (Int16.TryParse(this.ddlOperatorName.SelectedValue, out operatorID))
            {
                if (this.acctfltrEqual.Visible)
                {
                    value = this.acctfltrEqual.Text;
                    displayText = value;
                    hasValue = (value != string.Empty);
                }

                if (this.acctfltrStringRange.Visible)
                {
                    value = this.acctfltrStringRange.GetCriteria;
                    displayText = this.acctfltrStringRange.GetCriteriaForDisplay;
                    hasValue = this.acctfltrStringRange.HasValue;
                }
                if (this.calFlterDateEqual.Visible)
                {
                    value = this.calFlterDateEqual.Text;
                    displayText = value;
                    hasValue = (value != string.Empty);
                }
                if (this.acctfltrCheckBoxList.Visible)
                {
                    value = this.acctfltrCheckBoxList.GetCriteria;
                    displayText = this.acctfltrCheckBoxList.GetSelectedDisplayText;
                    hasValue = this.acctfltrCheckBoxList.HasValue;
                }

                if (this.acctfltrYesNoAll.Visible)
                {
                    value = this.acctfltrYesNoAll.GetCriteria;
                    displayText = this.acctfltrYesNoAll.GetDisplayValue;
                    hasValue = this.acctfltrYesNoAll.HasValue;
                }
                if (this.acctfltrAutoSuggest.Visible)
                {
                    value = this.acctfltrAutoSuggest.Text;
                    displayText = value;
                    hasValue = (value != string.Empty);
                }
                if (this.acctfltrDateRange.Visible)
                {
                    value = this.acctfltrDateRange.GetCriteria;
                    displayText = this.acctfltrDateRange.GetCriteriaForDisplay;
                    hasValue = this.acctfltrDateRange.HasValue;

                }
                if (hasValue)
                    this.AddCriteriaToSession(columnID, operatorID, value, displayText);
            }
        }


        if (this.isFilterCriteriaAvailable())
        {
            // Hard coded by Apoorv - shld be passed from QueryString
            //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ParentPageCallbackFunction", ScriptHelper.GetJSForParentPageCallbackFunction("SetIsPostBackFromFilterScreen"));
            //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
        }
        else
        {
            //Label1.Text = "<script type='text/javascript'> GetRadWindow().Close();</" + "script>";
            //Label1.Text = "<script type='text/javascript'>alert('No filter')</" + "script>";
            PopupMasterPageBase oMaster = (PopupMasterPageBase)this.Master;
            oMaster.ShowErrorMessage(5000192);

        }
        //}
    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            short columnID;
            short operatorID;
            string value = string.Empty;
            string displayText = string.Empty;

            if (Int16.TryParse(this.ddlFieldName.SelectedValue, out columnID))
            {
                operatorID = Convert.ToInt16(this.ddlOperatorName.SelectedValue);

                if (this.acctfltrEqual.Visible)
                {
                    value = this.acctfltrEqual.Text;
                    displayText = value;
                }
                if (this.calFlterDateEqual.Visible)
                {
                    value = this.calFlterDateEqual.Text;
                    displayText = value;
                }
                if (this.acctfltrStringRange.Visible)
                {
                    value = this.acctfltrStringRange.GetCriteria;
                    displayText = this.acctfltrStringRange.GetCriteriaForDisplay;
                }

                if (this.acctfltrCheckBoxList.Visible)
                {
                    value = this.acctfltrCheckBoxList.GetCriteria;
                    displayText = this.acctfltrCheckBoxList.GetSelectedDisplayText;
                }

                if (this.acctfltrYesNoAll.Visible)
                {
                    value = this.acctfltrYesNoAll.GetCriteria;
                    displayText = this.acctfltrYesNoAll.GetDisplayValue;
                }
                if (this.acctfltrAutoSuggest.Visible)
                {
                    value = this.acctfltrAutoSuggest.Text;
                    displayText = value;
                }
                if (this.acctfltrDateRange.Visible)
                {
                    value = this.acctfltrDateRange.GetCriteria;
                    displayText = this.acctfltrDateRange.GetCriteriaForDisplay;
                }
                this.AddCriteriaToSession(columnID, operatorID, value, displayText);
                this.ShowHideFilterControls(false);
                this.ClearCriteriaControls();
                this.PopulateFieldNameDDL();
                this.PopulateOperatorDDL();
            }
        }
    }
    protected void btnDummy_Click(object sender, EventArgs e)
    {
        short parameterID;
        if (Int16.TryParse(this.hdnField.Value, out parameterID))
        {
            DeleteCriteriaFromSession(parameterID);
            this.ShowHideFilterControls(false);
            this.PopulateFieldNameDDL();
            this.PopulateOperatorDDL();
        }
        //Delete this value from session

    }
    protected void ddlFieldName_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.PopulateOperatorDDL();

    }
    protected void ddlOperatorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        short columnID = Convert.ToInt16(this.ddlFieldName.SelectedValue);
        short operatorID = Convert.ToInt16(this.ddlOperatorName.SelectedValue);
        ShowFilterControlsBySelection(columnID, operatorID);
    }
 
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private bool isFilterCriteriaAvailable()
    {
        bool isCriteriaAvailable = false;
        List<FilterCriteria> oFltrCriteriaCollection = null;
        if (Session[sessionKey] == null)
            isCriteriaAvailable = false;
        else
        {
            oFltrCriteriaCollection = Session[sessionKey] as List<FilterCriteria>;
            if (oFltrCriteriaCollection.Count > 0)
                isCriteriaAvailable = true;
            else
                isCriteriaAvailable = false;

        }
        return isCriteriaAvailable;
    }

    private void ClearCriteriaControls()
    {
        this.acctfltrCheckBoxList.ClearSelection();
        this.acctfltrEqual.Text = string.Empty;
        this.calFlterDateEqual.Text = String.Empty;
        this.acctfltrDateRange.ClearSelection();
        this.acctfltrYesNoAll.ClearSelection();
        this.acctfltrStringRange.ClearSelection();
        this.acctfltrAutoSuggest.Text = "";
    }

    private void BindFieldNamesDDL(ListItemCollection oGridColumnInfoCollection)
    {
        this.ddlFieldName.DataSource = oGridColumnInfoCollection;
        this.ddlFieldName.DataTextField = "Text";
        this.ddlFieldName.DataValueField = "Value";
        this.ddlFieldName.DataBind();
    }

    private void BindOperatorDDL(List<OperatorMstInfo> oOperatorCollection)
    {
        this.ddlOperatorName.DataSource = oOperatorCollection;
        this.ddlOperatorName.DataTextField = "OperatorName";
        this.ddlOperatorName.DataValueField = "OperatorID";
        this.ddlOperatorName.DataBind();
    }

    private void ShowHideFilterControls(bool visibility)
    {
        this.acctfltrEqual.Visible = visibility;
        this.acctfltrStringRange.Visible = visibility;
        this.acctfltrDateRange.Visible = visibility;
        this.acctfltrCheckBoxList.Visible = visibility;
        this.acctfltrYesNoAll.Visible = visibility;
        this.acctfltrAutoSuggest.Visible = visibility;
        this.calFlterDateEqual.Visible = visibility;
    }

    private void ShowFilterControlsBySelection(short columnID, short operatorID)
    {
        this.ShowHideFilterControls(false);
        //this.acctfltrAutoSuggest.CausesValidation = false;
        //this.rfvFSCaption.Enabled = false;
        switch (operatorID)
        {
            case (short)WebEnums.Operator.Between:
                showControlForBetweenOperator(columnID);
                break;
            case (short)WebEnums.Operator.Contains:
                showControlForContainsOperator(columnID);
                break;
            case (short)WebEnums.Operator.Equals:
                showControlForEqualsOperator(columnID);

                break;
            case (short)WebEnums.Operator.GreaterThan:
                //this.acctfltrEqual.Visible = true;
                this.ShowControlForGreaterThanOperator(columnID);
                break;
            case (short)WebEnums.Operator.GreaterThanEqualTo:
                //this.acctfltrEqual.Visible = true;
                this.ShowControlForGreaterThanEqualToOperator(columnID);
                break;
            case (short)WebEnums.Operator.LessThan:
                //this.acctfltrEqual.Visible = true;
                this.ShowControlForLessThanOperator(columnID);
                break;
            case (short)WebEnums.Operator.LessThanEqualTo:
                //this.acctfltrEqual.Visible = true;
                this.ShowControlForLessThanEqualToOperator(columnID);
                break;
            case (short)WebEnums.Operator.Matches:
                this.acctfltrCheckBoxList.Visible = true;
                this.acctfltrCheckBoxList.CBLDataSource = GetDataSourceByColumnID(columnID);
                //Bind Check box list as per column selected
                break;
            case (short)WebEnums.Operator.NotEqualTo:
                this.acctfltrEqual.Visible = true;
                break;
        }
    }

    private void showControlForBetweenOperator(short columnID)
    {
        ExCustomValidator rfv = this.acctfltrStringRange.Validator;
        switch (columnID)
        {
            case (short)WebEnums.StaticAccountField.AccountNumber:
                rfv.ClientValidationFunction = "AcctFltrValidateAccountRange";
                this.acctfltrStringRange.ErrorLabelID = 5000191;
                this.acctfltrStringRange.Visible = true;
                break;
            case (short)WebEnums.StaticAccountField.GLBalance:
                this.acctfltrStringRange.Visible = true;
                break;
            case (short)WebEnums.StaticAccountField.RecBalance:
                this.acctfltrStringRange.Visible = true;
                break;
            case (short)WebEnums.StaticAccountField.UnexplainedVar:
                this.acctfltrStringRange.Visible = true;
                break;
            case (short)WebEnums.StaticAccountField.WriteOnOff:
                this.acctfltrStringRange.Visible = true;
                break;

            case (short)WebEnums.TaskColumnForFilter.TaskDuration:
            case (short)WebEnums.TaskColumnForFilter.AttachmentCount:
            case (short)WebEnums.TaskColumnForFilter.ApprovalDuration:
            case (short)WebEnums.TaskColumnForFilter.CompletionDocs:

                rfv.ClientValidationFunction = "ValidateNumberRange";
                this.acctfltrStringRange.ErrorLabelID = 5000195;
                this.acctfltrStringRange.Visible = true;
                break;

            case (short)WebEnums.TaskColumnForFilter.StartDate:
            case (short)WebEnums.TaskColumnForFilter.DueDate:
            case (short)WebEnums.TaskColumnForFilter.AssigneeDueDate:
            case (short)WebEnums.TaskColumnForFilter.TaskReviewerDueDate:      
                this.acctfltrDateRange.Visible = true;
                break;
            case (short)WebEnums.StaticAccountField.PreparerDueDate:
            case (short)WebEnums.StaticAccountField.ReviewerDueDate:
            case (short)WebEnums.StaticAccountField.ApproverDueDate:
                this.acctfltrDateRange.Visible = true;
                break;

        }
    }

    private void showControlForEqualsOperator(short columnID)
    {
        switch (columnID)
        {
            case (short)WebEnums.StaticAccountField.KeyAccount:
            case (short)WebEnums.StaticAccountField.Materiality:
            case (short)WebEnums.StaticAccountField.ZeroBalance:
                this.acctfltrYesNoAll.Visible = true;
                //this.acctfltrAutoSuggest.CausesValidation = true;
                //this.rfvFSCaption.Enabled = true;
                break;
            case (short)WebEnums.TaskColumnForFilter.TaskNumber:
            case (short)WebEnums.TaskColumnForFilter.TaskName:
            case (short)WebEnums.TaskColumnForFilter.Description:
            case (short)WebEnums.TaskColumnForFilter.AttachmentCount:
            case (short)WebEnums.TaskColumnForFilter.ApprovalDuration:
                this.acctfltrEqual.Visible = true;
                break;

            case (short)WebEnums.TaskColumnForFilter.StartDate:
            case (short)WebEnums.TaskColumnForFilter.DueDate:
            case (short)WebEnums.TaskColumnForFilter.AssigneeDueDate:
            case (short)WebEnums.TaskColumnForFilter.TaskReviewerDueDate:                
                this.calFlterDateEqual.Visible = true;
                break;
            case (short)WebEnums.StaticAccountField.PreparerDueDate:
            case (short)WebEnums.StaticAccountField.ReviewerDueDate:
            case (short)WebEnums.StaticAccountField.ApproverDueDate:
                this.calFlterDateEqual.Visible = true;
                break;

            default:
                this.acctfltrEqual.Visible = true;
                break;
        }
    }

    private void showControlForContainsOperator(short columnID)
    {
        switch (columnID)
        {
            case (short)WebEnums.StaticAccountField.FSCaption:
                this.acctfltrAutoSuggest.Visible = true;
                break;

            case (short)WebEnums.TaskColumnForFilter.TaskNumber:
            case (short)WebEnums.TaskColumnForFilter.TaskName:
            case (short)WebEnums.TaskColumnForFilter.Description:
            case (short)WebEnums.TaskColumnForFilter.ApproverComment:
            case (short)WebEnums.TaskColumnForFilter.CustomField1:
            case (short)WebEnums.TaskColumnForFilter.CustomField2:
                this.acctfltrEqual.Visible = true;
                break;

            default:
                this.acctfltrEqual.Visible = true;
                break;
        }
    }

    private void ShowFilterControlForKey(short operatorID)
    {
        this.acctfltrEqual.Visible = true;
        this.acctfltrStringRange.Visible = false;
        this.acctfltrDateRange.Visible = false;
        this.acctfltrCheckBoxList.Visible = false;
        this.acctfltrAutoSuggest.Visible = false;
        this.acctfltrYesNoAll.Visible = false;
    }

    private void ShowControlForGreaterThanOperator(short columnID)
    {
        switch (columnID)
        {
            case (short)WebEnums.TaskColumnForFilter.StartDate:
            case (short)WebEnums.TaskColumnForFilter.DueDate:
            case (short)WebEnums.TaskColumnForFilter.AssigneeDueDate:
            case (short)WebEnums.TaskColumnForFilter.TaskReviewerDueDate:  

                this.calFlterDateEqual.Visible = true;
                break;

            case (short)WebEnums.StaticAccountField.PreparerDueDate:
            case (short)WebEnums.StaticAccountField.ReviewerDueDate:
            case (short)WebEnums.StaticAccountField.ApproverDueDate:
                this.calFlterDateEqual.Visible = true;
                break;

            default:
                this.acctfltrEqual.Visible = true;
                break;

        }
    }

    private void ShowControlForGreaterThanEqualToOperator(short columnID)
    {
        this.ShowControlForGreaterThanOperator(columnID);
    }

    private void ShowControlForLessThanOperator(short columnID)
    {
        this.ShowControlForGreaterThanOperator(columnID);
    }

    private void ShowControlForLessThanEqualToOperator(short columnID)
    {
        this.ShowControlForGreaterThanOperator(columnID);
    }

    private ListItemCollection GetDataSourceByColumnID(short columnID)
    {
        ListItemCollection oItemCollection = null;
        switch (columnID)
        {
            case (short)WebEnums.StaticAccountField.AccountType:
                oItemCollection = AccountFilterHelper.GetListItemForAccountType();
                break;
            case (short)WebEnums.StaticAccountField.ReconciliationStatus:
                oItemCollection = ReportHelper.GetListItemCollectionForRecStatus();
                break;
            case (short)WebEnums.StaticAccountField.CertificationStatus:
                oItemCollection = AccountFilterHelper.GetListItemForCertStatus();
                break;
            case (short)WebEnums.StaticAccountField.RiskRating:
                oItemCollection = ReportHelper.GetListItemCollectionForRiskRating();
                break;
            case (short)WebEnums.StaticAccountField.Preparer:
                oItemCollection = AccountFilterHelper.GetListItemsForRole(SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.CurrentReconciliationPeriodID.Value, (short)ARTEnums.AccountAttribute.Preparer);
                break;
            case (short)WebEnums.StaticAccountField.Reviewer:
                oItemCollection = AccountFilterHelper.GetListItemsForRole(SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.CurrentReconciliationPeriodID.Value, (short)ARTEnums.AccountAttribute.Reviewer);
                break;
            case (short)WebEnums.StaticAccountField.Approver:
                oItemCollection = AccountFilterHelper.GetListItemsForRole(SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.CurrentReconciliationPeriodID.Value, (short)ARTEnums.AccountAttribute.Approver);
                break;
            case (short)WebEnums.StaticAccountField.BackupPreparer:
                oItemCollection = AccountFilterHelper.GetListItemsForRole(SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.CurrentReconciliationPeriodID.Value, (short)ARTEnums.AccountAttribute.BackupPreparer);
                break;
            case (short)WebEnums.StaticAccountField.BackupReviewer:
                oItemCollection = AccountFilterHelper.GetListItemsForRole(SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.CurrentReconciliationPeriodID.Value, (short)ARTEnums.AccountAttribute.BackupReviewer);
                break;
            case (short)WebEnums.StaticAccountField.BackupApprover:
                oItemCollection = AccountFilterHelper.GetListItemsForRole(SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.CurrentReconciliationPeriodID.Value, (short)ARTEnums.AccountAttribute.BackupApprover);
                break;

            case (short)WebEnums.TaskColumnForFilter.Approver:
            case (short)WebEnums.TaskColumnForFilter.AssignedTo:
            case (short)WebEnums.TaskColumnForFilter.CreatedBy:
            case (short)WebEnums.TaskColumnForFilter.Reviewer:
                oItemCollection = AccountFilterHelper.GetListItemsForUsers(SessionHelper.CurrentCompanyID.Value);
                break;

            case (short)WebEnums.TaskColumnForFilter.RecurrenceType:
                oItemCollection = AccountFilterHelper.GetListItemsForRecurrenceType();
                break;
            case (short)WebEnums.TaskColumnForFilter.TaskList:
                oItemCollection = AccountFilterHelper.GetListItemsForTaskList();
                break;
                
            case (short)WebEnums.TaskColumnForFilter.TaskStatus:
                oItemCollection = AccountFilterHelper.GetListItemsForTaskStatus();
                break;
            case (short)WebEnums.TaskColumnForFilter.TaskSubList:
                oItemCollection = AccountFilterHelper.GetListItemsForTaskSubList();
                break;

        }
        return oItemCollection;
    }

    private void PopulateFieldNameDDL()
    {
        //get fields from session
        List<FilterCriteria> oFltrCriteriaCollection = Session[sessionKey] as List<FilterCriteria>;
        ListItemCollection oFieldCollection = AccountFilterHelper.GetAllFields(eGrid);

        //Delete already present fields
        if (oFltrCriteriaCollection != null)
        {
            //Get All paramID in list
            foreach (FilterCriteria fltr in oFltrCriteriaCollection)
            {
                short parameterID = fltr.ParameterID;
                ListItem item = oFieldCollection.FindByValue(parameterID.ToString());
                if (item != null)
                    oFieldCollection.Remove(item);

            }
        }
        if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
        {
            short paramID = 59;//ApproverDueDate
            ListItem itemApproverDueDate = oFieldCollection.FindByValue(paramID.ToString());
            if (itemApproverDueDate != null)
                oFieldCollection.Remove(itemApproverDueDate);
        }
        this.BindFieldNamesDDL(oFieldCollection);
    }

    private void PopulateOperatorDDL()
    {
        string selectedColumn = this.ddlFieldName.SelectedValue;
        List<OperatorMstInfo> oOperatorCollection = null;
        short columnID;
        short operatorID;
        if (Int16.TryParse(selectedColumn, out columnID))
        {
            oOperatorCollection = AccountFilterHelper.GetOperatorsByColumnID(columnID);
            this.BindOperatorDDL(oOperatorCollection);
            operatorID = Convert.ToInt16(this.ddlOperatorName.SelectedValue);
            ShowFilterControlsBySelection(columnID, operatorID);
        }
        else
        {
            this.ddlOperatorName.Items.Clear();
            this.BindOperatorDDL(null);
            ShowFilterControlsBySelection(-1, -1);
        }
    }
    private void AddCriteriaToSession(short columnID, short operatorID, string value, string displayText)
    {
        FilterCriteria fltr = new FilterCriteria();
        fltr.ParameterID = columnID;
        fltr.OperatorID = operatorID;
        fltr.Value = value;
        fltr.DisplayValue = displayText;
        List<FilterCriteria> oFltrCriteriaCollection = Session[sessionKey] as List<FilterCriteria>;
        if (oFltrCriteriaCollection == null)
            oFltrCriteriaCollection = new List<FilterCriteria>();

        SessionHelper.ClearSession(sessionKey);
        oFltrCriteriaCollection.Add(fltr);
        Session[sessionKey] = oFltrCriteriaCollection;
    }
    private void ReadFromSessionAndPopulateTable()
    {
        const string evenrowStyle = "TableAlternateRowSameAsGrid";
        const string oddRowStyle = "TableRowSameAsGrid";
        string currentStyle = "";
        if (Session[sessionKey] != null)
        {
            List<FilterCriteria> oFltrCriteriaCollection = Session[sessionKey] as List<FilterCriteria>;
            if (oFltrCriteriaCollection != null && oFltrCriteriaCollection.Count > 0)
            {
                this.tblFltrCriteria.Visible = true;
                //HtmlTable dTable = this.tblFltrCriteria as HtmlTable;
                foreach (FilterCriteria fltr in oFltrCriteriaCollection)
                {
                    switch (currentStyle)
                    {
                        case "":
                            currentStyle = oddRowStyle;
                            break;
                        case oddRowStyle:
                            currentStyle = evenrowStyle;
                            break;
                        case evenrowStyle:
                            currentStyle = oddRowStyle;
                            break;
                    }
                    HtmlTableRow tr = new HtmlTableRow();
                    tr.Attributes.Add("class", currentStyle);
                    HtmlTableCell td1 = new HtmlTableCell();
                    //td1.VAlign = VerticalAlign.Top.ToString();
                    ExLabel lblColumnName = new ExLabel();
                    lblColumnName.SkinID = "Black11Arial";
                    lblColumnName.Text = AccountFilterHelper.GetColumnNameFromColumnID(eGrid, fltr.ParameterID);
                    td1.Controls.Add(lblColumnName);

                    HtmlTableCell td2 = new HtmlTableCell();
                    //td2.VAlign = VerticalAlign.Top.ToString();
                    ExLabel lblOperatorName = new ExLabel();
                    lblOperatorName.SkinID = "Black11Arial";
                    lblOperatorName.Text = AccountFilterHelper.GetOperatorNameByOperatorID(fltr.ParameterID, fltr.OperatorID);
                    td2.Controls.Add(lblOperatorName);

                    HtmlTableCell td3 = new HtmlTableCell();
                    //td3.VAlign = VerticalAlign.Top.ToString();
                    ExLabel lblValue = new ExLabel();
                    lblValue.SkinID = "Black11ArialNormal";
                    lblValue.Text = fltr.DisplayValue;//AccountFilterHelper.MapIDsToValues(fltr.ParameterID, fltr.OperatorID, fltr.Value);
                    td3.Controls.Add(lblValue);

                    HtmlTableCell td4 = new HtmlTableCell();
                    //td4.VAlign = VerticalAlign.Top.ToString();
                    td4.Align = HorizontalAlign.Center.ToString();
                    ExImageButton btnDelete = new ExImageButton();
                    btnDelete.SkinID = "DeleteIcon";
                    btnDelete.OnClientClick = "return deleteClick(this)";
                    btnDelete.Attributes.Add("paramID", fltr.ParameterID.ToString());
                    td4.Controls.Add(btnDelete);

                    tr.Cells.Add(td1);
                    tr.Cells.Add(td2);
                    tr.Cells.Add(td3);
                    tr.Cells.Add(td4);

                    this.tblFltrCriteria.Rows.Add(tr);
                }
            }
            else
            {
                this.tblFltrCriteria.Visible = false;
            }
        }
        else
        {
            this.tblFltrCriteria.Visible = false;
        }
    }
    private void DeleteCriteriaFromSession(short columnID)
    {
        List<FilterCriteria> oFltrCriteriaCollection = Session[sessionKey] as List<FilterCriteria>;
        SessionHelper.ClearSession(sessionKey);
        FilterCriteria oFltr = oFltrCriteriaCollection.Find(r => r.ParameterID == columnID);
        if (oFltr != null)
            oFltrCriteriaCollection.Remove(oFltr);
        Session[sessionKey] = oFltrCriteriaCollection;
    }
    #endregion

    #region Other Methods
    protected void cvStringRangeDataType_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        short columnID;
        short operatorID;
        string value = string.Empty;
        string displayText = string.Empty;
        decimal oVal;

        if (Int16.TryParse(this.ddlFieldName.SelectedValue, out columnID))
        {
            operatorID = Convert.ToInt16(this.ddlOperatorName.SelectedValue);

            if (this.acctfltrEqual.Visible)
            {
                value = this.acctfltrEqual.Text;
                displayText = value;
            }
            if (this.calFlterDateEqual.Visible)
            {
                value = this.calFlterDateEqual.Text;
                displayText = value;
            }
            if (this.acctfltrStringRange.Visible)
            {
                value = this.acctfltrStringRange.GetCriteria;
                displayText = this.acctfltrStringRange.GetCriteriaForDisplay;
            }

            if (this.acctfltrCheckBoxList.Visible)
            {
                value = this.acctfltrCheckBoxList.GetCriteria;
                displayText = this.acctfltrCheckBoxList.GetSelectedDisplayText;
            }

            if (this.acctfltrYesNoAll.Visible)
            {
                value = this.acctfltrYesNoAll.GetCriteria;
                displayText = this.acctfltrYesNoAll.GetDisplayValue;
            }
            if (this.acctfltrAutoSuggest.Visible)
            {
                value = this.acctfltrAutoSuggest.Text;
                displayText = value;
            }
            if (this.acctfltrDateRange.Visible)
            {
                value = this.acctfltrDateRange.GetCriteria;
                displayText = this.acctfltrDateRange.GetCriteriaForDisplay;
            }
        }
        if (!string.IsNullOrEmpty(value))
        {
            switch (columnID)
            {
                case (short)WebEnums.StaticAccountField.GLBalance:
                case (short)WebEnums.StaticAccountField.WriteOnOff:
                case (short)WebEnums.StaticAccountField.RecBalance:
                case (short)WebEnums.StaticAccountField.UnexplainedVar:
                    if (!decimal.TryParse(value, out oVal))
                    {
                        args.IsValid = false;
                        return;
                    }
                    break;
            }
        }


    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] AutoCompleteFSCaption(string prefixText, int count)
    {
        string[] oFSCaptionCollection = null;
        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IFSCaption oFSCaption = RemotingHelper.GetFSCaptioneObject();
                oFSCaptionCollection = oFSCaption.SelectFSCaptionByCompanyIDAndPrefixText(companyId, prefixText, count
                    , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

                if (oFSCaptionCollection == null || oFSCaptionCollection.Length == 0)
                {
                    oFSCaptionCollection = new string[] { DEFAULT_STRING_FOR_SEARCH };
                }
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(null, ex);
            throw ex;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(null, ex);
            throw ex;
        }

        return oFSCaptionCollection;
    }
    #endregion
}
