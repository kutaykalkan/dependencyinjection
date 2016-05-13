using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using System.Data;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls.Matching.Wizard
{
    public partial class MatchingConfiguration : UserControlMatchingWizardBase
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            MatchingWizardStepType = ARTEnums.MatchingWizardSteps.MatchingConfiguration;
            WizardStepID = (int)ARTEnums.WizardSteps.MatchingConfiguration;
            ddlMatchingCombinationSelection.OnMatchingCombinationSelectionChanged += new MatchingCombinationSelection.MatchingCombinationSelectionChanged(ddlMatchingCombinationSelection_OnMatchingCombinationSelectionChanged);
            rgMappingColumns.ItemCommand += new GridCommandEventHandler(rgMappingColumns_ItemCommand);
        }

        void ddlMatchingCombinationSelection_OnMatchingCombinationSelectionChanged(MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo)
        {
            UpdateSession();
            PopulateGrid();
        }

        protected void cvMinRowSelection_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            UpdateSession();
            e.IsValid = false;
            try
            {
                e.IsValid = ValidateGrid();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsEditMode)
            //    GridPnl.Enabled = false;

        }
        public override void RefreshData()
        {
            UpdateSession();
            BindGrid();
        }

        public override bool SaveData()
        {
            UpdateSession();
            SaveConfiguration();
            return false;
        }
        public override void LoadData()
        {
            PageBase oPageBase = (PageBase)this.Page;
            Helper.ShowInputRequirementSection(oPageBase, 2342, 2343, 2344);

            if (CurrentMatchSetHdrInfo != null)
            {
                uscMatchSetInfo.PopulateData(CurrentMatchSetHdrInfo);
                ddlMatchingCombinationSelection.BindMatchingCombination(CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection);
                PopulateGrid();
            }
        }
        public void LoadGridWithRule()
        {

        }
        public void BindGrid()
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = null;
            if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
                oMatchingConfigurationInfoCollection = (List<MatchingConfigurationInfo>)Session[SessionConstants.MATCHING_CONFIGURATION_DATA];
            rgMappingColumns.DataSource = oMatchingConfigurationInfoCollection;
            rgMappingColumns.DataBind();
        }

        public void PopulateGrid()
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
            oMatchingConfigurationInfoCollection = GetGridData();
            Session[SessionConstants.MATCHING_CONFIGURATION_DATA] = oMatchingConfigurationInfoCollection;
            BindGrid();
        }

        private bool ValidateGrid()
        {
            bool isValidKeySelected = true;
            int IsMatching = 0;
            int IsPartialMatching = 0;
            int ColumnCount = 0;
            long MatchingSource2ColumnID = 0;

            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
            if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
                oMatchingConfigurationInfoCollection = (List<MatchingConfigurationInfo>)Session[SessionConstants.MATCHING_CONFIGURATION_DATA];

            if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
            {
                foreach (MatchingConfigurationInfo oMatchingConfigurationInfo in oMatchingConfigurationInfoCollection)
                {
                    if (oMatchingConfigurationInfo.MatchingSource2ColumnID.HasValue)
                        MatchingSource2ColumnID = oMatchingConfigurationInfo.MatchingSource2ColumnID.Value;

                    //*** Count Repeted Column Mapping
                    foreach (MatchingConfigurationInfo oMatchingConfigurationInfo1 in oMatchingConfigurationInfoCollection)
                    {
                        if (oMatchingConfigurationInfo1.MatchingSource2ColumnID > 0 && oMatchingConfigurationInfo1.MatchingSource2ColumnID == MatchingSource2ColumnID)
                            ColumnCount++;
                    }
                    if (ColumnCount > 1)
                        break;
                    else
                        ColumnCount = 0;
                    //*** End Count Repeted Column Mapping

                    //*** Count PartialMatching and IsMatching
                    if (oMatchingConfigurationInfo.IsPartialMatching.Value)
                    {
                        //*** Checking PartialMatching must subset of IsMatching
                        if (!oMatchingConfigurationInfo.IsMatching.Value)
                        {
                            isValidKeySelected = false;
                            break;
                        }
                        //***  Checking PartialMatching must subset of IsMatching
                        IsPartialMatching++;
                    }
                    if (oMatchingConfigurationInfo.IsMatching.Value)
                        IsMatching++;
                    //*** End Count PartialMatching and IsMatching
                }
            }

            if (ColumnCount > 1)
            {
                string exceptionMessage = Helper.GetLabelIDValue(5000265);
                throw new Exception(exceptionMessage);
            }
            if (!isValidKeySelected)
            {
                string exceptionMessage = Helper.GetLabelIDValue(5000264);
                throw new Exception(exceptionMessage);
                //   return false;
            }
            if (IsPartialMatching > 0)
            {
                if (!(IsPartialMatching < IsMatching && IsPartialMatching > 1))
                {
                    string exceptionMessage = Helper.GetLabelIDValue(5000266);
                    throw new Exception(exceptionMessage);
                }
            }
            else
            {
                if (IsMatching < 2)
                {
                    string exceptionMessage = Helper.GetLabelIDValue(5000267);
                    throw new Exception(exceptionMessage);
                }
            }
            return true;
        }

        public List<MatchingConfigurationInfo> GetGridData()
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = null;

            MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = ddlMatchingCombinationSelection.CurrentMatchSetSubSetCombinationInfo;
            if (oMatchSetSubSetCombinationInfo != null)
                oMatchingConfigurationInfoList = oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;

            if (oMatchingConfigurationInfoList != null && oMatchingConfigurationInfoList.Count > 0)
            {
                oMatchingConfigurationInfoList = oMatchingConfigurationInfoList.FindAll(p => p.MatchingSource1ColumnID != null).ToList();
            }
            //** Get MatchingConfigurationInfo list from Session
            else
            {
                oMatchingConfigurationInfoList = CreateMatchingConfigurationInfo(oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID);
                oMatchSetSubSetCombinationInfo.IsConfigurationChange = true;
            }
            return oMatchingConfigurationInfoList;
        }

        /// <summary>
        /// Create New Matching Configuration for new new MatchSetSubSetCombinationID
        /// </summary>
        /// <param name="MatchSetSubSetCombinationID"></param>
        /// <returns></returns>
        private List<MatchingConfigurationInfo> CreateMatchingConfigurationInfo(long? MatchSetSubSetCombinationID)
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = new List<MatchingConfigurationInfo>();
            List<MatchingSourceColumnInfo> oMatchingSource1ColumnList = GetMatchingSourceColumnList(MatchSetSubSetCombinationID, false, null);
            List<MatchingSourceColumnInfo> oMatchingSource2ColumnList = GetMatchingSourceColumnList(MatchSetSubSetCombinationID, true, null);

            foreach (MatchingSourceColumnInfo oMatchingSourceColumnInfo in oMatchingSource1ColumnList)
            {
                MatchingConfigurationInfo MatchingConfigurationInfo = new MatchingConfigurationInfo();
                MatchingConfigurationInfo.MatchingConfigurationID = 0;
                MatchingConfigurationInfo.MatchingSource1ColumnID = oMatchingSourceColumnInfo.MatchingSourceColumnID;
                MatchingConfigurationInfo.MatchingSource2ColumnID = null;
                MatchingConfigurationInfo.DataTypeID = oMatchingSourceColumnInfo.DataTypeID;
                MatchingConfigurationInfo.ColumnName1 = oMatchingSourceColumnInfo.ColumnName;
                MatchingConfigurationInfo.ColumnName2 = "";
                MatchingConfigurationInfo.IsMatching = false;
                MatchingConfigurationInfo.IsPartialMatching = false;
                MatchingConfigurationInfo.IsDisplayedColumn = false;
                MatchingConfigurationInfo.MatchSetSubSetCombinationID = MatchSetSubSetCombinationID;
                oMatchingConfigurationInfoList.Add(MatchingConfigurationInfo);
            }
            return oMatchingConfigurationInfoList;
        }


        /// <summary>
        /// Get Column List
        /// </summary>
        /// <param name="IsSource2"></param>
        /// <param name="DataType"></param>
        /// <returns></returns>
        protected List<MatchingSourceColumnInfo> GetMatchingSourceColumnList(long? matchSetSubSetCombinationID, bool IsSource2, short? DataType)
        {
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoList = new List<MatchingSourceColumnInfo>();
            try
            {
                if (CurrentMatchSetHdrInfo != null && CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection != null)
                {
                    MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Find(T => T.MatchSetSubSetCombinationID == matchSetSubSetCombinationID);
                    if (oMatchSetSubSetCombinationInfo != null)
                    {
                        long? matchSetMatchingSourceDataImportID = null;
                        if (IsSource2)
                            matchSetMatchingSourceDataImportID = oMatchSetSubSetCombinationInfo.MatchSetMatchingSourceDataImport2ID;
                        else
                            matchSetMatchingSourceDataImportID = oMatchSetSubSetCombinationInfo.MatchSetMatchingSourceDataImport1ID;
                        //**** Get Column List by MatchSetMatchingSourceDataImportID
                        if (CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList != null && CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Count > 0)
                        {
                            foreach (MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo in CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList)
                            {
                                if (oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData.MatchSetMatchingSourceDataImportID == matchSetMatchingSourceDataImportID)
                                {
                                    if (DataType != null)
                                        oMatchingSourceColumnInfoList = (List<MatchingSourceColumnInfo>)Helper.DeepClone(oMatchingSourceDataImportHdrInfo.MatchingSourceColumnList.FindAll(r => r.DataTypeID == DataType).ToList());
                                    else
                                        oMatchingSourceColumnInfoList = (List<MatchingSourceColumnInfo>)Helper.DeepClone(oMatchingSourceDataImportHdrInfo.MatchingSourceColumnList);
                                    break;
                                }
                            }
                            //arryMandatoryFields = DataImportHelper.GetGLTBSDataLoadMandatoryFields();
                            ////*** Remove Account Column from Column List
                            //foreach (string strColName in arryMandatoryFields)
                            //{
                            //    MatchingSourceColumnInfo oColInfo = oMatchingSourceColumnInfoList.Find(p => p.ColumnName == strColName);
                            //    oMatchingSourceColumnInfoList.Remove(oColInfo);
                            //}
                            ////*** Remove Account Column from Column List
                        }
                        //***End 
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
            return oMatchingSourceColumnInfoList;
        }

        /// <summary>
        /// Update Data Into Session
        /// </summary>
        protected void UpdateSession()
        {
            try
            {
                long lngMatchSetSubSetCombinationID = 0;

                List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = new List<MatchingConfigurationInfo>();

                List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
                //*** get List from Grid data source
                if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
                    oMatchingConfigurationInfoCollection = (List<MatchingConfigurationInfo>)Session[SessionConstants.MATCHING_CONFIGURATION_DATA];

                if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
                {
                    //***For Compare List to check Change or not
                    oMatchingConfigurationInfoList = (List<MatchingConfigurationInfo>)Helper.DeepClone(oMatchingConfigurationInfoCollection);
                    //***For Compare List to check Change or not

                    foreach (GridDataItem item in rgMappingColumns.MasterTableView.Items)
                    {
                        long MatchingSource1ColumnID = (long)item.GetDataKeyValue("MatchingSource1ColumnID");
                        foreach (MatchingConfigurationInfo oMatchingConfiguration in oMatchingConfigurationInfoCollection)
                        {
                            if (oMatchingConfiguration.MatchingSource1ColumnID == MatchingSource1ColumnID)
                            {
                                DropDownList ddlSource2Column = (DropDownList)item.FindControl("ddlSource2Column");
                                ExCheckBox chkMatchKey = (ExCheckBox)item.FindControl("chkMatchKey");
                                ExCheckBox chkPartialMatchKey = (ExCheckBox)item.FindControl("chkPartialMatchKey");
                                ExCheckBox chkAmountColumn = (ExCheckBox)item.FindControl("chkAmountColumn");

                                ExLabel lblSource1ColumnName = (ExLabel)item.FindControl("lblSource1ColumnName");
                                //lblSource1ColumnName.Text = Helper.GetDisplayStringValue(oMatchingConfigurationInfo.ColumnName1);

                                long MatchingSource2ColumnID = 0;
                                long.TryParse(ddlSource2Column.SelectedValue.ToString(), out MatchingSource2ColumnID);
                                if (oMatchingConfiguration != null)
                                {
                                    if (MatchingSource2ColumnID > 0)
                                    {
                                        oMatchingConfiguration.MatchingSource2ColumnID = MatchingSource2ColumnID;
                                        oMatchingConfiguration.ColumnName1 = lblSource1ColumnName.Text;
                                        oMatchingConfiguration.ColumnName2 = ddlSource2Column.SelectedItem.Text;
                                        oMatchingConfiguration.IsMatching = chkMatchKey.Checked;
                                        oMatchingConfiguration.IsPartialMatching = chkPartialMatchKey.Checked;
                                        if (chkAmountColumn != null)
                                            oMatchingConfiguration.IsAmountColumn = chkAmountColumn.Checked;

                                    }
                                    else
                                    {
                                        oMatchingConfiguration.MatchingSource2ColumnID = null;
                                        oMatchingConfiguration.ColumnName1 = lblSource1ColumnName.Text;
                                        oMatchingConfiguration.ColumnName2 = "";
                                        oMatchingConfiguration.IsMatching = false;
                                        oMatchingConfiguration.IsPartialMatching = false;
                                        oMatchingConfiguration.IsAmountColumn = false;
                                    }
                                }
                            }
                        }
                    }
                    lngMatchSetSubSetCombinationID = oMatchingConfigurationInfoCollection[0].MatchSetSubSetCombinationID.Value;

                    MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Find(p => p.MatchSetSubSetCombinationID == lngMatchSetSubSetCombinationID);
                    if (IsListChanged(oMatchingConfigurationInfoList, oMatchingConfigurationInfoCollection))
                        oMatchSetSubSetCombinationInfo.IsConfigurationChange = true;

                    if (oMatchSetSubSetCombinationInfo.IsConfigurationChange)
                        oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList = oMatchingConfigurationInfoCollection;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        /// <summary>
        /// Detect changes
        /// </summary>
        /// <param name="oMatchingConfigurationInfoList1"></param>
        /// <param name="oMatchingConfigurationInfoList2"></param>
        /// <returns></returns>
        private bool IsListChanged(List<MatchingConfigurationInfo> oMatchingConfigurationInfoList1, List<MatchingConfigurationInfo> oMatchingConfigurationInfoList2)
        {
            if (oMatchingConfigurationInfoList1 == null && oMatchingConfigurationInfoList2 == null)
                return false;
            if (oMatchingConfigurationInfoList1 == null || oMatchingConfigurationInfoList2 == null)
                return true;

            foreach (MatchingConfigurationInfo oItem in oMatchingConfigurationInfoList1)
            {
                MatchingConfigurationInfo oItem2 = oMatchingConfigurationInfoList2.Find(C => C.MatchingConfigurationID == oItem.MatchingConfigurationID);
                if (oItem.MatchingSource2ColumnID != oItem2.MatchingSource2ColumnID)
                    return true;
                if (oItem.IsMatching != oItem2.IsMatching)
                    return true;
                if (oItem.IsPartialMatching != oItem2.IsPartialMatching)
                    return true;
                if (oItem.IsAmountColumn != oItem2.IsAmountColumn)
                    return true;
            }
            return false;
        }


        protected void rgMappingColumns_ItemCommand(object source, GridCommandEventArgs e)
        {
            CommandEventArgs cmd = e as CommandEventArgs;
            string[] values = null;
            long mcID, ruleID, mcrID;
            if (cmd.CommandName == "DeleteRule")
            {
                values = cmd.CommandArgument.ToString().Split('|');
                mcID = Convert.ToInt64(values[0]);
                ruleID = Convert.ToInt64(values[1]);
                mcrID = Convert.ToInt64(values[2]);

                List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = null;
                if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
                    oMatchingConfigurationInfoCollection = (List<MatchingConfigurationInfo>)Session[SessionConstants.MATCHING_CONFIGURATION_DATA];

                if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
                {
                    MatchingConfigurationInfo oMatchingConfigurationInfo = null;
                    if (mcrID != 0)
                        oMatchingConfigurationInfo = oMatchingConfigurationInfoCollection.Find(p => p.MatchingConfigurationID == mcID);
                    else
                        oMatchingConfigurationInfo = oMatchingConfigurationInfoCollection.Find(p => p.MatchingSource1ColumnID == mcID);
                    if (oMatchingConfigurationInfo != null)
                    {

                        MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfo = null;
                        foreach (MatchingConfigurationRuleInfo info in oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection)
                        {
                            if (ruleID == 0)
                            {
                                if (info.MatchingConfigurationRuleID == mcrID)
                                {
                                    oMatchingConfigurationRuleInfo = info;
                                    break;
                                }
                            }
                            else
                            {
                                if (info.RuleID == ruleID)
                                {
                                    oMatchingConfigurationRuleInfo = info;
                                    break;
                                }
                            }
                        }

                        if (oMatchingConfigurationRuleInfo != null)
                        {
                            oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Remove(oMatchingConfigurationRuleInfo);
                        }
                    }
                    //oMatchingConfigurationInfo.MatchingSource1ColumnID.Value
                }
                BindGrid();
            }
        }
        protected void rgMappingColumns_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    MatchingConfigurationInfo oMatchingConfigurationInfo = (MatchingConfigurationInfo)(e.Item.DataItem);

                    ExLabel lblSource1ColumnName = (ExLabel)e.Item.FindControl("lblSource1ColumnName");
                    lblSource1ColumnName.Text = Helper.GetDisplayStringValue(oMatchingConfigurationInfo.ColumnName1);

                    DropDownList ddlSource2Column = (DropDownList)e.Item.FindControl("ddlSource2Column");
                    ExCheckBox chkMatchKey = (ExCheckBox)e.Item.FindControl("chkMatchKey");
                    ExCheckBox chkPartialMatchKey = (ExCheckBox)e.Item.FindControl("chkPartialMatchKey");
                    ExCheckBox chkAmountColumn = (ExCheckBox)e.Item.FindControl("chkAmountColumn");
                    if (oMatchingConfigurationInfo.MatchingSource1ColumnID.HasValue)
                    {
                        if (oMatchingConfigurationInfo.DataTypeID == (short)WebEnums.DataType.Decimal || oMatchingConfigurationInfo.DataTypeID == (short)WebEnums.DataType.Integer)
                        {
                            chkAmountColumn.Visible = true;
                            if (oMatchingConfigurationInfo.IsAmountColumn.HasValue)
                                chkAmountColumn.Checked = Convert.ToBoolean(oMatchingConfigurationInfo.IsAmountColumn);
                            if (this.IsEditMode)
                            {
                                if (oMatchingConfigurationInfo.MatchingSource2ColumnID.HasValue)
                                    chkAmountColumn.Enabled = true;
                                else
                                    chkAmountColumn.Enabled = false;
                            }
                            else
                                chkAmountColumn.Enabled = false;

                        }
                        else
                            chkAmountColumn.Visible = false;
                    }
                    else
                        chkAmountColumn.Visible = false;


                    if (ddlSource2Column != null)
                    {
                        List<MatchingSourceColumnInfo> oMatchingSource2ColumnInfo = new List<MatchingSourceColumnInfo>();
                        oMatchingSource2ColumnInfo = GetMatchingSourceColumnList(oMatchingConfigurationInfo.MatchSetSubSetCombinationID, true, oMatchingConfigurationInfo.DataTypeID);
                        ddlSource2Column.DataSource = oMatchingSource2ColumnInfo;
                        ddlSource2Column.DataTextField = "ColumnName";
                        ddlSource2Column.DataValueField = "MatchingSourceColumnID";
                        ddlSource2Column.DataBind();
                        ListControlHelper.AddListItemForSelectOne(ddlSource2Column);
                        if (oMatchingConfigurationInfo.MatchingSource2ColumnID != null && oMatchingConfigurationInfo.MatchingSource2ColumnID.Value > 0)
                            ddlSource2Column.SelectedValue = Helper.GetDisplayStringValue(oMatchingConfigurationInfo.MatchingSource2ColumnID.Value.ToString());

                        ddlSource2Column.Enabled = this.IsEditMode;
                        ddlSource2Column.Attributes.Add("onchange", "javascript:ddlSource2Change(this,'" + chkMatchKey.ClientID + "', '" + chkPartialMatchKey.ClientID + "', '" + chkAmountColumn.ClientID + "');");
                    }

                    if (chkMatchKey != null)
                    {
                        chkMatchKey.Checked = oMatchingConfigurationInfo.IsMatching.Value;
                        chkMatchKey.Attributes.Add("onclick", "javascript:MatchKeyCheckedChange(this,'" + ddlSource2Column.ClientID + "', '" + chkPartialMatchKey.ClientID + "', '" + chkAmountColumn.ClientID + "');");
                        if (ddlSource2Column.SelectedIndex == 0 || !this.IsEditMode)
                            chkMatchKey.Enabled = false;
                    }

                    if (chkPartialMatchKey != null)
                    {
                        chkPartialMatchKey.Checked = oMatchingConfigurationInfo.IsPartialMatching.Value;
                        //chkPartialMatchKey.Attributes.Add("onclick", "PartialMatchKeyCheckedChange(this,'" + chkMatchKey.ClientID + "');");
                        if (!chkMatchKey.Checked || !this.IsEditMode)
                            chkPartialMatchKey.Enabled = false;
                    }

                    ExHyperLink lnkAddRule = (ExHyperLink)e.Item.FindControl("lnkAddRule");

                    if (lnkAddRule != null)
                    {
                        if (oMatchingConfigurationInfo.MatchingSource1ColumnID.Value != 0)
                            lnkAddRule.NavigateUrl = "javascript:LoadRuleSetupPopup(" + oMatchingConfigurationInfo.MatchingSource1ColumnID + ",'" + ddlSource2Column.ClientID + "'," + oMatchingConfigurationInfo.MatchingSource1ColumnID + ");";
                        else
                        {
                            lnkAddRule.Visible = false;
                        }
                    }

                    lnkAddRule.Visible = this.IsEditMode;

                    RuleDisplayControl oRuleDisplayControl = (RuleDisplayControl)e.Item.FindControl("RuleDisplayControl");
                    oRuleDisplayControl.IsEditMode = this.IsEditMode;
                    oRuleDisplayControl.PopulateRuleTable(oMatchingConfigurationInfo.MatchingSource1ColumnID.Value);

                    if (oMatchingConfigurationInfo != null && (oMatchingConfigurationInfo.DataTypeID == Convert.ToInt64(WebEnums.DataType.Integer)
                        || (oMatchingConfigurationInfo.DataTypeID == Convert.ToInt64(WebEnums.DataType.DataTime)
                        || oMatchingConfigurationInfo.DataTypeID == Convert.ToInt64(WebEnums.DataType.Decimal))))

                        if (oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection != null && oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Count > 0)
                        {
                            lnkAddRule.Visible = false;
                            ddlSource2Column.Enabled = false;
                        }
                        else
                        {
                            lnkAddRule.Visible = this.IsEditMode;
                            ddlSource2Column.Enabled = this.IsEditMode;
                        }
                    //oRuleDisplayControl.DeleteRule += new RuleDisplayControl.DeleteRuleByRuleID(oRuleDisplayControl_DeleteRule);
                    //oRuleDisplayControl.DeleteRule += new RuleDisplayControl.DeleteRuleByRuleID(oRuleDisplayControl_DeleteRule);
                    //oRuleDisplayControl.DeleteRuleCommand += new CommandEventHandler(oRuleDisplayControl_DeleteRuleCommand);
                    if (oMatchingConfigurationInfo.MatchingSource1ColumnID.Value == 0)
                    {
                        ddlSource2Column.Enabled = false;
                        chkMatchKey.Enabled = false;
                        chkPartialMatchKey.Enabled = false;
                        lnkAddRule.Enabled = false;
                    }
                    if (ddlSource2Column.Items.Count <= 0)
                    {
                        ddlSource2Column.Enabled = false;
                        chkMatchKey.Enabled = false;
                        chkPartialMatchKey.Enabled = false;
                        lnkAddRule.Enabled = false;
                    }

                }
            }

            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        void oRuleDisplayControl_DeleteRuleCommand(object sender, CommandEventArgs e)
        {

        }

        void oRuleDisplayControl_DeleteRule(long mcID, long ruleID, long mcrID, long _matchingSource1ColumnID)
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = null;
            if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
                oMatchingConfigurationInfoCollection = (List<MatchingConfigurationInfo>)Session[SessionConstants.MATCHING_CONFIGURATION_DATA];

            if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
            {
                MatchingConfigurationInfo oMatchingConfigurationInfo = null;
                if (mcID != 0)
                    oMatchingConfigurationInfo = oMatchingConfigurationInfoCollection.Find(p => p.MatchingConfigurationID == mcID);
                else
                    oMatchingConfigurationInfo = oMatchingConfigurationInfoCollection.Find(p => p.MatchingConfigurationID == _matchingSource1ColumnID);
                if (oMatchingConfigurationInfo != null)
                {
                    MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfo = null;
                    foreach (MatchingConfigurationRuleInfo info in oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection)
                    {
                        if (ruleID == 0)
                        {
                            if (info.MatchingConfigurationRuleID == mcrID)
                            {
                                oMatchingConfigurationRuleInfo = info;
                                break;
                            }
                        }
                        else
                        {
                            if (info.RuleID == ruleID)
                            {
                                oMatchingConfigurationRuleInfo = info;
                                break;
                            }
                        }
                    }

                    if (oMatchingConfigurationRuleInfo != null)
                    {
                        oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Remove(oMatchingConfigurationRuleInfo);
                    }
                }
                //oMatchingConfigurationInfo.MatchingSource1ColumnID.Value
            }
            BindGrid();
        }


        public void rgMappingColumns_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        public bool SaveConfiguration()
        {
            try
            {
                if (CurrentMatchSetHdrInfo != null)
                {
                    //*** Get MatchingConfigurationInfo List From Session data
                    List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
                    MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                    List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection = CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection;
                    if (oMatchSetSubSetCombinationInfoCollection != null && oMatchSetSubSetCombinationInfoCollection.Count > 0)
                    {
                        foreach (MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo in oMatchSetSubSetCombinationInfoCollection)
                        {
                            if (oMatchSetSubSetCombinationInfo.IsConfigurationChange)
                            {
                                if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
                                {
                                    if (oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList != null && oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList.Count > 0)
                                        oMatchingConfigurationInfoCollection.AddRange(oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList);
                                }
                                else
                                    oMatchingConfigurationInfoCollection = oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;

                                if (oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList != null && oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList.Count > 0)
                                {
                                    List<MatchingSourceColumnInfo> oMatchingSource2ColumnInfo = new List<MatchingSourceColumnInfo>();
                                    oMatchingSource2ColumnInfo = GetMatchingSourceColumnList(oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID.Value, true, null);
                                    if (oMatchingSource2ColumnInfo != null && oMatchingSource2ColumnInfo.Count > 0)
                                    {
                                        foreach (MatchingSourceColumnInfo oMatchingSourceColumnInfo in oMatchingSource2ColumnInfo)
                                        {
                                            if (!oMatchingConfigurationInfoCollection.Exists(p => p.MatchingSource2ColumnID == oMatchingSourceColumnInfo.MatchingSourceColumnID))
                                            {
                                                MatchingConfigurationInfo MatchingConfigurationInfo = new MatchingConfigurationInfo();
                                                MatchingConfigurationInfo.MatchingConfigurationID = 0;
                                                MatchingConfigurationInfo.MatchingSource2ColumnID = oMatchingSourceColumnInfo.MatchingSourceColumnID;
                                                MatchingConfigurationInfo.ColumnName2 = oMatchingSourceColumnInfo.ColumnName;
                                                MatchingConfigurationInfo.DataTypeID = oMatchingSourceColumnInfo.DataTypeID;
                                                MatchingConfigurationInfo.IsMatching = false;
                                                MatchingConfigurationInfo.IsDisplayedColumn = false;
                                                MatchingConfigurationInfo.IsPartialMatching = false;
                                                MatchingConfigurationInfo.MatchSetSubSetCombinationID = oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
                                                oMatchingConfigurationInfoCollection.Add(MatchingConfigurationInfo);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (oMatchingConfigurationInfoCollection != null && oMatchingConfigurationInfoCollection.Count > 0)
                    {
                        oMatchingParamInfo.oMatchingConfigurationInfoList = oMatchingConfigurationInfoCollection;
                        oMatchingParamInfo.IsActive = true;
                        oMatchingParamInfo.DateAdded = DateTime.Now;
                        oMatchingParamInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                        oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                        oMatchingParamInfo.DateRevised = DateTime.Now;
                        MatchingHelper.SaveMatchingConfiguration(oMatchingParamInfo);
                        ReloadMatchingConfigurationData();
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
            return true;
        }

        private void ReloadMatchingConfigurationData()
        {
            if (CurrentMatchSetHdrInfo != null)
            {
                CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection = MatchingHelper.GetAllMatchSetSubSetCombination(CurrentMatchSetHdrInfo.MatchSetID, GLDataID, SessionHelper.CurrentReconciliationPeriodID, null);
            }
        }

        public override void SetControlStatePostLoad()
        {
            base.SetControlStatePostLoad();
            //GridColumn oGridColumnSource2Column = rgMappingColumns.MasterTableView.Columns.FindByUniqueNameSafe("Source2Column");
            //GridColumn oGridColumnMatchKey = rgMappingColumns.MasterTableView.Columns.FindByUniqueNameSafe("MatchKey");
            //GridColumn oGridColumnPartialMatchKey = rgMappingColumns.MasterTableView.Columns.FindByUniqueNameSafe("PartialMatchKey");

            ////DropDownList ddlSource2Column = (DropDownList)item.FindControl("MatchKey");
            ////ExCheckBox chkMatchKey = (ExCheckBox)item.FindControl("chkMatchKey");
            ////ExCheckBox chkPartialMatchKey = (ExCheckBox)item.FindControl("chkPartialMatchKey");
            //if (oGridColumnSource2Column != null)
            //    oGridColumnSource2Column.Dis = this.IsEditMode;

            //if (oGridColumnMatchKey != null)
            //    oGridColumnMatchKey.Display = this.IsEditMode;

            //if (oGridColumnPartialMatchKey != null)
            //    oGridColumnPartialMatchKey.Display = this.IsEditMode;


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
