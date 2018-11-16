using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Utility
{
    public class FileUtil
    {
        /// <summary>
        /// 文件夹复制
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="objPath"></param>
        public static void CopyDirectory(string sourceDirectory, string desDirectory, bool copySubDirectory)
        {
            if (Directory.Exists(sourceDirectory) == false) return;
            if (Directory.Exists(desDirectory) == false) Directory.CreateDirectory(desDirectory);
            string[] directorys = Directory.GetDirectories(sourceDirectory);
            if (directorys != null && directorys.Length > 0 && copySubDirectory)
            {
                foreach (string dir in directorys)
                {
                    string subDir = (desDirectory.EndsWith("\\") ? desDirectory : desDirectory + "\\") + GetDirectoryName(dir);//Path.GetDirectoryName(dir);
                    CopyDirectory(dir, subDir, copySubDirectory);
                }
            }

            string[] files = Directory.GetFiles(sourceDirectory);
            if (files == null || files.Length < 1) return;
            for (int i = 0; i < files.Length; i++)
            {
                string desfile = ( desDirectory.EndsWith("\\") ? desDirectory : desDirectory + @"\" ) + Path.GetFileName(files[i]);
                File.Copy(files[i], desfile, true);
            }
        }
        /// <summary>
        /// 根据路径字符串获得文件夹名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected static string GetDirectoryName(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            string[] items = path.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            if (items == null || items.Length < 1) return string.Empty;
            return items[items.Length - 1].Trim();
        }
        /// <summary>
        /// 检测文件夹路径是否存在，不存在则创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CheckDirectoryExist(string path)
        {
            if (string.IsNullOrEmpty(path)) return;  
            Directory.CreateDirectory(path);
        }    
        /// <summary>
        /// 删除 指定文件夹下的创建日期大于指定天数的文件
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFile(string dir, int days)
        {
            try
            {
                if (Directory.Exists(dir) == false) return;
                string[] files = Directory.GetFiles(dir);
                if (files.Length < 1) return;

                System.Collections.IEnumerator ienum = files.GetEnumerator();
                while (ienum.MoveNext())
                {
                    object obj = ienum.Current;
                    string f = obj.ToString();
                    DateTime createTime = File.GetCreationTime(f);
                    if (DateTime.Now.Subtract(createTime).Days > days)
                    {
                        File.Delete(f);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
            }
        }

    }
}
