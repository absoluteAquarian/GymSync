using GymSync.Querying;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class AppointmentEntity : IQueryKeyable<AppointmentEntity, int>
    {
        [Key]
        public int appointment_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }

        static Expression<Func<AppointmentEntity, int>> IQueryKeyable<AppointmentEntity, int>.GetPrimaryKey() {
            return a => a.appointment_id;
        }
    }
}
