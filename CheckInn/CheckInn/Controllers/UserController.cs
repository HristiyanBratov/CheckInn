using CheckInn.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace CheckInn.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
