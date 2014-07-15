using System;
using AppStoreFramework.Infrastructure.Implementations.FileSystem;
using AppStoreFramework.Repository.Implementations.FileSystemToRepo;
using AppStoreFramework.Repository.Implementations.FolderToApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppStoreFramework.AutomatedTests
{
    [TestClass]
    public class LoadRepo
    {
        private string workingdirectory = @"\\srvchv122\Deployment\AppsRoot";

        
        [TestMethod]
        public void LoadRepoAutomatedTest()
        {
            var l = new TextLogger();
            var fs = new DefaultFileSystem();
            var mm = new ManifestMapper(fs);
          
            var ftmf = new FolderToAppMapperFactory(mm);
            var f = new FileSystemToRepoMapper(fs, ftmf, l);
          
            f.LoadApps(new Uri(workingdirectory,UriKind.RelativeOrAbsolute));
            Assert.AreEqual(f.StoreApps.Count,6);
        }
    }
}
