using CheckInn.Models.ViewModels;
using CheckInn.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInn.Services.ApiModule
{
    public class ApiConnectionService : IApiConnectionService
    {
        private readonly ApiConnectionViewModel _viewModel;

        public ApiConnectionService(ApiConnectionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public HttpClient ApiConnection { get; private set; }

        public void EstablishConnection()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("x-rapidapi-host", _viewModel.DomainName);
            client.DefaultRequestHeaders.Add("x-rapidapi-key", _viewModel.ApiKey);

            ApiConnection = client;
        }
    }
}