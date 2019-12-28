using System;
using UnityEngine;

namespace Netgo.Library
{
    public class NGLogger
    {
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public static void LogInfo(string message)
        {
            Debug.Log(message);
        }

        public static void LogDebug(string message)
        {
            Debug.Log("NGDebug Info: " +message);
        }
    }
}