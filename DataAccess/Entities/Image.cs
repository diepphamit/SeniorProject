using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int FlashcardId { get; set; }
        [ForeignKey("FlashcardId")]
        public virtual Flashcard Flashcard { get; set; }
    }
}
