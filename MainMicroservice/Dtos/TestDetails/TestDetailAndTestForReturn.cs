using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.TestDetails
{
    public class TestDetailAndTestForReturn
    {
        public int Id { get; set; }
        public int FlashcardId { get; set; }
        public int MyAnswer { get; set; }
        public int Answer1 { get; set; }
        public int Answer2 { get; set; }
        public int Answer3 { get; set; }
        public int Answer4 { get; set; }
    }
}
