using System.Collections.Generic;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Interfaces;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Storage
{
    internal class MutationInfo<TEntity>
    {
        private readonly Dictionary<OrderDirection, IComplexMutation<TEntity>> _mutations = new Dictionary<OrderDirection,IComplexMutation<TEntity>>();

        public IComplexMutation<TEntity> this[OrderDirection direction]
        {
            get { return _mutations[direction]; }
            set { _mutations[direction] = value; }
        }
    }
}