using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Utility
{
    public class InternetProxy
    {
        public const int INTERNET_OPTION_PROXY = 38;
        public const int INTERNET_OPEN_TYPE_PROXY = 3;
        public const int INTERNET_OPEN_TYPE_DIRECT = 1;
        public const int INTERNET_OPTION_PROXY_USERNAME = 43;
        public const int INTERNET_OPTION_PROXY_PASSWORD = 44;
        //[DllImport("wininet.dll", SetLastError = true)]
        //public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        public struct Struct_INTERNET_PROXY_INFO
        {
            public int dwAccessType;
            public IntPtr proxy;
            public IntPtr proxyBypass;
        }
        /// <summary>
        /// 设置代理服务器地址
        /// </summary>
        /// <param name="strProxy">xxx.xxx.xxx.xxx:xxxx</param>
        /// <returns></returns>
        public static bool SetIESettings(string strProxy )
        {
            Struct_INTERNET_PROXY_INFO struct_IPI;
            // Filling in structure 
            struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
            if (string.IsNullOrEmpty(strProxy))
            {
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;
            }
            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");
            // Allocating memory 
            IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));
            // Converting structure to IntPtr 
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);

            bool iReturn = NativeMethods.InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
                    
            return iReturn;
        }
        /// <summary>
        /// 设置代理验证
        /// </summary>
        /// <param name="handle">webbrowser句柄</param>
        /// <param name="username">代理服务器用户</param>
        /// <param name="password">代理服务器密码</param>
        /// <returns></returns>
        public static bool SetIEUserPassword( IntPtr handle , string username, string password)
        {
            IntPtr intPUsername = Marshal.StringToHGlobalAnsi(username);
            int len1 = Marshal.SizeOf(intPUsername);
            IntPtr intPPassword = Marshal.StringToHGlobalAnsi(password);
            int len2 = Marshal.SizeOf(intPPassword);
           NativeMethods.InternetSetOption(handle   /*WebBrowser.Handle.ToInt32() */ , INTERNET_OPTION_PROXY_USERNAME, intPUsername, len1 );
            int err = Marshal.GetLastWin32Error();

            NativeMethods.InternetSetOption(handle  /* WebBrowser.FromChildHandle.ToInt32() */ , INTERNET_OPTION_PROXY_PASSWORD, intPPassword, len2);
            err = Marshal.GetLastWin32Error();

            return true;
        }
        /// <summary>
        /// 取消 代理
        /// </summary>
        /// <returns></returns>
        public static bool DisableProxy()
        {
            return SetIESettings(string.Empty);
        }

        public static bool Test(string url , string proxyaddress , string proxyport , string username , string password ,bool isProxy )
        {
            try
            {
                System.Net.WebProxy proxy = null;
                if (isProxy)
                {
                    proxy = new System.Net.WebProxy(proxyaddress + ":" + proxyport, true);
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    {
                        proxy.Credentials = new System.Net.NetworkCredential(username, password);
                    }
                    else
                    {
                        //proxy.Credentials = new System.Net;
                    }
                }
                System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                req.Timeout = 10000;
                req.Proxy = proxy;
                req.GetResponse();                
                req.Abort();
                req = null;
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("测试代理服务器失败:代理服务器:"+ proxyaddress+":"+ proxyport +",用户名:"+username +"密码:"+password + Environment.NewLine + "错误:"+ ex.Message );
                return false;
            }
        }
    }
}
