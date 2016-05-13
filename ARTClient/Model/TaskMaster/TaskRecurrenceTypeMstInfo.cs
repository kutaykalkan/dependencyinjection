
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART TaskRecurrenceTypeMst table
	/// </summary>
	[Serializable]
	[DataContract]
    [XmlType("TaskRecurrenceType")]
	public class TaskRecurrenceTypeMstInfo : TaskRecurrenceTypeMstInfoBase
	{
        protected System.Boolean? _IsTaskCompleted = null;
        [XmlElement(ElementName = "IsTaskCompleted")]
        public virtual System.Boolean? IsTaskCompleted
        {
            get
            {
                return this._IsTaskCompleted;
            }
            set
            {
                this._IsTaskCompleted = value;

            }
        }
	}
}
