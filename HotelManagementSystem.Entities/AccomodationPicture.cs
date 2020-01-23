using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Entities
{
   public class AccomodationPicture
    {
        public int ID { get; set; }

        public int AccomodationID { get; set; }
        public int PictureID { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
