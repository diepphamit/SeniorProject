using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string FullName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserFlashcard> UserFlashcards { get; set; }

    }
}
