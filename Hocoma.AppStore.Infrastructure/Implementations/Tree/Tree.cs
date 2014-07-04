using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hocoma.AppStore.Infrastructure.Implementations.Tree
{

    public class Tree<T> : TreeNode<T>
    {
        public Tree(T RootValue)
            : base(RootValue)
        {
            Value = RootValue;
        }
    }
}
