using System;
using System.Linq;
using System.Linq.Expressions;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Base;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Instances
{
    internal class ThenByDescendingMutation<TEntity, TExpression> : BaseExpressionMutation<TEntity, TExpression>
    {
        public ThenByDescendingMutation(Expression<Func<TEntity, TExpression>> expression) : base(expression)
        {
        }

        protected override IQueryable<TEntity> MutateSource(IQueryable<TEntity> source)
        {
            return (source as IOrderedQueryable<TEntity>).ThenByDescending(Expression);
        }
    }
}