namespace AppStoreFramework.Infrastructure.Implementations.Tree
{

    public class Tree<T> : TreeNode<T>
    {
        public Tree(T RootValue)
            : base(RootValue)
        {
            this.Value = RootValue;
        }
    }
}
