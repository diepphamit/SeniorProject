using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthMicroservice.Dtos.Auth
{
    public class UserForReturnDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

    }
}
