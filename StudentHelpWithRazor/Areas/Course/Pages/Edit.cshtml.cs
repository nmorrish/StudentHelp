using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentHelpRazor.Data;
using StudentHelpRazor.Entities;

namespace StudentHelpRazor.Areas.Course.Pages
{
    public class EditModel : PageModel
    {
        private readonly StudenthelpContext _context;

        public EditModel(StudenthelpContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Entities.Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.CourseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CourseExists(Guid id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
    }
}
