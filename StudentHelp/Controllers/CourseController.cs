using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHelp.Data;
using StudentHelp.Models;
using StudentHelp.Views.Course.DTO;
using StudentHelp.Views.Course.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHelp.Controllers
{
    public class CourseController : Controller
    {
        private readonly StudenthelpContext _context;
        private readonly ILogger<HomeController> _logger;

        public CourseController(ILogger<HomeController> logger, StudenthelpContext studenthelpContext)
        {
            _context = studenthelpContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                List<Course> courses = _context.Course.ToList();
                return View(courses);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(Index)}");
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {

            if (ModelState.IsValid)
            {
                //TODO: Add user friendly logic for preventing duplicate entries
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Course");
            }
            else
            {
                ViewData["openModal"] = true;
            }

            return View();
        }

        public IActionResult Update(Guid id)
        {
            Course course = _context.Course.Where(x => x.CourseId == id).FirstOrDefault();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Course course)
        {
            _context.Update(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Course");
        }

        public IActionResult Delete(Guid id)
        {
            Course course = _context.Course.Where(x => x.CourseId == id).FirstOrDefault();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(Course course)
        {
            List<StudentCourse> studentsInCourse = _context.StudentCourse.Where( x=> x.CourseId == course.CourseId).ToList();
            List<Assignment> courseAssignments = _context.Assignment.Where(x => x.CourseId == course.CourseId).ToList();
            List<StudentAssignment> studentAssignments = _context.StudentAssignment.Where( x => courseAssignments.Contains(x.Assignment)).ToList();

            foreach (StudentAssignment studentAssignment in studentAssignments)
            {
                _context.Remove(studentAssignment);
            }
            foreach (Assignment assignment in courseAssignments)
            {
                _context.Remove(assignment);
            }
            foreach (StudentCourse studentCourse in studentsInCourse)
            {
                _context.Remove(studentCourse);
            }

            _context.Remove(course);

            _context.SaveChanges();

            return RedirectToAction("Index", "Course");
        }

        public IActionResult Enrollment()
        {

            // TODO: Solve? Linq expression could not be translated
            //var enrollment1 = (from x in _context.StudentCourse
            //                          group x by x.Course into xgroup
            //                          select new EnrollmentDTO
            //                          {
            //                              CourseId = xgroup.Key.CourseId,
            //                              CourseName = xgroup.Key.Name,
            //                              Students = (from y in xgroup
            //                                          select new EnrolledStudentPOCO
            //                                          {
            //                                              StudentId = y.StudentId,
            //                                              CompletionDate = y.CompletionDate,
            //                                              EnrolledDate = y.EnrolledDate,
            //                                              Grade = y.Grade,
            //                                              StudentName = y.Student.FirstName + " " + y.Student.LastName,
            //                                          }).ToList(),
            //                          }).ToList();

            var enrollment2 = (from x in _context.Course
                               select new EnrollmentDTO
                               {
                                   CourseId = x.CourseId,
                                   CourseName = x.Name,
                                   Students = (from y in _context.StudentCourse
                                               where y.CourseId == x.CourseId
                                               select new EnrolledStudentPOCO
                                               {
                                                   StudentId = y.StudentId,
                                                   CompletionDate = y.CompletionDate,
                                                   EnrolledDate = y.EnrolledDate,
                                                   Grade = y.Grade,
                                                   StudentName = y.Student.FirstName + " " + y.Student.LastName,
                                               }).ToList()
                               }).ToList();
            ViewData["Enrollment"] = enrollment2;
            return View();
        }

        public IActionResult EnrollStudents(Guid id)
        {
            ViewData["Course"] = _context.Course.Where(x => x.CourseId == id).FirstOrDefault();
            List<EnrolledStudentPOCO> currentStudents = (from x in _context.StudentCourse
                                                         where x.CourseId == id
                                                         select new EnrolledStudentPOCO
                                                         {
                                                             StudentId = x.StudentId,
                                                             CompletionDate = x.CompletionDate,
                                                             EnrolledDate = x.EnrolledDate,
                                                             Grade = x.Grade,
                                                             StudentName = x.Student.FirstName + " " + x.Student.LastName,
                                                         }).ToList();

            ViewData["EnrolledStudents"] = currentStudents;

            List<Guid> currentStudentIds = (from x in _context.StudentCourse
                                            where x.CourseId == id
                                            select x.StudentId).ToList();

            List<EnrolledStudentPOCO> unenrolledStudents = (from x in _context.Student
                                                            where !currentStudentIds.Contains(x.StudentId)
                                                            select new EnrolledStudentPOCO
                                                            {
                                                                IsChecked = false,
                                                                StudentName = x.FirstName + " " + x.LastName,
                                                                StudentId = x.StudentId
                                                            }).ToList();

            return View(unenrolledStudents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollStudents(Guid id, List<EnrolledStudentPOCO> enrolledStudents)
        {
            foreach (var student in enrolledStudents)
            {
                if (student.IsChecked)
                {
                    StudentCourse studentCourse = new()
                    {
                        StudentId = student.StudentId,
                        CourseId = id,
                        EnrolledDate = System.DateTime.Now,
                        CompletionDate = null,
                        Grade = null
                    };

                    _context.Add(studentCourse);

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("EnrollStudents", new { id });
        }
    }
}
