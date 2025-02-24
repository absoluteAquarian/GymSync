using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class ClientEntity
    {
        [Key]
        public int client_id { get; set; }
        public int current_trainer_id { get; set; }
    }
}
