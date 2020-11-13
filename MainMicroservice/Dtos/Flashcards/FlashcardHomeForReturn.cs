using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Flashcards
{
    public class FlashcardHomeForReturn
    {
        public int TotalUser { get; set; }
        public int TotalTopic { get; set; }
        public int TotalFlashcard { get; set; }
    }
}
