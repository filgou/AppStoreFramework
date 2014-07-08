using System.IO;
using AppStoreFramework.DAL.Interfaces.StoreApp;

namespace AppStoreFramework.Repository.Interfaces.FolderToApp
{
    public interface IFolderToAppMapper
    {
        IStoreApp LoadApp(DirectoryInfo folder);
        bool CheckIsValidFolder(DirectoryInfo folder);
    }
}
