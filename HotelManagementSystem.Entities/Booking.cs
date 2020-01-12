using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Entities
{
    [Table("tblBooking")]
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        public int AccomodationID { get; set; }

        public DateTime FromDate { get; set; }
        /// <summary>
        /// No Of Stay Nights
        /// </summary>
        public int Duration { get; set; }

    }
}
