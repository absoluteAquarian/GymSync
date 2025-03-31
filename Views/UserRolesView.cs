using GymSync.Models;

namespace GymSync.Views {
	public record class UserRolesView(UserView User, ClientEntity? Client, TrainerEntity? Trainer, StaffEntity? Staff);
}
