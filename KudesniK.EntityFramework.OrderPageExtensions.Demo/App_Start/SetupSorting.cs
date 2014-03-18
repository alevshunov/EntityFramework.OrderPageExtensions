using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;
using KudesniK.EntityFramework.OrderPageExtensions.Demo.Models;
using KudesniK.EntityFramework.OrderPageExtensions.Mappers;

namespace KudesniK.EntityFramework.OrderPageExtensions.Demo
{
    public class SetupSorting
    {
        public static void Setup()
        {
            new OrderBuilder<Country, CountryOrder>()
                .AddOrderBy(CountryOrder.Rank, it => it.Rank)
                .AddOrderBy(CountryOrder.Name, it => it.Name)

                // You can use .ThenBy():
                .AddOrderBy(CountryOrder.Date, it => it.Date).ThenBy(it => it.Name)
                .AddOrderBy(CountryOrder.Population, it => it.Population).ThenBy(it => it.Name)
                .AddOrderBy(CountryOrder.Percent, it => it.Percent).ThenBy(it => it.Name)
                .AddOrderBy(CountryOrder.Source, it => it.Source).ThenBy(it => it.Name)

                // Complex order with OrderDirection magic: for asc direction the query will be looks like
                // .OrderByDescending(it => it.Date.Year).ThenBy(it => it.Rank), and for desc direction
                // the order will be reversed to .OrderBy(it => it.Date.Year).ThenByDescending(it => it.Rank).
                .AddOrderBy(CountryOrder.RelevanceTop, it => it.Date.Year, OrderDirection.Desc).ThenBy(it => it.Rank);


            // Create new OrderBuilder for each entity per order-enum:
            // new OrderBuilder<AnotherEntity, EntityOrderEnum>()
            // .AddOrderBy(...);
        }
    }
}