using System;

namespace StudentHelp.Views.Assignment.POCO
{
    public class StudentAssignmentPOCO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AssignmentName { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public int? Grade { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DateSubmitted { get; set; }

    }
}
