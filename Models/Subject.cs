namespace SchoolManagmentSystem.Models
{
    public class Subject
    {
       public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
