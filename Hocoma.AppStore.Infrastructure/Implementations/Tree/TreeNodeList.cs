﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hocoma.AppStore.Infrastructure.Implementations.Tree
{
    public class TreeNodeList<T> : List<TreeNode<T>>
    {
        public TreeNode<T> Parent;

        public TreeNodeList(TreeNode<T> Parent)
        {
            this.Parent = Parent;
        }

        public new TreeNode<T> Add(TreeNode<T> Node)
        {
            base.Add(Node);
            Node.Parent = Parent;
            return Node;
        }

        public TreeNode<T> Add(T Value)
        {
            return Add(new TreeNode<T>(Value));
        }


        public override string ToString()
        {
            return "Count=" + Count.ToString();
        }


    }
}
