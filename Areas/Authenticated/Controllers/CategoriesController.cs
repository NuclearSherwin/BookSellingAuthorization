using bookselling.Data;
using bookselling.Models;
using bookselling.Utils;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace bookselling.Controllers
{
    [Area(SD.AuthenticatedArea)]
    [Authorize(Roles = SD.StoreOwnerRole)]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;
        public CategoriesController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        // -----------------INDEX--------------------
        [Authorize(Roles = SD.StoreOwnerRole)]
        [HttpGet]
        public IActionResult Index()
        {
            var objCategories = _db.Categories.ToList();
            return View(objCategories);
        }

        // -----------------DELETE------------------
        [Authorize(Roles = SD.StoreOwnerRole)]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var objCategory = _db.Categories.Find(id);

            _db.Categories.Remove(objCategory);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        // -----------------UPSERT------------------
        [Authorize(Roles = SD.StoreOwnerRole)]
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null) return View(new Category());

            var category = _db.Categories.Find(id);
            return View(category);
        }


        [Authorize(Roles = SD.StoreOwnerRole)]
        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _db.Categories.Add(category);
                }
                else
                    _db.Categories.Update(category);

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }


        // Upload excel file
        [HttpPost]
        public IActionResult UploadExcel(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction(nameof(Upsert));
            }

            // implementation
            var path = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // storing file
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(file.FileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }


            using var streamFile = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(streamFile);
            while (reader.Read())
            {
                var category = new Category()
                {
                    // StoreId = stored.Id
                    Name = reader.GetValue(0).ToString(),
                    Description = reader.GetValue(1).ToString()
                };

                _db.Categories.Add(category);
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}