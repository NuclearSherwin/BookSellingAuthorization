using System.ComponentModel;
using System.Linq;
using bookselling.Data;
using bookselling.Models;
using bookselling.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookselling.Controllers
{
    [Area(SD.AuthenticatedArea)]
    [Authorize(Roles = SD.StoreOwnerRole)]
    public class CategoriesController: Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
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

    }
}