using HotelManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.ViewModels
{
    public class HomeViewModel
    {
        public IList<AccomodationType> AccomodationTypes { get; set; }
        public IList<AccomodationPackage> AccomodationPackages { get; set; }
    }
}