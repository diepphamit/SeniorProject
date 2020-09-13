using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string Type { get; set; }
        public string Example { get; set; }
        public bool IsSystem { get; set; }
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Pronunciation> Pronunciations { get; set; }
        public virtual ICollection<UserFlashcard> UserFlashcards { get; set; }
    }
}
