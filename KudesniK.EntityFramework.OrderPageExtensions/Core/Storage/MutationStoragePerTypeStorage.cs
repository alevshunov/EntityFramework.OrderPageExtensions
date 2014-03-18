using System;
using System.Collections.Generic;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Storage
{
    internal class MutationStoragePerTypeStorage
    {
        private readonly Dictionary<Tuple<Type, Type>, IMutationPerOrderStorage> _storage = new Dictionary<Tuple<Type, Type>, IMutationPerOrderStorage>();

        public MutationInfoStorage<TEntity, TOrder> GetMutationStorage<TEntity, TOrder>()
        {
            return _storage[Key<TEntity, TOrder>()] as MutationInfoStorage<TEntity, TOrder>;
        }

        public MutationInfoStorage<TEntity, TOrder> CreateMutationStorage<TEntity, TOrder>()
        {
            var key = Key<TEntity, TOrder>();
            if (!_storage.ContainsKey(key))
            {
                _storage[key] = new MutationInfoStorage<TEntity, TOrder>();
            }

            return _storage[key] as MutationInfoStorage<TEntity, TOrder>;
        }

        private Tuple<Type, Type> Key<TEntity, TOrder>()
        {
            var type1 = typeof(TEntity);
            var type2 = typeof(TOrder);
            var key = new Tuple<Type, Type>(type1, type2);
            return key;
        }

        public void Clear()
        {
            _storage.Clear();
        }
    }
}