using System;
using System.Globalization;

namespace Hocoma.AppStore.Infrastructure.Implementations.Tree
{
    public class TreeNode<T> : IDisposable
    {
        public TreeNode(T value)
        {
            Value = value;
            Parent = null;
            Children = new TreeNodeList<T>(this);
        }

        public TreeNode(T value, TreeNode<T> parent)
        {
            Value = value;
            Parent = parent;
            Children = new TreeNodeList<T>(this);
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
            if (Value is IDisposable)
            {
                if (DisposeTraversal == TreeTraversalType.BottomUp)
                {
                    foreach (TreeNode<T> node in Children)
                    {
                        node.Dispose();
                    }
                }

                (Value as IDisposable).Dispose();

                if (DisposeTraversal == TreeTraversalType.TopDown)
                {
                    foreach (TreeNode<T> node in Children)
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
            if (Disposing != null)
            {
                Disposing(this, EventArgs.Empty);
            }
        }

        public void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public override string ToString()
        {
            string description = string.Empty;
            if (Value != null)
            {
                description = "[" + this.Value + "] ";
            }

            return description + "Depth=" + this.Depth + ", Children="
              + Children.Count.ToString(CultureInfo.InvariantCulture);
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
            foreach (TreeNode<Task> childTreeNode in Node.Children)
            {
                childTreeNode.Value.MarkComplete();
            }

            // now that all decendents are complete, mark this task complete
            Complete = true;
        }
    }

}
