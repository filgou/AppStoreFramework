using System;
using System.Collections.Generic;
using AppStoreFramework.DAL.Interfaces.StoreApp;

namespace AppStoreFramework.Repository.Interfaces.FileSystemToRepo
{
    public interface IFileSystemToRepoMapper
    {
        List<IStoreApp> LoadApps(Uri filesystemRootUncpPath);
    }
}
