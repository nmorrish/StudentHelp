using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentHelp.Data;
using StudentHelp.Models;
using StudentHelp.Views.Assignment.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHelp.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly StudenthelpContext _context;
        private readonly ILogger<HomeController> _logger;

        public AssignmentController(ILogger<HomeController> logger, StudenthelpContext studenthelpContext)
        {
            _context = studenthelpContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                ViewData["Assignments"] = (from x in _context.Assignment
                                           select new AssignmentPOCO
                                           {
                                               AssignedDate = x.AssignedDate,
                                               AssignmentId = x.AssignmentId,
                                               CourseId = x.CourseId,
                                               CourseName = x.Course.Name,
                                               Description = x.Course.Description,
                                               DueDate = x.DueDate,
                                               Name = x.Name,
                                           }).ToList();
                ViewData["Courses"] = new SelectList(_context.Course, "CourseId", "Name", "Please select");
                ViewData["openModal"] = false;
                return View();
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(Index)}");
                return NotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                ViewData["openModal"] = true;
                ViewData["Assignments"] = ViewData["Assignments"] = (from x in _context.Assignment
                                                                     select new AssignmentPOCO
                                                                     {
                                                                         AssignedDate = x.AssignedDate,
                                                                         AssignmentId = x.AssignmentId,
                                                                         CourseId = x.CourseId,
                                                                         CourseName = x.Course.Name,
                                                                         Description = x.Course.Description,
                                                                         DueDate = x.DueDate,
                                                                         Name = x.Name,
                                                                     }).ToList();
                ViewData["Courses"] = new SelectList(_context.Course, "CourseId", "Name", "Please select");
                return View();
            }
            else
            {
                Assignment newAssignment = new()
                {
                    Name = assignment.Name,
                    AssignedDate = assignment.AssignedDate,
                    CourseId = assignment.CourseId,
                    Description = assignment.Description,
                    DueDate = assignment.DueDate,
                };

                _context.Add(newAssignment);
                await _context.SaveChangesAsync();

                List<int> studentsInCourse = await _context.StudentCourse.Where(x => x.CourseId == assignment.CourseId).Select(x => x.StudentId).ToListAsync();

                foreach (var studentId in studentsInCourse)
                {
                    StudentAssignment studentAssignment = new()
                    {
                        AssignmentId = assignment.AssignmentId,
                        Grade = null,
                        DateSubmitted = null,
                        StudentId = studentId,
                    };
                    _context.Add(studentAssignment);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Assignment");
            }
        }

        public IActionResult StudentAssignments(int id)
        {
            ViewData["StudentAssignments"] = (from x in _context.StudentAssignment
                                              where x.AssignmentId == id
                                              select new StudentAssignmentPOCO
                                              {
                                                  AssignmentId = x.AssignmentId,
                                                  AssignmentName = x.Assignment.Name,
                                                  DateSubmitted = x.DateSubmitted,
                                                  DueDate = x.Assignment.DueDate,
                                                  FirstName = x.Student.FirstName,
                                                  LastName = x.Student.LastName,
                                                  Grade = x.Grade,
                                                  StudentId = x.StudentId
                                              }).ToList();
            return View();
        }
    }
}
