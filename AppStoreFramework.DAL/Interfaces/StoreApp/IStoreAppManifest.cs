using System;
using System.Collections.Generic;
using AppStoreFramework.Infrastructure.Implementations.Xml;

namespace AppStoreFramework.DAL.Interfaces.StoreApp
{
    public interface IStoreAppManifest
    {
        string TypeId { get; set; }
        XmlUri PreviewImageUri { get; set; }
        XmlUri PackageUri { get; set; }

        string Version { get; set; }
        List<string> Prerequisites { get; set; }

        DateTime PublishedDate { get; set; }
        DateTime LastUpdatedDate { get; set; }
        string PublishedBy { get; set; }

        List<string> Languages { get; set; }
        SerializableDictionary<string, string> LocalisedResources { get; set; }

        string ExtraData { get; set; }

        bool IsValid();
    }
}
