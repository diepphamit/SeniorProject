using DataAccess.Entities;
using MainMicroservice.Dtos.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface ITestRepository
    {
        IEnumerable<Test> GetAllTests(string keyword);
        Task<Test> GetTestByIdAsync(int id);
        Task<Test> CreateTestAsync(TestAndTestDetailForCreate testForCreate);

        Task<bool> UpdateTestAsync(int id, TestForUpdate testForUpdate);
        Task<bool> DeleteTestAsync(int id);
    }
}
