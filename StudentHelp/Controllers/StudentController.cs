using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHelp.Data;
using StudentHelp.Models;
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
            try
            {
                ViewData["AllStudents"] = _context.Student.ToList();
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
        public async Task<IActionResult> Index(Student student)
        {
            try
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
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(Index)}");
                return NotFound();
            }

        }

        public IActionResult Delete(int id)
        {
            try
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
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(Delete)}");
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
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
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(DeleteConfirmed)}");
                return NotFound();
            }
        }
    }
}
