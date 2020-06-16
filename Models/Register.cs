using System.ComponentModel.DataAnnotations;

namespace NerdDinner.Models
{
    public class Register
    {
        public int RegisterID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password {get; set;}
    }
}
