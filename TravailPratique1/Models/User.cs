using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TravailPratique1.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int userId { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
