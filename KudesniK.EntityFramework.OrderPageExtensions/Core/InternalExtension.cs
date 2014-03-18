using System.Linq;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;

namespace KudesniK.EntityFramework.OrderPageExtensions.Core
{
    internal class InternalExtension
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity, TOrder>(IQueryable<TEntity> source, TOrder order, OrderDirection direction)
        {
            var mutationStorage = Storage.Storage.Instance.GetMutationStorage<TEntity, TOrder>();
            var mutation = mutationStorage.GetMutation(order);
            var result = mutation[direction].Apply(source);
            return result as IOrderedQueryable<TEntity>;
        }
    }
}
