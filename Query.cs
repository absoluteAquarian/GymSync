﻿using GymSync.Data;
using GymSync.Models;
using GymSync.Querying;
using GymSync.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymSync {
	public class Query(ApplicationDbContext context) {
		private readonly ApplicationDbContext _context = context;

		#region Cross-Reference Queries
		public async Task<ClientEntity?> AppointmentToClient(int appointmentID) {
			return await _context.APPOINTMENT
				.Where(a => a.appointment_id == appointmentID)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ClientEntity>> AppointmentToClientMany(IEnumerable<int> appointmentIDs) {
			return await _context.APPOINTMENT
				.WhereKeysMatch(appointmentIDs)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<ClientEntity>> AppointmentToClientAll() {
			return await _context.APPOINTMENT
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<AppointmentEntity>> ClientToAppointmentAll(int client_id) {
			return await _context.CLIENT
				.Where(c => c.client_id == client_id)
				.ToCrossReferenceForeign(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferencePrimary(_context.APPOINTMENT)
				.ToListAsync();
		}

		public async Task<TrainerEntity?> AppointmentToTrainer(int appointmentID) {
			return await _context.APPOINTMENT
				.Where(a => a.appointment_id == appointmentID)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.FirstOrDefaultAsync();
		}

		public async Task<List<TrainerEntity>> AppointmentToTrainerMany(IEnumerable<int> appointmentIDs) {
			return await _context.APPOINTMENT
				.WhereKeysMatch(appointmentIDs)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<List<TrainerEntity>> AppointmentToTrainerAll() {
			return await _context.APPOINTMENT
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<UserView?> ClientToUser(int clientID) {
			return await _context.CLIENT
				.Where(c => c.client_id == clientID)
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> ClientToUserMany(IEnumerable<int> clientIDs) {
			return await _context.CLIENT
				.WhereKeysMatch(clientIDs)
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> ClientToUserAll() {
			return await _context.CLIENT
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<ItemEntity?> EquipmentToItem(int equipmentID) {
			return await _context.EQUIPMENT
				.Where(e => e.equipment_id == equipmentID)
				.ToCrossReferencePrimary(_context.EQUIPMENT_x_ITEM)
				.FromCrossReferenceForeign(_context.ITEM)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ItemEntity>> EquipmentToItemMany(IEnumerable<int> equipmentIDs) {
			return await _context.EQUIPMENT
				.WhereKeysMatch(equipmentIDs)
				.ToCrossReferencePrimary(_context.EQUIPMENT_x_ITEM)
				.FromCrossReferenceForeign(_context.ITEM)
				.ToListAsync();
		}

		public async Task<List<ItemEntity>> EquipmentToItemAll() {
			return await _context.EQUIPMENT
				.ToCrossReferencePrimary(_context.EQUIPMENT_x_ITEM)
				.FromCrossReferenceForeign(_context.ITEM)
				.ToListAsync();
		}

		public async Task<List<EquipmentEntity>> EquipmentToEquipmentAll() {
			return await _context.EQUIPMENT
				.WhereKeysMatch(_context.EQUIPMENT_x_ITEM)
				.ToListAsync();
		}

		public async Task<JobEntity?> StaffToJob(int staffID) {
			return await _context.STAFF
				.Where(s => s.staff_id == staffID)
				.ToCrossReferencePrimary(_context.STAFF_x_JOB)
				.FromCrossReferenceForeign(_context.JOB)
				.FirstOrDefaultAsync();
		}

		public async Task<List<JobEntity>> StaffToJobMany(IEnumerable<int> staffIDs) {
			return await _context.STAFF
				.WhereKeysMatch(staffIDs)
				.ToCrossReferencePrimary(_context.STAFF_x_JOB)
				.FromCrossReferenceForeign(_context.JOB)
				.ToListAsync();
		}

		public async Task<List<JobEntity>> StaffToJobAll() {
			return await _context.STAFF
				.ToCrossReferencePrimary(_context.STAFF_x_JOB)
				.FromCrossReferenceForeign(_context.JOB)
				.ToListAsync();
		}

		public async Task<UserView?> StaffToUser(int staffID) {
			return await _context.STAFF
				.Where(s => s.staff_id == staffID)
				.ToCrossReferenceForeign(_context.USER_x_STAFF)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> StaffToUserMany(IEnumerable<int> staffIDs) {
			return await _context.STAFF
				.WhereKeysMatch(staffIDs)
				.ToCrossReferenceForeign(_context.USER_x_STAFF)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> StaffToUserAll() {
			return await _context.STAFF
				.ToCrossReferenceForeign(_context.USER_x_STAFF)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<UserView?> TrainerToUser(int trainerID) {
			return await _context.TRAINER
				.Where(t => t.trainer_id == trainerID)
				.ToCrossReferenceForeign(_context.USER_x_TRAINER)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> TrainerToUserMany(IEnumerable<int> trainerIDs) {
			return await _context.TRAINER
				.WhereKeysMatch(trainerIDs)
				.ToCrossReferenceForeign(_context.USER_x_TRAINER)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> TrainerToUserAll() {
			return await _context.TRAINER
				.ToCrossReferenceForeign(_context.USER_x_TRAINER)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<ClientEntity?> UserToClient(int userID) {
			return await _context.USER
				.Where(u => u.user_id == userID)
				.ToCrossReferencePrimary(_context.USER_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ClientEntity>> UserToClientMany(IEnumerable<int> userIDs) {
			return await _context.USER
				.WhereKeysMatch(userIDs)
				.ToCrossReferencePrimary(_context.USER_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<ClientEntity>> UserToClientAll() {
			return await _context.USER
				.ToCrossReferencePrimary(_context.USER_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<StaffEntity?> UserToStaff(int userID) {
			return await _context.USER
				.Where(u => u.user_id == userID)
				.ToCrossReferencePrimary(_context.USER_x_STAFF)
				.FromCrossReferenceForeign(_context.STAFF)
				.FirstOrDefaultAsync();
		}

		public async Task<List<StaffEntity>> UserToStaffMany(IEnumerable<int> userIDs) {
			return await _context.USER
				.WhereKeysMatch(userIDs)
				.ToCrossReferencePrimary(_context.USER_x_STAFF)
				.FromCrossReferenceForeign(_context.STAFF)
				.ToListAsync();
		}

		public async Task<List<StaffEntity>> UserToStaffAll() {
			return await _context.USER
				.ToCrossReferencePrimary(_context.USER_x_STAFF)
				.FromCrossReferenceForeign(_context.STAFF)
				.ToListAsync();
		}

		public async Task<TrainerEntity?> UserToTrainer(int userID) {
			return await _context.USER
				.Where(u => u.user_id == userID)
				.ToCrossReferencePrimary(_context.USER_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.FirstOrDefaultAsync();
		}

		public async Task<List<TrainerEntity>> UserToTrainerMany(IEnumerable<int> userIDs) {
			return await _context.USER
				.WhereKeysMatch(userIDs)
				.ToCrossReferencePrimary(_context.USER_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<List<TrainerEntity>> UserToTrainerAll() {
			return await _context.USER
				.ToCrossReferencePrimary(_context.USER_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}
		#endregion

		public async Task<List<StaticGroup<UserView, UserView>>> GetClientsForTrainerAll() {
			// For data being propagated through the query, the extensions won't help

			// TODO: Return empty client lists for trainers with no clients?
			return await _context.TRAINER
				.ToCrossReferenceForeign(_context.APPOINTMENT_x_TRAINER)
				// Resolve the trainer user for each appointmen
				.AnonymousMergeCrossReferenceForeign(_context.USER_x_TRAINER, at => at.trainer_id, (at, ut) => new { at.appointment_id, at.trainer_id, ut.user_id })
				.AnonymousMergeWhereMatchesKeys(_context.USER, anon => anon.user_id, (anon, u) => new { anon.appointment_id, anon.trainer_id, TrainerUser = new UserView(u.user_id, u.firstName, u.lastName) })
				// Resolve the client user for each appointment
				.AnonymousMergeCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT, anon => anon.appointment_id, (anon, ac) => new { anon.trainer_id, anon.TrainerUser, ac.client_id })
				.AnonymousWhereMatchesKeys(_context.CLIENT, anon => anon.client_id)
				.AnonymousMergeWhereMatchesKeys(_context.USER_x_CLIENT, anon => anon.client_id, (anon, uc) => new { anon.trainer_id, anon.TrainerUser, uc.user_id })
				.AnonymousMergeWhereMatchesKeys(_context.USER, anon => anon.user_id, (anon, u) => new { anon.trainer_id, anon.TrainerUser, ClientUser = new UserView(u.user_id, u.firstName, u.lastName) })
				// Group the clients by their trainer
				.GroupBy(anon => anon.trainer_id, anon => new { anon.TrainerUser, anon.ClientUser })
				// Convert from server-sided evaluation (SQL) to client-sided evaluation (C#)
				// The group transformations should be handled client-side since doing them server-side results in absurdly large and complex queries
				.AsAsyncEnumerable()
				.TransformGroupsByElement(anon => anon.TrainerUser, anon => anon.ClientUser)
				.ToListAsync();
		}

		public async Task<List<EquipmentView>> GetEquipmentAll() {
			// For data being propagated through the query, the extensions won't help

			return await _context.EQUIPMENT
				// Resolve the equipment for each reference
				.MergeWithCrossReferencePrimary(_context.EQUIPMENT_x_ITEM, (e, ei) => new { ei.item_id, e.equipment_id, e.location_name, e.in_use })
				// Resolve the item for each equipment
				.AnonymousMergeWhereMatchesKeys(_context.ITEM, anon => anon.item_id, (anon, i) => new EquipmentView(anon.equipment_id, i.item_name, anon.location_name, anon.in_use))
				.ToListAsync();
		}

		public async Task<List<StaffJobView>> GetUserAndJobForStaffAll() {
			// For data being propagated through the query, the extensions won't help
			// This method allows missing columns when joining the info from the JOB table since a staff member may not have a job

			return await _context.STAFF
				.ToCrossReferencePrimary(_context.STAFF_x_JOB)
				// Resolve the job for each staff member.  If the job doesn't exist, the job info will be null
				.MergeFromCrossReferenceForeignAllowNull(_context.JOB, (sj, j) => new { sj.staff_id, Job = j })
				// Resolve the user for each staff member
				.AnonymousMergeCrossReferenceForeign(_context.USER_x_STAFF, anon => anon.staff_id, (anon, us) => new { anon.staff_id, anon.Job, us.user_id })
				.AnonymousMergeWhereMatchesKeys(_context.USER, anon => anon.user_id, (anon, u) => new { anon.staff_id, anon.Job, User = new UserView(u.user_id, u.firstName, u.lastName) })
				// Convert from server-sided evaluation (SQL) to client-sided evaluation (C#)
				// The job columns have to be resolved client-side since expression trees don't support null-coalescing / null-propagation operators
				.AsAsyncEnumerable()
				.Select(i => new StaffJobView(i.staff_id, i.User.UserID, i.User.FirstName, i.User.LastName, i.Job?.job_name ?? "No job assigned", i.Job?.job_description ?? "", i.Job?.hourly_wage ?? 0))
				.ToListAsync();
		}
	}

	public static partial class QueryExtensions {
		public static IQueryable<TResult> AnonymousMergeCrossReferencePrimary<TAnonymous, TCross, TResult>(this IQueryable<TAnonymous> query, IQueryable<TCross> cross, Expression<Func<TAnonymous, int>> anonymousKeySelector, Expression<Func<TAnonymous, TCross, TResult>> resultSelector)
		where TCross : ICrossReference<TCross> {
			return query.Join(cross, anonymousKeySelector, TCross.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> AnonymouseMergeCrossReferencePrimaryAllowNull<TAnonymous, TCross, TResult>(this IQueryable<TAnonymous> query, IQueryable<TCross> cross, Expression<Func<TAnonymous, int>> anonymousKeySelector, Expression<Func<TAnonymous, TCross?, TResult>> resultSelector)
		where TCross : ICrossReference<TCross> {
			return query.LeftOuterJoin(cross, anonymousKeySelector, TCross.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> AnonymousMergeCrossReferenceForeign<TAnonymous, TCross, TResult>(this IQueryable<TAnonymous> query, IQueryable<TCross> cross, Expression<Func<TAnonymous, int>> anonymousKeySelector, Expression<Func<TAnonymous, TCross, TResult>> resultSelector)
		where TCross : ICrossReference<TCross> {
			return query.Join(cross, anonymousKeySelector, TCross.GetForeignKey(), resultSelector);
		}

		public static IQueryable<TResult> AnonymousMergeCrossReferenceForeignAllowNull<TAnonymous, TCross, TResult>(this IQueryable<TAnonymous> query, IQueryable<TCross> cross, Expression<Func<TAnonymous, int>> anonymousKeySelector, Expression<Func<TAnonymous, TCross?, TResult>> resultSelector)
		where TCross : ICrossReference<TCross> {
			return query.LeftOuterJoin(cross, anonymousKeySelector, TCross.GetForeignKey(), resultSelector);
		}

		public static IQueryable<TResult> AnonymousMergeWhereMatchesKeys<TAnonymous, TTo, TResult>(this IQueryable<TAnonymous> query, IQueryable<TTo> to, Expression<Func<TAnonymous, int>> anonymousKeySelector, Expression<Func<TAnonymous, TTo, TResult>> resultSelector)
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, anonymousKeySelector, TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> AnonymousMergeWhereMatchesKeysAllowNull<TAnonymous, TTo, TResult>(this IQueryable<TAnonymous> query, IQueryable<TTo> to, Expression<Func<TAnonymous, int>> anonymousKeySelector, Expression<Func<TAnonymous, TTo?, TResult>> resultSelector)
		where TTo : IQueryKeyable<TTo, int> {
			return query.LeftOuterJoin(to, anonymousKeySelector, TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TAnonymous> AnonymousWhereMatchesKeys<TAnonymous, TTo>(this IQueryable<TAnonymous> query, IQueryable<TTo> to, Expression<Func<TAnonymous, int>> anonymousKeySelector)
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, anonymousKeySelector, TTo.GetPrimaryKey(), (q, _) => q);
		}

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

		public static IQueryable<TResult> MergeFromCrossReferencePrimary<TCross, TTo, TResult>(this IQueryable<TCross> query, IQueryable<TTo> to, Expression<Func<TCross, TTo, TResult>> resultSelector)
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetPrimaryKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeFromCrossReferencePrimaryAllowNull<TCross, TTo, TResult>(this IQueryable<TCross> query, IQueryable<TTo> to, Expression<Func<TCross, TTo?, TResult>> resultSelector)
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.LeftOuterJoin(to, TCross.GetPrimaryKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeFromCrossReferenceForeign<TCross, TTo, TResult>(this IQueryable<TCross> query, IQueryable<TTo> to, Expression<Func<TCross, TTo, TResult>> resultSelector)
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeFromCrossReferenceForeignAllowNull<TCross, TTo, TResult>(this IQueryable<TCross> query, IQueryable<TTo> to, Expression<Func<TCross, TTo?, TResult>> resultSelector)
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.LeftOuterJoin(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeWhereKeysMatch<TFrom, TTo, TResult>(this IQueryable<TFrom> query, IQueryable<TTo> to, Expression<Func<TFrom, TTo, TResult>> resultSelector)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeWhereKeysMatchAllowNull<TFrom, TTo, TResult>(this IQueryable<TFrom> query, IQueryable<TTo> to, Expression<Func<TFrom, TTo?, TResult>> resultSelector)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.LeftOuterJoin(to, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeWithCrossReferencePrimary<TFrom, TCross, TResult>(this IQueryable<TFrom> query, IQueryable<TCross> cross, Expression<Func<TFrom, TCross, TResult>> resultSelector)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeWithCrossReferencePrimaryAllowNull<TFrom, TCross, TResult>(this IQueryable<TFrom> query, IQueryable<TCross> cross, Expression<Func<TFrom, TCross?, TResult>> resultSelector)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TFrom> {
			return query.LeftOuterJoin(cross, TFrom.GetPrimaryKey(), TCross.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeWithCrossReferenceForeign<TFrom, TCross, TResult>(this IQueryable<TFrom> query, IQueryable<TCross> cross, Expression<Func<TFrom, TCross, TResult>> resultSelector)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetForeignKey(), resultSelector);
		}

		public static IQueryable<TResult> MergeWithCrossReferenceForeignAllowNull<TFrom, TCross, TResult>(this IQueryable<TFrom> query, IQueryable<TCross> cross, Expression<Func<TFrom, TCross?, TResult>> resultSelector)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TFrom> {
			return query.LeftOuterJoin(cross, TFrom.GetPrimaryKey(), TCross.GetForeignKey(), resultSelector);
		}

		public static IQueryable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IQueryable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner?, TResult>> resultSelector) {
			return QueryReflectionMethods.CreateLeftOuterJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector!);
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

		public static StaticGroup<TKeyTo, TElementTo> TransformGroup<TKeyFrom, TElementFrom, TKeyTo, TElementTo>(this IGrouping<TKeyFrom, TElementFrom> grouping, Func<TKeyFrom, TKeyTo> keyTransform, Func<TElementFrom, TElementTo> elementTransform) {
			return new StaticGroup<TKeyTo, TElementTo>(keyTransform(grouping.Key), grouping.Select(elementTransform));
		}

		public static IAsyncEnumerable<StaticGroup<TKeyTo, TElementTo>> TransformGroups<TKeyFrom, TElementFrom, TKeyTo, TElementTo>(this IAsyncEnumerable<IGrouping<TKeyFrom, TElementFrom>> groupings, Func<TKeyFrom, TKeyTo> keyTransform, Func<TElementFrom, TElementTo> elementTransform) {
			return groupings.Select(g => g.TransformGroup(keyTransform, elementTransform));
		}

		public static StaticGroup<TKeyTo, TElementTo> TransformGroupByElement<TKeyFrom, TElementFrom, TKeyTo, TElementTo>(this IGrouping<TKeyFrom, TElementFrom> grouping, Func<TElementFrom, TKeyTo> keyTransform, Func<TElementFrom, TElementTo> elementTransform) {
			return new StaticGroup<TKeyTo, TElementTo>(keyTransform(grouping.First()), grouping.Select(elementTransform));
		}

		public static IAsyncEnumerable<StaticGroup<TKeyTo, TElementTo>> TransformGroupsByElement<TKeyFrom, TElementFrom, TKeyTo, TElementTo>(this IAsyncEnumerable<IGrouping<TKeyFrom, TElementFrom>> groupings, Func<TElementFrom, TKeyTo> keyTransform, Func<TElementFrom, TElementTo> elementTransform) {
			return groupings.Select(g => g.TransformGroupByElement(keyTransform, elementTransform));
		}

		public static IQueryable<TTo> TransformWhereForeignKeyMatches<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TTo : IQueryKeyable<TTo, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo> {
			return query.Join(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<TResult> TransformWhereForeignKeyMatches<TCross, TTo, TResult>(this IQueryable<TCross> query, IQueryable<TTo> to, Expression<Func<TCross, TTo, TResult>> resultSelector)
		where TTo : IQueryKeyable<TTo, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo> {
			return query.Join(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TTo> TransformWhereKeysMatch<TFrom, TTo>(this IQueryable<TFrom> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<TResult> TransformWhereKeysMatch<TFrom, TTo, TResult>(this IQueryable<TFrom> query, IQueryable<TTo> keys, Expression<Func<TFrom, TTo, TResult>> resultSelector)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), resultSelector);
		}

		public static IQueryable<TCross> WhereForeignKeysMatchesKeys<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TTo : IQueryKeyable<TTo, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo> {
			return query.Join(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), (q, _) => q);
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

		public static IQueryable<TFrom> WhereKeysMatchForeignKeys<TFrom, TCross>(this IQueryable<TFrom> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetForeignKey(), (q, _) => q);
		}
	}
}
