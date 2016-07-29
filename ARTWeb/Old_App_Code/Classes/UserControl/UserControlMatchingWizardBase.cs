using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;

namespace SkyStem.ART.Web.Classes.UserControl
{
    /// <summary>
    /// Summary description for UserControlMatchingWizardBase
    /// </summary>
    public class UserControlMatchingWizardBase : UserControlWizardBase
    {

        #region Properties

        long? _GLDataID = null;
        public long? GLDataID
        {
            get
            {
                if (_GLDataID == null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
                        _GLDataID = long.Parse(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                }
                return _GLDataID;
            }
            set
            {
                _GLDataID = value;
                // Recalculate FormMode when GLDataID changed
                eFromMode = null;
            }
        }

        long? _MatchSetID = null;
        public long? MatchSetID
        {
            get
            {
                if (_MatchSetID == null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_ID]))
                        _MatchSetID = long.Parse(Request.QueryString[QueryStringConstants.MATCH_SET_ID]);
                    //if _MatchSetID is null in querystring means Create Mode, so pick the MatchSetID from current session object
                    if (_MatchSetID == null)
                    {
                        if (CurrentMatchSetHdrInfo != null)
                            _MatchSetID = CurrentMatchSetHdrInfo.MatchSetID;
                    }
                }
                return _MatchSetID;
            }
            set { _MatchSetID = value; }
        }

        public ARTEnums.MatchingStatus MatchingStatus
        {
            get
            {
                if (CurrentMatchSetHdrInfo != null)
                    return (ARTEnums.MatchingStatus)CurrentMatchSetHdrInfo.MatchingStatusID;
                return ARTEnums.MatchingStatus.Draft;
            }
        }

        long? _AccountID = null;

        public long? AccountID
        {
            get
            {
                if (_AccountID == null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
                        _AccountID = long.Parse(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
                }
                return _AccountID;
            }
            set { _AccountID = value; }
        }

        ARTEnums.MatchingType? _CurrentMatchingType = null;

        public ARTEnums.MatchingType? CurrentMatchingType
        {
            get
            {
                if (_CurrentMatchingType == null)
                {
                    if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]))
                        _CurrentMatchingType = (ARTEnums.MatchingType?)Enum.Parse(typeof(ARTEnums.MatchingType), Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]);
                }
                return _CurrentMatchingType;
            }
            set { _CurrentMatchingType = value; }
        }

        public MatchSetHdrInfo CurrentMatchSetHdrInfo
        {
            get { return (MatchSetHdrInfo)Session[SessionConstants.MATCH_SET]; }
            set { Session[SessionConstants.MATCH_SET] = value; }
        }

        private ARTEnums.MatchingWizardSteps _MatchingWizardStepType;
        public ARTEnums.MatchingWizardSteps MatchingWizardStepType
        {
            get { return _MatchingWizardStepType; }
            set
            {
                _MatchingWizardStepType = value;
                TitlePhraseID = (int)_MatchingWizardStepType;
            }
        }

        WebEnums.FormMode? eFromMode = null;
        public override bool IsEditMode
        {
            get
            {
                if (eFromMode == null)
                {
                    WebEnums.ReconciliationStatus? eRecStatus = null;
                    if (GLDataID.HasValue)
                        eRecStatus = (WebEnums.ReconciliationStatus?)Helper.GetReconciliationStatusByGLDataID(GLDataID);
                    eFromMode = MatchingHelper.GetFormModeForMatching(WebEnums.ARTPages.MatchingWizard, CurrentMatchingType, eRecStatus, this.GLDataID, CurrentMatchSetHdrInfo);
                }
                if (eFromMode == WebEnums.FormMode.Edit)
                    return true;
                return false;
            }
        }

        bool _IsConfigurationComplete = false;
        public bool IsConfigurationComplete
        {
            get
            {
                return _IsConfigurationComplete;
            }
            set
            {
                _IsConfigurationComplete = value;
            }
        }

        #endregion

        public UserControlMatchingWizardBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}