using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnThucTapFin.Models;
using DoAnThucTapFin.Constants;
using DoAnThucTapFin.Services;
using DoAnThucTapFin.Data;
using Microsoft.AspNetCore.Authorization;
using DoAnThucTapFin.Areas.Admin.Models;
using System.Data;


namespace DoAnThucTapFin.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    [Area("Admin")]
    public class tagsController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IStorageService _storageService;


		public tagsController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
			_storageService = storageService;

		}

		// GET: Admin/tags
		public async Task<IActionResult> Index()
        {
            return _context.tags != null ?
                        View(await _context.tags.ToListAsync()) :
                        Problem("Entity set 'ADbContext.tags'  is null.");
        }

        // GET: Admin/tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tags == null)
            {
                return NotFound();
            }

            var tags = await _context.tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tags == null)
            {
                return NotFound();
            }

            return View(tags);
        }

        // GET: Admin/tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Description")] Tags tags, IFormFile file)
        {
			if (file != null)
			{
				var filePath = UploadPathConstant.TagPath;
				var savedFileName = await _storageService.SaveFileAsync(file, filePath);
				tags.Image = savedFileName;
			}
			_context.Add(tags);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tags == null)
            {
                return NotFound();
            }

            var tags = await _context.tags.FindAsync(id);
            ViewBag.CurrentTagsImage = tags.Image;
            if (tags == null)
            {
                return NotFound();
            }
            return View(tags);
        }

        // POST: Admin/tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] Tags tags, IFormFile file)
        {
            if (id != tags.Id)
            {
                return NotFound();
            }
            try
            {
                var currentTags = await _context.tags.FindAsync(id);

                if (currentTags != null)
                {
                    if (file != null)
                    {
                        var filePath = UploadPathConstant.TagPath;
                        var savedFileName = await _storageService.UpdateFileAsync(file, tags.Image, filePath);
                        currentTags.Image = savedFileName;
                    }

                    currentTags.Name = tags.Name;
                    currentTags.Description = tags.Description;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tagsExists(tags.Id))
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

        // GET: Admin/tags/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tags == null)
            {
                return NotFound();
            }

            var tag = await _context.tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.tags.Remove(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.products == null)
            {
                return Problem("Entity set 'ADbContext.tags' is null.");
            }

            var tag = await _context.tags.FindAsync(id);
            if (tag != null)
            {
                _context.tags.Remove(tag);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private bool tagsExists(int id)
        {
            return (_context.tags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
