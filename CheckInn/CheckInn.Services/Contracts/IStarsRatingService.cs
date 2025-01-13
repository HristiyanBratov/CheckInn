using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInn.Services.Contracts
{
    public interface IStarsRatingService
    {
        public string StarRatingService(double? reviewScore);
    }
}