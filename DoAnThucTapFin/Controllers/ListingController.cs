using DoAnThucTapFin.Controllers;
using DoAnThucTapFin.Data;
using DoAnThucTapFin;
using Microsoft.AspNetCore.Mvc;
using DoAnThucTapFin.Areas.Admin.Models;
using DoAnThucTapFin.Models.DTOs;

namespace DoAnThucTap.Controllers
{
	public class ListingController : Controller
	{
		private readonly ILogger<ListingController> _logger;
		private readonly ApplicationDbContext dbContext;
		private readonly IHomeRepository _homeRepository;

		public ListingController(ILogger<ListingController> logger, ApplicationDbContext dbContext, IHomeRepository homeRepository)
		{
			_logger = logger;
			this.dbContext = dbContext;
			_homeRepository = homeRepository;
		}
		public async Task<IActionResult> grid(string sterm = "", int tagid = 0, int page = 1)
		{
			const int pageSize = 12; // Số sản phẩm hiển thị trên mỗi trang

			IEnumerable<Product> products = await _homeRepository.GetProduct(sterm, tagid);

			// Tính toán số lượng trang dựa trên tổng số sản phẩm và kích thước trang
			int totalItems = products.Count();
			int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

			// Lấy danh sách sản phẩm cho trang hiện tại
			IEnumerable<Product> currentProducts = products.Skip((page - 1) * pageSize).Take(pageSize);

			IEnumerable<Tags> tags = await _homeRepository.tags();
			ProductDisplayModel productModel = new ProductDisplayModel
			{
				products = currentProducts,
				tags = tags,
				Pagination = new PaginationModel
				{
					CurrentPage = page,
					TotalPages = totalPages
				}
			};
			return View(productModel);
		}

		public async Task<IActionResult> large(string sterm="", int tagid=0, int page = 1)
		{
            const int pageSize = 12; // Số sản phẩm hiển thị trên mỗi trang

            IEnumerable<Product> products = await _homeRepository.GetProduct(sterm, tagid);

            // Tính toán số lượng trang dựa trên tổng số sản phẩm và kích thước trang
            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Lấy danh sách sản phẩm cho trang hiện tại
            IEnumerable<Product> currentProducts = products.Skip((page - 1) * pageSize).Take(pageSize);

            IEnumerable<Tags> tags = await _homeRepository.tags();
            ProductDisplayModel productModel = new ProductDisplayModel
            {
                products = currentProducts,
                tags = tags,
                Pagination = new PaginationModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages
                }
            };
            return View(productModel);
        }
	}
}
