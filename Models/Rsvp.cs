using System.ComponentModel.DataAnnotations;

namespace NerdDinner.Models
{
    public class Rsvp
    {
        public int RsvpID { get; set; }
        public int DinnerID { get; set; }
        [Required]
        public string AttendeeName { get; set; }
    }
}