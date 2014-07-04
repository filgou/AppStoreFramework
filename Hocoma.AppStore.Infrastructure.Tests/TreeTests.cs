using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hocoma.AppStore.DAL.Implementations.StoreApp;
using Hocoma.AppStore.Infrastructure.Implementations.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Hocoma.AppStore.Infrastructure.Tests
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


            var t1 = new Tree<StoreApp>(dummyStoreApp);
            Assert.AreEqual(t1.ToString(), "["+dummyStoreApp+"] Depth=0, Children=0");
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
