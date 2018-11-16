using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Utility
{
    public  class ImageZoomByQuality
    {
        /// <summary>
        ///  判断 文件大小是否大于指定的最大值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool JudgeImageSize(string path, int maxSize )
        {
            if (File.Exists(path) == false) return false;
            FileInfo info = new FileInfo(path); 
            double size = info.Length / 1024.00 / 1024.00;
            if (size > 1)
            {
                return true;                  
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="fileSavePath"></param>
        /// <param name="quality"></param>
        /// <param name="imageFormat"></param>
        public static void Zoom(Image sourceImage, string fileSavePath , int quality , string imageFormat)
        {
            //创建目录
            string dir = Path.GetDirectoryName(fileSavePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //缩略图对象
            Image resultImage = new Bitmap(sourceImage.Width , sourceImage.Height );
            Graphics resultG = Graphics.FromImage(resultImage);
            //设置质量
            resultG.InterpolationMode = InterpolationMode.HighQualityBicubic;
            resultG.SmoothingMode = SmoothingMode.HighQuality;
            //用指定背景色清空画布
            resultG.Clear(Color.White);
            //绘制缩略图

            Rectangle sRect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            Rectangle dRect = sRect;

            resultG.DrawImage( sourceImage , dRect, sRect, System.Drawing.GraphicsUnit.Pixel);

            //关键质量控制
            //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
            ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders(); 
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo i in icis)
            {
                //if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                if( i.MimeType == imageFormat )      
                {
                    ici = i;
                }
            }
            EncoderParameters ep = new EncoderParameters(1);
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

            //保存缩略图
            resultImage.Save(fileSavePath , ici, ep);

            //释放关键质量控制所用资源
            ep.Dispose();

            //释放缩略图资源
            resultG.Dispose();
            resultImage.Dispose();

            //释放原始图片资源
            //initImage.Dispose();
        }
    }
}
