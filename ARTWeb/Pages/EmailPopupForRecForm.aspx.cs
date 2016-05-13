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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using ExpertPdf.HtmlToPdf;
using SkyStem.ART.Client.Exception;
using System.IO;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using System.Text;

public partial class Pages_EmailPopupForRecForm : PopupPageBase
{
    #region Variables & Constants
    public int countDisabledCheckboxes = 0;
    bool _IsDualReviewEnabled;
    long _gLDataID;
    #region Constants

    private const string GRID_ONROWSELECTED_EVENT_VALUE = "ValidateUserInput";
    private const string GRID_ONROWDESELECTED_EVENT_VALUE = "DevalidateUserInput";
    private const string GRID_ONROWCREATED_EVENT_VALUE = "OnRowCreated";
    private const string LABEL_ACCOUNT_NUMBER = "lblAccountNumber";
    private const string LABEL_NET_ACCOUNT = "lblNetAccount";
    private const string USERCONTROL_PREPARER = "ucPreparer";
    private const string USERCONTROL_REVIEWER = "ucReviewer";
    private const string USERCONTROL_APPROVER = "ucApprover";
    private const string MENU_KEY = "AccountOwnership";
    private const string DEFAULT_STRING_FOR_SEARCH = "No Records found";
    private const string COLUMN_NAME_ID = "ID";
    private const string COLUMN_NAME_NETACCOUNTID = "NetAccountID";
    private const string BLANK_TEXT_HYPHEN = "-";
    #endregion
    #endregion
    #region Properties
    private List<CompanyCapabilityInfo> _CompanyCapabilityInfoCollection = null;
    List<AttachmentInfo> oAttachmentInfoList;
    #endregion
    #region Delegates & Events
    #endregion
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //string pageTitle = "";
        //string isRedirectedFromDashboard = Request.QueryString["IsDashboardPreferences"];
        short PageID = Convert.ToInt16(Request.QueryString[QueryStringConstants.PAGE_ID]);
        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
            _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);

        //if (string.IsNullOrEmpty(isRedirectedFromDashboard))
        //{
        //    PopupHelper.SetPageTitle(this, 1927);
        //    pnlHeader.Visible = false;
        //    pnlMailNotification.Visible = false;
        //    //requiredFieldTo.Enabled = true;
        //}
        //else
        //{
        //    PopupHelper.SetPageTitle(this, 2508);
        //    PopupHelper.ShowInputRequirementSection(this, 2507);
        //    pnlHeader.Visible = true;
        //    pnlMailNotification.Visible = true;
        //    //requiredFieldTo.Enabled = false;
        //}

        //_PageTitleLabelID = 1927;
        string hdnInnerHTMLIDFromParent = Request.QueryString[QueryStringConstants.GENERIC_ID];
        //string strAccountDescription = Request.QueryString[QueryStringConstants.ACCOUNT_INFO];
        //string strGenericInfo = Request.QueryString[QueryStringConstants.EMAIL_INFO_SPECIFIC];
        if (!Page.IsPostBack)
        {
            SetErrorMessages();
            //pageTitle = LanguageUtil.GetValue(_PageTitleLabelID);
            //txtSubject.Text = Request.QueryString[QueryStringConstants.EMAIL_INFO_SPECIFIC] + WebConstants.ACCOUNT_ENTITY_SEPARATOR + Helper.GetDisplayCurrentRecInfo(); 
            // ExportHelper.GetRecFormNamePeriodText(pageTitle, true);
        }

        //set default value/properties based on Query string parameter QueryStringConstants.EMAIL_INFO_SPECIFIC

        switch (PageID)
        {
            case (short)WebEnums.ARTPages.ReportViewer:
                PopupHelper.SetPageTitle(this, 1927);
                pnlHeader.Visible = false;
                pnlMailNotification.Visible = false;
                chkSendAttachments.Visible = false;
                requiredFieldTo.Enabled = true;
                ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
                if (!Page.IsPostBack)
                    txtSubject.Text = oReportInfo.Report + WebConstants.ACCOUNT_ENTITY_SEPARATOR + Helper.GetDisplayCurrentRecInfo();
                break;
            case (short)WebEnums.ARTPages.DashboardPreferences:

                PopupHelper.SetPageTitle(this, 2508);
                PopupHelper.ShowInputRequirementSection(this, 2507);
                pnlHeader.Visible = true;
                pnlMailNotification.Visible = true;
                chkSendAttachments.Visible = false;
                requiredFieldTo.Enabled = false;
                break;
            default:
                //txtSubject.Text = Helper.GetAccountEntityStringToDisplay(63146);
                PopupHelper.SetPageTitle(this, 1927);
                pnlHeader.Visible = false;
                pnlMailNotification.Visible = false;
                chkSendAttachments.Visible = true;
                requiredFieldTo.Enabled = true;
                if (!Page.IsPostBack)
                    txtSubject.Text = Request.QueryString[QueryStringConstants.EMAIL_INFO_SPECIFIC];
                //btnSend.OnClientClick = "javascript:GetInnerHTMLFromParent('Title','" + hdnInnerHTMLIDFromParent + "' , '" + hdnInnerHTML.ClientID + "');";
                break;
        }

        ucSkyStemARTGrid.GridItemDataBound += new Telerik.Web.UI.GridItemEventHandler(ucSkyStemARTGrid_GridItemDataBound);
        ucSkyStemARTGrid.GridCommand += new Telerik.Web.UI.GridCommandEventHandler(ucSkyStemARTGrid_GridItemCommand);
        ucSkyStemARTGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);

        this._CompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
        this._IsDualReviewEnabled = (from capability in _CompanyCapabilityInfoCollection
                                     where capability.CapabilityID.HasValue
                                     && capability.IsActivated.HasValue
                                     && capability.CapabilityID.Value == (short)ARTEnums.Capability.DualLevelReview
                                     select capability.IsActivated.Value).FirstOrDefault();



        // if (this._IsDualReviewEnabled == false)
        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Hidden)
        {
            GridColumn oGridColumn = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("Approver");
            if (oGridColumn != null)
            {
                oGridColumn.Visible = false;
            }
        }


    }
    #endregion
    #region Grid Events
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            ListItem selectOne = new ListItem(LanguageUtil.GetValue(1343), WebConstants.SELECT_ONE);
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl(LABEL_ACCOUNT_NUMBER);
                lblAccountNumber.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.AccountNumber);

                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                lblAccountName.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.AccountName);

                ExCheckBox chkExcludeOwnershipForZBA = (ExCheckBox)e.Item.FindControl("chkExcludeOwnershipForZBA");
                HtmlInputText txtExcludeOwbershipValue = (HtmlInputText)e.Item.FindControl("txtExcludeOwnershipValue");
                chkExcludeOwnershipForZBA.Enabled = false;

                if (oAccountHdrInfo.AccountGLBalance.HasValue && oAccountHdrInfo.AccountGLBalance.Value == 0M
                    && oAccountHdrInfo.IsZeroBalance.HasValue && oAccountHdrInfo.IsZeroBalance.Value)
                {
                    chkExcludeOwnershipForZBA.Visible = true;
                }
                else
                {
                    chkExcludeOwnershipForZBA.Visible = false;
                }

                if (oAccountHdrInfo.IsExcludeOwnershipForZBA.HasValue && oAccountHdrInfo.IsExcludeOwnershipForZBA.Value)
                {
                    chkExcludeOwnershipForZBA.Checked = true;
                }

                ExLabel lblNetAccount = (ExLabel)e.Item.FindControl(LABEL_NET_ACCOUNT);

                if (!string.IsNullOrEmpty(oAccountHdrInfo.NetAccount))
                {
                    lblNetAccount.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.NetAccount);
                }
                else
                {
                    lblNetAccount.Text = BLANK_TEXT_HYPHEN;
                }


                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

                ExLabel lblPreparerExport = (ExLabel)e.Item.FindControl("lblPreparerExport");
                if (oAccountHdrInfo.PreparerUserID.HasValue && oAccountHdrInfo.PreparerUserID.Value > 0)
                {
                    lblPreparerExport.Text = Helper.GetPreparerNameByID(oAccountHdrInfo.PreparerUserID.Value);
                }
                else
                {
                    lblPreparerExport.Text = "-";
                }

                ExLabel lblReviewerExport = (ExLabel)e.Item.FindControl("lblReviewerExport");
                if (oAccountHdrInfo.ReviewerUserID.HasValue && oAccountHdrInfo.ReviewerUserID.Value > 0)
                {
                    lblReviewerExport.Text = Helper.GetReviewerNameByID(oAccountHdrInfo.ReviewerUserID.Value);
                }
                else
                {
                    lblReviewerExport.Text = "-";
                }

                #region BackupRoles Section

                ExLabel lblBackupPreparerExport = (ExLabel)e.Item.FindControl("lblBackupPreparerExport");
                if (oAccountHdrInfo.BackupPreparerUserID.HasValue && oAccountHdrInfo.BackupPreparerUserID.Value > 0)
                {
                    lblBackupPreparerExport.Text = Helper.GetBackupPreparerNameByID(oAccountHdrInfo.BackupPreparerUserID.Value);
                }
                else
                {
                    lblBackupPreparerExport.Text = "-";
                }

                ExLabel lblBackupReviewerExport = (ExLabel)e.Item.FindControl("lblBackupReviewerExport");
                if (oAccountHdrInfo.BackupReviewerUserID.HasValue && oAccountHdrInfo.BackupReviewerUserID.Value > 0)
                {
                    lblBackupReviewerExport.Text = Helper.GetBackupReviewerNameByID(oAccountHdrInfo.BackupReviewerUserID.Value);
                }
                else
                {
                    lblBackupReviewerExport.Text = "-";
                }

                #endregion

                if (this._IsDualReviewEnabled)
                {
                    ExLabel lblApproverExport = (ExLabel)e.Item.FindControl("lblApproverExport");
                    ExLabel lblBackupApproverExport = (ExLabel)e.Item.FindControl("lblBackupApproverExport");

                    if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                    || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped)
                    {
                        if (oAccountHdrInfo.ApproverUserID.HasValue && oAccountHdrInfo.ApproverUserID.Value > 0)
                        {
                            lblApproverExport.Text = Helper.GetApproverNameByID(oAccountHdrInfo.ApproverUserID.Value);
                        }
                        else
                        {
                            lblApproverExport.Text = "-";
                        }

                        //Backup Approver
                        if (oAccountHdrInfo.BackupApproverUserID.HasValue && oAccountHdrInfo.BackupApproverUserID.Value > 0)
                        {
                            lblBackupApproverExport.Text = Helper.GetBackupApproverNameByID(oAccountHdrInfo.BackupApproverUserID.Value);
                        }
                        else
                        {
                            lblBackupApproverExport.Text = "-";
                        }

                    }
                    else
                    {
                        //ddlApprover.Items.Remove(selectOne);
                        //ddlApprover.Items.Insert(0, selectOne);
                        if (oAccountHdrInfo.ApproverUserID.HasValue && oAccountHdrInfo.ApproverUserID.Value > 0)
                        {
                            lblApproverExport.Text = Helper.GetApproverNameByID(oAccountHdrInfo.ApproverUserID.Value);
                        }
                        else
                        {
                            lblApproverExport.Text = "-";
                        }

                        //Backup Approver
                        if (oAccountHdrInfo.BackupApproverUserID.HasValue && oAccountHdrInfo.BackupApproverUserID.Value > 0)
                        {
                            lblBackupApproverExport.Text = Helper.GetBackupApproverNameByID(oAccountHdrInfo.BackupApproverUserID.Value);
                        }
                        else
                        {
                            lblBackupApproverExport.Text = "-";
                        }

                    }
                }


                //if ((e.Item as GridDataItem)[COLUMN_NAME_ID] != null)
                //{
                //    (e.Item as GridDataItem)[COLUMN_NAME_ID].Text = oAccountHdrInfo.AccountID.ToString();
                //}

                //if ((e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID] != null)
                //{
                //    (e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID].Text = oAccountHdrInfo.NetAccountID.ToString();
                //}
            }
        }
        catch (Exception)
        {

        }
    }
    protected object ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler(int count)
    {
        List<AccountHdrInfo> oAccountHdrInfoCollection = null;
        try
        {
            oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];

            if (oAccountHdrInfoCollection == null || oAccountHdrInfoCollection.Count == 0)
            {
                AccountSearchCriteria oAccountSearchCriteria = GetSearchCriteria();
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                oAccountHdrInfoCollection = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());
                oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
                HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
            }
            if (oAccountHdrInfoCollection.Count == 0)
                requiredFieldTo.Enabled = true;
        }
        catch (ARTException)
        {
            
        }
        catch (Exception)
        {
            
        }

        return oAccountHdrInfoCollection;
    }
    public void ucSkyStemARTGrid_GridItemCommand(object source, GridCommandEventArgs e)
    {


        GridColumn oGridColumnPreparerExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("PreparerExport");
        //GridColumn oGridColumnPreparer = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("Preparer");
        if (oGridColumnPreparerExport != null)
            oGridColumnPreparerExport.Visible = true;

        //if (oGridColumnPreparer != null)
        //    oGridColumnPreparer.Visible = false;

        GridColumn oGridColumnReviewerExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("ReviewerExport");
        //GridColumn oGridColumnReviewer = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("Reviewer");
        if (oGridColumnReviewerExport != null)
            oGridColumnReviewerExport.Visible = true;

        //if (oGridColumnReviewer != null)
        //    oGridColumnReviewer.Visible = false;

        GridColumn oGridColumnApproverExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("ApproverExport");
        //GridColumn oGridColumnApprover = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("Approver");
        if (oGridColumnApproverExport != null)
            oGridColumnApproverExport.Visible = true;

        //if (oGridColumnApprover != null)
        //    oGridColumnApprover.Visible = false;

    }
    #endregion
    #region Other Events
    protected void btnSend_Click(object sender, EventArgs e)
    {
        short PageID = Convert.ToInt16(Request.QueryString[QueryStringConstants.PAGE_ID]);

        switch (PageID)
        {
            case (short)WebEnums.ARTPages.ReportViewer:
                ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
                SendMail(oReportInfo);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetJSForPopupClose", ScriptHelper.GetJSForPopupClose());
                break;
            case (short)WebEnums.ARTPages.DashboardPreferences:
                var oUserHdrInfoCollection = GetDistinctUsers();
                List<AccountHdrInfo> oAccountHdrInfoCollection = null;
                oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
                string AccountDetailTable = String.Empty;
                if (oAccountHdrInfoCollection != null && oAccountHdrInfoCollection.Count > 0)
                    AccountDetailTable = MailHelper.GetAccountDetailTable(oAccountHdrInfoCollection);
                string EmailIDs = string.Empty;
                if (!string.IsNullOrEmpty(this.txtTo.Text))
                    EmailIDs = this.txtTo.Text;

                foreach (UserHdrInfo user in oUserHdrInfoCollection)
                {
                    if (Helper.IsUserActive(user))
                    {
                        if (string.IsNullOrEmpty(EmailIDs))
                            EmailIDs = user.EmailID;
                        else
                            EmailIDs += "," + user.EmailID;
                    }
                }
                SendMailToUser(EmailIDs, AccountDetailTable);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetJSForPopupClose", ScriptHelper.GetJSForPopupClose());
                break;
            default:
                try
                {

                    String RecFormUrl = (String)Session[SessionConstants.REC_FORM_PRINT_PAGE_URL];
                    SendMail(RecFormUrl);

                    //////UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
                    //////string mailBody = this.txtMessage.Content;
                    //////string fromEmailAddress = oUserHdrInfo.EmailID;

                    //////string strEmailInfo = Request.QueryString[QueryStringConstants.EMAIL_INFO_SPECIFIC];
                    //////if (strEmailInfo != string.Empty)
                    //////    strEmailInfo = strEmailInfo.Replace('/', '-');
                    //////string pageTitle = LanguageUtil.GetValue(_PageTitleLabelID);
                    //////string fileName = strEmailInfo + WebConstants.ACCOUNT_ENTITY_SEPARATOR + Helper.GetCurrentRecInfo()
                    //////    + WebConstants.ACCOUNT_ENTITY_SEPARATOR
                    //////    + System.Guid.NewGuid().ToString() + ".pdf";
                    //////fileName = ExportHelper.RemoveInvalidFileNameChars(fileName);

                    //////string htmlToConvert = hdnInnerHTML.Value;
                    //////htmlToConvert = ExportHelper.GetHTMLStringForPDF(htmlToConvert, pageTitle);


                    //////ExportHelper.GeneratePDFAndSendMail(htmlToConvert, mailBody, this.txtTo.Text, fromEmailAddress, this.txtSubject.Text, pageTitle, fileName,string.Empty);
                    // If reached here, means Success
                    // Close Popup and reload Parent page
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetJSForPopupClose", ScriptHelper.GetJSForPopupClose());
                }
                catch
                {
                    ARTException oARTException = new ARTException(5000177);
                    PopupHelper.ShowErrorMessage(this, oARTException);
                }
                break;
        }
    }
    protected void chkSendAttachments_CheckedChanged(object sender, EventArgs e)
    {
        pnlAttachments.Visible = chkSendAttachments.Checked;
        if (chkSendAttachments.Checked)
        {
            rgAttachmentList.DataSource = GetAttachmebtlist();
            rgAttachmentList.DataBind();
        }
    }
    protected void rgAttachmentList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {


        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            AttachmentInfo oAttachmentInfo = (AttachmentInfo)e.Item.DataItem;
            ExLabel lblDocumentName = (ExLabel)e.Item.FindControl("lblDocumentName");
            ExLabel lblFileName = (ExLabel)e.Item.FindControl("lblFileName");
            ExLabel lblAttachmentType = (ExLabel)e.Item.FindControl("lblAttachmentType");
            ExLabel lblFileSize = (ExLabel)e.Item.FindControl("lblFileSize");
            lblDocumentName.Text = Helper.GetDisplayStringValue(oAttachmentInfo.DocumentName);
            lblFileName.Text = Helper.GetDisplayStringValue(ExportHelper.GetOriginalFileName(oAttachmentInfo.FileName));

            switch (oAttachmentInfo.RecordTypeID.Value)
            {
                case 1:
                    lblAttachmentType.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(1052));
                    break;
                case 2:
                    lblAttachmentType.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(1772));
                    break;
                case 3:
                    lblAttachmentType.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(1791));
                    break;
                case 4:
                    lblAttachmentType.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(2788));
                    break;
                case 5:
                    lblAttachmentType.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(2787));
                    break;
            }
            lblFileSize.Text = Helper.GetDisplayIntegerValue(oAttachmentInfo.FileSize);

        }

    }
    #endregion
    #region Validation Control Events
    #endregion
    #region Private Methods
    private AccountSearchCriteria GetSearchCriteria()
    {
        AccountSearchCriteria objSearchCriteria = new AccountSearchCriteria();
        objSearchCriteria.BusinessEntityID = SessionHelper.GetBusinessEntityID();
        objSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID;
        objSearchCriteria.IsDualReviewEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview);
        objSearchCriteria.IsKeyAccountEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.KeyAccount);
        objSearchCriteria.LCID = System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
        objSearchCriteria.PageID = 2;
        objSearchCriteria.ReconciliationPeriodID = (int)SessionHelper.CurrentReconciliationPeriodID;
        objSearchCriteria.UserID = (int)SessionHelper.CurrentUserID;
        objSearchCriteria.UserRoleID = (short)SessionHelper.CurrentRoleID;

        return objSearchCriteria;
    }

    private IEnumerable<UserHdrInfo> GetDistinctUsers()
    {
        List<int> DistinctUserIDsCollection = new List<int>();
        List<AccountHdrInfo> oAccountHdrInfoCollection = null;
        oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
        //DistinctRoleUserIDsCollection = (from obj in oAccountHdrInfoCollection
        //                                 where obj.PreparerUserID.HasValue
        //                                 select obj.PreparerUserID.Value).Distinct().ToList();
        //DistinctUserIDsCollection.AddRange(DistinctRoleUserIDsCollection);
        //DistinctRoleUserIDsCollection = (from obj in oAccountHdrInfoCollection
        //                                 where obj.ReviewerUserID.HasValue
        //                                 select obj.ReviewerUserID.Value).Distinct().ToList();
        //DistinctUserIDsCollection.AddRange(DistinctRoleUserIDsCollection);
        //DistinctRoleUserIDsCollection = (from obj in oAccountHdrInfoCollection
        //                                 where obj.BackupPreparerUserID.HasValue
        //                                 select obj.BackupPreparerUserID.Value).Distinct().ToList();
        //DistinctUserIDsCollection.AddRange(DistinctRoleUserIDsCollection);
        //DistinctRoleUserIDsCollection = (from obj in oAccountHdrInfoCollection
        //                                 where obj.BackupReviewerUserID.HasValue
        //                                 select obj.BackupReviewerUserID.Value).Distinct().ToList();
        //DistinctUserIDsCollection.AddRange(DistinctRoleUserIDsCollection);
        //DistinctRoleUserIDsCollection = (from obj in oAccountHdrInfoCollection
        //                                 where obj.ApproverUserID.HasValue
        //                                 select obj.ApproverUserID.Value).Distinct().ToList();
        //DistinctUserIDsCollection.AddRange(DistinctRoleUserIDsCollection);
        //DistinctRoleUserIDsCollection = (from obj in oAccountHdrInfoCollection
        //                                 where obj.BackupApproverUserID.HasValue
        //                                 select obj.BackupApproverUserID.Value).Distinct().ToList();
        //DistinctUserIDsCollection.AddRange(DistinctRoleUserIDsCollection);
        var result = oAccountHdrInfoCollection
                    .Where(u => u.BackupApproverUserID.HasValue).Select(u => u.BackupApproverUserID.Value)
                    .Union(oAccountHdrInfoCollection.Where(u => u.PreparerUserID.HasValue).Select(u => u.PreparerUserID.Value))
                    .Union(oAccountHdrInfoCollection.Where(u => u.ReviewerUserID.HasValue).Select(u => u.ReviewerUserID.Value))
                    .Union(oAccountHdrInfoCollection.Where(u => u.ApproverUserID.HasValue).Select(u => u.ApproverUserID.Value))
                    .Union(oAccountHdrInfoCollection.Where(u => u.BackupPreparerUserID.HasValue).Select(u => u.BackupPreparerUserID.Value))
                    .Union(oAccountHdrInfoCollection.Where(u => u.BackupReviewerUserID.HasValue).Select(u => u.BackupReviewerUserID.Value));

        DistinctUserIDsCollection.AddRange(result);
        if (SessionHelper.CurrentUserID.HasValue)
            DistinctUserIDsCollection.Remove(SessionHelper.CurrentUserID.Value);
        IUser oUserClient = RemotingHelper.GetUserObject();
        //List<UserHdrInfo> oAllUserHdrInfoCollection = oUserClient.SelectUserByUserID(DistinctUserIDsCollection);
        var oAllUserHdrInfoCollection = from AllUsers in CacheHelper.SelectAllUsersForCurrentCompany()
                                        join DistinctUsers in DistinctUserIDsCollection on AllUsers.UserID equals DistinctUsers
                                        select AllUsers;


        return oAllUserHdrInfoCollection;
    }
    private void SendMail(String PageUrl)
    {
        try
        {
            string mailBody = txtMessage.Text; // LanguageUtil.GetValue(1931); // comment the code , not to show msg body.
            string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            string subject = txtSubject.Text;
            // Send Attachment
            int pgTitle = 0;
            if (Request.QueryString[QueryStringConstants.PAGE_TITLE_ID] != null)
                pgTitle = Convert.ToInt32(Request.QueryString[QueryStringConstants.PAGE_TITLE_ID]);
            string pageTitle = LanguageUtil.GetValue(pgTitle);
            string fileName = pageTitle + "_" + System.Guid.NewGuid().ToString() + ".pdf";
            //string url = PageUrl;
            //TextWriter oTextWriter = new StringWriter();
            //Server.Execute(url, oTextWriter);
            //string htmlToConvert = oTextWriter.ToString();
            List<string> oFilePathList = new List<string>();
            oFilePathList.Add(Request.QueryString[QueryStringConstants.FILE_PATH]);
            oFilePathList.AddRange(GetSelectedAttachments());
            //ExportHelper.GeneratePDFAndSendMail(htmlToConvert, mailBody, this.txtTo.Text, fromEmailAddress, subject, pageTitle, fileName, oFilePathList);
            StringBuilder sb = new StringBuilder();
            sb.Append(mailBody);
            sb.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendByUser, fromEmailAddress));
            MailHelper.SendEmail(fromEmailAddress, this.txtTo.Text, subject, sb.ToString(), oFilePathList);
        }
        catch
        {
            ARTException oARTException = new ARTException(5000177);
            PopupHelper.ShowErrorMessage(this, oARTException);
        }

    }

    private void SendMail(ReportMstInfo oReportInfo)
    {
        try
        {
            string mailBody = txtMessage.Text; // LanguageUtil.GetValue(1931); // comment the code , not to show msg body.
            string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            string subject = txtSubject.Text;

            // Send Attachment
            string pageTitle = LanguageUtil.GetValue(oReportInfo.ReportLabelID.Value);
            string fileName = pageTitle + System.Guid.NewGuid().ToString() + ".pdf";

            string url = Page.ResolveClientUrl(oReportInfo.ReportPrintUrl)
                + "?" + QueryStringConstants.REC_PERIODC_END_DATE + "=" + Helper.GetDisplayCurrentRecInfo()
                + "&" + QueryStringConstants.SHOW_COMMENTS + "=" + (string.IsNullOrEmpty(string.Empty) ? "false" : "true");
            //+ "&" + QueryStringConstants.COMMENTS + "=" + Server.UrlEncode(txtMessage.Text);

            TextWriter oTextWriter = new StringWriter();
            Server.Execute(url, oTextWriter);
            string htmlToConvert = oTextWriter.ToString();
            ExportHelper.GeneratePDFAndSendMail(htmlToConvert, mailBody, this.txtTo.Text, fromEmailAddress, subject, pageTitle, fileName);
        }
        catch
        {
            ARTException oARTException = new ARTException(5000177);
            PopupHelper.ShowErrorMessage(this, oARTException);
        }

    }

    private void SetErrorMessages()
    {
        // Set Error Messages
        requiredFieldTo.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1345);
        txtSubject.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1778);
    }
    private void SendMailToUser(string emailID, string AccountDetailTable)
    {
        try
        {
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(2530)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0} ", this.txtMessage.Text));
            oMailBody.Append("<br><br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2533)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0} ", AccountDetailTable));
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            oMailBody.Append(MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendByUser, fromAddress));
            string mailSubject = this.txtSubject.Text;
            string toAddress = emailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }

    private List<AttachmentInfo> GetAttachmebtlist()
    {
        if (ViewState["AttachmentList"] == null)
        {
            int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
            short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
            IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
            oAttachmentInfoList = oAttachmentClient.GetAllAttachmentForGL(_gLDataID, userID, roleID, Helper.GetAppUserInfo());
            ViewState["AttachmentList"] = oAttachmentInfoList;
        }
        else
            oAttachmentInfoList = (List<AttachmentInfo>)ViewState["AttachmentList"];

        return oAttachmentInfoList;

    }
    private List<string> GetSelectedAttachments()
    {
        List<string> oSelectedFilePathList = new List<string>();
        string FilePath;
        foreach (GridDataItem item in rgAttachmentList.SelectedItems)
        {
            FilePath = Convert.ToString(item.GetDataKeyValue("PhysicalPath"));
            oSelectedFilePathList.Add(FilePath);
        }
        return oSelectedFilePathList;
    }
    #endregion
    #region Other Methods
    #endregion

}
