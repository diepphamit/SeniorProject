using AutoMapper;
using DataAccess.Constants;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.Users;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Implementions
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserRepository(DataContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> CreateUserAsync(UserForCreate userForCreate)
        {
            try
            {
                var user = _mapper.Map<User>(userForCreate);
                var result = await _userManager.CreateAsync(user, userForCreate.Password);
                if (!result.Succeeded)
                    return false;

                user = await _userManager.FindByNameAsync(user.UserName);

                result = await _userManager.AddToRoleAsync(user, Constants.USER_ROLE);

                if (!result.Succeeded)
                {
                    await DeleteUserAsync(user.Id);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
                return false;

            try
            {
                var result = await _userManager.DeleteAsync(user);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public IEnumerable<User> GetAllUsers(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var users = _context.Users
                     .Include(x=>x.UserRoles)
                     .Where(x =>
                         x.Email.ToLower().Contains(keyword.ToLower()) ||
                         x.UserName.ToLower().Contains(keyword.ToLower()) ||
                         x.FullName.ToLower().Contains(keyword.ToLower()) ||
                         x.PhoneNumber.ToLower().Contains(keyword.ToLower()))
                     .AsEnumerable();
                return users;
            }

            return _context.Users.Include(x => x.UserRoles).AsEnumerable();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<bool> UpdateUserAsync(int id, UserForUpdate userForUpdate)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
                return false;

            try
            {
                user.FullName = userForUpdate.FullName;
                user.PhoneNumber = userForUpdate.PhoneNumber;

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
