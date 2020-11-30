using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.Topic;
using MainMicroservice.Helpers;
using MainMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Implementions
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public TopicRepository(DataContext context, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _context = context;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
               _cloudinaryConfig.Value.CloudName,
               _cloudinaryConfig.Value.ApiKey,
               _cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }
        public async Task<bool> CreateTopicAsync(TopicForCreate topicForCreate)
        {
            try
            {
                var image = topicForCreate.File;

                var uploadResult = new ImageUploadResult();
                if (image.Length > 0)
                {
                    using (var stream = image.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(image.Name, stream),
                            Transformation = new Transformation()
                                .Width(500).Height(500).Crop("fill").Gravity("face")
                        };

                        uploadResult = _cloudinary.Upload(uploadParams);
                    }

                    var topic = _mapper.Map<Topic>(topicForCreate);
                    topic.ImageUrl = uploadResult.Url.ToString();

                    _context.Topics.Add(topic);

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTopicAsync(int id)
        {
            var topic = await GetTopicByIdAsync(id);
            if (topic == null)
                return false;

            try
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<Topic> GetAllTopics(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var topics = _context.Topics
                     .Where(x =>
                         x.Name.ToLower().Contains(keyword.ToLower()))
                     .AsEnumerable();
                return topics;
            }

            return _context.Topics.AsEnumerable();
        }

        public IEnumerable<Topic> GetPopularTopics()
        {
            Random rnd = new Random();
            return _context.Topics.Where(x => x.Id != 1).ToList().OrderBy(x => rnd.Next()).Take(6).AsEnumerable();
        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            return await _context.Topics.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateTopicAsync(int id, TopicForUpdate topicForUpdate)
        {
            var topic = await GetTopicByIdAsync(id);
            if (topic == null)
                return false;

            try
            {
                topic.Name = topicForUpdate.Name;
                topic.Content = topicForUpdate.Content;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
