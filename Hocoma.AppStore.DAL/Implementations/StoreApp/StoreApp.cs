using System;
using System.Collections.Generic;
using Hocoma.AppStore.DAL.Interfaces.StoreApp;

namespace Hocoma.AppStore.DAL.Implementations.StoreApp
{
    public class StoreApp : IStoreApp
    {
        public string NameId { get; set; }
        
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
       
        public bool IsAvailable { get; set; }

        public IStoreAppManifest Manifest { get; set; }

        public StoreApp(IStoreAppManifest manifest)
        {
            Manifest = manifest;
        }

        public override string ToString()
        {
            return String.Format("Name: {0}, CategoryId: {1}, SubcatecoryId: {2}",NameId,CategoryId,SubCategoryId);
        }
    }
}