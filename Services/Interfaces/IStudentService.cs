using SchoolManagmentSystem.Models;

namespace SchoolManagmentSystem.Services.Interfaces
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudentsAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByUserIdAsync(string userId); 
        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);

    }
}
