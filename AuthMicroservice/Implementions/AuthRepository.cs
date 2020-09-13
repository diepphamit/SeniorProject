using AuthMicroservice.Dtos.Auth;
using AuthMicroservice.Interfaces;
using AutoMapper;
using DataAccess.Constants;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthMicroservice.Implementions
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public AuthRepository(
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _userManager.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.NormalizedUserName == username.ToLower());
        }

        public async Task<bool> CheckLoginAdmin(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);

            if (user == null)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count == 1 && roles.Contains(Constants.USER_ROLE))
            {
                return false;
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckLogin(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);

            if (user == null)
            {
                return false;
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Register(UserForRegisterDto userForRegisterDto)
        {
            var userByUsername = await _userManager.FindByNameAsync(userForRegisterDto.Username);

            if (userByUsername != null)
            {
                return false;
            }

            var userByEmail = await _userManager.FindByEmailAsync(userForRegisterDto.Email);

            if (userByEmail != null)
            {
                return false;
            }

            try
            {
                var user = _mapper.Map<User>(userForRegisterDto);
                var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
                if (!result.Succeeded)
                    return false;

                user = await _userManager.FindByNameAsync(user.UserName);

                result = await _userManager.AddToRoleAsync(user, Constants.USER_ROLE);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
