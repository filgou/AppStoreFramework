using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.Infrastructure.Implementations.Extensions;
using AppStoreFramework.Infrastructure.Implementations.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppStoreFramework.DAL.Tests
{
    [TestClass]
    public class ManifestTests
    {
        private string extrdata = "extra;data;";
        private string version = "1.0.0.0";
        private string typeId = "Game";
        private string publisher = "gou";
        private List<string> prerequisites = new List<string> {"LC 6.4.1", "LC6.4.0"}; 
        private XmlUri testUri = new Uri("http://www.test.com", UriKind.Absolute);
        private List<string> languages = new List<string> {"EN", "DE"};
        private SerializableDictionary<string, string> localisationDictionary;

        [TestInitialize]
        public void TestSetup()
        {
            this.localisationDictionary = new SerializableDictionary<string, string>();
            this.localisationDictionary.Add("game", "game");
            this.localisationDictionary.Add("game_de", "Spiel");
        }


        [TestMethod]
        public void ManifestToxmlTest()
        {
            
            var s = new StoreAppManifest()
            {
                Languages = this.languages, 
                LastUpdatedDate = DateTime.Now, 
                PackageUri =this.testUri,
                PreviewImageUri = this.testUri,
                                             Prerequisites = this.prerequisites,
                                             PublishedBy = this.publisher,
                                             PublishedDate = DateTime.Now.AddDays(1),
                                             TypeId = this.typeId, 
                Version = this.version,
                LocalisedResources = this.localisationDictionary, 
                ExtraData = this.extrdata };
            var xml = s.Serialize();
            Assert.AreNotEqual(xml,string.Empty);
        }

        [TestMethod]
        public void ManifestFromXmlTest()
        {

            var xml = XElement.Load("ManifestExample.xml");
            var stringfromxml = xml.ToString(SaveOptions.None);
            var manifest = stringfromxml.DeSerialize<StoreAppManifest>();
            Assert.AreEqual(manifest.ExtraData, this.extrdata);
            Assert.AreEqual(manifest.PublishedBy, this.publisher); 
            Assert.AreEqual(manifest.Version, this.version);
            Assert.AreEqual(manifest.TypeId, this.typeId);
            Assert.AreEqual(manifest.PreviewImageUri.ToString(), this.testUri.ToString());
            Assert.AreEqual(manifest.PackageUri.ToString(), this.testUri.ToString());
            Assert.AreEqual(manifest.LocalisedResources.Count, this.localisationDictionary.Count);
            Assert.AreEqual(manifest.Languages.Count, this.languages.Count);
        }
    }
}
