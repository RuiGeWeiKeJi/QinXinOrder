using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Windows .Forms;

namespace Utility
{
    public class WebRequestEx : WebRequest
    {
        string iniPath = Application.StartupPath +"\\Config.ini";
        public WebRequestEx()
        {
            SetProxyInfo();
        }

        protected void SetProxyInfo()
        {
            string type = IniUtil.ReadIniValue(iniPath, "NetConfig", "Type");
            string proxyip = IniUtil.ReadIniValue(iniPath, "NetConfig", "IP");
            string portstr = IniUtil.ReadIniValue(iniPath, "NetConfig", "Port");
            int port = 0;
            int.TryParse(portstr, out port);
            string user = IniUtil.ReadIniValue(iniPath, "NetConfig", "User");
            string password = IniUtil.ReadIniValue(iniPath, "NetConfig", "Passsword");
            if( string.IsNullOrEmpty( type ) || type.Equals("不用代理")) return;

            this.Proxy = new WebProxy(proxyip, port);
            this.Proxy.Credentials = new NetworkCredential(user, password);
            
        }
    }
}
