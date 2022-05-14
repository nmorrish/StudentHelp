using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using StudentHelpRazor.Entities;
using System.Linq;
using System.Threading.Tasks;
using StudentHelpRazor.Data;

namespace StudentHelpRazor.Areas.Course.Pages
{
    public class CreateModel : PageModel
    {
        private readonly StudenthelpContext _context;
        public CreateModel(StudenthelpContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Entities.Course Course { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
