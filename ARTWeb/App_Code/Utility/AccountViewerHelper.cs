using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for AccountViewerHelper
    /// </summary>
    public class AccountViewerHelper
    {


        public AccountViewerHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<GLDataHdrInfo> GetAccountInfoForNetAccount(int? netAccountID)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            oGLDataHdrInfoCollection = oGLDataClient.GetAccountInfoForNetAccount(netAccountID, SessionHelper.CurrentReconciliationPeriodID,
                SessionHelper.CurrentCompanyID, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

            return oGLDataHdrInfoCollection;
        }

        public static List<GLDataHdrInfo> GetAccountInfoForNetAccount(int? netAccountID, int recPeriodID, int companyID)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            oGLDataHdrInfoCollection = oGLDataClient.GetAccountInfoForNetAccount(netAccountID, recPeriodID,
                companyID, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

            return oGLDataHdrInfoCollection;
        }

        public static void BindAccountFieldLabels(GridItemEventArgs e, AccountHdrInfo oAccountHdrInfo)
        {
            Helper.SetTextForLabel(e.Item, "lblFSCaption", oAccountHdrInfo.FSCaption);
            Helper.SetTextForLabel(e.Item, "lblAccountType", oAccountHdrInfo.AccountType);
            Helper.SetTextForLabel(e.Item, "lblKey2", oAccountHdrInfo.Key2);
            Helper.SetTextForLabel(e.Item, "lblKey3", oAccountHdrInfo.Key3);
            Helper.SetTextForLabel(e.Item, "lblKey4", oAccountHdrInfo.Key4);
            Helper.SetTextForLabel(e.Item, "lblKey5", oAccountHdrInfo.Key5);
            Helper.SetTextForLabel(e.Item, "lblKey6", oAccountHdrInfo.Key6);
            Helper.SetTextForLabel(e.Item, "lblKey7", oAccountHdrInfo.Key7);
            Helper.SetTextForLabel(e.Item, "lblKey8", oAccountHdrInfo.Key8);
            Helper.SetTextForLabel(e.Item, "lblKey9", oAccountHdrInfo.Key9);
            Helper.SetTextForLabel(e.Item, "lblAccountNumber", oAccountHdrInfo.AccountNumber);
            Helper.SetTextForLabel(e.Item, "lblAccountName", oAccountHdrInfo.AccountName);
        }

        public static void BindMasterGridFields(Telerik.Web.UI.GridItemEventArgs e, GLDataHdrInfo oGLDataHdrInfo, WebEnums.RecPeriodStatus eRecPeriodStatus, HtmlInputText Sel, ARTEnums.Grid eGrid, WebEnums.ARTPages eArtPages, ref int countDisabledCheckboxes)
        {
            ExHyperLink hlReadOnlyModeStatus = (ExHyperLink)e.Item.FindControl("hlReadOnlyModeStatus");
            ExHyperLink hlEditModeStatus = (ExHyperLink)e.Item.FindControl("hlEditModeStatus");
            ExHyperLink hlStartReconciliationStatus = (ExHyperLink)e.Item.FindControl("hlStartReconciliationStatus");


            CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
            ExHyperLink hlAccountNumber = (ExHyperLink)e.Item.FindControl("hlAccountNumber");
            ExHyperLink hlAccountName = (ExHyperLink)e.Item.FindControl("hlAccountName");
            ExHyperLink hlReconciliationStatus = (ExHyperLink)e.Item.FindControl("hlReconciliationStatus");
            ExHyperLink hlCertificationStatus = (ExHyperLink)e.Item.FindControl("hlCertificationStatus");
            ExHyperLink hlGLBalance = (ExHyperLink)e.Item.FindControl("hlGLBalance");
            ExHyperLink hlReconciliationBalance = (ExHyperLink)e.Item.FindControl("hlReconciliationBalance");
            ExHyperLink hlUnexplainedVariance = (ExHyperLink)e.Item.FindControl("hlUnexplainedVariance");
            ExHyperLink hlWriteOnOff = (ExHyperLink)e.Item.FindControl("hlWriteOnOff");
            //ExHyperLink hlNetAccount = (ExHyperLink)e.Item.FindControl("hlNetAccount");
            ExHyperLink hlMateriality = (ExHyperLink)e.Item.FindControl("hlMateriality");
            ExHyperLink hlZeroBalance = (ExHyperLink)e.Item.FindControl("hlZeroBalance");
            ExHyperLink hlKeyAccount = (ExHyperLink)e.Item.FindControl("hlKeyAccount");
            ExHyperLink hlRiskRating = (ExHyperLink)e.Item.FindControl("hlRiskRating");
            ExHyperLink hlPreparer = (ExHyperLink)e.Item.FindControl("hlPreparer");
            ExHyperLink hlReviewer = (ExHyperLink)e.Item.FindControl("hlReviewer");
            ExHyperLink hlApprover = (ExHyperLink)e.Item.FindControl("hlApprover");
            ExHyperLink hlFlagIcon = (ExHyperLink)e.Item.FindControl("hlFlagIcon");
            ExHyperLink hlUnFlagIcon = (ExHyperLink)e.Item.FindControl("hlUnFlagIcon");
            ExHyperLink hlSRRNumber = (ExHyperLink)e.Item.FindControl("hlSRRNumber");

            ExHyperLink hlPreparerDueDate = (ExHyperLink)e.Item.FindControl("hlPreparerDueDate");
            ExHyperLink hlReviewerDueDate = (ExHyperLink)e.Item.FindControl("hlReviewerDueDate");
            ExHyperLink hlApproverDueDate = (ExHyperLink)e.Item.FindControl("hlApproverDueDate");


            short currentRoleID = SessionHelper.CurrentRoleID.Value;
            if (currentRoleID == (short)WebEnums.UserRole.PREPARER
                || currentRoleID == (short)WebEnums.UserRole.BACKUP_PREPARER)
            {
                if (!oGLDataHdrInfo.IsEditable.GetValueOrDefault()
                    || (oGLDataHdrInfo.ReconciliationStatusID.HasValue
                    && !(oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.Prepared
                    || oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer)))
                {


                    //checkBox.Enabled = false;
                    //Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }
            else if (currentRoleID == (short)WebEnums.UserRole.REVIEWER
                || currentRoleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
            {

                if (!oGLDataHdrInfo.IsEditable.GetValueOrDefault()
                    || (oGLDataHdrInfo.ReconciliationStatusID.HasValue
                    && !(oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.Reviewed
                    || oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.PendingReview
                    || oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer)))
                {
                    //checkBox.Enabled = false;
                    //Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }
            else if (currentRoleID == (short)WebEnums.UserRole.APPROVER
                || currentRoleID == (short)WebEnums.UserRole.BACKUP_APPROVER)
            {
                if (!oGLDataHdrInfo.IsEditable.GetValueOrDefault()
                    || (oGLDataHdrInfo.ReconciliationStatusID.HasValue
                    && !(oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.Approved
                    || oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.PendingApproval)))
                {
                    //checkBox.Enabled = false;
                    //Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }
            else if (currentRoleID == (short)WebEnums.UserRole.SYSTEM_ADMIN)
            {
                if (oGLDataHdrInfo.IsLocked.GetValueOrDefault() || (oGLDataHdrInfo.ReconciliationStatusID.HasValue && (oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.NotStarted || (oGLDataHdrInfo.IsSystemReconcilied.HasValue && oGLDataHdrInfo.IsSystemReconcilied.Value == true && oGLDataHdrInfo.ReconciliationStatusID.Value == (short)WebEnums.ReconciliationStatus.Prepared))))
                {
                    //checkBox.Enabled = false;
                    //Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }

            // SRA Accts can be submitted from Acct Viewer
            //if (eArtPages == WebEnums.ARTPages.AccountViewer)
            //{
            //    if (oGLDataHdrInfo.IsSystemReconcilied.HasValue && oGLDataHdrInfo.IsSystemReconcilied.Value == true)
            //    {
            //        checkBox.Enabled = false;
            //        Sel.Value += e.Item.ItemIndex.ToString() + ":";
            //    }
            //}

            if (checkBox.Enabled == false)
                countDisabledCheckboxes += 1;

            string url = GetHyperlinkForAccountViewer(oGLDataHdrInfo.ReconciliationTemplateID, oGLDataHdrInfo.AccountID.ToString(), oGLDataHdrInfo.GLDataID.Value.ToString(), oGLDataHdrInfo.NetAccountID.ToString(), oGLDataHdrInfo.IsSystemReconcilied, eArtPages);
            Helper.SetUrlForHyperlink(hlStartReconciliationStatus, url);
            hlStartReconciliationStatus.ToolTip = LanguageUtil.GetValue(1636);

            Helper.SetUrlForHyperlink(hlEditModeStatus, url);
            hlEditModeStatus.ToolTip = LanguageUtil.GetValue(1429);

            Helper.SetUrlForHyperlink(hlReadOnlyModeStatus, url);
            hlReadOnlyModeStatus.ToolTip = LanguageUtil.GetValue(1470);


            Helper.SetUrlForHyperlink(hlAccountNumber, url);
            Helper.SetUrlForHyperlink(hlAccountName, url);
            Helper.SetUrlForHyperlink(hlReconciliationStatus, url);//TODO: get from labelID
            Helper.SetUrlForHyperlink(hlCertificationStatus, url);
            //hlReportingCurrency.NavigateUrl = url;
            Helper.SetUrlForHyperlink(hlGLBalance, url);
            Helper.SetUrlForHyperlink(hlReconciliationBalance, url);
            Helper.SetUrlForHyperlink(hlUnexplainedVariance, url);
            Helper.SetUrlForHyperlink(hlWriteOnOff, url);
            Helper.SetUrlForHyperlink(hlApprover, url);
            Helper.SetUrlForHyperlink(hlKeyAccount, url);
            Helper.SetUrlForHyperlink(hlMateriality, url);
            //hlNetAccount.NavigateUrl = url;
            Helper.SetUrlForHyperlink(hlPreparer, url);
            Helper.SetUrlForHyperlink(hlReviewer, url);
            Helper.SetUrlForHyperlink(hlRiskRating, url);
            Helper.SetUrlForHyperlink(hlZeroBalance, url);
            //TODO: decide the logic- which of the(PrepareDueDate,ReviewerDueDate or cerification due date)  due date is to be shown
            //hlDueDate.NavigateUrl = url;
            Helper.SetHyperLinkForOrganizationalHierarchyColumns(url, e);
            Helper.SetUrlForHyperlink(hlSRRNumber, url);
            Helper.SetUrlForHyperlink(hlPreparerDueDate, url);
            Helper.SetUrlForHyperlink(hlReviewerDueDate, url);
            Helper.SetUrlForHyperlink(hlApproverDueDate, url);

            hlReadOnlyModeStatus.Visible = false;
            hlEditModeStatus.Visible = false;
            hlStartReconciliationStatus.Visible = false;

            WebEnums.ReconciliationStatus eReconciliationStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), oGLDataHdrInfo.ReconciliationStatusID.Value.ToString());
            WebEnums.UserRole eUserRole = (WebEnums.UserRole)Enum.Parse(typeof(WebEnums.UserRole), currentRoleID.ToString());

            if (oGLDataHdrInfo.IsEditable.GetValueOrDefault())
            {
                switch (eUserRole)
                {
                    case WebEnums.UserRole.PREPARER:
                    case WebEnums.UserRole.BACKUP_PREPARER:
                        switch (eReconciliationStatus)
                        {
                            case WebEnums.ReconciliationStatus.NotStarted:
                                if (eRecPeriodStatus == WebEnums.RecPeriodStatus.Open || eRecPeriodStatus == WebEnums.RecPeriodStatus.InProgress)
                                    hlStartReconciliationStatus.Visible = true;
                                else
                                    hlReadOnlyModeStatus.Visible = true;
                                break;

                            case WebEnums.ReconciliationStatus.InProgress:
                            case WebEnums.ReconciliationStatus.PendingModificationPreparer:
                                hlEditModeStatus.Visible = true;
                                break;

                            default:
                                hlReadOnlyModeStatus.Visible = true;
                                break;
                        }
                        break;

                    case WebEnums.UserRole.REVIEWER:
                    case WebEnums.UserRole.BACKUP_REVIEWER:
                        switch (eReconciliationStatus)
                        {
                            case WebEnums.ReconciliationStatus.PendingReview:
                            case WebEnums.ReconciliationStatus.PendingModificationReviewer:
                                hlEditModeStatus.Visible = true;
                                break;

                            default:
                                hlReadOnlyModeStatus.Visible = true;
                                break;
                        }
                        break;

                    case WebEnums.UserRole.APPROVER:
                    case WebEnums.UserRole.BACKUP_APPROVER:
                        switch (eReconciliationStatus)
                        {
                            case WebEnums.ReconciliationStatus.PendingApproval:
                                hlEditModeStatus.Visible = true;
                                break;

                            default:
                                hlReadOnlyModeStatus.Visible = true;
                                break;
                        }
                        break;

                    default:
                        hlReadOnlyModeStatus.Visible = true;
                        break;
                }
            }
            else
            {
                hlReadOnlyModeStatus.Visible = true;
            }

            if (eReconciliationStatus == WebEnums.ReconciliationStatus.Reconciled
                || eReconciliationStatus == WebEnums.ReconciliationStatus.SysReconciled)
            {
                e.Item.CssClass = WebConstants.CSS_CLASS_RECONCILED_ROW;
            }

            if (oGLDataHdrInfo.IsFlagged.HasValue && oGLDataHdrInfo.IsFlagged.Value)
            {
                hlFlagIcon.Style.Add("display", "block");
                hlUnFlagIcon.Style.Add("display", "none");
            }
            else
            {
                hlFlagIcon.Style.Add("display", "none");
                hlUnFlagIcon.Style.Add("display", "block");
            }

            hlUnFlagIcon.NavigateUrl = "javascript:FlagGLDataForUser('" + SessionHelper.CurrentUserID.Value
                                                                        + "', '" + SessionHelper.CurrentUserLoginID
                                                                        + "', '" + oGLDataHdrInfo.GLDataID
                                                                        + "', '" + hlFlagIcon.ClientID
                                                                        + "', '" + hlUnFlagIcon.ClientID
                                                                        + "');";

            hlFlagIcon.NavigateUrl = "javascript:UnFlagGLDataForUser('" + SessionHelper.CurrentUserID.Value
                                                                      + "', '" + SessionHelper.CurrentUserLoginID
                                                                      + "', '" + oGLDataHdrInfo.GLDataID
                                                                      + "', '" + hlFlagIcon.ClientID
                                                                      + "', '" + hlUnFlagIcon.ClientID
                                                                      + "');";
        }

        public static void BindCommonFields(GridItemEventArgs e, GLDataHdrInfo oGLDataHdrInfo)
        {
            // If the Row is not for Net Account then hide the Expand Collapse
            if (oGLDataHdrInfo.NetAccountID == null || oGLDataHdrInfo.NetAccountID <= 0)
            {
                Helper.SetTextForHyperlink(e.Item, "hlAccountNumber", oGLDataHdrInfo.AccountNumber);
                Helper.SetTextForHyperlink(e.Item, "hlAccountName", oGLDataHdrInfo.AccountName);
            }
            else
            {
                Helper.SetTextForHyperlink(e.Item, "hlAccountNumber", LanguageUtil.GetValue(1257));
                Helper.SetTextForHyperlink(e.Item, "hlAccountName", oGLDataHdrInfo.AccountName);
            }
            Helper.SetTextForHyperlink(e.Item, "hlReconciliationStatus", oGLDataHdrInfo.ReconciliationStatus);
            Helper.SetTextForHyperlink(e.Item, "hlCertificationStatus", oGLDataHdrInfo.CertificationStatus);
            Helper.SetTextForHyperlink(e.Item, "hlGLBalance", Helper.GetDisplayDecimalValue(oGLDataHdrInfo.GLBalanceReportingCurrency));
            Helper.SetTextForHyperlink(e.Item, "hlReconciliationBalance", Helper.GetDisplayDecimalValue(oGLDataHdrInfo.ReconciliationBalanceReportingCurrency));
            Helper.SetTextForHyperlink(e.Item, "hlUnexplainedVariance", Helper.GetDisplayDecimalValue(oGLDataHdrInfo.UnexplainedVarianceReportingCurrency));
            Helper.SetTextForHyperlink(e.Item, "hlWriteOnOff", Helper.GetDisplayDecimalValue(oGLDataHdrInfo.WriteOnOffAmountReportingCurrency));

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MaterialitySelection))
            {
                Helper.SetTextForHyperlink(e.Item, "hlMateriality", oGLDataHdrInfo.AccountMateriality);
            }

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount))
            {
                Helper.SetTextForHyperlink(e.Item, "hlZeroBalance", oGLDataHdrInfo.ZeroBalance);
            }

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.KeyAccount))
            {
                Helper.SetTextForHyperlink(e.Item, "hlKeyAccount", oGLDataHdrInfo.KeyAccount);
            }

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
            {
                Helper.SetTextForHyperlink(e.Item, "hlRiskRating", oGLDataHdrInfo.RiskRating);
            }

            Helper.SetTextForHyperlink(e.Item, "hlPreparer", oGLDataHdrInfo.PreparerFullName);

            Helper.SetTextForHyperlink(e.Item, "hlReviewer", oGLDataHdrInfo.ReviewerFullName);

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
            {
                Helper.SetTextForHyperlink(e.Item, "hlApprover", oGLDataHdrInfo.ApproverFullName);
            }

            if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
            {
                Helper.SetTextForHyperlink(e.Item, "hlBackupPreparer", oGLDataHdrInfo.BackupPreparerFullName);

                Helper.SetTextForHyperlink(e.Item, "hlBackupReviewer", oGLDataHdrInfo.BackupReviewerFullName);

                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                {
                    Helper.SetTextForHyperlink(e.Item, "hlBackupApprover", oGLDataHdrInfo.BackupApproverFullName);
                }
            }
            Helper.SetTextForHyperlink(e.Item, "hlSRRNumber", oGLDataHdrInfo.SystemReconciliationRuleNumber, oGLDataHdrInfo.SystemReconciliationRuleLabelID);

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DueDateByAccount) && Helper.IsFeatureActivated(WebEnums.Feature.DueDateByAccount, SessionHelper.CurrentReconciliationPeriodID))
            {
                Helper.SetTextForHyperlink(e.Item, "hlPreparerDueDate", Helper.GetDisplayDate(oGLDataHdrInfo.PreparerDueDate));
                Helper.SetTextForHyperlink(e.Item, "hlReviewerDueDate", Helper.GetDisplayDate(oGLDataHdrInfo.ReviewerDueDate));
                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                    Helper.SetTextForHyperlink(e.Item, "hlApproverDueDate", Helper.GetDisplayDate(oGLDataHdrInfo.ApproverDueDate));
            }
            if (Helper.IsFeatureActivated(WebEnums.Feature.TaskMaster, SessionHelper.CurrentReconciliationPeriodID))
            {
                Helper.SetTextForHyperlink(e.Item, "hlTMStatus", Helper.GetDisplayIntegerValue(oGLDataHdrInfo.CompletedTaskCount) + "/" + Helper.GetDisplayIntegerValue(oGLDataHdrInfo.TotalTaskCount));
            }
        }

        public static void BindCommonFields(GridItemEventArgs e, AccountHdrInfo oAccountHdrInfo)
        {
            // If the Row is not for Net Account then hide the Expand Collapse
            if (oAccountHdrInfo.NetAccountID == null || oAccountHdrInfo.NetAccountID <= 0)
            {
                Helper.SetTextForLabel(e.Item, "lblAccountNumber", oAccountHdrInfo.AccountNumber);
                Helper.SetTextForLabel(e.Item, "lblAccountName", oAccountHdrInfo.AccountName);
            }
            else
            {
                Helper.SetTextForLabel(e.Item, "lblAccountNumber", LanguageUtil.GetValue(1257));
                Helper.SetTextForLabel(e.Item, "lblAccountName", oAccountHdrInfo.AccountName);
            }

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount))
            {
                Helper.SetTextForLabel(e.Item, "lblZeroBalance", oAccountHdrInfo.ZeroBalance);
            }

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.KeyAccount))
            {
                Helper.SetTextForLabel(e.Item, "lblKeyAccount", oAccountHdrInfo.KeyAccount);
            }

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
            {
                Helper.SetTextForLabel(e.Item, "lblRiskRating", oAccountHdrInfo.RiskRating);
            }

            Helper.SetTextForLabel(e.Item, "lblReconciliationTemplate", oAccountHdrInfo.ReconciliationTemplate);

            Helper.SetTextForLabel(e.Item, "lblPreparer", oAccountHdrInfo.PreparerFullName);

            Helper.SetTextForLabel(e.Item, "lblReviewer", oAccountHdrInfo.ReviewerFullName);

            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
            {
                Helper.SetTextForLabel(e.Item, "lblApprover", oAccountHdrInfo.ApproverFullName);
            }

            if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
            {
                Helper.SetTextForLabel(e.Item, "lblBackupPreparer", oAccountHdrInfo.BackupPreparerFullName);

                Helper.SetTextForLabel(e.Item, "lblBackupReviewer", oAccountHdrInfo.BackupReviewerFullName);

                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                {
                    Helper.SetTextForLabel(e.Item, "lblBackupApprover", oAccountHdrInfo.BackupApproverFullName);
                }
            }
            if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
            {
                Helper.SetTextForLabel(e.Item, "lblPreparerDueDays", Helper.GetDisplayIntegerValue(oAccountHdrInfo.PreparerDueDays));
                Helper.SetTextForLabel(e.Item, "lblPreparerDueDate", Helper.GetDisplayDate(oAccountHdrInfo.PreparerDueDate));

                Helper.SetTextForLabel(e.Item, "lblReviewerDueDays", Helper.GetDisplayIntegerValue(oAccountHdrInfo.ReviewerDueDays));
                Helper.SetTextForLabel(e.Item, "lblReviewerDueDate", Helper.GetDisplayDate(oAccountHdrInfo.ReviewerDueDate));

                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                {
                    Helper.SetTextForLabel(e.Item, "lblApproverDueDays", Helper.GetDisplayIntegerValue(oAccountHdrInfo.ApproverDueDays));
                    Helper.SetTextForLabel(e.Item, "lblApproverDueDate", Helper.GetDisplayDate(oAccountHdrInfo.ApproverDueDate));
                }
            }
        }

        public static string GetHyperlinkForAccountViewer(short? reconciliationTemplateID, string accountID, string gLDataID, string netAccountID, bool? isSRA, WebEnums.ARTPages eArtPages)
        {
            string url = "";
            if (Helper.IsFeatureActivated(WebEnums.Feature.Reconciliation, SessionHelper.CurrentReconciliationPeriodID))
            {
                bool isInvalidTemplateID = false;

                if (reconciliationTemplateID.HasValue && reconciliationTemplateID.Value > 0)
                {
                    ARTEnums.ReconciliationItemTemplate oReconciliationItemTemplate = (ARTEnums.ReconciliationItemTemplate)Enum.Parse(typeof(ARTEnums.ReconciliationItemTemplate), reconciliationTemplateID.Value.ToString());

                    switch (oReconciliationItemTemplate)
                    {
                        case ARTEnums.ReconciliationItemTemplate.BankForm:
                            url = URLConstants.URL_TEMPLATEID_1;
                            break;
                        case ARTEnums.ReconciliationItemTemplate.DerivedCalculationForm:
                            url = URLConstants.URL_TEMPLATEID_2;
                            break;
                        case ARTEnums.ReconciliationItemTemplate.AccrualForm:
                            url = URLConstants.URL_TEMPLATEID_3;
                            break;
                        case ARTEnums.ReconciliationItemTemplate.AmortizableBalanceForm:
                            url = URLConstants.URL_TEMPLATEID_4;
                            break;
                        case ARTEnums.ReconciliationItemTemplate.ItemizedListForm:
                            url = URLConstants.URL_TEMPLATEID_5;
                            break;
                        case ARTEnums.ReconciliationItemTemplate.Subledgerform:
                            url = URLConstants.URL_TEMPLATEID_6;
                            break;
                        default:
                            url = Helper.GetErrorPageUrl();
                            isInvalidTemplateID = true;
                            break;
                    }
                }
                else
                {
                    url = Helper.GetErrorPageUrl();
                    isInvalidTemplateID = true;
                }

                if (isInvalidTemplateID == false)
                {
                    url = url + "?" + QueryStringConstants.ACCOUNT_ID + "=" + accountID + "&" + QueryStringConstants.GLDATA_ID + "=" + gLDataID + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + netAccountID;

                    if (isSRA.HasValue && isSRA.Value)
                    {
                        url += "&" + QueryStringConstants.IS_SRA + "=1";
                    }
                    else
                    {
                        url += "&" + QueryStringConstants.IS_SRA + "=0";
                    }

                    url += "&" + QueryStringConstants.REFERRER_PAGE_ID + "=" + ((short)eArtPages).ToString();
                }
                else
                {
                    url = url + "?" + QueryStringConstants.ERROR_MESSAGE_LABEL_ID + "=5000369";
                    url = url + "&" + QueryStringConstants.ERROR_MESSAGE_SYSTEM + "=1";
                }
            }
            return url;
        }


        public static WebEnums.FormMode GetFormMode(WebEnums.RecPeriodStatus eRecPeriodStatus)
        {
            WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;

            /*             
             * 1. Check the Status based on Rec Period Status
             * 1a. If ReadOnly Mode, return
             * 3. Check based on Role
             * 4. Rec Status
             */


            switch (eRecPeriodStatus)
            {
                case WebEnums.RecPeriodStatus.Open:
                case WebEnums.RecPeriodStatus.InProgress:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }

            // If Rec Period is not workable, 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            // Check based on Role
            WebEnums.UserRole eUserRole = (WebEnums.UserRole)System.Enum.Parse(typeof(WebEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());

            switch (eUserRole)
            {
                case WebEnums.UserRole.PREPARER:
                case WebEnums.UserRole.REVIEWER:
                case WebEnums.UserRole.APPROVER:
                case WebEnums.UserRole.BACKUP_PREPARER:
                case WebEnums.UserRole.BACKUP_REVIEWER:
                case WebEnums.UserRole.BACKUP_APPROVER:
                case WebEnums.UserRole.SYSTEM_ADMIN:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }


            // If Any other Role apart from P / R / A
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            // Check for Due Dates
            //////////switch (eUserRole)
            //////////{
            //////////    case WebEnums.UserRole.PREPARER:
            //////////        // Get the Preparer Due Date for the Current Rec Period
            //////////        if (oReconciliationPeriodInfo.PreparerDueDate.HasValue && DateTime.Now.Date > oReconciliationPeriodInfo.PreparerDueDate.Value.Date)
            //////////        {
            //////////            eFormMode = WebEnums.FormMode.ReadOnly;
            //////////        }

            //////////        break;

            //////////    case WebEnums.UserRole.REVIEWER:
            //////////        // Get the Preparer Due Date for the Current Rec Period
            //////////        if (oReconciliationPeriodInfo.ReviewerDueDate.HasValue && DateTime.Now.Date > oReconciliationPeriodInfo.ReviewerDueDate.Value.Date)
            //////////        {
            //////////            eFormMode = WebEnums.FormMode.ReadOnly;
            //////////        }
            //////////        break;

            //////////    case WebEnums.UserRole.APPROVER:
            //////////        // Get the Preparer Due Date for the Current Rec Period
            //////////        if (oReconciliationPeriodInfo.ApproverDueDate.HasValue && DateTime.Now.Date > oReconciliationPeriodInfo.ApproverDueDate.Value.Date)
            //////////        {
            //////////            eFormMode = WebEnums.FormMode.ReadOnly;
            //////////        }
            //////////        break;
            //////////}


            // Check for Certification Start Dates

            ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
            WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, SessionHelper.CurrentReconciliationPeriodID);
            if (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible)
            {
                if (oReconciliationPeriodInfo.CertificationStartDate != null)
                {
                    if (DateTime.Now.Date >= oReconciliationPeriodInfo.CertificationStartDate.Value.Date)
                    {
                        eFormMode = WebEnums.FormMode.ReadOnly;
                    }
                }
            }


            return eFormMode;
        }


        public static void ShowFilterIcon(GridItemEventArgs e, ARTEnums.Grid eGrid)
        {
            string sessionKey = SessionHelper.GetSessionKeyForGridFilter(eGrid);
            List<FilterCriteria> oFilterCriteriaCollection = (List<FilterCriteria>)HttpContext.Current.Session[sessionKey];
            Control oControl = new Control();

            if (oFilterCriteriaCollection != null && oFilterCriteriaCollection.Count > 0)
            {
                //Show filter icon on organizational hierarchy columns
                ShowFilterIconForOrgHierarchyColumns(e, oFilterCriteriaCollection);

                foreach (FilterCriteria oFilterCriteria in oFilterCriteriaCollection)
                {
                    switch (oFilterCriteria.ParameterID)
                    {
                        case (short)WebEnums.StaticAccountField.AccountName:
                            oControl = (e.Item as GridHeaderItem)["AccountName"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.AccountNumber:
                            oControl = (e.Item as GridHeaderItem)["AccountNumber"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.AccountType:
                            oControl = (e.Item as GridHeaderItem)["AccountType"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.Approver:
                            oControl = (e.Item as GridHeaderItem)["Approver"].Controls[0];
                            break;
                        case (short)WebEnums.StaticAccountField.BackupApprover:
                            oControl = (e.Item as GridHeaderItem)["BackupApprover"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.CertificationStatus:
                            oControl = (e.Item as GridHeaderItem)["CertificationStatus"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.FSCaption:
                            oControl = (e.Item as GridHeaderItem)["FSCaption"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.GLBalance:
                            oControl = (e.Item as GridHeaderItem)["GLBalance"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.KeyAccount:
                            oControl = (e.Item as GridHeaderItem)["KeyAccount"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.Materiality:
                            oControl = (e.Item as GridHeaderItem)["Materiality"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.Preparer:
                            oControl = (e.Item as GridHeaderItem)["Preparer"].Controls[0];
                            break;
                        case (short)WebEnums.StaticAccountField.BackupPreparer:
                            oControl = (e.Item as GridHeaderItem)["BackupPreparer"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.RecBalance:
                            oControl = (e.Item as GridHeaderItem)["RecBalance"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.ReconciliationStatus:
                            oControl = (e.Item as GridHeaderItem)["ReconciliationStatus"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.Reviewer:
                            oControl = (e.Item as GridHeaderItem)["Reviewer"].Controls[0];
                            break;
                        case (short)WebEnums.StaticAccountField.BackupReviewer:
                            oControl = (e.Item as GridHeaderItem)["BackupReviewer"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.RiskRating:
                            oControl = (e.Item as GridHeaderItem)["RiskRating"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.UnexplainedVar:
                            oControl = (e.Item as GridHeaderItem)["UnexplainedVar"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.WriteOnOff:
                            oControl = (e.Item as GridHeaderItem)["WriteOnOff"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.ZeroBalance:
                            oControl = (e.Item as GridHeaderItem)["ZeroBalance"].Controls[0];
                            break;
                        case (short)WebEnums.StaticAccountField.PreparerDueDate:
                            oControl = (e.Item as GridHeaderItem)["PreparerDueDate"].Controls[0];
                            break;
                        case (short)WebEnums.StaticAccountField.ReviewerDueDate:
                            oControl = (e.Item as GridHeaderItem)["ReviewerDueDate"].Controls[0];
                            break;
                        case (short)WebEnums.StaticAccountField.ApproverDueDate:
                            oControl = (e.Item as GridHeaderItem)["ApproverDueDate"].Controls[0];
                            break;
                        case (short)WebEnums.StaticAccountField.TMStatus:
                            oControl = (e.Item as GridHeaderItem)["TMStatus"].Controls[0];
                            break;
                    }

                    if (oControl is LinkButton)
                    {
                        LinkButton oLinkButton = (LinkButton)oControl;
                        oLinkButton.Text = oLinkButton.Text + "<img src='../App_Themes/SkyStemBlueBrown/Images/FilterIcon.gif' border='0' />";

                    }
                    else
                    {
                        if (oControl is LiteralControl)
                        {
                            LiteralControl oLiteralControl = (LiteralControl)oControl;
                            oLiteralControl.Text = oLiteralControl.Text + "<img src='../App_Themes/SkyStemBlueBrown/Images/FilterIcon.gif' border='0' />";

                        }
                    }
                }
            }
        }

        private static void ShowFilterIconForOrgHierarchyColumns(GridItemEventArgs e, List<FilterCriteria> oFilterCriteriaCollection)
        {
            Control oControl = new Control();

            if (oFilterCriteriaCollection != null && oFilterCriteriaCollection.Count > 0)
            {
                foreach (FilterCriteria oFilterCriteria in oFilterCriteriaCollection)
                {
                    switch (oFilterCriteria.ParameterID)
                    {
                        case (short)WebEnums.GeographyClass.Key2:
                            oControl = (e.Item as GridHeaderItem)["Key2"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key3:
                            oControl = (e.Item as GridHeaderItem)["Key3"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key4:
                            oControl = (e.Item as GridHeaderItem)["Key4"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key5:
                            oControl = (e.Item as GridHeaderItem)["Key5"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key6:
                            oControl = (e.Item as GridHeaderItem)["Key6"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key7:
                            oControl = (e.Item as GridHeaderItem)["Key7"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key8:
                            oControl = (e.Item as GridHeaderItem)["Key8"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key9:
                            oControl = (e.Item as GridHeaderItem)["Key9"].Controls[0];
                            break;
                    }

                    if (oFilterCriteria.ParameterID >= (short)WebEnums.GeographyClass.Key2
                        && oFilterCriteria.ParameterID <= (short)WebEnums.GeographyClass.Key9)
                    {
                        if (oControl is LinkButton)
                        {
                            LinkButton oLinkButton = (LinkButton)oControl;
                            oLinkButton.Text = oLinkButton.Text + "<img src='../App_Themes/SkyStemBlueBrown/Images/FilterIcon.gif' border='0' />";

                        }
                        else
                        {
                            if (oControl is LiteralControl)
                            {
                                LiteralControl oLiteralControl = (LiteralControl)oControl;
                                oLiteralControl.Text = oLiteralControl.Text + "<img src='../App_Themes/SkyStemBlueBrown/Images/FilterIcon.gif' border='0' />";

                            }
                        }
                    }
                }
            }
        }

        public static bool ShowHideReopenAccountbtn(short RoleID, WebEnums.RecPeriodStatus eRecPeriodStatus, bool IsCertificationStarted)
        {
            bool flag = false;
            if (RoleID == (short)WebEnums.UserRole.SYSTEM_ADMIN)
            {
                if (eRecPeriodStatus == WebEnums.RecPeriodStatus.InProgress || eRecPeriodStatus == WebEnums.RecPeriodStatus.Open)
                {
                    if (!IsCertificationStarted)
                    {

                        flag = true;
                    }
                    else
                        flag = false;
                }
                else
                    flag = false;
            }
            else
                flag = false;
            return flag;
        }

    }
}
