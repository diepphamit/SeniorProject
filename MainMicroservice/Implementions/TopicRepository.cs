using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.Topic;
using MainMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public TopicRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateTopicAsync(TopicForCreate topicForCreate)
        {
            try
            {
                var topic = _mapper.Map<Topic>(topicForCreate);
                _context.Topics.Add(topic);

                await _context.SaveChangesAsync();

                return true;
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

            var s = _context.Tests.AsEnumerable().Count();
            return _context.Topics.AsEnumerable();
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
