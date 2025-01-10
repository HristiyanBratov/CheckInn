using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInn.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public ReservationStatus ReservationStatus { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumberOfGuests { get; set; }

        [Required]
        public ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();

        // Add payment, etc. !
    }
}