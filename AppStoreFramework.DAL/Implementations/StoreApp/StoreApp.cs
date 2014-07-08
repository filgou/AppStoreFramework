using System;
using AppStoreFramework.DAL.Interfaces.StoreApp;

namespace AppStoreFramework.DAL.Implementations.StoreApp
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
            this.Manifest = manifest;
        }

        public override string ToString()
        {
            return String.Format("Name: {0}, CategoryId: {1}, SubcatecoryId: {2}",this.NameId,this.CategoryId,this.SubCategoryId);
        }
    }
}