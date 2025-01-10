using System.ComponentModel.DataAnnotations;

namespace CheckInn.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int RoomCapacity { get; set; }

        [Required]
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}