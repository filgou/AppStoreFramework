using AppStoreFramework.DAL.Interfaces.StoreApp;

namespace AppStoreFramework.DAL.Interfaces.Commands.Repository
{
    public interface IAddAppCommand
    {
        bool AddApp(IStoreApp storeApp);
    }
}
