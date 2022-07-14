using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

namespace BmFramework.Core
{
    public class FileHandle
    {

        private FileHandle() { }
        public static readonly FileHandle instance = new FileHandle();

        //the filepath if there is a file
        public bool isExistFile(string filepath)
        {
            return File.Exists(filepath);
        }

        public bool IsExistDirectory(string directorypath)
        {
            return Directory.Exists(directorypath);
        }

        public bool Contains(string Path, string seachpattern)
        {
            try
            {
                string[] fileNames = GetFilenNames(Path, seachpattern, false);
                return fileNames.Length != 0;
            }
            catch
            {
                return false;
            }
        }

        //return a file all rows
        public static int GetLineCount(string filepath)
        {
            string[] rows = File.ReadAllLines(filepath);
            return rows.Length;
        }

        public bool CreateFile(string filepath)
        {
            try
            {
                if (!isExistFile(filepath))
                {
                    StreamWriter sw;
                    FileInfo file = new FileInfo(filepath);
                    sw = file.CreateText();
                    sw.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CreateDirectory(string filepath)
        {
            DirectoryInfo info = Directory.CreateDirectory(filepath);
            return info != null ? true : false;
        }

        public string[] GetFilenNames(string directorypath, string searchpattern, bool isSearchChild)
        {
            if (!IsExistDirectory(directorypath))
            {
                throw new FileNotFoundException();
            }
            try
            {

                return Directory.GetFiles(directorypath, searchpattern, isSearchChild ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            }
            catch
            {
                return null;
            }

        }

        public void WriteText(string filepath, string content)
        {
            File.WriteAllText(filepath, content);
            /*FileStream fs = new FileStream(filepath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();*/
        }

        public void WriteAllBytes(string filepath, byte[] content)
        {
            File.WriteAllBytes(filepath, content);
            // 把 byte[] 写入文件 
            /*FileStream fs = new FileStream(filepath, FileMode.Create); 
            BinaryWriter bw = new BinaryWriter(fs); 
            bw.Write(content); 
            bw.Close(); 
            fs.Close(); */
        }



        public void AppendText(string filepath, string content)
        {
            File.AppendAllText(filepath, content);
        }


        public byte[] ReadAllByte(string filepath)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            return bytes;
        }

        public string ReadAllString(string filepath, Encoding encoding)
        {
            FileStream fs = null;
            StreamReader reader = null;
            try
            {
                fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs, encoding);
                return reader.ReadToEnd();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (fs != null) fs.Close();
                if (reader != null) reader.Close();
            }
        }

        public string ReadAllString(string filepath)
        {
            return ReadAllString(filepath, Encoding.UTF8);
        }


        public void ClearFile(string filepath)
        {
            File.Delete(filepath);
            CreateFile(filepath);
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public void CleanFolder(string filePath)
        {
            foreach (string d in Directory.GetFileSystemEntries(filePath))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件  
                    Debug.Log("delete: " + d);
                }
                else
                {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0)
                    {
                        DeleteFolder(d1.FullName);
                    }
                    //Directory.Delete(d);
                }
            }
        }

        public void DeleteFolder(string filePath)
        {
            string[] files = GetFilenNames(filePath, "*", true);
            foreach (var t in files)
            {
                DeleteFile(t);
            }
            Directory.Delete(filePath);
        }

        public string GetFileName(string filePath, string tail)
        {
            return Path.GetFileName(filePath).Replace(tail, "");
        }
    }
}