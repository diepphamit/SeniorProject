using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.Pronunciations;
using MainMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Implementions
{
    public class PronunciationRepository : IPronunciationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PronunciationRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreatePronunciationAsync(PronunciationForCreate pronunciationForCreate)
        {
            try
            {
                var pronunciation = _mapper.Map<Pronunciation>(pronunciationForCreate);
                _context.Pronunciations.Add(pronunciation);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePronunciationAsync(int id)
        {
            var pronunciation = await GetPronunciationByIdAsync(id);
            if (pronunciation == null)
                return false;

            try
            {
                _context.Pronunciations.Remove(pronunciation);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<Pronunciation> GetAllPronunciations(string keyword)
        {
            return _context.Pronunciations.Include(x => x.Flashcard).AsEnumerable();
        }

        public async Task<Pronunciation> GetPronunciationByIdAsync(int id)
        {
            return await _context.Pronunciations.Include(x => x.Flashcard).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdatePronunciationAsync(int id, PronunciationForUpdate pronunciationForUpdate)
        {
            var pronunciation = await GetPronunciationByIdAsync(id);
            if (pronunciation == null)
                return false;

            try
            {
                pronunciation.FlashcardId = pronunciationForUpdate.FlashcardId;
                pronunciation.Link = pronunciationForUpdate.Link;
                pronunciation.Phonetic = pronunciationForUpdate.Phonetic;

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
