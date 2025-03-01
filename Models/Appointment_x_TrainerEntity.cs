using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class Appointment_x_TrainerEntity
    {
        [Key]
        public int appointment_id { get; set; }
        public int trainer_id { get; set; }
    }
}
