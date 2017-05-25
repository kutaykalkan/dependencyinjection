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
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Grid;
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.UserControls;
using System.Text;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Model.RecControlCheckList;


namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_RecControlCheckList : UserControlRecItemBase
    {
        #region Variables & Constants
        const int POPUP_WIDTH = 630;
        const int POPUP_HEIGHT = 480;
        ExImageButton ToggleControl;
        #endregion

        #region Properties
        private List<RecControlCheckListInfo> _RecControlCheckListInfoCollection = null;
        private bool? _IsExpandedValueForced = null;

        private List<RecControlCheckListInfo> GetRecControlCheckListInfoCollection
        {
            get
            {
                return this._RecControlCheckListInfoCollection;
            }
            set
            {
                this._RecControlCheckListInfoCollection = value;
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
        public bool? IsExpandedValueForced
        {
            get { return _IsExpandedValueForced; }
            set { _IsExpandedValueForced = value; }
        }

        public WebEnums.FormMode EditMode
        {
            get
            {
                return (WebEnums.FormMode)ViewState["EditMode"];
            }
            set
            {
                ViewState["EditMode"] = value;
            }
        }
        public string DivClientId
        {
            get { return divMainContent.ClientID; }
        }

        public bool IsDataLoadCondition
        {
            get
            {
                if (AccountID != null && NetAccountID != null && GLDataID != null && RecCategoryTypeID != null && IsSRA != null && RecCategoryID != null)
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

        public string OnRecControlListChanged { get; set; }
        #endregion

        #region Delegates & Events
        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            ucRecControlCheckListGrid.GridItemDataBound += new GridItemEventHandler(ucRecControlCheckListGrid_GridItemDataBound);
            ucRecControlCheckListGrid.GridCommand += new GridCommandEventHandler(ucRecControlCheckListGrid_GridCommand);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.EditMode = Helper.GetFormModeForRecControlCheckList(this.GLDataHdrInfo);
            ucRecControlCheckListGrid.EditMode = EditMode;
            if (!IsPostBack)
            {
                IsRefreshData = false;
                PopulateGrids();
            }

            SetControlState();
            if (EntityNameLabelID.HasValue)
            {
                ucRecControlCheckListGrid.BasePageTitleLabelID = EntityNameLabelID.Value;
            }
            ucRecControlCheckListGrid.OnRecControlListChanged = this.OnRecControlListChanged;

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            int lastPagePhraseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
        }
        #endregion

        #region Grid Events
        void ucRecControlCheckListGrid_GridCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int RecControlCheckListID = Convert.ToInt32(e.CommandArgument);
                List<RecControlCheckListInfo> oRecControlCheckListInfoAll = ucRecControlCheckListGrid.ALLRecControlCheckListInfoList;
                List<RecControlCheckListInfo> SelectedRecControlCheckListInfoList = (from obj in oRecControlCheckListInfoAll
                                                                                     where obj.RecControlCheckListID.Value == RecControlCheckListID
                                                                                     select obj).ToList();
                if (SelectedRecControlCheckListInfoList != null && SelectedRecControlCheckListInfoList.Count > 0)
                {
                    RecControlCheckListHelper.DeleteRecControlCheckListItems(SelectedRecControlCheckListInfoList);
                    RecHelper.RefreshRecForm(this);
                }
            }
        }
        void ucRecControlCheckListGrid_GridItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                RecControlCheckListInfo oRecControlCheckListInfo = (RecControlCheckListInfo)e.Item.DataItem;
                GridDataItem oItem = e.Item as GridDataItem;


                if ((e.Item as GridDataItem)["DeleteColumn"] != null)
                {
                    ImageButton deleteButton = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];
                    CheckBox chkSelectItem = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                    if (EditMode == WebEnums.FormMode.Edit && this.GLDataID.Value > 0)
                    {
                        if ((oRecControlCheckListInfo.RoleID == (short)ARTEnums.UserRole.PREPARER || oRecControlCheckListInfo.RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                            && (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER))
                        {
                            deleteButton.Visible = true;
                            chkSelectItem.Visible = true;

                        }
                        else
                        {
                            deleteButton.Visible = false;
                            chkSelectItem.Visible = false;
                        }
                    }
                    else
                    {
                        if (this.GLDataID.Value > 0 && oRecControlCheckListInfo.RoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN && SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN
                                && (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted))
                        {
                            deleteButton.Visible = true;
                            chkSelectItem.Visible = true;

                        }
                        else
                        {
                            deleteButton.Visible = false;
                            chkSelectItem.Visible = false;
                        }
                    }
                    deleteButton.CommandArgument = oRecControlCheckListInfo.RecControlCheckListID.ToString();
                }

            }
        }
        #endregion

        #region Other Events
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<RecControlCheckListInfo> SelectedRecControlCheckListInfoList = ucRecControlCheckListGrid.SelectedRecControlCheckListInfoCollection();
            if (SelectedRecControlCheckListInfoList != null && SelectedRecControlCheckListInfoList.Count > 0)
            {
                RecControlCheckListHelper.DeleteRecControlCheckListItems(SelectedRecControlCheckListInfoList);
                RecHelper.RefreshRecForm(this);
            }
        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            string pathAndQuery = (string)ViewState[ViewStateConstants.PATH_AND_QUERY];
            Response.Redirect(pathAndQuery);
        }
        public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            this._RecControlCheckListInfoCollection = null;
            if (IsPostBack)
            {
                IsRefreshData = true;
                if (IsExpanded)
                {
                    LoadData();
                }
            }
        }
        #endregion

        #region Validation Control Events
        #endregion

        #region Private Methods
        private void SetControlState()
        {
            if (IsPostBack && IsRefreshData && IsDataLoadCondition)
                LoadData();
            ExpandCollapse();
        }
        private DataTable GetGLDataParams()
        {
            DataTable dtGLData = new DataTable();
            //dtGLData.Columns.Add("ID");
            //List<GLDataRecItemInfo> GLDataRecItemIDCollection = ucGLDataRecItemGrid.SelectedGLDataRecItemInfoCollection();
            //if (GLDataRecItemIDCollection != null && GLDataRecItemIDCollection.Count > 0)
            //{
            //    for (int i = 0; i < GLDataRecItemIDCollection.Count; i++)
            //    {
            //        if (!GLDataRecItemIDCollection[i].IsForwardedItem.GetValueOrDefault())
            //        {
            //            DataRow rowGLData = dtGLData.NewRow();
            //            rowGLData["ID"] = GLDataRecItemIDCollection[i].GLDataRecItemID;
            //            dtGLData.Rows.Add(rowGLData);
            //        }
            //    }
            //}
            return dtGLData;
        }
        private string GetPreviousPageName()
        {
            string PName = "";
            if (Request.UrlReferrer != null)
            {
                PName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
            }
            return PName;
        }
        private void GetDataFromService()
        {

            GetRecControlCheckListInfoCollection = RecControlCheckListHelper.GetRecControlCheckListInfoList(this.GLDataHdrInfo.GLDataID, SessionHelper.CurrentReconciliationPeriodID);
        }
        #endregion

        #region Other Methods
        public override void LoadData()
        {
            if (IsRefreshData && IsExpanded)
            {
                this.EditMode = Helper.GetFormModeForRecControlCheckList(this.GLDataHdrInfo);
                ucRecControlCheckListGrid.EditMode = EditMode;

                if (this.IsPostBack)
                {
                    this._RecControlCheckListInfoCollection = null;
                    btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForAddRecControlCheckList(null, "OpenRadWindowWithName", "AddRecControlCheckList.aspx", (short)this.EditMode, this.AccountID, this.GLDataID, this.NetAccountID, hdIsRefreshData.ClientID);
                    btnAdd.Attributes.Add("onclick", "return false;");
                    ucRecControlCheckListGrid.OnRecControlListChanged = this.OnRecControlListChanged;
                }
                this.EnableDisableControls();
                PopulateGrids();
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
        public void RegisterToggleControl(ExImageButton imgToggleControl)
        {
            imgToggleControl.OnClientClick += "return ToggleDiv('" + imgToggleControl.ClientID + "','" + this.DivClientId + "','" + hdIsExpanded.ClientID + "','" + hdIsRefreshData.ClientID + "');";
            ToggleControl = imgToggleControl;
        }
        public void EnableDisableControls()
        {
            if (EditMode == WebEnums.FormMode.Edit && this.GLDataID.Value > 0)
            {
                if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                {
                    btnAdd.Visible = true;
                    btnDelete.Visible = true;
                }
                else if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN)
                {
                    btnAdd.Visible = false;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnDelete.Visible = false;
                }
            }
            else if (this.GLDataID.Value > 0 && SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN
                        && (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted))
            {
                btnDelete.Visible = true;
                btnAdd.Visible = false;
            }
            else
            {
                btnAdd.Visible = false;
                btnDelete.Visible = false;
            }

        }
        public List<GLDataRecControlCheckListInfo> GetGLDataRecControlCheckListInfoList()
        {
            return ucRecControlCheckListGrid.GetGLDataRecControlCheckListInfoList();
        }
        public void PopulateGrids()
        {
            if (this.GLDataID == null || this.GLDataID == 0)
            {
                this.GetRecControlCheckListInfoCollection = null;
                ucRecControlCheckListGrid.SetGLDataRecItemGridData(GetRecControlCheckListInfoCollection);

            }
            else
            {
                ucRecControlCheckListGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                GetDataFromService();
                ucRecControlCheckListGrid.SetGLDataRecItemGridData(GetRecControlCheckListInfoCollection);
                ucRecControlCheckListGrid.LoadGridData();
               
            }
        }
        public override void ExpandCollapse()
        {
            base.ExpandCollapse();
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
        #endregion

    }
}