using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnThucTapFin.Models;
using DoAnThucTapFin.Services;
using DoAnThucTapFin.Constants;
using DoAnThucTapFin.Data;
using DoAnThucTapFin.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DoAnThucTapFin.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;

        public ProductsController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {

              return _context.products != null ? 
                          View(await _context.products.Include(p => p.tags).ToListAsync()) :
                          Problem("Entity set 'ADbContext.Products'  is null.");
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products.Include(p => p.tags)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.Tags = new SelectList(_context.tags, "Id", "Name"); // Lấy danh sách các tag để hiển thị trong view
            return View();
        }
        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Resolution,Price,Quantity,TagId,Status")] Product product, IFormFile file)
        {
            ViewBag.Tags = new SelectList(_context.tags, "Id", "Name", product.TagId);
            if (file != null)
            {
                var filePath = UploadPathConstant.ProductPath;
                var savedFileName = await _storageService.SaveFileAsync(file, filePath);
                product.Productimg = savedFileName;
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            ViewBag.Tags = new SelectList(_context.tags, "Id", "Name", product.TagId);
            ViewBag.CurrentProductImage = product.Productimg;

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Productimg,Brand,Resolution,Quantity,TagId,Status")] Product product, IFormFile file)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            try
            {
                var currentProduct = await _context.products.FindAsync(id);

                if (currentProduct != null)
                {
                    if (file != null)
                    {
                        var filePath = UploadPathConstant.ProductPath;
                        var savedFileName = await _storageService.UpdateFileAsync(file, product.Productimg, filePath);
                        currentProduct.Productimg = savedFileName;
                    }

                    currentProduct.Name = product.Name;
                    currentProduct.Price = product.Price;
                    currentProduct.Brand = product.Brand;
                    currentProduct.Resolution = product.Resolution;
                    currentProduct.Quantity = product.Quantity;
                    currentProduct.Status = product.Status;
                    currentProduct.TagId = product.TagId;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }



		// GET: Admin/Products/Delete/5
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.products == null)
			{
				return NotFound();
			}

			var product = await _context.products
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			_context.products.Remove(product);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.products == null)
			{
				return Problem("Entity set 'ADbContext.banners' is null.");
			}

			var product = await _context.products.FindAsync(id);
			if (product != null)
			{
				_context.products.Remove(product);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}

		private bool ProductExists(int id)
        {
          return (_context.products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
