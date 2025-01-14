using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInn.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double PricePerNight { get; set; }

        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; } = null!;
    }
}