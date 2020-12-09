using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Flashcards
{
    public class FlashcardCreateByChatbotDB
    {
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string Type { get; set; }
        public string Example { get; set; }
        public string Phonetic { get; set; }
        public string PronunciationLink { get; set; }
        public string ImageUrl { get; set; }
    }
}
