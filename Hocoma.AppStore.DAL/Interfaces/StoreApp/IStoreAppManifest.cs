using System;
using System.Collections.Generic;

namespace Hocoma.AppStore.DAL.Interfaces.StoreApp
{
    public interface IStoreAppManifest
    {
        string TypeId { get; set; }
        Uri PreviewImageUri { get; set; }
        Uri PackageUri { get; set; }

        string Version { get; set; }
        IEnumerable<string> Prerequisites { get; set; }

        DateTime PublishedDate { get; set; }
        DateTime LastUpdatedDate { get; set; }
        string PublishedBy { get; set; }

        IEnumerable<string> Languages { get; set; }
        Dictionary<string, string> LocalisedResources { get; set; }

        string ExtraData { get; set; }
    }
}
