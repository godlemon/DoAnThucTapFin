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
        public async Task<IEnumerable<Product>> GetProduct(string sTerm = "", int tagid = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> products = await (from product in _db.products
                         join tag in _db.tags
                         on product.TagId equals tag.Id
                         where string.IsNullOrWhiteSpace(sTerm) || (product != null && product.Name.ToLower().StartsWith(sTerm))
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
            if (tagid > 0)
            {

                products = products.Where(a => a.TagId == tagid).ToList();
            }
            return products;

        }
    }
}
