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
using System.Collections.Generic;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.UserControls.Matching
{
    public partial class MatchingCombinationSelection : UserControlBase
    {
        // Delegate to Selected Index Changed Event
        public delegate void MatchingCombinationSelectionChanged(MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo);
        public event MatchingCombinationSelectionChanged OnMatchingCombinationSelectionChanged;
        private bool _IsEnabledValidatePageData = true;
        #region Properties
        /// <summary>
        /// Current Match Set Sub Set Combination ID
        /// </summary>
        public long? CurrentMatchSetSubSetCombinationID
        {
            get
            {
                long matchSetSubSetCombinationID;
                if (long.TryParse(ddlMatchingCombination.SelectedValue.ToString(), out matchSetSubSetCombinationID))
                    return matchSetSubSetCombinationID;
                return null;
            }
            set
            {
                this.ddlMatchingCombination.SelectedValue = value.GetValueOrDefault().ToString();
            }
        }

        /// <summary>
        /// Previous Match Set Sub Set Combination ID
        /// </summary>
        public long? PreviousMatchSetSubSetCombinationID
        {
            get
            {
                long matchSetSubSetCombinationID;
                if (long.TryParse(hdnPreviousMatchSetSubSetCombinationID.Value, out matchSetSubSetCombinationID))
                    return matchSetSubSetCombinationID;
                return null;
            }
            set
            {
                if (value.HasValue)
                    this.hdnPreviousMatchSetSubSetCombinationID.Value = value.GetValueOrDefault().ToString();
            }
        }

        /// <summary>
        /// Current Selected Sub Set Combination
        /// </summary>
        public MatchSetSubSetCombinationInfo CurrentMatchSetSubSetCombinationInfo
        {
            get { return FindCombination(CurrentMatchSetSubSetCombinationID); }
        }

        /// <summary>
        /// Previous Selected Sub Set Combination
        /// </summary>
        public MatchSetSubSetCombinationInfo PreviousMatchSetSubSetCombinationInfo
        {
            get { return FindCombination(PreviousMatchSetSubSetCombinationID); }
        }

        /// <summary>
        /// Match Set Sub Set Combination List
        /// </summary>
        private List<MatchSetSubSetCombinationInfo> MatchSetSubSetCombinationInfoList
        {
            get { return (List<MatchSetSubSetCombinationInfo>)Session[SessionConstants.MATCHING_MATCH_SET_SUB_SET_COMBINATION_INFO_LIST]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_SUB_SET_COMBINATION_INFO_LIST] = value; }
        }

        public bool Enabled
        {
            get { return this.ddlMatchingCombination.Enabled; }
            set { this.ddlMatchingCombination.Enabled = value; }
        }

        public override string ClientID
        {
            get { return this.ddlMatchingCombination.ClientID; }
        }

        public bool ValidatorEnable
        {
            get { return this.vldMatchingCombination.Enabled; }
            set
            {
                if (Enabled)
                {
                    this.vldMatchingCombination.Enabled = value;
                }
            }
        }
        /// <summary>
        /// Property to enable disable ValidatePageData() on onchange event. Default is set to true.
        /// </summary>
        public bool EnabledValidatePageData
        {
            get { return _IsEnabledValidatePageData; }
            set { _IsEnabledValidatePageData = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EnabledValidatePageData)
                ddlMatchingCombination.Attributes.Add("onchange", "ValidatePageData()");
            //ddlMatchingCombination.Attributes.Add("onmouseover","this.title=this.options[this.selectedIndex].title");
        }
        public void BindTooltip(ListControl lc)
        {
            for (int i = 0; i < lc.Items.Count; i++)
            {
                lc.Items[i].Attributes.Add("title", lc.Items[i].Text);
            }
            lc.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
        }

        /// <summary>
        /// Bind Dropdown
        /// </summary>
        public void BindMatchingCombination(List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoList)
        {
            MatchSetSubSetCombinationInfoList = oMatchSetSubSetCombinationInfoList;
            ddlMatchingCombination.DataSource = MatchSetSubSetCombinationInfoList;
            ddlMatchingCombination.DataTextField = "MatchSetSubSetCombinationName";
            ddlMatchingCombination.DataValueField = "MatchSetSubSetCombinationID";
            ddlMatchingCombination.DataBind();

            if (SessionHelper.CurrentMatchSetSubSetCombinationInfo != null && oMatchSetSubSetCombinationInfoList != null)
            {
                MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = SessionHelper.CurrentMatchSetSubSetCombinationInfo;
                if (oMatchSetSubSetCombinationInfoList.Find(T => T.MatchSetSubSetCombinationID == oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID) != null)
                    CurrentMatchSetSubSetCombinationID = oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
            }

            if (CurrentMatchSetSubSetCombinationID.HasValue)
                ddlMatchingCombination.SelectedValue = CurrentMatchSetSubSetCombinationID.GetValueOrDefault().ToString();
            else
                ddlMatchingCombination.SelectedIndex = 0;

            SessionHelper.CurrentMatchSetSubSetCombinationInfo = CurrentMatchSetSubSetCombinationInfo;
        }

        /// <summary>
        /// Find Sub Set combination
        /// </summary>
        /// <param name="combinationID"></param>
        /// <returns></returns>
        private MatchSetSubSetCombinationInfo FindCombination(long? combinationID)
        {
            if (MatchSetSubSetCombinationInfoList != null && MatchSetSubSetCombinationInfoList.Count > 0)
                return MatchSetSubSetCombinationInfoList.Find(T => T.MatchSetSubSetCombinationID == combinationID);
            return null;
        }

        /// <summary>
        /// Save Previous Value
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            hdnPreviousMatchSetSubSetCombinationID.Value = ddlMatchingCombination.SelectedValue;
            BindTooltip(ddlMatchingCombination);
            base.OnPreRender(e);
        }

        /// <summary>
        /// Raise Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMatchingCombination_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionHelper.CurrentMatchSetSubSetCombinationInfo = CurrentMatchSetSubSetCombinationInfo;
            if (OnMatchingCombinationSelectionChanged != null)
                OnMatchingCombinationSelectionChanged.Invoke(CurrentMatchSetSubSetCombinationInfo);
        }
    }
}