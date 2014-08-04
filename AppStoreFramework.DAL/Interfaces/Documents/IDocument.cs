using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreFramework.DAL.Interfaces.Documents
{
    public interface IDocument
    {
        Guid Id { get; set; }
        string GetLocation();
    }
}
