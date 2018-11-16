using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class UrlEncodeUtility
    {
        public static string Encode(string str)
        {
            if (str == null)
            {
                return null;
            }
            return Encode(str, Encoding.UTF8);
        }

        public static string Encode(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            return Encoding.ASCII.GetString(UrlEncodeToBytes(str, e));
        }

        internal static byte[] UrlEncodeToBytes(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            byte[] bytes = e.GetBytes(str);
            return Encode(bytes, 0, bytes.Length, false);
        }

        internal static byte[] Encode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
        {
            byte[] buffer = Encode(bytes, offset, count);
            if ((alwaysCreateNewReturnValue && (buffer != null)) && (buffer == bytes))
            {
                return (byte[])buffer.Clone();
            }
            return buffer;
        }

        internal static byte[] Encode(byte[] bytes, int offset, int count)
        {
            if (!ValidateUrlEncodingParameters(bytes, offset, count))
            {
                return null;
            }
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];
                if (ch == ' ')
                {
                    num++;
                }
                else if (!IsUrlSafeChar(ch))
                {
                    num2++;
                }
            }
            if ((num == 0) && (num2 == 0))
            {
                return bytes;
            }
            byte[] buffer = new byte[count + (num2 * 2)];
            int num4 = 0;
            for (int j = 0; j < count; j++)
            {
                byte num6 = bytes[offset + j];
                char ch2 = (char)num6;
                if (IsUrlSafeChar(ch2))
                {
                    buffer[num4++] = num6;
                }
                else if (ch2 == ' ')
                {
                    buffer[num4++] = 0x2b;
                }
                else
                {
                    buffer[num4++] = 0x25;
                    buffer[num4++] = (byte)IntToHex((num6 >> 4) & 15);
                    buffer[num4++] = (byte)IntToHex(num6 & 15);
                }
            }
            return buffer;
        }

        internal static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
        {
            if ((bytes == null) && (count == 0))
            {
                return false;
            }
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if ((offset < 0) || (offset > bytes.Length))
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if ((count < 0) || ((offset + count) > bytes.Length))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            return true;
        }

        public static bool IsUrlSafeChar(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }

        public static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 0x30);
            }
            return (char)((n - 10) + 0x61);
        }

    }

    public class UrlDecoder
    {
        // Fields
        private int _bufferSize;
        private byte[] _byteBuffer;
        private char[] _charBuffer;
        private Encoding _encoding;
        private int _numBytes;
        private int _numChars;

        // Methods
        internal UrlDecoder(int bufferSize, Encoding encoding)
        {
            this._bufferSize = bufferSize;
            this._encoding = encoding;
            this._charBuffer = new char[bufferSize];
        }

        internal void AddByte(byte b)
        {
            if (this._byteBuffer == null)
            {
                this._byteBuffer = new byte[this._bufferSize];
            }
            this._byteBuffer[this._numBytes++] = b;
        }

        internal void AddChar(char ch)
        {
            if (this._numBytes > 0)
            {
                this.FlushBytes();
            }
            this._charBuffer[this._numChars++] = ch;
        }

        private void FlushBytes()
        {
            if (this._numBytes > 0)
            {
                this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
                this._numBytes = 0;
            }
        }

        internal string GetString()
        {
            if (this._numBytes > 0)
            {
                this.FlushBytes();
            }
            if (this._numChars > 0)
            {
                return new string(this._charBuffer, 0, this._numChars);
            }
            return string.Empty;
        }

        public static int HexToInt(char h)
        {
            if ((h >= '0') && (h <= '9'))
            {
                return (h - '0');
            }
            if ((h >= 'a') && (h <= 'f'))
            {
                return ((h - 'a') + 10);
            }
            if ((h >= 'A') && (h <= 'F'))
            {
                return ((h - 'A') + 10);
            }
            return -1;
        }

        public static string UrlDecode(string value, Encoding encoding)
        {
            if (value == null)
            {
                return null;
            }
            int length = value.Length;
            UrlDecoder decoder = new UrlDecoder(length, encoding);
            for (int i = 0; i < length; i++)
            {
                char ch = value[i];
                if (ch == '+')
                {
                    ch = ' ';
                }
                else if ((ch == '%') && (i < (length - 2)))
                {
                    if ((value[i + 1] == 'u') && (i < (length - 5)))
                    {
                        int num3 = HexToInt(value[i + 2]);
                        int num4 = HexToInt(value[i + 3]);
                        int num5 = HexToInt(value[i + 4]);
                        int num6 = HexToInt(value[i + 5]);
                        if (((num3 < 0) || (num4 < 0)) || ((num5 < 0) || (num6 < 0)))
                        {
                            goto Label_010B;
                        }
                        ch = (char)((((num3 << 12) | (num4 << 8)) | (num5 << 4)) | num6);
                        i += 5;
                        decoder.AddChar(ch);
                        continue;
                    }
                    int num7 = HexToInt(value[i + 1]);
                    int num8 = HexToInt(value[i + 2]);
                    if ((num7 >= 0) && (num8 >= 0))
                    {
                        byte b = (byte)((num7 << 4) | num8);
                        i += 2;
                        decoder.AddByte(b);
                        continue;
                    }
                }
            Label_010B:
                if ((ch & 0xff80) == 0)
                {
                    decoder.AddByte((byte)ch);
                }
                else
                {
                    decoder.AddChar(ch);
                }
            }
            return decoder.GetString();
        }
    }
}

   

