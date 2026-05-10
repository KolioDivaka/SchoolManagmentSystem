using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagmentSystem.Models;
using SchoolManagmentSystem.Services.Interfaces;
using SchoolManagmentSystem.ViewModels;
using System.Runtime.CompilerServices;

namespace SchoolManagmentSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GradesController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;

        public GradesController(IStudentService studentService, IGradeService gradeService, ISubjectService subjectService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var grades = await _gradeService.GetAllAsync();
            return View(grades);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new GradesFormViewModel();
            await PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradesFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm );
                return View(vm);
            }

            var grade = new Grade
            {
                StudentId = vm.StudentId,
                SubjectId = vm.SubjectId,
                Mark = vm.Mark,
                Term = vm.Term
            };

            await _gradeService.AddAsync(grade);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            if (grade == null) return NotFound();
            var vm = new GradesFormViewModel
            {
                Id = grade.Id,
                StudentId = grade.StudentId,
                SubjectId = grade.SubjectId,
                Mark = grade.Mark,
                Term = grade.Term
            };
            await PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GradesFormViewModel vm, int id)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
                return View(vm);
            }
            
            var grade = await _gradeService.GetByIdAsync(id);
            if(grade == null) return NotFound();
            
            grade.StudentId = vm.StudentId;
            grade.SubjectId = vm.SubjectId; 
            grade.Mark = vm.Mark;
            grade.Term = vm.Term;
            
            await _gradeService.UpdateAsync(grade);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gradeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        private async Task PopulateDropdowns(GradesFormViewModel vm)
        {
            var students = await _studentService.GetAllStudentsAsync();
            var subjects = await _subjectService.GetAllAsync();

            vm.Students = students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.FullName
            }).ToList();

            vm.Subjects = subjects.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
        }
       

    }
}
