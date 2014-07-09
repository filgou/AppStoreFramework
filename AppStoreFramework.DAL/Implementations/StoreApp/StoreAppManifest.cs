using System;
using System.Collections.Generic;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Infrastructure.Implementations.Xml;

namespace AppStoreFramework.DAL.Implementations.StoreApp
{
    public class StoreAppManifest : IStoreAppManifest
    {
        private string typeId;
        private XmlUri previewImageUri;
        private XmlUri packageUri;
        private string version;
        private List<string> prerequisites;
        private DateTime publishedDate;
        private DateTime lastUpdatedDate;
        private string publishedBy;
        private List<string> languages;
        private SerializableDictionary<string, string> localisedResources;
        private string extraData;

        public string TypeId
        {
            get { return this.typeId; }
            set { this.typeId = value; }
        }

        public XmlUri PreviewImageUri
        {
            get { return this.previewImageUri; }
            set { this.previewImageUri = value; }
        }

        public XmlUri PackageUri
        {
            get { return this.packageUri; }
            set { this.packageUri = value; }
        }

        public string Version
        {
            get { return this.version; }
            set { this.version = value; }
        }

        public List<string> Prerequisites
        {
            get { return this.prerequisites; }
            set { this.prerequisites = value; }
        }

        public DateTime PublishedDate
        {
            get { return this.publishedDate; }
            set { this.publishedDate = value; }
        }

        public DateTime LastUpdatedDate
        {
            get { return this.lastUpdatedDate; }
            set { this.lastUpdatedDate = value; }
        }

        public string PublishedBy
        {
            get { return this.publishedBy; }
            set { this.publishedBy = value; }
        }

        public List<string> Languages
        {
            get { return this.languages; }
            set { this.languages = value; }
        }

        public SerializableDictionary<string, string> LocalisedResources
        {
            get { return this.localisedResources; }
            set { this.localisedResources = new SerializableDictionary<string, string>(value); }
        }

        private string Target(string s)
        {
            return s;
        }

        public string ExtraData
        {
            get { return this.extraData; }
            set { this.extraData = value; }
        }

        public bool IsValid()
        {
            try
            {
                //try to convert to absolute to force check   
                Uri validUri;
                return Uri.TryCreate(packageUri.ToString(), UriKind.Absolute, out validUri);
            }
            catch (NullReferenceException)
            {
                return false;
            }

        }
    }
}
