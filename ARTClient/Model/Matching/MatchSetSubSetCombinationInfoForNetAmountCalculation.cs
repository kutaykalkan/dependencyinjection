using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{
    [Serializable]
    [DataContract]
   public class MatchSetSubSetCombinationInfoForNetAmountCalculation
    {
        protected System.String _Source1Name = String.Empty;
        protected System.String _Source2Name = String.Empty;

        [XmlElement(ElementName = "Source1Name")]
        public virtual System.String Source1Name
        {
            get
            {
                return this._Source1Name;
            }
            set
            {
                this._Source1Name = value;

            }
        }


        [XmlElement(ElementName = "Source2Name")]
        public virtual System.String Source2Name
        {
            get
            {
                return this._Source2Name;
            }
            set
            {
                this._Source2Name = value;

            }
        }
    }
}
