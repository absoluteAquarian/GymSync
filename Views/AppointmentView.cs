using GymSync.Models;

namespace GymSync.Views {
		public record class AppointmentView(string clientName, DateTime start_time, DateTime end_time);	
}
