using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Flashcards
{
    public class FlashcardForCreateByChatbot
    {
        public int UserId { get; set; }
        public string data { get; set; }
    }
}
