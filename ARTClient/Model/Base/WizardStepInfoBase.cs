

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART WizardStep table
    /// </summary>
    [Serializable]
    public abstract class WizardStepInfoBase
    {
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Int32? _DisplayOrder = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _RevisedBy = null;
        protected System.Int32? _WizardStepID = null;
        protected System.String _WizardStepName = null;
        protected System.Int32? _WizardStepNameLabelID = null;
        protected System.String _WizardStepURL = null;
        protected System.Int16? _WizardTypeID = null;



        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this._AddedBy = value;

            }
        }

        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this._DateAdded = value;

            }
        }

        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime? DateRevised
        {
            get
            {
                return this._DateRevised;
            }
            set
            {
                this._DateRevised = value;

            }
        }

        [XmlElement(ElementName = "DisplayOrder")]
        public virtual System.Int32? DisplayOrder
        {
            get
            {
                return this._DisplayOrder;
            }
            set
            {
                this._DisplayOrder = value;

            }
        }

        [XmlElement(ElementName = "HostName")]
        public virtual System.String HostName
        {
            get
            {
                return this._HostName;
            }
            set
            {
                this._HostName = value;

            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

            }
        }

        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get
            {
                return this._RevisedBy;
            }
            set
            {
                this._RevisedBy = value;

            }
        }

        [XmlElement(ElementName = "WizardStepID")]
        public virtual System.Int32? WizardStepID
        {
            get
            {
                return this._WizardStepID;
            }
            set
            {
                this._WizardStepID = value;

            }
        }

        [XmlElement(ElementName = "WizardStepName")]
        public virtual System.String WizardStepName
        {
            get
            {
                return this._WizardStepName;
            }
            set
            {
                this._WizardStepName = value;

            }
        }

        [XmlElement(ElementName = "WizardStepNameLabelID")]
        public virtual System.Int32? WizardStepNameLabelID
        {
            get
            {
                return this._WizardStepNameLabelID;
            }
            set
            {
                this._WizardStepNameLabelID = value;

            }
        }

        [XmlElement(ElementName = "WizardStepURL")]
        public virtual System.String WizardStepURL
        {
            get
            {
                return this._WizardStepURL;
            }
            set
            {
                this._WizardStepURL = value;

            }
        }

        [XmlElement(ElementName = "WizardTypeID")]
        public virtual System.Int16? WizardTypeID
        {
            get
            {
                return this._WizardTypeID;
            }
            set
            {
                this._WizardTypeID = value;

            }
        }


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }
    }
}
