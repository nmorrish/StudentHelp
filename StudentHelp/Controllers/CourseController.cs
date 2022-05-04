using Microsoft.AspNetCore.Mvc;

namespace StudentHelp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
