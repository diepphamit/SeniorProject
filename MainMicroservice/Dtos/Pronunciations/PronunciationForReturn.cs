using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Pronunciations
{
    public class PronunciationForReturn
    {
        public int Id { get; set; }
        public int FlashcardId { get; set; }

        public string FlashcardName { get; set; }

        public string Phonetic { get; set; }
        public string Link { get; set; }
    }
}
