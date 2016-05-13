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
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.Library.Controls.TelerikWebControls.Grid.Templates;
using SkyStem.Library.Controls.TelerikWebControls.ToolBar;


namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_GeneralTaskGrid : UserControlTaskMasterBase
    {
        #region Variables

        const int GRID_COLUMN_INDEX_KEY_START = 2;
        bool isExportPDF;
        bool isExportExcel;
        private int _BasePageTitleLabelID = 0;
        const int GRID_COLUMN_INDEX_SELECT = 0;
        //public delegate GridItemEventHandler 
        public event GridCommandEventHandler GridCommand;
        public event GridItemEventHandler GridItemDataBound;
        public RadToolBar RadToolBar;
        bool _isCompletionDocsEditable = false;
        string _completionDocsMode = string.Empty;
        WebEnums.FormMode eFormMode;
        //bool bGetFormMode = true;
        #endregion

        #region Public Properties
        /// <summary>
        /// Base Page Title 
        /// </summary>
        public int BasePageTitleLabelID
        {
            get { return _BasePageTitleLabelID; }
            set { _BasePageTitleLabelID = value; }
        }

        /// <summary>
        /// The contained control "RadGrid"
        /// </summary>
        public ExRadGrid Grid
        {
            get
            {
                return rgGeneralTasks;
            }
        }

        /// <summary>
        /// Allow Action Menu
        /// </summary>
        private bool _AllowActionMenu = false;
        public bool AllowActionMenu
        {
            get { return _AllowActionMenu; }
            set
            {
                _AllowActionMenu = value;
            }

        }

        /// <summary>
        /// Allow Export To Excel
        /// </summary>
        private bool _AllowExportToExcel = false;
        public bool AllowExportToExcel
        {
            get { return _AllowExportToExcel; }
            set
            {
                _AllowExportToExcel = value;
                rgGeneralTasks.AllowExportToExcel = _AllowExportToExcel;
            }

        }
        /// <summary>
        /// Allow Export To Excel
        /// </summary>
        private bool _AllowExportToPDF = false;
        public bool AllowExportToPDF
        {
            get { return _AllowExportToPDF; }
            set
            {
                _AllowExportToPDF = value;
                rgGeneralTasks.AllowExportToPDF = _AllowExportToPDF;
            }
        }

        /// <summary>
        /// Allow  Custom Filter
        /// </summary>
        private bool _AllowCustomFilter = false;
        public bool AllowCustomFilter
        {
            get { return _AllowCustomFilter; }
            set
            {
                _AllowCustomFilter = value;
                rgGeneralTasks.AllowCustomFilter = _AllowCustomFilter;
            }
        }

        /// <summary>
        /// Allow  Custom Filter
        /// </summary>
        private bool _AllowCustomization = false;
        public bool AllowCustomization
        {
            get { return _AllowCustomization; }
            set
            {
                _AllowCustomization = value;
                rgGeneralTasks.AllowCustomization = _AllowCustomization;
            }
        }

        /// <summary>
        /// Allow  Custom Paging
        /// </summary>
        private bool _AllowCustomPaging = false;
        public bool AllowCustomPaging
        {
            get { return _AllowCustomPaging; }
            set
            {
                _AllowCustomPaging = value;
                rgGeneralTasks.AllowCustomPaging = _AllowCustomPaging;
            }
        }

        /// <summary>
        /// Allow  Custom Paging
        /// </summary>
        private bool _AllowSelectionPersist = false;
        public bool AllowSelectionPersist
        {
            get { return _AllowSelectionPersist; }
            set
            {
                _AllowSelectionPersist = value;
            }
        }

        /// <summary>
        /// Is On Page
        /// </summary>
        private bool _IsOnPage = true;
        public bool IsOnPage
        {
            get { return _IsOnPage; }
            set
            { _IsOnPage = value; }
        }

        /// <summary>
        /// Allow  Custom Import
        /// </summary>
        private bool _AllowCustomImport = false;
        public bool AllowCustomImport
        {
            get { return _AllowCustomImport; }
            set
            {
                _AllowCustomImport = value;
                rgGeneralTasks.AllowCustomImport = _AllowCustomImport;
            }
        }

        /// <summary>
        /// Allow  Custom Add
        /// </summary>
        private bool _AllowCustomAdd = false;
        public bool AllowCustomAdd
        {
            get { return _AllowCustomAdd; }
            set
            {
                _AllowCustomAdd = value;
                rgGeneralTasks.AllowCustomAdd = _AllowCustomAdd;
            }
        }

        /// <summary>
        /// Allow  Custom Edit
        /// </summary>
        private bool _AllowCustomEdit = false;
        public bool AllowCustomEdit
        {
            get { return _AllowCustomEdit; }
            set
            {
                _AllowCustomEdit = value;
                rgGeneralTasks.AllowCustomEdit = _AllowCustomEdit;
            }
        }

        /// <summary>
        /// Allow  Custom Delete
        /// </summary>
        private bool _AllowCustomDelete = false;
        public bool AllowCustomDelete
        {
            get { return _AllowCustomDelete; }
            set
            {
                _AllowCustomDelete = value;
                rgGeneralTasks.AllowCustomDelete = _AllowCustomDelete;
            }
        }

        /// <summary>
        /// Allow  Custom Approve
        /// </summary>
        private bool _AllowCustomApprove = false;
        public bool AllowCustomApprove
        {
            get { return _AllowCustomApprove; }
            set
            {
                _AllowCustomApprove = value;
                rgGeneralTasks.AllowCustomApprove = _AllowCustomApprove;
            }
        }

        /// <summary>
        /// Allow  Custom Reject
        /// </summary>
        private bool _AllowCustomReject = false;
        public bool AllowCustomReject
        {
            get { return _AllowCustomReject; }
            set
            {
                _AllowCustomReject = value;
                rgGeneralTasks.AllowCustomReject = _AllowCustomReject;
            }
        }

        /// <summary>
        /// Allow  Custom Done
        /// </summary>
        private bool _AllowCustomDone = false;
        public bool AllowCustomDone
        {
            get { return _AllowCustomDone; }
            set
            {
                _AllowCustomDone = value;
                rgGeneralTasks.AllowCustomDone = _AllowCustomDone;
            }
        }

        /// <summary>
        /// Allow  Custom Close
        /// </summary>
        private bool _AllowCustomClose = false;
        public bool AllowCustomClose
        {
            get { return _AllowCustomClose; }
            set
            {
                _AllowCustomClose = value;
                rgGeneralTasks.AllowCustomClose = _AllowCustomClose;
            }
        }

        /// <summary>
        /// Allow  Custom Reopen
        /// </summary>
        private bool _AllowCustomReopen = false;
        public bool AllowCustomReopen
        {
            get { return _AllowCustomReopen; }
            set
            {
                _AllowCustomReopen = value;
                rgGeneralTasks.AllowCustomReopen = _AllowCustomReopen;
            }
        }

        /// <summary>
        /// Allow  Custom Review
        /// </summary>
        private bool _AllowCustomReview = false;
        public bool AllowCustomReview
        {
            get { return _AllowCustomReview; }
            set
            {
                _AllowCustomReview = value;
                rgGeneralTasks.AllowCustomReview = _AllowCustomReview;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridClientSettings ClientSettings
        {
            get
            {
                return rgGeneralTasks.ClientSettings;
            }
        }

        /// <summary>
        /// Show Select CheckBox Colum
        /// </summary>
        private bool selectOption = true;
        public bool ShowSelectCheckBoxColum
        {
            get { return selectOption; }
            set { selectOption = value; }
        }

        /// <summary>
        /// GLDataRecItems Grid ShowFooter
        /// </summary>
        public bool ShowFooter
        {
            get { return rgGeneralTasks.ShowFooter; }
            set { rgGeneralTasks.ShowFooter = value; }
        }

        public string GridCustomizeOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridCustomizeOnClientClick = value;
            }
        }
        public string GridApplyFilterOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyFilterOnClientClick = value;
            }
        }
        public string GridApplyImportOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyImportOnClientClick = value;
            }
        }
        public string GridApplyAddOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyAddOnClientClick = value;
            }
        }
        public string GridApplyEditOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyEditOnClientClick = value;
            }
        }
        public string GridApplyDeleteOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyDeleteOnClientClick = value;
            }
        }
        public string GridApplyApproveOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyApproveOnClientClick = value;
            }
        }
        public string GridApplyRejectOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyRejectOnClientClick = value;
            }
        }
        public string GridApplyDoneOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyDoneOnClientClick = value;
            }
        }
        public string GridApplyCloseOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyCloseOnClientClick = value;
            }
        }
        public string GridApplyReopenOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyReopenOnClientClick = value;
            }
        }
        public string GridApplyReviewOnClientClick
        {
            set
            {
                this.rgGeneralTasks.GridApplyReviewOnClientClick = value;
            }
        }
        private ARTEnums.Grid _GridType = ARTEnums.Grid.None;
        public ARTEnums.Grid GridType
        {
            get
            {

                return _GridType;
            }
            set
            {
                _GridType = value;
            }
        }

        /// <summary>
        /// Show Edit Column
        /// </summary>
        private bool _ShowEditColumn = false;
        public bool ShowEditColumn
        {
            get { return _ShowEditColumn; }
            set
            {
                _ShowEditColumn = value;
            }

        }
        /// <summary>
        /// Show Delete Task Load Button
        /// </summary>
        private bool _DeleteTaskLoad = false;
        public bool DeleteTaskLoad
        {
            get { return _DeleteTaskLoad; }
            set
            {
                _DeleteTaskLoad = value;
            }

        }

        /// <summary>
        /// Show Edit Column
        /// </summary>
        private bool _ShowByDefaultViewIcon = false;
        public bool ShowByDefaultViewIcon
        {
            get { return _ShowByDefaultViewIcon; }
            set
            {
                _ShowByDefaultViewIcon = value;
            }

        }
        public List<TaskHdrInfo> GetTaskHdrInfoListCollection
        {
            get
            {
                return TaskHdrInfoListCollection;
            }
        }
        /// <summary>
        /// Show Completion Docs Column
        /// </summary>
        public bool IsCompletionDocsEditable
        {
            get { return _isCompletionDocsEditable; }
            set { _isCompletionDocsEditable = value; }
        }

        # endregion

        #region private Properties
        /// <summary>
        ///Task Detail Info List Collection
        /// </summary>
        private List<TaskHdrInfo> TaskHdrInfoListCollection
        {
            get
            {
                return (List<TaskHdrInfo>)Session[GetUniqueSessionKey()];
            }
            set { Session[GetUniqueSessionKey()] = value; }
        }
        # endregion

        #region Page Events
        //protected override void OnInit(EventArgs e)
        //{
        //    if (_AllowActionMenu)
        //    {
        //        this.RadToolBar = new RadToolBar();
        //        this.RadToolBar.ButtonClick += new RadToolBarEventHandler(RadToolBar1_ButtonClick);

        //        ActionDropDown = new ExRadToolBarDropDown();
        //        ActionDropDown.LabelID = 2589;

        //        this.RadToolBar.Items.Add(ActionDropDown);

        //        ExRadToolBarButton btnAdd = new ExRadToolBarButton();
        //        btnAdd.LabelID = 1560;
        //        btnAdd.CommandName = ToolBarCommmand.TOOLBAR_COMMAND_ADD;
        //        btnAdd.Value = ToolBarCommmand.TOOLBAR_COMMAND_ADD;

        //        ExRadToolBarButton btnBulk = new ExRadToolBarButton();
        //        btnBulk.LabelID = 2572;
        //        btnBulk.CommandName = ToolBarCommmand.TOOLBAR_COMMAND_BULKEDIT;
        //        btnBulk.Value = ToolBarCommmand.TOOLBAR_COMMAND_BULKEDIT;

        //        ExRadToolBarButton btnDelete = new ExRadToolBarButton();
        //        btnDelete.LabelID = 1564;
        //        btnDelete.CommandName = ToolBarCommmand.TOOLBAR_COMMAND_DELETE;
        //        btnDelete.Value = ToolBarCommmand.TOOLBAR_COMMAND_DELETE;

        //        ExRadToolBarButton btnApprove = new ExRadToolBarButton();
        //        btnApprove.LabelID = 1483;
        //        btnApprove.CommandName = ToolBarCommmand.TOOLBAR_COMMAND_APPROVE;
        //        btnApprove.Value = ToolBarCommmand.TOOLBAR_COMMAND_APPROVE;

        //        ExRadToolBarButton btnReject = new ExRadToolBarButton();
        //        btnReject.LabelID = 1482;
        //        btnReject.CommandName = ToolBarCommmand.TOOLBAR_COMMAND_REJECT;
        //        btnReject.Value = ToolBarCommmand.TOOLBAR_COMMAND_REJECT;

        //        ExRadToolBarButton btnDone = new ExRadToolBarButton();
        //        btnDone.LabelID = 2590;
        //        btnDone.CommandName = ToolBarCommmand.TOOLBAR_COMMAND_DONE;
        //        btnDone.Value = ToolBarCommmand.TOOLBAR_COMMAND_DONE;

        //        ActionDropDown.Buttons.Add(btnAdd);
        //        ActionDropDown.Buttons.Add(btnBulk);
        //        ActionDropDown.Buttons.Add(btnDelete);
        //        ActionDropDown.Buttons.Add(btnApprove);
        //        ActionDropDown.Buttons.Add(btnReject);
        //        ActionDropDown.Buttons.Add(btnDone);
        //    }

        //    base.OnInit(e);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                Session[rgGeneralTasks.ClientID + "NewPageSize"] = "10";

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_AllowSelectionPersist)
                RePopulateCheckBoxStates();
        }
        # endregion

        #region private Methods

        /// <summary>
        /// Bind General Tasks Grid
        /// </summary>
        private void BindrgGeneralTasksGrid(int PageIndex)
        {

            if (TaskHdrInfoListCollection != null)//&& TaskHdrInfoListCollection.Count > 0)
            {
                rgGeneralTasks.DataSource = TaskHdrInfoListCollection;
                rgGeneralTasks.VirtualItemCount = TaskHdrInfoListCollection.Count;
                rgGeneralTasks.CurrentPageIndex = PageIndex;
                GridHelper.ShowHideColumns(GRID_COLUMN_INDEX_KEY_START, rgGeneralTasks.MasterTableView, this.GridType, rgGeneralTasks.AllowCustomization);
                if (PageIndex > 0)
                {
                    rgGeneralTasks.DataBind();
                }
                else
                    rgGeneralTasks.Rebind();
                if (_ShowEditColumn)
                {
                    GridColumn oGridColumn = null;
                    oGridColumn = rgGeneralTasks.MasterTableView.Columns.FindByUniqueNameSafe("Edit");
                    if (oGridColumn != null)
                    {
                        oGridColumn.Visible = true;
                    }
                }
                GridColumn oGridColumnCommentIcon = null;
                oGridColumnCommentIcon = rgGeneralTasks.MasterTableView.Columns.FindByUniqueNameSafe("CommentIcon");
                if (oGridColumnCommentIcon != null)
                {
                    oGridColumnCommentIcon.Visible = true;
                }
                if (DeleteTaskLoad)
                {
                    GridColumn oGridColumnDeleteTaskLoad = null;
                    oGridColumnDeleteTaskLoad = rgGeneralTasks.MasterTableView.Columns.FindByUniqueNameSafe("DeleteTaskLoad");
                    if (oGridColumnDeleteTaskLoad != null)
                    {
                        oGridColumnDeleteTaskLoad.Visible = true;
                    }

                }


            }
        }

        /// <summary>
        /// Selected TaskDetailID on Current Page
        /// </summary>
        /// 
        public List<long> GetSelectedTaskDetailIDList()
        {
            List<long> oSelectedTaskDetailIDList = new List<long>(); ;
            long TaskDetailID;
            foreach (GridDataItem item in rgGeneralTasks.SelectedItems)
            {
                TaskDetailID = Convert.ToInt64(item.GetDataKeyValue("TaskDetailID"));
                oSelectedTaskDetailIDList.Add(TaskDetailID);
            }
            return oSelectedTaskDetailIDList;
        }

        /// <summary>
        /// Selected TasklID on Current Page
        /// </summary>
        /// 
        public List<long> GetSelectedTaskIDList()
        {
            List<long> oSelectedTaskIDList = new List<long>(); ;
            long TaskID;
            foreach (GridDataItem item in rgGeneralTasks.SelectedItems)
            {
                TaskID = Convert.ToInt64(item.GetDataKeyValue("TaskID"));
                if (!oSelectedTaskIDList.Contains(TaskID))
                    oSelectedTaskIDList.Add(TaskID);
            }
            return oSelectedTaskIDList;
        }
        /// <summary>
        /// Selected TasklID created in Current recperiod
        /// </summary>
        /// 
        public List<long> GetSelectedTaskIDList(int RecPeriodID)
        {
            List<long> oSelectedTaskIDList = new List<long>(); ;
            long TaskID;
            int CreatedRecPeriodID;
            foreach (GridDataItem item in rgGeneralTasks.SelectedItems)
            {
                TaskID = Convert.ToInt64(item.GetDataKeyValue("TaskID"));
                CreatedRecPeriodID = Convert.ToInt32(item.GetDataKeyValue("RecPeriodID"));
                if (CreatedRecPeriodID == SessionHelper.CurrentReconciliationPeriodID)
                    if (!oSelectedTaskIDList.Contains(TaskID))
                        oSelectedTaskIDList.Add(TaskID);
            }
            return oSelectedTaskIDList;
        }

        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        private string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }

        public string GetUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGeneralTasks) + SessionConstants.GENERAL_TASK_GRID_DATA;
        }

        private string GetFilteredDataUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGeneralTasks) + SessionConstants.GENERAL_TASK_GRID_DATA_FILTERED;
        }

        /// <summary>
        /// Clear General Task Grid Data
        /// </summary>
        /// 
        private void ClearGeneralTaskGridData()
        {
            TaskHdrInfoListCollection = null;
            Session[GetUniqueSessionKey() + "List"] = null;
        }
        # endregion

        #region public Methods
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        public void LoadGridData()
        {
            BindrgGeneralTasksGrid(0);
        }

        /// <summary>
        /// Selected GLDataRecItemIDList  List<long>
        /// </summary>
        public List<long> SelectedTaskIDs()
        {
            List<long> oSelectedTaskIDList = null;
            if (_AllowSelectionPersist)
            {
                SaveCheckBoxStates();
                if (Session[getCheckedItemsSessionKey()] != null)
                    oSelectedTaskIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            }
            else
                oSelectedTaskIDList = GetSelectedTaskDetailIDList();
            return oSelectedTaskIDList;
        }

        /// <summary>
        /// Selected TaskHdrInfo Collection
        /// </summary>
        /// 
        public List<TaskHdrInfo> SelectedTaskHdrInfoCollection()
        {
            List<TaskHdrInfo> oTaskHdrInfoCollection = null;
            List<TaskHdrInfo> oAllTaskHdrInfoCollection = (List<TaskHdrInfo>)Session[GetUniqueSessionKey() + "List"];
            List<long> oSelectedTaskHdrInfoList = GetSelectedTaskDetailIDList();
            if (oAllTaskHdrInfoCollection != null && oSelectedTaskHdrInfoList != null)
                oTaskHdrInfoCollection = (from oTaskHdrInfo in oAllTaskHdrInfoCollection
                                          join SelectedTaskDetailID in oSelectedTaskHdrInfoList on oTaskHdrInfo.TaskDetailID equals SelectedTaskDetailID
                                          select oTaskHdrInfo).ToList();
            return oTaskHdrInfoCollection;
        }

        /// <summary>
        /// Set General Task Grid Data
        /// </summary>
        /// 
        public void SetGeneralTaskGridData(List<TaskHdrInfo> oTaskHdrInfoCollection)
        {
            ClearGeneralTaskGridData();
            if (oTaskHdrInfoCollection != null)
            {
                Session[GetUniqueSessionKey() + "List"] = oTaskHdrInfoCollection;
                TaskHdrInfoListCollection = oTaskHdrInfoCollection;
                ClearCheckedItemViewState();
            }
        }

        /// <summary>
        ///Set Grid Group By Expression
        /// </summary>
        /// 
        public void SetGridGroupByExpression()
        {
            GridGroupByExpression GridGroupByExpression;
            GridGroupByExpression = Helper.GetGridGroupByExpressionForTaskListName();
            rgGeneralTasks.MasterTableView.GroupByExpressions.Add(GridGroupByExpression);
            rgGeneralTasks.GroupHeaderItemStyle.CssClass = "groupRadGrid";
            rgGeneralTasks.GroupingSettings.CollapseTooltip = String.Format("{0}...", LanguageUtil.GetValue(1908));
            rgGeneralTasks.GroupingSettings.ExpandTooltip = String.Format("{0}...", LanguageUtil.GetValue(1260));
        }

        public void HideGridColumns(List<string> ColumnNameList)
        {
            GridHelper.HideColumns(rgGeneralTasks.Columns, ColumnNameList);
        }

        public void ShowGridColumns(List<string> ColumnNameList)
        {
            GridHelper.ShowColumns(rgGeneralTasks.Columns, ColumnNameList);
        }


        # endregion

        #region General Task Grid Events
        protected void rgGeneralTasks_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (TaskHdrInfoListCollection != null)
            {
                if (_AllowCustomPaging && !(isExportPDF || isExportExcel))
                {
                    List<TaskHdrInfo> oTaskHdrInfo = new List<TaskHdrInfo>();
                    oTaskHdrInfo = TaskHdrInfoListCollection.Skip(rgGeneralTasks.CurrentPageIndex * rgGeneralTasks.PageSize).Take(rgGeneralTasks.PageSize).ToList();
                    rgGeneralTasks.DataSource = oTaskHdrInfo;
                }
                else
                {
                    rgGeneralTasks.DataSource = TaskHdrInfoListCollection;
                    rgGeneralTasks.VirtualItemCount = TaskHdrInfoListCollection.Count;
                }
            }
        }

        protected void rgGeneralTasks_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = TaskMasterHelper.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID);
                GridColumn oGridColumn1 = rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("TaskCustomField1");
                GridColumn oGridColumn2 = rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("TaskCustomField2");
                if (oTaskCustomFieldInfoList != null && oTaskCustomFieldInfoList.Count > 0)
                {
                    GridHeaderItem item = e.Item as GridHeaderItem;

                    if (oGridColumn1 != null)
                    {
                        string CustomField1 = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == (short)WebEnums.TaskCustomField.CustomField1).CustomFieldValue;
                        if (!string.IsNullOrEmpty(CustomField1))
                        {
                            //item["TaskCustomField1"].Text = CustomField1;
                            //oGridColumn1.HeaderText = CustomField1;
                            LinkButton lbtnCustom1 = (item["TaskCustomField1"].Controls[0]) as LinkButton;
                            if (lbtnCustom1 != null)
                                lbtnCustom1.Text = CustomField1;
                            else
                                oGridColumn1.HeaderText = CustomField1;
                        }
                        else
                        {
                            oGridColumn1.Visible = false;
                        }
                    }

                    if (oGridColumn2 != null)
                    {
                        string CustomField2 = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == (short)WebEnums.TaskCustomField.CustomField2).CustomFieldValue;
                        if (!string.IsNullOrEmpty(CustomField2))
                        {
                            //item["TaskCustomField2"].Text = CustomField2;
                            //oGridColumn2.HeaderText = CustomField2;
                            LinkButton lbtnCustom2 = (item["TaskCustomField2"].Controls[0]) as LinkButton;
                            if (lbtnCustom2 != null)
                                lbtnCustom2.Text = CustomField2;
                            else
                                oGridColumn2.HeaderText = CustomField2;
                        }
                        else
                        {
                            oGridColumn2.Visible = false;
                        }
                    }
                }
                else
                {
                    if (oGridColumn1 != null)
                        oGridColumn1.Visible = false;
                    if (oGridColumn2 != null)
                        oGridColumn2.Visible = false;
                }
                rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
                TaskMasterHelper.ShowFilterIcon(e, this.GridType);
                //bGetFormMode = true;
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                TaskHdrInfo oTaskHdrInfo = (TaskHdrInfo)e.Item.DataItem;
                //if (bGetFormMode)
                //{
                eFormMode = TaskMasterHelper.GetFormModeForTaskViewer(oTaskHdrInfo);
                //bGetFormMode = false;
                //}
                // Check for deleted Task
                if (oTaskHdrInfo.IsDeleted.HasValue && oTaskHdrInfo.IsDeleted.Value)
                {
                    e.Item.CssClass = "InactiveCompany";
                }
                ExHyperLink hlTaskNumber = (ExHyperLink)e.Item.FindControl("hlTaskNumber");
                ExHyperLink hlTaskName = (ExHyperLink)e.Item.FindControl("hlTaskName");
                ExHyperLink hlDescription = (ExHyperLink)e.Item.FindControl("hlDescription");
                //ExHyperLink hlStartDate = (ExHyperLink)e.Item.FindControl("hlStartDate");
                ExHyperLink hlAttachmentCount = (ExHyperLink)e.Item.FindControl("hlAttachmentCount");
                ExHyperLink hlDocs = (ExHyperLink)e.Item.FindControl("hlDocs");
                ExHyperLink hlAssigneeDueDate = (ExHyperLink)e.Item.FindControl("hlAssigneeDueDate");
                ExHyperLink hlReviewerDueDate = (ExHyperLink)e.Item.FindControl("hlReviewerDueDate");
                ExHyperLink hlDueDate = (ExHyperLink)e.Item.FindControl("hlDueDate");
                //  ExHyperLink hlTaskDuration = (ExHyperLink)e.Item.FindControl("hlTaskDuration");
                ExHyperLink hlRecurrenceType = (ExHyperLink)e.Item.FindControl("hlRecurrenceType");
                ExHyperLink hlTaskOwner = (ExHyperLink)e.Item.FindControl("hlTaskOwner");
                ExHyperLink hlTaskReviewer = (ExHyperLink)e.Item.FindControl("hlTaskReviewer");
                ExHyperLink hlApprover = (ExHyperLink)e.Item.FindControl("hlApprover");
                ExLabel lblComment = (ExLabel)e.Item.FindControl("lblComment");
                //ExLabel lblApprovalDuration = (ExLabel)e.Item.FindControl("lblApprovalDuration");
                ExHyperLink hlCompletionDate = (ExHyperLink)e.Item.FindControl("hlCompletionDate");
                ExHyperLink hlAttachment = (ExHyperLink)e.Item.FindControl("hlAttachment");
                ExHyperLink hlCompletionDocs = (ExHyperLink)e.Item.FindControl("hlCompletionDocs");
                ExHyperLink hlPendingStatus = (ExHyperLink)e.Item.FindControl("hlPendingStatus");
                ExHyperLink hlCompletedStatus = (ExHyperLink)e.Item.FindControl("hlCompletedStatus");
                ExHyperLink hlOverDueStatus = (ExHyperLink)e.Item.FindControl("hlOverDueStatus");
                ExHyperLink hlComment = (ExHyperLink)e.Item.FindControl("hlComment");
                ExHyperLink hlTaskStatus = (ExHyperLink)e.Item.FindControl("hlTaskStatus");
                ExHyperLink hlHiddenStatus = (ExHyperLink)e.Item.FindControl("hlHiddenStatus");
                ExHyperLink hlDeletedStatus = (ExHyperLink)e.Item.FindControl("hlDeletedStatus");
                ExImageButton btnDeleteTaskLoad = (ExImageButton)e.Item.FindControl("btnDeleteTaskLoad");
                ExHyperLink hlTaskCustomField1 = (ExHyperLink)e.Item.FindControl("hlTaskCustomField1");
                ExHyperLink hlTaskCustomField2 = (ExHyperLink)e.Item.FindControl("hlTaskCustomField2");

                if (hlTaskCustomField1 != null)
                    hlTaskCustomField1.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.CustomField1);
                if (hlTaskCustomField2 != null)
                    hlTaskCustomField2.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.CustomField2);


                hlDeletedStatus.Visible = false;

                //switch (oTaskHdrInfo.TaskStatusID)
                //{
                //    case ((short)ARTEnums.TaskStatus.Completed):
                //        hlTaskStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(2559));
                //        break;
                //    case ((short)ARTEnums.TaskStatus.InProgress):
                //        hlTaskStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(1090));
                //        break;
                //    case ((short)ARTEnums.TaskStatus.NotStarted):
                //        hlTaskStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(1475));
                //        break;
                //    case ((short)ARTEnums.TaskStatus.PendApproval):
                //        hlTaskStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(1094));
                //        break;
                //    case ((short)ARTEnums.TaskStatus.PendModAssignee):
                //        hlTaskStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(2558));
                //        break;
                //    case ((short)ARTEnums.TaskStatus.Deleted):
                //        hlTaskStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(2646));
                //        break;


                //}
                hlTaskStatus.Text = oTaskHdrInfo.TaskStatus;
                //ARTEnums.TaskStatus eTaskStatus = (ARTEnums.TaskStatus)System.Enum.Parse(typeof(ARTEnums.TaskStatus), oTaskHdrInfo.TaskStatusID.ToString());
                //lblTaskStatus.Text = Helper.GetDisplayStringValue(eTaskStatus.ToString());

                if (hlTaskName != null)
                    hlTaskName.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.TaskName);

                if (hlTaskNumber != null)
                    hlTaskNumber.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.TaskNumber);

                if (hlDescription != null)
                {
                    if (isExportExcel || isExportPDF)
                        hlDescription.Text = oTaskHdrInfo.TaskDescription;
                    else
                        Helper.SetTextAndTooltipValue(hlDescription, oTaskHdrInfo.TaskDescription);
                }

                //if (hlStartDate != null)
                //    hlStartDate.Text = Helper.GetDisplayDate(oTaskHdrInfo.TaskStartDate);

                if (hlAttachmentCount != null)
                {
                    //if (oTaskHdrInfo.CreationAttachment != null)
                    //    lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.CreationAttachment.Count);
                    //else
                    //{
                    //    lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(null);
                    //}


                    if (oTaskHdrInfo.CreationDocCount > 0)
                    {
                        hlDocs.ToolTip = LanguageUtil.GetValue(2618);
                        string windowName = string.Empty;
                        _completionDocsMode = IsCompletionDocsEditable ? QueryStringConstants.EDIT : QueryStringConstants.READ_ONLY;
                        hlDocs.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskID, (int)ARTEnums.RecordType.TaskCreation, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";
                        hlAttachmentCount.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskID, (int)ARTEnums.RecordType.TaskCreation, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";
                    }
                    else
                    {
                        hlDocs.ToolTip = LanguageUtil.GetValue(2619);
                        hlDocs.NavigateUrl = "javascript:";
                        hlAttachmentCount.NavigateUrl = "javascript:";
                    }
                    //lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.CreationDocCount);
                    hlAttachmentCount.Text = Helper.GetDisplayIntegerValueWithBracket(oTaskHdrInfo.CreationDocCount, isExportExcel);

                }

                if (hlAssigneeDueDate != null)
                    hlAssigneeDueDate.Text = Helper.GetDisplayDate(oTaskHdrInfo.AssigneeDueDate);
                if (hlReviewerDueDate != null)
                    hlReviewerDueDate.Text = Helper.GetDisplayDate(oTaskHdrInfo.ReviewerDueDate);
                if (hlDueDate != null)
                    hlDueDate.Text = Helper.GetDisplayDate(oTaskHdrInfo.TaskDueDate);

                //if (hlTaskDuration != null)
                //{
                //    if (oTaskHdrInfo.TaskDueDays.HasValue)
                //        hlTaskDuration.Text = Helper.GetDisplayIntegerValue(System.Math.Abs(oTaskHdrInfo.TaskDueDays.Value));
                //    else
                //        hlTaskDuration.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.TaskDueDays);
                //}

                if (hlRecurrenceType != null && oTaskHdrInfo.RecurrenceType != null)
                {

                    string recurrenceType = LanguageUtil.GetValue(oTaskHdrInfo.RecurrenceType.RecurrenceTypeLabelID.Value);
                    hlRecurrenceType.Text = Helper.GetDisplayStringValue(recurrenceType);
                    ExHyperLink hlTaskRecurrence = (ExHyperLink)e.Item.FindControl("hlTaskRecurrence");

                    if (oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.Custom ||
                        oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.MultipleRecPeriod ||
                        oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.Quarterly ||
                        oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.Annually)
                    {

                        hlTaskRecurrence.Visible = true;
                        string _popupUrl = Page.ResolveUrl(URLConstants.URL_RECFREQUENCY) + "?" + QueryStringConstants.TASK_ID + "=" + oTaskHdrInfo.TaskID.ToString() + "&" + QueryStringConstants.TASK_RECURRENCE_TYPE + "=" + oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID.ToString();
                        hlTaskRecurrence.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + _popupUrl + "', 'TestPopPage', 380 , 400);";
                        hlTaskRecurrence.ToolTipLabelID = 2549;
                        hlRecurrenceType.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + _popupUrl + "', 'TestPopPage', 380 , 400);";
                    }
                    else
                    {
                        hlTaskRecurrence.Visible = false;
                    }

                }


                if (hlTaskOwner != null && oTaskHdrInfo.AssignedTo != null)
                    hlTaskOwner.Text = Helper.GetDisplayTaskUserName(oTaskHdrInfo.AssignedTo);
                if (hlTaskReviewer != null && oTaskHdrInfo.Reviewer != null)
                    hlTaskReviewer.Text = Helper.GetDisplayTaskUserName(oTaskHdrInfo.Reviewer);
                if (hlApprover != null && oTaskHdrInfo.Approver != null)
                    hlApprover.Text = Helper.GetDisplayTaskUserName(oTaskHdrInfo.Approver);

                if (lblComment != null)
                {
                    if (isExportExcel || isExportPDF)
                        lblComment.Text = oTaskHdrInfo.Comment;
                    else
                        Helper.SetTextAndTooltipValue(lblComment, oTaskHdrInfo.Comment);
                    if (hlComment != null)
                    {
                        // It is hiding the assignee comments if approver did not put comments.
                        //if (string.Equals(lblComment.Text, WebConstants.HYPHEN))
                        //{
                        //    hlComment.Visible = false;
                        //hlComment.NavigateUrl = "javascript:";
                        //hlComment.ToolTipLabelID = 2596;
                        //}
                        //else
                        //{
                        string _popupUrl = Page.ResolveUrl(URLConstants.URL_TASK_VIEW_COMMENTS + "?" + QueryStringConstants.TASK_DETAIL_ID + "=" + oTaskHdrInfo.TaskDetailID.ToString());
                        hlComment.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + _popupUrl + "', 'CommentPage', 380 , 600);";
                        hlComment.ToolTipLabelID = 2595;
                        //}
                    }
                }


                //if (lblApprovalDuration != null)
                //    lblApprovalDuration.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.TaskApprovalDuration);

                if (hlCompletionDate != null)
                {
                    hlCompletionDate.Text = Helper.GetDisplayDate(oTaskHdrInfo.TaskCompletionDate);
                }

                if (hlCompletionDocs != null)
                {
                    //if (oTaskHdrInfo.CompletionAttachment != null && oTaskHdrInfo.CompletionAttachment.Count > 0)
                    //    lblCompletionDocs.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.CompletionAttachment.Count);
                    //else
                    //{
                    //    lblCompletionDocs.Text = Helper.GetDisplayIntegerValue(null);

                    //}
                    hlCompletionDocs.Text = Helper.GetDisplayIntegerValueWithBracket(oTaskHdrInfo.CompletionDocCount, isExportExcel);
                }

                if (hlAttachment != null)
                {
                    if (oTaskHdrInfo.CompletionDocCount > 0)
                    {
                        hlAttachment.ToolTip = LanguageUtil.GetValue(2618);
                        string windowName = string.Empty;
                        _completionDocsMode = IsCompletionDocsEditable ? QueryStringConstants.EDIT : QueryStringConstants.READ_ONLY;
                        hlAttachment.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskDetailID, (int)ARTEnums.RecordType.TaskComplition, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";
                        hlCompletionDocs.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskDetailID, (int)ARTEnums.RecordType.TaskComplition, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";
                    }
                    else
                    {
                        hlAttachment.ToolTip = LanguageUtil.GetValue(2619);
                        hlAttachment.NavigateUrl = "javascript:";
                        hlCompletionDocs.NavigateUrl = "javascript:";
                    }
                }

                ExHyperLink hlCreatedBy = (ExHyperLink)e.Item.FindControl("hlCreatedBy");
                ExHyperLink hlDateCreated = (ExHyperLink)e.Item.FindControl("hlDateCreated");
                ExHyperLink hlRevisedBy = (ExHyperLink)e.Item.FindControl("hlRevisedBy");
                ExHyperLink hlDateRevised = (ExHyperLink)e.Item.FindControl("hlDateRevised");

                if (hlCreatedBy != null && oTaskHdrInfo.TaskDetailAddedByUser != null)
                    hlCreatedBy.Text = Helper.GetDisplayUserFullName(oTaskHdrInfo.TaskDetailAddedByUser.FirstName, oTaskHdrInfo.TaskDetailAddedByUser.LastName);


                if (hlDateCreated != null)
                    hlDateCreated.Text = Helper.GetDisplayDate(oTaskHdrInfo.DateAdded);


                if (hlRevisedBy != null)
                {
                    if (oTaskHdrInfo.TaskDetailRevisedByUser != null)
                        hlRevisedBy.Text = Helper.GetDisplayUserFullName(oTaskHdrInfo.TaskDetailRevisedByUser.FirstName, oTaskHdrInfo.TaskDetailRevisedByUser.LastName);
                    else
                        hlRevisedBy.Text = Helper.GetDisplayStringValue(null);
                }

                if (hlDateRevised != null)
                    hlDateRevised.Text = Helper.GetDisplayDate(oTaskHdrInfo.TaskDetailDateRevised);

                if (oTaskHdrInfo.TaskStatusID.HasValue && oTaskHdrInfo.TaskStatusID.Value == (short)ARTEnums.TaskStatus.Completed)
                {
                    hlCompletedStatus.Visible = true;
                    hlOverDueStatus.Visible = false;
                    hlPendingStatus.Visible = false;
                    hlHiddenStatus.Visible = false;
                }
                else if (!oTaskHdrInfo.TaskDueDate.HasValue || DateTime.Now.Date < oTaskHdrInfo.TaskDueDate)
                {
                    hlCompletedStatus.Visible = false;
                    hlOverDueStatus.Visible = false;
                    hlPendingStatus.Visible = true;
                    hlHiddenStatus.Visible = false;
                }
                else if (oTaskHdrInfo.TaskDueDate.HasValue && DateTime.Now.Date >= oTaskHdrInfo.TaskDueDate)
                {
                    hlCompletedStatus.Visible = false;
                    hlOverDueStatus.Visible = true;
                    hlPendingStatus.Visible = false;
                    hlHiddenStatus.Visible = false;
                }
                else
                {
                    hlCompletedStatus.Visible = false;
                    hlOverDueStatus.Visible = false;
                    hlPendingStatus.Visible = false;
                    hlHiddenStatus.Visible = false;
                }
                if (oTaskHdrInfo.IsHidden.HasValue && oTaskHdrInfo.IsHidden.Value == true)
                {
                    hlHiddenStatus.Visible = true;
                    hlCompletedStatus.Visible = false;
                    hlOverDueStatus.Visible = false;
                    hlPendingStatus.Visible = false;
                }
                if (oTaskHdrInfo.TaskStatusID.HasValue && oTaskHdrInfo.TaskStatusID.Value == (short)ARTEnums.TaskStatus.Deleted)
                {
                    hlDeletedStatus.Visible = true;
                    hlHiddenStatus.Visible = false;
                    hlCompletedStatus.Visible = false;
                    hlOverDueStatus.Visible = false;
                    hlPendingStatus.Visible = false;

                }
                string UrlForHyperLink = String.Empty;
                if (ShowByDefaultViewIcon)
                {
                    if (ShowEditColumn)
                    {
                        //ExHyperLink hlEdit = (ExHyperLink)e.Item.FindControl("hlEdit");
                        //ExHyperLink hlReadOnly = (ExHyperLink)e.Item.FindControl("hlReadOnly");
                        string url = Page.ResolveUrl(URLConstants.CREATE_TASK_URL);
                        //hlEdit.Visible = false;
                        //hlReadOnly.Visible = true;
                        url = url + "?" + QueryStringConstants.MODE + "=" + QueryStringConstants.READ_ONLY + "&" + QueryStringConstants.TASK_ID + "=" + oTaskHdrInfo.TaskID.ToString() + "&" + QueryStringConstants.TASK_TYPE_ID + "=" + ((short)ARTEnums.TaskType.GeneralTask).ToString();
                        UrlForHyperLink = "javascript:OpenRadWindowForHyperlinkWithName('" + url + "', 'EditAddTaskWindow', 580 , 1050);";
                        //hlReadOnly.NavigateUrl = UrlForHyperLink;
                    }
                }
                else
                {
                    if (ShowEditColumn)
                    {


                        //ExHyperLink hlEdit = (ExHyperLink)e.Item.FindControl("hlEdit");
                        //ExHyperLink hlReadOnly = (ExHyperLink)e.Item.FindControl("hlReadOnly");
                        string url = Page.ResolveUrl(URLConstants.CREATE_TASK_URL);
                        if (eFormMode == WebEnums.FormMode.Edit)
                        {
                            //hlEdit.Visible = true;
                            //hlReadOnly.Visible = false;
                            url = url + "?" + QueryStringConstants.MODE + "=" + QueryStringConstants.EDIT + "&" + QueryStringConstants.TASK_ID + "=" + oTaskHdrInfo.TaskID.ToString() + "&" + QueryStringConstants.TASK_TYPE_ID + "=" + ((short)ARTEnums.TaskType.GeneralTask).ToString();
                            UrlForHyperLink = "javascript:OpenRadWindowForHyperlinkWithName('" + url + "', 'EditAddTaskWindow', 580 , 1050);";
                            //hlEdit.NavigateUrl = UrlForHyperLink;
                        }
                        else
                        {
                            //hlEdit.Visible = false;
                            //hlReadOnly.Visible = true;
                            url = url + "?" + QueryStringConstants.MODE + "=" + QueryStringConstants.READ_ONLY + "&" + QueryStringConstants.TASK_ID + "=" + oTaskHdrInfo.TaskID.ToString() + "&" + QueryStringConstants.TASK_TYPE_ID + "=" + ((short)ARTEnums.TaskType.GeneralTask).ToString();
                            UrlForHyperLink = "javascript:OpenRadWindowForHyperlinkWithName('" + url + "', 'EditAddTaskWindow', 580 , 1050);";
                            //hlReadOnly.NavigateUrl = UrlForHyperLink;
                        }

                    }
                }
                if (DeleteTaskLoad && eFormMode == WebEnums.FormMode.Edit && oTaskHdrInfo.DataImportID.HasValue)
                {
                    btnDeleteTaskLoad.Visible = true;
                    btnDeleteTaskLoad.CommandArgument = oTaskHdrInfo.DataImportID.Value.ToString();
                    if (oTaskHdrInfo.IsWorkStarted.HasValue && oTaskHdrInfo.IsWorkStarted.Value)
                    {
                        //Load Tasks Cascade delete message
                        btnDeleteTaskLoad.Attributes.Add("onclick", "return ConfirmDeletion('" + LanguageUtil.GetValue(2679) + "');");
                    }
                    else
                    {
                        //Simple delete Confirmation message
                        btnDeleteTaskLoad.Attributes.Add("onclick", "return ConfirmDeletion('" + LanguageUtil.GetValue(2678) + "');");
                    }
                }
                else
                {
                    btnDeleteTaskLoad.Visible = false;
                }

                hlTaskNumber.NavigateUrl = UrlForHyperLink;
                hlTaskName.NavigateUrl = UrlForHyperLink;
                hlDescription.NavigateUrl = UrlForHyperLink;
                //hlStartDate.NavigateUrl = UrlForHyperLink;
                hlAssigneeDueDate.NavigateUrl = UrlForHyperLink;
                hlReviewerDueDate.NavigateUrl = UrlForHyperLink;
                hlDueDate.NavigateUrl = UrlForHyperLink;
                // hlTaskDuration.NavigateUrl = UrlForHyperLink;
                hlTaskOwner.NavigateUrl = UrlForHyperLink;
                hlTaskReviewer.NavigateUrl = UrlForHyperLink;
                hlApprover.NavigateUrl = UrlForHyperLink;
                hlCompletionDate.NavigateUrl = UrlForHyperLink;
                //hlAttachmentCount.NavigateUrl = UrlForHyperLink;
                //hlRecurrenceType.NavigateUrl = UrlForHyperLink;
                //hlCompletionDocs.NavigateUrl = UrlForHyperLink;
                hlTaskStatus.NavigateUrl = UrlForHyperLink;
                hlCreatedBy.NavigateUrl = UrlForHyperLink;
                hlDateCreated.NavigateUrl = UrlForHyperLink;
                hlRevisedBy.NavigateUrl = UrlForHyperLink;
                hlDateRevised.NavigateUrl = UrlForHyperLink;
                if (hlTaskCustomField1 != null)
                    hlTaskCustomField1.NavigateUrl = UrlForHyperLink;
                if (hlTaskCustomField2 != null)
                    hlTaskCustomField2.NavigateUrl = UrlForHyperLink;



            }
            if (e.Item is GridGroupHeaderItem)
            {

                GridGroupHeaderItem gridGroupHeaderItem = e.Item as GridGroupHeaderItem;
                DataRowView groupDataRow = (DataRowView)gridGroupHeaderItem.DataItem;
                int level = gridGroupHeaderItem.GroupIndex.Split('_').Length;
                ExLabel lblTaskListNameVal = (ExLabel)e.Item.FindControl("lblTaskListNameVal");
                ExHyperLink hlEditTaskList = (ExHyperLink)e.Item.FindControl("hlEditTaskList");
                //ExLabel lblTaskSubListValue = (ExLabel)e.Item.FindControl("lblTaskSubListValue");
                //ExHyperLink hlEditTaskSubList = (ExHyperLink)e.Item.FindControl("hlEditTaskSubList");

                if (level == 1)
                {
                    //lblTaskListName.Text = LanguageUtil.GetValue(2584) + ":";
                    int TaskListID = Convert.ToInt32(groupDataRow["TaskListID"]);
                    string TaskListAddedBy = Convert.ToString(groupDataRow["TaskListAddedBy"]);
                    string TaskListName = Convert.ToString(groupDataRow["TaskListName"]);
                    lblTaskListNameVal.Text = TaskListName;
                    hlEditTaskList.NavigateUrl = null;
                    if (TaskListAddedBy == SessionHelper.CurrentUserLoginID)
                    {
                        string _popupUrl = Page.ResolveUrl(URLConstants.EDIT_TASK_LIST_URL) + "?" + QueryStringConstants.TASK_LIST_ID + "=" + TaskListID + "&" + QueryStringConstants.TASK_LIST_NAME + "=" + Server.HtmlEncode(TaskListName) + "&" + QueryStringConstants.TASK_LIST_LEVEL + "=" + level;
                        hlEditTaskList.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + _popupUrl + "', 'EditTaskListWindow', 250 , 400);";
                        hlEditTaskList.Visible = true;
                    }
                    else
                    {
                        hlEditTaskList.Visible = false;
                    }
                }
                if (level == 2)
                {
                    int TaskSubListID = Convert.ToInt32(groupDataRow["TaskSubListID"]);
                    string TaskSubListAddedBy = Convert.ToString(groupDataRow["TaskSubListAddedBy"]);
                    string TaskSubListName = Convert.ToString(groupDataRow["TaskSubListName"]);
                    lblTaskListNameVal.Text = TaskSubListName;
                    hlEditTaskList.NavigateUrl = null;
                    if (TaskSubListAddedBy == SessionHelper.CurrentUserLoginID)
                    {
                        string _popupUrl = Page.ResolveUrl(URLConstants.EDIT_TASK_SUB_LIST_URL) + "?" + QueryStringConstants.TASK_SUB_LIST_ID + "=" + TaskSubListID + "&" + QueryStringConstants.TASK_SUB_LIST_NAME + "=" + Server.HtmlEncode(TaskSubListName) + "&" + QueryStringConstants.TASK_LIST_LEVEL + "=" + level;
                        hlEditTaskList.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + _popupUrl + "', 'EditTaskListWindow', 250 , 400);";
                        hlEditTaskList.Visible = true;
                    }
                    else
                    {
                        hlEditTaskList.Visible = false;
                    }
                }
            }
            //// Raise Event for Page to Handle it         
            if (GridItemDataBound != null)
                GridItemDataBound(sender, e);

        }
        protected void rgGeneralTasks_ItemCommand(object source, GridCommandEventArgs e)
        {
            // Raise Event for Page to Handle it
            if (GridCommand != null)
            {
                GridCommand(source, e);
            }
            if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
            {
                SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgGeneralTasks));
            }
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("ImageColumn").Display = false;
                rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("CommentIcon").Display = false;
                GridColumn oGridColumn = null;
                oGridColumn = rgGeneralTasks.MasterTableView.Columns.FindByUniqueNameSafe("Edit");
                if (oGridColumn != null)
                    oGridColumn.Display = false;
                GridHelper.ExportGridToPDFLandScape(rgGeneralTasks, ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)), true);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("ImageColumn").Display = false;
                rgGeneralTasks.MasterTableView.Columns.FindByUniqueName("CommentIcon").Display = false;
                GridColumn oGridColumn = null;
                oGridColumn = rgGeneralTasks.MasterTableView.Columns.FindByUniqueNameSafe("Edit");
                if (oGridColumn != null)
                    oGridColumn.Display = false;
                GridHelper.ExportGridToExcel(rgGeneralTasks, this.BasePageTitleLabelID);
            }

        }
        protected void rgGeneralTasks_PdfExporting(object source, GridPdfExportingArgs e)
        {
            string validFileName = ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID));
            ExportHelper.GeneratePDFAndRender(validFileName, validFileName + ".pdf", e.RawHTML, false, false, true);
        }

        protected void rgGeneralTasks_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                GridCommandItem grdCmdItem = (GridCommandItem)e.Item;
                //grdCmdItem.Controls[0].Controls.Add(this.RadToolBar);
                //if (_AllowActionMenu)
                //{
                //    HtmlTableCell newCell = new HtmlTableCell();
                //    newCell.Controls.Add(this.RadToolBar);
                //    grdCmdItem.Controls[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls[0].Controls.Add(newCell);
                //}
            }

            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgGeneralTasks.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[GetGridClientIDKey(rgGeneralTasks) + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[GetGridClientIDKey(rgGeneralTasks) + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgGeneralTasks.ClientID + "');");
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
        protected void rgGeneralTasks_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            if (!(isExportPDF || isExportExcel))
                Session[GetGridClientIDKey(rgGeneralTasks) + "NewPageSize"] = e.NewPageSize.ToString();
        }
        protected void rgGeneralTasks_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            if (_AllowSelectionPersist)
                SaveCheckBoxStates();
            rgGeneralTasks.CurrentPageIndex = e.NewPageIndex;
            //BindrgGeneralTasksGrid(e.NewPageIndex);
        }
        #endregion

        #region GLDataRecItems Grid Paging Events

        /// <summary>
        /// get Checked Items SessionKey
        /// </summary>   
        private string getCheckedItemsSessionKey()
        {
            return SessionConstants.CHECKED_ITEMS + rgGeneralTasks.ClientID;
        }
        /// <summary>
        /// Save Check Box States
        /// </summary>  
        private void SaveCheckBoxStates()
        {
            List<long> oSelectedTaskDetailIDList;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedTaskDetailIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            else
                oSelectedTaskDetailIDList = new List<long>();
            long TaskDetailID;
            foreach (GridDataItem item in rgGeneralTasks.Items)
            {
                TaskDetailID = Convert.ToInt64(item.GetDataKeyValue("TaskDetailID"));
                if (item.Selected == true)
                {
                    if (oSelectedTaskDetailIDList != null && !oSelectedTaskDetailIDList.Contains(TaskDetailID))
                        oSelectedTaskDetailIDList.Add(TaskDetailID);
                }
                else
                {
                    if (oSelectedTaskDetailIDList != null && oSelectedTaskDetailIDList.Contains(TaskDetailID))
                        oSelectedTaskDetailIDList.Remove(TaskDetailID);
                }

            }
            if (oSelectedTaskDetailIDList != null && oSelectedTaskDetailIDList.Count > 0)
            {
                Session[getCheckedItemsSessionKey()] = oSelectedTaskDetailIDList;
            }

        }
        /// <summary>
        /// RePopulate CheckBox States
        /// </summary> 
        private void RePopulateCheckBoxStates()
        {

            List<long> oSelectedTaskDetailIDList = null;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedTaskDetailIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            if (oSelectedTaskDetailIDList != null && oSelectedTaskDetailIDList.Count > 0)
            {
                long TaskDetailID;
                foreach (GridDataItem item in rgGeneralTasks.Items)
                {
                    TaskDetailID = Convert.ToInt64(item.GetDataKeyValue("TaskDetailID"));
                    if (oSelectedTaskDetailIDList != null && oSelectedTaskDetailIDList.Contains(TaskDetailID))
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
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
        }
        #endregion

    }
}