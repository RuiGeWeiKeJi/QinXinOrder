using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace Utility
{
    /// <summary>
    /// 压缩 工具类
    /// </summary>
    public class ZipUtil
    {
        /// <summary>
        /// 递归压缩文件夹方法
        /// </summary>
        /// <param name="folderToZip"></param>
        /// <param name="zipOutStream"></param>
        /// <param name="parentFolderName"></param>
        private static bool ZipFileDictory(string folderToZip, ZipOutputStream zipOutStream, string parentFolderName)
        {
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();

            try
            {
                //创建当前文件夹
                entry = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/"));  //加上 “/” 才会当成是文件夹创建
                zipOutStream.PutNextEntry(entry);
                zipOutStream.Flush();


                //先压缩文件，再递归压缩文件夹 
                filenames = Directory.GetFiles(folderToZip);
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/" + Path.GetFileName(file)));

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();

                    crc.Reset();
                    crc.Update(buffer);

                    entry.Crc = crc.Value;

                    zipOutStream.PutNextEntry(entry);

                    zipOutStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }


            folders = Directory.GetDirectories(folderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, zipOutStream, Path.Combine(parentFolderName, Path.GetFileName(folderToZip))))
                {
                    return false;
                }
            }

            return res;
        }
        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="folderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="zipedFile">压缩后的文件名，全路径格式</param>
        /// <param name="password">压缩密码</param>
        /// <returns></returns>
        private static bool ZipFileDictory(string folderToZip, string zipedFile, String password)
        {
            bool res;
            if (!Directory.Exists(folderToZip))
            {
                return false;
            }

            ZipOutputStream s = new ZipOutputStream(File.Create(zipedFile));
            s.SetLevel(6);
            s.Password = password;

            res = ZipFileDictory(folderToZip, s, "");

            s.Finish();
            s.Close();
            return res;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToZip">要进行压缩的文件名</param>
        /// <param name="zipedFile">压缩后生成的压缩文件名</param>
        /// <param name="password">压缩密码</param>
        /// <returns></returns>
        private static bool ZipFile(string fileToZip, string zipedFile, String password)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(fileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");
            }
            //FileStream fs = null;
            FileStream ZipFile = null;
            ZipOutputStream ZipStream = null;
            ZipEntry ZipEntry = null;

            bool res = true;
            try
            {
                ZipFile = File.OpenRead(fileToZip);
                byte[] buffer = new byte[ZipFile.Length];
                ZipFile.Read(buffer, 0, buffer.Length);
                ZipFile.Close();

                ZipFile = File.Create(zipedFile);
                ZipStream = new ZipOutputStream(ZipFile);
                ZipStream.Password = password;
                ZipEntry = new ZipEntry(Path.GetFileName(fileToZip));
                ZipStream.PutNextEntry(ZipEntry);
                ZipStream.SetLevel(6);

                ZipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            return res;
        }

        /// <summary>
        /// 压缩文件 和 文件夹
        /// </summary>
        /// <param name="fileToZip">待压缩的文件或文件夹，全路径格式</param>
        /// <param name="zipedFile">压缩后生成的压缩文件名，全路径格式</param>
        /// <param name="password">压缩密码</param>
        /// <returns></returns>
        public static bool Zip(String fileToZip, String zipedFile, String password)
        {
            if (Directory.Exists(fileToZip))
            {
                return ZipFileDictory(fileToZip, zipedFile, password);
            }
            else if (File.Exists(fileToZip))
            {
                return ZipFile(fileToZip, zipedFile, password);
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    ///  解压 工具类
    /// </summary>
    public class UnZipUtil
    {
        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="fileToUpZip">待解压的文件</param>
        /// <param name="zipedFolder">指定解压目标目录</param>
        /// <param name="password">解压密码</param>
        public static void UnZip(string fileToUpZip, string zipedFolder, string password)
        {
            if (!File.Exists(fileToUpZip))
            {
                return;
            }

            if (!Directory.Exists(zipedFolder))
            {
                Directory.CreateDirectory(zipedFolder);
            }

            ZipInputStream s = null;
            ZipEntry theEntry = null;

            string fileName;
            FileStream streamWriter = null;
            try
            {
                s = new ZipInputStream(File.OpenRead(fileToUpZip));
                s.Password = password;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = Path.Combine(zipedFolder, theEntry.Name);
                        ///判断文件路径是否是文件夹
                        if (fileName.EndsWith("/") || fileName.EndsWith("//"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        streamWriter = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
        }
    }
}

