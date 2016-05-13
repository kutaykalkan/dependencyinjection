
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{

    /// <summary>
    /// An object representation of the SkyStemART MatchSetSubSetCombination table
    /// </summary>
    [Serializable]
    [DataContract]
    public class MatchSetSubSetCombinationInfo : MatchSetSubSetCombinationInfoBase
    {

        private List<MatchingConfigurationInfo> _MatchingConfigurationInfoList = null;
        private MatchSetResultInfo _MatchSetResultInfo = null;
        private MatchSetResultWorkspaceInfo _MatchSetResultWorkspaceInfo = null;
        private List<MatchSetGLDataRecItemInfo> _GLDataRecItemInfoListSource1 = null;
        private List<MatchSetGLDataRecItemInfo> _GLDataRecItemInfoListSource2 = null;

        [XmlElement(ElementName = "MatchingConfigurationInfoList")]
        public virtual List<MatchingConfigurationInfo> MatchingConfigurationInfoList
        {
            get
            {
                return this._MatchingConfigurationInfoList;
            }
            set
            {
                this._MatchingConfigurationInfoList = value;
            }
        }


        [XmlElement(ElementName = "MatchSetResultInfo")]
        public virtual MatchSetResultInfo MatchSetResultInfo
        {
            get
            {
                return this._MatchSetResultInfo;
            }
            set
            {
                this._MatchSetResultInfo = value;
            }
        }


        [XmlElement(ElementName = "MatchSetResultWorkspaceInfo")]
        public virtual MatchSetResultWorkspaceInfo MatchSetResultWorkspaceInfo
        {
            get
            {
                return this._MatchSetResultWorkspaceInfo;
            }
            set
            {
                this._MatchSetResultWorkspaceInfo = value;
            }
        }


        private bool _IsConfigurationChange = false;

        [XmlElement(ElementName = "IsConfigurationChange")]
        public virtual bool IsConfigurationChange
        {
            get
            {
                return this._IsConfigurationChange;
            }
            set
            {
                this._IsConfigurationChange = value;
            }
        }

        [XmlElement(ElementName = "GLDataRecItemInfoListSource1")]
        public virtual List<MatchSetGLDataRecItemInfo> GLDataRecItemInfoListSource1
        {
            get
            {
                return this._GLDataRecItemInfoListSource1;
            }
            set
            {
                this._GLDataRecItemInfoListSource1 = value;
            }
        }

        [XmlElement(ElementName = "GLDataRecItemInfoListSource2")]
        public virtual List<MatchSetGLDataRecItemInfo> GLDataRecItemInfoListSource2
        {
            get
            {
                return this._GLDataRecItemInfoListSource2;
            }
            set
            {
                this._GLDataRecItemInfoListSource2 = value;
            }
        }

        Int64? _MatchingSourceDataImport1ID = null;
        [XmlElement(ElementName = "MatchingSourceDataImport1ID")]
        public virtual System.Int64? MatchingSourceDataImport1ID
        {
            get
            {
                return this._MatchingSourceDataImport1ID;
            }
            set
            {
                this._MatchingSourceDataImport1ID = value;
            }
        }

        Int64? _MatchingSourceDataImport2ID = null;
        [XmlElement(ElementName = "MatchingSourceDataImport2ID")]
        public virtual System.Int64? MatchingSourceDataImport2ID
        {
            get
            {
                return this._MatchingSourceDataImport2ID;
            }
            set
            {
                this._MatchingSourceDataImport2ID = value;
            }
        }
    }
}
