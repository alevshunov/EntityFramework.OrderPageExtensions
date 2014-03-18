using System;
using System.Linq.Expressions;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Storage;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;

namespace KudesniK.EntityFramework.OrderPageExtensions.Mappers
{
    /// <summary>
    /// OrderBy and ThenBy builder.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity for manipulating.</typeparam>
    /// <typeparam name="TOrder">Type of order indicator.</typeparam>
    public class ThenByMapper<TEntity, TOrder>
    {
        /// <summary>
        /// Information about last assigned to OrderBy mutation.
        /// </summary>
        private readonly MutationInfo<TEntity> _mutationInfo;

        /// <summary>
        /// Link to OrderBy buider.
        /// </summary>
        private readonly OrderBuilder<TEntity, TOrder> _orderBuilder;

        /// <summary>
        /// Create ThenBy mapper based on OrderBy expression.
        /// </summary>
        /// <param name="mutationInfo"></param>
        /// <param name="orderBuilder"></param>
        internal ThenByMapper(MutationInfo<TEntity> mutationInfo, OrderBuilder<TEntity, TOrder> orderBuilder)
        {
            _mutationInfo = mutationInfo;
            _orderBuilder = orderBuilder;
        }

        /// <summary>
        /// Add the ThenBy expression to last OrderBy expression.
        /// </summary>
        /// <typeparam name="TExpression">Type of expression.</typeparam>
        /// <param name="expression">Expression.</param>
        /// <param name="defaultDirection">Default ordering direction for ASC mode. For DESC mode direction will be inverted.</param>
        /// <returns>Current ThenBy mapper.</returns>
        public ThenByMapper<TEntity, TOrder> ThenBy<TExpression>(Expression<Func<TEntity, TExpression>> expression, OrderDirection defaultDirection = OrderDirection.Asc)
        {
            _mutationInfo[defaultDirection].AddSubMutation(MutationFactory<TEntity>.Instance.CreateThenByMutation(expression));
            _mutationInfo[defaultDirection.Reverse()].AddSubMutation(MutationFactory<TEntity>.Instance.CreateThenByDescendingMutation(expression));
            return this;
        }

        /// <summary>
        /// Add new OrderBy expression.
        /// </summary>
        /// <typeparam name="TExpression">Type of expression.</typeparam>
        /// <param name="order">Order indicator.</param>
        /// <param name="expression">OrderBy expression.</param>
        /// <param name="defaultDirection">Default ordering direction for ASC mode. For DESC mode direction will be inverted.</param>
        /// <returns>ThenBy mapper.</returns>
        public ThenByMapper<TEntity, TOrder> AddOrderBy<TExpression>(TOrder order, Expression<Func<TEntity, TExpression>> expression, OrderDirection defaultDirection = OrderDirection.Asc)
        {
            return _orderBuilder.AddOrderBy(order, expression, defaultDirection);
        }
    }
}