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
using Telerik.Web.UI;
using System.Collections;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.Utility
{

    /// <summary>
    /// Summary description for GridHelper
    /// </summary>
    public class GridHelper
    {
        public GridHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void HandleSortCommand(GridSortCommandEventArgs e)
        {
            // Add Default Sort as Import Data, Desc
            GridSortExpression oGridSortExpression = new GridSortExpression();

            if (e.NewSortOrder == GridSortOrder.None)
            {
                oGridSortExpression.SortOrder = GridSortOrder.Ascending;
            }
            else
            {
                oGridSortExpression.SortOrder = e.NewSortOrder;
            }
            oGridSortExpression.FieldName = e.SortExpression;
            e.Item.OwnerTableView.SortExpressions.AddSortExpression(oGridSortExpression);
            e.Item.OwnerTableView.CurrentPageIndex = 0; // send to first page
            e.Canceled = true;
        }

        public static void SortDataSource(GridTableView oGridTableView)
        {
            if (oGridTableView.SortExpressions.Count > 0)
            {
                string[] oSortExpressionArray;
                SortDirection[] oSortDirectionArray;
                oSortExpressionArray = new string[1];
                oSortDirectionArray = new SortDirection[1];
                oSortExpressionArray[0] = oGridTableView.SortExpressions[0].FieldName;
                switch (oGridTableView.SortExpressions[0].SortOrder)
                {
                    case GridSortOrder.Ascending:
                        oSortDirectionArray[0] = SortDirection.Ascending;
                        break;

                    case GridSortOrder.Descending:
                        oSortDirectionArray[0] = SortDirection.Descending;
                        break;
                }

                IList oList;
                oList = (IList)oGridTableView.DataSource;
                if (oList != null)
                {
                    object[] oArray = new object[oList.Count];
                    oList.CopyTo(oArray, 0);
                    Array.Sort(oArray, new ArrayComparer(oSortExpressionArray, oSortDirectionArray));
                    oGridTableView.DataSource = oArray;
                }
            }
        }

        public static void GetSortExpressionAndDirection(AccountSearchCriteria oAccountSearchCriteria, GridTableView masterTableView)
        {
            oAccountSearchCriteria.SortExpression = masterTableView.SortExpressions[0].FieldName;
            oAccountSearchCriteria.SortDirection = masterTableView.SortExpressions[0].SortOrderAsString();

            switch (oAccountSearchCriteria.SortExpression)
            {
                case "Key2":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key2LabelID");
                    break;
                case "Key3":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key3LabelID");
                    break;
                case "Key4":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key4LabelID");
                    break;
                case "Key5":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key5LabelID");
                    break;
                case "Key6":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key6LabelID");
                    break;
                case "Key7":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key7LabelID");
                    break;
                case "Key8":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key8LabelID");
                    break;
                case "Key9":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("Key9LabelID");
                    break;
                case "AccountType":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("AccountTypeLabelID");
                    break;
                case "FSCaption":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("FSCaptionLabelID");
                    break;
                case "AccountName":
                    oAccountSearchCriteria.SortExpression = GetSortExpression("AccountNameLabelID");
                    break;
            }
        }

        private static string GetSortExpression(string labelID)
        {
            StringBuilder sbSortExpression = new StringBuilder();

            if (labelID == "AccountTypeLabelID")
            {
                sbSortExpression.Append("dbo.fn_GetPhraseForDefaultBuinsessEntityID(");
                sbSortExpression.Append(labelID);
                sbSortExpression.Append(", @LCID, @DefaultLCID)");
            }
            else
            {
                sbSortExpression.Append("dbo.fn_GetPhrase(");
                sbSortExpression.Append(labelID);
                sbSortExpression.Append(", @LCID, @BusinessEntityID, @DefaultLCID)");
            }


            return sbSortExpression.ToString();
        }

        private static int ShowHideColumnsBasedOnOrganizationalHierarchy(int colIndexStart, GridTableView gtv, int? CompanyID, bool allowCustomization)
        {
            // Get the Organizational Hierarchy based on Company ID
            // Show / Hide Columns based on the hierarchy
            List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(CompanyID);

            // Show Columns based on Count
            // First Level is always Company, hence start from 1
            int columnIndex = colIndexStart;
            for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
            {
                // If Customization - then Show / Hide will be done by Customization SP
                // Else Show / Hide columns here only
                if (!allowCustomization)
                {
                    gtv.Columns[columnIndex].Visible = true;
                }
                gtv.Columns[columnIndex].HeaderText = oGeographyStructureHdrInfoCollection[i].GeographyStructure;
                columnIndex++;
            }
            return columnIndex;
        }

        private static void HandleGridCustomization(GridTableView gtv, ARTEnums.Grid eGridType)
        {
            // Show Columns Based on User Personalization
            List<GridColumnInfo> oGridColumnInfoCollection = SessionHelper.GetGridPreference(eGridType);

            if (oGridColumnInfoCollection != null && oGridColumnInfoCollection.Count > 0)
            {
                GridColumn oGridColumn = null;
                for (int j = 0; j < oGridColumnInfoCollection.Count; j++)
                {
                    oGridColumn = gtv.Columns.FindByUniqueNameSafe(oGridColumnInfoCollection[j].ColumnUniqueName);
                    if (oGridColumn != null)
                    {
                        oGridColumn.Visible = true;
                    }
                }
            }
            /* Hide Columns based on Capability
             * This is the last check, since Even if a COlumn is visible because of Customization, 
             * if Capability is De-activated, it shld NOT show
             */
            GridHelper.HideColumnsBasedOnFeatureCapability(gtv);
        }

        public static void ShowHideColumnsBasedOnFeatureCapability(GridTableView gtv)
        {
            // Show / Hide Columns based on Feature + Capability + Rec Period
            /*
             * Cert Status
             * Materiality
             * Zero Balance
             * Key Account
             * Risk Rating
             * Approver
             */

            int? recPeriodID = SessionHelper.CurrentReconciliationPeriodID;

            if (HttpContext.Current.Request.QueryString[QueryStringConstants.IS_REPORT] != null)
            {
                // means this method is called on a Grid which is part of Report
                // hence use the Rec period as selected in Report

                recPeriodID = ReportHelper.GetRecPeriodIDFromReportCriteria();
            }

            GridColumn oGridColumn = gtv.Columns.FindByUniqueNameSafe("CertificationStatus");
            WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = WebEnums.FeatureCapabilityMode.Visible;
            if (oGridColumn != null)
            {
                // Check for Feature Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("Materiality");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MaterialitySelection, recPeriodID.Value, false);
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PercentMateriality");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MaterialitySelection, recPeriodID.Value, false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("ZeroBalance");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.ZeroBalanceAccount, recPeriodID.Value, false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("KeyAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PercentKeyAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }


            #region all related to riskRating
            bool isRiskRatingActivated = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.RiskRating, recPeriodID.Value, false);
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("RiskRating");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isRiskRatingActivated;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("HighAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isRiskRatingActivated;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PercentHighAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isRiskRatingActivated;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("MediumAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isRiskRatingActivated;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PercentMediumAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isRiskRatingActivated;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("LowAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isRiskRatingActivated;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PercentLowAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isRiskRatingActivated;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("RiskRatingFrequency");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = !isRiskRatingActivated;
            }
            #endregion


            oGridColumn = gtv.Columns.FindByUniqueNameSafe("Approver");
            eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, recPeriodID);
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("BackupPreparer");
            if (oGridColumn != null)
            {
                // Check for Feature
                oGridColumn.Visible = Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, recPeriodID);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("BackupReviewer");
            if (oGridColumn != null)
            {
                // Check for Feature
                oGridColumn.Visible = Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, recPeriodID);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("BackupApprover");
            eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.AccountOwnershipBackup, ARTEnums.Capability.DualLevelReview, recPeriodID);
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PreparerDueDays");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("ReviewerDueDays");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("ApproverDueDays");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible && Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.DualLevelReview, recPeriodID.Value, false) ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PendingApproval");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("PendingModificationReviewer");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("Approved");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("AmountBaseCurrency");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.MultiCurrency, ARTEnums.Capability.MultiCurrency, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("MadatoryReportSignOff");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.MandatoryReportSignOff, ARTEnums.Capability.MandatoryReportSignoff, recPeriodID);
                oGridColumn.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
            }

            WebEnums.FeatureCapabilityMode eFeatureCapabilityModeForCertification = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, recPeriodID);
            WebEnums.FeatureCapabilityMode eFeatureCapabilityModeForCEOCFOCertification = Helper.GetFeatureCapabilityMode(WebEnums.Feature.CEO_CFOCertification, ARTEnums.Capability.CEOCFOCertification, recPeriodID);

            bool isAnyCertificationEnabled = ((eFeatureCapabilityModeForCertification == WebEnums.FeatureCapabilityMode.Visible)
                                            || (eFeatureCapabilityModeForCEOCFOCertification == WebEnums.FeatureCapabilityMode.Visible));

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("CertificationBalancesSignOff");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isAnyCertificationEnabled;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("ExceptionCertificationSignOff");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isAnyCertificationEnabled;
            }
            oGridColumn = gtv.Columns.FindByUniqueNameSafe("AccountCertificationSignOff");
            if (oGridColumn != null)
            {
                // Check for Capability
                oGridColumn.Visible = isAnyCertificationEnabled;
            }
            WebEnums.FeatureCapabilityMode eFeatureCapabilityModeForDueDateByAccount = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, recPeriodID);
            GridColumn oGridColumnPreparerDueDate = gtv.Columns.FindByUniqueNameSafe("PreparerDueDate");
            GridColumn oGridColumnReviewerDueDate = gtv.Columns.FindByUniqueNameSafe("ReviewerDueDate");
            GridColumn oGridColumnApproverDueDate = gtv.Columns.FindByUniqueNameSafe("ApproverDueDate");
            if ((short)eFeatureCapabilityModeForDueDateByAccount == (short)WebEnums.FeatureCapabilityMode.Visible)
            {
                if (oGridColumnPreparerDueDate != null)
                    oGridColumnPreparerDueDate.Visible = true;
                if (oGridColumnReviewerDueDate != null)
                    oGridColumnReviewerDueDate.Visible = true;
                if (oGridColumnApproverDueDate != null)
                {
                    eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, recPeriodID);
                    // Check for Capability
                    oGridColumnApproverDueDate.Visible = (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible ? true : false);
                }
            }
            else
            {
                if (oGridColumnPreparerDueDate != null)
                    oGridColumnPreparerDueDate.Visible = false;
                if (oGridColumnReviewerDueDate != null)
                    oGridColumnReviewerDueDate.Visible = false;
                if (oGridColumnApproverDueDate != null)
                    oGridColumnApproverDueDate.Visible = false;
            }
        }

        private static void HideColumnsBasedOnFeatureCapability(GridTableView gtv)
        {
            // Hide Columns based on Feature + Capability + Rec Period
            /*
             * Cert Status
             * Materiality
             * Zero Balance
             * Key Account
             * Risk Rating
             * Approver
             */

            int? recPeriodID = SessionHelper.CurrentReconciliationPeriodID;

            if (HttpContext.Current.Request.QueryString[QueryStringConstants.IS_REPORT] != null)
            {
                // means this method is called on a Grid which is part of Report
                // hence use the Rec period as selected in Report

                recPeriodID = ReportHelper.GetRecPeriodIDFromReportCriteria();
            }

            WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = WebEnums.FeatureCapabilityMode.Visible;
            GridColumn oGridColumn = gtv.Columns.FindByUniqueNameSafe("CertificationStatus");
            // Check for Capability
            if (oGridColumn != null)
            {
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, recPeriodID);
                if (eFeatureCapabilityMode != WebEnums.FeatureCapabilityMode.Visible)
                {
                    oGridColumn.Visible = false;
                }
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("Materiality");
            if (oGridColumn != null
                && !Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MaterialitySelection, recPeriodID.Value, false))
            {
                // Check for Capability
                oGridColumn.Visible = false;
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("ZeroBalance");
            if (oGridColumn != null
                && !Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.ZeroBalanceAccount, recPeriodID.Value, false))
            {
                // Check for Capability
                oGridColumn.Visible = false;
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("KeyAccount");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount, recPeriodID);
                if (eFeatureCapabilityMode != WebEnums.FeatureCapabilityMode.Visible)
                {
                    oGridColumn.Visible = false;
                }
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("RiskRating");
            if (oGridColumn != null
                && !Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.RiskRating, recPeriodID.Value, false))
            {
                // Check for Capability
                oGridColumn.Visible = false;
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("Approver");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, recPeriodID);
                if (eFeatureCapabilityMode != WebEnums.FeatureCapabilityMode.Visible)
                {
                    oGridColumn.Visible = false;
                }
            }

            oGridColumn = gtv.Columns.FindByUniqueNameSafe("AmountBaseCurrency");
            if (oGridColumn != null)
            {
                // Check for Capability
                eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.MultiCurrency, ARTEnums.Capability.MultiCurrency, recPeriodID);
                if (eFeatureCapabilityMode != WebEnums.FeatureCapabilityMode.Visible)
                {
                    oGridColumn.Visible = false;
                }
            }

            WebEnums.FeatureCapabilityMode eFeatureCapabilityModeForDueDateByAccount = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, recPeriodID);
            GridColumn oGridColumnPreparerDueDate = gtv.Columns.FindByUniqueNameSafe("PreparerDueDate");
            GridColumn oGridColumnReviewerDueDate = gtv.Columns.FindByUniqueNameSafe("ReviewerDueDate");
            GridColumn oGridColumnApproverDueDate = gtv.Columns.FindByUniqueNameSafe("ApproverDueDate");
            if ((short)eFeatureCapabilityModeForDueDateByAccount != (short)WebEnums.FeatureCapabilityMode.Visible)
            {
                if (oGridColumnPreparerDueDate != null)
                    oGridColumnPreparerDueDate.Visible = false;
                if (oGridColumnReviewerDueDate != null)
                    oGridColumnReviewerDueDate.Visible = false;
                if (oGridColumnApproverDueDate != null)
                    oGridColumnApproverDueDate.Visible = false;             
               
            }
            eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, recPeriodID);
            if (eFeatureCapabilityMode != WebEnums.FeatureCapabilityMode.Visible)
            {
                if (oGridColumnApproverDueDate != null)
                    oGridColumnApproverDueDate.Visible = false;
            }
        }

        public static void ShowHideColumns(int colIndexStart, GridTableView gtv, int? companyID, ARTEnums.Grid eGrid, bool allowCustomization)
        {
            if (allowCustomization)
            {
                /* In case Customization is Enabled, first hide all columns in the Grid
                 * Grid might be re-loading because of Customization Popup, so hide all columns 
                 * and then show as needed
                 */
                GridHelper.HideAllColumns(colIndexStart, gtv);
            }

            int columnIndex = GridHelper.ShowHideColumnsBasedOnOrganizationalHierarchy(colIndexStart, gtv, companyID, allowCustomization);

            // If Customization is Enabled, then Show Hide Columns based on Customization
            // Else Show Hide Columns based on Capability
            if (allowCustomization)
            {
                GridHelper.HandleGridCustomization(gtv, eGrid);
            }
            else
            {
                GridHelper.ShowHideColumnsBasedOnFeatureCapability(gtv);
            }
        }
        public static void ShowHideColumns(int colIndexStart, GridTableView gtv, ARTEnums.Grid eGrid, bool allowCustomization)
        {
            if (allowCustomization)
            {
                /* In case Customization is Enabled, first hide all columns in the Grid
                 * Grid might be re-loading because of Customization Popup, so hide all columns 
                 * and then show as needed
                 */
                GridHelper.HideAllColumns(colIndexStart, gtv);
            }
            // If Customization is Enabled, then Show Hide Columns based on Customization
            // Else Show Hide Columns based on Capability
            if (allowCustomization)
            {
                GridHelper.HandleGridCustomization(gtv, eGrid);
            }
            else
            {
                GridHelper.ShowHideColumnsBasedOnFeatureCapability(gtv);
            }
        }


        //public static void ShowHideColumns(int colIndexStart, GridTableView gtv, int? companyID)
        //{
        //    int columnIndex = GridHelper.ShowHideColumnsBasedOnOrganizationalHierarchy(colIndexStart, gtv, companyID);


        //}



        public static void SetCSSClassForForwardedItems(Telerik.Web.UI.GridItemEventArgs e, bool? isForwardedItem)
        {
            if (isForwardedItem.HasValue && isForwardedItem.Value)
            {
                // setting the Filter Row CSS - for Carried forward item
                e.Item.CssClass = "rgFilterRow";
            }
        }

        /// <summary>
        /// Use this function to set width of pdf manually
        /// </summary>
        /// <param name="oRadGrid"></param>
        /// <param name="pageTitleID"></param>
        /// <param name="width"></param>
        public static void ExportGridToPDF(ExRadGrid oRadGrid, int pageTitleID, string width)
        {
            if (oRadGrid.Items.Count > 0)
            {
                oRadGrid.ExportSettings.Pdf.PageWidth = Unit.Parse(width);
                ExportGridToPDF(oRadGrid, pageTitleID);
            }
        }

        public static void ExportGridToPDF(ExRadGrid oRadGrid, int pageTitleID)
        {
            if (oRadGrid.Items.Count > 0)
            {
                string fileName = LanguageUtil.GetValue(pageTitleID);
                ExportGridToPDF(oRadGrid, fileName);
            }
        }

        public static void ExportGridToPDF(ExRadGrid oRadGrid, string entityLabelText)
        {
            if (oRadGrid.Items.Count > 0)
            {
                SetPDFExportOptions(oRadGrid, entityLabelText);
                oRadGrid.MasterTableView.ExportToPdf();
            }
        }
        public static void ExportGridToPDFLandScape(ExRadGrid oRadGrid, string fileName, bool isLandscape)
        {
            if (oRadGrid.Items.Count > 0)
            {
                SetPDFExportOptions(oRadGrid, fileName, isLandscape);
                oRadGrid.MasterTableView.ExportToPdf();
            }
        }

        private static void SetPDFExportOptions(ExRadGrid oRadGrid, string entityLabelText)
        {
            if (oRadGrid.Items.Count > 0)
            {
                oRadGrid.EntityNameText = entityLabelText;
                oRadGrid.ExportSettings.FileName = ExportHelper.RemoveInvalidFileNameChars(entityLabelText);
                oRadGrid.ExportSettings.Pdf.Title = entityLabelText;
                oRadGrid.ExportSettings.Pdf.PageLeftMargin = 5;
                oRadGrid.ExportSettings.Pdf.PageRightMargin = 5;
                oRadGrid.ExportSettings.Pdf.PageTopMargin = 5;
                oRadGrid.ExportSettings.Pdf.PageBottomMargin = 5;
                oRadGrid.ExportSettings.Pdf.PaperSize = Telerik.Web.UI.GridPaperSize.A4;
                oRadGrid.ExportSettings.ExportOnlyData = true;
                oRadGrid.ExportSettings.IgnorePaging = true;
                oRadGrid.ExportSettings.OpenInNewWindow = true;


                //Additional condition added to handle the Hierarichal Grid -------by Prafull on 08-Jul-2010
                //************************************************************************************
                oRadGrid.MasterTableView.HierarchyDefaultExpanded = true;

                (oRadGrid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem)["ExpandColumn"].Visible = false;
                foreach (GridDataItem dataItem in oRadGrid.MasterTableView.Items)
                {
                    dataItem["ExpandColumn"].Style["display"] = "none";
                    dataItem["ExpandColumn"].Visible = false;
                }

                if (oRadGrid.MasterTableView.DetailTables.Count > 0)
                {
                    for (int i = 0; i < oRadGrid.MasterTableView.DetailTables.Count; i++)
                    {
                        oRadGrid.MasterTableView.DetailTables[i].HierarchyDefaultExpanded = true;
                        foreach (GridDataItem dataItem in oRadGrid.MasterTableView.DetailTables[i].Items)
                        {
                            dataItem["ExpandColumn"].Style["display"] = "none";
                            dataItem["ExpandColumn"].Visible = false;
                        }

                    }
                }
            }
        }
        private static void SetPDFExportOptions(ExRadGrid oRadGrid, string entityLabelText, bool isLandscape)
        {
            if (oRadGrid.Items.Count > 0)
            {
                oRadGrid.EntityNameText = entityLabelText;
                oRadGrid.ExportSettings.FileName = ExportHelper.RemoveInvalidFileNameChars(entityLabelText);
                oRadGrid.ExportSettings.Pdf.Title = entityLabelText;
                oRadGrid.ExportSettings.Pdf.PageLeftMargin = 5;
                oRadGrid.ExportSettings.Pdf.PageRightMargin = 5;
                oRadGrid.ExportSettings.Pdf.PageTopMargin = 5;
                oRadGrid.ExportSettings.Pdf.PageBottomMargin = 5;
                oRadGrid.ExportSettings.Pdf.PaperSize = Telerik.Web.UI.GridPaperSize.A4;
                if (isLandscape)
                {
                    oRadGrid.ExportSettings.Pdf.PageHeight = Unit.Parse("400mm");
                    oRadGrid.ExportSettings.Pdf.PageWidth = Unit.Parse("600mm");
                }
                oRadGrid.ExportSettings.ExportOnlyData = true;
                oRadGrid.ExportSettings.IgnorePaging = true;
                oRadGrid.ExportSettings.OpenInNewWindow = true;


                //Additional condition added to handle the Hierarichal Grid -------by Prafull on 08-Jul-2010
                //************************************************************************************
                oRadGrid.MasterTableView.HierarchyDefaultExpanded = true;

                (oRadGrid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem)["ExpandColumn"].Visible = false;
                foreach (GridDataItem dataItem in oRadGrid.MasterTableView.Items)
                {
                    dataItem["ExpandColumn"].Style["display"] = "none";
                    dataItem["ExpandColumn"].Visible = false;
                }

                if (oRadGrid.MasterTableView.DetailTables.Count > 0)
                {
                    for (int i = 0; i < oRadGrid.MasterTableView.DetailTables.Count; i++)
                    {
                        oRadGrid.MasterTableView.DetailTables[i].HierarchyDefaultExpanded = true;
                        foreach (GridDataItem dataItem in oRadGrid.MasterTableView.DetailTables[i].Items)
                        {
                            dataItem["ExpandColumn"].Style["display"] = "none";
                            dataItem["ExpandColumn"].Visible = false;
                        }

                    }
                }
            }
        }

        public static void SetStylesForExportGrid(GridItemEventArgs e, bool isExportPDF, bool isExportExcel)
        {

            if (e.Item is GridGroupHeaderItem && isExportPDF)
            {
                e.Item.Style["background-color"] = "#DBC682";
                GridGroupHeaderItem headerItem = (GridGroupHeaderItem)e.Item;
                headerItem.CssClass = "groupRadGrid";
                headerItem.Style["font-size"] = "8px";
                headerItem.Style["font-family"] = "Arial";
                headerItem.Style["vertical-align"] = "middle";
                headerItem.Style["font-weight"] = "normal";
                headerItem.Style["background-color"] = "#DBC682";
                foreach (TableCell cell in headerItem.Cells)
                {
                    cell.Style["text-align"] = "left";
                    cell.Style["font-size"] = "8pt";
                    cell.Style["font-family"] = "Arial";
                    cell.Style["vertical-align"] = "middle";
                    cell.Style["background"] = "#DBC682";
                    cell.Style["background-color"] = "#DBC682";
                    cell.Style["font-weight"] = "normal";
                }
            }

            if (e.Item is GridHeaderItem && isExportPDF)
            {
                e.Item.Style["background-color"] = "#ab6501";
                GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                headerItem.Style["font-size"] = "9px";
                headerItem.Style["font-family"] = "Arial";
                headerItem.Style["vertical-align"] = "middle";
                headerItem.Style["color"] = "#ffffff";
                headerItem.Style["background-color"] = "#ab6501";
                foreach (TableCell cell in headerItem.Cells)
                {
                    cell.Style["text-align"] = "left";
                    cell.Style["font-size"] = "9pt";
                    cell.Style["font-family"] = "Arial";
                    cell.Style["vertical-align"] = "middle";
                    cell.Style["background"] = "#ab6501";
                    cell.Style["background-color"] = "#ab6501";
                }
            }
            if (e.Item is GridFooterItem && isExportPDF)
            {

                GridFooterItem gridItem = (GridFooterItem)e.Item;
                gridItem.Style["font-size"] = "8pt";
                gridItem.Style["font-family"] = "Arial";
                gridItem.Style["vertical-align"] = "middle";
                gridItem.Style["font-weight"] = "normal";
                if (e.Item.ItemIndex % 2 == 0)
                    gridItem.Style["background-color"] = "#E8D59C";
                else
                    gridItem.Style["background-color"] = "#E8D59C";
                foreach (TableCell cell in gridItem.Cells)
                {
                    cell.Style["font-size"] = "8pt";
                    cell.Style["font-family"] = "Arial";
                    cell.Style["vertical-align"] = "middle";
                    cell.Style["font-weight"] = "normal";
                }

                //e.Item.Style["background-color"] = "#E8D59C";
                //GridFooterItem footerItem = (GridFooterItem)e.Item;
                //footerItem.Style["font-family"] = "Arial";

                //footerItem.Style["font-size"] = "9px";
                //footerItem.Style["text-decoration"] = "none";
                //footerItem.Style["font-weight"] = "normal";
                //footerItem.Style["padding-left"] = "3px";
                //footerItem.Style["vertical-align"] = "middle";

                //footerItem.Style["color"] = "#000000";

                //foreach (TableCell cell in footerItem.Cells)
                //{

                //    //cell.Style["font-weight"] = "bold";
                //    cell.Style["text-align"] = "middle";
                //    cell.Style["font-size"] = "9pt";
                //    // cell.Style["font-family"] = "Arial";
                //    cell.Style["vertical-align"] = "middle";
                //    cell.Style["background"] = "#E8D59C";
                //    //cell.Style["background-color"] = "#ab6501";

                //}
            }
            if (e.Item is GridHeaderItem && isExportExcel)
            {

                GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                headerItem.Style["font-size"] = "9px";
                headerItem.Style["font-family"] = "Arial";
                headerItem.Style["vertical-align"] = "middle";
                headerItem.Style["font-weight"] = "bold";
                headerItem.Style["border"] = "1px solid #d0d7e5";
                foreach (TableCell cell in headerItem.Cells)
                {

                    cell.Style["font-weight"] = "bold";
                    cell.Style["text-align"] = "middle";
                    cell.Style["font-size"] = "9pt";
                    cell.Style["font-family"] = "Arial";
                    cell.Style["vertical-align"] = "middle";
                    cell.Style["border"] = "1px solid #d0d7e5";


                }

            }

            if (e.Item is GridDataItem && isExportPDF)
            {

                if (e.Item is GridItem)
                {
                    GridItem gridItem = (GridItem)e.Item;
                    gridItem.Style["font-size"] = "8pt";
                    gridItem.Style["font-family"] = "Arial";
                    gridItem.Style["vertical-align"] = "middle";
                    gridItem.Style["font-weight"] = "normal";
                    //gridItem.Style["text-align"] = "left";
                    if (e.Item.ItemIndex % 2 == 0)
                        gridItem.Style["background-color"] = "#e2f2fc";
                    else
                        gridItem.Style["background-color"] = "#a6d3f7";
                    foreach (TableCell cell in gridItem.Cells)
                    {
                        //cell.Style["text-align"] = "left";
                        cell.Style["font-size"] = "8pt";
                        cell.Style["font-family"] = "Arial";
                        cell.Style["vertical-align"] = "middle";
                        cell.Style["font-weight"] = "normal";
                    }
                }
            }
            if (e.Item is GridDataItem && isExportExcel)
            {


                if (e.Item is GridItem)
                {
                    GridItem gridItem = (GridItem)e.Item;
                    gridItem.Style["font-size"] = "8pt";
                    gridItem.Style["font-family"] = "Arial";
                    gridItem.Style["vertical-align"] = "middle";
                    gridItem.Style["font-weight"] = "normal";
                    //gridItem.Style["text-align"] = "middle";
                    gridItem.Style["border"] = "1px solid #d0d7e5";
                    foreach (TableCell cell in gridItem.Cells)
                    {

                        //cell.Style["text-align"] = "left";
                        //cell.Style["text-align"] = "middle";
                        cell.Style["font-size"] = "8pt";
                        cell.Style["font-family"] = "Arial";
                        cell.Style["vertical-align"] = "middle";
                        cell.Style["font-weight"] = "normal";
                        cell.Style["border"] = "1px solid #d0d7e5";
                    }
                }


            }


        }

        public static void ExportGridToExcel(RadGrid oRadGrid, int pageTitleID)
        {
            if (oRadGrid.Items.Count > 0)
            {
                oRadGrid.ExportSettings.FileName = ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(pageTitleID));
                oRadGrid.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Html;
                oRadGrid.ExportSettings.ExportOnlyData = true;
                oRadGrid.ExportSettings.IgnorePaging = true;
                oRadGrid.ExportSettings.OpenInNewWindow = true;


                //Additional condition added to handle the Hierarichal Grid -------by Prafull on 08-Jul-2010
                //************************************************************************************
                oRadGrid.MasterTableView.HierarchyDefaultExpanded = true;

                ////(oRadGrid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem)["ExpandColumn"].Visible = false;
                ////foreach (GridDataItem dataItem in oRadGrid.MasterTableView.Items)
                ////{
                ////    dataItem["ExpandColumn"].Style["display"] = "none";
                ////    dataItem["ExpandColumn"].Visible = false;
                ////}

                oRadGrid.MasterTableView.GroupHeaderItemStyle.CssClass = "groupRadGrid";
                if (oRadGrid.MasterTableView.DetailTables.Count > 0)
                {
                    for (int i = 0; i < oRadGrid.MasterTableView.DetailTables.Count; i++)
                    {
                        oRadGrid.MasterTableView.DetailTables[i].HierarchyDefaultExpanded = true;
                        foreach (GridDataItem dataItem in oRadGrid.MasterTableView.DetailTables[i].Items)
                        {
                            dataItem["ExpandColumn"].Style["display"] = "none";
                            dataItem["ExpandColumn"].Visible = false;
                        }
                    }
                }
                //************************************************************************************

                oRadGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
                oRadGrid.MasterTableView.ExportToExcel();
            }
        }

        public static void ExportGridToExcel(ExRadGrid oRadGrid, string entityLabelText)
        {
            if (oRadGrid.Items.Count > 0)
            {

                oRadGrid.EntityNameText = entityLabelText;
                oRadGrid.ExportSettings.FileName = ExportHelper.RemoveInvalidFileNameChars(entityLabelText);
                oRadGrid.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Html;
                oRadGrid.ExportSettings.ExportOnlyData = true;
                oRadGrid.ExportSettings.IgnorePaging = true;
                oRadGrid.ExportSettings.OpenInNewWindow = true;


                //Additional condition added to handle the Hierarichal Grid -------by Prafull on 08-Jul-2010
                //************************************************************************************
                oRadGrid.MasterTableView.HierarchyDefaultExpanded = true;

                ////(oRadGrid.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem)["ExpandColumn"].Visible = false;
                ////foreach (GridDataItem dataItem in oRadGrid.MasterTableView.Items)
                ////{
                ////    dataItem["ExpandColumn"].Style["display"] = "none";
                ////    dataItem["ExpandColumn"].Visible = false;
                ////}


                if (oRadGrid.MasterTableView.DetailTables.Count > 0)
                {
                    for (int i = 0; i < oRadGrid.MasterTableView.DetailTables.Count; i++)
                    {
                        oRadGrid.MasterTableView.DetailTables[i].HierarchyDefaultExpanded = true;
                        foreach (GridDataItem dataItem in oRadGrid.MasterTableView.DetailTables[i].Items)
                        {
                            dataItem["ExpandColumn"].Style["display"] = "none";
                            dataItem["ExpandColumn"].Visible = false;
                        }
                    }
                }
                //************************************************************************************

                oRadGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
                oRadGrid.MasterTableView.ExportToExcel();
            }

        }


        /// <summary>
        /// Set Sort Expression, Default Sort Order = ASC
        /// </summary>
        /// <param name="gtv"></param>
        /// <param name="fieldName">Field Name to Sort</param>
        public static void SetSortExpression(GridTableView gtv, string fieldName)
        {
            GridHelper.SetSortExpression(gtv, fieldName, GridSortOrder.Ascending);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gtv"></param>
        /// <param name="fieldName">Field Name to Sort</param>
        /// <param name="sortOrder"></param>
        public static void SetSortExpression(GridTableView gtv, string fieldName, GridSortOrder sortOrder)
        {
            // Add Default Sort as Date Revised, Desc
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = fieldName;
            oGridSortExpression.SortOrder = sortOrder;
            gtv.SortExpressions.AddSortExpression(oGridSortExpression);
        }


        public static void RegisterPDFAndExcelForPostback(GridItemEventArgs e, bool isExportPDF, bool isExportExcel, Page oPage)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.CommandItem && !isExportExcel && !isExportPDF)
                {
                    PageBase oPageBase = oPage as PageBase;

                    Control imgBtn = e.Item.FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON);
                    if (imgBtn != null && oPageBase != null)
                    {
                        Helper.RegisterPostBackToControls(oPageBase, imgBtn);
                    }

                    imgBtn = e.Item.FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON);
                    if (imgBtn != null && oPageBase != null)
                    {
                        Helper.RegisterPostBackToControls(oPageBase, imgBtn);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void HideAllColumns(int colIndexStart, GridTableView gtv)
        {
            for (int i = colIndexStart; i < gtv.Columns.Count; i++)
            {
                gtv.Columns[i].Visible = false;
            }
        }

        public static void HideColumns(GridColumnCollection GridColumnCollection, List<string> HideColumnNameList)
        {
            foreach (string columnName in HideColumnNameList)
            {
                if (GridColumnCollection.FindByUniqueNameSafe(columnName) != null)
                    GridColumnCollection.FindByUniqueNameSafe(columnName).Visible = false;
            }
        }

        public static void ShowColumns(GridColumnCollection GridColumnCollection, List<string> HideColumnNameList)
        {
            foreach (string columnName in HideColumnNameList)
            {
                if (GridColumnCollection.FindByUniqueNameSafe(columnName) != null)
                    GridColumnCollection.FindByUniqueNameSafe(columnName).Visible = true;
            }
        }

        public static void HideColumns(GridColumnCollection GridColumnCollection, List<ReportColumnInfo> HideColumnList)
        {
            foreach (ReportColumnInfo oReportColumnInfo in HideColumnList)
            {
                if (GridColumnCollection.FindByUniqueNameSafe(oReportColumnInfo.ColumnUniqueName) != null)
                    GridColumnCollection.FindByUniqueNameSafe(oReportColumnInfo.ColumnUniqueName).Visible = false;
            }
        }

        public static void ShowColumns(GridColumnCollection GridColumnCollection, List<ReportColumnInfo> ShowColumnList)
        {
            foreach (ReportColumnInfo oReportColumnInfo in ShowColumnList)
            {
                if (GridColumnCollection.FindByUniqueNameSafe(oReportColumnInfo.ColumnUniqueName) != null)
                    GridColumnCollection.FindByUniqueNameSafe(oReportColumnInfo.ColumnUniqueName).Visible = true;
            }
        }

        public static Dictionary<string, string> GetAccountBulkUpdataForBinding(AccountHdrInfo accountInfo)
        {
            Dictionary<string, string> dictAccountBulkUpdateInfo = new Dictionary<string, string>();

            //columns specific to Bulk Update Grid
            dictAccountBulkUpdateInfo.Add("AccountNumber", Helper.GetDisplayStringValue(accountInfo.AccountNumber));
            dictAccountBulkUpdateInfo.Add("AccountName", Helper.GetDisplayStringValue(accountInfo.AccountName));
            dictAccountBulkUpdateInfo.Add("NetAccount", Helper.GetDisplayStringValue(accountInfo.NetAccount));

            dictAccountBulkUpdateInfo.Add("ZeroBalance", Helper.GetDisplayStringValue(accountInfo.ZeroBalance));

            dictAccountBulkUpdateInfo.Add("KeyAccount", Helper.GetDisplayStringValue(accountInfo.KeyAccount));

            dictAccountBulkUpdateInfo.Add("IsReconcilable", Helper.GetDisplayStringValue(accountInfo.Reconcilable));

            dictAccountBulkUpdateInfo.Add("RiskRating", accountInfo.RiskRating);
            dictAccountBulkUpdateInfo.Add("ReconciliationTemplate", accountInfo.ReconciliationTemplate);
            dictAccountBulkUpdateInfo.Add("Subledger", Helper.GetDisplayStringValue(accountInfo.SubLedgerSource));

            //columns specific to Accounts Grid User control (Key2 to Key9)
            dictAccountBulkUpdateInfo.Add("Key2", accountInfo.Key2);
            dictAccountBulkUpdateInfo.Add("Key3", accountInfo.Key3);
            dictAccountBulkUpdateInfo.Add("Key4", accountInfo.Key4);
            dictAccountBulkUpdateInfo.Add("Key5", accountInfo.Key5);
            dictAccountBulkUpdateInfo.Add("Key6", accountInfo.Key6);
            dictAccountBulkUpdateInfo.Add("Key7", accountInfo.Key7);
            dictAccountBulkUpdateInfo.Add("Key8", accountInfo.Key8);
            dictAccountBulkUpdateInfo.Add("Key9", accountInfo.Key9);
            dictAccountBulkUpdateInfo.Add("FSCaption", accountInfo.FSCaption);
            dictAccountBulkUpdateInfo.Add("AccountType", accountInfo.AccountType);
            dictAccountBulkUpdateInfo.Add("Preparer", accountInfo.PreparerFullName);
            dictAccountBulkUpdateInfo.Add("Reviewer", accountInfo.ReviewerFullName);
            dictAccountBulkUpdateInfo.Add("Approver", accountInfo.ApproverFullName);
            if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
            {
                dictAccountBulkUpdateInfo.Add("BackupPreparer", accountInfo.BackupPreparerFullName);
                dictAccountBulkUpdateInfo.Add("BackupReviewer", accountInfo.BackupReviewerFullName);
                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                {
                    dictAccountBulkUpdateInfo.Add("BackupApprover", accountInfo.BackupApproverFullName);
                }
            }
            if (accountInfo.IsExcludeOwnershipForZBA.HasValue)
            {
                dictAccountBulkUpdateInfo.Add("ExcludeOwnershipForZBA", accountInfo.IsExcludeOwnershipForZBA.Value.ToString());
            }
            else
            {
                dictAccountBulkUpdateInfo.Add("ExcludeOwnershipForZBA", "-");
            }
            if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
            {
                dictAccountBulkUpdateInfo.Add("PreparerDueDays", Helper.GetDisplayIntegerValue(accountInfo.PreparerDueDays));
                dictAccountBulkUpdateInfo.Add("ReviewerDueDays", Helper.GetDisplayIntegerValue(accountInfo.ReviewerDueDays));
                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                {
                    dictAccountBulkUpdateInfo.Add("ApproverDueDays", Helper.GetDisplayIntegerValue(accountInfo.ApproverDueDays));
                }
            }
            return dictAccountBulkUpdateInfo;
        }
        /// <summary>
        /// Remove invalid characters for Column Unique Name
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string RemoveSpecialChars(string colName)
        {
            return colName.Replace(' ', '@');
        }

        /// <summary>
        /// Get Actual value for Column Unique Name
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string GetWithSpecialChars(string colName)
        {
            return colName.Replace('@', ' ');
        }
        /// <summary>
        /// Bind Page Size Grid
        /// </summary>       
        public static void BindPageSizeGrid(DropDownList DDL)
        {
            String defaultPageSize = AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_PAGE_SIZE);
            String[] PageSize = defaultPageSize.Split(',');
            for (int i = 0; i < PageSize.Length; i++)
            {
                DDL.Items.Add(new ListItem(PageSize[i], PageSize[i]));
            }
        }
        public static void SetCSSClassForIgnoreInCalculationItems(Telerik.Web.UI.GridItemEventArgs e, bool? isIgnoreInCalculation)
        {
            if (isIgnoreInCalculation.HasValue && isIgnoreInCalculation.Value)
            {
                // setting the Filter Row CSS - for Carried forward item
                e.Item.CssClass = "rgIgnoreInCalculation";
            }
        }
        public static void SetRecordCount(ExRadGrid rg)
        {
            rg.PagerStyle.PagerTextFormat = LanguageUtil.GetValue(2844);
        }

        public static void UpdateGridColumnInfoForCustomField(List<GridColumnInfo> oGridColumnInfoCollection)
        {
            TaskCustomFieldInfo oTaskCustomFieldInfo = null;
            List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = TaskMasterHelper.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID);
            for (int i = 0; i < oGridColumnInfoCollection.Count; i++)
            {
                switch (oGridColumnInfoCollection[i].ColumnID)
                {
                    case 64:
                        oTaskCustomFieldInfo = oTaskCustomFieldInfoList.Find(T => T.TaskCustomFieldID == (short)WebEnums.TaskCustomField.CustomField1);
                        if (oTaskCustomFieldInfo != null && !string.IsNullOrEmpty(oTaskCustomFieldInfo.CustomFieldValue))
                            oGridColumnInfoCollection[i].Name = oTaskCustomFieldInfo.CustomFieldValue;
                        break;
                    case 65:
                        oTaskCustomFieldInfo = oTaskCustomFieldInfoList.Find(T => T.TaskCustomFieldID == (short)WebEnums.TaskCustomField.CustomField2);
                        if (oTaskCustomFieldInfo != null && !string.IsNullOrEmpty(oTaskCustomFieldInfo.CustomFieldValue))
                            oGridColumnInfoCollection[i].Name = oTaskCustomFieldInfo.CustomFieldValue;
                        break;
                    default:
                        oGridColumnInfoCollection[i].Name = LanguageUtil.GetValue(oGridColumnInfoCollection[i].LabelID.Value);
                        break;
                }
            }
        }    
    }
}