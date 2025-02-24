using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSync.Models
{
    public class User_x_ClientEntity
    {
        [Key]
        public int user_id { get; set; }
        public int client_id { get; set; }
    }
}
