﻿using HotelManagementSystem.Areas.DashBoard.ViewModel;
using HotelManagementSystem.Entities;
using HotelManagementSystem.Services;
using HotelManagementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Areas.DashBoard.Controllers
{
    public class RolesController : Controller
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


        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager,HMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;  //checks if page is null then assigns 1

            RolesListingModel model = new RolesListingModel();

            model.SearchTerm = searchTerm;
           
            //  model.Roles = accomodationsService.SearchAccomodation(searchTerm, accomodationPackageID, page.Value, recordSize);

            model.Roles = SearchRoles(searchTerm, page.Value, recordSize);

            var totalRecords = SearchRolesCount(searchTerm);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int page, int recordSize)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            var skip = (page - 1) * recordSize;
            return  roles.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList();
        }
        public int SearchRolesCount(string searchTerm)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return roles.Count();
        }


        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {

            RolesActionModel model = new RolesActionModel();
            if (!string.IsNullOrEmpty(ID))  //we are trying to edit a record
            {
                var role = await RoleManager.FindByIdAsync(ID);
                model.ID = role.Id;
                model.Name = role.Name;
              

            }
            //  model.Roles = accomodationPackagesService.GetAllAccomodationPackages().ToList();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(RolesActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))  //we are trying to edit a record
            {
                var role = await RoleManager.FindByIdAsync(model.ID);

               role.Id = model.ID;
              role.Name = model.Name;
                
                result = await RoleManager.UpdateAsync(role);

            }
            else      //we are trying to create a record
            {
                var role = new IdentityRole();

                role.Name = model.Name;
              
                result = await RoleManager.CreateAsync(role);
            }
            json.Data = new { Success = result.Succeeded, Message = string.Join(" ,", result.Errors) };

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
           RolesActionModel model = new RolesActionModel();

            var role = await RoleManager.FindByIdAsync(ID);

            model.ID = role.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(RolesActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))  //we are trying to edit a record
            {
                var role = await RoleManager.FindByIdAsync(model.ID);
                result = await RoleManager.DeleteAsync(role);

                json.Data = new { Success = result.Succeeded, Message = string.Join(" ,", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Role." };
            }

            return json;
        }

    }
}