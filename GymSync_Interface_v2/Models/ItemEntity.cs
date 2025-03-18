using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync_Interface_v2.Models
{
    public class ItemEntity : IQueryKeyable<ItemEntity, int>
    {
        [Key]
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }

        static Expression<Func<ItemEntity, int>> IQueryKeyable<ItemEntity, int>.GetPrimaryKey() {
            return i => i.item_id;
        }
    }
}
