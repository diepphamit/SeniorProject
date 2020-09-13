using AuthMicroservice.Dtos.Auth;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthMicroservice.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> CheckLoginAdmin(UserForLoginDto userForLoginDto);
        Task<bool> CheckLogin(UserForLoginDto userForLoginDto);
        Task<bool> Register(UserForRegisterDto userForRegisterDto);
        Task<User> GetUserByUserName(string username);

        Task<string> GenerateJwtToken(User user);
    }
}
