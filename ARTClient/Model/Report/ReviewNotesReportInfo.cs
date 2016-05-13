using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Data;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Report
{
    [Serializable]
    public class ReviewNotesReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;
        protected System.Int32? _FSCaptionID = null;

        protected System.DateTime? _ReviewNoteDate = null;
        protected System.String _Subject = "";
        protected System.String _EnterBy = "";
        protected System.String _ReviewNote = "";
        protected System.DateTime? _Period = null;
        protected System.String _Perparer = "";
        protected System.String _Reviewer = "";
        protected UserHdrInfo _AddedByUserInfo = null;
        protected System.Int16? _AddedByUserRole = null;

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

        [XmlElement(ElementName = "AccountName")]
        public virtual System.String AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
            }
        }

        [XmlElement(ElementName = "AccountNameLabelID")]
        public virtual System.Int32? AccountNameLabelID
        {
            get
            {
                return this._AccountNameLabelID;
            }
            set
            {
                this._AccountNameLabelID = value;
            }
        }


        [XmlElement(ElementName = "AccountNumber")]
        public virtual System.String AccountNumber
        {
            get
            {
                return this._AccountNumber;
            }
            set
            {
                this._AccountNumber = value;
            }
        }


        [XmlElement(ElementName = "AccountTypeID")]
        public virtual System.Int16? AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                this._AccountTypeID = value;
            }
        }

        [XmlElement(ElementName = "AddedByUserRole")]
        public virtual System.Int16? AddedByUserRole
        {
            get
            {
                return this._AddedByUserRole;
            }
            set
            {
                this._AddedByUserRole = value;
            }
        }

        [XmlElement(ElementName = "FSCaptionID")]
        public virtual System.Int32? FSCaptionID
        {
            get
            {
                return this._FSCaptionID;
            }
            set
            {
                this._FSCaptionID = value;
            }
        }

        [XmlElement(ElementName = "Period")]
        public virtual System.DateTime? Period
        {
            get
            {
                return this._Period;
            }
            set
            {
                this._Period = value;
            }
        }

        [XmlElement(ElementName = "Perparer")]
        public virtual System.String Perparer
        {
            get
            {
                return this._Perparer;
            }
            set
            {
                this._Perparer = value;
            }
        }
        [XmlElement(ElementName = "Reviewer")]
        public virtual System.String Reviewer
        {
            get
            {
                return this._Reviewer;
            }
            set
            {
                this._Reviewer = value;
            }
        }

        [XmlElement(ElementName = "Subject")]
        public virtual System.String Subject
        {
            get
            {
                return this._Subject;
            }
            set
            {
                this._Subject = value;
            }
        }

        [XmlElement(ElementName = "EnterBy")]
        public virtual System.String EnterBy
        {
            get
            {
                return this._EnterBy;
            }
            set
            {
                this._EnterBy = value;
            }
        }

        [XmlElement(ElementName = "ReviewNote")]
        public virtual System.String ReviewNote
        {
            get
            {
                return this._ReviewNote;
            }
            set
            {
                this._ReviewNote = value;
            }
        }

        [XmlElement(ElementName = "ReviewNoteDate")]
        public virtual System.DateTime? ReviewNoteDate
        {
            get
            {
                return this._ReviewNoteDate;
            }
            set
            {
                this._ReviewNoteDate = value;
            }
        }

        [XmlElement(ElementName = "AddedByFullName")]
        public String AddedByFullName
        {
            get
            {
                return ModelHelper.GetFullName(this.AddedByUserInfo.FirstName, this.AddedByUserInfo.LastName);
            }
        }

        [XmlElement(ElementName = "AddedByUserInfo")]
        public UserHdrInfo AddedByUserInfo
        {
            get { return _AddedByUserInfo; }
            set { _AddedByUserInfo = value; }
        }
    }
}
