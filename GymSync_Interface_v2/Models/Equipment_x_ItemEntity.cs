using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync_Interface_v2.Models
{
    public class Equipment_x_ItemEntity : ICrossReference<Equipment_x_ItemEntity>, ICrossReferencePrimary<EquipmentEntity>, ICrossReferenceForeign<ItemEntity>
    {
        [Key]
        public int equipment_id { get; set; }
        public int item_id { get; set; }

        static Expression<Func<Equipment_x_ItemEntity, int>> ICrossReference<Equipment_x_ItemEntity>.GetForeignKey() {
            return e => e.item_id;
        }

        static Expression<Func<Equipment_x_ItemEntity, int>> IQueryKeyable<Equipment_x_ItemEntity, int>.GetPrimaryKey() {
            return e => e.equipment_id;
        }
    }
}
