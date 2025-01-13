using System.ComponentModel.DataAnnotations;

namespace CheckInn.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string HotelName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Reservation> reservations { get; set; } = new List<Reservation>();
    }
}