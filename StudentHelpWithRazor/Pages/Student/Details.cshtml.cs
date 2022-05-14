using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentHelpRazor.Data;
using StudentHelpRazor.Entities;

namespace StudentHelpRazor.Pages.Student
{
    public class DetailsModel : PageModel
    {
        private readonly StudenthelpContext _context;

        public DetailsModel(StudenthelpContext context)
        {
            _context = context;
        }

        public Entities.Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.StudentId == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
