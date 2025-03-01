using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class User_x_TrainerEntity : ICrossReference<User_x_TrainerEntity>, ICrossReferencePrimary<UserEntity>, ICrossReferenceForeign<TrainerEntity>
    {
        [Key]
        public int user_id { get; set; }
        public int trainer_id { get; set; }

        static Expression<Func<User_x_TrainerEntity, int>> ICrossReference<User_x_TrainerEntity>.GetForeignKey() {
            return e => e.trainer_id;
        }

        static Expression<Func<User_x_TrainerEntity, int>> IQueryKeyable<User_x_TrainerEntity, int>.GetPrimaryKey() {
            return e => e.user_id;
        }
    }
}
