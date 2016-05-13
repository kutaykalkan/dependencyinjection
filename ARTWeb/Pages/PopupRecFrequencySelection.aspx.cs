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
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using System.Text;
using SkyStem.ART.Client.Exception;

public partial class Pages_PopupRecFrequencySelection : PopupPageBase
{
    private long? _AccountID = 0;
    private string _txtRecPeriodContainerID = string.Empty;
    private string _RecPeriodIDCollection = string.Empty;
    private WebEnums.FormMode _Mode = WebEnums.FormMode.None;
    private string _HLRecFrequencyID = string.Empty;
    private bool bCreateNew = false;
    string viewStateKey = "RecFrequencyRecPeriod";

    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * Show Rec Frequency Dropdown with Select One / Create New / Rec Frequency Names
         * Three Cases
         * 1. Select One: 
         *      a. means user will select Rec Periods for association with Accounts
         *      - Show Financial Year Dropdown
         *      - Show Rec Periods Grids with checkbox
         * 2. Create New: 
         *      a. means user will select Rec Periods for creating a new Rec Frequency 
         *      b. the selected Rec Periods will be associated with Accounts
         *      - Show Text Box for Rec Frequency Name
         *      - Show Financial Year Dropdown
         *      - Show Rec Periods Grids with checkbox
         * 3. Req Frequency Selected
         *      a. means user will associate the Rec Periods available in the Rec Frequency to be associated with Accounts
         *      - Show Label with Rec Frequency Name
         *      - Show Rec Periods Grids without checkbox
         */


        trRecFreqName.Visible = true;
        trMessage.Visible = true;

        this.Title = LanguageUtil.GetValue(1427);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            this._AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MODE]))
            this._Mode = (WebEnums.FormMode)Enum.Parse(typeof(WebEnums.FormMode), Request.QueryString[QueryStringConstants.MODE]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.REC_PERIOD_CONTAINER_ID]))
            this._txtRecPeriodContainerID = Request.QueryString[QueryStringConstants.REC_PERIOD_CONTAINER_ID];

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.REC_PERIOD_ID_COLLECTION]))
            this._RecPeriodIDCollection = Request.QueryString[QueryStringConstants.REC_PERIOD_ID_COLLECTION];

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.HL_REC_FREQUENCY_ID]))
            this._HLRecFrequencyID = Request.QueryString[QueryStringConstants.HL_REC_FREQUENCY_ID];

        if (!Page.IsPostBack)
        {
            if (this._Mode == WebEnums.FormMode.ReadOnly)
            {
                btnSave.Visible = false;
                trRecFrequencyDropdown.Visible = false;
                trRecFreqName.Visible = false;
                trFinancialYear.Visible = false;

                rgRecFrequency.Columns[0].Visible = false;
                BindRecFrequencyGridByAccountID();

                lblMsg.LabelID = 2086;
            }
            else
            {
                lblMsg.LabelID = 1983;

                IReconciliationPeriod oIReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                List<ReconciliationFrequencyHdrInfo> oReconciliationFrequencyHdrInfoCollection = oIReconciliationPeriodClient.GetAllReconciliationFrequencyHdrInfoByCompanyID((int)SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
                if (oReconciliationFrequencyHdrInfoCollection.Count > 0)
                {
                    List<ListItem> lstoReconciliationFrequencyHdrInfoCollection = new List<ListItem>();
                    foreach (ReconciliationFrequencyHdrInfo oReconciliationFrequencyHdrInfo in oReconciliationFrequencyHdrInfoCollection)
                    {
                        ListItem lst = new ListItem();
                        lst.Text = Helper.GetLabelIDValue((int)oReconciliationFrequencyHdrInfo.ReconciliationFrequencyNameLabelID);
                        lst.Value = oReconciliationFrequencyHdrInfo.ReconciliationFrequencyID.ToString();
                        lstoReconciliationFrequencyHdrInfoCollection.Add(lst);
                    }
                    ddlrecfrequencyName.DataSource = lstoReconciliationFrequencyHdrInfoCollection;
                    ddlrecfrequencyName.DataTextField = "Text";
                    ddlrecfrequencyName.DataValueField = "Value";
                    ddlrecfrequencyName.DataBind();

                }
                ListControlHelper.AddListItemForSelectOne(ddlrecfrequencyName);
                ListControlHelper.AddListItemForCreateNew(ddlrecfrequencyName, 1);

                ddlrecfrequencyName_SelectedIndexChanged(null, null);
            }
        }
    }

    protected void ddlrecfrequencyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrecfrequencyName.SelectedValue != WebConstants.SELECT_ONE && ddlrecfrequencyName.SelectedValue != WebConstants.CREATE_NEW)
        {
            // need to rebind based on the Rec Freq Name selected from the dropdown
            rgRecFrequency.Columns[0].Visible = false;
            BindRecFrequencyGridByRecFrequencyID(Convert.ToInt32(ddlrecfrequencyName.SelectedValue));

            trFinancialYear.Visible = false;
            trRecFreqName.Visible = true;
            lblRecFrequencyNameValue.Visible = true;
            txtRecFrequencyNameValue.Visible = false;

            lblRecFrequencyNameValue.Text = ddlrecfrequencyName.SelectedItem.ToString();
        }
        else if (ddlrecfrequencyName.SelectedValue == WebConstants.SELECT_ONE)
        {
            if (!string.IsNullOrEmpty(this._RecPeriodIDCollection))
            {
                List<string> oRecPeriodIDCollection = null;
                oRecPeriodIDCollection = this._RecPeriodIDCollection.Split(';').ToList();
                SaveRecPeriodsInViewState(oRecPeriodIDCollection);
            }

            // Hide Rec Freq Row
            trRecFreqName.Visible = false;
            trFinancialYear.Visible = true;

            // need to rebind to clean out previous selections if any
            rgRecFrequency.Columns[0].Visible = true;
            BindAndSelectFinancialYearDropdown();
        }
        else
        {
            if (ddlrecfrequencyName.SelectedValue == WebConstants.CREATE_NEW)
            {
                bCreateNew = true;

                List<string> oRecPeriodIDCollection = null;
                SaveRecPeriodsInViewState(oRecPeriodIDCollection);

                trFinancialYear.Visible = true;
                trRecFreqName.Visible = true;
                lblRecFrequencyNameValue.Visible = false;
                txtRecFrequencyNameValue.Visible = true;
                txtRecFrequencyNameValue.Text = string.Empty;

                // need to rebind to clean out previous selections if any
                rgRecFrequency.Columns[0].Visible = true;
                BindAndSelectFinancialYearDropdown();
            }
        }
    }


    private void BindAndSelectFinancialYearDropdown()
    {
        if (ddlFinancialYear.Visible)
        {
            ListControlHelper.BindFinancialYearDropdown(ddlFinancialYear, true);
            if (SessionHelper.CurrentFinancialYearID != null)
            {
                ddlFinancialYear.SelectedValue = SessionHelper.CurrentFinancialYearID.Value.ToString();
            }

            BindRecFrequencyGridByFinancialYear();
        }
    }

    private void BindRecFrequencyGridByFinancialYear()
    {
        Sel.Value = string.Empty;

        int? financialYearID = null;
        if (ddlFinancialYear.SelectedItem != null
            && ddlFinancialYear.SelectedItem.Value != WebConstants.SELECT_ONE)
        {
            financialYearID = Convert.ToInt32(ddlFinancialYear.SelectedItem.Value);
        }

        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, financialYearID, Helper.GetAppUserInfo());

        rgRecFrequency.Columns[1].Visible = false;
        rgRecFrequency.DataSource = oReconciliationPeriodInfoCollection;
        rgRecFrequency.DataBind();
    }

    private void BindRecFrequencyGridByRecFrequencyID(int recFrequencyID)
    {
        List<string> oRecPeriodIDCollection = new List<string>() ;
        IReconciliationPeriod oIReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
        List<ReconciliationFrequencyReconciliationperiodInfo> oReconciliationFrequencyReconciliationperiodInfoCollection = oIReconciliationPeriodClient.GetAllReconciliationFrequencyReconciliationperiodInfoByRecFrequencyID(recFrequencyID, Helper.GetAppUserInfo());

        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = new List<ReconciliationPeriodInfo>();
        ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
        // Loop thru and make the Rec Period Info collection
        for (int i = 0; i < oReconciliationFrequencyReconciliationperiodInfoCollection.Count; i++)
        {
            oRecPeriodIDCollection.Add(oReconciliationFrequencyReconciliationperiodInfoCollection[i].ReconciliationPeriodID.ToString());

            oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
            oReconciliationPeriodInfo.ReconciliationPeriodID = oReconciliationFrequencyReconciliationperiodInfoCollection[i].ReconciliationPeriodID;
            oReconciliationPeriodInfo.PeriodEndDate = oReconciliationFrequencyReconciliationperiodInfoCollection[i].PeriodEndDate; ;
            oReconciliationPeriodInfo.PeriodNumber = oReconciliationFrequencyReconciliationperiodInfoCollection[i].PeriodNumber; 
            oReconciliationPeriodInfo.FinancialYear = oReconciliationFrequencyReconciliationperiodInfoCollection[i].FinancialYear;
            oReconciliationPeriodInfoCollection.Add(oReconciliationPeriodInfo);
        }

        SaveRecPeriodsInViewState(oRecPeriodIDCollection);

        rgRecFrequency.Columns[1].Visible = true;
        rgRecFrequency.DataSource = oReconciliationPeriodInfoCollection;
        rgRecFrequency.DataBind();
    }

    private void BindRecFrequencyGridByAccountID()
    {
        if (this._AccountID.HasValue)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();

            List<AccountReconciliationPeriodInfo> oAccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(this._AccountID.Value,Helper.GetAppUserInfo());

            List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = new List<ReconciliationPeriodInfo>();
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            // Loop thru and make the Rec Period Info collection
            for (int i = 0; i < oAccountReconciliationPeriodInfoCollection.Count; i++)
            {
                oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
                oReconciliationPeriodInfo.ReconciliationPeriodID = oAccountReconciliationPeriodInfoCollection[i].ReconciliationPeriodID;
                oReconciliationPeriodInfo.PeriodEndDate = oAccountReconciliationPeriodInfoCollection[i].PeriodEndDate; ;
                oReconciliationPeriodInfo.PeriodNumber = oAccountReconciliationPeriodInfoCollection[i].PeriodNumber;
                oReconciliationPeriodInfo.FinancialYear = oAccountReconciliationPeriodInfoCollection[i].FinancialYear; ;
                oReconciliationPeriodInfoCollection.Add(oReconciliationPeriodInfo);
            }
            rgRecFrequency.Columns[1].Visible = true;
            rgRecFrequency.DataSource = oReconciliationPeriodInfoCollection;
            rgRecFrequency.DataBind();
        }
    }

    protected void rgRecFrequency_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            List<string> oRecPeriodIDCollection = GetRecPeriodsFromViewState();
            string recPeriodID = null;

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
                ExLabel lblFinancialYear = (ExLabel)e.Item.FindControl("lblFinancialYear");

                ReconciliationPeriodInfo oReconciliationPeriodInfo = (ReconciliationPeriodInfo)e.Item.DataItem;
                lblDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);

                if (lblFinancialYear != null)
                {
                    lblFinancialYear.Text = Helper.GetDisplayStringValue(oReconciliationPeriodInfo.FinancialYear);
                }

                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

                if (oReconciliationPeriodInfo.PeriodEndDate < SessionHelper.CurrentReconciliationPeriodEndDate.Value)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }

                if (oRecPeriodIDCollection != null)
                {
                    recPeriodID = (from accRecPeriod in oRecPeriodIDCollection
                                   where accRecPeriod == oReconciliationPeriodInfo.ReconciliationPeriodID.ToString()
                                   select accRecPeriod).FirstOrDefault();

                    if (!bCreateNew && recPeriodID != null)
                    {
                        e.Item.Selected = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            List<int> listRecPeriodIDs = new List<int>();
            UpdateRecPeriodsInViewState();
            List<string> oRecPeriodIDCollection = GetRecPeriodsFromViewState();
            foreach (string item in oRecPeriodIDCollection)
            {
                if (!string.IsNullOrEmpty(item))
                    listRecPeriodIDs.Add(int.Parse(item));
            }

            if (ddlrecfrequencyName.SelectedValue == WebConstants.CREATE_NEW)
            {
                // Create New 
                ReconciliationFrequencyHdrInfo objReconciliationFrequencyHdrInfo = new ReconciliationFrequencyHdrInfo();
                objReconciliationFrequencyHdrInfo.CompanyID = (int?)SessionHelper.CurrentCompanyID;
                objReconciliationFrequencyHdrInfo.IsActive = true;
                objReconciliationFrequencyHdrInfo.DateAdded = DateTime.Now;
                objReconciliationFrequencyHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;

                int? LabelID = (int)LanguageUtil.InsertPhrase(txtRecFrequencyNameValue.Text, null, AppSettingHelper.GetApplicationID(), (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, null);
                objReconciliationFrequencyHdrInfo.ReconciliationFrequencyNameLabelID = LabelID;
                objReconciliationFrequencyHdrInfo.ReconciliationFrequencyName = txtRecFrequencyNameValue.Text;
                IReconciliationPeriod oIReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                oIReconciliationPeriodClient.InsertReconciliationPeriodreconcilationFrequency(objReconciliationFrequencyHdrInfo, listRecPeriodIDs, Helper.GetAppUserInfo());
            }

            string scriptKey = "SetcountOfDocumentAttached";
            // Render JS to Open the grid Customization Window, 
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);

            //string jsFunction = "OnSaveRecFrequency('" + rgRecFrequency.ClientID + "', '" + this._txtRecPeriodContainerID +
            //    "', '" + this._HLRecFrequencyID + "', '" + this._AccountID + "', '" + this._Mode + "','" + this.hdnRecPeriodIDs.ClientID + "');";

            string jsFunction = "GetRadWindow().BrowserWindow.GetRecPeriodIDCollectionForAccount('" + this._txtRecPeriodContainerID +
                "', '" + hdnRecPeriodIDs.Value + "', '" + this._HLRecFrequencyID + "', '" + this._AccountID + "', '" + this._Mode + "');";

            script.Append("function SetLabelCount()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            script.Append(System.Environment.NewLine);
            script.Append(jsFunction);
            script.Append(System.Environment.NewLine);
            script.Append("GetRadWindow().Close() ;");
            script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("SetLabelCount();");
            ScriptHelper.AddJSEndTag(script);
            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), scriptKey))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, script.ToString());
            }

        }
        else
        {
            txtRecFrequencyNameValue.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1822);
        }
    }


    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Update the View State with Rec Periods 
            UpdateRecPeriodsInViewState();

            // Rebind Rec Periods
            BindRecFrequencyGridByFinancialYear();
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    private void UpdateRecPeriodsInViewState()
    {
        List<string> oRecPeriodIDCollectionFromViewState = (List<string>)ViewState[viewStateKey];
        string recPeriodID = null;

        // Handle for NULL
        if (oRecPeriodIDCollectionFromViewState == null)
            oRecPeriodIDCollectionFromViewState = new List<string>();

        for (int i = 0; i < rgRecFrequency.Items.Count; i++)
        {
            string keyValue = rgRecFrequency.Items[i].GetDataKeyValue("ReconciliationPeriodID").ToString();

            recPeriodID = (from accRecPeriod in oRecPeriodIDCollectionFromViewState
                                  where accRecPeriod == keyValue
                                  select accRecPeriod).FirstOrDefault();

            CheckBox checkBox = (CheckBox)(rgRecFrequency.Items[i] as GridDataItem)["CheckboxSelectColumn"].Controls[0];
            // if selected, check for already exists in ViewState Collection
            if (checkBox.Checked)
            {
                if (recPeriodID == null)
                {
                    oRecPeriodIDCollectionFromViewState.Add(keyValue);
                }
            }
            else
            {
                if (recPeriodID != null)
                {
                    // Remove
                    oRecPeriodIDCollectionFromViewState.Remove(recPeriodID);
                }
            }
        }

        SaveRecPeriodsInViewState(oRecPeriodIDCollectionFromViewState);
    }

    private void SaveRecPeriodsInViewState(List<string> oRecPeriodIDCollection)
    {
        // store the updated collection into ViewState
        ViewState[viewStateKey] = oRecPeriodIDCollection;

        if (oRecPeriodIDCollection == null
            || oRecPeriodIDCollection.Count == 0)
        {
            hdnRecPeriodIDs.Value = "";
        }
        else
        {
            hdnRecPeriodIDs.Value = string.Join(";", oRecPeriodIDCollection.ToArray());
        }
    }


    public List<string> GetRecPeriodsFromViewState()
    {
        List<string> oRecPeriodIDCollection = (List<string>)ViewState[viewStateKey];
        return oRecPeriodIDCollection;
    }

}
