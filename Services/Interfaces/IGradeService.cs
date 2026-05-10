using SchoolManagmentSystem.Models;

namespace SchoolManagmentSystem.Services.Interfaces
{
    public interface IGradeService
    {
        Task<List<Grade>> GetAllAsync();
        Task<List<Grade>> GetByStudentIdAsync(int studentId);
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(int id);
        Task<Grade?> GetByIdAsync(int id);
    }
}
