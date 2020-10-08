using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Images
{
    public class ImageForCreate
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile File { get; set; }
        public int FlashcardId { get; set; }
    }
}
