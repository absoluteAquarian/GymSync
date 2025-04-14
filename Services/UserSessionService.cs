namespace GymSync.Services {
	public class UserSessionService {
		//reference for user ids across the site
		public int? UserID { get; set; }
		public int? ClientID {get; set;}
		public int TrainerID { get; set; }
		public int StaffID { get; set; }

		//credentials to determine what the user should have access to
		public bool IsUser { get; set; }
		public bool IsClient { get; set; }
		public bool IsTrainer { get; set; }

		public bool IsStaff { get; set; }

	}
}
