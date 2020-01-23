using HotelManagementSystem.Areas.DashBoard.ViewModel;
using HotelManagementSystem.Entities;
using HotelManagementSystem.Services;
using HotelManagementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static HotelManagementSystem.Areas.DashBoard.ViewModel.AccomodationsViewModel;

namespace HotelManagementSystem.Areas.DashBoard.Controllers
{
    public class UsersController : Controller
    {

        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public HMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public async Task<ActionResult> Index(string searchTerm, string roleID, int? page)
        {
            int recordSize = 3;
            page = page ?? 1;  //checks if page is null then assigns 1

            UsersListingModel model = new UsersListingModel();

            model.SearchTerm = searchTerm;
            model.RoleID = roleID;
            model.Roles = RoleManager.Roles.ToList();

            model.Users = await SearchUsers(searchTerm,roleID,page.Value, recordSize );

            var totalRecords = await SearchUsersCount(searchTerm, roleID);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        public async Task<IEnumerable<HMSUser>> SearchUsers(string searchTerm, string roleID, int page, int recordSize)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
               // users = users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleID)); // takes longer time 

                var role = await RoleManager.FindByIdAsync(roleID);
                var userIDs = role.Users.Select(x => x.UserId).ToList();
                // var userIds = User.Identity.GetUserId();
                users = users.Where(x => userIDs.Contains(x.Id));
            }

            var skip = (page - 1) * recordSize;
            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }
        public async Task<int> SearchUsersCount(string searchTerm, string roleID)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                var role = await RoleManager.FindByIdAsync(roleID);
                var userIDs = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIDs.Contains(x.Id));
            }
            return users.Count();
        }


        [HttpGet]
        public async Task< ActionResult> Action(string ID)
        {

           UsersActionModel model = new UsersActionModel();
            if (!string.IsNullOrEmpty(ID))  //we are trying to edit a record
            {
                var user = await UserManager.FindByIdAsync(ID);
                model.ID = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.Username = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;

            }
          //  model.Roles = accomodationPackagesService.GetAllAccomodationPackages().ToList();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(UsersActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))  //we are trying to edit a record
            {
                var user = await UserManager.FindByIdAsync(model.ID);
                 
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;


               result = await UserManager.UpdateAsync(user);
               
            }
            else      //we are trying to create a record
            {
                var user = new HMSUser();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;


                result = await UserManager.CreateAsync(user);
            }
            json.Data = new { Success = result.Succeeded, Message = string.Join(" ,", result.Errors) };

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            UsersActionModel model = new UsersActionModel();

            var user = await UserManager.FindByIdAsync(ID);

              model.ID = user.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UsersActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))  //we are trying to edit a record
            {
                var user = await UserManager.FindByIdAsync(model.ID);
                result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(" ,", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User" };
            }
         
            return json;
        }

        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {

            UserRolesModel model = new UserRolesModel();
   
            model.UserID = ID;
            var user = await UserManager.FindByIdAsync(ID);
            var userRoleIDs = user.Roles.Select(x => x.RoleId).ToList();

            model.UserRoles = RoleManager.Roles.Where(x => userRoleIDs.Contains(x.Id)).ToList();

            model.Roles = RoleManager.Roles.Where(x=> !userRoleIDs.Contains(x.Id)).ToList();

            return PartialView("_UserRoles", model);
        }
        //[HttpPost]
        //public async Task<JsonResult> UserRoles(UsersActionModel model)
        //{
        //    JsonResult json = new JsonResult();
        //    IdentityResult result = null;
        //    if (!string.IsNullOrEmpty(model.ID))  //we are trying to edit a record
        //    {
        //        var user = await UserManager.FindByIdAsync(model.ID);

        //        user.FullName = model.FullName;
        //        user.Email = model.Email;
        //        user.UserName = model.Username;
        //        user.Country = model.Country;
        //        user.City = model.City;
        //        user.Address = model.Address;


        //        result = await UserManager.UpdateAsync(user);

        //    }
        //    else      //we are trying to create a record
        //    {
        //        var user = new HMSUser();

        //        user.FullName = model.FullName;
        //        user.Email = model.Email;
        //        user.UserName = model.Username;
        //        user.Country = model.Country;
        //        user.City = model.City;
        //        user.Address = model.Address;


        //        result = await UserManager.CreateAsync(user);
        //    }
        //    json.Data = new { Success = result.Succeeded, Message = string.Join(" ,", result.Errors) };

        //    return json;
        //}

        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string userID, string roleID,bool isDelete=false)
        {
            JsonResult json = new JsonResult();
            var user = await UserManager.FindByIdAsync(userID);

            var role = await RoleManager.FindByIdAsync(roleID);

            if(user != null && role != null)
            {
                IdentityResult result = null;
                if (!isDelete)
                {
                     result = await UserManager.AddToRoleAsync(userID, role.Name);
                }
                else 
                {
                     result = await UserManager.RemoveFromRoleAsync(userID, role.Name);
                }    
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid operation." };
            }
           
            return json;
        }
    }
}