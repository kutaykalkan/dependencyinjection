
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART TaskListHdr table
	/// </summary>
	[Serializable]
	[DataContract]
    [XmlType("TaskList")]
	public class TaskListHdrInfo : TaskListHdrInfoBase
	{
	}
}
