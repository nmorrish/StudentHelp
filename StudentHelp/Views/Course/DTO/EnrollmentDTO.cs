using StudentHelp.Models;
using StudentHelp.Views.Course.POCO;
using System;
using System.Collections.Generic;

namespace StudentHelp.Views.Course.DTO
{
    public class EnrollmentDTO
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public List<EnrolledStudentPOCO> Students { get; set; }
    }
}
