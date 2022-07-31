using System.Collections.Generic;
using System.Linq;
using bookselling.Data;
using bookselling.Models;
using bookselling.Utils;
using bookselling.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bookselling.Controllers
{
    [Area(SD.AuthenticatedArea)]
    [Authorize(Roles = SD.StoreOwnerRole)]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET
        // --------------------INDEX-------------------
        [HttpGet]
        public IActionResult Index()
        {
            var books = _db.Books.Include(x => x.Category).ToList();
            return View(books);
        }


        // -------------------DELETE--------------------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var objBook = _db.Books.Find(id);
            _db.Books.Remove(objBook);
            _db.SaveChanges();


            return RedirectToAction(nameof(Index));
        }


        // -------------------UPSERT----------------------
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var bookVm = new BookVm();

            bookVm.Categories = CategorySelectListItems();


            if (id == null)
            {
                bookVm.Book = new Book();
                return View(bookVm);
            }

            var book = _db.Books.Find(id);
            bookVm.Book = book;

            return View(bookVm);
        }


        [HttpPost]
        public IActionResult Upsert(BookVm bookVm)
        {
            if (ModelState.IsValid)
            {
                if (bookVm.Book.Id == 0)
                {
                    _db.Books.Add(bookVm.Book);
                }
                else
                    _db.Books.Update(bookVm.Book);

                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // provide data for the categories list
            bookVm.Categories = CategorySelectListItems();

            return View(bookVm);
        }

        // method for category select list VM
        private IEnumerable<SelectListItem> CategorySelectListItems()
        {
            var categories = _db.Categories.ToList();

            // for each book
            var result = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return result;
        }
    }
}