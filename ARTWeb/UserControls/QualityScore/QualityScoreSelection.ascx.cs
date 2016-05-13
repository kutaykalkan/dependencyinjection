using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Web.Utility;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;

namespace SkyStem.ART.Web.UserControls
{
    public partial class QualityScoreSelection : UserControlQualityScoreBase
    {

        #region Properties

        /// <summary>
        /// Do not use variable, use property instead
        /// </summary>
        private List<CompanyQualityScoreInfo> _CompanyQualityScoreInfoList = null;
        /// <summary>
        /// Gets or sets the company quality score info list. 
        /// </summary>
        /// <value>
        /// The company quality score info list.
        /// </value>
        public List<CompanyQualityScoreInfo> CompanyQualityScoreInfoList
        {
            get
            {
                if (_CompanyQualityScoreInfoList == null)
                    _CompanyQualityScoreInfoList = (List<CompanyQualityScoreInfo>)Session[SessionConstants.QUALITY_SCORE_COMPANT_QUALITY_METRIC_LIST];
                return _CompanyQualityScoreInfoList;
            }
            set
            {
                _CompanyQualityScoreInfoList = value;
                Session[SessionConstants.QUALITY_SCORE_COMPANT_QUALITY_METRIC_LIST] = value;
            }
        }

        private bool IsCertificationStarted { get; set; }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ucInputRequirements.ShowInputRequirements(2424, 2425);
            SetControlState();
            if (!CertificationHelper.IsCertificationStarted() &&
           (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
           || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open
           || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress))
            {
                btnSave.Visible = true;
                pnlQualityScore.Enabled = true;
            }
            else
            {
                btnSave.Visible = false;
                pnlQualityScore.Enabled = false;
            }
        }

        #endregion

        #region Control Events

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Helper.RedirectToHomePage();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            SaveQualityScore();
        }

        #endregion

        #region Grid Events

        protected void rgQualityScoreSelection_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    CompanyQualityScoreInfo oCompanyQualityScoreInfo = (CompanyQualityScoreInfo)e.Item.DataItem;
                    GridDataItem oItem = e.Item as GridDataItem;

                    CheckBox chkSelectCol = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                    ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
                    ExLabel lblQualityScoreNumber = (ExLabel)e.Item.FindControl("lblQualityScoreNumber");
                    lblDescription.Text = oCompanyQualityScoreInfo.Description;
                    lblQualityScoreNumber.Text = Helper.GetDisplayStringValue(oCompanyQualityScoreInfo.QualityScoreNumber);
                    if (oItem != null)
                        oItem.Selected = oCompanyQualityScoreInfo.IsEnabled.GetValueOrDefault();
                    if (IsCertificationStarted)
                        chkSelectCol.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        #endregion

        #region Custom Functions
        /// <summary>
        /// Populates the data.
        /// </summary>
        public void PopulateData()
        {
            SetControlState();
            CompanyQualityScoreInfoList = QualityScoreHelper.GetCompanyQualityScoreInfoList(false);
            rgQualityScoreSelection.DataSource = CompanyQualityScoreInfoList;
            rgQualityScoreSelection.DataBind();
        }

        /// <summary>
        /// Sets the state of the control.
        /// </summary>
        private void SetControlState()
        {
            pnlQualityScore.Enabled = true;
            btnSave.Visible = true;
            IsCertificationStarted = CertificationHelper.IsCertificationStarted();
            if (IsCertificationStarted)
            {
                btnSave.Visible = false;
                pnlQualityScore.Enabled = false;
            }
            else
            {
                ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = Helper.GetRecPeriodStatusByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID);
                if (oReconciliationPeriodStatusMstInfo!=null &&( oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.Open ||
                   oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.InProgress))
                {
                    btnSave.Attributes.Remove("onclick");
                    btnSave.Attributes.Add("onclick", "return ConfirmAndSubmit('" + string.Format(LanguageUtil.GetValue(2459), LanguageUtil.GetValue(oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusLabelID.Value)) + "');");
                }
            }
        }

        /// <summary>
        /// Saves the quality score.
        /// </summary>
        public void SaveQualityScore()
        {
            try
            {
                foreach (GridDataItem oItem in rgQualityScoreSelection.Items)
                {
                    int rowNumber = (int)oItem.GetDataKeyValue("RowNumber");
                    CompanyQualityScoreInfo oCompanyQualityScoreInfo = CompanyQualityScoreInfoList.Find(T => T.RowNumber == rowNumber);
                    if (oCompanyQualityScoreInfo != null)
                    {
                        oCompanyQualityScoreInfo.IsEnabled = oItem.Selected;
                    }
                }
                QualityScoreHelper.SaveCompanyQualityScoreInfoList(CompanyQualityScoreInfoList, SessionHelper.CurrentUserLoginID);
                Helper.RedirectToHomePage(2452);
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

        #endregion

    }
}