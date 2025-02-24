using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class AppointmentEntity
    {
        [Key]
        public int appointment_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}
