using CheckInn.DataAccess.Data;
using CheckInn.Models;
using CheckInn.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInn.Services.HotelReservations
{
    public class HotelReservationsService : IHotelReservationsService
    {
        private readonly ApplicationDbContext _db;

        public HotelReservationsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Reservation> GetHotelReservations(string userId)
        {
            return _db.Users.Where(x => x.Id == userId).SelectMany(x => x.Reservations).ToList();
        } 

        public List<Hotel> GetHotels(List<Reservation> reservations)
        {
            return _db.Hotels.Where(x => reservations.Select(s => s.Id).Contains(x.Id)).ToList();
        }
    }
}
