using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;

namespace Utility
{
    /// <summary>
    /// 监听网络状态
    /// </summary>
    public sealed class NetworkMonitor
    {
        private static object AsyLook1 = new object();
        /// <summary>
        /// 监听网址_1
        /// </summary>
        public string MonitorUrl_1 = "";
        /// <summary>
        /// 监听计时器
        /// </summary>
        private Timer listenTimer;

        private int _tryCount = 0;

        private static NetworkMonitor instance;
        /// <summary>
        /// 监听间隔 ( 15 S )
        /// </summary>
        const int LISTEN_TIME_SPAN = 1000 * 15;
        const int NETWORK_ALIVE_LAN = 1;
        const int NETWORK_ALIVE_WAN = 2;
        const int NETWORK_ALIVE_AOL = 4;
        const int FLAG_ICC_FORCE_CONNECTION = 1;
        const int TRYCOUNT = 5;//失败尝试次数

        private NetworkMonitor()
        {
        }

        static NetworkMonitor()
        {
            instance = new NetworkMonitor();
        }

        public static NetworkMonitor GetNetworkMonitorInstance()
        {
            return instance;
        }
        /// <summary>
        /// 互联网是否可用
        /// </summary>
        /// <returns></returns>
        public bool IsInternetAlive()
        {
            int status;
            //检查网络是否可用
            if (NativeMethods.IsNetworkAlive(out status))
            {
                //如果WAN可用，检查能否建立连接
                //if (status == NETWORK_ALIVE_WAN)
                //{
                if (string.IsNullOrEmpty(MonitorUrl_1) == false && NativeMethods.InternetCheckConnection(MonitorUrl_1, FLAG_ICC_FORCE_CONNECTION, 0))
                {
                    return true; //如果能建立连接返回TRUE
                }
                else
                    return false;
                //}
                //else
                //    return false;
            }
            return false;
        }
        /// <summary>
        /// 为NetworkStatusChanged事件处理程序提供数据
        /// </summary>
        public class NetworkChangedEventArgs : EventArgs
        {
            public NetworkChangedEventArgs(bool status)
            {
                IsNetworkAlive = status;
            }
            /// <summary>
            /// 
            /// </summary>
            public bool IsNetworkAlive
            {
                get;
                private set;
            }
        }
        /// <summary>
        /// 表示NetworkStatusChanged事件的方法
        /// </summary>
        public delegate void NetworkChangedEventHandler(object sender, NetworkChangedEventArgs e);
        /// <summary>
        /// 网络状态变更时触发的事件
        /// </summary>
        public event NetworkChangedEventHandler NetworkStatusChanged;
        /// <summary>
        /// 网络状态变更时触发的事件
        /// </summary>
        private void OnNetworkStatusChanged(NetworkChangedEventArgs e)
        {
            if (NetworkStatusChanged != null)  NetworkStatusChanged(this, e);
        }
        /// <summary>
        /// 监听网络状态
        /// </summary>
        public void ListenNetworkStatus(SynchronizationContext context)
        {
            //启动监听网络状态，15秒钟检查一次，当状态变更时触发事件
            _tryCount = 0;
            listenTimer = new Timer(new TimerCallback(RunCall), null, LISTEN_TIME_SPAN, LISTEN_TIME_SPAN);
        }

        public void RunCall(object obj)
        {
            try
            {
                lock (AsyLook1)
                {
                    bool tmpStatus = IsInternetAlive();
                    if (tmpStatus == false)
                    {
                        _tryCount++;
                        LogHelper.WriteLog("网络出现异常或网站无法访问,尝试次数 " + _tryCount + "次。");
                        if (_tryCount >= TRYCOUNT)
                        {
                            OnNetworkStatusChanged(new NetworkChangedEventArgs(tmpStatus));
                        }
                    }
                    else
                    {
                        if (_tryCount > 0)
                        {
                            LogHelper.WriteLog("网络恢复正常。");
                        }
                        _tryCount = 0;
                        OnNetworkStatusChanged(new NetworkChangedEventArgs(tmpStatus));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
        }
        /// <summary>
        /// 停止监听网络状态
        /// </summary>
        public void CloseListenNetworkStatus()
        {
            _tryCount = 0;
            if (listenTimer == null) return;
            listenTimer.Dispose();
        }
    }
}