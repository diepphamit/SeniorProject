﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.Flashcards;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashcardController : ControllerBase
    {
        private readonly IFlashcardRepository _flashcardRepository;
        private readonly IUserFlashcardRepository _userFlashcardRepository;
        private readonly IMapper _mapper;

        public FlashcardController(
            IFlashcardRepository flashcardRepository,
            IMapper mapper,
            IUserFlashcardRepository userFlashcardRepository
        )
        {
            _flashcardRepository = flashcardRepository;
            _mapper = mapper;
            _userFlashcardRepository = userFlashcardRepository;
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

        [HttpGet("GetFlashcardsByTopicId")]
        public IActionResult GetFlashcardsByTopicId(int topicId,int userId, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentFlashcard = HttpContext.Flashcard.Identity.Name;
                //var list = _flashcardRepository.GetListFlashcardsRole(keyword);
                //list = list.Where(x => x.FlashcardName != currentFlashcard);
                var flashcards = _flashcardRepository.GetAllFlashcardsByTopicId(topicId);

                int totalCount = flashcards.Count();

                var query = flashcards.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Flashcard>, IEnumerable<FlashcardForReturn>>(query);

                foreach (var item in response)
                {
                    if (_userFlashcardRepository.GetUserFlashcardByUserIdAndFlashcardIdAsync(userId, item.Id))
                    {
                        item.hasUser = true;
                    }
                    else item.hasUser = false;
                }

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

        [HttpGet("GetFlashcardsByUserId")]
        public IActionResult GetFlashcardsByUserId(int userId, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentFlashcard = HttpContext.Flashcard.Identity.Name;
                //var list = _flashcardRepository.GetListFlashcardsRole(keyword);
                //list = list.Where(x => x.FlashcardName != currentFlashcard);
                var flashcards = _flashcardRepository.GetAllFlashcardsByUserId(userId);

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

        [HttpGet("GetFlashcardHome")]
        public async Task<IActionResult> GetFlashcardHome()
        {
            var flashcard = await _flashcardRepository.GetFlashcardHomeAsync();
            if (flashcard == null)
                return NotFound();

            return Ok(flashcard);
        }

        [HttpGet("GetPopularFlashcards")]
        public IActionResult GetPopularFlashcards()
        {
            var flashcards =  _flashcardRepository.GetPopularFlashcards();
            if (flashcards == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<Flashcard>, IEnumerable<FlashcardForReturn>>(flashcards));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlashcardById(int id)
        {
            var flashcard = await _flashcardRepository.GetFlashcardByIdAsync(id);
            if (flashcard == null)
                return NotFound();

            return Ok(flashcard);
        }

        [HttpPost("CreateFlashcard")]
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

        [HttpPost("CreateFlashcardAI")]
        public async Task<IActionResult> CreateFlashcardAI([FromForm] ImageFlashcardForCreate file) 
        {
            if (ModelState.IsValid)
            {

                var content = new MultipartFormDataContent();
                
                content.Add(new StreamContent(HttpContext.Request.Form.Files[0].OpenReadStream())
                {
                    Headers =
                {
                    ContentLength = HttpContext.Request.Form.Files[0].Length
                }
                }, "File", "FILES");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://ai-service/image", content);
                //HttpResponseMessage response = await client.PostAsync("http://localhost:8000/image", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var dataReturn = JObject.Parse(responseBody);
                string z = dataReturn["data"][0].ToString();


                FlashcardForCreateAI flashcard = new FlashcardForCreateAI
                {
                    Word = dataReturn["data"][0].ToString(),
                    Meaning = dataReturn["data"][1].ToString(),
                    Type = dataReturn["data"][2].ToString(),
                    Example = dataReturn["data"][3].ToString(),
                    Phonetic = dataReturn["data"][4].ToString(),
                    PronunciationLink = dataReturn["data"][5].ToString(),
                    File = file.Image
                };

                var result = await _flashcardRepository.CreateFlashcardAIAsync(flashcard, file.UserId);
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

        [HttpPost("CreateFlashcardByChatBot")]
        public async Task<IActionResult> CreateFlashcardByChatBot(FlashcardForCreateByChatbot flashcardForCreate)
        {
            if (ModelState.IsValid)
            {

                var json = JsonConvert.SerializeObject(flashcardForCreate);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://ai-service/getflashcard", data);
                //HttpResponseMessage response = await client.PostAsync("http://localhost:8000/image", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var dataReturn = JObject.Parse(responseBody);
                //var z = dataReturn.First.First[1];


                FlashcardCreateByChatbotDB flashcard = new FlashcardCreateByChatbotDB
                {
                    Word = dataReturn.First.First[0].ToString(),
                    Meaning = dataReturn.First.First[1].ToString(),
                    Type = dataReturn.First.First[2].ToString(),
                    Example = dataReturn.First.First[3].ToString(),
                    Phonetic = dataReturn.First.First[4].ToString(),
                    PronunciationLink = dataReturn.First.First[5].ToString(),
                    ImageUrl = dataReturn.First.First[6].ToString()
                };

                var result = await _flashcardRepository.CreateFlashcardByChatbotAsync(flashcard, flashcardForCreate.UserId);
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

        [HttpPost("CreateFlashcardByUserId")]
        public async Task<IActionResult> CreateFlashcardByUserId([FromForm] FlashcardForCreateByUserId flashcardForCreate)
        {
            var result = await _flashcardRepository.CreateFlashcardByUserIdAsync(flashcardForCreate, flashcardForCreate.UserId);

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