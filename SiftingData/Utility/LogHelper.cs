namespace Utility
{
    using System;
    using System.IO;
    /// <summary>
    /// 日志类
    /// </summary>
    public class LogHelper
    {
        private static readonly object object_0 = new object();
        private static StreamWriter streamWriter_0;

        private static void smethod_0(string string_0)
        {
            streamWriter_0 = !File.Exists(string_0) ? File.CreateText(string_0) : File.AppendText(string_0);
        }

        public static void WriteException(string prefix, Exception exception)
        {
            WriteLog(prefix, exception);
        }

        public static void WriteException(Exception exception)
        {
            WriteLog( string.Empty , exception);
        }

        public static void WriteLog(string msg)
        {
            WriteLog(string.Empty, msg);
        }

        public static void WriteLog(string prefix, Exception exception)
        {
            lock (object_0)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string path = string.Format("{0}\\Log", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + string.Format("\\{0}{1}.log", prefix , now.ToString("yyyy-MM-dd"));
                    if (streamWriter_0 == null)
                    {
                        smethod_0(path);
                    }
                    if (exception != null)
                    {
                        streamWriter_0.WriteLine(now.ToString("HH:mm:ss.fff") + "  异常信息：" + exception.Message + Environment.NewLine+ "  堆栈信息:"+ exception.StackTrace);
                    }
                }
                finally
                {
                    if (streamWriter_0 != null)
                    {
                        streamWriter_0.Flush();
                        streamWriter_0.Dispose();
                        streamWriter_0 = null;
                    }
                }
            }
        }

        public static void WriteLog(string prefix, string msg)
        {
            lock (object_0)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string path = string.Format("{0}\\Log", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + string.Format("\\{0}{1}.log", prefix , now.ToString("yyyy-MM-dd"));
                    if (streamWriter_0 == null)
                    {
                        smethod_0(path);
                    }
                    if ( string.IsNullOrEmpty( msg ) == false )
                    {
                        streamWriter_0.WriteLine(now.ToString("HH:mm:ss.fff ") + msg );
                    }
                }
                finally
                {
                    if (streamWriter_0 != null)
                    {
                        streamWriter_0.Flush();
                        streamWriter_0.Dispose();
                        streamWriter_0 = null;
                    }
                }
            }
        }



    }
}

