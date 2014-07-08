using System.IO;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;

namespace AppStoreFramework.Infrastructure.Implementations.FileSystem
{
    public class DefaultFileStream : IFileDataSource
    {
        public FileStream GetFileStream(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStream( path,  mode,  access,  share);
        }
    }
}
