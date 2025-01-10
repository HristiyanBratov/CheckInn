using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckInn.Models
{
    public class HotelImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
    }
}