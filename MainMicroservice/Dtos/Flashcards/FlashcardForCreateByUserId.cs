using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Flashcards
{
    public class FlashcardForCreateByUserId
    {
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string Type { get; set; }
        public string Example { get; set; }
        public string Phonetic { get; set; }
        public string PronunciationLink { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public IFormFile File { get; set; }
    }
}
