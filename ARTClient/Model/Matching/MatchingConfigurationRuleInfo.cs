
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingConfigurationRule table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MatchingConfigurationRuleInfo : MatchingConfigurationRuleInfoBase
	{
        private Boolean _isRuleDeleted = false;
        Int64 _ruleID;
        String _displayRuleText = string.Empty;

        public Boolean IsRuleDeleted
        {
            get { return _isRuleDeleted; }
            set { _isRuleDeleted = value; }
        }
        public Int64 RuleID
        {
            get { return _ruleID; }
            set { _ruleID = value; }
        }
        public string DisplayRuleText
        {
            get { return _displayRuleText; }
            set { _displayRuleText = value; }
        }

	}
}
