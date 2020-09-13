using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.Flashcards;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashcardController : ControllerBase
    {
        private readonly IFlashcardRepository _flashcardRepository;
        private readonly IMapper _mapper;

        public FlashcardController(
            IFlashcardRepository flashcardRepository,
            IMapper mapper
        )
        {
            _flashcardRepository = flashcardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllFlashcards(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentFlashcard = HttpContext.Flashcard.Identity.Name;
                //var list = _flashcardRepository.GetListFlashcardsRole(keyword);
                //list = list.Where(x => x.FlashcardName != currentFlashcard);
                var flashcards = _flashcardRepository.GetAllFlashcards(keyword);

                int totalCount = flashcards.Count();

                var query = flashcards.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Flashcard>, IEnumerable<FlashcardForReturn>>(query);

                var paginationSet = new PaginationSet<FlashcardForReturn>()
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
        public async Task<IActionResult> GetFlashcardById(int id)
        {
            var flashcard = await _flashcardRepository.GetFlashcardByIdAsync(id);
            if (flashcard == null)
                return NotFound();

            return Ok(flashcard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashcard([FromBody] FlashcardForCreate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _flashcardRepository.CreateFlashcardAsync(input);
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
        public async Task<IActionResult> UpdateFlashcard(int id, [FromBody] FlashcardForUpdate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _flashcardRepository.UpdateFlashcardAsync(id, input);
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
        public async Task<IActionResult> DeleteFlashcard(int id)
        {
            var result = await _flashcardRepository.DeleteFlashcardAsync(id);
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