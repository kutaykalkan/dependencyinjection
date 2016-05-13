using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Matching;
using Telerik.Web.UI;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls.Matching.Wizard
{
    public partial class PreviewConfirm : UserControlMatchingWizardBase
    {
        #region Control Properties 
        List<MatchingSourceDataImportHdrInfo> _MatchingSourceDataImportHdrInfoList = null;
        private List<MatchingSourceDataImportHdrInfo> MatchingSourceDataImportHdrInfoList
        {
            get
            {
                if (CurrentMatchSetHdrInfo != null)
                {
                    _MatchingSourceDataImportHdrInfoList = CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList;
                }
                return _MatchingSourceDataImportHdrInfoList;
            }
        }

        List<MatchSetSubSetCombinationInfo> _MatchSetSubSetCombinationInfoList = null;
        private List<MatchSetSubSetCombinationInfo> MatchSetSubSetCombinationInfoList
        {
            get
            {
                if (CurrentMatchSetHdrInfo != null)
                {
                    _MatchSetSubSetCombinationInfoList = CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection;
                }
                return _MatchSetSubSetCombinationInfoList;
            }
        }
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            MatchingWizardStepType = ARTEnums.MatchingWizardSteps.PreviewConfirm;
            WizardStepID = (int)ARTEnums.WizardSteps.PreviewConfirm;
        }
        #region Control Wizard Function 
        public override bool SaveData()
        {
           return UpdateMatchSetSubSetCombinationForConfigStatus(null);
        }
        public override void LoadData()
        {
            if (CurrentMatchSetHdrInfo != null)
            {
                uscMatchSetInfo.PopulateData(CurrentMatchSetHdrInfo);
                BindMatchingSourceGrid();
                BindConfigurationStatusGrid();
            }
        }
        public override bool SubmitData()
        {
            return UpdateMatchSetSubSetCombinationForConfigStatus(this.MatchSetID);
        }
        #endregion
        #region Load Matching Source 
        protected void BindMatchingSourceGrid()
        {
            if (MatchingSourceDataImportHdrInfoList != null)
            {
                rgMatchingSources.DataSource = MatchingSourceDataImportHdrInfoList;
                rgMatchingSources.DataBind();
            }
        }
        public void rgMatchingSources_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (MatchingSourceDataImportHdrInfoList != null)
                rgMatchingSources.DataSource = MatchingSourceDataImportHdrInfoList;
        }
        public void rgConfigurationStatus_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (MatchSetSubSetCombinationInfoList != null)
                rgConfigurationStatus.DataSource = MatchSetSubSetCombinationInfoList;
        }

        protected void rgMatchingSources_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item
                    || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = (MatchingSourceDataImportHdrInfo)(e.Item.DataItem);

                    ExLabel lblMatchingSourceName = (ExLabel)e.Item.FindControl("lblMatchingSourceName");
                    lblMatchingSourceName.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceName);

                    ExLabel lblMatchingSourceType = (ExLabel)e.Item.FindControl("lblMatchingSourceType");
                    if (oMatchingSourceDataImportHdrInfo.MatchingSourceTypeNameLabelID.HasValue)
                        lblMatchingSourceType.LabelID = oMatchingSourceDataImportHdrInfo.MatchingSourceTypeNameLabelID.Value;

                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this,ex);
            }
        }
        #endregion
        #region Load Configuration Status 
        protected void BindConfigurationStatusGrid()
        {
            if (MatchSetSubSetCombinationInfoList != null)
            {
                rgConfigurationStatus.DataSource = MatchSetSubSetCombinationInfoList;
                rgConfigurationStatus.DataBind();
                IsConfigurationComplete = (MatchSetSubSetCombinationInfoList.FindAll(C => C.IsConfigurationComplete == true).Count > 0);
            }
        }
        protected void rgConfigurationStatus_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item
                    || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = (MatchSetSubSetCombinationInfo)(e.Item.DataItem);

                    ExLabel lblDataSources = (ExLabel)e.Item.FindControl("lblDataSources");
                    ExLabel lblIsColMapping = (ExLabel)e.Item.FindControl("lblIsColMapping");
                    ExLabel lblIsMatchKey = (ExLabel)e.Item.FindControl("lblIsMatchKey");
                    ExLabel lblIsPartialMatchKey = (ExLabel)e.Item.FindControl("lblIsPartialMatchKey");
                    ExLabel lblIsDisplayCol = (ExLabel)e.Item.FindControl("lblIsDisplayCol");
                    ExLabel lblIsRecItemMapping = (ExLabel)e.Item.FindControl("lblIsRecItemMapping");
                    ExImage imgbtnTick = (ExImage)e.Item.FindControl("imgbtnTick");
                    ExImage imgbtnCross = (ExImage)e.Item.FindControl("imgbtnCross");
                    ExLabel lblIsAmountColumn = (ExLabel)e.Item.FindControl("lblIsAmountColumn");
                    
                    
                    //Set Default text of row Label
                    lblDataSources.Text = oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationName;
                    lblIsColMapping.LabelID = 1251;
                    lblIsMatchKey.LabelID = 1251;
                    lblIsPartialMatchKey.LabelID = 1251;
                    lblIsDisplayCol.LabelID = 1251;
                    lblIsRecItemMapping.LabelID = 1251;
                    imgbtnCross.Visible = true;
                    lblIsAmountColumn.LabelID = 1251;
                    //Set Default text of row

                    List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
                    if (oMatchingConfigurationInfoList != null && oMatchingConfigurationInfoList.Count > 0)
                    {
                        bool IsConfigurationComplete = true;
                        // Get count of column mapping
                        int ColumnMappingCount = oMatchingConfigurationInfoList.FindAll(p => p.MatchingSource1ColumnID != null && p.MatchingSource1ColumnID > 0 && p.MatchingSource2ColumnID != null && p.MatchingSource2ColumnID > 0).Count;
                        // Get count of Display column 
                        int DisplayColumnCount = oMatchingConfigurationInfoList.FindAll(p => p.MatchingSource1ColumnID != null && p.MatchingSource1ColumnID > 0 && p.MatchingSource2ColumnID != null && p.MatchingSource2ColumnID > 0 && p.DisplayColumnName != "").Count;

                        if (ColumnMappingCount > 1)
                        {
                            lblIsColMapping.LabelID = 1252;
                            if (ColumnMappingCount == DisplayColumnCount)
                                lblIsDisplayCol.LabelID = 1252;
                            else
                                IsConfigurationComplete = false;
                        }
                        else
                            IsConfigurationComplete = false;
                        //Checking Amount Column
                        if (oMatchingConfigurationInfoList.FindAll(p => p.IsAmountColumn.HasValue && p.IsAmountColumn == true).Count > 0)
                            lblIsAmountColumn.LabelID = 1252;

                        //Checking Match Key count
                        if (oMatchingConfigurationInfoList.FindAll(p => p.IsMatching == true).Count > 1)
                            lblIsMatchKey.LabelID = 1252;
                        else
                            IsConfigurationComplete = false;

                        //Checking Partial Match Key count
                        if (oMatchingConfigurationInfoList.FindAll(p => p.IsMatching == true && p.IsPartialMatching == true).Count > 0
                            && oMatchingConfigurationInfoList.FindAll(p => p.IsMatching == true).Count > oMatchingConfigurationInfoList.FindAll(p => p.IsMatching == true && p.IsPartialMatching == true).Count)
                            lblIsPartialMatchKey.LabelID = 1252;

                        //For AccoutnMatching
                        if (CurrentMatchingType != null && CurrentMatchingType == ARTEnums.MatchingType.AccountMatching)
                        {
                            //int RecItemMappingCount = oMatchingConfigurationInfoList.FindAll(p => p.MatchingSource1ColumnID != null && p.MatchingSource1ColumnID > 0 && p.MatchingSource2ColumnID != null && p.MatchingSource2ColumnID > 0 && p.DisplayColumnName != "" && p.RecItemColumnID != null && (p.RecItemColumnID == 1 || p.RecItemColumnID == 4)).Count;
                            //1 - Date && 4 - GL Balance
                            int RecItemMappingCount = oMatchingConfigurationInfoList.FindAll(p => p.DisplayColumnName != "" && p.RecItemColumnID != null && (p.RecItemColumnID == 1 || p.RecItemColumnID == 4)).Count;
                            if (RecItemMappingCount > 1)
                                lblIsRecItemMapping.LabelID = 1252;
                            else
                                IsConfigurationComplete = false;
                        }
                        //End 

                        //Showing Configuration Complete Button
                        if (IsConfigurationComplete)
                        {
                            imgbtnCross.Visible = false;
                            imgbtnTick.Visible = true;
                            if ((e.Item as GridDataItem)["IsConfigurationComplete"] != null)
                                (e.Item as GridDataItem)["IsConfigurationComplete"].Text = "true";
                        }
                        //End 

                        //Set Current counfig Status for Saving
                        oMatchSetSubSetCombinationInfo.IsConfigurationComplete = IsConfigurationComplete;

                    }
                   
                }

                //Hide RecItemMapping for Data Matching
                GridColumn oGridColumnIsRecItemMapping = rgConfigurationStatus.Columns.FindByUniqueNameSafe("IsRecItemMapping");
                if (oGridColumnIsRecItemMapping != null)
                {
                    if (CurrentMatchingType == null || CurrentMatchingType != ARTEnums.MatchingType.AccountMatching)
                        oGridColumnIsRecItemMapping.Visible = false;
                }
                //End
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        #endregion
        #region UpdateMatchSetSubSetCombinationForConfigStatus 
        protected bool UpdateMatchSetSubSetCombinationForConfigStatus(long? longMatchSetID)
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            oMatchingParamInfo.oMatchSetSubSetCombinationInfoList = MatchSetSubSetCombinationInfoList;
            oMatchingParamInfo.MatchSetID = longMatchSetID;
            oMatchingParamInfo.DateRevised = DateTime.Now;
            oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            return MatchingHelper.UpdateMatchSetSubSetCombinationForConfigStatus(oMatchingParamInfo);
        }
        #endregion

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