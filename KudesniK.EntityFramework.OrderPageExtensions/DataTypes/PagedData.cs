using System;

namespace KudesniK.EntityFramework.OrderPageExtensions.DataTypes
{
    /// <summary>
    /// Decorator for paged data.
    /// </summary>
    /// <typeparam name="TPageData">Type of paged data.</typeparam>
    public class PagedData<TPageData>
    {
        ///// <summary>
        ///// Create instance of a paged data with specific page index and page count.
        ///// </summary>
        ///// <param name="page">Selected page. 1-based.</param>
        ///// <param name="pagesCount"></param>
        //public PagedData(int page, int pagesCount)
        //{
        //    if (page < 1)
        //        throw new ArgumentOutOfRangeException("page", page, "Current page should be 1-based.");
        //    if (pagesCount < page && pagesCount != 0 && page != 1)
        //        throw new ArgumentOutOfRangeException("page", "Current page is larger than pages count.");

        //    Page = page;
        //    PagesCount = pagesCount;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsCount"></param>
        /// <param name="pageSize"></param>
        public PagedData(int page, int itemsCount, int pageSize)
        {
            if (pageSize < 1)
                throw new ArgumentException("Incorrect page size.", "pageSize");

            Page = page;
            ItemsCount = itemsCount;
            PagesCount = (int) Math.Ceiling((double)itemsCount / pageSize);
        }

        /// <summary>
        /// Current page.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Total pages count.
        /// </summary>
        public int PagesCount { get; private set; }

        /// <summary>
        /// Count of items.
        /// </summary>
        public int ItemsCount { get; set; }

        /// <summary>
        /// The paged data.
        /// </summary>
        public TPageData Data { get; set; }
    }
}