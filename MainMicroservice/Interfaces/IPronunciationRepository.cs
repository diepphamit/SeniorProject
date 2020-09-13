using DataAccess.Entities;
using MainMicroservice.Dtos.Pronunciations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface IPronunciationRepository
    {
        IEnumerable<Pronunciation> GetAllPronunciations(string keyword);
        Task<Pronunciation> GetPronunciationByIdAsync(int id);
        Task<bool> CreatePronunciationAsync(PronunciationForCreate pronunciationForCreate);

        Task<bool> UpdatePronunciationAsync(int id, PronunciationForUpdate pronunciationForUpdate);
        Task<bool> DeletePronunciationAsync(int id);
    }
}
