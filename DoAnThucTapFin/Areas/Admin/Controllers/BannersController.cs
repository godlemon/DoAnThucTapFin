using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnThucTapFin.Areas.Admin.Models;
using DoAnThucTapFin.Services;
using DoAnThucTapFin.Constants;
using DoAnThucTapFin.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DoAnThucTapFin.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    [Area("Admin")]
    public class BannersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;

        public BannersController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        // GET: Admin/Banners
        public async Task<IActionResult> Index()
        {
              return _context.banners != null ? 
                          View(await _context.banners.ToListAsync()) :
                          Problem("Entity set 'ADbContext.banners'  is null.");
        }

        // GET: Admin/Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.banners == null)
            {
                return NotFound();
            }

            var banner = await _context.banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Admin/Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url,active")] Banner banner, IFormFile file)
        {
            if (file != null)
            {
                string filePath = UploadPathConstant.BannerPath;
                var savedFileName = await _storageService.SaveFileAsync(file, filePath);
                banner.Url = savedFileName;
            }
            _context.Add(banner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.banners == null)
            {
                return NotFound();
            }

            var banner = await _context.banners.FindAsync(id);
            ViewBag.CurrentBannerImage = banner.Url;
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url,Active")] Banner banner, IFormFile file)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }
            try
            {
                var currentBanner = await _context.banners.FindAsync(id);

                if (currentBanner != null)
                {
                    if (file != null)
                    {
                        var bannerPath = UploadPathConstant.BannerPath;
                        var savedFileName = await _storageService.UpdateFileAsync(file, banner.Url, bannerPath);
                        currentBanner.Url = savedFileName;
                    }

                    currentBanner.Name = banner.Name;
                    currentBanner.Active = banner.Active;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(banner.Id))
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

        // GET: Admin/Banners/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.banners == null)
            {
                return NotFound();
            }

            var banner = await _context.banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            _context.banners.Remove(banner);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.banners == null)
            {
                return Problem("Entity set 'ADbContext.banners' is null.");
            }

            var banner = await _context.banners.FindAsync(id);
            if (banner != null)
            {
                _context.banners.Remove(banner);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }



        private bool BannerExists(int id)
        {
          return (_context.banners?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
