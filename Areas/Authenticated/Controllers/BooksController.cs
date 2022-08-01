using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bookselling.Data;
using bookselling.Models;
using bookselling.Utils;
using bookselling.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _environment;


        // IWebHostEnvironment will help you take the image path
        public BooksController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
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
            if (!ModelState.IsValid)
            {
                // if (bookVm.Book.Id == 0)
                //     _db.Books.Add(bookVm.Book);
                // else
                //     _db.Books.Update(bookVm.Book);
                //
                // _db.SaveChanges();
                //
                // return RedirectToAction(nameof(Index));

                bookVm.Categories = CategorySelectListItems();
                return View(bookVm);
            }

            var webRootPath = _environment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var fileName = Guid.NewGuid();
                var uploads = Path.Combine(webRootPath, @"images/products");
                var extension = Path.GetExtension(files[0].FileName);
                if (bookVm.Book.Id != 0)
                {
                    var productDb = _db.Books.AsNoTracking().Where(b => b.Id == bookVm.Book.Id).First();
                    if (productDb.ImgPath != null && bookVm.Book.Id != 0)
                    {
                        var imagePath = Path.Combine(webRootPath, productDb.ImgPath.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);
                    }
                }

                using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }

                bookVm.Book.ImgPath = @"/images/products/" + fileName + extension;
            }

            else
            {
                bookVm.Categories = CategorySelectListItems();
                return View(bookVm);
            }


            if (bookVm.Book.Id == 0 || bookVm.Book.Id == null)
                _db.Books.Add(bookVm.Book);
            else
                _db.Books.Update(bookVm.Book);

            _db.SaveChanges();

            // provide data for the categories list
            // bookVm.Categories = CategorySelectListItems();

            return RedirectToAction(nameof(Index));
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