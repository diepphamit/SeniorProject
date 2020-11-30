using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.UserFlashcards;
using MainMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Implementions
{
    public class UserFlashcardRepository : IUserFlashcardRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserFlashcardRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateUserFlashcardAsync(UserFlashcardForCreate userFlashcardForCreate)
        {
            try
            {
                var userFlashcard = _mapper.Map<UserFlashcard>(userFlashcardForCreate);
                _context.UserFlashcards.Add(userFlashcard);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserFlashcardAsync(int id)
        {
            var userFlashcard = await GetUserFlashcardByIdAsync(id);
            if (userFlashcard == null)
                return false;

            try
            {
                _context.UserFlashcards.Remove(userFlashcard);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<UserFlashcard> GetAllUserFlashcards(string keyword)
        {
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    var userFlashcards = _context.UserFlashcards
            //        .Include(x => x.Pronunciations)
            //        .Include(x => x.Images)
            //        .Include(x => x.UserUserFlashcards)
            //         .Where(x =>
            //             x.Word.ToLower().Contains(keyword.ToLower()))
            //         .AsEnumerable();
            //    return userFlashcards;
            //}

            return _context.UserFlashcards.AsEnumerable();
        }

        public IEnumerable<Flashcard> GetFlashcardByUserId(int userId)
        {
            var flashcardForReturn = new List<Flashcard>();
            var flashcards = _context.UserFlashcards.Where(x => x.UserId == userId).Include(x => x.Flashcard).Select(x => x.Flashcard);

            foreach(var flashcard in flashcards)
            {
                flashcardForReturn.Add(_context.Flashcards
                   .Include(x => x.Pronunciations)
                   .Include(x => x.Images)
                   .Include(x => x.UserFlashcards)
                   .Include(x => x.Topic).FirstOrDefault(x => x.Id == flashcard.Id));
            }

            return flashcardForReturn;
        }

        public async Task<UserFlashcard> GetUserFlashcardByIdAsync(int id)
        {
            return await _context.UserFlashcards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool GetUserFlashcardByUserIdAndFlashcardIdAsync(int userId, int flashcardId)
        {
            var result = _context.UserFlashcards.FirstOrDefault(x => x.FlashcardId == flashcardId && x.UserId == userId);

            return result != null;
        }

        public async Task<bool> UpdateUserFlashcardAsync(int id, UserFlashcardForUpdate userFlashcardForUpdate)
        {
            var userFlashcard = await GetUserFlashcardByIdAsync(id);
            if (userFlashcard == null)
                return false;

            try
            {
                userFlashcard.UserId = userFlashcardForUpdate.UserId;
                userFlashcard.FlashcardId = userFlashcardForUpdate.FlashcardId;

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
