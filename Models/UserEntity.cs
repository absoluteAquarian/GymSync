using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymSync.Models
{
    public class UserEntity
    {
        [Key]
        public int user_id { get; set; }
        [DisplayName("First Name")]
        public string? firstName { get; set; }
        [DisplayName("Last Name")]
        public string? lastName { get; set; }
        public string? userPassword { get; set; }
        public string? email { get; set; }
    }
}
