
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt SystemReconciliationRuleMst table
    /// </summary>
    [Serializable]
    [DataContract]
    public class SystemReconciliationRuleMstInfo : SystemReconciliationRuleMstInfoBase
    {
        private short? _CapabilityID = 0;

        public short? CapabilityID
        {
            get
            {
                return this._CapabilityID;
            }
            set
            {
                this._CapabilityID = value;
            }
        }

        public string SystemReconciliationRuleNumber { get; set; }
    }
}
