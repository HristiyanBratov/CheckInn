using System.ComponentModel.DataAnnotations;

namespace CheckInn.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string HotelName { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        [Range(1, 5)]
        public int StarRating { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public ICollection<HotelImage> Images { get; set; } = new List<HotelImage>();
    }
}