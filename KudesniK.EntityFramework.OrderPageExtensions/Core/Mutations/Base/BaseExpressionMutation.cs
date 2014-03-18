using System;
using System.Linq.Expressions;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Base
{
    internal abstract class BaseExpressionMutation<TEntity, TExpression> : BaseMutation<TEntity>
    {
        internal readonly Expression<Func<TEntity, TExpression>> Expression;

        protected BaseExpressionMutation(Expression<Func<TEntity, TExpression>> expression)
        {
            Expression = expression;
        }
    }
}