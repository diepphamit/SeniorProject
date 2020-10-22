using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.Flashcards;
using MainMicroservice.Dtos.Pronunciations;
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
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public FlashcardRepository(DataContext context, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
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

        public async Task<bool> CreateFlashcardAIAsync(FlashcardForCreateAI flashcardForCreate)
        {
            try
            {
                var flashcard = _mapper.Map<Flashcard>(flashcardForCreate);
                flashcard.TopicId = 1;
                flashcard.IsSystem = false;

                _context.Flashcards.Add(flashcard);

                Pronunciation pronunciation = new Pronunciation
                {
                    Link = flashcardForCreate.PronunciationLink,
                    Phonetic = flashcardForCreate.Phonetic,
                    Flashcard = flashcard

                };

                _context.Pronunciations.Add(pronunciation);

                var image = flashcardForCreate.File;

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

                    
                    Image img = new Image
                    {
                        ImageUrl = uploadResult.Url.ToString(),
                        Flashcard = flashcard
                    };

                    _context.Images.Add(img);
                   
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateFlashcardAsync(FlashcardForCreate flashcardForCreate)
        {
            try
            {
                var flashcard = _mapper.Map<Flashcard>(flashcardForCreate);
                _context.Flashcards.Add(flashcard);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFlashcardAsync(int id)
        {
            var flashcard = await GetFlashcardByIdAsync(id);
            if (flashcard == null)
                return false;

            try
            {
                _context.Flashcards.Remove(flashcard);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<Flashcard> GetAllFlashcards(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var flashcards = _context.Flashcards
                    .Include(x => x.Pronunciations)
                    .Include(x => x.Images)
                    .Include(x => x.UserFlashcards)
                     .Where(x =>
                         x.Word.ToLower().Contains(keyword.ToLower()))
                     .AsEnumerable();
                return flashcards;
            }

            return _context.Flashcards
                    .Include(x => x.Pronunciations)
                    .Include(x => x.Images)
                    .Include(x => x.UserFlashcards)
                    .Include(x => x.Topic)
                    .AsEnumerable();
        }

        public async Task<Flashcard> GetFlashcardByIdAsync(int id)
        {
            return await _context.Flashcards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateFlashcardAsync(int id, FlashcardForUpdate flashcardForUpdate)
        {
            var flashcard = await GetFlashcardByIdAsync(id);
            if (flashcard == null)
                return false;

            try
            {
                flashcard.Meaning = flashcardForUpdate.Meaning;
                flashcard.TopicId = flashcardForUpdate.TopicId;
                flashcard.Word = flashcardForUpdate.Word;
                flashcard.IsSystem = flashcardForUpdate.IsSystem;
                flashcard.Type = flashcardForUpdate.Type;

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
