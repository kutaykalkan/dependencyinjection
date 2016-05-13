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
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Client.Model.RecControlCheckList;


namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_RecControlCheckListGrid : UserControlRecItemBase
    {
        #region Variables & Constants
        private bool selectOption = true;
        bool isExportPDF;
        bool isExportExcel;
        const int POPUP_WIDTH = 630;
        const int POPUP_HEIGHT = 480;
        private int _BasePageTitleLabelID = 0;
        const int GRID_COLUMN_INDEX_SELECT = 0;
        #endregion

        #region Properties
        List<CompanyAttributeConfigInfo> oAttributeConfigInfo;
        private bool _AllowExportToExcel = false;
        private bool _AllowExportToPDF = false;
        private bool _AllowCustomPaging = false;
        private bool _AllowSelectionPersist = false;
        private bool _IsOnPage = true;
        private static Int32 _CompletedRecCount;


        /// <summary>
        /// Base Page Title 
        /// </summary>
        public int BasePageTitleLabelID
        {
            get { return _BasePageTitleLabelID; }
            set { _BasePageTitleLabelID = value; }
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
        /// <summary>
        /// The contained control "RadGrid"
        /// </summary>
        public ExRadGrid Grid
        {
            get
            {
                return rgRecControlCheckListItems;
            }
        }
        /// <summary>
        /// Allow Export To Excel
        /// </summary>
        public bool AllowExportToExcel
        {
            get { return _AllowExportToExcel; }
            set
            {
                _AllowExportToExcel = value;
                rgRecControlCheckListItems.AllowExportToExcel = _AllowExportToExcel;
            }

        }
        public bool AllowExportToPDF
        {
            get { return _AllowExportToPDF; }
            set
            {
                _AllowExportToPDF = value;
                rgRecControlCheckListItems.AllowExportToPDF = _AllowExportToPDF;
            }
        }
        public bool AllowCustomPaging
        {
            get { return _AllowCustomPaging; }
            set
            {
                _AllowCustomPaging = value;
                rgRecControlCheckListItems.AllowCustomPaging = _AllowCustomPaging;
            }
        }
        public bool AllowSelectionPersist
        {
            get { return _AllowSelectionPersist; }
            set
            {
                _AllowSelectionPersist = value;
            }
        }
        public bool IsOnPage
        {
            get { return _IsOnPage; }
            set
            { _IsOnPage = value; }
        }
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridColumnCollection ColumnCollection
        {
            get
            {
                return rgRecControlCheckListItems.MasterTableView.Columns;
            }
        }
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridClientSettings ClientSettings
        {
            get
            {
                return rgRecControlCheckListItems.ClientSettings;
            }
        }
        public bool ShowSelectCheckBoxColum
        {
            get { return selectOption; }
            set { selectOption = value; }
        }
        public List<RecControlCheckListInfo> ALLRecControlCheckListInfoList
        {
            get { return (List<RecControlCheckListInfo>)ViewState[SessionConstants.REC_CONTROL_CHECK_LIST_GLDATA]; }
            set { ViewState[SessionConstants.REC_CONTROL_CHECK_LIST_GLDATA] = value; }
        }
        public static Int32 CompletedRecCount
        {
            get
            {
                if (HttpContext.Current.Session["CompleteRecStatusCount"] != null)
                {
                    _CompletedRecCount = Convert.ToInt32(HttpContext.Current.Session["CompleteRecStatusCount"]);
                }
                else if (HttpContext.Current.Session["CompleteRecStatusCount"] == null)
                {
                    _CompletedRecCount = 0;
                }

                return _CompletedRecCount;
            }
            set
            {
                _CompletedRecCount = value;
            }
        }
        public string OnRecControlListChanged { get; set; }
        #endregion

        #region Delegates & Events
        public event GridCommandEventHandler GridCommand;
        public event GridItemEventHandler GridItemDataBound;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            Helper.SetGridImageButtonProperties(this.rgRecControlCheckListItems.MasterTableView.Columns);
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                Session[rgRecControlCheckListItems.ClientID + "NewPageSize"] = "10";
            }
            oAttributeConfigInfo = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_AllowSelectionPersist)
                RePopulateCheckBoxStates();
        }
        #endregion

        #region Grid Events
        protected void rgRecControlCheckListItems_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgRecControlCheckListItems.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[GetGridClientIDKey(rgRecControlCheckListItems) + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[GetGridClientIDKey(rgRecControlCheckListItems) + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgRecControlCheckListItems.ClientID + "');");
                    oRadComboBox.Visible = true;
                }
                else
                {
                    Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                    pnlPageSizeDDL.Visible = false;
                }
                Control numericPagerControl = gridPager.GetNumericPager();
                Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
                placeHolder.Controls.Add(numericPagerControl);
            }

            GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);

        }
        protected void rgRecControlCheckListItems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Header)
            {
                rgRecControlCheckListItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                RecControlCheckListInfo oRecControlCheckListInfo = (RecControlCheckListInfo)e.Item.DataItem;
                GridDataItem oItem = e.Item as GridDataItem;

                ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
                ExLabel lblCheckListNumber = (ExLabel)e.Item.FindControl("lblCheckListNumber");
                DropDownList ddlCompleted = (DropDownList)e.Item.FindControl("ddlCompleted");
                DropDownList ddlReviewed = (DropDownList)e.Item.FindControl("ddlReviewed");
                ExHyperLink hlAddComment = (ExHyperLink)e.Item.FindControl("hlAddComment");
                ListControlHelper.BindYesNoDropdown(ddlCompleted);
                ListControlHelper.BindYesNoDropdown(ddlReviewed);
                lblDescription.Text = Helper.GetDisplayStringValue(oRecControlCheckListInfo.Description);
                lblCheckListNumber.Text = Helper.GetDisplayStringValue(oRecControlCheckListInfo.CheckListNumber);
                GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo = oRecControlCheckListInfo.oGLDataRecControlCheckListInfo;
                if (oGLDataRecControlCheckListInfo != null)
                {
                    if (oGLDataRecControlCheckListInfo.CompletedRecStatus.HasValue)
                    {
                        if (oGLDataRecControlCheckListInfo.CompletedRecStatus.Value == 1)
                            ddlCompleted.SelectedValue = RecControlCheckListStatus.Yes;
                        else if (oGLDataRecControlCheckListInfo.CompletedRecStatus.Value == 2)
                            ddlCompleted.SelectedValue = RecControlCheckListStatus.No;
                        else if (oGLDataRecControlCheckListInfo.CompletedRecStatus.Value == 3)
                            ddlCompleted.SelectedValue = RecControlCheckListStatus.NA;
                    }
                    else
                        ddlCompleted.SelectedValue = RecControlCheckListStatus.No;

                    if (oGLDataRecControlCheckListInfo.ReviewedRecStatus.HasValue)
                    {
                        if (oGLDataRecControlCheckListInfo.ReviewedRecStatus.Value == 1)
                            ddlReviewed.SelectedValue = RecControlCheckListStatus.Yes;
                        else if (oGLDataRecControlCheckListInfo.ReviewedRecStatus.Value == 2)
                            ddlReviewed.SelectedValue = RecControlCheckListStatus.No;
                        else if (oGLDataRecControlCheckListInfo.ReviewedRecStatus.Value == 3)
                            ddlReviewed.SelectedValue = RecControlCheckListStatus.NA;
                    }
                    else
                        ddlReviewed.SelectedValue = RecControlCheckListStatus.No;

                    if (oGLDataRecControlCheckListInfo.IsCommentAvailable.HasValue && oGLDataRecControlCheckListInfo.IsCommentAvailable.Value)
                        hlAddComment.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/Comment_Green.gif";
                    else
                        hlAddComment.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/Comment.gif";

                    //if (oGLDataRecControlCheckListInfo.GLDataRecControlCheckListID.HasValue)
                    hlAddComment.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupHelper.getGlDataRecControlCheckListCommentPopupUrl(this.GLDataID, (short)EditMode, oGLDataRecControlCheckListInfo.GLDataRecControlCheckListID, oRecControlCheckListInfo.RecControlCheckListID) + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
                }
                ExImageButton imgViewFile = (ExImageButton)e.Item.FindControl("imgViewFile");
                if (oRecControlCheckListInfo.DataImportID.HasValue && oRecControlCheckListInfo.DataImportID.Value > 0)
                {
                    if (oRecControlCheckListInfo.PhysicalPath != null)
                    {
                        imgViewFile.Visible = true;
                        string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(oRecControlCheckListInfo.PhysicalPath);
                        imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                    }
                }


                if (EditMode == WebEnums.FormMode.Edit)
                {

                    if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BACKUP_PREPARER)
                    {
                        ddlCompleted.Enabled = true;
                        ddlReviewed.Enabled = false;

                    }
                    // Reviewer can modify only one below
                    else if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.REVIEWER || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
                    {
                        ddlReviewed.Enabled = true;
                        ddlCompleted.Enabled = false;
                    }
                    else
                    {
                        ddlReviewed.Enabled = false;
                        ddlCompleted.Enabled = false;
                    }
                }
                else
                {
                    ddlReviewed.Enabled = false;
                    ddlCompleted.Enabled = false;
                    if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.AUDIT && oAttributeConfigInfo != null)
                    {
                        CompanyAttributeConfigInfo oNotSeeRecControlChecklist = oAttributeConfigInfo.Find(c => c.AttributeID == (short)ARTEnums.AttributeList.NotSeeRecControlChecklist);
                        if (oNotSeeRecControlChecklist != null && oNotSeeRecControlChecklist.IsEnabled.HasValue)
                            hlAddComment.Enabled = !oNotSeeRecControlChecklist.IsEnabled.Value;
                    }
                    else
                        hlAddComment.Enabled = true;
                }

                ddlCompleted.Attributes.Add("onchange", "OnUserResponseRecChange('" + rgRecControlCheckListItems.ClientID
                    + "','Completed','" + this.OnRecControlListChanged + "');");

                ddlReviewed.Attributes.Add("onchange", "OnUserResponseRecChangeReviewed('" + rgRecControlCheckListItems.ClientID
                        + "','Reviewed','" + this.OnRecControlListChanged + "');");
            }

            if (GridItemDataBound != null)
                GridItemDataBound(sender, e);

        }
        protected void rgRecControlCheckListItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            rgRecControlCheckListItems.DataSource = ALLRecControlCheckListInfoList;
            if (ALLRecControlCheckListInfoList != null)
                rgRecControlCheckListItems.VirtualItemCount = ALLRecControlCheckListInfoList.Count;

        }
        protected void rgRecControlCheckListItems_ItemCommand(object source, GridCommandEventArgs e)
        {
            // Raise Event for Page to Handle it
            if (GridCommand != null)
            {
                GridCommand(source, e);
            }

            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgRecControlCheckListItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgRecControlCheckListItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }
                MangeColumnsForExport(true);
                GridHelper.ExportGridToPDF(rgRecControlCheckListItems, this.BasePageTitleLabelID);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgRecControlCheckListItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }

                rgRecControlCheckListItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                MangeColumnsForExport(true);
                GridHelper.ExportGridToExcel(rgRecControlCheckListItems, this.BasePageTitleLabelID);

            }
        }
        protected void rgRecControlCheckListItems_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            if (!(isExportPDF || isExportExcel))
                Session[GetGridClientIDKey(rgRecControlCheckListItems) + "NewPageSize"] = e.NewPageSize.ToString();
        }
        protected void rgRecControlCheckListItems_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            if (_AllowSelectionPersist)
                SaveCheckBoxStates();
            BindrgRecControlCheckListItems(e.NewPageIndex);

        }
        protected void rgRecControlCheckListItems_PdfExporting(object source, GridPdfExportingArgs e)
        {
            ExportHelper.GeneratePDFAndRender(ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)),
            ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)), e.RawHTML, false, false);
        }

        #endregion

        #region Other Events
        #endregion

        #region Validation Control Events
        #endregion

        #region Private Methods
        private void BindrgRecControlCheckListItems(int PageIndex)
        {
            if (ALLRecControlCheckListInfoList != null)
            {
                long? _gLDataID = null;

                if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
                {
                    _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                }
                GLDataHdrInfo oGLDataHdrInfo = this.GLDataHdrInfo;
                if (this.GLDataHdrInfo != null && this.GLDataHdrInfo.GLDataID.GetValueOrDefault() == _gLDataID.GetValueOrDefault())
                    GetGLDataRecControlCheckListInfoListValueFromControls();
                rgRecControlCheckListItems.DataSource = ALLRecControlCheckListInfoList;
                rgRecControlCheckListItems.VirtualItemCount = ALLRecControlCheckListInfoList.Count;
                rgRecControlCheckListItems.DataBind();
                MangeColumnsForExport(false);
            }
        }
        private List<int> GridSelectedItems()
        {
            List<int> oSelectedRecControlCheckListIDList = new List<int>();
            int RecControlCheckListID;
            foreach (GridDataItem item in rgRecControlCheckListItems.SelectedItems)
            {
                CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
                if (chkSelectItem != null && chkSelectItem.Checked)
                {
                    RecControlCheckListID = Convert.ToInt32(item.GetDataKeyValue("RecControlCheckListID"));
                    oSelectedRecControlCheckListIDList.Add(RecControlCheckListID);
                }


            }
            return oSelectedRecControlCheckListIDList;
        }
        private Int16? GetDDLSelectedValue(DropDownList DDL)
        {
            Int16? oDdlVal = null;
            if (DDL.SelectedValue == WebConstants.SELECT_ONE)
            {
                oDdlVal = null;
            }
            else if (DDL.SelectedValue == RecControlCheckListStatus.Yes)
            {
                oDdlVal = 1;
            }
            else if (DDL.SelectedValue == RecControlCheckListStatus.No)
            {
                oDdlVal = 2;
            }
            else if (DDL.SelectedValue == RecControlCheckListStatus.NA)
            {
                oDdlVal = 3;
            }
            return oDdlVal;

        }
        private string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }

        private string GetUniqueSessionKey()
        {
            return GetGridClientIDKey(rgRecControlCheckListItems) + SessionConstants.REC_CONTROL_CHECK_LIST_GLDATA;
        }
        private void ClearGLDataRecItemGridData()
        {
            ALLRecControlCheckListInfoList = null;

        }
        /// <summary>
        /// get Checked Items SessionKey
        /// </summary>   
        private string getCheckedItemsSessionKey()
        {
            return SessionConstants.CHECKED_ITEMS + rgRecControlCheckListItems.ClientID;
        }
        /// <summary>
        /// Save Check Box States
        /// </summary>  
        private void SaveCheckBoxStates()
        {
            List<int> oSelectedRecControlCheckListIDList;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedRecControlCheckListIDList = (List<int>)Session[getCheckedItemsSessionKey()];
            else
                oSelectedRecControlCheckListIDList = new List<int>();
            int RecControlCheckListID;
            foreach (GridDataItem item in rgRecControlCheckListItems.Items)
            {
                RecControlCheckListID = Convert.ToInt32(item.GetDataKeyValue("RecControlCheckListID"));
                if (item.Selected == true)
                {
                    if (oSelectedRecControlCheckListIDList != null && !oSelectedRecControlCheckListIDList.Contains(RecControlCheckListID))
                        oSelectedRecControlCheckListIDList.Add(RecControlCheckListID);
                }
                else
                {
                    if (oSelectedRecControlCheckListIDList != null && oSelectedRecControlCheckListIDList.Contains(RecControlCheckListID))
                        oSelectedRecControlCheckListIDList.Remove(RecControlCheckListID);
                }

            }
            if (oSelectedRecControlCheckListIDList != null && oSelectedRecControlCheckListIDList.Count > 0)
            {
                Session[getCheckedItemsSessionKey()] = oSelectedRecControlCheckListIDList;
            }

        }
        /// <summary>
        /// RePopulate CheckBox States
        /// </summary> 
        private void RePopulateCheckBoxStates()
        {

            List<int> oSelectedRecControlCheckListIDList = null;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedRecControlCheckListIDList = (List<int>)Session[getCheckedItemsSessionKey()];
            if (oSelectedRecControlCheckListIDList != null && oSelectedRecControlCheckListIDList.Count > 0)
            {
                int RecControlCheckListID;
                foreach (GridDataItem item in rgRecControlCheckListItems.Items)
                {
                    RecControlCheckListID = Convert.ToInt32(item.GetDataKeyValue("RecControlCheckListID"));
                    if (oSelectedRecControlCheckListIDList != null && oSelectedRecControlCheckListIDList.Contains(RecControlCheckListID))
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        private void ClearCheckedItemViewState()
        {
            Session[getCheckedItemsSessionKey()] = null;
        }
        private void MangeColumnsForExport(bool IsVisible)
        {
            //GridColumn oGridDeleteColumn = null;
            //oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("LCCYCode");
            //if (oGridDeleteColumn != null)
            //{
            //    oGridDeleteColumn.Visible = IsVisible;
            //}
            //oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("AmountImport");
            //if (oGridDeleteColumn != null)
            //{
            //    oGridDeleteColumn.Visible = IsVisible;
            //}
            //oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("DateForImport");
            //if (oGridDeleteColumn != null)
            //{
            //    oGridDeleteColumn.Visible = IsVisible;
            //}
            //oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("RefNo");
            //if (oGridDeleteColumn != null)
            //{
            //    oGridDeleteColumn.Visible = IsVisible;
            //}
            //oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("Amount");
            //if (oGridDeleteColumn != null)
            //{
            //    oGridDeleteColumn.Visible = !IsVisible;
            //}
            //oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("OpenDate");
            //if (oGridDeleteColumn != null)
            //{
            //    oGridDeleteColumn.Visible = !IsVisible;
            //}
            //oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("RecItemCommentForImport");
            //if (oGridDeleteColumn != null)
            //{
            //    oGridDeleteColumn.Visible = IsVisible;
            //}

        }
        #endregion

        #region Other Methods
        public void LoadGridData()
        {
            BindrgRecControlCheckListItems(0);
        }
        public List<int> SelectedRecControlCheckListIDs()
        {
            List<int> oSelectedRecControlCheckListIDs = null;
            if (_AllowSelectionPersist)
            {
                SaveCheckBoxStates();
                if (Session[getCheckedItemsSessionKey()] != null)
                    oSelectedRecControlCheckListIDs = (List<int>)Session[getCheckedItemsSessionKey()];
            }
            else
                oSelectedRecControlCheckListIDs = GridSelectedItems();
            return oSelectedRecControlCheckListIDs;
        }
        public List<RecControlCheckListInfo> SelectedRecControlCheckListInfoCollection()
        {
            List<RecControlCheckListInfo> oRecControlCheckListInfoCollection = null;

            List<int> oSelectedoRecControlCheckListIDList = GridSelectedItems();
            if (ALLRecControlCheckListInfoList != null && oSelectedoRecControlCheckListIDList != null)
                oRecControlCheckListInfoCollection = (from oRecControlCheckListInfo in ALLRecControlCheckListInfoList
                                                      join RecControlCheckListID in oSelectedoRecControlCheckListIDList on oRecControlCheckListInfo.RecControlCheckListID equals RecControlCheckListID
                                                      select oRecControlCheckListInfo).ToList();
            return oRecControlCheckListInfoCollection;
        }
        public void GetGLDataRecControlCheckListInfoListValueFromControls()
        {
            int? RecControlCheckListID;
            long? GLDataRecControlCheckListID;
            foreach (GridDataItem item in rgRecControlCheckListItems.Items)
            {
                RecControlCheckListID = (int?)item.GetDataKeyValue("RecControlCheckListID");
                GLDataRecControlCheckListID = (long?)item.GetDataKeyValue("GLDataRecControlCheckListID");
                DropDownList ddlCompleted = (DropDownList)item.FindControl("ddlCompleted");
                DropDownList ddlReviewed = (DropDownList)item.FindControl("ddlReviewed");
                RecControlCheckListInfo oRecControlCheckListInfo = ALLRecControlCheckListInfoList.Find(T => T.RecControlCheckListID == RecControlCheckListID);
                if (oRecControlCheckListInfo != null && oRecControlCheckListInfo.oGLDataRecControlCheckListInfo != null)
                {
                    if (ddlCompleted != null && ddlCompleted.Enabled && (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BACKUP_PREPARER))
                    {
                        if (ddlCompleted.SelectedValue != WebConstants.SELECT_ONE)
                            oRecControlCheckListInfo.oGLDataRecControlCheckListInfo.CompletedRecStatus = GetDDLSelectedValue(ddlCompleted);

                    }
                    if (ddlReviewed != null && ddlReviewed.Enabled && (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.REVIEWER || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BACKUP_REVIEWER))
                    {
                        if (ddlCompleted.SelectedValue != WebConstants.SELECT_ONE)
                            oRecControlCheckListInfo.oGLDataRecControlCheckListInfo.ReviewedRecStatus = GetDDLSelectedValue(ddlReviewed);
                    }
                }
            }
        }
        public List<GLDataRecControlCheckListInfo> GetGLDataRecControlCheckListInfoList()
        {
            //rgRecControlCheckListItems.AllowPaging = false;
            //rgRecControlCheckListItems.Rebind();
            List<GLDataRecControlCheckListInfo> oGLDataRecControlCheckListInfoList = new List<GLDataRecControlCheckListInfo>();
            int? RecControlCheckListID;
            long? GLDataRecControlCheckListID;
            GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo;
            if (rgRecControlCheckListItems.Items != null && rgRecControlCheckListItems.Items.Count > 0)
            {
                foreach (GridDataItem item in rgRecControlCheckListItems.Items)
                {
                    RecControlCheckListID = (int?)item.GetDataKeyValue("RecControlCheckListID");
                    GLDataRecControlCheckListID = (long?)item.GetDataKeyValue("GLDataRecControlCheckListID");
                    DropDownList ddlCompleted = (DropDownList)item.FindControl("ddlCompleted");
                    DropDownList ddlReviewed = (DropDownList)item.FindControl("ddlReviewed");
                    RecControlCheckListInfo oRecControlCheckListInfo = ALLRecControlCheckListInfoList.Find(T => T.RecControlCheckListID == RecControlCheckListID);
                    oGLDataRecControlCheckListInfo = oRecControlCheckListInfo.oGLDataRecControlCheckListInfo;
                    if (oGLDataRecControlCheckListInfo != null)
                    {
                        oGLDataRecControlCheckListInfo.GLDataRecControlCheckListID = GLDataRecControlCheckListID;
                        oGLDataRecControlCheckListInfo.RecControlCheckListID = RecControlCheckListID;
                        oGLDataRecControlCheckListInfo.IsActive = true;
                        oGLDataRecControlCheckListInfo.GLDataID = this.GLDataID;
                    }
                    else
                    {
                        oGLDataRecControlCheckListInfo = new GLDataRecControlCheckListInfo();
                        oGLDataRecControlCheckListInfo.RecControlCheckListID = RecControlCheckListID;
                        oGLDataRecControlCheckListInfo.GLDataRecControlCheckListID = GLDataRecControlCheckListID;
                        oGLDataRecControlCheckListInfo.IsActive = true;
                        oGLDataRecControlCheckListInfo.GLDataID = this.GLDataID;
                    }
                    if (ddlCompleted != null && ddlCompleted.Enabled && (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BACKUP_PREPARER))
                    {
                        oGLDataRecControlCheckListInfo.CompletedRecStatus = GetDDLSelectedValue(ddlCompleted);
                        if (oGLDataRecControlCheckListInfo.CompletedRecStatus.HasValue)
                        {
                            oGLDataRecControlCheckListInfo.CompletedBy = SessionHelper.CurrentUserID;
                            oGLDataRecControlCheckListInfo.DateCompleted = System.DateTime.Now;
                        }
                        if (string.IsNullOrEmpty(oGLDataRecControlCheckListInfo.AddedBy))
                            oGLDataRecControlCheckListInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                        if (!oGLDataRecControlCheckListInfo.DateAdded.HasValue)
                            oGLDataRecControlCheckListInfo.DateAdded = System.DateTime.Now;
                        if (oGLDataRecControlCheckListInfo.RevisedBy != null)
                            oGLDataRecControlCheckListInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                        if (oGLDataRecControlCheckListInfo.DateRevised.HasValue)
                            oGLDataRecControlCheckListInfo.DateRevised = System.DateTime.Now;
                    }
                    if (ddlReviewed != null && ddlReviewed.Enabled && (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.REVIEWER || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BACKUP_REVIEWER))
                    {
                        oGLDataRecControlCheckListInfo.ReviewedRecStatus = GetDDLSelectedValue(ddlReviewed);
                        if (oGLDataRecControlCheckListInfo.ReviewedRecStatus.HasValue)
                        {
                            oGLDataRecControlCheckListInfo.ReviewedBy = SessionHelper.CurrentUserID;
                            oGLDataRecControlCheckListInfo.DateReviewed = System.DateTime.Now;
                            oGLDataRecControlCheckListInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                            oGLDataRecControlCheckListInfo.DateRevised = System.DateTime.Now;
                        }
                    }
                    oGLDataRecControlCheckListInfoList.Add(oGLDataRecControlCheckListInfo);
                }
                //rgRecControlCheckListItems.AllowPaging = true;
                //rgRecControlCheckListItems.Rebind();
                if (oGLDataRecControlCheckListInfoList != null && oGLDataRecControlCheckListInfoList.Count != ALLRecControlCheckListInfoList.Count && (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BACKUP_PREPARER))
                {
                    foreach (RecControlCheckListInfo item in ALLRecControlCheckListInfoList)
                    {
                        RecControlCheckListID = item.RecControlCheckListID;
                        GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfoTemp = oGLDataRecControlCheckListInfoList.Find(T => T.RecControlCheckListID == RecControlCheckListID);
                        if (oGLDataRecControlCheckListInfoTemp == null)
                        {
                            oGLDataRecControlCheckListInfo = item.oGLDataRecControlCheckListInfo;
                            if (oGLDataRecControlCheckListInfo != null)
                            {
                                oGLDataRecControlCheckListInfo.IsActive = true;
                                oGLDataRecControlCheckListInfo.GLDataID = this.GLDataID;
                            }
                            else
                            {
                                oGLDataRecControlCheckListInfo = new GLDataRecControlCheckListInfo();
                                oGLDataRecControlCheckListInfo.RecControlCheckListID = item.RecControlCheckListID;
                                oGLDataRecControlCheckListInfo.GLDataRecControlCheckListID = item.GLDataRecControlCheckListID;
                                oGLDataRecControlCheckListInfo.IsActive = true;
                                oGLDataRecControlCheckListInfo.GLDataID = this.GLDataID;
                            }
                            if (string.IsNullOrEmpty(oGLDataRecControlCheckListInfo.AddedBy))
                                oGLDataRecControlCheckListInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                            if (!oGLDataRecControlCheckListInfo.DateAdded.HasValue)
                                oGLDataRecControlCheckListInfo.DateAdded = System.DateTime.Now;
                            oGLDataRecControlCheckListInfoList.Add(oGLDataRecControlCheckListInfo);
                        }
                    }
                }
            }
            return oGLDataRecControlCheckListInfoList;
        }
        public void SetGLDataRecItemGridData(List<RecControlCheckListInfo> oRecControlCheckListInfoCollection)
        {
            ClearGLDataRecItemGridData();
            if (oRecControlCheckListInfoCollection != null)
            {
                ALLRecControlCheckListInfoList = oRecControlCheckListInfoCollection;
                ClearCheckedItemViewState();
            }
        }
        #endregion
    }
}
