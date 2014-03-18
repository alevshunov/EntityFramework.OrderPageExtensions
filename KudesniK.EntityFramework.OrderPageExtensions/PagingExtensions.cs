using System;
using System.Linq;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;
using KudesniK.EntityFramework.OrderPageExtensions.DataTypes;

namespace KudesniK.EntityFramework.OrderPageExtensions
{
    /// <summary>
    /// Classic extensions for paging.
    /// </summary>
    public static class PagingExtensions
    {
        /// <summary>
        /// Apply paging for a query.
        /// </summary>
        /// <typeparam name="TQuery">Type of entity in query.</typeparam>
        /// <param name="query">Query for paging.</param>
        /// <param name="page">Page index for selection. 1-based.</param>
        /// <param name="pageSize">Itemms per page.</param>
        /// <returns></returns>
        public static IQueryable<TQuery> Page<TQuery>(this IOrderedQueryable<TQuery> query, int page, int pageSize)
        {
            if (page < 1)
                throw new ArgumentOutOfRangeException("page", page, "Page index is 1-based.");
            
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "Page size can't be negative or zero.");

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Get page of data.
        /// </summary>
        /// <typeparam name="TQuery">Type of entity.</typeparam>
        /// <typeparam name="TOrder">Type of order.</typeparam>
        /// <param name="query">Source collection.</param>
        /// <param name="order">Order.</param>
        /// <param name="direction">Order direction</param>
        /// <param name="page">Page index. 1-based.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>Paged and ordered data.</returns>
        public static PagedData<OrderedData<TQuery[], TOrder>> Paged<TQuery, TOrder>(this IQueryable<TQuery> query, TOrder order, OrderDirection direction, int page, int pageSize)
        {
            var pagedData = query.OrderBy(order, direction).Page(page, pageSize);
            var data = new PagedData<OrderedData<TQuery[], TOrder>>(page, query.Count(), pageSize);
            data.Data = new OrderedData<TQuery[], TOrder>(order, direction);
            data.Data.Data = pagedData.ToArray();
            return data;
        }
    }
}