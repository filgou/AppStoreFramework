using System;
using System.Collections.Generic;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Repository.Interfaces.FileSystemToRepo;

namespace AppStoreFramework.Repository.Implementations.FileSystemToRepo
{
    public class FileSystemToRepoMapper : IFileSystemToRepoMapper
    {
        public List<IStoreApp> LoadApps(Uri filesystemRootUncpPath)
        {
            throw new NotImplementedException();
        }
    }
}
