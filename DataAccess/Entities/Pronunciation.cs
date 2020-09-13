using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class Pronunciation
    {
        public int Id { get; set; }
        public int FlashcardId { get; set; }
        [ForeignKey("FlashcardId")]
        public virtual Flashcard Flashcard { get; set; }
        public string Phonetic { get; set; }
        public string Link { get; set; }
    }
}
