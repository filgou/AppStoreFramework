using System;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.DAL.Interfaces.Commands.Repository;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Logging.Interfaces;
using Raven.Client;

namespace AppStoreFramework.DAL.Client.Implementations.Commands.Repository
{
    public class UpdateAppCommand : UpdateCommand<IStoreApp>, IUpdateAppCommand
    {
        public UpdateAppCommand(IDocumentStore store, ILogger logger) : base(store, logger) {}

        public override bool UpdateInternal(IStoreApp documentToEdit, string location)
        {
            try
            {
                using (var session = this.Store.OpenSession())
                {
                    var existingApp = session.Load<StoreApp>(location);
                    existingApp.CategoryId = documentToEdit.CategoryId;
                    existingApp.NameId = documentToEdit.NameId;
                    existingApp.SubCategoryId = documentToEdit.SubCategoryId;
                    existingApp.Manifest = documentToEdit.Manifest;
                    existingApp.IsAvailable = documentToEdit.IsAvailable;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error("Updating document in Db error", e.ToString());
                return false;
            }
        }
    }
}
