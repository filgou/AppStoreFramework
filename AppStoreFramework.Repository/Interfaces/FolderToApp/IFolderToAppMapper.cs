﻿using System.IO;
using System.Security.Cryptography.X509Certificates;
using AppStoreFramework.DAL.Interfaces.StoreApp;

namespace AppStoreFramework.Repository.Interfaces.FolderToApp
{
    public interface IFolderToAppMapper
    {
        string WorkingDirectory { get; set; }
        IStoreApp LoadApp();
        bool CheckIsValidFolder();
    }
}
