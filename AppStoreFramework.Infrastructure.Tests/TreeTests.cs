using System;
using System.Linq;
using AppStoreFramework.DAL.Implementations.StoreApp;
using AppStoreFramework.Infrastructure.Implementations.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppStoreFramework.Infrastructure.Tests
{
    [TestClass]
    public class TreeTests
    {
        private StoreApp dummyStoreApp = new StoreApp(null) {NameId = "n", CategoryId = "c", SubCategoryId = "s"};

        [TestMethod]
        public void TestRoot()
        {
            var t = new Tree<int>(5);
            Assert.AreEqual(t.ToString(),"[5] Depth=0, Children=0");


            var t1 = new Tree<StoreApp>(this.dummyStoreApp);
            Assert.AreEqual(t1.ToString(), "["+this.dummyStoreApp+"] Depth=0, Children=0");
        }

        [TestMethod]
        public void TestDisposeNotDisposableValue()
        {
            var t = new Tree<int>(5);
            t.Children.Add(1);
            var c = t.Children.FirstOrDefault();
            Assert.AreEqual(t.ToString(), "[5] Depth=0, Children=1");
            Assert.AreEqual(c.ToString(), "[1] Depth=1, Children=0");
            t.Dispose();
            Assert.AreEqual(t.ToString(), "[5] Depth=0, Children=1");
        }

        [TestMethod]
        public void TestDisposeDisposableValue()
        {
            var disposableobject = new Mock<IDisposable>();
            var disposableobject2 = new Mock<IDisposable>();

            var tree = new Tree<IDisposable>(disposableobject.Object);
            tree.DisposeTraversal = TreeTraversalType.BottomUp;
            tree.Children.Add(disposableobject2.Object);
            var child = tree.Children.FirstOrDefault();
            Assert.AreEqual(tree.Children.Count, 1);
            Assert.AreEqual(child.Children.Count, 0);
            tree.Dispose();
            disposableobject2.Verify(o => o.Dispose(), Times.Once);
            disposableobject2.Verify(o => o.Dispose(), Times.Once);
            Assert.AreEqual(tree.Children.Count, 1);
        }
    }
}
