using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenMinhDuyThong;
using NguyenMinhDuyThong.Models;

namespace NguyenMinhDuyThong.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly MainDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AdminController(MainDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Phones.ToListAsync());
        }

        // GET: Admin/Admin/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneModels = await _context.Phones
                .FirstOrDefaultAsync(m => m.IdPro == id);
            if (phoneModels == null)
            {
                return NotFound();
            }

            return View(phoneModels);
        }

        // GET: Admin/Admin/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("CreatePhones")]
        // POST: Admin/Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreatePhones([Bind("IdPro,NamePro,Price,Description,ImageFile")] PhoneModels phoneModels)
        {
            if (ModelState.IsValid)
            {
                if (phoneModels.ImageFile != null) { 
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(phoneModels.ImageFile.FileName);
                string extension = Path.GetExtension(phoneModels.ImageFile.FileName);
                phoneModels.CoverImageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await phoneModels.ImageFile.CopyToAsync(fileStream);
                }
                }
                _context.Add(phoneModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phoneModels);
        }

        // GET: Admin/Admin/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneModels = await _context.Phones.FindAsync(id);
            if (phoneModels == null)
            {
                return NotFound();
            }
            return View(phoneModels);
        }
        [Route("EditPhones")]
        // POST: Admin/Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> EditPhones(int id, [Bind("IdPro,NamePro,Price,Description,CoverImageUrl")] PhoneModels phoneModels)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PhonesNameOld = await _context.Phones.Where(phone => phone.IdPro == phoneModels.IdPro).Select(phone => phone.CoverImageUrl).FirstOrDefaultAsync();
                    if (phoneModels.CoverImageUrl != null)
                    {
                        //Save Image to wwwroot/image
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(phoneModels.ImageFile.FileName);
                        string extension = Path.GetExtension(phoneModels.ImageFile.FileName);
                        phoneModels.CoverImageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await phoneModels.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        phoneModels.CoverImageUrl = PhonesNameOld;
                    }
                    _context.Update(phoneModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!phoneModelsExists(phoneModels.IdPro))
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
            return View(phoneModels); 
        }

        private bool phoneModelsExists(int idPro)
        {
            throw new NotImplementedException();
        }

        // GET: Admin/Admin/Delete/5
        [Route("Delete")]
        

        // POST: Admin/Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneModels = await _context.Phones.FindAsync(id);
            _context.Phones.Remove(phoneModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneModelsExists(int id)
        {
            return _context.Phones.Any(e => e.IdPro == id);
        }
    }
}
