using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInn.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
    }
}