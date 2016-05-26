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
using System.Globalization;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for MasterPageBase
    /// </summary>
    public class RecPeriodMasterPageBase : MasterPage
    {
        private long? _AccountID = null;
        private int? _NetAccountID = null;
        private long? _GLDataID = null;
        private System.Int32? _PageTitleLabeID = null;
        private WebEnums.RecStatusBarPageType _RecStatusBar = WebEnums.RecStatusBarPageType.RecForm;
        public long? GLDataID
        {
            get { return _GLDataID; }
            set { _GLDataID = value; }
        }

        public long? AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        public int? NetAccountID
        {
            get { return _NetAccountID; }
            set { _NetAccountID = value; }
        }

        public System.Int32? PageTitleLabeID
        {
            get { return _PageTitleLabeID; }
            set { _PageTitleLabeID = value; }
        }

        public WebEnums.RecStatusBarPageType RecStatusBar
        {
            get { return _RecStatusBar; }
            set { _RecStatusBar = value; }
        }

        public event EventHandler LocalRecPeriodChangedEventHandler;

        public RecPeriodMasterPageBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public void RaiseLocalRecPeriodChangedEvent(object sender, EventArgs e)
        {
            if (LocalRecPeriodChangedEventHandler != null)
            {
                LocalRecPeriodChangedEventHandler(sender, e);
            }
        }

        public virtual void SetMasterPageSettings(MasterPageSettings oMasterPageSettings)
        {
        }

    }//end of class
}//end of namespace