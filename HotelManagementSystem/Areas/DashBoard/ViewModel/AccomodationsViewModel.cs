using HotelManagementSystem.Entities;
using HotelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Areas.DashBoard.ViewModel
{
    public class AccomodationsViewModel
    {
        public class AccomodationsListingModel
        {
            public IEnumerable<Accomodation> Accomodations { get; set; }
            public string SearchTerm { get; set; }
            public List<AccomodationPackage> AccomodationPackages { get; set; }
            public int? AccomodationPackageID { get; set; }

            public Pager Pager { get; set; }
        }

        public class AccomodationsActionModel
        {
            public int ID { get; set; }

            public int AccomodationPackageID { get; set; }
            public AccomodationPackage AccomodationPackage { get; set; }

            public string Name { get; set; }
            public string Description { get; set; }
        

            public List<AccomodationPackage> AccomodationPackages { get; set; }

        }
    }
}