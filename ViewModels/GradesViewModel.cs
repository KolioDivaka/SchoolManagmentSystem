using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystem.ViewModels
{
    public class GradesFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        [Range(2,6)]
        public decimal Mark { get; set; }

        public string? Term { get; set; }

        public List<SelectListItem> Students { get; set; } = new ();
         public List<SelectListItem> Subjects { get; set; } = new ();


    }
}
