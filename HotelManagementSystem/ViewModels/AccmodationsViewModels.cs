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
        public int selectedAccomdationPackageID { get; set; }
    }

    public class AccmodationPackageDetailsViewModel
    {
        public AccomodationPackage AccomodationPackage { get; set; }
       
    }

    public class CheckAccomodationAvailabilityViewModel
    {
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildrens { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Notes { get; set; }
    }
}