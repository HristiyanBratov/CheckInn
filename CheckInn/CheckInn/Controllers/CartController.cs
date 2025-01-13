using CheckInn.DataAccess.Data;
using CheckInn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CheckInn.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult AddToCart(string hotelName, string hotelImg, string hotelPrice, string checkInDate, string checkOutDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User user = _db.Users.FirstOrDefault(x => x.Id == userId);

            Hotel hotel = new Hotel { HotelName = hotelName, ImageUrl = hotelImg };

            _db.Hotels.Add(hotel);
            _db.SaveChanges();

            Reservation reservation = new Reservation
            {
                CheckInDate = Convert.ToDateTime(checkInDate),
                CheckOutDate = Convert.ToDateTime(checkOutDate),
                PricePerNight = Convert.ToDouble(hotelPrice),
                Hotel = hotel,
                User = user
            };
            
            _db.Reservations.Add(reservation);
            _db.SaveChanges();

            return RedirectToAction("HotelsAPI", "Home");
        }

        public IActionResult RemoveReservation(int reservationId)
        {
            _db.Reservations.Remove(_db.Reservations.First(x => x.Id == reservationId));
            _db.SaveChanges();

            return RedirectToAction("BookedHotels", "UserBookedHotels");
        }

    }
}