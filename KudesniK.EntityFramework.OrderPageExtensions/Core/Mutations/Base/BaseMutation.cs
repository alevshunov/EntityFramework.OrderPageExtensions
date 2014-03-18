using System.Linq;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Interfaces;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Base
{
    internal abstract class BaseMutation<TEntity> : IComplexMutation<TEntity>
    {
        internal IComplexMutation<TEntity> SubMutation;

        public IQueryable<TEntity> Apply(IQueryable<TEntity> source)
        {
            var result = MutateSource(source);
            
            if (SubMutation != null)
                result = SubMutation.Apply(result);

            return result;
        }

        public void AddSubMutation(IComplexMutation<TEntity> subMutation)
        {
            if (SubMutation == null)
                SubMutation = subMutation;
            else 
                SubMutation.AddSubMutation(subMutation);
        }

        protected abstract IQueryable<TEntity> MutateSource(IQueryable<TEntity> source);
    }
}