using System;

namespace AppStoreFramework.DAL.Interfaces.Logging
{
    public interface ILogEntry
    {
        DateTime CreationDate { get; set; }
        string Category { get; set; }
        string Message { get; set; }
        string Sender { get; set; }
        string Details { get; set; }
        string Tags { get; set; }
    }
}
