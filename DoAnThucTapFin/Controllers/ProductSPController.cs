using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnThucTapFin.Controllers
{
	public class ProductSPController : Controller
	{
        private readonly ILogger<ProductSPController> _logger;
        private readonly ApplicationDbContext dbContext;

        public ProductSPController(ILogger<ProductSPController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }
		public async Task<IActionResult> DetailAsync (int productID)
		{
			var product = await dbContext.products.FindAsync(productID);

			// Lấy giá trị của tagsname từ Tags.name
			var tag = await dbContext.tags.FindAsync(product.TagId);
			ViewBag.Product = product;
			ViewBag.tagsname = tag?.Name;
			return View();
		}
	}
}
