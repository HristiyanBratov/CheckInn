using CheckInn.DataAccess.Data;
using CheckInn.Models;
using CheckInn.Models.ViewModels;
using CheckInn.Services.Contracts;
using CheckInn.Services.HotelReservations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CheckInn.Controllers
{
    public class UserBookedHotelsController : Controller
    {
        private readonly IHotelReservationsService _hotelReservationSer;

        public UserBookedHotelsController(IHotelReservationsService hotelReservationSer)
        {
            _hotelReservationSer = hotelReservationSer;
        }

        public IActionResult BookedHotels()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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