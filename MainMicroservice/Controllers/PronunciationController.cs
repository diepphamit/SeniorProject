using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.Pronunciations;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PronunciationController : ControllerBase
    {
        private readonly IPronunciationRepository _pronunciationRepository;
        private readonly IMapper _mapper;

        public PronunciationController(
            IPronunciationRepository pronunciationRepository,
            IMapper mapper
        )
        {
            _pronunciationRepository = pronunciationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllPronunciations(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentPronunciation = HttpContext.Pronunciation.Identity.Name;
                //var list = _pronunciationRepository.GetListPronunciationsRole(keyword);
                //list = list.Where(x => x.PronunciationName != currentPronunciation);
                var pronunciations = _pronunciationRepository.GetAllPronunciations(keyword);

                int totalCount = pronunciations.Count();

                var query = pronunciations.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Pronunciation>, IEnumerable<PronunciationForReturn>>(query);

                var paginationSet = new PaginationSet<PronunciationForReturn>()
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
        public async Task<IActionResult> GetPronunciationById(int id)
        {
            var pronunciation = await _pronunciationRepository.GetPronunciationByIdAsync(id);
            if (pronunciation == null)
                return NotFound();

            return Ok(pronunciation);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePronunciation([FromBody] PronunciationForCreate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _pronunciationRepository.CreatePronunciationAsync(input);
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
        public async Task<IActionResult> UpdatePronunciation(int id, [FromBody] PronunciationForUpdate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _pronunciationRepository.UpdatePronunciationAsync(id, input);
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
        public async Task<IActionResult> DeletePronunciation(int id)
        {
            var result = await _pronunciationRepository.DeletePronunciationAsync(id);
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