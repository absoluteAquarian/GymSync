using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class Staff_x_JobEntity
    {
        [Key]
        public int staff_id {get; set;}
        public int job_id { get; set; }
    }
}
