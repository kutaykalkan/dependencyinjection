

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART WizardStepDependency table
    /// </summary>
    [Serializable]
    public abstract class WizardStepDependencyInfoBase
    {
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Int32? _DependentWizardStepID = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _RevisedBy = null;
        protected System.Int64? _WizardStepDependencyID = null;
        protected System.Int32? _WizardStepID = null;



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

        [XmlElement(ElementName = "DependentWizardStepID")]
        public virtual System.Int32? DependentWizardStepID
        {
            get
            {
                return this._DependentWizardStepID;
            }
            set
            {
                this._DependentWizardStepID = value;

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

        [XmlElement(ElementName = "WizardStepDependencyID")]
        public virtual System.Int64? WizardStepDependencyID
        {
            get
            {
                return this._WizardStepDependencyID;
            }
            set
            {
                this._WizardStepDependencyID = value;

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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }
}
