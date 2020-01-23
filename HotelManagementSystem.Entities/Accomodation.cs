using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Entities
{
    [Table("tblAccomodation")]
    public class Accomodation
    {
        [Key]
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public virtual AccomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<AccomodationPicture> AccomodationPictures { get; set; }
    }
}
