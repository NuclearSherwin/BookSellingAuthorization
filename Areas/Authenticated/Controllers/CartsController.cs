using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bookselling.Data;
using bookselling.Utils;
using bookselling.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookselling.Controllers
{
    [Area(SD.AuthenticatedArea)]
    [Authorize(Roles = SD.CustomerRole)]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartsController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [BindProperty] public ShoppingCartVM ShoppingCartVm { get; set; }

        // GET
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVm = new ShoppingCartVM()
            {
                OrderHeader = new Models.OrderHeader(),
                ListCarts = _db.Carts.Where(u => u.CustomerId == claim.Value)
                    .Include(p => p.Book.Category)
            };

            ShoppingCartVm.OrderHeader.Total = 0;
            ShoppingCartVm.OrderHeader.ApplicationUser = _db.ApplicationUsers
                .FirstOrDefault(u => u.Id == claim.Value);


            foreach (var list in ShoppingCartVm.ListCarts)
            {
                list.Price = list.Book.Price;
                ShoppingCartVm.OrderHeader.Total += (list.Price * list.Count);
                if (list.Book.Description.Length > 100)
                {
                    list.Book.Description = list.Book.Description.Substring(0, 99) + "...";
                }
            }
            
            return View(ShoppingCartVm);
        }
        
        // Plus
        public IActionResult Plus(int cartId)
        {
            var cart = _db.Carts.Include(p => p.Book).FirstOrDefault(c => c.Id == cartId);

            cart.Count += 1;
            cart.Price = cart.Book.Price;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _db.Carts.Include(p => p.Book).FirstOrDefault(c => c.Id == cartId);
            if (cart.Count == 1)
            {
                var cnt = _db.Carts.Where(u => u.CustomerId == cart.CustomerId).ToList().Count;
                _db.Carts.Remove(cart);
                _db.SaveChanges();
            }
            else
            {
                cart.Count -= 1;
                cart.Price = cart.Book.Price;
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        
        // delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cart = _db.Carts.Include(p => p.Book)
                    .FirstOrDefault(c => c.Id == id);

                // get all ids
                var cnt = _db.Carts.Where(u => u.CustomerId == cart.CustomerId).ToList().Count;
                if (cart == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                _db.Carts.Remove(cart);
                await _db.SaveChangesAsync();
                HttpContext.Session.SetInt32(SD.ssShoppingCart, cnt - 1);
                return Json(new { success = true, message = "Delete successfully!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        // summary
        // [HttpGet]
        // public IActionResult Summary()
        // {
        //     
        // }
    }
}