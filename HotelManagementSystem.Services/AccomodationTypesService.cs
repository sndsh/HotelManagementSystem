using HotelManagementSystem.Data;
using HotelManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class AccomodationTypesService
    {
       
        public IEnumerable<AccomodationType> GetAllAccomodationTypes()
        {
            var context = new HMSDBContext();

            return context.AccomodationTypes.AsEnumerable();
        }

        public IEnumerable<AccomodationType> SearchAccomodationTypes(string searchTerm)
        {
            var context = new HMSDBContext();

            var accomodationTypes = context.AccomodationTypes.AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                accomodationTypes = accomodationTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return accomodationTypes.AsEnumerable();
        }

        public AccomodationType GetAccomodationTypeByID(int ID)
        {
            using (var context = new HMSDBContext())
            {
                return context.AccomodationTypes.Find(ID);
            }
        }

        public bool SaveAccomodationType(AccomodationType accomodationType)
        {
            var context = new HMSDBContext();

           context.AccomodationTypes.Add(accomodationType);

            return context.SaveChanges()>0;
        }
        public bool UpdateAccomodationType(AccomodationType accomodationType)
        {
            var context = new HMSDBContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccomodationType(AccomodationType accomodationType)
        {
            var context = new HMSDBContext();

            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
