using GymSync.Querying;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class StaffEntity : IQueryKeyable<StaffEntity, int>
    {
        [Key]
        public int staff_id { get; set; }

        static Expression<Func<StaffEntity, int>> IQueryKeyable<StaffEntity, int>.GetPrimaryKey() {
            return s => s.staff_id;
        }
    }
}
