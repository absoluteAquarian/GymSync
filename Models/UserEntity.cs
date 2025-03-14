using GymSync.Querying;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class UserEntity : IQueryKeyable<UserEntity, int>
    {
        [Key]
        public int user_id { get; set; }
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        public string userPassword { get; set; }
        public string email { get; set; }

        static Expression<Func<UserEntity, int>> IQueryKeyable<UserEntity, int>.GetPrimaryKey() {
            return u => u.user_id;
        }
    }
}
