using System;
using AppStoreFramework.Logging.Interfaces;
using Raven.Abstractions.Commands;
using Raven.Client;

namespace AppStoreFramework.DAL.Client.Implementations.Commands
{
    public class DeleteCommand
    {
        internal readonly IDocumentStore Store;
        protected readonly ILogger Logger;

        protected DeleteCommand(IDocumentStore store, ILogger logger)
        {
            this.Logger = logger;
            this.Store = store;
        }

        public bool Delete(string location)
        {
            try
            {
                using (var session = this.Store.OpenSession())
                {
                    session.Advanced.Defer(new DeleteCommandData {Key = location});
                    session.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error("Deleting document in Db error", e.ToString());
                return false;
            }
        }
    }
}
