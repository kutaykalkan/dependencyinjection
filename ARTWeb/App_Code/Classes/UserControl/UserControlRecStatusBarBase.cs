using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.Web.Classes.UserControl
{

    /// <summary>
    /// Summary description for UserControlRecStatusBarBase
    /// </summary>
    public class UserControlRecStatusBarBase : System.Web.UI.UserControl
    {
        public UserControlRecStatusBarBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private long? _AccountID = null;
        private int? _NetAccountID = null;
        private long? _GLDataID = null;
        private bool _ShowRecStatus = true;
        private bool _ShowReconciledBalance = true;
        private bool _ShowUnexpVar = true;
        private bool _ShowDueDates = true;
        private bool _ShowExportButton = false;
        private bool _ShowQualityScore = false;
        private System.Int32? _PageTitleLabeID = null;

        public System.Int32? PageTitleLabeID
        {
            get { return _PageTitleLabeID; }
            set { _PageTitleLabeID = value; }
        }

        public bool ShowRecStatus
        {
            get { return _ShowRecStatus; }
            set { _ShowRecStatus = value; }
        }

        public bool ShowReconciledBalance
        {
            get { return _ShowReconciledBalance; }
            set { _ShowReconciledBalance = value; }
        }

        public bool ShowUnexpVar
        {
            get { return _ShowUnexpVar; }
            set { _ShowUnexpVar = value; }
        }

        public bool ShowDueDates
        {
            get { return _ShowDueDates; }
            set { _ShowDueDates = value; }
        }

        public long? GLDataID
        {
            get
            {
                if (ViewState["GLDataID"] != null)
                    _GLDataID = (long?)ViewState["GLDataID"];
                return _GLDataID;
            }
            set
            {
                _GLDataID = value;
                ViewState["GLDataID"] = value;
            }
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


        public bool ShowExportButton
        {
            get { return _ShowExportButton; }
            set { _ShowExportButton = value; }
        }

        public bool ShowQualityScore
        {
            get { return _ShowQualityScore; }
            set { _ShowQualityScore = value; }
        }
    }
}