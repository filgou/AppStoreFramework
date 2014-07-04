using System.IO;
using Hocoma.AppStore.Infrastructure.Interfaces.FileSystem;

namespace Hocoma.AppStore.Infrastructure.Implementations.FileSystem
{
    public class DefaultFileStream : IFileDataSource
    {
        public FileStream GetFileStream(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStream( path,  mode,  access,  share);
        }
    }
}
