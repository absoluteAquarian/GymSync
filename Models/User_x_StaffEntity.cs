using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class User_x_StaffEntity
    {
        [Key]
        public int user_id { get; set; }
        public int staff_id { get; set; }
    }
}
