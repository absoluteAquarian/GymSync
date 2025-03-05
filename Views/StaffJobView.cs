using GymSync.Models;

namespace GymSync.Views
{
    public record class StaffJobView(int UserID, string FirstName, string LastName, string JobName, string JobDescription, float HourlyWage);
}
