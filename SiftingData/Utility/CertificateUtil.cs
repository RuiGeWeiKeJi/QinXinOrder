using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace Utility
{
    /// <summary>
    /// 证书操作类
    /// </summary>
    public class CertificateUtil
    {
        /// <summary>
        /// 通过序列号检测证书是否安装
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static bool CheckCertificateBySerialNumber(string serialNumber )
        {
            return CheckCertificate(X509FindType.FindBySerialNumber, serialNumber);
        }
        /// <summary>
        /// 通过主题名称检测证实是否安装
        /// </summary>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        public static bool CheckCertificateBySubjectName(string subjectName)
        {
            return CheckCertificate(X509FindType.FindBySubjectName, subjectName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="findType"></param>
        /// <param name="findName"></param>
        /// <returns></returns>
        protected static bool CheckCertificate( X509FindType findType , string findName)
        {
            try
            {
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.MaxAllowed);
                X509Certificate2Collection certs = store.Certificates.Find(findType, findName, false);
                if (certs.Count == 0 || certs[0].NotAfter < DateTime.Now)
                {
                    return false;
                }
                return true;
            }
            catch ( Exception ex )
            {
                LogHelper.WriteException(ex);
                return false;
            }
        }
        /// <summary>
        /// 安装CA的根证书到受信任根证书颁发机构
        /// </summary>
        /// <param name="certificatePath">证书路径</param>
        /// <returns></returns>
        public static bool SetupCertificate(string certificatePath)
        {
            try
            {
                if (System.IO.File.Exists(certificatePath) == false)
                {
                    LogHelper.WriteLog("证书文件不存在,无法安装。" + certificatePath);
                    return false;
                }
                X509Certificate2 certificate = new X509Certificate2(certificatePath);
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                store.Remove(certificate);   //可省略
                store.Add(certificate);
                store.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                return false;
            }
        }
    }
}
