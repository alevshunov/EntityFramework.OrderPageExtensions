using System;
using System.Linq;
using System.Linq.Expressions;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Base;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Instances
{
    internal class ThenByMutation<TEntity, TExpression> : BaseExpressionMutation<TEntity, TExpression>
    {
        public ThenByMutation(Expression<Func<TEntity, TExpression>> expression) : base(expression)
        {
        }

        protected override IQueryable<TEntity> MutateSource(IQueryable<TEntity> source)
        {
            return (source as IOrderedQueryable<TEntity>).ThenBy(Expression);
        }
    }
}