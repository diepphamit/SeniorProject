using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.TestDetails;
using MainMicroservice.Dtos.Tests;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _testRepository;
        private readonly ITestDetailRepository _testDetailRepository;
        private readonly IMapper _mapper;

        public TestController(
            ITestRepository testRepository,
            ITestDetailRepository testDetailRepository,
            IMapper mapper
        )
        {
            _testRepository = testRepository;
            _testDetailRepository = testDetailRepository;
            _mapper = mapper;
        }

        [Route("GetAllTests")]
        [HttpGet]
        public IActionResult GetAllTests(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentTest = HttpContext.Test.Identity.Name;
                //var list = _testRepository.GetListTestsRole(keyword);
                //list = list.Where(x => x.TestName != currentTest);
                var tests = _testRepository.GetAllTests(keyword);

                int totalCount = tests.Count();

                var query = tests.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Test>, IEnumerable<TestForReturn>>(query);

                var paginationSet = new PaginationSet<TestForReturn>()
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

        [Route("GetTest")]
        [HttpGet]
        public IActionResult GetTestsByuserId()
        {
            try
            {
                //var currentTest = HttpContext.Test.Identity.Name;
                //var list = _testRepository.GetListTestsRole(keyword);
                //list = list.Where(x => x.TestName != currentTest);
                var tests = _testDetailRepository.GetTestsByUserId(1);

                int totalCount = tests.Count();

                //var query = tests.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                //var response = _mapper.Map<IEnumerable<Test>, IEnumerable<TestForReturn>>(tests);

                var paginationSet = new PaginationSet<TestDetailAndTestForCreate>()
                {
                    Items = tests.ToList(),
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
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _testRepository.GetTestByIdAsync(id);
            if (test == null)
                return NotFound();

            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] TestAndTestDetailForCreate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _testRepository.CreateTestAsync(input);
   
                if (result != null)
                {
                    //TestAndTestDetailForReturn testForReturn = new TestAndTestDetailForReturn
                    //{
                    //    Id = result.Id,
                    //    TotalSentences = result.TotalSentences,
                    //    TotalCorrect = result.TotalCorrect,
                    //    CreateAt = result.CreateAt,
                    //    Score = result.Score,
                    //    UserId = result.UserId,
                    //    TestDetails = _mapper.Map<ICollection<TestDetail>, ICollection<TestDetailAndTestForReturn>>(result.TestDetails)
                    //};

                    return Ok(new {
                        message = "success",
                        statusCode = 201,
                        data = result
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
        public async Task<IActionResult> UpdateTest(int id, [FromBody] TestForUpdate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _testRepository.UpdateTestAsync(id, input);
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
        public async Task<IActionResult> DeleteTest(int id)
        {
            var result = await _testRepository.DeleteTestAsync(id);
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