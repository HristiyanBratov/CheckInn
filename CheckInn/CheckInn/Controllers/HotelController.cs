using CheckInn.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace CheckInn.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HotelController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
