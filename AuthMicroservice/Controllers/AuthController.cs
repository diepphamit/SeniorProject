using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthMicroservice.Dtos.Auth;
using AuthMicroservice.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost("loginAdmin")]
        public async Task<IActionResult> LoginAdmin(UserForLoginDto userForLoginDto)
        {
            if (await _authRepository.CheckLoginAdmin(userForLoginDto))
            {
                var appUser = await _authRepository.GetUserByUserName(userForLoginDto.Username);
                var userToReturn = _mapper.Map<UserForReturnDto>(appUser);
                var accessToken = await _authRepository.GenerateJwtToken(appUser);

                return Ok(new
                {
                    access_token = accessToken,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (await _authRepository.CheckLogin(userForLoginDto))
            {
                var appUser = await _authRepository.GetUserByUserName(userForLoginDto.Username);
                var userToReturn = _mapper.Map<UserForReturnDto>(appUser);
                var accessToken = await _authRepository.GenerateJwtToken(appUser);

                return Ok(new
                {
                    access_token = accessToken,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (await _authRepository.Register(userForRegisterDto))
            {
                var appUser = await _authRepository.GetUserByUserName(userForRegisterDto.Username);
                var userToReturn = _mapper.Map<UserForReturnDto>(appUser);
                var accessToken = await _authRepository.GenerateJwtToken(appUser);

                return Ok(new
                {
                    access_token = accessToken,
                    user = userToReturn
                });
            }

            return Ok(new { message = "error"});
        }
    }
}