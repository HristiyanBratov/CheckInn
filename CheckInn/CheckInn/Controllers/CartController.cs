using CheckInn.DataAccess.Data;
using CheckInn.Models;
using CheckInn.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveReservation(int reservationId)
        {
            _db.Reservations.Remove(_db.Reservations.First(x => x.Id == reservationId));
            _db.SaveChanges();

            return RedirectToAction("BookedHotels", "UserBookedHotels");
        }

        [HttpPost]
        public IActionResult ProcessPayment(int reservationId)
        {
            var reservation = _db.Reservations
                .Include(r => r.Hotel)
                .FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }

            var domain = "https://localhost:7102/";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain + $"Home/Index",
                CancelUrl = domain + "Home/Index",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };

            var sessionLineItem = new Stripe.Checkout.SessionLineItemOptions
            {
                PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(reservation.PricePerNight * 100 * (reservation.CheckInDate - reservation.CheckOutDate).TotalDays),
                    Currency = "usd",
                    ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"Reservation at {reservation.Hotel.HotelName}"
                    }
                },
                Quantity = 1
            };

            options.LineItems.Add(sessionLineItem);

            var service = new Stripe.Checkout.SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

    }
}