using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//namespace GymSync_Interface_v2.Data
//{
//    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
//    {
//    }
//}

using GymSync_Interface_v2.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GymSync_Interface_v2.Data
{
	//public class ApplicationDbContext : DbContext
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
		//public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		//{
		//}
		public DbSet<UserEntity> USER { get; set; }
		public DbSet<ClientEntity> CLIENT { get; set; }
		public DbSet<TrainerEntity> TRAINER { get; set; }
		public DbSet<StaffEntity> STAFF { get; set; }
		public DbSet<JobEntity> JOB { get; set; }
		public DbSet<AppointmentEntity> APPOINTMENT { get; set; }
		public DbSet<ItemEntity> ITEM { get; set; }
		public DbSet<EquipmentEntity> EQUIPMENT { get; set; }
		public DbSet<Appointment_x_ClientEntity> APPOINTMENT_x_CLIENT { get; set; }
		public DbSet<Appointment_x_TrainerEntity> APPOINTMENT_x_TRAINER { get; set; }
		public DbSet<Equipment_x_ItemEntity> EQUIPMENT_x_ITEM { get; set; }
		public DbSet<Staff_x_JobEntity> STAFF_x_JOB { get; set; }
		public DbSet<User_x_ClientEntity> USER_x_CLIENT { get; set; }
		public DbSet<User_x_StaffEntity> USER_x_STAFF { get; set; }
		public DbSet<User_x_TrainerEntity> USER_x_TRAINER { get; set; }
	}
}