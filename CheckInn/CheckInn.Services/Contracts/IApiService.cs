using CheckInn.Models;
using CheckInn.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInn.Services.Contracts
{
    public interface IApiService
    {
        public Task<List<HotelViewModel>?> GetHotelsByLocation(string apiUrl, string newApiUrl, ApiDataViewModel model);
    }
}