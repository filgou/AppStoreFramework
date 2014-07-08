using System.IO;
using AppStoreFramework.DAL.Interfaces.StoreApp;

namespace AppStoreFramework.Repository.Interfaces.FolderToApp
{
    public interface IManifestMapper
    {
        IStoreAppManifest LoadAppManifest(FileInfo manifestFile);
    }
}
