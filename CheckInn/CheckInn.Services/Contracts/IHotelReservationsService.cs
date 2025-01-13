using CheckInn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInn.Services.Contracts
{
    public interface IHotelReservationsService
    {
        public List<Reservation> GetHotelReservations(string userId);
        public List<Hotel> GetHotels(List<Reservation> reservations);
    }
}