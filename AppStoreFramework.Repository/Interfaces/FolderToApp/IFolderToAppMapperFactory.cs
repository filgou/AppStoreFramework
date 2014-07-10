using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;

namespace AppStoreFramework.Repository.Interfaces.FolderToApp
{
    public interface IFolderToAppMapperFactory
    {
        IFolderToAppMapper CreateMapper(string workingWirectory, IFileSystem filesSytem);
    }
}
