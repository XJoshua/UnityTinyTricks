
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Reflection;

namespace UnityUtilities
{
    /// <summary>
    /// 180209-修改 Warning 和 Error 的输出，和 Log 的输出一致
    /// </summary>
    public class Debugger
    {
        public enum LogLevel
        {
            Debug,
            Warn,
            Error
        }

        //public enum

        public static bool EnableLog = true;
        public static bool EnableTime = true;
        public static bool EnableColor = true;

        // old
        //private static Debugger _instance;
        //private GameObject _lineObject;
        //private Queue<MessageBody> _queue;
        //private float _totalHeight;
        // old end

        public static string ColorToHex(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255.0f);
            int g = Mathf.RoundToInt(color.g * 255.0f);
            int b = Mathf.RoundToInt(color.b * 255.0f);
            int a = Mathf.RoundToInt(color.a * 255.0f);
            string hex = $"#{r:X2}{g:X2}{b:X2}{a:X2}";
            return hex;
        }

        public static void Debug(string message, string methodName = null,  params object[] args)
        {
            string time2 = DateTime.Now.ToString("HH:mm:ss.fff");

            message = "#### " + time2 + " > " + methodName + " >>> " + (args.Length == 0 ? message : string.Format(message, args));

            UnityEngine.Debug.Log(message);
        }

        public static void Log(string tag, string methodName, string message, params object[] args)
        {
            if (Debugger.EnableLog)
            {
                message = GetLogText(tag, methodName, message);

                UnityEngine.Debug.Log(message);
            }
        }

        public static void Log(string tag, string methodName, string message, Color color, params object[] args)
        {
            if (Debugger.EnableLog)
            {
                message = GetLogText(tag, methodName, message, color);

                UnityEngine.Debug.Log(message);
            }
        }

        public static void Warn(string tag, string methodName, string message,  params object[] args)
        {
            if (Debugger.EnableLog)
            {
                message = GetLogText(tag, methodName, message);

                UnityEngine.Debug.LogWarning(message);
            }
        }

        public static void Error(string tag, string methodName, string message,  params object[] args)
        {
            if (Debugger.EnableLog)
            {
                message = GetLogText(tag, methodName, message);

                UnityEngine.Debug.LogError(message);
            }
        }

        internal class MessageBody
        {
            public string Content;
            public LogLevel level;
        }

        //----------------------------------------------------------------------
        private static string GetLogText(string tag, string methodName, string message)
        {
            string str = "";

            if (Debugger.EnableTime)
            {
                str = DateTime.Now.ToString("HH:mm:ss.fff") + " ";
            }

            return "##Log## " + str + " > " + tag + " : " + methodName + "() " + " >>> " + message;
        }

        private static string GetLogText(string tag, string methodName, string message, Color color)
        {
            string str = "";

            if (Debugger.EnableTime)
            {
                str = DateTime.Now.ToString("HH:mm:ss.fff");
            }

            return $"##Log## {str} > {tag} : {methodName}() >>> <color={ColorToHex(color)}>{message}</color>";
            //"##Log## " + str + " > " + tag + " : " + methodName + "() " + " >>> " + "<color=" + ColorToHex(color) +">" + message + "</color>";
        }
    }

    // 这个扩展是静态的，debugger是继承自MonoBehaviour的，可能有问题
    /// <summary>
    /// 扩展方法，提供其他类的debug方法
    /// </summary>
    public static class DebuggerExtension
    {
        //----------------------------------------------------------------------
        [Conditional("ENABLE_LOG")]
        public static void Log(this object obj, string message = "")
        {
            if (Debugger.EnableLog)
            {
                Debugger.Log(GetLogTag(obj), GetLogCallerMethod(), (string)message);
            }
        }

        [Conditional("ENABLE_LOG")]
        public static void Log(this object obj, string message, Color color)
        {
            if (Debugger.EnableLog)
            {
                Debugger.Log(GetLogTag(obj), GetLogCallerMethod(), (string)message, color);
            }
        }

        [Conditional("ENABLE_LOG")]
        public static void Log(this object obj, object message)
        {
            if (Debugger.EnableLog)
            {
                Debugger.Log(GetLogTag(obj), GetLogCallerMethod(), message.ToString());
            }
        }

        [Conditional("ENABLE_LOG")]
        public static void Log(this object obj, string format, params object[] args)
        {
            if (Debugger.EnableLog)
            {
                Debugger.Log(GetLogTag(obj), GetLogCallerMethod(), string.Format(format, args));
            }
        }


        //----------------------------------------------------------------------
        [Conditional("ENABLE_LOG")]
        public static void Error(this object obj, string message)
        {
            Debugger.Error(GetLogTag(obj), GetLogCallerMethod(), (string)message);
        }

        [Conditional("ENABLE_LOG")]
        public static void Error(this object obj, string format, params object[] args)
        {
            Debugger.Error(GetLogTag(obj), GetLogCallerMethod(), string.Format(format, args));
        }


        //----------------------------------------------------------------------
        [Conditional("ENABLE_LOG")]
        public static void Warn(this object obj, string message)
        {
            Debugger.Warn(GetLogTag(obj), GetLogCallerMethod(), (string)message);
        }

        [Conditional("ENABLE_LOG")]
        public static void Warn(this object obj, string format, params object[] args)
        {
            Debugger.Warn(GetLogTag(obj), GetLogCallerMethod(), string.Format(format, args));
        }


        //通过反射获得标签和方法----------------------------------------------------------------------
        private static string GetLogTag(object obj)
        {
            FieldInfo fi = obj.GetType().GetField("LOG_TAG");
            if (fi != null)
            {
                return (string)fi.GetValue(obj);
            }

            return obj.GetType().Name;
        }

        private static Assembly ms_Assembly;
        private static string GetLogCallerMethod()
        {
            StackTrace st = new StackTrace(2, false);
            if (st != null)
            {
                if (null == ms_Assembly)
                {
                    ms_Assembly = typeof(Debugger).Assembly;
                }

                int currStackFrameIndex = 0;
                while (currStackFrameIndex < st.FrameCount)
                {
                    StackFrame oneSf = st.GetFrame(currStackFrameIndex);
                    MethodBase oneMethod = oneSf.GetMethod();

                    if (oneMethod.Module.Assembly != ms_Assembly)
                    {
                        return oneMethod.Name;
                    }

                    currStackFrameIndex++;
                }
            }
            return "";
        }
    }
}