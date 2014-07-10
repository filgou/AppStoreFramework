using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.Infrastructure.Implementations.Xml;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;
using AppStoreFramework.Repository.Implementations.FolderToApp;
using AppStoreFramework.Repository.Interfaces.FolderToApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppStoreFramework.Repository.Tests
{
    [TestClass]
    public class FolderToAppMapperTest
    {
        Mock<IManifestMapper> manifestMapper = new Mock<IManifestMapper>();
        Mock<IFileSystem> fileSystem = new Mock<IFileSystem>();
        DirectoryInfo workingdirectory = new DirectoryInfo(@"\\server\\category\subcategory\name");
        DirectoryInfo longworkingdirectory = new DirectoryInfo(@"\\server\1\2\3\category\subcategory\name");
        DirectoryInfo shortworkingdirectory = new DirectoryInfo(@"\\server\name");
        DirectoryInfo shortworkingdirectory2 = new DirectoryInfo(@"\\server\category\name");
        DirectoryInfo emptyworkingdirectory = new DirectoryInfo(@"C:\");
        private string[] validResults = new string[] {"test.manifest", "test.package"};
        private string[] missingManifestResults = new string[] { "test.manifest1", "test.package" };
        private string[] multipleManifestResults = new string[] { "test.manifest", "test2.manifest" };
      

        private StoreAppManifest manifest = new StoreAppManifest();
        [TestInitialize]
        public void TestSetup()
        {
            this.manifest.TypeId = "game";
            this.manifest.Version = "1.0.0.0";
            this.manifest.PackageUri = new XmlUri(new Uri("http://www.test.com/"));
        }

        [TestMethod]
        public void TestValidContents()
        {
            var foldermapper = new FolderToAppMapper(workingdirectory.FullName, manifestMapper.Object, fileSystem.Object);

            fileSystem.Setup(o => o.GetFiles(workingdirectory.FullName)).Returns(validResults);
            manifestMapper.Setup(o => o.LoadAppManifest(It.IsAny<FileInfo>())).Returns(manifest);
            Assert.AreEqual(foldermapper.CheckIsValidFolder(),true);

        }

        [ExpectedException(typeof( ArgumentException ), "None or multiple manifests found for single app" )]
        [TestMethod]
        public void TestMissingManifest()
        {
            var foldermapper = new FolderToAppMapper(workingdirectory.FullName, manifestMapper.Object, fileSystem.Object);

            fileSystem.Setup(o => o.GetFiles(workingdirectory.FullName)).Returns(missingManifestResults);
            manifestMapper.Setup(o => o.LoadAppManifest(It.IsAny<FileInfo>())).Returns(manifest);
            foldermapper.CheckIsValidFolder();
        }

        [ExpectedException(typeof(ArgumentException), "Manifest was not valid")]
        [TestMethod]
        public void TestMultipleManifests()
        {
            var foldermapper = new FolderToAppMapper(workingdirectory.FullName, manifestMapper.Object, fileSystem.Object);

            fileSystem.Setup(o => o.GetFiles(workingdirectory.FullName)).Returns(validResults);
            this.manifest.PackageUri = new XmlUri();
            manifestMapper.Setup(o => o.LoadAppManifest(It.IsAny<FileInfo>())).Returns(manifest);
            foldermapper.CheckIsValidFolder();
        }

        [TestMethod]
        public void TestCateogryName()
        {
            var foldermapper = new FolderToAppMapper(workingdirectory.FullName, manifestMapper.Object, fileSystem.Object);
          

            var category = foldermapper.GetCategoryFromFileSystem();
            Assert.AreEqual(category,"category");

            var subcategory = foldermapper.GetSubategoryFromFileSystem();
            Assert.AreEqual(subcategory, "subcategory");

            var name = foldermapper.GetNameFromFileSystem();
            Assert.AreEqual(name, "name");
        }

        [TestMethod]
        public void TestLongCateogryName()
        {
            var foldermapper = new FolderToAppMapper(longworkingdirectory.FullName, manifestMapper.Object, fileSystem.Object);
           
            var category = foldermapper.GetCategoryFromFileSystem();
            Assert.AreEqual(category, "category");

            var subcategory = foldermapper.GetSubategoryFromFileSystem();
            Assert.AreEqual(subcategory, "subcategory");

            var name = foldermapper.GetNameFromFileSystem();
            Assert.AreEqual(name, "name");
        }

        [TestMethod]
        public void TestShortCateogryName()
        {
            var foldermapper = new FolderToAppMapper(shortworkingdirectory.FullName, manifestMapper.Object, fileSystem.Object);
           

            var category = foldermapper.GetCategoryFromFileSystem();
            Assert.AreEqual(category, "server");

            var subcategory = foldermapper.GetSubategoryFromFileSystem();
            Assert.AreEqual(subcategory, string.Empty);

            var name = foldermapper.GetNameFromFileSystem();
            Assert.AreEqual(name, "name");
        }

        [TestMethod]
        public void TestShortCateogryName2()
        {
            var foldermapper = new FolderToAppMapper(shortworkingdirectory2.FullName, manifestMapper.Object, fileSystem.Object);
          

            var category = foldermapper.GetCategoryFromFileSystem();
            Assert.AreEqual(category, "server");

            var subcategory = foldermapper.GetSubategoryFromFileSystem();
            Assert.AreEqual(subcategory, "category");

            var name = foldermapper.GetNameFromFileSystem();
            Assert.AreEqual(name, "name");
        }

        [TestMethod]
        public void TestCategoryEmptyName()
        {
            var foldermapper = new FolderToAppMapper(emptyworkingdirectory.FullName, manifestMapper.Object, fileSystem.Object);
            

            var category = foldermapper.GetCategoryFromFileSystem();
            Assert.AreEqual(category, "C:");

            var subcategory = foldermapper.GetSubategoryFromFileSystem();
            Assert.AreEqual(subcategory, "");

            var name = foldermapper.GetNameFromFileSystem();
            Assert.AreEqual(name, "");
        }
    }
}
