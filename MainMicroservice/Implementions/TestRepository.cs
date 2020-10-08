using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.Dtos.TestDetails;
using MainMicroservice.Dtos.Tests;
using MainMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Implementions
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TestRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Test> CreateTestAsync(TestAndTestDetailForCreate testForCreate)
        {
            var totalCorrect = this.GetTotalCorret(testForCreate.TestDetails.ToList());

            Test test = new Test
            {
                CreateAt = DateTime.Now,
                Score = Convert.ToInt32(totalCorrect * 10 / testForCreate.TestDetails.ToList().Count()),
                TotalCorrect = totalCorrect,
                TotalSentences = testForCreate.TestDetails.ToList().Count(),
                UserId = testForCreate.UserId
            };

            _context.Tests.Add(test);

            foreach(var item in testForCreate.TestDetails)
            {
                TestDetail testDetail = new TestDetail
                {
                    Answer1 = item.Answer1,
                    Answer2 = item.Answer2,
                    Answer3 = item.Answer3,
                    Answer4 = item.Answer4,
                    FlashcardId = item.FlashcardId,
                    MyAnswer = item.MyAnswer,
                    Status = item.FlashcardId == item.MyAnswer,
                    Test = test
                };

                _context.TestDetails.Add(testDetail);
            }

            try
            {
                await _context.SaveChangesAsync();

                return test;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> DeleteTestAsync(int id)
        {
            var test = await GetTestByIdAsync(id);
            if (test == null)
                return false;

            try
            {
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<Test> GetAllTests(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var tests = _context.Tests.Include(x => x.User).AsEnumerable();
                return tests;
            }

            return _context.Tests.Include(x => x.User).AsEnumerable();
        }

        public int GetTotalCorret(List<TestDetailAndTestForCreate> input)
        {

            var count = 0;
            foreach(var item in input)
            {
                if (item.FlashcardId == item.MyAnswer) count++;
            }

            return count;
        }

        public async Task<Test> GetTestByIdAsync(int id)
        {
            return await _context.Tests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateTestAsync(int id, TestForUpdate testForUpdate)
        {
            var test = await GetTestByIdAsync(id);
            if (test == null)
                return false;

            try
            {
                test.UserId = testForUpdate.UserId;
                //test.Name = testForUpdate.Name;
                //test.Name = testForUpdate.Name;
                //test.Name = testForUpdate.Name;

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
