﻿using DataAccess.Entities;
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
        Task<Flashcard> GetFlashcardByIdAsync(int id);
        Task<bool> CreateFlashcardAsync(FlashcardForCreate flashcardForCreate);

        Task<bool> UpdateFlashcardAsync(int id, FlashcardForUpdate flashcardForUpdate);
        Task<bool> DeleteFlashcardAsync(int id);
    }
}
