using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Utility
{
    /// <summary>
    /// ini文件读写类
    /// </summary>
    public class IniUtil
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        /// <summary>
        /// 读取 指定节下的键的值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadIniValue(string path, string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, 255, path);
            return sb.ToString();
        }
        /// <summary>
        /// 读取指定节的所有键
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="list"></param>
        public static void ReadSections(string path, string section, System.Collections.Specialized.StringCollection keys)
        {
            Byte[] buffer = new Byte[16384];
            long bufLen = GetPrivateProfileString(section, null, null, buffer, buffer.GetUpperBound(0), path);
            //对Section进行解析 
            GetStringsFromBuffer(buffer, bufLen, keys);//
        }
        /// <summary>
        /// 读取指定节下的所有键值对
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="values"></param>
        public static void ReadSections(string path, string section, System.Collections.Specialized.NameValueCollection values)
        {
            System.Collections.Specialized.StringCollection keys = new System.Collections.Specialized.StringCollection();//
            ReadSections(path, section, keys);
            values.Clear();
            foreach (string key in keys)
            {
                values.Add(key, ReadIniValue(path, section, key));
            }
        }
        private static void GetStringsFromBuffer(Byte[] buffer, long bufLen, System.Collections.Specialized.StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((buffer[i] == 0) && ((i - start) > 0))//
                    {
                        String s = Encoding.GetEncoding(0).GetString(buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }
        /// <summary>
        /// 写指定节的键值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="keyValue"></param>
        public static void WriteIniValue(string path, string section, string key, string keyValue)
        {
            WritePrivateProfileString(section, key, keyValue, path);
        }
    }
}
