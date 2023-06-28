using Microsoft.AspNetCore.Mvc;
using DoAnThucTapFin.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using DoAnThucTapFin.Data;

namespace DoAnThucTapFin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var bannersQuery = from b in dbContext.banners
                               orderby b.Id descending
                               select b;

            ViewBag.Banners = await bannersQuery.ToListAsync();

            var tagsQuery = from t in dbContext.tags
                            select t;
            ViewBag.Tags = await tagsQuery.ToListAsync();
            var ProductsQuery = from t in dbContext.products
                            select t;
            ViewBag.Product = await ProductsQuery.ToListAsync();
            return View();
        }
        public IActionResult category()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}