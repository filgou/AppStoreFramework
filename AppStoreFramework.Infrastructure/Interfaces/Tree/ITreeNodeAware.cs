using AppStoreFramework.Infrastructure.Implementations.Tree;

namespace AppStoreFramework.Infrastructure.Interfaces.Tree
{
    public interface ITreeNodeAware<T>
    {
        TreeNode<T> Node { get; set; }
    }
}
