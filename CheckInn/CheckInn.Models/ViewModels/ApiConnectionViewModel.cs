﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInn.Models.ViewModels
{
    public class ApiConnectionViewModel
    {
        public string DomainName { get; set; }
        
        public string ApiHost { get; set; }

        public string ApiKey { get; set; }
    }
}