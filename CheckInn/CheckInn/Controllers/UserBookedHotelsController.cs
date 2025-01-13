using CheckInn.DataAccess.Data;
using CheckInn.Models;
using CheckInn.Models.ViewModels;
using CheckInn.Services.HotelReservations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CheckInn.Controllers
{
    public class UserBookedHotelsController : Controller
    {
        private readonly HotelReservationsService _hotelReservationSer;

        public UserBookedHotelsController(HotelReservationsService hotelReservationSer)
        {
            _hotelReservationSer = hotelReservationSer;
        }

        public IActionResult BookedHotels()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            List<Reservation> bookings = _hotelReservationSer.GetHotelReservations(userId.ToString());
                
            List<Hotel> hotels = _hotelReservationSer.GetHotels(bookings);

            UserBookedHotelsViewModel userReservations = new UserBookedHotelsViewModel
            {
                Hotels = hotels,
                Bookings = bookings
            };

            return View(userReservations);
        }
    }
}