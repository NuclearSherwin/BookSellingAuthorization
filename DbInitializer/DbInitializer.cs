using System;
using System.Linq;
using bookselling.Data;
using bookselling.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bookselling.DbInitializer
{
    public class DbInitializer: IDbInitializer
    {
        private readonly RoleManager<IdentityRole> _roleMananger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleMananger = roleManager;
        }
        
        
        public void Initializer()
        {
            // checking database, if not migration then migrate
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
            // checking in table Role, if yes then return, if not deploy the codes after these conditions
            if (_db.Roles.Any(r => r.Name == "StoreOwner")) return;
            if (_db.Roles.Any(r => r.Name == "Customer")) return;
            if (_db.Roles.Any(r => r.Name == "Admin")) return;
            
            // this will deploy if there no have any role yet
            _roleMananger.CreateAsync(new IdentityRole("StoreOwner")).GetAwaiter().GetResult();
            _roleMananger.CreateAsync(new IdentityRole("Customer")).GetAwaiter().GetResult();
            _roleMananger.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            
            // create user admin
            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FullName = "Admin",
                Address = "Kepler452B"
            }, password:"Admin123@").GetAwaiter().GetResult();
            
            
            // finding the user which is just have created
            ApplicationUser admin = _db.ApplicationUsers.Where(a => a.Email == "admin@gmail.com").FirstOrDefault();
            
            // add that user (admin) to admin role
            _userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
        }
    }
}