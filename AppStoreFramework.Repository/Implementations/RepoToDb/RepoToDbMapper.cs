using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStoreFramework.DAL.Client.Interfaces.Commands.Repository;
using AppStoreFramework.DAL.Interfaces.Commands.Repository;
using AppStoreFramework.DAL.Interfaces.Queries;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Repository.Interfaces.RepoToDb;

namespace AppStoreFramework.Repository.Implementations.RepoToDb
{
    public class RepoToDbMapper : IRepoToDbMapper
    {
        private IAddAppCommand addCommand;
        private IUpdateAppCommand updateCommand;
        private readonly List<IStoreApp> appsInMemory;
        private IGetAppsQuery appQuery;


        public RepoToDbMapper(List<IStoreApp> appsInMemory, IGetAppsQuery appQuery, IAddAppCommand addCommand, IUpdateAppCommand updateCommand)
        {
            this.appsInMemory = appsInMemory;
            this.appQuery = appQuery;
            this.updateCommand = updateCommand;
            this.addCommand = addCommand;
        }

        public bool SaveAppsToDb( )
        {
            var success = true;
            foreach (var appInMemory in appsInMemory)
            {
                var appInDb = appQuery.GetAppById(appInMemory.Id);
                if(appInDb != null)
                {
                    success &= UpdateExisitngApp(appInMemory);
                }
                else
                {
                    success &= InsertNewApp(appInMemory);
                }
            }
            return success;
        }

        internal bool UpdateExisitngApp(IStoreApp appFromMemory)
        {
           return updateCommand.Update(appFromMemory);
        }
        internal bool InsertNewApp(IStoreApp appFromMemory)
        {
            return addCommand.AddApp(appFromMemory);
        }
    }
}
