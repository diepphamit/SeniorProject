using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.Images;
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
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public ImageRepository(DataContext context, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
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
        public async Task<bool> CreateImageAsync(ImageForCreate imageForCreate)
        {
            var image = imageForCreate.File;

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

                try
                {
                    ImageForCreateDB img = new ImageForCreateDB
                    {
                        ImageUrl = uploadResult.Url.ToString(),
                        FlashcardId = imageForCreate.FlashcardId
                    };

                    var imgCreate = _mapper.Map<Image>(img);
                    _context.Images.Add(imgCreate);

                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = await GetImageByIdAsync(id);
            if (image == null)
                return false;

            try
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Image> GetAllImages(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var images = _context.Images.Include(x => x.Flashcard)
                     .AsEnumerable();
                return images;
            }

            return _context.Images.Include(x => x.Flashcard).AsEnumerable();
        }

        public async Task<Image> GetImageByIdAsync(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
