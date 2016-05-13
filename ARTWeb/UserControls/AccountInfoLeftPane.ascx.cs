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

using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_AccountInfoLeftPane : UserControlBase
    {
        #region "Private Properties"
        //private List<GLDataHdrInfo> _GLDataHdrInfoCollection = null;        
        private long _GLDataID;
        private long _AccountID;
        private int _NetAccountID;
        private int _CompanyID;
        private short _RoleID;
        private int _ReconciliationPeriodID;
        private int _UserID;
        private bool _IsDualReviewEnabled;
        private bool _IsMaterialityEnabled;
        public bool _IsZeroBalanceEnabled = false;
        private bool _IsKeyAccountEnabled;
        private bool _IsRiskRatingEnabled;
        public bool _IsNetAccountEnabled = false;
        private bool _IsMultiCurrencyEnabled;
        private bool _IsCertficationEnabled;
        #endregion

        #region "Public Properties"

        public long GLDataID
        {
            get
            {
                return this._GLDataID;
            }
            set
            {
                this._GLDataID = value;
            }
        }

        public long AccountID
        {
            get
            {
                return this._AccountID;
            }
            set
            {
                this._AccountID = value;
            }
        }

        public int NetAccountID
        {
            get
            {
                return this._NetAccountID;
            }
            set
            {
                this._NetAccountID = value;
            }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (this.IsPrintMode)
            {
                tdFSCaptionValue.Attributes.Add("class", "tdAccountInfoFSCaption");
            }
            else
            {
                tdFSCaptionValue.Attributes.Add("class", "tdAccountInfo");
            }

            ShowAccountInfo();
            if (this.IsPrintMode)
                ucPopupRecFrequency.Visible = false;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            MasterPageBase ompage = (MasterPageBase)this.Page.Master.Master;
            if (ompage != null)
                ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this._AccountID > 0)
                ucPopupRecFrequency.AccountID = this._AccountID;
            if (this._NetAccountID > 0)
                ucPopupRecFrequency.NetAccountID = this._NetAccountID;
        }

        protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            // Apoorv - removed everything
        }

        private void ShowAccountInfo()
        {
            try
            {
                trActivityInPeriod.Visible = false;
                SetPrivateVariableValue();
                FillRecFrequencyGrid();
                //if (!Page.IsPostBack)
                    PopulateItemsOnPage();
                ShowHideControlsBasedOnCapabilityAndFeature();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
        }

        private void PopulateItemsOnPage()
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();//TODO: make a different Info object for this control and BLL methods
            GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = oGLDataClient.GetGLDataAndAccountInfoByGLDataID(this._GLDataID, this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID
                , this._IsDualReviewEnabled, this._IsCertficationEnabled, this._IsMaterialityEnabled, (short)WebEnums.CertificationType.Certification
                , (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                 (short)ARTEnums.AccountAttribute.BackupPreparer, (short)ARTEnums.AccountAttribute.BackupReviewer, (short)ARTEnums.AccountAttribute.BackupApprover, Helper.GetAppUserInfo());

            if (oGLDataAndAccountHdrInfo != null)
            {
                oGLDataHdrInfoCollection.Add(oGLDataAndAccountHdrInfo);
                oGLDataAndAccountHdrInfo = (GLDataAndAccountHdrInfo)LanguageHelper.TranslateLabelsGLData(oGLDataHdrInfoCollection).FirstOrDefault();

                if (this.NetAccountID == 0)
                {
                    lblFSCaptionValue.Text = oGLDataAndAccountHdrInfo.FSCaption;
                    lblAccountTypeValue.Text = oGLDataAndAccountHdrInfo.AccountType;
                }
                else
                {
                    // For a Net Account this fields are not valid
                    lblFSCaptionValue.Text = Helper.GetDisplayStringValue(string.Empty);
                    lblAccountTypeValue.Text = Helper.GetDisplayStringValue(string.Empty);
                }

                lblKeyAccountValue.Text = oGLDataAndAccountHdrInfo.KeyAccount;
                lblSystemReconciledValue.Text = oGLDataAndAccountHdrInfo.SystemReconciled;

                lblRiskRatingValue.Text = oGLDataAndAccountHdrInfo.RiskRating;

                lblBaseCurrencyValue.Text = Helper.GetDisplayBaseCurrencyCode(oGLDataAndAccountHdrInfo.BaseCurrencyCode);
                lblReportingCurrencyValue.Text = SessionHelper.ReportingCurrencyCode;

                if ((this.NetAccountID == 0) && !oGLDataAndAccountHdrInfo.IsSubledgerSourceIDNull)
                {
                    ISubledger oSubledger = RemotingHelper.GetSubledgerObject();
                    List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = LanguageHelper.TranslateLabelSubledgerSource(oSubledger.SelectAllByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo()));
                    SubledgerSourceInfo oSubledgerSourceInfo = oSubledgerSourceInfoCollection.Find(sub => sub.SubledgerSourceID == oGLDataAndAccountHdrInfo.SubledgerSourceID);
                    if (oSubledgerSourceInfo != null)
                    {
                        lblSubLedgerSourceValue.Text = oSubledgerSourceInfo.Name;
                    }
                    else
                    {
                        lblSubLedgerSourceValue.Text = Helper.GetDisplayStringValue(string.Empty);
                    }
                }
                else
                {
                    lblSubLedgerSourceValue.Text = Helper.GetDisplayStringValue(string.Empty);
                }

                lblPreparerValue.Text = oGLDataAndAccountHdrInfo.PreparerFullName;
                lblReviewerValue.Text = oGLDataAndAccountHdrInfo.ReviewerFullName;
                lblCertStatusApproverValue.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.ApproverCertificationSignOffDate);

                lblApproverValue.Text = oGLDataAndAccountHdrInfo.ApproverFullName;

                lblBackupApproverValue.Text = oGLDataAndAccountHdrInfo.BackupApproverFullName;
                lblBackupPreparerValue.Text = oGLDataAndAccountHdrInfo.BackupPreparerFullName;
                lblBackupReviewerValue.Text = oGLDataAndAccountHdrInfo.BackupReviewerFullName;
                // Check for status- If it is reconciled or sys reconciled then only display these fields' value else display "-"

                lblReconciledValue.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.ReconciledStatusDate);
                lblPendingReviewValue.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.PendingReviewStatusDate);
                lblPendingApprovalValue.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.PendingApprovalStatusDate);

                if (this._IsMultiCurrencyEnabled)
                {
                    lblAccountMaterialityValue.Text = SessionHelper.ReportingCurrencyCode + " " + Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.AccountMaterialityThreshold);
                    lblUnexplainedVarianceMaterialityValue.Text = SessionHelper.ReportingCurrencyCode + " " + Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.UnexplainedVarianceThreshold);
                }
                else
                {
                    lblAccountMaterialityValue.Text = SessionHelper.ReportingCurrencyCode + " " + Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.AccountMaterialityThreshold);
                    //TODO: why its set UnexplainedVariance(BaseCurrency), rather it should be UnexplainedVarianceThreshold with exchanged rate if needed
                    lblUnexplainedVarianceMaterialityValue.Text = SessionHelper.ReportingCurrencyCode + " " + Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.UnexplainedVarianceThreshold);
                }

                if (this._IsCertficationEnabled)
                {
                    lblCertStatusPreparerValue.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.PreparerCertificationSignOffDate);
                    lblCertStatusReviewerValue.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.ReviewerCertificationSignOffDate);
                }
                else
                {
                    lblCertStatus.Visible = false;
                    lblCertStatusApprover.Visible = false;
                    lblCertStatusApproverValue.Visible = false;
                    lblCertStatusPreparer.Visible = false;
                    lblCertStatusPreparerValue.Visible = false;
                    lblCertStatusReviewer.Visible = false;
                    lblCertStatusReviewerValue.Visible = false;
                }
                lblPreparerDueDateVal.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.PreparerDueDate);
                lblReviewerDueDateVal.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.ReviewerDueDate);
                lblApproverDueDateVal.Text = Helper.GetDisplayDate(oGLDataAndAccountHdrInfo.ApproverDueDate);
            }

            if (this._IsKeyAccountEnabled)
            {
                trKeyAccount.Visible = true;
            }
            else
            {
                trKeyAccount.Visible = false;
            }
            if (this._IsRiskRatingEnabled)
            {
                lblRiskRating.Text = LanguageUtil.GetValue(1013) + ":";
                lblRiskRatingValue.Visible = true;
                ucPopupRecFrequency.Visible = false;
            }
            else
            {
                lblRiskRating.Text = LanguageUtil.GetValue(1427) + ":";
                lblRiskRatingValue.Visible = false;
                ucPopupRecFrequency.Visible = true;
            }

            if (!this._IsDualReviewEnabled)
            {
                lblApprover.Visible = false;
                lblApproverValue.Visible = false;
                lblPendingApproval.Visible = false;
                lblPendingApprovalValue.Visible = false;
                lblCertStatusApprover.Visible = false;
                lblCertStatusApproverValue.Visible = false;
                lblBackupApprover.Visible = false;
                lblBackupApproverValue.Visible = false;
            }
            else
            {
                lblApprover.Visible = true;
                lblApproverValue.Visible = true;
                lblPendingApproval.Visible = true;
                lblPendingApprovalValue.Visible = true;
                lblCertStatusApprover.Visible = true;
                lblCertStatusApproverValue.Visible = true;
                if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                {
                    lblBackupApprover.Visible = true;
                    lblBackupApproverValue.Visible = true;
                }
                else
                {
                    lblBackupApprover.Visible = false;
                    lblBackupApproverValue.Visible = false;
                }
            }

            if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
            {
                lblBackupPreparer.Visible = true;
                lblBackupPreparerValue.Visible = true;
                lblBackupReviewer.Visible = true;
                lblBackupReviewerValue.Visible = true;
            }
            else
            {
                lblBackupPreparer.Visible = false;
                lblBackupPreparerValue.Visible = false;
                lblBackupReviewer.Visible = false;
                lblBackupReviewerValue.Visible = false;
            }

        }

        private void SetPrivateVariableValue()
        {
            this._CompanyID = SessionHelper.CurrentCompanyID.Value;
            this._RoleID = SessionHelper.CurrentRoleID.Value;
            this._ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            this._UserID = oUserHdrInfo.UserID.Value;

            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            SetCapabilityInfo(oCompanyCapabilityInfoCollection);
        }

        private void SetCapabilityInfo(List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection)
        {
            foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oCompanyCapabilityInfoCollection)
            {
                if (oCompanyCapabilityInfo.CapabilityID.HasValue)
                {
                    ARTEnums.Capability oCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oCompanyCapabilityInfo.CapabilityID.Value.ToString());

                    switch (oCapability)
                    {
                        case ARTEnums.Capability.DualLevelReview:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsDualReviewEnabled = true;
                            }
                            break;

                        case ARTEnums.Capability.MaterialitySelection:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsMaterialityEnabled = true;
                            }
                            break;

                        case ARTEnums.Capability.KeyAccount:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsKeyAccountEnabled = true;
                            }
                            break;

                        case ARTEnums.Capability.NetAccount:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsNetAccountEnabled = true;
                            }
                            break;

                        case ARTEnums.Capability.RiskRating:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsRiskRatingEnabled = true;
                            }
                            break;

                        case ARTEnums.Capability.ZeroBalanceAccount:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsZeroBalanceEnabled = true;
                            }
                            break;

                        case ARTEnums.Capability.MultiCurrency:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsMultiCurrencyEnabled = true;
                            }
                            break;

                        case ARTEnums.Capability.CertificationActivation:
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsCertficationEnabled = true;
                            }
                            break;
                    }
                }
            }
        }

        private void FillRecFrequencyGrid()
        {
            //IAccount oAccountClient = RemotingHelper.GetAccountObject();
            //List<AccountReconciliationPeriodInfo> oAccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(this._AccountID);

            //List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods();

            //oReconciliationPeriodInfoCollection = (from recPeriod in oReconciliationPeriodInfoCollection
            //                                       from accRecPeriod in oAccountReconciliationPeriodInfoCollection
            //                                       where recPeriod.ReconciliationPeriodID == accRecPeriod.ReconciliationPeriodID
            //                                       select recPeriod).ToList();
            //radRecFrequency.DataSource = oReconciliationPeriodInfoCollection;
            //radRecFrequency.DataBind();
        }

        protected void imgReconciliationFrequency_OnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles Reconciliation frequency item data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void radRecFrequency_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
                    ReconciliationPeriodInfo oReconciliationPeriodInfo = (ReconciliationPeriodInfo)e.Item.DataItem;
                    lblDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
        }

        private void ShowHideControlsBasedOnCapabilityAndFeature()
        {
            /*TODO : HIDE/SHOW below 
             * Key Account 
             * Base currency
             * Ownership
             *      Approver
             * Rec Status
             *      Pending Approval
             * Cert Status
             *      Approver
             */

            WebEnums.FeatureCapabilityMode eMode;
            WebEnums.FeatureCapabilityMode eModeDualLevelReview;

            eMode = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount);

            ShowHideControl(eMode, trKeyAccount);

            eMode = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.MultiCurrency, ARTEnums.Capability.MultiCurrency);

            ShowHideControl(eMode, trBaseCurrency);

            eModeDualLevelReview = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview);

            ShowHideControl(eModeDualLevelReview, trA);

            ShowHideControl(eModeDualLevelReview, trApproverDueDate);

            ShowHideControl(eMode, trRecStatusApprover);

            //bool isCertificationActivated = Helper.IsFeatureActivated(WebEnums.Feature.Certification, SessionHelper.CurrentReconciliationPeriodID);
            eMode = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation);

            ShowHideControl(eMode, pnlCertStatus);

            if (eMode == WebEnums.FeatureCapabilityMode.Visible)
            {
                // means Certification Feature + Capability is activated
                // check for Dual Level Review and Certification Activation for Approver Cert Status
                eMode = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation);

                if (eMode == WebEnums.FeatureCapabilityMode.Visible
                    && eModeDualLevelReview == WebEnums.FeatureCapabilityMode.Visible)
                {
                    ShowHideControl(WebEnums.FeatureCapabilityMode.Visible, trCertStatusApprover);
                }
                else
                {
                    ShowHideControl(WebEnums.FeatureCapabilityMode.Hidden, trCertStatusApprover);
                }
            }
        }

        private void ShowHideControl(WebEnums.FeatureCapabilityMode eMode, Control ctl)
        {
            if (eMode == WebEnums.FeatureCapabilityMode.Hidden || eMode == WebEnums.FeatureCapabilityMode.Disable)
            {
                ctl.Visible = false;
            }
            else
            {
                ctl.Visible = true;
            }
        }
    }//end of class
}//end of namespace
