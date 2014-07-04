using System;
using System.IO;
using System.Linq;
using Hocoma.AppStore.Infrastructure.Interfaces.FileSystem;

namespace Hocoma.AppStore.Infrastructure.Implementations.FileSystem
{
    public class DefaultFileSystem : IFileSystem
    {
        public string CommonApplicationData
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); }
        }

        public DateTime GetFileCreationDate(string path)
        {
            return File.GetCreationTime(path);
        }

        public void RemoveDirectory(string path)
        {
            foreach (var filetodelete in Directory.GetFiles(path))
            {
                File.Delete(filetodelete);
            }
            Directory.Delete(path);
        }

        public void CopyFile(string sourcepath, string destpath)
        {
            File.Copy(sourcepath, destpath);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }

        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public string GetPathRoot(string path)
        {
            return Path.GetPathRoot(path);
        }

        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public string PathCombine(params string[] args)
        {
            return Path.Combine(args);
        }

        public void MoveFile(string filePath, string newFilePath)
        {
            File.Move(filePath, newFilePath);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string[] GetFiles(string path, string searchpatterns, SearchOption options)
        {
            return Directory.GetFiles(path, searchpatterns, options);
        }

        public void Delete(string filepath)
        {
            File.Delete(filepath);
        }

        public string GetFirstAvailableUSB()
        {
            string usbPath = null;
            try
            {
                var removableDrives = from d in System.IO.DriveInfo.GetDrives()
                    where d.DriveType == DriveType.Removable
                          && d.IsReady
                          && d.VolumeLabel != "RESTRICTED"
                          && d.DriveType == DriveType.Removable
                    select d;
                var firstUsb = removableDrives.FirstOrDefault();
                if(firstUsb != null)
                {
                    usbPath = firstUsb.RootDirectory.FullName;
                }
            }
            catch (Exception exc)
            {
                //TODO: log this
                usbPath = null;
            }
            return usbPath;
        }


    }
}
