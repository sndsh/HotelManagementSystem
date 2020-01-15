using HotelManagementSystem.Areas.DashBoard.ViewModel;
using HotelManagementSystem.Entities;
using HotelManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Areas.DashBoard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypesListingModel model = new AccomodationTypesListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationTypes = accomodationTypesService.SearchAccomodationTypes(searchTerm);
            return View(model);
        }

      

        [HttpGet]
        public ActionResult Action(int? ID)
        {
           
            AccomodationTypesActionModel model = new AccomodationTypesActionModel();
            if (ID.HasValue)  //we are trying to edit a record
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypeByID(ID.Value);
                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }
            return PartialView("_Action",model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationTypesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)  //we are trying to edit a record
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypeByID(model.ID);
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypesService.UpdateAccomodationType(accomodationType);
            }
            else      //we are trying to create a record
            {
                AccomodationType accomodationType = new AccomodationType();

                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                 result = accomodationTypesService.SaveAccomodationType(accomodationType);
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

            AccomodationTypesActionModel model = new AccomodationTypesActionModel();
           
            var accomodationType = accomodationTypesService.GetAccomodationTypeByID(ID);

            model.ID = accomodationType.ID;
         
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationTypesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodationType = accomodationTypesService.GetAccomodationTypeByID(model.ID);
           

            result = accomodationTypesService.DeleteAccomodationType(accomodationType);


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
    }
}