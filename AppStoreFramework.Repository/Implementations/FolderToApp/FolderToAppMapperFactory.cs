using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;
using AppStoreFramework.Repository.Interfaces.FolderToApp;

namespace AppStoreFramework.Repository.Implementations.FolderToApp
{
    public class FolderToAppMapperFactory : IFolderToAppMapperFactory
    {
        private readonly IManifestMapper manifestMapper;

        public FolderToAppMapperFactory(IManifestMapper manifestMapper)
        {
            this.manifestMapper = manifestMapper;
        }

        public IFolderToAppMapper CreateMapper(string workingWirectory, IFileSystem filesSytem)
        {
            return new FolderToAppMapper(workingWirectory, manifestMapper, filesSytem);
        }

       
    }
}
