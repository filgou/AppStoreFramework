using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreFramework.DAL.Interfaces
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
