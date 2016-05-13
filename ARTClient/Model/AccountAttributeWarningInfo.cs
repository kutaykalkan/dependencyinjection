using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    public class AccountAttributeWarningInfo
    {
        protected System.Int64? _AccountID = 0;
        protected System.Int32? _NetAccountID = 0;
        bool? _FutureNetAccountWarning = null;
        bool? _LossOfWorkWarning = null;

        [XmlElement(ElementName = "FutureNetAccountWarning")]
        public bool? FutureNetAccountWarning
        {
            get { return _FutureNetAccountWarning; }
            set { _FutureNetAccountWarning = value; }
        }

        [XmlElement(ElementName = "LossOfWorkWarning")]
        public bool? LossOfWorkWarning
        {
            get { return _LossOfWorkWarning; }
            set { _LossOfWorkWarning = value; }
        }
        [XmlElement(ElementName = "AccountID")]
        public virtual System.Int64? AccountID
        {
            get
            {
                return this._AccountID;
            }
            set
            {
                this._AccountID = value;
            }
        }

        [XmlElement(ElementName = "NetAccountID")]
        public virtual System.Int32? NetAccountID
        {
            get
            {
                return this._NetAccountID;
            }
            set
            {
                this._NetAccountID = value;
            }
        }

    }
}
