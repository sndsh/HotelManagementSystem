using HotelManagementSystem.Entities;
using HotelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Areas.DashBoard.ViewModel
{
    public class AccomodationPackagesListingModel
    {
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }
        public List<AccomodationType> AccomodationTypes { get; set; }
        public int? AccomodationTypeID { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccomodationPackagesActionModel
    {
        public int ID { get; set; }

        public int AccomodationTypeID { get; set; }
        public AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerNight { get; set; }

        public List<AccomodationType> AccomodationTypes { get; set; }

    }
}