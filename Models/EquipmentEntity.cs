using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class EquipmentEntity
    {
        [Key]
        public int equipment_id { get; set; }
        public bool in_use { get; set; }
        public string location_name { get; set; }
    }
}
