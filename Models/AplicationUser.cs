using Microsoft.AspNetCore.Identity;

namespace SchoolManagmentSystem.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public Student? Student { get; set; }

        public bool MustChangePassword { get; set; } = true;
    }
}
