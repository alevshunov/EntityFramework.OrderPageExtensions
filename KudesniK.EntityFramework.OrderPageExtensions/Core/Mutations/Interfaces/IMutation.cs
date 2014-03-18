using System.Linq;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Interfaces
{
    internal interface IMutation<TEntity>
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> source);
    }
}