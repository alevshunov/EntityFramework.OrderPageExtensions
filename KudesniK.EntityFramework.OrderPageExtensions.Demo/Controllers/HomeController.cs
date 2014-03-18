using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using KudesniK.EntityFramework.OrderPageExtensions.Core.Types;
using KudesniK.EntityFramework.OrderPageExtensions.Demo.Helper;
using KudesniK.EntityFramework.OrderPageExtensions.Demo.Models;

namespace KudesniK.EntityFramework.OrderPageExtensions.Demo.Controllers
{
    public class HomeController : Controller
    {
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
	}
}