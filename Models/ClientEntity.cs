using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class ClientEntity : IQueryKeyable<ClientEntity, int>
    {
        [Key]
        public int client_id { get; set; }
        public int current_trainer_id { get; set; }

        static Expression<Func<ClientEntity, int>> IQueryKeyable<ClientEntity, int>.GetPrimaryKey() {
            return c => c.client_id;
        }
    }
}
