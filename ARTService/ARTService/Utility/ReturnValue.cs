using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace SkyStem.ART.Service.Utility
{
    public class ReturnValue
    {
        [XmlElement(ElementName = "ErrorMessageForServiceToLog")]
        public string ErrorMessageForServiceToLog { get; set; }

        [XmlElement(ElementName = "ErrorMessageToSave")]
        public string ErrorMessageToSave { get; set; }

        [XmlElement(ElementName = "ImportStatus")]
        public string ImportStatus { get; set; }

        [XmlElement(ElementName = "RecordsImported")]
        public int? RecordsImported { get; set; }

        [XmlElement(ElementName = "WarningReasonID")]
        public short? WarningReasonID { get; set; }

        [XmlElement(ElementName = "IsAlertRaised")]
        public bool? IsAlertRaised { get; set; }

        [XmlElement(ElementName = "OverridenEmailID")]
        public string OverridenEmailID { get; set; }

        [XmlElement(ElementName = "NewUserEmailID")]
        public string NewUserEmailID { get; set; }

        [XmlElement(ElementName = "ProfilingData")]
        public string ProfilingData { get; set; }

        public static ReturnValue DeSerialize(string xmlString)
        {
            XmlSerializer xmlSerial = null;
            StringReader strReader = null;
            XmlTextReader txtReader = null;
            try
            {
                xmlSerial = new XmlSerializer(typeof(ReturnValue));
                strReader = new StringReader(xmlString);
                txtReader = new XmlTextReader(strReader);
                return (ReturnValue)xmlSerial.Deserialize(txtReader);
            }
            finally
            {
                txtReader.Close();
                strReader.Close();
            }

        }

    }
}
