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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Base class for all User Controls
    /// </summary>
    public class UserControlBase : System.Web.UI.UserControl
    {
        private bool _IsPrintMode = false;

        public bool IsPrintMode
        {
            get { return _IsPrintMode; }
            set { _IsPrintMode = value; }
        }

        private int? _WizardStepID = null;

        public int? WizardStepID
        {
            get { return _WizardStepID; }
            set { _WizardStepID = value; }
        }
        private int _EditMode;

        public WebEnums.FormMode EditMode
        {
            get
            {
                return (WebEnums.FormMode)_EditMode;
            }
            set
            {
                _EditMode = (int)value;
            }
        }
        public UserControlBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public virtual void SetSignature(string userName, DateTime? dtSignOff)
        {
        }

        //public virtual List<RiskRatingReconciliationPeriodInfo> GetSelectedRecPeriods()
        //{
        //    return null;
        //}

        public virtual bool Collapsed
        {
            set{ }
        }

        WebEnums.RecPeriodStatus? _CurrentRecProcessStatus = null;
        public WebEnums.RecPeriodStatus? CurrentRecProcessStatus
        {
            get
            {
                if (!_CurrentRecProcessStatus.HasValue)
                    _CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
                return _CurrentRecProcessStatus;
            }
            set
            {
                // Save to View State
                _CurrentRecProcessStatus = value;
            }
        }
    }
}