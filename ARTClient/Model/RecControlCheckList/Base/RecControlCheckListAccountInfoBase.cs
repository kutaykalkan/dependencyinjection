using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList.Base
{
    /// <summary>
    /// An object representation of the SkyStemART RecControlCheckListAccount table
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class RecControlCheckListAccountInfoBase
    {
        protected Int64? _RecControlCheckListAccountID = null;
        protected Int32? _RecControlCheckListID = null;
        protected Int64? _AccountID = null;
        protected Int32? _NetAccountID = null;
        protected Int32? _StartRecPeriodID = null;
        protected Int32? _EndRecPeriodID = null;
        protected Int32? _DataImportID = null;
        protected System.Boolean? _IsActive = null;
        protected Int32? _AddedByUserID = null;
        protected Int16? _RoleID = null;
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.String _RevisedBy = null;
        protected System.DateTime? _DateRevised = null;

        [DataMember]
        [XmlElement(ElementName = "RecControlCheckListAccountID")]
        public virtual System.Int64? RecControlCheckListAccountID
        {
            get { return this._RecControlCheckListAccountID; }
            set { this._RecControlCheckListAccountID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RecControlCheckListID")]
        public virtual System.Int32? RecControlCheckListID
        {
            get { return this._RecControlCheckListID; }
            set { this._RecControlCheckListID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AccountID")]
        public virtual System.Int64? AccountID
        {
            get { return this._AccountID; }
            set { this._AccountID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "NetAccountID")]
        public virtual System.Int32? NetAccountID
        {
            get { return this._NetAccountID; }
            set { this._NetAccountID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "StartRecPeriodID")]
        public virtual System.Int32? StartRecPeriodID
        {
            get { return this._StartRecPeriodID; }
            set { this._StartRecPeriodID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "EndRecPeriodID")]
        public virtual System.Int32? EndRecPeriodID
        {
            get { return this._EndRecPeriodID; }
            set { this._EndRecPeriodID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DataImportID")]
        public virtual System.Int32? DataImportID
        {
            get { return this._DataImportID; }
            set { this._DataImportID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AddedByUserID")]
        public virtual System.Int32? AddedByUserID
        {
            get { return this._AddedByUserID; }
            set { this._AddedByUserID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
        {
            get { return this._RoleID; }
            set { this._RoleID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get { return this._IsActive; }
            set { this._IsActive = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get { return this._AddedBy; }
            set { this._AddedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get { return this._DateAdded; }
            set { this._DateAdded = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get { return this._RevisedBy; }
            set { this._RevisedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime? DateRevised
        {
            get { return this._DateRevised; }
            set { this._DateRevised = value; }
        }
    }
}
