using System.ComponentModel.DataAnnotations;

namespace NerdDinner.Models
{
    public class Dinner
    {
        public int DinnerID { get; set;}

        public int LoginID { get; set; }

        [Required]
        public string Title { get; set; }
        public string EventDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string HostedBy { get; set; }
        [Required]
        public string ContactPhone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}