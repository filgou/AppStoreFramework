using System;
using System.Collections.Generic;

namespace Hocoma.AppStore.DAL.Interfaces.StoreApp
{
    public interface IStoreApp
    {
        string NameId { get; set; }
        string CategoryId { get; set; }
        string SubCategoryId { get; set; }
        
        bool IsAvailable { get; set; }

        IStoreAppManifest Manifest { get; set; }
    }
}