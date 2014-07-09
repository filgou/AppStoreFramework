using System.IO;
using System.Xml.Linq;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.Infrastructure.Implementations.Extensions;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;
using AppStoreFramework.Repository.Implementations.FolderToApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppStoreFramework.Repository.Tests
{
    [TestClass]
    public class ManifestMapperTests
    {
        private Mock<IFileSystem> fileSystem = new Mock<IFileSystem>();
        private StoreAppManifest manifest = new StoreAppManifest();
        [TestInitialize]
        public void TestSetup()
        {
            this.manifest.TypeId = "game";
            this.manifest.Version = "1.0.0.0";
        }

        [TestMethod]
        public void ManifestTestMapping()
        {
            var mapper = new ManifestMapper(this.fileSystem.Object);
            this.fileSystem.Setup(o => o.ReadAllText(It.IsAny<string>())).Returns(this.manifest.Serialize());

            var mappedManifest = mapper.LoadAppManifest(new FileInfo( "c:\\test.manifest"));
            Assert.AreEqual(mappedManifest.Version, this.manifest.Version);
        }

        [TestMethod]
        public void ManifestTestPartialMapping()
        {
            var mapper = new ManifestMapper(this.fileSystem.Object);
            var partialxml = XElement.Load("ManifestExample.xml").ToString(SaveOptions.None);
            this.fileSystem.Setup(o => o.ReadAllText(It.IsAny<string>())).Returns(partialxml);

            var mappedManifest = mapper.LoadAppManifest(new FileInfo("c:\\test.manifest"));
            Assert.AreEqual(mappedManifest.Version, this.manifest.Version);
        }
    }
}
