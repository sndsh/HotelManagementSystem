using System.Web.Mvc;

namespace HotelManagementSystem.Areas.DashBoard
{
    public class DashBoardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DashBoard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DashBoard_default",
                "DashBoard/{controller}/{action}/{id}",
                new {controller="Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}