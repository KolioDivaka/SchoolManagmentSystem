using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data;
using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Services.Interfaces;
namespace SchoolManagmentSystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly AppDbContex _db;
        public SubjectService(AppDbContex db)
        {
            _db = db;
        }

        public async Task<List<Subject>> GetAllAsync() => await _db.Subjects.ToListAsync();

        public async Task<Subject?> GetByIdAsync(int id) => await _db.Subjects.FindAsync(id);

        public async Task AddAsync(Subject subject)
        {
            _db.Subjects.Add(subject);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var subject = await _db.Subjects.FindAsync(id);
            if (subject is not null)
            {
                _db.Subjects.Remove(subject);
                await _db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Subject subject)
        {
            _db.Subjects.Update(subject);
            await _db.SaveChangesAsync();
        }
    }

}
