using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using StudentHelpRazor.Entities;
using System.Linq;
using StudentHelpRazor.Data;

namespace StudentHelpRazor.Areas.Course.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StudenthelpContext _context;

        public List<Entities.Course> Courses;

        public IndexModel(Data.StudenthelpContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Courses = _context.Course.ToList();
        }
    }
}
