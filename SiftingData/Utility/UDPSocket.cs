using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace Utility
{
    /// <summary> 
    /// 名称:UDP通讯类 
    /// </summary> 
    public class UDPSocket
    {       
        /// <summary> 
        /// 发送命令文本常量 
        /// </summary> 
        private string m_sendText;
        /// <summary> 
        /// 默认发送的字符串 
        /// </summary> 
        private const string m_sendStr = "Test";
        /// <summary> 
        /// Udp对象 
        /// </summary> 
        private UdpClient m_Client;        
        /// <summary> 
        /// 本地通讯端口(默认8888) 
        /// </summary> 
        private int m_LocalPort;
        /// <summary> 
        /// 对方IP 
        /// </summary> 
        private string m_SendToIP;
        /// <summary> 
        /// 远程通讯端口 
        /// </summary> 
        private int m_RemotePort;
        /// <summary> 
        /// 跟踪是否退出程序 
        /// </summary> 
        private bool m_Done;
        /// <summary> 
        /// 定义一个接受线程 
        /// </summary> 
        public Thread recvThread;
        /// <summary> 
        /// 定义一个检测发送线程 
        /// </summary> 
        public Thread checkSendThread;
        /// <summary>
        /// 定义一个字符串编码
        /// </summary>
        protected Encoding m_Encoding = Encoding.UTF8;
        /// <summary> 
        /// 定义委托 
        /// </summary> 
        public delegate void SOCKETDelegateArrive(string sReceived);
        /// <summary> 
        /// 定义一个消息接收事件 
        /// </summary> 
        public event SOCKETDelegateArrive SOCKETEventArrive;
        /// <summary> 
        /// 设置对方IP地址 
        /// </summary> 
        public string SendToIP
        {
            set { m_SendToIP = value; }
            get { return m_SendToIP; }
        }
        /// <summary>
        /// 远程通讯端口
        /// </summary>
        public int RemotePort
        {
            get { return m_RemotePort; }
            set { m_RemotePort = value; }
        }
        /// <summary> 
        /// 设置本地监听端口 
        /// </summary> 
        public int LocalPort
        {
            set { m_LocalPort = value; }
            get { return m_LocalPort; }
        }
        /// <summary>
        /// 设置字符串编码
        /// </summary>
        public Encoding Encoding
        {
            get { return m_Encoding; }
            set { m_Encoding = value; }
        }
        /// <summary> 
        /// 断开接收  
        /// </summary> 
        public bool Done
        {
            set { m_Done = value; }
            get { return m_Done; }
        }
        /// <summary> 
        /// 构造函数设置各项默认值 
        /// </summary> 
        public UDPSocket()
        {
            m_sendText = string.Empty;
            m_Done = false;
            m_LocalPort = 8888;
            m_RemotePort = 8888;
        }
        /// <summary> 
        /// 析构函数 
        /// </summary> 
        ~UDPSocket() { Dispose(); }        
        /// <summary> 
        /// 关闭对象 
        /// </summary> 
        public void Dispose()
        {
            DisConnection();
        }        
        /// <summary> 
        /// 初始化 
        /// </summary> 
        public void Init()
        {
            //初始化UDP对象 
            try
            {
                m_Client = new UdpClient(m_LocalPort);

                OnSOCKETEventArrive("Initialize succeed by " + m_LocalPort.ToString() + " port");
            }
            catch
            {
                OnSOCKETEventArrive("Initialize failed by " + m_LocalPort.ToString() + " port");
            }
        }        
        /// <summary> 
        /// 关闭UDP对象 
        /// </summary> 
        public void DisConnection()
        {
            if (m_Client != null)
            {
                this.Done = true;
                if (recvThread != null)
                {
                    this.recvThread.Abort();
                }
                if (checkSendThread != null)
                {
                    this.checkSendThread.Abort();
                }
                m_Client.Close();
                m_Client = null;
                OnSOCKETEventArrive("UDP Object Closed");
            }
        }
        /// <summary> 
        /// 向IP发送消息 自定义消息体 
        /// </summary> 
        public int send(string ip, string strBody)
        {
            this.SendToIP = ip;
            this.m_sendText = strBody;
            return sendCmd();
        }
        /// <summary> 
        /// 向IP发送默认字符串 
        /// </summary> 
        /// <param name="ip"></param> 
        public int send(string ip)
        {
            this.SendToIP = ip;
            this.m_sendText = m_sendStr;
            return sendCmd();
        }
        /// <summary> 
        /// 发送已知IP默认字符串
        /// </summary> 
        public int send()
        {
            this.m_sendText = m_sendStr;
            return sendCmd();
        }        
        /// <summary> 
        /// 发送 
        /// </summary> 
        private int sendCmd()
        {
            UdpClient udp = new UdpClient();
            try
            {
                udp.Connect(this.m_SendToIP, m_RemotePort);
                // 连接后传送一个消息给ip主机 
                Byte[] sendBytes = m_Encoding.GetBytes(this.m_sendText); 
                int sendlength = udp.Send(sendBytes, sendBytes.Length);

                OnSOCKETEventArrive("IP:" + m_SendToIP + " Port:" + m_RemotePort + " Send:" + m_sendText + " succeed");
                return sendlength;
            }
            catch
            {
                OnSOCKETEventArrive("IP:"+ m_SendToIP +" Port:"+ m_RemotePort +" SendText:" + m_sendText + " failed。");
                return 0;
            }
            finally
            {
                udp.Close();
                udp = null;
            }
        }        
        /// <summary> 
        /// 侦听线程 
        /// </summary> 
        public void StartRecvThreadListener()
        {
            try
            {
                // 启动等待连接的线程 
                recvThread = new Thread(new ThreadStart(Received));
                recvThread.Priority = ThreadPriority.Normal;
                recvThread.Start();
                OnSOCKETEventArrive("[Received]Thread Start....");
            }
            catch
            {
                OnSOCKETEventArrive("[Received]Thread Start failed!");
            }
        }        
        /// <summary> 
        /// 循环接收 
        /// </summary> 
        private void Received()
        {
            Thread.Sleep(1000);
            while (!m_Done)
            {
                IPEndPoint endpoint = null;
                if (m_Client != null && recvThread.IsAlive)
                {
                    //接收数据   
                    try
                    {
                        Byte[] data = m_Client.Receive(ref endpoint);
                        //得到数据的ACSII的字符串形式 
                        String strData = m_Encoding.GetString(data);
                        OnSOCKETEventArrive( strData);
                    }
                    catch
                    {
                        OnSOCKETEventArrive("receive:Nullerror"); 
                    }
                }
                Thread.Sleep(50); //防止系统资源耗尽 
            }
        }

        protected void OnSOCKETEventArrive( string msg )
        {
            if (SOCKETEventArrive != null)
            {
                SOCKETEventArrive(msg);
            }
        }
    }
}

