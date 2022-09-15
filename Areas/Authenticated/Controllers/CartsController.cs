using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bookselling.Data;
using bookselling.Models;
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
        private readonly ISendMailService _emailSender;

        public CartsController(ApplicationDbContext db, ISendMailService emailSender)
        {
            _db = db;
            _emailSender = emailSender;
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
        [HttpGet]
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVm = new ShoppingCartVM()
            {
                OrderHeader = new Models.OrderHeader(),
                ListCarts = _db.Carts.Where(u => u.CustomerId == claim.Value).Include(c => c.Book)
            };

            ShoppingCartVm.OrderHeader.ApplicationUser = _db.ApplicationUsers
                .FirstOrDefault(u => u.Id == claim.Value);

            foreach (var list in ShoppingCartVm.ListCarts)
            {
                list.Price = list.Book.Price;
                ShoppingCartVm.OrderHeader.Total += (list.Price + list.Count);
            }

            ShoppingCartVm.OrderHeader.Address = ShoppingCartVm.OrderHeader.ApplicationUser.Address;
            ShoppingCartVm.OrderHeader.OrderDate = DateTime.Now;

            return View(ShoppingCartVm);
        }
        
        // summary post
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            // lay id cua user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // lay thong tin user dang mua
            // lay toan bo list cart
            ShoppingCartVm.OrderHeader.ApplicationUser = _db.ApplicationUsers
                .FirstOrDefault(c => c.Id == claim.Value);
            ShoppingCartVm.ListCarts = _db.Carts.Where(c => c.CustomerId == claim.Value)
                .Include(c => c.Book);

            
            // assign value for each field in order header
            ShoppingCartVm.OrderHeader.CustomerId = claim.Value;
            ShoppingCartVm.OrderHeader.Address = ShoppingCartVm.OrderHeader.ApplicationUser.Address;
            ShoppingCartVm.OrderHeader.OrderDate = DateTime.Now;

            // add to order header and save changes to get order header id
            _db.OrderHeaders.Add(ShoppingCartVm.OrderHeader);
            _db.SaveChanges();

            // moi san pham add vao order detail
            foreach (var item in ShoppingCartVm.ListCarts)
            {
                item.Price = item.Book.Price;
                OrderDetail orderDetail = new OrderDetail()
                {
                    BookId = item.BookId,
                    OrderHeaderId = ShoppingCartVm.OrderHeader.Id,
                    Price = item.Price,
                    Quantity = item.Count
                };

                // calculate total for order header and add to order detail
                ShoppingCartVm.OrderHeader.Total += orderDetail.Quantity + orderDetail.Price;
                _db.OrderDetails.Add(orderDetail);
            }
            
            // remove that item from cart
            _db.Carts.RemoveRange(ShoppingCartVm.ListCarts);
            _db.SaveChanges();
            HttpContext.Session.SetInt32(SD.ssShoppingCart, 0);

            return RedirectToAction("OrderConfirmation", "Carts", 
                new { id = ShoppingCartVm.OrderHeader.Id });
        }
        
        // order confirm 
        public IActionResult OrderConfirmation(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var emailDb = _db.ApplicationUsers.Find(claim.Value).Email;

            MailContent content = new MailContent()
            {
                To = emailDb,
                Subject = "Thanks for order",
                Body = "<p>Order successfully!</p>"
            };

            _emailSender.SendMail(content);
            return View(id);
        }
    }
}