using GymSync.Data;
using GymSync.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Numerics;

namespace GymSync {
	public class Query(ApplicationDbContext context) {
		private readonly ApplicationDbContext _context = context;

		#region Cross-Reference Queries
		public async Task<ClientEntity?> AppointmentToClient(int appointmentID) {
			return await FilterOne(
				_context.APPOINTMENT_x_CLIENT,
				_context.CLIENT,
				ac => ac.appointment_id == appointmentID,
				ac => ac.client_id,
				c => c.client_id
			);
		}

		public async Task<TrainerEntity?> AppointmentToTrainer(int appointmentID) {
			return await FilterOne(
				_context.APPOINTMENT_x_TRAINER,
				_context.TRAINER,
				at => at.appointment_id == appointmentID,
				at => at.trainer_id,
				t => t.trainer_id
			);
		}

		public async Task<UserEntity?> ClientToUser(int clientID) {
			return await FilterOne(
				_context.USER_x_CLIENT,
				_context.USER,
				uc => uc.client_id == clientID,
				uc => uc.user_id,
				u => u.user_id
			);
		}

		public async Task<ItemEntity?> EquipmentToItem(int equipmentID) {
			return await FilterOne(
				_context.EQUIPMENT_x_ITEM,
				_context.ITEM,
				ei => ei.equipment_id == equipmentID,
				ei => ei.item_id,
				i => i.item_id
			);
		}

		public async Task<JobEntity?> StaffToJob(int staffID) {
			return await FilterOne(
				_context.STAFF_x_JOB,
				_context.JOB,
				sj => sj.staff_id == staffID,
				sj => sj.job_id,
				j => j.job_id
			);
		}

		public async Task<UserEntity?> StaffToUser(int staffID) {
			return await FilterOne(
				_context.USER_x_STAFF,
				_context.USER,
				us => us.staff_id == staffID,
				us => us.user_id,
				u => u.user_id
			);
		}

		public async Task<UserEntity?> TrainerToUser(int trainerID) {
			return await FilterOne(
				_context.USER_x_TRAINER,
				_context.USER,
				ut => ut.trainer_id == trainerID,
				ut => ut.user_id,
				u => u.user_id
			);
		}

		public async Task<ClientEntity?> UserToClient(int userID) {
			return await FilterOne(
				_context.USER_x_CLIENT,
				_context.CLIENT,
				uc => uc.user_id == userID,
				uc => uc.client_id,
				c => c.client_id
			);
		}

		public async Task<StaffEntity?> UserToStaff(int userID) {
			return await FilterOne(
				_context.USER_x_STAFF,
				_context.STAFF,
				us => us.user_id == userID,
				us => us.staff_id,
				s => s.staff_id
			);
		}

		public async Task<TrainerEntity?> UserToTrainer(int userID) {
			return await FilterOne(
				_context.USER_x_TRAINER,
				_context.TRAINER,
				ut => ut.user_id == userID,
				ut => ut.trainer_id,
				t => t.trainer_id
			);
		}
		#endregion

		public async Task<IEnumerable<ClientEntity>> GetClientsForTrainer(int trainerID) {
			return await FindMany(
				Filter(
					_context.APPOINTMENT_x_TRAINER,
					_context.APPOINTMENT_x_CLIENT,
					at => at.trainer_id == trainerID,
					at => at.appointment_id, ac => ac.appointment_id
				),
				_context.CLIENT,
				ac => ac.client_id
			);
		}

		#region Utility Functions
		private static IQueryable<TFilter> Filter<TOrig, TKey, TFilter>(DbSet<TOrig> set, DbSet<TFilter> filterSet, Expression<Func<TOrig, bool>> getOrigFilter, Expression<Func<TOrig, TKey>> getOrigKey, Expression<Func<TFilter, TKey>> getFilterKey) 
		where TOrig : class
		where TFilter : class {
			return set.Where(getOrigFilter).Join(filterSet, getOrigKey, getFilterKey, (s, f) => f);
		}

		private static async Task<TFilter?> FilterOne<TOrig, TKey, TFilter>(DbSet<TOrig> set, DbSet<TFilter> filterSet, Expression<Func<TOrig, bool>> getOrigFilter, Expression<Func<TOrig, TKey>> getOrigKey, Expression<Func<TFilter, TKey>> getFilterKey)
		where TOrig : class
		where TFilter : class {
			return await set.Where(getOrigFilter).Join(filterSet, getOrigKey, getFilterKey, (s, f) => f).FirstOrDefaultAsync();
		}

		private static async Task<List<TResult>> FindMany<TOrig, TResult>(IQueryable<TOrig> list, DbSet<TResult> set, Func<TOrig, object> getKey)
		where TResult : class {
			List<TResult> results = [];
			foreach (var item in await list.ToListAsync()) {
				var result = await set.FindAsync(getKey(item));
				if (result is not null)
					results.Add(result);
			}
			return results;
		}
		#endregion
	}
}
