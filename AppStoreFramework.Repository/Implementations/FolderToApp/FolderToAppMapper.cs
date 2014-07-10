using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.DAL.Interfaces.StoreApp;
using AppStoreFramework.Infrastructure.Interfaces.FileSystem;
using AppStoreFramework.Repository.Interfaces.FolderToApp;

namespace AppStoreFramework.Repository.Implementations.FolderToApp
{
    public class FolderToAppMapper : IFolderToAppMapper
    {
        internal string ManifestFileExtension =".manifest";
        private string workingDirectory;
        private readonly IManifestMapper manifestMapper;
        private readonly IFileSystem fileSystem;
        private IStoreAppManifest loadedManifest;
        private IStoreApp loadedStoreApp;

        public FolderToAppMapper(string workingDirectory, IManifestMapper manifestMapper, IFileSystem fileSystem)
        {
            this.workingDirectory = workingDirectory;
            this.manifestMapper = manifestMapper;
            this.fileSystem = fileSystem;
        }

        public string WorkingDirectory
        {
            get { return this.workingDirectory; }
            set { this.workingDirectory = value; }
        }

        public IStoreApp LoadApp()
        {
            if(CheckIsValidFolder())
            {
                loadedStoreApp = new StoreApp(loadedManifest);
               
            }
            return loadedStoreApp;
        }

        internal void InitializeStoreApp()
        {
            if(loadedStoreApp == null)
            {
                return;
            }
            loadedStoreApp.CategoryId = GetCategoryFromFileSystem();
            loadedStoreApp.SubCategoryId = GetSubategoryFromFileSystem();
            loadedStoreApp.NameId = GetNameFromFileSystem();
        }

        internal string TrimUnc(string uncpath)
        {
            return uncpath.TrimStart(@"\\".ToCharArray());
        }

        internal string GetCategoryFromFileSystem()
        {
            var path = workingDirectory;
            path = TrimUnc(path);
            var directories = path.Split("\\".ToCharArray());
            var depth = directories.Length;
            if(depth >= 2)
            {
                if(depth == 2)
                {
                    return directories[0];
                }
                else
                {
                    return directories[depth-3];
                }
            }
            else
            {
                 return string.Empty;
            }
        }

        internal string GetSubategoryFromFileSystem()
        {
            var path = workingDirectory;
            path = TrimUnc(path);
            var directories = path.Split("\\".ToCharArray());
            var depth = directories.Length;
            if (depth >= 3)
            {
                if (depth == 3)
                {
                    return directories[1];
                }
                else
                {
                    return directories[depth - 2];
                }
            }
            else
            {
                return string.Empty;
            }
        }

        internal string GetNameFromFileSystem()
        {
            var path = workingDirectory;
            path = TrimUnc(path);
            var directories = path.Split("\\".ToCharArray());
            var depth = directories.Length;
            if (depth >= 1)
            {
                if (depth == 1)
                {
                    return directories[0];
                }
                else
                {
                    return directories[depth - 1];
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public bool CheckIsValidFolder()
        {
            var isValid = CheckForValidContents();
            return isValid;
        }

        internal bool CheckForValidContents()
        {
            var contents = fileSystem.GetFiles(workingDirectory);

            if (!CheckForUniqueManifest(contents.ToList()))
            {
                throw new ArgumentException("None or multiple manifests found for single app");
            }
            var manifest = contents.FirstOrDefault(o => o.EndsWith(ManifestFileExtension));
           
            if (!LoadAndCheckForValidManifest(manifest))
            {
                throw new ArgumentException("Manifest was not valid");
            }
            return true;
        }

        internal bool CheckForUniqueManifest( List<string> filesList)
        {
            return filesList.Count(o => o.EndsWith(ManifestFileExtension)) == 1;
        }

        internal bool LoadAndCheckForValidManifest(string manifestFullName)
        {
            var manifestfile = new FileInfo(manifestFullName);
            loadedManifest = manifestMapper.LoadAppManifest(manifestfile);
            return loadedManifest.IsValid();
        }
    }
}
