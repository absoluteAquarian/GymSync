using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class StaffEntity
    {
        [Key]
        public int staff_id { get; set; }
    }
}
