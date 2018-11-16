using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 验证工具类
    /// </summary>
    public class ValidateUtil
    {
        /// <summary>
        /// 验证 IP 地址的合法性
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static  bool ValidateIP( string ip , ref string message )
        {
            if (string.IsNullOrEmpty( ip ))
            {
                message = "请设置服务IP地址";
                return false;
            }
            if ( ip.ToLower().Trim().Equals("localhost"))
            {
                return true;
            }

            string patten = @"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b";
            if (System.Text.RegularExpressions.Regex.IsMatch( ip.Trim(), patten) == false)
            {
                message = "请输入正确的IP地址。";
                return false;
            }

            return true;
        }
        /// <summary>
        ///  验证 端口 合法性
        /// </summary>
        /// <param name="portstr"></param>
        /// <returns></returns>
        public static bool ValidatePort( string portStr , ref string message )
        {
            if (string.IsNullOrEmpty( portStr ))
            {
                 message = "请设置端口";
                return false;
            }
            int port = 0;
            if (int.TryParse( portStr , out port) == false)
            {
                 message = "端口值错误!";
                return false;
            }
            if (port < System.Net.IPEndPoint.MinPort || port > System.Net.IPEndPoint.MaxPort)
            {
                 message = "请输入[" + System.Net.IPEndPoint.MinPort + "-" + System.Net.IPEndPoint.MaxPort + "]之间的端口。";
                return false;
            }
            return true;
        }
    }
}
