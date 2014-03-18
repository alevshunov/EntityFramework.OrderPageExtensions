using System.Linq;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Base;

namespace KudesniK.EntityFramework.OrderPageExtensions.Tests
{
    internal class FakeMutation : BaseMutation<object>
    {
        private readonly IQueryable<object> _afterMutation;

        public FakeMutation(IQueryable<object> afterMutation)
        {
            _afterMutation = afterMutation;
        }

        protected override IQueryable<object> MutateSource(IQueryable<object> source)
        {
            return _afterMutation;
        }
    }
}