using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DatabaseInitializer
    {
        private readonly DataContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public DatabaseInitializer(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            bool isProcess = false;

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Constants.Constants.ADMIN_ROLE
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Constants.Constants.TEACHER_ROLE
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Constants.Constants.USER_ROLE
                });

                isProcess = true;
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                }, "123456@");

                var user = await _userManager.FindByNameAsync("admin");

                await _userManager.AddToRoleAsync(user, Constants.Constants.ADMIN_ROLE);

                isProcess = true;
            }

            if (isProcess)
                await _context.SaveChangesAsync();
        }
    }
}
