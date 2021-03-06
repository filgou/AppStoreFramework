﻿using System;
using AppStoreFramework.DAL.Interfaces.Documents;

namespace AppStoreFramework.DAL.Interfaces.StoreApp
{
    public interface IStoreApp : IDocument
    {
        string NameId { get; set; }
        string CategoryId { get; set; }
        string SubCategoryId { get; set; }
       

        bool IsAvailable { get; set; }

        IStoreAppManifest Manifest { get; set; }
    }
}