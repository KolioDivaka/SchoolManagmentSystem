using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystem.ViewModels
{
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        
    }
}