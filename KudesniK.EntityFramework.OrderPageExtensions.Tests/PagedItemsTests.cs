using KudesniK.EntityFramework.OrderPageExtensions.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KudesniK.EntityFramework.OrderPageExtensions.Tests
{
    [TestClass]
    public class PagedItemsTests
    {
        [TestMethod]
        public void ZeroItemsCountTest()
        {
            var pagedItems = new PagedData<object>(1, 0, 10);
            Assert.AreEqual(1, pagedItems.Page, "Incorrect current page.");
            Assert.AreEqual(0, pagedItems.PagesCount, "Incorrect pages count.");
            Assert.AreEqual(0, pagedItems.ItemsCount, "Incorrect items count");
        }

        //[TestMethod]
        //public void EmptyDataTest()
        //{
        //    var pagedItems = new PagedData<object>(1, 0);
        //    Assert.AreEqual(1, pagedItems.Page, "Incorrect current page.");
        //    Assert.AreEqual(0, pagedItems.PagesCount, "Incorrect pages count.");
        //}

        [TestMethod]
        public void FullFilledPageTest()
        {
            var pagedItems = new PagedData<object>(2, 100, 25);
            Assert.AreEqual(2, pagedItems.Page, "Incorrect current page.");
            Assert.AreEqual(4, pagedItems.PagesCount, "Incorrect pages count.");
            Assert.AreEqual(100, pagedItems.ItemsCount, "Incorrect items count");
        }

        [TestMethod]
        public void PartlyFilledLastPageTest()
        {
            var pagedItems = new PagedData<object>(2, 70, 25);
            Assert.AreEqual(2, pagedItems.Page, "Incorrect current page.");
            Assert.AreEqual(3, pagedItems.PagesCount, "Incorrect pages count.");
            Assert.AreEqual(70, pagedItems.ItemsCount, "Incorrect items count");
        }
    }
}
