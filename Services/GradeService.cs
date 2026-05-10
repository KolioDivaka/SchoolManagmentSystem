using SchoolManagmentSystem.Services.Interfaces;
using SchoolManagmentSystem.Data;
using SchoolManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace SchoolManagmentSystem.Services
{
    public class GradeService : IGradeService
    {
        public readonly AppDbContex _db;
        public GradeService(AppDbContex db)
        {
            _db = db;
        }

      public async Task<Grade?> GetByIdAsync(int id)
            => await _db.Grades
                .Include(g => g.Student)
                .FirstOrDefaultAsync(g => g.Id == id);

        public async Task<List<Grade>> GetAllAsync()
            => await _db.Grades
                .Include(g => g.Student)
                .ToListAsync();
        public async Task<List<Grade>> GetByStudentIdAsync(int studentId)
        => await _db.Grades
            .Include(g => g.Student)
            .Where(g => g.StudentId == studentId)
            .ToListAsync();

        public async Task AddAsync(Grade grade)
        {
            _db.Grades.Add(grade);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var grade = await _db.Grades.FindAsync(id);
            if (grade != null)
            {
                _db.Grades.Remove(grade);
                await _db.SaveChangesAsync();
            }

        }

        public async Task UpdateAsync(Grade grade)
        {
            _db.Grades.Update(grade);
            await _db.SaveChangesAsync();

        }
    }
}
