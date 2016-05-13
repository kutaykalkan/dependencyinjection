
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt AlertMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class AlertMstInfo : AlertMstInfoBase
	{
        private string _AlertType = string.Empty;

        public string AlertType 
        {
            get
            {
                return this._AlertType;
            }
            set
            {
                this._AlertType = value;
            }
        }

        private string _ThresholdType = string.Empty;

        public string ThresholdType
        {
            get
            {
                return this._ThresholdType;
            }
            set
            {
                this._ThresholdType = value;
            }
        }

	}
}
