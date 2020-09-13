using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Entities;
using MainMicroservice.Dtos.Topic;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicController(
            ITopicRepository topicRepository,
            IMapper mapper
        )
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllTopics(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentTopic = HttpContext.Topic.Identity.Name;
                //var list = _topicRepository.GetListTopicsRole(keyword);
                //list = list.Where(x => x.TopicName != currentTopic);
                var topics = _topicRepository.GetAllTopics(keyword);

                int totalCount = topics.Count();

                var query = topics.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicForReturn>>(query);

                var paginationSet = new PaginationSet<TopicForReturn>()
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
        public async Task<IActionResult> GetTopicById(int id)
        {
            var topic = await _topicRepository.GetTopicByIdAsync(id);
            if (topic == null)
                return NotFound();

            return Ok(topic);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] TopicForCreate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _topicRepository.CreateTopicAsync(input);
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
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicForUpdate input)
        {
            if (ModelState.IsValid)
            {
                var result = await _topicRepository.UpdateTopicAsync(id, input);
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
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var result = await _topicRepository.DeleteTopicAsync(id);
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