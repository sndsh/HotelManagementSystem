﻿using HotelManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data
{
    public class HMSDBContext : DbContext
    {
        public HMSDBContext() : base("name=HMSDBContext")
        {
        }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}