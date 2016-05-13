using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls.Matching.Wizard
{
    public partial class DisplayColumnSelection : UserControlMatchingWizardBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ucMatchingCombinationSelection.OnMatchingCombinationSelectionChanged += new MatchingCombinationSelection.MatchingCombinationSelectionChanged(ucMatchingCombinationSelection_OnMatchingCombinationSelectionChanged);
            MatchingWizardStepType = ARTEnums.MatchingWizardSteps.DisplayColumnSelection;
            WizardStepID = (int)ARTEnums.WizardSteps.DisplayColumnSelection;

        }

        void ucMatchingCombinationSelection_OnMatchingCombinationSelectionChanged(MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo)
        {
            rbFirstMatchingSource.Checked = false;
            rbSecondMatchingSource.Checked = false;

            UpdateSession();
            PopulateGrid();
        }

        public void BindGrid()
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = null;
            if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
                oMatchingConfigurationInfoCollection = (List<MatchingConfigurationInfo>)Session[SessionConstants.MATCHING_CONFIGURATION_DATA];
            rgDisplayColumnSelection.DataSource = oMatchingConfigurationInfoCollection;
            rgDisplayColumnSelection.DataBind();
        }

        public void PopulateGrid()
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
            oMatchingConfigurationInfoCollection = GetGridData();

            Session[SessionConstants.MATCHING_CONFIGURATION_DATA] = oMatchingConfigurationInfoCollection;
            BindGrid();
        }

        public List<MatchingConfigurationInfo> GetGridData()
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = null;
            MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = ucMatchingCombinationSelection.CurrentMatchSetSubSetCombinationInfo;
            if (oMatchSetSubSetCombinationInfo != null)
                oMatchingConfigurationInfoList = oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
            return oMatchingConfigurationInfoList;
        }

        protected void UpdateSession()
        {
            try
            {
                long lngMatchSetSubSetCombinationID = 0;
                List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = null;
                //*** get List from Grid data source
                if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
                    oMatchingConfigurationInfoCollection = (List<MatchingConfigurationInfo>)Session[SessionConstants.MATCHING_CONFIGURATION_DATA];

                if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
                {
                    foreach (GridDataItem item in rgDisplayColumnSelection.MasterTableView.Items)
                    {
                        long MatchingConfigurationID = (long)item.GetDataKeyValue("MatchingConfigurationID");
                        foreach (MatchingConfigurationInfo oMatchingConfiguration in oMatchingConfigurationInfoCollection)
                        {
                            if (oMatchingConfiguration.MatchingConfigurationID == MatchingConfigurationID)
                            {
                                ExCheckBox chkIsDisplayColumn = (ExCheckBox)item.FindControl("chkSelectColumn");
                                TextBox txtDisplayName = (TextBox)item.FindControl("txtDisplayName");
                                ExLabel lblDataTypeID = (ExLabel)item.FindControl("lblDataTypeID");
                                ExLabel lblMatchSetSubSetCombinationID = (ExLabel)item.FindControl("lblMatchSetSubSetCombinationID");
                                HiddenField hdnDisplayColumn = (HiddenField)item.FindControl("hdnDisplayColumn");
                              
                                oMatchingConfiguration.DisplayColumnName = txtDisplayName.Text.Trim();
                                oMatchingConfiguration.IsDisplayedColumn = chkIsDisplayColumn.Checked;
                                oMatchingConfiguration.RevisedBy = SessionHelper.CurrentUserLoginID;
                                oMatchingConfiguration.DateRevised = DateTime.Now;
                               
                            }
                        }
                    }
                    lngMatchSetSubSetCombinationID = oMatchingConfigurationInfoCollection[0].MatchSetSubSetCombinationID.Value;
                    MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Find(p => p.MatchSetSubSetCombinationID == lngMatchSetSubSetCombinationID);
                    oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList = oMatchingConfigurationInfoCollection;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        public override bool SaveData()
        {
            //Save values  in Database from session 
            UpdateSession();
            if (SaveValuesinDB())
            {
                Session[SessionConstants.MATCHING_CONFIGURATION_DATA] = null;
                ReloadMatchingConfigurationData();
            }
            return true;
        }

        bool SaveValuesinDB()
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();

            oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oMatchingParamInfo.DateRevised = DateTime.Now;

            // Prepare Records to Save
            if (CurrentMatchSetHdrInfo != null)
            {
                if (CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection != null && CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Count > 0)
                {
                    foreach (MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo in CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection)
                    {
                        if (oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList != null && oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList.Count > 0)
                        {
                            if (oMatchingParamInfo.oMatchingConfigurationInfoList == null)
                                oMatchingParamInfo.oMatchingConfigurationInfoList = new List<MatchingConfigurationInfo>();
                            oMatchingParamInfo.oMatchingConfigurationInfoList.AddRange(oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList);
                        }
                    }
                }
            }
            // Save Records to Database
            int recordsAffected = MatchingHelper.UpdateMatchingConfigurationDisplayedColumn(oMatchingParamInfo);
            if (recordsAffected > 0)
            {
                return true;
            }
            return false;
        }

        private void ReloadMatchingConfigurationData()
        {
            if (CurrentMatchSetHdrInfo != null)
            {
                CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection = MatchingHelper.GetAllMatchSetSubSetCombination(CurrentMatchSetHdrInfo.MatchSetID, GLDataID, SessionHelper.CurrentReconciliationPeriodID, null);
            }
        }

        protected void rgDisplayColumnSelection_ItemDataBound(object Sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                MatchingConfigurationInfo oMatchingConfigurationInfo = (MatchingConfigurationInfo)e.Item.DataItem;

                ExCheckBox chkSelectColumn = (ExCheckBox)e.Item.FindControl("chkSelectColumn");
                chkSelectColumn.Checked = Convert.ToBoolean(oMatchingConfigurationInfo.IsDisplayedColumn);

                if ((oMatchingConfigurationInfo.IsMatching != null && oMatchingConfigurationInfo.IsMatching == true) ||
                     (oMatchingConfigurationInfo.IsPartialMatching != null && oMatchingConfigurationInfo.IsPartialMatching == true))
                {
                    chkSelectColumn.Checked = true;
                    chkSelectColumn.Visible = false;
                }

                ExLabel lblMatchingConfigurationID = (ExLabel)e.Item.FindControl("lblMatchingConfigurationID");
                lblMatchingConfigurationID.Text = oMatchingConfigurationInfo.MatchingConfigurationID.ToString();

                ExLabel lblSource1ColumnName = (ExLabel)e.Item.FindControl("lblSource1ColumnName");

                lblSource1ColumnName.Text = Helper.GetDisplayStringValue(oMatchingConfigurationInfo.ColumnName1);

                ExLabel lblSource2ColumnName = (ExLabel)e.Item.FindControl("lblSource2ColumnName");
                lblSource2ColumnName.Text = Helper.GetDisplayStringValue(oMatchingConfigurationInfo.ColumnName2);

                TextBox txtDisplayName = (TextBox)e.Item.FindControl("txtDisplayName");
                if (oMatchingConfigurationInfo.DisplayColumnName != null)
                {
                    txtDisplayName.Text = oMatchingConfigurationInfo.DisplayColumnName.ToString();
                }

                ExLabel lblDataTypeID = (ExLabel)e.Item.FindControl("lblDataTypeID");
                lblDataTypeID.Text = oMatchingConfigurationInfo.DataTypeID.ToString();

                ExLabel lblMatchSetSubSetCombinationID = (ExLabel)e.Item.FindControl("lblMatchSetSubSetCombinationID");
                lblMatchSetSubSetCombinationID.Text = oMatchingConfigurationInfo.MatchSetSubSetCombinationID.ToString();

                if (txtDisplayName != null)
                    txtDisplayName.Enabled = this.IsEditMode;

            }
        }

        public override void ClearData()
        {

        }

        /// <summary>
        /// Override this method for Loading the data in Wizard Step
        /// </summary>
        public override void LoadData()
        {
            PageBase oPageBase = (PageBase)this.Page;
            Helper.ShowInputRequirementSection(oPageBase, 2289, 2290, 2395);
            if (CurrentMatchSetHdrInfo != null)
            {
                uscMatchSetInfo.PopulateData(CurrentMatchSetHdrInfo);
                ucMatchingCombinationSelection.BindMatchingCombination(CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection);
                PopulateGrid();
            }
        }

        private void PopulateData(MatchSetHdrInfo oMatchSetHdrInfo)
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = null;
            if (ucMatchingCombinationSelection.CurrentMatchSetSubSetCombinationInfo != null)
            {
                oMatchingConfigurationInfoCollection = ucMatchingCombinationSelection.CurrentMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
                if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
                {
                    rgDisplayColumnSelection.DataSource = oMatchingConfigurationInfoCollection;
                    rgDisplayColumnSelection.DataBind();
                }
            }
        }

        public override void SetControlStatePostLoad()
        {
            base.SetControlStatePostLoad();
            rbFirstMatchingSource.Enabled = this.IsEditMode;
            rbSecondMatchingSource.Enabled = this.IsEditMode;
            GridColumn oGridColumn = rgDisplayColumnSelection.MasterTableView.Columns.FindByUniqueNameSafe("chkSelectColumn");
            if (oGridColumn != null)
                oGridColumn.Display = this.IsEditMode;
        }

        /// <summary>
        /// Can Load Step or not
        /// </summary>
        /// <returns></returns>
        public override bool CanLoadStep()
        {
            if (CurrentMatchSetHdrInfo != null)
            {
                if (CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection != null && CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Count > 0)
                    return true;
            }
            return false;
        }
    }
}
