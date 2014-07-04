using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hocoma.AppStore.Infrastructure.Implementations.Tree
{
    public interface ITreeNodeAware<T>
    {
        TreeNode<T> Node { get; set; }
    }
}
