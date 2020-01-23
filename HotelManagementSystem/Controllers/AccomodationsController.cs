using HotelManagementSystem.Services;
using HotelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationsService accomodationsService = new AccomodationsService();
        
        public ActionResult Index(int accomodationTypeID,int? accomodationPackageID)
        {
            AccmodationsViewModel model = new AccmodationsViewModel();

            model.AccomdationType = accomodationTypesService.GetAccomodationTypeByID(accomodationTypeID);
            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackagesByAccomodationType(accomodationTypeID);

            model.selectedAccomdationPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : model.AccomodationPackages.First().ID;

            model.Accomodations = accomodationsService.GetAllAccomodationsByAccomodationPackage(model.selectedAccomdationPackageID);


            return View(model);
        }
    }
}