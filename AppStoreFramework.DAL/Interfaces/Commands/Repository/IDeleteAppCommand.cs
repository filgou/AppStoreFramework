using AppStoreFramework.DAL.Implementations.StoreApp;

namespace AppStoreFramework.DAL.Client.Interfaces.Commands.Repository
{
    public interface IDeleteAppCommand
    {
        bool DeleteApp(StoreApp storeApp);
    }
}
