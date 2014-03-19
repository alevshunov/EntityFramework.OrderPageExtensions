### Example of using ordering in EntityFramework and ASP.NET MVC with enums
===

## The problem
---


Classic activity with EF is paging and ordering, by the issue is that using the swith is extra dirty:
```
public ActionResult Countries(string order = "name", string direction = "asc", int page = 1)
{
    using (var db = new Db())
    { 
        var countries = db.Countries.AsQueryable();

        if (direction == "asc")
        {
            switch (order)
            {
                case "name": countries = countries.OrderBy(it => it.Name); break;
                case "population": countries = countries.OrderBy(it => it.Population); break;
                // etc.
                default: countries = countries.OrderBy(it => it.Name); break;
            }
        }
        else
        {
            switch (order)
            {
                case "name": countries = countries.OrderByDescending(it => it.Name); break;
                case "population": countries = countries.OrderByDescending(it => it.Population); break;
                // etc.
                default: countries = countries.OrderByDescending(it => it.Name); break;
            }
        }

        return View(countries);
    }
}
```

The one of possible solution was to calculate Expressions at first and call the OrderBy method:
```
public ActionResult Countries(string order = "name", string direction = "asc", int page = 1)
{
    using (var db = new Db())
    { 
        var countries = db.Countries.AsQueryable();
        Expression<Func<Country, Object>> orderByExpression;
        switch (order)
        {
            case "name": orderByExpression = it => it.Name; break;
            case "population": orderByExpression = it => it.Population; break;
            // etc.
            default: orderByExpression = it => it.Name; break;
        }

        if (direction == "asc")
            countries = countries.OrderBy(orderByExpression);
        else
            countries = countries.OrderByDescending(orderByExpression);

        return View(countries);
    }
}
```

But the problem will be founded during sorting, for example by Int32 fields: 
```
Unable to cast the type 'System.Int32' to type 'System.Object'. LINQ to Entities only supports casting EDM primitive or enumeration types.
```

## Solution
---

Solution provided in this application contains several steps:
1. Create enum instead of string for sorting.
2. Map enum values with expressions at application start.
3. Call method from extension 'OrderBy', and pass enum instead of expression.

### 1. Create order-indicator enum instead of string
```
public enum CountryOrder
{
    Rank,
    Name,
    Population,
    RelevanceTop,
    Date,
    Percent,
    Source
}
```

### 2. Map enum values with expressions

SetupSorting.cs:
```
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
```

Global.asax:
```
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);

    SetupSorting.Setup();
}
```

### 3. Call extenshion method 'OrderBy', and pass enum instead of expression
```
public ActionResult Countries(CountryOrder order = CountryOrder.Name, OrderDirection direction = OrderDirection.Asc, int page = 1)
{
    using (var db = new Db())
    {
        // Paged method returns complex structure PagedData<OrderedData<Country[], CountryOrder>>
        // with whole information about order and page inside.
        var viewModel = db.Countries.Paged(order, direction, page, Settings.PageSize);
                
        // If you need only IOrderedQueryable<Country> there is a way:
        // IOrderedQueryable<Country> orderedData = db.Countries.OrderBy(order, direction);

        // Or if you need a IQueryable<Country> of a page you can add .Page(index, size):
        // IQueryable<Country> pageData = db.Countries.OrderBy(order, direction).Page(page, Settings.PageSize);

        return View(viewModel);
    }
}
```


### How to play

Download the repository, open solution, run the 'KudesniK.EntityFramework.OrderPageExtensions.Demo' web-application.

### Nuget

Now the library is available on nuget:
```
Install-Package KudesniK.EntityFramework.OrderPageExtensions
```
And with examples and small setup:
```
Install-Package KudesniK.EntityFramework.OrderPageExtensions.AspNet.Mvc
```