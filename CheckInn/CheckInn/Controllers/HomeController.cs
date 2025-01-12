using CheckInn.Models;
using CheckInn.Models.ViewModels;
using CheckInn.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CheckInn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;

        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> HotelsAPI(ApiDataViewModel apiDataViewModel)
        {
            var response = await _apiService.GetHotelsByLocation("https://booking-com.p.rapidapi.com/v1/hotels/locations", "https://booking-com.p.rapidapi.com/v1/hotels/search", apiDataViewModel);

            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}