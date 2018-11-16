using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Utility
{
    /// <summary>
    /// 图像处理类
    /// </summary>
    public class ImageUtil
    {
        /// <summary>
        /// 获得默认头像图片
        /// </summary>
        /// <returns></returns>
        public static Image GetDoctorDefaultImage()
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\Images\\Heads\\doctordefault.jpg";
            string path2 = System.Windows.Forms.Application.StartupPath + "\\Images\\Heads\\doctordefault.png";
            if (File.Exists(path2) == true)
            {
                Image bmp = Bitmap.FromFile(path2);
                return bmp;
            }
            if (File.Exists(path) == true)
            {
                Image bmp = Bitmap.FromFile(path);
                return bmp;
            }
            return null;
        }
        /// <summary>
        /// 图像的灰度化
        /// gray=(r*19595 + g*38469 + b*7472) >> 16;
        /// </summary>
        public static Image GrayImage(Image image)
        {
            if (image == null) return null;
            Bitmap bmp = new Bitmap(image);
            Bitmap newbmp = new Bitmap(image);
            Color c = new Color();
            Color NewC;
            Byte r, g, b, gray;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    r = c.R;
                    g = c.G;
                    b = c.B;
                    gray = (Byte)((r * 19595 + g * 38469 + b * 7472) >> 16);
                    NewC = Color.FromArgb(gray, gray, gray);
                    newbmp.SetPixel(i, j, NewC);
                }
            }
            return newbmp;
        }
        /// <summary>
        /// 通过图片名称获得图片
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static Image GetImage(string imageName)
        {
            string path = System.Windows.Forms.Application.StartupPath;
            path += path.EndsWith(Path.DirectorySeparatorChar.ToString() ) ? "Images" : Path.DirectorySeparatorChar + "Images";   
            return GetImage(path, imageName);           
        }
        /// <summary>
        /// 通过图片文件夹和图片名称获得图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static Image GetImage(string path, string imageName)
        {
            path += path.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" : Path.DirectorySeparatorChar.ToString();              
            string imagePath = path + imageName;
            if (File.Exists(imagePath))
            {
                return Image.FromFile(imagePath);
            }
            return null;
        }

        public static byte[] ImageToBytes(Image image, ref string imageFormat)
        {
            imageFormat = "";
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                    imageFormat = ".jpeg";
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                    imageFormat = ".png";
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                    imageFormat = ".bmp";
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                    imageFormat = ".gif";
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                    imageFormat = ".icon";
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// Convert Byte[] to Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

    }
}
