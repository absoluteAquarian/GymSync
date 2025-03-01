using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class Staff_x_JobEntity : ICrossReference<Staff_x_JobEntity>, ICrossReferencePrimary<StaffEntity>, ICrossReferenceForeign<JobEntity>
    {
        [Key]
        public int staff_id {get; set;}
        public int job_id { get; set; }

		static Expression<Func<Staff_x_JobEntity, int>> ICrossReference<Staff_x_JobEntity>.GetForeignKey() {
			return e => e.job_id;
		}

		static Expression<Func<Staff_x_JobEntity, int>> IQueryKeyable<Staff_x_JobEntity, int>.GetPrimaryKey() {
			return e => e.staff_id;
		}
	}
}
