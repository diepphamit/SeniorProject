using DataAccess.Entities;
using MainMicroservice.Dtos.Flashcards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface IFlashcardRepository
    {
        IEnumerable<Flashcard> GetAllFlashcards(string keyword);
        IEnumerable<Flashcard> GetAllFlashcardsByTopicId(int topicId);
        IEnumerable<Flashcard> GetAllFlashcardsByUserId(int userId);
        Task<FlashcardHomeForReturn> GetFlashcardHomeAsync();
        Task<Flashcard> GetFlashcardByIdAsync(int id);
        Task<bool> CreateFlashcardAsync(FlashcardForCreate flashcardForCreate);
        Task<bool> CreateFlashcardAIAsync(FlashcardForCreateAI flashcardForCreate, int userId);
        Task<bool> CreateFlashcardByUserIdAsync(FlashcardForCreateByUserId flashcardForCreate, int userId);

        Task<bool> UpdateFlashcardAsync(int id, FlashcardForUpdate flashcardForUpdate);
        Task<bool> DeleteFlashcardAsync(int id);
    }
}
