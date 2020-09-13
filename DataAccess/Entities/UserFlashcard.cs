using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class UserFlashcard
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int FlashcardId { get; set; }
        [ForeignKey("FlashcardId")]
        public virtual Flashcard Flashcard { get; set; }
    }
}
