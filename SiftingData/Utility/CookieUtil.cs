using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Utility
{
    /// <summary>
    /// Cookie操作类
    /// </summary>
    public class CookieUtil
    {
        /// <summary>
        ///  通过指定的url地址获得cookie
        /// </summary>
        /// <param name="cookieContainer"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Cookie GetCookie(CookieContainer cookieContainer , string url)
        {
            Uri uri = new Uri(url);
            CookieCollection cookieColl = cookieContainer.GetCookies( uri );
            if (cookieColl != null && cookieColl.Count > 0 )
            {
                return cookieColl[0];
            }
            return null;
        }
        /// <summary>
        /// 通过指定的url地址获得cookie
        /// </summary>
        /// <param name="cookieContainer"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetCookieInfo(CookieContainer cookieContainer, string url)
        {
            if (cookieContainer == null) return "Cookie有问题。";
            Cookie cookie = GetCookie(cookieContainer, url);
            if (cookie == null) return string.Empty;
            return cookie.Name + "=" + cookie.Value + " Domain=" + cookie.Domain + " Path=" + cookie.Path ; 
        }
        /// <summary>
        /// 清除Webbrowser控件的Cookie
        /// </summary>
        public static unsafe void ClearWebbrowserCookie()
        {
            int option = (int)3;//WinInet.SuppressBehaviorFlags.INTERNET_SUPPRESS_COOKIE_POLICY;
            int* optionPtr = &option;

            bool success = Utility.NativeMethods.InternetSetOption(IntPtr.Zero, 81/* WinInet.InternetOption.INTERNET_OPTION_SUPPRESS_BEHAVIOR */, new IntPtr(optionPtr), sizeof(int));

            if (!success)
            {
                //XtraMessageBox.Show("Failed in WinInet.InternetSetOption call with INTERNET_OPTION_SUPPRESS_BEHAVIOR, INTERNET_SUPPRESS_COOKIE_POLICY");
                LogHelper.WriteLog("清除Webbrowser控件的cookie失败。");
            }
        }
    }
}
