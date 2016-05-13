
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReconciliationCategoryMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class ReconciliationCategoryMstInfo : ReconciliationCategoryMstInfoBase
	{
        protected System.String _ReconciliationCategoryType = "";
        protected System.Int16? _ReconciliationCategoryTypeID = 0;
        protected System.Int32? _ReconciliationCategoryTypeLabelID = 0;
        protected System.Int16? _RecItemControlID = 0;
        [XmlElement(ElementName = "ReconciliationCategoryType")]
        public virtual System.String ReconciliationCategoryType
        {
            get
            {
                return this._ReconciliationCategoryType;
            }
            set
            {
                this._ReconciliationCategoryType = value;
               
            }
        }

        [XmlElement(ElementName = "ReconciliationCategoryTypeID")]
        public virtual System.Int16? ReconciliationCategoryTypeID
        {
            get
            {
                return this._ReconciliationCategoryTypeID;
            }
            set
            {
                this._ReconciliationCategoryTypeID = value;
               
            }
        }

        [XmlElement(ElementName = "ReconciliationCategoryTypeLabelID")]
        public virtual System.Int32? ReconciliationCategoryTypeLabelID
        {
            get
            {
                return this._ReconciliationCategoryTypeLabelID;
            }
            set
            {
                this._ReconciliationCategoryTypeLabelID = value;
              
            }
        }

        [XmlElement(ElementName = "RecItemControlID")]
        public virtual System.Int16? RecItemControlID
        {
            get
            {
                return this._RecItemControlID;
            }
            set
            {
                this._RecItemControlID = value;

            }
        }
	}
}
