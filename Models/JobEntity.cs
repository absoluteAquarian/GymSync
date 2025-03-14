using GymSync.Querying;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class JobEntity : IQueryKeyable<JobEntity, int>
    {
        [Key]
        public int job_id { get; set; }
        public string job_name { get; set; }
        public string job_description { get; set; }
        public float hourly_wage { get; set; }

        static Expression<Func<JobEntity, int>> IQueryKeyable<JobEntity, int>.GetPrimaryKey() {
            return j => j.job_id;
        }
    }
}
