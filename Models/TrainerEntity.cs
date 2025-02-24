using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class TrainerEntity
    {
        [Key]
        public int trainer_id { get; set; }
    }
}
