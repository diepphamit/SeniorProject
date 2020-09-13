using DataAccess.Entities;
using MainMicroservice.Dtos.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> GetAllTopics(string keyword);
        Task<Topic> GetTopicByIdAsync(int id);
        Task<bool> CreateTopicAsync(TopicForCreate topicForCreate);

        Task<bool> UpdateTopicAsync(int id, TopicForUpdate topicForUpdate);
        Task<bool> DeleteTopicAsync(int id);
    }
}
