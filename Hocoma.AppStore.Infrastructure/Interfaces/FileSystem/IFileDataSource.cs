using System.IO;

namespace Hocoma.AppStore.Infrastructure.Interfaces.FileSystem
{
    public interface IFileDataSource
    {
        FileStream GetFileStream(string path, FileMode mode, FileAccess access, FileShare share);
    }
}
