using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync_Interface_v2.Models
{
    public class Appointment_x_ClientEntity : ICrossReference<Appointment_x_ClientEntity>, ICrossReferencePrimary<AppointmentEntity>, ICrossReferenceForeign<ClientEntity>
    {
        [Key]
        public int appointment_id { get; set; }
        public int client_id { get; set; }

        static Expression<Func<Appointment_x_ClientEntity, int>> ICrossReference<Appointment_x_ClientEntity>.GetForeignKey() {
            return a => a.client_id;
        }

        static Expression<Func<Appointment_x_ClientEntity, int>> IQueryKeyable<Appointment_x_ClientEntity, int>.GetPrimaryKey() {
            return a => a.appointment_id;
        }
    }
}
