using GymSync.Querying;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class User_x_StaffEntity : ICrossReference<User_x_StaffEntity>, ICrossReferencePrimary<UserEntity>, ICrossReferenceForeign<StaffEntity>
    {
        [Key]
        public int user_id { get; set; }
        public int staff_id { get; set; }

        static Expression<Func<User_x_StaffEntity, int>> ICrossReference<User_x_StaffEntity>.GetForeignKey() {
            return e => e.staff_id;
        }

        static Expression<Func<User_x_StaffEntity, int>> IQueryKeyable<User_x_StaffEntity, int>.GetPrimaryKey() {
            return e => e.user_id;
        }
    }
}
