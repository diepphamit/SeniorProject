using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Pronunciations
{
    public class PronunciationForCreate
    {
        public int FlashcardId { get; set; }
        public string Phonetic { get; set; }
        public string Link { get; set; }
    }
}
