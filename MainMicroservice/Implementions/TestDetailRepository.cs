using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.TestDetails;
using MainMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Implementions
{
    public class TestDetailRepository : ITestDetailRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TestDetailRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateTestDetailAsync(TestDetailForCreate testDetailForCreate)
        {
            try
            {
                var testDetail = _mapper.Map<TestDetail>(testDetailForCreate);
                _context.TestDetails.Add(testDetail);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTestDetailAsync(int id)
        {
            var testDetail = await GetTestDetailByIdAsync(id);
            if (testDetail == null)
                return false;

            try
            {
                _context.TestDetails.Remove(testDetail);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<TestDetail> GetAllTestDetails(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var testDetails = _context.TestDetails.AsEnumerable();
                return testDetails;
            }

            return _context.TestDetails.AsEnumerable();
        }

        public async Task<TestDetail> GetTestDetailByIdAsync(int id)
        {
            return await _context.TestDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<TestDetailAndTestForCreate> GetTestsByUserId(int userId)
        {
            var userFlashcards = _context.UserFlashcards.ToList();

            var listReturn = new List<TestDetailAndTestForCreate>();

            Random rnd = new Random();
            //var flashcards = _context.Flashcards.OrderBy(x => rnd.Next()).ToList();
            var flashcards = _context.Flashcards.ToList();
            foreach (var item in userFlashcards)
            {
                var listSuff = flashcards.OrderBy(x => rnd.Next()).ToList();
                listSuff = listSuff.Where(x => x.Id != item.FlashcardId).Take(3).ToList();
                //var s = _context.Flashcards.FirstOrDefault(x => x.Id == item.FlashcardId);
                listSuff.Add(_context.Flashcards.FirstOrDefault(x => x.Id == item.FlashcardId));
                listSuff = listSuff.OrderBy(x => rnd.Next()).ToList();

                TestDetailAndTestForCreate td = new TestDetailAndTestForCreate
                {
                    Answer1 = listSuff[0].Id,
                    Answer1Meaning = listSuff[0].Meaning,
                    Answer2 = listSuff[1].Id,
                    Answer2Meaning = listSuff[1].Meaning,
                    Answer3 = listSuff[2].Id,
                    Answer3Meaning = listSuff[2].Meaning,
                    Answer4 = listSuff[3].Id,
                    Answer4Meaning = listSuff[3].Meaning,
                    FlashcardId = item.FlashcardId,
                    MyAnswer = 0
                };

                td.Word = item.Flashcard.Word;

                listReturn.Add(td);

                if (listReturn.Count == 5) break;
            }

            return listReturn.AsEnumerable();
        }

        public async Task<bool> UpdateTestDetailAsync(int id, TestDetailForUpdate testDetailForUpdate)
        {
            var testDetail = await GetTestDetailByIdAsync(id);
            if (testDetail == null)
                return false;

            try
            {
                testDetail.TestId = testDetailForUpdate.TestId;

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
