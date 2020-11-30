using DataAccess.Entities;
using MainMicroservice.Dtos.UserFlashcards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface IUserFlashcardRepository
    {
        IEnumerable<UserFlashcard> GetAllUserFlashcards(string keyword);
        IEnumerable<Flashcard> GetFlashcardByUserId(int userId);
        Task<UserFlashcard> GetUserFlashcardByIdAsync(int id);
        bool GetUserFlashcardByUserIdAndFlashcardIdAsync(int userId, int flashcardId);
        Task<bool> CreateUserFlashcardAsync(UserFlashcardForCreate userFlashcardForCreate);

        Task<bool> UpdateUserFlashcardAsync(int id, UserFlashcardForUpdate userFlashcardForUpdate);
        Task<bool> DeleteUserFlashcardAsync(int id);
    }
}
