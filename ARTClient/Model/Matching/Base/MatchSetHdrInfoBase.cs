

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{

    /// <summary>
    /// An object representation of the SkyStemART MatchSetHdr table
    /// </summary>
    [Serializable]
    public abstract class MatchSetHdrInfoBase
    {

        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Boolean? _IsActive = null;
        protected System.Int16? _MatchingStatusID = null;
        protected System.Int16? _MatchingTypeID = null;
        protected System.String _MatchSetDescription = null;
        protected System.Int64? _MatchSetID = null;
        protected System.String _MatchSetName = null;
        protected System.String _RevisedBy = null;
        protected System.Int64? _GLDataID = null;
        protected System.Int32? _RecPeriodID = null;
        protected System.Int32? _AddedByUserID = null;
    
        protected System.String _MatchSetRef = null;
        
        
        protected System.Int16? _RoleID = null;
        


        public bool IsAddedByNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsIsActiveNull = true;


        public bool IsMatchingStatusIDNull = true;


        public bool IsMatchingTypeIDNull = true;


        public bool IsMatchSetDescriptionNull = true;


        public bool IsMatchSetIDNull = true;
        

        public bool IsMatchSetNameNull = true;
        
        public bool IsGLDataIDNull = true;

        public bool IsRecPeriodIDNull = true;

        public bool IsRevisedByNull = true;

        public bool IsAddedByUserIDNull = true;

        public bool IsMatchSetRefNull = true;


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

                this.IsAddedByNull = (_AddedBy == null);
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

                this.IsDateAddedNull = (_DateAdded == DateTime.MinValue);
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

                this.IsDateRevisedNull = (_DateRevised == DateTime.MinValue);
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

                this.IsIsActiveNull = false;
            }
        }

        [XmlElement(ElementName = "MatchingStatusID")]
        public virtual System.Int16? MatchingStatusID
        {
            get
            {
                return this._MatchingStatusID;
            }
            set
            {
                this._MatchingStatusID = value;

                this.IsMatchingStatusIDNull = false;
            }
        }

        [XmlElement(ElementName = "MatchingTypeID")]
        public virtual System.Int16? MatchingTypeID
        {
            get
            {
                return this._MatchingTypeID;
            }
            set
            {
                this._MatchingTypeID = value;

                this.IsMatchingTypeIDNull = false;
            }
        }

        [XmlElement(ElementName = "MatchSetDescription")]
        public virtual System.String MatchSetDescription
        {
            get
            {
                return this._MatchSetDescription;
            }
            set
            {
                this._MatchSetDescription = value;

                this.IsMatchSetDescriptionNull = (_MatchSetDescription == null);
            }
        }

        [XmlElement(ElementName = "MatchSetID")]
        public virtual System.Int64? MatchSetID
        {
            get
            {
                return this._MatchSetID;
            }
            set
            {
                this._MatchSetID = value;

                this.IsMatchSetIDNull = false;
            }
        }

        [XmlElement(ElementName = "MatchSetName")]
        public virtual System.String MatchSetName
        {
            get
            {
                return this._MatchSetName;
            }
            set
            {
                this._MatchSetName = value;

                this.IsMatchSetNameNull = (_MatchSetName == null);
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

                this.IsRevisedByNull = (_RevisedBy == null);
            }
        }


        [XmlElement(ElementName = "GLDataID")]
        public virtual System.Int64? GLDataID
        {
            get
            {
                return this._GLDataID;
            }
            set
            {
                this._GLDataID = value;

                this.IsGLDataIDNull = false;
            }
        }

        [XmlElement(ElementName = "RecPeriodID")]
        public virtual System.Int32? RecPeriodID
        {
            get
            {
                return this._RecPeriodID;
            }
            set
            {
                this._RecPeriodID = value;

                this.IsRecPeriodIDNull = false;
            }
        }


        [XmlElement(ElementName = "AddedByUserID")]
        public virtual System.Int32? AddedByUserID
        {
            get
            {
                return this._AddedByUserID;
            }
            set
            {
                this._AddedByUserID = value;
                this.IsAddedByUserIDNull = (_AddedByUserID == null);
                
            }
        }
         

        [XmlElement(ElementName = "MatchSetRef")]
        public virtual System.String MatchSetRef
        {
            get
            {
                return this._MatchSetRef;
            }
            set
            {
                this._MatchSetRef = value;

                this.IsMatchSetRefNull = (_MatchSetRef == null);
            }
        }


        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
        {
            get
            {
                return this._RoleID;
            }
            set
            {
                this._RoleID = value;
                this.IsAddedByUserIDNull = (_RoleID == null);

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
