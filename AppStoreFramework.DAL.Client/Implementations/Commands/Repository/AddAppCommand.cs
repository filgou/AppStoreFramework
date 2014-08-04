using AppStoreFramework.DAL.Interfaces.Commands.Repository;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Logging.Interfaces;
using Raven.Client;

namespace AppStoreFramework.DAL.Client.Implementations.Commands.Repository
{
    public class AddAppCommand : AddCommand<IStoreApp>, IAddAppCommand
    {
        public AddAppCommand(IDocumentStore store, ILogger logger) : base(store,logger)
        {
            
        }

        public bool AddApp(IStoreApp storeApp)
        {
            return Add(storeApp);
        }
    }
}
