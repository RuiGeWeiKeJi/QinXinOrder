using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Collections;

namespace Utility
{
    /// <summary>
    /// HTTP请求处理类
    /// </summary>
    public class HttpUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpUtil()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 500;
            System.Net.ServicePointManager.Expect100Continue = false;
        }
        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="responseContent"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpStatusCode Post(string url, string data, string contentType, string accept, string method, ref string responseContent, ref WebHeaderCollection headers)
        {
            return Post(null, url, data, contentType, accept, method, true, ref responseContent, ref headers);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="responseContent"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpStatusCode Post(string url, string data, string contentType, string method, ref string responseContent, ref WebHeaderCollection headers)
        {
            return Post(null, url, data, contentType, "", method, true, ref responseContent, ref headers);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="responseContent"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpStatusCode Post(CookieContainer cookie, string url, string data, string contentType, string method, ref string responseContent, ref WebHeaderCollection headers)
        {
            return Post(cookie, url, data, contentType, "", method, true, ref responseContent, ref headers);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="responseContent"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpStatusCode Post(CookieContainer cookie, string url, string data, string contentType, string accecpt, string method, bool allowAutoRedirect, ref string responseContent, ref WebHeaderCollection headers)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                //request.Proxy = null;
                request.Method = method;
                request.Timeout = 15000;
                request.AllowAutoRedirect = allowAutoRedirect;
                request.KeepAlive = false;
                request.CookieContainer = cookie;

                request.ServicePoint.Expect100Continue = false;
                //request.Headers[HttpRequestHeader.CacheControl] = "no-cache";
                //request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

                if (string.IsNullOrEmpty(accecpt) == false)
                {
                    request.Accept = accecpt;
                }
                if (string.IsNullOrEmpty(contentType) == false)
                {
                    request.ContentType = contentType;
                }

                if (string.IsNullOrEmpty(data) == false)
                {
                    byte[] buffer = UTF8Encoding.UTF8.GetBytes(data);
                    request.ContentLength = buffer.Length;
                    Stream requeststream = request.GetRequestStream();
                    requeststream.Write(buffer, 0, buffer.Length);
                    requeststream.Flush();
                    requeststream.Close();
                }
                response = request.GetResponse() as HttpWebResponse;

                Stream responseStream = response.GetResponseStream();
                HttpStatusCode statuscode = response.StatusCode;
                if (statuscode == HttpStatusCode.OK || statuscode == HttpStatusCode.Created)
                {
                    using (StreamReader reader = new StreamReader(responseStream, System.Text.UTF8Encoding.UTF8))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                    headers = response.Headers;
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }

                return statuscode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
            }
        }


        /// <summary>
        /// 上传文件 
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="fileParameter"></param>
        /// <param name="serverFileName"></param>
        /// <param name="fileType"></param>
        /// <param name="timeout"></param>
        /// <param name="localFilePath"></param>
        /// <param name="allowWriteStreamBuffering"></param>
        /// <param name="accept"></param>
        /// <param name="method"></param>
        /// <param name="userAgent"></param>
        /// <param name="allowAutoRedirect"></param>
        /// <param name="responseContent"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpStatusCode PostFile(CookieContainer cookie, string url, System.Collections.Hashtable formData,
            string fileParameterName , string serverFileName ,  string fileType , int timeout ,
            string localFilePath , bool allowWriteStreamBuffering , string accept, string method,
            string userAgent, bool allowAutoRedirect, ref string responseContent, ref WebHeaderCollection headers)
        {

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            FileStream fs = null;
            BinaryReader reader = null;
            try
            {
                //判断文件是否存在
                if (File.Exists(localFilePath) == false)
                {
                    return HttpStatusCode.ExpectationFailed;
                }

                fs = new FileStream(localFilePath, System.IO.FileMode.Open ,  FileAccess.Read);
                //时间戳
                string boundary = DateTime.Now.Ticks.ToString("x");
                byte[] boundaryBytes = Encoding.UTF8.GetBytes("\r\n------" + boundary + "--\r\n");
                //表单数据
                string formDataString = string.Empty;
                byte[] formDataBytes = null;
                if (formData != null || formData.Count > 0)
                {
                    foreach (DictionaryEntry entry in formData)
                    {
                        string key = entry.Key.ToString();
                        string value = entry.Value ==null? string.Empty : entry.Value.ToString();
                        string nv = string.Format("------{0}\r\nContent-Disposition:form-data;name=\"{1}\"\r\n\r\n{2}\r\n", boundary, key, value);
                        formDataString += nv;
                    }
                }
                if (string.IsNullOrEmpty(formDataString) == false)
                {
                    formDataBytes = Encoding.UTF8.GetBytes(formDataString);
                }
                //文件参数数据
                string fileDataString = string.Format("------{0}\r\nContent-Disposition:form-data;name=\"{1}\";filename=\"{2}\"\r\nContent-Type:{3}\r\n\r\n",
                    boundary, fileParameterName , serverFileName, fileType);
                byte[] fileDataBytes = Encoding.UTF8.GetBytes(fileDataString); 

                request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = method;
                request.Timeout = timeout;
                request.AllowAutoRedirect = allowAutoRedirect;
                request.KeepAlive = false;
                request.AllowWriteStreamBuffering = allowWriteStreamBuffering;
                request.CookieContainer = cookie;
                //计算 请求流的总数据长度
                long length = (formDataBytes ==null ? 0 : formDataBytes.Length ) + fileDataBytes.Length + fs.Length + boundaryBytes.Length;                 
                request.ContentLength = length;

                string contentType = "multipart/form-data;boundary=----" + boundary;
                request.ContentType = contentType;
                if (string.IsNullOrEmpty(accept) == false)
                {
                    request.Accept = accept;
                }                
                
                if (string.IsNullOrEmpty(userAgent) == false)
                {
                    request.UserAgent = userAgent; //"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                }
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                //获得请求流
                Stream requeststream = request.GetRequestStream();  
                //发送 formData 表单数据
                if (formDataBytes != null && formDataBytes.Length > 0)
                {
                    requeststream.Write(formDataBytes, 0, formDataBytes.Length);
                }
                //发送 文件参数 数据
                requeststream.Write(fileDataBytes, 0, fileDataBytes.Length);
                //发送 文件流数据
                reader = new BinaryReader(fs);
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];
                int size = reader.Read(buffer, 0, bufferLength);
                int offset = 0;
                while (size > 0)
                {
                    requeststream.Write(buffer, 0, size);
                    offset += size;
                    size = reader.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳
                requeststream.Write(boundaryBytes, 0, boundaryBytes.Length);
                //关闭请求流
                requeststream.Close();

                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }

                //获取服务端的响应
                response = request.GetResponse() as HttpWebResponse;
                //获得响应流
                Stream responseStream = response.GetResponseStream();
                HttpStatusCode statuscode = response.StatusCode;
                if (statuscode == HttpStatusCode.OK || statuscode == HttpStatusCode.Created)
                {
                    using (StreamReader responseReader = new StreamReader(responseStream, System.Text.UTF8Encoding.UTF8))
                    {
                        responseContent = responseReader.ReadToEnd();
                    }
                    headers = response.Headers;
                }
                //关闭响应流
                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }
                return statuscode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
            }
        }
    }
}
