using System.Collections.Generic;

namespace AppStoreFramework.Infrastructure.Implementations.Tree
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
            Node.Parent = this.Parent;
            return Node;
        }

        public TreeNode<T> Add(T Value)
        {
            return Add(new TreeNode<T>(Value));
        }


        public override string ToString()
        {
            return "Count=" + this.Count.ToString();
        }


    }
}
