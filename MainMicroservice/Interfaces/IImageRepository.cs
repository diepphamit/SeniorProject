using DataAccess.Entities;
using MainMicroservice.Dtos.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface IImageRepository
    {
        IEnumerable<Image> GetAllImages(string keyword);
        Task<Image> GetImageByIdAsync(int id);
        Task<bool> CreateImageAsync(ImageForCreate imageForCreate);

        //Task<bool> UpdateImageAsync(int id, ImageForUpdate topicForUpdate);
        Task<bool> DeleteImageAsync(int id);
    }
}
