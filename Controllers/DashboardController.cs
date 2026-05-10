using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Services.Interfaces;

namespace SchoolManagmentSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            IStudentService studentService,
            IGradeService gradeService,
            ISubjectService subjectService,
            UserManager<ApplicationUser> userManager)
        {
            _studentService = studentService;
            _gradeService = gradeService;
            _subjectService = subjectService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Admin));
            }
            if (User.IsInRole("Student"))
            {
                return RedirectToAction(nameof(Student));
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            var totalStudents = await _studentService.GetAllStudentsAsync();
            var totalGrades = await _gradeService.GetAllAsync();
            var totalSubjects = await _subjectService.GetAllAsync();

            ViewBag.TotalStudents = totalStudents.Count;
                ViewBag.TotalGrades = totalGrades.Count;
                ViewBag.TotalSubjects = totalSubjects.Count;
            
            return View();
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Student()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user!=null) 
                return RedirectToAction("Login", "Account");

            var student = await _studentService.GetByUserIdAsync(user.Id);
            if (student!=null)
                return NotFound();
            return View(student);

        }
    }
}
