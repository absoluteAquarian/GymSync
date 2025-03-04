using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class Appointment_x_TrainerEntity : ICrossReference<Appointment_x_TrainerEntity>, ICrossReferencePrimary<AppointmentEntity>, ICrossReferenceForeign<TrainerEntity>
    {
        [Key]
        public int appointment_id { get; set; }
        public int trainer_id { get; set; }

        static Expression<Func<Appointment_x_TrainerEntity, int>> ICrossReference<Appointment_x_TrainerEntity>.GetForeignKey() {
            return a => a.trainer_id;
        }

        static Expression<Func<Appointment_x_TrainerEntity, int>> IQueryKeyable<Appointment_x_TrainerEntity, int>.GetPrimaryKey() {
            return a => a.appointment_id;
        }
    }
}
