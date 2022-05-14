using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHelp.Data;
using StudentHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHelp.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudenthelpContext _context;
        private readonly ILogger<HomeController> _logger;

        public StudentController(ILogger<HomeController> logger, StudenthelpContext studenthelpContext)
        {
            _context = studenthelpContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Student> students = _context.Student.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            ViewData["AllStudents"] = _context.Student.ToList();
            ViewData["openModal"] = false;

            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Student");
            }
            else
            {
                ViewData["openModal"] = true;
            }

            return View();
        }

        public IActionResult Delete(Guid id)
        {
            Student student = _context.Student.Where(x => x.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction("Index", "Student");
            }
            else
            {
                return View(student);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            Student student = _context.Student.Where(x => x.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction("Index", "Student");
            }
            else
            {
                _context.Remove(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
