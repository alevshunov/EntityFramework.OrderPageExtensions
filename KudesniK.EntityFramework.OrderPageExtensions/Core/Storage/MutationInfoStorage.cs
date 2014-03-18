using System.Collections.Generic;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Storage
{
    internal class MutationInfoStorage<TEntity, TOrder> : IMutationPerOrderStorage
    {
        private readonly Dictionary<TOrder, MutationInfo<TEntity>> _storage = new Dictionary<TOrder, MutationInfo<TEntity>>();

        public MutationInfo<TEntity> GetMutation(TOrder order)
        {
            return _storage[order];
        }

        public void AddMutation(TOrder order, MutationInfo<TEntity> mutation)
        {
            _storage[order] = mutation;
        }
    }
}