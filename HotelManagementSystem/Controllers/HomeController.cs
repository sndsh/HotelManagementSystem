using HotelManagementSystem.Services;
using HotelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            AccomodationTypesService service = new AccomodationTypesService();
            AccomodationPackagesService accomodationPackageservice = new AccomodationPackagesService();


            model.AccomodationTypes = service.GetAllAccomodationTypes().ToList();
            model.AccomodationPackages = accomodationPackageservice.GetAllAccomodationPackages().ToList();

            return View(model);
        }

      
    }
}