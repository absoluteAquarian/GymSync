using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class User_x_TrainerEntity
    {
        [Key]
        public int user_id { get; set; }
        public int trainer_id { get; set; }
    }
}
