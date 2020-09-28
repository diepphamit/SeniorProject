using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.UserFlashcards;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFlashcardController : ControllerBase
    {
        private readonly IUserFlashcardRepository _userFlashcardRepository;
        private readonly IMapper _mapper;

        public UserFlashcardController(
            IUserFlashcardRepository userFlashcardRepository,
            IMapper mapper
        )
        {
            _userFlashcardRepository = userFlashcardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUserFlashcards(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentUserFlashcard = HttpContext.UserFlashcard.Identity.Name;
                //var list = _userFlashcardRepository.GetListUserFlashcardsRole(keyword);
                //list = list.Where(x => x.UserFlashcardName != currentUserFlashcard);
                var userFlashcards = _userFlashcardRepository.GetAllUserFlashcards(keyword);

                int totalCount = userFlashcards.Count();

                var query = userFlashcards.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<UserFlashcard>, IEnumerable<UserFlashcardForReturn>>(query);

                var paginationSet = new PaginationSet<UserFlashcardForReturn>()
                {
                    Items = response.ToList(),
                    Total = totalCount,
                };

                return Ok(paginationSet);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFlashcardById(int id)
        {
            var userFlashcard = await _userFlashcardRepository.GetUserFlashcardByIdAsync(id);
            if (userFlashcard == null)
                return NotFound();

            return Ok(userFlashcard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserFlashcard([FromBody] UserFlashcardForCreate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userFlashcardRepository.CreateUserFlashcardAsync(input);
                if (result)
                {
                    return Ok(new
                    {
                        message = "success",
                        StatusCode = 201
                    });
                }

                return BadRequest(new
                {
                    message = "fail"
                });
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserFlashcard(int id, [FromBody] UserFlashcardForUpdate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userFlashcardRepository.UpdateUserFlashcardAsync(id, input);
                if (result)
                {
                    return Ok(new
                    {
                        message = "success",
                        StatusCode = 200
                    });
                }

                return BadRequest(new
                {
                    message = "fail"
                });
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFlashcard(int id)
        {
            var result = await _userFlashcardRepository.DeleteUserFlashcardAsync(id);
            if (result)
            {
                return Ok(new
                {
                    message = "success",
                    StatusCode = 200
                });
            }

            return BadRequest(new
            {
                message = "fail"
            });
        }
    }
}