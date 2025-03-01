using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class ItemEntity
    {
        [Key]
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
    }
}
