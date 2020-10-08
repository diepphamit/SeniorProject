using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.TestDetails;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDetailController : ControllerBase
    {
        private readonly ITestDetailRepository _testDetailRepository;
        private readonly IMapper _mapper;

        public TestDetailController(
            ITestDetailRepository testDetailRepository,
            IMapper mapper
        )
        {
            _testDetailRepository = testDetailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllTestDetails(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentTestDetail = HttpContext.TestDetail.Identity.Name;
                //var list = _testDetailRepository.GetListTestDetailsRole(keyword);
                //list = list.Where(x => x.TestDetailName != currentTestDetail);
                var testDetails = _testDetailRepository.GetAllTestDetails(keyword);

                int totalCount = testDetails.Count();

                var query = testDetails.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<TestDetail>, IEnumerable<TestDetailForReturn>>(query);

                var paginationSet = new PaginationSet<TestDetailForReturn>()
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
        public async Task<IActionResult> GetTestDetailById(int id)
        {
            var testDetail = await _testDetailRepository.GetTestDetailByIdAsync(id);
            if (testDetail == null)
                return NotFound();

            return Ok(testDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestDetail([FromBody] TestDetailForCreate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _testDetailRepository.CreateTestDetailAsync(input);
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
        public async Task<IActionResult> UpdateTestDetail(int id, [FromBody] TestDetailForUpdate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _testDetailRepository.UpdateTestDetailAsync(id, input);
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
        public async Task<IActionResult> DeleteTestDetail(int id)
        {
            var result = await _testDetailRepository.DeleteTestDetailAsync(id);
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