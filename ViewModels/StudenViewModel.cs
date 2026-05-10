using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystem.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        
        public string? StudentNumber { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}