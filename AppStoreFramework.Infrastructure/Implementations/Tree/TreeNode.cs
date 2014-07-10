using System;
using System.Globalization;
using AppStoreFramework.Infrastructure.Interfaces.Tree;

namespace AppStoreFramework.Infrastructure.Implementations.Tree
{
    public class TreeNode<T> : IDisposable
    {
        public TreeNode(T value)
        {
            this.Value = value;
            this.Parent = null;
            this.Children = new TreeNodeList<T>(this);
        }

        public TreeNode(T value, TreeNode<T> parent)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new TreeNodeList<T>(this);
        }

 
        private TreeNode<T> parent;
        public TreeNode<T> Parent
        {
            get { return this.parent; }
            set
            {
                if (value == this.parent)
                {
                    return;
                }

                if (this.parent != null)
                {
                    this.parent.Children.Remove(this);
                }

                if (value != null && !value.Children.Contains(this))
                {
                    value.Children.Add(this);
                }

                this.parent = value;
            }
        }

        public TreeNode<T> Root
        {
            get
            {
                //return (Parent == null) ? this : Parent.Root;

                TreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return node;
            }
        }

        private TreeNodeList<T> children;
        public TreeNodeList<T> Children
        {
            get { return this.children; }
            private set { this.children = value; }
        }

        private T value;
        public T Value
        {
            get { return this.value; }
            set
            {
                this.value = value;

                if (this.value != null && this.value is ITreeNodeAware<T>)
                {
                    (this.value as ITreeNodeAware<T>).Node = this;
                }
            }
        }

        public int Depth
        {
            get
            {
                //return (Parent == null ? -1 : Parent.Depth) + 1;

                int depth = 0;
                TreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }
                return depth;
            }
        }

        private bool isDisposed;
        public bool IsDisposed
        {
            get { return this.isDisposed; }
        }

        private TreeTraversalType disposeTraversal = TreeTraversalType.BottomUp;
        public TreeTraversalType DisposeTraversal
        {
            get { return this.disposeTraversal; }
            set { this.disposeTraversal = value; }
        }

        public void Dispose()
        {
            CheckDisposed();
            OnDisposing();

            // clean up contained objects (in Value property)
            if (this.Value is IDisposable)
            {
                if (this.DisposeTraversal == TreeTraversalType.BottomUp)
                {
                    foreach (TreeNode<T> node in this.Children)
                    {
                        node.Dispose();
                    }
                }

                (this.Value as IDisposable).Dispose();

                if (this.DisposeTraversal == TreeTraversalType.TopDown)
                {
                    foreach (TreeNode<T> node in this.Children)
                    {
                        node.Dispose();
                    }
                }
            }

            this.isDisposed = true;
        }

        public event EventHandler Disposing;

        protected void OnDisposing()
        {
            if (this.Disposing != null)
            {
                this.Disposing(this, EventArgs.Empty);
            }
        }

        public void CheckDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public override string ToString()
        {
            string description = string.Empty;
            if (this.Value != null)
            {
                description = "[" + this.Value + "] ";
            }

            return description + "Depth=" + this.Depth + ", Children="
              + this.Children.Count.ToString(CultureInfo.InvariantCulture);
        }
        
    }


    public class Task : ITreeNodeAware<Task>
    {
        public bool Complete = false;

        private TreeNode<Task> node;
        public TreeNode<Task> Node
        {
            get { return this.node; }
            set
            {
                this.node = value;

                // do something when the Node changes
                // if non-null, maybe we can do some setup
            }
        }

        // recursive
        public void MarkComplete()
        {
            // mark all children, and their children, etc., complete
            foreach (TreeNode<Task> childTreeNode in this.Node.Children)
            {
                childTreeNode.Value.MarkComplete();
            }

            // now that all decendents are complete, mark this task complete
            this.Complete = true;
        }
    }

}
