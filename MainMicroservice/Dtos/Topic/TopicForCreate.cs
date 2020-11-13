using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainMicroservice.Dtos.Topic
{
    public class TopicForCreate
    {
        public string Name { get; set; }
        public string Content { get; set; }

        [Display(Name = "File")]
        public IFormFile File { get; set; }

    }
}
