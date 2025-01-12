using CheckInn.Models;
using CheckInn.Models.ViewModels;
using CheckInn.Services.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CheckInn.Services.ApiModule
{
    public class ApiService : IApiService
    {
        public async Task<List<HotelViewModel>?> GetHotelsByLocation(string apiUrl, string newApiUrl, ApiDataViewModel model)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "...api-key-here...");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "booking-com.p.rapidapi.com");

            var response = await client.GetAsync(apiUrl + $"?name={model.City}&locale=en-us");

            response.EnsureSuccessStatusCode();

            var responseJSON = await response.Content.ReadAsStringAsync();
            var hotels = JsonConvert.DeserializeObject<List<LocationId>>(responseJSON);

            List<string> destinationIds = new List<string>();

            foreach(var obj in hotels)
            {
                string destId = obj.Dest_id;
                destinationIds.Add(destId);
            }

            string formattedCheckInDate = model.CheckInDate.ToString("yyyy-MM-dd");
            string formattedCheckOutDate = model.CheckOutDate.ToString("yyyy-MM-dd");

            List<HotelViewModel> newHotels = new List<HotelViewModel>();

            foreach(var id in destinationIds)
            {
                var newResponse = await client.GetAsync(newApiUrl + $"?order_by=price&adults_number=2&checkin_date={formattedCheckInDate}&filter_by_currency=USD&dest_id={id}&locale=en-us&checkout_date={formattedCheckOutDate}&units=metric&room_number=1&dest_type=city");

                response.EnsureSuccessStatusCode();

                var newResponseJson = await newResponse.Content.ReadAsStringAsync();


                JToken responseToken = JToken.Parse(newResponseJson);

                var hotel = responseToken.SelectToken("result");

                foreach(var ht in hotel)
                {
                    var hotelName = ht.Value<string>("hotel_name");
                    var hotelPhotoMainUrl = ht.Value<string>("main_photo_url");
                    var hotelPrice = ht.Value<double>("min_total_price");
                    var reviewScoreWord = ht.Value<string>("review_score_word");
                    var reviewScore = ht.Value<double?>("review_score");

                    if (hotelName != null && hotelPhotoMainUrl != null)
                    {
                        var newHotel = new HotelViewModel
                        {
                            Name = hotelName,
                            PhotoMainUrl = hotelPhotoMainUrl,
                            ReviewScore = reviewScore,
                            ReviewScoreWord = reviewScoreWord,
                            Price = hotelPrice,
                            StartAt = formattedCheckInDate,
                            EndAt = formattedCheckOutDate
                        };

                        newHotels.Add(newHotel);
                    }         

                }
            }

            return newHotels;
        }
    }
}