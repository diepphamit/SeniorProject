using MainMicroservice.Dtos.TestDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Tests
{
    public class TestAndTestDetailForCreate
    {
        public int UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public ICollection<TestDetailAndTestForCreate> TestDetails { get; set; }
    }
}
