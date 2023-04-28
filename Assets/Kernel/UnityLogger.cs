using System;
using UnityEngine;

namespace Kernel
{
    public class UnityLogger : ILogger
    {
        public void Log(string message) => Debug.Log(message);

        public void LogError(Exception exception) => Debug.LogError(exception);

        public void LogError(string message) => Debug.LogError(message);
        
        public void LogWarning(string message) => Debug.LogWarning(message);

    }
}