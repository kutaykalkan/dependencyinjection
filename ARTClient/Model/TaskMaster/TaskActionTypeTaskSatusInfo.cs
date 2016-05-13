using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class TaskActionTypeTaskSatusInfo
    {
        private System.Int16? _TaskActionTypeTaskStatusID = null;
        private System.Int16? _TaskActionTypeID = null;
        private System.Int16? _TaskStatusID = null;
        

        public System.Int16? TaskActionTypeTaskStatusID
        {
            get { return _TaskActionTypeTaskStatusID; }
            set { _TaskActionTypeTaskStatusID = value; }
        }

        public System.Int16? TaskActionTypeID
        {
            get { return _TaskActionTypeID; }
            set { _TaskActionTypeID = value; }
        }

        public System.Int16? TaskStatusID
        {
            get { return _TaskStatusID; }
            set { _TaskStatusID = value; }
        }

        
    }
}
