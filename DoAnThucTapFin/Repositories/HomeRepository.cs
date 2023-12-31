﻿using DoAnThucTapFin;
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
		public async Task<IEnumerable<Product>> GetProduct(string sTerm = "", int tagid = 0, string brand = "", double minPrice = 0, double maxPrice = double.MaxValue)
		{
			sTerm = sTerm.ToLower();
			brand = brand.ToLower();

			IEnumerable<Product> products = await (from product in _db.products
												   join tag in _db.tags on product.TagId equals tag.Id
												   where (string.IsNullOrWhiteSpace(sTerm) || product.Name.ToLower().Contains(sTerm))
														 && (tagid == 0 || product.TagId == tagid)
														 && (string.IsNullOrWhiteSpace(brand) || product.Brand.ToLower() == brand)
														 && (product.Price >= minPrice && product.Price <= maxPrice)
                                                         && product.Status == true
                                                   select new Product
												   {
													   Id = product.Id,
													   Productimg = product.Productimg,
													   Brand = product.Brand,
													   Name = product.Name,
													   TagId = product.TagId,
													   Price = product.Price,
													   tagname = tag.Name,
                                                       Quantity = product.Quantity,
                                                       Quantitysell = product.Quantitysell
                                                   }).ToListAsync();

			return products;
		}


	}
}
