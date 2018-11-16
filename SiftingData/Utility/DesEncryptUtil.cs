﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public class DesEncryptUtil
    {
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// 
        /// DES加密字符串
        /// 
        /// 待加密的字符串
        /// 加密密钥,要求为8位
        /// 加密成功返回加密后的字符串，失败返回源串
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        /// 
        /// DES解密字符串
        /// 
        /// 待解密的字符串
        /// 解密密钥,要求为8位,和加密密钥相同
        /// 解密成功返回解密后的字符串，失败返源串
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }


        /// <summary>
        /// MD5 加密字符串
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string EncryptMD5(string encryptString)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes ( encryptString );
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] outs = md5.ComputeHash(bytes);

            string result = string.Empty;
            for (int i = 0; i < outs.Length; i++)
            {
                result += outs[i].ToString("x");
            }
            return result;
        }
    }
}
