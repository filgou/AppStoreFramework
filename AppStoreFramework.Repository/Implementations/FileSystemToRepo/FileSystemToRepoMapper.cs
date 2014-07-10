using System;
using System.Collections.Generic;
using System.IO;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;
using AppStoreFramework.Logging.Interfaces;
using AppStoreFramework.Repository.Interfaces.FileSystemToRepo;
using AppStoreFramework.Repository.Interfaces.FolderToApp;

namespace AppStoreFramework.Repository.Implementations.FileSystemToRepo
{
    public class FileSystemToRepoMapper : IFileSystemToRepoMapper
    {
        private readonly IFileSystem fileSystem;
        private readonly IFolderToAppMapperFactory folderToAppMapperFactory;
        private readonly ILogger logger;
        internal Stack<string> StoreAppDirectories;
        internal List<IStoreApp> StoreAppRepo;

        public FileSystemToRepoMapper(IFileSystem fileSystem, IFolderToAppMapperFactory folderToAppMapperFactory, ILogger logger)
        {
            this.fileSystem = fileSystem;
            this.folderToAppMapperFactory = folderToAppMapperFactory;
            this.logger = logger;
        }

        public List<IStoreApp> LoadApps(Uri filesystemRootUncpPath)
        {
            CheckRootIsUnc(filesystemRootUncpPath);
            var root = new FileInfo(filesystemRootUncpPath.LocalPath);
            TraverseTree(root.FullName);
            return StoreAppRepo;
        }

        internal void CheckRootIsUnc(Uri filesystemRootUncpPath)
        {
            if (!filesystemRootUncpPath.IsUnc)
            {
                throw new ArgumentException("The root folder is not a valid UNC path");
            }
           
        }

        internal void CheckRootExists(string root)
        {
            if (!fileSystem.DirectoryExists(root))
            {
                throw new ArgumentException("The root folder was not found");
            }
        }

        //Source: How to: Iterate Through a Directory Tree http://msdn.microsoft.com/en-us/library/bb513869.aspx
        internal void TraverseTree(string root)
        {
            // Data structure to hold names of subfolders to be 
            // examined for files.
            StoreAppRepo = new List<IStoreApp>();
            StoreAppDirectories = new Stack<string>();
            CheckRootExists(root);

            StoreAppDirectories.Push(root);

            while (StoreAppDirectories.Count > 0)
            {
                string currentDir = StoreAppDirectories.Pop();
                string[] subDirs;
                try
                {
                    subDirs = fileSystem.GetDirectories(currentDir);
                }
                // An UnauthorizedAccessException exception will be thrown if we do not have 
                // discovery permission on a folder or file. It may or may not be acceptable  
                // to ignore the exception and continue enumerating the remaining files and  
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception  
                // will be raised. This will happen if currentDir has been deleted by 
                // another application or thread after our call to Directory.Exists. The  
                // choice of which exceptions to catch depends entirely on the specific task  
                // you are intending to perform and also on how much you know with certainty  
                // about the systems on which this code will run. 
                catch (UnauthorizedAccessException e)
                {
                    logger.Error("error traversing folder tree", e.ToString());
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    logger.Error("error traversing folder tree", e.ToString());
                    continue;
                }
                //if we are on a leaf, then try to map folder to a store app
                if(subDirs.Length == 0)
                {
                    try
                    {
                        var currentapp = folderToAppMapperFactory.CreateMapper(currentDir, fileSystem).LoadApp();
                        StoreAppRepo.Add(currentapp);
                    }
                    catch (ArgumentException e)
                    {
                        logger.Error("Could not map folder to app", e.ToString());
                    }
                }
                // Push the subdirectories onto the stack for traversal. 
                // This could also be done before handing the files. 
                foreach (string str in subDirs)
                {
                    StoreAppDirectories.Push(str);
                }
            }
        }
    }
}
