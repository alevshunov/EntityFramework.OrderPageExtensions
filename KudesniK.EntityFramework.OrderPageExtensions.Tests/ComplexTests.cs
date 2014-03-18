using System;
using System.Linq;
using System.Linq.Expressions;
using KudesniK.EntityFramework.OrderPageExtensions.Core;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Base;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Mutations.Instances;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Storage;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;
using KudesniK.EntityFramework.OrderPageExtensions.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace KudesniK.EntityFramework.OrderPageExtensions.Tests
{
    [TestClass]
    public class ComplexTests
    {
        [TestMethod]
        public void BaseMutation_CreateOrder_Test()
        {
            Storage.Clear();

            Expression<Func<int,int>> expression = it => it%5;
            Expression<Func<int,int>> expression2 = it => it%6;

            var ob = new OrderBuilder<int, Orders>();
            
            ob.AddOrderBy(Orders.Order1, expression).ThenBy(expression2);

            var mutationStorage = Storage.Instance.GetMutationStorage<int, Orders>();
            Assert.IsNotNull(mutationStorage, "Invalid mutation storage.");

            var mutation = mutationStorage.GetMutation(Orders.Order1);
            Assert.IsNotNull(mutation, "Invalid mutation.");

            Assert.IsInstanceOfType(mutation[OrderDirection.Asc], typeof(OrderByMutation<int,int>));
            Assert.IsInstanceOfType(mutation[OrderDirection.Desc], typeof(OrderByDescendingMutation<int,int>));

            Assert.AreEqual(expression, (mutation[OrderDirection.Asc] as BaseExpressionMutation<int, int>).Expression);
            Assert.AreEqual(expression, (mutation[OrderDirection.Desc] as BaseExpressionMutation<int, int>).Expression);


            var ascSubMutation = (mutation[OrderDirection.Asc] as BaseMutation<int>).SubMutation;
            var descSubMutation = (mutation[OrderDirection.Desc] as BaseMutation<int>).SubMutation;

            Assert.IsNotNull(ascSubMutation, "Invalid sub mutation.");
            Assert.IsNotNull(descSubMutation, "Invalid sub mutation.");


            Assert.IsInstanceOfType(ascSubMutation, typeof(ThenByMutation<int, int>));
            Assert.IsInstanceOfType(descSubMutation, typeof(ThenByDescendingMutation<int, int>));

            Assert.AreEqual(expression2, (ascSubMutation as BaseExpressionMutation<int, int>).Expression);
            Assert.AreEqual(expression2, (descSubMutation as BaseExpressionMutation<int, int>).Expression);
        }

        [TestMethod]
        public void Simple_IntOrder_Test()
        {
            Storage.Clear();

            new OrderBuilder<int, Orders>()
                .AddOrderBy(Orders.Order1, it => it%5);

            var source = Enumerable.Range(1, 1000).AsQueryable();

            var result_1 = source.OrderBy(Orders.Order1, OrderDirection.Desc).ToArray();
            var expect_1 = source.OrderByDescending(it => it%5).ToArray();

            for (var i=0; i<Math.Max(result_1.Length, expect_1.Length);i++)
                Assert.AreEqual(expect_1[i], result_1[i]);
        }        
        
        [TestMethod]
        public void Simpe_ThenBy_Test()
        {
            Storage.Clear();

            new OrderBuilder<int, Orders>()
                .AddOrderBy(Orders.Order2, it => it%5).ThenBy(it => it%3, OrderDirection.Desc);

            var source = Enumerable.Range(1, 1000).AsQueryable();

            var result_1 = source.OrderBy(Orders.Order2, OrderDirection.Desc).ToArray();
            var expect_1 = source.OrderByDescending(it => it%5).ThenBy(it => it%3).ToArray();

            for (var i=0; i<Math.Max(result_1.Length, expect_1.Length);i++)
                Assert.AreEqual(expect_1[i], result_1[i]);
        }
    }
}
