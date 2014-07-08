using System;
using System.IO;

namespace AppStoreFramework.Infrastructure.Interfaces.FileSystem
{
    public interface IFileSystem
    {
        string CommonApplicationData { get; }
        DateTime GetFileCreationDate(string filepath);

        void RemoveDirectory(string path);

        void CopyFile(string sourcepath, string destpath);

        bool FileExists(string path);

        bool DirectoryExists(string path);

        string GetFullPath(string path);

        string GetFileName(string filePath);

        string GetPathRoot(string path);

        string GetDirectoryName(string path);

        string PathCombine(params string[] args);

        void MoveFile(string filePath, string newFilePath);

        void CreateDirectory(string path);

        string[] GetFiles(string path);

        string[] GetFiles(string path, string searchpatterns, SearchOption options);

        void Delete(string filepath);

        string ReadAllText(string fullName);
    }
}
