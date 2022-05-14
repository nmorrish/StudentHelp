using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentHelpRazor.Data;
using StudentHelpRazor.Entities;

namespace StudentHelpRazor.Areas.Course.Pages
{
    public class EnrollmentModel : PageModel
    {
        private readonly StudenthelpContext _context;

        public EnrollmentModel(StudenthelpContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Description");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "Email");
            return Page();
        }

        [BindProperty]
        public StudentCourse StudentCourse { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StudentCourse.Add(StudentCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
