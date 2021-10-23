using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;
using static UnityEngine.Debug;
using static UnityEngine.ColorUtility;

namespace lukassacher.UnityTools
{
    public static class UnityExtensions
    {
        // TODO: Change constants and settings in a scriptable object
        
        private static string get_log_string(object msg) => $"{msg}".Size(23);
        
        /// <summary>
        /// Log method when debugging
        /// </summary>
        public static void l(this MonoBehaviour target, object message)
        {
            Log(get_log_string(message).Color(new Color(0.15f, 0.7f, 0.4f)));
        }
        public static void log(this MonoBehaviour target, object message)
        {
            Log(get_log_string(message));
        }
        public static void log(this MonoBehaviour target, object message, Object context)
        {
            Log(get_log_string(message), context);
        }
        public static void log(this MonoBehaviour target, object message, LogType type)
        {
            var logString = get_log_string(message);
            
            if(type == LogType.Log)
                log(target, logString);
            else if(type == LogType.Warning)
                warn(target,  logString.Warning());
            else
                error(target, logString.Error());
        }
        public static void log(this MonoBehaviour target, object message, Object context, LogType type)
        {
            var logString = get_log_string(message);
            
            if(type == LogType.Log)
                log(target, logString, context);
            else if(type == LogType.Warning)
                warn(target, logString.Warning(), context);
            else
                error(target, logString.Error(), context);
        }
        public static void warn(this MonoBehaviour target, object message)
        {
            LogWarning(get_log_string(message));
        }
        public static void warn(this MonoBehaviour target, object message, Object context)
        {
            LogWarning(get_log_string(message), context);
        }
        public static void error(this MonoBehaviour target, object message)
        {
            LogError(get_log_string(message));
        }
        public static void error(this MonoBehaviour target, object message, Object context)
        {
            LogError(get_log_string(message), context);
        }

        public static string Color(this string s, Color c)
        {
            return $"<color=#{ToHtmlStringRGB(c)}>{s}</color>";
        }
        
        public static string Size(this string s, int size)
        {
            return $"<size={size}>{s}</size>";
        }

        public static string Bold(this string s)
        {
            return $"<b>{s}</b>";
        }

        public static string Italic(this string s)
        {
            return $"<i>{s}</i>";
        }

        public static string Error(this string s)
        {
            return "Error: ".Color(new Color(0.7f, 0.26f, 0.26f)) + s;
        }
        
        public static string Warning(this string s)
        {
            return "Warning: ".Color(new Color(0.8f, 0.7f, 0.2f)) + s;
        }

        public static string GetAOrAnPrefix(this string s, bool caps = false)
        {
            string prefix = "a";
            char first = s.ToLower()[0];
            if (first == 'e' || first == 'a' || first == 'u' || first == 'o' || first == 'i') 
                prefix = "an";

            return caps ? prefix.Replace('a', 'A') : prefix;
        }
    }

    public struct UnityUtility
    {
        public static IEnumerable<GameObject> FindAllGameObjectsInCurrentScene()
        {
            var rootGameObjects = GetActiveScene().GetRootGameObjects();
            var allGos = new List<GameObject>();
            
            foreach (var rootObject in rootGameObjects)
            {
                allGos.AddRange(
                    rootObject.GetComponentsInChildren<Transform>(true)
                        .Select(t => t.gameObject));
            }

            return allGos;
        }
    }
}
