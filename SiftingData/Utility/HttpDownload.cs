using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Utility
{
    /// <summary>
    /// HTTP下载类
    /// </summary>
    public class HttpDownload
    {
        public delegate void DownloadProgressChangedHandler(int precent , object tag );
        public delegate void DownloadProgressCompletedHandler(Exception ex , bool isok , object tag );
        public event DownloadProgressChangedHandler DownloadProgress = null;
        public event DownloadProgressCompletedHandler DownloadCompleted = null;
        ///
        /// 下载文件方法 支持断点续传
        ///
        /// 文件保存路径和文件名
        /// 服务器文件名   
        public bool DownloadFile( CookieContainer cookie , string strFileName, string fileUrl)
        {
            bool flag = false;
            //打开上次下载的文件
            long SPosition = 0;
            //实例化流对象
            FileStream FStream;
            //判断要下载的文件是否存在
            if (File.Exists(strFileName))
            {
                //打开要下载的文件
                FStream = File.OpenWrite(strFileName);
                //获取已经下载的长度
                SPosition = FStream.Length;
                FStream.Seek(SPosition, SeekOrigin.Current);
                //File.Delete(strFileName);
            }
            else
            {
                //文件不保存创建一个文件
                FStream = new FileStream(strFileName, FileMode.Create);
                SPosition = 0;
            }

            HttpWebRequest myRequest = null;
            HttpWebResponse myResponse = null;
            try
            {
                //打开网络连接
                myRequest = (HttpWebRequest)HttpWebRequest.Create(GetUrl(fileUrl));
                //myRequest.AllowAutoRedirect = true;
                myRequest.CookieContainer = cookie;
                //OnDownloadProgress(SPosition, total ,strFileName );

                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                myResponse = myRequest.GetResponse() as HttpWebResponse;
                //myResponse.Headers.Add("content-reanghe", SPosition.ToString() );
                long total = myResponse.ContentLength;
                Stream myStream = myResponse.GetResponseStream();

                //定义一个字节数据
                int bufflength = 1024;
                byte[] btContent = new byte[bufflength];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, bufflength);

                SPosition += intSize;
                OnDownloadProgress(SPosition, total, strFileName);

                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, bufflength);

                    SPosition += intSize;
                    OnDownloadProgress(SPosition, total, strFileName);
                }
                
                myStream.Close();
                flag = true;        //返回true下载成功

                OnDownloadCompleted(null, true, null);
            }
            catch (Exception ex)
            {
                flag = false;       //返回false下载失败
                OnDownloadCompleted(ex, false, strFileName);
            }
            finally
            {
                //关闭流
                if (FStream != null)
                {
                    FStream.Close();
                    FStream = null;
                }
                if (myResponse != null)
                {
                    myResponse.Close();
                    myResponse = null;
                }
                if (myRequest != null)
                {
                    myRequest.Abort();
                    myRequest = null;
                }
            }
            return flag;
        }

        public bool DownloadFile( string strFileName, string fileUrl)
        {
            return DownloadFile(null, strFileName, fileUrl);
        }

        protected void OnDownloadProgress( long current , long total , object tag )
        {
            if (DownloadProgress != null)
            {
                int precent = (int) (current * 100 / total);
                DownloadProgress(precent , tag);
            }
        }
        protected void OnDownloadCompleted( Exception ex , bool isok , object obj )
        {
            if (DownloadCompleted != null)
            {
                DownloadCompleted ( ex, isok , obj );
            }
        }

        public static string GetUrl(string URL)
        {
            if (!(URL.Contains("http://") || URL.Contains("https://")))
            {
                URL = "http://" + URL;
            }
            return URL;
        }
    }
}
