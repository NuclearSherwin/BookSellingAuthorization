using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppdevPhong.ViewModels;
using bookselling.Data;
using bookselling.Models;
using bookselling.Utils;
using bookselling.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookselling.Controllers
{
    [Area(SD.AuthenticatedArea)]
    [Authorize(Roles = SD.AdminRole)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;

        // GET
        public UsersController(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManger = roleManager;
        }
        
        public async Task<IActionResult> Index()
        {
            // taking current login user id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // exception itself admin
            var userList = _db.ApplicationUsers.Where(u => u.Id != claims.Value);

            foreach (var user in userList)
            {
                var userTemp = await _userManager.FindByIdAsync(user.Id);
                var roleTemp = await _userManager.GetRolesAsync(userTemp);
                user.Role = roleTemp.FirstOrDefault();
            }


            return View(userList.ToList());
        }

        
        // lock and unlock
        
        [HttpGet]
        public async Task<IActionResult> LockUnlock(string id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userNeedToLock = _db.ApplicationUsers.Where(u => u.Id == id).First();

            if (userNeedToLock.Id == claims.Value)
            {
                // hien ra loi ban dang khoa tai khoan cua chinh minh
            }

            if (userNeedToLock.LockoutEnd != null && userNeedToLock.LockoutEnd > DateTime.Now)
                userNeedToLock.LockoutEnd = DateTime.Now;
            else
                userNeedToLock.LockoutEnd = DateTime.Now.AddYears(1000);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string id)
        {
            var user = _db.ApplicationUsers.Find(id);

            if (user == null) return View();

            var confirmEmailVm = new ConfirmEmailVm()
            {
                Email = user.Email
            };

            return View(confirmEmailVm);
        }
        
        
        [HttpGet]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (token == null || email == null) ModelState.AddModelError("", "Invalid password reset token");

            var resetPasswordViewModel = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(resetPasswordViewModel);
        }

        
        [HttpPost]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailVm confirmEmailVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(confirmEmailVm.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    return RedirectToAction("ResetPassword", "Users"
                        , new { token, email = user.Email });
                }
            }

            return View(confirmEmailVm);
        }
        
        [HttpPost]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token,
                        resetPasswordViewModel.Password);
                    if (result.Succeeded) return RedirectToAction(nameof(Index));
                }
            }

            return View(resetPasswordViewModel);
        }
        
        
        [HttpGet]
        public IActionResult EditAdminStoreOwnerCustomer(string id)
        {
            var adminStoreOwnerCustomer = _db.ApplicationUsers.Find(id);
            return View(adminStoreOwnerCustomer);
        }

        [HttpPost]
        public IActionResult EditAdminStoreOwnerCustomer(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var adminStoreOwnerCustomer = _db.ApplicationUsers.Find(applicationUser.Id);
                adminStoreOwnerCustomer.FullName = applicationUser.FullName;
                _db.ApplicationUsers.Update(adminStoreOwnerCustomer);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }
        
        public async Task<IActionResult> Edit(string id)
        {
            var user = _db.ApplicationUsers.Find(id);
            var roletemp = await _userManager.GetRolesAsync(user);
            var role = roletemp.First();

            return RedirectToAction("EditAdminStoreOwnerCustomer", new { id });
        }
        
    }
}