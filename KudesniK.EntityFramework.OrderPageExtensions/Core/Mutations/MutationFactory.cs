using System;
using System.Linq.Expressions;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Instances;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Interfaces;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations
{
    internal class MutationFactory<TEntity>
    {
        private static MutationFactory<TEntity> _instance;

        public static MutationFactory<TEntity> Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MutationFactory<TEntity>();

                return _instance;
            }
        }

        private MutationFactory()
        {
        }

        public IComplexMutation<TEntity> CreateOrderByMutation<TExpression>(Expression<Func<TEntity, TExpression>> expression)
        {
            return new OrderByMutation<TEntity, TExpression>(expression);
        }

        public IComplexMutation<TEntity> CreateOrderByDescendingMutation<TExpression>(Expression<Func<TEntity, TExpression>> expression)
        {
            return new OrderByDescendingMutation<TEntity, TExpression>(expression);
        }
        public IComplexMutation<TEntity> CreateThenByMutation<TExpression>(Expression<Func<TEntity, TExpression>> expression)
        {
            return new ThenByMutation<TEntity, TExpression>(expression);
        }

        public IComplexMutation<TEntity> CreateThenByDescendingMutation<TExpression>(Expression<Func<TEntity, TExpression>> expression)
        {
            return new ThenByDescendingMutation<TEntity, TExpression>(expression);
        }
    }
}