using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class ByteUtil
    {
        public  static string ByteToString(byte[] buffer)
        {
            string temp = "";
            if (buffer != null)
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    temp += buffer[i].ToString("X2");
                }
            }
            return temp;
        }

        public static byte[] StringToByte(string txt)
        {
            txt = txt.Replace(" ", "");
            if ((txt.Length % 2) != 0)
                txt += " ";
            byte[] returnBytes = new byte[txt.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(txt.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
