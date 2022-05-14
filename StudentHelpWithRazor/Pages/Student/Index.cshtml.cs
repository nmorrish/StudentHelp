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
    public class IndexModel : PageModel
    {
        private readonly StudenthelpContext _context;

        public IndexModel(StudenthelpContext context)
        {
            _context = context;
        }

        public IList<Entities.Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }
    }
}
