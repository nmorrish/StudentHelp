using System;

namespace StudentHelp.Views.Assignment.POCO
{
    public class AssignmentPOCO
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
