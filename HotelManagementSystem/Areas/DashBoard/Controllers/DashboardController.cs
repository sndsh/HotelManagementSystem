using System;
using System.Collections.Generic;
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
    }
}