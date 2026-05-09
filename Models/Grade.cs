using System.Globalization;

namespace SchoolManagmentSystem.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null;

        public decimal Mark { get; set; }
        public string? Term { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}