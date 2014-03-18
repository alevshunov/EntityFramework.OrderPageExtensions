using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KudesniK.EntityFramework.OrderPageExtensions.Tests
{
    [TestClass]
    public class PagingExtensionsTests
    {
        [TestMethod]
        public void PageSelectionFullPageTest()
        {
            var data = Enumerable.Range(1, 1000).AsQueryable().OrderBy(it => it);
            var result = data.Page(3, 20);
            Assert.AreEqual(20, result.Count());
            Assert.AreEqual(41, result.First());
            Assert.AreEqual(60, result.Last());
        }

        [TestMethod]
        public void PageSelectionLastPageTest()
        {
            var data = Enumerable.Range(1, 55).AsQueryable().OrderBy(it => it);
            var result = data.Page(3, 25);
            Assert.AreEqual(5, result.Count());
            Assert.AreEqual(51, result.First());
            Assert.AreEqual(55, result.Last());
        }

        [TestMethod]
        public void WrongPageSelectionTest()
        {
            var data = Enumerable.Range(1, 55).AsQueryable().OrderBy(it => it);
            try
            {
                var result = data.Page(0, 25);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("page", ex.ParamName);
            }
        }

        [TestMethod]
        public void WrongPageSizeTest()
        {
            var data = Enumerable.Range(1, 55).AsQueryable().OrderBy(it => it);
            try
            {
                var result = data.Page(2, 0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("pageSize", ex.ParamName);
            }
        }
    }
}
