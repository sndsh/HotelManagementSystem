using HotelManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.ViewModels
{
    public class AccmodationsViewModel
    {
        public AccomodationType AccomdationType { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public int selectedAccomdationPackageID { get; internal set; }
    }
}