using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInn.Models.ViewModels
{
    public class UserBookedHotelsViewModel
    {
        public List<Hotel> Hotels { get; set; }

        public List<Reservation> Bookings { get; set; }
    }
}