using HotelManagementSystem.Entities;
using HotelManagementSystem.Services;
using HotelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HotelManagementSystem.Areas.DashBoard.ViewModel.AccomodationsViewModel;

namespace HotelManagementSystem.Areas.DashBoard.Controllers
{
    public class AccomodationsController : Controller
    {
        // GET: DashBoard/Accomodations
        AccomodationsService accomodationsService = new AccomodationsService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        public ActionResult Index(string searchTerm, int? roleID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;  //checks if page is null then assigns 1
            AccomodationsListingModel model = new AccomodationsListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationPackageID = roleID;
            model.Accomodations = accomodationsService.SearchAccomodation(searchTerm, roleID, page.Value, recordSize);

            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages().ToList();

            var totalRecords = accomodationsService.SearchAccomodationCount(searchTerm, roleID);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }



        [HttpGet]
        public ActionResult Action(int? ID)
        {

            AccomodationsActionModel model = new AccomodationsActionModel();
            if (ID.HasValue)  //we are trying to edit a record
            {
                var accomodation = accomodationsService.GetAccomodationByID(ID.Value);
                model.ID = accomodation.ID;
                model.AccomodationPackageID = accomodation.AccomodationPackageID;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
               
            }
            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages().ToList();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationsActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)  //we are trying to edit a record
            {
                var accomodation = accomodationsService.GetAccomodationByID(model.ID);

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;
              
                result = accomodationsService.UpdateAccomodation(accomodation);
            }
            else      //we are trying to create a record
            {
                Accomodation accomodation = new Accomodation();

                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;
               

                result = accomodationsService.SaveAccomodation(accomodation);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Type." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {

            AccomodationsActionModel model = new AccomodationsActionModel();

            var accomodation = accomodationsService.GetAccomodationByID(ID);

            model.ID = accomodation.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationsActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodation = accomodationsService.GetAccomodationByID(model.ID);


            result = accomodationsService.DeleteAccomodation(accomodation);


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation." };
            }

            return json;
        }
    }
}