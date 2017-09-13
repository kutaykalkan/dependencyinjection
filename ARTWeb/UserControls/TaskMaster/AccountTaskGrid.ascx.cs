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
using SkyStem.Library.Controls.TelerikWebControls.ToolBar;


namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_AccountTaskGrid : UserControlTaskMasterBase
    {
        //public RadToolBar RadToolBar;
        //public ExRadToolBarDropDown ActionDropDown;
        public event GridCommandEventHandler CustomGridCommand;
        string _completionDocsMode = QueryStringConstants.READ_ONLY;
        private bool _isExportPDF;
        private bool _isExportExcel;
        WebEnums.FormMode eFormMode;
        bool bGetFormMode = true;


        #region Public Properties
        private bool _AllowExportToExcel = false;
        public bool isExportPDF
        {
            get { return _isExportPDF; }
            set
            {
                _isExportPDF = value;
            }

        }
        public bool isExportExcel
        {
            get { return _isExportExcel; }
            set
            {
                _isExportExcel = value;
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
        public bool AllowExportToExcel
        {
            get { return _AllowExportToExcel; }
            set
            {
                _AllowExportToExcel = value;
                ucSkyStemARTAccountTaskGrid.Grid.AllowExportToExcel = _AllowExportToExcel;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowExportToPDF = _AllowExportToPDF;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomFilter = _AllowCustomFilter;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomPaging = _AllowCustomPaging;
            }
        }

        /// <summary>
        ///Allow Customization
        /// </summary>
        private bool _AllowCustomization = false;
        public bool AllowCustomization
        {
            get { return _AllowCustomization; }
            set
            {
                _AllowCustomization = value;
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomization = _AllowCustomization;
            }
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomImport = _AllowCustomImport;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomAdd = _AllowCustomAdd;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomEdit = _AllowCustomEdit;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomDelete = _AllowCustomDelete;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomApprove = _AllowCustomApprove;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomReject = _AllowCustomReject;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomDone = _AllowCustomDone;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomClose = _AllowCustomClose;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomReopen = _AllowCustomReopen;
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
                ucSkyStemARTAccountTaskGrid.Grid.AllowCustomReview = _AllowCustomReview;
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
        /// The contained control "RadGrid"
        /// </summary>
        public ExRadGrid Grid
        {
            get
            {
                return ucSkyStemARTAccountTaskGrid.Grid;
            }
        }

        public ARTEnums.Grid GridType
        {
            get
            {
                return ucSkyStemARTAccountTaskGrid.GridType;
            }
            set
            {
                ucSkyStemARTAccountTaskGrid.GridType = value;
            }
        }
        public string CustomGridApplyFilterOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyFilterOnClientClick = value;
            }
        }
        public string GridApplyImportOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyImportOnClientClick = value;
            }
        }
        public string GridApplyAddOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyAddOnClientClick = value;
            }
        }
        public string GridApplyEditOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyEditOnClientClick = value;
            }
        }
        public string GridApplyDeleteOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyDeleteOnClientClick = value;
            }
        }
        public string GridApplyApproveOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyApproveOnClientClick = value;
            }
        }
        public string GridApplyRejectOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyRejectOnClientClick = value;
            }
        }
        public string GridApplyDoneOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyDoneOnClientClick = value;
            }
        }
        public string GridApplyCloseOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyCloseOnClientClick = value;
            }
        }
        public string GridApplyReopenOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyReopenOnClientClick = value;
            }
        }
        public string GridApplyReviewOnClientClick
        {
            set
            {
                this.ucSkyStemARTAccountTaskGrid.Grid.GridApplyReviewOnClientClick = value;
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
        /// Show Completion Docs Column
        /// </summary>
        private bool _isCompletionDocsEditable = false;
        public bool IsCompletionDocsEditable
        {
            get { return _isCompletionDocsEditable; }
            set { _isCompletionDocsEditable = value; }
        }

        #endregion

        # region Private Properties

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

        # region Page Events
        protected override void OnInit(EventArgs e)
        {
            ucSkyStemARTAccountTaskGrid.GridItemDataBound += new GridItemEventHandler(ucSkyStemARTAccountTaskGrid_GridItemDataBound);
            ucSkyStemARTAccountTaskGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTAccountTaskGrid_Grid_NeedDataSourceEventHandler);
            ucSkyStemARTAccountTaskGrid.GridItemCreatedEvent += new UserControls_SkyStemARTGrid.GridItemCreated(ucSkyStemARTAccountTaskGrid_GridItemCreatedEvent);
            ucSkyStemARTAccountTaskGrid.PageIndexChangedEvent += new UserControls_SkyStemARTGrid.Grid_PageIndexChanged(ucSkyStemARTAccountTaskGrid_PageIndexChangedEvent);
            ucSkyStemARTAccountTaskGrid.Grid.MasterTableView.DataKeyNames = new string[] { "TaskID", "TaskDetailID", "RecPeriodID" };
            base.OnInit(e);
        }

        void ucSkyStemARTAccountTaskGrid_PageIndexChangedEvent()
        {
            BindrgAccountTasksGrid(ucSkyStemARTAccountTaskGrid.Grid.CurrentPageIndex);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucSkyStemARTAccountTaskGrid.BasePageTitle = 2546;
            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
            }
            if (IsCompletionDocsEditable)
                _completionDocsMode = QueryStringConstants.EDIT;
        }


        void ucSkyStemARTAccountTaskGrid_GridItemCreatedEvent(object sender, GridItemEventArgs e, bool isExportPDF, bool isExportExcel)
        {
            GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);

        }

        # endregion

        #region Public Methods

        /// <summary>
        /// Set Account Task Grid Data
        /// </summary>
        /// 
        public void SetAccountTaskGridData(List<TaskHdrInfo> oTaskHdrInfoCollection)
        {
            ClearAccountTaskGridData();
            if (oTaskHdrInfoCollection != null)
            {
                Session[GetUniqueSessionKey() + "List"] = oTaskHdrInfoCollection;
                TaskHdrInfoListCollection = oTaskHdrInfoCollection;
            }
        }
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        public void LoadGridData()
        {
            BindrgAccountTasksGrid();
            if (_ShowEditColumn)
            {
                GridColumn oGridColumn = null;
                oGridColumn = ucSkyStemARTAccountTaskGrid.Grid.MasterTableView.Columns.FindByUniqueNameSafe("Edit");
                if (oGridColumn != null)
                {
                    oGridColumn.Visible = true;
                }
            }
            GridColumn oGridColumnCommentIcon = null;
            oGridColumnCommentIcon = ucSkyStemARTAccountTaskGrid.Grid.MasterTableView.Columns.FindByUniqueNameSafe("CommentIcon");
            if (oGridColumnCommentIcon != null)
            {
                oGridColumnCommentIcon.Visible = true;
            }

        }
        /// <summary>
        ///Set Grid Group By Expression
        /// </summary>
        /// 
        public void SetGridGroupByExpression()
        {
            ucSkyStemARTAccountTaskGrid.TaskListGridGroupByExpression = Helper.GetGridGroupByExpressionForTaskListName();
            ucSkyStemARTAccountTaskGrid.TaskSubListGridGroupByExpression = Helper.GetGridGroupByExpressionForTaskSubListName();
        }

        public void HideGridColumns(List<string> ColumnNameList)
        {
            GridHelper.HideColumns(ucSkyStemARTAccountTaskGrid.Grid.Columns, ColumnNameList);
        }

        public void ShowGridColumns(List<string> ColumnNameList)
        {
            GridHelper.ShowColumns(ucSkyStemARTAccountTaskGrid.Grid.Columns, ColumnNameList);
        }

        # endregion

        #region private Methods
        /// <summary>
        /// Bind Account Tasks Grid
        /// </summary>
        private void BindrgAccountTasksGrid()
        {

            if (TaskHdrInfoListCollection != null)
            {
                ucSkyStemARTAccountTaskGrid.Grid.VirtualItemCount = TaskHdrInfoListCollection.Count;
                ucSkyStemARTAccountTaskGrid.Grid.EntityNameLabelID = 2546;
                //ucSkyStemARTAccountTaskGrid.Grid.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";
                ucSkyStemARTAccountTaskGrid.ShowStatusImageColumn = false;
                ucSkyStemARTAccountTaskGrid.ShowSelectCheckBoxColum = selectOption;
                ucSkyStemARTAccountTaskGrid.CompanyID = SessionHelper.CurrentCompanyID;
                ucSkyStemARTAccountTaskGrid.DataSource = TaskHdrInfoListCollection;
                ucSkyStemARTAccountTaskGrid.Grid.CurrentPageIndex = 0;
                ucSkyStemARTAccountTaskGrid.BindGrid();

            }
        }

        /// <summary>
        /// Bind Account Tasks Grid
        /// </summary>
        private void BindrgAccountTasksGrid(int PageIndex)
        {

            if (TaskHdrInfoListCollection != null)
            {
                ucSkyStemARTAccountTaskGrid.Grid.VirtualItemCount = TaskHdrInfoListCollection.Count;
                ucSkyStemARTAccountTaskGrid.Grid.EntityNameLabelID = 2546;
                ucSkyStemARTAccountTaskGrid.ShowStatusImageColumn = false;
                ucSkyStemARTAccountTaskGrid.ShowSelectCheckBoxColum = selectOption;
                ucSkyStemARTAccountTaskGrid.CompanyID = SessionHelper.CurrentCompanyID;
                ucSkyStemARTAccountTaskGrid.Grid.CurrentPageIndex = PageIndex;
                ucSkyStemARTAccountTaskGrid.ShowHideColumns();
                ucSkyStemARTAccountTaskGrid.ApplyGridGroupByExpression();
            }
            if (_ShowEditColumn)
            {
                GridColumn oGridColumn = null;
                oGridColumn = ucSkyStemARTAccountTaskGrid.Grid.MasterTableView.Columns.FindByUniqueNameSafe("Edit");
                if (oGridColumn != null)
                {
                    oGridColumn.Visible = true;
                }
            }
        }
        /// <summary>
        /// Clear Account Task Grid Data
        /// </summary>
        /// 
        private void ClearAccountTaskGridData()
        {
            TaskHdrInfoListCollection = null;
            Session[GetUniqueSessionKey() + "List"] = null;
        }


        /// <summary>
        /// Selected TaskDetailID on Current Page
        /// </summary>
        /// 
        public List<long> GetSelectedTaskDetailIDList()
        {
            List<long> oSelectedTaskDetailIDList = new List<long>();
            long TaskDetailID;
            foreach (GridDataItem item in ucSkyStemARTAccountTaskGrid.Grid.SelectedItems)
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
            foreach (GridDataItem item in ucSkyStemARTAccountTaskGrid.Grid.SelectedItems)
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
            foreach (GridDataItem item in ucSkyStemARTAccountTaskGrid.Grid.SelectedItems)
            {
                TaskID = Convert.ToInt64(item.GetDataKeyValue("TaskID"));
                CreatedRecPeriodID = Convert.ToInt32(item.GetDataKeyValue("RecPeriodID"));
                if (CreatedRecPeriodID == SessionHelper.CurrentReconciliationPeriodID)
                    if (!oSelectedTaskIDList.Contains(TaskID))
                        oSelectedTaskIDList.Add(TaskID);
            }
            return oSelectedTaskIDList;
        }

        public string GetUniqueSessionKey()
        {
            return GetGridClientIDKey(ucSkyStemARTAccountTaskGrid.Grid) + SessionConstants.ACCOUNT_TASK_GRID_DATA;
        }
        /// <summary>
        ///Get Grid Client ID
        /// </summary>
        public string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }
        /// <summary>
        ///Hide Other Images in Image column in ucSkyStemARTAccountTaskGrid 
        /// </summary>
        private void HideOtherImages(GridItemEventArgs e)
        {
            ExHyperLink hlReadOnlyModeStatus = (ExHyperLink)e.Item.FindControl("hlReadOnlyModeStatus");
            ExHyperLink hlEditModeStatus = (ExHyperLink)e.Item.FindControl("hlEditModeStatus");
            ExHyperLink hlStartReconciliationStatus = (ExHyperLink)e.Item.FindControl("hlStartReconciliationStatus");
            ExHyperLink hlUnFlagIcon = (ExHyperLink)e.Item.FindControl("hlUnFlagIcon");
            ExHyperLink hlFlagIcon = (ExHyperLink)e.Item.FindControl("hlFlagIcon");
            hlReadOnlyModeStatus.Visible = false;
            hlEditModeStatus.Visible = false;
            hlStartReconciliationStatus.Visible = false;
            hlUnFlagIcon.Visible = false;
            hlFlagIcon.Visible = false;
        }
        # endregion

        #region Grid Events
        protected void ucSkyStemARTAccountTaskGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Header)
                {
                    List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = TaskMasterHelper.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID);
                    GridColumn oGridColumn1 = ucSkyStemARTAccountTaskGrid.Grid.MasterTableView.Columns.FindByUniqueName("TaskCustomField1");
                    GridColumn oGridColumn2 = ucSkyStemARTAccountTaskGrid.Grid.MasterTableView.Columns.FindByUniqueName("TaskCustomField2");
                    if (oTaskCustomFieldInfoList != null && oTaskCustomFieldInfoList.Count > 0)
                    {
                        GridHeaderItem item = e.Item as GridHeaderItem;

                        if (oGridColumn1 != null)
                        {
                            string CustomField1 = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == (short)WebEnums.TaskCustomField.CustomField1).CustomFieldValue;
                            if (!string.IsNullOrEmpty(CustomField1))
                            {
                                //item["TaskCustomField1"].Text = CustomField1;
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

                    if (e.Item.OwnerTableView.Name == "SkyStemARTGridView")
                    {
                        TaskMasterHelper.ShowFilterIconAccountTask(e, this.GridType);
                    }
                    //bGetFormMode = true;
                }
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    TaskHdrInfo oTaskHdrInfo = (TaskHdrInfo)e.Item.DataItem;
                    //if (bGetFormMode)
                    //{
                        eFormMode = TaskMasterHelper.GetFormModeForTaskViewer(oTaskHdrInfo);
                    //    bGetFormMode = false;
                    //}
                    // Check for deleted Task
                    if (oTaskHdrInfo.IsDeleted.HasValue && oTaskHdrInfo.IsDeleted.Value)
                    {
                        e.Item.CssClass = "InactiveCompany";
                    }
                    ucSkyStemARTAccountTaskGrid.ShowStatusImageColumn = true;
                    ucSkyStemARTAccountTaskGrid.ShowSelectCheckBoxColum = selectOption;

                    //  ExHyperLink hlStartDate = (ExHyperLink)e.Item.FindControl("hlStartDate");
                    ExHyperLink hlTaskNumber = (ExHyperLink)e.Item.FindControl("hlTaskNumber");
                    ExHyperLink hlTaskName = (ExHyperLink)e.Item.FindControl("hlTaskName");
                    ExHyperLink hlDescription = (ExHyperLink)e.Item.FindControl("hlDescription");
                    ExHyperLink hlAttachmentCount = (ExHyperLink)e.Item.FindControl("hlAttachmentCount");
                    ExHyperLink hlDocs = (ExHyperLink)e.Item.FindControl("hlDocs");
                    ExHyperLink hlAssigneeDueDate = (ExHyperLink)e.Item.FindControl("hlAssigneeDueDate");
                    ExHyperLink hlReviewerDueDate = (ExHyperLink)e.Item.FindControl("hlReviewerDueDate");
                    ExHyperLink hlDueDate = (ExHyperLink)e.Item.FindControl("hlDueDate");
                    //ExHyperLink hlTaskDuration = (ExHyperLink)e.Item.FindControl("hlTaskDuration");
                    ExHyperLink hlRecurrenceType = (ExHyperLink)e.Item.FindControl("hlRecurrenceType");
                    ExHyperLink hlTaskOwner = (ExHyperLink)e.Item.FindControl("hlTaskOwner");
                    ExHyperLink hlTaskReviewer = (ExHyperLink)e.Item.FindControl("hlTaskReviewer");
                    ExHyperLink hlApprover = (ExHyperLink)e.Item.FindControl("hlApprover");
                    ExLabel lblComment = (ExLabel)e.Item.FindControl("lblComment");
                    //ExLabel lblApprovalDuration = (ExLabel)e.Item.FindControl("lblApprovalDuration");
                    ExHyperLink hlCompletionDate = (ExHyperLink)e.Item.FindControl("hlCompletionDate");
                    ExHyperLink hlCompletionDocs = (ExHyperLink)e.Item.FindControl("hlCompletionDocs");
                    ExHyperLink hlCreatedBy = (ExHyperLink)e.Item.FindControl("hlCreatedBy");
                    ExHyperLink hlDateCreated = (ExHyperLink)e.Item.FindControl("hlDateCreated");
                    ExHyperLink hlRevisedBy = (ExHyperLink)e.Item.FindControl("hlRevisedBy");
                    ExHyperLink hlDateRevised = (ExHyperLink)e.Item.FindControl("hlDateRevised");

                    ExHyperLink hlPendingStatus = (ExHyperLink)e.Item.FindControl("hlPendingStatus");
                    ExHyperLink hlCompletedStatus = (ExHyperLink)e.Item.FindControl("hlCompletedStatus");
                    ExHyperLink hlOverDueStatus = (ExHyperLink)e.Item.FindControl("hlOverDueStatus");
                    ExHyperLink hlComment = (ExHyperLink)e.Item.FindControl("hlComment");
                    ExHyperLink hlAccountNumber = (ExHyperLink)e.Item.FindControl("hlAccountNumber");
                    ExHyperLink hlAccountName = (ExHyperLink)e.Item.FindControl("hlAccountName");
                    ExHyperLink hlHiddenStatus = (ExHyperLink)e.Item.FindControl("hlHiddenStatus");
                    ExHyperLink hlTaskStatus = (ExHyperLink)e.Item.FindControl("hlTaskStatus");
                    ExHyperLink hlAttachment = (ExHyperLink)e.Item.FindControl("hlAttachment");
                    ExHyperLink hlDeletedStatus = (ExHyperLink)e.Item.FindControl("hlDeletedStatus");
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
                    if (hlAccountNumber != null)
                        hlAccountNumber.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.AccountNumber);

                    if (hlAccountName != null)
                        hlAccountName.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.AccountName);

                    HideOtherImages(e);

                    //lblStartDate.Text = Helper.GetDisplayDate(oTaskHdrInfo.TaskStartDate);
                    //lblTaskNumber.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.TaskNumber);
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
                        if (oTaskHdrInfo.CreationDocCount > 0)
                        {
                            hlDocs.ToolTip = LanguageUtil.GetValue(2618);
                            string windowName = string.Empty;
                            _completionDocsMode = IsCompletionDocsEditable ? QueryStringConstants.EDIT : QueryStringConstants.READ_ONLY;
                            hlDocs.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskID, oTaskHdrInfo.TaskTypeID, oTaskHdrInfo.TaskID, (int)ARTEnums.RecordType.TaskCreation, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";
                            hlAttachmentCount.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskID, oTaskHdrInfo.TaskTypeID, oTaskHdrInfo.TaskID, (int)ARTEnums.RecordType.TaskCreation, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";

                        }
                        else
                        {
                            hlDocs.ToolTip = LanguageUtil.GetValue(2619);
                            hlDocs.NavigateUrl = "javascript:";
                            hlAttachmentCount.NavigateUrl = "javascript:";
                        }
                        hlAttachmentCount.Text = Helper.GetDisplayIntegerValueWithBracket(oTaskHdrInfo.CreationDocCount, isExportExcel);
                        //lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.CreationDocCount);
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
                    // hlTaskDuration.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.TaskDueDays);

                    if (hlRecurrenceType != null)
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

                    if (hlTaskOwner != null)
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
                            //    //hlComment.NavigateUrl = "javascript:";
                            //    //hlComment.ToolTipLabelID = 2596;
                            //}
                            //else
                            //{
                            string _popupUrl = Page.ResolveUrl(URLConstants.URL_TASK_VIEW_COMMENTS + "?"
                                    + QueryStringConstants.TASK_ID + "=" + oTaskHdrInfo.TaskID.GetValueOrDefault()
                                    + "&" + QueryStringConstants.TASK_TYPE_ID + "=" + oTaskHdrInfo.TaskTypeID.GetValueOrDefault()
                                    + "&" + QueryStringConstants.TASK_DETAIL_ID + "=" + oTaskHdrInfo.TaskDetailID.GetValueOrDefault().ToString());
                            hlComment.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + _popupUrl + "', 'TestPopPage', 380 , 600);";
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
                        hlCompletionDocs.Text = Helper.GetDisplayIntegerValueWithBracket(oTaskHdrInfo.CompletionDocCount, isExportExcel);
                    }

                    if (hlAttachment != null)
                    {
                        if (oTaskHdrInfo.CompletionDocCount > 0)
                        {
                            hlAttachment.ToolTip = LanguageUtil.GetValue(2618);
                            string windowName = string.Empty;
                            _completionDocsMode = IsCompletionDocsEditable ? QueryStringConstants.EDIT : QueryStringConstants.READ_ONLY;
                            hlAttachment.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskID, oTaskHdrInfo.TaskTypeID, oTaskHdrInfo.TaskDetailID, (int)ARTEnums.RecordType.TaskComplition, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";
                            hlCompletionDocs.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(Helper.SetDocumentUploadURLForTasks(oTaskHdrInfo.TaskID, oTaskHdrInfo.TaskTypeID, oTaskHdrInfo.TaskDetailID, (int)ARTEnums.RecordType.TaskComplition, _completionDocsMode, out windowName)) + "', '" + windowName + "', 350, 500);";
                        }
                        else
                        {
                            hlAttachment.ToolTip = LanguageUtil.GetValue(2619);
                            hlAttachment.NavigateUrl = "javascript:";
                            hlCompletionDocs.NavigateUrl = "javascript:";
                        }
                    }

                    if (hlCreatedBy != null)
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
                    else if (!oTaskHdrInfo.TaskDueDate.HasValue || DateTime.Now.Date.Date < oTaskHdrInfo.TaskDueDate.Value.Date)
                    {
                        hlCompletedStatus.Visible = false;
                        hlOverDueStatus.Visible = false;
                        hlPendingStatus.Visible = true;
                        hlHiddenStatus.Visible = false;
                    }
                    else if (oTaskHdrInfo.TaskDueDate.HasValue && DateTime.Now.Date.Date >= oTaskHdrInfo.TaskDueDate.Value.Date)
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
                        hlCompletedStatus.Visible = false;
                        hlOverDueStatus.Visible = false;
                        hlPendingStatus.Visible = false;
                        hlHiddenStatus.Visible = false;
                        hlDeletedStatus.Visible = true;
                    }
                    string UrlForHyperLink = String.Empty;
                    if (_ShowEditColumn)
                    {
                        //ExHyperLink hlEdit = (ExHyperLink)e.Item.FindControl("hlEdit");
                        //ExHyperLink hlReadOnly = (ExHyperLink)e.Item.FindControl("hlReadOnly");
                        string url = Page.ResolveUrl(URLConstants.CREATE_TASK_URL);
                        if (eFormMode == WebEnums.FormMode.Edit)
                        {
                            //hlEdit.Visible = true;
                            //hlReadOnly.Visible = false;
                            url = url + "?" + QueryStringConstants.MODE + "=" + QueryStringConstants.EDIT + "&" + QueryStringConstants.TASK_ID + "=" + oTaskHdrInfo.TaskID.ToString() + "&" + QueryStringConstants.TASK_TYPE_ID + "=" + ((short)ARTEnums.TaskType.AccountTask).ToString();
                            UrlForHyperLink = "javascript:OpenRadWindowForHyperlinkWithName('" + url + "', 'EditAddTaskWindow', 580 , 1050);";
                            //hlEdit.NavigateUrl = UrlForHyperLink;
                        }
                        else
                        {
                            //hlEdit.Visible = false;
                            //hlReadOnly.Visible = true;
                            url = url + "?" + QueryStringConstants.MODE + "=" + QueryStringConstants.READ_ONLY + "&" + QueryStringConstants.TASK_ID + "=" + oTaskHdrInfo.TaskID.ToString() + "&" + QueryStringConstants.TASK_TYPE_ID + "=" + ((short)ARTEnums.TaskType.AccountTask).ToString();
                            UrlForHyperLink = "javascript:OpenRadWindowForHyperlinkWithName('" + url + "', 'EditAddTaskWindow', 580 , 1050);";
                            //hlReadOnly.NavigateUrl = UrlForHyperLink;
                        }

                    }
                    hlTaskNumber.NavigateUrl = UrlForHyperLink;
                    hlTaskName.NavigateUrl = UrlForHyperLink;
                    hlDescription.NavigateUrl = UrlForHyperLink;
                    //hlStartDate.NavigateUrl = UrlForHyperLink;
                    hlAssigneeDueDate.NavigateUrl = UrlForHyperLink;
                    hlReviewerDueDate.NavigateUrl = UrlForHyperLink;
                    hlDueDate.NavigateUrl = UrlForHyperLink;
                    //  hlTaskDuration.NavigateUrl = UrlForHyperLink;
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
                    hlAccountNumber.NavigateUrl = UrlForHyperLink;
                    hlAccountName.NavigateUrl = UrlForHyperLink;
                    Helper.SetHyperLinkForOrganizationalHierarchyColumns(UrlForHyperLink, e);

                }
                if (e.Item is GridGroupHeaderItem)
                {
                    GridGroupHeaderItem gridGroupHeaderItem = e.Item as GridGroupHeaderItem;
                    DataRowView groupDataRow = (DataRowView)gridGroupHeaderItem.DataItem;
                    int level = gridGroupHeaderItem.GroupIndex.Split('_').Length;
                    ExHyperLink hlEditTaskList = (ExHyperLink)e.Item.FindControl("hlEditTaskList");
                    ExLabel lblTaskListNameVal = (ExLabel)e.Item.FindControl("lblTaskListNameVal");
                    //ExLabel lblTaskListName = (ExLabel)e.Item.FindControl("lblTaskListName");
                    //lblTaskListName.Text = LanguageUtil.GetValue(2584) + ":";
                    if (level == 1)
                    {
                        int TaskListID = Convert.ToInt32(groupDataRow["TaskListID"]);
                        string TaskListName = (string)groupDataRow["TaskListName"];
                        string TaskListAddedBy = Convert.ToString(groupDataRow["TaskListAddedBy"]);
                        lblTaskListNameVal.Text = TaskListName;
                        if (TaskListAddedBy == SessionHelper.CurrentUserLoginID)
                        {
                            string _popupUrl = Page.ResolveUrl(URLConstants.EDIT_TASK_LIST_URL) + "?" + QueryStringConstants.TASK_LIST_ID + "=" + TaskListID + "&" + QueryStringConstants.TASK_LIST_NAME + "=" + Server.HtmlEncode(TaskListName) + "&" + QueryStringConstants.TASK_LIST_LEVEL + "=" + level;
                            hlEditTaskList.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + _popupUrl + "', 'EditTaskListWindow', 250 , 400);";
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
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        /// <summary>
        /// Handles user controls Need data source event
        /// </summary>
        /// <param name="count">Number of items needed to bind the grid</param>
        /// <returns>object</returns>
        protected object ucSkyStemARTAccountTaskGrid_Grid_NeedDataSourceEventHandler(int count)
        {
            return TaskHdrInfoListCollection;
        }
        protected void ucAccountTaskGrid_GridCommand(object sender, GridCommandEventArgs e)
        {
            if (this.CustomGridCommand != null)
                CustomGridCommand(sender, e);
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {

        }
        # endregion
    }
}