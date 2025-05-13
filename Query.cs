using GymSync.Data;
using GymSync.Models;
using GymSync.Querying;
using GymSync.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GymSync {
	public class Query(ApplicationDbContext context) {
		private readonly ApplicationDbContext _context = context;

		#region Locking
		private readonly QueryLock _lock = new();

		public QueryLock AcquireLock() => _lock.Acquire();
		#endregion

		#region User Look-up
		public async Task<UserEntity?> UserToUser(int userID) {
			Console.WriteLine("[Query::UserToUser] Looking up user with ID: " + userID);

			return await _context.USER
				.Where(uid => uid.user_id == userID)
				.FirstOrDefaultAsync();
		}
		#endregion

		#region Cross-Reference Queries
		public async Task<ClientEntity?> AppointmentToClient(int appointmentID) {
			Console.WriteLine("[Query::AppointmentToClient] Looking up client for appointment with ID: " + appointmentID);

			return await _context.APPOINTMENT
				.Where(a => a.appointment_id == appointmentID)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ClientEntity>> AppointmentToClientMany(IEnumerable<int> appointmentIDs) {
			Console.WriteLine("[Query::AppointmentToClientMany] Looking up clients for appointments with IDs: " + string.Join(", ", appointmentIDs));

			return await _context.APPOINTMENT
				.WhereKeysMatch(appointmentIDs)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<ClientEntity?> ClientIdToClient(int clientId) {
			Console.WriteLine("[Query::ClientIdToClient] Looking up client with ID: " + clientId);

			return await _context.CLIENT
				.FindAsync(clientId);
				
		}

		public async Task<List<ClientEntity>> AppointmentToClientAll() {
			Console.WriteLine("[Query::AppointmentToClientAll] Looking up all clients for all appointments");

			return await _context.APPOINTMENT
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<AppointmentEntity>> ClientToAppointmentAll(int client_id) {
			Console.WriteLine("[Query::ClientToAppointmentAll] Looking up all appointments for client with ID: " + client_id);

			return await _context.CLIENT
				.Where(c => c.client_id == client_id)
				.ToCrossReferenceForeign(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferencePrimary(_context.APPOINTMENT)
				.ToListAsync();
		}

		public async Task<List<AppointmentEntity>> TrainerToAppointmentAll(int trainer_id) {
			Console.WriteLine("[Query::TrainerToAppointmentAll] Looking up all appointments for trainer with ID: " + trainer_id);

			return await _context.TRAINER
				.Where(t => t.trainer_id == trainer_id)
				.ToCrossReferenceForeign(_context.APPOINTMENT_x_TRAINER)
				.FromCrossReferencePrimary(_context.APPOINTMENT)
				.ToListAsync();
		}

		public async Task<TrainerEntity?> AppointmentToTrainer(int appointmentID) {
			Console.WriteLine("[Query::AppointmentToTrainer] Looking up trainer for appointment with ID: " + appointmentID);

			return await _context.APPOINTMENT
				.Where(a => a.appointment_id == appointmentID)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.FirstOrDefaultAsync();
		}

		public async Task<List<TrainerEntity>> AppointmentToTrainerMany(IEnumerable<int> appointmentIDs) {
			Console.WriteLine("[Query::AppointmentToTrainerMany] Looking up trainers for appointments with IDs: " + string.Join(", ", appointmentIDs));

			return await _context.APPOINTMENT
				.WhereKeysMatch(appointmentIDs)
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<List<TrainerEntity>> AppointmentToTrainerAll() {
			Console.WriteLine("[Query::AppointmentToTrainerAll] Looking up all trainers for all appointments");

			return await _context.APPOINTMENT
				.ToCrossReferencePrimary(_context.APPOINTMENT_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<UserView?> ClientToUser(int clientID) {
			Console.WriteLine("[Query::ClientToUser] Looking up user for client with ID: " + clientID);

			return await _context.CLIENT
				.Where(c => c.client_id == clientID)
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> ClientToUserMany(IEnumerable<int> clientIDs) {
			Console.WriteLine("[Query::ClientToUserMany] Looking up users for clients with IDs: " + string.Join(", ", clientIDs));

			return await _context.CLIENT
				.WhereKeysMatch(clientIDs)
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> ClientToUserAll() {
			Console.WriteLine("[Query::ClientToUserAll] Looking up all users for all clients");

			return await _context.CLIENT
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<ItemEntity?> EquipmentToItem(int equipmentID) {
			Console.WriteLine("[Query::EquipmentToItem] Looking up item for equipment with ID: " + equipmentID);

			return await _context.EQUIPMENT
				.Where(e => e.equipment_id == equipmentID)
				.ToCrossReferencePrimary(_context.EQUIPMENT_x_ITEM)
				.FromCrossReferenceForeign(_context.ITEM)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ItemEntity>> EquipmentToItemMany(IEnumerable<int> equipmentIDs) {
			Console.WriteLine("[Query::EquipmentToItemMany] Looking up items for equipment with IDs: " + string.Join(", ", equipmentIDs));

			return await _context.EQUIPMENT
				.WhereKeysMatch(equipmentIDs)
				.ToCrossReferencePrimary(_context.EQUIPMENT_x_ITEM)
				.FromCrossReferenceForeign(_context.ITEM)
				.ToListAsync();
		}

		public async Task<List<ItemEntity>> EquipmentToItemAll() {
			Console.WriteLine("[Query::EquipmentToItemAll] Looking up all items for all equipment");

			return await _context.EQUIPMENT
				.ToCrossReferencePrimary(_context.EQUIPMENT_x_ITEM)
				.FromCrossReferenceForeign(_context.ITEM)
				.ToListAsync();
		}

		public async Task<List<EquipmentEntity>> EquipmentToEquipmentAll() {
			Console.WriteLine("[Query::EquipmentToEquipmentAll] Looking up all equipment for all items");

			return await _context.EQUIPMENT
				.WhereKeysMatch(_context.EQUIPMENT_x_ITEM)
				.ToListAsync();
		}

		public async Task<JobEntity?> StaffToJob(int staffID) {
			Console.WriteLine("[Query::StaffToJob] Looking up job for staff with ID: " + staffID);

			return await _context.STAFF
				.Where(s => s.staff_id == staffID)
				.ToCrossReferencePrimary(_context.STAFF_x_JOB)
				.FromCrossReferenceForeign(_context.JOB)
				.FirstOrDefaultAsync();
		}

		public async Task<List<JobEntity>> StaffToJobMany(IEnumerable<int> staffIDs) {
			Console.WriteLine("[Query::StaffToJobMany] Looking up jobs for staff with IDs: " + string.Join(", ", staffIDs));

			return await _context.STAFF
				.WhereKeysMatch(staffIDs)
				.ToCrossReferencePrimary(_context.STAFF_x_JOB)
				.FromCrossReferenceForeign(_context.JOB)
				.ToListAsync();
		}

		public async Task<List<JobEntity>> StaffToJobAll() {
			Console.WriteLine("[Query::StaffToJobAll] Looking up all jobs for all staff");

			return await _context.STAFF
				.ToCrossReferencePrimary(_context.STAFF_x_JOB)
				.FromCrossReferenceForeign(_context.JOB)
				.ToListAsync();
		}

		public async Task<UserView?> StaffToUser(int staffID) {
			Console.WriteLine("[Query::StaffToUser] Looking up user for staff with ID: " + staffID);

			return await _context.STAFF
				.Where(s => s.staff_id == staffID)
				.ToCrossReferenceForeign(_context.USER_x_STAFF)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> StaffToUserMany(IEnumerable<int> staffIDs) {
			Console.WriteLine("[Query::StaffToUserMany] Looking up users for staff with IDs: " + string.Join(", ", staffIDs));

			return await _context.STAFF
				.WhereKeysMatch(staffIDs)
				.ToCrossReferenceForeign(_context.USER_x_STAFF)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> StaffToUserAll() {
			Console.WriteLine("[Query::StaffToUserAll] Looking up all users for all staff");

			return await _context.STAFF
				.ToCrossReferenceForeign(_context.USER_x_STAFF)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<UserView?> TrainerToUser(int trainerID) {
			Console.WriteLine("[Query::TrainerToUser] Looking up user for trainer with ID: " + trainerID);

			return await _context.TRAINER
				.Where(t => t.trainer_id == trainerID)
				.ToCrossReferenceForeign(_context.USER_x_TRAINER)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}

		public async Task<List<UserView>> TrainerToUserMany(IEnumerable<int> trainerIDs) {
			Console.WriteLine("[Query::TrainerToUserMany] Looking up users for trainers with IDs: " + string.Join(", ", trainerIDs));

			return await _context.TRAINER
				.WhereKeysMatch(trainerIDs)
				.ToCrossReferenceForeign(_context.USER_x_TRAINER)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<List<UserView>> TrainerToUserAll() {
			Console.WriteLine("[Query::TrainerToUserAll] Looking up all users for all trainers");

			return await _context.TRAINER
				.ToCrossReferenceForeign(_context.USER_x_TRAINER)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<ClientEntity?> UserToClient(int userID) {
			Console.WriteLine("[Query::UserToClient] Looking up client for user with ID: " + userID);

			return await _context.USER
				.Where(u => u.user_id == userID)
				.ToCrossReferencePrimary(_context.USER_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.FirstOrDefaultAsync();
		}

		public async Task<List<ClientEntity>> UserToClientMany(IEnumerable<int> userIDs) {
			Console.WriteLine("[Query::UserToClientMany] Looking up clients for users with IDs: " + string.Join(", ", userIDs));

			return await _context.USER
				.WhereKeysMatch(userIDs)
				.ToCrossReferencePrimary(_context.USER_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<ClientEntity>> UserToClientAll() {
			Console.WriteLine("[Query::UserToClientAll] Looking up all clients for all users");

			return await _context.USER
				.ToCrossReferencePrimary(_context.USER_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToListAsync();
		}

		public async Task<List<ClientEntity>> TrainerClientList(int trainer_id) {
			Console.WriteLine("[Query::TrainerClientList] Looking up clients for trainer with ID: " + trainer_id);

			return await _context.CLIENT
				.Where(c => c.current_trainer_id == trainer_id)
				.ToListAsync();

		}

		public async Task<List<UserView>> GetClientsForTrainer(int trainerID) {
			Console.WriteLine("[Query::GetClientsForTrainer] Looking up clients for trainer with ID: " + trainerID);

			return await _context.APPOINTMENT_x_TRAINER
				.Where(at => at.trainer_id == trainerID)
				.TransformWhereKeysMatch(_context.APPOINTMENT_x_CLIENT)
				.FromCrossReferenceForeign(_context.CLIENT)
				.ToCrossReferenceForeign(_context.USER_x_CLIENT)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.ToListAsync();
		}

		public async Task<StaffEntity?> UserToStaff(int userID) {
			Console.WriteLine("[Query::UserToStaff] Looking up staff for user with ID: " + userID);

			return await _context.USER
				.Where(u => u.user_id == userID)
				.ToCrossReferencePrimary(_context.USER_x_STAFF)
				.FromCrossReferenceForeign(_context.STAFF)
				.FirstOrDefaultAsync();
		}

		public async Task<List<StaffEntity>> UserToStaffMany(IEnumerable<int> userIDs) {
			Console.WriteLine("[Query::UserToStaffMany] Looking up staff for users with IDs: " + string.Join(", ", userIDs));

			return await _context.USER
				.WhereKeysMatch(userIDs)
				.ToCrossReferencePrimary(_context.USER_x_STAFF)
				.FromCrossReferenceForeign(_context.STAFF)
				.ToListAsync();
		}

		public async Task<List<StaffEntity>> UserToStaffAll() {
			Console.WriteLine("[Query::UserToStaffAll] Looking up all staff for all users");

			return await _context.USER
				.ToCrossReferencePrimary(_context.USER_x_STAFF)
				.FromCrossReferenceForeign(_context.STAFF)
				.ToListAsync();
		}

		public async Task<TrainerEntity?> UserToTrainer(int userID) {
			Console.WriteLine("[Query::UserToTrainer] Looking up trainer for user with ID: " + userID);

			return await _context.USER
				.Where(u => u.user_id == userID)
				.ToCrossReferencePrimary(_context.USER_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.FirstOrDefaultAsync();
		}

		public async Task<List<TrainerEntity>> UserToTrainerMany(IEnumerable<int> userIDs) {
			Console.WriteLine("[Query::UserToTrainerMany] Looking up trainers for users with IDs: " + string.Join(", ", userIDs));

			return await _context.USER
				.WhereKeysMatch(userIDs)
				.ToCrossReferencePrimary(_context.USER_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<List<TrainerEntity>> UserToTrainerAll() {
			Console.WriteLine("[Query::UserToTrainerAll] Looking up all trainers for all users");

			return await _context.USER
				.ToCrossReferencePrimary(_context.USER_x_TRAINER)
				.FromCrossReferenceForeign(_context.TRAINER)
				.ToListAsync();
		}

		public async Task<List<UserEntity>> GetAllUsers() {
			Console.WriteLine("[Query::GetAllUsers] Looking up all users");

			return await _context.USER
				.ToListAsync();
		}

		public async Task<List<User_x_TrainerEntity>> GetAllUsersXTrainer() {
			Console.WriteLine("[Query::GetAllUsersXTrainer] Looking up all user/trainer cross references");

			return await _context.USER_x_TRAINER
				.ToListAsync();
		}

		//ASP user lookup
		public async Task<UserView?> AspNetUserToUser(string Email) {
			Console.WriteLine("[Query::AspNetUserToUser] Looking up user for email: " + Email);

			return await _context.USER
				.Where(s => s.email == Email)
				.AsUserView()
				.FirstOrDefaultAsync();
		}
		#endregion

		#region Specialized Queries
		public async Task<List<StaticGroup<UserView, UserView>>> GetClientsForTrainerAll() {
			Console.WriteLine("[Query::GetClientsForTrainerAll] Looking up all clients for all trainers");

			// TODO: Return empty client lists for trainers with no clients?
			return await _context.TRAINER
				.ToCrossReferenceForeign(_context.APPOINTMENT_x_TRAINER)
				// Resolve the trainer user for each appointment
				.AnonymousMergeCrossReferenceForeign(_context.USER_x_TRAINER, at => at.trainer_id, (at, ut) => new { at.appointment_id, at.trainer_id, ut.user_id })
				.AnonymousMergeWhereMatchesKeys(_context.USER, anon => anon.user_id, (anon, u) => new { anon.appointment_id, anon.trainer_id, TrainerUser = new UserView(u.user_id, u.firstName, u.lastName, u.email) })
				// Resolve the client user for each appointment
				.AnonymousMergeCrossReferencePrimary(_context.APPOINTMENT_x_CLIENT, anon => anon.appointment_id, (anon, ac) => new { anon.trainer_id, anon.TrainerUser, ac.client_id })
				.AnonymousWhereMatchesKeys(_context.CLIENT, anon => anon.client_id)
				.AnonymousMergeWhereMatchesKeys(_context.USER_x_CLIENT, anon => anon.client_id, (anon, uc) => new { anon.trainer_id, anon.TrainerUser, uc.user_id })
				.AnonymousMergeWhereMatchesKeys(_context.USER, anon => anon.user_id, (anon, u) => new { anon.trainer_id, anon.TrainerUser, ClientUser = new UserView(u.user_id, u.firstName, u.lastName, u.email) })
				// Group the clients by their trainer
				.GroupBy(anon => anon.trainer_id, anon => new { anon.TrainerUser, anon.ClientUser })
				// Convert from server-sided evaluation (SQL) to client-sided evaluation (C#)
				// The group transformations should be handled client-side since doing them server-side results in absurdly large and complex queries
				.AsAsyncEnumerable()
				.TransformGroupsByElement(anon => anon.TrainerUser, anon => anon.ClientUser)
				.ToListAsync();
		}

		public async Task<List<EquipmentView>> GetEquipmentAll() {
			Console.WriteLine("[Query::GetEquipmentAll] Looking up all equipment");

			return await _context.EQUIPMENT
				// Resolve the equipment for each reference
				.MergeWithCrossReferencePrimary(_context.EQUIPMENT_x_ITEM, (e, ei) => new { ei.item_id, e.equipment_id, e.location_name, e.last_maintenance, e.in_use })
				// Resolve the item for each equipment
				.AnonymousMergeWhereMatchesKeys(_context.ITEM, anon => anon.item_id, (anon, i) => new EquipmentView(anon.equipment_id, i.item_name, anon.location_name, anon.last_maintenance, anon.in_use))
				.ToListAsync();
		}

		public async Task<List<StaffJobView>> GetUserAndJobForStaffAll() {
			// This method allows missing columns when joining the info from the JOB table since a staff member may not have a job
			Console.WriteLine("[Query::GetUserAndJobForStaffAll] Looking up all staff and their jobs");

			return await _context.STAFF
				// Resolve the job for each staff member.  If the job doesn't exist, the job info will be null
				.MergeWithCrossReferencePrimaryAllowNull(_context.STAFF_x_JOB, (s, sj) => new { s.staff_id, STAFFxJOB = sj })
				.AnonymousMergeWhereMatchesKeysAllowNull(_context.JOB, anon => anon.STAFFxJOB.job_id, (anon, j) => new { anon.staff_id, Job = j })
				// Resolve the user for each staff member
				.AnonymousMergeCrossReferenceForeign(_context.USER_x_STAFF, anon => anon.staff_id, (anon, us) => new { anon.staff_id, anon.Job, us.user_id })
				.AnonymousMergeWhereMatchesKeys(_context.USER, anon => anon.user_id, (anon, u) => new { anon.staff_id, anon.Job, User = new UserView(u.user_id, u.firstName, u.lastName, u.email) })
				// Convert from server-sided evaluation (SQL) to client-sided evaluation (C#)
				// The job columns have to be resolved client-side since expression trees don't support null-coalescing / null-propagation operators
				.AsAsyncEnumerable()
				.Select(anon => new StaffJobView(anon.staff_id, anon.User.UserID, anon.User.FirstName, anon.User.LastName, anon.Job?.job_name ?? "No job assigned", anon.Job?.job_description ?? "", anon.Job?.hourly_wage ?? 0))
				.ToListAsync();
		}

		public async Task<UserRolesView?> GetUserRolesForUser(int userID) {
			// This method allows missing columns when joining the various cross reference tables, since a user may not have a given role
			Console.WriteLine("[Query::GetUserRolesForUser] Looking up user roles for user with ID: " + userID);

			return await _context.USER
				.Where(u => u.user_id == userID)
				// Resolve the client role for the user.  If the user isn't a client, the client info will be null
				.MergeWithCrossReferencePrimaryAllowNull(_context.USER_x_CLIENT, (u, uc) => new { USER = u, USERxCLIENT = uc })
				.AnonymousMergeWhereMatchesKeysAllowNull(_context.CLIENT, anon => anon.USERxCLIENT.client_id, (anon, c) => new { anon.USER, CLIENT = c })
				// Resolve the trainer role for the user.  If the user isn't a trainer, the trainer info will be null
				.AnonymouseMergeCrossReferencePrimaryAllowNull(_context.USER_x_TRAINER, anon => anon.USER.user_id, (anon, ut) => new { anon.USER, anon.CLIENT, USERxTRAINER = ut })
				.AnonymousMergeWhereMatchesKeysAllowNull(_context.TRAINER, anon => anon.USERxTRAINER.trainer_id, (anon, t) => new { anon.USER, anon.CLIENT, TRAINER = t })
				// Resolve the staff role for the user.  If the user isn't a staff member, the staff info will be null
				.AnonymouseMergeCrossReferencePrimaryAllowNull(_context.USER_x_STAFF, anon => anon.USER.user_id, (anon, us) => new { anon.USER, anon.CLIENT, anon.TRAINER, USERxSTAFF = us })
				.AnonymousMergeWhereMatchesKeysAllowNull(_context.STAFF, anon => anon.USERxSTAFF.staff_id, (anon, s) => new { anon.USER, anon.CLIENT, anon.TRAINER, STAFF = s })
				// Convert from server-sided evaluation (SQL) to client-sided evaluation (C#)
				// The role columns have to be resolved client-side since expression trees don't support null-coalescing / null-propagation operators
				.AsAsyncEnumerable()
				.Select(anon => new UserRolesView(new UserView(anon.USER.user_id, anon.USER.firstName, anon.USER.lastName, anon.USER.email), anon.CLIENT, anon.TRAINER, anon.STAFF))
				.FirstOrDefaultAsync();
		}

		public async Task<List<AppointmentView>> GetAppointmentsForClient(int clientID) {
			Console.WriteLine("[Query::GetAppointmentsForClient] Looking up appointments for client with ID: " + clientID);

			return await _context.CLIENT
				.Where(c => c.client_id == clientID)
				// Resolve the user for the client
				.MergeWithCrossReferenceForeign(_context.USER_x_CLIENT, (c, uc) => new { c.client_id, uc.user_id })
				.AnonymousMergeWhereMatchesKeys(_context.USER, anon => anon.user_id, (anon, u) => new { anon.client_id, User = new UserView(u.user_id, u.firstName, u.lastName, u.email) })
				// Resolve the appointments for the client
				.AnonymousMergeCrossReferenceForeign(_context.APPOINTMENT_x_CLIENT, anon => anon.client_id, (anon, ac) => new { anon.User, ac.appointment_id })
				.AnonymousMergeWhereMatchesKeys(_context.APPOINTMENT, anon => anon.appointment_id, (anon, a) => new AppointmentView(anon.User.FirstName + " " + anon.User.LastName, a.start_time, a.end_time))
				.ToListAsync();
		}

		public async Task<UserView?> GetAssignedTrainerForClient(int clientID) {
			Console.WriteLine("[Query::GetAssignedTrainerForClient] Looking up assigned trainer for client with ID: " + clientID);

			return await _context.CLIENT
				.Where(c => c.client_id == clientID)
				// Resolve the trainer for the client
				.Select(c => c.current_trainer_id)
				.AnonymousMergeCrossReferenceForeign(_context.USER_x_TRAINER, c => c, (c, ut) => ut)
				.FromCrossReferencePrimary(_context.USER)
				.AsUserView()
				.FirstOrDefaultAsync();
		}
		#endregion
	}

	public static partial class QueryExtensions {
		public static async Task<T> AllowNull<T>(this Task<T?> task) => (await task)!;

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

		public static IQueryable<TAnonymous> AnonymousWhereMatchesForeignKeys<TAnonymous, TCross>(this IQueryable<TAnonymous> query, IQueryable<TCross> cross, Expression<Func<TAnonymous, int>> anonymousKeySelector)
		where TCross : ICrossReference<TCross> {
			return query.Join(cross, anonymousKeySelector, TCross.GetForeignKey(), (q, _) => q);
		}

		public static IQueryable<UserView> AsUserView(this IQueryable<UserEntity> query) {
			return query.Select(u => new UserView(u.user_id, u.firstName, u.lastName, u.email));
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
