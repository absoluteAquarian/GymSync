using GymSync.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GymSync.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> USER { get; set; }
        public DbSet<ClientEntity> CLIENT { get; set; }
        public DbSet<TrainerEntity> TRAINER { get; set; }
        public DbSet<StaffEntity> STAFF { get; set; }
        public DbSet<JobEntity> JOB { get; set; }
        public DbSet<AppointmentEntity> APPOINTMENT { get; set; }
        public DbSet<ItemEntity> ITEM { get; set; }
        public DbSet<EquipmentEntity> EQUIPMENT { get; set; }
    }

    

    
}
