namespace KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Interfaces
{
    internal interface IComplexMutation<TEntity> : IMutation<TEntity>
    {
        void AddSubMutation(IComplexMutation<TEntity> subMutation);
    }
}