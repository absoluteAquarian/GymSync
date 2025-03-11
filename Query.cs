using GymSync.Data;
using GymSync.Models;
using GymSync.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace GymSync {
	public class Query(ApplicationDbContext context) {
		private readonly ApplicationDbContext _context = context;

		#region Cross-Reference Queries
		public async Task<ClientEntity?> AppointmentToClient(int appointmentID) {
			return await _context.APPOINTMENT_x_CLIENT
				.Where(ac => ac.appointment_id == appointmentID)
				.FromCrossReferenceForeign(_context.CLIENT)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ClientEntity>> AppointmentToClientMany(IEnumerable<int> appointmentIDs) {
			return await _context.APPOINTMENT_x_CLIENT
				.WhereKeysMatch(appointmentIDs)
				.TransformWhereKeysMatch(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<ClientEntity>> AppointmentToClientAll() {
			return await _context.APPOINTMENT_x_CLIENT
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<AppointmentEntity>> ClientToAppointmentAll(int client_id) {
			return await _context.APPOINTMENT_x_CLIENT
				.Where(ac => ac.client_id == client_id)
				.FromCrossReferencePrimary(_context.APPOINTMENT)
				.ToListAsync();
		}

		public async Task<TrainerEntity?> AppointmentToTrainer(int appointmentID) {
			return await _context.APPOINTMENT_x_TRAINER
				.Where(at => at.appointment_id == appointmentID)
				.FromCrossReferenceForeign(_context.TRAINER)
				.FirstOrDefaultAsync();
		}

		public async Task<List<TrainerEntity>> AppointmentToTrainerMany(IEnumerable<int> appointmentIDs) {
			return await _context.APPOINTMENT_x_TRAINER
				.WhereKeysMatch(appointmentIDs)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<List<TrainerEntity>> AppointmentToTrainerAll() {
			return await _context.APPOINTMENT_x_TRAINER
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<UserView?> ClientToUser(int clientID) {
			return await _context.USER_x_CLIENT
				.Where(uc => uc.client_id == clientID)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> ClientToUserMany(IEnumerable<int> clientIDs) {
			return await _context.USER_x_CLIENT
				.WhereKeysMatch(clientIDs)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> ClientToUserAll() {
			return await _context.USER_x_CLIENT
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<ItemEntity?> EquipmentToItem(int equipmentID) {
			return await _context.EQUIPMENT_x_ITEM
				.Where(ei => ei.equipment_id == equipmentID)
				.FromCrossReferenceForeign(_context.ITEM)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ItemEntity>> EquipmentToItemMany(IEnumerable<int> equipmentIDs) {
			return await _context.EQUIPMENT_x_ITEM
				.WhereKeysMatch(equipmentIDs)
				.FromCrossReferenceForeign(_context.ITEM)
				.ToListAsync();
		}

		public async Task<List<ItemEntity>> EquipmentToItemAll() {
			return await _context.EQUIPMENT_x_ITEM
				.FromCrossReferenceForeign(_context.ITEM)
				.ToListAsync();
		}

		public async Task<List<EquipmentEntity>> EquipmentToEquipmentAll() {
			return await _context.EQUIPMENT_x_ITEM
				.FromCrossReferencePrimary(_context.EQUIPMENT)
				.ToListAsync();
		}

		public async Task<JobEntity?> StaffToJob(int staffID) {
			return await _context.STAFF_x_JOB
				.Where(sj => sj.staff_id == staffID)
				.FromCrossReferenceForeign(_context.JOB)
				.FirstOrDefaultAsync();
		}

		public async Task<List<JobEntity>> StaffToJobMany(IEnumerable<int> staffIDs) {
			return await _context.STAFF_x_JOB
				.WhereKeysMatch(staffIDs)
				.FromCrossReferenceForeign(_context.JOB)
				.ToListAsync();
		}

		public async Task<List<JobEntity>> StaffToJobAll() {
			return await _context.STAFF_x_JOB
				.FromCrossReferenceForeign(_context.JOB)
				.ToListAsync();
		}

		public async Task<UserView?> StaffToUser(int staffID) {
			return await _context.USER_x_STAFF
				.Where(us => us.staff_id == staffID)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> StaffToUserMany(IEnumerable<int> staffIDs) {
			return await _context.USER_x_STAFF
				.WhereKeysMatch(staffIDs)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> StaffToUserAll() {
			return await _context.USER_x_STAFF
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<UserView?> TrainerToUser(int trainerID) {
			return await _context.USER_x_TRAINER
				.Where(ut => ut.trainer_id == trainerID)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> TrainerToUserMany(IEnumerable<int> trainerIDs) {
			return await _context.USER_x_TRAINER
				.WhereKeysMatch(trainerIDs)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> TrainerToUserAll() {
			return await _context.USER_x_TRAINER
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<ClientEntity?> UserToClient(int userID) {
			return await _context.USER_x_CLIENT
				.Where(uc => uc.user_id == userID)
				.FromCrossReferenceForeign(_context.CLIENT)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ClientEntity>> UserToClientMany(IEnumerable<int> userIDs) {
			return await _context.USER_x_CLIENT
				.WhereKeysMatch(userIDs)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<ClientEntity>> UserToClientAll() {
			return await _context.USER_x_CLIENT
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<StaffEntity?> UserToStaff(int userID) {
			return await _context.USER_x_STAFF
				.Where(us => us.user_id == userID)
				.FromCrossReferenceForeign(_context.STAFF)
				.FirstOrDefaultAsync();
		}

		public async Task<List<StaffEntity>> UserToStaffMany(IEnumerable<int> userIDs) {
			return await _context.USER_x_STAFF
				.WhereKeysMatch(userIDs)
				.FromCrossReferenceForeign(_context.STAFF)
				.ToListAsync();
		}

		public async Task<List<StaffEntity>> UserToStaffAll() {
			return await _context.USER_x_STAFF
				.FromCrossReferenceForeign(_context.STAFF)
				.ToListAsync();
		}

		public async Task<TrainerEntity?> UserToTrainer(int userID) {
			return await _context.USER_x_TRAINER
				.Where(ut => ut.user_id == userID)
				.FromCrossReferenceForeign(_context.TRAINER)
				.FirstOrDefaultAsync();
		}

		public async Task<List<TrainerEntity>> UserToTrainerMany(IEnumerable<int> userIDs) {
			return await _context.USER_x_TRAINER
				.WhereKeysMatch(userIDs)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<List<TrainerEntity>> UserToTrainerAll() {
			return await _context.USER_x_TRAINER
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}
		#endregion

		public async Task<List<UserView>> GetClientsForTrainer(int trainerID) {
			return await _context.APPOINTMENT_x_TRAINER
				.Where(at => at.trainer_id == trainerID)
				.TransformWhereKeysMatch(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<StaticGroup<UserView, UserView>>> GetClientsForTrainerAll() {
			// For data being propagated through the query, the extensions won't help
			// The anonymous types are based on the tree from GetClientsForTrainer()
			return await _context.APPOINTMENT_x_TRAINER
				.Join(_context.TRAINER, at => at.trainer_id, t => t.trainer_id, (at, t) => new { TrainerID = at.trainer_id, AppointmentID = at.appointment_id })
				.Join(_context.USER_x_TRAINER, i => i.TrainerID, ut => ut.trainer_id, (i, ut) => new { i.TrainerID, i.AppointmentID, TrainerUserID = ut.user_id })
				.Join(_context.USER, i => i.TrainerUserID, u => u.user_id, (i, u) => new { i.TrainerID, i.AppointmentID, TrainerUser = new UserView(i.TrainerUserID, u.firstName, u.lastName) })
				.Join(_context.APPOINTMENT_x_CLIENT, i => i.AppointmentID, ac => ac.appointment_id, (i, ac) => new { i.TrainerID, i.TrainerUser, ClientID = ac.client_id })
				.Join(_context.CLIENT, i => i.ClientID, c => c.client_id, (i, _) => i)
				.Join(_context.USER_x_CLIENT, i => i.ClientID, uc => uc.client_id, (i, uc) => new { i.TrainerID, i.TrainerUser, ClientUserID = uc.user_id })
				.Join(_context.USER, i => i.ClientUserID, u => u.user_id, (i, u) => new { i.TrainerID, i.TrainerUser, ClientUser = new UserView(i.ClientUserID, u.firstName, u.lastName) })
				.GroupBy(i => i.TrainerID, i => i)
				.Select(g => new StaticGroup<UserView, UserView>(g.First().TrainerUser, g.Select(i => i.ClientUser)))
				.ToListAsync();
		}
	}

	public record class StaticGroup<TKey, TElement>(TKey Key, IEnumerable<TElement> Elements) : IGrouping<TKey, TElement> {
		TKey IGrouping<TKey, TElement>.Key => Key;

		IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator() => Elements.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();
	}

	public static class QueryExtensions {
		public static IQueryable<UserView> AsUserView(this IQueryable<UserEntity> query) {
			return query.Select(u => new UserView(u.user_id, u.firstName, u.lastName));
		}

		public static IQueryable<TTo> FromCrossReferencePrimary<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetPrimaryKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<TTo> FromCrossReferenceForeign<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<TCross> ToCrossReferencePrimary<TFrom, TCross>(this IQueryable<TFrom> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<TCross> ToCrossReferenceForeign<TFrom, TCross>(this IQueryable<TFrom> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetForeignKey(), (_, s) => s);
		}

		public static IQueryable<TTo> TransformWhereKeysMatch<TFrom, TTo>(this IQueryable<TFrom> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<TFrom> WhereKeysMatch<TFrom, TTo>(this IQueryable<TFrom> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), (q, _) => q);
		}

		public static IQueryable<TFrom> WhereKeysMatch<TFrom, TKey>(this IQueryable<TFrom> query, IEnumerable<TKey> keys)
		where TFrom : IQueryKeyable<TFrom, TKey> {
			return query.Join(keys, TFrom.GetPrimaryKey(), x => x, (q, _) => q);
		}
	}
}
