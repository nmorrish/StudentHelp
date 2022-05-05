using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHelp.Data;

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
                return View();
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(Index)}");
                return NotFound();
            }

        }
    }
}
