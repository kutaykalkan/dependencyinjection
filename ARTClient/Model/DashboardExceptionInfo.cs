using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SkyStem.ART.Client.Model.Base;

namespace SkyStem.ART.Client.Model
{
    public class DashboardExceptionInfo 
    {
        protected List<ExceptionByFSCaptionNetAccountInfo> _FSCaptionExceptionInfoCollection = null;
        protected List<ExceptionByFSCaptionNetAccountInfo> _NetAccountExceptionInfoCollection = null;

        [XmlElement(ElementName = "FSCaptionExceptionInfoCollection")]
        public virtual List<ExceptionByFSCaptionNetAccountInfo> FSCaptionExceptionInfoCollection
        {
            get
            {
                return this._FSCaptionExceptionInfoCollection;
            }
            set
            {
                this._FSCaptionExceptionInfoCollection = value;
            }
        }

        [XmlElement(ElementName = "NetAccountExceptionInfoCollection")]
        public virtual List<ExceptionByFSCaptionNetAccountInfo> NetAccountExceptionInfoCollection
        {
            get
            {
                return this._NetAccountExceptionInfoCollection;
            }
            set
            {
                this._NetAccountExceptionInfoCollection = value;
            }
        }



    }
}
