using HotelManagementSystem.Data;
using HotelManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
  public class AccomodationsService
    {
        public IEnumerable<Accomodation> GetAllAccomodation()
        {
            var context = new HMSDBContext();

            return context.Accomodations.AsEnumerable();
        }

        public IEnumerable<Accomodation> GetAllAccomodationsByAccomodationPackage(int accomodationPackageID)
        {
            var context = new HMSDBContext();

            return context.Accomodations.Where(x=>x.AccomodationPackageID== accomodationPackageID).ToList();
        }

        public IEnumerable<Accomodation> SearchAccomodation(string searchTerm, int? accomodationPackageID, int page, int recordSize)
        {
            var context = new HMSDBContext();

            var accomodation = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodation = accomodation.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodation = accomodation.Where(a => a.AccomodationPackageID == accomodationPackageID.Value);
            }

            // skip =(1-1)*3=0
            // skip =(2-1)*3=3
            var skip = (page - 1) * recordSize;


            return accomodation.OrderBy(x => x.AccomodationPackageID).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchAccomodationCount(string searchTerm, int? accomodationPackageID)
        {
            var context = new HMSDBContext();

            var accomodation = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodation = accomodation.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodation = accomodation.Where(a => a.AccomodationPackageID == accomodationPackageID.Value);
            }
            return accomodation.Count();
        }

        public Accomodation GetAccomodationByID(int ID)
        {
            using (var context = new HMSDBContext())
            {
                return context.Accomodations.Find(ID);
            }
        }

        public bool SaveAccomodation(Accomodation accomodation)
        {
            var context = new HMSDBContext();

            context.Accomodations.Add(accomodation);

            return context.SaveChanges() > 0;
        }
        public bool UpdateAccomodation(Accomodation accomodation)
        {
            var context = new HMSDBContext();

            context.Entry(accomodation).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccomodation(Accomodation accomodation)
        {
            var context = new HMSDBContext();

            context.Entry(accomodation).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
