using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHelp.Data;
using StudentHelp.Models;
using StudentHelp.Views.Course.DTO;
using StudentHelp.Views.Course.POCO;
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
                ViewData["AllCourses"] = _context.Course.ToList();
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
        public async Task<IActionResult> Index(Course course)
        {
            try
            {
                ViewData["AllCourses"] = _context.Course.ToList();
                ViewData["openModal"] = false;

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
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(Index)}");
                return NotFound();
            }

        }

        public IActionResult Enrollment()
        {
            try
            {
                //TODO: Solve Linq expression could not be translated
                //ViewData["Enrollment"] = (from x in _context.StudentCourse
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

                ViewData["Enrollment"] = (from x in _context.Course
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
                return View();


            }
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(Index)}");
                return NotFound();
            }
        }

        public IActionResult EnrollStudents(int id)
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

            List<int> currentStudentIds = (from x in _context.StudentCourse
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
        public async Task<IActionResult> EnrollStudents(int id, List<EnrolledStudentPOCO> enrolledStudents)
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
