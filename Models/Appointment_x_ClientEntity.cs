using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class Appointment_x_ClientEntity
    {
        [Key]
        public int appointment_id { get; set; }
        public int client_id { get; set; }
    }
}
