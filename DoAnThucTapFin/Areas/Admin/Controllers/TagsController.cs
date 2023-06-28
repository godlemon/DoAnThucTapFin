﻿using System;
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
using DoAnThucTapFin.Areas.Admin.Models;

namespace DoAnThucTapFin.Areas.Admin.Controllers
{
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
                if (file != null)
                {
                    var filePath = UploadPathConstant.TagPath;
                    var savedFileName = await _storageService.SaveFileAsync(file, filePath);
                    tags.Image = savedFileName;
                }
                _context.Update(tags);
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tags == null)
            {
                return Problem("Entity set 'ADbContext.tags'  is null.");
            }
            var tags = await _context.tags.FindAsync(id);
            if (tags != null)
            {
                _context.tags.Remove(tags);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tagsExists(int id)
        {
            return (_context.tags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}