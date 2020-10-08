using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Tests
{
    public class TestForCreate
    {
        public int UserId { get; set; }
        public int Score { get; set; }
        public int TotalCorrect { get; set; }
        public DateTime CreateAt { get; set; }
        public int TotalSentences { get; set; }
    }
}
