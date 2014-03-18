using System.Linq;
using KudesniK.EntityFramework.OrderPageExtensions.Core;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;

namespace KudesniK.EntityFramework.OrderPageExtensions
{
    public static class OrderExtensions
    {
        /// <summary>
        /// Reverse order from Asc to Desc and from Desc to Asc.
        /// </summary>
        /// <param name="order">Order to reverse.</param>
        /// <returns>Reversed order.</returns>
        public static OrderDirection Reverse(this OrderDirection order)
        {
            return order == OrderDirection.Asc ? OrderDirection.Desc : OrderDirection.Asc;
        }

        /// <summary>
        /// Order source query by provided creterian.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TOrder">Type of order.</typeparam>
        /// <param name="source">Source data.</param>
        /// <param name="order">Order.</param>
        /// <param name="direction">Order direction.</param>
        /// <returns>Ordered items.</returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity, TOrder>(this IQueryable<TEntity> source, TOrder order, OrderDirection direction)
        {
            return InternalExtension.OrderBy(source, order, direction);
        }
    }
}