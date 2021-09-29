using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegApp.Models
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsDelete { get; set; }

        public string Address { get; set; }
    }
}
