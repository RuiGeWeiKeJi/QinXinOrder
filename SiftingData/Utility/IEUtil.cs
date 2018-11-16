using System;
using System.Collections.Generic;
using System.Text;


namespace Utility
{
    public class IEUtil
    {
        /// <summary>
        /// 获得IE浏览器的版本号
        /// </summary>
        /// <returns></returns>
        public Version GetIEVersion()
        {
            try
            {
                RegisterUtil util = new RegisterUtil("software\\Microsoft\\Internet Explorer", RegDomain.LocalMachine);
                util.CreateSubKey();

                util.RegeditKey = "Version";
                Version version = null;
                if (util.IsRegeditKeyExist() == true)
                {
                    string versionStr = util.ReadRegeditKey().ToString();
                    version = new Version(versionStr);
                }

                util.RegeditKey = "svcVersion";
                Version svcVersion = null;
                if (util.IsRegeditKeyExist() == true)
                {
                    string svcVersionStr = util.ReadRegeditKey().ToString();
                    svcVersion = new Version(svcVersionStr);
                }

                Version newVersion = version;
                if (version == null && svcVersion != null) newVersion = svcVersion;
                else if (version != null && svcVersion == null) newVersion = version;
                else if (version != null && svcVersion != null && version.Major >= svcVersion.Major) newVersion = version;
                else if (version != null && svcVersion != null && version.Major < svcVersion.Major) newVersion = svcVersion;

                return newVersion;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException( ex );
                return null;
            }
        }
        /// <summary>
        /// 设置WebBrowser控件的IE兼容模式
        /// </summary>
        /// <param name="exeName"></param>
        /// <param name="value"></param>
        protected void SetWebBrowserIEMode( string exeName , int value)
        {
            try
            {
                Utility.RegisterUtil util = new Utility.RegisterUtil();
                util.SubKey = "SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";
                util.Domain = Utility.RegDomain.LocalMachine;
                util.RegeditKey = exeName;
                util.CreateSubKey();
                if (util.IsRegeditKeyExist() == false)
                {
                    bool isok = util.WriteRegeditKey(value);//(0x1f40);
                }
                else
                {
                    object obj = util.ReadRegeditKey();
                    if (obj != null)
                    {
                        string temp = obj.ToString();
                        if(temp.Equals ( value.ToString())) return ;
                    }
                   
                    util.WriteRegeditKey(value);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
        }
        /// <summary>
        /// 设置WebBrowser控件的IE浏览器的模式
        /// </summary>
        /// <param name="version"></param>
        /// <param name="exeName"></param>
        public void SetWebBrowserIEMode( string exeName )
        {
            Version version = GetIEVersion();
            if (version == null) return;
 
            if (version.Major == 8)
            {
                //SetWebBrowserIEMode(exeName, 0x1f40);
                SetWebBrowserIEMode(exeName, 0x22B8);  //IE8强制模式
            }
            else if (version.Major == 9)
            {
                SetWebBrowserIEMode(exeName, 0x2328);
            }
            else if (version.Major == 10)
            {
                SetWebBrowserIEMode(exeName, 0x2710);
            }
            else if (version.Major == 11)
            {
                SetWebBrowserIEMode(exeName, 0x270f);//IE9模式
            }
        }
    }
}
