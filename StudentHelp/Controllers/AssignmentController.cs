using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentHelp.Data;
using StudentHelp.Models;
using StudentHelp.Views.Assignment.POCO;
using System;
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
            List<AssignmentPOCO> assignments = (from x in _context.Assignment
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
            return View(assignments);
        }

        public IActionResult Create()
        {
            ViewData["Courses"] = new SelectList(_context.Course, "CourseId", "Name", "Please select");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Courses"] = new SelectList(_context.Course, "CourseId", "Name", "Please select");
                return View(assignment);
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
                    AssignmentId = Guid.NewGuid()
                };

                _context.Add(newAssignment);

                List<Guid> studentsInCourse = _context.StudentCourse.Where(x => x.CourseId == assignment.CourseId).Select(x => x.StudentId).ToList();

                foreach (var studentId in studentsInCourse)
                {
                    StudentAssignment studentAssignment = new()
                    {
                        AssignmentId = newAssignment.AssignmentId,
                        Grade = null,
                        DateSubmitted = null,
                        StudentId = studentId,
                    };
                    _context.Add(studentAssignment);
                    _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Assignment");
            }
        }

        public IActionResult StudentAssignments(Guid id)
        {
            List<StudentAssignmentPOCO> StudentAssignments = (from x in _context.StudentAssignment
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
            return View(StudentAssignments);
        }
    }
}
