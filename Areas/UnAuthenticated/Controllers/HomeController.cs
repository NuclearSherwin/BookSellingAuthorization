using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using bookselling.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bookselling.Models;
using bookselling.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace bookselling.Controllers
{
    [Area(SD.UnAuthenticatedArea)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly int _recordsPerPage = 40;

        public HomeController(ILogger<HomeController> logger, 
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(int id, string searchString = "")
        {
            var products = _dbContext.Books
                .Where(b => b.Name.Contains(searchString) || b.Author.Contains(searchString))
                .Include(p => p.Category)
                .ToList();

            int numberOfRecords = products.Count();
            int numberOfPages = (int)Math.Ceiling((double)numberOfRecords / _recordsPerPage);

            ViewBag.numberOfPages = numberOfPages;
            ViewBag.currentPage = id;
            ViewData["Current Filter"] = searchString;

            var productList = products.Skip(id * numberOfPages).Take(_recordsPerPage).ToList();

            ViewData["Message"] = "Welcome!";    
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        // =================== Detail ==========================
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var productSelected = _dbContext.Books
                .Include(c => c.Category)
                .FirstOrDefault(b => b.Id == id);
            Cart cart = new Cart()
            {
                Book = productSelected,
                BookId = productSelected.Id
            };
            return View(cart);
        }
        
        // post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public IActionResult Detail(Cart cartObject)
        {
            cartObject.Id = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cartObject.CustomerId = claim.Value;
            
            if (cartObject.CustomerId == null || cartObject.BookId == null)
            {
                var productFromDb = _dbContext.Books
                    .Include(a => a.Category)
                    .FirstOrDefault(u => u.Id == cartObject.BookId);
                Cart cart = new Cart()
                {
                    Book = productFromDb,
                    BookId = productFromDb.Id
                };
                return View(cart);
            }
           

            Cart cartFromDb = _dbContext.Carts
                .FirstOrDefault(c => c.CustomerId == cartObject.CustomerId && c.BookId == cartObject.BookId);

            if (cartFromDb == null)
            {
                _dbContext.Add(cartObject);
                ViewData["Message"] = "Order successfully!";
            }
            else
            {
                cartFromDb.Count += cartObject.Count;
                _dbContext.Update(cartFromDb);
                
            }

            
            _dbContext.SaveChanges();

            // count product through session
            var count = _dbContext.Carts
                .Where(c => c.CustomerId == cartObject.CustomerId)
                .ToList().Count();
            
            HttpContext.Session.SetInt32(SD.ssShoppingCart, count);

            return RedirectToAction(nameof(Index));
        }
    }
}