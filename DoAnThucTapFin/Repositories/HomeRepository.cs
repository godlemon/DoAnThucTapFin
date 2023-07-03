using DoAnThucTapFin;
using DoAnThucTapFin.Areas.Admin.Models;
using DoAnThucTapFin.Data;
using Microsoft.EntityFrameworkCore;

namespace DoAnThucTapFin.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Tags>> tags()
        {
            return await _db.tags.ToListAsync();
        }
		public async Task<IEnumerable<Product>> GetProduct(string sTerm = "", int tagid = 0, string brand = "")
		{
			sTerm = sTerm.ToLower();
			brand = brand.ToLower();

			IEnumerable<Product> products = await (from product in _db.products
												   join tag in _db.tags on product.TagId equals tag.Id
												   where (string.IsNullOrWhiteSpace(sTerm) || product.Name.ToLower().StartsWith(sTerm))
												   && (tagid == 0 || product.TagId == tagid)
												   && (string.IsNullOrWhiteSpace(brand) || product.Brand.ToLower() == brand)
												   select new Product
												   {
													   Id = product.Id,
													   Productimg = product.Productimg,
													   Brand = product.Brand,
													   Name = product.Name,
													   TagId = product.TagId,
													   Price = product.Price,
													   tagname = tag.Name
												   }
												  ).ToListAsync();

			return products;
		}

	}
}
