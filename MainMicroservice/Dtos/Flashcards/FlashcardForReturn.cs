using DataAccess.Entities;
using MainMicroservice.Dtos.Images;
using MainMicroservice.Dtos.Pronunciations;
using MainMicroservice.Dtos.UserFlashcards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Flashcards
{
    public class FlashcardForReturn
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string Type { get; set; }
        public string Example { get; set; }
        public bool IsSystem { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        //public Topic Topic { get; set; }
        public ICollection<ImageForReturn> Images { get; set; }
        public ICollection<PronunciationForReturn> Pronunciations { get; set; }
        public ICollection<UserFlashcardForReturn> UserFlashcards { get; set; }
    }
}
