using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStoreFramework.DAL.Interfaces.Documents;
using AppStoreFramework.Logging.Interfaces;
using Raven.Client;

namespace AppStoreFramework.DAL.Client.Implementations.Commands
{
    public abstract class UpdateCommand<T> where T : IDocument
    {
         internal readonly IDocumentStore Store;
        protected readonly ILogger Logger;

        protected UpdateCommand(IDocumentStore store, ILogger logger)
         {
             this.Store = store;
             this.Logger = logger;
         }

        public bool Update(T documentToAdd)
        {
          return  UpdateInternal(documentToAdd, documentToAdd.GetLocation());
        }

        public abstract bool UpdateInternal(T documentToAdd, string location);

    }
}
