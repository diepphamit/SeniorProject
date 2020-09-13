using DataAccess.Entities;
using MainMicroservice.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(string keyword);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> CreateUserAsync(UserForCreate userForCreate);

        Task<bool> UpdateUserAsync(int id, UserForUpdate userForUpdate);
        Task<bool> DeleteUserAsync(int id);
    }
}
