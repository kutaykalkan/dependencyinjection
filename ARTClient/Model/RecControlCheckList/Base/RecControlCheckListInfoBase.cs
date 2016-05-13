using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList.Base
{
    /// <summary>
    /// An object representation of the SkyStemART RecControlCheckListHdr table
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class RecControlCheckListInfoBase
    {
        protected Int32? _RecControlCheckListID = null;
        protected string _CheckListNumber = null;
        protected string _Description = null;
        protected Int32? _DescriptionLabelID = null;
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
        [XmlElement(ElementName = "RecControlCheckListID")]
        public virtual System.Int32? RecControlCheckListID
        {
            get { return this._RecControlCheckListID; }
            set { this._RecControlCheckListID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "CheckListNumber")]
        public virtual System.String CheckListNumber
        {
            get { return this._CheckListNumber; }
            set { this._CheckListNumber = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "Description")]
        public virtual System.String Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DescriptionLabelID")]
        public virtual System.Int32? DescriptionLabelID
        {
            get { return this._DescriptionLabelID; }
            set { this._DescriptionLabelID = value; }
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
