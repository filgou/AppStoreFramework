using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStoreFramework.Logging.Interfaces;
using Raven.Client;

namespace AppStoreFramework.DAL.Client.Implementations.Commands
{
    public class AddCommand<T>
    {
        internal readonly IDocumentStore Store;
        protected  ILogger Logger;

        public AddCommand(IDocumentStore store, ILogger logger)
        {
            this.Store = store;
            this.Logger = logger;
        }

        public bool Add(T documentToAdd)
        {
            try
            {
                using (var session = this.Store.OpenSession())
                {
                    session.Store(documentToAdd);
                    session.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Adding document in Db error",ex.ToString());
                return false;
            }
           
        }
    }
}
