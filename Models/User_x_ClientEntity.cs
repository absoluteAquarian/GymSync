using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace GymSync.Models
{
    public class User_x_ClientEntity : ICrossReference<User_x_ClientEntity>, ICrossReferencePrimary<UserEntity>, ICrossReferenceForeign<ClientEntity>
    {
        [Key]
        public int user_id { get; set; }
        public int client_id { get; set; }

        static Expression<Func<User_x_ClientEntity, int>> ICrossReference<User_x_ClientEntity>.GetForeignKey() {
            return e => e.client_id;
        }

        static Expression<Func<User_x_ClientEntity, int>> IQueryKeyable<User_x_ClientEntity, int>.GetPrimaryKey() {
            return e => e.user_id;
        }
    }
}
