using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core.Util
{
    public class FileSystemUtil
    {
        static String LOG_TAG = "FileSystemUtil";

        public static bool ExistFile(String path)
        {
            return File.Exists(path);
        }

        public static bool ExistDir(String path)
        {
            return Directory.Exists(path);
        }

        public static FileStream CreateFile(String path, StringBuilder outMsg = null)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(path);
            }
            catch (System.Exception e)
            {
                fileStream = null;
                CLogger.Instance.error(LOG_TAG, "CreateFile Fail , Path :{0}, Error :{1}", path, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return fileStream;
            
        }

        public static DirectoryInfo CreateDir(String path, StringBuilder outMsg = null)
        {
            DirectoryInfo directoryInfo = null;
            try
            {
                directoryInfo = Directory.CreateDirectory(path);
            }
            catch (System.Exception e)
            {
                directoryInfo = null;
                CLogger.Instance.error(LOG_TAG, "CreateDir Fail , Path :{0}, Error :{1}", path, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return directoryInfo;
        }

        public static bool DeleteFile(String path, StringBuilder outMsg = null)
        {
            if (!ExistFile(path))
            {
                return false;
            }

            bool succ = false;
            try
            {
                File.Delete(path);
                succ = true;
            }
            catch (System.Exception e)
            {
                succ = false;
                CLogger.Instance.error(LOG_TAG, "DeleteFile Fail , Path :{0}, Error :{1}", path, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return succ;
        }

        public static bool DeleteDir(String path, StringBuilder outMsg = null)
        {
            if (!ExistDir(path))
            {
                return false;
            }

            bool succ = false;
            try
            {
                Directory.Delete(path);
                succ = true;
            }
            catch (System.Exception e)
            {
                succ = false;
                CLogger.Instance.error(LOG_TAG, "DeleteDir Fail , Path :{0}, Error :{1}", path, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return succ;
        }

        public static DirectoryInfo GetDirInfo(String path, StringBuilder outMsg = null)
        {
            DirectoryInfo directoryInfo = null;
            try
            {
                directoryInfo = new DirectoryInfo(path);
            }
            catch (System.Exception e)
            {
                directoryInfo = null;
                CLogger.Instance.error(LOG_TAG, "GetDirInfo Fail , Path :{0}, Error :{1}", path, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return directoryInfo;
        }

        public static FileInfo GetFileInfo(String path, StringBuilder outMsg = null)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(path);
            }
            catch (System.Exception e)
            {
                fileInfo = null;
                CLogger.Instance.error(LOG_TAG, "GetDirInfo Fail , Path :{0}, Error :{1}", path, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return fileInfo;
        }

        public static String[] GetDirs(String dirPath, StringBuilder outMsg = null)
        {
            return GetDirs(dirPath, "*", SearchOption.TopDirectoryOnly, outMsg);
        }

        public static String[] GetDirs(String dirPath, String searchPattern, StringBuilder outMsg = null)
        {
            return GetDirs(dirPath, searchPattern, SearchOption.TopDirectoryOnly, outMsg);
        }

        public static String[] GetDirs(String dirPath, String searchPattern, SearchOption searchOption, StringBuilder outMsg = null)
        {
            String[] dirs = null;
            try
            {
                dirs = Directory.GetDirectories(dirPath, searchPattern, searchOption);
            }
            catch (System.Exception e)
            {
                dirs = null;
                CLogger.Instance.error(LOG_TAG, "GetDirs Fail , Path :{0}, Error :{1}", dirPath, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return dirs;
        }

        public static String[] GetFiles(String dirPath, StringBuilder outMsg = null)
        {
            return GetFiles(dirPath, "*", SearchOption.TopDirectoryOnly, outMsg);
        }

        public static String[] GetFiles(String dirPath, String searchPattern, StringBuilder outMsg = null)
        {
            return GetFiles(dirPath, searchPattern, SearchOption.TopDirectoryOnly, outMsg);
        }

        public static String[] GetFiles(String dirPath, String searchPattern, SearchOption searchOption, StringBuilder outMsg = null)
        {
            String[] files = null;
            try
            {
                files = Directory.GetFiles(dirPath, searchPattern, searchOption);
            }
            catch (System.Exception e)
            {
                files = null;
                CLogger.Instance.error(LOG_TAG, "GetFiles Fail , Path :{0}, Error :{1}", dirPath, e.Message);

                if (outMsg != null)
                {
                    outMsg.Append(e.Message);
                }
            }

            return files;
        }

        public static String GetLinkTargetPath(String path, StringBuilder outMsg = null)
        {
            String targetPath = null;
            FileInfo fileInfo = GetFileInfo(path);

            if (fileInfo != null)
            {
                try
                {
                    string dirPath = fileInfo.DirectoryName;
                    string fileName = fileInfo.Name;

                    Shell shell = new Shell();
                    Folder folder = shell.NameSpace(dirPath);
                    FolderItem folderItem = folder.ParseName(fileName);
                    if (folderItem != null)
                    {
                        Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                        targetPath = link.Path;
                    }
                }
                catch (System.Exception e)
                {
                    targetPath = null;
                    outMsg.Append(e.Message);
                }
            }

            return targetPath;
        }

        public static bool CopyTo(String srcPath, String destPath, StringBuilder outMsg = null)
        {
            return CopyTo(srcPath, destPath, false, outMsg);
        }

        public static bool CopyTo(String srcPath, String destPath, bool overWrite, StringBuilder outMsg = null)
        {
            bool succ = false;

            try
            {
                File.Copy(srcPath, destPath, overWrite);
                succ = true;
            }
            catch (System.Exception e)
            {
                outMsg.Append(e.Message);
            }
            
            return succ;
        }
    }
}
