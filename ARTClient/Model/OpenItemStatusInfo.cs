using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace SkyStem.ART.Client.Model
{
    public class OpenItemStatusInfo
    {
        System.Decimal? _TotalAmountForOpenRecItem = null;
        System.Int16? _AgingCategoryId = null;
        System.String _AgingCategoryName = string.Empty;

        [XmlElement(ElementName = "TotalAmountForOpenRecItem")]
        public System.Decimal? TotalAmountForOpenRecItem
        {
            get { return _TotalAmountForOpenRecItem; }
            set { _TotalAmountForOpenRecItem = value; }
        }

        [XmlElement(ElementName = "AgingCategoryId")]
        public System.Int16? AgingCategoryId
        {
            get { return _AgingCategoryId; }
            set { _AgingCategoryId = value; }
        }

        [XmlElement(ElementName = "AgingCategoryName")]
        public System.String AgingCategoryName
        {
            get { return _AgingCategoryName; }
            set { _AgingCategoryName = value; }
        }

       

    }
}
