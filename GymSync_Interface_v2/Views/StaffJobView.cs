using GymSync_Interface_v2.Models;

namespace GymSync_Interface_v2.Views
{
    public record class StaffJobView(int StaffID, int UserID, string FirstName, string LastName, string JobName, string JobDescription, float HourlyWage);
}
