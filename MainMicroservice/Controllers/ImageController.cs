using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataAccess.Entities;
using MainMicroservice.Dtos.Images;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageController(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllImages(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                //var currentTopic = HttpContext.Topic.Identity.Name;
                //var list = _topicRepository.GetListTopicsRole(keyword);
                //list = list.Where(x => x.TopicName != currentTopic);
                var images = _imageRepository.GetAllImages(keyword);

                int totalCount = images.Count();

                var query = images.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Image>, IEnumerable<ImageForReturn>>(query);

                var paginationSet = new PaginationSet<ImageForReturn>()
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
        public async Task<IActionResult> GetImageById(int id)
        {
            var image = await _imageRepository.GetImageByIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage([FromForm] ImageForCreate imageForCreate)
        {
            if (ModelState.IsValid)
            {
                var result = await _imageRepository.CreateImageAsync(imageForCreate);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var result = await _imageRepository.DeleteImageAsync(id);
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