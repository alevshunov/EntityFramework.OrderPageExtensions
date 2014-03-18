using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;

namespace KudesniK.EntityFramework.OrderPageExtensions.DataTypes
{
    /// <summary>
    /// Decorator for ordered data.
    /// </summary>
    /// <typeparam name="TOrderedData">Type of ordered data.</typeparam>
    /// <typeparam name="TOrder">Type of order.</typeparam>
    public class OrderedData<TOrderedData, TOrder> 
    {
        /// <summary>
        /// Create instance of OrderedData class.
        /// </summary>
        /// <param name="order">Order key.</param>
        /// <param name="direction">Order direction.</param>
        public OrderedData(TOrder order, OrderDirection direction)
        {
            Order = order;
            Direction = direction;
        }

        /// <summary>
        /// Order key.
        /// </summary>
        public TOrder Order { get; private set; }

        /// <summary>
        /// Order direction.
        /// </summary>
        public OrderDirection Direction { get; private set; }

        /// <summary>
        /// Ordered data.
        /// </summary>
        public TOrderedData Data { get; set; }
    }
}