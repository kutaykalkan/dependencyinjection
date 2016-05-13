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
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

public partial class Pages_GridCustomization : PopupPageBase
{

    #region Variables & Constants
    ARTEnums.Grid eGrid = ARTEnums.Grid.None;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 1765);
        PopupHelper.ShowInputRequirementSection(this, 1767, 1768);

        try
        {

            // Fetch the Grid ID
            if (Request.QueryString[QueryStringConstants.GRID_TYPE] == null)
            {
                throw new ARTSystemException(5000090);
            }
            eGrid = (ARTEnums.Grid)System.Enum.Parse(typeof(ARTEnums.Grid), Request.QueryString[QueryStringConstants.GRID_TYPE].ToString(), true);

            if (!Page.IsPostBack)
            {
                LoadGridColumns();
            }
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

    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        /* 
         * 1. Get the Current Column Selection
         * 2. Save the Column Collection in DB
         * 3. Clear Session
         * 4. Reload Parent Page
        */

        try
        {
            List<int> oGridColumnIDCollection = new List<int>();
            for (int i = 0; i < cblColumns.Items.Count; i++)
            {
                if (cblColumns.Items[i].Selected)
                {
                    oGridColumnIDCollection.Add(Convert.ToInt32(cblColumns.Items[i].Value));
                }
            }

            IUser oUserClient = RemotingHelper.GetUserObject();
            oUserClient.SaveGridPrefernce(oGridColumnIDCollection, SessionHelper.GetCurrentUser().UserID, Helper.GetAppUserInfo());

            SessionHelper.ClearSession(SessionHelper.GetSessionKeyForGridCustomization(eGrid));
            // Hard coded by Apoorv - shld be passed from QueryString
            //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ParentPageCallbackFunction", ScriptHelper.GetJSForParentPageCallbackFunction("SetIsPostBackFromFilterScreen"));
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
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
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods

    private void LoadGridColumns()
    {
        // Fetch the Grid ID
        if (Request.QueryString[QueryStringConstants.GRID_TYPE] == null)
        {
            throw new ARTSystemException(5000090);
        }
        eGrid = (ARTEnums.Grid)System.Enum.Parse(typeof(ARTEnums.Grid), Request.QueryString[QueryStringConstants.GRID_TYPE].ToString(), true);

        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
        List<GridColumnInfo> oGridColumnInfoCollection = oReconciliationPeriodClient.GetAllGridColumnsForRecPeriod(eGrid, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

        // Translate
        TranslateGridColumnInfo(oGridColumnInfoCollection);
        //Update custom field
        GridHelper.UpdateGridColumnInfoForCustomField(oGridColumnInfoCollection);

        if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
        {
            short paramID = 59;//ApproverDueDate
            GridColumnInfo oGridColumnInfo = oGridColumnInfoCollection.Find(o => o.ColumnID == paramID);
            if (oGridColumnInfo != null)
                oGridColumnInfoCollection.Remove(oGridColumnInfo);
        }

        BindGridColumns(oGridColumnInfoCollection);

        // Show Current Selection
        ShowSelectedColumns();
    }

    private void ShowSelectedColumns()
    {
        // Show Columns selected Based on User Personalization
        GridColumnInfo oGridColumnInfo = null;
        List<GridColumnInfo> oCurrentGridColumnInfoCollection = SessionHelper.GetGridPreference(eGrid);

        if (oCurrentGridColumnInfoCollection != null && oCurrentGridColumnInfoCollection.Count > 0)
        {
            for (int i = 0; i < cblColumns.Items.Count; i++)
            {
                oGridColumnInfo = oCurrentGridColumnInfoCollection.Find(c => c.GridColumnID.ToString() == cblColumns.Items[i].Value);
                if (oGridColumnInfo != null)
                {
                    cblColumns.Items[i].Selected = true;
                }

            }
        }
    }

    private void BindGridColumns(List<GridColumnInfo> oGridColumnInfoCollection)
    {
        cblColumns.DataSource = oGridColumnInfoCollection;
        cblColumns.DataTextField = "Name";
        cblColumns.DataValueField = "GridColumnID";
        cblColumns.DataBind();
    }
    #endregion

    #region Other Methods
    public void TranslateGridColumnInfo(List<GridColumnInfo> oGridColumnInfoCollection)
    {
        for (int i = 0; i < oGridColumnInfoCollection.Count; i++)
        {
            oGridColumnInfoCollection[i].Name = LanguageUtil.GetValue(oGridColumnInfoCollection[i].LabelID.Value);
        }
    }
    #endregion

}
