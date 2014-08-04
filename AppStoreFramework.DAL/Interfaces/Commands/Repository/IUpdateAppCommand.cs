using AppStoreFramework.DAL.Interfaces.StoreApp;

namespace AppStoreFramework.DAL.Interfaces.Commands.Repository
{
    public interface IUpdateAppCommand
    {
        bool Update(IStoreApp documentToEdit);
    }
}
