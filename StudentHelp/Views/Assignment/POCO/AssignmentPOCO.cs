using System;

namespace StudentHelp.Views.Assignment.POCO
{
    public class AssignmentPOCO
    {
        public Guid AssignmentId { get; set; }
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
