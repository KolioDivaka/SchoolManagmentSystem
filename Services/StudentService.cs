using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data;
using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Services.Interfaces;

namespace SchoolManagmentSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContex _db;
        public StudentService(AppDbContex db)
        {
            _db = db;
        }
        public async Task<List<Student>> GetAllStudentsAsync()
            => await _db.Students
            .Include(s => s.Grades)
            .ToListAsync();


        public async Task<Student?> GetByIdAsync(int id)
            => await _db.Students
            .Include(s => s.Grades)
            .ThenInclude(g => g.Subject)
            .FirstOrDefaultAsync(s => s.Id == id);
       

        public async Task<Student?> GetByUserIdAsync(string userId) 
            => await _db.Students
            .Include(s => s.Grades).
            ThenInclude(g => g.Subject)
            .FirstOrDefaultAsync(s => s.ApplicationUserId == userId);

        public async Task CreateAsync(Student student)
        {

            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            var yearPart = DateTime.UtcNow.Year % 100;
            student.StudentNumber = $"{yearPart:D2}{student.Id:D3}";

            await _db.SaveChangesAsync();   
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if(student != null) {
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
        }
    }
}
