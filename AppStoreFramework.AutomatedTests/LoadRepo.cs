using System;
using AppStoreFramework.DAL.Client.Implementations.Commands.Repository;
using AppStoreFramework.DAL.Client.Implementations.Queries;
using AppStoreFramework.Infrastructure.Implementations.FileSystem;
using AppStoreFramework.Repository.Implementations.FileSystemToRepo;
using AppStoreFramework.Repository.Implementations.FolderToApp;
using AppStoreFramework.Repository.Implementations.RepoToDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Client.Document;

namespace AppStoreFramework.AutomatedTests
{
    [TestClass]
    public class LoadRepo
    {
        private string workingdirectory = @"\\srvchv122\Deployment\AppsRoot";
        private TextLogger logger = new TextLogger();
        private DefaultFileSystem filesystem = new DefaultFileSystem();
        private ManifestMapper manifestmapper;

        
        [TestMethod]
        public void LoadRepoAutomatedTest()
        {
            manifestmapper = new ManifestMapper(this.filesystem);
          
            var ftmf = new FolderToAppMapperFactory(manifestmapper);
            var f = new FileSystemToRepoMapper(this.filesystem, ftmf, logger);

            IDocumentStore documentStore = new DocumentStore { Url = "http://localhost/databases/TestDb" };
            documentStore.Initialize();
        
          
            f.LoadApps(new Uri(workingdirectory,UriKind.RelativeOrAbsolute));
            Assert.AreEqual(f.StoreApps.Count,6);

            var query = new GetAppsQuery(documentStore, logger);
            var addcommand = new AddAppCommand(documentStore, logger);
            var updatecommand = new UpdateAppCommand(documentStore, logger);


            var dbMapper = new RepoToDbMapper(f.StoreApps, query, addcommand, updatecommand);
            dbMapper.SaveAppsToDb();


        }
    }
}
