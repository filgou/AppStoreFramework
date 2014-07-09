using System.IO;
using System.Xml.Linq;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Infrastructure.Implementations.Extensions;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;
using AppStoreFramework.Repository.Interfaces.FolderToApp;

namespace AppStoreFramework.Repository.Implementations.FolderToApp
{
    public class ManifestMapper: IManifestMapper
    {
        private readonly IFileSystem fileSystem;

        public ManifestMapper(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public IStoreAppManifest LoadAppManifest(FileInfo manifestFile)
        {
            var filecontents = this.fileSystem.ReadAllText(manifestFile.FullName);
            var xml = XElement.Parse(filecontents);
            var xmlToString = xml.ToString(SaveOptions.None);
            return xmlToString.DeSerialize<StoreAppManifest>();
        }
    }
}
