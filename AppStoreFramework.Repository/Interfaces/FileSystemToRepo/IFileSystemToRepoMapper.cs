using System;
using System.Collections.Generic;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;

namespace AppStoreFramework.Repository.Interfaces.FileSystemToRepo
{
    public interface IFileSystemToRepoMapper
    {
        List<IStoreApp> LoadApps(Uri filesystemRootUncpPath);
    }
}
