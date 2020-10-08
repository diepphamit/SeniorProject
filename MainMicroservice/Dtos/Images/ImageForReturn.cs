using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Images
{
    public class ImageForReturn
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int FlashcardId { get; set; }
        public string FlashcardName { get; set; }
    }
}
