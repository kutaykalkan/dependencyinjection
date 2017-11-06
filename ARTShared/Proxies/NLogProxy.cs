using System;
using NLog;
using ILogger = SkyStem.ART.Shared.Interfaces.ILogger;

namespace SkyStem.ART.Shared.Proxies
{
    public class NLogProxy<T> : ILogger
    {
        private static readonly NLog.ILogger Logger = LogManager.GetLogger(typeof(T).FullName);

        public void LogInfo(string message)
        {
            Logger.Log(LogLevel.Info, message);
        }

        public void LogError(string message)
        {
            Logger.Log(LogLevel.Error, message);
        }

        public void LogError(Exception ex, string message)
        {
            Logger.Log(LogLevel.Error, ex, message);
        }

        public void LogError(Exception ex)
        {
            Logger.Log(LogLevel.Error, ex);
        }
    }
}