namespace AppStoreFramework.Infrastructure.Implementations.Tree
{
    public interface ITreeNodeAware<T>
    {
        TreeNode<T> Node { get; set; }
    }
}
