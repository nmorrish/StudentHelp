using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentHelpRazor.POCO;
using StudentHelpRazor.Data;
using StudentHelpRazor.Entities;

namespace StudentHelpRazor.Areas.Course.Pages
{
    public class DetailModel : PageModel
    {
        private readonly StudenthelpContext _context;

        public DetailModel(StudenthelpContext context)
        {
            _context = context;
        }

        public Entities.Course Course { get; set; }
        public List<StudentCoursePOCO> Students { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.CourseId == id);
            Students = await _context.StudentCourse.Where(x => x.CourseId == id).Select(x => new StudentCoursePOCO
            {
                FirstName = x.Student.FirstName,
                LastName = x.Student.LastName,
                Email = x.Student.Email,
                Grade = x.Grade,
                RegistrationDate = x.Student.RegistrationDate,
                StudentId = x.StudentId
            }).ToListAsync();

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
