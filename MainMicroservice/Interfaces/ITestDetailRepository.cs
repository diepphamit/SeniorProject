using DataAccess.Entities;
using MainMicroservice.Dtos.TestDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Interfaces
{
    public interface ITestDetailRepository
    {
        IEnumerable<TestDetail> GetAllTestDetails(string keyword);
        IEnumerable<TestDetailAndTestForCreate> GetTestsByUserId(int userId);
        Task<TestDetail> GetTestDetailByIdAsync(int id);
        Task<bool> CreateTestDetailAsync(TestDetailForCreate testDetailForCreate);

        Task<bool> UpdateTestDetailAsync(int id, TestDetailForUpdate testDetailForUpdate);
        Task<bool> DeleteTestDetailAsync(int id);

    }
}
