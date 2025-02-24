using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class Equipment_x_ItemEntity
    {
        [Key]
        public int equipment_id { get; set; }
        public int item_id { get; set; }
    }
}
