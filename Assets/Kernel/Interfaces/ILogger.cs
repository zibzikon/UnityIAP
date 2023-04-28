using System;

namespace Kernel
{
    public interface ILogger
    {
        void Log(string message);
    
        void LogError(Exception exception);
        void LogError(string message);
        void LogWarning(string message);
    }
}