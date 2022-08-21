using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using bookselling.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bookselling.Models;
using bookselling.Utils;
using Microsoft.EntityFrameworkCore;

namespace bookselling.Controllers
{
    [Area(SD.UnAuthenticatedArea)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly int _recordsPerpage = 40;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(int id, string searchSting = "")
        {
            var products = _dbContext.Books
                .Where(b => b.Name.Contains(searchSting) || b.Author.Contains(searchSting))
                .Include(p => p.Category)
                .ToList();

            int numberOfRecords = products.Count();
            int numberOfPages = (int)Math.Ceiling((double)numberOfRecords / _recordsPerpage);

            ViewBag.numberOfPages = numberOfPages;
            ViewBag.currentPage = id;
            ViewData["Current Filter"] = searchSting;

            var productList = products.Skip(id * numberOfPages).Take(_recordsPerpage).ToList();

                
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
    }
}