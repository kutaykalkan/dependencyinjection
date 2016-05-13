

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchSetSubSetCombination table
	/// </summary>
    [Serializable]
    public abstract class MatchSetSubSetCombinationInfoBase
    {

        protected System.String _MatchSetSubSetCombinationName = null;
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Boolean? _IsActive = null;
        protected System.Int64? _MatchSetMatchingSourceDataImport1ID = null;
        protected System.Int64? _MatchSetMatchingSourceDataImport2ID = null;
        protected System.Int64? _MatchSetSubSetCombinationID = null;
        protected System.String _RevisedBy = null;
        protected System.Boolean? _IsConfigurationComplete = null;

        

        [XmlElement(ElementName = "MatchSetSubSetCombinationName")]
        public virtual System.String MatchSetSubSetCombinationName
        {
            get
            {
                return this._MatchSetSubSetCombinationName;
            }
            set
            {
                this._MatchSetSubSetCombinationName = value;

            }
        }


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

        [XmlElement(ElementName = "MatchSetMatchingSourceDataImport1ID")]
        public virtual System.Int64? MatchSetMatchingSourceDataImport1ID
        {
            get
            {
                return this._MatchSetMatchingSourceDataImport1ID;
            }
            set
            {
                this._MatchSetMatchingSourceDataImport1ID = value;

            }
        }

        [XmlElement(ElementName = "MatchSetMatchingSourceDataImport2ID")]
        public virtual System.Int64? MatchSetMatchingSourceDataImport2ID
        {
            get
            {
                return this._MatchSetMatchingSourceDataImport2ID;
            }
            set
            {
                this._MatchSetMatchingSourceDataImport2ID = value;

            }
        }

        [XmlElement(ElementName = "MatchSetSubSetCombinationID")]
        public virtual System.Int64? MatchSetSubSetCombinationID
        {
            get
            {
                return this._MatchSetSubSetCombinationID;
            }
            set
            {
                this._MatchSetSubSetCombinationID = value;

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

        [XmlElement(ElementName = "IsConfigurationComplete")]
        public virtual System.Boolean? IsConfigurationComplete
        {
            get
            {
                return this._IsConfigurationComplete;
            }
            set
            {
                this._IsConfigurationComplete = value;

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
