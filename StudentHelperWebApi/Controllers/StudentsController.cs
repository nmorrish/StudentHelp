using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentHelperWebApi.Data;
using StudentHelperWebApi.Data.Entities;
using StudentHelperWebApi.POCO;
using StudentHelperWebApi.Validators;

namespace StudentHelperWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudenthelpContext _context;

        public StudentsController(StudenthelpContext context)
        {
            _context = context;
        }

        // GET: Students
        /// <summary>
        /// The basic 'index' view. Returns a list of all students.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentPOCO>>> GetStudents()
        {
            return await _context.Student.Select(x => new StudentPOCO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                RegistrationDate = x.RegistrationDate,
                StudentId = x.StudentId
            }).ToListAsync();
        }

        /// <summary>
        /// The detail view. Takes in student Id via /student/{id} and returns detail data
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>student detail data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentPOCO>> GetStudent(Guid id)
        {
            var student = await _context.Student.Select(x => new StudentPOCO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                RegistrationDate = x.RegistrationDate,
                StudentId = x.StudentId
            }).FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        /// <summary>
        /// The update command. Requires the student id and object with student data
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="student">student data</param>
        /// <returns>Nothing, but database should be updated if all goes well</returns>
        [HttpPut("{student}")]
        public async Task<IActionResult> PutStudent(StudentPOCO student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            else
            {

                Student modifiedStudent = new()
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    RegistrationDate = student.RegistrationDate,
                    Email = student.Email,
                    StudentId = student.StudentId
                };

                StudentValidator validation = new();

                var result = validation.Validate(modifiedStudent);

                if (result.IsValid && _context.Student.Where(s => s.StudentId == modifiedStudent.StudentId).FirstOrDefault() != null)
                {
                    _context.Entry(modifiedStudent).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }

                    return NoContent();
                }
                else
                {
                    String[] errors = result.Errors.Select(e => e.ErrorMessage).ToArray();
                    return BadRequest();
                }
            }

        }

        /// <summary>
        /// Creates a new student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<StudentPOCO>> PostStudent(StudentPOCO student)
        {

            Student newStudent = new()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                RegistrationDate = student.RegistrationDate,
                Email = student.Email,
                StudentId = new Guid()
            };

            StudentValidator validation = new();

            var result = validation.Validate(newStudent);

            if (result.IsValid)
            {


                _context.Student.Add(newStudent);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetStudent", new { id = newStudent.StudentId }, newStudent);
            }
            else
            {

                String[] errors = result.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest();
            }



        }

        /// <summary>
        /// Delete a student with a given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(Guid id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
