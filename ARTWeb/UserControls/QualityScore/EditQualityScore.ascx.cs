using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Web.UserControls
{
    public partial class EditQualityScore : UserControlRecItemBase
    {

        #region Local Variables

        ExImageButton ToggleControl;
        private bool? _IsExpandedValueForced = null;
        private short _RecPeriodStatus = 0;
        private short _UserRole = 0;
        bool _IsEditMode = false;
        bool _IsGLDataIDChanged = false;

        #endregion

        #region Properties

        public string OnQualityScoreChanged { get; set; }

        public override bool IsExpanded
        {
            get
            {
                if (hdIsExpanded.Value == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    hdIsExpanded.Value = "1";
                else
                    hdIsExpanded.Value = "0";
            }
        }

        public override bool IsRefreshData
        {
            get
            {
                if (hdIsRefreshData.Value == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    hdIsRefreshData.Value = "1";
                else
                    hdIsRefreshData.Value = "0";
            }
        }

        public bool? IsExpandedValueForced
        {
            get { return _IsExpandedValueForced; }
            set { _IsExpandedValueForced = value; }
        }

        public string DivClientId
        {
            get { return divMainContent.ClientID; }
        }

        public bool IsDataLoadCondition
        {
            get
            {
                if (AccountID != null && NetAccountID != null && GLDataID != null && IsSRA != null)
                    return true;
                return false;
            }
        }

        public bool ContentVisibility
        {
            set
            {
                this.Visible = true;
                divMainContent.Visible = true;
            }
        }

        public List<GLDataQualityScoreInfo> GLDataQualityScoreInfoList
        {
            get { return (List<GLDataQualityScoreInfo>)Session[SessionConstants.QUALITY_SCORE_GLDATA_QUALITY_METRIC_LIST]; }
            set { Session[SessionConstants.QUALITY_SCORE_GLDATA_QUALITY_METRIC_LIST] = value; }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            SetControlState();
            if (EntityNameLabelID.HasValue)
            {
                this.rgEditQualityScore.EntityNameLabelID = EntityNameLabelID.Value;
                this.rgEditQualityScore.EntityNameLabelID = EntityNameLabelID.Value;
            }
            if (!Page.IsPostBack)
                GLDataQualityScoreInfoList = null;
        }

        #endregion

        #region Grid Events

        protected void rgEditQualityScore_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GLDataQualityScoreInfo oGLDataQualityScoreInfo = (GLDataQualityScoreInfo)e.Item.DataItem;
                GridDataItem oItem = e.Item as GridDataItem;

                ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
                ExLabel lblQualityScoreNumber = (ExLabel)e.Item.FindControl("lblQualityScoreNumber");
                ExLabel lblSystemQualityScoreStatus = (ExLabel)e.Item.FindControl("lblSystemQualityScoreStatus");
                DropDownList ddlUserQualityScoreStatus = (DropDownList)e.Item.FindControl("ddlUserQualityScoreStatus");
                ExTextBox txtComments = (ExTextBox)e.Item.FindControl("txtComments");
                Image imgInfo = (Image)e.Item.FindControl("imgInfo");

                lblDescription.Text = oGLDataQualityScoreInfo.CompanyQualityScoreInfo.Description;
                lblQualityScoreNumber.Text = Helper.GetDisplayStringValue(oGLDataQualityScoreInfo.CompanyQualityScoreInfo.QualityScoreNumber);
                string qualityScoreStatus = Helper.GetQualityScoreStatusByID(oGLDataQualityScoreInfo.SystemQualityScoreStatusID);
                lblSystemQualityScoreStatus.Text = Helper.GetDisplayStringValue(qualityScoreStatus);
                imgInfo.Visible = false;
                if (oGLDataQualityScoreInfo.SystemQualityScoreStatusID.HasValue && 
                    oGLDataQualityScoreInfo.SystemQualityScoreStatusID == (short)ARTEnums.QualityScoreStatus.No &&
                    oGLDataQualityScoreInfo.CompanyQualityScoreInfo.QualityScoreID.GetValueOrDefault() == (int)ARTEnums.QualityScoreItem.NoCapabilityChanges)
                {
                    //imgInfo.Visible = true;
                    //imgInfo.ToolTip = Helper.GetDisplayStringForChangedCompanyCapabilities();
                    lblSystemQualityScoreStatus.ToolTip = Helper.GetDisplayStringForChangedCompanyCapabilities();
                }
                ListControlHelper.BindUserQualityScoreStatusDropdown(ddlUserQualityScoreStatus, true);
                if (oGLDataQualityScoreInfo.UserQualityScoreStatusID.HasValue)
                {
                    ddlUserQualityScoreStatus.SelectedValue = oGLDataQualityScoreInfo.UserQualityScoreStatusID.Value.ToString();
                }
                txtComments.Text = oGLDataQualityScoreInfo.Comments;
                bool editMode = _IsEditMode;
                // Is Score editable by user
                if (editMode && !oGLDataQualityScoreInfo.CompanyQualityScoreInfo.IsUserScoreEnabled.GetValueOrDefault())
                {
                    editMode = false;
                }
                // Is Score editable for SRA accounts
                if (editMode && IsSRA.GetValueOrDefault() && !oGLDataQualityScoreInfo.CompanyQualityScoreInfo.IsApplicableForSRA.GetValueOrDefault())
                {
                    editMode = false;
                }
                ddlUserQualityScoreStatus.Enabled = editMode;
                ddlUserQualityScoreStatus.Attributes.Add("onchange", "OnUserResponseChange('" + rgEditQualityScore.ClientID
                    + "','SystemQualityScoreStatusID','UserQualityScoreStatusID','Comments'," + this.OnQualityScoreChanged
                    + ",'" + ((short)ARTEnums.QualityScoreType.UserScore).ToString()
                    + "');");

                // Comments should be editable based upon RecForm mode
                txtComments.Enabled = _IsEditMode;

                // Enable/Disable Based Upon Role
                // Preparer can modify all except two below
                if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.PREPARER)
                {
                    if (oGLDataQualityScoreInfo.CompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.ReviewerDueDate
                        || oGLDataQualityScoreInfo.CompanyQualityScoreInfo.QualityScoreID == (int)ARTEnums.QualityScoreItem.ApproverDueDate)
                        txtComments.Enabled = false;
                }
                // Reviewer can modify only one below
                else if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.REVIEWER)
                {
                    ddlUserQualityScoreStatus.Enabled = false;
                    if (oGLDataQualityScoreInfo.CompanyQualityScoreInfo.QualityScoreID != (int)ARTEnums.QualityScoreItem.ReviewerDueDate)
                    {
                        txtComments.Enabled = false;
                    }
                }
                // Approver can modify only one below
                else if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.APPROVER)
                {
                    ddlUserQualityScoreStatus.Enabled = false;
                    if (oGLDataQualityScoreInfo.CompanyQualityScoreInfo.QualityScoreID != (int)ARTEnums.QualityScoreItem.ApproverDueDate)
                    {
                        txtComments.Enabled = false;
                    }
                }
                txtComments.IsRequired = false;
                if (txtComments.Enabled && oGLDataQualityScoreInfo.SystemQualityScoreStatusID.HasValue && oGLDataQualityScoreInfo.UserQualityScoreStatusID.HasValue)
                {
                    if (oGLDataQualityScoreInfo.SystemQualityScoreStatusID.Value != oGLDataQualityScoreInfo.UserQualityScoreStatusID.Value)
                        txtComments.IsRequired = true;
                }
            }
        }

        #endregion

        #region Custom Functions

        /// <summary>
        /// Sets the state of the control.
        /// </summary>
        private void SetControlState()
        {
            if (IsPostBack && IsRefreshData && IsDataLoadCondition)
                LoadData();
            ExpandCollapse();
        }

        /// <summary>
        /// Registers the toggle control.
        /// </summary>
        /// <param name="imgToggleControl">The img toggle control.</param>
        public void RegisterToggleControl(ExImageButton imgToggleControl)
        {
            imgToggleControl.OnClientClick += "return ToggleDiv('" + imgToggleControl.ClientID + "','" + this.DivClientId + "','" + hdIsExpanded.ClientID + "','" + hdIsRefreshData.ClientID + "');";
            ToggleControl = imgToggleControl;
        }

        /// <summary>
        /// Enables the disable controls for non preparers and closed periods.
        /// </summary>
        public void EnableDisableControlsForNonPreparersAndClosedPeriods()
        {
            this._UserRole = SessionHelper.CurrentRoleID.Value;
            this._RecPeriodStatus = (short)CurrentRecProcessStatus.Value;
            if (Helper.GetFormMode(WebEnums.ARTPages.EditQualityScore, this.GLDataHdrInfo ) == WebEnums.FormMode.Edit
                && this.GLDataID.Value > 0)
            {
                _IsEditMode = true;
            }
            else
            {
                _IsEditMode = false;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        public override void LoadData()
        {
            if (IsExpanded)
            {
                this.EnableDisableControlsForNonPreparersAndClosedPeriods();
                GetDataFromService();
                if(!_IsGLDataIDChanged)
                    GetData();
                _IsGLDataIDChanged = false;
                BingGrids();
                IsRefreshData = false;
                if (IsExpanded)
                {
                    this.divMainContent.Visible = true;
                    //set style in code behind as same as of client side
                    //this is required as during postback whole content refreshed so style, as of initial state

                }
                if (IsExpanded && !this.IsPrintMode)
                {
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                }
            }
        }

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <returns></returns>
        public List<GLDataQualityScoreInfo> GetData()
        {
            if (GLDataQualityScoreInfoList != null && GLDataQualityScoreInfoList.Count > 0)
            {
                foreach (GridDataItem oItem in rgEditQualityScore.Items)
                {
                    int? companyQualityScoreID = (int?)oItem.GetDataKeyValue("CompanyQualityScoreID");
                    if (companyQualityScoreID.HasValue)
                    {
                        GLDataQualityScoreInfo oGLDataQualityScoreInfo = GLDataQualityScoreInfoList.Find(T => T.CompanyQualityScoreID == companyQualityScoreID);
                        if (oGLDataQualityScoreInfo != null)
                        {
                            DropDownList ddlUserQualityScoreStatus = (DropDownList)oItem.FindControl("ddlUserQualityScoreStatus");
                            ExTextBox txtComments = (ExTextBox)oItem.FindControl("txtComments");
                            if (ddlUserQualityScoreStatus.SelectedValue == WebConstants.SELECT_ONE)
                                oGLDataQualityScoreInfo.UserQualityScoreStatusID = null;
                            else
                                oGLDataQualityScoreInfo.UserQualityScoreStatusID = Convert.ToInt16(ddlUserQualityScoreStatus.SelectedValue);
                            oGLDataQualityScoreInfo.Comments = txtComments.Text;
                        }
                    }
                }
            }
            return GLDataQualityScoreInfoList;
        }

        /// <summary>
        /// Gets the data from service.
        /// </summary>
        private void GetDataFromService()
        {
            // Store List to Session
            GLDataQualityScoreInfoList = QualityScoreHelper.GetGLDataQualityScoreInfoList(GLDataID);
        }

        /// <summary>
        /// Bind the grids.
        /// </summary>
        private void BingGrids()
        {
            // Bind Grid
            rgEditQualityScore.DataSource = GLDataQualityScoreInfoList;
            rgEditQualityScore.DataBind();
        }

        /// <summary>
        /// Expands the collapse.
        /// </summary>
        public override void ExpandCollapse()
        {
            if (IsExpandedValueForced == null)
            {
                //get default state from hidden field
                this.divMainContent.Visible = IsExpanded;
            }
            else
            {
                //show state of the control based on property IsExpandedValueForced
                this.divMainContent.Visible = (bool)IsExpandedValueForced;
            }
            //during pustback - manage state of Image button expandable/colapsable
            if (IsPostBack)
            {
                if (IsExpanded)
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                else
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/ExpandGlass.gif";
            }
        }

        public override void OnGLDataIDChanged()
        {
            base.OnGLDataIDChanged();
            _IsGLDataIDChanged = true;
            GLDataQualityScoreInfoList = null;
            BingGrids();
        }

        #endregion
    }
}