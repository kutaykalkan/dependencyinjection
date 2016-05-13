using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Exception
{
    /// <summary>
    /// ART Exception
    /// </summary>
    [DataContractAttribute]
    public class ARTException : ApplicationException
    {
        /// <summary>
        /// Represents the Phrase ID of the ARTException
        /// </summary>
        protected int _exceptionPhraseID = 0;

        /// <summary>
        /// Represents the Message of the ARTException
        /// </summary>
        protected string _message = "";

        /// <summary>
        /// Represents the Format Objects to be used while formatting exception message
        /// </summary>
        protected string[] _arrFormatObject;

        /// <summary>
        /// Gets or Sets the ID of the ARTException
        /// </summary>
        [DataMemberAttribute]
        public int ExceptionPhraseID
        {
            get { return _exceptionPhraseID; }
            set { _exceptionPhraseID = value; }
        }

        /// <summary>
        /// Gets or Sets the Message of the ARTException
        /// </summary>
        [DataMemberAttribute]
        public string ExceptionMessage
        {
            get { return _message; }
            set { _message = value; }
        }

        [DataMemberAttribute]
        public string[] FormatObject
        {
            get { return _arrFormatObject; }
            set { _arrFormatObject = value; }
        }

        public ARTException()
            : base()
        {
        }

        public ARTException(int exceptionPhraseId)
            : base()
        {
            //TODO: (Low) write code here to log this message also along with other comments
            ExceptionPhraseID = exceptionPhraseId;
        }

        public ARTException(int exceptionPhraseId, string[] arrFormatObject)
            : this(exceptionPhraseId)
        {
            //TODO: (Low) write code here to log this message also along with other comments
            FormatObject = arrFormatObject;
        }

        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("_exceptionPhraseID", _exceptionPhraseID);
        //    info.AddValue("_message", _message);
        //    info.AddValue("_arrFormatObject", _arrFormatObject);
        //}

        public override string ToString()
        {
            return "Exception: " + _exceptionPhraseID.ToString() + " Message: " + _message;
        }
    } // end of class
}