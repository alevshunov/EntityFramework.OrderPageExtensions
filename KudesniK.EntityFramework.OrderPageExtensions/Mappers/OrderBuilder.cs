using System;
using System.Linq.Expressions;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Storage;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;

namespace KudesniK.EntityFramework.OrderPageExtensions.Mappers
{
    /// <summary>
    /// OrderBy mapper.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity for manipulating.</typeparam>
    /// <typeparam name="TOrder">Type of order indicator.</typeparam>
    public class OrderBuilder<TEntity, TOrder>
    {
        /// <summary>
        /// Common storage for all mutation with entity.
        /// </summary>
        private readonly MutationInfoStorage<TEntity, TOrder> _infoStorage;
        
        /// <summary>
        /// Create entry point for mapping.
        /// </summary>
        public OrderBuilder()
        {
            _infoStorage = Storage.Instance.CreateMutationStorage<TEntity, TOrder>();
        }

        /// <summary>
        /// Create OrderBy map for specific order indicator.
        /// </summary>
        /// <typeparam name="TExpression">Type of expression.</typeparam>
        /// <param name="order">Order indicator.</param>
        /// <param name="expression">Expression.</param>
        /// <param name="defaultDirection">Default ordering direction for ASC mode. For DESC mode direction will be inverted.</param>
        /// <returns>ThenBy for current expression and OrderBy mapper.</returns>
        public ThenByMapper<TEntity, TOrder> AddOrderBy<TExpression>(TOrder order, Expression<Func<TEntity, TExpression>> expression, OrderDirection defaultDirection = OrderDirection.Asc)
        {
            var mutationInfo = new MutationInfo<TEntity>();
            _infoStorage.AddMutation(order, mutationInfo);

            mutationInfo[defaultDirection] = MutationFactory<TEntity>.Instance.CreateOrderByMutation(expression);
            mutationInfo[defaultDirection.Reverse()] = MutationFactory<TEntity>.Instance.CreateOrderByDescendingMutation(expression);

            return new ThenByMapper<TEntity, TOrder>(mutationInfo, this);
        }
    }
}