using HotelManagementSystem.Entities;
using HotelManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Areas.DashBoard.Controllers
{
    //[Authorize(Roles ="Administrator")]
    public class DashboardController : Controller
    {
        // GET: DashBoard/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();
            var dashBoardService = new DashboardService();

            var picLists = new List<Picture>();

            var files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i];  // selecting file
                var pathToImagesFolder = Server.MapPath("/images/site/");
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var filePath = pathToImagesFolder + fileName;

                picture.SaveAs(filePath);

                var dbPicture = new Picture();
                dbPicture.URL = fileName;

                if (dashBoardService.SavePicture(dbPicture))
                {
                    picLists.Add(dbPicture);
                }
            }
            result.Data=picLists;
            return result;
        }
    }
}