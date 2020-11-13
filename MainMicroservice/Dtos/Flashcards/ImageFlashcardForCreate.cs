using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Flashcards
{
    public class ImageFlashcardForCreate
    {
        [Required]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public int UserId { get; set; }
    }
}
