using System.ComponentModel.DataAnnotations;

namespace NerdDinner.Models
{
    public class Login
    {
        public int LoginID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password {get; set;}
    }
}