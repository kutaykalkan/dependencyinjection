using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Exception
{
    /// <summary>
    /// 
    /// </summary>
    [DataContractAttribute]
    public class ARTSystemException : ARTException
    {
        public ARTSystemException()
            : base()
        {
        }

        public ARTSystemException(int exceptionPhraseId)
            : base(exceptionPhraseId)
        {
        }

    }
}
