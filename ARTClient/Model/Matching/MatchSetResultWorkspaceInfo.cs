
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchSetResultWorkspace table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MatchSetResultWorkspaceInfo : MatchSetResultWorkspaceInfoBase
	{
        public string ResultSchema { get; set; }
	}
}
