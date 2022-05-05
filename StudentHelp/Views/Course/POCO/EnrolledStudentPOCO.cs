using System;

namespace StudentHelp.Views.Course.POCO
{
    public class EnrolledStudentPOCO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? Grade { get; set; }
        public bool IsChecked { get; set; }

    }
}
