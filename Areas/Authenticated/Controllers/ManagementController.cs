using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using bookselling.Data;
using bookselling.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookselling.Controllers
{
    [Area(SD.AuthenticatedArea)]
    [Authorize(Roles = SD.StoreOwnerRole)]
    public class ManagementController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ManagementController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            List<int> listId = new List<int>();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var orderHeaderList = _db.OrderHeaders
                .Include(o => o.ApplicationUser)
                .ToList();
            return View(orderHeaderList);
        }
        
        // detail of management
        [HttpGet]
        public IActionResult Detail(int managementId)
        {
            var managementDetail = _db.OrderDetails
                .Where(d => d.OrderHeaderId == managementId)
                .Include(d => d.Book)
                .ToList();


            return View(managementDetail);
        }
    }
}