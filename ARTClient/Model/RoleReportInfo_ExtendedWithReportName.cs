using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
     public  class RoleReportInfo_ExtendedWithReportName : RoleReportInfoBase
    {
        [XmlElement(ElementName = "Report")]
        public virtual System.String Report
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        [XmlElement(ElementName = "ReportLabelID")]
        public virtual System.Int32? ReportLabelID
        {
            get
            {
                return this.LabelID;
            }
            set
            {
                this.LabelID = value;
            }
        }



        //public bool IsReportNull = true;
        //public bool IsReportLabelIDNull = true;


        protected System.String _Description = "";
        public bool IsDescriptionNull = true;
        [XmlElement(ElementName = "Description")]
        public virtual System.String Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
                this.IsDescriptionNull = (_Description == null);
            }
        }
        protected System.Int32? _DescriptionLabelID = 0;
        public bool IsDescriptionLabelIDNull = true;
        [XmlElement(ElementName = "DescriptionLabelID")]
        public virtual System.Int32? DescriptionLabelID
        {
            get
            {
                return this._DescriptionLabelID;
            }
            set
            {
                this._DescriptionLabelID = value;

                this.IsDescriptionLabelIDNull = false;
            }
        }			




    }//end of class
}//end of namespace
