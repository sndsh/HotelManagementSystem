﻿using HotelManagementSystem.Entities;
using HotelManagementSystem.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Areas.DashBoard.ViewModel
{
    public class UsersListingModel
    { 
        public IEnumerable<HMSUser> Users { get; set; }
        public string SearchTerm { get; set; }
        //public List<IdentityRole> AccomodationPackages { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string RoleID { get; set; }

        public Pager Pager { get; set; }
    }

    public class UsersActionModel
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string ID { get; set; }

        //public string RoleID { get; set; }
        //public IdentityRole Role { get; set; }
        //public IEnumerable<IdentityRole> Roles { get; set; }

    }

    public class UserRolesModel
    {
        public string UserID { get; set; }
        public IEnumerable<IdentityRole> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

    }
}