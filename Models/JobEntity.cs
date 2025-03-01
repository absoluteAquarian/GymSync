using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class JobEntity
    {
        [Key]
        public int job_id { get; set; }
        public string job_name { get; set; }
        public string job_description { get; set; }
        public float hourly_wage { get; set; }

    }
}
