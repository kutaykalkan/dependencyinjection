using System;

namespace SkyStem.ART.Shared.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogError(Exception ex, string message);
        void LogError(Exception ex);
    }
}