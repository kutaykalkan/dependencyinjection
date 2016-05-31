using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Utility;
using System.Diagnostics;

namespace SkyStem.ART.Service.Log
{
    public class EventLogger
    {
        public const string EVENT_SOURCE_NAME = "SkyStemARTServiceLog";
        public const string EVENT_LOG_NAME = "";

        public static void CreateEventLog()
        {
            if (!EventLog.SourceExists(EventLogger.EVENT_SOURCE_NAME))
            {
                EventLog.CreateEventSource(EventLogger.EVENT_SOURCE_NAME, EventLogger.EVENT_LOG_NAME); //"Application");
                System.Threading.Thread.Sleep(2000); 
            }
        }

        internal static void WriteLogEntry(string message)
        {
            EventLog oEventLog = new EventLog(EventLogger.EVENT_LOG_NAME);
            oEventLog.Source = EventLogger.EVENT_SOURCE_NAME;
            oEventLog.WriteEntry(message);
        }
    }
}
