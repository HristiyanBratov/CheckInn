using System.ComponentModel.DataAnnotations;

namespace CheckInn.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string HotelName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Range(1, 5)]
        public int StarRating { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public ICollection<HotelImage> Images { get; set; } = new List<HotelImage>();
    }
}
