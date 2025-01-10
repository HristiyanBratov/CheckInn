using System.ComponentModel.DataAnnotations;

namespace CheckInn.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}