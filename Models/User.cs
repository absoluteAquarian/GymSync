namespace GymSync.Components.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; } = "temp@tempemail.com";
        public string Password { get; set; } = "tempPassword";
        public string FirstName { get; set; } = "First";
        public string LastName { get; set; } = "Last";
        public DateTime? RegistrationDate { get; set; }
    }
}
