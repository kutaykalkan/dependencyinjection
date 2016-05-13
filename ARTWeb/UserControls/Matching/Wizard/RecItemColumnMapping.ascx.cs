using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;

namespace SkyStem.ART.Web.UserControls.Matching.Wizard
{
    public partial class RecItemColumnMapping : UserControlMatchingWizardBase
    {

        List<MatchSetSubSetCombinationInfo> _MatchSetSubSetCombinationInfoList = null;
        private List<MatchSetSubSetCombinationInfo> MatchSetSubSetCombinationInfoCollection
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
        
        private List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = null;

        protected void Page_Init(object sender, EventArgs e)
        {
            MatchingWizardStepType = ARTEnums.MatchingWizardSteps.RecItemColumnMapping;
            WizardStepID = (int)ARTEnums.WizardSteps.RecItemColumnMapping;
            ddlMatchingCombinationSelection.OnMatchingCombinationSelectionChanged += new MatchingCombinationSelection.MatchingCombinationSelectionChanged(ddlMatchingCombinationSelection_OnMatchingCombinationSelectionChanged);
        }

        void ddlMatchingCombinationSelection_OnMatchingCombinationSelectionChanged(MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo)
        {
            // clear and save Data for current combination
            //if (Page.IsValid)
            //{
                ClearAndUpdateSeesiondataByMatchSetSubSetCombinationID();
                hdnMatchSetSubSetCombinationID.Value = oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID.GetValueOrDefault().ToString();
            //}
            bindGridData();
            // load Data for changed combination
            hdnDuplicateMatchingConfigurationID.Value = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private List<MatchingConfigurationInfo> GetMatchingConfigurationInfoCollectionFromSession(long MatchSetSubSetCombinationID)
        {
            List<MatchingConfigurationInfo> SessionObjMatchingConfigurationInfoCollecton = new List<MatchingConfigurationInfo>();

            //MatchSetHdrInfo oMatchSetHdrInfo = SessionHelper.GetCurrentMatchSet();
            //List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection = oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection;

            MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = (from oMatchSetSubSetCombinationInf in MatchSetSubSetCombinationInfoCollection
                                                                            where oMatchSetSubSetCombinationInf.MatchSetSubSetCombinationID.Value == MatchSetSubSetCombinationID
                                                                            select oMatchSetSubSetCombinationInf).FirstOrDefault();
            if (oMatchSetSubSetCombinationInfo != null)
            {
                SessionObjMatchingConfigurationInfoCollecton = oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
            }
            return SessionObjMatchingConfigurationInfoCollecton;
        }

        private void ClearAndUpdateSeesiondataByMatchSetSubSetCombinationID()
        {
            List<MatchingConfigurationInfo> SessionObjMatchingConfigurationInfoCollecton = null;
            SessionObjMatchingConfigurationInfoCollecton = GetMatchingConfigurationInfoCollectionFromSession(Convert.ToInt64(hdnMatchSetSubSetCombinationID.Value));
            ClearSeesionData(SessionObjMatchingConfigurationInfoCollecton);
            SaveDataToSeesionObj(SessionObjMatchingConfigurationInfoCollecton);

        }
        private void ClearSeesionData(List<MatchingConfigurationInfo> SessionObjMatchingConfigurationInfoCollecton)
        {
            if (SessionObjMatchingConfigurationInfoCollecton != null)
            {
                for (int i = 0; i < SessionObjMatchingConfigurationInfoCollecton.Count - 1; i++)
                {
                    SessionObjMatchingConfigurationInfoCollecton[i].RecItemColumnID = null;
                }
            }
        }
        private void SaveDataToSeesionObj(List<MatchingConfigurationInfo> SessionObjMatchingConfigurationInfoCollecton)
        {
            MatchingConfigurationInfo oMatchingConfigurationInfo = null;
            GridItemCollection dataItemCollectionGLTBS = this.rgMappingColumns.MasterTableView.Items;

            foreach (GridDataItem Item in dataItemCollectionGLTBS)
            {

                int? RecItemColumnID = Convert.ToInt32(rgMappingColumns.MasterTableView.DataKeyValues[Item.ItemIndex]["RecItemColumnID"].ToString());

                DropDownList ddlUnMatchedItemColumn = (DropDownList)Item.FindControl("ddlUnMatchedItemColumn");
                if (ddlUnMatchedItemColumn.SelectedValue != "")
                {
                    long selectedMatchingConfigurationID = Convert.ToInt64(ddlUnMatchedItemColumn.SelectedValue);
                    if (selectedMatchingConfigurationID > 0)
                    {
                        oMatchingConfigurationInfo = (from obj in SessionObjMatchingConfigurationInfoCollecton
                                                      where obj.MatchingConfigurationID.Value == selectedMatchingConfigurationID
                                                      select obj).FirstOrDefault();
                        if (oMatchingConfigurationInfo.RecItemColumnID != RecItemColumnID)
                            this.IsDataChanged = true;
                        oMatchingConfigurationInfo.RecItemColumnID = RecItemColumnID;
                    }
                }
            }
        }

        public override void LoadData()
        {
            PageBase oPageBase = (PageBase)this.Page;
            Helper.ShowInputRequirementSection(oPageBase, 2347);

            if (CurrentMatchSetHdrInfo != null)
            {
                uscMatchSetInfo.PopulateData(CurrentMatchSetHdrInfo);
                ddlMatchingCombinationSelection.BindMatchingCombination(CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection);
                hdnMatchSetSubSetCombinationID.Value = ddlMatchingCombinationSelection.CurrentMatchSetSubSetCombinationID.GetValueOrDefault().ToString();
                hdnDuplicateMatchingConfigurationID.Value = "";
                bindGridData();
            }
        }

        public override void ClearData()
        {

            bool flag = false;
            //if (Page.IsValid)
            //{
                flag = CleanRecItemColumnMappingInDB();
           // }
            hdnDuplicateMatchingConfigurationID.Value = "";
            //return flag;

        }
        protected void rgMappingColumns_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    MatchingConfigurationInfo oMatchingConfigurationInfo = new MatchingConfigurationInfo();
                    RecItemColumnMstInfo oRecItemColumnMstInfo = (RecItemColumnMstInfo)e.Item.DataItem;
                    ExLabel lblRecItemColumnName = (ExLabel)e.Item.FindControl("lblRecItemColumnName");
                    lblRecItemColumnName.LabelID = oRecItemColumnMstInfo.RecItemColumnNameLabelID.Value;
                    DropDownList ddlUnMatchedItemColumn = (DropDownList)e.Item.FindControl("ddlUnMatchedItemColumn");
                    BindddlUnMatchedItemColumn(ddlUnMatchedItemColumn, oRecItemColumnMstInfo.DataTypeID.Value, Convert.ToInt64(hdnMatchSetSubSetCombinationID.Value));
                    oMatchingConfigurationInfo = (from obj in oMatchingConfigurationInfoCollection
                                                  where obj.RecItemColumnID == oRecItemColumnMstInfo.RecItemColumnID
                                                  select obj).FirstOrDefault();
                    
                    if (oMatchingConfigurationInfo != null)
                    {
                        ddlUnMatchedItemColumn.SelectedValue = oMatchingConfigurationInfo.MatchingConfigurationID.Value.ToString();
                    }
                    if (ddlUnMatchedItemColumn != null)
                        ddlUnMatchedItemColumn.Enabled = this.IsEditMode;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
        }


        private void BindddlUnMatchedItemColumn(DropDownList DDL, int DataTypeID, long MatchSetSubSetCombinationID)
        {
            // ToDo: Put a check Isdisplay Property when Is display property Work is done on privious step
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollectionbuDatatype = oMatchingConfigurationInfoCollection.FindAll(o => o.DataTypeID.Value == DataTypeID && o.DisplayColumnName != "").ToList();
            DDL.DataSource = oMatchingConfigurationInfoCollectionbuDatatype;
            DDL.DataTextField = "DisplayColumnName";
            DDL.DataValueField = "MatchingConfigurationID";
            DDL.DataBind();
            ListControlHelper.AddListItemForSelectOne(DDL);

            //DDL.Items.Add(new ListItem("Col1", "5"));
            //DDL.Items.Add(new ListItem("Col12", "8"));

        }
        protected void rgMappingColumns_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                bindGridData();
            }
            catch (Exception ex)
            {

                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }

        }
        protected void rgMappingColumns_SortCommand(object source, GridSortCommandEventArgs e)
        {
            GridHelper.HandleSortCommand(e);
            rgMappingColumns.Rebind();

        }

        private void bindGridData()
        {

            oMatchingConfigurationInfoCollection = GetMatchingConfigurationInfoCollectionFromSession(Convert.ToInt64(hdnMatchSetSubSetCombinationID.Value));
            List<RecItemColumnMstInfo> oRecItemColumnMstInfoCollection = null;
            if (ViewState["RecItemColumnMstInfoCollection"] == null)
            {

                oRecItemColumnMstInfoCollection = MatchingHelper.GetAllRecItemColumns();
                ViewState["RecItemColumnMstInfoCollection"] = oRecItemColumnMstInfoCollection;
            }
            else
            {
                oRecItemColumnMstInfoCollection = (List<RecItemColumnMstInfo>)ViewState["RecItemColumnMstInfoCollection"];
            }
            rgMappingColumns.DataSource = oRecItemColumnMstInfoCollection;
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = "RecItemColumnName";
            oGridSortExpression.SortOrder = GridSortOrder.Ascending;
            rgMappingColumns.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
            rgMappingColumns.DataBind();

        }
        public override bool SaveData()
        {
            bool flag = false;
            //if (Page.IsValid)
            //{

            flag = SaveRecItemColumnMappingInDB();
            //bindGridData();
            // flag = CleanRecItemColumnMappingInDB();
            //}
            //hdnDuplicateMatchingConfigurationID.Value = "";
            return true;
        }
        private bool SaveRecItemColumnMappingInDB()
        {
            bool IsSuccessFullySaved = false;

            List<MatchingConfigurationInfo> SessionObjMatchingConfigurationInfoCollecton = null;
            SessionObjMatchingConfigurationInfoCollecton = GetMatchingConfigurationInfoCollectionFromSession(Convert.ToInt64(hdnMatchSetSubSetCombinationID.Value));
            // clear and save Data for current combination
            ClearSeesionData(SessionObjMatchingConfigurationInfoCollecton);
            SaveDataToSeesionObj(SessionObjMatchingConfigurationInfoCollecton);
            // fill data to MatchingParamInfo object

            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            AddAllMatchingConfigurationInfoCollectonToMatchingParamInfo(oMatchingParamInfo, false);
            if (oMatchingParamInfo.oMatchingConfigurationInfoList != null && oMatchingParamInfo.oMatchingConfigurationInfoList.Count > 0)
            {
                oMatchingParamInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oMatchingParamInfo.DateAdded = System.DateTime.Now;
                oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oMatchingParamInfo.DateRevised = System.DateTime.Now;
                oMatchingParamInfo.IsActive = true;
                IsSuccessFullySaved = MatchingHelper.SaveRecItemColumnMapping(oMatchingParamInfo);
                return IsSuccessFullySaved;
            }
            return true;

        }

        private bool CleanRecItemColumnMappingInDB()
        {
            bool IsSuccessFullySaved = false;

            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            AddAllMatchingConfigurationInfoCollectonToMatchingParamInfo(oMatchingParamInfo, true);
            oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oMatchingParamInfo.DateRevised = System.DateTime.Now;
            IsSuccessFullySaved = MatchingHelper.CleanRecItemColumnMapping(oMatchingParamInfo);
            return IsSuccessFullySaved;

        }

        private void AddAllMatchingConfigurationInfoCollectonToMatchingParamInfo(MatchingParamInfo oMatchingParamInfo, bool IsClean)
        {
            List<MatchingConfigurationInfo> SessionObjMatchingConfigurationInfoCollecton = new List<MatchingConfigurationInfo>();
            MatchSetHdrInfo oMatchSetHdrInfo = SessionHelper.GetCurrentMatchSet();
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection = oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection;
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollecton = null;
            for (int i = 0; i < oMatchSetSubSetCombinationInfoCollection.Count; i++)
            {
                oMatchingConfigurationInfoCollecton = new List<MatchingConfigurationInfo>();
                oMatchingConfigurationInfoCollecton = oMatchSetSubSetCombinationInfoCollection[i].MatchingConfigurationInfoList.FindAll(p => p.MatchSetSubSetCombinationID == oMatchSetSubSetCombinationInfoCollection[i].MatchSetSubSetCombinationID).ToList();
                if (IsClean)
                    ClearSeesionData(oMatchingConfigurationInfoCollecton);
                if (oMatchingParamInfo.oMatchingConfigurationInfoList != null && oMatchingParamInfo.oMatchingConfigurationInfoList.Count > 0)
                {
                    oMatchingParamInfo.oMatchingConfigurationInfoList.AddRange(oMatchingConfigurationInfoCollecton);
                }
                else
                {
                    oMatchingParamInfo.oMatchingConfigurationInfoList = oMatchingConfigurationInfoCollecton;
                }
            }
        }
        private bool IsDuplicateMatchingConfigurationID(string rowMatchingConfigurationID)
        {
            bool flag = false;
            if (rowMatchingConfigurationID != WebConstants.SELECT_ONE)
            {
                if (hdnDuplicateMatchingConfigurationID.Value == "")
                {
                    hdnDuplicateMatchingConfigurationID.Value = rowMatchingConfigurationID;
                    flag = false;
                }
                else
                {
                    string[] arrMatchingConfigurationID = hdnDuplicateMatchingConfigurationID.Value.Split(',');
                    if (arrMatchingConfigurationID.Contains(rowMatchingConfigurationID))
                    {
                        flag = true;
                    }
                    else
                    {
                        hdnDuplicateMatchingConfigurationID.Value += "," + rowMatchingConfigurationID;
                        flag = false;
                    }


                }
            }


            return flag;

        }

        protected void cvDuplicateUnMatchedItemColumn_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                ExCustomValidator cv = (ExCustomValidator)source;
                //Telerik.Web.UI.GridDataItem gdi = (Telerik.Web.UI.GridDataItem)cv.NamingContainer;
                //DropDownList ddl = (DropDownList)gdi.FindControl(cv.ControlToValidate);
                //string rowMatchingConfigurationID = ddl.SelectedValue;


                MatchingConfigurationInfo oMatchingConfigurationInfo = new MatchingConfigurationInfo();
                GridItemCollection dataItemCollectionGLTBS = this.rgMappingColumns.MasterTableView.Items;

                foreach (GridDataItem Item in dataItemCollectionGLTBS)
                {

                    int? RecItemColumnID = Convert.ToInt32(rgMappingColumns.MasterTableView.DataKeyValues[Item.ItemIndex]["RecItemColumnID"].ToString());

                    DropDownList ddlUnMatchedItemColumn = (DropDownList)Item.FindControl("ddlUnMatchedItemColumn");


                    // Rec Column ID 1 For Date
                    if (RecItemColumnID.Value == 1 && ddlUnMatchedItemColumn.SelectedValue == WebConstants.SELECT_ONE)
                    {
                        cv.ErrorMessage = LanguageUtil.GetValue(5000274);
                        ddlMatchingCombinationSelection.CurrentMatchSetSubSetCombinationID = Convert.ToInt64(hdnMatchSetSubSetCombinationID.Value);
                        args.IsValid = false;
                    }



                    // RecColumn ID 4 for Amount L-CCY

                    if (RecItemColumnID.Value == 4 && ddlUnMatchedItemColumn.SelectedValue == WebConstants.SELECT_ONE)
                    {
                        cv.ErrorMessage = LanguageUtil.GetValue(5000275);
                        ddlMatchingCombinationSelection.CurrentMatchSetSubSetCombinationID = Convert.ToInt64(hdnMatchSetSubSetCombinationID.Value);
                        args.IsValid = false;
                    }




                    if (ddlUnMatchedItemColumn.SelectedValue != "")
                    {
                        if (IsDuplicateMatchingConfigurationID(ddlUnMatchedItemColumn.SelectedValue))
                        {
                            cv.ErrorMessage = LanguageUtil.GetValue(5000272);
                            ddlMatchingCombinationSelection.CurrentMatchSetSubSetCombinationID =Convert.ToInt64(hdnMatchSetSubSetCombinationID.Value);
                            args.IsValid = false;
                        }
                    }
                }
                if (args.IsValid == false)
                    bindGridData();
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