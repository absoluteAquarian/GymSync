using GymSync.Data;
using GymSync.Models;
using GymSync.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static GymSync.QueryPropagationExtensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

		public async Task<List<QueryGroup<UserView, UserView>>> GetClientsForTrainerAll() {
			return await _context.TRAINER
				.ToCrossReferenceForeignAndPropagateKey(_context.USER_x_TRAINER)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.MergeOnPropagations(
					_context.APPOINTMENT_x_TRAINER
						.TransformWhereKeysMatchAndPropagateKey(_context.APPOINTMENT_x_CLIENT)
						.FromCrossReferenceForeign(_context.CLIENT)
						.ToCrossReferenceForeign(_context.USER_x_CLIENT)
						.FromCrossReferencePrimary(_context.USER)
						.AsUserView()
				)
				.ToListAsync();
		}
	}

	public record class QueryPropagation<TItem, TEntity>(TItem Item, TEntity Entity) {
		public static PropertyInfo ItemProperty { get; } = typeof(QueryPropagation<TItem, TEntity>).GetProperty(nameof(QueryPropagation<TItem, TEntity>.Item))!;

		public static PropertyInfo EntityProperty { get; } = typeof(QueryPropagation<TItem, TEntity>).GetProperty(nameof(QueryPropagation<TItem, TEntity>.Entity))!;

		public static ConstructorInfo Constructor { get; } = typeof(QueryPropagation<TItem, TEntity>).GetConstructor([ typeof(TItem), typeof(TEntity) ])!;

		public class TransformBuilder(IQueryable<QueryPropagation<TItem, TEntity>> query) {
			internal IQueryable<QueryPropagation<TItem, TEntity>> query = query;

			public IQueryable<QueryPropagation<TItem, TEntity>> Transform() => query;

			public QueryPropagation<TTo, TEntity>.TransformBuilder TransformWhereMatches<TTo>(IQueryable<TTo> items, Expression<Func<TTo, TItem>> selector) {
				return new(query.Join<QueryPropagation<TItem, TEntity>, TTo, TItem, QueryPropagation<TTo, TEntity>>(items, p => p.Item, selector, (p, k) => new(k, p.Entity)));
			}

			public QueryPropagation<TTo, TEntity>.TransformBuilder TransformWhereMatchesKey<TTo>(IQueryable<TTo> keys) where TTo : IQueryKeyable<TTo, TItem> {
				return new(query.Join<QueryPropagation<TItem, TEntity>, TTo, TItem, QueryPropagation<TTo, TEntity>>(keys, p => p.Item, TTo.GetPrimaryKey(), (p, k) => new(k, p.Entity)));
			}

			public TransformBuilder WhereMatches<TTo>(IQueryable<TTo> keys, Expression<Func<TTo, TItem>> selector) {
				query = query.Join(keys, p => p.Item, selector, (p, _) => p);
				return this;
			}

			public TransformBuilder WhereMatchesKey<TKey>(IQueryable<TKey> keys) where TKey : IQueryKeyable<TKey, TItem> {
				query = query.Join(keys, p => p.Item, TKey.GetPrimaryKey(), (p, _) => p);
				return this;
			}
		}
	}

	public record class QueryGroup<TKey, TValue>(TKey Key, IEnumerable<TValue> Values);

	public static class QueryExtensions {
		#region AsUserView
		public static IQueryable<UserView> AsUserView(this IQueryable<UserEntity> query) {
			return query.Select(u => new UserView(u.user_id, u.firstName, u.lastName));
		}

		public static IQueryable<QueryPropagation<TItem, UserView>> AsUserView<TItem>(this IQueryable<QueryPropagation<TItem, UserEntity>> query) {
			return query.Select(q => new QueryPropagation<TItem, UserView>(q.Item, new UserView(q.Entity.user_id, q.Entity.firstName, q.Entity.lastName)));
		}

		public static QueryPropagation<UserView, TEntity>.TransformBuilder AsUserView<TEntity>(this QueryPropagation<UserEntity, TEntity>.TransformBuilder builder) {
			return new(builder.query.Select(p => new QueryPropagation<UserView, TEntity>(new UserView(p.Item.user_id, p.Item.firstName, p.Item.lastName), p.Entity)));
		}
		#endregion

		public static QueryPropagation<TItem, TEntity>.TransformBuilder CreatePropagationTransformer<TItem, TEntity>(this IQueryable<QueryPropagation<TItem, TEntity>> query) {
			return new(query);
		}

		#region FromCrossReference
		public static IQueryable<TTo> FromCrossReferencePrimary<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetPrimaryKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<QueryPropagation<int, TTo>> FromCrossReferencePrimaryAndPropagateKey<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetPrimaryKey(), TTo.GetPrimaryKey(), PropagateItemWith<TCross, int, TTo>(TCross.GetPrimaryKey()));
		}

		public static IQueryable<QueryPropagation<TItem, TTo>> FromCrossReferencePrimary<TItem, TCross, TTo>(this IQueryable<QueryPropagation<TItem, TCross>> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, UnwrapEntityPrimaryKey<TItem, TCross, int>(), TTo.GetPrimaryKey(), (q, s) => new QueryPropagation<TItem, TTo>(q.Item, s));
		}

		public static QueryPropagation<TTo, TEntity>.TransformBuilder FromCrossReferencePrimary<TCross, TEntity, TTo>(this QueryPropagation<TCross, TEntity>.TransformBuilder builder, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return new(builder.query.Join(to, UnwrapItemPrimaryKey<TCross, TEntity, int>(), TTo.GetPrimaryKey(), (p, s) => new QueryPropagation<TTo, TEntity>(s, p.Entity)));
		}

		public static IQueryable<TTo> FromCrossReferenceForeign<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<QueryPropagation<int, TTo>> FromCrossReferenceForeignAndPropagateKey<TCross, TTo>(this IQueryable<TCross> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, TCross.GetForeignKey(), TTo.GetPrimaryKey(), PropagateItemWith<TCross, int, TTo>(TCross.GetForeignKey()));
		}

		public static IQueryable<QueryPropagation<TItem, TTo>> FromCrossReferenceForeign<TItem, TCross, TTo>(this IQueryable<QueryPropagation<TItem, TCross>> query, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(to, UnwrapEntityForeignKey<TItem, TCross>(), TTo.GetPrimaryKey(), (q, s) => new QueryPropagation<TItem, TTo>(q.Item, s));
		}

		public static QueryPropagation<TTo, TEntity>.TransformBuilder FromCrossReferenceForeign<TCross, TEntity, TTo>(this QueryPropagation<TCross, TEntity>.TransformBuilder builder, IQueryable<TTo> to)
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TTo>
		where TTo : IQueryKeyable<TTo, int> {
			return new(builder.query.Join(to, UnwrapItemForeignKey<TCross, TEntity>(), TTo.GetPrimaryKey(), (p, s) => new QueryPropagation<TTo, TEntity>(s, p.Entity)));
		}
		#endregion

		#region GroupByPropagation
		public static IQueryable<IGrouping<TItem, TEntity>> GroupByPropagation<TItem, TEntity>(this IQueryable<QueryPropagation<TItem, TEntity>> query) {
			return query.GroupBy(p => p.Item, p => p.Entity);
		}

		public static IQueryable<IGrouping<TKey, TEntity>> GroupByPropagation<TItem, TEntity, TKey>(this IQueryable<QueryPropagation<TItem, TEntity>> query, Expression<Func<TItem, TKey>> selector) {
			// Build an expression that wrap around the selector, since the source query is a QueryPropagation
			var parameter = Expression.Parameter(typeof(QueryPropagation<TItem, TEntity>), "p");
			var getItem = UnwrapItem<TItem, TEntity>();
			var invoke = Expression.Invoke(selector, getItem);
			var lambda = Expression.Lambda<Func<QueryPropagation<TItem, TEntity>, TKey>>(invoke, parameter);
			return query.GroupBy(lambda, p => p.Entity);
		}
		#endregion

		public static IQueryable<QueryGroup<TEntity1, TEntity2>> MergeOnPropagations<TItem, TEntity1, TEntity2>(this IQueryable<QueryPropagation<TItem, TEntity1>> firstQuery, IQueryable<QueryPropagation<TItem, TEntity2>> secondQuery) {
			return firstQuery.GroupJoin(secondQuery, q => q.Item, q => q.Item, (q, e) => new QueryGroup<TEntity1, TEntity2>(q.Entity, e.Select(q => q.Entity)));
		}

		public static IQueryable<QueryPropagation<TItem, TEntity>> Propagate<TItem, TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, TItem>> getItem) where TEntity : IQueryKeyable<TEntity, int> {
			return query.Select(PropagateItem(getItem));
		}

		#region ToCrossReference
		public static IQueryable<TCross> ToCrossReferencePrimary<TFrom, TCross>(this IQueryable<TFrom> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<QueryPropagation<int, TCross>> ToCrossReferencePrimaryAndPropagateKey<TFrom, TCross>(this IQueryable<TFrom> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetPrimaryKey(), PropagateItemWith<TFrom, int, TCross>(TFrom.GetPrimaryKey()));
		}

		public static IQueryable<QueryPropagation<TItem, TCross>> ToCrossReferencePrimary<TItem, TFrom, TCross>(this IQueryable<QueryPropagation<TItem, TFrom>> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TFrom> {
			return query.Join(cross, UnwrapEntityPrimaryKey<TItem, TFrom, int>(), TCross.GetPrimaryKey(), (q, s) => new QueryPropagation<TItem, TCross>(q.Item, s));
		}

		public static QueryPropagation<TCross, TEntity>.TransformBuilder ToCrossReferencePrimary<TEntity, TCross>(this QueryPropagation<int, TEntity>.TransformBuilder builder, IQueryable<TCross> cross)
		where TCross : ICrossReference<TCross> {
			return new(builder.query.Join(cross, p => p.Item, TCross.GetPrimaryKey(), (p, s) => new QueryPropagation<TCross, TEntity>(s, p.Entity)));
		}

		public static QueryPropagation<TCross, TEntity>.TransformBuilder ToCrossReferencePrimary<TItem, TEntity, TCross>(this QueryPropagation<TItem, TEntity>.TransformBuilder builder, IQueryable<TCross> cross)
		where TItem : IQueryKeyable<TItem, int>
		where TCross : ICrossReference<TCross>, ICrossReferencePrimary<TItem> {
			return new(builder.query.Join(cross, UnwrapItemPrimaryKey<TItem, TEntity, int>(), TCross.GetPrimaryKey(), (p, s) => new QueryPropagation<TCross, TEntity>(s, p.Entity)));
		}

		public static IQueryable<TCross> ToCrossReferenceForeign<TFrom, TCross>(this IQueryable<TFrom> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetForeignKey(), (_, s) => s);
		}

		public static IQueryable<QueryPropagation<int, TCross>> ToCrossReferenceForeignAndPropagateKey<TFrom, TCross>(this IQueryable<TFrom> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TFrom> {
			return query.Join(cross, TFrom.GetPrimaryKey(), TCross.GetForeignKey(), PropagateItemWith<TFrom, int, TCross>(TFrom.GetPrimaryKey()));
		}

		public static IQueryable<QueryPropagation<TItem, TCross>> ToCrossReferenceForeign<TItem, TFrom, TCross>(this IQueryable<QueryPropagation<TItem, TFrom>> query, IQueryable<TCross> cross)
		where TFrom : IQueryKeyable<TFrom, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TFrom> {
			return query.Join(cross, UnwrapEntityPrimaryKey<TItem, TFrom, int>(), TCross.GetForeignKey(), (q, s) => new QueryPropagation<TItem, TCross>(q.Item, s));
		}

		public static QueryPropagation<TCross, TEntity>.TransformBuilder ToCrossReferenceForeign<TEntity, TCross>(this QueryPropagation<int, TEntity>.TransformBuilder builder, IQueryable<TCross> cross)
		where TCross : ICrossReference<TCross> {
			return new(builder.query.Join(cross, p => p.Item, TCross.GetForeignKey(), (p, s) => new QueryPropagation<TCross, TEntity>(s, p.Entity)));
		}

		public static QueryPropagation<TCross, TEntity>.TransformBuilder ToCrossReferenceForeign<TItem, TEntity, TCross>(this QueryPropagation<TItem, TEntity>.TransformBuilder builder, IQueryable<TCross> cross)
		where TItem : IQueryKeyable<TItem, int>
		where TCross : ICrossReference<TCross>, ICrossReferenceForeign<TItem> {
			return new(builder.query.Join(cross, UnwrapItemPrimaryKey<TItem, TEntity, int>(), TCross.GetForeignKey(), (p, s) => new QueryPropagation<TCross, TEntity>(s, p.Entity)));
		}
		#endregion

		#region TransformWhereKeysMatch
		public static IQueryable<TTo> TransformWhereKeysMatch<TFrom, TTo>(this IQueryable<TFrom> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), (_, s) => s);
		}

		public static IQueryable<QueryPropagation<int, TTo>> TransformWhereKeysMatchAndPropagateKey<TFrom, TTo>(this IQueryable<TFrom> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), PropagateItemWith<TFrom, int, TTo>(TFrom.GetPrimaryKey()));
		}

		public static IQueryable<QueryPropagation<TItem, TTo>> TransformWhereKeysMatch<TItem, TFrom, TTo>(this IQueryable<QueryPropagation<TItem, TFrom>> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, UnwrapEntityPrimaryKey<TItem, TFrom, int>(), TTo.GetPrimaryKey(), (q, s) => new QueryPropagation<TItem, TTo>(q.Item, s));
		}
		#endregion

		#region WhereKeysMatch
		public static IQueryable<TFrom> WhereKeysMatch<TFrom, TTo>(this IQueryable<TFrom> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, TFrom.GetPrimaryKey(), TTo.GetPrimaryKey(), (q, _) => q);
		}

		public static IQueryable<QueryPropagation<TItem, TFrom>> WhereKeysMatch<TItem, TFrom, TTo>(this IQueryable<QueryPropagation<TItem, TFrom>> query, IQueryable<TTo> keys)
		where TFrom : IQueryKeyable<TFrom, int>
		where TTo : IQueryKeyable<TTo, int> {
			return query.Join(keys, UnwrapEntityPrimaryKey<TItem, TFrom, int>(), TTo.GetPrimaryKey(), (q, _) => q);
		}

		public static IQueryable<TFrom> WhereKeysMatch<TFrom, TKey>(this IQueryable<TFrom> query, IEnumerable<TKey> keys)
		where TFrom : IQueryKeyable<TFrom, TKey> {
			return query.Join(keys, TFrom.GetPrimaryKey(), x => x, (q, _) => q);
		}

		public static IQueryable<QueryPropagation<TItem, TFrom>> WhereKeysMatch<TItem, TFrom, TKey>(this IQueryable<QueryPropagation<TItem, TFrom>> query, IEnumerable<TKey> keys)
		where TFrom : IQueryKeyable<TFrom, TKey> {
			return query.Join(keys, UnwrapEntityPrimaryKey<TItem, TFrom, TKey>(), x => x, (q, _) => q);
		}
		#endregion
	}

	public static class QueryPropagationExtensions {
		private static class Cache<T1> {
			public static Expression<Func<T1, int>>? ForeignKey;
		}

		private static class Cache<T1, T2> {
			public static Expression<Func<T1, T2>>? PrimaryKey;
			public static Expression<Func<T1, QueryPropagation<T2, T1>>>? PropagateItem;
			public static Expression<Func<QueryPropagation<T1, T2>, T2>>? Entity;
			public static Expression<Func<QueryPropagation<T1, T2>, T1>>? Item;
			public static Expression<Func<QueryPropagation<T1, T2>, int>>? EntityForeignKey;
			public static Expression<Func<QueryPropagation<T1, T2>, int>>? ItemForeignKey;
		}

		private static class Cache<T1, T2, T3> {
			public static Expression<Func<T1, T3, QueryPropagation<T2, T3>>>? PropagateItemWith;
			public static Expression<Func<QueryPropagation<T1, T2>, T3>>? EntityPrimaryKey;
			public static Expression<Func<QueryPropagation<T1, T2>, T3>>? ItemPrimaryKey;
		}

		public static Expression<Func<TCross, int>> GetForeignKey<TCross>() where TCross : ICrossReference<TCross> {
			ref var cache = ref Cache<TCross>.ForeignKey;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(TCross), "self");
			cache = Expression.Lambda<Func<TCross, int>>(Expression.Invoke(TCross.GetForeignKey(), parameter), parameter);
			return cache;
		}

		public static Expression<Func<TEntity, TKey>> GetPrimaryKey<TEntity, TKey>() where TEntity : IQueryKeyable<TEntity, TKey> {
			ref var cache = ref Cache<TEntity, TKey>.PrimaryKey;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(TEntity), "self");
			cache = Expression.Lambda<Func<TEntity, TKey>>(Expression.Invoke(TEntity.GetPrimaryKey(), parameter), parameter);
			return cache;
		}

		public static Expression<Func<TEntity, QueryPropagation<TKey, TEntity>>> PropagateItem<TEntity, TKey>(Expression<Func<TEntity, TKey>> getKey) {
			ref var cache = ref Cache<TEntity, TKey>.PropagateItem;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(TEntity), "self");
			var invoke = Expression.Invoke(getKey, parameter);
			var ctor = Expression.New(QueryPropagation<TKey, TEntity>.Constructor, invoke, parameter);
			cache = Expression.Lambda<Func<TEntity, QueryPropagation<TKey, TEntity>>>(ctor, parameter);
			return cache;
		}

		public static Expression<Func<TFrom, TTo, QueryPropagation<TKey, TTo>>> PropagateItemWith<TFrom, TKey, TTo>(Expression<Func<TFrom, TKey>> getKey) {
			ref var cache = ref Cache<TFrom, TKey, TTo>.PropagateItemWith;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(TFrom), "self");
			var parameter2 = Expression.Parameter(typeof(TTo), "other");
			var invoke = Expression.Invoke(getKey, parameter);
			var ctor = Expression.New(QueryPropagation<TKey, TTo>.Constructor, invoke, parameter2);
			cache = Expression.Lambda<Func<TFrom, TTo, QueryPropagation<TKey, TTo>>>(ctor, parameter, parameter2);
			return cache;
		}

		public static Expression<Func<QueryPropagation<TItem, TEntity>, TEntity>> UnwrapEntity<TItem, TEntity>() {
			ref var cache = ref Cache<TItem, TEntity>.Entity;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(QueryPropagation<TItem, TEntity>), "self");
			var property = Expression.Property(parameter, QueryPropagation<TItem, TEntity>.EntityProperty);
			cache = Expression.Lambda<Func<QueryPropagation<TItem, TEntity>, TEntity>>(property, parameter);
			return cache;
		}

		public static Expression<Func<QueryPropagation<TItem, TCross>, int>> UnwrapEntityForeignKey<TItem, TCross>() where TCross : ICrossReference<TCross> {
			ref var cache = ref Cache<TItem, TCross>.EntityForeignKey;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(QueryPropagation<TItem, TCross>), "self");
			var property = Expression.Property(parameter, QueryPropagation<TItem, TCross>.EntityProperty);
			cache = Expression.Lambda<Func<QueryPropagation<TItem, TCross>, int>>(Expression.Invoke(TCross.GetForeignKey(), property), parameter);
			return cache;
		}

		public static Expression<Func<QueryPropagation<TItem, TEntity>, TKey>> UnwrapEntityPrimaryKey<TItem, TEntity, TKey>() where TEntity : IQueryKeyable<TEntity, TKey> {
			ref var cache = ref Cache<TItem, TEntity, TKey>.EntityPrimaryKey;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(QueryPropagation<TItem, TEntity>), "self");
			var property = Expression.Property(parameter, QueryPropagation<TItem, TEntity>.EntityProperty);
			cache = Expression.Lambda<Func<QueryPropagation<TItem, TEntity>, TKey>>(Expression.Invoke(TEntity.GetPrimaryKey(), property), parameter);
			return cache;
		}

		public static Expression<Func<QueryPropagation<TItem, TEntity>, TItem>> UnwrapItem<TItem, TEntity>() {
			ref var cache = ref Cache<TItem, TEntity>.Item;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(QueryPropagation<TItem, TEntity>), "self");
			var property = Expression.Property(parameter, QueryPropagation<TItem, TEntity>.ItemProperty);
			cache = Expression.Lambda<Func<QueryPropagation<TItem, TEntity>, TItem>>(property, parameter);
			return cache;
		}

		public static Expression<Func<QueryPropagation<TItem, TEntity>, int>> UnwrapItemForeignKey<TItem, TEntity>() where TItem : ICrossReference<TItem> {
			ref var cache = ref Cache<TItem, TEntity>.ItemForeignKey;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(QueryPropagation<TItem, TEntity>), "self");
			var property = Expression.Property(parameter, QueryPropagation<TItem, TEntity>.ItemProperty);
			cache = Expression.Lambda<Func<QueryPropagation<TItem, TEntity>, int>>(Expression.Invoke(TItem.GetForeignKey(), property), parameter);
			return cache;
		}

		public static Expression<Func<QueryPropagation<TItem, TEntity>, TKey>> UnwrapItemPrimaryKey<TItem, TEntity, TKey>() where TItem : IQueryKeyable<TItem, TKey> {
			ref var cache = ref Cache<TItem, TEntity, TKey>.ItemPrimaryKey;
			if (cache is not null)
				return cache;

			var parameter = Expression.Parameter(typeof(QueryPropagation<TItem, TEntity>), "self");
			var property = Expression.Property(parameter, QueryPropagation<TItem, TEntity>.ItemProperty);
			cache = Expression.Lambda<Func<QueryPropagation<TItem, TEntity>, TKey>>(Expression.Invoke(TItem.GetPrimaryKey(), property), parameter);
			return cache;
		}
	}
}
